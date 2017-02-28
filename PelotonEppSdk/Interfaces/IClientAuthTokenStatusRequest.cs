using System.ComponentModel.DataAnnotations;

namespace PelotonEppSdk.Interfaces
{
    public interface IClientAuthTokenStatusRequest
    {
        [StringLength(32, MinimumLength = 32, ErrorMessage = "ClientAuthToken must be 32 characters in length")]
        string ClientAuthToken { get; set; }
    }
}