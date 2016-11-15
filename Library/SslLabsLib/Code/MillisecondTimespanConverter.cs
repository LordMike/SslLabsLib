using System;
using Newtonsoft.Json;

namespace SslLabsLib.Code
{
    public class MillisecondTimespanConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long ms = (long)((TimeSpan)value).TotalMilliseconds;
            writer.WriteValue(ms);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return TimeSpan.FromMilliseconds((long)reader.Value);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(TimeSpan) == objectType;
        }
    }
}