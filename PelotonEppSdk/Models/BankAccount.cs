using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PelotonEppSdk.Models
{
    /// <summary>
    /// The set of properties which define a bank account
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// The identifier used to identify a bank account. For new bank accounts, submit a value up to 32
        /// characters or leave blank to use a system generated value. If you are modifying a bank account you must reference the existing
        /// bank_account_token associated with the bank account being modified. A bank_account_token cannot be modified once it has been assigned to a bank account.
        /// </summary>
        [StringLength(32, ErrorMessage = "The " + nameof(Token) + " field must be less than 32 characters long.")]
        public string Token { get; set; }

        /// <summary>
        /// Specify a name for the account which will be used for display purposes.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Specify the name of the account owner as it appears on cheque or statement.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Specify the type of the account.
        /// 0 = Chequing,
        /// 1 = Savings,
        /// 2 = Trust.
        /// This field will default to 0 (Chequing).
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// Financial Institution Number
        /// </summary>
        [Required]
        public decimal? FinancialInstitution { get; set; }

        /// <summary>
        /// Branch Transit Number.
        /// </summary>
        [Required]
        public decimal? BranchTransitNumber { get; set; }

        /// <summary>
        /// Bank Account Number.
        /// </summary>
        [Required]
        public string AccountNumber { get; set; }

        /// <summary>
        /// Currency Code, acceptable values are CAD and USD.
        /// </summary>
        [Required, RegularExpression("CAD|USD")]
        public string CurrencyCode { get; set; }
 }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class bank_account
    {
        public string bank_account_token { get; set; }

        public string name { get; set; }

        public string owner { get; set; }

        public int type_code { get; set; }

        public string financial_institution_number { get; set; }

        public string branch_transit_number { get; set; }

        public string account_number { get; set; }

        public string currency_code { get; set; }

        public static explicit operator bank_account(BankAccount bankAccount)
        {
            return new bank_account
            {
                account_number = bankAccount.AccountNumber,
                bank_account_token = bankAccount.Token,
                branch_transit_number = bankAccount.BranchTransitNumber?.ToString(),
                currency_code = bankAccount.CurrencyCode,
                financial_institution_number = bankAccount.FinancialInstitution?.ToString(),
                name = bankAccount.Name,
                owner = bankAccount.Owner,
                type_code = Convert.ToInt32(bankAccount.TypeCode)
            };
        }
    }
}
