using System;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Interfaces;
using PelotonEppSdk.Models;
using PelotonEppSdk.Properties;

namespace PelotonEppSdk.Classes
{
    public class RequestFactory
    {
        private readonly int _clientId;
        private readonly string _clientKey;
        private readonly string _applicationName;
        private readonly LanguageCode _languageCode;
        private readonly Uri _uri;

        public RequestFactory(int clientId, string clientKey, string applicationName, LanguageCode languageCode = LanguageCode.en) : this(clientId, clientKey, applicationName, new Uri(Settings.Default.PelotonUri), languageCode) { }

        public RequestFactory(int clientId, string clientKey, string applicationName, Uri uri, LanguageCode languageCode = LanguageCode.en)
        {
            _clientId = clientId;
            _clientKey = clientKey;
            _applicationName = applicationName;
            _languageCode = languageCode;
            _uri = uri;
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

        private void SetBaseUri(RequestBase request)
        {
            request.BaseUri = _uri;
        }

        public FundsTransferRequest GetFundsTransferRequest()
        {
            var fundsTransfer = new FundsTransferRequest();
            SetBaseHeaders(fundsTransfer);
            SetBaseFields(fundsTransfer);
            SetBaseUri(fundsTransfer);
            return fundsTransfer;
        }

        public TransferRequest GetTransferRequest()
        {
            var transferRequest = new TransferRequest();
            SetBaseHeaders(transferRequest);
            SetBaseFields(transferRequest);
            SetBaseUri(transferRequest);
            return transferRequest;
        }

        public IBankAccountCreateRequest GetBankAccountCreateRequest()
        {
            var bankAccountRequest = new BankAccountRequest();
            SetBaseHeaders(bankAccountRequest);
            SetBaseFields(bankAccountRequest);
            SetBaseUri(bankAccountRequest);
            return bankAccountRequest;
        }

        public IBankAccountDeleteRequest GetBankAccountDeleteRequest()
        {
            var bankAccountRequest = new BankAccountRequest();
            SetBaseHeaders(bankAccountRequest);
            SetBaseFields(bankAccountRequest);
            SetBaseUri(bankAccountRequest);
            return bankAccountRequest;
        }

        public ICreditCardCreateRequest GetCreditCardCreateRequest()
        {
            var request = new CreditCardRequest();
            SetBaseHeaders(request);
            SetBaseFields(request);
            SetBaseUri(request);
            return request;
        }

        public ICreditCardUpdateRequest GetCreditCardUpdateRequest()
        {
            var request = new CreditCardRequest();
            SetBaseHeaders(request);
            SetBaseFields(request);
            SetBaseUri(request);
            return request;
        }

        public ICreditCardDeleteRequest GetCreditCardDeleteRequest()
        {
            var request = new CreditCardRequest();
            SetBaseHeaders(request);
            SetBaseFields(request);
            SetBaseUri(request);
            return request;
        }

        public CreditCardTokenTransactionRequest GetCreditCardTransactionRequest()
        {
            var request = new CreditCardTokenTransactionRequest();
            SetBaseHeaders(request);
            SetBaseFields(request);
            SetBaseUri(request);
            return request;
        }

        public StatementsRequest GetStatementsRequest()
        {
            var request = new StatementsRequest();
            SetBaseHeaders(request);
            SetBaseFields(request);
            SetBaseUri(request);
            return request;
        }
    }
}
