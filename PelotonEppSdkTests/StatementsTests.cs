using System;
using System.Diagnostics;
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

        [TestMethod]
        public void DarrylTest()
        {
            try
            {
                var requestFactory = new RequestFactory(119, "45bd9dfd", "mychildcarepro™", new Uri("https://testapi.peloton-technologies.com/"));
                var request = requestFactory.GetStatementsRequest();

                request.AccountToken = "E52A4CF90506099E75C727855812B5A9";
                request.FromDate = new DateTime(2016, 3, 01);
                request.ToDate = new DateTime(2016, 4, 1);
                var result = request.PostAsync().Result;
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Assert.Fail();
            }
        }
    }
}
