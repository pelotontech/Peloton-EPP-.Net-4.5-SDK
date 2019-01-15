using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using PelotonEppSdk.Enums;
using static System.Text.Encoding;
using Convert = System.Convert;
[assembly: InternalsVisibleTo("PelotonEppSdkTests")]

namespace PelotonEppSdk.Models
{
    public class RequestBase
    {
        [JsonIgnore]
        internal Uri BaseUri { get; set; }
        [JsonIgnore]
        internal AuthenticationHeaderValue AuthenticationHeader { get; private set; }
        [Required]
        internal string ApplicationName { get; set; }
        internal LanguageCode LanguageCode { get; set; }

        internal void SetAuthentication(string username, string password)
        {
            AuthenticationHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCII.GetBytes($"{username}:{password}")));
        }

        /// <summary>
        /// Validates the request. A return value indicates whether the validation succeeded.
        /// </summary>
        /// <param name="errorList">When this method returns, contains a list of any errors which were found during validation.
        /// The list must be instantiated before calling TryValidate</param>
        /// <returns><c>True</c> if no validation errors were found, <c>false</c> if errors were found</returns>
        /// <exception cref="ArgumentNullException"><paramref name="errorList"/> is <see langword="null" />.</exception>
        public bool TryValidate(ICollection<string> errorList)
        {
            if(errorList == null) throw new ArgumentNullException(nameof(errorList));
            var list = Validate();
            foreach (var error in list)
            {
                errorList.Add(error);
            }
            return !errorList.Any();
        }

        /// <summary>
        /// Validates the request. Return contains a collection of errors found during validation.
        /// </summary>
        /// <returns>A list of errors found during validation. List will be empty if no errors are found.</returns>
        public ICollection<string> Validate()
        {
            var context = new ValidationContext(this, null, null);
            var results = new Collection<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true);
            return results.Select(r => r.ErrorMessage).ToList();
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class request_base
    {
        protected request_base(RequestBase requestBase)
        {
            base_uri = requestBase.BaseUri;
            application_name = requestBase.ApplicationName;
            authentication_header = requestBase.AuthenticationHeader;
            language_code = Enum.GetName(typeof (LanguageCode), requestBase.LanguageCode);
        }


        [JsonIgnore]
        public Uri base_uri { get; set; }

        [JsonIgnore]
        internal AuthenticationHeaderValue authentication_header { get; set; }

        public string application_name { get; set; }

        public string language_code { get; set; }
    }

}
