using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using NUnit.Framework;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Models;
using PelotonEppSdk.Validations;

namespace PelotonEppSdkTests
{
    [TestFixture]
    public class CreditCardCreateTests: TestBase
    {
        [Test]
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
            Assert.AreEqual("Success", result.Message);
        }

        [Test]
        public void TestCreateCardNoVerifyFr()
        {
            var createRequest = GetBasicRequest(107, "9cf9b8f4", "PelonEppSdkTests", LanguageCode.fr);
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
            Assert.AreEqual("Réussi", result.Message);
        }

        [Test]
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

        [Test]
        public void TestCreateCardValidationRequired()
        {
            var createRequest = GetEmptyRequest(107, "9cf9b8f4", "PelonEppSdkTests");

            var errors = new Collection<string>();
            if (!createRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var errorList = new List<string>();
            var result = createRequest.TryValidatePropertySubset(errorList, RequestMethodAttribute.RequestMethodEnum.POST);
            Assert.IsTrue(result);
            foreach (var item in errorList)
            {
                Debug.WriteLine(item);
            }

            errorList = new List<string>();
            result = createRequest.TryValidatePropertySubset(errorList, RequestMethodAttribute.RequestMethodEnum.PUT);
            Assert.IsFalse(result);
            foreach (var item in errorList)
            {
                Debug.WriteLine(item);
            }

            errorList = new List<string>();
            result = createRequest.TryValidatePropertySubset(errorList, RequestMethodAttribute.RequestMethodEnum.DELETE);
            Assert.IsFalse(result);
            foreach (var item in errorList)
            {
                Debug.WriteLine(item);
            }
        }

        [Test]
        public void TestCreateCardValidationLengths()
        {
            var createRequest = GetEmptyRequest(107, "9cf9b8f4", "PelonEppSdkTests");
            createRequest.CreditCardToken = new string('a', 33);
            createRequest.OrderNumber = new string('a', 51);
            createRequest.CardOwner = new string('a', 27);
            createRequest.ExpiryMonth = new string('a', 3);
            createRequest.ExpiryYear = new string('a', 3);
            createRequest.CardSecurityCode = new string('a', 5);

            var errors = new Collection<string>();
            if (!createRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var errorList = new List<string>();
            var result = createRequest.TryValidatePropertySubset(errorList, RequestMethodAttribute.RequestMethodEnum.POST);
            Assert.IsFalse(result);
            Assert.AreEqual(5, errorList.Count);
            foreach (var item in errorList)
            {
                Debug.WriteLine(item);
                Console.WriteLine(item);
            }

            errorList = new List<string>();
            result = createRequest.TryValidatePropertySubset(errorList, RequestMethodAttribute.RequestMethodEnum.PUT);
            Assert.IsFalse(result);
            Assert.AreEqual(6, errorList.Count);
            foreach (var item in errorList)
            {
                Debug.WriteLine(item);
                Console.WriteLine(item);
            }

            errorList = new List<string>();
            result = createRequest.TryValidatePropertySubset(errorList, RequestMethodAttribute.RequestMethodEnum.DELETE);
            Assert.IsFalse(result);
            Assert.AreEqual(1, errorList.Count);
            foreach (var item in errorList)
            {
                Debug.WriteLine(item);
                Console.WriteLine(item);
            }
        }

        private static CreditCardRequest GetEmptyRequest(int clientid, string clientkey, string applicationName, LanguageCode languageCode = LanguageCode.en)
        {
            var factory = new RequestFactory(clientid, clientkey, applicationName, baseUri, languageCode);
            //var factory = new RequestFactory(80, "e9ab9532", "PelonEppSdkTests");
            var createRequest = factory.GetCreditCardRequest();

            return createRequest;
        }

        private static CreditCardRequest GetBasicRequest(int clientid, string clientkey, string applicationName, LanguageCode languageCode = LanguageCode.en)
        {
            var factory = new RequestFactory(clientid, clientkey, applicationName, baseUri, languageCode);
            //var factory = new RequestFactory(80, "e9ab9532", "PelonEppSdkTests");
            var createRequest = factory.GetCreditCardRequest();

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