using System;
using System.Globalization;
using System.Threading;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Responses;

namespace WebMoney.XmlInterfaces
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    public class ExpressTrustConfirmation : WmRequest<ExpressTrustReport>
    {
        protected override string ClassicUrl => "https://merchant.webmoney.ru/conf/xml/XMLTrustConfirm.asp";

        protected override string LightUrl => "https://merchant.wmtransfer.com/conf/xml/XMLTrustConfirm.asp";

        public uint Reference { get; set; }

        public string ConfirmationCode { get; set; }

        private CultureInfo _culture;
        public CultureInfo Culture
        {
            get { return _culture; }
            set
            {
                if (null == value)
                    throw new ArgumentNullException(nameof(value));

                _culture = value;
            }
        }

        protected internal ExpressTrustConfirmation()
        {
        }

        public ExpressTrustConfirmation(uint reference, CultureInfo culture)
            :this(reference, null, culture)
        {
        }

        public ExpressTrustConfirmation(uint reference, string confirmationCode, CultureInfo culture = null)
        {
            if (null == culture)
                culture = Thread.CurrentThread.CurrentUICulture;

            Reference = reference;
            ConfirmationCode = confirmationCode??"0";
            Culture = culture;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", Initializer.Id, Reference, ConfirmationCode);
        }

        protected override void BuildXmlHead(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder) throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartDocument();

            xmlRequestBuilder.WriteStartElement("merchant.request"); // <merchant.request>

            xmlRequestBuilder.WriteElement("wmid", Initializer.Id.ToString());

            ulong requestNumber = Initializer.GetRequestNumber();

            switch (Initializer.Mode)
            {
                case AuthorizationMode.Merchant:
                    xmlRequestBuilder.WriteElement(
                        "md5", Utilities.CryptographyUtility.ComputeHash(BuildMessage(requestNumber) + Initializer.SecretKey));
                    break;
                case AuthorizationMode.Classic:
                    xmlRequestBuilder.WriteElement("sign", Initializer.Sign(BuildMessage(requestNumber)));
                    break;
                default:
                    throw new InvalidOperationException("Initializer.Mode=" + Initializer.Mode);
            }
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder) throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteElement("lmi_purseid", Reference);
            xmlRequestBuilder.WriteElement("lmi_clientnumber_code", ConfirmationCode);
            xmlRequestBuilder.WriteElement("lang", Culture.Name);
        }

        protected override void BuildXmlFoot(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder) throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteEndElement(); // </merchant.request>

            xmlRequestBuilder.WriteEndDocument();
        }
    }
}
