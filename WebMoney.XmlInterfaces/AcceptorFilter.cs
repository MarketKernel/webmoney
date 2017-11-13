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
    public class AcceptorFilter : WmRequest<AcceptorRegister>
    {
        protected override string ClassicUrl => "https://arbitrage.webmoney.ru/xml/X17_GetContractInfo.aspx";

        protected override string LightUrl => null;

        public uint ContractId { get; set; }

        public Description Mode => (Description) "acceptdate";

        protected internal AcceptorFilter()
        {
        }

        public AcceptorFilter(uint contractId)
        {
            ContractId = contractId;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", ContractId, Mode);
        }

        protected override void BuildXmlHead(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartDocument();

            xmlRequestBuilder.WriteStartElement("contract.request"); // <contract.request>

            ulong requestNumber = Initializer.GetRequestNumber();

            if (AuthorizationMode.Classic == Initializer.Mode)
            {
                xmlRequestBuilder.WriteElement("wmid", Initializer.Id.ToString());
                xmlRequestBuilder.WriteElement("sign", Initializer.Sign(BuildMessage(requestNumber)));
            }
            else
                throw new InvalidOperationException("AuthorizationMode.Classic != Initializer.Mode");
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteElement("mode", Mode);
            xmlRequestBuilder.WriteElement("contractid", ContractId);
        }

        protected override void BuildXmlFoot(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteEndElement(); // </contract.request>

            xmlRequestBuilder.WriteEndDocument();
        }
    }
}