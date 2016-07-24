using System;
using System.Configuration;
using System.Xml.Serialization;

namespace WebMoney.XmlInterfaces.Configuration
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable, XmlRoot("applicationInterfaces")]
    public sealed class AuthorizationSettings
    {
        private AuthorizationMode _authorizationMode = AuthorizationMode.Classic;
        private MerchantSettings _merchant;
        private KeeperClassicSettings _keeperClassic;
        private KeeperLightSettings _keeperLight;

        [XmlElement("authorizationMode")]
        public AuthorizationMode AuthorizationMode
        {
            get
            {
                return _authorizationMode;
            }
            set
            {
                _authorizationMode = value;
            }
        }

        [XmlElement("merchant")]
        public MerchantSettings Merchant
        {
            get
            {
                if (null == _merchant)
                    throw new ConfigurationErrorsException("webMoneyConfiguration/applicationInterfaces/merchant");

                return _merchant;
            }
            set
            {
                _merchant = value;
            }
        }

        [XmlElement("keeperClassic")]
        public KeeperClassicSettings KeeperClassic
        {
            get
            {
                if (null == _keeperClassic)
                    throw new ConfigurationErrorsException("webMoneyConfiguration/applicationInterfaces/keeperClassic");

                return _keeperClassic;
            }
            set
            {
                _keeperClassic = value;
            }
        }

        [XmlElement("keeperLight")]
        public KeeperLightSettings KeeperLight
        {
            get
            {
                if (null == _keeperLight)
                    throw new ConfigurationErrorsException("webMoneyConfiguration/applicationInterfaces/keeperLight");

                return _keeperLight;
            }
            set
            {
                _keeperLight = value;
            }
        }
    }
}
