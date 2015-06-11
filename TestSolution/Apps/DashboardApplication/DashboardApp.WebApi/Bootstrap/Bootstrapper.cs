using System.Web.Http.Dependencies;
using DashboardApp.BLL.Services;
using Microsoft.Practices.Unity;

namespace DashboardApp.WebApi.Bootstrap
{
    public static class Bootstrapper
    {
        public static IDependencyResolver CreateResolver()
        {
            var container = new UnityContainer();
            RegisterServices(container);
            var resolver = new UnityResolver(container);
            return resolver;
        }

        private static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<ITasksService, TasksService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUsersService, UsersService>(new ContainerControlledLifetimeManager());
        }

    }
}