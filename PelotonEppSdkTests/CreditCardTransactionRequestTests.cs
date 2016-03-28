using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Models;

namespace PelotonEppSdkTests
{
	[TestClass]
    public class CreditCardTransactionRequestTests
    {

        [TestMethod]
        public void TestCreditCardTransaction()
        {
            var request = GetBasicRequest();

            var errors = new Collection<string>();
            if (request.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }
            var result = request.PostAsync().Result;
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
        }

        private static CreditCardTokenTransactionRequest GetBasicRequest()
        {
            var factory = new RequestFactory(106, "c57cbd1d", "PelonEppSdkTests");
            //var factory = new RequestFactory(80, "e9ab9532", "PelonEppSdkTests");
            var request = factory.GetCreditCardTransactionRequest();

            request.OrderNumber = "12345678";
            request.CreditCardToken = null;
            request.Amount = (decimal?) 0.01;
            request.Type = TransactionType.Purchase.ToString();
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