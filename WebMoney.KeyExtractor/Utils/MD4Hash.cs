using System;
using System.Text;
using WebMoney.Cryptography;

namespace WebMoney.KeyExtractor.Utils
{
    internal static class MD4Hash
    {
        private static readonly Encoding Encoding = Encoding.GetEncoding("windows-1251");

        public static string GetHashString(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            var baValue = Encoding.GetBytes(s);

            var md4 = new MD4Managed();
            var baHash = md4.ComputeHash(baValue);

            return BitConverter.ToString(baHash, 0).Replace("-", string.Empty);
        }
    }
}