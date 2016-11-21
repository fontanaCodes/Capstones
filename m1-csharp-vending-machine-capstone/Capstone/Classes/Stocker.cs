using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public class Stocker
    {

        public static Dictionary<string, Inventory> UsingPathToLoadStocker(string filepath)
        {
            Dictionary<string, Inventory> currentInventory = new Dictionary<string, Inventory>();
            try
            {
                using (StreamReader sr = new StreamReader(filepath))
                {
                    while (!sr.EndOfStream)
                    {
                        double price = 0;
                        string itemname = "";

                        string newline = sr.ReadLine();
                        string[] wholeLine = null;
                        wholeLine = newline.Split(new char[] { '|' });
                        itemname = wholeLine[1];
                        price = (double.Parse(wholeLine[2]) * 100);
                        int priceint = Convert.ToInt32(price);
                        Item currentitem = new Item(itemname, priceint);
                        Inventory currentLilInventory = new Inventory(currentitem, 5);

                        currentInventory.Add(wholeLine[0], currentLilInventory);

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Whoops");
            }
            return currentInventory;
        }



    }
}
