using System;
using PelotonEppSdk.Enums;

namespace PelotonEppSdk.Classes
{
    internal class UriFactory
    {
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
                case ApiTarget.FundsTransferNotifications:
                    return "/v1/FundsTransferNotifications/";
                case ApiTarget.Transfers:
                    return "/v1/Transfers/";
                case ApiTarget.Statements:
                    return "/v1/Statements/";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}