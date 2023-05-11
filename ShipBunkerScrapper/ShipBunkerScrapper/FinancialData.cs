using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBunkerScrapper
{
    public class FinancialData
    {
        //Here are the properties that will hold the scraping data and 
        
        private DateTime Date { get; set; }
        private double Price { get; set; }
        private double High { get; set; }
        private double Low { get; set; }

        public FinancialData()
        {
            
        }

    }
}
