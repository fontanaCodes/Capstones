using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Campsite
    {

        public int siteID { get; set; }
        public int campgroundID { get; set; }
        public int siteNumber { get; set; }
        public int maxOccupancy { get; set; }
        public bool accessible { get; set; }
        public int maxRVLength { get; set; }
        public bool utilities { get; set; }
        public double totalPrice { get; set; }
        public double dailyFee { get; set; }

        public override string ToString()
        {
            string isAccessible = (accessible)? "Yes" : "No";
            string hasUtilities = (utilities) ? "Yes" : "No";

            return ("CampsiteID: " + siteID.ToString().PadRight(10) + "CampsiteNumber" + siteNumber.ToString().PadRight(10) + "Max Occupancy: " + maxOccupancy.ToString().PadRight(10) +"Accessible: " + isAccessible.ToString().PadRight(10) + "Max RV Length: " + (maxRVLength + " feet ").PadRight(15) + "Utilities: " + hasUtilities + "\n" + "Total Price: " + totalPrice.ToString("C"));
        }

        
        public double CalcTotalPrice(DateTime fromDate, DateTime toDate, double dailyFee)
        {
            return totalPrice = dailyFee * (toDate.Subtract(fromDate)).TotalDays;
        }

        //public void FilterByMonths(DateTime fromDate, DateTime toDate, int openFrom, int openTo)
        //{
        //    //find the range between openFrom/openTo when either fromDate/toDate fall within this range.
        //    //if toDate > fromDate & toYear = fromYear then 
        //}


    }
}
