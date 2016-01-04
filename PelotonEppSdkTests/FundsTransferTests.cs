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
    public class FundsTransferTests
    {
        private static FundsTransferRequest GetBasicRequest()
        {
            var factory = new RequestFactory(107, "9cf9b8f4", "PelonEppSdkTests");
            var transfer = factory.GetFundsTransferRequest();
            transfer.Amount = (decimal)0.01;
            transfer.TransferSystem = FundsTransferSystem.EFT;
            transfer.BankAccountToken = "b9a6db04e0f64d2d958df098b6de1056";
            transfer.AccountToken = "0C01957EE5D8B468342E673CC010BE0A";
            transfer.Type = FundsTransferType.CREDIT;
                new List<Reference>
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
    }
}
