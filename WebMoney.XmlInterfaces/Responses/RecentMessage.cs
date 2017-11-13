using System;
using System.Xml.Serialization;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Exceptions;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    [XmlRoot(ElementName = "w3s.response")]
    public class RecentMessage : WmResponse
    {
        public uint Id { get; protected set; }
        public WmId WmId { get; protected set; }
        public Description Subject { get; protected set; }
        public Message Content { get; protected set; }
        public WmDateTime CreateTime { get; protected set; }

        protected override void Inspect(XmlPackage xmlPackage)
        {
            if (null == xmlPackage)
                throw new ArgumentNullException(nameof(xmlPackage));

            int errorNumber = xmlPackage.SelectInt32("retval");

            if (0 != errorNumber)
                throw new OriginalMessageException(errorNumber, xmlPackage.SelectString("retdesc"));
        }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            Id = wmXmlPackage.SelectUInt32("message/@id");
            WmId = wmXmlPackage.SelectWmId("message/receiverwmid");
            Subject = (Description) wmXmlPackage.SelectString("message/msgsubj");
            Content = (Message) wmXmlPackage.SelectNotEmptyString("message/msgtext");
            CreateTime = wmXmlPackage.SelectWmDateTime("message/datecrt");
        }
    }
}