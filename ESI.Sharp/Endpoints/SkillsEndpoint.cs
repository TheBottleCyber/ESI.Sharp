using ESI.Sharp.Models.Authorization;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class SkillsEndpoint : EndpointBase
    {
        public SkillsEndpoint(RestClient restClient, ValidatedToken validatedToken) : base(restClient, validatedToken) { }
    }
}