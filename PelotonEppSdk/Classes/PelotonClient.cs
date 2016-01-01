using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using PelotonEppSdk.Enums;

namespace PelotonEppSdk.Classes
{
    public class PelotonClient
    {
        public async Task<T> PostAsync<T>(HttpContent content, ApiTarget target)
        {
            var factory = new TestUriFactory();
            using (var client = new HttpClient())
            {
                client.BaseAddress = factory.GetBaseUri();
                var httpResponseMessage = await client.PostAsync(factory.GetTargetUriPart(target), content);
                var stringResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
                var serializer = new JavaScriptSerializer();
                return serializer.Deserialize<T>(stringResult);
            }
        }
    }
}
