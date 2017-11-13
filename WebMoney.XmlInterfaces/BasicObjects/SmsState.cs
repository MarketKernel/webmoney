namespace WebMoney.XmlInterfaces.BasicObjects
{
    public enum SmsState
    {
        BUFFERED,
        SENDING,
        SENDED,
        DELIVERED,
        NON_DELIVERED,
        SUSPENDED,
        HLRPENDING,
        HLRMISMATCH
    }
}