using Newtonsoft.Json;

namespace SslLabsLib.Objects
{
    public class HpkpPin
    {
        /// <summary>
        /// The hash function in use, e.g. sha256
        /// </summary>
        [JsonProperty("hashFunction")]
        public string HashFunction { get; set; }

        /// <summary>
        /// The fingerprint of the pin, hex encoded
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}