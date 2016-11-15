using Newtonsoft.Json;
using SslLabsLib.Enums;

namespace SslLabsLib.Objects
{
    public class DrownHost
    {
        /// <summary>
        /// Ip address of server that shares same RSA-Key/hostname in its certificate
        /// </summary>
        [JsonProperty("ip")]
        public string Ip { get; set; }

        /// <summary>
        /// True if export cipher suites detected
        /// </summary>
        [JsonProperty("export")]
        public bool Export { get; set; }

        /// <summary>
        /// Port number of the server
        /// </summary>
        [JsonProperty("port")]
        public int Port { get; set; }

        /// <summary>
        /// True if vulnerable OpenSSL version detected
        /// </summary>
        [JsonProperty("special")]
        public bool Special { get; set; }

        /// <summary>
        /// True if SSL v2 is supported
        /// </summary>
        [JsonProperty("sslv2")]
        public string Sslv2 { get; set; }

        /// <summary>
        /// Drown host status:
        /// </summary>
        [JsonProperty("status")]
        public DrownHostStatus Status { get; set; }
    }
}