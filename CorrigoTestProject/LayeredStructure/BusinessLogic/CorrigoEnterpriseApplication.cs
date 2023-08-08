using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using CorrigoTestProject.LayeredStructure.PageObjects;
using System.Threading;

namespace CorrigoTestProject.LayeredStructure.BusinessLogic;

public class CorrigoEnterpriseApplication
{

    ApplicationContext context;

    private readonly LoginPage loginPage;
    private readonly ReportListPage reportListPage;
    private readonly ReportRenderPage reportRenderPage;

    public CorrigoEnterpriseApplication()
    {
        context = new ApplicationContext();

        var options = new ChromeOptions();
        options.AddArguments("start-maximized");
        var driver = new ChromeDriver(options);
        

        context.driver = driver;
        context.baseUrl = Environment.GetEnvironmentVariable("ENT_QA_BASE_URL");
        context.username = Environment.GetEnvironmentVariable("ENT_QA_USER");
        context.password = Environment.GetEnvironmentVariable("ENT_QA_PASS");
        context.company = Environment.GetEnvironmentVariable("ENT_QA_COMPANY");

        loginPage = new LoginPage(context);
        reportListPage = new ReportListPage(context);
        reportRenderPage = new ReportRenderPage(context);
    }

    public void CloseApp()
    {
        context.driver.Quit();
    }

    public CorrigoEnterpriseApplication LoginWithDefaultUser()
    {
        loginPage.LoginWithDefaultUser();
        return this;
    }

    public string CurrentWindowHandle()
    {
        return context.driver.CurrentWindowHandle;
    }

    public CorrigoEnterpriseApplication OpenReportListPage()
    {
        reportListPage.Open();
        return this;
    }

    public string OpenGenerateReportDialogForFirstReportInList()
    {
        return reportListPage.OpenGenerateReportDialogForFirstReportInList();
    }

    public CorrigoEnterpriseApplication GenerateReportWithHeaders()
    {
        reportListPage.GenerateReportWithHeaders();
        return this;
    }

    public CorrigoEnterpriseApplication VerifyReportName(string reportName)
    {
        reportRenderPage.VerifyReportName(reportName);
        return this;
    }

    public IWebDriver OpenTheOriginalTab(string originalWindow)
    {
        return context.driver.SwitchTo().Window(originalWindow);
    }

    public CorrigoEnterpriseApplication CloseGenerateReportDialog()
    {
        reportListPage.CloseGenerateReportDialog();
        return this;
    }
}
