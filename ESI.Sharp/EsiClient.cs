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
        private string _eTag = string.Empty;

        /// <summary>
        /// Alliance endpoint /alliances/
        /// </summary>
        public AllianceEndpoint Alliance { get; set; }

        /// <summary>
        /// Status endpoint /status/
        /// </summary>
        public StatusEndpoint Status { get; set; }

        /// <summary>
        /// ETag from a previous request. A 304 will be returned if this matches the current ETag. Can be empty string
        /// </summary>
        public string ETag
        {
            get => _eTag;
            set
            {
                if (value == null)
                    throw new ArgumentException("ETag cannot be null", nameof(ETag));

                _eTag = value;
            }
        }

        /// <summary>
        /// Initialize ESI api client by <see cref="EsiConfig"/>
        /// </summary>
        /// <param name="esiConfig">EsiConfig object</param>
        /// <exception cref="ArgumentNullException">throws when trying set <see cref="EsiConfig"/> parameter null</exception>
        public EsiClient(EsiConfig esiConfig)
        {
            var config = esiConfig ?? throw new ArgumentNullException(nameof(esiConfig), "EsiClient constructor parameter cannot be null");

            var options = new RestClientOptions(esiConfig.EsiEndpoint)
            {
                Timeout = 5000,
                UserAgent = esiConfig.UserAgent,
            };

            var restClient = new RestClient(options).AddDefaultHeader(KnownHeaders.Accept, "application/json")
                                                    .AddDefaultHeader("Cache-Control", "no-cache")
                                                    .UseNewtonsoftJson()
                                                    .AddDefaultQueryParameter("datasource", config.EsiSource.ToEsiValue());

            var executor = new EsiEndpointExecutor(restClient, this);

            Alliance = new AllianceEndpoint(executor);
            Status = new StatusEndpoint(executor);
        }
    }
}