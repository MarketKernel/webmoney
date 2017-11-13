using System;
using System.Xml;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Responses;

namespace WebMoney.XmlInterfaces
{
    public class CardSelector : WmRequest<CardRegister>
    {
        protected override string ClassicUrl => "https://cards.webmoney.ru/asp/XMLCardsListByWMID.asp";

        protected override string LightUrl => throw new NotSupportedException();

        protected override string BuildMessage(ulong requestNumber)
        {
            throw new NotImplementedException();
        }

        protected override void WriteContent(XmlWriter xmlWriter)
        {
            if (null == xmlWriter)
                throw new ArgumentNullException(nameof(xmlWriter));

            var xmlRequestBuilder = new XmlRequestBuilder(xmlWriter);

            xmlRequestBuilder.WriteStartElement("cards.webmoney.request"); // <cards.webmoney.request>

            var wmId = Initializer.Id;

            xmlRequestBuilder.WriteElement("wmid", wmId.ToString());
            xmlRequestBuilder.WriteElement("signstr", Initializer.Sign($"{wmId}{wmId}"));
            xmlRequestBuilder.WriteElement("clientwmid", wmId.ToString());

            xmlRequestBuilder.WriteEndElement(); // </cards.webmoney.request>
        }
    }
}
