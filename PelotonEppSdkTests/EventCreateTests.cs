using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using NUnit.Framework;
using PelotonEppSdk.Classes;
using PelotonEppSdk.Models;

namespace PelotonEppSdkTests
{
    [TestFixture]
    public class EventCreateTests : TestBase
    {
        //private string duplicateFriendlyUrlPath = "NEWEVENT";

        //private static EventRequest GetBasicEventRequest()
        //{
        //    var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
        //    var request = factory.GetEventCreateRequest();
        //    return request;
        //}

        //[Test]
        //public void TestSuccessCreateEvent()
        //{
        //    var createRequest = GetBasicEventRequest();
        //    createRequest.Name = "Test";
        //    createRequest.Description = "Test";
        //    createRequest.FriendlyUrlPath = duplicateFriendlyUrlPath;
        //    createRequest.StartDatetime = DateTime.UtcNow;
        //    createRequest.EndDatetime = DateTime.UtcNow;
        //    createRequest.TermsAndConditionsContent = "";
        //    createRequest.RefundPolicyContent = "";

        //    var errors = new Collection<string>();
        //    if (!createRequest.TryValidate(errors))
        //    {
        //        foreach (var error in errors)
        //        {
        //            Debug.WriteLine(error);
        //        }
        //    }

        //    var result = createRequest.PostAsync().Result;
        //    Debug.WriteLine(result.Message);
        //    Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
        //    Assert.IsTrue(result.Success);
        //    Assert.AreEqual(0, result.MessageCode);
        //}

        //[Test]
        //public void TestFailureCreateDuplicateEvent()
        //{
        //    var createRequest = GetBasicEventRequest();
        //    createRequest.Name = "Test";
        //    createRequest.Description = "Test";
        //    createRequest.FriendlyUrlPath = duplicateFriendlyUrlPath; 
        //    createRequest.StartDatetime = DateTime.UtcNow;
        //    createRequest.EndDatetime = DateTime.UtcNow;
        //    createRequest.TermsAndConditionsContent = "";
        //    createRequest.RefundPolicyContent = "";

        //    var errors = new Collection<string>();
        //    if (!createRequest.TryValidate(errors))
        //    {
        //        foreach (var error in errors)
        //        {
        //            Debug.WriteLine(error);
        //        }
        //    }

        //    var result = createRequest.PostAsync().Result;
        //    Debug.WriteLine(result.Message);
        //    Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
        //    Assert.IsFalse(result.Success);
        //    Assert.AreEqual(106, result.MessageCode);
        //}
    }
}
