using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smokey.Demo.Localizations;
using Smokey.Demo.PageObjects;
using Smokey.Features.Caching;

namespace Smokey.Demo
{
    [TestClass]
    public sealed class Demo : DemoTestBase
    {
        private readonly Page _page = Pool.Get<Page>();

        [TestInitialize]
        public void TestSetup()
        {
            _page.ClearStorage();
        }

        [TestMethod]
        public void ToggleDarkTheme()
        {
            // Arrange
            const string lightTehemeColor = "rgb(255, 255, 255)";
            const string darkTehemeColor = "rgb(32, 33, 36)";

            var expected = new[]
            {
                lightTehemeColor,
                darkTehemeColor,
                lightTehemeColor
            };

            // Act
            _page
                .CollectValues()
                .OpenSettings()
                .ToggleDarkTheme()
                .CollectValues()
                .OpenSettings()
                .ToggleDarkTheme()
                .CollectValues();

            var actual = _page.Storage.By(CollectableKeys.Page.BackgroundColor).ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Localization_Ru()
        {
            // Arrange
            var expected = new List<string>()
            {
                Localization.For(Language.Ru).About,
                Localization.For(Language.Ru).Ads,
                Localization.For(Language.Ru).Services,
                Localization.For(Language.Ru).HowSearchWorks,
                Localization.For(Language.Ru).Privacy,
                Localization.For(Language.Ru).Terms,
                Localization.For(Language.Ru).Settings
            };
            
            expected = expected.OrderBy(value => value).ToList();
            
            // Act
            _page.CollectValues();

            var actual = _page.Storage.By(CollectableKeys.Page.Localization).OrderBy(value => value).ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
