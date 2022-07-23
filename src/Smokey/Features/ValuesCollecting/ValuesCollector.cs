using System;
using System.Linq;
using System.Reflection;
using Smokey.Extensions;
using Smokey.Features.Caching;

namespace Smokey.Features.ValuesCollecting;

public static class ValuesCollector
{
    private static readonly FuncCache<object, Type> TypesCache = new(obj => obj.GetType());
    private static readonly FuncCache<Type, PropertyInfo[]> PropertiesCache = new(type => type.GetProperties());

    public static void CollectValues(object from)
    {
        HandleRecursive(from);
    }

    private static void HandleRecursive(object invoker)
    {
        if (invoker is null)
        {
            return;
        }

        var type = TypesCache.GetBy(invoker);
        
        if (type.BaseType == typeof(ValueType) || type == typeof(string))
        {
            return;
        }

        var properties = PropertiesCache.GetBy(type);

        properties
            .Where(property => property.IsCollectable())
            .ForEach(property => SavePropertyValue(invoker, property));

        properties
            .Where(property => property.NotCollectable())
            .ForEach(property => InvokePropertyInstance(invoker, property));
    }

    private static void SavePropertyValue(object invoker, PropertyInfo property)
    {
        var key = property.GetCustomAttributes().OfType<CollectableAttribute>().FirstOrDefault()?.Key;
        var value = property.GetValue(invoker);

        ValuesStorage.GetInstance().Save(
            key: key ?? $"{property.DeclaringType}.{property.Name}",
            value: value);
    }

    private static void InvokePropertyInstance(object invoker, MemberInfo property)
    {
        var type = TypesCache.GetBy(invoker);

        var instance = type.InvokeMember(
            name: property.Name,
            invokeAttr: BindingFlags.GetProperty,
            binder: null,
            target: invoker,
            args: null);

        HandleRecursive(instance);
    }
}
