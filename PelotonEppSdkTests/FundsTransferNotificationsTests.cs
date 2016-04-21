using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Models;

namespace PelotonEppSdkTests
{
    [TestClass]
    public class FundsTransferNotificationsTests : TestBase
    {
        private static FundsTransferNotificationsTokenRequest GetBasicTokenRequest()
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var transferNotificationsToken = factory.GetFundsTransferNotificationsTokenRequest();
            transferNotificationsToken.FromDateUtc = DateTime.UtcNow.ToString("yyyy-MM-dd");
            return transferNotificationsToken;
        }

        private static FundsTransferNotificationsRequest GetBasicRequest(string token, int items = 100)
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var transferNotifications = factory.GetFundsTransferNotificationsRequest();
            transferNotifications.Token = token;
            transferNotifications.Items = items;
            return transferNotifications;
        }

        [TestMethod]
        public void TestFailSettingTokenNoDate()
        {
            var tokenRequest = GetBasicTokenRequest();
            tokenRequest.FromDateUtc = null;
            var errors = new Collection<string>();
            if (tokenRequest.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("The FromDateUtc field is required.", errors.FirstOrDefault());
        }

        [TestMethod]
        public void TestSuccessSettingToken()
        {
            var request = GetBasicTokenRequest();
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
            Assert.IsNotNull(result.Token);
            Assert.IsNotNull(result.TransactionRefCode);
        }

        //[TestMethod]
        //public void TestSuccessNoReferences()
        //{
        //    var request = GetBasicRequest();
        //    request.References = null;
        //    var errors = new Collection<string>();
        //    if (request.TryValidate(errors))
        //    {
        //        foreach (var error in errors)
        //        {
        //            Debug.WriteLine(error);
        //        }
        //    }
        //    var result = request.PostAsync().Result;
        //    Assert.IsTrue(result.Success);
        //    Assert.AreEqual(0, result.MessageCode);
        //    Assert.IsNotNull(result.TransactionRefCode);
        //}

        //[TestMethod]
        //public void TestSuccessDebitEft()
        //{
        //    var request = GetBasicRequest();
        //    request.Type = FundsTransferType.DEBIT;
        //    var errors = new Collection<string>();
        //    if (request.TryValidate(errors))
        //    {
        //        foreach (var error in errors)
        //        {
        //            Debug.WriteLine(error);
        //        }
        //    }
        //    var result = request.PostAsync().Result;
        //    Assert.IsTrue(result.Success);
        //    Assert.AreEqual(0, result.MessageCode);
        //    Assert.IsNotNull(result.TransactionRefCode);
        //}

        //[TestMethod]
        //public void TestFailShortAccountToken()
        //{
        //    var transfer = GetBasicRequest();
        //    transfer.AccountToken = "ba6db04e0f64d2d958df098b6de1056";
        //    var errors = new Collection<string>();
        //    if (transfer.TryValidate(errors)) Assert.Fail();
        //    Assert.AreEqual(1, errors.Count);
        //    Assert.AreEqual("AccountToken must be 32 characters long.", errors.FirstOrDefault());
        //}

        //[TestMethod]
        //public void TestFailLongAccountToken()
        //{
        //    var transfer = GetBasicRequest();
        //    transfer.AccountToken = "aaba6db04e0f64d2d958df098b6de1056";
        //    var errors = new Collection<string>();
        //    if (transfer.TryValidate(errors)) Assert.Fail();
        //    Assert.AreEqual(1, errors.Count);
        //    Assert.AreEqual("AccountToken must be 32 characters long.", errors.FirstOrDefault());
        //}

        //[TestMethod]
        //public void TestFailNoAccountToken()
        //{
        //    var transfer = GetBasicRequest();
        //    transfer.AccountToken = null;
        //    var errors = new Collection<string>();
        //    if (transfer.TryValidate(errors)) Assert.Fail();
        //    Assert.AreEqual(1, errors.Count);
        //    Assert.AreEqual("The AccountToken field is required.", errors.FirstOrDefault());
        //}

        //[TestMethod]
        //public void TestFailShortBankAccountToken()
        //{
        //    var transfer = GetBasicRequest();
        //    transfer.AccountToken = "C01957EE5D8B468342E673CC010BE0A";
        //    var errors = new Collection<string>();
        //    if (transfer.TryValidate(errors)) Assert.Fail();
        //    Assert.AreEqual(1, errors.Count);
        //    Assert.AreEqual("AccountToken must be 32 characters long.", errors.FirstOrDefault());
        //}

        //[TestMethod]
        //public void TestFailLongBankAccountToken()
        //{
        //    var transfer = GetBasicRequest();
        //    transfer.AccountToken = "AAC01957EE5D8B468342E673CC010BE0A";
        //    var errors = new Collection<string>();
        //    if (transfer.TryValidate(errors)) Assert.Fail();
        //    Assert.AreEqual(1, errors.Count);
        //    Assert.AreEqual("AccountToken must be 32 characters long.", errors.FirstOrDefault());
        //}
    }
}
