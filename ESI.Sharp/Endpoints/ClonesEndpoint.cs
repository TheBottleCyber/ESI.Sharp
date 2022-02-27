using System.Collections.Generic;
using System.Threading.Tasks;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Authorization;
using ESI.Sharp.Models.Endpoints;
using ESI.Sharp.Models.Enumerations.Static;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class ClonesEndpoint : EndpointBase
    {
        public ClonesEndpoint(RestClient restClient, ValidatedToken validatedToken) : base(restClient, validatedToken) { }

        /// <summary>
        /// Get clones <br/><br/>
        /// /characters/{character_id}/clones/ <br/><br/>
        /// <c>This route is cached for up to 120 seconds</c>
        /// <br/><c>Requires the following scope: esi-clones.read_clones.v1 </c>
        /// </summary>
        /// <returns>List of the character’s clones</returns>
        public async Task<EsiResponse<Clones>> List()
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/clones/").AddUrlSegment("character_id", _validatedToken.CharacterID);

            return await ExecuteAuthorizedEndpointAsync<Clones>(endpointRequest, Scope.ClonesReadClones);
        }

        /// <summary>
        /// Get active implants <br/><br/>
        /// /characters/{character_id}/implants/ <br/><br/>
        /// <c>This route is cached for up to 120 seconds</c>
        /// <br/><c>Requires the following scope: esi-clones.read_implants.v1 </c>
        /// </summary>
        /// <returns>Implants on the active clone of a character</returns>
        public async Task<EsiResponse<List<int>>> Implants()
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/implants/").AddUrlSegment("character_id", _validatedToken.CharacterID);

            return await ExecuteAuthorizedEndpointAsync<List<int>>(endpointRequest, Scope.ClonesReadImplants);
        }
    }
}