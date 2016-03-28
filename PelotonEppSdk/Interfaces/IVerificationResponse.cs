using System.Diagnostics.CodeAnalysis;

namespace PelotonEppSdk.Models
{
    public interface IVerificationResponse
    {
        string AddressVerificationResult { get; set; }

        string CardSecurityCodeVerificationResult { get; set; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal interface Iverification_response
    {
        /// <summary>
        /// Result of the address verification process
        /// </summary>
        string address_verification_result { get; set; }

        /// <summary>
        /// Result of the card security code verification process
        /// </summary>
        string card_security_code_verification_result { get; set; }
    }
}