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
            int listIndex = CalculateHash(element);

        }

        public void Remove(T element)
        {

        }

        public bool Contains(T element)
        {
            int listIndex = CalculateHash(element);

            if (listIndex >= _storage.Length)
            {
                return false;
            }

            return _storage[listIndex].Contains(element);
        }

        private int CalculateHash(T element)
        {
            int elementHashCode = element.GetHashCode();
            double constKey = (Math.Sqrt(5) - 1) / 2;

            return (int)Math.Round(elementHashCode * constKey % 1 * _storage.Length);
        }

        HashSet<int> sss = new HashSet<int>();
    }
}
