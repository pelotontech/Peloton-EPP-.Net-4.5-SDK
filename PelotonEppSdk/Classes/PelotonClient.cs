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
            var factory = new TestUriFactory();
            var serializer = new JavaScriptSerializer();
            var serializedContent = serializer.Serialize(content);
            var stringContent = new StringContent(serializedContent,Encoding.Default, "application/json");
            string stringResult;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = content.authentication_header;
                client.BaseAddress = factory.GetBaseUri();
                var targetUriPart = factory.GetTargetUriPart(target);
                var httpResponseMessage = await client.PostAsync(targetUriPart, stringContent);
                stringResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
            }
            return serializer.Deserialize<T>(stringResult);
        }
    }
}
