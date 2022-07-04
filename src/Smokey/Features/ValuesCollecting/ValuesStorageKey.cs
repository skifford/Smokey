using System;

namespace Smokey.Features.ValuesCollecting;

public static class ValuesStorageKey
{
    public static string Create(Type typeOfAggregate, string propertyName) =>
        $"{typeOfAggregate.FullName}.{propertyName}";
}
