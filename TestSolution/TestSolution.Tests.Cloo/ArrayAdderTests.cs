using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Cloo;
using NUnit.Framework;
using TestSolution.Cloo.Helpers;
using TestSolution.Cloo.Math;
using TestSolution.Common;
using TestSolution.Tests.Common;

namespace TestSolution.Tests.Cloo
{
    
    [RequiresSTA]
    public class ArrayAdderTests : BaseTestClass
    {

        [Test]
        public void Should_add_arrays()
        {
            var computePlatforms = DeviceFinder.GetComputePlatforms();
            var adder = new ArrayAdder(computePlatforms[0]);
            var array1 = new float[] {10, 20, 30, 45};
            var array2 = new float[] {14, 56, 12, 67};
            var expectedResult = new float[] { 24, 76, 42, 112 };
            adder.Add(array1, ref array2);
            CollectionAssert.AreEqual(expectedResult, array2);
        }

        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        [TestCase(1000000)]
        [TestCase(10000000)]
        [TestCase(100000000)]
        public void Arrays_adding_speed_comaprison(int arraySize)
        {
            //SetUp
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
                    var array1 = randomGenerator.GenerateRandomFloatsArray(arraySize);
                    var array2 = randomGenerator.GenerateRandomFloatsArray(arraySize);
                    var description = DeviceFinder.GetComputeDeviceDescription(computeDevice);

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine(description);
                    watch.Restart();
                    arrayAdder.Add(computeDevice, array1, ref array2);
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
