using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Configuration;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL.Tests
{
    [TestClass()]
    public class ParkSqlDALTests
    {
        private TransactionScope tran;
        private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        private int parkId = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM reservation", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("DELETE FROM site", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("DELETE FROM campground", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("DELETE FROM park", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO park VALUES ('Test Park', 'Cleveland', '01/01/01', 3289.2, 123829, 'This park is fabulous'); SELECT CAST(scope_identity() as int);", conn);
                parkId = (int)cmd.ExecuteScalar();
            }
        }        

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void GetParkTest()
        {
            //Arrange
            ParkSqlDAL dal = new ParkSqlDAL(connectionString);

            //Act
            List<Park> listOfParks = dal.GetPark();

            //Assert
            Assert.AreEqual(1, listOfParks.Count());
            Assert.AreEqual("Test Park", listOfParks[0].name);
        }
    }
}