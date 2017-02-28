using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace PelotonEppSdk.Models
{
    public class StateChangeNotification
    {
        public decimal? Amount { get; set; }

        public State StateChangeTo { get; set; }

        public DateTime StateChangeDatetime { get; set; }

        public string TransactionRefCode { get; set; }

        public string ScheduleToken { get; set; }

        public ICollection<Reference> References { get; set; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class state_change_notification
    {
        public decimal? amount { get; set; }

        public state state_change_to { get; set; }

        public DateTime state_change_datetime { get; set; }

        public string transaction_ref_code { get; set; }

        public string schedule_token { get; set; }

        public ICollection<reference> references { get; set; }

        public static explicit operator StateChangeNotification(state_change_notification scn)
        {
            return new StateChangeNotification
            {
                Amount = scn.amount,
                References = scn.references.Select(r => (Reference) r).ToList(),
                ScheduleToken = scn.schedule_token,
                StateChangeDatetime = scn.state_change_datetime,
                StateChangeTo = (State) scn.state_change_to,
                TransactionRefCode = scn.transaction_ref_code
            };
        }
    }
}
