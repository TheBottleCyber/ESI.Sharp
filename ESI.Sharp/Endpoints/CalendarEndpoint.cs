using System.Collections.Generic;
using System.Threading.Tasks;
using ESI.Sharp.Helpers;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Authorization;
using ESI.Sharp.Models.Endpoints.Calendar;
using ESI.Sharp.Models.Enumerations.Static;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class CalendarEndpoint : EndpointBase
    {
        public CalendarEndpoint(RestClient restClient, ValidatedToken validatedToken) : base(restClient, validatedToken) { }

        /// <summary>
        /// List calendar event summaries <br/><br/>
        /// /characters/{character_id}/calendar/ <br/><br/>
        /// <c>This route is cached for up to 5 seconds</c>
        /// <br/><c>Requires the following scope: esi-calendar.read_calendar_events.v1 </c>
        /// </summary>  
        /// <returns>Get 50 event summaries from the calendar</returns>
        public async Task<EsiResponse<List<CalendarItem>>> Events()
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/calendar/").AddUrlSegment("character_id", _validatedToken.CharacterID);

            return await ExecuteAuthorizedEndpointAsync<List<CalendarItem>>(endpointRequest, Scope.CalendarReadCalendarEvents);
        }

        /// <summary>
        /// Get an event <br/><br/>
        /// /characters/{character_id}/calendar/{event_id}/ <br/><br/>
        /// <c>This route is cached for up to 5 seconds</c>
        /// <br/><c>Requires the following scope: esi-calendar.read_calendar_events.v1 </c>
        /// </summary>  
        /// <returns>Full details of a specific event</returns>
        public async Task<EsiResponse<List<CalendarItem>>> Events(int eventId)
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/calendar/{event_id}/").AddUrlSegment("character_id", _validatedToken.CharacterID)
                                                                                                    .AddUrlSegment("event_id", eventId);

            return await ExecuteAuthorizedEndpointAsync<List<CalendarItem>>(endpointRequest, Scope.CalendarReadCalendarEvents);
        }

        /// <summary>
        /// Respond to an event <br/><br/>
        /// /characters/{character_id}/calendar/{event_id}/ <br/><br/>
        /// <c>This route is cached for up to 5 seconds</c>
        /// <br/><c>Requires the following scope: esi-calendar.respond_calendar_events.v1 </c>
        /// </summary>  
        /// <returns>Event updated status, blank string</returns>
        public async Task<EsiResponse<string>> Respond(int eventId, EventResponse response)
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/calendar/{event_id}/", Method.Put).AddUrlSegment("character_id", _validatedToken.CharacterID)
                                                                                                                .AddUrlSegment("event_id", eventId)
                                                                                                                .AddQueryParameter("response", response.GetEnumMemberAttribute());

            return await ExecuteAuthorizedEndpointAsync<string>(endpointRequest, Scope.CalendarRespondCalendarEvents);
        }

        /// <summary>
        /// Get attendees <br/><br/>
        /// /characters/{character_id}/calendar/{event_id}/attendees/ <br/><br/>
        /// <c>This route is cached for up to 600 seconds</c>
        /// <br/><c>Requires the following scope: esi-calendar.read_calendar_events.v1 </c>
        /// </summary>  
        /// <returns>Get all invited attendees for a given event</returns>
        public async Task<EsiResponse<CalendarResponse>> Attendees(int eventId)
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/calendar/{event_id}/attendees/").AddUrlSegment("character_id", _validatedToken.CharacterID)
                                                                                                              .AddUrlSegment("event_id", eventId);

            return await ExecuteAuthorizedEndpointAsync<CalendarResponse>(endpointRequest, Scope.CalendarReadCalendarEvents);
        }
    }
}