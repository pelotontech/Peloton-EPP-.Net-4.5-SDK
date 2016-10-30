using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;

namespace PelotonEppSdk.Models
{
    public class EventRequest : RequestBase
    {
        public string Token { private get; set; }
        public string Name { private get; set; }
        public string FriendlyUrlPath { private get; set; }
        public string Description { private get; set; }
        public DateTime StartDatetime { private get; set; }
        public DateTime EndDatetime { private get; set; }
        public State State { get; set; }
        public ICollection<EventItem> Items { get; set; }
        public string TermsAndConditionsContent { private get; set; }
        public string RefundPolicyContent { private get; set; }
        public string LanguageCode { get; set; }

        public async Task<EventResponse> GetAsync()
        {
            var client = new PelotonClient();
            var parameter = "?token=" + Token;
            parameter += "&languageCode=" + LanguageCode;
            var result =
                await
                    client.GetAsync<event_response>((event_request) this, ApiTarget.Events, parameter)
                        .ConfigureAwait(false);
            return (EventResponse)result;
        }

        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is not <c>2XX Success</c>.</exception>
        public async Task<EventResponse> PostAsync()
        {
            var client = new PelotonClient();
            var request = (event_request)this;
            var result = await client.PostAsync<event_response>(request, ApiTarget.Events).ConfigureAwait(false);
            return (EventResponse)result;
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
        internal class event_request : request_base
        {
            /// <summary>
            /// The token for the event.
            /// </summary>
            [Required]
            public string token { get; set; }
            /// <summary>
            /// The name for the event.
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// The friendly url for the event.
            /// </summary>
            public string friendly_url_path { get; set; }
            /// <summary>
            /// The description for the event.
            /// </summary>
            public string description { get; set; }
            /// <summary>
            /// The start datetime for the event.
            /// </summary>
            public DateTime start_datetime { get; set; }
            /// <summary>
            ///  The end datetime for the event.
            /// </summary>
            public DateTime end_datetime { get; set; }
            /// <summary>
            /// The state of the event.
            /// </summary>
            // public state state { get; set; }
            /// <summary>
            /// The list of items associated with an event.
            /// </summary>
            public ICollection<EventItem.event_item> items { get; set; }
            /// <summary>
            /// The terms and conditions associated with the event.
            /// </summary>
            public string terms_and_conditions_content { get; set; }
            /// <summary>
            /// The refund policy associated with the event.
            /// </summary>
            public string refund_policy_content { get; set; }

            public event_request(RequestBase requestBase) : base(requestBase) { }

            public static explicit operator event_request(EventRequest eventsRequest)
            {
                return new event_request(eventsRequest)
                {
                    application_name = eventsRequest.ApplicationName,
                    authentication_header = eventsRequest.AuthenticationHeader,
                    name = eventsRequest.Name,
                    description = eventsRequest.Description,
                    friendly_url_path = eventsRequest.FriendlyUrlPath,
                    start_datetime = eventsRequest.StartDatetime,
                    end_datetime = eventsRequest.EndDatetime,
                    terms_and_conditions_content = eventsRequest.TermsAndConditionsContent,
                    refund_policy_content = eventsRequest.TermsAndConditionsContent
                };
            }
        }
    }
}
