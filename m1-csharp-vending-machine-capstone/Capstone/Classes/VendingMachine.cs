using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        public Dictionary<string, Inventory> currentInventory
        {
            get; set;
        }

        public int currentBalance { get; set; }

        public void FeedMoney(int dollars)
        {
            currentBalance = currentBalance + dollars * 100;
        }

        public Item Purchase( string slotID )
        {
            int amountToDeduct = currentInventory[slotID].RealTimeItem.PriceOfProduct;
            currentBalance = currentBalance - amountToDeduct;
            currentInventory[slotID].Quantity = currentInventory[slotID].Quantity -1;
            return currentInventory[slotID].RealTimeItem;
        }

        //public string ReturnChange()
        //{
        //}
    }
}