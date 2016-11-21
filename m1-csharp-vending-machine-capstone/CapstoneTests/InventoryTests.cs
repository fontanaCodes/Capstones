using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class InventoryTests
    {
        [TestMethod]
        public void TestInventory_ContentCorrect()
        {
            Item itemOne = new Item("Clam Chowder", 4);
            Inventory inventoryOne = new Inventory(itemOne, 2);

            Assert.AreEqual("Clam Chowder", inventoryOne.RealTimeItem.NameOfProduct);
            Assert.AreEqual(2, inventoryOne.Quantity);
            Assert.AreEqual(4, inventoryOne.RealTimeItem.PriceOfProduct);
        }
    }
}
