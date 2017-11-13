using System;
using System.Globalization;
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
    public struct Amount : IXmlSerializable
    {
        private const string Separator = ".";
        private const string Separator2 = ",";
        private const string Format = "0.##";

        private decimal _value;

        private Amount(decimal amount)
        {
            _value = amount;
        }

        public static explicit operator Amount(decimal value)
        {
            decimal temp = Math.Round(value, 2, MidpointRounding.AwayFromZero);
            return new Amount(temp);
        }

        public static implicit operator decimal(Amount value)
        {
            return value._value;
        }

        public static explicit operator Amount(double value)
        {
            double temp = Math.Round(value, 2, MidpointRounding.AwayFromZero);
            return new Amount((decimal)temp);
        }

        public static explicit operator double(Amount value)
        {
            return (double) value._value;
        }

        public static explicit operator Amount(float value)
        {
            float temp = (float)Math.Round(value, 2, MidpointRounding.AwayFromZero);
            return new Amount((decimal)temp);
        }

        public static explicit operator float(Amount value)
        {
            return (float)value._value;
        }

        public override string ToString()
        {
            return ToString(Format);
        }

        public string ToString(string format)
        {
            if (null == format)
                throw new ArgumentNullException(nameof(format));

            var numberFormat = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            numberFormat.NumberDecimalSeparator = Separator;

            return _value.ToString(format, numberFormat);
        }

        public string ToBankString()
        {
            var value = (int) (_value*100);

            return value.ToString("0", CultureInfo.InvariantCulture.NumberFormat);
        }

        public static Amount Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            Amount amount;

            if (!TryParse(value, out amount))
                throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The string '{0}' is not a valid Amount value.", value));

            return amount;
        }

        public static Amount BankParse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            Amount amount;

            if (!TryBankParse(value, out amount))
                throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The string '{0}' is not a valid Amount value.", value));

            return amount;
        }
        
        public static bool TryParse(string value, out Amount amount)
        {
            if (string.IsNullOrEmpty(value))
            {
                amount = default(Amount);
                return false;
            }

            value = value.Replace(Separator2, Separator);

            int sepInd = value.IndexOf(Separator, StringComparison.Ordinal);

            // TODO: [L] Заменить ручной парсинг на стандартный (с указанием формата разделителей).
            if (sepInd >= 0)
            {
                string temp = value.Substring(0, sepInd + 1);

                if (value.Length > sepInd)
                    temp += value.Substring(sepInd + 1, value.Length - sepInd - 1).Replace(Separator, string.Empty);

                value = temp;
            }

            decimal @decimal;

            if (!decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat, out @decimal))
            {
                amount = default(Amount);
                return false;
            }

            amount = new Amount(@decimal);
            return true;
        }

        public static bool TryBankParse(string value, out Amount amount)
        {
            if (string.IsNullOrEmpty(value))
            {
                amount = default(Amount);
                return false;
            }

            int @int;

            if (!int.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat, out @int))
            {
                amount = default(Amount);
                return false;
            }

            amount = (Amount) (@int/100M);
            return true;
        }

        public static bool operator ==(Amount lhs, Amount rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Amount lhs, Amount rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator >(Amount lhs, Amount rhs)
        {
            return lhs._value > rhs._value;
        }

        public static bool operator <(Amount lhs, Amount rhs)
        {
            return lhs._value < rhs._value;
        }

        public static bool operator >=(Amount lhs, Amount rhs)
        {
            return lhs._value >= rhs._value;
        }

        public static bool operator <=(Amount lhs, Amount rhs)
        {
            return lhs._value <= rhs._value;
        }

        public bool Equals(Amount other)
        {
            return _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Amount && Equals((Amount)obj);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        #region IXmlSerializable Members

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            _value = Parse(reader.ReadElementContentAsString())._value;
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteString(ToString());
        }

        #endregion
    }
}