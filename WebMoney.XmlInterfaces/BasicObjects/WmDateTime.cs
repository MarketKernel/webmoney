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
    public struct WmDateTime : IXmlSerializable
    {
        private const string LocalTemplate = "dd.MM.yyyy HH:mm:ss";

        private const string Template1 = "yyyyMMdd HH:mm:ss";
        private const string Template2 = "yyyy-MM-ddTHH:mm:ss.fff";
        private const string Template3 = "yyyy-MM-ddTHH:mm:ss";
        private const string Template4 = "dd.MM.yyyy HH:mm:ss";
        private const string Template5 = "dd.MM.yyyy H:mm:ss";
        private const string Template6 = "yyyy-MM-dd HH:mm:ss";
        private const string Template7 = "yyyy-MM-dd HH:mm:ss.fff";

        private const int PutinTimeZone = 3;
        //private const int MedvedevTimeZone = 4;

        // private static readonly DateTime summerTimeStart;
        // private static readonly DateTime winterTimeStart;
        private DateTime _utcTime;

        private WmDateTime(DateTime utcTime)
        {
            _utcTime = utcTime;
        }

        public DateTime ToUniversalTime()
        {
            return _utcTime;
        }

        public static implicit operator WmDateTime(DateTime value)
        {
            return new WmDateTime(value.ToUniversalTime());
        }

        public static implicit operator DateTime(WmDateTime value)
        {
            return value._utcTime.ToLocalTime();
        }

        public static WmDateTime Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            WmDateTime wmDateTime;

            if (!TryParse(value, out wmDateTime))
                throw new FormatException(string.Format(CultureInfo.InvariantCulture,
                                                        "The string '{0}' is not a valid WmDateTime value.", value));

            return wmDateTime;
        }

        internal static WmDateTime ParseServerString(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            WmDateTime wmDateTime;

            if (!TryParseServerString(value, out wmDateTime))
                throw new FormatException(string.Format(CultureInfo.InvariantCulture,
                                                        "The string '{0}' is not a valid WmDateTime value.", value));

            return wmDateTime;
        }

        public static bool TryParse(string value, out WmDateTime wmDateTime)
        {
            if (string.IsNullOrEmpty(value))
            {
                wmDateTime = default(WmDateTime);
                return false;
            }

            DateTime dateTime;

            if (
                !DateTime.TryParseExact(value, LocalTemplate, CultureInfo.InvariantCulture.DateTimeFormat,
                                        DateTimeStyles.None, out dateTime))
            {
                wmDateTime = default(WmDateTime);
                return false;
            }

            wmDateTime = dateTime;
            return true;
        }

        internal static bool TryParseServerString(string value, out WmDateTime wmDateTime)
        {
            if (string.IsNullOrEmpty(value))
            {
                wmDateTime = default(WmDateTime);
                return false;
            }

            DateTime serverTime;

            if (
                !DateTime.TryParseExact(
                    value, Template1, CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.None, out serverTime))
            {
                if (
                    !DateTime.TryParseExact(
                        value,
                        Template2,
                        CultureInfo.InvariantCulture.DateTimeFormat,
                        DateTimeStyles.None,
                        out serverTime))
                {
                    if (
                        !DateTime.TryParseExact(
                            value,
                            Template3,
                            CultureInfo.InvariantCulture.DateTimeFormat,
                            DateTimeStyles.None,
                            out serverTime))
                    {
                        if (
                            !DateTime.TryParseExact(
                                value,
                                Template4,
                                CultureInfo.InvariantCulture.DateTimeFormat,
                                DateTimeStyles.None,
                                out serverTime))
                        {
                            if (
                                !DateTime.TryParseExact(
                                    value,
                                    Template5,
                                    CultureInfo.InvariantCulture.DateTimeFormat,
                                    DateTimeStyles.None,
                                    out serverTime))
                            {
                                if (
                                    !DateTime.TryParseExact(
                                        value,
                                        Template6,
                                        CultureInfo.InvariantCulture.DateTimeFormat,
                                        DateTimeStyles.None,
                                        out serverTime))
                                {
                                    if (
                                     !DateTime.TryParseExact(
                                         value,
                                         Template7,
                                         CultureInfo.InvariantCulture.DateTimeFormat,
                                         DateTimeStyles.None,
                                         out serverTime))
                                    {
                                        wmDateTime = default(WmDateTime);
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            wmDateTime = new WmDateTime(ServerTime2UtcTime(serverTime));
            return true;
        }

        public override string ToString()
        {
            return _utcTime.ToLocalTime().ToString(LocalTemplate, CultureInfo.InvariantCulture.DateTimeFormat);
        }

        internal string ToServerString()
        {
            DateTime serverTime = UtcTime2ServerTime(_utcTime);
            return serverTime.ToString(Template1, CultureInfo.InvariantCulture.DateTimeFormat);
        }

        internal static DateTime ServerTime2UtcTime(DateTime serverTime)
        {
            return serverTime.AddHours(-PutinTimeZone);

            // DateTime utcTime = serverTime.AddHours(-3);

            // if (utcTime.CompareTo(summerTimeStart) > 0 && utcTime.CompareTo(winterTimeStart) < 0)
            // utcTime = utcTime.AddHours(-1); // летнее время

            // return utcTime;
        }

        internal static DateTime UtcTime2ServerTime(DateTime utcTime)
        {
            return utcTime.AddHours(PutinTimeZone);

            // DateTime serverTime = utcTime.AddHours(+3);

            // if (utcTime.CompareTo(summerTimeStart) > 0 && utcTime.CompareTo(winterTimeStart) < 0)
            // serverTime = serverTime.AddHours(+1); // летнее время

            // return serverTime;
        }

        public static bool operator ==(WmDateTime lhs, WmDateTime rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(WmDateTime lhs, WmDateTime rhs)
        {
            return !lhs.Equals(rhs);
        }

        public bool Equals(WmDateTime other)
        {
            return _utcTime.Equals(other._utcTime);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is WmDateTime && Equals((WmDateTime) obj);
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