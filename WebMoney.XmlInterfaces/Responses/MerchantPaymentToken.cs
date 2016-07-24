using System;
using System.Xml.Serialization;
using WebMoney.XmlInterfaces.Utility;

namespace WebMoney.XmlInterfaces.Responses
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    [XmlRoot(ElementName = "merchant.response")]
    public class MerchantPaymentToken : WmResponse
    {
        public string Token { get; protected set; }

        public uint Lifetime { get; protected set; }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage) throw new ArgumentNullException(nameof(wmXmlPackage));

            Token = wmXmlPackage.SelectNotEmptyString("transtoken");
            Lifetime = wmXmlPackage.SelectUInt32("validityperiodinhours");
        }
    }
}
