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
    public class ArrayMultiplicatorTests : BaseTestClass
    {

        [Test]
        public void Should_multiply_arrays()
        {
            var computePlatforms = DeviceFinder.GetComputePlatforms();
            var adder = new ArrayMultiplicator(computePlatforms[0]);
            var array1 = new float[] {5, 10, 30, 5};
            var array2 = new float[] {2, 20, 10, 5};
            var expectedResult = new float[] { 10, 200, 300, 25 };
            adder.Multiply(array1, ref array2);
            CollectionAssert.AreEqual(expectedResult, array2);
        }

        [TestCase(100)]
        [TestCase(1000)]
        public void Arrays_adding_speed_comaprison(int arraySize)
        {
            //SetUp
            var randomGenerator = new RandomGenerator();
            var computePlatforms = DeviceFinder.GetComputePlatforms();
            var arrayMultiplicators = computePlatforms.Select(computePlatform => new ArrayMultiplicator(computePlatform)).ToList();

            // Act
            var watch = new Stopwatch();
            var list = new List<KeyValuePair<ComputeDevice, TimeSpan>>();
            foreach (var arrayAdder in arrayMultiplicators)
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
                    arrayAdder.Multiply(computeDevice, array1, ref array2);
                    watch.Stop();
                    Console.WriteLine();
                    Console.WriteLine("Time: " + watch.Elapsed);
                    Console.WriteLine();
                    list.Add(new KeyValuePair<ComputeDevice, TimeSpan>(computeDevice, watch.Elapsed));
                }
            }

            foreach (var arrayAdder in arrayMultiplicators)
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
