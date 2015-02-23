using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyFirstMVC5WebApplication.Startup))]
namespace MyFirstMVC5WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
