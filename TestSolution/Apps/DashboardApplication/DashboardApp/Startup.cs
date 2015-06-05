[assembly: Microsoft.Owin.OwinStartup(typeof(App.DashboardApp.Startup))]

namespace App.DashboardApp
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //// For more information on how to configure your application, visit:
            //// http://go.microsoft.com/fwlink/?LinkID=316888
            this.ConfigureAuth(app);
        }
    }
}
