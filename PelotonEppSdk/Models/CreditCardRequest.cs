using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Interfaces;

namespace PelotonEppSdk.Models
{
    public class CreditCardRequest : RequestBase, ICreditCardCreateRequest, ICreditCardDeleteRequest, ICreditCardUpdateRequest, IOptionalAccountToken
    {
        public CreditCardRequest()
        {
            References = new List<Reference>();
        }

        public string CreditCardToken { get; set; }

        /// <inheritdoc cref="IOptionalAccountToken.AccountToken"/>
        public string AccountToken { get; set; }

        /// <summary>
        /// Recommended: Order number provided by the source system, otherwise one will be automatically generated
        /// </summary>
        [StringLength(50)]
        public string OrderNumber { get; set; }

        /// <summary>
        /// The name of the card owner as it appears on the credit card
        /// </summary>
        [StringLength(26, MinimumLength = 2)]
        public string CardOwner { get; set; }

        /// <summary>
        /// The credit card number as it appears on the card. Not applicable for updates.
        /// </summary>
        public CreditCardNumber CardNumber { get; set; }

        /// <summary>
        /// The credit card expiration month in two-digit format (e.g. 09 for September)
        /// </summary>
        [StringLength(2, MinimumLength = 2)]
        public string ExpiryMonth { get; set; }

        /// <summary>
        /// The credit card expiration year in two-digit format (e.g. 08 for 2008)
        /// </summary>
        [StringLength(2, MinimumLength = 2)]
        public string ExpiryYear { get; set; }

        /// <summary>
        /// The 3 or 4 digit CVV2/CVC2/CID. Not applicable for updates.
        /// </summary>
        [StringLength(4, MinimumLength = 3)]
        public string CardSecurityCode { get; set; }

        /// <summary>
        /// Additional information to record with the credit card request
        /// </summary>
        public ICollection<Reference> References { get; set; }

        /// <summary>
        /// The primary billing contact name
        /// </summary>
        public string BillingName { get; set; }

        /// <summary>
        /// The phone number for the primary billing contact
        /// </summary>
        public string BillingPhone { get; set; }

        /// <summary>
        /// The email address for the primary billing contact
        /// </summary>
        public string BillingEmail { get; set; }

        /// <summary>
        /// Address for the primary billing contact
        /// </summary>
        public Address BillingAddress { get; set; }

        /// <summary>
        /// Verify the credit card. Default is true. Not applicable for updates.
        /// </summary>
        public bool Verify { get; set; }

        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is not <c>2XX Success</c>.</exception>
        public async Task<CreditCardResponse> PostAsync()
        {
            var client = new PelotonClient();
            var request = (credit_card_request) this;
            var result = await client.PostAsync<credit_card_response>(request, ApiTarget.CreditCards).ConfigureAwait(false);
            return (CreditCardResponse) result;
        }

        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is not <c>2XX Success</c>.</exception>
        public async Task<CreditCardResponse> PutAsync()
        {
            var client = new PelotonClient();
            var request = (credit_card_request) this;
            var result = await client.PutAsync<credit_card_response>(request, ApiTarget.CreditCards, CreditCardToken).ConfigureAwait(false);
            return (CreditCardResponse)result;
        }

        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is not <c>2XX Success</c>.</exception>
        public async Task<CreditCardResponse> DeleteAsync()
        {
            var client = new PelotonClient();
            var request = (credit_card_request) this;
            var result = await client.DeleteAsync<credit_card_response>(request, ApiTarget.CreditCards, CreditCardToken).ConfigureAwait(false);
            return (CreditCardResponse) result;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class credit_card_request : request_base, Ioptional_account_token
    {
        public string account_token { get; set; }

        public string order_number { get; set; }

        public string name_on_card { get; set; }

        public long? card_number { get; set; }

        public string expiry_month { get; set; }

        public string expiry_year { get; set; }

        public string card_security_code { get; set; }

        public string billing_name { get; set; }

        public string billing_email { get; set; }

        public string billing_phone { get; set; }

        public address billing_address { get; set; }

        public IEnumerable<reference> references { get; set; }

        public bool verify { get; set; } = true;

        public credit_card_request(RequestBase requestBase) : base(requestBase) { }

        public static explicit operator credit_card_request(CreditCardRequest creditCardRequest)
        {
            return new credit_card_request(creditCardRequest)
            {
                account_token = creditCardRequest.AccountToken,
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

                references = creditCardRequest.References?.Select(r => (reference)r),
            };
        }
    }
}