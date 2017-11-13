using System;

namespace WebMoney.Cryptography.Internal
{
    internal sealed class BigInteger
    {
        private const int Int32Size = 32;
        private const int Int16Size = 16;
        private const int ByteSize = 8;

        private readonly byte[] _value;

        public BigInteger(byte[] value)
        {
            if (null == value)
                throw new ArgumentNullException(nameof(value));

            _value = TrimLeadingZeros(value);
        }

        public BigInteger(uint[] value)
        {
            if (null == value)
                throw new ArgumentNullException(nameof(value));

            byte[] byteArray = new byte[value.Length*Int32Size/ByteSize];
            Buffer.BlockCopy(value, 0, byteArray, 0, byteArray.Length);

            _value = TrimLeadingZeros(byteArray);
        }

        public BigInteger PerformMontgomeryExponentiation(BigInteger e, BigInteger m)
        {
            if (null == e)
                throw new ArgumentNullException(nameof(e));

            if (null == m)
                throw new ArgumentNullException(nameof(m));

            var exponentiationResult = Montgomery.PerformMontgomeryExponentiation(ToUInt32Array(), e.ToUInt32Array(),
                m.ToUInt32Array());
            
            return new BigInteger(exponentiationResult);
        }

        public byte[] ToByteArray()
        {
            return _value;
        }

        public ushort[] ToUInt16Array()
        {
            var result = new ushort[(_value.Length + (Int16Size / ByteSize) - 1) / (Int16Size / ByteSize)];
            Buffer.BlockCopy(_value, 0, result, 0, _value.Length);

            return result;
        }

        public uint[] ToUInt32Array()
        {
            var result = new uint[(_value.Length + (Int32Size / ByteSize) - 1) / (Int32Size / ByteSize)];
            Buffer.BlockCopy(_value, 0, result, 0, _value.Length);

            return result;
        }

        public static byte[] TrimLeadingZeros(byte[] value)
        {
            if (null == value)
                throw new ArgumentNullException(nameof(value));

            var length = GetSignificanceLength(value);

            if (value.Length != length)
                ArrayUtility.Resize(ref value, length);

            return value;
        }

        public static int GetSignificanceLength(byte[] value)
        {
            if (null == value)
                throw new ArgumentNullException(nameof(value));

            var length = value.Length;

            while (length > 0 && value[length - 1] == 0)
                length--;

            return length;
        }
    }
}