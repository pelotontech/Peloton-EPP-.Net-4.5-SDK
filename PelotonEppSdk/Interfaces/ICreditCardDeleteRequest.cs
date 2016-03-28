using System.Threading.Tasks;
using PelotonEppSdk.Models;

namespace PelotonEppSdk.Interfaces
{
    public interface ICreditCardDeleteRequest
    {
        Task<Response> DeleteAsync();
    }
}