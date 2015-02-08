using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloo;

namespace TestSolution.Cloo.Math
{
    public class ArrayMultiplicator : BaseGPUMathUnit
    {
        public ArrayMultiplicator(ComputePlatform computePlatform) : base(computePlatform)
        {
        }

        protected override string KernelFunctionName
        {
            get { return "floatVectorMultiplication"; }
        }

        #region Methods

        public void Multiply(float[] array1, ref float[] array2)
        {
            Multiply(DeafultDevice, array1, ref array2);
        }

        public void Multiply(ComputeDevice computeDevice, float[] array1, ref float[] array2)
        {
            //Creates OpenCL buffers (copy data to Device)
            //Something very positive is that you declare the type of your buffer
            var bufV1 = new ComputeBuffer<float>(ComputeContext,
                ComputeMemoryFlags.ReadWrite | ComputeMemoryFlags.UseHostPointer, array1);
            var bufV2 = new ComputeBuffer<float>(ComputeContext,
                ComputeMemoryFlags.ReadWrite | ComputeMemoryFlags.UseHostPointer, array2);

            //You have to set kernels arguments by manually assigning them
            //This has a API-like fashion
            ComputeKernel.SetMemoryArgument(0, bufV1);
            ComputeKernel.SetMemoryArgument(1, bufV2);

            //Create the command queue
            var queue = new ComputeCommandQueue(ComputeContext, computeDevice, ComputeCommandQueueFlags.None);

            //Enqueue the Execute command. 
            queue.Execute(ComputeKernel, null, new long[] { array1.Length }, null, null);

            //Enqueue read command.
            queue.ReadFromBuffer(bufV1, ref array2, true, null);

            bufV1.Dispose();
            bufV2.Dispose();
            queue.Dispose();
        }

        #endregion Methods
    }
}
