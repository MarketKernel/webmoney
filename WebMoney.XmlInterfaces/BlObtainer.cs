using System;
using System.Xml;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Responses;

namespace WebMoney.XmlInterfaces
{
    public class BLObtainer : WmRequest<BLReport>
    {
        protected override string ClassicUrl => "https://stats.wmtransfer.com/levels/XMLWMIDLevel.aspx";
        protected override string LightUrl => "https://stats.wmtransfer.com/levels/XMLWMIDLevel.aspx";

        public WmId WmId { get; set; }

        public BLObtainer(WmId wmId)
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

            xmlRequestBuilder.WriteStartElement("WMIDLevel.request"); // <WMIDLevel.request>

            xmlRequestBuilder.WriteElement("wmid", WmId.ToString());

            xmlRequestBuilder.WriteEndElement(); // </WMIDLevel.request>
        }
    }
}
