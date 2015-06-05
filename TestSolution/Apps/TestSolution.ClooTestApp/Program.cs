using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Cloo;
using TestSolution.Cloo.Helpers;
using TestSolution.Cloo.Math;
using TestSolution.Common;

namespace TestSolution.ClooTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //SetUp
            var size = 1000000;
            var randomGenerator = new RandomGenerator();
            var computePlatforms = DeviceFinder.GetComputePlatforms();
            var adders = computePlatforms.Select(computePlatform => new ArrayAdder(computePlatform)).ToList();

            // Act
            var watch = new Stopwatch();
            var list = new List<KeyValuePair<ComputeDevice, TimeSpan>>();
            foreach (var arrayAdder in adders)
            {
                foreach (var computeDevice in arrayAdder.ComputeDevices)
                {
                    var array1 = randomGenerator.GenerateRandomFloatsArray(size);
                    var array2 = randomGenerator.GenerateRandomFloatsArray(size);
                    var description = DeviceFinder.GetComputeDeviceDescription(computeDevice);

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine(description);
                    watch.Restart();
                    for (var i = 0; i < 100; i++)
                    {
                        arrayAdder.Add(computeDevice, array1, ref array2);
                    }
                    watch.Stop();
                    Console.WriteLine();
                    Console.WriteLine("Time: " + watch.Elapsed);
                    Console.WriteLine();
                    list.Add(new KeyValuePair<ComputeDevice, TimeSpan>(computeDevice, watch.Elapsed));
                }
            }

            foreach (var arrayAdder in adders)
            {
                arrayAdder.Dispose();
            }

            // Assert
            Console.WriteLine();
            Console.WriteLine();
            foreach (var keyValuePair in list)
            {
                Console.WriteLine(keyValuePair.Key.Name);
                Console.WriteLine(keyValuePair.Value);
            }
        }
    }
}
