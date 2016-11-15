using System.Collections.Generic;
using Newtonsoft.Json;

namespace SslLabsLib.Objects
{
    // TODO: Rename SimDetails
    public class SimulationDetails
    {
        /// <summary>
        /// Simulations
        /// </summary>
        [JsonProperty("results")]
        public List<Simulation> Simulations { get; set; }

        public SimulationDetails()
        {
            Simulations = new List<Simulation>();
        }
    }
}