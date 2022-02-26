using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Bookmarks
{
    public class CorporationFolder : CharacterFolder
    {
        [JsonProperty("creator_id")]
        public int CreatorId { get; set; }
    }
}