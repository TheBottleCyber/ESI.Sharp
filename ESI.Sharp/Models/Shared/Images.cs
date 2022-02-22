using Newtonsoft.Json;

namespace ESI.Sharp.Models.Shared
{
    /// <summary>
    /// Icon URLs
    /// </summary>
    public class Images
    {
        [JsonProperty("px512x512")]
        public string x512 { get; set; }

        [JsonProperty("px256x256")]
        public string x256 { get; set; }

        [JsonProperty("px128x128")]
        public string x128 { get; set; }

        [JsonProperty("px64x64")]
        public string x64 { get; set; }

        public Images() { }

        public Images(string x512 = "", string x256 = "", string x128 = "", string x64 = "")
        {
            this.x512 = x512;
            this.x256 = x256;
            this.x128 = x128;
            this.x64 = x64;
        }
    }
}