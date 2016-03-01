using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using PelotonEppSdk.Enums;
using PelotonEppSdk.Models;

namespace PelotonEppSdk.Classes
{
    internal class PelotonClient
    {
        public async Task<T> PostAsync<T>(request_base content, ApiTarget target)
        {
            var factory = new UriFactory();
            var serializer = new JavaScriptSerializer();
            // TODO: invent a serializer which can convert the bank account delete request into the plain string that the API expects
            //serializer.RegisterConverters(new JavaScriptConverter[] { new AdditionalDataSerializer() });
            var serializedContent = serializer.Serialize(content);
            var stringContent = new StringContent(serializedContent,Encoding.Default, "application/json");
            string stringResult;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = content.authentication_header;
                client.BaseAddress = factory.GetBaseUri();
                var targetUriPart = factory.GetTargetUriPart(target);
                var httpResponseMessage = await client.PostAsync(targetUriPart, stringContent);

                // TODO: handle server errors
                
                stringResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
                
            }
            return serializer.Deserialize<T>(stringResult);
        }
    }
}
