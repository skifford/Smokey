using System;
using System.Collections.Generic;
using System.Linq;
using Smokey.Guarding;

namespace Smokey.Features.ValuesCollecting
{
    public sealed class ValuesStorage
    {
        private static readonly ValuesStorage Instance = new();
        
        private readonly Dictionary<string,List<object>> _storage = new();

        private ValuesStorage()
        {
        }

        /// <summary>
        /// Provide singleton instance of <see cref="ValuesStorage"/>.
        /// </summary>
        public static ValuesStorage GetInstance() => Instance;

        /// <summary>
        /// Clears all data in storage. 
        /// </summary>
        public void Clear() => _storage.Clear();
        
        /// <summary>
        /// Finds bucket with given key and adds value into it, otherwise, create new bucket.
        /// </summary>
        /// <param name="key">Key to locate bucket for given value in storage</param>
        /// <param name="value">Value required to save</param>
        public void Save(string key, object value)
        {
            Guard.NotEmpty(key);
            
            if (_storage.ContainsKey(key))
            {
                _storage[key].Add(value);
            }
            else
            {
                _storage[key] = new List<object> { value };
            }
        }

        /// <summary>
        /// Finds bucket by given key.
        /// </summary>
        /// <param name="key">Key to locate bucket</param>
        /// <returns>Content of bucket or null if bucket does not exists</returns>
        public IEnumerable<object> By(string key)
        {
            return _storage.ContainsKey(key) ? _storage[key] : null;
        }
        
        /// <summary>
        /// Finds first entry by given key and predicate
        /// </summary>
        /// <param name="key">Key to locate bucket</param>
        /// <param name="predicate">Condition to filter values</param>
        /// <returns>Value or null if bucket does not exists or condition is false</returns>
        public object FirstEntry(string key, Func<object, bool> predicate = null)
        {
            return By(key)?.FirstOrDefault(predicate ?? (_ => true));
        }
        
        /// <summary>
        /// Finds last entry by given key and predicate
        /// </summary>
        /// <param name="key">Key to locate bucket</param>
        /// <param name="predicate">Condition to filter values</param>
        /// <returns>Value or null if bucket does not exists or condition is false</returns>
        public object LastEntry(string key, Func<object, bool> predicate = null)
        {
            return By(key)?.LastOrDefault(predicate ?? (_ => true));
        }
        
        /// <summary>
        /// Finds all entries by given key and predicate
        /// </summary>
        /// <param name="key">Key to locate bucket</param>
        /// <param name="predicate">Condition to filter values</param>
        /// <returns>Content of bucket or null if bucket does not exists</returns>
        public IEnumerable<object> AllEntries(string key, Func<object, bool> predicate = null)
        {
            return By(key)?.Where(predicate ?? (_ => true));
        }
    }
}
