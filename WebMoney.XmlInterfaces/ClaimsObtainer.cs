using System;
using System.Xml;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Responses;

namespace WebMoney.XmlInterfaces
{
    public class ClaimsObtainer : WmRequest<ClaimsReport>
    {
        protected override string ClassicUrl => "https://passport.webmoney.ru/xml/XMLGetWMIDInfo.aspx";

        protected override string LightUrl => "https://passport.webmoney.ru/xml/XMLGetWMIDInfo.aspx";

        public WmId WmId { get; set; }

        public ClaimsObtainer(WmId wmId)
        {
            WmId = wmId;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            throw new NotSupportedException();
        }

        protected override void WriteContent(XmlWriter xmlWriter)
        {
            if (null == xmlWriter)
                throw new ArgumentNullException(nameof(xmlWriter));

            var xmlRequestBuilder = new XmlRequestBuilder(xmlWriter);

            xmlRequestBuilder.WriteStartElement("request"); // <request>

            xmlRequestBuilder.WriteElement("wmid", WmId.ToString());

            xmlRequestBuilder.WriteEndElement(); // </request>
        }
    }
}