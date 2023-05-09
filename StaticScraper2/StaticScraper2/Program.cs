// See https://aka.ms/new-console-template for more information
using HtmlAgilityPack;
using System.Globalization;
using CsvHelper;

namespace StaticScraper2
{

    public class Program
    {
        public static void Main(string[] args) {

            String url = "https://en.wikipedia.org/wiki/List_of_SpongeBob_SquarePants_episodes";
            var web = new HtmlWeb();
            var document = web.Load(url);


            //I will select all <table row elements> from all tables 
            //This time I will use XPATH queries instead of CSS selectors
            //The below query means: From all the id attributes with 'mw-content-text' as a name, select the first(positionaly) div,
            //then grab the (position()>1 up to 15) all tables with episodes and from them, all the episode rows included.
            //
            var nodes = document.DocumentNode.SelectNodes("//*[@id='mw-content-text']/div[1]/table[position()>=1 and position()<15]/tbody/tr[position()>1]");
            // initializing the list of objects that will
            // store the scraped data

            List<Episodes> episodes = new List<Episodes>();
            int i = 0;
            //looping to get all the data from all the tables 

            foreach ( var node in nodes ) {
                    i++;
                var overallNumberNode = node.SelectSingleNode("th[1]");
                var titleNode = node.SelectSingleNode("td[2]");
                var directorsNode = node.SelectSingleNode("td[3]");
                var writtenByNode = node.SelectSingleNode("td[4]");
                var releaseDateNode = node.SelectSingleNode("td[5]");


                //null checking to ensure everyone of the nodes is filled.
                //should have a node difference always on the console output,
                //since the format of the table has rows merged for every episode element
                
                if (overallNumberNode != null && titleNode != null && directorsNode != null && writtenByNode != null && releaseDateNode != null)
                {
                    Console.WriteLine($"Node #{i} is not null!");
                    episodes.Add(new Episodes()
                    {
                        OverallNumber = HtmlEntity.DeEntitize(overallNumberNode.InnerText),
                        Title = HtmlEntity.DeEntitize(titleNode.InnerText),
                        Directors = HtmlEntity.DeEntitize(directorsNode.InnerText),
                        WrittenBy = HtmlEntity.DeEntitize(writtenByNode.InnerText),
                        ReleaseDate = HtmlEntity.DeEntitize(releaseDateNode.InnerText)
                    });
                }
            }
            
            //initializing CSV file
            //use of 'using' for immediate disposing of resources after we are done
            using (var writer = new StreamWriter("output.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                // populating CSV file
                csv.WriteRecords(episodes);
            }


        }
    }
}