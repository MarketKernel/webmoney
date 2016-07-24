using System;
using System.Configuration;
using System.Xml.Serialization;
using WebMoney.XmlInterfaces.BasicObjects;

namespace WebMoney.XmlInterfaces.Configuration
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    public sealed class MerchantSettings
    {
        private WmId? _wmId;
        private string _secretKey;

        [XmlElement("wmId")]
        public WmId WmId
        {
            get
            {
                if (!_wmId.HasValue)
                    throw new ConfigurationErrorsException("webMoneyConfiguration/applicationInterfaces/merchantSettings/wmId");

                return _wmId.Value;
            }
            set
            {
                _wmId = value;
            }
        }

        [XmlElement("secretKey")]
        public string SecretKey
        {
            get
            {
                if (string.IsNullOrEmpty(_secretKey))
                    throw new ConfigurationErrorsException("webMoneyConfiguration/applicationInterfaces/merchantSettings/secretKey");

                return _secretKey;
            }
            set
            {
                _secretKey = value;
            }
        }
    }
}