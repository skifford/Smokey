using Smokey.Features.PropertyDecoration;

namespace Smokey.Features.ValuesCollecting;

public interface ICollectableProperty<T> : IPropertyDecorator<T>, ICollectableProperty
{
}
