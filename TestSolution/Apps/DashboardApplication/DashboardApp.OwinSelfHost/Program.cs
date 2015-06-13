using System;
using System.Net.Http;
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
                var client = new HttpClient();

                PrintResponse(client, baseAddress, "api/values");
                PrintResponse(client, baseAddress, "api/values/5");
                PrintResponse(client, baseAddress, "api/tasks");
                Console.ReadLine();
            }           
        }

        private static void PrintResponse(HttpClient client, string baseAddress, string uri)
        {
            var response = client.GetAsync(baseAddress + uri).Result;
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        }

    }
}
