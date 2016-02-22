using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PelotonEppSdk.Models
{
    public class BankAccount
    {
        [StringLength(32, ErrorMessage = "The " + nameof(Token) + " field must be less than 32 characters long.")]
        public string Token { get; set; }

        [Required]
        public string Name { get; set; }

        public string Owner { get; set; }

        public string TypeCode { get; set; }

        [Required]
        public decimal? FinancialInstitution { get; set; }

        [Required]
        public decimal? BranchTransitNumber { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required, RegularExpression("CAD|USD")]
        public string CurrencyCode { get; set; }
 }

    /// <summary>
    /// The set of properties which define a bank account
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class bank_account
    {
        /// <summary>
        /// The identifier used to identify a bank account. For new bank accounts, submit a value up to 32 
        /// characters or leave blank to use a system generated value. If you are modifying a bank account you must reference the existing 
        /// bank_account_token associated with the bank account being modified. A bank_account_token cannot be modified once it has been assigned to a bank account. 
        /// </summary>
        [StringLength(32, ErrorMessage = "The " + nameof(bank_account_token) + " field must be less than 32 characters long.")]
        public string bank_account_token { get; set; }

        /// <summary>
        /// Specify a name for the account which will be used for display purposes.
        /// </summary>
        [Required]
        public string name { get; set; }

        /// <summary>
        /// Specify the name of the account owner as it appears on cheque or statement.
        /// </summary>
        public string owner { get; set; }

        /// <summary>
        /// Specify the type of the account.
        /// 0 = Chequing,
        /// 1 = Savings,
        /// 2 = Trust.
        /// This field will default to 0 (Chequing).
        /// </summary>
        public int type_code { get; set; }

        /// <summary>
        /// Financial Institution Number
        /// </summary>
        [Required]
        public string financial_institution_number { get; set; }

        /// <summary>
        /// Branch Transit Number.
        /// </summary>
        [Required]
        public string branch_transit_number { get; set; }

        /// <summary>
        /// Bank Account Number.
        /// </summary>
        [Required]
        public string account_number { get; set; }

        /// <summary>
        /// Currency Code, acceptable values are CAD and USD.
        /// </summary>
        [Required, RegularExpression("CAD|USD")]
        public string currency_code { get; set; }

        public static explicit operator bank_account(BankAccount account)
        {
            return new bank_account
            {
                account_number = account.AccountNumber,
                bank_account_token = account.Token,
                branch_transit_number = account.BranchTransitNumber?.ToString(),
                currency_code = account.CurrencyCode,
                financial_institution_number = account.FinancialInstitution?.ToString(),
                name = account.Name,
                owner = account.Owner,
                type_code = Convert.ToInt32(account.TypeCode)
            };
        }
    }
}
