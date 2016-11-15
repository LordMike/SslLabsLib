using System;
using Newtonsoft.Json;

namespace SslLabsLib.Code
{
    public class MillisecondEpochConverter : JsonConverter
    {
        static DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long ms = (long)(((DateTime)value) - _epoch).TotalMilliseconds;
            writer.WriteValue(ms);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return _epoch.AddMilliseconds((long)reader.Value);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(DateTime) == objectType;
        }
    }
}