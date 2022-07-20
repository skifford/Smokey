using Smokey.Extensions.WebDriver;
using Smokey.Features.ValuesCollecting;

namespace Smokey.Demo.PageObjects
{
    public partial class Page
    {
        [Collectable(Key = CollectableKeys.Page.Localization)]
        public string About => WebDriver.Text(Locators.Page.About);
        
        [Collectable(Key = CollectableKeys.Page.Localization)] 
        public string Ads => WebDriver.Text(Locators.Page.Ads);
        
        [Collectable(Key = CollectableKeys.Page.Localization)] 
        public string Services => WebDriver.Text(Locators.Page.Services);
        
        [Collectable(Key = CollectableKeys.Page.Localization)] 
        public string HowSearchWorks => WebDriver.Text(Locators.Page.HowSearchWorks);
        
        [Collectable(Key = CollectableKeys.Page.Localization)] 
        public string Privacy => WebDriver.Text(Locators.Page.Privacy);
        
        [Collectable(Key = CollectableKeys.Page.Localization)] 
        public string Terms => WebDriver.Text(Locators.Page.Terms);
        
        [Collectable(Key = CollectableKeys.Page.Localization)] 
        public string Settings => WebDriver.Text(Locators.Page.Settings.Self);
    }
}
