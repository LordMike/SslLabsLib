using System.Collections.Generic;
using Newtonsoft.Json;
using SslLabsLib.Code;
using SslLabsLib.Enums;

namespace SslLabsLib.Objects
{
    public class HpkpPolicy
    {
        /// <summary>
        /// HPKP Status
        /// </summary>
        [JsonProperty("status")]
        public HpkpStatus Status { get; set; }

        /// <summary>
        /// The contents of the HPKP response header, if present
        /// </summary>
        [JsonProperty("header", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Header { get; set; }

        /// <summary>
        /// Error message, when the policy is invalid
        /// </summary>
        [JsonProperty("error", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Error { get; set; }

        /// <summary>
        /// The max-age value from the policy
        /// </summary>
        [JsonProperty("maxAge", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long MaxAge { get; set; }

        /// <summary>
        /// True if the includeSubDomains directive is set; null otherwise
        /// </summary>
        [JsonProperty("includeSubDomains", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IncludeSubDomains { get; set; }

        /// <summary>
        /// The report-uri value from the policy
        /// </summary>
        [JsonProperty("reportUri", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ReportUri { get; set; }

        /// <summary>
        /// List of all pins used by the policy
        /// </summary>
        [JsonProperty("pins")]
        public List<HpkpPin> Pins { get; set; }

        /// <summary>
        /// List of pins that match the current configuration
        /// </summary>
        [JsonProperty("matchedPins")]
        public List<HpkpPin> MatchedPins { get; set; }

        /// <summary>
        /// List of raw policy directives
        /// </summary>
        [JsonProperty("directives")]
        [JsonConverter(typeof(KeyValuePairListConverter))]
        public List<KeyValuePair<string, string>> Directives { get; set; }
    }
}