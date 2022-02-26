using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Assets
{
    public class AssetsItemName
    {
        [JsonProperty("item_id")]
        public long ItemId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}