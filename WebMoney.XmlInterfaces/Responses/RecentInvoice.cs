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
    public class RecentInvoice : Invoice
    {
        public WmId SourceWmId { get; protected set; }
        public InvoiceState State { get; protected set; }

        internal override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            Id = wmXmlPackage.SelectUInt32("invoice/@id");
            Ts = wmXmlPackage.SelectUInt32("invoice/@ts");
            OrderId = wmXmlPackage.SelectUInt32("invoice/orderid");
            SourceWmId = wmXmlPackage.SelectWmId("invoice/customerwmid");
            TargetPurse = wmXmlPackage.SelectPurse("invoice/storepurse");
            Amount = wmXmlPackage.SelectAmount("invoice/amount");
            Description = (Description)wmXmlPackage.SelectString("invoice/desc");
            Address = (Description)wmXmlPackage.SelectString("invoice/address");
            Period = wmXmlPackage.SelectUInt8("invoice/period");
            Expiration = wmXmlPackage.SelectUInt8("invoice/expiration");
            State = (InvoiceState)wmXmlPackage.SelectInt32("invoice/state");
            CreateTime = wmXmlPackage.SelectWmDateTime("invoice/datecrt");
            UpdateTime = wmXmlPackage.SelectWmDateTime("invoice/dateupd");
        }
    }
}