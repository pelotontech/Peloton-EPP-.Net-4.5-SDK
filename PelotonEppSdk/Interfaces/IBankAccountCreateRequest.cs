using System.Collections.Generic;
using System.Threading.Tasks;
using PelotonEppSdk.Models;

namespace PelotonEppSdk.Interfaces
{
    public interface IBankAccountCreateRequest
    {
        BankAccount BankAccount { get; set; }

        bool VerifyAccountByDeposit { get; set; }

        Document Document { get; set; }

        IEnumerable<Reference> References { get; set; }

        Task<Response> PostAsync();

    }
}