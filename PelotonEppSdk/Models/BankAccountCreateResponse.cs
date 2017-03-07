using System.Diagnostics.CodeAnalysis;

namespace PelotonEppSdk.Models
{
    public class BankAccountCreateResponse : Response
    {
        public string BankAccountToken { get; set; }

        internal BankAccountCreateResponse(response r) : base(r)
        {
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class bank_account_response: response
    {
        public string bank_account_token { get; set; }

        public static explicit operator BankAccountCreateResponse(bank_account_response bankAccountResponse)
        {
            if (bankAccountResponse == null) return null;
            return new BankAccountCreateResponse(bankAccountResponse)
            {
                Success = bankAccountResponse.success,
                Message = bankAccountResponse.message,
                Errors = bankAccountResponse.errors,
                MessageCode = bankAccountResponse.message_code,
                BankAccountToken = bankAccountResponse.bank_account_token
            };
        }

        public static explicit operator bank_account_response(BankAccountCreateResponse bankAccountCreateResponse)
        {
            if (bankAccountCreateResponse == null) return null;
            return new bank_account_response
            {
                errors = bankAccountCreateResponse.Errors,
                message = bankAccountCreateResponse.Message,
                message_code = bankAccountCreateResponse.MessageCode,
                success = bankAccountCreateResponse.Success,
                bank_account_token = bankAccountCreateResponse.BankAccountToken
            };
        }
    }
}
