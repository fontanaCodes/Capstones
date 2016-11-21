using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class ChangeDispenser
    {

        public int Quarters { get; private set; }
        public int Dimes { get; private set; }
        public int Nickels { get; private set; }


        public ChangeDispenser(int customerBalance)
        {

            while (customerBalance > 0)
            {
                if (customerBalance >= 25)
                {
                    customerBalance = customerBalance - 25;
                    Quarters++;
                }
                else if (customerBalance >= 10)
                {
                    customerBalance = customerBalance - 10;
                    Dimes++;
                }
                else
                {
                    customerBalance = customerBalance - 5;
                    Nickels++;
                }
            }
        }
    }
}
