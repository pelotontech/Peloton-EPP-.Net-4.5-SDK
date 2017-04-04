using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using NUnit.Framework;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Models;

namespace PelotonEppSdkTests
{
    [TestFixture]
    public class CreditCardCreateAndTransactionAndDeleteTests: TestBase
    {

        private long _visaNumber = 4012000033330026;
        private static long _mastercardNumber = 5424180279791732;
        
        private int clientid = 127;
        private string clientkey = "3fe172d4";
        private string applicationName = "PelonEppSdkTests";

        [Test]
        public void TestCreditCardTokenTransactionAndReturn()
        {
            var cardtoken = GetCreditCardToken(clientid, clientkey, applicationName);
            Assert.IsNotNull(cardtoken, "Failed to create credit card token");

            // make a transaction
            var transactionRequest = GetBasicCreditCardTokenTransactionRequest(clientid, clientkey, applicationName);

            transactionRequest.CreditCardToken = cardtoken; // using just-created token
            transactionRequest.Amount = 10;
            transactionRequest.Type = TransactionType.PURCHASE.ToString();

            //transactionRequest.OrderNumber = null;

            var errors = new Collection<string>();
            if (!transactionRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            CreditCardTransactionResponse transactionResult = null;
            try
            {
                transactionResult = transactionRequest.PostAsync().Result;
            }
            catch (Exception e)
            {
                // what happened here...
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e);
                Assert.Fail();
            }
            Debug.WriteLine(transactionResult.Message);
            Debug.WriteLineIf((transactionResult.Errors != null && transactionResult.Errors.Count >= 1), string.Join("; ", transactionResult.Errors ?? new List<string>()));
            Assert.IsTrue(transactionResult.Success);
            Assert.AreEqual(0, transactionResult.MessageCode);
            Assert.IsTrue(transactionResult.Errors == null);

            // make a return
            var returnRequest = GetBasicCreditCardTokenTransactionRequest(clientid, clientkey, applicationName);

            returnRequest.TransactionRefCode = transactionResult.TransactionRefCode;
            returnRequest.CreditCardToken = cardtoken; // using just-created token
            returnRequest.Amount = 10;
            returnRequest.Type = TransactionType.RETURN.ToString();

            errors = new Collection<string>();
            if (!returnRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            CreditCardTransactionResponse returnResult = null;
            try
            {
                returnResult = returnRequest.PostAsync().Result;
            }
            catch (Exception e)
            {
                // what happened here...
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e);
                Assert.Fail();
            }
            Debug.WriteLine(returnResult.Message);
            Debug.WriteLineIf((returnResult.Errors != null && returnResult.Errors.Count >= 1), string.Join("; ", returnResult.Errors ?? new List<string>()));
            Assert.IsTrue(returnResult.Success);
            Assert.AreEqual(0, returnResult.MessageCode);
            Assert.IsTrue(returnResult.Errors == null);

            // clean up the token
            var successfulDelete = DeleteCreditCardToken(cardtoken, clientid, clientkey, applicationName);
            Assert.IsTrue(successfulDelete, "Failed to delete credit card token");
        }

