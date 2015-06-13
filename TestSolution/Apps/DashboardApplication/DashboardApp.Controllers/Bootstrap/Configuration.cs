using System.Web.Http;
using System.Web.Http.Dispatcher;
using DashboardApp.Controllers.Others;

namespace DashboardApp.Controllers.Bootstrap
{
    public static class Configuration
    {
        public static void SetupConfiguration(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute(
                  name: "DefaultApi",
                  routeTemplate: "api/{controller}/{id}",
                  defaults: new { id = RouteParameter.Optional }
                );
            configuration.Services.Replace(typeof(IAssembliesResolver), new MyAssembliesResolver());
            configuration.DependencyResolver = Bootstrapper.CreateResolver();
        }
    }
}
