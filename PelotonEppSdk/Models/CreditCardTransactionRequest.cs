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
  	public class CreditCardTransactionRequest : RequestBase
    {
        public string OrderNumber { get; set; }

        public string CreditCardToken { get; set; }

        public decimal? Amount { get; set; }

        public string Type { get; set; }

		public string BillingName { get; set; }

        public string BillingEmail { get; set; }

        public string BillingPhone { get; set; }

        public Address BillingAddress { get; set; }

        public string ShippingName { get; set; }

        public string ShippingEmail { get; set; }

        public string ShippingPhone { get; set; }

        public Address ShippingAddress { get; set; }

        public bool ShippingMatchesBillingAddress => BillingAddress != null && BillingAddress.Equals(ShippingAddress);

        public ICollection<Reference> References { get; set; }

        public string TransactionRefCode { get; set; }

        public DeviceData DeviceData { get; set; }

  	    public async Task<Response> PostAsync()
        {
            var client = new PelotonClient();
            var request = (credit_card_transaction_request) this;
            // TODO: implement this method to use the EPP.API endpoint which corresponds to this request type (Fraser has details)
            //var result = await client.PostAsync<credit_card_transaction_response>(request, ApiTarget.CreditCardTransactions);
            //return (CreditCardTransactionResponse) result;

  	        return new CreditCardTransactionResponse
  	        {
  	            Message = "Success",
  	            MessageCode = 0,
  	            Success = true,
  	            TransactionRefCode = Guid.NewGuid().ToString().Replace("-", "")
  	        };
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class credit_card_transaction_request : request_base
    {
		/// <summary>
        /// Recommended: Order number provided by the source system, otherwise one will be automatically generated
        /// </summary>
        [StringLength(50)]
        public string order_number { get; set; }

        /// <summary>
        /// The credit_card_token assigned corresponding to the credit card which will be charged
        /// </summary>
        [Required]
        public string credit_card_token { get; set; }

        /// <summary>
        /// Total amount to be charged to the customer via this transaction
        /// </summary>
        public decimal? amount { get; set; }

        /// <summary>
        /// The type of transaction to perform
        /// </summary>
        [Required]
        [RegularExpression("VERIFY|PURCHASE|RETURN|AUTHORIZE|COMPLETE")]
        public string type { get; set; }

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
        /// The name of the shipping contact
        /// </summary>
        public string shipping_name { get; set; }

        /// <summary>
        /// This email address for the shipping contact
        /// </summary>
        public string shipping_email { get; set; }

        /// <summary>
        /// The phone number for the shipping contact
        /// </summary>
        public string shipping_phone { get; set; }

        /// <summary>
        /// The address for the shipping contact
        /// </summary>
        public address shipping_address { get; set; }

        /// <summary>
        /// The transaction reference code provided during the original pre-authorization.
        /// Required for types "RETURN|AUTHORIZE|COMPLETE"
        /// </summary>
        [StringLength(36, MinimumLength = 36)]
        public string transaction_ref_code { get; set; }

        /// <summary>
        /// Additional information to record with the transaction request
        /// </summary>
        public ICollection<reference> references { get; set; }


        public static explicit operator credit_card_transaction_request(CreditCardTransactionRequest creditCardTransactionRequest)
        {
            return new credit_card_transaction_request
            {
                order_number = creditCardTransactionRequest.OrderNumber,
                credit_card_token = creditCardTransactionRequest.CreditCardToken,
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