using System;
using System.Globalization;
using System.Xml;

namespace WebMoney.XmlInterfaces.Core
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    public class XmlRequestBuilder
    {
        private readonly XmlWriter _xmlWriter;

        public XmlRequestBuilder(XmlWriter xmlWriter)
        {
            _xmlWriter = xmlWriter;
        }

        public void WriteStartDocument()
        {
            _xmlWriter.WriteStartDocument();
        }

        public void WriteStartElement(string elementName)
        {
            if (null == elementName)
                throw new ArgumentNullException(nameof(elementName));

            _xmlWriter.WriteStartElement(elementName);
        }

        public void AppendAttribute(string attributeName, int value)
        {
            if (null == attributeName)
                throw new ArgumentNullException(nameof(attributeName));

            AppendAttribute(attributeName, value.ToString(CultureInfo.InvariantCulture.NumberFormat));
        }

        public void AppendAttribute(string attributeName, uint value)
        {
            if (null == attributeName)
                throw new ArgumentNullException(nameof(attributeName));

            AppendAttribute(attributeName, value.ToString(CultureInfo.InvariantCulture.NumberFormat));
        }

        public void AppendAttribute(string attributeName, string value)
        {
            if (null == attributeName)
                throw new ArgumentNullException(nameof(attributeName));

            if (null == value)
                throw new ArgumentNullException(nameof(value));

            _xmlWriter.WriteAttributeString(attributeName, value);
        }

        public void WriteElement(string elementName, int value)
        {
            if (null == elementName)
                throw new ArgumentNullException(nameof(elementName));

            WriteElement(elementName, value.ToString(CultureInfo.InvariantCulture.NumberFormat));
        }

        public void WriteElement(string elementName, uint value)
        {
            if (null == elementName)
                throw new ArgumentNullException(nameof(elementName));

            WriteElement(elementName, value.ToString(CultureInfo.InvariantCulture.NumberFormat));
        }

        public void WriteElement(string elementName, long value)
        {
            if (null == elementName)
                throw new ArgumentNullException(nameof(elementName));

            WriteElement(elementName, value.ToString(CultureInfo.InvariantCulture.NumberFormat));
        }

        public void WriteElement(string elementName, ulong value)
        {
            if (null == elementName)
                throw new ArgumentNullException(nameof(elementName));

            WriteElement(elementName, value.ToString(CultureInfo.InvariantCulture.NumberFormat));
        }

        public void WriteElement(string elementName, string value)
        {
            if (null == elementName)
                throw new ArgumentNullException(nameof(elementName));

            if (null == value)
                value = string.Empty;

            _xmlWriter.WriteStartElement(elementName);
            _xmlWriter.WriteString(value);
            _xmlWriter.WriteEndElement();
        }

        public void WriteString(string value)
        {
            if (null == value)
                value = string.Empty;

            _xmlWriter.WriteString(value);
        }

        public void WriteEndElement()
        {
            _xmlWriter.WriteEndElement();
        }

        public void WriteEndDocument()
        {
            _xmlWriter.WriteEndDocument();
        }

        public void Flush()
        {
            _xmlWriter.Flush();
        }
    }
}