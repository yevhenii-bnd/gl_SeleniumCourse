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
using CorrigoTestProject.LayeredStructure.BusinessLogic;

namespace CorrigoTestProject.LayeredStructure;

public class GenerateReportTest
{
    private CorrigoEnterpriseApplication app;


    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        app = new CorrigoEnterpriseApplication();
    }


    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        app.CloseApp();
    }


    [Test]
    public void VerifyFirstReportGeneration()
    {
        var originalWindow = app.CurrentWindowHandle();

        app.LoginWithDefaultUser()
            .OpenReportListPage();
        var reportName = app.OpenGenerateReportDialogForFirstReportInList();
        app.GenerateReportWithHeaders()
            .VerifyReportName(reportName)
            .OpenTheOriginalTab(originalWindow);
        app.CloseGenerateReportDialog();

    }
}