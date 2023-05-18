using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBunkerWindowsService.Models
{
    public class ScrapingResourses
    {
        //TODO : Turn them into appsettings since they are standard BUT FOR NOW keep them in 
        String MgoUrl = "https://shipandbunker.com/prices/av/global/av-glb-global-average-bunker-price#MGO";
        String VlsfoUrl = "https://shipandbunker.com/prices/av/global/av-glb-global-average-bunker-price#VLSFO";

        String MgoXpath = "//*[@id='_MGO']/h3/table/tbody/tr[position()<=11]";
        String VlsfoXpath = "//*[@id='_VLSFO']/h3/table/tbody/tr[position()<=11]";
            
        string MgoCsvFile = "Mgo.Csv";
        string VlsfoCsvFile = "Vlso.Csv";



        //---------------Running Interval ---------//
        public int IntervalTime = 60 * 1000; 
    }
}
