using CsvHelper;
using CsvHelper.TypeConversion;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBunkerWindowsService.Repos
{
    public class VlsfoRepo : IEntityRepo<FinancialData>
    {
        public void CsvOutput(List<FinancialData> ScrapingData)
        {
            string VlsfoCsvFile = "Vlsfo.Csv";
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), VlsfoCsvFile);

            using (var writer = new StreamWriter(FilePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                FinancialData records = new FinancialData();
                foreach (var date in ScrapingData)
                {
                    date.DayofMonth = IsoFormatConverter(date.DayofMonth);
                }
                //Console.WriteLine("\nMGO bunker data\n");
                //foreach (var data in ScrapingData) { Console.WriteLine($"Date: {data.DayofMonth}, Price: {data.Price}, High: {data.High}, Low: {data.Low}"); }

                csv.WriteRecords(ScrapingData);

            }
        }

        public bool DayOfWeekCheck()
        {
            bool isNotWeekend = false;
            DayOfWeekEnum currentDay = (DayOfWeekEnum)DateTime.UtcNow.DayOfWeek;
            if (currentDay != DayOfWeekEnum.Saturday || currentDay != DayOfWeekEnum.Sunday) { isNotWeekend = true; }
            return isNotWeekend;
        }

        public HtmlDocument DocumentLoader()
        {  //TODO : See if I can feed it the URL in the worker
            String VlsfoURL = "https://shipandbunker.com/prices/av/global/av-glb-global-average-bunker-price#VLSFO";
            string success = string.Empty;
            var web = new HtmlWeb();
            var document = web.Load(VlsfoURL);
            return document;
        }

        public string? IsoFormatConverter(string DayOfMonth)
        {
            string[] splitDayOfMonth = DayOfMonth.Split(' ');
            DayOfMonth = splitDayOfMonth[1] + " " + splitDayOfMonth[2];
            DateTime isoDate = Convert.ToDateTime(DayOfMonth);
            string isoToString = isoDate.ToString("o") + "Z";

            return isoToString;
        }

        public List<FinancialData>? ScrapingLogic(HtmlDocument loadedDoc)
        {
            List<FinancialData>? ScrapingData = new List<FinancialData>();

            var web = new HtmlWeb();
            var nodes = loadedDoc.DocumentNode.SelectNodes("//*[@id='_VLSFO']/h3/table/tbody/tr[position()<=11]");
            foreach (var node in nodes)
            {
                ScrapingData.Add(new FinancialData()
                {
                    DayofMonth = HtmlEntity.DeEntitize(node.SelectSingleNode("th[1]").InnerText),
                    Price = HtmlEntity.DeEntitize(node.SelectSingleNode("td[1]").InnerText),
                    High = HtmlEntity.DeEntitize(node.SelectSingleNode("td[3]").InnerText),
                    Low = HtmlEntity.DeEntitize(node.SelectSingleNode("td[4]").InnerText)
                });
            }
            return ScrapingData;
        }
    }
}
