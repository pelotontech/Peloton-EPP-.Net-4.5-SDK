using System.Diagnostics.CodeAnalysis;

namespace PelotonEppSdk.Models
{
    public class BankAccountCreateResponse : Response
    {
        public string BankAccountToken { get; set; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class bank_account_response: response
    {
        public string bank_account_token { get; set; }

        public static explicit operator BankAccountCreateResponse(bank_account_response r)
        {
            if (r == null) return null;
            return new BankAccountCreateResponse
            {
                Success = r.success,
                Message = r.message,
                Errors = r.errors,
                MessageCode = r.message_code,
                BankAccountToken = r.bank_account_token
            };
        }

        public static explicit operator bank_account_response(BankAccountCreateResponse r)
        {
            if (r == null) return null;
            return new bank_account_response
            {
                errors = r.Errors,
                message = r.Message,
                message_code = r.MessageCode,
                success = r.Success,
                bank_account_token = r.BankAccountToken
            };
        }
    }
}
