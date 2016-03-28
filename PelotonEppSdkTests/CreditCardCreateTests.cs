using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Remoting.Channels;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Models;

namespace PelotonEppSdkTests
{
    [TestClass]
    public class CreditCardCreateTests
    {

        [TestMethod]
        public void TestCreateCard()
        {
            var createRequest = GetBasicRequest();

            // at this point in time, March 20th, 2016, most card verification is not implemented, so prevent the verification process
            // TODO: when card verification is implemented, then use Verify = true here
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
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
        }

        private static CreditCardRequest GetBasicRequest()
        {
            //var factory = new RequestFactory(106, "c57cbd1d", "PelonEppSdkTests");
            var factory = new RequestFactory(80, "e9ab9532", "PelonEppSdkTests");
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