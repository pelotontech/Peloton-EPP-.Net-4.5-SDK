using System.Diagnostics.CodeAnalysis;

namespace PelotonEppSdk.Models
{
    public class CreditCardTransactionResponse: Response, IVerificationResponse
    {
        public string AddressVerificationResult { get; set; }

        public string CardSecurityCodeVerificationResult { get; set; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class credit_card_transaction_response : response, Iverification_response
    {
        /// <summary>
        /// Result of the address verification process
        /// </summary>
        public string address_verification_result { get; set; }

        /// <summary>
        /// Result of the card security code verification process
        /// </summary>
        public string card_security_code_verification_result { get; set; }
    }
}