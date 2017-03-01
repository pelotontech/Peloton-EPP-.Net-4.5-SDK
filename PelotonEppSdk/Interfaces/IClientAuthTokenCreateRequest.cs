using System.ComponentModel.DataAnnotations;

namespace PelotonEppSdk.Interfaces
{
    public interface IClientAuthTokenCreateRequest
    {
        /// <summary>
        /// The token used to identify the Peloton account
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 32, ErrorMessage = "AccountToken must be 32 characters in length")]
        string AccountToken { get; set; }

        /// <summary>
        /// The URL used for redirection, when provided.
        /// </summary>
        [StringLength(256)]
        string ReturnUrl { get; set; }
    }
}