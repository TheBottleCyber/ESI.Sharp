using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Endpoints.Character;
using ESI.Sharp.Models.Endpoints.Contracts;
using ESI.Sharp.Models.Enumerations;
using ESI.Sharp.Models.Shared;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RichardSzalay.MockHttp;

namespace ESI.Sharp.Tests.Endpoints
{
    public class ContractsEndpointTests
    {
        private EsiClient _esiMockedClient;

        public ContractsEndpointTests()
        {
            var mockHttp = new MockHttpMessageHandler();
            var config = new EsiConfig("mocked", "mocked", "mocked", "mocked", "http://localhost/api");
            var dataSource = config.EsiSource.ToString().ToLower();
            
            var publicContracts = new List<Contract>
            {
                new Contract
                {
                    Buyout = 10000000000.01,
                    ContractId = 1,
                    DateExpired = DateTime.UtcNow,
                    DateIssued = DateTime.UtcNow,
                    DaysToComplete = 1,
                    EndLocationId = 60014719,
                    ForCorporation = true,
                    IssuerCorporationId = 456,
                    IssuerId = 123,
                    Price = 1000000.01,
                    Reward = 0.01,
                    StartLocationId = 60014719,
                    Type = ContractType.Auction,
                    Volume = 0.01
                }
            };
            
            var publicContractsJsonString = JsonConvert.SerializeObject(publicContracts);
            mockHttp.When($"{config.EsiEndpoint}/contracts/public/10000060/?page=1&datasource={dataSource}")
                    .Respond("application/json", publicContractsJsonString);

            var contractItems = new List<ContractItem>
            {
                new ContractItem
                {
                    IsIncluded = true,
                    ItemId = 1111,
                    Quantity = 1,
                    RecordId = 123456,
                    TypeId = 789
                }
            };
            
            var contractItemsJsonString = JsonConvert.SerializeObject(contractItems);
            mockHttp.When($"{config.EsiEndpoint}/contracts/public/items/1111/?datasource={dataSource}")
                    .Respond("application/json", contractItemsJsonString);
            
            var contractBids = new List<ContractBid> { new ContractBid(1, 1, DateTime.UtcNow, 1.23) };
            var contractBidsJsonString = JsonConvert.SerializeObject(contractBids);
            mockHttp.When($"{config.EsiEndpoint}/contracts/public/bids/1111/?datasource={dataSource}")
                    .Respond("application/json", contractBidsJsonString);

            _esiMockedClient = new EsiClient(config, new RestClientOptions { ConfigureMessageHandler = _ => mockHttp });
        }
        
        [TestCase(Region.Delve, 10000060, 123)]
        public async Task ExecuteEsiEndpointContracts(Region region, int regionId, int issuerId)
        {
            var esiResponseEnum = await _esiMockedClient.Contracts.Contracts(region);
            var esiResponseId = await _esiMockedClient.Contracts.Contracts(regionId);

            Assert.IsTrue(esiResponseEnum.Data[0].IssuerId == issuerId);
            Assert.IsTrue(esiResponseId.Data[0].IssuerId == issuerId);
        }
        
        [TestCase(1111, 1)]
        public async Task ExecuteEsiEndpointItems(int contractId, int quantity)
        {
            var esiResponseEnum = await _esiMockedClient.Contracts.ContractItems(contractId);

            Assert.IsTrue(esiResponseEnum.Data[0].Quantity == quantity);
        }
        
        [TestCase(1111, 1.23)]
        public async Task ExecuteEsiEndpointBids(int contractId, double amount)
        {
            var esiResponseEnum = await _esiMockedClient.Contracts.ContractBids(contractId);

            Assert.AreEqual(esiResponseEnum.Data[0].Amount, amount);
        }
    }
}