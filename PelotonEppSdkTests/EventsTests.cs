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
    public class EventsTests : EventTestsBase
    {
        private static EventRequest GetBasicEventRequest(string token, LanguageCode languageCode = LanguageCode.en)
        {
            var factory = new RequestFactory(24, "PAssword123", "PelonEppSdkTests", baseUri, languageCode);
            var request = factory.GetEventRequest();
            request.EventToken = token;
            return request;
        }

        [TestMethod]
        public void TestSuccessGetEvent()
        {
            var token = "667fbd353e8d4e9d9e0611d489d5efb6";
            var eventRequest = GetBasicEventRequest(token);
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

            CheckGetResponse(token, result, LanguageCode.en);
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
            Assert.AreEqual(1800, result.MessageCode);
            Assert.AreEqual("EventToken must be 32 characters in length.", errors.Single());
        }

        [TestMethod]
        public void TestFailureGetEvent2()
        {
            var eventRequest = GetBasicEventRequest("invalidtokenoflength32+tenmorech");
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
            Assert.AreEqual(1800, result.MessageCode);
            Assert.AreEqual("Event Token Cannot Be Found", result.Message);
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
                Assert.AreEqual("EventToken must be 32 characters in length.", errors.Single());
            }
            else
            {
                Assert.Fail("no validation errors when errors should be seen");
            }
        }

        [TestMethod]
        public void TestGetEventEn()
        {
            var token = "667fbd353e8d4e9d9e0611d489d5efb6";
            var eventRequest = GetBasicEventRequest(token, LanguageCode.en);
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
            Assert.AreEqual("Success", result.Message);

            CheckGetResponse(token, result, LanguageCode.en);
        }

        [TestMethod]
        public void TestGetEventFr()
        {
            var token = "667fbd353e8d4e9d9e0611d489d5efb6";
            var eventRequest = GetBasicEventRequest(token, LanguageCode.fr);
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
            Assert.AreEqual("Réussi", result.Message);

            CheckGetResponse(token, result, LanguageCode.fr);
        }

        private void CheckGetResponse(string token, EventResponse actual, LanguageCode languageCode)
        {
            switch (token)
            {
                case "667fbd353e8d4e9d9e0611d489d5efb6":
                    Assert.AreEqual(32, actual.EventToken.Length);
                    Assert.AreEqual("Dueling over a Grand Regatta", actual.Name);
                    Assert.AreEqual("DOG2015", actual.FriendlyUrlPath);
                    Assert.AreEqual(null, actual.Description);
                    Assert.AreEqual(EventStateEnum.Active, actual.State);
                    Assert.AreEqual("2015-03-01 00:00:00", actual.StartDatetime.ToString());
                    Assert.AreEqual("2015-03-27 00:00:00", actual.EndDatetime.ToString());
                    if (languageCode == LanguageCode.en)
                    {
                        Assert.AreEqual(@"By making this payment you expressly authorize the service provider, Peloton Technologies Inc., to charge your credit card for the noted dollar amount.
You can expect that your credit card information will be transmitted securely with the utmost protection by the service provider Peloton Technologies Inc.
If you require an adjustment to your transaction after processing has occurred, please contact one of our representatives prior to contacting the service provider, Peloton Technologies.
If you wish to dispute a charge please contact Peloton Technologies Inc. at {0} or {1}.".Replace("\n", "")
                            .Replace("\r", ""), actual.TermsAndConditionsContent.Replace("\n", "").Replace("\r", ""));

                        Assert.AreEqual("For all refunds, please contact one of our representatives.", actual.RefundPolicyContent);
                    }

                    if (languageCode == LanguageCode.fr)
                    {
                        Assert.AreEqual(@"En faisant ce paiement, vous autorisez expressément le fournisseur de services, Peloton Technologies Inc., de débiter votre carte de crédit pour le montant indiqué.Vous pouvez vous attendre que vos informations de carte de crédit sera transmis en toute sécurité avec la plus grande protection par le fournisseur de services Peloton Technologies Inc.Si vous avez besoin d'un ajustement à votre transaction après le traitement a eu lieu, s'il vous plaît contacter un de nos représentants avant de communiquer avec le fournisseur de services, Peloton Technologies. Si vous souhaitez contester une accusation s'il vous plaît contacter Peloton Technologies Inc. à {0} ou {1}.".Replace("\n", "")
                            .Replace("\r", ""), actual.TermsAndConditionsContent.Replace("\n", "").Replace("\r", ""));

                        Assert.AreEqual("Pour tous les remboursements, s'il vous plaît contacter un de nos représentants.", actual.RefundPolicyContent);
                    }

                    // TODO: the api only saves/returns one Event Item. The rest are discarded.
                    //Assert.AreEqual(expected.Items.Count, actual.Items.Count);
                    Assert.AreEqual(1, actual.Items.Count);


                    for (var i = 0; i < actual.Items.Count; i++)
                    {
                        Debug.WriteLine($"item {i}");


                        var actualItem = actual.Items.ElementAt(i);

                        // TODO: the name field is ignored on creation of Event POST
                        //Assert.AreEqual(expectedItem.Name, actualItem.Name);
                        // item name actually comes back as the event name
                        // workaround:
                        Assert.AreEqual("Dueling over a Grand Regatta", actualItem.Name);
                        // TODO: the description field is ignored on creation of Event POST
                        //Assert.AreEqual(expectedItem.Description, actualItem.Description);
                        // item description comes back as the event description
                        // workaround:
                        Assert.AreEqual(null, actualItem.Description);
                        Assert.AreEqual(false, actualItem.QuantitySelector);
                        // TODO: the default_unit_quantity field is ignored on creation of Event POST
                        //Assert.AreEqual(expectedItem.DefaultUnitQuantity, actualItem.DefaultUnitQuantity);
                        // item default quantity will come back un-set
                        // workaround:
                        Assert.AreEqual(default(int), actualItem.DefaultUnitQuantity);
                        Assert.AreEqual(null, actualItem.UnitQuantityDescription);
                        Assert.AreEqual(0, actualItem.UnitAmount);
                        Assert.AreEqual(0, actualItem.Amount);
                        // TODO: seems that the API is not returning the AmountAdjustable field on GET requests
                        //Assert.AreEqual(expectedItem.AmountAdjustable, actualItem.AmountAdjustable);

                        Assert.AreEqual(5, actualItem.CustomFields.Count);

                        Debug.WriteLine($"item custom field {0}");

                        var actualItemCustomField = actualItem.CustomFields.ElementAt(0);

                        Assert.AreEqual("Race Name", actualItemCustomField.Name);
                        Assert.AreEqual(null, actualItemCustomField.DefaultValue);
                        Assert.AreEqual(EventCustomFieldTypeEnum.@string, actualItemCustomField.Type);
                        Assert.AreEqual(0, actualItemCustomField.DisplayOrder);
                        Assert.AreEqual(true, actualItemCustomField.Required);

                        Debug.WriteLine($"item custom field {1}");
                        actualItemCustomField = actualItem.CustomFields.ElementAt(1);

                        Assert.AreEqual("Participant Name", actualItemCustomField.Name);
                        Assert.AreEqual(null, actualItemCustomField.DefaultValue);
                        Assert.AreEqual(EventCustomFieldTypeEnum.@string, actualItemCustomField.Type);
                        Assert.AreEqual(1, actualItemCustomField.DisplayOrder);
                        Assert.AreEqual(true, actualItemCustomField.Required);

                        Debug.WriteLine($"item custom field {2}");
                        actualItemCustomField = actualItem.CustomFields.ElementAt(2);

                        Assert.AreEqual("Other 1", actualItemCustomField.Name);
                        Assert.AreEqual(null, actualItemCustomField.DefaultValue);
                        Assert.AreEqual(EventCustomFieldTypeEnum.@string, actualItemCustomField.Type);
                        Assert.AreEqual(2, actualItemCustomField.DisplayOrder);
                        Assert.AreEqual(false, actualItemCustomField.Required);

                        Debug.WriteLine($"item custom field {3}");
                        actualItemCustomField = actualItem.CustomFields.ElementAt(3);

                        Assert.AreEqual("Other 2", actualItemCustomField.Name);
                        Assert.AreEqual(null, actualItemCustomField.DefaultValue);
                        Assert.AreEqual(EventCustomFieldTypeEnum.@string, actualItemCustomField.Type);
                        Assert.AreEqual(3, actualItemCustomField.DisplayOrder);
                        Assert.AreEqual(false, actualItemCustomField.Required);

                        Debug.WriteLine($"item custom field {4}");
                        actualItemCustomField = actualItem.CustomFields.ElementAt(4);

                        Assert.AreEqual("Other 3", actualItemCustomField.Name);
                        Assert.AreEqual(null, actualItemCustomField.DefaultValue);
                        Assert.AreEqual(EventCustomFieldTypeEnum.@string, actualItemCustomField.Type);
                        Assert.AreEqual(4, actualItemCustomField.DisplayOrder);
                        Assert.AreEqual(false, actualItemCustomField.Required);

                    }
                    break;
                default:
                    break;
            }
        }

    }
}
