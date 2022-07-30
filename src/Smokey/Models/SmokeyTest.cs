namespace Smokey.Models
{
    public abstract class SmokeyTest
    {
        /// <summary>
        /// Instance of <see cref="Browser"/>
        /// </summary>
        protected static Browser Browser { get; set; }

        /// <summary>
        /// Disposing sources of managed/unmanaged code
        /// </summary>
        protected static void Dispose()
        {
            Browser?.Dispose();
        }
    }
}
