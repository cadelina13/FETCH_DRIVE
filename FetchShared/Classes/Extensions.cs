using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FetchShared.Classes
{
    public static class Extensions
    {
        public static string ToJSON(this object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
        public static T DeserializeJSON<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
