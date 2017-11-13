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
    public class ExpressTrustReport : WmResponse
    {
        public uint TrustId { get; protected set; }

        public Purse ClientPurse { get; protected set; }

        public WmId ClientWmId { get; protected set; }

        public WmId StoreWmId { get; protected set; }

        public string Info { get; protected set; }

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
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            // <?xml version="1.0"?><merchant.response><trust id="65376205"><slavepurse>U731654115046</slavepurse><slavewmid>729376294758</slavewmid><masterwmid>301095414760</masterwmid></trust><retval>0</retval><retdesc></retdesc><userdesc>Trust successfull! The required trust set from member`s purse U731654115046 to merchant wmid 301095414760</userdesc></merchant.response>
            TrustId = wmXmlPackage.SelectUInt32("trust/@id");
            ClientPurse = wmXmlPackage.SelectPurse("trust/slavepurse");
            ClientWmId = wmXmlPackage.SelectWmId("trust/slavewmid");
            StoreWmId = wmXmlPackage.SelectWmId("trust/masterwmid");
            Info = wmXmlPackage.SelectString("userdesc");
        }
    }
}
