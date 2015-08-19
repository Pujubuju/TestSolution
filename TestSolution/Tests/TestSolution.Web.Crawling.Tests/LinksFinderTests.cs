using NUnit.Framework;
using TestSolution.Tests.Common;

namespace TestSolution.Web.Crawling.Tests
{
    public class LinksFinderTests : BaseTest
    {
        private LinksFinder _linksFinder;

        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _linksFinder = new LinksFinder();
        }

        [Test]
        public void Gets_all_links_from_file()
        {
            // Act
            var links = _linksFinder.GetLinksFromFile("TestFiles\\Index.html");

            // Assert
            Assert.AreEqual(2, links.Count);
        }

        [Test]
        public void Returns_valid_links_from_file()
        {
            // Act
            var links = _linksFinder.GetLinksFromFile("TestFiles\\Index.html");

            // Assert
            Assert.AreEqual("<a href=\"Page1.html\"></a>", links[0]);
            Assert.AreEqual("<a href=\"http://www.google.pl\"></a>", links[1]);
        }

    }
}
