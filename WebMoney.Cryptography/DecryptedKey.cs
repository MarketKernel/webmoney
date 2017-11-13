using System;
using System.Globalization;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using WebMoney.Cryptography.Internal;

namespace WebMoney.Cryptography
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    public class DecryptedKey : KeeperKey
    {
        private const int ByteSize = 8;
        private const int Int32Size = 32;
        private const int HashSize = 128;

        protected const int MagicNumberPosition = 0;
        protected const int MagicNumberSize = 2;
        protected const int MarkPosition = 2;
        protected const int MarkSize = 2;
        protected const int ProofPosition = 4;
        protected const int ProofSize = 16;
        protected const int BodySizePosition = 20;
        protected const int BodySizeSize = 4;
        protected const int HeaderSize =
            MagicNumberSize + MarkSize + ProofSize + BodySizeSize; // 24

        // Body
        protected const int ReservedPosition = 24;
        protected const int ReservedSize = 4;
        protected const int DSizePosition = 28;
        protected const int DSizeSize = 2;
        protected const int DPosition = 30;
        protected const int ModulusSizeSize = 2;

        public const int IdLength = 12;
        public const long MaximumId = 999999999999;
        public const string IdFormat = "000000000000";

        public const int MinPasswordLength = 1;
        public const int MaxPasswordLength = 50;

        protected static readonly byte[] MagicNumber = { 0x81, 0x00 };
        protected static readonly byte[] Mark = { 0x01, 0x00 };

        protected static readonly Encoding DefaultEncoding = Encoding.GetEncoding(1251);
        
        protected DecryptedKey()
        {
        }

        public DecryptedKey(byte[] modulus, byte[] d)
            : base(modulus, d)
        {
        }

        public DecryptedKey(string value)
            : base(value)
        {
        }

        public DecryptedKey(byte[] encrypted, byte[] id, byte[] password)
            : this()
        {
            if (null == encrypted)
                throw new ArgumentNullException(nameof(encrypted));

            if (null == id)
                throw new ArgumentNullException(nameof(id));

            if (null == password)
                throw new ArgumentNullException(nameof(password));

            if (0 == encrypted.Length)
                throw new ArgumentOutOfRangeException(nameof(encrypted));

            if (IdLength != id.Length)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (password.Length < MinPasswordLength || password.Length > MaxPasswordLength)
                throw new ArgumentOutOfRangeException(nameof(password));

            for (var pos = 0; pos < MagicNumber.Length; pos++)
                if (encrypted[pos] != MagicNumber[pos])
                    throw new CryptographicException("Bad magic number.");

            var decrypted = Transform(encrypted, id, password, true);

            Array.Clear(decrypted, MarkPosition, MarkSize);

            var valid = Check(decrypted);

            if (!valid)
            {
                decrypted = Transform(encrypted, id, password, false);

                Array.Clear(decrypted, MarkPosition, MarkSize);

                valid = Check(decrypted);

                if (!valid)
                    throw new CryptographicException("Wrong WMID or password.");
            }

            Initialize(decrypted);
        }

        protected void Initialize(byte[] decrypted)
        {
            if (null == decrypted)
                throw new ArgumentNullException(nameof(decrypted));

            if (0 == decrypted.Length)
                throw new ArgumentOutOfRangeException(nameof(decrypted));

            if (decrypted.Length < DSizePosition + DSizeSize)
                throw new CryptographicException("Size of the 'RSA' 'D' parameter is missing.");

            // D
            var pos = DSizePosition;
            var dLength = BitConverter.ToUInt16(decrypted, pos);

            if (decrypted.Length < DPosition + dLength)
                throw new CryptographicException("Incorrect length of the 'RSA' 'D' parameter.");

            var d = new byte[dLength];
            Array.Copy(decrypted, DPosition, d, 0, dLength);

            // Modulus
            pos += DSizeSize;
            pos += dLength;

            if (decrypted.Length < pos + ModulusSizeSize)
                throw new CryptographicException("Size of the 'RSA' 'Modulus' parameter is missing.");

            var modulusLength = BitConverter.ToUInt16(decrypted, pos);

            pos += ModulusSizeSize;

            if (decrypted.Length < pos + modulusLength)
                throw new CryptographicException("Incorrect length of the 'RSA' 'Modulus' parameter.");

            var modulus = new byte[modulusLength];
            Array.Copy(decrypted, pos, modulus, 0, modulusLength);

            Initialize(modulus, d);
        }

        #region Decrypt

        [SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
        public static DecryptedKey Decrypt(byte[] encrypted, long id, SecureString securePassword)
        {
            if (null == securePassword)
                throw new ArgumentNullException(nameof(securePassword));

            if (0 == securePassword.Length)
                throw new ArgumentOutOfRangeException(nameof(securePassword));

            byte[] password = null;

            try
            {
                password = SecureStringUtility.SecureStringToByteArray(securePassword, DefaultEncoding);

                return Decrypt(encrypted, id, password);
            }
            finally
            {
                if (null != password)
                    Array.Clear(password, 0, password.Length);
            }
        }

        public static DecryptedKey Decrypt(byte[] encrypted, long id, byte[] password)
        {
            byte[] idBytes = IdToBytes(id);
            return new DecryptedKey(encrypted, idBytes, password);
        }

        #endregion

        #region Encrypt
        
        [SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
        public byte[] Encrypt(long id, SecureString securePassword)
        {
            return Encrypt(id, securePassword, false);
        }

        public byte[] Encrypt(long id, byte[] password)
        {
            return Encrypt(id, password, false);
        }

        public byte[] Encrypt(byte[] id, byte[] password)
        {
            return Encrypt(id, password, false);
        }
        
        [SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
        public byte[] Encrypt(long id, SecureString securePassword, bool half)
        {
            if (id < 0 || id > MaximumId)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (null == securePassword)
                throw new ArgumentNullException(nameof(securePassword));

            if (0 == securePassword.Length)
                throw new ArgumentOutOfRangeException(nameof(securePassword));

            byte[] password = null;

            try
            {
                password = SecureStringUtility.SecureStringToByteArray(securePassword, DefaultEncoding);

                return Encrypt(id, password, half);
            }
            finally
            {
                if (null != password)
                    Array.Clear(password, 0, password.Length);
            }
        }

        public byte[] Encrypt(long id, byte[] password, bool half)
        {
            var idBytes = IdToBytes(id);
            return Encrypt(idBytes, password, half);
        }

        public byte[] Encrypt(byte[] id, byte[] password, bool half)
        {
            if (null == id)
                throw new ArgumentNullException(nameof(id));

            if (IdLength != id.Length)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (null == password)
                throw new ArgumentNullException(nameof(password));

            if (password.Length < MinPasswordLength || password.Length > MaxPasswordLength)
                throw new ArgumentOutOfRangeException(nameof(password));

            var modulus = new byte[Modulus.Length];
            var d = new byte[D.Length];

            Array.Copy(Modulus, modulus, Modulus.Length);
            Array.Copy(D, d, D.Length);

            if (modulus.Length < KeySize / ByteSize)
                ArrayUtility.Resize(ref modulus, KeySize / ByteSize);

            if (d.Length < KeySize / ByteSize)
                ArrayUtility.Resize(ref d, KeySize / ByteSize);

            var bodyLength = ReservedSize + DSizeSize + KeySize / ByteSize + ModulusSizeSize + KeySize / ByteSize;
            var decrypted = new byte[HeaderSize + bodyLength];

            Array.Copy(MagicNumber, 0, decrypted, MagicNumberPosition, MagicNumberSize);
            Array.Copy(BitConverter.GetBytes(bodyLength), 0, decrypted, BodySizePosition, BodySizeSize);
            Array.Copy(BitConverter.GetBytes(KeySize / ByteSize), 0, decrypted, DSizePosition, DSizeSize);
            Array.Copy(d, 0, decrypted, DPosition, KeySize / ByteSize);
            Array.Copy(BitConverter.GetBytes(KeySize / ByteSize), 0, decrypted, DSizePosition + DSizeSize + KeySize / ByteSize,
                             ModulusSizeSize);
            Array.Copy(modulus, 0, decrypted, DSizePosition + DSizeSize + KeySize / ByteSize + ModulusSizeSize, KeySize / ByteSize);

            var proof = ComputeUInt32Hash(decrypted, 0, decrypted.Length);
            var encrypted = Transform(decrypted, id, password, half);

            Buffer.BlockCopy(proof, 0, encrypted, ProofPosition, ProofSize);
            Array.Copy(Mark, 0, encrypted, MarkPosition, MarkSize);

            return encrypted;
        }

        #endregion

        public static byte[] IdToBytes(long id)
        {
            if (id < 0 || id > MaximumId)
                throw new ArgumentOutOfRangeException(nameof(id));

            var idString = id.ToString(IdFormat, CultureInfo.InvariantCulture);
            var idBytes = DefaultEncoding.GetBytes(idString);

            return idBytes;
        }

        protected byte[] Transform(byte[] input, byte[] id, byte[] password, bool half)
        {
            if (null == input)
                throw new ArgumentNullException(nameof(input));

            if (null == id)
                throw new ArgumentNullException(nameof(id));

            if (null == password)
                throw new ArgumentNullException(nameof(password));

            if (0 == input.Length)
                throw new ArgumentOutOfRangeException(nameof(input));

            if (IdLength != id.Length)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (0 == password.Length)
                throw new ArgumentOutOfRangeException(nameof(password));

            var passwordLength = half ? password.Length / 2 : password.Length;
            var combination = new byte[id.Length + passwordLength];

            Array.Copy(id, 0, combination, 0, id.Length);
            Array.Copy(password, 0, combination, id.Length, passwordLength);

            try
            {
                return Transform(input, combination);
            }
            finally
            {
                Array.Clear(combination, 0, combination.Length);
            }
        }

        protected byte[] Transform(byte[] input, byte[] combination)
        {
            if (null == input)
                throw new ArgumentNullException(nameof(input));

            if (null == combination)
                throw new ArgumentNullException(nameof(combination));

            if (0 == input.Length)
                throw new ArgumentOutOfRangeException(nameof(input));

            if (0 == combination.Length)
                throw new ArgumentOutOfRangeException(nameof(combination));

            if (input.Length <= DPosition)
                throw new CryptographicException("The 'RSA' 'D' parameter is missing.");

            byte[] hash = null;

            try
            {
                hash = ComputeHash(combination, 0, combination.Length);

                var output = new byte[input.Length];
                Array.Copy(input, 0, output, 0, DPosition);

                for (var pos = DPosition; pos < input.Length; pos++)
                    output[pos] = (byte)(input[pos] ^ hash[(pos - DPosition) % (HashSize / ByteSize)]);

                return output;
            }
            finally
            {
                if (null != hash)
                    Array.Clear(hash, 0, hash.Length);
            }
        }

        protected bool Check(byte[] decrypted)
        {
            if (null == decrypted)
                throw new ArgumentNullException(nameof(decrypted));

            if (0 == decrypted.Length)
                throw new ArgumentOutOfRangeException(nameof(decrypted));

            if (decrypted.Length < BodySizePosition + BodySizeSize)
                throw new CryptographicException("Size of the body is missing.");

            var bodyLength = BitConverter.ToInt32(decrypted, BodySizePosition);

            if (decrypted.Length - HeaderSize < bodyLength)
                throw new CryptographicException("Incorrect length of the body.");

            var proof = new uint[ProofSize / (Int32Size / ByteSize)];
            Buffer.BlockCopy(decrypted, ProofPosition, proof, 0, ProofSize);

            Array.Clear(decrypted, ProofPosition, ProofSize);

            var hash = ComputeUInt32Hash(decrypted, 0, HeaderSize + bodyLength);

            for (int pos = 0; pos < HashSize / Int32Size; pos++)
                if (proof[pos] != hash[pos])
                    return false;

            return true;
        }

        [CLSCompliant(false)]
        protected uint[] ComputeUInt32Hash(byte[] value, int offset, int count)
        {
            if (null == value)
                throw new ArgumentNullException(nameof(value));

            if (offset < 0 || offset > value.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || offset + count > value.Length)
                throw new ArgumentOutOfRangeException(nameof(count));

            var uHash = new uint[(HashSize / Int32Size)];

            var bHash = ComputeHash(value, offset, count);
            Buffer.BlockCopy(bHash, 0, uHash, 0, (HashSize / ByteSize));

            return uHash;
        }

        protected byte[] ComputeHash(byte[] value, int offset, int count)
        {
            if (null == value)
                throw new ArgumentNullException(nameof(value));

            if (offset < 0 || offset > value.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0 || offset + count > value.Length)
                throw new ArgumentOutOfRangeException(nameof(count));

            byte[] hash;

            using (MD4 md4 = new MD4Managed())
            {
                md4.Initialize();
                hash = md4.ComputeHash(value, offset, count);
            }

            return hash;
        }
    }
}