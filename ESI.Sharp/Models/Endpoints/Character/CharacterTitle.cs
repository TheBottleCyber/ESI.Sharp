using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Character
{
    public class CharacterTitle
    {
        [JsonProperty("title_id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}