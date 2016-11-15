using Newtonsoft.Json;

namespace SslLabsLib.Objects
{
    public class Key
    {
        /// <summary>
        /// Key size, e.g., 1024 or 2048 for RSA and DSA, or 256 bits for EC.
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; set; }

        /// <summary>
        /// Key size expressed in RSA bits.
        /// </summary>
        [JsonProperty("strength")]
        public int Strength { get; set; }

        /// <summary>
        /// Key algorithm; possible values: RSA, DSA, and EC.
        /// </summary>
        [JsonProperty("alg")]
        public string Alg { get; set; }

        /// <summary>
        /// True if we suspect that the key was generated using a weak random number generator (detected via a blacklist database)
        /// </summary>
        [JsonProperty("debianFlaw")]
        public bool DebianFlaw { get; set; }

        /// <summary>
        /// 0 if key is insecure, null otherwise
        /// </summary>
        [JsonProperty("q", NullValueHandling = NullValueHandling.Ignore)]
        public int? Q { get; set; }
    }
}