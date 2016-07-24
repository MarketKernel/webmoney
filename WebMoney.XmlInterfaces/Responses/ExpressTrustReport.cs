using System;
using System.Xml.Serialization;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Utility;

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

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage) throw new ArgumentNullException(nameof(wmXmlPackage));

            TrustId = wmXmlPackage.SelectUInt32("trust/@id");
            ClientPurse = wmXmlPackage.SelectPurse("trust/slavepurse");
            ClientWmId = wmXmlPackage.SelectWmId("trust/slavewmid");
            StoreWmId = wmXmlPackage.SelectWmId("trust/masterwmid");
            Info = wmXmlPackage.SelectString("userdesc");
        }
    }
}
