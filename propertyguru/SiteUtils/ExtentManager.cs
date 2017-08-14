using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;

namespace propertyguru
{
    public class ExtentManager
    {
        private static readonly ExtentReports _instance = new ExtentReports();

        static ExtentManager()
        {
            System.IO.Directory.CreateDirectory(SiteSettings.ReportPath);
            string fileName = SiteSettings.ReportFileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".html";
            Instance.AttachReporter(new ExtentHtmlReporter(SiteSettings.ReportPath + fileName));
        }

        private ExtentManager() { }

        public static ExtentReports Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
