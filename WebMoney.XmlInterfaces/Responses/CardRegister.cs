using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
    public class CardRegister : WmResponse
    {
        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            // Интерфейс устарел:
            // <?xml version="1.0" encoding="windows-1251"?><cards.webmoney.response><cards><card typedesc="Obnovite versiu!" canbalance="0" canpay="0" cardnumber="0000000000000000" cardbalance="0.0" pendingbalance="0.0" DateCrt="2017.10.01 22:03" idopnotpay="" idopnotproc="">1</card></cards><retval>0</retval><retdesc></retdesc></cards.webmoney.response>
        }
    }
}