using CsvHelper;
using CsvHelper.TypeConversion;
using HtmlAgilityPack;
using ShipBunkerWindowsService.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBunkerWindowsService.Repos
{
    public class ScrapingRepo : IEntityRepo<FinancialData,ScrapingResourses>
    {
        public void CsvOutput(List<FinancialData> ScrapingData,string csvOutputName)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), csvOutputName);

            using (var writer = new StreamWriter(FilePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                FinancialData records = new FinancialData();
                foreach (var date in ScrapingData)
                {
                    date.DayofMonth = IsoFormatConverter(date.DayofMonth);
                }               
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

        public HtmlDocument DocumentLoader(string siteURL)
        {  
            var web = new HtmlWeb();
            var document = web.Load(siteURL);
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

        public List<FinancialData>? ScrapingLogic(HtmlDocument loadedDoc,string xpath)
        {
            List<FinancialData>? ScrapingData = new List<FinancialData>();
           
            //var web = new HtmlWeb(); --> to be removed
            var nodes = loadedDoc.DocumentNode.SelectNodes(xpath);
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
