using System.Collections.Generic;
using System.Threading.Tasks;
using PelotonEppSdk.Models;

namespace PelotonEppSdk.Interfaces
{
    public interface ICreditCardCreateRequest
    {
        string OrderNumber { get; set; }
        string CardOwner { get; set; }

        CreditCardNumber CardNumber { get; set; }

        string ExpiryMonth { get; set; }
        string ExpiryYear { get; set; }
        string CardSecurityCode { get; set; }

        IEnumerable<Reference> References { get; set; }

        string BillingName { get; set; }
        string BillingPhone { get; set; }
        string BillingEmail { get; set; }
        Address BillingAddress { get; set; }

        bool Verify { get; set; }

        Task<CreditCardResponse> PostAsync();
    }
}