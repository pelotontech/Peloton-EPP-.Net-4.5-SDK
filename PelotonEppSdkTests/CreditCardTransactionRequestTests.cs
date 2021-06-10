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
    public class CreditCardTransactionRequestTests: TestBase
    {

        [TestMethod]
        public void TestCreditCardTransaction()
        {
            var request = GetBasicRequest();
            request.OrderNumber = Guid.NewGuid().ToString("N").Substring(0, 30);

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
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
            Assert.AreEqual(result.Message, "Success");
            Assert.IsTrue(result.Errors == null);
        }

        [TestMethod]
        public void TestCreditCardTransactionMaxOrderNumberLength()
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
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
            Assert.AreEqual(result.Message, "Success");
            Assert.IsTrue(result.Errors == null);
        }

        [TestMethod]
        public void TestCreditCardTransactionValidationError()
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
        public void TestCreditCardTransactionInvalidUsernameAndPassword()
        {
	        var factory = new RequestFactory(106, "wrong password", "PelonEppSdkTests", baseUri);
	        var request = factory.GetUntokenizedCreditCardTransactionRequest();
			request.OrderNumber = Guid.NewGuid().ToString("N").Substring(0,25);
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

        private static CreditCardTransactionRequest GetBasicRequest()
        {
            //var factory = new RequestFactory(106, "c57cbd1d", "PelonEppSdkTests", baseUri);
            var factory = new RequestFactory(107, "9cf9b8f4", "PelonEppSdkTests", baseUri);
            var request = factory.GetUntokenizedCreditCardTransactionRequest();

            request.CardOwner        = "P. Tech.";
            request.CardNumber       = 5499990123456781;
            request.ExpiryMonth      = "12";
            request.ExpiryYear       = DateTime.UtcNow.Year.ToString().Substring(2,2);
            request.CardSecurityCode = "123";

            request.OrderNumber            = "12345678";
            request.Amount                 = (decimal?) 1.00;
            request.Type                   = TransactionType.PURCHASE.ToString();
            request.BillingName            = "P. Tech.";
            request.BillingEmail           = "p.tech@peloton-technologies.com";
            request.BillingPhone           = "250-555-1212";
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