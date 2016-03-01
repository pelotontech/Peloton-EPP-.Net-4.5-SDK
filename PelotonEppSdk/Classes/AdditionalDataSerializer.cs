using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using PelotonEppSdk.Models;

namespace PelotonEppSdk.Classes
{
    public class AdditionalDataSerializer : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            var result = new bank_account_delete_request();
            foreach (var keyValuePair in dictionary)
            {
                //result.SetTable(keyValuePair.Key, Convert.FromBase64String((string)keyValuePair.Value));
            }
            return result;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var request = obj as bank_account_delete_request;
            //return request.bank_account_token;
                //.Where(t => t.Value?.ToBytes() != null)
                //.ToDictionary<
                //    KeyValuePair<string, AdditionalDatawireDataFieldBase> // Type of our current object (additionalDatawireData.TableDictionary)
                //    , string // Key of our target dictionary
                //    , object> // value of our our target dictionary
                //    (table => table.Key, table => Convert.ToBase64String(table.Value.ToBytes()));
            return null;
        }

        public override IEnumerable<Type> SupportedTypes => new List<Type> { typeof(bank_account_request) };
    }
}
