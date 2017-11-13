using System;

namespace WebMoney.XmlInterfaces.BasicObjects
{
    [Serializable]
    public enum ExchangeType
    {
        None = 0,
        Cash = 1,
        OfflineSystem = 2,
        BankAccount = 3,
        BankCard = 4,
        InternetSystem = 5,
        Sms = 6,
        Mobile = 7,
        CryptoCurrency = 8
    }
}