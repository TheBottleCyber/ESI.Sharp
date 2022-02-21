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
                    if (xEsiRequestId?.Value is not null)
                    {
                        RequestId = Guid.Parse((string) xEsiRequestId.Value);
                    }

                    var xPages = response.Headers.FirstOrDefault(x => x.Name == "X-Pages");
                    if (xPages?.Value is not null)
                    {
                        Pages = int.Parse((string) xPages.Value);
                    }
                    
                    var eTag = response.Headers.FirstOrDefault(x => x.Name == "ETag");
                    if (eTag?.Value is not null)
                    {
                        ETag = ((string) eTag.Value).Replace("\"", string.Empty);
                    }
                    
                    var expires = response.ContentHeaders.FirstOrDefault(x => x.Name == "Expires");
                    if (expires?.Value is not null)
                    {
                        Expires = DateTime.Parse((string) expires.Value).ToUniversalTime();
                    }
                    
                    var lastModified = response.ContentHeaders.FirstOrDefault(x => x.Name == "Last-Modified");
                    if (lastModified?.Value is not null)
                    {
                        LastModified = DateTime.Parse((string) lastModified.Value).ToUniversalTime();
                    }
                    
                    var xEsiErrorLimitRemain = response.Headers.FirstOrDefault(x => x.Name == "X-Esi-Error-Limit-Remain");
                    if (xEsiErrorLimitRemain?.Value is not null)
                    {
                        ErrorLimitRemain = int.Parse((string) xEsiErrorLimitRemain.Value);
                    }
                    
                    var xEsiErrorLimitError = response.Headers.FirstOrDefault(x => x.Name == "X-Esi-Error-Limit-Reset");
                    if (xEsiErrorLimitError?.Value is not null)
                    {
                        ErrorLimitReset = int.Parse((string) xEsiErrorLimitError.Value);
                    }
                }

                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    var result = response.Content;

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.Created:
                        {
                            if (!string.IsNullOrEmpty(result))
                            {
                                if (result.StartsWith("{") && result.EndsWith("}") || result.StartsWith("[") && result.EndsWith("]"))
                                    Data = JsonConvert.DeserializeObject<T>(result);
                                else
                                    Message = result;
                            }

                            break;
                        }
                        case HttpStatusCode.NotModified:
                            Message = "Not Modified";
                            break;
                        default:
                        {
                            if (!string.IsNullOrEmpty(result))
                            {
                                Message = JsonConvert.DeserializeAnonymousType(result, new { error = string.Empty }).error;
                            }
                            
                            break;
                        }
                    }
                }
                else if (response.StatusCode == HttpStatusCode.NoContent)
                    Message = "No Content";
            }
            catch (Exception ex)
            {
                Message = response.Content;
                Exception = ex;
            }
        }
    }
}