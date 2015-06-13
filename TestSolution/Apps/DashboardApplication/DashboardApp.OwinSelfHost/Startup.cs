using System.Web.Http;
using Owin;

namespace DashboardApp.OwinSelfHost
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            Controllers.Bootstrap.Configuration.SetupConfiguration(config);
            appBuilder.UseWebApi(config);
        }
    } 
}
