using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Contacts
{
    public class ContactLabel
    {
        [JsonProperty("label_id")]
        public long LabelId { get; set; }

        [JsonProperty("label_name")]
        public string LabelName { get; set; }
    }
}