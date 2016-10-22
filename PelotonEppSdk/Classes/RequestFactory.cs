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

        /// <summary>
        /// Creates a RequestFactory with your provided authentication credentials and target URI
        /// </summary>
        /// <param name="clientId">Your peloton ClientId - this can be found on MyAccount.</param>
        /// <param name="clientKey">The password or key used for authentication - this can also be found on MyAccount</param>
        /// <param name="applicationName">The name of your application. This is used by Peloton for providing support.</param>
        /// <param name="uri">The target URI for performing transactions - e.g. sbapi.peloton-technologies.com, or api.peloton-technologies.com</param>
        /// <param name="languageCode">The language you would like responses in.</param>
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

        public EventRequest GetEventRequest()
        {
            var eventRequest = new EventRequest();
            SetBaseHeaders(eventRequest);
            SetBaseFields(eventRequest);
            SetBaseUri(eventRequest);
            return eventRequest;
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
