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
    public class OriginalTransfer : WmRequest<TransferEnvelope>
    {
        protected override string ClassicUrl => "https://w3s.webmoney.ru/asp/XMLTrans.asp";

        protected override string LightUrl => "https://w3s.wmtransfer.com/asp/XMLTransCert.asp";

        public uint TransferId { get; set; }
        public Purse SourcePurse { get; set; }
        public Purse TargetPurse { get; set; }
        public Amount Amount { get; set; }
        public byte Period { get; set; }
        public Description Code { get; set; }
        public Description Description { get; set; }
        public uint InvoiceId { get; set; }
        public bool Force { get; set; }

        protected internal OriginalTransfer()
        {
        }

        public OriginalTransfer(uint transferId, Purse sourcePurse, Purse targetPurse, Amount amount)
        {
            TransferId = transferId;
            SourcePurse = sourcePurse;
            TargetPurse = targetPurse;
            Amount = amount;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}{5}{6}{7}{8}", requestNumber,
                                     TransferId, SourcePurse, TargetPurse, Amount, Period, Code, Description, InvoiceId);
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartElement("trans"); // <trans>

            xmlRequestBuilder.WriteElement("tranid", TransferId);
            xmlRequestBuilder.WriteElement("pursesrc", SourcePurse.ToString());
            xmlRequestBuilder.WriteElement("pursedest", TargetPurse.ToString());
            xmlRequestBuilder.WriteElement("amount", Amount.ToString());
            xmlRequestBuilder.WriteElement("period", Period);
            xmlRequestBuilder.WriteElement("pcode", Code);
            xmlRequestBuilder.WriteElement("desc", Description);
            xmlRequestBuilder.WriteElement("wminvid", InvoiceId);
            xmlRequestBuilder.WriteElement("onlyauth", Force ? 0 : 1);
            xmlRequestBuilder.WriteElement("wmb_denomination", string.Empty);

            xmlRequestBuilder.WriteEndElement(); // </trans>
        }
    }
}