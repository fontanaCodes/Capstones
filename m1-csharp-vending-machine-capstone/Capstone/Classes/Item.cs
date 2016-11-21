using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class Item
    {
        public string NameOfProduct { get; private set; }
        public int PriceOfProduct { get; private set; }

        public Item(string NameOfProduct, int PriceOfProduct)
        {
            this.NameOfProduct = NameOfProduct;
            this.PriceOfProduct = PriceOfProduct;
        }
    }
}
