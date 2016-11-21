using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class FeederTest
    {
        [TestMethod]
        public void FeedMoneyTest()
        {
            VendingMachine vm1 = new VendingMachine();
            vm1.FeedMoney(200);

            Assert.AreEqual(20000, vm1.currentBalance);
        }
    }
}
