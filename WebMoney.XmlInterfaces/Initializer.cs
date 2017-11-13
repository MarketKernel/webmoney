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

        private AuthorizationMode _mode;
        private WmId _id;
        private readonly Signer _signer;
        private X509Certificate2 _certificate;
        private string _secretKey;
        private ulong _lastNumber;
        private WebProxy _proxy;

        public static Initializer Instance { get; private set; }

        public virtual AuthorizationMode Mode
        {
            get => _mode;
            protected set => _mode = value;
        }

        // WmId (для авторизацией ключами Keeper Classic или SecretKey)
        public virtual WmId Id
        {
            get => _id;
            protected set => _id = value;
        }

        // Keeper Light
        public virtual X509Certificate2 Certificate
        {
            get => _certificate;
            protected set => _certificate = value;
        }

        public virtual string SecretKey
        {
            get => _secretKey;
            protected set => _secretKey = value;
        }

        public virtual WebProxy Proxy
        {
            get => _proxy;
            protected set => _proxy = value;
        }

        public Initializer(WmId wmId, KeeperKey keeperKey, WebProxy proxy = null)
        {
            if (null == keeperKey)
                throw new ArgumentNullException(nameof(keeperKey));

            _mode = AuthorizationMode.Classic;
            _id = wmId;

            var signer = new Signer();
            signer.Initialize(keeperKey);

            _signer = signer;
            _proxy = proxy;
        }

        public Initializer(X509Certificate2 certificate, WebProxy proxy = null)
        {
            _mode = AuthorizationMode.Light;
            _certificate = certificate ?? throw new ArgumentNullException(nameof(certificate));
            _proxy = proxy;
        }

        public Initializer(WmId wmId, string secretKey, WebProxy proxy = null)
        {
            _mode = AuthorizationMode.Merchant;
            _id = wmId;
            _secretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
            _proxy = proxy;

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
            ulong requestNumber = ulong.Parse(timestamp, NumberStyles.Integer,
                CultureInfo.InvariantCulture.NumberFormat);

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

            lock (Anchor)
            {
                return _signer.Sign(value);
            }
        }
    }
}