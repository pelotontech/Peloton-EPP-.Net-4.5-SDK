using System;
using System.Collections.Generic;
using PelotonEppSdk.Enums;

namespace PelotonEppSdk.Models
{
    public class CreditCardNumber
    {
        /* This value should never have a setter.
           If setting is allowed, all implicit operators should be disabled to avoid confusion.
        */
        public long CardNumber { get; }

        public int Length => ((string) this).Length;

        public static readonly Dictionary<CreditCardType, string> DISPLAYNAME = new Dictionary<CreditCardType, string>
        {
            {CreditCardType.Amex, "Amex"},
            {CreditCardType.JCB, "JCB"},
            {CreditCardType.MasterCard, "MasterCard"},
            {CreditCardType.Visa, "Visa"},
            {CreditCardType.Unknown, "N/A"}
        };

        public string CardName => DISPLAYNAME[CardType];

        public CreditCardType CardType
        {
            get
            {
                var cardNumber = CardNumber.ToString();
                if (cardNumber.StartsWith("4")) return CreditCardType.Visa;
                if (cardNumber.StartsWith("5"))
                {
                    if (cardNumber[1] == '1' || cardNumber[1] == '2' || cardNumber[1] == '3' || cardNumber[1] == '4' || cardNumber[1] == '5')
                        return CreditCardType.MasterCard;
                }

                if (cardNumber.StartsWith("3"))
                {
                    if (cardNumber[1] == '4' || cardNumber[1] == '7') return CreditCardType.Amex;
                    if (cardNumber[1] == '5') return CreditCardType.JCB;
                }

                return CreditCardType.Unknown;
            }
        }

        public decimal Last4Digits
        {
            get
            {
                var cardNumber = CardNumber.ToString();
                return Convert.ToDecimal(cardNumber.Substring(cardNumber.Length - 4, 4));
            }
        }

        public string Masked
        {
            get
            {
                var cardNumber = CardNumber.ToString();
                string retValue;
                if (cardNumber.Length > 10)
                {
                    retValue = cardNumber.Substring(0, 6) + new string('*', cardNumber.Length - 10) + cardNumber.Substring(cardNumber.Length - 4);
                }
                else
                {
                    retValue = new string('*', cardNumber.Length - 4) + cardNumber.Substring(cardNumber.Length - 4);
                }
                return retValue;
            }
        }

        public CreditCardNumber(long creditCardNumber)
        {
            CardNumber = creditCardNumber;
        }

        public CreditCardNumber(string creditCardNumber)
        {
            if (creditCardNumber.Length < 3) throw new ArgumentException("Credit card number must be at least 3 digits long.");
            CardNumber = Convert.ToInt64(creditCardNumber);
        }


        public static implicit operator long?(CreditCardNumber cn)
        {
            return cn?.CardNumber;
        }

        public static implicit operator CreditCardNumber(long? creditCardNumber)
        {
            return creditCardNumber.HasValue? new CreditCardNumber(creditCardNumber.Value) : null;
        }

        public static implicit operator CreditCardNumber(long creditCardNumber)
        {
            return new CreditCardNumber(creditCardNumber);
        }

        public static explicit operator CreditCardNumber(string creditCardNumber)
        {
            if (creditCardNumber == null) return null;
            if(creditCardNumber.Length <3)throw new InvalidCastException("Credit card number must be at least 3 digits long.");
            return new CreditCardNumber(creditCardNumber);
        }

        public static implicit operator string(CreditCardNumber creditCardNumber)
        {
            return creditCardNumber?.CardNumber.ToString();
        }

        public override string ToString()
        {
            return this;
        }
    }
}