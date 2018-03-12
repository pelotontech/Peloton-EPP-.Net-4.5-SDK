using System.Diagnostics.CodeAnalysis;
using PelotonEppSdk.Interfaces;

namespace PelotonEppSdk.Models
{
    public class CreditCardResponse : Response, IVerificationResponse
    {
        /// <summary>
        /// The credit card token assigned to the newly created credit card
        /// </summary>
        public string CreditCardToken { get; set; }

        /// <summary>
        /// Result of the address verification process
        /// </summary>
        public string AddressVerificationResult { get; set; }

        /// <summary>
        /// Result of the card security code verification process
        /// </summary>
        public string CardSecurityCodeVerificationResult { get; set; }

        // ReSharper disable once UnusedMember.Global
        public CreditCardResponse()
        {
        }

        internal CreditCardResponse(response r) : base(r)
        {
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class credit_card_response : response, Iverification_response
    {
        public string credit_card_token { get; set; }

        public string address_verification_result { get; set; }

        public string card_security_code_verification_result { get; set; }

        public static explicit operator CreditCardResponse(credit_card_response creditCardResponse)
        {
            if (creditCardResponse == null) return null;
            return new CreditCardResponse(creditCardResponse)
            {
                CreditCardToken = creditCardResponse.credit_card_token,
                AddressVerificationResult = creditCardResponse.address_verification_result,
                CardSecurityCodeVerificationResult = creditCardResponse.card_security_code_verification_result
            };
        }
    }
}