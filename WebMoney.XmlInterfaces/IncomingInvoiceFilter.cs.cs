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
    public class IncomingInvoiceFilter : WmRequest<IncomingInvoiceRegister>
    {
        protected override string ClassicUrl => "https://w3s.webmoney.ru/asp/XMLInInvoices.asp";

        protected override string LightUrl => "https://w3s.wmtransfer.com/asp/XMLInInvoicesCert.asp";

        public WmId WmId { get; set; }
        public uint InvoiceId { get; set; }
        public WmDateTime StartTime { get; set; }
        public WmDateTime FinishTime { get; set; }

        protected internal IncomingInvoiceFilter()
        {
        }

        public IncomingInvoiceFilter(WmId wmId, WmDateTime startTime, WmDateTime finishTime)
        {
            WmId = wmId;
            StartTime = startTime;
            FinishTime = finishTime;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}", WmId, InvoiceId,
                                     StartTime.ToServerString(), FinishTime.ToServerString(), requestNumber);
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartElement("getininvoices"); // <getininvoices>

            xmlRequestBuilder.WriteElement("wmid", WmId.ToString());
            xmlRequestBuilder.WriteElement("wminvid", InvoiceId);
            xmlRequestBuilder.WriteElement("datestart", StartTime.ToServerString());
            xmlRequestBuilder.WriteElement("datefinish", FinishTime.ToServerString());

            xmlRequestBuilder.WriteEndElement(); // </getininvoices>
        }
    }
}