using System.Collections.Generic;
using ESI.Sharp.Models.Enumerations.Static;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Character
{
    public class CharacterRoles
    {
        [JsonProperty("roles")]
        public List<Role> MainRoles { get; set; }

        [JsonProperty("roles_at_base")]
        public List<Role> RolesAtBase { get; set; }

        [JsonProperty("roles_at_hq")]
        public List<Role> RolesAtHq { get; set; }

        [JsonProperty("roles_at_other")]
        public List<Role> RolesAtOther { get; set; }
    }
}