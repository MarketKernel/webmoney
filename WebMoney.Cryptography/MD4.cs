using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace WebMoney.Cryptography
{

#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [ComVisible(true)]
    public abstract class MD4 : HashAlgorithm
    {
        protected MD4()
        {
            HashSizeValue = 128;
        }

        [ComVisible(false)]
        public static new MD4 Create()
        {
            return Create("WebMoney.Cryptography.MD4");
        }

        [ComVisible(false)]
        public static new MD4 Create(string hashName)
        {
            return (MD4)CryptoConfig.CreateFromName(hashName);
        }
    }
}