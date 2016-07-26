using System;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    public class RecentTransfer : Operation
    {
        public uint TransferId { get; protected set; }
        public Purse SourcePurse { get; protected set; }
        public Amount Commission { get; protected set; }
        public TransferType TransferType { get; protected set; }
        public byte Period { get; protected set; }
        public uint InvoiceId { get; protected set; }

        internal override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            Id = wmXmlPackage.SelectUInt32("operation/@id");
            Ts = wmXmlPackage.SelectUInt32("operation/@ts");
            TransferId = wmXmlPackage.SelectUInt32("operation/tranid");
            SourcePurse = wmXmlPackage.SelectPurse("operation/pursesrc");
            TargetPurse = wmXmlPackage.SelectPurse("operation/pursedest");
            Amount = wmXmlPackage.SelectAmount("operation/amount");
            Commission = wmXmlPackage.SelectAmount("operation/comiss");
            TransferType = (TransferType)wmXmlPackage.SelectInt32("operation/opertype");
            Period = wmXmlPackage.SelectUInt8("operation/period");
            InvoiceId = wmXmlPackage.SelectUInt32("operation/wminvid");
            Description = (Description)wmXmlPackage.SelectString("operation/desc");
            CreateTime = wmXmlPackage.SelectWmDateTime("operation/datecrt");
            UpdateTime = wmXmlPackage.SelectWmDateTime("operation/dateupd");
        }
    }
}