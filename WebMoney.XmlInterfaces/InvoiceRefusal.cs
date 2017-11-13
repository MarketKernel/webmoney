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
    public class InvoiceRefusal : WmRequest<InvoiceReport>
    {
        protected override string ClassicUrl => "https://w3s.webmoney.ru/asp/XMLInvoiceRefusal.asp";

        protected override string LightUrl => "https://w3s.wmtransfer.com/asp/XMLInvoiceRefusalCert.asp";

        public WmId WmId { get; set; }
        public uint InvoiceId { get; set; }

        protected internal InvoiceRefusal()
        {
        }

        public InvoiceRefusal(WmId wmId, uint invoiceId)
        {
            WmId = wmId;
            InvoiceId = invoiceId;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", WmId, InvoiceId, requestNumber);
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartElement("invoicerefuse"); // <invoicerefuse>

            xmlRequestBuilder.WriteElement("wmid", WmId.ToString());
            xmlRequestBuilder.WriteElement("wminvid", InvoiceId);

            xmlRequestBuilder.WriteEndElement(); // </invoicerefuse>
        }
    }
}