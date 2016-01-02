using PelotonEppSdk.Models;

namespace PelotonEppSdk.Classes
{
    public class RequestFactory
    {
        private int _clientId;
        private string _clientKey;
        private string _applicationName;
        private string _languageCode;

        public RequestFactory(int clientId, string clientKey, string applicationName, string languageCode = "en")
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



        public TransferRequest GetTransferRequest()
        {
            var transferRequest = new TransferRequest {ApplicationName = _applicationName};
            SetBaseHeaders(transferRequest);
            SetBaseFields(transferRequest);
            return transferRequest;
        }
    }
}
