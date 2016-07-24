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
    public class PassportFinder : WmRequest<Passport>
    {
        protected override string ClassicUrl => "https://passport.webmoney.ru/asp/XMLGetWMPassport.asp";

        protected override string LightUrl => "https://passport.webmoney.ru/asp/XMLGetWMPassport.asp";

        public WmId WmId { get; set; }

        protected internal PassportFinder()
        {
        }

        public PassportFinder(WmId wmId)
        {
            WmId = wmId;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", Initializer.Id, WmId);
        }

        protected override void BuildXmlHead(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartDocument();

            ulong requestNumber = Initializer.GetRequestNumber();

            xmlRequestBuilder.WriteStartElement("request"); // <request>

            if (AuthorizationMode.Classic == Initializer.Mode)
            {
                xmlRequestBuilder.WriteElement("wmid", Initializer.Id.ToString());
                xmlRequestBuilder.WriteElement("sign", Initializer.Sign(BuildMessage(requestNumber)));
            }
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteElement("passportwmid", WmId.ToString());

            xmlRequestBuilder.WriteStartElement("params"); // <params>

            xmlRequestBuilder.WriteElement("dict", 0);
            xmlRequestBuilder.WriteElement("info", 1);
            xmlRequestBuilder.WriteElement("mode", 1);

            xmlRequestBuilder.WriteEndElement(); // </params>
        }

        protected override void BuildXmlFoot(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteEndElement(); // </request>

            xmlRequestBuilder.WriteEndDocument();
        }
    }
}