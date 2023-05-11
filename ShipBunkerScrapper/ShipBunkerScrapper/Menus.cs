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


        public char MenuChoices()
        {
            char choice = ' ';
            bool validChoice = false;
            String MgoURL = "https://shipandbunker.com/prices/av/global/av-glb-global-average-bunker-price#MGO";
            String VlsfoURL = "https://shipandbunker.com/prices/av/global/av-glb-global-average-bunker-price#VLSFO";


            //menu loop for invalid choices -- breaks the loop at the correct key press: 1-2-3 choice
            do
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine(" 1) MGO financial elements\n " + " 2) VLSFO financial elements\n" + "3) Both MGO and VLSFO\n");
                Console.WriteLine("----------------------------------------------------------");
                choice = Convert.ToChar((Console.ReadKey()));

                if (choice == ' ' || choice != '1' || choice != '2' || choice != '3')
                {
                    Console.WriteLine("Invalid Choice, try again!");
                }
                switch (choice)
                {
                    case '1':
                        //loader MGO


                        validChoice = true;
                        break;
                    case '2':
                        //loader VLSFO
                        

                        validChoice = true;
                        break;
                    case '3':
                        //loader Both
                       
                        validChoice = true;
                        break;
                }
            }while (validChoice) ;
                Console.Clear();
            
            return choice;
        }

        public void MGOMenu() { }
        public void VLSFOMenu() { }

        public void BothMenu() { }
    }
}
