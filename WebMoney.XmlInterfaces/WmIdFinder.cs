using System;
using System.Globalization;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Responses;

namespace WebMoney.XmlInterfaces
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    public class WmIdFinder : WmRequest<WmIdReport>
    {
        protected override string ClassicUrl => "https://w3s.webmoney.ru/asp/XMLFindWMPurseNew.asp";

        protected override string LightUrl => "https://w3s.wmtransfer.com/asp/XMLFindWMPurseCertNew.asp";

        public WmId? WmId { get; private set; }
        public Purse? Purse { get; private set; }

        protected internal WmIdFinder()
        {
        }

        public WmIdFinder(WmId wmId)
        {
            WmId = wmId;
        }

        public WmIdFinder(Purse purse)
        {
            Purse = purse;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", WmId, Purse);
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartElement("testwmpurse"); // <testwmpurse>

            if (WmId.HasValue)
                xmlRequestBuilder.WriteElement("wmid", WmId.ToString());

            if (Purse.HasValue)
                xmlRequestBuilder.WriteElement("purse", Purse.ToString());

            xmlRequestBuilder.WriteEndElement(); // </testwmpurse>
        }
    }
}