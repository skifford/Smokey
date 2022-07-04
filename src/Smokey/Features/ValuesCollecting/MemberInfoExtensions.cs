using System.Reflection;

namespace Smokey.Features.ValuesCollecting;

internal static class MemberInfoExtensions
{
    public static bool IsCollectable(this MemberInfo memberInfo)
    {
        return memberInfo.IsDefined(typeof(CollectableAttribute));
    }
    
    public static bool NotCollectable(this MemberInfo memberInfo)
    {
        return memberInfo.IsCollectable() is false;
    }
}
