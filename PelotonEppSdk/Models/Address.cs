﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PelotonEppSdk.Models
{
    public class Address
    {
        /// <summary>
        /// Address line 1
        /// </summary>
        [StringLength(256)]
        public string Address1 { get; set; }

        /// <summary>
        /// Address line 2
        /// </summary>
        [StringLength(256)]
        public string Address2 { get; set; }

        /// <summary>
        /// Name of the city
        /// </summary>
        [StringLength(50)]
        public string City { get; set; }

        /// <summary>
        /// The province or state code
        /// </summary>
        [StringLength(2)]
        public string ProvinceStateCode { get; set; }

        /// <summary>
        /// The 2 digit country ISO code
        /// </summary>
        [StringLength(2)]
        public string CountryCode { get; set; }

        [MaxLength(256)]
        public string OtherCountry { get; set; }

        /// <summary>
        ///The postal or zip code
        /// </summary>
        [StringLength(10)]
        public string PostalZipCode { get; set; }

        public override bool Equals(object obj)
        {
            var a = obj as Address;
            if (a == null)
            {
                return false;
            }
            // Addresses can be equal even if the case differs or the leading/trailing whitespace differs
            return (
                string.Equals(Address1?.Trim(), a.Address1?.Trim(), StringComparison.OrdinalIgnoreCase)
                && string.Equals(Address2?.Trim(), a.Address2?.Trim(), StringComparison.OrdinalIgnoreCase)
                && string.Equals(City?.Trim(), a.City?.Trim(), StringComparison.OrdinalIgnoreCase)
                && string.Equals(ProvinceStateCode?.Trim(), a.ProvinceStateCode?.Trim(), StringComparison.OrdinalIgnoreCase)
                && string.Equals(CountryCode?.Trim(), a.CountryCode?.Trim(), StringComparison.OrdinalIgnoreCase)
                && string.Equals(OtherCountry?.Trim(), a.OtherCountry?.Trim(), StringComparison.OrdinalIgnoreCase)
                && string.Equals(PostalZipCode?.Trim(), a.PostalZipCode?.Trim(), StringComparison.OrdinalIgnoreCase)
                );
        }

        public bool IsEmpty => string.IsNullOrWhiteSpace(Address1)
                               && string.IsNullOrWhiteSpace(Address2)
                               && string.IsNullOrWhiteSpace(City)
                               && string.IsNullOrWhiteSpace(PostalZipCode);

        protected bool Equals(Address address)
        {
            return Equals((object) address);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Address1?.GetHashCode() ?? 0;
                hashCode = (hashCode*397) ^ (Address2?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (City?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (ProvinceStateCode?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (CountryCode?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (OtherCountry?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (PostalZipCode?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class address
    {
        public string address1 { get; set; }

        public string address2 { get; set; }

        public string city { get; set; }

        public string province_state { get; set; }

        public string country_code { get; set; }

        public string postal_zip_code { get; set; }

        public static explicit operator address(Address add)
        {
            if (add == null) return null;

            return new address
            {
                address1 = add.Address1,
                address2 = add.Address2,
                city = add.City,
                country_code = add.CountryCode,
                postal_zip_code = add.PostalZipCode,
                province_state = add.ProvinceStateCode
            };
        }
    }
}