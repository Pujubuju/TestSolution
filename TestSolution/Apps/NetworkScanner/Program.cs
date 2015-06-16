using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace NetworkScanner
{
    internal class Program
    {
        private static void Main()
        {
            var generator = new IpGenerator();
            List<string> addressess = generator.GetAddressesFromRange("192.168.0.1", "192.168.0.256");
            foreach (string address in addressess)
            {
                PingReply result = Ping(address, 3, 5);
                if (result != null)
                {
                    Console.WriteLine("Address: {0}, Status: {1}, Time: {2}", address, result.Status, new TimeSpan(result.RoundtripTime));
                }
            }
            Console.ReadKey();
        }

        private static PingReply Ping(string host, int attempts, int timeout)
        {
            var ping = new Ping();
            for (int i = 0; i < attempts; i++)
            {
                try
                {
                    return ping.Send(host, timeout);
                }
                catch
                {
                    // Do nothing and let it try again until the attempts are exausted.
                    // Exceptions are thrown for normal ping failurs like address lookup
                    // failed.  For this reason we are supressing errors.
                }
            }

            // Return false if we can't successfully ping the server after several attempts.
            return null;
        }
    }
}