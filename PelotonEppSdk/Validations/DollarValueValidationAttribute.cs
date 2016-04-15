using System;
using System.ComponentModel.DataAnnotations;

namespace PelotonEppSdk.Validations
{
    /// <summary>
    /// Validates that the property is a multiple of 0.01.
    /// </summary>
    internal class DecimalDollarValueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var decValue = (decimal)value * 100;
            var intValue = (int) decValue;
            return decValue == intValue;
        }
    }
}
