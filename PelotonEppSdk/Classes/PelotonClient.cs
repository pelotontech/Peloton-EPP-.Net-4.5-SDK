using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
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
        private async Task<T> MakeBasicHttpRequest<T>(RequestType type, request_base content, ApiTarget target, string parameter)
        {
            var factory = new UriFactory();
            var serializer = new JavaScriptSerializer();
            var serializedContent = serializer.Serialize(content);
            var stringContent = new StringContent(serializedContent, Encoding.Default, "application/json");
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = content.authentication_header;
                client.BaseAddress = factory.GetBaseUri();
                var targetUriPart = factory.GetTargetUriPart(target);
                var targetPath = targetUriPart + parameter;

                HttpResponseMessage httpResponseMessage = null;
                switch (type)
                {
                    case RequestType.Get:
                        throw new NotImplementedException();
                    case RequestType.Post:
                        httpResponseMessage = await client.PostAsync(targetPath, stringContent);
                        break;
                    case RequestType.Put:
                        httpResponseMessage = await client.PutAsync(targetPath, stringContent);
                        break;
                    case RequestType.Delete:
                        httpResponseMessage = await client.DeleteAsync(targetPath);
                        break;
                    default:
                        throw new NotImplementedException();
                }
                var stringResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (stringResult != null)
                {
                    return serializer.Deserialize<T>(stringResult);
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

        // <exception cref="HttpException">When status code is not <c>2XX Success</c>.</exception>
        public async Task<T> PostAsync<T>(request_base content, ApiTarget target, string parameter = null)
        {
            return await MakeBasicHttpRequest<T>(RequestType.Post, content, target, parameter);
        }

        // <exception cref="HttpException">When status code is not <c>2XX Success</c>.</exception>
        public async Task<T> PutAsync<T>(request_base content, ApiTarget target, string parameter = null)
        {
            return await MakeBasicHttpRequest<T>(RequestType.Put, content, target, parameter);
        }

        // <exception cref="HttpException">When status code is not <c>2XX Success</c>.</exception>
        public async Task<T> DeleteAsync<T>(request_base content, ApiTarget target, string parameter = null)
        {
            return await MakeBasicHttpRequest<T>(RequestType.Delete, content, target, parameter);
        }

        // Due to the nature of the BankAccounts Delete method, it must use this special Delete method
        // <exception cref="HttpException">When status code is not <c>2XX Success</c>.</exception>
        public async Task<T> DeleteAsyncBankAccountsV1<T>(bank_account_delete_request content, ApiTarget target)
        {
            var factory = new UriFactory();
            var serializer = new JavaScriptSerializer();
            var stringContent = new StringContent(content.bank_account_token, Encoding.Default, "application/json");
            string stringResult;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = content.authentication_header;
                client.BaseAddress = factory.GetBaseUri();
                var targetUriPart = factory.GetTargetUriPart(target);
                // following snippet gleaned from: http://stackoverflow.com/questions/28054515/how-to-send-delete-with-json-to-the-rest-api-using-httpclient
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Content = stringContent,
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(targetUriPart)
                };
                var httpResponseMessage = await client.SendAsync(request);

                // handle server errors
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    throw new HttpException((int)httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase);
                }
                stringResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
            }
            return serializer.Deserialize<T>(stringResult);
        }
    }
}
