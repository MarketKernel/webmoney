using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using WebMoney.Cryptography.Internal;

namespace WebMoney.Cryptography
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [ComVisible(true)]
    public class Signer : ISigner, IDisposable
    {
        private const int ByteSize = 8;
        private const int Int32Size = 32;
        private const int Int16Size = 16;
        private const int HashSize = 128;

        private static readonly byte[] Header = { 0x38, 0 };
        private static readonly byte[] Tail = { 1, 0 };

        private static readonly Encoding MessageEncoding = Encoding.GetEncoding(1251);
        
        private KeeperKey _key;

        private bool _isDisposed;

        public void Initialize(string value)
        {
            var keeperKey = new KeeperKey(value);
            Initialize(keeperKey);
        }

        [ComVisible(false)]
        public void Initialize(KeeperKey keeperKey)
        {
            if (null == keeperKey)
                throw new ArgumentNullException(nameof(keeperKey));
            
            _key = keeperKey;
        }

        public string Sign(string value)
        {
            return Sign(value, true);
        }

        [ComVisible(false)]
        public string Sign(string value, bool randomEnable)
        {
            if (null == value)
                throw new ArgumentNullException(nameof(value));

            if (value.IndexOf('\r') >= 0)
                throw new ArgumentException("Message cannot contain of the following character: '\r'.");

            if (null == _key)
                throw new InvalidOperationException("Signer must be initialized before it can be used.");

            var hash = ComputeHash(value, 0, value.Length);
            var random = new byte[_key.KeySize / ByteSize - Header.Length - HashSize / ByteSize - Tail.Length];

            if (randomEnable)
            {
                var rng = new RNGCryptoServiceProvider();
                rng.GetBytes(random);
            }

            var blob = new uint[(_key.KeySize / ByteSize + Int32Size / ByteSize - 1) / (Int32Size / ByteSize)];

            Buffer.BlockCopy(Header, 0, blob, 0, Header.Length);
            Buffer.BlockCopy(hash, 0, blob, Header.Length, HashSize / ByteSize);
            Buffer.BlockCopy(random, 0, blob, Header.Length + HashSize / ByteSize, random.Length);
            Buffer.BlockCopy(Tail, 0, blob, Header.Length + HashSize / ByteSize + random.Length, Tail.Length);

            var signature =
                new BigInteger(blob).PerformMontgomeryExponentiation(new BigInteger(_key.D),
                    new BigInteger(_key.Modulus)).ToUInt16Array();

            var stringBuilder = new StringBuilder();

            var keySize = _key.KeySize > KeeperKey.StandardKeySize ? _key.KeySize : KeeperKey.StandardKeySize;

            for (int pos = 0; pos < keySize / Int16Size; pos++)
            {
                stringBuilder.Append(signature.Length > pos
                                         ? string.Format(CultureInfo.InvariantCulture, "{0:x4}", signature[pos])
                                         : string.Format(CultureInfo.InvariantCulture, "{0:x4}", 0));
            }

            return stringBuilder.ToString();
        }

        private byte[] ComputeHash(string value, int offset, int count)
        {
            byte[] hash;

            using (MD4 md4 = new MD4Managed())
            {
                md4.Initialize();
                hash = md4.ComputeHash(MessageEncoding.GetBytes(value), offset, count);
            }

            return hash;
        }
        
        #region IDisposable Members

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;
            
            if (disposing)
            {
                if (null != _key)
                {
                    _key.Dispose();
                    _key = null;
                }
            }

            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}