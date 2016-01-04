using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PelotonEppSdk.Models
{
    public class FundsTransferRequest : RequestBase
    {
        /// <summary>
        /// Total Amount to be transferred between accounts. Must be positive.
        ///  </summary>
        [Required]
        public decimal? Amount { get; set; }

        /// <summary>
        /// The Type of transfer (EFT or ACH)
        /// </summary>
        [Required]
        [RegularExpression("EFT|ACH")]
        [StringLength(3)]
        public string TransferSystem { get; set; }

        /// <summary>
        /// The account identifier used to identify a bank account.
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 32)]
        public string BankAccountToken { get; set; }

        /// <summary>
        /// ClientToken for the target peloton account
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 32)]
        public string AccountToken { get; set; }

        /// <summary>
        /// This will default to the current UTC DateTime. If past dates are supplied the current UTC DateTime will be used.
        /// A future UTC DateTime can be provided and the transfer will be scheduled for processing on that date.
        /// </summary>
        public DateTime? TransferDate { get; set; }

        /// <summary>
        /// Flag for credit or debit
        /// </summary>
        [RegularExpression("CREDIT|DEBIT")]
        public string Type { get; set; }

        /// <summary>
        /// A list of fields used to pass additional information to record with the transfer request.
        /// </summary>
        public IEnumerable<Reference> References { get; set; }
    }

    internal class funds_transfer_request :request_base
    {  
        [Required]
        public decimal? amount { get; set; }

        [Required]
        [RegularExpression("EFT|ACH")]
        [StringLength(3)]
        public string transfer_system { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 32)]
        public string bank_account_token { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 32)]
        public string account_token { get; set; }

        public DateTime? transfer_date { get; set; }

        [RegularExpression("CREDIT|DEBIT")]
        public string type { get; set; }

        public IEnumerable<reference> references { get; set; }

        public static explicit operator funds_transfer_request(FundsTransferRequest request)
        {
            return new funds_transfer_request
            {
                account_token = request.AccountToken,
                amount = request.Amount,
                application_name = request.ApplicationName,
                authentication_header = request.AuthenticationHeader,
                bank_account_token = request.BankAccountToken,
                language_code = request.LanguageCode,
                references = request.References?.Select(r => (reference) r),
                transfer_date = request.TransferDate,
                transfer_system = request.TransferSystem,
                type = request.Type
            };
        }
    }
}
