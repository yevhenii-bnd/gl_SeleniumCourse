using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrigoTestProject
{
    internal class GenerateReportTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;


        [SetUp]
        public void OneTimeSetup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");

            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); // Explicit Wait

            driver.Navigate().GoToUrl(Environment.GetEnvironmentVariable("ENT_QA_BASE_URL") + "/corpnet/Login.aspx");
            driver.FindElement(By.Name("username")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_USER"));
            driver.FindElement(By.Name("password")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_PASS"));
            driver.FindElement(By.Name("_companyText")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_COMPANY"));
            driver.FindElement(By.CssSelector(".login-submit-button")).Click();
        }

        [TearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }


        [Test]
        public void VerifyFirstReportGeneration()
        {
            var originalWindow = driver.CurrentWindowHandle;
            driver.Navigate().GoToUrl($"{Environment.GetEnvironmentVariable("ENT_QA_BASE_URL")}/corpnet/report/reportlist.aspx");

            var reportLink = By.XPath("//td[@data-column='DisplayAs'][1]/a");
            var reportLinkElement = wait.Until(driver => driver.FindElement(reportLink));
            var reportName = driver.FindElement(reportLink).Text;

            driver.FindElement(reportLink).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".showHeaders .k-switch-container"))).Click();
            driver.FindElement(By.CssSelector(".id-generate-button")).Click();

            wait.Until(driver => driver.WindowHandles.Count == 2);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            wait.Until(driver => driver.FindElement(By.XPath($"//span[contains(text(),'{reportName}')]")));
            Assert.IsTrue(driver.FindElements(By.XPath($"//span[contains(text(),'{reportName}')]")).Count > 0, "Header not found");

            driver.SwitchTo().Window(originalWindow);
            driver.FindElement(By.CssSelector(".id-btn-close")).Click();
        }
    }
}