using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace OrangeHRMSAutomation
{
    public class LoginTest
    {
        IWebDriver driver;
        ExtentReports extent;
        ExtentTest test;
        WebDriverWait wait;

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
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/");
        }

        [Test]
        public void LoginToOrangeHRMS()
        {
            test = extent.CreateTest("Login Test").Info("Test Started");

            try
            {
                var usernameWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                usernameWait.Until(driver => driver.FindElement(By.Name("username")));
                driver.FindElement(By.Name("username")).SendKeys("Admin");
                driver.FindElement(By.Name("password")).SendKeys("admin123");
                driver.FindElement(By.CssSelector("button[type='submit']")).Click();

                var dashboardWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                dashboardWait.Until(d => d.Url.Contains("dashboard"));
                Assert.That(driver.Url, Does.Contain("dashboard"));
                test.Pass("Login successful");
            }
            catch (Exception ex)
            {
                test.Fail("Test failed: " + ex.Message);
                throw;
            }
        }

        [TearDown]
        public void EndTest()
        {
            driver.Quit();
        }

        [OneTimeTearDown]
        public void GenerateReport()
        {
            extent.Flush();
        }
    }
}
