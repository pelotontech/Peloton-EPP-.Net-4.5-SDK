using System;
using System.Linq;

namespace PelotonEppSdk.Validations
{
    public interface IValidationSubsetAttribute
    {
        Enum[] ValidationSubsetEnum { get; set; }
    }

    public class ValidationSubsetAttribute : Attribute, IValidationSubsetAttribute
    {
        public enum GeneralEnum
        {
            One,
            Two,
            Three
        }

        public Enum[] ValidationSubsetEnum { get; set; }
        public Type EnumType;

        public ValidationSubsetAttribute(Enum[] method)
        {
            ValidationSubsetEnum = method;
            EnumType = method.GetType();
        }

        public ValidationSubsetAttribute(Enum method)
        {
            ValidationSubsetEnum = new [] { method };
            EnumType = method.GetType();
        }

        public ValidationSubsetAttribute(GeneralEnum[] method)
        {
            ValidationSubsetEnum = ConvertToEnumArray(method);
            EnumType = method.GetType();
        }

        public ValidationSubsetAttribute(GeneralEnum method)
        {
            ValidationSubsetEnum = ConvertToEnumArray(method);
            EnumType = method.GetType();
        }

        protected ValidationSubsetAttribute()
        {
        }

        public Enum[] ConvertToEnumArray<T>(T partitionEnum)
        {
            return new[]{  partitionEnum as Enum };
        }

        public Enum[] ConvertToEnumArray<T>(T[] partitionEnum)
        {
            return partitionEnum.Select(m => m as Enum).ToArray();
        }
    }

    public class RequestMethodAttribute: ValidationSubsetAttribute
    {
        public enum RequestMethodEnum
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public RequestMethodAttribute(RequestMethodEnum[] method)
        {
            ValidationSubsetEnum = ConvertToEnumArray(method);
        }

        public RequestMethodAttribute(RequestMethodEnum method)
        {
            ValidationSubsetEnum = ConvertToEnumArray(method);
        }
    }

}