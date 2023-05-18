using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBunkerWindowsService.Repos
{
    public interface IEntityRepo<TEntity,SEntity>
    {
        //What functions can be reused indepedently -- Could do it with generics but no great point in it
        
        //Functional Logic
         HtmlDocument DocumentLoader(string siteURL);
         List<TEntity>? ScrapingLogic(HtmlDocument loadedDoc, string xpath);
         
         void CsvOutput(List<TEntity> ScrapingData,string csvOutputName);

        //------------------------------------------------------------//
        //Additional small ones 
        bool DayOfWeekCheck();
         string? IsoFormatConverter(string DayOfMonth); 



    }
}
