using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    class PurchaseLog
    {

        public static void AddToLog(VendingMachine activeVendingMachine, string SlotId, int PriorBalance)
        {
            string timestamp1 = DateTime.Now.ToString();

            string timestamp = ((DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks) / 10000000).ToString();   //Convert windows ticks to seconds

            string productLine = $"{timestamp1}|{activeVendingMachine.currentInventory[SlotId].RealTimeItem.NameOfProduct}|{SlotId}|{((Convert.ToDouble(PriorBalance))/100).ToString("C")}|{((Convert.ToDouble(activeVendingMachine.currentBalance)) / 100).ToString("C")}";     //amountaccepted //changetendered

            string filePathName = $@"C:\Temp\vending_machine_purchase_log.txt";
            bool DoesExist = (File.Exists(filePathName));

            using (StreamWriter purchaseLog = new StreamWriter(filePathName, true))
            {
                
                if (!DoesExist)
                {
                    string titleLine = "Date|Time|Product|SlotID|AmountAccepted|ChangeTendered";
                    purchaseLog.WriteLine(titleLine);
                }

                purchaseLog.WriteLine(productLine);
            }
        }

        
    }
}
