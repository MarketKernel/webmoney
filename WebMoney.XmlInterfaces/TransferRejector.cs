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
    public class TransferRejector : WmRequest<MoneybackReport>
    {
        protected override string ClassicUrl => "https://w3s.webmoney.ru/asp/XMLTransMoneyback.asp";

        protected override string LightUrl => "https://w3s.wmtransfer.com/asp/XMLTransMoneybackCert.asp";

        public uint OperationId { get; set; }
        public Amount Amount { get; set; }
        public Phone Phone { get; set; }

        protected internal TransferRejector()
        {
        }

        public TransferRejector(uint operationId, Amount amount)
        {
            OperationId = operationId;
            Amount = amount;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", requestNumber, OperationId, Amount);
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartElement("trans"); // <trans>

            xmlRequestBuilder.WriteElement("inwmtranid", OperationId);
            xmlRequestBuilder.WriteElement("amount", Amount.ToString());
            xmlRequestBuilder.WriteElement("moneybackphone", Phone.ToString());

            xmlRequestBuilder.WriteEndElement(); // </trans>
        }
    }
}