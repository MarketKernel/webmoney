using System;

namespace WebMoney.Cryptography.Internal
{
    internal static class Montgomery
    {
        private const int Int32Size = 32;

        private const uint BitMask = 0x80000000; // битовая маска

        // [1] 14.94 Algorithm Montgomery exponentiation
        // INPUT:
        //      m = (m[l-1] ... m[0]){b},
        //      R = b^l,
        //      mQ = m^-1 mod b,
        //      e = (e[t] ... e[0]){2}
        //           with e[t] = 1,
        //           and an integer x, 1 <= x < m.
        // OUTPUT: x^e mod m.
        public static uint[] PerformMontgomeryExponentiation(uint[] x, uint[] e, uint[] m)
        {
            if (null == x)
                throw new ArgumentNullException(nameof(x));

            if (null == e)
                throw new ArgumentNullException(nameof(e));

            if (null == m)
                throw new ArgumentNullException(nameof(m));

            // 1 <= x
            if (Compare(new uint[] { 1 }, x) > 0)
                throw new ArgumentOutOfRangeException(nameof(x));

            // e[t] = 1
            if (Compare(new uint[] { 1 }, e) > 0)
                throw new ArgumentOutOfRangeException(nameof(e));

            // x < m
            if (Compare(x, m) >= 0)
                throw new ArgumentOutOfRangeException(nameof(m));

            // mQ = m^-1 mod b
            if (0 == m[0] % 2)
                throw new ArgumentOutOfRangeException(nameof(m));

            var mQ = Inverse(m[0]);

            var eLength = GetSignificanceLength(e);
            var mLength = GetSignificanceLength(m);

            // Resize
            if (mLength > x.Length)
                ArrayUtility.Resize(ref x, mLength);

            // 1. temp = Mont(x, R^2 mod m), A = R mod m.
            var r2 = new uint[m.Length * 2 + 1];
            r2[r2.Length - 1] = 1;

            Remainder(r2, m);

            var temp = PerformMontgomeryMultiplication(x, r2, m, mQ);

            var a = new uint[m.Length + 1];
            a[a.Length - 1] = 1;

            Remainder(a, m);

            var pos = eLength - 1; // позиция
            var mask = BitMask; // битовая маска

            // Узнаем количество значащих битов степени
            while (0 == (e[pos] & mask))
            {
                mask >>= 1;
            }

            // 2. For i from t down to 0 do the following:
            while (pos >= 0)
            {
                // 2.1 A = Mont(A, A).
                a = PerformMontgomeryMultiplication(a, a, m, mQ);

                // 2.2 If e[i] = 1 then A = Mont(A, temp).
                if (0 != (e[pos] & mask)) // если бит равен 1
                    a = PerformMontgomeryMultiplication(a, temp, m, mQ);

                mask >>= 1;

                if (0 == mask)
                {
                    mask = BitMask;
                    pos--;
                }
            }

            // 3. A Mont(A, 1).
            var one = new uint[m.Length];
            one[0] = 1;

            a = PerformMontgomeryMultiplication(a, one, m, mQ);

            // 4. Return (A).
            return TrimLeadingZeros(a);
        }

        // [1] 14.36 Algorithm Montgomery multiplication
        // INPUT: integers
        //      m = (m[n-1] ... m[1] m[0]){b},
        //      x = (x[n-1] ... x[1] x[0]){b},
        //      y = (y[n-1] ... y[1] y[0]){b}
        //           with 0 <= x, y < m,
        //           R = b^n with gcd(m, b) = 1,
        //           and mQ = m^-1 mod b.
        // OUTPUT: x * y * R^-1 mod m.
        private static uint[] PerformMontgomeryMultiplication(uint[] x, uint[] y, uint[] m, uint mQ)
        {
            var n = GetSignificanceLength(m);

            if (0 == n)
                throw new ArgumentOutOfRangeException(nameof(m), "Attempted to divide by zero.");

            if (x.Length < n)
                ArrayUtility.Resize(ref x, n);

            if (y.Length < n)
                ArrayUtility.Resize(ref y, n);

            // 1. A = 0. (Notation: A = (a[n] a[n-1] ... a[1] a[0]){b})
            var a = new uint[n + 1];

            // 2. For i from 0 to (n - 1) do the following:
            for (var i = 0; i < n; i++)
            {
                // 2.1 u_i = (a[0] + x[i] * y[0]) * mQ mod b.
                var u = (uint)((a[0] + (ulong)x[i] * y[0]) * mQ);

                // 2.2 A = (A + x[i] * y + u_i * m) / b.
                var xy = (ulong)x[i] * y[0];
                var um = (ulong)u * m[0];

                var temp = (ulong)a[0] + ((uint)xy) + (uint)um;
                var carry = (xy >> Int32Size) + (um >> Int32Size) + (temp >> Int32Size);

                for (var pos = 1; pos < n; pos++)
                {
                    xy = (ulong)x[i] * y[pos];
                    um = (ulong)u * m[pos];

                    temp = (ulong)a[pos] + ((uint)xy) + (uint)um + (uint)carry;
                    carry = (xy >> Int32Size) + (um >> Int32Size) + (temp >> Int32Size) + (carry >> Int32Size);

                    a[pos - 1] = (uint)temp;
                }

                carry += a[n];

                a[n - 1] = (uint)carry;
                a[n] = (uint)(carry >> Int32Size);
            }

            // 3. If A >= m then A = A - m
            if (Compare(a, m) >= 0)
                Sub(a, m);

            // 4. Return (A).
            return a;
        }

