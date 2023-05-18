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
        //What functions can be reused indepedently -- Could do it with generics but no great point in it
        
        //Functional Logic
        HtmlDocument DocumentLoader();
         List<FinancialData>? ScrapingLogic(HtmlDocument loadedDoc);
         
         void CsvOutput(List<FinancialData> ScrapingData);

        //------------------------------------------------------------//
        //Additional small ones 
        bool DayOfWeekCheck();
         string? IsoFormatConverter(string DayOfMonth); 



    }
}
