using System;
using System.Diagnostics.CodeAnalysis;
using Smokey.Features.PropertyDecoration;

namespace Smokey.Features.ValuesCollecting;

public class CollectableProperty<T> : PropertyDecorator<T>, ICollectableProperty<T>
{
    public string Key { get; }

    public CollectableProperty(
        [NotNull] string key,
        CollectableType collectableType,
        Func<T> valueProvider = null) : base(
        valueProvider: valueProvider,
        getterDecorator: GetGetterDecorator(key, collectableType),
        setterDecorator: GetSetterDecorator(key, collectableType))
    {
        Key = key;
    }

    private static Action<T> GetGetterDecorator(string key, CollectableType type)
    {
        return (CollectableType.Getter & type) is CollectableType.Getter
            ? ValueCollectingAction(key)
            : null;
    }
    
    private static Action<T> GetSetterDecorator(string key, CollectableType type)
    {
        return (CollectableType.Setter & type) is CollectableType.Setter
            ? ValueCollectingAction(key)
            : null;
    }
    
    private static Action<T> ValueCollectingAction(string key)
    {
        return value => ValuesStorage.GetInstance().Save(key, value);
    }
}
