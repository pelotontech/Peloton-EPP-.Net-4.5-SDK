using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Models;

namespace PelotonEppSdkTests
{
    [TestClass]
    public class EventsTests : TestBase
    {
        private static EventRequest GetBasicEventRequest(string token)
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var request = factory.GetEventRequest();
            request.EventToken = token;
            return request;
        }

        [TestMethod]
        public void TestSuccessGetEvent()
        {
            var eventRequest = GetBasicEventRequest("667fbd353e8d4e9d9e0611d489d5efb6");
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
            var eventRequest = GetBasicEventRequest("invalidtoken");
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
        public void TestEventTokenNull()
        {
            var eventRequest = GetBasicEventRequest(null);
            var errors = new Collection<string>();
            if (!eventRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
                Assert.AreEqual("The EventToken field is required.", errors.Single());
            }
            else
            {
                Assert.Fail("no validation errors when errors should be seen");
            }
        }

        [TestMethod]
        public void TestEventTokenEmpty()
        {
            var eventRequest = GetBasicEventRequest(string.Empty);
            var errors = new Collection<string>();
            if (!eventRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
                Assert.AreEqual("The EventToken field is required.", errors.Single());
            }
            else
            {
                Assert.Fail("no validation errors when errors should be seen");
            }
        }
    }
}
