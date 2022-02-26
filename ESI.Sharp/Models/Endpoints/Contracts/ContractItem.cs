using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Contracts
{
    /// <summary>
    /// Contract item
    /// </summary>
    public class ContractItem
    {
        /// <summary>
        /// Unique ID for the item, used by the contract system
        /// </summary>
        [JsonProperty("record_id")]
        public long RecordId { get; set; }

        /// <summary>
        /// Type ID for item
        /// </summary>
        [JsonProperty("type_id")]
        public int TypeId { get; set; }

        /// <summary>
        /// Number of items in the stack
        /// </summary>
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// -1 indicates that the item is a singleton (non-stackable). If the item happens to be a Blueprint, -1 is an Original and -2 is a Blueprint Copy
        /// </summary>
        [JsonProperty("raw_quantity")]
        public int RawQuantity { get; set; }

        /// <summary>
        /// True if the item is a singleton (non-stackable)
        /// </summary>
        [JsonProperty("is_singleton")]
        public bool IsSingleton { get; set; }

        /// <summary>
        /// True if the contract issuer has submitted this item with the contract, False if the isser is asking for this item in the contract
        /// </summary>
        [JsonProperty("is_included")]
        public bool IsIncluded { get; set; }

        /// <summary>
        /// True if the blueprint is a copy
        /// </summary>
        [JsonProperty("is_blueprint_copy")]
        public bool IsBlueprintCopy { get; set; }

        /// <summary>
        /// Unique ID for the item being sold. Not present if item is being requested by contract rather than sold with contract
        /// </summary>
        [JsonProperty("item_id")]
        public long ItemId { get; set; }

        /// <summary>
        /// Material Efficiency Level of the blueprint
        /// </summary>
        [JsonProperty("material_efficiency")]
        public int MaterialEfficiency { get; set; }

        /// <summary>
        /// Number of runs remaining if the blueprint is a copy, -1 if it is an original
        /// </summary>
        [JsonProperty("runs")]
        public int Runs { get; set; }

        /// <summary>
        /// Time Efficiency Level of the blueprint
        /// </summary>
        [JsonProperty("time_efficiency")]
        public int TimeEfficiency { get; set; }
    }
}