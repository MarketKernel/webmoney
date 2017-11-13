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
    public class ClientInspector : WmRequest<ClientEvidence>
    {
        private bool _output = true;
        private string _cryptoCurrencyAddress;
        
        protected override string ClassicUrl => "https://apipassport.webmoney.ru/XMLCheckUser.aspx";

        protected override string LightUrl => "https://apipassportcrt.webmoney.ru/XMLCheckUserCert.aspx";

        public ExchangeType OperationType { get; set; }

        public bool Output
        {
            get => _output;
            set => _output = value;
        }

        public WmCurrency Currency { get; set; }
        public Amount Amount { get; set; }

        public WmId Wmid { get; set; }
        public Description PassportNumber { get; set; }
        public Description SecondName { get; set; }
        public Description FirstName { get; set; }
        public Description BankName { get; set; }
        public BankAccount BankAccount { get; set; }
        public BankCard CardNumber { get; set; }
        public PaymentSystem PaymentSystem { get; set; }
        public Description PaymentId { get; set; }
        public string Phone { get; set; }
        public CryptoCurrencyType CryptoCurrencyType { get; set; }

        public string CryptoCurrencyAddress
        {
            get => _cryptoCurrencyAddress;
            set => _cryptoCurrencyAddress = value ?? throw new ArgumentNullException(nameof(value));
        }

        protected internal ClientInspector()
        {
        }

        // Cash
        public ClientInspector(WmCurrency currency, Amount amount, WmId wmid, Description passportNumber, Description secondName, Description firstName)
        {
            OperationType = ExchangeType.Cash;
            Currency = currency;
            Amount = amount;
            Wmid = wmid;
            PassportNumber = passportNumber;
            FirstName = firstName;
            SecondName = secondName;
        }

        // OfflineSystem
        public ClientInspector(WmCurrency currency, Amount amount, WmId wmid, Description secondName, Description firstName)
        {
            OperationType = ExchangeType.OfflineSystem;
            Currency = currency;
            Amount = amount;
            Wmid = wmid;
            SecondName = secondName;
            FirstName = firstName;
        }

        // BankAccount
        public ClientInspector(WmCurrency currency, Amount amount, WmId wmid, Description secondName, Description firstName, Description bankName, BankAccount bankAccount)
        {
            OperationType = ExchangeType.BankAccount;
            Currency = currency;
            Amount = amount;
            Wmid = wmid;
            SecondName = secondName;
            FirstName = firstName;
            BankName = bankName;
            BankAccount = bankAccount;
        }

        // BankCard
        public ClientInspector(WmCurrency currency, Amount amount, WmId wmid, Description secondName, Description firstName, Description bankName, BankCard cardNumber)
        {
            OperationType = ExchangeType.BankCard;
            Currency = currency;
            Amount = amount;
            Wmid = wmid;
            SecondName = secondName;
            FirstName = firstName;
            BankName = bankName;
            CardNumber = cardNumber;
        }

        // InternetSystem
        public ClientInspector(WmCurrency currency, Amount amount, WmId wmid, PaymentSystem paymentSystem, Description paymentId)
        {
            OperationType = ExchangeType.InternetSystem;
            Currency = currency;
            Amount = amount;
            Wmid = wmid;
            PaymentSystem = paymentSystem;
            PaymentId = paymentId;
        }

        // SMS
        public ClientInspector(WmCurrency currency, Amount amount, WmId wmid, string phone)
        {
            OperationType = ExchangeType.Sms;
            Currency = currency;
            Amount = amount;
            Wmid = wmid;
            Phone = phone;
        }

        // Moble
        public ClientInspector(WmCurrency currency, Amount amount, WmId wmid, string phone, bool mobile)
        {
            OperationType = ExchangeType.Mobile;
            Currency = currency;
            Amount = amount;
            Wmid = wmid;
            Phone = phone;
        }

        // Crypto
        public ClientInspector(WmCurrency currency, Amount amount, WmId wmid, CryptoCurrencyType cryptoCurrencyType, string address)
        {
            OperationType = ExchangeType.CryptoCurrency;
            Currency = currency;
            Amount = amount;
            Wmid = wmid;
            CryptoCurrencyType = cryptoCurrencyType;
            CryptoCurrencyAddress = address ?? throw new ArgumentNullException(nameof(address));
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", requestNumber, (int)OperationType, Wmid);
        }

        protected override void BuildXmlHead(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartDocument();

            xmlRequestBuilder.WriteStartElement("passport.request"); // <passport.request>

            ulong requestNumber = Initializer.GetRequestNumber();

            xmlRequestBuilder.WriteElement("reqn", requestNumber);

            if (AuthorizationMode.Classic == Initializer.Mode)
            {
                xmlRequestBuilder.WriteElement("signerwmid", Initializer.Id.ToString());
                xmlRequestBuilder.WriteElement("sign", Initializer.Sign(BuildMessage(requestNumber)));
            }
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartElement("operation"); // <operation>

            xmlRequestBuilder.WriteElement("type", (int)OperationType);
            xmlRequestBuilder.WriteElement("direction", Output ? 1 : 2);
            xmlRequestBuilder.WriteElement("pursetype", "WM" + Purse.CurrencyToLetter(Currency));
            xmlRequestBuilder.WriteElement("amount", Amount.ToString());

            xmlRequestBuilder.WriteEndElement(); // </operation>

            xmlRequestBuilder.WriteStartElement("userinfo"); // <userinfo>

            xmlRequestBuilder.WriteElement("wmid", Wmid.ToString());

            switch (OperationType)
            {
                case ExchangeType.Cash:
                    xmlRequestBuilder.WriteElement("pnomer", PassportNumber);
                    xmlRequestBuilder.WriteElement("fname", SecondName);
                    xmlRequestBuilder.WriteElement("iname", FirstName);
                    break;
                case ExchangeType.OfflineSystem:
                    xmlRequestBuilder.WriteElement("fname", SecondName);
                    xmlRequestBuilder.WriteElement("iname", FirstName);
                    break;
                case ExchangeType.BankAccount:
                    xmlRequestBuilder.WriteElement("fname", SecondName);
                    xmlRequestBuilder.WriteElement("iname", FirstName);
                    xmlRequestBuilder.WriteElement("bank_name", BankName);
                    xmlRequestBuilder.WriteElement("bank_account", BankAccount.ToString());
                    break;
                case ExchangeType.BankCard:
                    xmlRequestBuilder.WriteElement("fname", SecondName);
                    xmlRequestBuilder.WriteElement("iname", FirstName);
                    xmlRequestBuilder.WriteElement("bank_name", BankName);
                    xmlRequestBuilder.WriteElement("card_number", CardNumber.ToString());
                    break;
                case ExchangeType.InternetSystem:
                {
                    string paymentSystem;

                    switch (PaymentSystem)
                    {
                        case PaymentSystem.PayPal:
                            paymentSystem = "paypal.com";
                            break;
                        case PaymentSystem.Skrill:
                            paymentSystem = "skrill.com";
                            break;
                        case PaymentSystem.Alipay:
                            paymentSystem = "alipay.com";
                            break;
                        case PaymentSystem.Qiwi:
                            paymentSystem = "qiwi.ru";
                            break;
                        case PaymentSystem.YandexMoney:
                            paymentSystem = "pamoney.yandex.ru";
                            break;
                        default:
                            throw new InvalidOperationException("PaymentSystem == " + PaymentSystem);
                    }

                    xmlRequestBuilder.WriteElement("emoney_name", paymentSystem);
                    xmlRequestBuilder.WriteElement("emoney_id", PaymentId);
                }
                    break;
                case ExchangeType.Sms:
                    xmlRequestBuilder.WriteElement("phone", Phone);
                    break;
                case ExchangeType.Mobile:
                    xmlRequestBuilder.WriteElement("phone", Phone);
                    break;
                case ExchangeType.CryptoCurrency:
                {
                    string cryptoCurrencyName;

                    switch (CryptoCurrencyType)
                    {
                        case CryptoCurrencyType.Bitcoin:
                            cryptoCurrencyName = "bitcoin";
                            break;
                        case CryptoCurrencyType.BitcoinCash:
                            cryptoCurrencyName = "bitcoincash";
                            break;
                        default:
                            throw new InvalidOperationException("CryptoCurrencyType == " + CryptoCurrencyType);
                    }

                    xmlRequestBuilder.WriteElement("crypto_name", cryptoCurrencyName);
                    xmlRequestBuilder.WriteElement("crypto_address", CryptoCurrencyAddress);
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            xmlRequestBuilder.WriteEndElement(); // </userinfo>
        }

        protected override void BuildXmlFoot(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteEndElement(); // </passport.request>

            xmlRequestBuilder.WriteEndDocument();
        }
    }
}