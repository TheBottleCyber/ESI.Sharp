using System.Collections.Generic;
using System.Threading.Tasks;
using ESI.Sharp.Helpers;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Authorization;
using ESI.Sharp.Models.Endpoints.Character;
using ESI.Sharp.Models.Enumerations.Static;
using ESI.Sharp.Models.Shared;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class CharacterEndpoint
    {
        private readonly EndpointExecutor _executor;
        private readonly ValidatedToken _validatedToken;

        public CharacterEndpoint(EndpointExecutor executor, ValidatedToken validatedToken)
        {
            _executor = executor;
            _validatedToken = validatedToken;
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

            return await _executor.ExecutePublicEndpointAsync<CharacterInformation>(endpointRequest);
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

            return await _executor.ExecutePublicEndpointAsync<List<CharacterAffiliation>>(endpointRequest);
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

            return await _executor.ExecutePublicEndpointAsync<List<CharacterCorporationHistory>>(endpointRequest);
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

            return await _executor.ExecutePublicEndpointAsync<Images>(endpointRequest);
        }

        /// <summary>
        /// Get character medals <br/><br/>
        /// /characters/{character_id}/medals/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// <br/><c>Requires the following scope: esi-characters.read_medals.v1 </c>
        /// </summary>
        /// <returns>List of medals the character has</returns>
        public async Task<EsiResponse<List<CharacterMedal>>> Medals()
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/medals/").AddUrlSegment("character_id", _validatedToken.CharacterID);

            return await _executor.ExecuteAuthorizatedEndpointAsync<List<CharacterMedal>>(endpointRequest, _validatedToken,
                Scope.CharactersReadMedals);
        }
    }
}