using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace PelotonEppSdk.Models
{
    public class EventItem
    {
        /// <summary>
        /// The name for the event item.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The description for the event item.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Whether quantity selection is allowed for an event item.
        /// </summary>
        public bool QuantitySelector { get; set; }
        /// <summary>
        /// The default unit quantity.
        /// </summary>
        public int DefaultUnitQuantity { get; set; }
        /// <summary>
        /// The description for the unit quantity.
        /// </summary>
        public string UnitQuantityDescription { get; set; }
        /// <summary>
        /// The unit amount for an event item.
        /// </summary>
        public decimal? UnitAmount { get; set; }
        /// <summary>
        /// The amount associated with an event item.
        /// </summary>
        public decimal? Amount { get; set; }
        /// <summary>
        /// Is the amount associated with an event item adjustable? Default: false
        /// </summary>
        public bool? AmountAdjustable { get; set; }
        /// <summary>
        /// A list of custom fields associated with an event.
        /// </summary>
        public ICollection<EventCustomField> CustomFields { get; set; }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
        internal class event_item
        {
            public string name { get; set; }
            public string description { get; set; }
            public bool quantity_selector { get; set; } 
            public int default_unit_quantity { get; set; }
            public string unit_quantity_description { get; set; }
            public decimal? unit_amount { get; set; }
            public decimal? amount { get; set; }
            public bool? amount_adjustable { get; set; }
            public ICollection<event_custom_field> custom_fields { get; set; }
            public static explicit operator EventItem(event_item ei)
            {
                if (ei == null) return null;

                ICollection<EventCustomField> customFields = null;
                if (ei.custom_fields != null)
                    customFields = ei.custom_fields.Select(cf => (EventCustomField)cf).ToList();

                return new EventItem
                {
                    Name = ei.name,
                    Description = ei.description,
                    QuantitySelector = ei.quantity_selector,
                    DefaultUnitQuantity = ei.default_unit_quantity,
                    UnitQuantityDescription = ei.unit_quantity_description,
                    UnitAmount = ei.unit_amount,
                    Amount = ei.amount,
                    AmountAdjustable = ei.amount_adjustable,
                    CustomFields = customFields
                };
            }
        }
    }
}
