using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace propertyguru
{
    public static class SiteSettings
    {
        public static string BaseURL
        {
            get
            {
                return getSettingValue("baseurl");
            }
        }
        public static string ReportPath
        {
            get
            {
                return getSettingValue("reportpath");
            }
        }
        public static string ReportFileName
        {
            get
            {
                return getSettingValue("reportfilename");
            }
        }
        public static int PageLoadTimeout
        {
            get
            {
                return Convert.ToInt32(getSettingValue("pageloadtimeout"));
            }
        }
        public static int ElementTimeout
        {
            get
            {
                return Convert.ToInt32(getSettingValue("elementtimeout"));
            }
        }

        private static string getSettingValue(string nodeName)
        {
            string filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../"));
            string fileName = "SiteSettings.xml";
            XElement doc = XElement.Load(filePath + fileName);
            string nodeValue = doc.Elements()
                            .Where(x => x.Name == nodeName)
                            .Select(y => y.Value)
                            .FirstOrDefault();

            return nodeValue;
        }

    }
}
