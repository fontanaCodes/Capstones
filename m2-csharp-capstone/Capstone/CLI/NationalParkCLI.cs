using Capstone.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.CLI
{
    public class NationalParkCLI
    {
        const string Command_ViewListOfParks = "1";
        const string Command_ViewListOfCampgrounds = "2";
        const string Command_ViewListOfCampsites = "3";
        const string Command_ViewListOfBestCampsitesInEachCampground = "4";
        const string Command_ViewListOfAllReservationsIn30Days = "5";

        const string Command_Quit = "q";
        readonly string DatabaseConnection = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;

        public void RunCLI()
        {



            while (true)
            {
                PrintMenu();

                string command = Console.ReadLine();

                Console.Clear();

                switch (command.ToLower())
                {
                    case Command_ViewListOfParks:
                        GetPark();
                        break;

                    case Command_ViewListOfCampgrounds:
                        GetCampground();
                        break;

                    case Command_ViewListOfCampsites:
                        GetCampsite();
                        break;

                    case Command_ViewListOfBestCampsitesInEachCampground:
                        GetTop5CampsitesInPark();
                        break;

                    case Command_ViewListOfAllReservationsIn30Days:
                        GetReservationsIn30Days();
                        break;

                    case Command_Quit:
                        return;

                    default:
                        Console.WriteLine("That is not a valid option, please try again. \n");
                        break;
                }
            }
        }

        private void GetTop5CampsitesInPark()
        {
            selectingReservation = true;

            while (selectingReservation)
            {
                Console.WriteLine("Please enter the ParkID that you want to view:");
                int theParkID = Convert.ToInt32(Console.ReadLine());


                Console.WriteLine("\nWhat date would you like to begin your trip?(Please use format: mm/dd/yyyy)");
                DateTime fromDate = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("\nWhat date would you like to end your trip?(Please use format: mm/dd/yyyy)");
                DateTime toDate = Convert.ToDateTime(Console.ReadLine());

                CampsiteSqlDAL dal = new CampsiteSqlDAL(DatabaseConnection);
                List<Campsite> campsites = dal.GetTop5CampsitesInPark(theParkID, fromDate, toDate);

                if (campsites.Count > 0)
                {
                    campsites.ForEach(c =>
                    {
                        Console.WriteLine(c);
                    });

                    Console.WriteLine("\nPlease select the ID of the site you would like to reserve. If none, select '0'.");
                    int reservationSelection = Convert.ToInt32(Console.ReadLine());

                    if (reservationSelection == 0) { return; }

                    Console.WriteLine("\nWhat name would you like to place the reservation under?");
                    string reservationName = Console.ReadLine();

                    ReservationSqlDAL resDAL = new ReservationSqlDAL(DatabaseConnection);
                    resDAL.MakeReservation(reservationSelection, fromDate, toDate, reservationName);


                    Console.WriteLine("\nConfirmation Code: " + randomHex());


                    selectingReservation = false;

                }
                else
                {
                    Console.WriteLine("\nNo site available.\n");
                }
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine("Please choose an option below:");
            Console.WriteLine("1 - View List Of Parks");
            Console.WriteLine("2 - View List Of Campgrounds");
            Console.WriteLine("3 - View List of Campsites");
            Console.WriteLine("4 - View Top 5 Campsites in Each Campground");
            Console.WriteLine("5 - View List of All Reservations in the Next 30 Days");

            Console.WriteLine("Q - Quit");
            Console.WriteLine();

        }

        private void GetPark()
        {

            ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
            List<Park> parks = dal.GetPark();

            if (parks.Count > 0)
            {
                parks.ForEach(p =>
                {
                    Console.WriteLine(p);
                });
            }
            else
            {
                Console.WriteLine("****NO RESULTS****");
            }
        }


        private void GetCampground()
        {
            Console.WriteLine("Please enter the Park ID to view the list of campgrounds:");
            int input = Convert.ToInt32(Console.ReadLine());

            //ParkSqlDAL park = new ParkSqlDAL(DatabaseConnection);
            //List<Park> parkList = park.GetPark();

            CampgroundSqlDAL dal = new CampgroundSqlDAL(DatabaseConnection);
            List<Campground> campgrounds = dal.GetCampground(input);

            if (campgrounds.Count > 0)
            {
                campgrounds.ForEach(c =>
                {
                    Console.WriteLine(c);
                });
            }
            else
            {
                Console.WriteLine("*****NO RESULTS*****");
            }
        }

        private void GetCampsite()
        {
            selectingReservation = true;

            while (selectingReservation)
            {

                Console.WriteLine("Please enter the Campground ID to view the list of available campsites");
                int input = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\nWhat date would you like to begin your trip?(Please use format: mm/dd/yyyy)");
                DateTime fromDate = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("\nWhat date would you like to end your trip?(Please use format: mm/dd/yyyy)");
                DateTime toDate = Convert.ToDateTime(Console.ReadLine());

                CampsiteSqlDAL dal = new CampsiteSqlDAL(DatabaseConnection);
                List<Campsite> campsites = dal.GetCampsite(input, fromDate, toDate);



                if (campsites.Count > 0)
                {
                    campsites.ForEach(c =>
                    {
                        Console.WriteLine(c);
                    });

                    Console.WriteLine("\nPlease select the ID of the site you would like to reserve. If none, select '0'.");
                    int reservationSelection = Convert.ToInt32(Console.ReadLine());

                    if (reservationSelection == 0) { return; }

                    Console.WriteLine("\nWhat name would you like to place the reservation under?");
                    string reservationName = Console.ReadLine();

                    ReservationSqlDAL resDAL = new ReservationSqlDAL(DatabaseConnection);
                    resDAL.MakeReservation(reservationSelection, fromDate, toDate, reservationName);


                    Console.WriteLine("\nConfirmation Code: " + randomHex());


                    selectingReservation = false;

                }
                else
                {
                    Console.WriteLine("\nNo site available.\n");
                }
            }
        }

        private void GetReservationsIn30Days()
        {
            ReservationSqlDAL resDAL = new ReservationSqlDAL(DatabaseConnection);

            Console.WriteLine("Site Number".PadRight(14) + "Name".PadRight(30) + "Date\n");

            List<Reservation> list = resDAL.GetReservationsFor30Days();

            if (list.Count > 0)
            {
                list.ForEach(c =>
                {
                    Console.WriteLine(c);
                });
            }
            else
            {
                Console.WriteLine("\nNo reservations found.");
            }

            Console.WriteLine("\n");
        }

        static Random random = new Random();
        private string randomHex()
        {
            byte[] buffer = new byte[8 / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            return result;
        }

        private bool selectingReservation { get; set; }

    }


}


