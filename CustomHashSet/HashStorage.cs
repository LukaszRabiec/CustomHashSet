using System;
using System.Collections.Generic;

namespace CustomHashSet
{
    public class HashStorage<T> where T : IConvertible
    {
        private LinkedList<T>[] _storage;

        public HashStorage()
        {
            _storage = new LinkedList<T>[16];
        }

        public void Add(T element)
        {

        }

        public void Remove(T element)
        {

        }

        public void Contains(T element)
        {

        }

        private int CalculateHash(T element)
        {
            int elementHashCode = element.GetHashCode();
            double constKey = (Math.Sqrt(5) - 1) / 2;

            return (int)Math.Round(elementHashCode * constKey % 1 * _storage.Length);
        }
    }
}
