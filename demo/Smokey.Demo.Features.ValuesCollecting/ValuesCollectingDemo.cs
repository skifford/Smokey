using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smokey.Extensions;
using Smokey.Features.ValuesCollecting;

namespace Smokey.Demo.Features.ValuesCollecting
{
    [TestClass]
    public sealed class ValuesCollectingDemo
    {
        private readonly Page _page = new();

        [TestInitialize]
        public void TestInitialize()
        {
            // Clean up the singleton storage to does not affecting the results of launches from test to test
            _page.ClearStorage();
        }

        /// <summary>
        /// Demonstrates direct collecting value as key-value pair without Smokey features: one value by one invoke.
        /// </summary>
        [TestMethod]
        public void DirectCollecting_ExternalStorage_ValuesCollected()
        {
            // Define expected values
            const int defaultValue = 0;
            const int expectedValue = 10;

            // Define keys
            var defaultValueKey = Guid.NewGuid().ToString();
            var expectedValueKey = Guid.NewGuid().ToString();

            // Define storage
            var storage = new Dictionary<string, object>();

            // Build chain of invocation without breaks with calling method for direct collecting values
            _page
                .Run(() => storage.Add(key: defaultValueKey, value: _page.Number))
                .SetNumber(expectedValue)
                .Run(() => storage.Add(key: expectedValueKey, value: _page.Number));

            // Finds entry by key and asserts with expected value
            Assert.AreEqual(defaultValue, storage.GetValueOrDefault(defaultValueKey));
            Assert.AreEqual(expectedValue, storage.GetValueOrDefault(expectedValueKey));
        }

        /// <summary>
        /// Demonstrates direct collecting value as key-value pair with Smokey features: one value by one invoke.
        /// Same key can be used for sequentially collecting values one by one.
        /// </summary>
        [TestMethod]
        public void DirectCollecting_SmokeyStorage_ValuesCollected()
        {
            // Define expected values
            const int defaultValue = 0;
            const int value1 = 10;
            const int value2 = 100;

            // Define the keys
            var defaultValueKey = Guid.NewGuid().ToString();
            var expectedValueKey = Guid.NewGuid().ToString();

            // Build chain of invocation without breaks with calling method for direct collecting values
            _page
                .CollectValue(key: defaultValueKey, value: _page.Number)
                .SetNumber(value1)
                .CollectValue(key: expectedValueKey, value: _page.Number)
                .SetNumber(value2)
                .CollectValue(key: expectedValueKey, value: _page.Number);

            // Finds entry by key and asserts with expected value
            Assert.AreEqual(defaultValue, _page.Storage.FirstEntry(defaultValueKey));
            Assert.AreEqual(value1, _page.Storage.FirstEntry(expectedValueKey));
            Assert.AreEqual(value2, _page.Storage.LastEntry(expectedValueKey));
        }

        /// <summary>
        /// Demonstrates collecting multiple values for properties with [Collectable] attribute:
        /// many values by one invoke. Can be used for taking snapshot of global state for all page objects in memory.
        /// Example is given for single property with built-in C# simple type 'int'.
        /// </summary>
        [TestMethod]
        public void Attribute_SimpleType_MultipleValues_ValuesCollected()
        {
            // Define expected values
            const int value1 = 1;
            const int value2 = 2;
            const int value3 = 3;

            var expectedCollection = new List<int> { value1, value2, value3 };

            // Define the key to the property of interest
            var key = ValuesStorageKey.Create(typeof(Page), nameof(_page.Number));

            // Build chain of invocation without breaks with calling method for collecting values of properties
            // that is defined an [Collectable] attribute
            _page
                .SetNumber(value1)
                .CollectValues()
                .SetNumber(value2)
                .CollectValues()
                .SetNumber(value3)
                .CollectValues();

            // Finds first entry or returns null if incorrect key is given
            Assert.AreEqual(value1, _page.Storage.FirstEntry(key));
            Assert.IsNull(_page.Storage.By(Guid.NewGuid().ToString()));

            // Finds first entry with predicate or returns null if predicate condition is false
            Assert.AreEqual(value3, _page.Storage.FirstEntry(key, value => value.ToType<int>() >= 3));
            Assert.IsNull(_page.Storage.FirstEntry(key, value => value.ToType<int>() == 5));

            // Likewise all entries
            CollectionAssert.AreEqual(expectedCollection, _page.Storage.AllEntries(key).ToList());
            Assert.IsFalse(_page.Storage.AllEntries(key, value => value.ToType<int>() < 0).Any());
        }

