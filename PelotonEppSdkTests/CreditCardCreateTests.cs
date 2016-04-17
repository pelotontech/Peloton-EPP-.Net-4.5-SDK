using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Models;

namespace PelotonEppSdkTests
{
    [TestClass]
    public class CreditCardCreateTests: TestBase
    {
        [TestMethod]
        public void TestCreateCardNoVerify()
        {
            var createRequest = GetBasicRequest(107, "9cf9b8f4", "PelonEppSdkTests");
            createRequest.Verify = false;

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
        }

        [TestMethod]
        public void TestCreateCardVerify()
        {
            var createRequest = GetBasicRequest(107, "9cf9b8f4", "PelonEppSdkTests");
            createRequest.CardNumber = 5499990123456781;
            createRequest.Verify = true;

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
        }

        private static CreditCardRequest GetBasicRequest(int clientid, string clientkey, string applicationName)
        {
            var factory = new RequestFactory(clientid, clientkey, applicationName, baseUri);
            //var factory = new RequestFactory(80, "e9ab9532", "PelonEppSdkTests");
            var createRequest = factory.GetCreditCardCreateRequest();

            createRequest.OrderNumber = Guid.NewGuid().ToString().Replace("-","").Substring(0,9);
            createRequest.BillingName = "P. Tech.";
            createRequest.BillingEmail = "p.tech@peloton-technologies.com";
            createRequest.BillingPhone = "250-555-1212";
            createRequest.BillingAddress = new Address()
            {
                Address1 = "234 Testing Street",
                Address2 = "",
                City = "Victoria",
                PostalZipCode = "A1A1A1",
                CountryCode = "CA",
                ProvinceStateCode = "BC"
            };
            createRequest.CardOwner = "P. Tech.";
            createRequest.CardNumber = 5499990123456781;
            createRequest.ExpiryMonth = "12";
            createRequest.ExpiryYear = "16";
            createRequest.CardSecurityCode = "123";
            createRequest.Verify = true;

            createRequest.References = new List<Reference>
            	{
                    new Reference {Name = "reference_1", Value = "String1"},
                    new Reference {Name = "reference_2", Value = "String2"}
                };
            return (CreditCardRequest)createRequest;
        }
    }
}