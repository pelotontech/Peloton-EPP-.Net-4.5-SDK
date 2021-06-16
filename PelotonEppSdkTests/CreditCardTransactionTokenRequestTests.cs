using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Models;

namespace PelotonEppSdkTests
{
	[TestClass]
    public class CreditCardTransactionTokenRequestTests: TestBase
    {

        [TestMethod]
        public void TestCreditCardTokenTransaction()
        {
            var request = GetBasicRequest();

            var errors = new Collection<string>();
            if (!request.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            CreditCardTransactionResponse result = null;
            try
            {
                result = request.PostAsync().Result;
            }
            catch (Exception e)
            {
                // what happened here...
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e);
                //Debug.WriteLineIf((esult.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
                Assert.Fail();
            }
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            // this transaction fails because the card token is not related to a card that actually accepts transactions.
            Assert.IsFalse(result.Success);
            Assert.AreEqual(741, result.MessageCode);
            Assert.AreEqual(result.Message, "The transaction was declined by your financial institution. Please contact your financial institution for further information.");
            Assert.IsTrue(result.Errors == null);
        }

        [TestMethod]
        public void TestCreditCardTokenTransactionMaxOrderNumberLength()
        {
            var request = GetBasicRequest();
            request.OrderNumber = Guid.NewGuid().ToString("N").Substring(0, 30);
            var errors = new Collection<string>();
            if (request.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            CreditCardTransactionResponse result = null;
            try
            {
                result = request.PostAsync().Result;
            }
            catch (Exception e)
            {
                // what happened here...
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e);
                //Debug.WriteLineIf((esult.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
                Assert.Fail();
            }
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            // this transaction fails because the card token is not related to a card that actually accepts transactions.
            Assert.IsFalse(result.Success);
            Assert.AreEqual(741, result.MessageCode);
            Assert.AreEqual(result.Message, "The transaction was declined by your financial institution. Please contact your financial institution for further information.");
            Assert.IsTrue(result.Errors == null);
        }

        [TestMethod]
        public void TestCreditCardTokenTransactionValidationError()
        {
            var request = GetBasicRequest();
            request.Amount = null;

            var errors = new Collection<string>();
            if (!request.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            CreditCardTransactionResponse result = null;
            try
            {
                result = request.PostAsync().Result;
            }
            catch (AggregateException e)
            {
                // what happened here...
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e);
            }
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.MessageCode);
            Assert.AreEqual(result.Message, "Validation Error");
            Assert.IsTrue(result.Errors != null);
            Assert.IsTrue(result.Errors.Count == 1);
            Assert.AreEqual("amount: required", result.Errors.Single());
        }

	    [TestMethod]
        public void TestCreditCardTokenTransactionInvalidUsernameAndPassword()
        {
	        var factory = new RequestFactory(106, "wrong password", "PelonEppSdkTests", baseUri);
	        var request = factory.GetCreditCardTransactionRequest();
			request.OrderNumber = Guid.NewGuid().ToString("N").Substring(0,25);
	        request.CreditCardToken = "2fb92b4fb43a453288b388fcce6659d3";
            request.Amount = (decimal?) 1.00;
            request.Type = TransactionType.PURCHASE.ToString();

            var errors = new Collection<string>();
            if (!request.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            CreditCardTransactionResponse result = null;
            try
            {
                result = request.PostAsync().Result;

                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("One or more errors occurred.", e.Message);
                Assert.AreEqual("Invalid username or password", e.InnerException.Message);
                Debug.WriteLine("test successful");
                return;
            }

	        Assert.Fail();
        }

        private static CreditCardTokenTransactionRequest GetBasicRequest()
        {
            //var factory = new RequestFactory(106, "c57cbd1d", "PelonEppSdkTests", baseUri);
            var factory = new RequestFactory(107, "9cf9b8f4", "PelonEppSdkTests", baseUri);
            var request = factory.GetCreditCardTransactionRequest();

            request.AccountToken = "1D4E237930EB70FC115E6ACD95E878E6";

            request.OrderNumber = "12345678";
            request.CreditCardToken = "70401f6f4f684ab4a24728181f368d7c"; //"2fb92b4fb43a453288b388fcce6659d3"; //  "93610b81fd4749f69b81d7f12286bf61"; //"3aa58fecce92433fbcc17ea9a3e6d923"; // "ae7b55027a6a439ea29a1e2b718e0f8a";// "6fefd54fa8854710a8331797bfd14e3a";
            request.Amount = (decimal?) 1.00;
            request.Type = TransactionType.PURCHASE.ToString();
            request.BillingName = "P. Tech.";
            request.BillingEmail = "p.tech@peloton-technologies.com";
            request.BillingPhone = "250-555-1212";
            request.BillingAddress = new Address()
            {
                Address1 = "234 Testing Street",
                Address2 = "",
                City = "Victoria",
                PostalZipCode = "A1A1A1",
                CountryCode = "CA",
                ProvinceStateCode = "BC"
            };
            
            request.ShippingName = "P. Tech.";
            request.ShippingEmail = "p.tech@peloton-technologies.com";
            request.ShippingPhone = "250-555-1212";
            request.ShippingAddress = new Address()
            {
                Address1 = "234 Testing Street",
                Address2 = "",
                City = "Victoria",
                PostalZipCode = "A1A1A1",
                CountryCode = "CA",
                ProvinceStateCode = "BC"
            };

            request.References = new List<Reference>
                {
                    new Reference {Name = "reference_1", Value = "String1"},
                    new Reference {Name = "reference_2", Value = "String2"}
                };
            return request;
        }
    }
}