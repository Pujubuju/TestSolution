using System;
using System.Collections.Generic;
using System.Diagnostics;
using Cudafy;
using Cudafy.Host;
using TestSolution.Cudafy.Components;

namespace TestSolution.Cudafy
{
    public class GPUDeviceFinder
    {

        private Dictionary<string, TrackQuery> _trackQueries = new Dictionary<string, TrackQuery>();
        public Dictionary<string, TrackQuery> TrackQueries { get { return _trackQueries; } }

        public GPUDeviceFinder()
        {
            
        }

        public void FindDevices()
        {
            var gpuList = new List<GPGPU>();
            for (var j = 0; j < 2; j++)
            {
                var gpuType = j == 0 ? eGPUType.Cuda : eGPUType.OpenCL;
                var cnt = 0;
                Debug.WriteLine("Enumerating {0} devices", gpuType);
                try
                {
                    cnt = CudafyHost.GetDeviceCount(gpuType);
                    if (cnt == 0)
                    {
                        Console.WriteLine("Failed to find any devices for {0}", gpuType);
                        continue;
                    }
                }
                catch (Exception deviceCntEx)
                {
                    Console.WriteLine("Failed to get device count for {0}\r\nError:\r\n{1}", gpuType, deviceCntEx.Message);
                    continue;
                }
                for (var i = 0; i < cnt; i++)
                {
                    try
                    {
                        var gpu = CudafyHost.GetDevice(gpuType, i);
                        var name = GetFullName(gpu);
                        var track = new TrackQuery(gpu);
                        gpuList.Add(gpu);
                        _trackQueries.Add(name, track);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }

        private static string GetFullName(GPGPU gpu)
        {
            var prop = gpu.GetDeviceProperties();
            return ((gpu is OpenCLDevice) ? "OpenCL-" : "CUDA-") + prop.PlatformName + "-" + prop.Name;
        }
    }
}
