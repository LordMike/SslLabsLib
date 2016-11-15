using Newtonsoft.Json;

namespace SslLabsLib.Objects
{
    // TODO: Rename to SimClient
    public class Client
    {
        /// <summary>
        /// Unique client ID
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary> 
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Platform
        /// </summary>
        [JsonProperty("platform", NullValueHandling = NullValueHandling.Ignore)]
        public string Platform { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// True if the browser is considered representative of modern browsers, false otherwise. 
        /// This flag does not correlate to client's capabilities, but is used by SSL Labs to determine if a particular configuration is effective. 
        /// For example, to track Forward Secrecy support, we mark several representative browsers as "modern" and then test to see if they succeed in negotiating a FS suite. 
        /// Just as an illustration, modern browsers are currently Chrome, Firefox (not ESR versions), IE/Win7, and Safari.
        /// </summary>
        [JsonProperty("isReference")]
        public bool IsReference { get; set; }
    }
}