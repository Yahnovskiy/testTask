using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Services
{
    public static class JsonConverter
    {
        public static T FromJson<T>(string json) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(json, Settings);
        }

        public static string ToJson<T>(this T self) where T : new()
        {
            return JsonConvert.SerializeObject(self, Settings);
        }

        public static List<T> FromJsonToListOfObjects<T>(string json) where T : class
        {
            return JsonConvert.DeserializeObject<List<T>>(json, Settings);
        }

        public static string FromListOfObjectsToJson<T>(this List<T> self) where T : class
        {
            return JsonConvert.SerializeObject(self, Settings);
        }

        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
