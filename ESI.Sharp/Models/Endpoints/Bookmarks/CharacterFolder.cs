using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Bookmarks
{
    public class CharacterFolder
    {
        [JsonProperty("folder_id")]
        public int FolderId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}