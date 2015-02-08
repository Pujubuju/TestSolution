using System;
using Cudafy;
using Cudafy.Host;
using Cudafy.Translator;

namespace TestSolution.Cudafy
{
    public class CudafyExposer
    {





        [Cudafy]
        public static void Add(GThread thread, int[] a, int[] b, int[] c)
        {
            int tid = thread.blockIdx.x;
            if (tid < 10)
            {
                c[tid] = a[tid] + b[tid];
            }
        }

        public static void Add(int[] a, int[] b, int[] c)
        {
            //Add(new GThread(1, 1, new GBlock()), );
        }
    }

    public class AddArrayssByGPU
    {
        public const int N = 10;

        public static void Execute()
        {
            CudafyModule km = CudafyTranslator.Cudafy(typeof (CudafyExposer));

            GPGPU gpu = CudafyHost.GetDevice(CudafyModes.Target, CudafyModes.DeviceId);
            gpu.LoadModule(km);

            int[] a = new int[N];
            int[] b = new int[N];
            int[] c = new int[N];

            // allocate the memory on the GPU
            int[] dev_a = gpu.Allocate<int>(a);
            int[] dev_b = gpu.Allocate<int>(b);
            int[] dev_c = gpu.Allocate<int>(c);

            // fill the arrays 'a' and 'b' on the CPU
            for (int i = 0; i < N; i++)
            {
                a[i] = -i;
                b[i] = i*i;
            }

            // copy the arrays 'a' and 'b' to the GPU
            gpu.CopyToDevice(a, dev_a);
            gpu.CopyToDevice(b, dev_b);

            // launch add on N threads.. ie 10 threads
            gpu.Launch(N, 1).adder(dev_a, dev_b, dev_c);

            // copy the array 'c' back from the GPU to the CPU
            gpu.CopyFromDevice(dev_c, c);

            // display the results
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("{0} + {1} = {2}", a[i], b[i], c[i]);
            }

            // free the memory allocated on the GPU
            gpu.Free(dev_a);
            gpu.Free(dev_b);
            gpu.Free(dev_c);
        }

        [Cudafy]
        public static void adder(GThread thread, int[] a, int[] b, int[] c)
        {
            // tid will be a number between 0 and N
            // each thread only adds 2 numbers (elements) together
            // yet is being passed the entire array
            int tid = thread.blockIdx.x;
            if (tid < N)
                c[tid] = a[tid] + b[tid];
        }

    }
}