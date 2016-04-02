using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Models;

namespace PelotonEppSdkTests
{
    [TestClass]
    public class FundsTransferTests: TestBase
    {
        private static FundsTransferRequest GetBasicRequest()
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var transfer = factory.GetFundsTransferRequest();
            transfer.Amount = (decimal)0.01;
            transfer.TransferSystem = FundsTransferSystem.EFT;
            transfer.BankAccountToken = "b9a6db04e0f64d2d958df098b6de1056";
            transfer.AccountToken = "0C01957EE5D8B468342E673CC010BE0A";
            transfer.Type = FundsTransferType.CREDIT;
            transfer.References = new List<Reference>
                {
                    new Reference {Name = "String 1", Value = "String2"},
                    new Reference {Name = "String 3", Value = "String4"}
                };
            return transfer;
        }

        [TestMethod]
        public void TestSuccessCreditEft()
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
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
            Assert.IsNotNull(result.TransactionRefCode);
        }

        [TestMethod]
        public void TestSuccessNoReferences()
        {
            var request = GetBasicRequest();
            request.References = null;
            var errors = new Collection<string>();
            if (request.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }
            var result = request.PostAsync().Result;
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
            Assert.IsNotNull(result.TransactionRefCode);
        }

        [TestMethod]
        public void TestSuccessDebitEft()
        {
            var request = GetBasicRequest();
            request.Type = FundsTransferType.DEBIT;
            var errors = new Collection<string>();
            if (request.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }
            var result = request.PostAsync().Result;
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
            Assert.IsNotNull(result.TransactionRefCode);
        }

        [TestMethod]
        public void TestFailShortAccountToken()
        {
            var transfer = GetBasicRequest();
            transfer.AccountToken = "ba6db04e0f64d2d958df098b6de1056";
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("AccountToken must be 32 characters long.", errors.FirstOrDefault());
        }

        [TestMethod]
        public void TestFailLongAccountToken()
        {
            var transfer = GetBasicRequest();
            transfer.AccountToken = "aaba6db04e0f64d2d958df098b6de1056";
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("AccountToken must be 32 characters long.", errors.FirstOrDefault());
        }

        [TestMethod]
        public void TestFailNoAccountToken()
        {
            var transfer = GetBasicRequest();
            transfer.AccountToken = null;
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("The AccountToken field is required.", errors.FirstOrDefault());
        }

        [TestMethod]
        public void TestFailShortBankAccountToken()
        {
            var transfer = GetBasicRequest();
            transfer.AccountToken = "C01957EE5D8B468342E673CC010BE0A";
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("AccountToken must be 32 characters long.", errors.FirstOrDefault());
        }

        [TestMethod]
        public void TestFailLongBankAccountToken()
        {
            var transfer = GetBasicRequest();
            transfer.AccountToken = "AAC01957EE5D8B468342E673CC010BE0A";
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("AccountToken must be 32 characters long.", errors.FirstOrDefault());
        }

        [TestMethod]
        public void TestFailNoBankAccountToken()
        {
            var transfer = GetBasicRequest();
            transfer.AccountToken = null;
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("The AccountToken field is required.", errors.FirstOrDefault());
        }

        [TestMethod]
        public void TestFailZeroAmount()
        {
            var transfer = GetBasicRequest();
            transfer.Amount = 0;
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("The field Amount must be between 0.01 and 2147483647.", errors.FirstOrDefault());
        }

        [TestMethod]
        public void TestFailNegativeAmount()
        {
            var transfer = GetBasicRequest();
            transfer.Amount = -1;
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("The field Amount must be between 0.01 and 2147483647.", errors.FirstOrDefault());
        }

        [TestMethod]
        public void TestFailBadAmount()
        {
            var transfer = GetBasicRequest();
            transfer.Amount = (decimal)1.001;
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Amount must be a multiple of 0.01.", errors.FirstOrDefault());
        }

        [TestMethod]
        public void TestFailNoTransferSystem()
        {
            var transfer = GetBasicRequest();
            transfer.TransferSystem = null;
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("The TransferSystem field is required.", errors.FirstOrDefault());
        }

        [TestMethod]
        public void TestFailNoTypeSystem()
        {
            var transfer = GetBasicRequest();
            transfer.Type = null;
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("The Type field is required.", errors.FirstOrDefault());
        }
    }
}
