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

            ScrapingLogic multiScraping = new ScrapingLogic();
            Console.WriteLine("Press anything to start scraping!");
            choice = Console.ReadKey().KeyChar;
            if (choice != null)
            {
                Console.Clear();
                multiScraping.MgoVlsfoScrapingTimer();
            }
        }
    }
}
