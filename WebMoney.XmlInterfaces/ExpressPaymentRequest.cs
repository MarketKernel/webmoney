using System;
using System.Globalization;
using System.Net.Mail;
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
    public class ExpressPaymentRequest : WmRequest<ExpressPaymentResponse>
    {
        protected override string ClassicUrl => "https://merchant.webmoney.ru/conf/xml/XMLTransRequest.asp";

        protected override string LightUrl
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public Purse StorePurse { get; set; }

        public uint OrderId { get; set; }

        public Amount Amount { get; set; }

        public Description Description { get; set; }

        public Phone ClientPhone { get; set; }

        public WmId ClientWmId { get; set; }

        public MailAddress ClientEmail { get; set; }

        public ClientIdType ClientIdType { get; set; }

        public ConfirmationType ConfirmationType { get; set; }

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

        protected internal ExpressPaymentRequest()
        {
        }

        public ExpressPaymentRequest(
            Purse storePurse,
            uint orderId,
            Amount amount,
            Description description,
            Phone clientPhone,
            ConfirmationType confirmationType,
            CultureInfo culture = null)
        {
            if (null == culture)
                culture = Thread.CurrentThread.CurrentUICulture;

            StorePurse = storePurse;
            OrderId = orderId;
            Amount = amount;
            Description = description;
            ClientPhone = clientPhone;
            ClientIdType = ClientIdType.Phone;
            ConfirmationType = confirmationType;
            Culture = culture;
        }

        public ExpressPaymentRequest(
            Purse storePurse,
            uint orderId,
            Amount amount,
            Description description,
            WmId clientWmId,
            ConfirmationType confirmationType,
            CultureInfo culture = null)
        {
            if (null == culture)
                culture = Thread.CurrentThread.CurrentUICulture;

            StorePurse = storePurse;
            OrderId = orderId;
            Amount = amount;
            Description = description;
            ClientWmId = clientWmId;
            ClientIdType = ClientIdType.WmId;
            ConfirmationType = confirmationType;
            Culture = culture;
        }

        public ExpressPaymentRequest(
            Purse storePurse,
            uint orderId,
            Amount amount,
            Description description,
            MailAddress clientEmail,
            ConfirmationType confirmationType,
            CultureInfo culture = null)
        {
            if (null == clientEmail)
                throw new ArgumentNullException(nameof(clientEmail));

            if (null == culture)
                culture = Thread.CurrentThread.CurrentUICulture;

            StorePurse = storePurse;
            OrderId = orderId;
            Amount = amount;
            Description = description;
            ClientEmail = clientEmail;
            ClientIdType = ClientIdType.Email;
            ConfirmationType = confirmationType;
            Culture = culture;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0}{1}{2}{3}{4}",
                Initializer.Id,
                StorePurse,
                OrderId,
                GetClientId(),
                (int)ClientIdType);
        }

        protected override void BuildXmlHead(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

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

            xmlRequestBuilder.WriteElement("lmi_payee_purse", StorePurse.ToString());
            xmlRequestBuilder.WriteElement("lmi_payment_no", OrderId);
            xmlRequestBuilder.WriteElement("lmi_payment_amount", Amount.ToString());
            xmlRequestBuilder.WriteElement("lmi_payment_desc", Description);
            xmlRequestBuilder.WriteElement("lmi_clientnumber", GetClientId());
            xmlRequestBuilder.WriteElement("lmi_clientnumber_type", (int)ClientIdType);
            xmlRequestBuilder.WriteElement("lmi_sms_type", (int)ConfirmationType);
            xmlRequestBuilder.WriteElement("lang", Culture.Name);
        }

        protected override void BuildXmlFoot(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteEndElement(); // </merchant.request>

            xmlRequestBuilder.WriteEndDocument();
        }

        private string GetClientId()
        {
            string clientId;

            switch (ClientIdType)
            {
                case ClientIdType.Phone:
                    clientId = ClientPhone.ToString().Remove(0, 1);
                    break;
                case ClientIdType.WmId:
                    clientId = ClientWmId.ToString();
                    break;
                case ClientIdType.Email:
                    clientId = ClientEmail.Address;
                    break;
                default:
                    throw new InvalidOperationException("ClientIdType=" + ClientIdType);
            }

            return clientId;
        }
    }
}
