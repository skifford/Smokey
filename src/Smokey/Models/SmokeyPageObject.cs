using OpenQA.Selenium;
using Smokey.Features.ValuesCollecting;
using Smokey.Guarding;

namespace Smokey.Models
{
    public abstract class SmokeyPageObject<T> where T : class
    {
        public ValuesStorage Storage { get; }

        protected IWebDriver WebDriver { get; }

        protected SmokeyPageObject(IWebDriver webDriver)
        {
            Storage = ValuesStorage.GetInstance();
            WebDriver = webDriver;
        }

        public T CollectValue(string key, object value)
        {
            Guard.NotNull(key);
            Storage.Save(key, value);
            return this as T;
        }

        public T CollectValues()
        {
            ValuesCollector.CollectValues(from: this);
            return this as T;
        }

        public T ClearStorage()
        {
            Storage.Clear();
            return this as T;
        }
    }
}
