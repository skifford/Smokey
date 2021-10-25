using System.Collections.Generic;

namespace Smokey
{
    public sealed class BrowserConfiguration
    {
        public BrowserType BrowserType { get; init; } = BrowserType.Undefined;
        public string DriverSource { get; init; }
        public IReadOnlyCollection<string> Arguments { get; init; }
        public string RemoteHost { get; init; }
    }
}
