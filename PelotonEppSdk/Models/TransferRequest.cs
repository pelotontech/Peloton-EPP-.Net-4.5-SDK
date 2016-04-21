using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Validations;

namespace PelotonEppSdk.Models
{
    public class TransferRequest : RequestBase
    {
        [Required]
        [StringLength(32, ErrorMessage = "SourceAccountToken must be 32 characters long.", MinimumLength = 32)]
        public string SourceAccountToken { get; set; }

        [Required]
        [StringLength(32,ErrorMessage = "TargetAccountToken must be 32 characters long.", MinimumLength = 32)]
        public string TargetAccountToken { get; set; }

        [Range(0.01,int.MaxValue)]
        [DecimalDollarValue(ErrorMessage = "Amount must be a multiple of 0.01.")]
        public decimal Amount { get; set; }

        public bool AutoAccept { get; set; }
        
        public ICollection<Reference> References { get; set; }

        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is not <c>2XX Success</c>.</exception>
        public async Task<TransactionResponse> PostAsync()
        {
            var client = new PelotonClient();
            var result = await client.PostAsync<transaction_response>((transfer_request)this, ApiTarget.Transfers).ConfigureAwait(false);
            return (TransactionResponse) result;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class transfer_request : request_base
    {
        public decimal? amount { get; set; }
        public string source_account_token { get; set; }
        public string target_account_token { get; set; }
        public bool? auto_accept { get; set; }
        
        public ICollection<reference> references { get; set; }

        public transfer_request(RequestBase requestBase) : base(requestBase) { }

        public static explicit operator transfer_request(TransferRequest transferRequest)
        {
            return new transfer_request(transferRequest)
            {
                authentication_header = transferRequest.AuthenticationHeader,
                amount = transferRequest.Amount,
                application_name = transferRequest.ApplicationName,
                auto_accept = transferRequest.AutoAccept,
                language_code = Enum.GetName(typeof(LanguageCode), transferRequest.LanguageCode),
                references = transferRequest.References?.Select(r => (reference) r).ToList(),
                source_account_token = transferRequest.SourceAccountToken,
                target_account_token = transferRequest.TargetAccountToken
            };
        }
    }
}
