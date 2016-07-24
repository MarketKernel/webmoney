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
    public class OriginalMessage : WmRequest<RecentMessage>
    {
        private Message _content;

        protected override string ClassicUrl => "https://w3s.webmoney.ru/asp/XMLSendMsg.asp";

        protected override string LightUrl => "https://w3s.wmtransfer.com/asp/XMLSendMsgCert.asp";

        public WmId WmId { get; set; }
        public Description Subject { get; set; }

        public Message Content
        {
            get { return _content; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(value));
                _content = value;
            }
        }

        protected internal OriginalMessage()
        {
        }

        public OriginalMessage(WmId wmId, Description subject, Message content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            WmId = wmId;
            Subject = subject;
            Content = content;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}", WmId, requestNumber, Content, Subject);
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartElement("message"); // <message>

            xmlRequestBuilder.WriteElement("receiverwmid", WmId.ToString());
            xmlRequestBuilder.WriteElement("msgsubj", Subject);
            xmlRequestBuilder.WriteElement("msgtext", Content);

            xmlRequestBuilder.WriteEndElement(); // </message>
        }
    }
}