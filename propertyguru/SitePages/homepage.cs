using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;

using NUnit.Framework;

namespace propertyguru.Pages
{
    public static class homepage
    {
        static WebDriverWait wait;
        static IWebDriver driver;
        public static ExtentTest logger;

        static homepage()
        {
            driver = scenario_all.driver;
            wait = scenario_all.wait;
        }

        public static void GotoHomePage(string baseURL)
        {
            try
            {
                driver.Navigate().GoToUrl(baseURL);                

                Assert.That(baseURL, Is.EqualTo(driver.Url).IgnoreCase);
                logger.Info("Site loaded :" + baseURL);
            }
            catch (Exception e)
            {
                logger.Log(Status.Fail, e.ToString());
            }
        }

        public static void PropertySearch(string propName,string propFullName)
        {
            try
            {
                //Enter - Marina
                string selector = "freetext";
                wait.Until(ExpectedConditions.ElementIsVisible(By.Name(selector))).SendKeys(propName);
                var searchbox = driver.FindElement(By.Name(selector));
                logger.Log(Status.Info, propName + " typed on search box successfully");

                //Select - Marina Bay Residence
                bool isSelected = false;
                string selector1 = "div.tt-dataset.tt-dataset-autocomplete > div";
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(selector1)));
                var result = driver.FindElements(By.CssSelector(selector1));

                foreach (var item in result)
                {
                    string hotelname = item.FindElement(By.CssSelector("p.tt-sug-text")).Text;                                        
                    if (hotelname == propFullName)
                    {
                        item.Click();
                        isSelected = true;                        
                        break;
                    }
                }
                Assert.That(isSelected, Is.True);
                logger.Log(Status.Info, propFullName + " selected sucessfully");
            }
            catch (Exception e)
            {
                logger.Log(Status.Fail, e.ToString());
            }
        }

        public static void FloorAreaFilter(string maxArea)
        {
                //Click More Option
                ClickMoreOption();

                //click floor            
                string selector3 = "div.expanded-col.expanded-0 > fieldset:nth-child(2)";
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(selector3))).Click();
                logger.Info("Clicked on Floor Area dropdown Filter");

                //enter 3000
                string selector4 = "div.expanded-col.expanded-0 > fieldset:nth-child(2) > div.js-form-group.btn-group.param-.tide-to.tide-to-market.searchbox-hidden-xs.btn-group-range.btn-group-expand-left.open > ul > li.dropdown-input-range.range-max.form-group-numeric > input";
                driver.FindElement(By.CssSelector(selector4)).SendKeys(maxArea);
                logger.Info(maxArea + " room size is added as filter.");
        }

        private static void ClickMoreOption()
        {
            //click moreoption
            string selector2 = "search-options-moreoptions";
            var moreOption = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(selector2)));
            moreOption.Click();

            logger.Log(Status.Info, "More option dropdown clicked");
        }

        public static void ClickSearch()
        {
            string selector5 = "btn-submit";
            driver.FindElement(By.ClassName(selector5)).Click();
            logger.Log(Status.Info, "clicked on search button");
        }

        public static void ChangePropertyListing(string _listing)
        {
            try
            {
                // click listing type
                string selector1 = @"#searchbox-n1 > fieldset > div.sticky-container > div > div > div > div.js-form-group.btn-group.param-listing_type.btn-group-expand-right.js-has-value > button";
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(selector1))).Click();
                logger.Log(Status.Info, "Listing button clicked");

                //Select rent      
                if (_listing == "Rent")
                {
                    string selector2 = "#searchbox-n1 > fieldset > div.sticky-container > div > div > div > div.js-form-group.btn-group.param-listing_type.btn-group-expand-right.js-has-value.open > ul > li:nth-child(2)";
                    var rent = driver.FindElement(By.CssSelector(selector2));
                    wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(selector2))).Click();
                    logger.Log(Status.Info, "Rent is selected");
                }
            }
            catch (Exception e)
            {
                logger.Log(Status.Fail, e.ToString());
            }
        }

        public static void PhotosFilter()
        {
            ClickMoreOption();

            string selector6 = "with_photos";
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name(selector6))).SendKeys(Keys.Space);
            logger.Log(Status.Info, "Photos filter is checked");
         }

        public static void PriceFilter(string maxPrice)
        {
                string selector3 = @"btn-price-range";
                wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(selector3))).Click();
                logger.Log(Status.Info, "price dropdown clicked");

                //Enter the max price
                string selector4 = "maxprice";
                wait.Until(ExpectedConditions.ElementIsVisible(By.Name(selector4))).SendKeys(maxPrice);             
                driver.FindElement(By.Name(selector4)).SendKeys(Keys.Enter);
                logger.Log(Status.Info, maxPrice + " max price is entered sucessfully");
        }
    } //class
}
