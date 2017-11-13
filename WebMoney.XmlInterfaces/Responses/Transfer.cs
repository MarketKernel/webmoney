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
    public class Transfer : Operation
    {
        public Purse SourcePurse { get; protected set; }
        public Amount Commission { get; protected set; }
        public TransferType TransferType { get; protected set; }
        public uint InvoiceId { get; protected set; }
        public uint OrderId { get; protected set; }
        public uint TransferId { get; protected set; }
        public byte Period { get; protected set; }
        public WmId Partner { get; protected set; }
        public Amount Rest { get; protected set; }
        public bool IsLocked { get; protected set; }

        internal override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            Id = wmXmlPackage.SelectUInt32("@id");
            Ts = wmXmlPackage.SelectUInt32("@ts");
            SourcePurse = wmXmlPackage.SelectPurse("pursesrc");
            TargetPurse = wmXmlPackage.SelectPurse("pursedest");
            Amount = wmXmlPackage.SelectAmount("amount");
            Commission = wmXmlPackage.SelectAmount("comiss");
            TransferType = (TransferType)wmXmlPackage.SelectInt32("opertype");
            InvoiceId = wmXmlPackage.SelectUInt32("wminvid");
            OrderId = wmXmlPackage.SelectUInt32("orderid");
            TransferId = wmXmlPackage.SelectUInt32("tranid");
            Period = wmXmlPackage.SelectUInt8("period");
            Description = (Description)wmXmlPackage.SelectString("desc");
            CreateTime = wmXmlPackage.SelectWmDateTime("datecrt");
            UpdateTime = wmXmlPackage.SelectWmDateTime("dateupd");
            Partner = wmXmlPackage.SelectWmId("corrwm");
            Rest = wmXmlPackage.SelectAmount("rest");

            if (wmXmlPackage.Exists("timelock"))
                IsLocked = true;
        }
    }
}