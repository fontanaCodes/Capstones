using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ParkSqlDAL : IParkDAL
    {
        private string connectionString;

        public ParkSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Park> GetPark()
        {
            List<Park> parkOutput = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM park ORDER BY name;", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park p = new Park();
                        p.parkID = Convert.ToInt32(reader["park_id"]);
                        p.name = Convert.ToString(reader["name"]);
                        p.location = Convert.ToString(reader["location"]);
                        p.establishDate = Convert.ToDateTime(reader["establish_date"]);
                        p.area = Convert.ToInt32(reader["area"]);
                        p.visitors = Convert.ToInt32(reader["visitors"]);
                        p.description = Convert.ToString(reader["description"]);

                        parkOutput.Add(p);

                    }
                }
            }
            catch(SqlException e)
            {
                throw;
            }

            return parkOutput;

        }
    }
}
