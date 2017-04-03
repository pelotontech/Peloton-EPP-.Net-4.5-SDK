using System.Diagnostics.CodeAnalysis;

namespace PelotonEppSdk.Models
{
    public class BankAccountResponse : Response
    {
        public string BankAccountToken { get; set; }

        internal BankAccountResponse(response r) : base(r)
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

        public static explicit operator BankAccountResponse(bank_account_response bankAccountResponse)
        {
            if (bankAccountResponse == null) return null;
            return new BankAccountResponse(bankAccountResponse)
            {
                Success = bankAccountResponse.success,
                Message = bankAccountResponse.message,
                Errors = bankAccountResponse.errors,
                MessageCode = bankAccountResponse.message_code,
                BankAccountToken = bankAccountResponse.bank_account_token
            };
        }

        public static explicit operator bank_account_response(BankAccountResponse bankAccountResponse)
        {
            if (bankAccountResponse == null) return null;
            return new bank_account_response
            {
                errors = bankAccountResponse.Errors,
                message = bankAccountResponse.Message,
                message_code = bankAccountResponse.MessageCode,
                success = bankAccountResponse.Success,
                bank_account_token = bankAccountResponse.BankAccountToken
            };
        }
    }
}
