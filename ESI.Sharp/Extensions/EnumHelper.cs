using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace ESI.Sharp.Extensions
{
    public static class EnumHelper
    {
        public static string GetEnumMemberAttribute(this Enum e)
        {
            return e.GetType().GetTypeInfo().DeclaredMembers.SingleOrDefault(x => x.Name == e.ToString())
                    ?.GetCustomAttribute<EnumMemberAttribute>(false)?.Value;
        }
    }
}