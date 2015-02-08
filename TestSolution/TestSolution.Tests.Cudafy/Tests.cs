using System.Diagnostics;
using NUnit.Framework;
using TestSolution.Cudafy;
using TestSolution.Tests.Common;

namespace TestSolution.Tests.Cudafy
{

    public class Tests : BaseTestClass
    {

        [Test]
        public void Test1()
        {
            var deviceFinder = new GPUDeviceFinder();
            deviceFinder.FindDevices();

            foreach (var track in deviceFinder.TrackQueries)
            {
                Debug.WriteLine(track.Key);
            }
        }



        [Test]
        public void Test2()
        {
            add_loop_gpu_alt.Execute();
        }

    }
}
