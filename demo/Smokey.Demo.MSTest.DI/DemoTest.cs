using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Smokey.Demo.MSTest.DI
{
    [TestClass]
    public sealed class DemoTest : DemoTestBase
    {
        [TestCategory("Smoke")]
        [TestMethod]
        public async Task TestMethod()
        {
            await Task.Delay(LoadingTime);
            
            // Your code here
        }
    }
}