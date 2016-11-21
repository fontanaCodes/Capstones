using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Capstone.Classes;

namespace Capstone.Classes
{
    class TechnicianProductReport
    {

        static Dictionary<string, int> salesOutput = new Dictionary<string, int>()
            {
                {"Potato Crisps", 0 },
                {"Stackers", 0 },
                {"Grain Waves", 0 },
                {"Cloud Popcorn", 0 },
                {"Moonpie", 0 },
                {"Cowtales", 0 },
                {"Wonka Bar", 0 },
                {"Crunchie", 0 },
                {"Cola", 0 },
                {"Dr. Salt", 0 },
                {"Mountain Melter", 0 },
                {"Heavy", 0 },
                {"U-Chews", 0 },
                {"Little League Chew", 0 },
                {"Chiclets", 0 },
                {"Triplemint", 0 }
            };

        static double totalPrice = 0;

        public void UpdateDictionary(VendingMachine activeVendingMachine, string slotID)
        {

            string currentItem = activeVendingMachine.currentInventory[slotID].RealTimeItem.NameOfProduct;

            salesOutput[currentItem] = salesOutput[currentItem] + 1;

            totalPrice = totalPrice + ((Convert.ToDouble(activeVendingMachine.currentInventory[slotID].RealTimeItem.PriceOfProduct)) / 100);
            
        }


        public void CreateReport(VendingMachine activeVendingMachine)
        {
            string filePathName = $@"C:\Temp\vending_machine_purchase_stats.txt";
            bool DoesExist = (File.Exists(filePathName));

            using (StreamWriter purchaseStats = new StreamWriter(filePathName, false))
            {

                if (!DoesExist)
                {
                    string titleLine = "Product|Quantity";
                    purchaseStats.WriteLine(titleLine);
                }

                foreach (KeyValuePair<string, int> kvp in salesOutput)
                {

                    string productLine = $"{kvp.Key}|{kvp.Value}";
                    purchaseStats.WriteLine(productLine);
                }

                Console.WriteLine();
                purchaseStats.WriteLine($"Total Amount Spent: {totalPrice.ToString("C")}");

            }
        }


    }

}


