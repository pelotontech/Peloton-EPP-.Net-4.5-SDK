using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;

namespace PelotonEppSdk.Models
{
    public class EventRequest : RequestBase
    {
        /// <summary>
        /// The token representing the code that identifies an event
        /// </summary>
        [Required]
        public string Token { get; set; }

        public async Task<EventResponse> GetAsync()
        {
            var client = new PelotonClient();
            var parameter = "?token=" + Token;
            var result =
                await
                    client.GetAsync<event_response>((event_request) this, ApiTarget.Events, parameter)
                        .ConfigureAwait(false);
            return (EventResponse)result;
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
        internal class event_request : request_base
        {
            [Required]
            public string token { get; set; }

            public event_request(RequestBase requestBase) : base(requestBase) { }

            public static explicit operator event_request(EventRequest eventsRequest)
            {
                return new event_request(eventsRequest)
                {
                    token = eventsRequest.Token,
                    application_name = eventsRequest.ApplicationName,
                    authentication_header = eventsRequest.AuthenticationHeader
                };
            }
        }
    }
}
