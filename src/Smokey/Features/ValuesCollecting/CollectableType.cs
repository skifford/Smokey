using System;

namespace Smokey.Features.ValuesCollecting;

[Flags]
public enum CollectableType : byte
{
    Getter = 1,
    Setter = 2
}
