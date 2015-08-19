using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;

namespace TestSolution.Web.Crawling
{
    public class LinksFinder
    {

        public List<string> GetLinksFromFile(string fileName)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(File.ReadAllText(fileName));
            return GetLinks(doc);
        }

        public List<string> GetLinksFromUrl(string url)
        {        
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(url);
            return GetLinks(doc);
        }

        private static List<string> GetLinks(HtmlDocument doc)
        {
            List<string> links = new List<string>();
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                links.Add(link.OuterHtml);
            }
            return links;
        }

    }
}
