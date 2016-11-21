using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Reservation
    {
        public int reservationID { get; set; }
        public int siteID { get; set; }
        public string name { get; set;}
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public DateTime createDate { get; set; }

        public override string ToString()
        {
            return siteID.ToString().PadRight(14) + name.PadRight(30) + fromDate.ToString("MM/dd/yyyy");
        }
    }
}
