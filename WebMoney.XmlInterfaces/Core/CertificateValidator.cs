using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using log4net;

namespace WebMoney.XmlInterfaces.Core
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    public static class CertificateValidator
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CertificateValidator));

        private static readonly object Anchor = new object();
        private static readonly List<X509Certificate2> TrustedCertificateList;

        public static bool DisableValidation { get; set; }

        static CertificateValidator()
        {
            TrustedCertificateList = new List<X509Certificate2>();
        }

        public static void RegisterTrustedCertificate(X509Certificate2 trustedCertificate)
        {
            if (null == trustedCertificate)
                throw new ArgumentNullException(nameof(trustedCertificate));

            lock (Anchor)
            {
                TrustedCertificateList.Add(trustedCertificate);
            }
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
                if (1 == chain.ChainStatus.Length &&
                    (chain.ChainStatus[0].Status.HasFlag(X509ChainStatusFlags.UntrustedRoot) || chain.ChainStatus[0]
                         .Status.HasFlag(X509ChainStatusFlags.PartialChain)))
                {
                    X509ChainElement rootElement = chain.ChainElements[chain.ChainElements.Count - 1];

                    lock (Anchor)
                    {
                        foreach (X509Certificate2 trustedCertificate in TrustedCertificateList)
                        {
                            if (rootElement.Certificate.Equals(trustedCertificate))
                                return true;

                            Logger.DebugFormat("{0} != {1}", rootElement.Certificate.Thumbprint,
                                trustedCertificate.Thumbprint);
                        }
                    }
                }
                else
                {
                    Logger.Debug("chain.ChainStatus.Length == " + chain.ChainStatus.Length);

                    int index = 0;

                    foreach (var chainStatus in chain.ChainStatus)
                    {
                        Logger.DebugFormat("chainStatus[{0}] == {1}", index, chainStatus.Status);
                        index++;
                    }
                }
            }
            else
            {
                Logger.Debug("sslPolicyErrors.HasFlag(SslPolicyErrors.RemoteCertificateChainErrors) == " + sslPolicyErrors.HasFlag(SslPolicyErrors.RemoteCertificateChainErrors));
                Logger.Debug("sslPolicyErrors.HasFlag(SslPolicyErrors.RemoteCertificateNameMismatch) == " + sslPolicyErrors.HasFlag(SslPolicyErrors.RemoteCertificateNameMismatch));
                Logger.Debug("sslPolicyErrors.HasFlag(SslPolicyErrors.RemoteCertificateNotAvailable) == " + sslPolicyErrors.HasFlag(SslPolicyErrors.RemoteCertificateNotAvailable));
            }

            return false;
        }
    }
}