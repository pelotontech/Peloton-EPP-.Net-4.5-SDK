using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using PelotonEppSdk.Interfaces;

namespace PelotonEppSdk.Models
{
    public class FundsTransferNotificationsResponse : Response
    {
        /// <summary>
        /// List containing the new notifications since the time associated with the token
        /// </summary>
        public ICollection<StateChangeNotification> Notifications { get; set; }

        /// <summary>
        /// The new token to be submitted for the next Funds Transfer Notification Request
        /// </summary>
        public string Token { get; set; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class funds_transfer_notifications_response : response
    {
        public ICollection<StateChangeNotification> notifications { get; set; }

        public string token { get; set; }

        public static explicit operator FundsTransferNotificationsResponse(funds_transfer_notifications_response fundsTransferNotificationsResponse)
        {
            if (fundsTransferNotificationsResponse == null) return null;
            return new FundsTransferNotificationsResponse
            {
                Success = fundsTransferNotificationsResponse.success,
                Message = fundsTransferNotificationsResponse.message,
                Errors = fundsTransferNotificationsResponse.errors,
                MessageCode = fundsTransferNotificationsResponse.message_code,
                TransactionRefCode = fundsTransferNotificationsResponse.transaction_ref_code
            };
        }
    }
}