using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;

namespace PelotonEppSdk.Models
{
    public class TransferRequest : RequestBase
    {
        public string SourceAccountToken { get; set; }
        public string TargetAccountToken { get; set; }
        public string ApplicationName { get; set; }

        public decimal Amount { get; set; }
        public bool AutoAccept { get; set; }
        public string LanguageCode { get; set; }
        public ICollection<Reference> References { get; set; }

        public async Task<Response> PostAsync()
        {
            var client = new PelotonClient();
            var result = await client.PostAsync<response>((transfer_request)this, ApiTarget.Transfers);
            return (Response) result;
        }
       
    }

    internal class transfer_request : request_base
    {
        public string application_name { get; set; }
        public decimal? amount { get; set; }
        public string source_account_token { get; set; }
        public string target_account_token { get; set; }
        public bool? auto_accept { get; set; }
        public string language_code { get; set; }
        public ICollection<reference> references { get; set; }

        public static explicit operator transfer_request(TransferRequest t)
        {
            return new transfer_request
            {
                authentication_header = t.AuthenticationHeader,
                amount = t.Amount,
                application_name = t.ApplicationName,
                auto_accept = t.AutoAccept,
                language_code = t.LanguageCode,
                references = t.References?.Select(r => (reference) r).ToList(),
                source_account_token = t.SourceAccountToken,
                target_account_token = t.TargetAccountToken
            };
        }
    }
}
