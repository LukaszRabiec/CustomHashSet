using System;
using System.Linq;

namespace CustomHashSet
{
    internal static class HashingFunctions
    {
        internal static int CalculateHashForInt<T>(T element, int storageSize)
        {
            double constKey = (Math.Sqrt(5) - 1) / 2;

            return (int)(Math.Abs(Convert.ToInt32(element)) * constKey % 1 * storageSize);
        }

        internal static int CalculateHashForDoubleFrom0To1<T>(T element, int storageSize)
        {
            return (int)(Convert.ToDouble(element) * storageSize);
        }

        /// <summary>
        /// Hashing method for ASCII letters and special chars. This method doesn't recognize anagrams.
        /// </summary>
        /// <typeparam name="T">string for this method</typeparam>
        /// <param name="element">word or char to hash</param>
        /// <param name="storageSize">overall size of hash storage</param>
        /// <returns></returns>
        internal static int CalculateHashForString<T>(T element, int storageSize)
        {
            string convertedElement = Convert.ToString(element);
            double sum = 0;

            for (int i = 0; i < convertedElement.Length; i++)
            {
                sum += convertedElement.ElementAt(i);
            }

            sum /= convertedElement.Length;

            sum = (sum - 33) / (126 - 33 + 1);  // ASCII letters + special chars - norm <0,1)

            return (int)(sum * storageSize);
        }
    }
}
