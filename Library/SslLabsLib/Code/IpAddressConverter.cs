using System;
using System.Net;
using Newtonsoft.Json;

namespace SslLabsLib.Code
{
    class IpAddressConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            IPAddress addr = (IPAddress)value;

            writer.WriteValue(addr.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string str = (string)reader.Value;

            return IPAddress.Parse(str);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IPAddress);
        }
    }
}