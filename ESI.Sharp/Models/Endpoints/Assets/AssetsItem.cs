using ESI.Sharp.Models.Enumerations.Static;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Assets
{
    public class AssetsItem
    {
        [JsonProperty("is_blueprint_copy")]
        public bool IsBlueprintCopy { get; set; }

        [JsonProperty("is_singleton")]
        public bool IsSingleton { get; set; }

        [JsonProperty("item_id")]
        public long ItemId { get; set; }

        [JsonProperty("location_flag")]
        public ItemLocation LocationFlag { get; set; }

        [JsonProperty("location_id")]
        public long LocationId { get; set; }

        [JsonProperty("location_type")]
        public string LocationType { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("type_id")]
        public int TypeId { get; set; }
    }
}