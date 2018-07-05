using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Models;

namespace PelotonEppSdk.Classes
{
    internal enum RequestType
    {
        Get,
        Post,
        Put,
        Delete
    }

    internal class PelotonClient
    {
        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is not <c>2XX Success</c>.</exception>
        /// <exception cref="NotImplementedException"><see cref="RequestType"/> GET is not yet supported.</exception>
        private async Task<T> MakeBasicHttpRequest<T>(RequestType type, request_base content, ApiTarget target, string parameter)
        {
            var serializedContent = JsonConvert.SerializeObject(content);
            var stringContent = new StringContent(serializedContent, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                var factory = new UriFactory();
                client.DefaultRequestHeaders.Authorization = content.authentication_header;
                client.BaseAddress = content.base_uri;
                var targetUriPart = factory.GetTargetUriPart(target);
                var targetPath = targetUriPart + parameter;

                HttpResponseMessage httpResponseMessage;
                switch (type)
                {
                    case RequestType.Get:
                        httpResponseMessage = await client.GetAsync(targetPath).ConfigureAwait(false);
                        break;
                    case RequestType.Post:
                        httpResponseMessage = await client.PostAsync(targetPath, stringContent).ConfigureAwait(false);
                        break;
                    case RequestType.Put:
                        httpResponseMessage = await client.PutAsync(targetPath, stringContent).ConfigureAwait(false);
                        break;
                    case RequestType.Delete:
                        httpResponseMessage = await client.DeleteAsync(targetPath).ConfigureAwait(false);
                        break;
                    default:
                        throw new NotImplementedException();
                }
                string stringResult = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!string.IsNullOrEmpty(stringResult))
                {
                    return JsonConvert.DeserializeObject<T>(stringResult);
                }

                // handle server errors
                switch (httpResponseMessage.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        throw new HttpException((int)httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase);
                    case HttpStatusCode.Unauthorized:
                        throw new HttpException((int)httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase);
                    default:
                        throw new HttpException((int)httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase);
                }
            }
        }

        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is not <c>2XX Success</c>.</exception>
        public async Task<T> GetAsync<T>(request_base content, ApiTarget target, string parameter = null)
        {
            return await MakeBasicHttpRequest<T>(RequestType.Get, content, target, parameter).ConfigureAwait(false);
        }

        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is not <c>2XX Success</c>.</exception>
        public async Task<T> PostAsync<T>(request_base content, ApiTarget target, string parameter = null)
        {
            return await MakeBasicHttpRequest<T>(RequestType.Post, content, target, parameter).ConfigureAwait(false);
        }

        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is not <c>2XX Success</c>.</exception>
        public async Task<T> PutAsync<T>(request_base content, ApiTarget target, string parameter = null)
        {
            return await MakeBasicHttpRequest<T>(RequestType.Put, content, target, parameter).ConfigureAwait(false);
        }

        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is not <c>2XX Success</c>.</exception>
        public async Task<T> DeleteAsync<T>(request_base content, ApiTarget target, string parameter = null)
        {
            return await MakeBasicHttpRequest<T>(RequestType.Delete, content, target, parameter).ConfigureAwait(false);
        }
    }
}
