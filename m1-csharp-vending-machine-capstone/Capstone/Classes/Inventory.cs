using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class Inventory
    {
        public int Quantity { get; set; }

        public Item RealTimeItem { get; }

        public Inventory(Item realtimeItem, int Quantity)
        {
            RealTimeItem = realtimeItem;
            this.Quantity = Quantity;
        }
    }
}
