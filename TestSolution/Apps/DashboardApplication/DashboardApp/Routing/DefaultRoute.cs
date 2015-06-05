using System.Web.Routing;

namespace App.DashboardApp.Routing
{
    public class DefaultRoute : Route
    {
        public DefaultRoute()
            : base("{*path}", new DefaultRouteHandler())
        {
            RouteExistingFiles = false;
        }
    }
}
