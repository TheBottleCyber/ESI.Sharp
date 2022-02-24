using System;
using ESI.Sharp.Endpoints;
using ESI.Sharp.Helpers;
using ESI.Sharp.Models;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace ESI.Sharp
{
    public class EsiClient
    {
        /// <summary>
        /// SSO Authorization
        /// </summary>
        public Authorization Authorization { get; set; }
        
        /// <summary>
        /// Alliance endpoint /alliances/
        /// </summary>
        public AllianceEndpoint Alliance { get; set; }

        /// <summary>
        /// Status endpoint /status/
        /// </summary>
        public StatusEndpoint Status { get; set; }

        /// <summary>
        /// Character endpoint /characters/
        /// </summary>
        public CharacterEndpoint Character { get; set; }

        /// <summary>
        /// Contracts endpoint /contracts/
        /// </summary>
        public ContractsEndpoint Contracts { get; set; }

        /// <summary>
        /// Initialize ESI api client by <see cref="EsiConfig"/>
        /// </summary>
        /// <param name="esiConfig">EsiConfig object</param>
        /// <exception cref="ArgumentNullException">throws when trying set <see cref="EsiConfig"/> parameter null</exception>
        public EsiClient(EsiConfig esiConfig) : this(esiConfig, new RestClientOptions()) { }

        /// <summary>
        /// Initialize ESI api client by <see cref="EsiConfig"/> and <see cref="RestClientOptions"/>
        /// </summary>
        /// <param name="esiConfig">EsiConfig object</param>
        /// <param name="restClientOptions">RestClientOptions object</param>
        /// <exception cref="ArgumentNullException">throws when trying set parameters null</exception>
        public EsiClient(EsiConfig esiConfig, RestClientOptions restClientOptions)
        {
            if (esiConfig == null)
                throw new ArgumentNullException(nameof(esiConfig), "EsiClient constructor parameter cannot be null");

            if (restClientOptions == null)
                throw new ArgumentNullException(nameof(restClientOptions), "EsiClient constructor parameter cannot be null");

            restClientOptions.BaseUrl = new Uri(esiConfig.EsiEndpoint);
            restClientOptions.UserAgent = esiConfig.UserAgent;

            var restClient = new RestClient(restClientOptions).AddDefaultHeader(KnownHeaders.Accept, "application/json")
                                                              .AddDefaultHeader("Cache-Control", "no-cache")
                                                              .UseNewtonsoftJson()
                                                              .AddDefaultQueryParameter("datasource", esiConfig.EsiSource.ToString().ToLower());

            var endpointExecutor = new EndpointExecutor(restClient);

            Authorization = new Authorization(esiConfig);
            Alliance = new AllianceEndpoint(endpointExecutor);
            Status = new StatusEndpoint(endpointExecutor);
            Character = new CharacterEndpoint(endpointExecutor);
            Contracts = new ContractsEndpoint(endpointExecutor);
        }
    }
}