using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Models;

namespace PelotonEppSdkTests
{
    [TestFixture]
    public class FundsTransferNotificationsTests : TestBase
    {
        private static FundsTransferNotificationsTokenRequest GetBasicTokenRequest()
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var transferNotificationsToken = factory.GetFundsTransferNotificationsTokenRequest();
            transferNotificationsToken.AccountToken = "0C01957EE5D8B468342E673CC010BE0A";
            transferNotificationsToken.FromDateUtc = DateTime.UtcNow.ToString("yyyy-MM-dd");
            return transferNotificationsToken;
        }

        private static FundsTransferNotificationsRequest GetBasicNotificationRequest(string token, int items = 100)
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var transferNotifications = factory.GetFundsTransferNotificationsRequest();
            transferNotifications.AccountToken = "0C01957EE5D8B468342E673CC010BE0A";
            transferNotifications.Token = token;
            transferNotifications.Items = items;
            return transferNotifications;
        }

        private static FundsTransferRequest GetBasicFundsTransferRequest()
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


        [Test]
        public void TestFailSettingTokenNoDate()
        {
            var tokenRequest = GetBasicTokenRequest();
            tokenRequest.FromDateUtc = null;
            var errors = new Collection<string>();
            if (tokenRequest.TryValidate(errors)) Assert.Fail();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("The FromDateUtc field is required.", errors.FirstOrDefault());
        }

        [Test]
        public void TestSuccessSettingToken()
        {
            var request = GetBasicTokenRequest();
            var errors = new Collection<string>();
            if (!request.TryValidate(errors))
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
        }

        [Test]
        public void TestSuccessNoStateChange()
        {
            var tokenRequest = GetBasicTokenRequest();
            var errors = new Collection<string>();
            if (!tokenRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }
            var result = tokenRequest.PostAsync().Result;
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);

            // Get the notifications to obtain a new token
            // We don't care if we get results or not as that is not the point of the test
            var notificationsRequest = GetBasicNotificationRequest(result.Token);
            if (!notificationsRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var result2 = notificationsRequest.GetAsync().Result;
            Assert.IsTrue(result2.Success);
            Assert.AreEqual(0, result2.MessageCode);
            Assert.IsNotNull(result2.Token);

            // Get the notifications again and ensure there are none
            var notificationsRequest2 = GetBasicNotificationRequest(result2.Token);
            if (!notificationsRequest2.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var result3 = notificationsRequest2.GetAsync().Result;
            Assert.IsTrue(result3.Success);
            Assert.AreEqual(0, result3.MessageCode);
            Assert.IsNotNull(result3.Token);
            Assert.AreEqual(0, result3.Notifications.Count);
        }

        [Test]
        public void TestSuccessPendingStateChange()
        {
            var tokenRequest = GetBasicTokenRequest();
            var errors = new Collection<string>();
            if (!tokenRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }
            var result = tokenRequest.PostAsync().Result;
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);

            // Get the notifications to obtain a new token
            // We don't care if we get results or not as that is not the point of the test
            var notificationsRequest = GetBasicNotificationRequest(result.Token);
            if (!notificationsRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var result2 = notificationsRequest.GetAsync().Result;
            Assert.IsTrue(result2.Success);
            Assert.AreEqual(0, result2.MessageCode);
            Assert.IsNotNull(result2.Token);

            // Create a transfer
            var transfer = GetBasicFundsTransferRequest();
            transfer.References = null;

            if (!transfer.TryValidate(errors))
            {
                foreach (var validationResult in errors)
                {
                    Debug.WriteLine(validationResult);
                }
                Assert.Fail();
            }
            var result3 = transfer.PostAsync().Result;

            Assert.IsTrue(result3.Success);
            Assert.AreEqual(0, result3.MessageCode);
            Assert.IsNotNull(result3.TransactionRefCode);

            // Get the notifications again and ensure there is one
            var notificationsRequest2 = GetBasicNotificationRequest(result2.Token);
            if (!notificationsRequest2.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var result4 = notificationsRequest2.GetAsync().Result;
            Assert.IsTrue(result4.Success);
            Assert.AreEqual(0, result4.MessageCode);
            Assert.IsNotNull(result4.Token);
            Assert.AreEqual(1, result4.Notifications.Count);
            Assert.AreEqual("Pending", result4.Notifications.First().StateChangeTo.Name);
            Assert.AreEqual("0", result4.Notifications.First().StateChangeTo.Code);
        }
    }
}
