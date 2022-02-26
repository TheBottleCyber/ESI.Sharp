using ESI.Sharp.Models.Authorization;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class CalendarEndpoint : EndpointBase
    {
        public CalendarEndpoint(RestClient restClient, ValidatedToken validatedToken) : base(restClient, validatedToken) { }
    }
}