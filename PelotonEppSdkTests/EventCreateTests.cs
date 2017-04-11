using System;
using System.Collections.Generic;
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
    public class EventCreateTests : EventTestsBase
    {
        private static string duplicateFriendlyUrlPath = "NEWEVENT";

        private static EventRequest GetBasicEventRequest()
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var request = factory.GetEventRequest();
            request.AccountToken = "0C01957EE5D8B468342E673CC010BE0A";
            request.Name = "Test";
            request.Description = "Test";
            request.FriendlyUrlPath = duplicateFriendlyUrlPath + Guid.NewGuid().ToString("N");
            request.Items = new List<EventItem>()
            {
                new EventItem
                {
                    Amount = 1,
                    AmountAdjustable = true,
                    CustomFields = new List<EventCustomField>
                    {
                        new EventCustomField
                        {
                            DefaultValue = "default value",
                            DisplayOrder = 0,
                            Name = "custom field name",
                            Required = true,
                            Type = EventCustomFieldTypeEnum.@string
                        }
                    },
                    DefaultUnitQuantity = 1,
                    Description = "event item description",
                    Name = "event item 1",
                    QuantitySelector = true,
                    UnitAmount = 1,
                    UnitQuantityDescription = "description for quantity"
                }
            };
            request.StartDate = DateTime.UtcNow;
            request.EndDate = DateTime.UtcNow.AddDays(1);
            request.TermsAndConditionsContent = "";
            request.RefundPolicyContent = "";
            return request;
        }
        
        private static EventRequest GetBasicEventRequest(string token, LanguageCode languageCode = LanguageCode.en)
        {
            var factory = new RequestFactory(24, "PAssword123", "PelonEppSdkTests", baseUri, languageCode);
            var request = factory.GetEventRequest();
            request.EventToken = token;
            return request;
        }

        [TestMethod]
        public void TestCreateEventSuccess()
        {
            var createRequest = GetBasicEventRequest();

            var errors = new Collection<string>();
            if (!createRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var result = createRequest.PostAsync().Result;
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
            Assert.AreEqual(32, result.EventToken.Length);
            
            
            // get and check the event
            var getRequest = GetBasicEventRequest(result.EventToken);
            var getResult = getRequest.GetAsync().Result;
            Debug.WriteLine(getResult.Message);
            Debug.WriteLineIf((getResult.Errors != null && getResult.Errors.Count >= 1), string.Join("; ", getResult.Errors ?? new List<string>()));
            Assert.IsTrue(getResult.Success);
            Assert.AreEqual(0, getResult.MessageCode);

            CheckResponseAndResult(createRequest, getResult, EventStateEnum.New);
        }

        [TestMethod]
        public void TestCreateEventWithMultipleItems()
        {
            var createRequest = GetBasicEventRequest();

            createRequest.Items = new List<EventItem>()
            {
                new EventItem
                {
                    Amount = 1,
                    AmountAdjustable = true,
                    CustomFields = new List<EventCustomField>
                    {
                        new EventCustomField
                        {
                            DefaultValue = "default value",
                            DisplayOrder = 0,
                            Name = "custom field name",
                            Required = true,
                            Type = EventCustomFieldTypeEnum.@string
                        }
                    },
                    DefaultUnitQuantity = 10,
                    Description = "event item description",
                    Name = "event item 1",
                    QuantitySelector = true,
                    UnitAmount = 1,
                    UnitQuantityDescription = "description for quantity"
                },
                new EventItem
                {
                    Amount = 2,
                    AmountAdjustable = false,
                    CustomFields = new List<EventCustomField>
                    {
                        new EventCustomField
                        {
                            DefaultValue = "item 2 custom field default value",
                            DisplayOrder = 0,
                            Name = "item 2 custom field name",
                            Required = true,
                            Type = EventCustomFieldTypeEnum.@string
                        }
                    },
                    DefaultUnitQuantity = 10,
                    Description = "event item 2 description",
                    Name = "event item 2",
                    QuantitySelector = true,
                    UnitAmount = 25,
                    UnitQuantityDescription = "description for quantity 25"
                },
                new EventItem
                {
                    Amount = 3,
                    AmountAdjustable = true,
                    CustomFields = new List<EventCustomField>
                    {
                        new EventCustomField
                        {
                            DefaultValue = "item 3 custom field default value",
                            DisplayOrder = 0,
                            Name = "item 3 custom field name",
                            Required = true,
                            Type = EventCustomFieldTypeEnum.@string
                        }
                    },
                    DefaultUnitQuantity = 10,
                    Description = "event item 3 description",
                    Name = "event item 3",
                    QuantitySelector = true,
                    UnitAmount = 50,
                    UnitQuantityDescription = "description for quantity 50"
                }
            };

            var errors = new Collection<string>();
            if (!createRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var result = createRequest.PostAsync().Result;
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
            Assert.AreEqual(32, result.EventToken.Length);


            // get and check the event
            var getRequest = GetBasicEventRequest(result.EventToken);
            var getResult = getRequest.GetAsync().Result;
            Debug.WriteLine(getResult.Message);
            Debug.WriteLineIf((getResult.Errors != null && getResult.Errors.Count >= 1), string.Join("; ", getResult.Errors ?? new List<string>()));
            Assert.IsTrue(getResult.Success);
            Assert.AreEqual(0, getResult.MessageCode);

            CheckResponseAndResult(createRequest, getResult, EventStateEnum.New);
        }

        [TestMethod]
        public void TestCreateEventWithMultipleCustomFields()
        {
            var createRequest = GetBasicEventRequest();

            createRequest.Items = new List<EventItem>()
            {
                new EventItem
                {
                    Amount = 1,
                    AmountAdjustable = true,
                    CustomFields = new List<EventCustomField>
                    {
                        new EventCustomField
                        {
                            DefaultValue = "default value 1",
                            DisplayOrder = 5,
                            Name = "custom field name 1",
                            Required = true,
                            Type = EventCustomFieldTypeEnum.@string
                        },
                        new EventCustomField
                        {
                            DefaultValue = "default value 2",
                            DisplayOrder = 10,
                            Name = "custom field name 2",
                            Required = true,
                            Type = EventCustomFieldTypeEnum.@string
                        },
                        new EventCustomField
                        {
                            DefaultValue = "default value 3",
                            DisplayOrder = 15,
                            Name = "custom field name 3",
                            Required = false,
                            Type = EventCustomFieldTypeEnum.@string
                        },
                        new EventCustomField
                        {
                            DefaultValue = "default value 4",
                            DisplayOrder = 20,
                            Name = "custom field name 4",
                            Required = false,
                            Type = EventCustomFieldTypeEnum.@string
                        }
                    },
                    DefaultUnitQuantity = 10,
                    Description = "event item description",
                    Name = "event item 1",
                    QuantitySelector = true,
                    UnitAmount = 1,
                    UnitQuantityDescription = "description for quantity"
                }
            };

            var errors = new Collection<string>();
            if (!createRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var result = createRequest.PostAsync().Result;
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
            Assert.AreEqual(32, result.EventToken.Length);


            // get and check the event
            var getRequest = GetBasicEventRequest(result.EventToken);
            var getResult = getRequest.GetAsync().Result;
            Debug.WriteLine(getResult.Message);
            Debug.WriteLineIf((getResult.Errors != null && getResult.Errors.Count >= 1), string.Join("; ", getResult.Errors ?? new List<string>()));
            Assert.IsTrue(getResult.Success);
            Assert.AreEqual(0, getResult.MessageCode);

            CheckResponseAndResult(createRequest, getResult, EventStateEnum.New);
        }

        [TestMethod]
        public void TestCreateEventDuplicateEvent()
        {
            var createRequest = GetBasicEventRequest();
            createRequest.Name = "Test";
            createRequest.Description = "Test";
            createRequest.FriendlyUrlPath = duplicateFriendlyUrlPath;
            createRequest.Items = new List<EventItem>()
            {
                new EventItem
                {
                    Description = "event item description",
                    Name = "event item 1",
                }
            };
            createRequest.StartDate = DateTime.UtcNow;
            createRequest.EndDate = DateTime.UtcNow;
            createRequest.TermsAndConditionsContent = "";
            createRequest.RefundPolicyContent = "";

            var errors = new Collection<string>();
            if (!createRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var result = createRequest.PostAsync().Result;
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.MessageCode);
            Assert.AreEqual("Validation Error", result.Message);
            Assert.AreEqual("friendly_url_path: must be unique", result.Errors.Single());
        }

        [TestMethod]
        public void TestCreateEventValidationErrors()
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var request = factory.GetEventRequest();
            //var createRequest = GetBasicEventRequest();

            var errors = new Collection<string>();
            if (!request.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            Assert.AreEqual(7, errors.Count);

        }

        [TestMethod]
        public void TestCreateEventValidationErrors2()
        {
            var factory = new RequestFactory(24, "Password123", "PelonEppSdkTests", baseUri);
            var request = factory.GetEventRequest();
            request.Name = new string('a', 40);


            var create = request;
            var update = request;

            var createErrors = new Collection<string>();
            if (!create.TryValidate(createErrors))
            {
                foreach (var error in createErrors)
                {
                    Debug.WriteLine(error);
                }
            }

            var updateErrors = new Collection<string>();
            if (!update.TryValidate(updateErrors))
            {
                foreach (var error in updateErrors)
                {
                    Debug.WriteLine(error);
                }
            }

            Assert.AreEqual(1, updateErrors.Count);
            Assert.AreEqual(1, createErrors.Count);

        }

        [TestMethod]
        public void TestUpdateEventSuccess()
        {
            var createRequest = GetBasicEventRequest();

            var errors = new Collection<string>();
            if (!createRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var result = createRequest.PostAsync().Result;
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
            Assert.AreEqual(32, result.EventToken.Length);


            // get and check the event
            var getRequest = GetBasicEventRequest(result.EventToken);
            var getResult = getRequest.GetAsync().Result;
            Debug.WriteLine(getResult.Message);
            Debug.WriteLineIf((getResult.Errors != null && getResult.Errors.Count >= 1), string.Join("; ", getResult.Errors ?? new List<string>()));
            Assert.IsTrue(getResult.Success);
            Assert.AreEqual(0, getResult.MessageCode);

            CheckResponseAndResult(createRequest, getResult, EventStateEnum.New);


            // update the createRequest
            var updateRequest = createRequest;
            updateRequest.EventToken = result.EventToken;
            updateRequest.Name = updateRequest.Name + " Updated";

            updateRequest.State = new State()
            {
                Code = EventStateEnum.Active.ToString(),
                Name = EventStateEnum.Active.ToString()
            };
            updateRequest.Items.First().Amount = 10;
            updateRequest.Items.First().UnitAmount = 5;
            updateRequest.Items.First().CustomFields.Add(
                new EventCustomField
                {
                    DefaultValue = "default value 2",
                    DisplayOrder = 10,
                    Name = "custom field name 2",
                    Required = true,
                    Type = EventCustomFieldTypeEnum.@string
                });
            updateRequest.Items.First().CustomFields.Add(
                new EventCustomField
                {
                    DefaultValue = "default value 3",
                    DisplayOrder = 15,
                    Name = "custom field name 3",
                    Required = false,
                    Type = EventCustomFieldTypeEnum.@string
                });

            errors = new Collection<string>();
            if (!updateRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            result = updateRequest.PutAsync().Result;
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
            Assert.AreEqual(32, result.EventToken.Length);


            // get and check the event
            getRequest = GetBasicEventRequest(result.EventToken);
            getResult = getRequest.GetAsync().Result;
            Debug.WriteLine(getResult.Message);
            Debug.WriteLineIf((getResult.Errors != null && getResult.Errors.Count >= 1), string.Join("; ", getResult.Errors ?? new List<string>()));
            Assert.IsTrue(getResult.Success);
            Assert.AreEqual(0, getResult.MessageCode);

            CheckResponseAndResult(updateRequest, getResult, EventStateEnum.Active);

        }

        [TestMethod]
        public void TestUpdateEventToFrench()
        {
            var createRequest = GetBasicEventRequest();

            Debug.WriteLine("creating event");
            var errors = new Collection<string>();
            if (!createRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            var result = createRequest.PostAsync().Result;
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
            Assert.AreEqual(32, result.EventToken.Length);


            // get and check the event
            Debug.WriteLine($"getting event with token: {result.EventToken}");
            var getRequest = GetBasicEventRequest(result.EventToken);
            var getResult = getRequest.GetAsync().Result;
            Debug.WriteLine(getResult.Message);
            Debug.WriteLineIf((getResult.Errors != null && getResult.Errors.Count >= 1), string.Join("; ", getResult.Errors ?? new List<string>()));
            Assert.IsTrue(getResult.Success);
            Assert.AreEqual(0, getResult.MessageCode);

            CheckResponseAndResult(createRequest, getResult, EventStateEnum.New);


            // update the createRequest
            Debug.WriteLine($"updating event with token: {result.EventToken}");
            var updateRequest = createRequest;
            updateRequest.LanguageCode = LanguageCode.fr;
            updateRequest.EventToken = result.EventToken;
            updateRequest.Name += " (fr)";
            updateRequest.Description += " (fr)";
            updateRequest.TermsAndConditionsContent += " (fr)";
            updateRequest.RefundPolicyContent += " (fr)";

            updateRequest.State = new State()
            {
                Code = EventStateEnum.New.ToString(),
                Name = EventStateEnum.New.ToString()
            };

            updateRequest.Items.First().Name += " (fr)";
            updateRequest.Items.First().Description += " (fr)";
            updateRequest.Items.First().UnitQuantityDescription += " (fr)";
            updateRequest.Items.First().CustomFields.First().Name += " (fr)";
            updateRequest.Items.First().CustomFields.First().DefaultValue += " (fr)";

            errors = new Collection<string>();
            if (!updateRequest.TryValidate(errors))
            {
                foreach (var error in errors)
                {
                    Debug.WriteLine(error);
                }
            }

            result = updateRequest.PutAsync().Result;
            Debug.WriteLine(result.Message);
            Debug.WriteLineIf((result.Errors != null && result.Errors.Count >= 1), string.Join("; ", result.Errors ?? new List<string>()));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.MessageCode);
            Assert.AreEqual(32, result.EventToken.Length);


            // get and check the event
            Debug.WriteLine($"getting event with token: {result.EventToken}");
            getRequest = GetBasicEventRequest(result.EventToken);
            getRequest.LanguageCode = LanguageCode.fr;
            getResult = getRequest.GetAsync().Result;
            Debug.WriteLine(getResult.Message);
            Debug.WriteLineIf((getResult.Errors != null && getResult.Errors.Count >= 1), string.Join("; ", getResult.Errors ?? new List<string>()));
            Assert.IsTrue(getResult.Success);
            Assert.AreEqual(0, getResult.MessageCode);

            CheckResponseAndResult(updateRequest, getResult, EventStateEnum.New);

        }
    }
}
