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
    public sealed class KeeperClassicSettings
    {
        private WmId? _wmId;
        private string _wmKey;

        [XmlElement("wmId")]
        public WmId WmId
        {
            get
            {
                if (!_wmId.HasValue)
                    throw new ConfigurationErrorsException("webMoneyConfiguration/applicationInterfaces/keeperClassic/wmId");

                return _wmId.Value;
            }
            set
            {
                _wmId = value;
            }
        }

        [XmlElement("wmKey")]
        public string WmKey
        {
            get
            {
                if (string.IsNullOrEmpty(_wmKey))
                    throw new ConfigurationErrorsException("webMoneyConfiguration/applicationInterfaces/keeperClassic/wmKey");

                return _wmKey;
            }
            set
            {
                _wmKey = value;
            }
        }
    }
}