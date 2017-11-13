using System;
using System.IO;
using System.Xml;

namespace WebMoney.XmlInterfaces.Core
{
    public abstract class XmlResponse : Response
    {
        protected internal sealed override void ReadContent(Stream stream)
        {
            if (null == stream)
                throw new ArgumentNullException(nameof(stream));

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(new StreamReader(stream, ResponseEncoding));

            var xmlPackage = new XmlPackage(xmlDocument);

            ReadContent(xmlPackage);
        }

        protected abstract void ReadContent(XmlPackage xmlPackage);
    }
}