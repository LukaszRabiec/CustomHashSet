using System;

namespace CustomHashSet.Client
{
    class Program
    {
        private static readonly Random _rand = new Random(1);

        static void Main(string[] args)
        {
            int size = 32;

            HashStorageWithInts(size);
            HashStorageWithDoubles(size);
            HashStorageWithStrings();

            Console.ReadKey();
        }

        // INT

        private static void HashStorageWithInts(int size)
        {
            var hashStorage = new HashStorage<int>();
            var data = GenerateIntData(size);

            foreach (var i in data)
            {
                hashStorage.Add(i);
            }

            Console.WriteLine("---== INT ==---");
            Console.Write(hashStorage.ToString());
            Console.WriteLine("Removing...");

            RemoveSomeIntData(hashStorage, 20);

            Console.Write(hashStorage.ToString());

            const int element = 21;
            Console.WriteLine($"Is '21' in collection?: {hashStorage.Contains(element)}\n");
        }

        private static int[] GenerateIntData(int size)
        {
            var data = new int[size];

            for (int i = 0; i < size; i++)
            {
                data[i] = i;
            }

            return data;
        }

        private static void RemoveSomeIntData(HashStorage<int> hashStorage, int removeSize)
        {
            for (int i = 0; i < removeSize; i++)
            {
                hashStorage.Remove(i);
            }
        }

        // DOUBLE

        private static void HashStorageWithDoubles(int size)
        {
            var hashStorage = new HashStorage<double>();
            var data = GenerateDoubleData(size);

            foreach (var i in data)
            {
                hashStorage.Add(i);
            }

            Console.WriteLine("---== DOUBLE ==---");
            Console.WriteLine(hashStorage.ToString());

            const double element = 2.0;

            try
            {
                hashStorage.Add(element);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Trying add {element}: {exception.Message}");
            }
        }

        private static double[] GenerateDoubleData(int size)
        {
            var data = new double[size];

            for (int i = 0; i < size; i++)
            {
                data[i] = _rand.NextDouble();
            }

            return data;
        }

        // STRING
        private static void HashStorageWithStrings()
        {
            var hashStorage = new HashStorage<string>();
            var data = GenerateStringData();

            hashStorage.Add("!!!");
            hashStorage.Add("~~~~~");

            foreach (var i in data)
            {
                hashStorage.Add(i);
            }

            Console.WriteLine("---== STRING ==---");
            Console.WriteLine(hashStorage.ToString());
        }

        private static string[] GenerateStringData()
        {
            var data = new string[127 - 33];

            for (int i = 33; i <= 126; i++)
            {
                data[i - 33] = ((char)i).ToString();
            }

            return data;
        }
    }
}
