using System;
using System.Xml.Serialization;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    [XmlRoot(ElementName = "w3s.response")]
    public class InvoiceReport : WmResponse
    {
        public uint Id { get; protected set; }
        public ulong Ts { get; protected set; }
        public InvoiceState State { get; protected set; }
        public WmDateTime UpdateTime { get; protected set; }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            Id = wmXmlPackage.SelectUInt32("ininvoice/@id");
            Ts = wmXmlPackage.SelectUInt32("ininvoice/@ts");
            State = (InvoiceState)wmXmlPackage.SelectUInt32("ininvoice/state");
            UpdateTime = wmXmlPackage.SelectWmDateTime("ininvoice/dateupd");
        }
    }
}