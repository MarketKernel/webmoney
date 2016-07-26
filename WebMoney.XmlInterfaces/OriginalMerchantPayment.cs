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
    public class OriginalMerchantPayment : WmRequest<MerchantPaymentToken>
    {
        protected override string ClassicUrl => "https://merchant.webmoney.ru/conf/xml/XMLTransSave.asp";

        protected override string LightUrl
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public uint OrderId { get; set; }

        public Purse StorePurse { get; set; }

        public Amount Amount { get; set; }

        public Description Description { get; set; }

        public uint Lifetime { get; set; }

        protected internal OriginalMerchantPayment()
        {
        }

        public OriginalMerchantPayment(
            uint orderId, Purse storePurse, Amount amount, Description description, uint lifeTime)
        {
            OrderId = orderId;
            StorePurse = storePurse;
            Amount = amount;
            Description = description;
            Lifetime = lifeTime;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(
                CultureInfo.InvariantCulture, "{0}{1}{2}{3}", Initializer.Id, StorePurse, OrderId, Lifetime);
        }

        protected override void BuildXmlHead(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder) throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartDocument();

            xmlRequestBuilder.WriteStartElement("merchant.request"); // <merchant.request>

            xmlRequestBuilder.WriteStartElement("signtags"); // <signtags>

            xmlRequestBuilder.WriteElement("wmid", Initializer.Id.ToString());
            xmlRequestBuilder.WriteElement("validityperiodinhours", Lifetime.ToString(CultureInfo.InvariantCulture));

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

            xmlRequestBuilder.WriteEndElement(); // </signtags>
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartElement("paymenttags"); // <paymenttags>

            xmlRequestBuilder.WriteElement("lmi_payee_purse", StorePurse.ToString());
            xmlRequestBuilder.WriteElement("lmi_payment_amount", Amount.ToString());
            xmlRequestBuilder.WriteElement("lmi_payment_no", OrderId);
            xmlRequestBuilder.WriteElement("lmi_payment_desc", Description);

            // Добавочные
            xmlRequestBuilder.WriteElement("lmi_payment_desc", Description);

            xmlRequestBuilder.WriteEndElement(); // </paymenttags>
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
