using System;
using System.Globalization;
using System.Threading;
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
    public class ExpressPaymentConfirmation : WmRequest<ExpressPaymentReport>
    {
        protected override string ClassicUrl => "https://merchant.webmoney.ru/conf/xml/XMLTransConfirm.asp";

        protected override string LightUrl
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public Purse StorePurse { get; set; }

        public string ConfirmationCode { get; set; }

        public uint InvoiceId { get; set; }

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

        protected internal ExpressPaymentConfirmation()
        {
        }

        public ExpressPaymentConfirmation(Purse storePurse, string confirmationCode, uint invoiceId, CultureInfo culture)
        {
            if (null == culture)
                culture = Thread.CurrentThread.CurrentUICulture;

            StorePurse = storePurse;
            ConfirmationCode = confirmationCode ?? "0";
            InvoiceId = invoiceId;
            Culture = culture;
        }

        public ExpressPaymentConfirmation(Purse storePurse, uint invoiceId, CultureInfo culture)
            : this(storePurse, null, invoiceId, culture)
        {
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(
                CultureInfo.InvariantCulture, "{0}{1}{2}{3}", Initializer.Id, StorePurse, InvoiceId, ConfirmationCode);
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

            xmlRequestBuilder.WriteElement("lmi_payee_purse", StorePurse.ToString());
            xmlRequestBuilder.WriteElement(
                "lmi_clientnumber_code", ConfirmationCode);
            xmlRequestBuilder.WriteElement("lmi_wminvoiceid", InvoiceId);
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
