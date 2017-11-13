using System;
using System.Globalization;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Responses;

namespace WebMoney.XmlInterfaces
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    public class ProtectionRejector : WmRequest<ProtectionReport>
    {
        protected override string ClassicUrl => "https://w3s.webmoney.ru/asp/XMLRejectProtect.asp";

        protected override string LightUrl => "https://w3s.wmtransfer.com/asp/XMLRejectProtectCert.asp";

        public uint OperationId { get; set; }

        protected internal ProtectionRejector()
        {
        }

        public ProtectionRejector(uint operationId)
        {
            OperationId = operationId;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", OperationId, requestNumber);
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartElement("rejectprotect"); // <rejectprotect>

            xmlRequestBuilder.WriteElement("wmtranid", OperationId);

            xmlRequestBuilder.WriteEndElement(); // </rejectprotect>
        }
    }
}