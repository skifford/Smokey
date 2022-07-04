using System;
using System.Diagnostics.CodeAnalysis;

namespace Smokey.Features.ValuesCollecting
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class CollectableAttribute : Attribute
    {
        [NotNull]
        public string Key { get; set; }
    }
}
