using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SeleniumExtras.PageObjects;
using CorrigoTestProject.LayeredStructure.BusinessLogic;
using OpenQA.Selenium.Support.UI;


namespace CorrigoTestProject.LayeredStructure.PageObjects;

public class ReportListPage : BasePage
{
    [FindsBy(How = How.XPath, Using = "//div[@class='blockUI blockOverlay']")]
    private IWebElement blockOverlay;

    [FindsBy(How = How.CssSelector, Using = "table tr:nth-child(1) td.gc-ReportsListGrid-DisplayAs a")]
    private readonly IWebElement firstReportName;

    [FindsBy(How = How.CssSelector, Using = ".showHeaders .k-switch-container")]
    private IWebElement showHeadersSwitchContainer;

    [FindsBy(How = How.CssSelector, Using = ".id-generate-button")]
    private IWebElement generateButton;

    [FindsBy(How = How.CssSelector, Using = ".id-btn-close")]
    private IWebElement closeButton;


    public ReportListPage(ApplicationContext context) : base(context)
    {
        PageFactory.InitElements(driver, this);
    }

    public void Open()
    {
        driver.Navigate().GoToUrl($"{context.baseUrl}/corpnet/report/reportlist.aspx");
        wait.Until(driver => !blockOverlay.Displayed);
    }

    public string OpenGenerateReportDialogForFirstReportInList()
    {
        var reportName = wait.Until(ExpectedConditions.ElementToBeClickable(firstReportName)).Text;
        firstReportName.Click();
        return reportName;
    }

    public void GenerateReportWithHeaders()
    {
        wait.Until(ExpectedConditions.ElementToBeClickable(showHeadersSwitchContainer));
        //wait.Until(ExpectedConditions.ElementToBeClickable((showHeadersSwitchContainer)).Click();
        generateButton.Click();
    }

    public void CloseGenerateReportDialog()
    {
        closeButton.Click();
    }
}
