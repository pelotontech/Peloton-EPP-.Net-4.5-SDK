using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using PelotonEppSdk.Classes;

namespace PelotonEppSdkTests
{
    [TestFixture]
    public class StatementsTests : TestBase
    {
        [Test]
        public void TestStatementsOk()
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var request = factory.GetStatementsRequest();

            request.AccountToken = "0C01957EE5D8B468342E673CC010BE0A";
            request.FromDate = DateTime.UtcNow.AddDays(-30);
            request.ToDate = DateTime.UtcNow;

            var errors = new Collection<string>();
            if (!request.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var result = request.PostAsync().Result;
            Debug.WriteLine("details count: " + result.StatementDetails.Count);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public void TestStatementsTokenNull()
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var request = factory.GetStatementsRequest();

            request.AccountToken = null;
            request.FromDate = DateTime.UtcNow.AddHours(-1);
            request.ToDate = DateTime.UtcNow;

            var errors = new Collection<string>();
            if (!request.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            Assert.IsTrue(errors.Count == 1);
            Assert.AreEqual("The AccountToken field is required.", errors.Single());

            var result = request.PostAsync().Result;
            Debug.WriteLine("details count: " + result.StatementDetails?.Count);
            Debug.WriteLine(result.Message);
            Debug.WriteLine(result.MessageCode);

            foreach (var error in result.Errors)
            {
                Debug.WriteLine(error);
            }

            Assert.AreEqual("account_token: required", result.Errors.Single());

            Assert.IsFalse(result.Success);
        }

        [Test]
        public void TestStatementsTokenEmptyString()
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var request = factory.GetStatementsRequest();

            request.AccountToken = "";
            request.FromDate = DateTime.UtcNow.AddHours(-1);
            request.ToDate = DateTime.UtcNow;

            var errors = new Collection<string>();
            if (!request.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            Assert.IsTrue(errors.Count == 1);
            Assert.AreEqual("The AccountToken field is required.", errors.Single());

            var result = request.PostAsync().Result;
            Debug.WriteLine("details count: " + result.StatementDetails?.Count);
            Debug.WriteLine(result.Message);
            Debug.WriteLine(result.MessageCode);

            foreach (var error in result.Errors)
            {
                Debug.WriteLine(error);
            }

            Assert.AreEqual("account_token: must be 32 characters", result.Errors.Single());
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void TestStatementsTokenTooLong()
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var request = factory.GetStatementsRequest();

            request.AccountToken = new string('a', 33);
            request.FromDate = DateTime.UtcNow.AddHours(-1);
            request.ToDate = DateTime.UtcNow;

            var errors = new Collection<string>();
            if (!request.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            Assert.IsTrue(errors.Count == 1);
            Assert.AreEqual("AccountToken must be 32 characters in length.", errors.Single());

            var result = request.PostAsync().Result;
            Debug.WriteLine("details count: " + result.StatementDetails?.Count);
            Debug.WriteLine(result.Message);
            Debug.WriteLine(result.MessageCode);

            foreach (var error in result.Errors)
            {
                Debug.WriteLine(error);
            }

            Assert.AreEqual("account_token: must be 32 characters", result.Errors.Single());
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void TestStatementsTokenMaxLengthButInvalid()
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var request = factory.GetStatementsRequest();

            request.AccountToken = new string('a', 32);
            request.FromDate = DateTime.UtcNow.AddHours(-1);
            request.ToDate = DateTime.UtcNow;

            var errors = new Collection<string>();
            if (!request.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            Assert.IsTrue(errors.Count == 0);

            var result = request.PostAsync().Result;
            Debug.WriteLine("details count: " + result.StatementDetails?.Count);
            Debug.WriteLine(result.Message);
            Debug.WriteLine(result.MessageCode);

            foreach (var error in result.Errors)
            {
                Debug.WriteLine(error);
            }
            Assert.AreEqual("account_token: not found", result.Errors.Single());
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void TestStatementsShortTimePeriod()
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var request = factory.GetStatementsRequest();

            request.AccountToken = "0C01957EE5D8B468342E673CC010BE0A";
            request.FromDate = DateTime.UtcNow.AddHours(-1);
            request.ToDate = DateTime.UtcNow;

            var errors = new Collection<string>();
            if (!request.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var result = request.PostAsync().Result;
            Debug.WriteLine("details count: " + result.StatementDetails.Count);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public void TestStatementsTimePeriodInFuture()
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var request = factory.GetStatementsRequest();

            request.AccountToken = "0C01957EE5D8B468342E673CC010BE0A";
            request.FromDate = DateTime.UtcNow.AddHours(1);
            request.ToDate = DateTime.UtcNow.AddHours(10);

            var errors = new Collection<string>();
            if (!request.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var result = request.PostAsync().Result;
            Debug.WriteLine("details count: " + result.StatementDetails.Count);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public void TestStatementsTimePeriodVeryLong1Year()
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var request = factory.GetStatementsRequest();

            request.AccountToken = "0C01957EE5D8B468342E673CC010BE0A";
            request.FromDate = DateTime.UtcNow.AddYears(-1);
            request.ToDate = DateTime.UtcNow;

            var errors = new Collection<string>();
            if (!request.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var result = request.PostAsync().Result;
            Debug.WriteLine("details count: " + result.StatementDetails.Count);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public void TestStatementsTimePeriodVeryLong10Years()
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var request = factory.GetStatementsRequest();

            request.AccountToken = "0C01957EE5D8B468342E673CC010BE0A";
            request.FromDate = DateTime.UtcNow.AddYears(-10);
            request.ToDate = DateTime.UtcNow;

            var errors = new Collection<string>();
            if (!request.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var result = request.PostAsync().Result;
            Debug.WriteLine("details count: " + result.StatementDetails.Count);
            Assert.IsTrue(result.Success);
        }
    }
}
