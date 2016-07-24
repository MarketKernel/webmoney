using System;
using System.Globalization;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using WebMoney.Cryptography;
using WebMoney.XmlInterfaces.BasicObjects;

namespace WebMoney.XmlInterfaces
{
    public class Initializer
    {
        private static readonly object Anchor = new object();

        private ulong _lastNumber;
        private readonly Signer _signer;

        public static Initializer Instance { get; private set; }

        public WebProxy Proxy { get; set; }

        public AuthorizationMode Mode { get; protected set; }

        // WmId (для авторизацией ключами Keeper Classic или SecretKey)
        public WmId Id { get; protected set; }

        // Keeper Light
        public X509Certificate2 Certificate { get; protected set; }

        public string SecretKey { get; protected set; }

        public Initializer(WmId wmId, KeeperKey keeperKey)
        {
            if (null == keeperKey)
                throw new ArgumentNullException(nameof(keeperKey));

            Mode = AuthorizationMode.Classic;
            Id = wmId;
            _signer = new Signer();
            _signer.Initialize(keeperKey);
        }

        public Initializer(X509Certificate2 certificate)
        {
            if (null == certificate)
                throw new ArgumentNullException(nameof(certificate));

            Mode = AuthorizationMode.Light;
            Certificate = certificate;
        }

        public Initializer(string secretKey)
        {
            if (null == secretKey)
                throw new ArgumentNullException(nameof(secretKey));

            Mode = AuthorizationMode.Merchant;
            SecretKey = secretKey;
        }

        public Initializer()
        {
        }

        public void Apply()
        {
            Instance = this;
        }

        public virtual ulong GetRequestNumber()
        {
            string timestamp = DateTime.UtcNow.ToString("yyMMddHHmmssfff", CultureInfo.InvariantCulture.DateTimeFormat);
            ulong requestNumber = ulong.Parse(timestamp, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat);

            lock (Anchor)
            {
                if (requestNumber <= _lastNumber)
                    requestNumber = _lastNumber + 1;

                _lastNumber = requestNumber;
            }

            return requestNumber;
        }

        public virtual string Sign(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if (null == _signer)
                throw new InvalidOperationException("null == _signer");

            return _signer.Sign(value);
        }
    }
}