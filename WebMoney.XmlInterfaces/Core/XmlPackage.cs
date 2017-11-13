using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using WebMoney.XmlInterfaces.Core.Exceptions;

namespace WebMoney.XmlInterfaces.Core
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    public class XmlPackage
    {
        private readonly XmlNamespaceManager _namespaceManager;
        private readonly XmlNode _xmlNode;

        public string OuterXml => _xmlNode.OwnerDocument.OuterXml;

        public string NodeOuterXml => _xmlNode.OuterXml;

        public XmlPackage(XmlPackage prototype)
        {
            if (null == prototype)
                throw new ArgumentNullException(nameof(prototype));

            _xmlNode = prototype._xmlNode;
            _namespaceManager = prototype._namespaceManager;
        }

        public XmlPackage(XmlDocument xmlDocument)
        {
            if (null == xmlDocument)
                throw new ArgumentNullException(nameof(xmlDocument));

            XmlElement documentElement = xmlDocument.DocumentElement;

            if (null == documentElement)
                throw new InvalidOperationException("Root element is missing.");

            _xmlNode = documentElement;

            XmlNameTable nameTable = xmlDocument.NameTable;

            if (null == nameTable)
                throw new InvalidOperationException("null == nameTable");

            _namespaceManager = new XmlNamespaceManager(nameTable);
        }

        public XmlPackage(XmlNode xmlNode, XmlNamespaceManager namespaceManager = null)
        {
            if (null == xmlNode)
                throw new ArgumentNullException(nameof(xmlNode));

            _xmlNode = xmlNode;

            if (null != namespaceManager)
                _namespaceManager = namespaceManager;
            else
            {
                XmlDocument xmlDocument = xmlNode.OwnerDocument;

                if (null == xmlDocument)
                    throw new InvalidOperationException("null == xmlNode.OwnerDocument");

                XmlNameTable nameTable = xmlDocument.NameTable;

                if (null == nameTable)
                    throw new InvalidOperationException("null == nameTable");

                _namespaceManager = new XmlNamespaceManager(nameTable);
            }
        }

        public static XmlPackage CreateXmlPackage(XmlReader xmlReader)
        {
            if (null == xmlReader)
                throw new ArgumentNullException(nameof(xmlReader));

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlReader);

            return new XmlPackage(xmlDocument);
        }

        public static XmlPackage CreateXmlPackage(string xmlString)
        {
            if (null == xmlString)
                throw new ArgumentNullException(nameof(xmlString));

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);

            return new XmlPackage(xmlDocument);
        }

        public void AddNamespace(string prefix, string uri)
        {
            if (null == prefix)
                throw new ArgumentNullException(nameof(prefix));

            if (null == uri)
                throw new ArgumentNullException(nameof(uri));

            _namespaceManager.AddNamespace(prefix, uri);
        }

        // READING

        public XmlPackage Select(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            XmlNode xmlNode = _xmlNode.SelectSingleNode(xPath, _namespaceManager);

            if (null == xmlNode)
                throw new MissingParameterException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The element '{0}' does not exists.",
                        xPath));

            return new XmlPackage(xmlNode, _namespaceManager);
        }

        public IList<XmlPackage> SelectList(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            var xmlPackageList = new List<XmlPackage>();

            XmlNodeList xmlNodeList = _xmlNode.SelectNodes(xPath, _namespaceManager);

            if (xmlNodeList != null)
                foreach (XmlElement xmlElement in xmlNodeList)
                {
                    xmlPackageList.Add(new XmlPackage(xmlElement, _namespaceManager));
                }

            return xmlPackageList;
        }

        public bool Exists(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            return null != _xmlNode.SelectSingleNode(xPath, _namespaceManager);
        }

        public string SelectString(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            XmlNode xmlNode = _xmlNode.SelectSingleNode(xPath, _namespaceManager);

            if (null == xmlNode)
                throw new MissingParameterException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The element '{0}' does not exists.",
                        xPath));

            return xmlNode.InnerText;
        }

        public string SelectNotEmptyString(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            string text = SelectString(xPath);

            if (string.IsNullOrEmpty(text))
                throw new IncorrectFormatException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The element '{0}' is empty. Expected a non-empty string.",
                        xPath));

            return text;
        }

        public bool SelectBool(string xPath, bool digit = true)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            if (digit)
            {
                byte @byte = SelectUInt8(xPath);

                if (@byte > 1)
                    throw new IncorrectFormatException(string.Format(CultureInfo.InvariantCulture,
                                                                     "The value '{0}' of the element '{1}' is not in a correct format. Expected a 'Bool' expression.",
                                                                     @byte, xPath));

                return @byte == 1;
            }

            string text = SelectNotEmptyString(xPath);
            bool @bool;

            if (!bool.TryParse(text.ToUpper(CultureInfo.InvariantCulture), out @bool))
                throw new IncorrectFormatException(
                    string.Format(CultureInfo.InvariantCulture,
                                  "The value '{0}' of the element '{1}' is not in a correct format. Expected a 'Bool' expression.",
                                  text, xPath));

            return @bool;
        }

        public sbyte SelectInt8(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            string text = SelectNotEmptyString(xPath);
            sbyte @sbyte;

            if (!sbyte.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out @sbyte))
                throw new IncorrectFormatException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The value '{0}' of the element '{1}' is not in a correct format. Expected a 'Int8' expression.",
                        text,
                        xPath));

            return @sbyte;
        }

        public byte SelectUInt8(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            string text = SelectNotEmptyString(xPath);
            byte @byte;

            if (!byte.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out @byte))
                throw new IncorrectFormatException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The value '{0}' of the element '{1}' is not in a correct format. Expected a 'UInt8' expression.",
                        text,
                        xPath));

            return @byte;
        }

        public short SelectInt16(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            string text = SelectNotEmptyString(xPath);
            short @short;

            if (!short.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out @short))
                throw new IncorrectFormatException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The value '{0}' of the element '{1}' is not in a correct format. Expected a 'Int16' expression.",
                        text,
                        xPath));

            return @short;
        }

        public ushort SelectUInt16(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            string text = SelectNotEmptyString(xPath);
            ushort @ushort;

            if (!ushort.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out @ushort))
                throw new IncorrectFormatException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The value '{0}' of the element '{1}' is not in a correct format. Expected a 'UInt16' expression.",
                        text,
                        xPath));

            return @ushort;
        }

        public int SelectInt32(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            string text = SelectNotEmptyString(xPath);
            int @int;

            if (!int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out @int))
                throw new IncorrectFormatException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The value '{0}' of the element '{1}' is not in a correct format. Expected a 'Int32' expression.",
                        text,
                        xPath));

            return @int;
        }

        public uint SelectUInt32(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            string text = SelectNotEmptyString(xPath);
            uint @uint;

            if (!uint.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out @uint))
                throw new IncorrectFormatException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The value '{0}' of the element '{1}' is not in a correct format. Expected a 'UInt32' expression.",
                        text,
                        xPath));

            return @uint;
        }

        public long SelectInt64(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            string text = SelectNotEmptyString(xPath);
            long @long;

            if (!long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out @long))
                throw new IncorrectFormatException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The value '{0}' of the element '{1}' is not in a correct format. Expected a 'Int64' expression.",
                        text,
                        xPath));

            return @long;
        }

        public ulong SelectUInt64(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            string text = SelectNotEmptyString(xPath);
            ulong @ulong;

            if (!ulong.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out @ulong))
                throw new IncorrectFormatException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The value '{0}' of the element '{1}' is not in a correct format. Expected a 'UInt64' expression.",
                        text,
                        xPath));

            return @ulong;
        }
        
        public object SelectEnum(Type type, string xPath)
        {
            string text = SelectNotEmptyString(xPath);

            try
            {
                return Enum.Parse(type, text, true);
            }
            catch (ArgumentException)
            {
                throw new IncorrectFormatException(string.Format(CultureInfo.InvariantCulture,
                                                                 "The value '{0}' of the element '{1}' is not in a correct format. Expected a '" +
                                                                 type.Name + "' expression.",
                                                                 text, xPath));
            }
        }

        // WRITING

        public void Import(XmlPackage xmlPackage)
        {
            if (null == xmlPackage)
                throw new ArgumentNullException(nameof(xmlPackage));

            if (_xmlNode.OwnerDocument != null)
            {
                XmlNode newChild = _xmlNode.OwnerDocument.ImportNode(xmlPackage._xmlNode, true);
                _xmlNode.InsertAfter(newChild, _xmlNode.LastChild);
            }
            else
                throw new InvalidOperationException("_xmlNode.OwnerDocument != null");
        }

        public void SetInnerText(string xPath, string value)
        {
            if (null == xPath)
                throw new ArgumentNullException(nameof(xPath));

            if (null == value)
                throw new ArgumentNullException(nameof(value));

            Select(xPath)._xmlNode.InnerText = value;
        }

        public void SetInnerText(string xPath, bool value, bool digit = true)
        {
            if (null == xPath)
                throw new ArgumentNullException(nameof(xPath));

            if (digit)
                SetInnerText(xPath, value ? "1" : "0");
            else
                SetInnerText(xPath, value.ToString(CultureInfo.InvariantCulture));
        }

        public void SetInnerText(string xPath, sbyte value)
        {
            if (null == xPath)
                throw new ArgumentNullException(nameof(xPath));

            SetInnerText(xPath, value.ToString(CultureInfo.InvariantCulture));
        }

        public void SetInnerText(string xPath, byte value)
        {
            if (null == xPath)
                throw new ArgumentNullException(nameof(xPath));

            SetInnerText(xPath, value.ToString(CultureInfo.InvariantCulture));
        }

        public void SetInnerText(string xPath, short value)
        {
            if (null == xPath)
                throw new ArgumentNullException(nameof(xPath));

            SetInnerText(xPath, value.ToString(CultureInfo.InvariantCulture));
        }

        public void SetInnerText(string xPath, ushort value)
        {
            if (null == xPath)
                throw new ArgumentNullException(nameof(xPath));

            SetInnerText(xPath, value.ToString(CultureInfo.InvariantCulture));
        }

        public void SetInnerText(string xPath, int value)
        {
            if (null == xPath)
                throw new ArgumentNullException(nameof(xPath));

            SetInnerText(xPath, value.ToString(CultureInfo.InvariantCulture));
        }

        public void SetInnerText(string xPath, uint value)
        {
            if (null == xPath)
                throw new ArgumentNullException(nameof(xPath));

            SetInnerText(xPath, value.ToString(CultureInfo.InvariantCulture));
        }

        public void SetInnerText(string xPath, long value)
        {
            if (null == xPath)
                throw new ArgumentNullException(nameof(xPath));

            SetInnerText(xPath, value.ToString(CultureInfo.InvariantCulture));
        }

        public void SetInnerText(string xPath, ulong value)
        {
            if (null == xPath)
                throw new ArgumentNullException(nameof(xPath));

            SetInnerText(xPath, value.ToString(CultureInfo.InvariantCulture));
        }
        
        public void WriteContentTo(XmlWriter xmlWriter)
        {
            if (null == xmlWriter)
                throw new ArgumentNullException(nameof(xmlWriter));

            _xmlNode.OwnerDocument.WriteContentTo(xmlWriter);
        }
    }
}