namespace Smokey.Features.PropertyDecoration
{
    public interface IPropertyDecorator<T> : IPropertyDecorator
    {
        T Value { get; set; }
    }
}
