using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NetworkScanner
{
    internal class Program
    {
        private static void Main()
        {

            var addr = "87.206.229.249";
            var ping = Ping(addr, 3, 5);
            WriteResult(addr, ping);
            Console.ReadKey();

            var ipAddress = IPAddress.Parse(addr);
            var endPoint = new IPEndPoint(ipAddress, 9000);
            var client = new TcpClient(endPoint);
            //client.Connect();

            var generator = new IpGenerator();
            List<string> addressess = generator.GetAddressesFromRange("192.168.0.1", "192.168.0.256");
            foreach (string address in addressess)
            {
                PingReply result = Ping(address, 3, 5);
                if (result != null)
                {
                    WriteResult(address, result);
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
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