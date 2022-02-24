using System;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Character
{
    public class CharacterAgent
    {
        [JsonProperty("agent_id")]
        public int AgentId { get; set; }

        [JsonProperty("points_per_day")]
        public float PointsPerDay { get; set; }

        [JsonProperty("remainder_points")]
        public float RemainderPoints { get; set; }

        [JsonProperty("skill_type_id")]
        public int SkillTypeId { get; set; }

        [JsonProperty("started_at")]
        public DateTime StartedAt { get; set; }
    }
}