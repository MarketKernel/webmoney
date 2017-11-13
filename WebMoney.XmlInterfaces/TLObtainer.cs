using System;
using System.Xml;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Responses;

namespace WebMoney.XmlInterfaces
{
    public class TLObtainer : WmRequest<TLReport>
    {
        protected override string ClassicUrl => "https://debt.wmtransfer.com/xmlTrustLevelsGet.aspx";
        protected override string LightUrl => "https://debt.wmtransfer.com/xmlTrustLevelsGet.aspx";

        public WmId WmId { get; set; }

        public TLObtainer(WmId wmId)
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

            xmlRequestBuilder.WriteStartElement("trustlimits"); // <trustlimits>
            xmlRequestBuilder.WriteStartElement("getlevels"); // <getlevels>

            xmlRequestBuilder.WriteElement("wmid", WmId.ToString());

            xmlRequestBuilder.WriteEndElement(); // </getlevels>
            xmlRequestBuilder.WriteEndElement(); // </trustlimits>
        }
    }
}
