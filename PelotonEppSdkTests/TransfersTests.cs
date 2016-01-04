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
    public class TransfersTests
    {
        [TestMethod]
        public void TestSuccessfulWithoutReferences()
        {
            var factory = new RequestFactory(107, "9cf9b8f4", "PelonEppSdkTests");

            var transfer = factory.GetTransferRequest();
            transfer.Amount = (decimal)0.11;
            transfer.AutoAccept = true;
            transfer.SourceAccountToken = "B4849ED0C336EEE802494A46937B5122";
            transfer.TargetAccountToken = "1D4E237930EB70FC115E6ACD95E878E6";

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
            var factory = new RequestFactory(107, "9cf9b8f4", "PelonEppSdkTests");
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
    }
}
