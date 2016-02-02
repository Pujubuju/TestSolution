using System;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecflowTutorial.Steps
{
    [Binding]
    public class CalculatorSteps
    {

        private readonly Calculator _calculator = new Calculator();
        private int a, b, c;
        private ArgumentOutOfRangeException _divideByZeroException;

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {
            a = p0;
        }
        
        [Given(@"I have also entered (.*) into the calculator")]
        public void GivenIHaveAlsoEnteredIntoTheCalculator(int p0)
        {
            b = p0;
        }
        
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            c = _calculator.Add(a, b);
        }

        [When(@"I press subtract")]
        public void WhenIPressSubtract()
        {
            c = _calculator.Subtract(a, b);
        }

        [When(@"I press multiply")]
        public void WhenIPressMultiply()
        {
            c = _calculator.Multiply(a, b);
        }

        [When(@"I press divide")]
        public void WhenIPressDivide()
        {
            try
            {
                c = _calculator.Divide(a, b);
            }
            catch (ArgumentOutOfRangeException e)
            {
                _divideByZeroException = e;
            }
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            Assert.AreEqual(p0, c);
        }

        [Then(@"the exception should be thrown")]
        public void ThenTheExceptionShouldBeThrown()
        {
            Assert.IsNotNull(_divideByZeroException);
        }
    }
}
