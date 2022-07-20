using System.Linq;
using Smokey.Extensions.WebDriver;
using Smokey.Features.ValuesCollecting;

namespace Smokey.Demo.PageObjects
{
    public partial class Page
    {
        [Collectable(Key = CollectableKeys.Page.BackgroundColor)] 
        public string BackgroundColor => GetColor();
        
        private string GetColor()
        {
            return WebDriver
                .CssValue(Locators.Page.Body, "background")
                .Split(" none")
                .First();
        }
    }
}
