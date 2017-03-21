using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SslLabsLib.Code;
using SslLabsLib.Enums;

namespace SslLabsLib.Objects
{
    public class Host
    {
        /// <summary>
        /// Assessment host, which can be a hostname or an IP address
        /// </summary>
        [JsonProperty("host")]
        public string Hostname { get; set; }

        /// <summary>
        /// Assessment port (e.g., 443)
        /// </summary>
        [JsonProperty("port")]
        public int Port { get; set; }

        /// <summary>
        /// Protocol (e.g., HTTP)
        /// </summary>
        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        /// <summary>
        /// True if this assessment publicly available (listed on the SSL Labs assessment boards)
        /// </summary>
        [JsonProperty("isPublic")]
        public bool IsPublic { get; set; }

        /// <summary>
        /// Assessment status; possible values: DNS, ERROR, IN_PROGRESS, and READY.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("status")]
        public AnalysisStatus Status { get; set; }

        /// <summary>
        /// Status message in English. When status is ERROR, this field will contain an error message.
        /// </summary>
        [JsonProperty("statusMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string StatusMessage { get; set; }

        /// <summary>
        /// Assessment starting time, in milliseconds since 1970
        /// </summary>
        [JsonConverter(typeof(MillisecondEpochConverter))]
        [JsonProperty("startTime")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Assessment completion time, in milliseconds since 1970
        /// </summary>
        [JsonConverter(typeof(MillisecondEpochConverter))]
        [JsonProperty("testTime")]
        public DateTime TestTime { get; set; }

        /// <summary>
        /// Assessment engine version (e.g., "1.0.120")
        /// </summary>
        [JsonProperty("engineVersion")]
        public string EngineVersion { get; set; }

        /// <summary>
        /// Grading criteria version (e.g., "2009")
        /// </summary>
        [JsonProperty("criteriaVersion")]
        public string CriteriaVersion { get; set; }

        /// <summary>
        /// When will the assessment results expire from the cache (typically set only for assessment with errors; otherwise the results stay in the cache for as long as there's sufficient room)
        /// </summary>
        [JsonProperty("cacheExpiryTime")]
        public long CacheExpiryTime { get; set; }

        /// <summary>
        /// List of Endpoint objects
        /// </summary>
        [JsonProperty("endpoints")]
        public List<Endpoint> Endpoints { get; set; }

        /// <summary>
        /// The list of certificate hostnames collected from the certificates seen during assessment. The hostnames may not be valid. 
        /// This field is available only if the server certificate doesn't match the requested hostname. In that case, this field saves you some time as you don't have to inspect the certificates yourself to find out what valid hostnames might be.
        /// </summary>
        [JsonProperty("certHostnames", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> CertHostnames { get; set; }
        
        public Host()
        {
            Endpoints = new List<Endpoint>();
            CertHostnames = new List<string>();
        }
    }
}