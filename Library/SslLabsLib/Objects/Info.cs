using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SslLabsLib.Code;

namespace SslLabsLib.Objects
{
    public class Info
    {
        /// <summary>
        /// SSL Labs software version as a string (e.g., "1.11.14")
        /// </summary>
        [JsonProperty("engineVersion")]
        public string EngineVersion { get; set; }

        /// <summary>
        /// Rating criteria version as a string (e.g., "2009f")
        /// </summary>
        [JsonProperty("criteriaVersion")]
        public string CriteriaVersion { get; set; }

        /// <summary>
        /// The maximum number of concurrent assessments the client is allowed to initiate.
        /// </summary>
        [JsonProperty("maxAssessments")]
        public int MaxAssessments { get; set; }

        /// <summary>
        /// The number of ongoing assessments submitted by this client.
        /// </summary>
        [JsonProperty("currentAssessments")]
        public int CurrentAssessments { get; set; }

        /// <summary>
        /// The cool-off period after each new assessment; you're not allowed to submit a new assessment before the cool-off expires, otherwise you'll get a 429.
        /// </summary>
        [JsonProperty("newAssessmentCoolOff")]
        [JsonConverter(typeof(MillisecondTimespanConverter))]
        public TimeSpan NewAssessmentCoolOff { get; set; }

        /// <summary>
        /// A list of messages (strings). Messages can be public (sent to everyone) and private (sent only to the invoking client). Private messages are prefixed with "[Private]".
        /// </summary>
        [JsonProperty("messages")]
        public List<string> Messages { get; set; }

        public Info()
        {
            Messages = new List<string>();
        }
    }
}