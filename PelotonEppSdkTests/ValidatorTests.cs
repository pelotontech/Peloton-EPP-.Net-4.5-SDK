using System;
using System.Collections.Generic;
using NUnit.Framework;
using PelotonEppSdk.Models;
using PelotonEppSdk.Validations;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using static PelotonEppSdk.Validations.RequestMethodAttribute;
using static PelotonEppSdk.Validations.ValidationSubsetAttribute;


namespace PelotonEppSdkTests
{
    public class TestModel: RequestBase
    {
        [ValidationSubset(new [] { GeneralEnum.One, GeneralEnum.Two, GeneralEnum.Three })]
        [Required]
        public string TestStringInSubsetsOneTwoThree { get; set; }

        [ValidationSubset(GeneralEnum.Two)]
        [StringLength(50)]
        public string TestStringInSubsetTwo { get; set; }

        [ValidationSubset(new[] { GeneralEnum.Three })]
        [Required]
        [StringLength(20)]
        public string TestStringInSubsetThree { get; set; }

        [Required]
        [StringLength(50)]
        public string TestStringInZeroSubsets { get; set; }

        [System.ComponentModel.DataAnnotations.Range(0, 10)]
        public int TestInteger { get; set; }

        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is not <c>2XX Success</c>.</exception>
        /// <exception cref="ValidationException">Model is not valid.</exception>
        public async Task<Response> OneAsync(bool validate = true)
        {
            if (validate)
            {
                // validate the request before sending it
                ValidationErrors = new List<string>();
                var isValid = TryValidatePropertySubset(ValidationErrors, GeneralEnum.One);
                if(!isValid) throw new ValidationException("Validation Error", null, ValidationErrors);
            }

            var result = new Response();
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is not <c>2XX Success</c>.</exception>
        /// <exception cref="ValidationException">Model is not valid.</exception>
        public async Task<Response> TwoAsync(bool validate = true)
        {
            if (validate)
            {
                // validate the request before sending it
                ValidationErrors = new List<string>();
                var isValid = TryValidatePropertySubset(ValidationErrors, GeneralEnum.Two);
                if(!isValid) throw new ValidationException("Validation Error", null, ValidationErrors);
            }

            var result = new Response();
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is not <c>2XX Success</c>.</exception>
        /// <exception cref="ValidationException">Model is not valid.</exception>
        public async Task<Response> ThreeAsync(bool validate = true)
        {
            if (validate)
            {
                // validate the request before sending it
                ValidationErrors = new List<string>();
                var isValid = TryValidatePropertySubset(ValidationErrors, GeneralEnum.Three);
                if(!isValid) throw new ValidationException("Validation Error", null, ValidationErrors);
            }

            var result = new Response();
            return await Task.FromResult(result).ConfigureAwait(false);
        }
    }
    
    public class TestRequestMethodModel: RequestBase
    {
        [RequestMethod(new [] { RequestMethodEnum.POST, RequestMethodEnum.PUT, RequestMethodEnum.DELETE })]
        [Required]
        public string TestStringInPOSTPUTAndDELETE { get; set; }

        [Required]
        [StringLength(50)]
        public string TestStringInZeroSubsets { get; set; }

        [RequestMethod(RequestMethodEnum.PUT)]
        [StringLength(50)]
        public string TestStringInPUTSubset { get; set; }

        [System.ComponentModel.DataAnnotations.Range(0, 10)]
        public int TestIntegerInZeroSubsets { get; set; }

        [RequestMethod(new[] { RequestMethodEnum.DELETE })]
        [Required]
        [System.ComponentModel.DataAnnotations.Range(0, 10)]
        public int TestIntegerInSubsetDELETE { get; set; }
    }

    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        public void TestValidationSubsetAttributeSubsetEnumNull()
        {
            TestModel testModel = new TestModel();

            var errorList = new List<string>();
            try
            {
                testModel.TryValidatePropertySubset(errorList, null);
                Assert.Fail();
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("Value cannot be null. Parameter name: subsetEnum", e.Message.Replace("\r\n", " "));
                Assert.AreEqual("subsetEnum", e.ParamName);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestValidationSubsetAttributeErrorListNull()
        {
            TestModel testModel = new TestModel();

            try
            {
                testModel.TryValidatePropertySubset(null, GeneralEnum.One);
                Assert.Fail();
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("Value cannot be null. Parameter name: errorList", e.Message.Replace("\r\n", " "));
                Assert.AreEqual("errorList", e.ParamName);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestValidationSubsetAttributeModelValid()
        {
            TestModel testModel = new TestModel()
            {
                TestStringInSubsetsOneTwoThree = "teststring",
                TestStringInZeroSubsets = "testString2",
                TestStringInSubsetTwo = "testString3",
                TestInteger = 0,
                TestStringInSubsetThree = "testString4"
            };

            var errorList = new List<string>();
            var result = testModel.TryValidatePropertySubset(errorList, GeneralEnum.One);
            Assert.IsTrue(result);
            Assert.IsTrue(!errorList.Any());
        }

        [Test]
        public void TestValidationSubsetAttributeModelOnePropertyInvalid()
        {
            TestModel testModel = new TestModel()
            {
                TestStringInSubsetsOneTwoThree = null,
                TestStringInZeroSubsets = "testString2",
                TestStringInSubsetTwo = "testString3",
                TestInteger = 0,
                TestStringInSubsetThree = "testString4"
            };

            var errorList = new List<string>();
            var result = testModel.TryValidatePropertySubset(errorList, GeneralEnum.One);
            Assert.IsFalse(result);
            Assert.IsTrue(errorList.Single().Any());
            Assert.AreEqual("The TestStringInSubsetsOneTwoThree field is required.", errorList.Single());
        }

        [Test]
        public void TestValidationSubsetAttributeModelMultiplePropertiesInvalid()
        {
            TestModel testModel = new TestModel()
            {
                TestStringInSubsetsOneTwoThree = null,
                TestStringInZeroSubsets = null,
                TestStringInSubsetTwo = null,
                TestInteger = -1,
                TestStringInSubsetThree = null
            };

            var errorList = new List<string>();
            var result = testModel.TryValidatePropertySubset(errorList, GeneralEnum.Three);
            Assert.IsFalse(result);
            foreach (var item in errorList)
            {
                Debug.WriteLine(item);
            }
            Assert.IsTrue(errorList.Count == 2);

            Assert.IsTrue(errorList.Contains("The TestStringInSubsetsOneTwoThree field is required."));
            Assert.IsTrue(errorList.Contains("The TestStringInSubsetThree field is required."));
        }

        [Test]
        public void TestValidationSubsetAttributeModelOnePropertyInvalidButNotInSelectedSubset()
        {
            TestModel testModel = new TestModel()
            {
                TestStringInSubsetsOneTwoThree = "testString", // valid
                TestStringInZeroSubsets = new string('a', 51), // invalid, but not part of the POST subset of properties
                TestStringInSubsetTwo = "testString3", // valid, not in subset
                TestInteger = 0, // valid
                TestStringInSubsetThree = "testString4"
            };

            var errorList = new List<string>();
            var result = testModel.TryValidatePropertySubset(errorList, GeneralEnum.One);
            Assert.IsTrue(result);
            Assert.IsTrue(!errorList.Any());
        }

        [Test]
        public void TestValidationSubsetAttributeCheckValidPropertyInSubset()
        {
            TestModel testModel = new TestModel()
            {
                TestStringInSubsetsOneTwoThree = "testString", // valid
                TestStringInZeroSubsets = new string('a', 51), // invalid, but not part of the POST subset of properties
                TestStringInSubsetTwo = "testString3", // valid, in subset
                TestInteger = 0, // valid
                TestStringInSubsetThree = "testString4"
            };

            var errorList = new List<string>();
            var result = testModel.TryValidatePropertySubset(errorList, GeneralEnum.Two);
            Assert.IsTrue(result);
            Assert.IsTrue(!errorList.Any());
        }

        [Test]
        public void TestValidationSubsetAttributeCheckInvalidPropertyNotInSubset()
        {
            TestModel testModel = new TestModel()
            {
                TestStringInSubsetsOneTwoThree = "testString", // valid
                TestStringInZeroSubsets = new string('a', 50), // valid, but not part of the POST subset of properties
                TestStringInSubsetTwo = "testString3", // valid, in subset
                TestInteger = 20, // invalid, not in any subset
                TestStringInSubsetThree = "testString4"
            };

            var errorList = new List<string>();
            var result = testModel.TryValidatePropertySubset(errorList, GeneralEnum.Two);
            Assert.IsTrue(result);
            Assert.IsTrue(!errorList.Any());
        }

        [Test]
        public void TestValidationSubsetAttributeCheckRequiredRangePropertyInSubset()
        {
            TestModel testModel = new TestModel()
            {
                TestStringInSubsetsOneTwoThree = "testString", // valid
                TestStringInZeroSubsets = new string('a', 50), // valid, but not part of the POST subset of properties
                TestStringInSubsetTwo = "testString3", // valid, in subset
                TestInteger = 20, // invalid, not in any subset
                TestStringInSubsetThree = new string('a', 21) // invalid, in sub set
            };

            var errorList = new List<string>();
            var result = testModel.TryValidatePropertySubset(errorList, GeneralEnum.Three);
            Assert.IsFalse(result);
            Assert.IsTrue(errorList.Single().Any());
            Assert.AreEqual("The field TestStringInSubsetThree must be a string with a maximum length of 20.", errorList.Single());
        }

        [Test]
        public void TestValidationSubsetAttributeSubsetOneMethodWithInvalidModel()
        {
            TestModel testModel = new TestModel();


            var responseTask = testModel.OneAsync();

            var continuedTask = responseTask.ContinueWith(t =>
                {
                    Assert.IsTrue(t.Exception != null);
                    Assert.IsTrue(t.Exception.InnerException != null);
                    Assert.IsTrue(t.Exception.InnerException.GetType() == typeof(ValidationException));
                    Assert.AreEqual("The TestStringInSubsetsOneTwoThree field is required.", testModel.ValidationErrors.Single());
                }, TaskContinuationOptions.OnlyOnFaulted);

            continuedTask.Wait();

            Assert.IsTrue(responseTask.IsFaulted);

            // no need to get the result from this task. Calling Result will throw an AggregateException with the ValidationException as the InnerException.
            //var response = responseTask.Result;
        }

        [Test]
        public void TestValidationSubsetAttributeSubsetThreeMethodWithInvalidModel()
        {
            TestModel testModel = new TestModel()
            {
                TestStringInSubsetsOneTwoThree = "dfrgdr",
                //TestStringInSubsetThree = null,
                TestStringInSubsetTwo = "test2",
                TestStringInZeroSubsets = "sytrng",
                TestInteger = 5
            };

            var responseTask = testModel.ThreeAsync();

            var continuedTask = responseTask.ContinueWith(t =>
            {
                Assert.IsTrue(t.Exception != null);
                Assert.IsTrue(t.Exception.InnerException != null);
                Assert.IsTrue(t.Exception.InnerException.GetType() == typeof(ValidationException));
                Assert.AreEqual("The TestStringInSubsetThree field is required.", testModel.ValidationErrors.Single());
                Debug.WriteLine("continuation task successful");
            }, TaskContinuationOptions.OnlyOnFaulted);

            Assert.IsTrue(responseTask.IsFaulted);

            continuedTask.Wait();

            // no need to get the result from this task. Calling Result will throw an AggregateException with the ValidationException as the InnerException.
            //var response = responseTask.Result;
        }

        [Test]
        public void TestValidationSubsetAttributeSubsetOneTwoAndThree()
        {
            TestModel testModel = new TestModel();

            var responseTask = testModel.OneAsync();

            var continuedTask = responseTask.ContinueWith(t =>
            {
                Assert.IsTrue(t.Exception != null);
                Assert.IsTrue(t.Exception.InnerException != null);
                Assert.IsTrue(t.Exception.InnerException.GetType() == typeof(ValidationException));
                Assert.AreEqual("The TestStringInSubsetsOneTwoThree field is required.", testModel.ValidationErrors.Single());
            }, TaskContinuationOptions.OnlyOnFaulted);

            continuedTask.Wait();

            Assert.IsTrue(responseTask.IsFaulted);

            // no need to get the result from this task. Calling Result will throw an AggregateException with the ValidationException as the InnerException.
            //var response = responseTask.Result;

            responseTask = testModel.TwoAsync();

            continuedTask = responseTask.ContinueWith(t =>
            {
                Assert.IsTrue(t.Exception != null);
                Assert.IsTrue(t.Exception.InnerException != null);
                Assert.IsTrue(t.Exception.InnerException.GetType() == typeof(ValidationException));
                Assert.AreEqual("The TestStringInSubsetsOneTwoThree field is required.", testModel.ValidationErrors.Single());
            }, TaskContinuationOptions.OnlyOnFaulted);

            continuedTask.Wait();

            Assert.IsTrue(responseTask.IsFaulted);


            responseTask = testModel.ThreeAsync();

            continuedTask = responseTask.ContinueWith(t =>
            {
                Assert.IsTrue(t.Exception != null);
                Assert.IsTrue(t.Exception.InnerException != null);
                Assert.IsTrue(t.Exception.InnerException.GetType() == typeof(ValidationException));
                Assert.IsTrue(testModel.ValidationErrors.Count == 2);
                Assert.IsTrue(testModel.ValidationErrors.Contains("The TestStringInSubsetsOneTwoThree field is required."));
                Assert.IsTrue(testModel.ValidationErrors.Contains("The TestStringInSubsetThree field is required."));
            }, TaskContinuationOptions.OnlyOnFaulted);

            continuedTask.Wait();

            Assert.IsTrue(responseTask.IsFaulted);

        }

        [Test]
        public void TestRequestMethodAttributeCheckValidForOneSetButNotAnother()
        {
            TestRequestMethodModel testModel = new TestRequestMethodModel()
            {
                TestStringInPOSTPUTAndDELETE = "testString", // valid
                TestStringInZeroSubsets = new string('a', 50), // valid
                TestStringInPUTSubset = "testString3", // valid, in subset
                TestIntegerInZeroSubsets = 20, // invalid, not in any subset
                TestIntegerInSubsetDELETE = -1 // invalid, in sub set
            };

            var errorList = new List<string>();
            var result = testModel.TryValidatePropertySubset(errorList, RequestMethodEnum.DELETE);
            Assert.IsFalse(result);
            Assert.IsTrue(errorList.Single().Any());
            Assert.AreEqual("The field TestIntegerInSubsetDELETE must be between 0 and 10.", errorList.Single());

            var errorList2 = new List<string>();
            var result2 = testModel.TryValidatePropertySubset(errorList2, RequestMethodEnum.GET);
            Assert.IsTrue(result2);
            Assert.IsTrue(!errorList2.Any());
        }
    }
}