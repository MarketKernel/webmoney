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
    public struct BankCard : IXmlSerializable
    {
        private const string Pattern = @"^\d+$";

        private string _number;

        BankCard(string number)
        {
            _number = number;
        }

        public override string ToString()
        {
            return _number;
        }

        public static BankCard Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            BankCard bankCard;

            if (!TryParse(value, out bankCard))
                throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The string '{0}' is not a valid BankCard value.", value));

            return bankCard;
        }

        public static bool TryParse(string value, out BankCard bankCard)
        {
            if (string.IsNullOrEmpty(value))
            {
                bankCard = default(BankCard);
                return false;
            }

            Match match = Regex.Match(value, Pattern);

            if (match.Value != value)
            {
                bankCard = default(BankCard);
                return false;
            }

            bankCard = new BankCard(value);
            return true;
        }

        public static bool operator ==(BankCard lhs, BankCard rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(BankCard lhs, BankCard rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(BankCard other)
        {
            if (null == _number)
                return null == other._number;

            return _number.Equals(other._number, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is BankCard && Equals((BankCard)obj);
        }

        public override int GetHashCode()
        {
            return _number?.GetHashCode() ?? 0;
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

            _number = Parse(reader.ReadElementContentAsString())._number;
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