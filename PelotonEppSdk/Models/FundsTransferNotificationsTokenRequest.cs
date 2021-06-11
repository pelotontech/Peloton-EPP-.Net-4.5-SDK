using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Interfaces;

namespace PelotonEppSdk.Models
{
    public class FundsTransferNotificationsTokenRequest : RequestBase, IOptionalAccountToken
    {
        /// <inheritdoc cref="IOptionalAccountToken.AccountToken"/>
        public string AccountToken { get; set; }

        /// <summary>
        /// Sets the date associated with the token, from which the Funds Transfers Notifications will be returned
        /// </summary>
        [Required]
        public string FromDateUtc { get; set; }

        public async Task<FundsTransferNotificationsResponse> PostAsync()
        {
            var client = new PelotonClient();
            var result = await client.PostAsync<funds_transfer_notifications_response>((funds_transfer_token_request)this, ApiTarget.FundsTransferNotifications).ConfigureAwait(false);
            return (FundsTransferNotificationsResponse)result;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class funds_transfer_token_request : request_base, Ioptional_account_token
    {
        public string account_token { get; set; }

        [Required]
        public string from_date_utc { get; set; }

        public funds_transfer_token_request(RequestBase requestBase) : base(requestBase) { }

        public static explicit operator funds_transfer_token_request(FundsTransferNotificationsTokenRequest fundsTransferNotificationsRequest)
        {
            return new funds_transfer_token_request(fundsTransferNotificationsRequest)
            {
                account_token = fundsTransferNotificationsRequest.AccountToken,
                from_date_utc = fundsTransferNotificationsRequest.FromDateUtc,
                application_name = fundsTransferNotificationsRequest.ApplicationName,
                authentication_header = fundsTransferNotificationsRequest.AuthenticationHeader,
                language_code = Enum.GetName(typeof(LanguageCode), fundsTransferNotificationsRequest.LanguageCode)
            };
        }
    }
}
