using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using PelotonEppSdk.Enums;
using static System.Text.Encoding;
using Convert = System.Convert;

namespace PelotonEppSdk.Models
{
    public class RequestBase
    {
        [ScriptIgnore]
        internal Uri BaseUri { get; set; }
        [ScriptIgnore]
        internal AuthenticationHeaderValue AuthenticationHeader { get; private set; }
        internal string ApplicationName { get; set; }
        internal LanguageCode LanguageCode { get; set; }

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

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class request_base
    {
        public request_base(RequestBase requestBase)
        {
            base_uri = requestBase.BaseUri;
            application_name = requestBase.ApplicationName;
            authentication_header = requestBase.AuthenticationHeader;
            language_code = Enum.GetName(typeof (LanguageCode), requestBase.LanguageCode);
        }


        [ScriptIgnore]
        public Uri base_uri { get; set; }

        [ScriptIgnore]
        internal AuthenticationHeaderValue authentication_header { get; set; }

        public string application_name { get; set; }
        public string language_code { get; set; }
    }

}
