using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace PelotonEppSdk.Models
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class EventResponse : Response
    {
        /// <summary>
        /// The token for the event.
        /// </summary>
        public string EventToken { get; set; }
        /// <summary>
        /// The name for the event.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The description for the event.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The start datetime for the event.
        /// </summary>
        public DateTime StartDatetime { get; set; }
        /// <summary>
        ///  The end datetime for the event.
        /// </summary>
        public DateTime EndDatetime { get; set; }
        /// <summary>
        /// The state of the event.
        /// </summary>
        public State State { get; set; }
        /// <summary>
        /// The list of items associated with an event.
        /// </summary>
        public ICollection<EventItem> Items { get; set; }
        /// <summary>
        /// The terms and conditions associated with the event.
        /// </summary>
        public string TermsAndConditionsContent { get; set; }
        /// <summary>
        /// The refund policy associated with the event.
        /// </summary>
        public string RefundPolicyContent { get; set; }

        internal EventResponse(response r) : base(r)
        {
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    internal class event_response : response
    {
        public string event_token { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime start_datetime { get; set; }
        public DateTime end_datetime { get; set; }
        public state state { get; set; }
        public ICollection<EventItem.event_item> items { get; set; }
        public string terms_and_conditions_content { get; set; }
        public string refund_policy_content { get; set; }

        public static explicit operator EventResponse(event_response eventResponse)
        {
            if (eventResponse == null) return null;

            ICollection<EventItem> items = null;
            if (eventResponse.items != null)
                items = eventResponse.items.Select(ei => (EventItem)ei).ToList();

            return new EventResponse(eventResponse)
            {
                Name = eventResponse.name,
                Description = eventResponse.description,
                StartDatetime = eventResponse.start_datetime,
                EndDatetime = eventResponse.end_datetime,
                State = (State)eventResponse.state,
                Items = items,
                TermsAndConditionsContent = eventResponse.terms_and_conditions_content,
                RefundPolicyContent = eventResponse.refund_policy_content,
                EventToken = eventResponse.event_token,
            };
        }
    }
}
