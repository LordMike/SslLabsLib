using System.Collections.Generic;
using Newtonsoft.Json;
using SslLabsLib.Enums;

namespace SslLabsLib.Objects
{
    public class Chain
    {
        /// <summary>
        /// Certificates in the chain
        /// </summary>
        [JsonProperty("certs")]
        public List<ChainCert> Certs { get; set; }

        /// <summary>
        /// The issues with this chain
        /// </summary>
        [JsonProperty("issues")]
        public ChainIssues Issues { get; set; }

        public Chain()
        {
            Certs = new List<ChainCert>();
        }
    }
}