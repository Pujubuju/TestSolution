using System;
using System.Web;
using System.Web.Routing;
using System.Web.WebPages;

namespace App.DashboardApp.Routing
{
    public class DefaultRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            // Use cases:
            //     ~/            -> ~/views/index.cshtml
            //     ~/about       -> ~/views/about.cshtml or ~/views/about/index.cshtml
            //     ~/views/about -> ~/views/about.cshtml
            //     ~/xxx         -> ~/views/404.cshtml
            string filePath = requestContext.HttpContext.Request.AppRelativeCurrentExecutionFilePath;

            if (filePath == "~/")
            {
                filePath = "~/views/index.cshtml";
            }
            else
            {
                if (!filePath.StartsWith("~/views/", StringComparison.OrdinalIgnoreCase))
                {
                    filePath = filePath.Insert(2, "views/");
                }

                if (!filePath.EndsWith(".cshtml", StringComparison.OrdinalIgnoreCase))
                {
                    filePath += ".cshtml";
                }
            }

            IHttpHandler handler = WebPageHttpHandler.CreateFromVirtualPath(filePath);
            if (handler == null)
            {
                requestContext.RouteData.DataTokens.Add("templateUrl", "/views/404");
                handler = WebPageHttpHandler.CreateFromVirtualPath("~/views/404.cshtml");
            }
            else
            {
                requestContext.RouteData.DataTokens.Add("templateUrl", filePath.Substring(1, filePath.Length - 8));
            }
            return handler;
        }
    }
}
