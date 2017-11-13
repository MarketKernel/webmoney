using System;

namespace WebMoney.Cryptography.Internal
{
    internal sealed class ArrayUtility
    {
        public static void Resize<T>(ref T[] array, int newSize)
        {
            if (newSize < 0) throw new ArgumentOutOfRangeException(nameof(newSize));

            var sourceArray = array;

            if (sourceArray == null)
                array = new T[newSize];
            else if (sourceArray.Length != newSize)
            {
                var destinationArray = new T[newSize];
                Array.Copy(sourceArray, 0, destinationArray, 0,
                    (sourceArray.Length > newSize) ? newSize : sourceArray.Length);
                array = destinationArray;
            }
        }
    }
}
