using System;

namespace WebMoney.XmlInterfaces.BasicObjects
{
    [Serializable]
    public enum PassportDegree
    {
        Alias = 100,
        Formal = 110,
        Initial = 120,
        Personal = 130,
        Merchant = 135,
        Capitaller = 136,
        Cashier = 138,
        Developer = 140,
        Registrar = 150,
        Guarantor = 170,
        Service1 = 190,
        Service2 = 200,
        Operator = 300,
    }
}