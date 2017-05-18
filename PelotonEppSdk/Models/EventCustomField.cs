using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using PelotonEppSdk.Enums;

namespace PelotonEppSdk.Models
{
    public class EventCustomField
    {
        /// <summary>
        /// The name of this custom field.
        /// </summary>
        [StringLength(50, MinimumLength = 0, ErrorMessage = "Name must be 50 or fewer characters in length.")]
        public string Name { get; set; }
        /// <summary>
        /// The default value for this custom field.
        /// </summary>
        [StringLength(128, MinimumLength = 0, ErrorMessage = "DefaultValue must be 128 or fewer characters in length.")]
        public string DefaultValue { get; set; }
        /// <summary>
        /// The type for this custom field.
        /// </summary>
        //[Required]
        public EventCustomFieldTypeEnum Type { get; set; }
        /// <summary>
        /// The display order for this custom field.
        /// </summary>
        //[Required]
        [Range(0, 999, ErrorMessage = "DisplayOrder must be between 0 and 999.")]
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
        public type type { get; set; }
        public int display_order { get; set; }
        public bool required { get; set; }

        public static explicit operator EventCustomField(event_custom_field ei)
        {

            EventCustomFieldTypeEnum type = EventCustomFieldTypeEnum.Invalid;

            if (ei.type?.code != null)
            {
                var parseResult = Enum.TryParse(ei.type.code, out type);
                if (!parseResult) Debug.WriteLine("Failed to parse custom field type code.");
            }

            return new EventCustomField
            {
                Name = ei.name,
                DefaultValue = ei.default_value,
                Type = type,
                DisplayOrder = ei.display_order,
                Required = ei.required
            };
        }

        public static explicit operator event_custom_field(EventCustomField ei)
        {
            return new event_custom_field
            {
                name = ei.Name,
                default_value = ei.DefaultValue,
                type = new type()
                {
                    code = ((int)ei.Type).ToString(),
                    name = ei.Type.ToString()
                },
                display_order = ei.DisplayOrder,
                required = ei.Required
            };
        }
    }
}
