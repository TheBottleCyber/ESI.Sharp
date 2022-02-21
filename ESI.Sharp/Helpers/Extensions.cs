using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace ESI.Sharp.Helpers
{
    public static class Extensions
    {
        public static string ToEsiValue(this Enum e)
        {
            var enums = e.ToString();
            if (enums.Contains(", "))
            {
                var values = enums.Replace(" ", "").Split(',');
                var newValues = new List<string>();
                foreach (var item in values)
                    newValues.Add(Enum.Parse(e.GetType(), item).GetType().GetTypeInfo().DeclaredMembers.SingleOrDefault(x => x.Name == item)
                                      ?.GetCustomAttribute<EnumMemberAttribute>(true)?.Value);

                return string.Join(",", newValues);
            }
            else
                return e.GetType().GetTypeInfo().DeclaredMembers.SingleOrDefault(x => x.Name == e.ToString())
                        ?.GetCustomAttribute<EnumMemberAttribute>(false)?.Value;
        }

    }
}