using System;
using Newtonsoft.Json;
#if NETCORE
using System.Reflection;
#endif

namespace SslLabsLib.Code
{
    public class EnumConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString().ToLower());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Enum.Parse(objectType, (string)reader.Value, true);
        }

        public override bool CanConvert(Type objectType)
        {
#if NETCORE
            return objectType.GetTypeInfo().IsEnum;
#else
            return objectType.IsEnum;
#endif
        }
    }
}