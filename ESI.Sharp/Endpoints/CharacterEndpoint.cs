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

            return await _executor.ExecuteAuthorizatedEndpointAsync<List<CharacterMedal>>(endpointRequest, _validatedToken, Scope.CharactersReadMedals);
        }

        /// <summary>
        /// Get agents research <br/><br/>
        /// /characters/{character_id}/agents_research/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// <br/><c>Requires the following scope: esi-characters.read_agents_research.v1 </c>
        /// </summary>
        /// <returns>List of agents research information for a character. The formula for finding the current research points with an agent is: currentPoints = remainderPoints + pointsPerDay * days(currentTime - researchStartDate)</returns>
        public async Task<EsiResponse<List<CharacterAgent>>> AgentsResearch()
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/agents_research/").AddUrlSegment("character_id", _validatedToken.CharacterID);

            return await _executor.ExecuteAuthorizatedEndpointAsync<List<CharacterAgent>>(endpointRequest, _validatedToken, Scope.CharactersReadAgentsResearch);
        }

        /// <summary>
        /// Get blueprints <br/><br/>
        /// /characters/{character_id}/blueprints/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// <br/><c>Requires the following scope: esi-characters.read_blueprints.v1 </c>
        /// </summary>
        /// <returns>List of blueprints the character owns</returns>
        public async Task<EsiResponse<List<CharacterBlueprint>>> Blueprints()
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/blueprints/").AddUrlSegment("character_id", _validatedToken.CharacterID);

            return await _executor.ExecuteAuthorizatedEndpointAsync<List<CharacterBlueprint>>(endpointRequest, _validatedToken, Scope.CharactersReadBlueprints);
        }

        /// <summary>
        /// Calculate a CSPA charge cost <br/><br/>
        /// /characters/{character_id}/cspa/ <br/><br/>
        /// <c>This route is not cached</c>
        /// <br/><c>Requires the following scope: esi-characters.read_contacts.v1 </c>
        /// </summary>
        /// <returns>List of blueprints the character owns</returns>
        public async Task<EsiResponse<float>> CalculateCspa(IEnumerable<int> characterIds)
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/cspa/", Method.Post).AddUrlSegment("character_id", _validatedToken.CharacterID)
                                                                                                  .AddJsonBody(characterIds);

            return await _executor.ExecuteAuthorizatedEndpointAsync<float>(endpointRequest, _validatedToken, Scope.CharactersReadContacts);
        }
        
        /// <summary>
        /// Get jump fatigue <br/><br/>
        /// /characters/{character_id}/fatigue/ <br/><br/>
        /// <c>This route is cached for up to 300 seconds</c>
        /// <br/><c>Requires the following scope: esi-characters.read_fatigue.v1 </c>
        /// </summary>
        /// <returns>Character’s jump activation and fatigue information</returns>
        public async Task<EsiResponse<CharacterFatique>> Fatique()
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/fatigue/").AddUrlSegment("character_id", _validatedToken.CharacterID);

            return await _executor.ExecuteAuthorizatedEndpointAsync<CharacterFatique>(endpointRequest, _validatedToken, Scope.CharactersReadFatigue);
        }
        
        /// <summary>
        /// Get character notifications <br/><br/>
        /// /characters/{character_id}/notifications/ <br/><br/>
        /// <c>This route is cached for up to 600 seconds</c>
        /// <br/><c>Requires the following scope: esi-characters.read_notifications.v1 </c>
        /// </summary>
        /// <returns>Character notifications</returns>
        public async Task<EsiResponse<List<CharacterNotification>>> Notifications()
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/notifications/").AddUrlSegment("character_id", _validatedToken.CharacterID);

            return await _executor.ExecuteAuthorizatedEndpointAsync<List<CharacterNotification>>(endpointRequest, _validatedToken, Scope.CharactersReadNotifications);
        }
        
        /// <summary>
        /// Get new contact notifications <br/><br/>
        /// /characters/{character_id}/notifications/contacts/ <br/><br/>
        /// <c>This route is cached for up to 600 seconds</c>
        /// <br/><c>Requires the following scope: esi-characters.read_notifications.v1 </c>
        /// </summary>
        /// <returns>Notifications about having been added to someone’s contact list</returns>
        public async Task<EsiResponse<List<CharacterContactNotification>>> ContactNotifications()
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/notifications/contacts/").AddUrlSegment("character_id", _validatedToken.CharacterID);

            return await _executor.ExecuteAuthorizatedEndpointAsync<List<CharacterContactNotification>>(endpointRequest, _validatedToken, Scope.CharactersReadNotifications);
        }
        
        /// <summary>
        /// Get character corporation roles <br/><br/>
        /// /characters/{character_id}/roles/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// <br/><c>Requires the following scope: esi-characters.read_corporation_roles.v1 </c>
        /// </summary>
        /// <returns>Character’s corporation roles</returns>
        public async Task<EsiResponse<CharacterRoles>> Roles()
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/roles/").AddUrlSegment("character_id", _validatedToken.CharacterID);

            return await _executor.ExecuteAuthorizatedEndpointAsync<CharacterRoles>(endpointRequest, _validatedToken, Scope.CharactersReadCorporationRoles);
        }
        
        /// <summary>
        /// Get standings <br/><br/>
        /// /characters/{character_id}/standings/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// <br/><c>Requires the following scope: esi-characters.read_standings.v1 </c>
        /// </summary>
        /// <returns>Сharacter standings from agents, NPC corporations, and factions</returns>
        public async Task<EsiResponse<List<CharacterStanding>>> Standings()
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/standings/").AddUrlSegment("character_id", _validatedToken.CharacterID);

            return await _executor.ExecuteAuthorizatedEndpointAsync<List<CharacterStanding>>(endpointRequest, _validatedToken, Scope.CharactersReadStandings);
        }
        
        /// <summary>
        /// Get character corporation titles <br/><br/>
        /// /characters/{character_id}/titles/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// <br/><c>Requires the following scope: esi-characters.read_titles.v1 </c>
        /// </summary>
        /// <returns>character’s corporation titles</returns>
        public async Task<EsiResponse<List<CharacterTitle>>> Titles()
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/titles/").AddUrlSegment("character_id", _validatedToken.CharacterID);

            return await _executor.ExecuteAuthorizatedEndpointAsync<List<CharacterTitle>>(endpointRequest, _validatedToken, Scope.CharactersReadTitles);
        }
    }
}