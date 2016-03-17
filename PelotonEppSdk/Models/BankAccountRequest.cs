using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Interfaces;

namespace PelotonEppSdk.Models
{
    public class BankAccountRequest : RequestBase, IBankAccountCreateRequest, IBankAccountDeleteRequest
    {
        // start of create fields and methods
        [Required]
        public BankAccount BankAccount { get; set; }

        public bool VerifyAccountByDeposit { get; set; }

        public Document Document { get; set; }

        public IEnumerable<Reference> References { get; set; }

        public async Task<Response> PostAsync()
        {
            var client = new PelotonClient();
            var request = (bank_account_request) this;
            var result = await client.PostAsync<bank_account_response>(request, ApiTarget.BankAccounts);
            return (BankAccountCreateResponse) result;
        }
        // end of create fields and methods

        // start of delete fields and methods
        public string Token { get; set; }

        public async Task<Response> DeleteAsync()
        {
            var client = new PelotonClient();
            var result = await client.DeleteAsyncBankAccountsV1<response>((bank_account_delete_request)this, ApiTarget.FundsTransfers);
            return (Response)result;
        }
        // end of delete fields and methods

    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class bank_account_request : request_base
    {
        /// <summary>
        /// The bank account which is to be created
        /// </summary>
        [Required]
        public bank_account bank_account { get; set; }

        /// <summary>
        /// Set True if you would like to initiate the verification process by issuing one or more deposits totalling less than $1.00
        /// to this Bank Account.
        /// </summary>
        public bool verify_account_by_deposit { get; set; }

        /// <summary>
        /// Data that can be used to verify the account or conditions associated with the account.
        /// Typically a void cheque, PAD agreement or other official bank document.
        /// </summary>
        public document document { get; set; }

        /// <summary>
        /// A list of fields used to pass additional information to record with the transfer request.
        /// </summary>
        public IEnumerable<reference> references { get; set; }

        public static explicit operator bank_account_request(BankAccountRequest bankAccountCreateRequest)
        {
            return new bank_account_request
            {
                bank_account = (bank_account) bankAccountCreateRequest.BankAccount,
                verify_account_by_deposit = bankAccountCreateRequest.VerifyAccountByDeposit,
                document = (document) bankAccountCreateRequest.Document,
                application_name = bankAccountCreateRequest.ApplicationName,
                references = bankAccountCreateRequest.References?.Select(r => (reference)r),
                authentication_header = bankAccountCreateRequest.AuthenticationHeader,
                language_code = Enum.GetName(typeof(LanguageCode), bankAccountCreateRequest.LanguageCode)
            };
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class bank_account_delete_request : request_base
    {
        /// <summary>
        /// The token for the bank account which is to be deleted
        /// </summary>
        public string bank_account_token { get; set; }

        public static explicit operator bank_account_delete_request(BankAccountRequest bankAccountCreateRequest)
        {
            return new bank_account_delete_request
            {
                bank_account_token = bankAccountCreateRequest.Token
            };
        }
    }
}