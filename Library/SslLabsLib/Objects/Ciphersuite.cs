using Newtonsoft.Json;

namespace SslLabsLib.Objects
{
    // TODO: Rename to Suite
    public class Ciphersuite
    {
        /// <summary>
        /// Suite RFC ID (e.g., 5)
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Suite name (e.g., TLS_RSA_WITH_RC4_128_SHA)
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Suite strength (e.g., 128)
        /// </summary>
        [JsonProperty("cipherStrength")]
        public int CipherStrength { get; set; }

        /// <summary>
        /// Strength of DH params (e.g., 1024)
        /// </summary>
        [JsonProperty("dhStrength", NullValueHandling = NullValueHandling.Ignore)]
        public string DhStrength { get; set; }

        /// <summary>
        /// DH params, p component
        /// </summary>
        [JsonProperty("dhP", NullValueHandling = NullValueHandling.Ignore)]
        public string DhP { get; set; }

        /// <summary>
        /// DH params, g component
        /// </summary>
        [JsonProperty("dhG", NullValueHandling = NullValueHandling.Ignore)]
        public string DhG { get; set; }

        /// <summary>
        /// DH params, Ys component
        /// </summary>
        [JsonProperty("dhYs", NullValueHandling = NullValueHandling.Ignore)]
        public string DhYs { get; set; }

        /// <summary>
        /// ECDH bits
        /// </summary>
        [JsonProperty("ecdhBits")]
        public int EcdhBits { get; set; }

        /// <summary>
        /// ECDH RSA-equivalent strength
        /// </summary>
        [JsonProperty("ecdhStrength")]
        public int EcdhStrength { get; set; }

        /// <summary>
        /// 0 if the suite is insecure, null otherwise
        /// </summary>
        [JsonProperty("q", NullValueHandling = NullValueHandling.Ignore)]
        public int? Q { get; set; }
    }
}