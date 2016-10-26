namespace CustomHashSet
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class HashStorage<T> where T : IConvertible
    {
        private const int _minSize = 16;
        private readonly HashingFunction _calculateHash;
        private int _numberOfElements;

        private LinkedList<T>[] _storage;

        public HashStorage()
        {
            _calculateHash = CheckTypeAndGetHashingFunction();
            _storage = new LinkedList<T>[_minSize];
            _numberOfElements = 0;
        }

        public bool Add(T element)
        {
            if (typeof(T) == typeof(double))
                ValidateElementInDouble(element);

            var listIndex = _calculateHash(element, _storage.GetLength(0));
            var elementIsInStorage = Contains(element);

            if (elementIsInStorage)
                return false;

            if (_storage[listIndex] == null)
                _storage[listIndex] = new LinkedList<T>();

            _storage[listIndex].AddLast(element);
            _numberOfElements++;

            if (ElementsExceededSize())
                IncreaseStorage();

            return true;
        }

        public bool Remove(T element)
        {
            if (typeof(T) == typeof(double))
                ValidateElementInDouble(element);

            var listIndex = _calculateHash(element, _storage.GetLength(0));
            var elementIsInStorage = Contains(element);

            if (!elementIsInStorage)
                return false;

            _storage[listIndex].Remove(element);
            _numberOfElements--;

            if (StorageLengthIsLargerThenMinSize() && ElementsDecreasedBy60PercentOfSize())
                DecreaseStorage();

            return true;
        }

        public bool Contains(T element)
        {
            if (typeof(T) == typeof(double))
                ValidateElementInDouble(element);

            var listIndex = _calculateHash(element, _storage.GetLength(0));

            if (listIndex >= _storage.Length)
                return false;

            if (_storage[listIndex] != null)
                return _storage[listIndex].Contains(element);

            return false;
        }

        public override string ToString()
        {
            var resultString = "";
            var stringBuilder = new StringBuilder();

            for (var listIndex = 0; listIndex < _storage.GetLength(0); listIndex++)
            {
                stringBuilder.Clear();
                stringBuilder.Append($"[{listIndex}]: ");

                if (_storage[listIndex] != null)
                    foreach (var item in _storage[listIndex])
                        stringBuilder.Append($"{item}, ");

                stringBuilder.Remove(stringBuilder.Length - 2, 2);
                stringBuilder.Append(";\n");

                resultString += stringBuilder;
            }

            return resultString;
        }

        private void IncreaseStorage()
        {
            _numberOfElements = 0;
            var newSize = _storage.GetLength(0) * 2;

            var oldStorage = _storage;
            _storage = new LinkedList<T>[newSize];

            for (var i = 0; i < oldStorage.GetLength(0); i++)
                if (oldStorage[i] != null)
                    foreach (var element in oldStorage[i])
                        Add(element);
        }

        private void DecreaseStorage()
        {
            _numberOfElements = 0;
            var newSize = _storage.GetLength(0) / 2;

            var oldStorage = _storage;
            _storage = new LinkedList<T>[newSize];

            for (var i = 0; i < oldStorage.GetLength(0); i++)
                if (oldStorage[i] != null)
                    foreach (var element in oldStorage[i])
                        Add(element);
        }

        private HashingFunction CheckTypeAndGetHashingFunction()
        {
            var type = typeof(T);

            switch (type.ToString())
            {
                case "System.Int32":
                    return HashingFunctions.CalculateHashForInt;
                case "System.Double":
                    return HashingFunctions.CalculateHashForDoubleFrom0To1;
                case "System.String":
                    return HashingFunctions.CalculateHashForString;
                default:
                    throw new Exception($"Type {typeof(T)} is illegal.");
            }
        }

        private void ValidateElementInDouble(T element)
        {
            var convertedElement = Convert.ToDouble(element);

            if ((convertedElement < 0) || (convertedElement > 1))
                throw new ArgumentException("Double type values must be between 0 and 1.");
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

        private delegate int HashingFunction(T element, int storageSize);
    }
}