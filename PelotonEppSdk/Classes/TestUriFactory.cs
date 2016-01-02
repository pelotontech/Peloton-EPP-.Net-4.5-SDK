using System;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Properties;

namespace PelotonEppSdk.Classes
{
    public class TestUriFactory
    {
        public Uri GetBaseUri()
        {
            return new Uri(Settings.Default.PelotonUri);
        }

        public string GetTargetUriPart(ApiTarget apiTarget)
        {
            switch (apiTarget)
            {
                case ApiTarget.BankAccounts:
                    return "/v1/BankAccounts/";
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