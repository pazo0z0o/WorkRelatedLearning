using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ShipBunkerScrapper
{
    public class Menus
    {
        public void InitialMenu()
        {
            char? choice;
            bool validChoice = false;


            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("1) MGO financial elements\n" + "2) VLSFO financial elements\n" + "3) Both MGO and VLSFO\n");
            Console.WriteLine("----------------------------------------------------------");
            do
            {
                choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        MGOMenu();
                        validChoice = true;
                        break;
                    case '2':
                        VLSFOMenu();
                        validChoice = true;
                        break;
                    case '3':
                        BothMenu();
                        validChoice = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Choice, try again!");
                        break;
                }
            } while (validChoice);
            Console.Clear();
        }

        public void MGOMenu()
        {
            char? choice;
            StorageHandler CsvOut = new StorageHandler();
            CsvOut.MgoCsvOutputs();
            Console.WriteLine("\nPress anything to return to initial menu");
            choice = Console.ReadKey().KeyChar;
            if (choice != null)
            {
                Console.Clear();
                InitialMenu();
            }
        }

        public void VLSFOMenu()
        {
            char? choice;
            StorageHandler CsvOut = new StorageHandler();
            CsvOut.VlsfoCsvOutputs();
            Console.WriteLine("\nPress anything to return to initial menu");
            choice = Console.ReadKey().KeyChar;
            if (choice != null)
            {
                Console.Clear();
                InitialMenu();
            }

        }

        public void BothMenu()
        {
            char? choice;
            StorageHandler CsvOut = new StorageHandler();
            CsvOut.VlsfoCsvOutputs();
            CsvOut.MgoCsvOutputs();
            Console.WriteLine("\nPress anything to return to initial menu");
            choice = Console.ReadKey().KeyChar;
            if (choice != null)
            {
                Console.Clear();
                InitialMenu();
            }
        }
    }
}
