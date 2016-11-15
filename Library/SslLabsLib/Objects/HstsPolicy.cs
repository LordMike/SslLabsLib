using System.Collections.Generic;
using Newtonsoft.Json;
using SslLabsLib.Code;
using SslLabsLib.Enums;

namespace SslLabsLib.Objects
{
    public class HstsPolicy
    {
        /// <summary>
        /// This constant contains what SSL Labs considers to be sufficiently large max-age value
        /// </summary>
        [JsonProperty("LONG_MAX_AGE")]
        public long LongMaxAge { get; set; }

        /// <summary>
        /// The contents of the HSTS response header, if present
        /// </summary>
        [JsonProperty("header")]
        public string Header { get; set; }

        /// <summary>
        /// HSTS Status
        /// </summary>
        [JsonProperty("status")]
        public HstsStatus Status { get; set; }

        /// <summary>
        /// Error message when error is encountered, null otherwise
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }

        /// <summary>
        /// The max-age value specified in the policy; null if policy is missing or invalid or on parsing error; the maximum value currently supported is 9223372036854775807
        /// </summary>
        [JsonProperty("maxAge")]
        public long? MaxAge { get; set; }

        /// <summary>
        /// True if the includeSubDomains directive is set; null otherwise
        /// </summary>
        [JsonProperty("includeSubDomains")]
        public bool IncludeSubDomains { get; set; }

        /// <summary>
        /// True if the preload directive is set; null otherwise
        /// </summary>
        [JsonProperty("preload", NullValueHandling = NullValueHandling.Ignore)]
        public bool Preload { get; set; }

        /// <summary>
        /// List of raw policy directives
        /// </summary>
        [JsonProperty("directives")]
        [JsonConverter(typeof(KeyValuePairListConverter))]
        public List<KeyValuePair<string, string>> Directives { get; set; }
    }
}