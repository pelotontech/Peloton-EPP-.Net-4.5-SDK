using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Models;

namespace PelotonEppSdkTests
{
    [TestFixture]
    public class ClientAuthTokenTests: TestBase
    {
        // For testapi, use id 107 and key "9cf9b8f4",
        private int _clientId = 107;
        private string _clientKey = "9cf9b8f4";
        private string _accountToken = "1D4E237930EB70FC115E6ACD95E878E6";


        [Test]
        public void TestCreateClientAuthToken()
        {
            var createRequest = GetBasicRequest(_clientId, _clientKey, _accountToken, "PelonEppSdkTests");

            var errors = new Collection<string>();
            if (!createRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }
            var result = createRequest.PostAsync().Result;
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
            Assert.AreEqual("Success", result.Message);
            Assert.AreEqual(32, result.ClientAuthToken.Length);
        }

        [Test]
        public void TestCreateClientAuthTokenAccountTokenInvalid()
        {
            var createRequest = GetBasicRequest(_clientId, _clientKey, _accountToken, "PelonEppSdkTests");

            createRequest.AccountToken = "a garbage valued account token";

            var errors = new Collection<string>();
            if (!createRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            Assert.AreEqual("AccountToken must be 32 characters in length", errors.Single());
        }

        [Test]
        public void TestCreateClientAuthTokenAccountTokenInvalid2()
        {
            var createRequest = GetBasicRequest(_clientId, _clientKey, _accountToken, "PelonEppSdkTests");

            createRequest.AccountToken = new string('t', 32);

            var errors = new Collection<string>();
            if (!createRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var result = createRequest.PostAsync().Result;
            Debug.WriteLine(result.Message);
            Debug.WriteLine(result.MessageCode);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.MessageCode);
            Assert.AreEqual("Validation Error", result.Message);
            Assert.IsTrue(string.IsNullOrEmpty(result.ClientAuthToken));
            Assert.AreEqual("account_token: invalid", result.Errors.Single());
        }

        [Test]
        public void TestCreateClientAuthTokenAccountTokenNull()
        {
            var createRequest = GetBasicRequest(_clientId, _clientKey, _accountToken, "PelonEppSdkTests");

            createRequest.AccountToken = null;

            var errors = new Collection<string>();
            if (!createRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            Assert.AreEqual("The AccountToken field is required.", errors.Single());
        }

        [Test]
        public void TestGetClientAuthToken()
        {
            var createRequest = GetBasicRequest(_clientId, _clientKey, _accountToken, "PelonEppSdkTests");

            var errors = new Collection<string>();
            if (!createRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }
            var result = createRequest.PostAsync().Result;
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
            Assert.AreEqual("Success", result.Message);
            Assert.AreEqual(32, result.ClientAuthToken.Length);

            var cat = result.ClientAuthToken;
            var getRequest = GetBasicRequest(_clientId, _clientKey, _accountToken, "PelonEppSdkTests");

            getRequest.ReturnUrl = null;
            getRequest.ClientAuthToken = cat;
            var getResult = getRequest.GetAsync().Result;
            Debug.WriteLine(getResult.Message);
            Debug.WriteLineIf((getResult.Errors != null && getResult.Errors.Count >= 1), string.Join("; ", getResult.Errors ?? new List<string>()));
            Assert.IsTrue(getResult.Success);
            Assert.AreEqual(0, getResult.MessageCode);
            Assert.AreEqual("Success", getResult.Message);
            Assert.AreEqual(32, getResult.ClientAuthToken.Length);
            Assert.IsTrue(getResult.Errors == null || !getResult.Errors.Any());
            Assert.AreEqual(createRequest.ReturnUrl, getResult.ReturnUrl);
            Assert.IsTrue(getResult.Active);
            Assert.AreEqual(null, getResult.AuthorizedDatetime);
            Assert.AreEqual(null, getResult.CardToken);
            Assert.AreEqual(null, getResult.BankAccountToken);
            Assert.AreEqual(null, getResult.TransactionRefCode);
            Assert.AreEqual(null, getResult.EventToken);
        }

        [Test]
        public void TestGetClientAuthTokenExpired()
        {
            // expired: 5a1c7be3998a4205a2b1c8c49a0e4599
            var cat = "ef273d91c8214d6ea463517aaf61e514";
            var getRequest = GetBasicRequest(_clientId, _clientKey, _accountToken, "PelonEppSdkTests");

            getRequest.ReturnUrl = null;
            getRequest.ClientAuthToken = cat;
            var getResult = getRequest.GetAsync().Result;
            Debug.WriteLine(getResult.Message);
            Debug.WriteLineIf((getResult.Errors != null && getResult.Errors.Count >= 1), string.Join("; ", getResult.Errors ?? new List<string>()));
            Assert.IsTrue(getResult.Success);
            Assert.AreEqual(0, getResult.MessageCode);
            Assert.AreEqual("Success", getResult.Message);
            Assert.AreEqual(32, getResult.ClientAuthToken.Length);
            Assert.IsTrue(getResult.Errors == null || !getResult.Errors.Any());
            Assert.AreEqual("Keep for Testing", getResult.ReturnUrl);
            Assert.IsFalse(getResult.Active);
            Assert.AreEqual(null, getResult.AuthorizedDatetime);
            Assert.AreEqual(null, getResult.CardToken);
            Assert.AreEqual(null, getResult.BankAccountToken);
            Assert.AreEqual(null, getResult.TransactionRefCode);
            Assert.AreEqual(null, getResult.EventToken);
        }


        [Test]
        public void TestGetClientAuthTokenAuthorizedCreditCardToken()
        {
            // authorized: 8c0a15527850422ea505b9bbda076eb4
            var cat = "21701fbd4b2044f2aee48d554dd52008";
            var getRequest = GetBasicRequest(_clientId, _clientKey, _accountToken, "PelonEppSdkTests");

            getRequest.ReturnUrl = null;
            getRequest.ClientAuthToken = cat;
            var getResult = getRequest.GetAsync().Result;
            Debug.WriteLine(getResult.Message);
            Debug.WriteLineIf((getResult.Errors != null && getResult.Errors.Count >= 1), string.Join("; ", getResult.Errors ?? new List<string>()));
            Assert.IsTrue(getResult.Success);
            Assert.AreEqual(0, getResult.MessageCode);
            Assert.AreEqual("Success", getResult.Message);
            Assert.AreEqual(32, getResult.ClientAuthToken.Length);
            Assert.IsTrue(getResult.Errors == null || !getResult.Errors.Any());
            Assert.AreEqual("Keep for Testing", getResult.ReturnUrl);
            Assert.IsFalse(getResult.Active);
            Assert.AreEqual(ClientAuthTokenAuthorizationType.Card, getResult.Type);
            Assert.AreEqual("2017-03-07 02:04:14", getResult.AuthorizedDatetime.ToString());
            Assert.AreEqual("0d1d2933c4104796815bf2ab98f3e6d9", getResult.CardToken);
            Assert.AreEqual(null, getResult.BankAccountToken);
            Assert.AreEqual(null, getResult.TransactionRefCode);
            Assert.AreEqual(null, getResult.EventToken);
        }
        

        private static ClientAuthTokenRequest GetBasicRequest(int clientid, string clientkey, string accountToken, string applicationName, LanguageCode languageCode = LanguageCode.en)
        {
            var factory = new RequestFactory(clientid, clientkey, applicationName, baseUri, languageCode);
            var createRequest = factory.GetClientAuthTokenRequest();

            createRequest.AccountToken = accountToken;
            createRequest.ReturnUrl = "https://peloton-technologies.com/";

            return createRequest;
        }
    }
}