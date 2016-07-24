using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

namespace WebMoney.XmlInterfaces.Configuration
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    public sealed class KeeperLightSettings
    {
        [Serializable]
        public sealed class LightContainerSettings
        {
            private StoreLocation _storeLocation = StoreLocation.CurrentUser;
            private StoreName _storeName = StoreName.My;
            private string _thumbprint;

            [XmlElement("storeLocation")]
            public StoreLocation StoreLocation
            {
                get
                {
                    return _storeLocation;
                }
                set
                {
                    _storeLocation = value;
                }
            }

            [XmlElement("storeName")]
            public StoreName StoreName
            {
                get
                {
                    return _storeName;
                }
                set
                {
                    _storeName = value;
                }
            }

            [XmlElement("thumbprint")]
            public string Thumbprint
            {
                get
                {
                    if (string.IsNullOrEmpty(_thumbprint))
                        throw new ConfigurationErrorsException("webMoneyConfiguration/applicationInterfaces/keeperLight/containerInfo/thumbprint");

                    return _thumbprint;
                }
                set
                {
                    _thumbprint = value;
                }
            }
        }

        private byte[] _rawData;
        private LightContainerSettings _lightContainer;

        [XmlElement("containerInfo")]
        public LightContainerSettings LightContainer
        {
            get
            {
                return _lightContainer;
            }
            set
            {
                _lightContainer = value;
            }
        }

        [XmlElement("rawData")]
        public byte[] RawData
        {
            get
            {
                if (null == _rawData)
                    throw new ConfigurationErrorsException("webMoneyConfiguration/applicationInterfaces/keeperLight/rawData");

                return _rawData;
            }
            set
            {
                _rawData = value;
            }
        }
    }
}