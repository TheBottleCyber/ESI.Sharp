using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Character
{
    public class CharacterStanding
    {
        [JsonProperty("from_id")]
        public int FromId { get; set; }

        [JsonProperty("from_type")]
        public string FromType { get; set; }

        [JsonProperty("standing")]
        public decimal Value { get; set; }
    }
}