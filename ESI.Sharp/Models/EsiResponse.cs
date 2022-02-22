using System;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using RestSharp;

namespace ESI.Sharp.Models
{
    public class EsiResponse<T>
    {
        /// <summary>
        /// Request Id
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// Response Status Code
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

        /// <summary>
        /// Exception
        /// </summary>
        public Exception Exception { get; set; }

        internal EsiResponse(RestResponseBase response)
        {
            try
            {
                StatusCode = response.StatusCode;

                if (response.Headers is not null && response.ContentHeaders is not null)
                {
                    var xEsiRequestId = response.Headers.FirstOrDefault(x => x.Name == "X-Esi-Request-Id");
                    var xPages = response.Headers.FirstOrDefault(x => x.Name == "X-Pages");
                    var eTag = response.Headers.FirstOrDefault(x => x.Name == "ETag");
                    var expires = response.ContentHeaders.FirstOrDefault(x => x.Name == "Expires");
                    var lastModified = response.ContentHeaders.FirstOrDefault(x => x.Name == "Last-Modified");
                    var xEsiErrorLimitRemain = response.Headers.FirstOrDefault(x => x.Name == "X-Esi-Error-Limit-Remain");
                    var xEsiErrorLimitError = response.Headers.FirstOrDefault(x => x.Name == "X-Esi-Error-Limit-Reset");

                    if (xEsiRequestId?.Value is not null) RequestId = Guid.Parse((string) xEsiRequestId.Value);
                    if (xPages?.Value is not null) Pages = int.Parse((string) xPages.Value);
                    if (eTag?.Value is not null) ETag = ((string) eTag.Value).Replace("\"", string.Empty);
                    if (expires?.Value is not null) Expires = DateTime.Parse((string) expires.Value).ToUniversalTime();
                    if (lastModified?.Value is not null) LastModified = DateTime.Parse((string) lastModified.Value).ToUniversalTime();
                    if (xEsiErrorLimitRemain?.Value is not null) ErrorLimitRemain = int.Parse((string) xEsiErrorLimitRemain.Value);
                    if (xEsiErrorLimitError?.Value is not null) ErrorLimitReset = int.Parse((string) xEsiErrorLimitError.Value);
                }
                
                Message = response.Content;
                if (response.StatusCode is HttpStatusCode.OK or HttpStatusCode.Created)
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        if (response.Content.StartsWith("{") && response.Content.EndsWith("}") ||
                            response.Content.StartsWith("[") && response.Content.EndsWith("]"))
                            Data = JsonConvert.DeserializeObject<T>(response.Content);
                    }
                }
            }
            catch (Exception ex)
            {
                Message = response.Content;
                Exception = ex;
            }
        }
    }
}