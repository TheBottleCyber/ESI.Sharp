using System;
using System.Threading.Tasks;
using ESI.Sharp.Helpers;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Endpoints;
using ESI.Sharp.Models.Shared;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RichardSzalay.MockHttp;

namespace ESI.Sharp.Tests.Endpoints
{
    public class StatusEndpointTests
    {
        private EsiClient _esiMockedClient;
        
        public StatusEndpointTests()
        {
            var mockHttp = new MockHttpMessageHandler();
            var config = new EsiConfig("mocked", "mocked", "mocked", "mocked", "http://localhost/api");

            var alliancesJsonString = JsonConvert.SerializeObject(new Status(1000, "1111", DateTime.UtcNow, false));
            mockHttp.When($"{config.EsiEndpoint}/status/")
                    .WithQueryString("datasource", config.EsiSource.ToString())
                    .Respond("application/json", alliancesJsonString);
            
            _esiMockedClient = new EsiClient(config, new RestClientOptions { ConfigureMessageHandler = _ => mockHttp });
        }
        
        [Test]
        public async Task EndpointRetrieveMethod()
        {
            var status = await _esiMockedClient.Status.Retrieve();

            Assert.IsFalse(string.IsNullOrEmpty(status.Data.ServerVersion));
        }
    }
}