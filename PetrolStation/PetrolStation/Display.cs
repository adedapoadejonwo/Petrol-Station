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
    class Display
    {
        /// <summary>
        /// This Method is used to show the queue of vehicles waiting to be serviced. 
        /// the for loop writes to the console and displays each vehicle in the queue, showing their carID, Fuel type and vehicle type.
        /// </summary>
        public static void DrawVehicles()
        {
            Vehicle v; // An object of the Vehicle class is created

            Console.WriteLine("Vehicles Queue:");

            for (int i = 0; i < Data.vehicles.Count; i++) 
            {
                v = Data.vehicles[i]; //assigns each of vehicle in the vehicles array to the object v
                Console.Write("#{0} Fuel: {1}, Vehicle: {2}| ", v.CarID, v.FuelType, v.VehicleType);// writes for every vehicle in the queue
            }
        }

        /// <summary>
        /// This Method Draws all of the pumps on the screen, it creates the nine pumps using a for loop.
        /// it checks to see if a pump is available or not and writes "FREE" or "BUSY" depending on the status of the pump.
        /// The pumps are spaced out evenly in three lanes.
        /// </summary>
        public static void DrawPumps()
        {
            Pump p; // creates an object of the pump class

            Console.WriteLine("Pumps Status:");

            for (int i = 0; i < 9; i++)
            {
                p = Data.pumps[i];

                Console.Write("#{0} ", i + 1);
                if (p.IsAvailable()) { Console.Write("FREE    "); } // calls IsAvailable method to see if a pump is free and if true "FREE" is written
                else { Console.Write("BUSY (#{0})", p.currentVehicle.CarID); } // if the IsAvailable Method returns false then "BUSY" is written and the vehicle's carID is Written
                Console.Write(" | ");
                
                if (i % 3 == 2) { Console.WriteLine(); } //uses modulus calculation to split the pumps into three lanes.
            }
        }
    }
}
