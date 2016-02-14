using MyLibrary;
using Xunit;

namespace NetCoreTutorial.Tests
{
    public class GreeterTests
    {

        [Fact]
        public void Should_greet()
        {
            Greeter greeter = new Greeter();
            Assert.Equal("Good moring Master!", greeter.Greet());
        }

    }
}
