using System;
using Microsoft.Owin.Hosting;

namespace DashboardApp.OwinSelfHost
{
    public static class Program
    {
        static void Main()
        {
            var options = CreateStartupOptions();
            using (WebApp.Start<Startup>(options))
            {
                PrintStartupInfo(options);
            }
        }

        private static void PrintStartupInfo(StartOptions options)
        {
            Console.WriteLine("Server successfully started at: ");
            foreach (string url in options.Urls)
            {
                Console.WriteLine(url);
            }
            Console.WriteLine();
            Console.WriteLine("Press key to shut down...");
            Console.ReadLine();
        }

        private static StartOptions CreateStartupOptions()
        {
            const int port = 9000;
            var options = new StartOptions();
            options.Urls.Add(GetUrl("localhost", port));
            options.Urls.Add(GetUrl("127.0.0.1", port));
            options.Urls.Add(GetUrl(Environment.MachineName, port));
            return options;
        }

        private static string GetUrl(string ip, int port)
        {
            return string.Format("http://{0}:{1}", ip, port);
        }

    }
}
