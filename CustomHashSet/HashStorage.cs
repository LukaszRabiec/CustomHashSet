using System;
using System.Collections.Generic;

namespace CustomHashSet
{
    public class HashStorage<T> where T : IConvertible
    {
        private const int _minSize = 16;

        private LinkedList<T>[] _storage;
        private int _numberOfElements;

        public HashStorage()
        {
            _storage = new LinkedList<T>[_minSize];
            _numberOfElements = 0;
        }

        public bool Add(T element)
        {
            int listIndex = CalculateHash(element);
            bool elementIsInStorage = Contains(element);

            if (elementIsInStorage)
            {
                return false;
            }

            if (_storage[listIndex] == null)
            {
                _storage[listIndex] = new LinkedList<T>();
            }

            _storage[listIndex].AddLast(element);
            _numberOfElements++;

            if (ElementsExceededSize())
            {
                IncreaseStorage();
            }

            return true;
        }

        public bool Remove(T element)
        {
            int listIndex = CalculateHash(element);
            bool elementIsInStorage = Contains(element);

            if (!elementIsInStorage)
            {
                return false;
            }

            _storage[listIndex].Remove(element);
            _numberOfElements--;

            if (StorageLengthIsLargerThenMinSize() && ElementsDecreasedBy60PercentOfSize())
            {
                DecreaseStorage();
            }

            return true;
        }

        public bool Contains(T element)
        {
            int listIndex = CalculateHash(element);

            if (listIndex >= _storage.Length)
            {
                return false;
            }

            if (_storage[listIndex] != null)
            {
                return _storage[listIndex].Contains(element);
            }

            return false;
        }

        public int IndexOf(T element)
        {
            int listIndex = CalculateHash(element);

            if (_storage[listIndex] != null)
            {
                int index = 0;
                foreach (var item in _storage[listIndex])
                {
                    if (item.Equals(element))
                    {
                        return index;
                    }

                    index++;
                }
            }

            return -1;
        }

        private int CalculateHash(T element)
        {
            int elementHashCode = Math.Abs(element.GetHashCode());
            double constKey = (Math.Sqrt(5) - 1) / 2;

            return (int)Math.Round(elementHashCode * constKey % 1 * _storage.Length);
        }

        private void IncreaseStorage()
        {
            _numberOfElements = 0;
            int newSize = _storage.GetLength(0) * 2;

            var oldStorage = _storage;
            _storage = new LinkedList<T>[newSize];

            for (int i = 0; i < newSize; i++)
            {
                if (oldStorage[i] != null)
                {
                    foreach (var element in oldStorage[i])
                    {
                        Add(element);
                    }
                }
            }
        }

        private void DecreaseStorage()
        {
            _numberOfElements = 0;
            int newSize = _storage.GetLength(0) / 2;

            var oldStorage = _storage;
            _storage = new LinkedList<T>[newSize];

            for (int i = 0; i < newSize; i++)
            {
                if (oldStorage[i] != null)
                {
                    foreach (var element in oldStorage[i])
                    {
                        Add(element);
                    }
                }
            }
        }

        private bool ElementsExceededSize()
        {
            return _numberOfElements > _storage.Length;
        }

        private bool StorageLengthIsLargerThenMinSize()
        {
            return _storage.GetLength(0) > _minSize;
        }

        private bool ElementsDecreasedBy60PercentOfSize()
        {
            return _numberOfElements * 2.5 < _storage.GetLength(0);
        }
    }
}
