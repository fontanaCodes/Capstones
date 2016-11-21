using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class ChangeDispenserTests
    {
        [TestMethod]
        public void TestChangeDispenser_ArrayUpdatesAfterCounterIncrements()
        {
            ChangeDispenser changeArray = new ChangeDispenser(345);
            ChangeDispenser changeArray1 = new ChangeDispenser(540);
            ChangeDispenser changeArray2 = new ChangeDispenser(25);
            ChangeDispenser changeArray3 = new ChangeDispenser(10);
            ChangeDispenser changeArray4 = new ChangeDispenser(5);
            ChangeDispenser changeArray5 = new ChangeDispenser(0);

            Assert.AreEqual(13, changeArray.Quarters);
            Assert.AreEqual(2, changeArray.Dimes);
            Assert.AreEqual(0, changeArray.Nickels);

            Assert.AreEqual(21, changeArray1.Quarters);
            Assert.AreEqual(1, changeArray1.Dimes);
            Assert.AreEqual(1, changeArray1.Nickels);

            Assert.AreEqual(1, changeArray2.Quarters);
            Assert.AreEqual(0, changeArray2.Dimes);
            Assert.AreEqual(0, changeArray2.Nickels);

            Assert.AreEqual(0, changeArray3.Quarters);
            Assert.AreEqual(1, changeArray3.Dimes);
            Assert.AreEqual(0, changeArray3.Nickels);

            Assert.AreEqual(0, changeArray4.Quarters);
            Assert.AreEqual(0, changeArray4.Dimes);
            Assert.AreEqual(1, changeArray4.Nickels);

            Assert.AreEqual(0, changeArray5.Quarters);
            Assert.AreEqual(0, changeArray5.Dimes);
            Assert.AreEqual(0, changeArray5.Nickels);
        }
    }
}
