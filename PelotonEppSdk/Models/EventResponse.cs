using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using PelotonEppSdk.Enums;

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

        public string FriendlyUrlPath { get; set; }
        /// <summary>
        /// The description for the event.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The start datetime for the event.
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        ///  The end datetime for the event.
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// The state of the event.
        /// </summary>
        public EventStateEnum State { get; set; }
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
        public string friendly_url_path { get; set; }
        public string description { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
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

            var eventState = (State)eventResponse.state;
            EventStateEnum eventStateEnumValue = EventStateEnum.Unknown;
            if (eventState != null)
            {
                eventStateEnumValue = (EventStateEnum)int.Parse(eventState.Code);
            }

            return new EventResponse(eventResponse)
            {
                Name = eventResponse.name,
                FriendlyUrlPath = eventResponse.friendly_url_path,
                Description = eventResponse.description,
                StartDate = eventResponse.start_date,
                EndDate = eventResponse.end_date,
                State = eventStateEnumValue,
                Items = items,
                TermsAndConditionsContent = eventResponse.terms_and_conditions_content,
                RefundPolicyContent = eventResponse.refund_policy_content,
                EventToken = eventResponse.event_token,
            };
        }
    }
}
