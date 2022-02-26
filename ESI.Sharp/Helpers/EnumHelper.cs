using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace ESI.Sharp.Helpers
{
    public static class EnumHelper
    {
        public static string GetEnumMemberAttribute(this Enum e) => e.GetType().GetTypeInfo().DeclaredMembers.SingleOrDefault(x => x.Name == e.ToString())?.GetCustomAttribute<EnumMemberAttribute>(false)?.Value;
        public static IEnumerable<T> GetEnumValues<T>() where T : Enum => Enum.GetValues(typeof(T)).Cast<T>();
        public static T ParseEnum<T>(string value) => (T) Enum.Parse(typeof(T), value, true);
    }
}