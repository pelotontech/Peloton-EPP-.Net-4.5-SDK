using System.Threading.Tasks;
using PelotonEppSdk.Models;

namespace PelotonEppSdk.Interfaces
{
    public interface ICreditCardDeleteRequest
    {
        string CreditCardToken { get; set; }

        Task<CreditCardResponse> DeleteAsync(bool validate = true);
    }
}