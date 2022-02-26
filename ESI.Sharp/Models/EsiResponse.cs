using System;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace ESI.Sharp.Models
{
    public class EsiResponse<T>
    {
        /// <summary>
        /// Request id
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// Response status code
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// RFC7231 formatted datetime string
        /// </summary>
        public DateTime? Expires { get; set; }

        /// <summary>
        /// RFC7231 formatted datetime string
        /// </summary>
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// RFC7232 compliant entity tag
        /// </summary>
        public string ETag { get; set; }

        /// <summary>
        /// Keep in mind some endpoints have additional rate limits imposed by game design. If you are limited by the game on some action, ESI will have the same restrictions
        /// </summary>
        public int? ErrorLimitRemain { get; set; }

        /// <summary>
        /// Keep in mind some endpoints have additional rate limits imposed by game design. If you are limited by the game on some action, ESI will have the same restrictions
        /// </summary>
        public int? ErrorLimitReset { get; set; }

        /// <summary>
        /// Maximum page number
        /// </summary>
        public int? Pages { get; set; }

        /// <summary>
        /// Naked request response
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Deserialized json response
        /// </summary>
        public T Data { get; set; }

        internal EsiResponse(RestResponseBase response)
        {
            StatusCode = response.StatusCode;

            if (response.Headers is not null && response.ContentHeaders is not null)
            {
                foreach (var responseHeader in response.Headers.Concat(response.ContentHeaders))
                {
                    ParseEsiResponseHeaders(responseHeader);
                }
            }

            if (response.IsSuccessful)
            {
                if (ValidateJSON(response.Content))
                    Data = JsonConvert.DeserializeObject<T>(response.Content);
            }
            
            Message = response.Content;
        }

        public void ParseEsiResponseHeaders(HeaderParameter responseHeader)
        {
            if (responseHeader.Value is not null)
            {
                switch (responseHeader.Name)
                {
                    case "X-Esi-Request-Id":
                        RequestId = Guid.Parse((string) responseHeader.Value);
                        break;
                    case "X-Pages":
                        Pages = int.Parse((string) responseHeader.Value);
                        break;
                    case "ETag":
                        ETag = ((string) responseHeader.Value).Replace("\"", string.Empty);
                        break;
                    case "X-Esi-Error-Limit-Remain":
                        ErrorLimitRemain = int.Parse((string) responseHeader.Value);
                        break;
                    case "X-Esi-Error-Limit-Reset":
                        ErrorLimitReset = int.Parse((string) responseHeader.Value);
                        break;
                    case "Expires":
                        Expires = DateTime.Parse((string) responseHeader.Value).ToUniversalTime();
                        break;
                    case "Last-Modified":
                        LastModified = DateTime.Parse((string) responseHeader.Value).ToUniversalTime();
                        break;
                }
            }
        }

        public static bool ValidateJSON(string s)
        {
            try
            {
                JToken.Parse(s);

                return true;
            }
            catch { }

            return false;
        }
    }
}