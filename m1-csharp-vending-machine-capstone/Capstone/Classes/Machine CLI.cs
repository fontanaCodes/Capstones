using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone;
using System.IO;

namespace Capstone.Classes
{
    public class Machine_CLI
    {
        public void RunVendor()
        {
            string filepath = Path.Combine(Environment.CurrentDirectory, "vendingmachine.csv");
            Dictionary<string, Inventory> todaysInventory = Stocker.UsingPathToLoadStocker(filepath);
            Console.WriteLine();
            Console.WriteLine("Welcome to the Umbrella Corp. Vendo3000.");
            Console.WriteLine();
            string input ="";

            VendingMachine activeVendingMachine = new VendingMachine();
            activeVendingMachine.currentInventory = todaysInventory;

            while (input != "5746")
            {

                Console.WriteLine($"Current Balance: {(Convert.ToDouble(activeVendingMachine.currentBalance) / 100).ToString("C")}");
                Console.WriteLine("1) Deposit Money");
                Console.WriteLine("2) Display Snack Options");
                Console.WriteLine("3) Purchase Snack");
                Console.WriteLine("4) Finished and Get Change");
                input = (Console.ReadLine());


                if (input == "1")
                {
                    //Deposit Money
                    Console.WriteLine("How much would you like to deposit in dollars? Please use only $1, $5, $10");
                    int depositedAmount = int.Parse(Console.ReadLine());
                    if (depositedAmount == 1 || depositedAmount == 5 || depositedAmount == 10)
                    {
                        activeVendingMachine.FeedMoney(depositedAmount);
                    }
                    else
                    {
                        Console.WriteLine("That is machine only accepts $1 bills, $5 bills, or $10 bills.");
                    }


                }
                else if (input == "2")
                {
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Console.WriteLine($"|{todaysInventory["A1"].RealTimeItem.NameOfProduct} |   {todaysInventory["A2"].RealTimeItem.NameOfProduct}   |   {todaysInventory["A3"].RealTimeItem.NameOfProduct}  | {todaysInventory["A4"].RealTimeItem.NameOfProduct} |          |");
                    Console.WriteLine($"|    {(todaysInventory["A1"].RealTimeItem.PriceOfProduct * .01).ToString("C")}     |     {(todaysInventory["A2"].RealTimeItem.PriceOfProduct * .01).ToString("C")}    |      {(todaysInventory["A3"].RealTimeItem.PriceOfProduct * .01).ToString("C")}     |      {(todaysInventory["A4"].RealTimeItem.PriceOfProduct * .01).ToString("C")}    |          |");
                    Console.WriteLine("|     A1       |      A2      |       A3       |      A4       |    $$    |");
                    Console.WriteLine("----------------------------------------------------------------   |__|   |");
                    Console.WriteLine($"|  {todaysInventory["B1"].RealTimeItem.NameOfProduct}     |   {todaysInventory["B2"].RealTimeItem.NameOfProduct}   |  {todaysInventory["B3"].RealTimeItem.NameOfProduct}     |   {todaysInventory["B4"].RealTimeItem.NameOfProduct}    |          |");
                    Console.WriteLine($"|    {(todaysInventory["B1"].RealTimeItem.PriceOfProduct * .01).ToString("C")}     |    {(todaysInventory["B2"].RealTimeItem.PriceOfProduct * .01).ToString("C")}     |     {(todaysInventory["B3"].RealTimeItem.PriceOfProduct * .01).ToString("C")}      |     {(todaysInventory["B4"].RealTimeItem.PriceOfProduct * .01).ToString("C")}     |__________|");
                    Console.WriteLine("|     B1       |      B2      |       B3       |      B4       | o o  o o |");
                    Console.WriteLine("---------------------------------------------------------------- o o  o o |");
                    Console.WriteLine($"|    {todaysInventory["C1"].RealTimeItem.NameOfProduct}      |  {todaysInventory["C2"].RealTimeItem.NameOfProduct}    |{todaysInventory["C3"].RealTimeItem.NameOfProduct} |     {todaysInventory["C4"].RealTimeItem.NameOfProduct}     | o o  o o |");
                    Console.WriteLine($"|    {(todaysInventory["C1"].RealTimeItem.PriceOfProduct * .01).ToString("C")}     |    {(todaysInventory["C2"].RealTimeItem.PriceOfProduct * .01).ToString("C")}     |      {(todaysInventory["C3"].RealTimeItem.PriceOfProduct * .01).ToString("C")}     |     {(todaysInventory["C4"].RealTimeItem.PriceOfProduct * .01).ToString("C")}     | o o  o o |");
                    Console.WriteLine("|     C1       |      C2      |       C3       |      C4       |__________|");
                    Console.WriteLine("----------------------------------------------------------------          |");
                    Console.WriteLine("|              |    Little    |                |               |          |");
                    Console.WriteLine($"|  {todaysInventory["D1"].RealTimeItem.NameOfProduct}     | {(todaysInventory["D2"].RealTimeItem.NameOfProduct).Substring(7,11)}  |   {todaysInventory["D3"].RealTimeItem.NameOfProduct}     |  {todaysInventory["D4"].RealTimeItem.NameOfProduct}   |          |");
                    Console.WriteLine($"|   {(todaysInventory["D1"].RealTimeItem.PriceOfProduct * .01).ToString("C")}      |    {(todaysInventory["D2"].RealTimeItem.PriceOfProduct * .01).ToString("C")}     |     {(todaysInventory["D3"].RealTimeItem.PriceOfProduct * .01).ToString("C")}      |     {(todaysInventory["D4"].RealTimeItem.PriceOfProduct * .01).ToString("C")}     |          |");
                    Console.WriteLine("|     D1       |      D2      |       D3       |      D4       |          |");
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Console.WriteLine("|                                                                         |");
                    Console.WriteLine("|                 __________________________                              |");
                    Console.WriteLine("|                |                          |                             |");
                    Console.WriteLine("|                |__________________________|                             |");
                    Console.WriteLine("|                                                                       NB|");
                    Console.WriteLine("---------------------------------------------------------------------------");
                }
                else if (input == "3")
                {
                    Console.WriteLine("Please enter the snack ID for the item that you would like to purchase:");
                    string slotID = Console.ReadLine().ToUpper();

                    if (activeVendingMachine.currentInventory.ContainsKey(slotID))
                    {
                        if (activeVendingMachine.currentBalance >= activeVendingMachine.currentInventory[slotID].RealTimeItem.PriceOfProduct)
                        {
                            if (activeVendingMachine.currentInventory[slotID].Quantity > 0)
                            {
                                int priorBalance = activeVendingMachine.currentBalance;
                                activeVendingMachine.Purchase(slotID);
                               
                                Console.WriteLine($"You have selected {activeVendingMachine.currentInventory[slotID].RealTimeItem.NameOfProduct} for {(Convert.ToDouble(activeVendingMachine.currentInventory[slotID].RealTimeItem.PriceOfProduct)/100).ToString("C")}." );
                                PurchaseLog.AddToLog(activeVendingMachine, slotID, priorBalance);
                                TechnicianProductReport newProductReport = new TechnicianProductReport();
                                newProductReport.UpdateDictionary(activeVendingMachine, slotID);
                            }
                            else
                            {
                                Console.WriteLine("Sorry we are out of this product.");
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("You have not given this vending machine enough money. Please deposit more money.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("This is not a valid snack ID");
                    }

                }
                else if (input == "4")
                {
                    //Get Change
                    ChangeDispenser changeToBeOrNotToBeDispensed = new ChangeDispenser(activeVendingMachine.currentBalance);
                    Console.WriteLine($"You have been dispensed {(Convert.ToDouble(activeVendingMachine.currentBalance)/100).ToString("C")}. \nPlease take your change: {changeToBeOrNotToBeDispensed.Quarters} Quarters, {changeToBeOrNotToBeDispensed.Dimes} Dimes, {changeToBeOrNotToBeDispensed.Nickels} Nickels.");
                    activeVendingMachine.currentBalance = 0;

                    break;
                }
                else if (input == "216")
                {
                    TechnicianProductReport newReport = new TechnicianProductReport();
                    newReport.CreateReport(activeVendingMachine);
                }
                else
                {
                    Console.WriteLine("Invalid choice, please try again.");
                }
            }
            
        }
    }
}
