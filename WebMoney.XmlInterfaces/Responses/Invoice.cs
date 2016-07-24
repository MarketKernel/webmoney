using WebMoney.XmlInterfaces.BasicObjects;

namespace WebMoney.XmlInterfaces.Responses
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    public abstract class Invoice : Operation
    {
        public uint OrderId { get; protected set; }
        public Description Address { get; protected set; }
        public byte Period { get; protected set; }
        public byte Expiration { get; protected set; }
        public InvoiceState InvoiceState { get; protected set; }
    }
}