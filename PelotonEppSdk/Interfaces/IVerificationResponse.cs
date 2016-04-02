using System.Diagnostics.CodeAnalysis;

namespace PelotonEppSdk.Models
{
    public interface IVerificationResponse
    {
        /// <summary>
        /// Result of the address verification process
        /// </summary>
        string AddressVerificationResult { get; set; }

        /// <summary>
        /// Result of the card security code verification process
        /// </summary>
        string CardSecurityCodeVerificationResult { get; set; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal interface Iverification_response
    {
        string address_verification_result { get; set; }

        string card_security_code_verification_result { get; set; }
    }
}