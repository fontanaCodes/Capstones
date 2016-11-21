using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;
using Capstone.Models;

namespace Capstone.DAL.Tests
{
    [TestClass()]
    public class CampgroundSqlDALTests
    {
        private TransactionScope tran;
        private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        private int parkId = 0;
        private int campgroundId = 0;

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

                cmd = new SqlCommand("INSERT INTO campground VALUES (" + parkId + ", 'TechElevator', 2, 5, 15.00); SELECT CAST(scope_identity() as int);", conn);
                campgroundId = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }


        [TestMethod()]
        public void GetCampgroundTest()
        {
            CampgroundSqlDAL dal = new CampgroundSqlDAL(connectionString);

            List<Campground> testList = dal.GetCampground(parkId);

            Assert.AreEqual(1, testList.Count());
            Assert.AreEqual("TechElevator", testList[0].name);
        }
    }
}