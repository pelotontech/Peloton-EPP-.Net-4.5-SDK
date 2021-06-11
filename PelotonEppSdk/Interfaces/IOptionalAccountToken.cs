using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PelotonEppSdk.Interfaces
{
    public interface IOptionalAccountToken
    {
        /// <summary>
        /// The token used to identify the Peloton account.
        /// Required when more than one account is available.
        /// </summary>
        [StringLength(32, MinimumLength = 32)]
        string AccountToken { get; set; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public interface Ioptional_account_token
    {
        string account_token { get; set; }
    }
}