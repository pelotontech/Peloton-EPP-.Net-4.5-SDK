using System.Diagnostics.CodeAnalysis;

namespace PelotonEppSdk.Models
{
    public class GenericType
    {
        /// <summary>
        /// Name of the type
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Code representing the type
        /// </summary>
        public string Code { get; set; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    internal class type
    {
        public string name { get; set; }
        public string code { get; set; }

        public static explicit operator type(GenericType t)
        {
            return new type { name = t.Name, code = t.Code };
        }

        public static explicit operator GenericType(type t)
        {
            return new GenericType { Name = t.name, Code = t.code };
        }
    }
}