using System.Diagnostics.CodeAnalysis;

namespace PelotonEppSdk.Models
{
    public class Reference
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class reference
    {
        public string name { get; set; }
        public string value { get; set; }

        public static explicit operator reference(Reference r)
        {
            return new reference {name = r.Name, value = r.Value};
        }

        public static explicit operator Reference(reference r)
        {
            return new Reference { Name = r.name, Value = r.value };
        }
    }
}
