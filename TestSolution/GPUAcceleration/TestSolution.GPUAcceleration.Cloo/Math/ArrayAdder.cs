using Cloo;

namespace TestSolution.GPUAcceleration.Cloo.Math
{
    public class ArrayAdder : BaseGPUMathUnit
    {

        #region Fields and Propertis

        protected override string KernelFunctionName
        {
            get { return "floatVectorSum"; }
        }

        #endregion Fields and Propertis

        #region Constructor

        public ArrayAdder(
            ComputePlatform computePlatform)
            :base(computePlatform)
        {
        }

        #endregion Constructor

        #region Methods

        public void Add(float[] array1, ref float[] array2)
        {
            Add(DeafultDevice, array1, ref array2);
        }

        public void Add(ComputeDevice computeDevice, float[] array1, ref float[] array2)
        {
            var bufV1 = new ComputeBuffer<float>(ComputeContext,
                ComputeMemoryFlags.ReadWrite | ComputeMemoryFlags.UseHostPointer, array1);
            var bufV2 = new ComputeBuffer<float>(ComputeContext,
                ComputeMemoryFlags.ReadWrite | ComputeMemoryFlags.UseHostPointer, array2);
            ComputeKernel.SetMemoryArgument(0, bufV1);
            ComputeKernel.SetMemoryArgument(1, bufV2);
            var queue = new ComputeCommandQueue(ComputeContext, computeDevice, ComputeCommandQueueFlags.None);
            queue.Execute(ComputeKernel, null, new long[] { array1.Length }, null, null);
            queue.ReadFromBuffer(bufV1, ref array2, true, null);

            bufV1.Dispose();
            bufV2.Dispose();
            queue.Dispose();
        }

        #endregion Methods

    }
}
