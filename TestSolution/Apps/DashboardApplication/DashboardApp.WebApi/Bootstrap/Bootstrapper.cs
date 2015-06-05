using System.Web.Http.Dependencies;
using DashboardApp.BLL;
using Microsoft.Practices.Unity;

namespace DashboardApp.WebApi.Bootstrap
{
    public static class Bootstrapper
    {
        public static IDependencyResolver CreateResolver()
        {
            var container = new UnityContainer();
            container.RegisterType<ITasksService, TasksService>(new ContainerControlledLifetimeManager());
            var resolver = new UnityResolver(container);
            return resolver;
        }
    }
}