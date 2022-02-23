using System;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints
{
    /// <summary>
    /// EVE Server status
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Current online player count
        /// </summary>
        [JsonProperty("players")]
        public int Players { get; set; }

        /// <summary>
        /// Running version as string
        /// </summary>
        [JsonProperty("server_version")]
        public string ServerVersion { get; set; }

        /// <summary>
        /// Server start timestamp
        /// </summary>
        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// If the server is in VIP mode
        /// </summary>
        [JsonProperty("vip")]
        public bool VIP { get; set; }

        public Status() { }

        public Status(int players, string serverVersion, DateTime startTime, bool vip)
        {
            Players = players;
            ServerVersion = serverVersion;
            StartTime = startTime;
            VIP = vip;
        }
    }
}