using System;
using System.Collections.ObjectModel;
using System.IO;
using Cloo;
using TestSolution.Common;

namespace TestSolution.GPUAcceleration.Cloo.Math
{
    public abstract class BaseGPUMathUnit : DisposableObject
    {

        #region Fields and Propertis

        public ReadOnlyCollection<ComputeDevice> ComputeDevices
        {
            get { return _computePlatform.Devices; }
        }

        protected abstract string KernelFunctionName { get; }

        private string KernelFilePath
        {
            get { return string.Format("Kernels\\{0}.cl", KernelFunctionName); }
        }

        protected ComputeDevice DeafultDevice
        {
            get { return ComputeDevices[0]; }
        }

        private bool _disposed;

        protected ComputeKernel ComputeKernel
        {
            get { return _computeKernel; }
        }

        protected ComputeContext ComputeContext
        {
            get { return _computeContext; }
        }

        private readonly ComputeContext _computeContext;
        private readonly ComputePlatform _computePlatform;
        private ComputeKernel _computeKernel;

        #endregion Fields and Propertis

        #region Constructor

        protected BaseGPUMathUnit(
            ComputePlatform computePlatform)
        {

            _computePlatform = computePlatform;
            var properties = new ComputeContextPropertyList(_computePlatform);
            _computeContext = new ComputeContext(ComputeDeviceTypes.All, properties, null, IntPtr.Zero);
            Initialize();
        }

        #endregion Constructor

        #region Methods

        private void Initialize()
        {
            var openCLc99Program = File.ReadAllText(KernelFilePath);
            ComputeProgram prog = null;
            try
            {
                prog = new ComputeProgram(_computeContext, openCLc99Program);
                prog.Build(_computePlatform.Devices, string.Empty, null, IntPtr.Zero);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Kerel build failed.");
                Console.WriteLine(exception);
            }
            if (prog != null)
            {
                _computeKernel = prog.CreateKernel(KernelFunctionName);
            }
        }

        #endregion Methods

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_disposed) return;
            _computeKernel.Dispose();
            _computeContext.Dispose();
            base.Dispose(true);
            _disposed = true;
        }

        #endregion IDisposable

    }
}
