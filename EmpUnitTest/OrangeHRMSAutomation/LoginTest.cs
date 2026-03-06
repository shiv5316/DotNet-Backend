using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace OrangeHRMSAutomation
{
    [TestFixture]
    public class LoginTest
    {
        private IWebDriver? driver;
        private ExtentReports? extent;
        private ExtentTest? test;

        [OneTimeSetUp]
        public void SetupReporting()
        {
            var htmlReporter = new ExtentHtmlReporter("TestReport.html");
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [SetUp]
        public void StartBrowser()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");

            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/");

            System.Threading.Thread.Sleep(2000);
        }

        [Test]
        public void LoginToOrangeHRMS()
        {
            test = extent!.CreateTest("Login Test").Info("Test Started");

            try
            {
                driver!.FindElement(By.Name("username")).SendKeys("Admin");
                driver.FindElement(By.Name("password")).SendKeys("admin123");
                driver.FindElement(By.CssSelector("button[type='submit']")).Click();

                System.Threading.Thread.Sleep(3000);

                Assert.That(driver.Url, Does.Contain("dashboard"), "Dashboard URL not found after login");
                test.Pass("Login successful");
            }
            catch (Exception ex)
            {
                test.Fail($"Test failed: {ex.Message}");
                throw;
            }
        }

        [TearDown]
        public void EndTest()
        {
            driver?.Quit();
        }

        [OneTimeTearDown]
        public void GenerateReport()
        {
            extent?.Flush();
        }
    }
}

