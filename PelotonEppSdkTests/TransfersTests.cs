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
    public class TransfersTests: TestBase
    {
        private static TransferRequest GetBasicRequest()
        {
            var factory = new RequestFactory(107, "9cf9b8f4", "PelonEppSdkTests", baseUri);
            var transfer = factory.GetTransferRequest();
            transfer.Amount = (decimal)0.11;
            transfer.AutoAccept = true;
            transfer.SourceAccountToken = "B4849ED0C336EEE802494A46937B5122";
            transfer.TargetAccountToken = "1D4E237930EB70FC115E6ACD95E878E6";
            transfer.References =
                new List<Reference>
                {
                    new Reference {Name = "String 1", Value = "String2"},
                    new Reference {Name = "String 3", Value = "String4"}
                };
            return transfer;
        }

        [TestMethod]
        public void TestSuccessfulWithoutReferences()
        {
            var transfer = GetBasicRequest();
            transfer.References = null;

            var validationResults = transfer.Validate();
            if (validationResults.Any())
            {
                foreach (var validationResult in validationResults)
                {
                    Debug.WriteLine(validationResult);
                }
                Assert.Fail();
            }
            var result = transfer.PostAsync().Result;

            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
            Assert.IsNotNull(result.TransactionRefCode);
        }

        [TestMethod]
        public void TestSuccessfulWithReferences()
        {
            var transfer = GetBasicRequest();

            var errors = new Collection<string>();
            if (!transfer.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }
            var result = transfer.PostAsync().Result;
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
            Assert.IsNotNull(result.TransactionRefCode);
        }

        [TestMethod]
        public void TestFailShortSourceAccountToken()
        {
            var transfer = GetBasicRequest();
            transfer.SourceAccountToken = "4849ED0C336EEE802494A46937B5122";
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("SourceAccountToken must be 32 characters long.", errors.FirstOrDefault());
        }

        [TestMethod]
        public void TestFailShortTargetAccountToken()
        {
            var transfer = GetBasicRequest();
            transfer.TargetAccountToken = "4849ED0C336EEE802494A46937B5122";
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("TargetAccountToken must be 32 characters long.", errors.FirstOrDefault());
        }

        [TestMethod]
        public void TestFailLongSourceAccountToken()
        {
            var transfer = GetBasicRequest();
            transfer.SourceAccountToken = "AAA4849ED0C336EEE802494A46937B5122";
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("SourceAccountToken must be 32 characters long.", errors.FirstOrDefault());
        }

        [TestMethod]
        public void TestFailLongTargetAccountToken()
        {
            var transfer = GetBasicRequest();
            transfer.TargetAccountToken = "AAA4849ED0C336EEE802494A46937B5122";
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("TargetAccountToken must be 32 characters long.", errors.FirstOrDefault());
        }

        [TestMethod]
        public void TestFailNoTargetAccountToken()
        {
            var transfer = GetBasicRequest();
            transfer.TargetAccountToken = null;
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("The TargetAccountToken field is required.", errors.FirstOrDefault());
        }

        [TestMethod]
        public void TestFailNoSourceAccountToken()
        {
            var transfer = GetBasicRequest();
            transfer.SourceAccountToken = null;
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("The SourceAccountToken field is required.", errors.FirstOrDefault());
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
            transfer.Amount = (decimal) 1.001;
            var errors = new Collection<string>();
            if (transfer.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Amount must be a multiple of 0.01.", errors.FirstOrDefault());
        }
    }
}