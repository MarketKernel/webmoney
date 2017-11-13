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
    public struct Message : IXmlSerializable
    {
        private const int MaxLength = 1024;

        private string _value;

        public Message(string value, bool check)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            _value = Description.Clean(value, MaxLength, check);
        }

        public static explicit operator Message(string value)
        {
            return new Message(value, true);
        }

        public static implicit operator string(Message value)
        {
            return value._value;
        }

        public override string ToString()
        {
            return _value;
        }

        public static bool operator ==(Message lhs, Message rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Message lhs, Message rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(Message other)
        {
            if (null == _value)
                return null == other._value;

            return _value.Equals(other._value, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Message && Equals((Message) obj);
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

            _value = Description.Clean(reader.ReadElementContentAsString(), MaxLength, true);
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