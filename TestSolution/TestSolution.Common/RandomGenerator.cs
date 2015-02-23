using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSolution.Common
{
    public class RandomGenerator
    {

        #region Fields and Propertis

        private readonly Random _random;

        #endregion Fields and Propertis

        #region Constructor

        public RandomGenerator()
        {
            _random = new Random((int) DateTime.Now.Ticks);
        }

        public RandomGenerator(int seed)
        {
            _random = new Random(seed);
        }

        #endregion Constructor

        #region Methods

        public int Next()
        {
            return _random.Next();
        }

        public float NextFloat()
        {
            return (float)_random.NextDouble();
        }

        public int Next(int max)
        {
            return _random.Next(max);
        }

        public int Next(int min, int max)
        {
            return _random.Next(min, max);
        }

        public string RandomAlphanumericString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = new string(
                Enumerable.Repeat(chars, 8)
                    .Select(s => s[Next(s.Length)])
                    .ToArray());
            return result;
        }

        public List<T> GetRandomElements<T>(List<T> list, int count)
        {
            return list.OrderBy(x => Next()).Take(count).ToList();
        }

        public int[] GenerateRandomIntegersArray(int size)
        {
            var array = new int[size];
            for (var i = 0; i < size; i++)
            {
                array[i] = Next();
            }
            return array;
        }

        public float[] GenerateRandomFloatsArray(int size)
        {
            var array = new float[size];
            for (var i = 0; i < size; i++)
            {
                array[i] = NextFloat();
            }
            return array;
        }

        #endregion Methods

    }
}
