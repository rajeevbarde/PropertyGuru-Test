using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using propertyguru.Pages;
using System;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace propertyguru
{
    [Binding]
    public partial class scenario_all
    {
        public static IWebDriver driver { get; set; }
        public static WebDriverWait wait { get; set; }
        public static ExtentReports extentReport { get; set; }
        static ExtentTest logger { get; set; }

        [BeforeFeature]
        public static void beforeFeature()
        {
            extentReport  = ExtentManager.Instance;
            logger = extentReport.CreateTest("[Pre-requisite] Selenium setup");

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(SiteSettings.PageLoadTimeout);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(SiteSettings.ElementTimeout));

            Assert.That(driver, Is.InstanceOf(typeof(ChromeDriver)));
            logger.Pass("Chrome Drivers & component initialized");
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            if (driver != null)
                driver.Quit();

            extentReport.Flush();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            if (ScenarioContext.Current.ScenarioInfo.Tags.GetValue(0).ToString() == "scenario1")
                logger = extentReport.CreateTest("1. Search By Property");

            if (ScenarioContext.Current.ScenarioInfo.Tags.GetValue(0).ToString() == "scenario2")
                logger = extentReport.CreateTest("2. Verify the image displayed on listing details page");

            homepage.logger = logger;
            propertypage.logger = logger;
            searchpage.logger = logger;
        }

        [Given(@"When I am at home page")]
        public void GivenWhenIAmAtHomePage()
        {
            homepage.GotoHomePage(SiteSettings.BaseURL);
        }

        [When(@"click on search")]
        public void WhenClickOnSearch()
        {
            homepage.ClickSearch();
        }
    }
}
