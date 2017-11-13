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
    public class MoneybackReport : WmResponse
    {
        public uint Id { get; protected set; }
        public ulong Ts { get; protected set; }
        public ulong OperationId { get; protected set; }
        public Purse SourcePurse { get; protected set; }
        public Purse TargetPurse { get; protected set; }
        public Amount Amount { get; protected set; }
        public Amount Commission { get; protected set; }
        public Description Description { get; protected set; }
        public WmDateTime CreateTime { get; protected set; }
        public WmDateTime UpdateTime { get; protected set; }

        protected override void Inspect(XmlPackage xmlPackage)
        {
            if (null == xmlPackage)
                throw new ArgumentNullException(nameof(xmlPackage));

            int errorNumber = xmlPackage.SelectInt32("retval");

            if (0 != errorNumber)
                throw new TransferRejectorException(errorNumber, xmlPackage.SelectString("retdesc"));
        }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            Id = wmXmlPackage.SelectUInt32("operation/@id");
            Ts = wmXmlPackage.SelectUInt32("operation/@ts");
            OperationId = wmXmlPackage.SelectUInt32("operation/inwmtranid");
            SourcePurse = wmXmlPackage.SelectPurse("operation/pursesrc");
            TargetPurse = wmXmlPackage.SelectPurse("operation/pursedest");
            Amount = wmXmlPackage.SelectAmount("operation/amount");
            Commission = wmXmlPackage.SelectAmount("operation/comiss");
            Description = (Description)wmXmlPackage.SelectString("operation/desc");
            CreateTime = wmXmlPackage.SelectWmDateTime("operation/datecrt");
            UpdateTime = wmXmlPackage.SelectWmDateTime("operation/dateupd");
        }
    }
}