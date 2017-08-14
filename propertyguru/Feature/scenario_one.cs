using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using propertyguru.Pages;
using System;
using TechTalk.SpecFlow;

namespace propertyguru
{
    public partial class scenario_all
    {
        [Given(@"selected (.*) by searching (.*)")]
        public void GivenSelectedMarinaBayResidenceBySearchingMarina(string propFullName,string propShortName)
        {
            homepage.PropertySearch(propShortName, propFullName);
        }
        
        [Given(@"floor area  to have maximum (.*) sqft")]
        public void GivenFloorAreaToHaveMaximumSqft(string maxRoomArea)
        {
            homepage.FloorAreaFilter(maxRoomArea);
        }
        
        [Then(@"all search result must have property name - (.*)  with area less than (.*) sqft")]
        public void ThenAllSearchResultMustHavePropertyName_MarinaBayResidenceWithAreaLessThanSqft(string propName, string maxFloor)
        {
            searchpage.verifyPropNameAndFloor(propName,maxFloor);
        }
    }
}
