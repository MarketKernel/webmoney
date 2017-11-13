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
    public class IncomingInvoice : Invoice
    {
        public WmId TargetWmId { get; protected set; }
        public uint OperationId { get; protected set; }

        internal override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            Id = wmXmlPackage.SelectUInt32("@id");
            Ts = wmXmlPackage.SelectUInt32("@ts");
            OrderId = wmXmlPackage.SelectUInt32("orderid");
            TargetWmId = wmXmlPackage.SelectWmId("storewmid");
            TargetPurse = wmXmlPackage.SelectPurse("storepurse");
            Amount = wmXmlPackage.SelectAmount("amount");
            Description = (Description)wmXmlPackage.SelectString("desc");
            Address = (Description)wmXmlPackage.SelectString("address");
            Period = wmXmlPackage.SelectUInt8("period");
            Expiration = wmXmlPackage.SelectUInt8("expiration");
            InvoiceState = (InvoiceState)wmXmlPackage.SelectInt32("state");
            CreateTime = wmXmlPackage.SelectWmDateTime("datecrt");
            UpdateTime = wmXmlPackage.SelectWmDateTime("dateupd");
            OperationId = wmXmlPackage.SelectUInt32("wmtranid");
        }
    }
}