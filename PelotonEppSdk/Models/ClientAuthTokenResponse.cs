using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using PelotonEppSdk.Enums;

namespace PelotonEppSdk.Models
{
    public class ClientAuthTokenResponse: Response
    {
        /// <summary>
        /// The one-time-use client authorization token (CAT).
        /// </summary>
        public string ClientAuthToken { get; set; }

        /// <summary>
        /// The URL used for redirection, when provided.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// The type of the action is represented here, if the CAT has been used, otherwise null.
        /// The name/value pair indicates which property will be populated with the payload.
        /// </summary>
        public ClientAuthTokenAuthorizationType Type { get; set; }

        /// <summary>
        /// The card_token assigned to the created credit card.
        /// </summary>
        [StringLength(32)]
        public string CardToken { get; set; }

        /// <summary>
        /// The bank_account_token assigned to the created bank account.
        /// </summary>
        [StringLength(32)]
        public string BankAccountToken { get; set; }

        /// <summary>
        /// This field will return a value for any transaction that occurs, otherwise will be empty.
        /// </summary>
        [StringLength(32)]
        public string TransactionRefCode { get; set; }

        /// <summary>
        /// The unique identifier for the event.
        /// </summary>
        [StringLength(32)]
        public string EventToken { get; set; }

        /// <summary>
        /// True if the CAT has not expired and can be used for an authorization, otherwise false.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// The datetime when the CAT was used, or null if the CAT is active
        /// </summary>
        public DateTime? AuthorizedDatetime { get; set; }

        internal ClientAuthTokenResponse(response r) : base(r)
        {
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class client_auth_token_response: response
    {
        public string client_auth_token { get; set; }

        public static explicit operator ClientAuthTokenResponse(client_auth_token_response clientAuthTokenResponse)
        {
            if (clientAuthTokenResponse == null) return null;
            return new ClientAuthTokenResponse(clientAuthTokenResponse)
            {
                ClientAuthToken = clientAuthTokenResponse.client_auth_token
            };
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class client_auth_token_status_response: response
    {
        public string client_auth_token { get; set; }

        public string return_url { get; set; }

        public type type { get; set; }

        public string card_token { get; set; }

        public string bank_account_token { get; set; }

        public string transaction_ref_code { get; set; }

        public string event_token { get; set; }

        public bool active { get; set; }

        public DateTime? authorized_datetime { get; set; }

        public static explicit operator ClientAuthTokenResponse(client_auth_token_status_response clientAuthTokenResponse)
        {
            if (clientAuthTokenResponse == null) return null;

            ClientAuthTokenAuthorizationType type = 0;

            if (clientAuthTokenResponse.type?.code != null)
            {
                var parseResult = Enum.TryParse(clientAuthTokenResponse.type.code, out type);
                if (!parseResult) Debug.WriteLine("Failed to parse Client Auth Token response type code.");
            }

            return new ClientAuthTokenResponse(clientAuthTokenResponse)
            {
                ClientAuthToken = clientAuthTokenResponse.client_auth_token,
                ReturnUrl = clientAuthTokenResponse.return_url,
                Active = clientAuthTokenResponse.active,
                AuthorizedDatetime = clientAuthTokenResponse.authorized_datetime,

                Type = type,
                CardToken = clientAuthTokenResponse.card_token,
                BankAccountToken = clientAuthTokenResponse.bank_account_token,
                TransactionRefCode = clientAuthTokenResponse.transaction_ref_code,
                EventToken = clientAuthTokenResponse.event_token
            };
        }
    }
}