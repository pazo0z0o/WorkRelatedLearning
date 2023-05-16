using HtmlAgilityPack;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization;
using CsvHelper;
using CsvHelper.TypeConversion;

namespace ShipBunkerScrapper
{
    public class ScrapingLogic
    {
        //ctor
        public ScrapingLogic() { }
        //properties of The Lists used in scraping functions
        public string? DayofMonth { get; set; }
        public string? Price { get; set; }
        public string? High { get; set; }
        public string? Low { get; set; }

        //enum for checking and excluding execution of the timer 
        enum DayOfWeekEnum
        {
            Sunday = 0,
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6
        }
        
        //fields -- URLs of scraping functions and masterTimer
        String MgoURL = "https://shipandbunker.com/prices/av/global/av-glb-global-average-bunker-price#MGO";
        String VlsfoURL = "https://shipandbunker.com/prices/av/global/av-glb-global-average-bunker-price#VLSFO";
        private System.Threading.Timer masterTimer;

        public List<ScrapingLogic> MgoScrapingLogic()
        {
            List<ScrapingLogic>? ScrapingData = new List<ScrapingLogic>();
            DocumentLoaders document = new DocumentLoaders();
            var doc = document.DocumentLoader(MgoURL);
            var web = new HtmlWeb();
           
            var nodes = doc.DocumentNode.SelectNodes("//*[@id='_MGO']/h3/table/tbody/tr[position()<=11]");
            foreach (var node in nodes)
            {
                ScrapingData.Add(new ScrapingLogic()
                {
                    DayofMonth = HtmlEntity.DeEntitize(node.SelectSingleNode("th[1]").InnerText),
                    Price = HtmlEntity.DeEntitize(node.SelectSingleNode("td[1]").InnerText),
                    High = HtmlEntity.DeEntitize(node.SelectSingleNode("td[3]").InnerText),
                    Low = HtmlEntity.DeEntitize(node.SelectSingleNode("td[4]").InnerText)
                });
            }
            return ScrapingData;
        }


        public List<ScrapingLogic> VlsfoScrapingLogic()
        {
            List<ScrapingLogic>? ScrapingData = new List<ScrapingLogic>();
            DocumentLoaders document = new DocumentLoaders();
            var doc = document.DocumentLoader(VlsfoURL);
            var web = new HtmlWeb();

            var nodes = doc.DocumentNode.SelectNodes("//*[@id='_VLSFO']/h3/table/tbody/tr[position()<=11]");
            foreach (var node in nodes)
            {
                ScrapingData.Add(new ScrapingLogic()
                {
                    DayofMonth = HtmlEntity.DeEntitize(node.SelectSingleNode("th[1]").InnerText),
                    Price = HtmlEntity.DeEntitize(node.SelectSingleNode("td[1]").InnerText),
                    High = HtmlEntity.DeEntitize(node.SelectSingleNode("td[3]").InnerText),
                    Low = HtmlEntity.DeEntitize(node.SelectSingleNode("td[4]").InnerText)
                });
            }
            return ScrapingData;
        }

       
        public void MasterDelegate(Object? list)
        {
            StorageHandler multiCsv = new StorageHandler();
            multiCsv.VlsfoCsvOutputs();
            multiCsv.MgoCsvOutputs();
        }

        public bool DayOfWeekCheck()
        {
            
            bool isNotWeekend = false;
            DayOfWeekEnum currentDay = (DayOfWeekEnum)DateTime.UtcNow.DayOfWeek;
            if (currentDay != DayOfWeekEnum.Saturday || currentDay != DayOfWeekEnum.Sunday) { isNotWeekend = true; }
            return isNotWeekend;
        }

        public void MgoVlsfoScrapingTimer()
        {
            var now = DateTime.UtcNow;
            int intervalTime = 60000;
            TimerCallback masterCallback = new TimerCallback(MasterDelegate);
            var next930am = new DateTime(now.Year, now.Month, now.Day, 9, 30, 0, DateTimeKind.Utc);
            var next2130pm = new DateTime(now.Year, now.Month, now.Day, 21, 30, 0, DateTimeKind.Utc);
            ScrapingLogic timingLogic = new ScrapingLogic();
            if (timingLogic.DayOfWeekCheck() && now >= next930am && now <= next2130pm)
            {
                masterTimer = new Timer(masterCallback, null, 0, intervalTime);
            }
        }

        public string IsoFormatConverter(string DayOfMonth) 
        {
        
            string[] splitDayOfMonth = DayOfMonth.Split(' ');
            DayOfMonth = splitDayOfMonth[1] + " " + splitDayOfMonth[2];
            DateTime isoDate = Convert.ToDateTime(DayOfMonth);
            string isoToString = isoDate.ToString("o")+"Z"; 

            return isoToString;
        }

    }
}
