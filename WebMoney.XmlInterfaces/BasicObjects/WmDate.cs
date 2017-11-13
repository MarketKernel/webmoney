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
    public struct WmDate : IXmlSerializable
    {
        private const string LocalTemplate = "dd.MM.yyyy";

        private const string Template1 = "yyyy.MM.dd";
        private const string Template2 = @"MM\/dd\/yyyy";
        private const string Template3 = @"yyyy-MM-dd";

        private DateTime _utcTime;

        private WmDate(DateTime utcTime)
        {
            _utcTime = utcTime;
        }

        public DateTime ToUniversalTime()
        {
            return _utcTime;
        }

        public static explicit operator WmDate(DateTime value)
        {
            return new WmDate(value.ToUniversalTime());
        }

        public static implicit operator DateTime(WmDate value)
        {
            return value._utcTime.ToLocalTime();
        }

        public static WmDate Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            WmDate wmDate;

            if (!TryParse(value, out wmDate))
                throw new FormatException(string.Format(CultureInfo.InvariantCulture,
                                                        "The string '{0}' is not a valid WmDate value.", value));

            return wmDate;
        }

        internal static WmDate ParseServerString(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            WmDate wmDate;

            if (!TryParseServerString(value, out wmDate))
                throw new FormatException(string.Format(CultureInfo.InvariantCulture,
                                                        "The string '{0}' is not a valid WmDate value.", value));

            return wmDate;
        }

        public static bool TryParse(string value, out WmDate wmDate)
        {
            if (string.IsNullOrEmpty(value))
            {
                wmDate = default(WmDate);
                return false;
            }

            DateTime dateTime;

            if (
                !DateTime.TryParseExact(value, LocalTemplate, CultureInfo.InvariantCulture.DateTimeFormat,
                                        DateTimeStyles.None, out dateTime))
            {
                wmDate = default(WmDate);
                return false;
            }

            wmDate = (WmDate) dateTime;
            return true;
        }

        internal static bool TryParseServerString(string value, out WmDate wmDate)
        {
            if (string.IsNullOrEmpty(value))
            {
                wmDate = default(WmDate);
                return false;
            }

            DateTime serverTime;

            if (!DateTime.TryParseExact(value, Template1, CultureInfo.InvariantCulture.DateTimeFormat,
                DateTimeStyles.None, out serverTime))
            {
                if (!DateTime.TryParseExact(value, Template2, CultureInfo.InvariantCulture.DateTimeFormat,
                    DateTimeStyles.None, out serverTime))
                {
                    if (!DateTime.TryParseExact(value, Template3, CultureInfo.InvariantCulture.DateTimeFormat,
                        DateTimeStyles.None, out serverTime))
                    {
                        wmDate = default(WmDate);
                        return false;
                    }
                }
            }

            serverTime = new DateTime(serverTime.Year, serverTime.Month, serverTime.Day, 0, 0, 0);
            wmDate = new WmDate(WmDateTime.ServerTime2UtcTime(serverTime));
            return true;
        }

        internal string ToServerString()
        {
            DateTime serverDate = WmDateTime.UtcTime2ServerTime(_utcTime);
            return serverDate.ToString(Template1, CultureInfo.InvariantCulture.DateTimeFormat);
        }

        internal string ToServerString(string format)
        {
            DateTime serverDate = WmDateTime.UtcTime2ServerTime(_utcTime);
            return serverDate.ToString(format, CultureInfo.InvariantCulture.DateTimeFormat);
        }

        public override string ToString()
        {
            return _utcTime.ToLocalTime().ToString(LocalTemplate, CultureInfo.InvariantCulture.DateTimeFormat);
        }

        public static bool operator ==(WmDate lhs, WmDate rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(WmDate lhs, WmDate rhs)
        {
            return !lhs.Equals(rhs);
        }

        public bool Equals(WmDate other)
        {
            return _utcTime.Year == other._utcTime.Year
                   && _utcTime.Month == other._utcTime.Month
                   && _utcTime.Day == other._utcTime.Day;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is WmDate && Equals((WmDate) obj);
        }

        public override int GetHashCode()
        {
            return _utcTime.GetHashCode();
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

            _utcTime = ParseServerString(reader.ReadElementContentAsString())._utcTime;
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (null == writer)
                throw new ArgumentNullException(nameof(writer));

            writer.WriteString(ToServerString());
        }

        #endregion
    }
}