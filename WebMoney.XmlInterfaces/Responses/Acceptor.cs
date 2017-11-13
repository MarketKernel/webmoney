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
    public class Acceptor
    {
        public uint ContractId { get; protected set; }
        public WmId WmId { get; protected set; }
        public WmDateTime? AcceptTime { get; protected set; }

        internal void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            ContractId = wmXmlPackage.SelectUInt32("@contractid");
            WmId = wmXmlPackage.SelectWmId("@wmid");

            if (wmXmlPackage.Exists("@acceptdate") && !string.IsNullOrEmpty(wmXmlPackage.SelectString("@acceptdate")))
                AcceptTime = wmXmlPackage.SelectWmDateTime("@acceptdate");
        }
    }
}