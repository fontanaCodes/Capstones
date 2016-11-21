using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Park
    {

        public int parkID { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public DateTime establishDate { get; set; }
        public int area { get; set; }
        public int visitors { get; set; }
        public string description { get; set; }
        
        public override string ToString()
        {
            return "ID: " + parkID.ToString().PadRight(5) + "Name: " + name.PadRight(15) + ("(" + location + ")").PadRight(15) + "Estb: " + establishDate.ToString("MM/dd/yyyy").PadRight(20) + "  Area: " + area.ToString().PadRight(5) + "\n\n" + "Number of Visitors per Year: " + visitors.ToString() + "\n\n" + description + "\n\n";
        }
    }
}
