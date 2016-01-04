using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        internal string ApplicationName { get; set; }
        internal string LanguageCode { get; set; }

        internal void SetAuthentication(string username, string password)
        {
            AuthenticationHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCII.GetBytes($"{username}:{password}")));
        }

        public bool TryValidate(ICollection<string> errorList)
        {
            var list = Validate();
            foreach (var error in list)
            {
                errorList.Add(error);
            }
            return !errorList.Any();
        }

        public ICollection<string> Validate()
        {
            var context = new ValidationContext(this, null, null);
            var results = new Collection<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true);
            return results.Select(r => r.ErrorMessage).ToList();
        }
    }

    internal class request_base
    {
        [ScriptIgnore]
        internal AuthenticationHeaderValue authentication_header { get; set; }

        public string application_name { get; set; }
        public string language_code { get; set; }
    }

}
