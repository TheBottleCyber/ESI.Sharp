using System;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Contracts
{
    /// <summary>
    /// Contract bid
    /// </summary>
    public class ContractBid
    {
        /// <summary>
        /// Unique ID for the bid
        /// </summary>
        [JsonProperty("bid_id")]
        public int BidId { get; set; }

        /// <summary>
        /// Character ID of the bidder
        /// </summary>
        [JsonProperty("bidder_id")]
        public int BidderId { get; set; }

        /// <summary>
        /// Datetime when the bid was placed
        /// </summary>
        [JsonProperty("date_bid")]
        public DateTime DateBid { get; set; }

        /// <summary>
        /// The amount bid, in ISK
        /// </summary>
        [JsonProperty("amount")]
        public double Amount { get; set; }
    }
}