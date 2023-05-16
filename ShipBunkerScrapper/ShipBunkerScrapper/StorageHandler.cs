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
            string MgoCsvFile = "Mgo.Csv";
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), MgoCsvFile);
            using (var writer = new StreamWriter(FilePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                ScrapingLogic records = new ScrapingLogic();
                //TODO : fix this one last2
                List<ScrapingLogic>? recordsTable = records.MgoScrapingLogic();
                foreach (var date in recordsTable)
                { 
                    date.DayofMonth = records.IsoFormatConverter(date.DayofMonth);
                }


                Console.WriteLine("\nMGO bunker data\n");
                foreach (var data in recordsTable) { Console.WriteLine($"Date: {data.DayofMonth}, Price: {data.Price}, High: {data.High}, Low: {data.Low}"); }
                
                csv.WriteRecords(recordsTable);
            }
        }
        public void VlsfoCsvOutputs()
        {
            string VlsfoCsvFile = "Vlsfo.Csv";
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), VlsfoCsvFile);
            using (var writer = new StreamWriter(FilePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                ScrapingLogic records = new ScrapingLogic();
                //TODO : fix this one last
                List<ScrapingLogic>? recordsTable = records.VlsfoScrapingLogic();
                foreach(var date in recordsTable) 
                {
                    date.DayofMonth = records.IsoFormatConverter(date.DayofMonth);
                }
                Console.WriteLine("\nVLSFO bunker data\n");
                foreach (var data in recordsTable) { Console.WriteLine($"Date: {data.DayofMonth}, Price: {data.Price}, High: {data.High}, Low: {data.Low}"); }
               
                csv.WriteRecords(recordsTable);  
            }
        }



    }
}
