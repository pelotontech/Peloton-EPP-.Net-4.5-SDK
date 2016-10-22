using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Models;

namespace PelotonEppSdkTests
{
    [TestClass]
    public class EventsTest : TestBase
    {
        private static EventRequest GetBasicEventRequest(string token)
        {
            var factory = new RequestFactory(32, "Peloton123", "PelotonEppSdkTests", baseUri);
            var request = factory.GetEventRequest();
            request.Token = token;
            return request;
        }

        [TestMethod]
        public void TestSuccessGetEvent()
        {
            var eventRequest = GetBasicEventRequest("709a51a13b694e4aa5b7d1c620cbe9c1");
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
    }
}
