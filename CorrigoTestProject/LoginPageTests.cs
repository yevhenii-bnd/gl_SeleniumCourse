using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CorrigoTestProject

{
    public class LoginPageTests
    {
        IWebDriver driver;
        string loginUrl;
        string userId;
        string password;
        string companyName;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            driver = new ChromeDriver(options);

            loginUrl = Environment.GetEnvironmentVariable("ENT_QA_BASE_URL") + "/corpnet/Login.aspx";
            userId = Environment.GetEnvironmentVariable("ENT_QA_USER");
            password = Environment.GetEnvironmentVariable("ENT_QA_PASS");
            companyName = Environment.GetEnvironmentVariable("ENT_QA_COMPANY");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }


        [Test]
        public void VerifySuccessfulLogin()
        {
            driver.Navigate().GoToUrl(loginUrl);
            driver.FindElement(By.CssSelector("#username")).SendKeys(userId);
            driver.FindElement(By.CssSelector("#password")).SendKeys(password);
            driver.FindElement(By.CssSelector("#_companyText")).SendKeys(companyName);
            driver.FindElement(By.CssSelector(".login-submit-button")).Click();
            Assert.That(driver.FindElement(By.CssSelector(".menu-user")).Text, Is.EqualTo("Quality Assurance"));
        }


        [Test]
        public void VerifyForgotPasswordLink()
        {
            driver.Navigate().GoToUrl(loginUrl);
            driver.FindElement(By.Id("forgotPasswordLink")).Click();
            Assert.That(driver.Url, Is.EqualTo(Environment.GetEnvironmentVariable("ENT_QA_BASE_URL") + "/CorpNet/Login.aspx/PasswordRecovery"));
        }



        public static IEnumerable<TestCaseData> LoginNegativeTestCases()
        {
            string userId = Environment.GetEnvironmentVariable("ENT_QA_USER");
            string password = Environment.GetEnvironmentVariable("ENT_QA_PASS");
            string companyName = Environment.GetEnvironmentVariable("ENT_QA_COMPANY");

            Random rnd = new Random();
            string wrongUserName = userId + rnd.Next(1000, 9000).ToString();
            string wrongPassword = password + rnd.Next(1000, 9000).ToString();
            string wrongCompanyName = companyName + rnd.Next(1000, 9000).ToString();

            yield return new TestCaseData("Invalid User ID or Password", "", password, companyName).SetName("1: VerifyErrorOnBlankUserIdLogin");
            yield return new TestCaseData("Invalid User ID or Password", userId, "", companyName).SetName("2: VerifyErrorOnBlankPasswordIdLogin");
            yield return new TestCaseData("Invalid User ID or Password", wrongUserName, password, companyName).SetName("3: VerifyErrorOnWrongUserIdLogin");
            yield return new TestCaseData("Invalid User ID or Password", userId, wrongPassword, companyName).SetName("4: VerifyErrorOnWrongPasswordIdLogin");
            yield return new TestCaseData("Invalid company name", userId, password, "").SetName("5: VerifyErrorOnBlankCompanyLogin");
            yield return new TestCaseData("Invalid company name", userId, password, wrongCompanyName).SetName("6: VerifyErrorOnWrongCompanyLogin");

        }

        [TestCaseSource(nameof(LoginNegativeTestCases))]
        public void VerifyLoginNegativeScenarious(string expected, string userId, string password, string companyName)
        {
            driver.Navigate().GoToUrl(loginUrl);
            driver.FindElement(By.CssSelector("#username")).SendKeys(userId);
            driver.FindElement(By.CssSelector("#password")).SendKeys(password);
            driver.FindElement(By.CssSelector("#_companyText")).SendKeys(companyName);
            driver.FindElement(By.CssSelector(".login-submit-button")).Click();
            Assert.That(driver.FindElement(By.CssSelector(".validation-summary-errors")).Text, Is.EqualTo(expected));
        }
    }
}