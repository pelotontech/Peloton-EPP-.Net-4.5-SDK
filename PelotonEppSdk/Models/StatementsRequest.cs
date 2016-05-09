using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;

namespace PelotonEppSdk.Models
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class StatementsRequest : RequestBase
    {
        /// <summary>
        /// The Account token for the account which the statement is being created for
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 32, ErrorMessage = "AccountToken must be 32 characters in length")]
        public string AccountToken { get; set; }

        /// <summary>
        /// The UTC timestamp for the opening date of the statement
        /// </summary>
        [Required]
        public DateTime FromDate { get; set; }

        /// <summary>
        /// The UTC timestamp for ending of the statement
        /// </summary>
        [Required]
        public DateTime ToDate { get; set; }

        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is not <c>2XX Success</c>.</exception>
        public async Task<StatementsResponse> PostAsync()
        {
            var client = new PelotonClient();
            var request = (statements_request)this;
            var result = await client.PostAsync<statements_response>(request, ApiTarget.Statements).ConfigureAwait(false);
            return (StatementsResponse)result;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class statements_request : request_base
    {
        protected statements_request(StatementsRequest requestBase) : base(requestBase)
        {
            account_token = requestBase.AccountToken;
            from_date_utc = requestBase.FromDate;
            to_date_utc = requestBase.ToDate;
        }

        public static explicit operator statements_request(StatementsRequest sr)
        {
            return new statements_request(sr);
        }

        /// <summary>
        /// currently support only the authenticated client
        /// </summary>
        public string account_token { get; set; }

        /// <summary>
        /// The UTC datetime 
        /// </summary>
        public DateTime from_date_utc { get; set; }
        public DateTime to_date_utc { get; set; }
    }
}