        /// <summary>
        /// Demonstrates collecting value of custom type of property with predefined key:
        /// [Collectable(By = "{KeyValue}")]. As before, many values by one invoke.
        /// </summary>
        [TestMethod]
        public void Attribute_CustomType_AttributeWithKey_ValuesCollected()
        {
            // Define expected values
            var value = Guid.NewGuid().ToString();

            // Build chain of invocation without breaks with calling method for collecting values of properties
            // that is defined an [Collectable] attribute
            _page
                .SetElementName(value)
                .CollectValues();

            // Finds first entry, cast and extract property with simple type 'string'
            Assert.AreEqual(value, _page.Storage.LastEntry(Constants.Keys.Element).ToType<Element>().Name);
        }

        /// <summary>
        /// Demonstrates collecting values from special type of property - CollectableProperty. This type
        /// has predefined delegates to decorate moment of getting or setting value.
        /// Values will be collected automatically.
        /// </summary>
        [TestMethod]
        public void PropertyDecoration_WithoutValueProducing_ValuesCollected()
        {
            // Define expected values
            var title = Guid.NewGuid().ToString();
            var description = Guid.NewGuid().ToString();

            // Remember keys for expected values
            var titleKey = _page.Component.Title.Key;
            var descriptionKey = _page.Component.Description.Key;

            // Build chain of invocation without breaks, values will be collected automatically
            // in order to selected CollectableType: on getter, on setter or on both of them
            _page
                .SetComponentTitle(title)
                .SetComponentDescription(description);

            // Assert expected and actual values from storage 
            Assert.AreEqual(title, _page.Storage.LastEntry(titleKey));
            Assert.AreEqual(description, _page.Storage.LastEntry(descriptionKey));
        }

        /// <summary>
        /// Demonstrates collecting values from CollectableProperty with predefined values providing delegate.
        /// Values will be collected automatically.
        /// </summary>
        [TestMethod]
        public void PropertyDecoration_DefinedValueProducing_ValuesCollected()
        {
            // Define expected value
            const int defaultCount = -1;

            // Define key for expected value
            var key = Key.Create(nameof(_page.Count));

            // Define value providing for collectable property as delegate: lambda expression, function, etc.
            // When control thread will invoke getter function of CollectableProperty,
            // first of all value providing delegate will be invoked, and after it - getter decorator delegate 
            int ValueProvider() => new Random().Next(0, 100);

            // It is possible to configure the moment of collection of values: when the getter is invoked,
            // when the setter is invoked, or both. Use a boolean operation to define both a getter and a setter at the
            // same time, because CollectableType is an enum with a bitmap option.
            _page.Count = new CollectableProperty<int>(
                key: key,
                collectableType: CollectableType.Getter | CollectableType.Setter,
                valueProvider: ValueProvider);

            // Remark: this is example, but better makes properties readonly and initialize them in constructor

            // Build chain of invocation without breaks, values will be collected automatically
            // in order to selected CollectableType: on getter, on setter or on both of them
            _page
                .SetCount(defaultCount)
                .UpdateCount();

            // Assert expected and actual values from storage 
            Assert.AreEqual(defaultCount, _page.Storage.FirstEntry(key));
            Assert.IsTrue(_page.Storage.LastEntry(key).ToType<int>() >= 0);
        }
    }
}
