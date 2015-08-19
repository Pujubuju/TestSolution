using System.Linq;
using NUnit.Framework;
using TestSolution.Tests.Common;

namespace TestSolution.Web.Crawling.Tests
{
    internal class LinkExtractorTests : BaseTest
    {

        private LinkExtractor _linkExtractor;

        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _linkExtractor = new LinkExtractor();
        }

        [TestCase("<a href='http://www.google.pl' />", "http://www.google.pl")]
        [TestCase("<a href=\"http://www.google.pl\" />", "http://www.google.pl")]
        [TestCase("<a href='https://www.google.pl' />", "https://www.google.pl")]
        [TestCase("<a href='https://mic.pl' />", "https://mic.pl")]
        public void Extracts_link_from_html_tag(string  html, string expectedExtractedLink)
        {
            // Act
            string link = _linkExtractor.ExtractLinks(html).First();

            // Assert
            Assert.AreEqual(expectedExtractedLink, link);
        }

    }
}
