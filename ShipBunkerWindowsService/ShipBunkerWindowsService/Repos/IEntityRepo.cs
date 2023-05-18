using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBunkerWindowsService.Repos
{
    public interface IEntityRepo<TEntity>
    {
        //What functions can be reused indepedently 
         HtmlDocument DocumentLoader();
         List<FinancialData>? ScrapingLogic(HtmlDocument loadedDoc);
         
         void CsvOutput(List<FinancialData> ScrapingData);
        
        
        //------------------------------------------------------------//
         bool DayOfWeekCheck();
         string? IsoFormatConverter(string DayOfMonth); 



    }
}
