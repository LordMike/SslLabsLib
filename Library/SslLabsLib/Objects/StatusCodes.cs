using System.Collections.Generic;
using Newtonsoft.Json;

namespace SslLabsLib.Objects
{
    public class StatusCodes
    {
        /// <summary>
        /// A map containing all status details codes and the corresponding English translations. 
        /// Please note that, once in use, the codes will not change, whereas the translations may change at any time.
        /// </summary>
        [JsonProperty("statusDetails")]
        public Dictionary<string, string> StatusDetails { get; set; }

        public StatusCodes()
        {
            StatusDetails = new Dictionary<string, string>();
        }
    }
}