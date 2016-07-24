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
    public class SignatureInspector : WmRequest<SignatureEvidence>
    {
        protected override string ClassicUrl => "https://w3s.webmoney.ru/asp/XMLClassicAuth.asp";

        protected override string LightUrl => "https://w3s.wmtransfer.com/asp/XMLClassicAuthCert.asp";

        public WmId WmId { get; set; }
        public Message Message { get; set; }
        public Description Signature { get; set; }

        protected internal SignatureInspector()
        {
        }

        public SignatureInspector(WmId wmId, Message message, Description signature)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            if (string.IsNullOrEmpty(signature))
                throw new ArgumentNullException(nameof(signature));

            WmId = wmId;
            Message = message;
            Signature = signature;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}", Initializer.Id, WmId, Message,
                                     Signature);
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartElement("testsign"); // <testsign>

            xmlRequestBuilder.WriteElement("wmid", WmId.ToString());
            xmlRequestBuilder.WriteElement("plan", Message);
            xmlRequestBuilder.WriteElement("sign", Signature);

            xmlRequestBuilder.WriteEndElement(); // </testsign>
        }
    }
}