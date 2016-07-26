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
    [XmlRoot(ElementName = "merchant.response")]
    public class ExpressPaymentReport : WmResponse
    {
        public uint TransferId { get; protected set; }

        public uint InvoiceId { get; protected set; }

        public Amount Amount { get; protected set; }

        public WmDateTime Date { get; protected set; }

        public Description Description { get; protected set; }

        public Purse ClientPurse { get; protected set; }

        public WmId ClientWmId { get; protected set; }

        public string Info { get; protected set; }

        public SmsState SmsState { get; private set; }

        protected override void Inspect(XmlPackage xmlPackage)
        {
            if (null == xmlPackage)
                throw new ArgumentNullException(nameof(xmlPackage));

            int errorNumber = xmlPackage.SelectInt32("retval");

            if (0 != errorNumber)
                throw new ExpressPaymentException(errorNumber, xmlPackage.SelectString("retdesc"));
        }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage) throw new ArgumentNullException(nameof(wmXmlPackage));

            TransferId = wmXmlPackage.SelectUInt32("operation/@wmtransid");
            InvoiceId = wmXmlPackage.SelectUInt32("operation/@wminvoiceid");
            Amount = wmXmlPackage.SelectAmount("operation/amount");
            Date = wmXmlPackage.SelectWmDateTime("operation/operdate");
            Description = (Description)wmXmlPackage.SelectString("operation/purpose");
            ClientPurse = wmXmlPackage.SelectPurse("operation/pursefrom");
            ClientWmId = wmXmlPackage.SelectWmId("operation/wmidfrom");
            Info = wmXmlPackage.SelectString("userdesc");

            if (wmXmlPackage.Exists("smssentstate"))
                SmsState =
                    (SmsState)Enum.Parse(typeof(SmsState), wmXmlPackage.SelectNotEmptyString("smssentstate"), true);
        }
    }
}