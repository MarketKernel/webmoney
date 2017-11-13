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
    public class WmIdInfo
    {
        public WmId WmId { get; protected set; }
        public string Alias { get; protected set; }
        public string Description { get; protected set; }
        public WmDateTime RegistrationDate { get; protected set; }

        internal void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            WmId = wmXmlPackage.SelectWmId("@wmid");
            Alias = wmXmlPackage.SelectString("@nickname");
            Description = wmXmlPackage.SelectString("@info");
            RegistrationDate = wmXmlPackage.SelectWmDateTime("@datereg");
        }
    }
}