        [Test]
        public void TestCreditCardTokenTransactionWithNullOrderNumber()
        {
            var cardtoken = GetCreditCardToken(clientid, clientkey, applicationName);
            Assert.IsNotNull(cardtoken, "Failed to create credit card token");

            // make a transaction
            var transactionRequest = GetBasicCreditCardTokenTransactionRequest(clientid, clientkey, applicationName);

            transactionRequest.CreditCardToken = cardtoken; // using just-created token
            transactionRequest.Amount = 10;
            transactionRequest.Type = TransactionType.PURCHASE.ToString();

            transactionRequest.OrderNumber = null;

            var errors = new Collection<string>();
            if (!transactionRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            CreditCardTransactionResponse transactionResult = null;
            try
            {
                transactionResult = transactionRequest.PostAsync().Result;
            }
            catch (Exception e)
            {
                // what happened here...
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e);
                Assert.Fail();
            }
            Debug.WriteLine(transactionResult.Message);
            Debug.WriteLineIf((transactionResult.Errors != null && transactionResult.Errors.Count >= 1), string.Join("; ", transactionResult.Errors ?? new List<string>()));
            Assert.IsTrue(transactionResult.Success);
            Assert.AreEqual(0, transactionResult.MessageCode);
            Assert.IsTrue(transactionResult.Errors == null);

            // make a return
            var returnRequest = GetBasicCreditCardTokenTransactionRequest(clientid, clientkey, applicationName);

            returnRequest.TransactionRefCode = transactionResult.TransactionRefCode;
            returnRequest.CreditCardToken = cardtoken; // using just-created token
            returnRequest.Amount = 10;
            returnRequest.Type = TransactionType.RETURN.ToString();

            errors = new Collection<string>();
            if (!returnRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            CreditCardTransactionResponse returnResult = null;
            try
            {
                returnResult = returnRequest.PostAsync().Result;
            }
            catch (Exception e)
            {
                // what happened here...
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e);
                Assert.Fail();
            }
            Debug.WriteLine(returnResult.Message);
            Debug.WriteLineIf((returnResult.Errors != null && returnResult.Errors.Count >= 1), string.Join("; ", returnResult.Errors ?? new List<string>()));
            Assert.IsTrue(returnResult.Success);
            Assert.AreEqual(0, returnResult.MessageCode);
            Assert.IsTrue(returnResult.Errors == null);

            // clean up the token
            var successfulDelete = DeleteCreditCardToken(cardtoken, clientid, clientkey, applicationName);
            Assert.IsTrue(successfulDelete, "Failed to delete credit card token");
        }

        [Test]
        public void TestCreditCardTokenTransactionAndReturnWithNullReturnOrderNumber()
        {
            var cardtoken = GetCreditCardToken(clientid, clientkey, applicationName);
            Assert.IsNotNull(cardtoken, "Failed to create credit card token");

            // make a transaction
            var transactionRequest = GetBasicCreditCardTokenTransactionRequest(clientid, clientkey, applicationName);

            transactionRequest.CreditCardToken = cardtoken; // using just-created token
            transactionRequest.Amount = 10;
            transactionRequest.Type = TransactionType.PURCHASE.ToString();

            var errors = new Collection<string>();
            if (!transactionRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            CreditCardTransactionResponse transactionResult = null;
            try
            {
                transactionResult = transactionRequest.PostAsync().Result;
            }
            catch (Exception e)
            {
                // what happened here...
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e);
                Assert.Fail();
            }
            Debug.WriteLine(transactionResult.Message);
            Debug.WriteLineIf((transactionResult.Errors != null && transactionResult.Errors.Count >= 1), string.Join("; ", transactionResult.Errors ?? new List<string>()));
            Assert.IsTrue(transactionResult.Success);
            Assert.AreEqual(0, transactionResult.MessageCode);
            Assert.IsTrue(transactionResult.Errors == null);

            // make a return
            var returnRequest = GetBasicCreditCardTokenTransactionRequest(clientid, clientkey, applicationName);

            returnRequest.TransactionRefCode = transactionResult.TransactionRefCode;
            returnRequest.CreditCardToken = cardtoken; // using just-created token
            returnRequest.Amount = 10;
            returnRequest.Type = TransactionType.RETURN.ToString();

            returnRequest.OrderNumber = null;

            errors = new Collection<string>();
            if (!returnRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            CreditCardTransactionResponse returnResult = null;
            try
            {
                returnResult = returnRequest.PostAsync().Result;
            }
            catch (Exception e)
            {
                // what happened here...
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e);
                Assert.Fail();
            }
            Debug.WriteLine(returnResult.Message);
            Debug.WriteLineIf((returnResult.Errors != null && returnResult.Errors.Count >= 1), string.Join("; ", returnResult.Errors ?? new List<string>()));
            Assert.IsTrue(returnResult.Success);
            Assert.AreEqual(0, returnResult.MessageCode);
            Assert.IsTrue(returnResult.Errors == null);

            // clean up the token
            var successfulDelete = DeleteCreditCardToken(cardtoken, clientid, clientkey, applicationName);
            Assert.IsTrue(successfulDelete, "Failed to delete credit card token");
        }

