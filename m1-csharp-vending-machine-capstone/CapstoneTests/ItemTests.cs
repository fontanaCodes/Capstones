using Capstone.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void TestItem_ContentCorrect()
        {

            Item itemOne = new Item("Clam Chowder", 4);

            Assert.AreEqual("Clam Chowder", itemOne.NameOfProduct);
            Assert.AreEqual(4, itemOne.PriceOfProduct);

        }
    }
   

}
