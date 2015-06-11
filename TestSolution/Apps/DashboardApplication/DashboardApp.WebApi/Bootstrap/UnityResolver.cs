using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;
using TestSolution.Common;

namespace DashboardApp.WebApi.Bootstrap
{
    public class UnityResolver : DisposableObject, IDependencyResolver
    {
        #region Fields and Properties

        private readonly IUnityContainer _container;
        private bool _disposed;

        #endregion Fields and Properties

        #region Constructor

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
        }

        #endregion Constructor

        #region IDependencyResolver

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            IUnityContainer child = _container.CreateChildContainer();
            return new UnityResolver(child);
        }

        #endregion IDependencyResolver

        #region IDisposable
    
        protected override void Dispose(bool disposing)
        {
            if (!disposing || _disposed)
            {
                return;
            }
            _container.Dispose();
            base.Dispose(true);
            _disposed = true;
        }

        #endregion IDisposable

    }
}