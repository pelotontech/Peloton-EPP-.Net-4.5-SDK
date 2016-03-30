using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
        public void TestCreditCardTokenTransaction()
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
            request.CreditCardToken = "";

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
            Assert.AreEqual("card_number: required for transaction type", result.Errors.Single());
        }

        private static CreditCardTokenTransactionRequest GetBasicRequest()
        {
            //var factory = new RequestFactory(83, "f7117723", "PelonEppSdkTests");
            var factory = new RequestFactory(106, "c57cbd1d", "PelonEppSdkTests");
            //var factory = new RequestFactory(80, "e9ab9532", "PelonEppSdkTests");
            var request = factory.GetCreditCardTransactionRequest();

            request.OrderNumber = "12345678";
            request.CreditCardToken = "2fb92b4fb43a453288b388fcce6659d3"; //  "93610b81fd4749f69b81d7f12286bf61"; //"3aa58fecce92433fbcc17ea9a3e6d923"; // "ae7b55027a6a439ea29a1e2b718e0f8a";// "6fefd54fa8854710a8331797bfd14e3a";
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