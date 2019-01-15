using System.Net;
using System.Net.Http;

namespace PelotonEppSdk.Classes
{
    internal class HttpException : HttpRequestException
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public HttpStatusCode StatusCode { get; set; }
        public HttpException(HttpStatusCode statusCode, string reasonPhrase): base(reasonPhrase)
        {
            StatusCode = statusCode;
        }
    }
}