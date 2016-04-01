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
  	public class CreditCardTokenTransactionRequest : RequestBase
    {
		/// <summary>
        /// Recommended: Order number provided by the source system, otherwise one will be automatically generated
        /// </summary>
        [StringLength(50)]
        public string OrderNumber { get; set; }

        /// <summary>
        /// The credit_card_token assigned corresponding to the credit card which will be charged
        /// </summary>
        [Required]
        public string CreditCardToken { get; set; }

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

        public async Task<CreditCardTransactionResponse> PostAsync()
        {
            var client = new PelotonClient();
            var request = (credit_card_token_transaction_request) this;
  	        var result = await client.PostAsync<credit_card_transaction_response>(request, ApiTarget.CreditCardTransactions, CreditCardToken).ConfigureAwait(false);
  	        return (CreditCardTransactionResponse) result;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class credit_card_token_transaction_request : request_base
    {
        public string order_number { get; set; }

        public string credit_card_token { get; set; }

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

        public credit_card_token_transaction_request(RequestBase requestBase) : base(requestBase) { }

        public static explicit operator credit_card_token_transaction_request(CreditCardTokenTransactionRequest creditCardTokenTransactionRequest)
        {
            return new credit_card_token_transaction_request(creditCardTokenTransactionRequest)
            {
                order_number = creditCardTokenTransactionRequest.OrderNumber,
                credit_card_token = creditCardTokenTransactionRequest.CreditCardToken,
                amount = creditCardTokenTransactionRequest.Amount,
                type = creditCardTokenTransactionRequest.Type,
                billing_name = creditCardTokenTransactionRequest.BillingName,
                billing_email = creditCardTokenTransactionRequest.BillingEmail,
                billing_phone = creditCardTokenTransactionRequest.BillingPhone,
                billing_address = (address)creditCardTokenTransactionRequest.BillingAddress,

                shipping_name = creditCardTokenTransactionRequest.ShippingName,
                shipping_email = creditCardTokenTransactionRequest.ShippingEmail,
                shipping_phone = creditCardTokenTransactionRequest.ShippingPhone,
                shipping_address = (address)creditCardTokenTransactionRequest.ShippingAddress,

                transaction_ref_code = creditCardTokenTransactionRequest.TransactionRefCode,

                references = creditCardTokenTransactionRequest.References?.Select(r => (reference)r).ToList(),
                application_name = creditCardTokenTransactionRequest.ApplicationName,
                authentication_header = creditCardTokenTransactionRequest.AuthenticationHeader,
                language_code = Enum.GetName(typeof(LanguageCode), creditCardTokenTransactionRequest.LanguageCode)
            };
        }
    }
}