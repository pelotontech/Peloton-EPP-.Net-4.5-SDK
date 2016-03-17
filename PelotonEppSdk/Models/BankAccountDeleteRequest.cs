using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;

namespace PelotonEppSdk.Models
{
    public class BankAccountDeleteRequest : RequestBase
    {
        public string Token { get; set; }

        public async Task<Response> DeleteAsync()
        {
            var client = new PelotonClient();
            var result = await client.DeleteAsyncBankAccountsV1<response>((bank_account_delete_request)this, ApiTarget.FundsTransfers);
            return (Response)result;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class bank_account_delete_request : request_base
    {
        public string bank_account_token { get; set; }

        public static explicit operator bank_account_delete_request(BankAccountDeleteRequest bankAccountCreateRequest)
        {
            return new bank_account_delete_request
            {
                bank_account_token = bankAccountCreateRequest.Token
            };
        }
    }
}
