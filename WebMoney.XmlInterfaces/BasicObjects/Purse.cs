using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WebMoney.XmlInterfaces.BasicObjects
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    public struct Purse : IXmlSerializable
    {
        private const string Format = "000000000000";
        private const string Pattern = @"^[A-Z]\d{12}$";

        private ulong _number;
        private WmCurrency _type;

        private Purse(string purseStr)
        {
            _type = GetType(purseStr);
            _number = GetNumber(purseStr);
        }

        public Purse(WmCurrency type, ulong number)
        {
            _type = type;
            _number = number;
        }

        public WmCurrency Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public ulong Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public override string ToString()
        {
            char letter = CurrencyToLetter(_type);
            return letter + _number.ToString(Format, CultureInfo.InvariantCulture);
        }

        private static WmCurrency GetType(string purse)
        {
            char letter = purse.Substring(0, 1)[0];
            return LetterToCurrency(letter);
        }

        private static ulong GetNumber(string purse)
        {
            string number = purse.Substring(1);
            return ulong.Parse(number, NumberStyles.Integer, CultureInfo.InvariantCulture);
        }

        public static WmCurrency LetterToCurrency(char letter)
        {
            return (WmCurrency) Enum.Parse(typeof(WmCurrency), letter.ToString());
        }

        public static char CurrencyToLetter(WmCurrency currency)
        {
            if (WmCurrency.None == currency)
                throw new ArgumentOutOfRangeException(nameof(currency));

            var letter = currency.ToString();

            if (1 != letter.Length)
                throw new ArgumentOutOfRangeException(nameof(currency));

            return letter[0];
        }

        public static Purse Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            Purse purse;

            if (!TryParse(value, out purse))
                throw new FormatException(string.Format(CultureInfo.InvariantCulture,
                                                        "The string '{0}' is not a valid Purse value.", value));

            return purse;
        }

        public static bool TryParse(string value, out Purse purse)
        {
            if (string.IsNullOrEmpty(value))
            {
                purse = default(Purse);
                return false;
            }

            Match match = Regex.Match(value, Pattern);

            if (match.Value != value)
            {
                purse = default(Purse);
                return false;
            }

            purse = new Purse(value);
            return true;
        }

        public static bool operator ==(Purse lhs, Purse rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Purse lhs, Purse rhs)
        {
            return !lhs.Equals(rhs);
        }

        public bool Equals(Purse other)
        {
            return _type == other.Type
                   && _number == other.Number;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Purse && Equals((Purse) obj);
        }

        public override int GetHashCode()
        {
            return _type.GetHashCode() + 29*(int) _number;
        }

        #region IXmlSerializable Members

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (null == reader)
                throw new ArgumentNullException(nameof(reader));

            Purse purse = Parse(reader.ReadElementContentAsString());

            _type = purse._type;
            _number = purse._number;
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (null == writer)
                throw new ArgumentNullException(nameof(writer));

            writer.WriteString(ToString());
        }

        #endregion
    }
}