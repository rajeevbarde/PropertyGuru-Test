using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;

using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;

using NUnit.Framework;

namespace propertyguru.Pages
{
    public static class searchpage
    {
        static WebDriverWait wait;
        static IWebDriver driver;
        public static ExtentTest logger;

        static searchpage()
        {
            driver = scenario_all.driver;
            wait = scenario_all.wait;
        }

        public static void verifyPropNameAndFloor(string propFullName,string floorArea)
        {
            try
            {
                bool validateNextPage = false;
                int expectedfloor = Convert.ToInt32(floorArea);
                do
                {
                    //Find all search result
                    string selector6 = @"div.listing-list.listings-page-results.enabled-boost > ul > li.listing-item";
                    wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(selector6)));
                    var box = driver.FindElements(By.CssSelector(selector6));
                    
                    foreach (var item in box)
                    {
                        verifyPropertyName(item, propFullName); //Verify Property Name
                        verifyFloorArea(item, expectedfloor); //Verify Floor area
                    }

                    bool hasNextPage = checkPagination(By.CssSelector("div.listing-pagination > ul > li.pagination-next"));

                    if (hasNextPage)
                    {
                        gotoNextPage();
                        validateNextPage = true;
                    }
                    else
                        validateNextPage = false;

                } while (validateNextPage);
            }
            catch (Exception e)
            {
                logger.Log(Status.Fail, e.ToString());
            }
        }

        private static void verifyPropertyName(IWebElement item,string expectedPropertyName)
        {
            var propertyName = item.FindElement(By.CssSelector("div:nth-child(1) > div.listing-info > h3 > a > span")).Text;
            var propertyURL = item.FindElement(By.CssSelector("div.listing-info > h3 > a")).GetAttribute("href");

            Warn.If(propertyName == propertyURL, expectedPropertyName + " is NOT found on listing : " + propertyURL);

            if (propertyName == expectedPropertyName)
                logger.Pass(expectedPropertyName + " is found on listing : " + propertyURL);
            else
                logger.Fail(expectedPropertyName + " is NOT found on listing : " + propertyURL);
        }

        private static void verifyFloorArea(IWebElement item, int expectedFloorArea)
        {
            var roomsizeMix = item.FindElement(By.CssSelector("div:nth-child(1) > div.listing-info > ul:nth-child(4) > li.lst-sizes")).Text;
            int roomsize = Convert.ToInt32(roomsizeMix.Substring(0, roomsizeMix.IndexOf("sqft")).Trim());
            var propertyURL = item.FindElement(By.CssSelector("div.listing-info > h3 > a")).GetAttribute("href");

            Warn.If(roomsize < expectedFloorArea, roomsize + " room area is MORE than expected on listing : " + propertyURL);

            if (roomsize < expectedFloorArea)
                logger.Pass(roomsize + " room area is less than expected on listing : " + propertyURL);
            else
                logger.Fail(roomsize + " room area is more than expected on Listing : " + propertyURL);
        }

        private static void gotoNextPage()
        {
            string selector = "»";
            wait.Until(ExpectedConditions.ElementExists(By.LinkText(selector)));
            RemoteWebElement rweLink = (RemoteWebElement)driver.FindElement(By.LinkText(selector));
            var scroller = rweLink.LocationOnScreenOnceScrolledIntoView;
            rweLink.Click();
        }

        private static bool checkPagination(By by)
        {
            try
            {
                bool isDisabled = driver.FindElement(by).GetAttribute("Class").Contains("disabled");

                if (isDisabled)
                    return false;
                else
                    return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static void ClickFirstSearchResult()
        {
            // Click on First search listing
            string selector6 = "listing-info";
            driver.FindElement(By.ClassName(selector6)).Click();
            logger.Log(Status.Info,"property is clicked");
        }
    }
}
