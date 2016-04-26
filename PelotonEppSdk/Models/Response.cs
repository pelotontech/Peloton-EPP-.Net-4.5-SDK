using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace PelotonEppSdk.Models
{
    /// <summary>
    /// General Response
    /// </summary>
    public class Response
    {
        /// <summary>
        /// True if an Successful, False otherwise.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// This field will return an Message regarding the operation in language specified in the request.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The code associated with this message.
        /// </summary>
        public int MessageCode { get; set; }

        /// <summary>
        /// This field will return a value for any transaction that occurs, otherwise will be empty.
        /// </summary>
        public string TransactionRefCode { get; set; }

        /// <summary>
        /// This field will return any validation errors that occured
        /// </summary>
        public ICollection<string> Errors { get; set; }

        public Response() { }

        internal Response(response r)
        {
            Success = r.success;
            Message = r.message;
            MessageCode = r.message_code;
            TransactionRefCode = r.transaction_ref_code;
            Errors = r.errors.ToList(); // copy the errors into a new object
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class response
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int message_code { get; set; }
        public string transaction_ref_code { get; set; }
        public ICollection<string> errors { get; set; }

        public static explicit operator Response(response r)
        {
            if (r == null) return null;
            return new Response
            {
                Success = r.success,
                Message = r.message,
                Errors = r.errors,
                MessageCode = r.message_code,
                TransactionRefCode = r.transaction_ref_code
            };
        }

        public static explicit operator response(Response r)
        {
            if (r == null) return null;
            return new response
            {
                errors = r.Errors,
                message = r.Message,
                message_code = r.MessageCode,
                success = r.Success,
                transaction_ref_code = r.TransactionRefCode
            };
        }
    }
}