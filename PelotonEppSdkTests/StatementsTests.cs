using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PelotonEppSdk.Classes;

namespace PelotonEppSdkTests
{
    [TestClass]
    public class StatementsTests : TestBase
    {
        [TestMethod]
        public void TestStatements()
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var request = factory.GetStatementsRequest();

            request.AccountToken = "0C01957EE5D8B468342E673CC010BE0A";
            request.FromDate = DateTime.UtcNow.AddDays(-30);
            request.ToDate = DateTime.UtcNow;
            var result = request.PostAsync().Result;
            Assert.IsTrue(result.Success);
        }
    }
}
