using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PelotonEppSdk.Models
{
    /// <summary>
    /// Data that can be used to verify the account or conditions associated with the account.
    /// Typically a void cheque, PAD agreement or other official bank document.
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Base64 string representation of data.
        /// </summary>
        [Required]
        public string Data { get; set; }

        /// <summary>
        /// Name that the document has when represented on a file system.
        /// </summary>
        [Required, StringLength(128, ErrorMessage = "The " + nameof(Filename) + " field must be less than 128 characters long.")]
        public string Filename { get; set; }

        /// <summary>
        /// Media Type of the Document. Example http://en.wikipedia.org/wiki/Internet_media_type.
        /// </summary>
        [Required, StringLength(256, ErrorMessage = "The " + nameof(MediaType) + " field must be less than 256 characters long.")]
        public string MediaType { get; set; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class document
    {
        public string data { get; set; }

        public string filename { get; set; }

        public string media_type { get; set; }

        public static explicit operator document(Document doc)
        {
            if (doc == null) return null;

            return new document
            {
                data = doc.Data,
                filename = doc.Filename,
                media_type = doc.MediaType
            };
        }
    }
}
