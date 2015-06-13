using System;
using System.Web.Http.SelfHost;
using DashboardApp.Controllers.Bootstrap;

namespace DaashboardApp.WebApiSelfHost
{
    static class Program
    {
        private static readonly Uri _baseAddress = new Uri("http://localhost:9000/");

        static void Main()
        {
            var config = new HttpSelfHostConfiguration(_baseAddress);
            Configuration.SetupConfiguration(config);
            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Web API Self hosted on " + _baseAddress + " Hit ENTER to exit...");
                Console.ReadLine();
                server.CloseAsync().Wait();
            }
        }

    }
}
