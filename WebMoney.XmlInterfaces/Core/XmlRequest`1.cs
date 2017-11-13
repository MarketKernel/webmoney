using System;
using System.IO;
using System.Xml;

namespace WebMoney.XmlInterfaces.Core
{
    public abstract class XmlRequest<TXmlResponse> : Request<TXmlResponse>
        where TXmlResponse : XmlResponse, new()
    {
        protected internal sealed override void WriteContent(Stream stream)
        {
            if (null == stream)
                throw new ArgumentNullException(nameof(stream));

            var xmlWriter = new XmlTextWriter(stream, RequestEncoding);
            WriteContent(xmlWriter);
            xmlWriter.Flush();
        }

        protected abstract void WriteContent(XmlWriter xmlWriter);
    }
}