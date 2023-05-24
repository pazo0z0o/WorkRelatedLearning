using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBunkerWindowsService.Models
{
    public class ScrapingResourses
    {
        
       public String? MgoUrl { get; set; }
       public String? VlsfoUrl { get; set; }
      
       public String? MgoXpath { get; set; }
       public String? VlsfoXpath { get; set; }
          
       public string? MgoCsvFile { get; set; }
       public string? VlsfoCsvFile { get; set; }
       
       //---------------Running Interval ---------//
       public int IntervalTime { get; set; }
        public string StartRunTime { get; set; }
        public string EndRunTime { get; set; }

    }
}
