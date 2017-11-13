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
    public struct WmId : IXmlSerializable
    {
        public const int Length = 12;

        private const string Format = "000000000000";
        private const ulong WmidMax = 999999999999;
        private const string Pattern = @"^\d{12}$";

        private ulong _wmId;

        private WmId(ulong wmId)
        {
            if (wmId > WmidMax)
                throw new ArgumentOutOfRangeException(nameof(wmId));

            _wmId = wmId;
        }

        public static explicit operator WmId(ulong value)
        {
            return new WmId(value);
        }

        public static explicit operator WmId(long value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            return new WmId((ulong)value);
        }

        public static implicit operator ulong(WmId value)
        {
            return value._wmId;
        }

        public static implicit operator long(WmId value)
        {
            return (long) value._wmId;
        }

        public override string ToString()
        {
            return _wmId.ToString(Format, CultureInfo.InvariantCulture);
        }

        public static WmId Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            WmId wmId;

            if (!TryParse(value, out wmId))
                throw new FormatException(string.Format(CultureInfo.InvariantCulture,
                                                        "The string '{0}' is not a valid WmId value.", value));

            return wmId;
        }

        public static bool TryParse(string value, out WmId wmId)
        {
            if (string.IsNullOrEmpty(value))
            {
                wmId = default(WmId);
                return false;
            }

            Match match = Regex.Match(value, Pattern);

            if (match.Value != value)
            {
                wmId = default(WmId);
                return false;
            }

            wmId = new WmId(ulong.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat));
            return true;
        }

        public static bool operator ==(WmId lhs, WmId rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(WmId lhs, WmId rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(WmId other)
        {
            return _wmId == other._wmId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is WmId && Equals((WmId) obj);
        }

        public override int GetHashCode()
        {
            return _wmId.GetHashCode();
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

            _wmId = Parse(reader.ReadElementContentAsString())._wmId;
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