using System;
using System.Security.Cryptography;
using System.Text;

namespace WebMoney.KeyExtractor.Utils
{
    internal static class MD5Hash
    {
        private static readonly Encoding Encoding = Encoding.GetEncoding("windows-1251");

        public static string GetHashString(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            byte[] baValue = Encoding.GetBytes(s);

            var cryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] baHash = cryptoServiceProvider.ComputeHash(baValue);

            return BitConverter.ToString(baHash, 0).Replace("-", string.Empty);
        }
    }
}