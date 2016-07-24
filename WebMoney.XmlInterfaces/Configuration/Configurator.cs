using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using WebMoney.Cryptography;

namespace WebMoney.XmlInterfaces.Configuration
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    public class Configurator : Initializer
    {
        private readonly Signer _signer;

        public Configurator()
        {
            AuthorizationSettings authorizationSettings = ConfigurationAccessor.GetAuthorizationSettings();

            Mode = authorizationSettings.AuthorizationMode;

            switch (Mode)
            {
                case AuthorizationMode.Merchant:
                    Id = authorizationSettings.Merchant.WmId;
                    SecretKey = authorizationSettings.Merchant.SecretKey;
                    break;
                case AuthorizationMode.Classic:
                    Id = authorizationSettings.KeeperClassic.WmId;
                    _signer = new Signer();
                    _signer.Initialize(authorizationSettings.KeeperClassic.WmKey);
                    break;
                case AuthorizationMode.Light:
                    if (null != authorizationSettings.KeeperLight.LightContainer)
                    {
                        X509Certificate2 certificate;
                        var store = new X509Store(authorizationSettings.KeeperLight.LightContainer.StoreName,
                                                        authorizationSettings.KeeperLight.LightContainer.StoreLocation);

                        try
                        {
                            store.Open(OpenFlags.ReadOnly);

                            var collection =
                                store.Certificates.Find(X509FindType.FindByThumbprint,
                                                        authorizationSettings.KeeperLight.LightContainer.Thumbprint, false);

                            if (0 == collection.Count || !collection[0].HasPrivateKey)
                                throw new ConfigurationErrorsException("webMoneyConfiguration/applicationInterfaces/keeperLight/containerInfo/thumbprint");

                            certificate = collection[0];
                        }
                        finally
                        {
                            store.Close();
                        }

                        Certificate = certificate;
                    }
                    else
                    {
                        var certificate = new X509Certificate2(authorizationSettings.KeeperLight.RawData,
                                                                            string.Empty);

                        Certificate = certificate;
                    }
                    break;
                default:
                    throw new InvalidOperationException("Mode=" + Mode);
            }
        }

        public override string Sign(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if (null == _signer)
                throw new InvalidOperationException("null == _signer");

            return _signer.Sign(value);
        }
    }
}