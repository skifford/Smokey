namespace Smokey.Extensions
{
    public static class ObjectExtensions
    {
        public static T ToType<T>(this object source)
        {
            return (T) source;
        }
    }
}
