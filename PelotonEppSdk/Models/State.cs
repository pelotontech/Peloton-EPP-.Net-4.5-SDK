using System.Diagnostics.CodeAnalysis;

namespace PelotonEppSdk.Models
{
    public class State
    {
        /// <summary>
        /// Name of the state
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Code representing the state
        /// </summary>
        public string Code { get; set; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    internal class state
    {
        public string name { get; set; }
        public string code { get; set; }

        public static explicit operator state(State s)
        {
            return new state { name = s.Name, code = s.Code };
        }

        public static explicit operator State(state s)
        {
            return new State { Name = s.name, Code = s.code };
        }
    }
}
