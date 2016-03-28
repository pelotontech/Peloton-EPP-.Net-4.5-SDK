using PelotonEppSdk.Enums;
using PelotonEppSdk.Interfaces;
using PelotonEppSdk.Models;

namespace PelotonEppSdk.Classes
{
    public class RequestFactory
    {
        private int _clientId;
        private string _clientKey;
        private string _applicationName;
        private LanguageCode _languageCode;

        public RequestFactory(int clientId, string clientKey, string applicationName, LanguageCode languageCode = LanguageCode.en)
        {
            _clientId = clientId;
            _clientKey = clientKey;
            _applicationName = applicationName;
            _languageCode = languageCode;
        }

        private void SetBaseHeaders(RequestBase request)
        {
            request.SetAuthentication(_clientId.ToString(), _clientKey);
        }

        private void SetBaseFields(RequestBase request)
        {
            request.LanguageCode = _languageCode;
            request.ApplicationName = _applicationName;
        }

        public FundsTransferRequest GetFundsTransferRequest()
        {
            var fundsTransfer = new FundsTransferRequest();
            SetBaseHeaders(fundsTransfer);
            SetBaseFields(fundsTransfer);
            return fundsTransfer;
        }

        public TransferRequest GetTransferRequest()
        {
            var transferRequest = new TransferRequest();
            SetBaseHeaders(transferRequest);
            SetBaseFields(transferRequest);
            return transferRequest;
        }

        public IBankAccountCreateRequest GetBankAccountCreateRequest()
        {
            var bankAccountRequest = new BankAccountRequest();
            SetBaseHeaders(bankAccountRequest);
            SetBaseFields(bankAccountRequest);
            return bankAccountRequest;
        }

        public IBankAccountDeleteRequest GetBankAccountDeleteRequest()
        {
            var bankAccountRequest = new BankAccountRequest();
            SetBaseHeaders(bankAccountRequest);
            SetBaseFields(bankAccountRequest);
            return bankAccountRequest;
        }

        public ICreditCardCreateRequest GetCreditCardCreateRequest()
        {
            var request = new CreditCardRequest();
            SetBaseHeaders(request);
            SetBaseFields(request);
            return request;
        }

        public ICreditCardUpdateRequest GetCreditCardUpdateRequest()
        {
            var request = new CreditCardRequest();
            SetBaseHeaders(request);
            SetBaseFields(request);
            return request;
        }

        public ICreditCardDeleteRequest GetCreditCardDeleteRequest()
        {
            var request = new CreditCardRequest();
            SetBaseHeaders(request);
            SetBaseFields(request);
            return request;
        }

        public CreditCardTokenTransactionRequest GetCreditCardTransactionRequest()
        {
            var request = new CreditCardTokenTransactionRequest();
            SetBaseHeaders(request);
            SetBaseFields(request);
            return request;
        }
    }
}
