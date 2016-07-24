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
    public class ProtectionFinisher : WmRequest<ProtectionReport>
    {
        protected override string ClassicUrl => "https://w3s.webmoney.ru/asp/XMLFinishProtect.asp";

        protected override string LightUrl => "https://w3s.wmtransfer.com/asp/XMLFinishProtectCert.asp";

        public uint OperationId { get; set; }
        public Description Code { get; set; }

        protected internal ProtectionFinisher()
        {
        }

        public ProtectionFinisher(uint operationId, Description code)
        {
            OperationId = operationId;
            Code = code;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", OperationId, Code, requestNumber);
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartElement("finishprotect"); // <finishprotect>

            xmlRequestBuilder.WriteElement("wmtranid", OperationId);
            xmlRequestBuilder.WriteElement("pcode", Code);

            xmlRequestBuilder.WriteEndElement(); // </finishprotect>
        }
    }
}