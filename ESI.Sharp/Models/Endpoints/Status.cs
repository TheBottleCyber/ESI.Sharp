using System;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints
{
    /// <summary>
    /// EVE Server status
    /// <example>Example json body<code>
    /// {
    /// "players": 21514,
    /// "server_version": "2003445",
    /// "start_time": "2022-02-20T11:01:33Z"
    /// }
    /// </code></example>
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