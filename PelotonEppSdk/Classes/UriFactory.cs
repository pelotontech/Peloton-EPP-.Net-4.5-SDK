using System;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Properties;

namespace PelotonEppSdk.Classes
{
    internal class UriFactory
    {
        public Uri GetBaseUri()
        {
            //return new Uri(Settings.Default.PelotonUri);
            return new Uri("http://localhost:2590");
        }

        public string GetTargetUriPart(ApiTarget apiTarget)
        {
            switch (apiTarget)
            {
                case ApiTarget.BankAccounts:
                    return "/v1/BankAccounts/";
                case ApiTarget.CreditCards:
                    return "/v1/CreditCards/";
                case ApiTarget.CreditCardTransactions:
                    return "/v1/CreditCardTransactions/";
                case ApiTarget.FundsTransfers:
                    return "/v1/FundsTransfers/";
                case ApiTarget.Transfers:
                    return "/v1/Transfers/";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}