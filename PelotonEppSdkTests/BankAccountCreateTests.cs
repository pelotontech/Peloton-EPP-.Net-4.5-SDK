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
    public class BankAccountCreateTests
    {
        [TestMethod]
        public void TestCreateAccount()
        {
            var createRequest = GetBasicRequest();

            var errors = new Collection<string>();
            if (createRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }
            var result = createRequest.PostAsync().Result;
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
            Assert.IsNotNull(result.TransactionRefCode);

        }

        private static BankAccountCreateRequest GetBasicRequest()
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests");
            var createRequest = factory.GetBankAccountCreateRequest();
            createRequest.BankAccount = new BankAccount
            {
                AccountNumber = "1",
                BranchTransitNumber = 1,
                CurrencyCode = "CAD",
                FinancialInstitution = 1,
                Name = "Bank Banktasia",
                Owner = "Unit test SDK",
                Token = "bank token 1",
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
            return createRequest;
        }
    }
}
