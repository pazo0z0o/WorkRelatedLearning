// See https://aka.ms/new-console-template for more information

using HtmlAgilityPack;
using ShipBunkerScrapper;
using System.Net.Http;

namespace ShipBunkerScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            //menu Prompts
            Console.WriteLine("Welcome to the Super Amazing Scraper 15.0\n");
   
            ScrapingLogic multiScraping = new ScrapingLogic();   
            multiScraping.MgoVlsfoScrapingTimer();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        
        }

    }





}