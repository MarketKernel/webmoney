using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace WebMoney.XmlInterfaces.Core
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    public static class CertificateValidator
    {
        private static readonly List<X509Certificate> TrustedCertificateList;

        public static bool DisableValidation { get; set; }

        static CertificateValidator()
        {
            TrustedCertificateList = new List<X509Certificate>();
        }

        public static void RegisterTrustedCertificate(X509Certificate trustedCertificate)
        {
            if (null == trustedCertificate)
                throw new ArgumentNullException(nameof(trustedCertificate));

            TrustedCertificateList.Add(trustedCertificate);
        }

        public static bool RemoteCertificateValidationCallback(
            object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (SslPolicyErrors.None == sslPolicyErrors)
                return true;

            if (DisableValidation)
                return true;

            // Сертификат не установлен в корневое хранилище
            if (SslPolicyErrors.RemoteCertificateChainErrors == sslPolicyErrors)
            {
                if (chain.ChainStatus.Length == 1 && chain.ChainStatus[0].Status == X509ChainStatusFlags.UntrustedRoot)
                {
                    X509ChainElement rootElement = chain.ChainElements[chain.ChainElements.Count - 1];

                    foreach (X509Certificate trustedCertificate in TrustedCertificateList)
                    {
                        if (rootElement.Certificate.Equals(trustedCertificate))
                            return true;
                    }
                }
            }

            return false;
        }
    }
}