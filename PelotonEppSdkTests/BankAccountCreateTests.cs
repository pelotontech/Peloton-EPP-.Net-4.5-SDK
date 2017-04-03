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
    public class BankAccountCreateTests: TestBase
    {
        [TestMethod]
        public void TestCreateAccount()
        {
            var createRequest = GetBasicRequest();

            // invent a new bank account token
            createRequest.BankAccount.Token = Guid.NewGuid().ToString("N");

            var errors = new Collection<string>();
            if (!createRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }
            BankAccountCreateResponse result = null;

            try
            {
                result = createRequest.PostAsync().Result;
            }
            catch (AggregateException e)
            {
                // what happened here...
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e);
                Assert.Fail();
            }

            Assert.IsNotNull(result);
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);

        }

        private static BankAccountRequest GetBasicRequest()
        {
            var factory = new RequestFactory(106, "c57cbd1d", "PelonEppSdkTests", baseUri);
            //var factory = new RequestFactory(80, "e9ab9532", "PelonEppSdkTests");
            var createRequest = factory.GetBankAccountRequest();
            createRequest.BankAccount = new BankAccount
            {
                AccountNumber = "1234567890",
                BranchTransitNumber = 1,
                CurrencyCode = "CAD",
                FinancialInstitution = 1,
                Name = "Bank Banktasia",
                Owner = "Unit test SDK",
                Token = "bank account token 1",
                TypeCode = "1"
            };
            createRequest.Document = null; //new Document();
            createRequest.VerifyAccountByDeposit = false;
            
            createRequest.References =
                new List<Reference>
                {
                    new Reference {Name = "String 1", Value = "String2"},
                    new Reference {Name = "String 3", Value = "String4"}
                };
            return (BankAccountRequest)createRequest;
        }
    }
}
