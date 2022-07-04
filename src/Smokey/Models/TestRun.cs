namespace Smokey.Models
{
    public sealed class TestRun
    {
        public string Domain { get; init; }
        public Authentication Authentication { get; set; }
        public BrowserConfiguration BrowserConfiguration { get; init; }
    }
}
