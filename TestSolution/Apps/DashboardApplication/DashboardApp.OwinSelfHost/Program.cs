using System;
using Microsoft.Owin.Hosting;

namespace DashboardApp.OwinSelfHost
{
    public static class Program
    {
        static void Main()
        {
            const string baseAddress = "http://localhost:9000/";
            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine("Server successfully started at" + baseAddress);
                Console.WriteLine("Press key to shut down...");
                Console.ReadLine();
            }           
        }

    }
}
