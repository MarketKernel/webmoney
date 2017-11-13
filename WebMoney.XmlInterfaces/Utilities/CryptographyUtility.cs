using System.Security.Cryptography;
using System.Text;
using System;
using WebMoney.XmlInterfaces.BasicObjects;

namespace WebMoney.XmlInterfaces.Utilities
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    internal static class CryptographyUtility
    {
        private static readonly Encoding DefaultEncoding = Encoding.GetEncoding("windows-1251");

        public static string ComputeHash(string value, HashKind hashKind = HashKind.MD5)
        {
            HashAlgorithm hashAlgorithm;

            switch (hashKind)
            {
                case HashKind.MD5:
                    hashAlgorithm = new MD5CryptoServiceProvider();
                    break;
                case HashKind.SHA256:
                    hashAlgorithm = new SHA256Managed();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(hashKind), hashKind, null);
            }

            byte[] hash = hashAlgorithm.ComputeHash(DefaultEncoding.GetBytes(value));
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
    }
}