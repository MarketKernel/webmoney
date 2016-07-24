using System;

namespace WebMoney.XmlInterfaces.BasicObjects
{
    [Flags]
    public enum PartnerAvailability
    {
        InvoiceForbidden = 0x02,
        MessageForbidden = 0x04,
        TransferForbidden = 0x08,
    }
}