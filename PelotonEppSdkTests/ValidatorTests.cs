using System;
using System.Collections.Generic;
using NUnit.Framework;
using PelotonEppSdk.Models;
using PelotonEppSdk.Validations;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static PelotonEppSdk.Validations.RequestMethodAttribute;
using static PelotonEppSdk.Validations.ValidationSubsetAttribute.GeneralEnum;

namespace PelotonEppSdkTests
{
    public class TestModel: RequestBase
    {
        [ValidationSubset(new [] { One, Two, Three })]
        [Required]
        public string TestStringInSubsetsOneTwoThree { get; set; }

        [ValidationSubset(Two)]
        [StringLength(50)]
        public string TestStringInSubsetTwo { get; set; }

        [ValidationSubset(new[] { Three })]
        [Required]
        [StringLength(20)]
        public string TestStringInSubsetThree { get; set; }

        [Required]
        [StringLength(50)]
        public string TestStringInZeroSubsets { get; set; }

        [System.ComponentModel.DataAnnotations.Range(0, 10)]
        public int TestInteger { get; set; }
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
                testModel.TryValidatePropertySubset(null, One);
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
            var result = testModel.TryValidatePropertySubset(errorList, One);
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
            var result = testModel.TryValidatePropertySubset(errorList, One);
            Assert.IsFalse(result);
            Assert.IsTrue(errorList.Single().Any());
            Assert.AreEqual("The TestStringInSubsetsOneTwoThree field is required.", errorList.Single());
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
            var result = testModel.TryValidatePropertySubset(errorList, One);
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
            var result = testModel.TryValidatePropertySubset(errorList, Two);
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
            var result = testModel.TryValidatePropertySubset(errorList, Two);
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
            var result = testModel.TryValidatePropertySubset(errorList, Three);
            Assert.IsFalse(result);
            Assert.IsTrue(errorList.Single().Any());
            Assert.AreEqual("The field TestStringInSubsetThree must be a string with a maximum length of 20.", errorList.Single());
        }

        [Test]
        public void TestRequestMethodAttributeCheckValidForOneSetButNotAnother()
        {
            TestRequestMethodModel testModel = new TestRequestMethodModel()
            {
                TestStringInPOSTPUTAndDELETE = "testString", // valid,
                TestStringInZeroSubsets = new string('a', 50), // valid, but not part of the POST subset of properties
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