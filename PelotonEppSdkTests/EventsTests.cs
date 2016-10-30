using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Models;

namespace PelotonEppSdkTests
{
    [TestClass]
    public class EventsTests : TestBase
    {
        private static EventRequest GetBasicEventRequest(string token, LanguageCode languageCode)
        {
            var factory = new RequestFactory(32, "Peloton123", "PelonEppSdkTests", baseUri, languageCode);
            var request = factory.GetEventRequest();
            request.Token = token;
            return request;
        }

        [TestMethod]
        public void TestSuccessGetEvent()
        {
            var eventRequest = GetBasicEventRequest("709a51a13b694e4aa5b7d1c620cbe9c1", LanguageCode.fr);
            var errors = new Collection<string>();
            if (!eventRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }
            var result = eventRequest.GetAsync().Result;
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
        }

        [TestMethod]
        public void TestFailureGetEvent()
        {
            var eventRequest = GetBasicEventRequest("invalidtoken", LanguageCode.en);
            var errors = new Collection<string>();
            if (!eventRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }
            var result = eventRequest.GetAsync().Result;
            Assert.IsFalse(result.Success);
            Assert.AreEqual(109, result.MessageCode);
        }

        [TestMethod]
        public void TestSuccessTokenNull()
        {
            var eventRequest = GetBasicEventRequest(null, LanguageCode.fr);
            var errors = new Collection<string>();
            if (!eventRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
                Assert.AreEqual("The Token field is required.", errors.Single());
            }
            else
            {
                Assert.Fail("no validation errors when errors should be seen");
            }
        }
    }
}
