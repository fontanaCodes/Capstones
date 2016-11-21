using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ReservationSqlDAL : IReservationDAL
    {
        private string connectionString;

        public ReservationSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Reservation> GetReservation()
        {
            List<Reservation> output = new List<Reservation>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM reservation;", conn);
      

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Reservation r = new Reservation();
                        r.siteID = Convert.ToInt32(reader["site_id"]);
                        r.name = Convert.ToString(reader["name"]);
                        r.fromDate = Convert.ToDateTime(reader["from_date"]);
                        r.toDate = Convert.ToDateTime(reader["to_date"]);
                        r.createDate = Convert.ToDateTime(reader["create_date"]);

                        output.Add(r);
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }

            return output;
        }

        public void MakeReservation(int reservationSelection, DateTime fromDate, DateTime toDate, string name)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"INSERT INTO reservation VALUES (@reservationSelection, @name, @fromDate, @toDate, GETDATE())", conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@reservationSelection", reservationSelection);
                    cmd.Parameters.AddWithValue("@fromDate", fromDate);
                    cmd.Parameters.AddWithValue("@toDate", toDate);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(SqlException e)
            {
                throw;
            }
        }
    
        public List<Reservation> GetReservationsFor30Days()
        {
            List<Reservation> output = new List<Reservation>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM reservation WHERE from_date BETWEEN GETDATE() AND DATEADD(day, 30, GETDATE()) ORDER BY from_date;", conn);


                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Reservation r = new Reservation();
                        r.siteID = Convert.ToInt32(reader["site_id"]);
                        r.name = Convert.ToString(reader["name"]);
                        r.fromDate = Convert.ToDateTime(reader["from_date"]);
                        r.toDate = Convert.ToDateTime(reader["to_date"]);
                        r.createDate = Convert.ToDateTime(reader["create_date"]);

                        output.Add(r);
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }

            return output;
        }

    }
}
