using System;

namespace WebMoney.XmlInterfaces.BasicObjects
{
    [Serializable]
    public enum InvoiceState
    {
        NotPaid = 0,
        PaidProtection,
        Paid,
        Refusal
    }
}