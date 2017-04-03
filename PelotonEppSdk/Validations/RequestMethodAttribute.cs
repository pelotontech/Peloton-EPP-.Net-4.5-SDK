// ReSharper disable InconsistentNaming
namespace PelotonEppSdk.Validations
{
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