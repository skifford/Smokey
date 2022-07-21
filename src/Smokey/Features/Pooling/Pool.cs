using System;
using System.Collections.Generic;
using Smokey.Guarding;

namespace Smokey.Features.Pooling;

public static class Pool
{
    private const int DefaultCapacity = 1;
    
    private static readonly Dictionary<Type, Stack<object>> Storage = new();
    
    public static void Add<T>(int capacity = DefaultCapacity) where T : class, new()
    {
        var key = Register<T>(capacity);
        
        for (var i = 0; i < capacity; i++)
        {
            var instance = new T();
            Storage[key].Push(instance);
        }
    }
    
    public static void Add<T>(Func<T> instantiating, int capacity = DefaultCapacity) where T : class
    {
        var key = Register<T>(capacity);
        
        Guard.NotNull(instantiating);
        
        for (var i = 0; i < capacity; i++)
        {
            var instance = instantiating.Invoke();
            Storage[key].Push(instance);
        }
    }
    
    public static void Add<T>(
        Func<object[],T> instantiating,
        object[] args,
        int capacity = DefaultCapacity) where T : class
    {
        var key = Register<T>(capacity);
        
        Guard.NotNull(instantiating);
        
        for (var i = 0; i < capacity; i++)
        {
            var instance = instantiating.Invoke(args);
            Storage[key].Push(instance);
        }
    }

    public static T Get<T>() where T : class
    {
        var key = typeof(T);
        var instanse = Storage[key].Pop();
        
        Storage[key].Push(instanse);
        
        return instanse as T;
    }
    
    public static T Pop<T>() where T : class
    {
        var key = typeof(T);
        return Storage[key].Pop() as T;
    }
    
    public static void Release<T>(T instance)
    {
        var key = typeof(T);

        if (Storage[key].Count < DefaultCapacity)
        {
            Storage[key].Push(key);
        }
    }

    private static Type Register<T>(int capacity)
    {
        var key = typeof(T);

        if (!Storage.ContainsKey(key))
        {
            Storage.Add(key: key, value: new Stack<object>(capacity));
        }

        return key;
    }
}