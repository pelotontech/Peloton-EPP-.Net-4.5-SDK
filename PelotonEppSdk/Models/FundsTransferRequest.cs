using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Validations;

namespace PelotonEppSdk.Models
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum FundsTransferType
    {
        CREDIT,
        DEBIT
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum FundsTransferSystem
    {
        EFT
    }

    public class FundsTransferRequest : RequestBase
    {
        /// <summary>
        /// Total Amount to be transferred between accounts. Must be positive.
        ///  </summary>
        [Range(0.01, int.MaxValue)]
        [DecimalDollarValue(ErrorMessage = "Amount must be a multiple of 0.01.")]
        public decimal Amount { get; set; }

        /// <summary>
        /// The Type of transfer (EFT or ACH)
        /// </summary>
        [Required]
        public FundsTransferSystem? TransferSystem { get; set; }

        /// <summary>
        /// The account identifier used to identify a bank account.
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 32, ErrorMessage = "BankAccountToken must be 32 characters long.")]
        public string BankAccountToken { get; set; }

        /// <summary>
        /// ClientToken for the target peloton account
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 32, ErrorMessage = "AccountToken must be 32 characters long.")]
        public string AccountToken { get; set; }

        /// <summary>
        /// This will default to the current UTC DateTime. If past dates are supplied the current UTC DateTime will be used.
        /// A future UTC DateTime can be provided and the transfer will be scheduled for processing on that date.
        /// </summary>
        public DateTime? TransferDate { get; set; }

        /// <summary>
        /// Flag for credit or debit
        /// </summary>
        [Required]
        public FundsTransferType? Type { get; set; }

        /// <summary>
        /// A list of fields used to pass additional information to record with the transfer request.
        /// </summary>
        public IEnumerable<Reference> References { get; set; }

        public async Task<TransactionResponse> PostAsync()
        {
            var client = new PelotonClient();
            var result = await client.PostAsync<transaction_response>((funds_transfer_request)this, ApiTarget.FundsTransfers).ConfigureAwait(false);
            return (TransactionResponse)result;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
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

        public funds_transfer_request(RequestBase requestBase) : base(requestBase) { }

        public static explicit operator funds_transfer_request(FundsTransferRequest fundsTransferRequest)
        {
            return new funds_transfer_request(fundsTransferRequest)
            {
                account_token = fundsTransferRequest.AccountToken,
                amount = fundsTransferRequest.Amount,
                application_name = fundsTransferRequest.ApplicationName,
                authentication_header = fundsTransferRequest.AuthenticationHeader,
                bank_account_token = fundsTransferRequest.BankAccountToken,
                language_code = Enum.GetName(typeof(LanguageCode), fundsTransferRequest.LanguageCode),
                references = fundsTransferRequest.References?.Select(r => (reference) r),
                transfer_date = fundsTransferRequest.TransferDate,
                transfer_system = Enum.GetName(typeof(FundsTransferSystem), fundsTransferRequest.TransferSystem.Value),
                type = Enum.GetName(typeof(FundsTransferType), fundsTransferRequest.Type.Value)
            };
        }
    }
}
