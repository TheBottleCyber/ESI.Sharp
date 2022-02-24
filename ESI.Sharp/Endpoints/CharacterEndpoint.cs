using System.Collections.Generic;
using System.Threading.Tasks;
using ESI.Sharp.Helpers;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Endpoints.Character;
using ESI.Sharp.Models.Shared;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class CharacterEndpoint
    {
        private readonly EndpointExecutor _executor;

        public CharacterEndpoint(EndpointExecutor executor)
        {
            _executor = executor;
        }
        
        /// <summary>
        /// Get character's public information <br/><br/>
        /// /characters/{character_id}/ <br/><br/>
        /// <c>This route is cached for up to 86400 seconds</c>
        /// </summary>
        /// <param name="characterId">An EVE character ID</param>
        /// <returns>Public information about a character</returns>
        public async Task<EsiResponse<CharacterInformation>> Information(int characterId)
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/").AddUrlSegment("character_id", characterId);

            return await _executor.ExecuteEndpointAsync<CharacterInformation>(endpointRequest);
        }
        
        /// <summary>
        /// Bulk lookup of character IDs to corporation, alliance and faction <br/><br/>
        /// /characters/affiliation/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// </summary>
        /// <param name="characterIds">The character IDs to fetch affiliations for. All characters must exist, or none will be returned</param>
        /// <returns>Characters corporations, alliances and factions IDs</returns>
        public async Task<EsiResponse<List<CharacterAffiliation>>> Affiliation(IEnumerable<int> characterIds)
        {
            var endpointRequest = new RestRequest("/characters/affiliation/", Method.Post).AddJsonBody(characterIds);

            return await _executor.ExecuteEndpointAsync<List<CharacterAffiliation>>(endpointRequest);
        }
        
        /// <summary>
        /// Get a list of all the corporations a character has been a member of <br/><br/>
        /// /characters/{character_id}/corporationhistory/ <br/><br/>
        /// <c>This route is cached for up to 86400 seconds</c>
        /// </summary>
        /// <param name="characterId">An EVE character ID</param>
        /// <returns>Character corporation history</returns>
        public async Task<EsiResponse<List<CharacterCorporationHistory>>> CorporationHistory(int characterId)
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/corporationhistory/").AddUrlSegment("character_id", characterId);

            return await _executor.ExecuteEndpointAsync<List<CharacterCorporationHistory>>(endpointRequest);
        }
        
        /// <summary>
        /// Get character portraits <br/><br/>
        /// characters/{character_id}/portrait/ <br/><br/>
        /// <c>This route expires daily at 11:05</c>
        /// </summary>
        /// <param name="characterId">An EVE character ID</param>
        /// <returns>Portrait urls for a character</returns>
        public async Task<EsiResponse<Images>> Portrait(int characterId)
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/portrait/").AddUrlSegment("character_id", characterId);

            return await _executor.ExecuteEndpointAsync<Images>(endpointRequest);
        }
    }
}