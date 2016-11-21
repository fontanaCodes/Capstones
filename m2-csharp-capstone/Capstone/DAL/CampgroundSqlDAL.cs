using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class CampgroundSqlDAL : ICampgroundDAL
    {
        private string connectionString;

        public CampgroundSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Campground> GetCampground()
        {
            throw new NotImplementedException();
        }

        public List<Campground> GetCampground(int parkId)
        {
            List<Campground> output = new List<Campground>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM campground WHERE park_id = @parkId;", conn);
                    cmd.Parameters.AddWithValue("@parkId", parkId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground c = new Campground();
                        c.campgroundID = Convert.ToInt32(reader["campground_id"]);
                        c.parkID = Convert.ToInt32(reader["park_id"]);
                        c.name = Convert.ToString(reader["name"]);
                        c.openFrom = Convert.ToInt32(reader["open_from_mm"]);
                        c.openTo = Convert.ToInt32(reader["open_to_mm"]);
                        c.dailyFee = Convert.ToDouble(reader["daily_fee"]);

                        output.Add(c);
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
