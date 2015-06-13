using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace DashboardApp.OwinSelfHost
{
    public class Startup
    {
        /// <summary>
        /// This code configures Web API. The Startup class is specified 
        /// as a type parameter in the WebApp.Start method.
        /// </summary>
        /// <param name="appBuilder"></param>
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            Controllers.Bootstrap.Configuration.SetupConfiguration(config);
            appBuilder.UseWebApi(config);

#if DEBUG
            appBuilder.UseErrorPage();
#endif

            // Remap '/' to '.\defaults\'.
            // Turns on static files and default files.
            appBuilder.UseFileServer(new FileServerOptions
            {
                RequestPath = PathString.Empty,
                FileSystem = new PhysicalFileSystem(@".\static"),
            });

            // Only serve files requested by name.
            //appBuilder.UseStaticFiles("/files");

            // Turns on static files, directory browsing, and default files.
            //appBuilder.UseFileServer(new FileServerOptions
            //{
            //    RequestPath = new PathString("/public"),
            //    EnableDirectoryBrowsing = true,
            //});

            // Browse the root of your application (but do not serve the files).
            // NOTE: Avoid serving static files from the root of your application or bin folder,
            // it allows people to download your application binaries, config files, etc..
            //appBuilder.UseDirectoryBrowser(new DirectoryBrowserOptions
            //{
            //    RequestPath = new PathString("/src"),
            //    FileSystem = new PhysicalFileSystem(@""),
            //});

            // Anything not handled will land at the welcome page.
            appBuilder.UseWelcomePage();

        }
    } 
}
