using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace StaticScraper2
{

    //class to store the Elements of our scraping
    public class Episodes
    {
        public string? OverallNumber { get; set; }
        public string? Title { get; set; }
        public string? Directors { get; set; }
        public string? WrittenBy { get; set; }
        public string? ReleaseDate { get; set; }

        public Episodes() { }

    }
}


