using System;
using System.Xml.Serialization;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    [XmlRoot(ElementName = "w3s.response")]
    public class RecentTrust : WmResponse
    {
        public uint Id { get; protected set; }
        public bool InvoiceAllowed { get; protected set; }
        public bool TransferAllowed { get; protected set; }
        public bool BalanceAllowed { get; protected set; }
        public bool HistoryAllowed { get; protected set; }
        public WmId Master { get; protected set; }
        public Purse? Purse { get; protected set; }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            Id = wmXmlPackage.SelectUInt32("trust/@id");
            InvoiceAllowed = wmXmlPackage.SelectBool("trust/@inv");
            TransferAllowed = wmXmlPackage.SelectBool("trust/@trans");
            BalanceAllowed = wmXmlPackage.SelectBool("trust/@purse");
            HistoryAllowed = wmXmlPackage.SelectBool("trust/@transhist");
            Master = wmXmlPackage.SelectWmId("trust/master");

            // TODO: [L] Упростить проверку!
            if (wmXmlPackage.Exists("trust/purse") &&
                !string.IsNullOrEmpty(wmXmlPackage.SelectString("trust/purse")))
                Purse = wmXmlPackage.SelectPurse("trust/purse");
        }
    }
}
