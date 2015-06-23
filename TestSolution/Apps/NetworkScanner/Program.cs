using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;

namespace NetworkScanner
{
    internal class Program
    {
        private static void Main()
        {
            var generator = new IpGenerator();
            List<string> addressess = generator.GetAddressesFromRange("192.168.0.1", "192.168.0.15");
            foreach (string address in addressess)
            {
                PingReply result = Ping(address, 2, 2);
                if (result != null)
                {
                    WriteResult(address, result);
                }
                Thread.Sleep(TimeSpan.FromSeconds(0.1));
            }

            Console.ReadKey();
        }

        private static void WriteResult(string address, PingReply result)
        {
            Console.WriteLine("Address: {0}, Status: {1}, Time: {2} ms", address, result.Status, result.RoundtripTime);
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
                }
            }
            return null;
        }

    }
}