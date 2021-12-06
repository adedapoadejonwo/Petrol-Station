using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PetrolStation
{
    class Counters
    {
        // These Variables are used to hold the values for the counters
        private double totalAmountOfFuelDispensed = 0;
        private double totalAmountOfDieselDispensed = 0;
        private double totalAmountOfLPGDispensed = 0;
        private double totalAmountOfUnleadedDispensed = 0;
        private int vehiclesServiced;
        private int vehiclesNotServiced;
        private double totalEarnings;
        private double totalCommission;
        
        /// <summary>
        /// This Method calls both the UpdateCounter Method and the WriteToFile Method
        /// </summary>
        public void SaveCounters()
        {
            UpdateCounter();
            WriteToFile();
        }

        /// <summary>
        /// This Method is used to update the counters of the app, during its lifetime
        /// </summary>
        public void UpdateCounter()
        {
            // This Foreach Loop increments the vehiclesNotServiced variable for every object in the CarsThatleft list
            // It is used to find out how many cars left before they were serviced
            foreach(Vehicle v in Data.CarsThatLeft)
            {
                vehiclesNotServiced++;
            }

            //this foreach loop is used to update the values of the Counter variables every time a car has been serviced.
            foreach(transaction t in Data.listOfTransactions)
            {
                totalAmountOfDieselDispensed += t.AmountOfDieselDispensed;
                totalAmountOfFuelDispensed += t.LitresDispensed;
                totalAmountOfLPGDispensed += t.AmountOfLPGDispensed;
                totalAmountOfUnleadedDispensed += t.AmountOfUnleadedDispensed;
                totalEarnings += t.CostOftransaction;
                totalCommission += t.CommissionOfTransaction;
                vehiclesServiced++;
            }
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.io.streamwriter?view=netframework-4.7.2
        /// With the information I gained from the link above i Used System.IO to access the StreamWriter class
        /// This Method is used to create a file called "Counters.txt", This file contains all of the counters for this app
        /// </summary>
        public void WriteToFile()
        {            
            using (StreamWriter counter = new StreamWriter(@"Counters.txt")) //this line is used to create the counters text file using StreamWriter
            {
                counter.WriteLine("Here are the counters for this app"); //this tells the user that theses are the counters for the app
                counter.WriteLine();
                counter.WriteLine("1) Total litres of fuel dispensed: " + totalAmountOfFuelDispensed);
                counter.WriteLine("2) Total Earnings: " +  totalEarnings );
                counter.WriteLine("3) Total Commission: " + totalCommission);
                counter.WriteLine("4) Number of Cars Serviced: " + vehiclesServiced);
                counter.WriteLine("5) Number of Not Serviced: " + vehiclesNotServiced);
                counter.WriteLine("6) Total litres of LPG Fuel dispensed: " + totalAmountOfLPGDispensed);
                counter.WriteLine("7) Total litres of Diesel Fuel dispensed: " + totalAmountOfDieselDispensed);
                counter.WriteLine("8) Total litres of Unleaded Fuel dispensed: " + totalAmountOfUnleadedDispensed);
                counter.WriteLine();

                //This foreach loop is used to record the counters for each individual transaction, it prints out the transaction counters
                //for every serviced vehicle
                foreach(transaction t in Data.listOfTransactions)
                {
                    counter.WriteLine("Pump Number: " + t.PumpNumber);
                    counter.WriteLine("Car ID: " + t.CarNumber);
                    counter.WriteLine("Vehicle Type: " + t.TypeOfCarServiced);
                    counter.WriteLine("Number Of Litres: " + t.LitresDispensed);
                    counter.WriteLine();
                }
            }
        }
                        
    }
}
