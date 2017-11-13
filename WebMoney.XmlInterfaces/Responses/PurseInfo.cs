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
    public class PurseInfo
    {
        public uint Id { get; protected set; }
        public Purse Purse { get; protected set; }
        public Amount Amount { get; protected set; }
        public Description Description { get; protected set; }
        public bool Enable { get; protected set; }
        public uint LastIncomingTransfer { get; protected set; }
        public uint LastOutgoingTransfer { get; protected set; }

        internal void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            Id = wmXmlPackage.SelectUInt32("@id");
            Purse = wmXmlPackage.SelectPurse("pursename");
            Amount = wmXmlPackage.SelectAmount("amount");
            Description = (Description) wmXmlPackage.SelectString("desc");
            Enable = wmXmlPackage.SelectBool("outsideopen");
            LastIncomingTransfer = wmXmlPackage.SelectUInt32("lastintr");
            LastOutgoingTransfer = wmXmlPackage.SelectUInt32("lastouttr");
        }
    }
}