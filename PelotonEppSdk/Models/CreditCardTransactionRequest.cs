using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;

namespace PelotonEppSdk.Models
{
    public class CreditCardTransactionRequest : RequestBase
    {
  	    public CreditCardTransactionRequest()
  	    {
            References = new List<Reference>();
        }

        /// <summary>
        /// The name of the card owner as it appears on the credit card
        /// </summary>
        [StringLength(26, MinimumLength = 2)]
        public string CardOwner { get; set; }

        /// <summary>
        /// The credit card number as it appears on the card.
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
        /// The 3 or 4 digit CVV2/CVC2/CID.
        /// </summary>
        [StringLength(4, MinimumLength = 3)]
        public string CardSecurityCode { get; set; }

		/// <summary>
        /// Recommended: Order number provided by the source system, otherwise one will be automatically generated
        /// </summary>
        [StringLength(50)]
        public string OrderNumber { get; set; }

        /// <summary>
        /// Total amount to be charged to the customer via this transaction
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// The type of transaction to perform
        /// </summary>
        [Required]
        [RegularExpression("VERIFY|PURCHASE|RETURN|AUTHORIZE|COMPLETE")]
        public string Type { get; set; }

        /// <summary>
        /// The primary billing contact name
        /// </summary>
		public string BillingName { get; set; }

        /// <summary>
        /// The email address for the primary billing contact
        /// </summary>
        public string BillingEmail { get; set; }

        /// <summary>
        /// The phone number for the primary billing contact
        /// </summary>
        public string BillingPhone { get; set; }

        /// <summary>
        /// Address for the primary billing contact
        /// </summary>
        public Address BillingAddress { get; set; }

        /// <summary>
        /// The name of the shipping contact
        /// </summary>
        public string ShippingName { get; set; }

        /// <summary>
        /// This email address for the shipping contact
        /// </summary>
        public string ShippingEmail { get; set; }

        /// <summary>
        /// The phone number for the shipping contact
        /// </summary>
        public string ShippingPhone { get; set; }

        /// <summary>
        /// The address for the shipping contact
        /// </summary>
        public Address ShippingAddress { get; set; }

        public bool ShippingMatchesBillingAddress => BillingAddress != null && BillingAddress.Equals(ShippingAddress);

        /// <summary>
        /// Additional information to record with the transaction request
        /// </summary>
        public ICollection<Reference> References { get; set; }

        /// <summary>
        /// The transaction reference code provided during the original pre-authorization.
        /// Required for types "RETURN|AUTHORIZE|COMPLETE"
        /// </summary>
        [StringLength(36, MinimumLength = 36)]
        public string TransactionRefCode { get; set; }

  	    /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is not <c>2XX Success</c>.</exception>
  	    public async Task<CreditCardTransactionResponse> PostAsync()
        {
            var client = new PelotonClient();
            var request = (credit_card_transaction_request) this;
  	        var result = await client.PostAsync<credit_card_transaction_response>(request, ApiTarget.CreditCardTransactions).ConfigureAwait(false);
  	        return (CreditCardTransactionResponse) result;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class credit_card_transaction_request : request_base
    {
        public string name_on_card { get; set; }

        public long? card_number { get; set; }

        public string expiry_month { get; set; }

        public string expiry_year { get; set; }

        public string card_security_code { get; set; }

        public string order_number { get; set; }

        public decimal? amount { get; set; }

        public string type { get; set; }

        public string billing_name { get; set; }

        public string billing_email { get; set; }

        public string billing_phone { get; set; }

        public address billing_address { get; set; }

        public string shipping_name { get; set; }

        public string shipping_email { get; set; }

        public string shipping_phone { get; set; }

        public address shipping_address { get; set; }

        public string transaction_ref_code { get; set; }

        public ICollection<reference> references { get; set; }

        public credit_card_transaction_request(RequestBase requestBase) : base(requestBase) { }

        public static explicit operator credit_card_transaction_request(CreditCardTransactionRequest creditCardTransactionRequest)
        {
            return new credit_card_transaction_request(creditCardTransactionRequest)
            {
                card_number = creditCardTransactionRequest.CardNumber,
                name_on_card = creditCardTransactionRequest.CardOwner,
                expiry_month = creditCardTransactionRequest.ExpiryMonth,
                expiry_year = creditCardTransactionRequest.ExpiryYear,
                card_security_code = creditCardTransactionRequest.CardSecurityCode,
                order_number = creditCardTransactionRequest.OrderNumber,
                amount = creditCardTransactionRequest.Amount,
                type = creditCardTransactionRequest.Type,
                billing_name = creditCardTransactionRequest.BillingName,
                billing_email = creditCardTransactionRequest.BillingEmail,
                billing_phone = creditCardTransactionRequest.BillingPhone,
                billing_address = (address)creditCardTransactionRequest.BillingAddress,

                shipping_name = creditCardTransactionRequest.ShippingName,
                shipping_email = creditCardTransactionRequest.ShippingEmail,
                shipping_phone = creditCardTransactionRequest.ShippingPhone,
                shipping_address = (address)creditCardTransactionRequest.ShippingAddress,

                transaction_ref_code = creditCardTransactionRequest.TransactionRefCode,

                references = creditCardTransactionRequest.References?.Select(r => (reference)r).ToList(),
                application_name = creditCardTransactionRequest.ApplicationName,
                authentication_header = creditCardTransactionRequest.AuthenticationHeader,
                language_code = Enum.GetName(typeof(LanguageCode), creditCardTransactionRequest.LanguageCode)
            };
        }
    }
}