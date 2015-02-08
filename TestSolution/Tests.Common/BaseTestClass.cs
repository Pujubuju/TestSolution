using NUnit.Framework;

namespace TestSolution.Tests.Common
{

    [TestFixture]
    public class BaseTestClass
    {
        [SetUp]
        public virtual void SetUp()
        {
            
        }

        [TearDown]
        public virtual void TearDown()
        {

        }

        [TestFixtureSetUp]
        public virtual void TestFixtureSetUp()
        {

        }

        [TestFixtureTearDown]
        public virtual void TestFixtureTearDown()
        {

        }

    }
}
