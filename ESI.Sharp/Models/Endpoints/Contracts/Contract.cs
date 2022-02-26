using System;
using ESI.Sharp.Models.Enumerations;
using ESI.Sharp.Models.Enumerations.Static;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Contracts
{
    public class Contract
    {
        /// <summary>
        /// Contract id
        /// </summary>
        [JsonProperty("contract_id")]
        public int ContractId { get; set; }

        /// <summary>
        /// Character ID for the issuer
        /// </summary>
        [JsonProperty("issuer_id")]
        public int IssuerId { get; set; }

        /// <summary>
        /// Character's corporation ID for the issuer
        /// </summary>
        [JsonProperty("issuer_corporation_id")]
        public int IssuerCorporationId { get; set; }

        /// <summary>
        /// ID to whom the contract is assigned, can be alliance, corporation or character ID
        /// </summary>
        [JsonProperty("assignee_id")]
        public int AssigneeId { get; set; }

        /// <summary>
        /// Who will accept the contract
        /// </summary>
        [JsonProperty("acceptor_id")]
        public int AcceptorId { get; set; }

        /// <summary>
        /// Start location ID (for Couriers contract)
        /// </summary>
        [JsonProperty("start_location_id")]
        public long StartLocationId { get; set; }

        /// <summary>
        /// End location ID (for Couriers contract)
        /// </summary>
        [JsonProperty("end_location_id")]
        public long EndLocationId { get; set; }

        /// <summary>
        /// Type of the contract
        /// </summary>
        [JsonProperty("type")]
        public ContractType Type { get; set; }

        /// <summary>
        /// Status of the contract
        /// </summary>
        [JsonProperty("status")]
        public ContractStatus Status { get; set; }

        /// <summary>
        /// Title of the contract
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// True if the contract was issued on behalf of the issuer's corporation
        /// </summary>
        [JsonProperty("for_corporation")]
        public bool ForCorporation { get; set; }

        /// <summary>
        /// To whom the contract is available
        /// </summary>
        [JsonProperty("availability")]
        public string Availability { get; set; }

        /// <summary>
        /// Сreation date of the contract
        /// </summary>
        [JsonProperty("date_issued")]
        public DateTime DateIssued { get; set; }

        /// <summary>
        /// Expiration date of the contract
        /// </summary>
        [JsonProperty("date_expired")]
        public DateTime DateExpired { get; set; }

        /// <summary>
        /// Date when contract was accepted
        /// </summary>
        [JsonProperty("date_accepted")]
        public DateTime DateAccepted { get; set; }

        /// <summary>
        /// Number of days to perform the contract
        /// </summary>
        [JsonProperty("days_to_complete")]
        public int DaysToComplete { get; set; }

        /// <summary>
        /// Date when contract was completed
        /// </summary>
        [JsonProperty("date_completed")]
        public DateTime DateCompleted { get; set; }

        /// <summary>
        /// Price of contract (for ItemsExchange and Auctions)
        /// </summary>
        [JsonProperty("price")]
        public double Price { get; set; }

        /// <summary>
        /// Remuneration for contract (for Couriers only)
        /// </summary>
        [JsonProperty("reward")]
        public double Reward { get; set; }

        /// <summary>
        /// Collateral price (for Couriers only)
        /// </summary>
        [JsonProperty("collateral")]
        public double Collateral { get; set; }

        /// <summary>
        /// Buyout price (for Auctions only)
        /// </summary>
        [JsonProperty("buyout")]
        public double Buyout { get; set; }

        /// <summary>
        /// Volume of items in the contract
        /// </summary>
        [JsonProperty("volume")]
        public double Volume { get; set; }
    }
}