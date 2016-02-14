using System;
using System.Linq;

namespace CollectionsPerformanceComparison.Utils
{
    public class RandomGenerator
    {

        private readonly Random _random = new Random();

        public string RandomString(int length)
        {
            const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(CHARS, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
