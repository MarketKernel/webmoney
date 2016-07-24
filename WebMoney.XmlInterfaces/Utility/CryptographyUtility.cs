using System.Security.Cryptography;
using System.Text;
using System;

namespace WebMoney.XmlInterfaces.Utility
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    public static class CryptographyUtility
    {
        private static readonly Encoding DefaultEncoding = Encoding.GetEncoding("windows-1251");

        public static string ComputeMD5Hash(string value)
        {
            using(var cryptoServiceProvider = new MD5CryptoServiceProvider())
            {
                byte[] hash = cryptoServiceProvider.ComputeHash(DefaultEncoding.GetBytes(value));
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            }
        }
    }
}