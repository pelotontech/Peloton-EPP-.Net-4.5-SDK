using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Interfaces;
using PelotonEppSdk.Models;

namespace PelotonEppSdkTests
{
    [TestClass]
    public class CreditCardUpdateTests : TestBase
    {
        [TestMethod]
        public void TestCreateUpdateCard()
        {
            var createRequest = GetBasicRequest();

            createRequest.Verify = false;

            var errors = new Collection<string>();
            if (createRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }
            var result = createRequest.PostAsync().Result;
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf(result.Errors != null && result.Errors.Count >= 1, string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);

            ICreditCardUpdateRequest updateRequest = GetBasicRequest();
            updateRequest.CreditCardToken = result.CreditCardToken;
            updateRequest.BillingAddress.Address2 = "123";
            updateRequest.BillingEmail = "p.tech.updated@peloton-technologies.com";

            result = updateRequest.PutAsync().Result;
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
        }

        [TestMethod]
        public void TestCreateUpdateCardNoToken()
        {
            ICreditCardUpdateRequest updateRequest = GetBasicRequest();
            updateRequest.BillingAddress.Address2 = "123";
            updateRequest.BillingEmail = "p.tech.updated@peloton-technologies.com";

            var result = updateRequest.PutAsync().Result;
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsFalse(result.Success);
        }

        private static CreditCardRequest GetBasicRequest()
        {
            var factory = new RequestFactory(106, "c57cbd1d", "PelonEppSdkTests", baseUri);
            //var factory = new RequestFactory(80, "e9ab9532", "PelonEppSdkTests");
            var createRequest = factory.GetCreditCardCreateRequest();

            createRequest.OrderNumber = "12345678";
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
            createRequest.CardNumber = 123412341234;
            createRequest.ExpiryMonth = "09";
            createRequest.ExpiryYear = "17";
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
