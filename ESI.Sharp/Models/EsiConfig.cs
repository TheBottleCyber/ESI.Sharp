using System;
using System.Runtime.Serialization;

namespace ESI.Sharp.Models
{
    /// <summary>
    /// Specifies which server to call the endpoints from
    /// </summary>
    public enum EsiSource
    {
        /// <summary>
        /// EVE Online test server
        /// </summary>
        [EnumMember(Value = "singularity")] Singularity,
        /// <summary>
        /// EVE Online default server
        /// </summary>
        [EnumMember(Value = "tranquility")] Tranquility,
        /// <summary>
        /// EVE Online special server for China players
        /// </summary>
        [EnumMember(Value = "serenity")] Serenity
    }

    /// <summary>
    /// Used to configure <see cref="EsiClient"/>
    /// </summary>
    public class EsiConfig
    {
        private string _clientId = string.Empty;
        private string _secretKey = string.Empty;
        private string _callbackUrl = string.Empty;
        private string _userAgent = string.Empty;
        private string _esiEndpoint = "https://esi.evetech.net/latest/";

        /// <summary>
        /// Client Id property from ESI application authentication settings
        /// </summary>
        /// <exception cref="ArgumentException">throws when trying set field null or empty</exception>
        public string ClientId
        {
            get => _clientId;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("EsiConfig constructor parameter cannot be null or empty", nameof(ClientId));

                _clientId = value;
            }
        }

        /// <summary>
        /// Secret Key property from ESI application authentication settings
        /// </summary>
        /// <exception cref="ArgumentException">throws when trying set field null or empty</exception>
        public string SecretKey 
        {
            get => _secretKey;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("EsiConfig constructor parameter cannot be null or empty", nameof(SecretKey));
                
                _secretKey = value;
            }
        }

        /// <summary>
        /// Callback Url property from ESI application authentication settings
        /// </summary>
        /// <exception cref="ArgumentException">throws when trying set field null or empty</exception>
        public string CallbackUrl
        {
            get => _callbackUrl;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("EsiConfig constructor parameter cannot be null or empty", nameof(CallbackUrl));
                
                _callbackUrl = value;
            }
        }

        /// <summary>
        /// UserAgent property needs to be setted so that the developers of eve online know what kind of application it is
        /// </summary>
        /// <exception cref="ArgumentException">throws when trying set field null or empty</exception>
        public string UserAgent
        {
            get => _userAgent;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("EsiConfig constructor parameter cannot be null or empty", nameof(UserAgent));
                
                _userAgent = value;
            }
        }

        /// <summary>
        /// Endpoint to ESI api definition, default is https://esi.evetech.net/
        /// </summary>
        /// <exception cref="ArgumentException">throws when trying set field null or empty</exception>
        public string EsiEndpoint
        {
            get => _esiEndpoint;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("EsiConfig constructor parameter cannot be null or empty", nameof(EsiEndpoint));
                
                _esiEndpoint = value;
            }
        }
        
        /// <summary>
        /// Specifies which server to call the endpoints from, default is Tranquility
        /// </summary>
        /// <exception cref="ArgumentException">throws when trying set field null or empty</exception>
        public EsiSource EsiSource { get; set; } = EsiSource.Tranquility;

        /// <summary>
        /// Initialize EsiConfig for using it in EsiClient
        /// </summary>
        public EsiConfig() { }

        /// <summary>
        /// Initialize EsiConfig for using it in EsiClient
        /// </summary>
        /// <param name="clientId"><see cref="ClientId"/> property from ESI application authentication settings</param>
        /// <param name="secretKey"><see cref="SecretKey"/> property from ESI application authentication settings</param>
        /// <param name="callbackUrl"><see cref="CallbackUrl"/> property from ESI application authentication settings</param>
        /// <param name="userAgent"><see cref="UserAgent"/> property needs to be setted so that the developers of eve online know what kind of application it is</param>
        /// <param name="esiEndpoint"><see cref="EsiEndpoint"/> property is endpoint to ESI api definition, default is https://esi.evetech.net/</param>
        /// <param name="esiSource"><see cref="EsiSource"/> property specifies which server to call the endpoints from, default is Tranquility</param>
        public EsiConfig(string clientId, string secretKey, string callbackUrl, string userAgent, 
            string esiEndpoint = "https://esi.evetech.net/latest/", EsiSource esiSource = EsiSource.Tranquility)
        {
            ClientId = clientId;
            SecretKey = secretKey;
            CallbackUrl = callbackUrl;
            UserAgent = userAgent;
            EsiEndpoint = esiEndpoint;
            EsiSource = esiSource;
        }
    }
}