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
        //Interface used also in the D.I part -- Could do it with generics 

        //Functional Logic

        HtmlDocument DocumentLoader(string siteURL);

        List<TEntity>? ScrapingLogic(HtmlDocument loadedDoc, string xpath);

        void CsvOutput(List<TEntity> ScrapingData, string csvOutputName);

        //------------------------------------------------------------//
        //Additional small ones 

        //bool ValidRunningTime();
        bool ValidRunningTime(string startRun, string endRun);
        string? IsoFormatConverter(string DayOfMonth);

        //TODO : ShipBunkerWindowsService DevelopmentShell command
        //sc.exe create ShipBunkerWindowsService binPath=C:\Users\k.stamos\Desktop\WorkRelatedLearning\ShipBunkerWindowsService\ShipBunkerWindowsService\ShipBunkerWindowsService.exe

    }
}
