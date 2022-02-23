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
            var dataSource = config.EsiSource.ToString().ToLower();
            
            var characterInformation = new CharacterInformation(99004425, DateTime.UtcNow, 2, 98660988, "", "male", "The Bottle", 1, 0.28943491000000005, "<color=red>Director</color>, <color=Green>Diplomat</color>");
            var characterInformationJsonString = JsonConvert.SerializeObject(characterInformation);
            mockHttp.When($"{config.EsiEndpoint}/characters/2117314232/?datasource={dataSource}")
                    .Respond("application/json", characterInformationJsonString);

            var charactersAffiliation = new List<CharacterAffiliation> { new CharacterAffiliation(99004425, 2117314232, 98660988, 0) };
            var charactersAffiliationJsonString = JsonConvert.SerializeObject(charactersAffiliation);
            mockHttp.When($"{config.EsiEndpoint}/characters/affiliation/?datasource={dataSource}")
                    .WithContent("[2117314232]")
                    .Respond("application/json", charactersAffiliationJsonString);

            var characterCorporationHistory = new List<CharacterCorporationHistory> { new CharacterCorporationHistory(98660988, false, 0, DateTime.UtcNow) };
            var characterCorporationHistoryJsonString = JsonConvert.SerializeObject(characterCorporationHistory);
            mockHttp.When($"{config.EsiEndpoint}/characters/2117314232/corporationhistory/?datasource={dataSource}")
                    .Respond("application/json", characterCorporationHistoryJsonString);

            var characterPortrait = new Images { x64 = "x64", x128 = "x128", x256 = "x256", x512 = "x512"};
            var characterPortraitJsonString = JsonConvert.SerializeObject(characterPortrait);
            mockHttp.When($"{config.EsiEndpoint}/characters/2117314232/portrait/?datasource={dataSource}")
                    .Respond("application/json", characterPortraitJsonString);

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
        
        [TestCase(2117314232, "x64", "x128", "x256", "x512")]
        public async Task ExecuteEsiEndpointCorporationHistory(int characterId, string x64, string x128, string x256, string x512)
        {
            var esiResponse = await _esiMockedClient.Character.Portrait(characterId);

            Assert.IsTrue(esiResponse.Data.x64 == x64);
            Assert.IsTrue(esiResponse.Data.x128 == x128);
            Assert.IsTrue(esiResponse.Data.x256 == x256);
            Assert.IsTrue(esiResponse.Data.x512 == x512);
        }
    }
}