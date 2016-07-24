using System;

namespace WebMoney.XmlInterfaces.BasicObjects
{
    [Serializable]
    public enum PassportAppointment
    {
        None = 0,
        PrivatePerson = 1,
        Director = 20,
        Accountant = 21,
        Representative = 22,
        PrivateEntrepreneur = 23,
    }
}