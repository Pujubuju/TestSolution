using System.Web.Http;
using System.Web.Http.Dispatcher;
using DashboardApp.Controllers.Bootstrap;
using DashboardApp.Controllers.Others;

namespace DashboardApp.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Services.Replace(typeof(IAssembliesResolver), new MyAssembliesResolver());
            config.DependencyResolver = Bootstrapper.CreateResolver();            
        }
    }
}
