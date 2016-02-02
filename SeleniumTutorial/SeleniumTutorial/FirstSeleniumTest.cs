using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTutorial
{
    [TestFixture]
    public class FirstSeleniumTest
    {

        [Test]
        public void Selenium_test()
        {
            // Initialize the Chrome Driver
            using (var driver = new ChromeDriver())
            {
                // Go to the home page
                driver.Navigate().GoToUrl("http://testing-ground.scraping.pro/login");

                // Get the page elements
                var userNameField = driver.FindElementById("usr");
                var userPasswordField = driver.FindElementById("pwd");
                var loginButton = driver.FindElementByXPath("//input[@value='Login']");

                // Type user name and password
                userNameField.SendKeys("admin");
                userPasswordField.SendKeys("12345");

                // and click the login button
                loginButton.Click();

                // Extract the text and save it into result.txt
                var result = driver.FindElementByXPath("//div[@id='case_login']/h3").Text;
                File.WriteAllText("result.txt", result);

                // Take a screenshot and save it into screen.png
                driver.GetScreenshot().SaveAsFile(@"screen.png", ImageFormat.Png);
            }
        }

        [Test]
        public void Selenium_test_2()
        {
            // Initialize the Chrome Driver
            using (var driver = new ChromeDriver())
            {
                // Go to the home page
                driver.Navigate().GoToUrl("file:///C:/Repos/TestSolution/SeleniumTutorial/SeleniumTutorial/bin/Debug/Main.html");

                // Get the page elements
                var userNameField = driver.FindElementById("lst-ib");
                var searchButton = driver.FindElement(By.XPath("//input[@type='submit']"));

                // Type user name and password
                userNameField.SendKeys("salsa");

                // and click the login button
                searchButton.Click();

                // Extract the text and save it into result.txt
                //var result = driver.FindElementByXPath("//div[@id='case_login']/h3").Text;
                //File.WriteAllText("result.txt", result);

                // Take a screenshot and save it into screen.png
                driver.GetScreenshot().SaveAsFile(@"screen.png", ImageFormat.Png);
            }
        }

    }
}
