using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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

        internal FundsTransferNotificationsResponse(response r) : base(r)
        {
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class funds_transfer_notifications_response : response
    {
        public ICollection<state_change_notification> notifications { get; set; }

        public string token { get; set; }

        public static explicit operator FundsTransferNotificationsResponse(funds_transfer_notifications_response fundsTransferNotificationsResponse)
        {
            if (fundsTransferNotificationsResponse == null) return null;

            ICollection<StateChangeNotification> notifications = null;
            if (fundsTransferNotificationsResponse.notifications != null)
                notifications = fundsTransferNotificationsResponse.notifications.Select(ftn => (StateChangeNotification) ftn).ToList();

            return new FundsTransferNotificationsResponse(fundsTransferNotificationsResponse)
            {
                Notifications = notifications,
                Token = fundsTransferNotificationsResponse.token
            };
        }
    }
}