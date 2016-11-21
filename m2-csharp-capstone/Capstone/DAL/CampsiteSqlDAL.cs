using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class CampsiteSqlDAL : ICampsiteDAL
    {
        private string connectionString;

        public CampsiteSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Campsite> GetCampsite()
        {
            throw new NotImplementedException();
        }

        public List<Campsite> GetCampsite(int campgroundId, DateTime fromDate, DateTime toDate)
        {
            List<Campsite> output = new List<Campsite>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT TOP 5*, campground.daily_fee FROM site LEFT JOIN reservation ON site.site_id=reservation.site_id LEFT JOIN campground ON site.campground_id = campground.campground_id WHERE site.campground_id = @campgroundId AND (@fromDate > to_date OR @toDate < from_date);", conn);
                    cmd.Parameters.AddWithValue("@campgroundId", campgroundId);
                    cmd.Parameters.AddWithValue("@fromDate", fromDate);
                    cmd.Parameters.AddWithValue("@toDate", toDate);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campsite c = new Campsite();
                        c.siteID = Convert.ToInt32(reader["site_id"]);
                        c.campgroundID = Convert.ToInt32(reader["campground_id"]);
                        c.siteNumber = Convert.ToInt32(reader["site_number"]);
                        c.maxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                        c.accessible = Convert.ToBoolean(reader["accessible"]);
                        c.maxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                        c.utilities = Convert.ToBoolean(reader["utilities"]);
                        c.dailyFee = Convert.ToDouble(reader["daily_fee"]);
                        c.totalPrice = c.CalcTotalPrice(fromDate, toDate, c.dailyFee);


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

        public List<Campsite> GetTop5CampsitesInPark(int parkId, DateTime fromDate, DateTime toDate)
        {
            List<Campsite> output = new List<Campsite>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    CampgroundSqlDAL dal = new CampgroundSqlDAL(connectionString);
                    List<Campground> listOfCampgrounds = dal.GetCampground(parkId);

                    SqlCommand cmd = new SqlCommand();

                    foreach (Campground cg in listOfCampgrounds)
                    {
                        cmd = new SqlCommand(@"SELECT TOP 5*, campground.daily_fee FROM site 
INNER JOIN campground ON campground.campground_id = site.campground_id 
WHERE site.site_id not in 
	(SELECT site_id 
		FROM reservation 
		WHERE (@toDate >  reservation.from_date AND @toDate < reservation.to_date)
		OR (@fromDate > reservation.from_date AND @fromDate < reservation.to_date)
		OR (@fromDate < reservation.from_date AND @toDate > reservation.to_date)
		) 
AND site.campground_id = @campground ;", conn);
                        cmd.Parameters.AddWithValue("@campground", cg.campgroundID);
                        cmd.Parameters.AddWithValue("@fromDate", fromDate);
                        cmd.Parameters.AddWithValue("@toDate", toDate);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Campsite c = new Campsite();
                                c.siteID = Convert.ToInt32(reader["site_id"]);
                                c.campgroundID = Convert.ToInt32(reader["campground_id"]);
                                c.siteNumber = Convert.ToInt32(reader["site_number"]);
                                c.maxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                                c.accessible = Convert.ToBoolean(reader["accessible"]);
                                c.maxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                                c.utilities = Convert.ToBoolean(reader["utilities"]);
                                c.dailyFee = Convert.ToDouble(reader["daily_fee"]);
                                c.totalPrice = c.CalcTotalPrice(fromDate, toDate, c.dailyFee);

                                output.Add(c);
                            }

                        }
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
