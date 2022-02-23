using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Endpoints;
using ESI.Sharp.Models.Endpoints.Character;
using ESI.Sharp.Models.Shared;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RichardSzalay.MockHttp;

namespace ESI.Sharp.Tests.Endpoints
{
    public class CharacterEndpointTests
    {
        private EsiClient _esiMockedClient;

        public CharacterEndpointTests()
        {
            var mockHttp = new MockHttpMessageHandler();
            var config = new EsiConfig("mocked", "mocked", "mocked", "mocked", "http://localhost/api");

            var characterInformation = new Information(99004425, DateTime.UtcNow, 2, 98660988, "", "male", "The Bottle", 1, 0.28943491000000005, "<color=red>Director</color>, <color=Green>Diplomat</color>");
            var characterInformationJsonString = JsonConvert.SerializeObject(characterInformation);
            mockHttp.When($"{config.EsiEndpoint}/characters/2117314232/")
                    .WithQueryString("datasource", config.EsiSource.ToString())
                    .Respond("application/json", characterInformationJsonString);

            var charactersAffiliation = new List<Affiliation> { new Affiliation(99004425, 2117314232, 98660988, 0) };
            var charactersAffiliationJsonString = JsonConvert.SerializeObject(charactersAffiliation);
            mockHttp.When($"{config.EsiEndpoint}/characters/affiliation/")
                    .WithQueryString("datasource", config.EsiSource.ToString())
                    .WithContent("[2117314232]")
                    .Respond("application/json", charactersAffiliationJsonString);

            var characterCorporationHistory = new List<CorporationHistory> { new CorporationHistory(98660988, false, 0, DateTime.UtcNow) };
            var characterCorporationHistoryJsonString = JsonConvert.SerializeObject(characterCorporationHistory);
            mockHttp.When($"{config.EsiEndpoint}/characters/2117314232/corporationhistory/")
                    .WithQueryString("datasource", config.EsiSource.ToString())
                    .Respond("application/json", characterCorporationHistoryJsonString);

            _esiMockedClient = new EsiClient(config, new RestClientOptions { ConfigureMessageHandler = _ => mockHttp });
        }

        [TestCase(2117314232, "The Bottle")]
        public async Task ExecuteEsiEndpointInformation(int characterId, string name)
        {
            var esiResponse = await _esiMockedClient.Character.Information(characterId);

            Assert.IsTrue(esiResponse.Data.Name == name);
        }

        [TestCase(2117314232, 99004425)]
        public async Task ExecuteEsiEndpointAffiliation(int characterId, int allianceId)
        {
            var esiResponse = await _esiMockedClient.Character.Affiliation(new List<int> { characterId });

            Assert.IsTrue(esiResponse.Data[0].AllianceId == allianceId);
        }
        
        [TestCase(2117314232, 98660988)]
        public async Task ExecuteEsiEndpointCorporationHistory(int characterId, int corporationid)
        {
            var esiResponse = await _esiMockedClient.Character.CorporationHistory(characterId);

            Assert.IsTrue(esiResponse.Data[0].CorporationId == corporationid);
        }
    }
}