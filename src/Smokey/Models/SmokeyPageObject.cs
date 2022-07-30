using OpenQA.Selenium;
using Smokey.Features.ValuesCollecting;
using Smokey.Guarding;

namespace Smokey.Models
{
    /// <summary>
    /// Suggested base type for page object pattern 
    /// </summary>
    /// <typeparam name="T">type of first inheritor</typeparam>
    public abstract class SmokeyPageObject<T> where T : class
    {
        /// <summary>
        /// Global singleton storage for values needed to collecting
        /// </summary>
        public ValuesStorage Storage { get; }

        /// <summary>
        /// Reference to <see cref="IWebDriver"/> instance 
        /// </summary>
        protected IWebDriver WebDriver { get; }
        
        /// <param name="webDriver">reference to <see cref="IWebDriver"/> instance</param>
        protected SmokeyPageObject(IWebDriver webDriver)
        {
            Storage = ValuesStorage.GetInstance();
            WebDriver = webDriver;
        }

        /// <summary>
        /// Direct value collecting without breaking of chain invocation
        /// </summary>
        /// <param name="key">unique and not null string to identify value in storage</param>
        /// <param name="value">value needed to save by key</param>
        /// <returns>inheritor instance</returns>
        public T CollectValue(string key, object value)
        {
            Guard.NotNull(key);
            Storage.Save(key, value);
            return this as T;
        }

        /// <summary>
        /// Global values collecting in all inheritors in all properties its defined <see cref="CollectableAttribute"/>
        /// without breaking of chain invocation
        /// </summary>
        /// <returns>inheritor instance</returns>
        public T CollectValues()
        {
            ValuesCollector.CollectValues(from: this);
            return this as T;
        }

        /// <summary>
        /// Clears of global values storage
        /// </summary>
        /// <returns>inheritor instance</returns>
        public T ClearStorage()
        {
            Storage.Clear();
            return this as T;
        }
    }
}
