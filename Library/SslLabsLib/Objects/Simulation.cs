using Newtonsoft.Json;

namespace SslLabsLib.Objects
{
    public class Simulation
    {
        /// <summary>
        /// The client used
        /// </summary>
        [JsonProperty("client")]
        public SimClient Client { get; set; }

        /// <summary>
        /// Zero if handshake was successful, 1 if it was not.
        /// </summary>
        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// Always 1 with the current implementation.
        /// </summary>
        [JsonProperty("attempts")]
        public int Attempts { get; set; }

        /// <summary>
        /// Negotiated protocol ID.
        /// </summary>
        [JsonProperty("protocolId")]
        public int ProtocolId { get; set; }

        /// <summary>
        /// Negotiated suite ID.
        /// </summary>
        [JsonProperty("suiteId")]
        public int SuiteId { get; set; }

        public Simulation()
        {
            Client = new SimClient();
        }
    }
}