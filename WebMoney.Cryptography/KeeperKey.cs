using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Xml;
using WebMoney.Cryptography.Internal;

namespace WebMoney.Cryptography
{

#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    public class KeeperKey : IDisposable
    {
        private const int ByteSize = 8;
        public const int StandardKeySize = 528;

        private const string Template = "<RSAKeyValue><Modulus>{0}</Modulus><D>{1}</D></RSAKeyValue>";
        
        private byte[] _d;
        private byte[] _modulus;
        private int _keySize;

        private bool _isDisposed;

        [CLSCompliant(false)]
        public byte[] D
        {
            get
            {
                if (null == _d)
                    throw new InvalidOperationException("KeeperKey must be initialized before it can be used.");

                return _d;
            }
        }

        [CLSCompliant(false)]
        public byte[] Modulus
        {
            get
            {
                if (null == _modulus)
                    throw new InvalidOperationException("KeeperKey must be initialized before it can be used.");

                return _modulus;
            }
        }

        public int KeySize
        {
            get
            {
                if (null == _modulus)
                    throw new InvalidOperationException("KeeperKey must be initialized before it can be used.");

                if (0 == _keySize)
                {
                    var keySize = BigInteger.GetSignificanceLength(_modulus)*ByteSize;
                    _keySize = keySize;
                }

                return _keySize;
            }
        }

        protected KeeperKey()
        {
        }

        public KeeperKey(byte[] modulus, byte[] d)
        {
            Initialize(modulus, d);
        }

        public KeeperKey(string value)
        {
            if (null == value)
                throw new ArgumentNullException(nameof(value));

            if (string.IsNullOrEmpty(value))
                throw new ArgumentOutOfRangeException(nameof(value));

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(value);

            var topElement = xmlDocument.DocumentElement;

            if (null == topElement)
                throw new CryptographicException("The format of the xmlString parameter is not valid.");

            var modulusNode = topElement.SelectSingleNode("Modulus");
            var dNode = topElement.SelectSingleNode("D");

            if (string.IsNullOrEmpty(modulusNode?.InnerText))
                throw new CryptographicException("Input string does not contain a valid encoding of the 'RSA' 'Modulus' parameter.");
            if (string.IsNullOrEmpty(dNode?.InnerText))
                throw new CryptographicException("Input string does not contain a valid encoding of the 'RSA' 'D' parameter.");

            var modulus = Convert.FromBase64String(modulusNode.InnerText);
            var d = Convert.FromBase64String(dNode.InnerText);

            Initialize(modulus, d);
        }

        protected void Initialize(byte[] modulus, byte[] d)
        {
            if (null == modulus)
                throw new ArgumentNullException(nameof(modulus));

            if (null == d)
                throw new ArgumentNullException(nameof(d));

            if (0 == modulus.Length)
                throw new ArgumentOutOfRangeException(nameof(modulus));

            if (0 == d.Length)
                throw new ArgumentOutOfRangeException(nameof(d));

            var modulusLength = BigInteger.GetSignificanceLength(modulus);
            var dLength = BigInteger.GetSignificanceLength(d);

            //if (0 == dLength || modulusLength < (StandardKeySize / ByteSize))
            //    throw new CryptographicException("Invalid key length.");

            if (0 == dLength || 0 == modulusLength)
                throw new CryptographicException("Invalid key length.");

            if (0 == modulus[0] % 2)
                throw new CryptographicException("Invalid key.");

            modulus = BigInteger.TrimLeadingZeros(modulus);

            if (modulus.Length < StandardKeySize/ByteSize)
                ArrayUtility.Resize(ref modulus, StandardKeySize/ByteSize);

            d = BigInteger.TrimLeadingZeros(d);

            if (d.Length < StandardKeySize / ByteSize)
                ArrayUtility.Resize(ref d, StandardKeySize / ByteSize);

            _modulus = modulus;
            _d = d;
        }

        public virtual string ToXmlString()
        {
            if (null == _modulus || null == _d)
                throw new InvalidOperationException("KeeperKey must be initialized before it can be used.");
            
            var modulusString = Convert.ToBase64String(_modulus);
            var dString = Convert.ToBase64String(_d);

            return string.Format(CultureInfo.InvariantCulture, Template, modulusString, dString);
        }
        
        #region IDisposable Members

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;


            if (disposing)
            {
                if (null != _modulus)
                {
                    Array.Clear(_modulus, 0, _modulus.Length);
                    _modulus = null;
                }

                if (null != _d)
                {
                    Array.Clear(_d, 0, _d.Length);
                    _d = null;
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