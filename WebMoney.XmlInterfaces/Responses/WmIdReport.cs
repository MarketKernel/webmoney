using System;
using System.Xml.Serialization;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    [XmlRoot(ElementName = "w3s.response")]
    public class WmIdReport : WmResponse
    {
        public bool Success { get; protected set; }

        public WmId? WmId { get; protected set; }
        public bool? TransferRejected { get; protected set; }
        public PartnerAvailability? Availability { get; protected set; }
        public PassportDegree? Passport { get; protected set; }

        public Purse? Purse { get; protected set; }
        public bool? MerchantEnabled { get; protected set; }
        public bool? CashierEnabled { get; protected set; }

        protected override void Inspect(XmlPackage xmlPackage)
        {
            if (null == xmlPackage)
                throw new ArgumentNullException(nameof(xmlPackage));
        }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            Success = wmXmlPackage.SelectBool("retval");

            if (!Success)
                return;

            if (!string.IsNullOrEmpty(wmXmlPackage.SelectString("testwmpurse/wmid")))
                WmId = wmXmlPackage.SelectWmId("testwmpurse/wmid");

            if (wmXmlPackage.SelectInt32("testwmpurse/wmid/@available") >= 0)
                TransferRejected = wmXmlPackage.SelectBool("testwmpurse/wmid/@available");

            if (wmXmlPackage.SelectInt32("testwmpurse/wmid/@themselfcorrstate") > 0)
                Availability = (PartnerAvailability) wmXmlPackage.SelectInt32("testwmpurse/wmid/@themselfcorrstate");

            if (wmXmlPackage.SelectInt32("testwmpurse/wmid/@newattst") >= 0)
                Passport = (PassportDegree) wmXmlPackage.SelectInt32("testwmpurse/wmid/@newattst");

            if (!string.IsNullOrEmpty(wmXmlPackage.SelectString("testwmpurse/purse")))
                Purse = wmXmlPackage.SelectPurse("testwmpurse/purse");

            if (wmXmlPackage.SelectInt32("testwmpurse/purse/@merchant_active_mode") >= 0)
                MerchantEnabled = wmXmlPackage.SelectBool("testwmpurse/purse/@merchant_active_mode");

            if (wmXmlPackage.SelectInt32("testwmpurse/purse/@merchant_allow_cashier") >= 0)
                CashierEnabled = wmXmlPackage.SelectBool("testwmpurse/purse/@merchant_allow_cashier");
        }
    }
}