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
        [Given(@"select listing type as (.*)")]
        public void GivenSelectListingTypeAsRent(string _listingType)
        {
            homepage.ChangePropertyListing(_listingType);
        }
        
        [Given(@"having maximum price of \$(.*)")]
        public void GivenHavingMaximumPriceOf(string maxPrice)
        {
            homepage.PriceFilter(maxPrice);
        }
        
        [Given(@"properties should have photos displayed")]
        public void GivenPropertiesShouldHavePhotosDisplayed()
        {
            homepage.PhotosFilter();
        }
  
        [Then(@"select the first result")]
        public void ThenSelectTheFirstResult()
        {
            searchpage.ClickFirstSearchResult();
        }
        
        [Then(@"check if all images are displayed")]
        public void ThenCheckIfAllImagesAreDisplayed()
        {
            propertypage.verifyImagePresent();
        }
    }
}
