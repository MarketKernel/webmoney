using System;
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
    public struct Description : IXmlSerializable
    {
        public const int MaxLength = 255;

        private string _value;

        public Description(string value, bool check)
        {
            _value = Clean(value, MaxLength, check);
        }

        public static explicit operator Description(string value)
        {
            return new Description(value, true);
        }

        public static implicit operator string(Description value)
        {
            return value._value;
        }

        internal static string Clean(string value, int maxLength, bool check)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            string result = value.Replace("\r", string.Empty);
            result = result.TrimStart(' ', '\n', '\t').TrimEnd(' ', '\n', '\t');

            if (value.Length > maxLength)
            {
                if (check)
                    throw new ArgumentOutOfRangeException(nameof(value));

                result = result.Substring(0, value.Length < maxLength ? value.Length : maxLength);
                result = result.TrimStart(' ', '\n', '\t').TrimEnd(' ', '\n', '\t');
            }

            return result;
        }

        public override string ToString()
        {
            return _value;
        }

        public static bool operator ==(Description lhs, Description rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Description lhs, Description rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(Description other)
        {
            if (null == _value)
                return null == other._value;

            return _value.Equals(other._value, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Description && Equals((Description) obj);
        }

        public override int GetHashCode()
        {
            return _value?.GetHashCode() ?? 0;
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

            _value = Clean(reader.ReadElementContentAsString(), MaxLength, true);
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