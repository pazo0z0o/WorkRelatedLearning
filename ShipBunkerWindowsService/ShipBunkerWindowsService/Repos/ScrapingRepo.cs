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
        private readonly ILogger<ScrapingRepo> _logger;
        public ScrapingRepo(ILogger<ScrapingRepo> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Checks the current time and runs or doesn't run according to the specified times we want.
        /// </summary>
        /// <param name="loadedDoc"> The loaded HtmlDocument that was returned by Document loader</param>
        /// <param name="xpath">The Xpath of the element we want to scrape from the table</param>
        /// <returns>A <see cref="List{FinancialData}" /> that will be used for the export into .Csv format for our output</returns>
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
        /// <summary>
        /// Prints the scraped data into CSV output
        /// </summary>
        /// <param name="ScrapingData">List of all the data that was scraped by the ScrapingLogic function</param>
        /// <param name="csvOutputName">name of the .csv output file</param>
        public void CsvOutput(List<FinancialData> ScrapingData,string csvOutputName)
        {
            //correctly sets the current Directory to that of the windows service's
          Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            try
            {
                _logger.LogInformation("CsvOutput initiated successfuly");
                _logger.LogInformation("================================================");
                string FilePath = Path.Combine(Directory.GetCurrentDirectory(), csvOutputName);
               
                using (var writer = new StreamWriter(FilePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
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
        /// <summary>
        /// Loads the intented website to be scraped 
        /// </summary>
        /// <param name="siteURL"></param>
        /// <returns>A <see cref="HtmlDocument" /> that is going to be used by ScrapingLogic </returns>
        public HtmlDocument DocumentLoader(string siteURL)
        {  
            var web = new HtmlWeb();
            var document = web.Load(siteURL);
            _logger.LogInformation("Document loaded successfuly");
            return document;
        }

        /// <summary>
        /// Checks the current time and runs or doesn't run according to the specified times we want.
        /// </summary>
        /// <param name="startRun">The start point of the running period we want</param>
        /// <param name="startRun">The end point of the running period we want</param>
        /// <returns>A <see cref="bool"/> that needs to be TRUE for the app to function</returns>
        public bool ValidRunningTime(string startRun, string endRun)
        {//TODO : modify for appsettings.json input |  Keep prior code for a return to the stable state of the app

            DateTime currentTime = DateTime.UtcNow;
            string[] splitStartTime = startRun.Split(':');
            string[] splitEndTime = endRun.Split(':');
           // var startRunTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 9, 30, 0, DateTimeKind.Utc);
           // var endRunTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 21, 30, 0, DateTimeKind.Utc);

            var startRunTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, Int32.Parse(splitStartTime[0]), Int32.Parse(splitStartTime[1]), Int32.Parse(splitStartTime[2]), DateTimeKind.Utc);
            var endRunTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, Int32.Parse(splitEndTime[0]), Int32.Parse(splitEndTime[1]), Int32.Parse(splitEndTime[2]), DateTimeKind.Utc);

            var currentDay = currentTime.DayOfWeek;

            bool isValid = (currentDay >= DayOfWeek.Monday && currentDay <= DayOfWeek.Friday) 
                &&(currentTime >= startRunTime
                && currentTime <= endRunTime);

            return isValid;
        }
        /// <summary>
        /// Converts the Date column in the scraped data, into Iso time format.
        /// </summary>
        /// <param name="DayOfMonth">The day of the month from the original scraped data that will be converted into Iso format</param>
        /// <returns>A <see cref="string"/> that is the conversion of the DateTime.Local to UTC time format </returns>
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
