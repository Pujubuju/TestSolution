using System;
using NUnit.Framework;
using TestSolution.Cloo;
using TestSolution.Cloo.Helpers;
using TestSolution.Tests.Common;

namespace TestSolution.Tests.Cloo
{
    public class DeviceFinderTests : BaseTest
    {

        [Test]
        public void Should_get_all_compute_platforms_descriptions()
        {
            var descriptions = DeviceFinder.GetComputePlatformsDescriptions();
            foreach (var description in descriptions)
            {
                Console.WriteLine(description);
            }
        }

        [Test]
        public void Should_get_devices_dscritions_for_platform()
        {
            var descriptions = DeviceFinder.GetAllComputePlatformsDevicesDescriptions();
            foreach (var description in descriptions)
            {
                Console.WriteLine(description);
            }
        }
    }
}
