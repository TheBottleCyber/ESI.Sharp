using ESI.Sharp.Models.Shared;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Assets
{
    public class AssetsItemLocation
    {
        [JsonProperty("item_id")]
        public long ItemId { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }
    }
}