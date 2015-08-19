using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TestSolution.Web.Crawling
{
    public class LinkExtractor
    {
        public List<string> ExtractLinks(string htmlLinkTag)
        {
            List<string> links = new List<string>();
            var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match m in linkParser.Matches(htmlLinkTag))
            {
                links.Add(m.Value);
            }
            return links;
        }
    }
}
