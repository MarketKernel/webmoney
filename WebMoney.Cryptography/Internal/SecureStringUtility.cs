using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace WebMoney.Cryptography.Internal
{
    internal static class SecureStringUtility
    {
        [SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
        public static byte[] SecureStringToByteArray(SecureString securePassword, Encoding encoding = null)
        {
            if (null == securePassword)
                throw new ArgumentNullException(nameof(securePassword));

            if (0 == securePassword.Length)
                throw new ArgumentOutOfRangeException(nameof(securePassword));

            if (null == encoding)
                encoding = Encoding.UTF8;

            var memory = IntPtr.Zero;
            byte[] password = null;

            try
            {
                password = new byte[securePassword.Length * 2];

                memory = Marshal.SecureStringToCoTaskMemUnicode(securePassword);
                Marshal.Copy(memory, password, 0, password.Length);

                return Encoding.Convert(Encoding.Unicode, encoding, password);
            }
            finally
            {
                Marshal.ZeroFreeCoTaskMemUnicode(memory);

                if (null != password)
                    Array.Clear(password, 0, password.Length);
            }
        }
    }
}
