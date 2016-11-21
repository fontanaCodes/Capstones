using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using System.IO;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class StockerTest
    {
        [TestMethod]
        public void StockerTest_DoesItWork()
        {
            string filepath = Path.Combine(Environment.CurrentDirectory, "TestInventory.txt");
            
            Dictionary<string, Inventory> testanswer = Stocker.UsingPathToLoadStocker(filepath);


        }

    }
}
