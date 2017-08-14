using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using System.Net;
using NUnit.Framework;

namespace propertyguru.Pages
{
    public static class propertypage
    {
        static WebDriverWait wait;
        static IWebDriver driver;
        public static ExtentTest logger;

        static propertypage()
        {
            driver = scenario_all.driver;
            wait = scenario_all.wait;
        }

        public static void verifyImagePresent()
        {
            try
            {
                logger.Log(Status.Info, "Current URL : " + driver.Url);
                
                //check big images
                var bigImgSelector = "#carousel-photos > div.carousel-inner > div";
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(bigImgSelector)));
                var box1 = driver.FindElements(By.CssSelector(bigImgSelector));

                foreach (var item in box1)
                {
                    string imglink = item.FindElement(By.TagName("img")).GetAttribute("data-original");
                    checkImageExist(imglink);
                }

                //check small images
                var smallImgSelector = "#carousel-photos > ol > li";
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(smallImgSelector)));
                var box2 = driver.FindElements(By.CssSelector(smallImgSelector));

                foreach (var item in box2)
                {
                    string imglink = item.FindElement(By.TagName("img")).GetAttribute("data-original");
                    checkImageExist(imglink);
                }
            }
            catch (Exception e)
            {
                logger.Log(Status.Fail, e.ToString());
            }
        }

        private static void checkImageExist(string _imglink)
        {
            Warn.If(GetStatusCode(_imglink) == HttpStatusCode.NotFound, "Image is missing : " + _imglink);

            if (GetStatusCode(_imglink) == HttpStatusCode.NotFound)
                logger.Log(Status.Fail, "Image is missing : " + _imglink);
            else
                logger.Log(Status.Pass, "Image exist : " + _imglink);
        }

        private static HttpStatusCode GetStatusCode(string url)
        {
            var result = default(HttpStatusCode);
            var request = WebRequest.Create(url);
            request.Method = "HEAD";
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    result = response.StatusCode;
                    response.Close();
                }
            }
            catch (WebException)
            {
                return HttpStatusCode.NotFound;
            }
            return result;
        }
    }
}