        [Test]
        public void TestCreditCardTokenCreateAndDoubleDelete()
        {
            // create a credit card token
            var token = GetCreditCardToken(clientid, clientkey, applicationName);
            Assert.IsNotNull(token, "Failed to create credit card token");

            // first delete of the credit card token
            var successfulDelete = DeleteCreditCardToken(token, clientid, clientkey, applicationName);
            Assert.IsTrue(successfulDelete, "Failed to delete credit card token");

            // second delete of the credit card token
            var deleteRequest = GetBasicCreditCardDeleteRequest(clientid, clientkey, applicationName);

            deleteRequest.CreditCardToken = token;

            var errors = new Collection<string>();
            if (!deleteRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }
            var result = deleteRequest.DeleteAsync().Result;
            Debug.WriteLine(result.Message);
            Debug.WriteLine(result.MessageCode);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);

        }

        private static string GetCreditCardToken(int clientid, string clientkey, string applicationName)
        {
            // create a credit card token
            var createRequest = GetBasicCreditCardRequest(clientid, clientkey, applicationName);
            createRequest.Verify = false;
            createRequest.CardNumber = _mastercardNumber;
            createRequest.CardSecurityCode = "999";


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
            //Assert.IsTrue(result.Success);
            //Assert.AreEqual(0, result.MessageCode);

            return result.CreditCardToken;
        }

        private static bool DeleteCreditCardToken(string token, int clientid, string clientkey, string applicationName)
        {
            // clean away the credit card token
            var deleteRequest = GetBasicCreditCardDeleteRequest(clientid, clientkey, applicationName);

            deleteRequest.CreditCardToken = token;

            var errors = new Collection<string>();
            if (!deleteRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }
            var result = deleteRequest.DeleteAsync().Result;
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            //Assert.IsTrue(result.Success);
            //Assert.AreEqual(0, result.MessageCode);
            return result.Success;
        }


        private static CreditCardRequest GetBasicCreditCardRequest(int clientid, string clientkey, string applicationName)
        {
            var factory = new RequestFactory(clientid, clientkey, applicationName, baseUri);
            //var factory = new RequestFactory(80, "e9ab9532", "PelonEppSdkTests");
            var createRequest = factory.GetCreditCardRequest();

            createRequest.OrderNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 9);
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
            createRequest.ExpiryYear = DateTime.UtcNow.AddYears(1).ToString("yy");
            createRequest.CardSecurityCode = "123";
            createRequest.Verify = true;

            createRequest.References = new List<Reference>
                {
                    new Reference {Name = "reference_1", Value = "String1"},
                    new Reference {Name = "reference_2", Value = "String2"}
                };
            return (CreditCardRequest)createRequest;
        }

        private static CreditCardTokenTransactionRequest GetBasicCreditCardTokenTransactionRequest(int clientid, string clientkey, string applicationName)
        {
            //var factory = new RequestFactory(83, "f7117723", "PelonEppSdkTests");
            var factory = new RequestFactory(clientid, clientkey, applicationName, baseUri);
            //var factory = new RequestFactory(80, "e9ab9532", "PelonEppSdkTests");
            var request = factory.GetCreditCardTransactionRequest();

            request.OrderNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 30);
            request.CreditCardToken = "2fb92b4fb43a453288b388fcce6659d3"; //  "93610b81fd4749f69b81d7f12286bf61"; //"3aa58fecce92433fbcc17ea9a3e6d923"; // "ae7b55027a6a439ea29a1e2b718e0f8a";// "6fefd54fa8854710a8331797bfd14e3a";
            request.Amount = (decimal?)1.00;
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

        private static CreditCardRequest GetBasicCreditCardDeleteRequest(int clientid, string clientkey, string applicationName)
        {
            var factory = new RequestFactory(clientid, clientkey, applicationName, baseUri);
            //var factory = new RequestFactory(80, "e9ab9532", "PelonEppSdkTests");
            var deleteRequest = factory.GetCreditCardRequest();
          
            return (CreditCardRequest)deleteRequest;
        }
    }
}
