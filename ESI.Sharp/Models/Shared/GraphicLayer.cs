using Newtonsoft.Json;

namespace ESI.Sharp.Models.Shared
{
    public class GraphicLayer
    {
        [JsonProperty("color")]
        public int Color { get; set; }

        [JsonProperty("graphic")]
        public string Graphic { get; set; }

        [JsonProperty("layer")]
        public int Layer { get; set; }

        [JsonProperty("part")]
        public int Part { get; set; }
    }
}