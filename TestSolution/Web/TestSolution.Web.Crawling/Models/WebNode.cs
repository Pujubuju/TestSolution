using System.Collections.Generic;

namespace TestSolution.Web.Crawling.Models
{
    public class WebNode
    {

        public List<WebNode> WebNodes { get; set; }
        public WebNode Parent { get; set; }
        public string Url { get; set; }

        public WebNode(string url)
        {
            Url = url;
        }

        public WebNode(WebNode parentNode, string url)
            :this(url)
        {
            Parent = parentNode;
            WebNodes = new List<WebNode>();
        }

    }
}
