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
    public class ExpressTrustResponse : WmResponse
    {
        public uint Reference { get; protected set; }

        public ConfirmationType ConfirmationType { get; protected set; }

        public string Info { get; protected set; }

        public string SmsReference { get; protected set; }

        protected override void Inspect(XmlPackage xmlPackage)
        {
            if (null == xmlPackage)
                throw new ArgumentNullException(nameof(xmlPackage));

            int errorNumber = xmlPackage.SelectInt32("retval");

            if (0 != errorNumber)
                throw new ExpressTrustException(errorNumber, xmlPackage.SelectString("retdesc"));
        }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage) throw new ArgumentNullException(nameof(wmXmlPackage));

            Reference = wmXmlPackage.SelectUInt32("trust/@purseid");
            ConfirmationType = (ConfirmationType)wmXmlPackage.SelectInt32("trust/realsmstype");
            Info = wmXmlPackage.SelectString("userdesc");
            SmsReference = wmXmlPackage.SelectString("smssecureid");
        }
    }
}
