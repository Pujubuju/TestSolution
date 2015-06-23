using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using TestSolution.Tests.Common;

namespace TestSolution.Web.Tcp.Tests
{
    internal class TcpSenderReciverTests : BaseTest
    {
        [Test]
        public void Server_should_recive_message_from_sender_via_tcp()
        {
            var server = new TcpServer(IPAddress.Parse("192.168.0.12"), 9000);
            Task.Factory.StartNew(server.Start);

            var sender = new TcpSender(IPAddress.Parse("192.168.0.12"), 9001);
            sender.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.12"), 9000));
            sender.Send("11111");
            sender.Send("22222");
            sender.Send("33333");

            Thread.Sleep(TimeSpan.FromSeconds(1));

            sender.Close();
            server.Stop();
        }

    }
}
