// See https://aka.ms/new-console-template for more information

using HtmlAgilityPack;
using System.Net.Http;

namespace StaticScraper1 
{ 
class Program 
    {
    static void Main(string[] args) 
        {
            //Simple web scraper using weather.com landing page to test scraping with.
            //Gathered the the location, time, temperature and conditions for a the current day.
            //Used HttpClient and HtmlDocument to fetch(get) the data from the URL and then Document them to a htmlDocument

            //fetch from site to be scraped -- GET request
            String url = "https://weather.com/el-GR/weather/today/l/GRXX0004:1:GR?Goto=Redirected";
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url).Result;

            //document and load website 
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            //Get location
            var locationsElement = htmlDocument.DocumentNode.SelectSingleNode("//h1[@class='CurrentConditions--location--1YWj_']");
            var location = locationsElement.InnerText.Trim();
            
            //Get Time 
            var timeElement = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='CurrentConditions--timestamp--1ybTk']");
            var time = timeElement.InnerText.Trim();
            Console.WriteLine($"In {location} | { time}\n");


            //Get Temperature
            var temperatureElement = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='CurrentConditions--tempValue--MHmYY']");
            var temperature = temperatureElement.InnerText.Trim();
            Console.WriteLine("Temperature: " + temperature);

            //Get conditions
            var conditionsElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='CurrentConditions--phraseValue--mZC_p']");
            var conditions = conditionsElement.InnerText.Trim();
            Console.WriteLine("Condition: " + conditions);






















        }
    
    }





}