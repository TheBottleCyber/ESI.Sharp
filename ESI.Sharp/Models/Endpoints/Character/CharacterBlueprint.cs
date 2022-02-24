using ESI.Sharp.Models.Enumerations.Static;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Character
{
    public class CharacterBlueprint
    {
        /// <summary>
        /// Unique ID for this item.
        /// </summary>
        [JsonProperty("item_id")]
        public long ItemId { get; set; }

        /// <summary>
        /// Type of the location_id
        /// </summary>
        [JsonProperty("location_flag")]
        public ItemLocation LocationFlag { get; set; }

        /// <summary>
        /// References a station, a ship or an item_id if this blueprint is located within a container. If the return value is an item_id, then the Character AssetList API must be queried to find the container using the given item_id to determine the correct location of the Blueprint.
        /// </summary>
        [JsonProperty("location_id")]
        public long LocationId { get; set; }

        /// <summary>
        /// Material Efficiency Level of the blueprint.
        /// </summary>
        [JsonProperty("material_efficiency")]
        public int MaterialEfficiency { get; set; }

        /// <summary>
        /// A range of numbers with a minimum of -2 and no maximum value where -1 is an original and -2 is a copy. It can be a positive integer if it is a stack of blueprint originals fresh from the market (e.g. no activities performed on them yet).
        /// </summary>
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Number of runs remaining if the blueprint is a copy, -1 if it is an original.
        /// </summary>
        [JsonProperty("runs")]
        public int Runs { get; set; }

        /// <summary>
        /// Time Efficiency Level of the blueprint.
        /// </summary>
        [JsonProperty("time_efficiency")]
        public int TimeEfficiency { get; set; }
        
        [JsonProperty("type_id")]
        public int TypeId { get; set; }
    }
}