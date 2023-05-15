using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper;


namespace ShipBunkerScrapper
{
    public class StorageHandler
    {
        public StorageHandler() { }

        //DEFINETELY needs optimisation to use only one Csv output class but it works temporarily
        public void MgoCsvOutputs()
        {
            using (var writer = new StreamWriter(@"C:\Users\nikolaos.skliris\Desktop\WorkRelatedLearning\StaticScraper\ShipBunkerScrapper\ScrapingResults\Mgo_Output.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                ScrapingLogic records = new ScrapingLogic();
                //TODO : fix this one last2
                List<ScrapingLogic>? recordsTable = records.MgoScrapingLogic();
                Console.WriteLine("\nMGO bunker data\n");
                foreach (var data in recordsTable) { Console.WriteLine($"Date: {data.DayofMonth}, Price: {data.Price}, High: {data.High}, Low: {data.Low}"); }
                // populating CSV file
                csv.WriteRecords(recordsTable);
                Console.WriteLine("\nPress anything to return to initial menu");
                Console.ReadKey();
               
            }
        }
        public void VlsfoCsvOutputs()
        {

            using (var writer = new StreamWriter(@"C:\Users\nikolaos.skliris\Desktop\WorkRelatedLearning\StaticScraper\ShipBunkerScrapper\ScrapingResults\Vlsfo_Output.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                ScrapingLogic records = new ScrapingLogic();
                //TODO : fix this one last
                List<ScrapingLogic>? recordsTable = records.VlsfoScrapingLogic();

                Console.WriteLine("\nVLSFO bunker data\n");
                foreach (var data in recordsTable) { Console.WriteLine($"Date: {data.DayofMonth}, Price: {data.Price}, High: {data.High}, Low: {data.Low}"); }
                // populating CSV file
                csv.WriteRecords(recordsTable);
                Console.WriteLine("\nPress anything to return to initial menu");
                Console.ReadKey();
                

            }
        }



    }
}
