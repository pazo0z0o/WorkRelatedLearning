using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization;
using System.Threading;
using CsvHelper;

namespace ShipBunkerScrapper
{
    public class ScrapingLogic
    {
        public ScrapingLogic() { }
        public string? DayofMonth { get; set; }
        public string? Price { get; set; }
        public string? High { get; set; }
        public string? Low { get; set; }



        //for the sat-sun check, so that the scraping isn't executed
        enum DayOfWeekEnum
        {
            Monday = 0,
            Tuesday = 1,
            Wednesday = 2,
            Thursday = 3,
            Friday = 4,
            Saturday = 5,
            Sunday = 6
        }

        String MgoURL = "https://shipandbunker.com/prices/av/global/av-glb-global-average-bunker-price#MGO";
        String VlsfoURL = "https://shipandbunker.com/prices/av/global/av-glb-global-average-bunker-price#VLSFO";

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

            var doc = document.DocumentLoader(MgoURL);
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

        public void MgoDelegate(Object? list) 
        {
            StorageHandler mgoCSV = new StorageHandler();
            mgoCSV.VlsfoCsvOutputs();

        }

        public void VlsfoDelegate(Object? list)
        {
            StorageHandler vlsfoCSV = new StorageHandler();
            vlsfoCSV.VlsfoCsvOutputs();
        }

        public bool DayOfWeekCheck()
        {
            bool isNotWeekend = false;
            DayOfWeekEnum currentDay = (DayOfWeekEnum)DateTime.UtcNow.DayOfWeek;
            if (currentDay == DayOfWeekEnum.Saturday || currentDay == DayOfWeekEnum.Sunday)
            {
                isNotWeekend = true;
            }
            return isNotWeekend;
        }


        public void VlsfoScrapingTimer()
        {
            var now = DateTime.UtcNow;
            int intervalTime = 1800000;

            TimerCallback mgoCallback = new TimerCallback(MgoDelegate);
            Timer timerMGO = new Timer(mgoCallback, null, 0, intervalTime);

            TimerCallback vlsfoCallback = new TimerCallback(VlsfoDelegate);
            Timer timerVLSFO = new Timer(vlsfoCallback, null, 0, intervalTime);

            var next930am = new DateTime(now.Year, now.Month, now.Day, 9, 30, 0, DateTimeKind.Utc);
            var next2130pm = new DateTime(now.Year, now.Month, now.Day, 21, 30, 0, DateTimeKind.Utc);

            //inelegant -- might return later
            ScrapingLogic timingLogic = new ScrapingLogic();
            if (timingLogic.DayOfWeekCheck() && now >= next930am && now <= next2130pm)
            {
                


            }           
           


            
           
        }
    }
}
