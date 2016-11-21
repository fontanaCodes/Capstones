using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class PurchaseTest
    {
        [TestMethod]
        public void PurchaserTest_BalanceAndQuantityDecrement()
        {
            VendingMachine vendingMachineTest = new VendingMachine();

            Item itemOne = new Item("Clam Chowder", 400);
            Inventory inventoryOne = new Inventory(itemOne, 2);
            Dictionary<string, Inventory> inventoryNow = new Dictionary<string, Inventory>();
            inventoryNow.Add("slot1", inventoryOne);
            vendingMachineTest.currentInventory = inventoryNow;
            vendingMachineTest.FeedMoney(5);
            vendingMachineTest.Purchase("slot1");
            Assert.AreEqual(100, vendingMachineTest.currentBalance);
            Assert.AreEqual(1, inventoryNow["slot1"].Quantity);

            
            
            
           
        }
    }
}
