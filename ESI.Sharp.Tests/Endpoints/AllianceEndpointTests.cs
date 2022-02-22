using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ESI.Sharp.Helpers;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Endpoints;
using ESI.Sharp.Models.Shared;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RichardSzalay.MockHttp;
using RichardSzalay.MockHttp.Matchers;

namespace ESI.Sharp.Tests.Endpoints
{
    public class AllianceEndpointTests
    {
        private EsiClient _esiMockedClient;

        public AllianceEndpointTests()
        {
            var mockHttp = new MockHttpMessageHandler();
            var config = new EsiConfig("mocked", "mocked", "mocked", "mocked", "http://localhost/api");
            var esiSource = config.EsiSource.ToEsiValue();

            var alliancesJsonString = JsonConvert.SerializeObject(new[] { 99000001, 99000002 });
            mockHttp.When($"{config.EsiEndpoint}/alliances/")
                    .WithQueryString("datasource", esiSource)
                    .Respond("application/json", alliancesJsonString);

            var allianceJsonString = JsonConvert.SerializeObject(new Alliance(45678, 12345, DateTime.UtcNow, 98356193, 0, "C C P Alliance", "C C P"));
            mockHttp.When($"{config.EsiEndpoint}/alliances/434243723/")
                    .WithQueryString("datasource", esiSource)
                    .Respond("application/json", allianceJsonString);

            var corporationsJsonString = JsonConvert.SerializeObject(new[] { 98356193 });
            mockHttp.When($"{config.EsiEndpoint}/alliances/434243723/corporations/")
                    .WithQueryString("datasource", esiSource)
                    .Respond("application/json", corporationsJsonString);

            var imagesJsonString = JsonConvert.SerializeObject(new Images(x128: "url", x64: "url"));
            mockHttp.When($"{config.EsiEndpoint}/alliances/434243723/icons/")
                    .WithQueryString("datasource", esiSource)
                    .Respond("application/json", imagesJsonString);

            _esiMockedClient = new EsiClient(config, new RestClientOptions { ConfigureMessageHandler = _ => mockHttp });
        }

        [TestCase(99000001)]
        [TestCase(99000002)]
        public async Task ExecuteEsiEndpointAll(int allianceId)
        {
            var allAlliances = await _esiMockedClient.Alliance.All();

            Assert.That(allAlliances.Data, Does.Contain(allianceId));
        }

        [TestCase(434243723, "C C P")]
        public async Task ExecuteEsiEndpointInformation(int allianceId, string ticker)
        {
            var allianceInformation = await _esiMockedClient.Alliance.Information(allianceId);

            Assert.AreEqual(allianceInformation.Data.Ticker, ticker);
        }

        [TestCase(434243723, 98356193)]
        public async Task ExecuteEsiEndpointCorporations(int allianceId, int corporationId)
        {
            var allianceCorporations = await _esiMockedClient.Alliance.Corporations(allianceId);

            Assert.That(allianceCorporations.Data, Does.Contain(corporationId));
        }

        [TestCase(434243723)]
        public async Task ExecuteEsiEndpointIcons(int allianceId)
        {
            var allianceIcons = await _esiMockedClient.Alliance.Icons(allianceId);

            Assert.IsTrue(!string.IsNullOrEmpty(allianceIcons.Data.x128));
            Assert.IsTrue(!string.IsNullOrEmpty(allianceIcons.Data.x64));
        }
    }
}