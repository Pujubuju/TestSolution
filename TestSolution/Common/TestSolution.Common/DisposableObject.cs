using System;

namespace TestSolution.Common
{
    public class DisposableObject : IDisposable
    {

        #region Fields and Propertis

        private bool _disposed;

        #endregion Fields and Propertis

        #region Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed)
            {
                return;
            }
            _disposed = true;
        }

        #endregion Methods

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable

    }
}