        private static uint Inverse(uint value)
        {
            var temp = (((value + 2) & 4) << 1) + value;

            temp *= 2 - value * temp;
            temp *= 2 - value * temp;
            temp *= 2 - value * temp;

            return (uint)-temp;
        }

        private static void Remainder(uint[] lhs, uint[] rhs)
        {
            var rhsBitsCount = GetBitsCount(rhs);

            if (0 == rhsBitsCount)
                throw new ArgumentOutOfRangeException(nameof(rhs));

            while (Compare(lhs, rhs) >= 0)
            {
                var lhsBitsCount = GetBitsCount(lhs);

                if (0 == lhsBitsCount)
                    break;

                var shift = lhsBitsCount - rhsBitsCount;

                var temp = Shift(rhs, shift);

                if (Compare(lhs, temp) < 0)
                    ShiftRight(temp);

                while (Compare(lhs, temp) >= 0)
                    Sub(lhs, temp);
            }
        }

        private static void Sub(uint[] lhs, uint[] rhs)
        {
            var lhsLength = GetSignificanceLength(lhs);
            var rhsLength = GetSignificanceLength(rhs);

            if (lhsLength < rhsLength)
                throw new ArgumentOutOfRangeException(nameof(rhs));

            var pos = 0;
            ulong borrow = 0;

            for (; pos < rhsLength; pos++)
            {
                var temp = (ulong)lhs[pos] - rhs[pos] - borrow;
                lhs[pos] = unchecked((uint)temp);
                borrow = (temp >> Int32Size) & 1;
            }

            if (rhsLength < lhsLength)
                for (; pos < lhsLength; pos++)
                {
                    var temp = lhs[pos] - borrow;
                    lhs[pos] = unchecked((uint)temp);
                    borrow = (temp >> Int32Size) & 1;
                }

            if (1 == borrow)
                throw new ArgumentOutOfRangeException(nameof(rhs));
        }

        private static uint[] Shift(uint[] lhs, int rhs)
        {
            var shiftBits = (rhs > 0 ? rhs : -rhs) % Int32Size;
            var shiftWords = (rhs > 0 ? rhs : -rhs) / Int32Size;

            var inBitsCount = GetBitsCount(lhs);
            var inWordsCount = inBitsCount / Int32Size + (inBitsCount % Int32Size > 0 ? 1 : 0);

            var outBitsCount = inBitsCount + rhs;
            var outWordsCount = outBitsCount / Int32Size + (outBitsCount % Int32Size > 0 ? 1 : 0);

            if (outWordsCount <= 0)
                return new uint[] { 0 };

            var result = new uint[inWordsCount > outWordsCount ? inWordsCount : outWordsCount];

            if (rhs > 0)
            {
                if (0 == shiftBits)
                    Array.Copy(lhs, 0, result, shiftWords, outWordsCount - shiftWords);
                else
                {
                    var pos = 0;
                    uint carry = 0;

                    for (; pos < inWordsCount; pos++)
                    {
                        var temp = lhs[pos];

                        result[pos + shiftWords] = (temp << shiftBits) | carry;
                        carry = temp >> (Int32Size - shiftBits);
                    }

                    if (pos + shiftWords < outWordsCount)
                        result[pos + shiftWords] |= carry;
                }
            }
            else
            {
                if (0 == shiftBits)
                    Array.Copy(lhs, shiftWords, result, 0, outWordsCount);
                else
                {
                    uint carry = 0;
                    var pos = outWordsCount;

                    if (pos + shiftWords < inWordsCount)
                        carry = lhs[pos + shiftWords] << (Int32Size - shiftBits);

                    pos--;

                    for (; pos >= 0; pos--)
                    {
                        var temp = lhs[pos + shiftWords];

                        result[pos] = (temp >> shiftBits) | carry;
                        carry = temp << (Int32Size - shiftBits);
                    }
                }
            }

            return result;
        }

        private static void ShiftRight(uint[] value)
        {
            var len = GetSignificanceLength(value);

            uint carry = 0;

            for (var pos = len - 1; pos >= 0; pos--)
            {
                var temp = value[pos];

                var nextCarry = (temp & 1) << (Int32Size - 1);
                value[pos] = (temp >> 1) | carry;
                carry = nextCarry;
            }
        }

        private static int GetBitsCount(uint[] value)
        {
            if (0 == value.Length)
                return 0;

            var length = GetSignificanceLength(value);

            if (0 == length)
                return 0;

            var bits = 0;
            var temp = value[length - 1];

            while (temp > 0)
            {
                bits++;
                temp = temp >> 1;
            }

            return (length - 1) * Int32Size + bits;
        }

        private static uint[] TrimLeadingZeros(uint[] value)
        {
            var length = GetSignificanceLength(value);

            if (value.Length != length)
                ArrayUtility.Resize(ref value, length);

            return value;
        }

        private static int GetSignificanceLength(uint[] value)
        {
            var length = value.Length;

            while (length > 0 && value[length - 1] == 0)
                length--;

            return length;
        }

        private static int Compare(uint[] lhs, uint[] rhs)
        {
            var lhsLen = GetSignificanceLength(lhs);
            var rhsLen = GetSignificanceLength(rhs);

            if (lhsLen > rhsLen)
                return 1;
            if (lhsLen < rhsLen)
                return -1;

            for (var pos = lhsLen - 1; pos >= 0; pos--)
            {
                if (lhs[pos] > rhs[pos])
                    return 1;

                if (lhs[pos] < rhs[pos])
                    return -1;
            }

            return 0;
        }
    }
}
