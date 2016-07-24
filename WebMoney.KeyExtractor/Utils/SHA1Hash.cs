using System;
using System.Security.Cryptography;
using System.Text;

namespace WebMoney.KeyExtractor.Utils
{
    internal static class SHA1Hash
    {
        private static readonly Encoding Encoding = Encoding.GetEncoding("windows-1251");

        public static string GetHashString(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            var baValue = Encoding.GetBytes(s);

            var cryptoServiceProvider = new SHA1CryptoServiceProvider();
            var baHash = cryptoServiceProvider.ComputeHash(baValue);

            return BitConverter.ToString(baHash, 0).Replace("-", string.Empty);
        }
    }
}