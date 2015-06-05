using App.DashboardApp.Routing;
using System.Web.Routing;

namespace App.DashboardApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add("Default", new DefaultRoute());
        }
    }
}
