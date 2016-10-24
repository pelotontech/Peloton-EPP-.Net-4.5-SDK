using System.Diagnostics.CodeAnalysis;

namespace PelotonEppSdk.Models
{
    public class EventCustomField
    {
        /// <summary>
        /// The name of this custom field.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The default value for this custom field.
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// The type for this custom field.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// The display order for this custom field.
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// Whether this custom field is required.
        /// </summary>
        public bool Required { get; set; }  
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class event_custom_field
    {
        public string name { get; set; }
        public string default_value { get; set; }
        public string type { get; set; }
        public int display_order { get; set; }
        public bool required { get; set; }

        public static explicit operator EventCustomField(event_custom_field ei)
        {
            return new EventCustomField
            {
                Name = ei.name,
                DefaultValue = ei.default_value,
                Type = ei.type,
                DisplayOrder = ei.display_order,
                Required = ei.required
            };
        }
    }
}
