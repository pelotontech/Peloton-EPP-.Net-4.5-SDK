namespace PelotonEppSdk.Models
{
    public class CreditCardResponse: Response, IVerificationResponse
    {
        public string CreditCardToken { get; set; }

        public string AddressVerificationResult { get; set; }

        public string CardSecurityCodeVerificationResult { get; set; }
    }

    // ReSharper disable InconsistentNaming
    internal class credit_card_response: response, Iverification_response
    {
        /// <summary>
        /// The credit_card_token assigned to the newly created credit card
        /// </summary>
        public string credit_card_token { get; set; }


        /// <summary>
        /// Result of the address verification process
        /// </summary>
        public string address_verification_result { get; set; }

        /// <summary>
        /// Result of the card security code verification process
        /// </summary>
        public string card_security_code_verification_result { get; set; }

        public static explicit operator CreditCardResponse(credit_card_response r)
        {
            if (r == null) return null;
            return new CreditCardResponse()
            {
                Success = r.success,
                Message = r.message,
                Errors = r.errors,
                MessageCode = r.message_code,
                CreditCardToken = r.credit_card_token,
                AddressVerificationResult = r.address_verification_result,
                CardSecurityCodeVerificationResult = r.card_security_code_verification_result
            };
        }
    }
}