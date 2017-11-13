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
    public class OriginalInvoice : WmRequest<InvoiceEnvelope>
    {
        protected override string ClassicUrl => "https://w3s.webmoney.ru/asp/XMLInvoice.asp";

        protected override string LightUrl => "https://w3s.wmtransfer.com/asp/XMLInvoiceCert.asp";

        public uint OrderId { get; set; }
        public WmId SourceWmId { get; set; }
        public Purse TargetPurse { get; set; }
        public Amount Amount { get; set; }
        public Description Description { get; set; }
        public Description Address { get; set; }
        public byte Period { get; set; }
        public byte Expiration { get; set; }
        public bool Force { get; set; }
        public int? ShopId { get; set; }

        protected internal OriginalInvoice()
        {
        }

        public OriginalInvoice(uint orderId, WmId sourceWmId, Purse targetPurse, Amount amount)
        {
            OrderId = orderId;
            SourceWmId = sourceWmId;
            TargetPurse = targetPurse;
            Amount = amount;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}{5}{6}{7}{8}", OrderId,
                                     SourceWmId, TargetPurse, Amount, Description, Address, Period, Expiration,
                                     requestNumber);
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartElement("invoice"); // <invoice>

            xmlRequestBuilder.WriteElement("orderid", OrderId);
            xmlRequestBuilder.WriteElement("customerwmid", SourceWmId.ToString());
            xmlRequestBuilder.WriteElement("storepurse", TargetPurse.ToString());
            xmlRequestBuilder.WriteElement("amount", Amount.ToString());
            xmlRequestBuilder.WriteElement("desc", Description);
            xmlRequestBuilder.WriteElement("address", Address);
            xmlRequestBuilder.WriteElement("period", Period);
            xmlRequestBuilder.WriteElement("expiration", Expiration);
            xmlRequestBuilder.WriteElement("onlyauth", Force ? 0 : 1);

            if (null != ShopId)
                xmlRequestBuilder.WriteElement("lmi_shop_id", ShopId.Value);

            xmlRequestBuilder.WriteEndElement(); // </invoice>
        }
    }
}