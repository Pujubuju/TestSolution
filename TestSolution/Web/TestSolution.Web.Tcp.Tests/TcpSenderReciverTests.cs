using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using TestSolution.Common;
using TestSolution.Tests.Common;
using TestSolution.Web.Tcp.Client;
using TestSolution.Web.Tcp.Server;
using TestSolution.Web.Tcp.Server.Models;
using TestSolution.Web.Tcp.Utils;

namespace TestSolution.Web.Tcp.Tests
{
    internal class TcpSenderReciverTests : BaseTest
    {
        [Test]
        public void Server_should_recive_message_from_sender_via_tcp()
        {
            var ipHelper = new IPHelper();
            string myLocalIp = ipHelper.LocalIPAddress();

            var tcpHelper = new TcpHelper();
            int serverPort = 0;
            int clientPort = 0;
            for (int i = 9000; i < 9100; i++)
            {
                if (tcpHelper.IsTcpPortFree(i))
                {
                    if (serverPort == 0)
                    {
                        serverPort = i;
                    }
                    else
                    {
                        clientPort = i;
                        break;
                    }
                }
            }

            Console.WriteLine("Server port: " + serverPort);
            Console.WriteLine("Client port: " + clientPort);

            var server = new TcpServer(IPAddress.Parse(myLocalIp), serverPort);
            server.DataRecivedEvent += ServerOnDataRecivedEvent;
            Task.Factory.StartNew(server.Start);

            var sender = new TcpSender(IPAddress.Parse(myLocalIp), clientPort);
            sender.Connect(new IPEndPoint(IPAddress.Parse(myLocalIp), serverPort));
            sender.Send("11111");
            sender.Send("22222");
            sender.Send("33333");

            Thread.Sleep(TimeSpan.FromSeconds(1));

            sender.Close();
            server.Stop();
        }

        [Test]
        public void Server_should_recive_long_message_from_sender_via_tcp()
        {
            var ipHelper = new IPHelper();
            string myLocalIp = ipHelper.LocalIPAddress();

            var tcpHelper = new TcpHelper();
            int serverPort = 0;
            int clientPort = 0;
            for (int i = 9000; i < 9100; i++)
            {
                if (tcpHelper.IsTcpPortFree(i))
                {
                    if (serverPort == 0)
                    {
                        serverPort = i;
                    }
                    else
                    {
                        clientPort = i;
                        break;
                    }
                }
            }

            Console.WriteLine("Server port: " + serverPort);
            Console.WriteLine("Client port: " + clientPort);

            var server = new TcpServer(IPAddress.Parse(myLocalIp), serverPort);
            server.DataRecivedEvent += ServerOnDataRecivedEvent;
            Task.Factory.StartNew(server.Start);

            var sender = new TcpSender(IPAddress.Parse(myLocalIp), clientPort);
            sender.Connect(new IPEndPoint(IPAddress.Parse(myLocalIp), serverPort));

            var randomGenerator = new RandomGenerator(575984757);

            sender.Send(randomGenerator.RandomAlphanumericString(2000));
            sender.Send(randomGenerator.RandomAlphanumericString(2000));

            Thread.Sleep(TimeSpan.FromSeconds(1));

            sender.Close();
            server.Stop();
        }

        private void ServerOnDataRecivedEvent(object sender, TcpData tcpData)
        {
            Console.WriteLine("Recived:" + Encoding.ASCII.GetString(tcpData.Bytes, 0, tcpData.Count));
        }
    }
}
