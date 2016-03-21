namespace PelotonEppSdk.Models
{
    public class CreditCardResponse: Response
    {

    }

    // ReSharper disable InconsistentNaming
    internal class credit_card_response: response
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
    }
}