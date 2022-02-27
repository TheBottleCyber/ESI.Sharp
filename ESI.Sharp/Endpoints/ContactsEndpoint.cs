using System.Collections.Generic;
using System.Threading.Tasks;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Authorization;
using ESI.Sharp.Models.Endpoints.Contacts;
using ESI.Sharp.Models.Enumerations.Static;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class ContactsEndpoint : EndpointBase
    {
        public ContactsEndpoint(RestClient restClient, ValidatedToken validatedToken) : base(restClient, validatedToken) { }

        /// <summary>
        /// Get alliance contacts <br/><br/>
        /// /alliances/{alliance_id}/contacts/ <br/><br/>
        /// <c>This route is cached for up to 300 seconds</c>
        /// <br/><c>Requires the following scope: esi-alliances.read_contacts.v1 </c>
        /// </summary>
        /// <returns>Contacts of character alliance</returns>
        public async Task<EsiResponse<List<Contact>>> AllianceContacts()
        {
            var endpointRequest = new RestRequest("/alliances/{alliance_id}/contacts/").AddUrlSegment("alliance_id", _validatedToken.CharacterAllianceId);

            return await ExecuteAuthorizedEndpointAsync<List<Contact>>(endpointRequest, Scope.AlliancesReadContacts);
        }

        /// <summary>
        /// Get alliance contact labels <br/><br/>
        /// /alliances/{alliance_id}/contacts/labels/ <br/><br/>
        /// <c>This route is cached for up to 300 seconds</c>
        /// <br/><c>Requires the following scope: esi-alliances.read_contacts.v1 </c>
        /// </summary>
        /// <returns>Custom labels for an alliance’s contacts</returns>
        public async Task<EsiResponse<List<ContactLabel>>> AllianceLabels()
        {
            var endpointRequest = new RestRequest("/alliances/{alliance_id}/contacts/labels/").AddUrlSegment("alliance_id", _validatedToken.CharacterAllianceId);

            return await ExecuteAuthorizedEndpointAsync<List<ContactLabel>>(endpointRequest, Scope.AlliancesReadContacts);
        }

        /// <summary>
        /// Delete contacts <br/><br/>
        /// /alliances/{alliance_id}/contacts/ <br/><br/>
        /// <c>This route is cached for up to 300 seconds</c>
        /// <br/><c>Requires the following scope: esi-characters.write_contacts.v1 </c>
        /// </summary>
        /// <returns>Bulk delete contacts</returns>
        public async Task<EsiResponse<string>> CharacterDeleteContact(params int[] contactIds)
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/contacts/", Method.Delete).AddUrlSegment("character_id", _validatedToken.CharacterID)
                                                                                                        .AddQueryParameter("contact_ids", string.Join(",", contactIds));

            return await ExecuteAuthorizedEndpointAsync<string>(endpointRequest, Scope.CharactersWriteContacts);
        }

        /// <summary>
        /// Get contacts <br/><br/>
        /// /characters/{character_id}/contacts/ <br/><br/>
        /// <c>This route is cached for up to 300 seconds</c>
        /// <br/><c>Requires the following scope: esi-characters.read_contacts.v1 </c>
        /// </summary>
        /// <returns>Contacts of character</returns>
        public async Task<EsiResponse<List<Contact>>> CharacterContacts(int page = 1)
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/contacts/").AddUrlSegment("character_id", _validatedToken.CharacterID)
                                                                                         .AddQueryParameter("page", page);

            return await ExecuteAuthorizedEndpointAsync<List<Contact>>(endpointRequest, Scope.CharactersReadContacts);
        }

        /// <summary>
        /// Add contacts <br/><br/>
        /// /characters/{character_id}/contacts/ <br/><br/>
        /// <c>This route is not cached</c>
        /// <br/><c>Requires the following scope: esi-characters.write_contacts.v1 </c>
        /// </summary>
        /// <returns>Bulk add contacts with same settings</returns>
        public async Task<EsiResponse<List<int>>> CharacterAddContacts(params int[] contactIds)
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/contacts/", Method.Post).AddUrlSegment("character_id", _validatedToken.CharacterID)
                                                                                                      .AddJsonBody(contactIds);

            return await ExecuteAuthorizedEndpointAsync<List<int>>(endpointRequest, Scope.CharactersWriteContacts);
        }

        /// <summary>
        /// Edit contacts <br/><br/>
        /// /characters/{character_id}/contacts/ <br/><br/>
        /// <c>This route is not cached</c>
        /// <br/><c>Requires the following scope: esi-characters.write_contacts.v1 </c>
        /// </summary>
        /// <returns>Bulk edit contacts with same settings</returns>
        public async Task<EsiResponse<string>> CharacterEditContacts(IEnumerable<int> contactIds, IEnumerable<int> labelIds, float standing, bool watched)
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/contacts/", Method.Put).AddUrlSegment("character_id", _validatedToken.CharacterID)
                                                                                                     .AddQueryParameter("label_ids", string.Join(",", labelIds))
                                                                                                     .AddQueryParameter("standing", standing)
                                                                                                     .AddQueryParameter("watched", watched)
                                                                                                     .AddJsonBody(contactIds);

            return await ExecuteAuthorizedEndpointAsync<string>(endpointRequest, Scope.CharactersWriteContacts);
        }
        
        /// <summary>
        /// Get contact labels <br/><br/>
        /// /characters/{character_id}/contacts/labels/ <br/><br/>
        /// <c>This route is cached for up to 300 seconds</c>
        /// <br/><c>Requires the following scope: esi-characters.read_contacts.v1 </c>
        /// </summary>
        /// <returns>Custom labels for an character’s contacts</returns>
        public async Task<EsiResponse<List<ContactLabel>>> CharacterLabels()
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/contacts/labels/").AddUrlSegment("character_id", _validatedToken.CharacterID);

            return await ExecuteAuthorizedEndpointAsync<List<ContactLabel>>(endpointRequest, Scope.CharactersReadContacts);
        }
        
        /// <summary>
        /// Get corporation contacts <br/><br/>
        /// /corporations/{corporation_id}/contacts/ <br/><br/>
        /// <c>This route is cached for up to 300 seconds</c>
        /// <br/><c>Requires the following scope: esi-corporations.read_contacts.v1 </c>
        /// </summary>
        /// <returns>Contacts of corporation</returns>
        public async Task<EsiResponse<List<Contact>>> CorporationContacts()
        {
            var endpointRequest = new RestRequest("/corporations/{corporation_id}/contacts/").AddUrlSegment("corporation_id", _validatedToken.CharacterCorporationId);

            return await ExecuteAuthorizedEndpointAsync<List<Contact>>(endpointRequest, Scope.CorporationsReadContacts);
        }
        
        /// <summary>
        /// Get corporation contact labels <br/><br/>
        /// /characters/{character_id}/contacts/labels/ <br/><br/>
        /// <c>This route is cached for up to 300 seconds</c>
        /// <br/><c>Requires the following scope: esi-characters.read_contacts.v1 </c>
        /// </summary>
        /// <returns>Custom labels for a corporation’s contacts</returns>
        public async Task<EsiResponse<List<ContactLabel>>> CorporationLabels()
        {
            var endpointRequest = new RestRequest("/corporations/{corporation_id}/contacts/labels/").AddUrlSegment("corporation_id", _validatedToken.CharacterCorporationId);

            return await ExecuteAuthorizedEndpointAsync<List<ContactLabel>>(endpointRequest, Scope.CorporationsReadContacts);
        }
    }
}