using IntegrationToGist.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegrationToGist.Extensions
{
    public static class DinamicJsonExtensions
    {
        public static IEnumerable<T> DeserializeMembers<T>(this DinamicJson dynamicJson, Func<dynamic, T> resultSelector)
        {
            foreach (var name in dynamicJson.GetDynamicMemberNames())
            {
                yield return resultSelector(((dynamic)dynamicJson)[name]);
            }
        }
    }
}