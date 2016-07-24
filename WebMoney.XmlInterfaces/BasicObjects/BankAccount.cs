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
    public struct BankAccount : IXmlSerializable
    {
        private const string Pattern = @"^\d+$";

        private string _number;

        BankAccount(string number)
        {
            _number = number;
        }

        public override string ToString()
        {
            return _number;
        }

        public static BankAccount Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            BankAccount bankAccount;

            if (!TryParse(value, out bankAccount))
                throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The string '{0}' is not a valid BankCard value.", value));

            return bankAccount;
        }

        public static bool TryParse(string value, out BankAccount bankAccount)
        {
            if (string.IsNullOrEmpty(value))
            {
                bankAccount = default(BankAccount);
                return false;
            }

            Match match = Regex.Match(value, Pattern);

            if (match.Value != value)
            {
                bankAccount = default(BankAccount);
                return false;
            }

            bankAccount = new BankAccount(value);
            return true;
        }

        public static bool operator ==(BankAccount lhs, BankAccount rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(BankAccount lhs, BankAccount rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(BankAccount other)
        {
            if (null == _number)
                return null == other._number;

            return _number.Equals(other._number, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is BankAccount && Equals((BankAccount)obj);
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