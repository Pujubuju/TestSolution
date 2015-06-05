using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace App.DashboardApp
{
    public class Application : HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
