using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using static System.Text.Encoding;
using Convert = System.Convert;

namespace PelotonEppSdk.Models
{
    public class RequestBase
    {
        [ScriptIgnore]
        internal AuthenticationHeaderValue AuthenticationHeader { get; private set; }

        internal void SetAuthentication(string username, string password)
        {
            AuthenticationHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCII.GetBytes($"{username}:{password}")));
        }
    }

    internal class request_base
    {
        [ScriptIgnore]
        internal AuthenticationHeaderValue authentication_header { get; set; }
    }

}
