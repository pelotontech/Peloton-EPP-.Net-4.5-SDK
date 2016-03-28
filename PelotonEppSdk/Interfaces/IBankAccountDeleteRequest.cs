using System.Threading.Tasks;
using PelotonEppSdk.Models;

namespace PelotonEppSdk.Interfaces
{
    public interface IBankAccountDeleteRequest
    {
        string Token { get; set; }

        Task<Response> DeleteAsync();
    }
}