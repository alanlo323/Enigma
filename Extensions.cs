using Newtonsoft.Json;
using System;

namespace Enigma
{
    internal static class Extensions
    {
        public static string ToJSON(this object obj, Formatting formatting = Formatting.Indented, JsonSerializerSettings settings = null)
        {
            return JsonConvert.SerializeObject(obj, formatting, settings);
        }

        public static object ToObject(this string str, Type type)
        {
            return JsonConvert.DeserializeObject(str, type);
        }
    }
}