using System;

namespace WebMoney.XmlInterfaces.BasicObjects
{
    [Flags]
    [Serializable]
    public enum ConfirmationFlag
    {
        None = 0,
        Protected = 1,
        Verify = 2,
    }
}