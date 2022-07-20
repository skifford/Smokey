namespace Smokey.Demo.PageObjects
{
    public static class Locators
    {
        public static class Page
        {
            // Global
            public const string Body = "html > body";
            
            // Footer links
            public const string About  = "div.AghGtd a.pHiOh:nth-child(1)";
            public const string Ads  = "div.AghGtd a.pHiOh:nth-child(2)";
            public const string Services  = "div.AghGtd a.pHiOh:nth-child(3)";
            public const string HowSearchWorks  = "div.AghGtd a.pHiOh:nth-child(4)";
            public const string Privacy  = "div.iTjxkf a.pHiOh:nth-child(1)";
            public const string Terms  = "div.iTjxkf a.pHiOh:nth-child(2)";
            
            public static class Settings
            {
                public const string Self = "span > g-popup";
                public const string ToggleDarkTheme = "g-menu-item > div > div";
            }
        }
    }
}
