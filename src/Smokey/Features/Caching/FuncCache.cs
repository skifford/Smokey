using System;
using System.Collections.Generic;
using Smokey.Guarding;

namespace Smokey.Features.Caching;

internal class FuncCache<TKey, TValue>
{
    private readonly Dictionary<TKey, TValue> _cache = new();
    private readonly Func<TKey, TValue> _func;

    public FuncCache(Func<TKey, TValue> func)
    {
        _func = Guard.NotNull(func);
    }

    public TValue GetBy(TKey key)
    {
        Guard.NotNull(key);
        
        if (_cache.TryGetValue(key, out var cache))
        {
            return cache;
        }

        var value = _func.Invoke(key);

        _cache.TryAdd(key, value);

        return value;
    }
}
