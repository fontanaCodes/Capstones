using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Campground
    {

        public int campgroundID { get; set; }
        public int parkID { get; set; }
        public string name { get; set; }
        public int openFrom { get; set; }
        public int openTo { get; set; }
        public double dailyFee { get; set; }
        
        public override string ToString()
        {
            DateTime dt = Convert.ToDateTime(Convert.ToString(openTo) + "/01/01");
            DateTime df = Convert.ToDateTime(Convert.ToString(openFrom) + "/01/01");
            return "Campground: " + campgroundID.ToString().PadRight(5) + name.PadRight(25) + ("Open from " + df.ToString("MMMM") + " to " +dt.ToString("MMMM")).PadRight(38) + "Price: " + dailyFee.ToString("C") + " per Day \n";
        }
    }
}
