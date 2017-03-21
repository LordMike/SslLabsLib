using System.Collections.Generic;
using Newtonsoft.Json;

namespace SslLabsLib.Objects
{
    public class SimDetails
    {
        /// <summary>
        /// Simulations
        /// </summary>
        [JsonProperty("results")]
        public List<Simulation> Simulations { get; set; }

        public SimDetails()
        {
            Simulations = new List<Simulation>();
        }
    }
}