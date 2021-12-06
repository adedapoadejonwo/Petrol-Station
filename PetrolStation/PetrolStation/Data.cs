using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Timers;
using System;
using System.IO;

namespace PetrolStation
{
    class Data
    {
        //these are the properties for the Data class
        private static Timer timer;
        public static List<Vehicle> vehicles;
        public static List<Pump> pumps;
        public static  List<transaction> listOfTransactions = new List<transaction>();
        public static List<Vehicle> listOfServicedCars;
        public static List<Vehicle> CarsThatLeft;

        private static Random random = new Random();// a object of the random class is created.

        private const int QUEUE_LIMIT = 5; //this is the limit of vehicles allowed to wait in the queue


        /// <summary>
        /// This method is used to call the InitialisePumps and the InitialiseVehicles Methods
        /// </summary>
        public static void Initialise()
        {
            InitialisePumps();
            InitialiseVehicles();           
        }

        /// <summary>
        /// This method is used to initialise the vehicles, it sets a timer for vehicle creation so that a vehicle is created every time the interval has been elapsed
        /// </summary>
        private static void InitialiseVehicles()
        {
            vehicles = new List<Vehicle>();

            // https://msdn.microsoft.com/en-us/library/system.timers.timer(v=vs.71).aspx
            timer = new Timer();
            timer.Interval = random.Next(1500,2200);// this is a random interval between 1500 and 2200 milliseconds 
            timer.AutoReset = true; 
            timer.Elapsed += CreateVehicle; //when the interval has elapsed the CreateVehicle Method is called
            timer.Enabled = true;
            timer.Start(); // the timer has started
                                 
        }


        /// <summary>       
        /// This method was supposed to be used to remove a vehicle form the queue after its waiting limit has passed
        /// but wasnt used due to bugs during the development cycle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void remonveFromQueue(object sender, ElapsedEventArgs e)
        {            
            
            if (vehicles.Count == 0) { return; }

            Vehicle v;
            v = vehicles[0];

            if(v.InQueue == true)
            {
                vehicles.Remove(v);

                CarsThatLeft.Add(v);
            }
            
        }

        /// <summary>
        /// This method creates a vehicle and adds it to the queue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CreateVehicle(object sender, ElapsedEventArgs e)
        {
            Pump p;         
            Vehicle v = new Vehicle();// a new vehicle object is created

            CarsThatLeft = new List<Vehicle>();

            if (vehicles.Count == QUEUE_LIMIT)// this if statement stops the creation of vehicles if the amount of cars in line is at the limit
            {
                return;
            }
            
            //These if and else iff statements assign a tank capacity to a vehicle depending on the type of vehicle
            // and the restrict the types of fuel they can have and use random to create vehicles with a random amount of fuel in their tanks
            if (v.VehicleType == "Car")
            {
                v.TankCapacity = 40.0;
                v.FuelType = v.typesOfFuel[random.Next(0, 3)];
                v.FuelInTank = (double)random.Next(0, 10)*random.NextDouble();//https://stackoverflow.com/questions/1064901/random-number-between-2-double-numbers
            }
            else if (v.VehicleType == "HGV")
            {
                v.TankCapacity = 150.0;
                v.FuelType = "Diesel";
                v.FuelInTank = (double)random.Next(0,37)*random.NextDouble();

            }
            else if(v.VehicleType == "Van")
            {
                v.TankCapacity = 80.0;
                v.FuelInTank = (double)random.Next(0, 20) * random.NextDouble();
                v.FuelType = v.typesOfFuel[random.Next(0,2)];

            }

            vehicles.Add(v);// object v is added to the vehicles list

            
            //Timer timer2 = new Timer();
            //timer2.Interval = v.QueueWaitingTime;
            //timer2.AutoReset = false;
            //timer2.Elapsed += remonveFromQueue;
            //timer2.Enabled = true;

            //for (int i = 0; i < 9; i++)
            //{
            //    p = pumps[i];

             //   if (p.IsAvailable() == false)
             //   {
             //       v = vehicles[0];
             //       v.InQueue = true;
             //       timer2.Start();
             //
             //   }
            //}
            
                                           
            short runNextAfter = (short)random.Next(1500, 2200); //this creates a random time for the vehicles to be created
            timer.Interval = runNextAfter; // this sets the timer's interval
        }
        
        /// <summary>
        /// This method creates the pumps
        /// </summary>
        private static void InitialisePumps()
        {
            pumps = new List<Pump>();

            Pump p;

            for (int i = 0; i < 9; i++)// this for loop is used to create 9 pumps and adds them to the "pumps" list
            {
                p = new Pump();
                pumps.Add(p);
            }
        }
      
        /// <summary>
        /// this method checks to see if a pump is open and assigns a vehicle to it when this is true
        /// </summary>
        public static void AssignVehicleToPump()
        {
            Vehicle v;
            Pump p;

            listOfServicedCars = new List<Vehicle>();

            if (vehicles.Count == 0) { return; }
          
            for (int i = 0; i < 9; i++)
            {
                p = pumps[i];

                
                if (p.IsAvailable())// checks each pump to see if it is available 
                {
                    v = vehicles[0]; // get first vehicle
                    vehicles.RemoveAt(0); // remove vehicles from queue
                    p.AssignVehicle(v); // assign it to the pump
                    listOfServicedCars.Add(v); //this adds the vehicle to the list of serviced cars
                    v.AssignedPump = p.PumpID;
                }

                if (vehicles.Count == 0) { break; }//if there are no vehicles waiting then nothing is assigned to the pump

            }
        }



    }
}
