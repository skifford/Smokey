using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smokey.Extensions;
using Smokey.Features.ValuesCollecting;

namespace Smokey.Demo.Features.ValuesCollecting
{
    [TestClass]
    public sealed class ValuesStorageDemo
    {
        [TestMethod]
        public void By()
        {
            // Get instance of storage
            var storage = ValuesStorage.GetInstance();

            // Define variables
            var key = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();

            // Act
            storage.Save(key: key, value: string.Empty);
            storage.Save(key: key, value: value);

            // Given invalid key
            Assert.IsNull(storage.By(value));

            // Given valid key
            Assert.IsNotNull(storage.By(key));

            // Assert expectation
            Assert.IsTrue(storage.By(key).ToList().Count == 2);
            Assert.AreEqual(string.Empty, storage.By(key).First());
            Assert.AreEqual(value, storage.By(key).Last());
        }

        [TestMethod]
        public void FirstEntry()
        {
            // Get instance of storage
            var storage = ValuesStorage.GetInstance();

            // Define variables
            var key = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();

            // Act
            storage.Save(key: key, value: value);
            storage.Save(key: key, value: string.Empty);

            // Given invalid key
            Assert.IsNull(storage.FirstEntry(value));

            // Given valid key
            Assert.IsNotNull(storage.FirstEntry(key));

            // Assert expectation without predicate
            Assert.AreEqual(value, storage.FirstEntry(key));

            // Assert expectation with predicate
            Assert.AreEqual(string.Empty, storage.FirstEntry(key, v => string.IsNullOrEmpty(v.ToType<string>())));

            // Assert null when predicate is false
            Assert.IsNull(storage.FirstEntry(key, obj => (string) obj == nameof(obj)));
        }

        [TestMethod]
        public void LastEntry()
        {
            // Get instance of storage
            var storage = ValuesStorage.GetInstance();

            // Define variables
            var key = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();

            // Act
            storage.Save(key: key, value: string.Empty);
            storage.Save(key: key, value: value);

            // Given invalid key
            Assert.IsNull(storage.LastEntry(value));

            // Given valid key
            Assert.IsNotNull(storage.LastEntry(key));

            // Assert expectation without predicate
            Assert.AreEqual(value, storage.LastEntry(key));

            // Assert expectation with predicate
            Assert.AreEqual(string.Empty, storage.LastEntry(key, v => string.IsNullOrEmpty(v.ToType<string>())));

            // Assert null when predicate is false
            Assert.IsNull(storage.LastEntry(key, obj => (string) obj == nameof(obj)));
        }

        [TestMethod]
        public void AllEntries()
        {
            // Get instance of storage
            var storage = ValuesStorage.GetInstance();

            // Define variables
            var key = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();

            // Act
            storage.Save(key: key, value: string.Empty);
            storage.Save(key: key, value: value);

            // Given invalid key
            Assert.IsNull(storage.AllEntries(value));

            // Given valid key
            Assert.IsNotNull(storage.AllEntries(key));

            // Assert expectation without predicate
            Assert.IsTrue(storage.AllEntries(key).Count() == 2);
            Assert.AreEqual(string.Empty, storage.AllEntries(key).First());
            Assert.AreEqual(value, storage.AllEntries(key).Last());

            // Assert expectation with predicate
            Assert.IsTrue(storage.AllEntries(key, obj => string.IsNullOrEmpty((string) obj)).Count() == 1);

            // Assert empty collection when predicate is false
            Assert.IsFalse(storage.AllEntries(key, obj  => (string) obj == nameof(obj)).Any());
        }
    }
}