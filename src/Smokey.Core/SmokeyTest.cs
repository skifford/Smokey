namespace Smokey
{
    public abstract class SmokeyTest
    {
        protected static Browser Browser { get; set; }

        protected static void Dispose()
        {
            Browser?.Dispose();
        }
    }
}