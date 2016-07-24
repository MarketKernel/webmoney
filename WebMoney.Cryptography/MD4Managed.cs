namespace WebMoney.Cryptography
{
    using System;
    using System.Runtime.InteropServices;

#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [ComVisible(true)]
    public sealed class MD4Managed : MD4
    {
        private const int ByteSize = 8;
        private const int Int32Size = 32;

        // Block
        private const int BlockSize = 512;

        // State
        private const int StateSize = 128;

        // Count
        private const int CountSize = 64;

        // Scope
        private const int FinalScopeSize = BlockSize - CountSize; // 448

        private const int EndMask = 0x80;

        private const uint I0 = 0x67452301;
        private const uint I1 = 0xEFCDAB89;
        private const uint I2 = 0x98BADCFE;
        private const uint I3 = 0x10325476;

        private const uint C2 = 0x5A827999;
        private const uint C3 = 0x6ED9EBA1;

        private const int Fs1 = 3;
        private const int Fs2 = 7;
        private const int Fs3 = 11;
        private const int Fs4 = 19;

        private const int Gs1 = 3;
        private const int Gs2 = 5;
        private const int Gs3 = 9;
        private const int Gs4 = 13;

        private const int Hs1 = 3;
        private const int Hs2 = 9;
        private const int Hs3 = 11;
        private const int Hs4 = 15;

        private readonly uint[] _block;
        private readonly uint[] _state;
        private long _count;

        public MD4Managed()
        {
            _block = new uint[BlockSize / Int32Size];
            _state = new uint[StateSize / Int32Size];

            InternalInitialize();
        }

        public override void Initialize()
        {
            InternalInitialize();
        }

        private void InternalInitialize()
        {
            Array.Clear(_block, 0, _block.Length);
            Array.Clear(_state, 0, _state.Length);

            _state[0] = I0;
            _state[1] = I1;
            _state[2] = I2;
            _state[3] = I3;

            _count = 0;
        }

        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            if (null == array)
                throw new ArgumentNullException(nameof(array));

            if (ibStart < 0)
                throw new ArgumentOutOfRangeException(nameof(ibStart));

            if (cbSize < 0)
                throw new ArgumentOutOfRangeException(nameof(cbSize));

            if (array.Length < ibStart)
                throw new ArgumentOutOfRangeException(nameof(ibStart));

            if (array.Length < ibStart + cbSize)
                throw new ArgumentOutOfRangeException(nameof(cbSize));

            InternalHashCore(array, ibStart, cbSize);
        }

        private void InternalHashCore(byte[] array, int ibStart, int cbSize)
        {
            var offset = ibStart;
            var count = cbSize;

            var internalOffset = (int)(_count % (BlockSize / ByteSize));
            _count += count;

            if ((internalOffset > 0) && ((internalOffset + count) >= (BlockSize / ByteSize)))
            {
                Buffer.BlockCopy(array, offset, _block, internalOffset, (BlockSize / ByteSize) - internalOffset);

                offset += (BlockSize / ByteSize) - internalOffset;
                count -= (BlockSize / ByteSize) - internalOffset;

                TransformBlock();
                internalOffset = 0;
            }

            while (count >= (BlockSize / ByteSize))
            {
                Buffer.BlockCopy(array, offset, _block, 0, (BlockSize / ByteSize));

                offset += (BlockSize / ByteSize);
                count -= (BlockSize / ByteSize);

                TransformBlock();
            }

            if (count > 0)
                Buffer.BlockCopy(array, offset, _block, internalOffset, count);
        }

        protected override byte[] HashFinal()
        {
            var internalOffset = (int) (_count%(BlockSize/ByteSize));

            int length;

            if (internalOffset >= FinalScopeSize/ByteSize)
                length = (BlockSize/ByteSize) + (BlockSize/ByteSize) - internalOffset;
            else
                length = (BlockSize/ByteSize) - internalOffset;

            var array = new byte[length];

            array[0] = EndMask;

            Buffer.BlockCopy(BitConverter.GetBytes(_count*ByteSize), 0, array, length - (CountSize/ByteSize),
                CountSize/ByteSize);

            InternalHashCore(array, 0, length);

            var hash = new byte[HashSize/ByteSize];
            Buffer.BlockCopy(_state, 0, hash, 0, HashSize/ByteSize);

            return hash;
        }

        private void TransformBlock()
        {
            var a = _state[0];
            var b = _state[1];
            var c = _state[2];
            var d = _state[3];

            a = Ff(a, b, c, d, _block[0], Fs1);
            d = Ff(d, a, b, c, _block[1], Fs2);
            c = Ff(c, d, a, b, _block[2], Fs3);
            b = Ff(b, c, d, a, _block[3], Fs4);
            a = Ff(a, b, c, d, _block[4], Fs1);
            d = Ff(d, a, b, c, _block[5], Fs2);
            c = Ff(c, d, a, b, _block[6], Fs3);
            b = Ff(b, c, d, a, _block[7], Fs4);
            a = Ff(a, b, c, d, _block[8], Fs1);
            d = Ff(d, a, b, c, _block[9], Fs2);
            c = Ff(c, d, a, b, _block[10], Fs3);
            b = Ff(b, c, d, a, _block[11], Fs4);
            a = Ff(a, b, c, d, _block[12], Fs1);
            d = Ff(d, a, b, c, _block[13], Fs2);
            c = Ff(c, d, a, b, _block[14], Fs3);
            b = Ff(b, c, d, a, _block[15], Fs4);

            a = Gg(a, b, c, d, _block[0], Gs1);
            d = Gg(d, a, b, c, _block[4], Gs2);
            c = Gg(c, d, a, b, _block[8], Gs3);
            b = Gg(b, c, d, a, _block[12], Gs4);
            a = Gg(a, b, c, d, _block[1], Gs1);
            d = Gg(d, a, b, c, _block[5], Gs2);
            c = Gg(c, d, a, b, _block[9], Gs3);
            b = Gg(b, c, d, a, _block[13], Gs4);
            a = Gg(a, b, c, d, _block[2], Gs1);
            d = Gg(d, a, b, c, _block[6], Gs2);
            c = Gg(c, d, a, b, _block[10], Gs3);
            b = Gg(b, c, d, a, _block[14], Gs4);
            a = Gg(a, b, c, d, _block[3], Gs1);
            d = Gg(d, a, b, c, _block[7], Gs2);
            c = Gg(c, d, a, b, _block[11], Gs3);
            b = Gg(b, c, d, a, _block[15], Gs4);

            a = Hh(a, b, c, d, _block[0], Hs1);
            d = Hh(d, a, b, c, _block[8], Hs2);
            c = Hh(c, d, a, b, _block[4], Hs3);
            b = Hh(b, c, d, a, _block[12], Hs4);
            a = Hh(a, b, c, d, _block[2], Hs1);
            d = Hh(d, a, b, c, _block[10], Hs2);
            c = Hh(c, d, a, b, _block[6], Hs3);
            b = Hh(b, c, d, a, _block[14], Hs4);
            a = Hh(a, b, c, d, _block[1], Hs1);
            d = Hh(d, a, b, c, _block[9], Hs2);
            c = Hh(c, d, a, b, _block[5], Hs3);
            b = Hh(b, c, d, a, _block[13], Hs4);
            a = Hh(a, b, c, d, _block[3], Hs1);
            d = Hh(d, a, b, c, _block[11], Hs2);
            c = Hh(c, d, a, b, _block[7], Hs3);
            b = Hh(b, c, d, a, _block[15], Hs4);

            _state[0] += a;
            _state[1] += b;
            _state[2] += c;
            _state[3] += d;
        }

        private static uint Rot(uint t, int s)
        {
            var result = (t << s) | (t >> (Int32Size - s));

            return result;
        }

        private static uint F(uint x, uint y, uint z)
        {
            var t = (x & y) | (~x & z);

            return t;
        }

        private static uint G(uint x, uint y, uint z)
        {
            var t = (x & y) | (x & z) | (y & z);

            return t;
        }

        private static uint H(uint x, uint y, uint z)
        {
            var t = x ^ y ^ z;

            return t;
        }

        private static uint Ff(uint a, uint b, uint c, uint d, uint x, int s)
        {
            var t = a + F(b, c, d) + x;

            return Rot(t, s);
        }

        private static uint Gg(uint a, uint b, uint c, uint d, uint x, int s)
        {
            var t = a + G(b, c, d) + x + C2;

            return Rot(t, s);
        }

        private static uint Hh(uint a, uint b, uint c, uint d, uint x, int s)
        {
            var t = a + H(b, c, d) + x + C3;

            return Rot(t, s);
        }
    }
}