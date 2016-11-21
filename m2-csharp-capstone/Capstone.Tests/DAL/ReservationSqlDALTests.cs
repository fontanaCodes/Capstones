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
    public class ReservationSqlDALTests
    {
        private TransactionScope tran;
        private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM reservation", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("insert into reservation values (1, 'Natalie', '02/03/2016', '02/10/2016', GETDATE());", conn);
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void MakeReservationTest()
        {
            //Arrange
            ReservationSqlDAL dal = new ReservationSqlDAL(connectionString);

            //Act
            dal.MakeReservation(1, Convert.ToDateTime("05/22/2016"), Convert.ToDateTime("05/25/2016"), "Crespo");

            List<Reservation> list = dal.GetReservation();

            //Assert
            Assert.AreEqual(2, list.Count());
            Assert.AreEqual("Crespo", list[1].name);
        }

        [TestMethod()]
        public void GetReservationTest()
        {
            ReservationSqlDAL dal = new ReservationSqlDAL(connectionString);

            List<Reservation> list = dal.GetReservation();

            Assert.AreEqual(1, list.Count());
            Assert.AreEqual("Natalie", list[0].name);
        }
    }
}