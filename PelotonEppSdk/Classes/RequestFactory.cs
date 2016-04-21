using System;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Interfaces;
using PelotonEppSdk.Models;

namespace PelotonEppSdk.Classes
{
    public class RequestFactory
    {
        private readonly int _clientId;
        private readonly string _clientKey;
        private readonly string _applicationName;
        private readonly LanguageCode _languageCode;
        private readonly Uri _uri;

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

        public FundsTransferNotificationsTokenRequest GetFundsTransferNotificationsTokenRequest()
        {
            var fundsTransferNotificationsToken = new FundsTransferNotificationsTokenRequest();
            SetBaseHeaders(fundsTransferNotificationsToken);
            SetBaseFields(fundsTransferNotificationsToken);
            SetBaseUri(fundsTransferNotificationsToken);
            return fundsTransferNotificationsToken;
        }

        public FundsTransferNotificationsRequest GetFundsTransferNotificationsRequest()
        {
            var fundsTransferNotifications = new FundsTransferNotificationsRequest();
            SetBaseHeaders(fundsTransferNotifications);
            SetBaseFields(fundsTransferNotifications);
            SetBaseUri(fundsTransferNotifications);
            return fundsTransferNotifications;
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
    }
}
