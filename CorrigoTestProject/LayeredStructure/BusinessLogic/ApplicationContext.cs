using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CorrigoTestProject.LayeredStructure.BusinessLogic;

public record ApplicationContext
{
    public IWebDriver driver { get; set; }

    public string baseUrl { get; set; }

    public string username { get; set; }

    public string password { get; set; }

    public string company { get; set; }
}
