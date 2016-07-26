using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    public abstract class Operation
    {
        public uint Id { get; protected set; }
        public uint Ts { get; protected set; }
        public Purse TargetPurse { get; protected set; }
        public Amount Amount { get; protected set; }
        public Description Description { get; protected set; }
        public WmDateTime CreateTime { get; protected set; }
        public WmDateTime UpdateTime { get; protected set; }

        internal abstract void Fill(WmXmlPackage wmXmlPackage);
    }
}