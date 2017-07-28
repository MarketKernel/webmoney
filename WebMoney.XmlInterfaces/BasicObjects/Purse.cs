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
        private const string Pattern = @"^[Z|E|R|U|B|Y|G|D|C|X]\d{12}$";

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
            WmCurrency currency;

            switch (letter)
            {
                case 'Z':
                    currency = WmCurrency.Z;
                    break;
                case 'E':
                    currency = WmCurrency.E;
                    break;
                case 'R':
                    currency = WmCurrency.R;
                    break;
                case 'U':
                    currency = WmCurrency.U;
                    break;
                case 'B':
                    currency = WmCurrency.B;
                    break;
                case 'Y':
                    currency = WmCurrency.Y;
                    break;
                case 'G':
                    currency = WmCurrency.G;
                    break;
                case 'D':
                    currency = WmCurrency.D;
                    break;
                case 'C':
                    currency = WmCurrency.C;
                    break;
                case 'X':
                    currency = WmCurrency.X;
                    break;
                default:
                    throw new FormatException("letter == " + letter);
            }

            return currency;
        }

        public static char CurrencyToLetter(WmCurrency currency)
        {
            char letter;

            switch (currency)
            {
                case WmCurrency.Z:
                    letter = 'Z';
                    break;
                case WmCurrency.E:
                    letter = 'E';
                    break;
                case WmCurrency.Y:
                    letter = 'Y';
                    break;
                case WmCurrency.R:
                    letter = 'R';
                    break;
                case WmCurrency.U:
                    letter = 'U';
                    break;
                case WmCurrency.B:
                    letter = 'B';
                    break;
                case WmCurrency.G:
                    letter = 'G';
                    break;
                case WmCurrency.D:
                    letter = 'D';
                    break;
                case WmCurrency.C:
                    letter = 'C';
                    break;
                case WmCurrency.X:
                    letter = 'X';
                    break;
                default:
                    throw new FormatException("currency == " + currency);
            }

            return letter;
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