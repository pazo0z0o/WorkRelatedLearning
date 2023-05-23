using CsvHelper;
using CsvHelper.TypeConversion;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using ShipBunkerWindowsService.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBunkerWindowsService.Repos
{
    public class ScrapingRepo : IEntityRepo<FinancialData>
    {
       // private readonly ScrapingResourses _scrapingResourses; //TODO: CARE FOR THIS -- Might be unused/uneccessary
        private readonly ILogger<ScrapingRepo> _logger;
        public ScrapingRepo(ILogger<ScrapingRepo> logger)
        {
           // _scrapingResourses = scrap.Value;  //It takes the value of the configuration in the appSettings 
            _logger = logger;

        }

        public List<FinancialData>? ScrapingLogic(HtmlDocument loadedDoc, string xpath)
        {
            List<FinancialData>? ScrapingData = new List<FinancialData>();

            
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
            _logger.LogInformation("Scraping performed successfuly");
            return ScrapingData;
        }

        public void CsvOutput(List<FinancialData> ScrapingData,string csvOutputName)
        {
            //correctly sets the current Directory to that of the windows service's
            Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            try
            {
                _logger.LogInformation("CsvOutput initiated successfuly");
                _logger.LogInformation("================================================");
                string FilePath = Path.Combine(Directory.GetCurrentDirectory(), csvOutputName);
                //_logger.LogInformation("Filepath is:  {0}",FilePath);
                using (var writer = new StreamWriter(FilePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    //FinancialData records = new FinancialData();
                    foreach (var date in ScrapingData)
                    {
                        date.DayofMonth = IsoFormatConverter(date.DayofMonth);
                    }
                    csv.WriteRecords(ScrapingData);

                }
            }
            catch (Exception exception)
            {
                _logger.LogError("Something went wrong {ex}",exception.Message);
                
            }
            
        }
        public HtmlDocument DocumentLoader(string siteURL)
        {  
            var web = new HtmlWeb();
            var document = web.Load(siteURL);
            _logger.LogInformation("Document loaded successfuly");
            return document;
        }

        public bool ValidRunningTime()
        {//TODO : check if the performance of the check is the desired one 
            
            DateTime currentTime = DateTime.UtcNow;
           //TODO : Determine its usefulness and delete if neccessary
            var next930am = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 9, 30, 0, DateTimeKind.Utc);
            var next2130pm = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 21, 30, 0, DateTimeKind.Utc);
            var currentDay = currentTime.DayOfWeek;
            //bool isValid = ((currentDay >= DayOfWeekEnum.Monday && currentDay <= DayOfWeekEnum.Friday) && (currentTime >= next930am && currentTime <= next2130pm));

            bool isValid = (currentDay >= DayOfWeek.Monday && currentDay <= DayOfWeek.Friday) 
                &&(currentTime.TimeOfDay >= TimeSpan.Parse("09:30:00") 
                && currentTime.TimeOfDay <= TimeSpan.Parse("21:30:00"));

            return isValid;
        }

        public string? IsoFormatConverter(string DayOfMonth)
        {
            string[] splitDayOfMonth = DayOfMonth.Split(' ');
            DayOfMonth = splitDayOfMonth[1] + " " + splitDayOfMonth[2];
            DateTime isoDate = Convert.ToDateTime(DayOfMonth);
            string isoToString = isoDate.ToString("o") + "Z";

            return isoToString;
        }

        
    }
}
