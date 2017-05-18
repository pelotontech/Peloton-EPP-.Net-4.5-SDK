using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Interfaces;

namespace PelotonEppSdk.Models
{
    public class ClientAuthTokenRequest: RequestBase, IClientAuthTokenCreateRequest, IClientAuthTokenStatusRequest
    {
        /// <summary>
        /// The Client Authorization Token. Required for POST requests.
        /// </summary>
        [StringLength(32, MinimumLength = 32, ErrorMessage = "ClientAuthToken must be 32 characters in length.")]
        public string ClientAuthToken { get; set; }

        /// <summary>
        /// The token used to identify the Peloton account
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 32, ErrorMessage = "AccountToken must be 32 characters in length.")]
        public string AccountToken { get; set; }

        /// <summary>
        /// The URL used for redirection, when provided.
        /// </summary>
        [StringLength(2048)]
        public string ReturnUrl { get; set; }

        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is not <c>2XX Success</c>.</exception>
        public async Task<ClientAuthTokenResponse> GetAsync()
        {
            var client = new PelotonClient();
            var parameter = ClientAuthToken;
            parameter += "?languageCode=" + LanguageCode;
            var result = await client.GetAsync<client_auth_token_status_response>((client_auth_token_request)this, ApiTarget.ClientAuthTokens, parameter).ConfigureAwait(false);
            return (ClientAuthTokenResponse)result;
        }

        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is not <c>2XX Success</c>.</exception>
        public async Task<ClientAuthTokenResponse> PostAsync()
        {
            var client = new PelotonClient();
            var request = (client_auth_token_request) this;
            var result = await client.PostAsync<client_auth_token_response>(request, ApiTarget.ClientAuthTokens).ConfigureAwait(false);
            return (ClientAuthTokenResponse) result;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class client_auth_token_request : request_base
    {
        public string account_token { get; set; }

        public string return_url { get; set; }

        public client_auth_token_request(RequestBase requestBase) : base(requestBase) { }

        public static explicit operator client_auth_token_request(ClientAuthTokenRequest clientAuthTokenRequest)
        {
            return new client_auth_token_request(clientAuthTokenRequest)
            {
                account_token = clientAuthTokenRequest.AccountToken,
                return_url = clientAuthTokenRequest.ReturnUrl
            };
        }
    }
}