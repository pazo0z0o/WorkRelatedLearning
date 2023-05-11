using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ShipBunkerScrapper
{
    public class DocumentLoaders
    {
        
        //Will have classes that handle the document loading of the VLSFO/MGO pages of the website
        public DocumentLoaders() { }

        protected HtmlDocument MgoDocument(string url) {
            
            string success = string.Empty;

            var web = new HtmlWeb();
            var document = web.Load(url);
            while (document is null) {
                if (document != null)
                {
                    success = "Document loaded successfully ";
                    Console.WriteLine(success);
                    break;
                }
            }
          

            return document; //Could need a change ager
        }


        


    }
}
