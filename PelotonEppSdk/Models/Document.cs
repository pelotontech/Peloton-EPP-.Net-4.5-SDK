using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PelotonEppSdk.Models
{
    /// <summary>
    /// Value is important, therefore order must be aligned with DocumentType table in db.
    /// </summary>
    public enum DocumentType
    {
        BankAccountVerification,
        NotificationTemplate,
        News
    }

    public class Document
    {
        public string Data { get; set; }

        public string Filename { get; set; }

        public string MediaType { get; set; }

        public DocumentType? Type { get; set; }
    }

    /// <summary>
    /// Data that can be used to verify the account or conditions associated with the account.
    /// Typically a void cheque, PAD agreement or other official bank document.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class document
    {
        /// <summary>
        /// Base64 string representation of data.
        /// </summary>
        [Required]
        public string data { get; set; }

        /// <summary>
        /// Name that the document has when represented on a file system.
        /// </summary>
        [Required, StringLength(128, ErrorMessage = "The " + nameof(filename) + " field must be less than 128 characters long.")]
        public string filename { get; set; }

        /// <summary>
        /// Media Type of the Document. Example http://en.wikipedia.org/wiki/Internet_media_type.
        /// </summary>
        [Required, StringLength(256, ErrorMessage = "The " + nameof(media_type) + " field must be less than 256 characters long.")]
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
