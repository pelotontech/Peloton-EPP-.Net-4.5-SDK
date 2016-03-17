using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;

namespace PelotonEppSdk.Models
{
    public class CreditCardRequest : RequestBase
    {
        public string OrderNumber { get; set; }
        public string CardOwner { get; set; }

        public CreditCardNumber CardNumber { get; set; }

        public string CardType => CardNumber?.CardName;
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string CardSecurityCode { get; set; }
        public decimal? CardLast4Digits => CardNumber.Last4Digits;
        public string MaskedCardDigits => CardNumber.Masked;

        public IEnumerable<Reference> References { get; set; }

        public string BillingName { get; set; }
        public string BillingPhone { get; set; }
        public string BillingEmail { get; set; }
        public Address BillingAddress { get; set; }

        public bool Verify { get; set; }

        public async Task<Response> PostAsync()
        {
            var client = new PelotonClient();
            var request = (credit_card_request) this;
            var result = await client.PostAsync<bank_account_response>(request, ApiTarget.BankAccounts);
            return (BankAccountCreateResponse) result;
        }

    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class credit_card_request : request_base
    {
        /// <summary>
        /// Recommended: Order number provided by the source system, otherwise one will be automatically generated
        /// </summary>
        [StringLength(50)]
        public string order_number { get; set; }

        /// <summary>
        /// The name of the card owner as it appears on the credit card
        /// </summary>
        [StringLength(26, MinimumLength = 2)]
        public string name_on_card { get; set; }

        /// <summary>
        /// The credit card number as it appears on the card. Not applicable for updates.
        /// </summary>
        public long? card_number { get; set; }

        /// <summary>
        /// The credit card expiration month in two-digit format (e.g. 09 for September)
        /// </summary>
        [StringLength(2, MinimumLength = 2)]
        public string expiry_month { get; set; }

        /// <summary>
        /// The credit card expiration year in two-digit format (e.g. 08 for 2008)
        /// </summary>
        [StringLength(2, MinimumLength = 2)]
        public string expiry_year { get; set; }

        /// <summary>
        /// The 3 or 4 digit CVV2/CVC2/CID. Not applicable for updates.
        /// </summary>
        [StringLength(4, MinimumLength = 3)]
        public string card_security_code { get; set; }

        /// <summary>
        /// The primary billing contact name
        /// </summary>
        public string billing_name { get; set; }

        /// <summary>
        /// The email address for the primary billing contact
        /// </summary>
        public string billing_email { get; set; }

        /// <summary>
        /// The phone number for the primary billing contact
        /// </summary>
        public string billing_phone { get; set; }

        /// <summary>
        /// Address for the primary billing contact
        /// </summary>
        public address billing_address { get; set; }

        /// <summary>
        /// Additional information to record with the credit card request
        /// </summary>
        public IEnumerable<reference> references { get; set; }

        /// <summary>
        /// Verify the credit card. Default is true. Not applicable for updates.
        /// </summary>
        public bool verify { get; set; } = true;

        public static explicit operator credit_card_request(CreditCardRequest creditCardRequest)
                {
                    return new credit_card_request
                    {
                        order_number = creditCardRequest.OrderNumber,
                        name_on_card = creditCardRequest.CardOwner,
                        card_number = creditCardRequest.CardNumber,
                        expiry_month = creditCardRequest.ExpiryMonth,
                        expiry_year = creditCardRequest.ExpiryYear,
                        card_security_code = creditCardRequest.CardSecurityCode,
                        billing_name = creditCardRequest.BillingName,
                        billing_email = creditCardRequest.BillingEmail,
                        billing_phone = creditCardRequest.BillingPhone,
                        billing_address = (address)creditCardRequest.BillingAddress,
                        verify = creditCardRequest.Verify,

                        application_name = creditCardRequest.ApplicationName,
                        references = creditCardRequest.References?.Select(r => (reference)r),
                        authentication_header = creditCardRequest.AuthenticationHeader,
                        language_code = Enum.GetName(typeof(LanguageCode), creditCardRequest.LanguageCode)
                    };
                }
    }
}