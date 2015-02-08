using System;
using System.Collections.Generic;
using Cloo;

namespace TestSolution.Cloo
{
    public class ClooTutorial
    {

        public ClooTutorial()
        {
            //Number of Platforms
            var numPlats = ComputePlatform.Platforms.Count;
            var properties = new ComputeContextPropertyList(ComputePlatform.Platforms[0]); 
            var context = new ComputeContext(ComputeDeviceTypes.All, properties, null, IntPtr.Zero);

            //Vector sum source code
            var vecSum = @"
                            __kernel void
                            floatVectorSum(
                            __global float * v1,
                            __global float * v2)
                            {
                                // Vector element index
                                int i = get_global_id(0);
                                v1[i] = v1[i] + v2[i];
                            }
                            "; 
            
            //Get a list of devices
            var Devs = new List<ComputeDevice>();
            Devs.Add(ComputePlatform.Platforms[0].Devices[0]);
            Devs.Add(ComputePlatform.Platforms[0].Devices[1]);

            //Create a new OpenCL program
            ComputeProgram prog = null;
            try
            {
                prog = new ComputeProgram(context, vecSum);
                prog.Build(Devs, "", null, IntPtr.Zero);
            }
            catch
            {
                
            }

            //Create the kernel
            var kernelVecSum = prog.CreateKernel("floatVectorSum");

            //In case you want to create all kernels at the same time
            var Kernels = prog.CreateAllKernels();
            foreach (var k in Kernels)
            {
            }

            //Creates host variables to pass to device memory
            float[] v1 = new float[10], v2 = new float[10];

            for (var i = 0; i < v1.Length; i++)
            {
                v1[i] = i;
                v2[i] = 2 * i;
            } 
            
            //Creates OpenCL buffers (copy data to Device)
            //Something very positive is that you declare the type of your buffer
            var bufV1 = new ComputeBuffer<float>(context, ComputeMemoryFlags.ReadWrite | ComputeMemoryFlags.UseHostPointer, v1);
            var bufV2 = new ComputeBuffer<float>(context, ComputeMemoryFlags.ReadWrite | ComputeMemoryFlags.UseHostPointer, v2);

            //You have to set kernels arguments by manually assigning them
            //This has a API-like fashion
            kernelVecSum.SetMemoryArgument(0, bufV1);
            kernelVecSum.SetMemoryArgument(1, bufV2);

            //Create the command queue
            var Queue = new ComputeCommandQueue(context, ComputePlatform.Platforms[0].Devices[1], ComputeCommandQueueFlags.None);

            //Enqueue the Execute command. 
            Queue.Execute(kernelVecSum, null, new long[] { v1.Length }, null, null);

            //Enqueue read command.
            //v1 = Queue.Read<float>(bufV1, null);
            var ptr = new IntPtr(0);
            //Queue.Read(bufV1, true, v1.Length, 1, ptr, null);

            Queue.ReadFromBuffer(bufV1, ref v2, true, null);
        }

    }
}
