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
    public class ProtectionReport : WmResponse
    {
        public uint Id { get; protected set; }
        public uint Ts { get; protected set; }
        public TransferType TransferType { get; protected set; }
        public WmDateTime UpdateTime { get; protected set; }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            Id = wmXmlPackage.SelectUInt32("operation/@id");
            Ts = wmXmlPackage.SelectUInt32("operation/@ts");
            TransferType = (TransferType) wmXmlPackage.SelectInt32("operation/opertype");
            UpdateTime = wmXmlPackage.SelectWmDateTime("operation/dateupd");
        }
    }
}