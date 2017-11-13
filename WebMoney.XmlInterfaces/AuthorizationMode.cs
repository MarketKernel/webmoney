using System;

namespace WebMoney.XmlInterfaces
{
    [
    Serializable
    ]
    public enum AuthorizationMode
    {
        None,
        Merchant,
        Classic,
        Light,
    }
}