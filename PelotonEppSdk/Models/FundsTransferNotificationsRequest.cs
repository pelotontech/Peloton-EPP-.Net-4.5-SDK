using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;

namespace PelotonEppSdk.Models
{
    public class FundsTransferNotificationsRequest : RequestBase
    {
        /// <summary>
        /// The token representing the date and time for which notifications will be provided
        /// </summary>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// The number of items to be returned
        /// </summary>
        public int? Items { get; set; }

        public async Task<FundsTransferNotificationsResponse> GetAsync()
        {
            var client = new PelotonClient();
            var parameter = Token;
            if (Items.HasValue)
                parameter = parameter + "?items=" + Items;
            var result = await client.GetAsync<funds_transfer_notifications_response>((funds_transfer_notifications_request)this, ApiTarget.FundsTransferNotifications, parameter).ConfigureAwait(false);
            return (FundsTransferNotificationsResponse)result;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class funds_transfer_notifications_request : request_base
    {
        [Required]
        public string token { get; set; }

        public int? items { get; set; }

        public funds_transfer_notifications_request(RequestBase requestBase) : base(requestBase) { }

        public static explicit operator funds_transfer_notifications_request(FundsTransferNotificationsRequest fundsTransferNotificationsRequest)
        {
            return new funds_transfer_notifications_request(fundsTransferNotificationsRequest)
            {
                token = fundsTransferNotificationsRequest.Token,
                items = fundsTransferNotificationsRequest.Items,
                application_name = fundsTransferNotificationsRequest.ApplicationName,
                authentication_header = fundsTransferNotificationsRequest.AuthenticationHeader,
                language_code = Enum.GetName(typeof(LanguageCode), fundsTransferNotificationsRequest.LanguageCode)
            };
        }
    }
}
