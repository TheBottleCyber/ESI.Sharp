using System;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Character
{
    public class Information
    {
        [JsonProperty("alliance_id")]
        public int AllianceId { get; set; }

        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }

        [JsonProperty("bloodline_id")]
        public int BloodlineId { get; set; }

        [JsonProperty("corporation_id")]
        public int CorporationId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("race_id")]
        public int RaceId { get; set; }

        [JsonProperty("security_status")]
        public double SecurityStatus { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        public Information() { }

        public Information(int allianceId, DateTime birthday, int bloodlineId, int corporationId, string description, 
            string gender, string name, int raceId, double securityStatus, string title)
        {
            AllianceId = allianceId;
            Birthday = birthday;
            BloodlineId = bloodlineId;
            CorporationId = corporationId;
            Description = description;
            Gender = gender;
            Name = name;
            RaceId = raceId;
            SecurityStatus = securityStatus;
            Title = title;
        }
    }
}