using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PelotonEppSdk.Models
{
    /// <summary>
    /// General Response
    /// </summary>
    public class TransactionResponse : Response
    {
        /// <summary>
        /// This field will return a value for any transaction that occurs, otherwise will be empty.
        /// </summary>
        [StringLength(36)]
        public string TransactionRefCode { get; set; }

        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once MemberCanBeProtected.Global
        public TransactionResponse()
        {
        }

        internal TransactionResponse(response r) : base(r)
        {
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    internal class transaction_response : response
    {
        public string transaction_ref_code { get; set; }

        public static explicit operator TransactionResponse(transaction_response tr)
        {
            if (tr == null) return null;
            return new TransactionResponse(tr)
            {
                TransactionRefCode = tr.transaction_ref_code
            };
        }

        public static explicit operator transaction_response(TransactionResponse tr)
        {
            if (tr == null) return null;
            return new transaction_response
            {
                errors = tr.Errors,
                message = tr.Message,
                message_code = tr.MessageCode,
                success = tr.Success,
                transaction_ref_code = tr.TransactionRefCode
            };
        }
    }
}