using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Interfaces;

namespace PelotonEppSdk.Models
{
    public class BankAccountRequest : RequestBase, IBankAccountCreateRequest, IBankAccountDeleteRequest
    {
        // start of create fields and methods
        [Required]
        public BankAccount BankAccount { get; set; }

        public bool VerifyAccountByDeposit { get; set; }

        public Document Document { get; set; }

        public IEnumerable<Reference> References { get; set; }

        public async Task<BankAccountCreateResponse> PostAsync()
        {
            var client = new PelotonClient();
            var request = (bank_account_request) this;
            var result = await client.PostAsync<bank_account_response>(request, ApiTarget.BankAccounts).ConfigureAwait(false);
            return (BankAccountCreateResponse) result;
        }
        // end of create fields and methods

        // start of delete fields and methods
        public string Token { get; set; }

        public async Task<Response> DeleteAsync()
        {
            var result = await DeleteAsyncBankAccountsV1<response>((bank_account_delete_request)this, ApiTarget.BankAccounts).ConfigureAwait(false);
            return (Response)result;
        }
        // end of delete fields and methods


        // Due to the nature of the BankAccounts Delete method, it must use this special Delete method
        /// <exception cref="HttpException">When status code is not <c>2XX Success</c>.</exception>
        private async Task<T> DeleteAsyncBankAccountsV1<T>(bank_account_delete_request content, ApiTarget target)
        {
            var factory = new UriFactory();
            var serializer = new JavaScriptSerializer();
            var stringContent = new StringContent(content.bank_account_token, Encoding.Default, "application/json");
            string stringResult;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = content.authentication_header;
                client.BaseAddress = content.base_uri;
                var targetUriPart = factory.GetTargetUriPart(target);
                // following snippet gleaned from: http://stackoverflow.com/questions/28054515/how-to-send-delete-with-json-to-the-rest-api-using-httpclient
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Content = stringContent,
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(targetUriPart)
                };
                var httpResponseMessage = await client.SendAsync(request).ConfigureAwait(false);

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

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class bank_account_request : request_base
    {
        /// <summary>
        /// The bank account which is to be created
        /// </summary>
        [Required]
        public bank_account bank_account { get; set; }

        /// <summary>
        /// Set True if you would like to initiate the verification process by issuing one or more deposits totalling less than $1.00
        /// to this Bank Account.
        /// </summary>
        public bool verify_account_by_deposit { get; set; }

        /// <summary>
        /// Data that can be used to verify the account or conditions associated with the account.
        /// Typically a void cheque, PAD agreement or other official bank document.
        /// </summary>
        public document document { get; set; }

        /// <summary>
        /// A list of fields used to pass additional information to record with the transfer request.
        /// </summary>
        public IEnumerable<reference> references { get; set; }

        public bank_account_request(RequestBase requestBase) : base(requestBase) { }

        public static explicit operator bank_account_request(BankAccountRequest bankAccountRequest)
        {
            return new bank_account_request(bankAccountRequest)
            {
                bank_account = (bank_account) bankAccountRequest.BankAccount,
                verify_account_by_deposit = bankAccountRequest.VerifyAccountByDeposit,
                document = (document) bankAccountRequest.Document,
                application_name = bankAccountRequest.ApplicationName,
                references = bankAccountRequest.References?.Select(r => (reference)r),
                authentication_header = bankAccountRequest.AuthenticationHeader,
                language_code = Enum.GetName(typeof(LanguageCode), bankAccountRequest.LanguageCode)
            };
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class bank_account_delete_request : request_base
    {
        /// <summary>
        /// The token for the bank account which is to be deleted
        /// </summary>
        public string bank_account_token { get; set; }

        public bank_account_delete_request(RequestBase requestBase) : base(requestBase) { }

        public static explicit operator bank_account_delete_request(BankAccountRequest bankAccountRequest)
        {
            return new bank_account_delete_request(bankAccountRequest)
            {
                bank_account_token = bankAccountRequest.Token
            };
        }
    }
}