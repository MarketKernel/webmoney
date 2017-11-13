using System;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Responses;

namespace WebMoney.XmlInterfaces
{
    public sealed class PaymerTransfer : WmRequest<PaymerTransferReport>
    {
        protected override string ClassicUrl => "https://w3s.webmoney.ru/asp/XMLPaymer2purse.asp";
        protected override string LightUrl => throw new NotSupportedException();

        public Purse Purse { get; }
        public Description Number { get; }
        public Description Code { get; }

        public PaymerTransfer(Purse purse, Description number, Description code)
        {
            Purse = purse;
            Number = number;
            Code = code;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return $"{Purse}{Number}{Code}{requestNumber}";
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartElement("paymer2purse"); // <paymer2purse>

            xmlRequestBuilder.WriteElement("purse", Purse.ToString());

            xmlRequestBuilder.WriteStartElement("paymer"); // <paymer>
            xmlRequestBuilder.WriteElement("number", Number);
            xmlRequestBuilder.WriteElement("code", Code);
            xmlRequestBuilder.WriteEndElement(); // </paymer>

            xmlRequestBuilder.WriteEndElement(); // </paymer2purse>
        }
    }
}
