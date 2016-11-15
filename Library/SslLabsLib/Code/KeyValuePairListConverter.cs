using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SslLabsLib.Code
{
    class KeyValuePairListConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            List<KeyValuePair<string, string>> list = (List<KeyValuePair<string, string>>)value;

            JObject obj = new JObject();

            foreach (KeyValuePair<string, string> pair in list)
                obj[pair.Key] = pair.Value;

            obj.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken obj = JObject.ReadFrom(reader);

            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            foreach (JProperty jToken in obj.OfType<JProperty>())
                list.Add(new KeyValuePair<string, string>(jToken.Name, (string)jToken.Value));

            return list;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<KeyValuePair<string, object>>);
        }
    }
}