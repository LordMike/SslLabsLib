using System;
using Newtonsoft.Json;
using SslLabsLib.Code;
using SslLabsLib.Enums;

namespace SslLabsLib.Objects
{
    public class HstsPreload
    {
        /// <summary>
        /// Source name
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Preload status
        /// </summary>
        [JsonProperty("status")]
        public HstsPreloadStatus Status { get; set; }

        /// <summary>
        /// Error message, when status is "error"
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }

        /// <summary>
        /// Time, as a Unix timestamp, when the preload database was retrieved
        /// </summary>
        [JsonProperty("sourceTime")]
        [JsonConverter(typeof(MillisecondEpochConverter))]
        public DateTime SourceTime { get; set; }
    }
}