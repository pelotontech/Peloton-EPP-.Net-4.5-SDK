/*****************************************************************
* THIS CLASS IS USED BY THE API AND IS EXTERNALLY FACING         *
* MODIFYING THIS CLASS WILL MODIFY OUR THE API FOR OUR CLIENTS   *
* DO NOT MODIFY THIS CLASS UNLESS YOU KNOW WHAT YOU ARE DOING    *
* PLEASE ENSURE THAT CLIENTS ARE NOTIFIED OF ANY CHANGES         *
*****************************************************************/
// ReSharper disable InconsistentNaming

using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace PelotonEppSdk.RequestsAndResponses
{
    /// <summary>
    /// General Response
    /// </summary>
    internal class response
    {
        /// <summary>
        /// Common response for API transactions
        /// </summary>
        public response()
        {
        }

        /// <summary>
        /// True if an Successful, False otherwise.
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// This field will return an Message regarding the operation in language specified in the request.
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// The code associated with this message.
        /// </summary>
        public int message_code { get; set; }

        /// <summary>
        /// This field will return a value for any transaction that occurs, otherwise will be empty.
        /// </summary>
        public string transaction_ref_code { get; set; }

        /// <summary>
        /// This field will return any validation errors that occured
        /// </summary>
        public ICollection<string> errors { get; set; }
    }
}