using System;

namespace WebMoney.XmlInterfaces.BasicObjects
{
    [Serializable]
    public enum TransferType
    {
        Normal = 0,
        Protection = 4,
        ProtectionCancel = 12,
    }
}