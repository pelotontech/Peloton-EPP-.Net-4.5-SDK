using System.Collections.Generic;
using System.Threading.Tasks;
using PelotonEppSdk.Models;

namespace PelotonEppSdk.Interfaces
{
    public interface ICreditCardUpdateRequest
    {
        string OrderNumber { get; set; }
        string CardOwner { get; set; }

        string ExpiryMonth { get; set; }
        string ExpiryYear { get; set; }

        IEnumerable<Reference> References { get; set; }

        string BillingName { get; set; }
        string BillingPhone { get; set; }
        string BillingEmail { get; set; }
        Address BillingAddress { get; set; }

        Task<CreditCardResponse> PutAsync();

    }
}