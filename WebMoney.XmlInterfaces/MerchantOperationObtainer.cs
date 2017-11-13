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
    public class MerchantOperationObtainer : WmRequest<MerchantOperation>
    {
        protected override string ClassicUrl => "https://merchant.webmoney.ru/conf/xml/XMLTransGet.asp";

        protected override string LightUrl
        {
            get { throw new NotSupportedException(); }
        }

        public Purse TargetPurse { get; set; }
        public uint PaymentNumber { get; set; }
        public PaymentNumberKind NumberType { get; set; }

        protected internal MerchantOperationObtainer()
        {
        }

        public MerchantOperationObtainer(Purse targetPurse, uint paymentNumber, PaymentNumberKind numberType)
        {
            TargetPurse = targetPurse;
            PaymentNumber = paymentNumber;
            NumberType = numberType;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", Initializer.Id, TargetPurse, PaymentNumber);
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
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteElement("lmi_payee_purse", TargetPurse.ToString());
            xmlRequestBuilder.WriteElement("lmi_payment_no_type", (int)NumberType);
            xmlRequestBuilder.WriteElement("lmi_payment_no", PaymentNumber);
        }

        protected override void BuildXmlFoot(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteEndElement(); // </merchant.request>

            xmlRequestBuilder.WriteEndDocument();
        }
    }
}