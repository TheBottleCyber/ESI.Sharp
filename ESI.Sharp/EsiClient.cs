using System;
using ESI.Sharp.Endpoints;
using ESI.Sharp.Helpers;
using ESI.Sharp.Models;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace ESI.Sharp
{
    public class EsiClient
    {
        private readonly RestClient _client;
        private readonly EsiConfig _esiConfig;

        /// <summary>
        /// Alliance endpoint /alliances/
        /// </summary>
        public AllianceEndpoint Alliance { get; set; }
        
        /// <summary>
        /// Status endpoint /status/
        /// </summary>
        public StatusEndpoint Status { get; set; }

        /// <summary>
        /// Initialize ESI api client by <see cref="EsiConfig"/>
        /// </summary>
        /// <param name="esiConfig">EsiConfig object</param>
        /// <exception cref="ArgumentNullException">throws when trying set <see cref="EsiConfig"/> parameter null</exception>
        public EsiClient(EsiConfig esiConfig)
        {
            _esiConfig = esiConfig ?? throw new ArgumentNullException(nameof(esiConfig), "EsiClient constructor parameter cannot be null");

            var options = new RestClientOptions(esiConfig.EsiEndpoint)
            {
                Timeout = 5000,
                UserAgent = esiConfig.UserAgent,
            };

            _client = new RestClient(options).AddDefaultHeader(KnownHeaders.Accept, "application/json")
                                             .AddDefaultHeader("Cache-Control", "no-cache")
                                             .UseNewtonsoftJson()
                                             .AddDefaultQueryParameter("datasource", _esiConfig.EsiSource.ToEsiValue());
            
            Alliance = new AllianceEndpoint(_client);
            Status = new StatusEndpoint(_client);
        }
    }
}