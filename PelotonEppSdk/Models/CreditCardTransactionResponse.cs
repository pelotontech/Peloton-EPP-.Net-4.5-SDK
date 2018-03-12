using System.Diagnostics.CodeAnalysis;
using PelotonEppSdk.Interfaces;

namespace PelotonEppSdk.Models
{
    public class CreditCardTransactionResponse : TransactionResponse, IVerificationResponse
    {
        /// <summary>
        /// Result of the address verification process
        /// </summary>
        public string AddressVerificationResult { get; set; }

        /// <summary>
        /// Result of the card security code verification process
        /// </summary>
        public string CardSecurityCodeVerificationResult { get; set; }

        // ReSharper disable once UnusedMember.Global
        public CreditCardTransactionResponse()
        {
        }

        internal CreditCardTransactionResponse(response r) : base(r)
        {
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class credit_card_transaction_response : transaction_response, Iverification_response
    {
        public string address_verification_result { get; set; }

        public string card_security_code_verification_result { get; set; }

        public static explicit operator CreditCardTransactionResponse(credit_card_transaction_response creditCardTransactionResponse)
        {
            if (creditCardTransactionResponse == null) return null;
            return new CreditCardTransactionResponse(creditCardTransactionResponse)
            {
                AddressVerificationResult = creditCardTransactionResponse.address_verification_result,
                CardSecurityCodeVerificationResult = creditCardTransactionResponse.card_security_code_verification_result,
                TransactionRefCode = creditCardTransactionResponse.transaction_ref_code
            };
        }
    }
}