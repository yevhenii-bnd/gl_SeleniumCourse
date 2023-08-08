using CorrigoTestProject.LayeredStructure.BusinessLogic;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrigoTestProject.LayeredStructure.PageObjects;

public class ReportRenderPage : BasePage
{
    public ReportRenderPage(ApplicationContext context) : base(context)
    {
        PageFactory.InitElements(driver, this);
    }

    public void VerifyReportName(string reportName)
    {
        wait.Until(driver => driver.WindowHandles.Count == 2);
        driver.SwitchTo().Window(driver.WindowHandles.Last());
        //wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//tr[1]//tr[2]")));
        wait.Until(driver => driver.FindElement(By.XPath($"//span[contains(text(),'{reportName}')]")));

        Assert.IsTrue(driver.FindElements(By.XPath($"//span[contains(text(),'{reportName}')]")).Count > 0, "Header not found");
    }
}
