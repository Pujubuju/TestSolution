using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestSolution.Web.Tcp.Client
{
    public class TcpSender
    {
        private readonly TcpClient _tcpClient;
        private NetworkStream _connectionStream;

        public TcpSender(IPAddress address, int port)
        {
            var endPoint = new IPEndPoint(address, port);
            _tcpClient = new TcpClient(endPoint);
        }

        public void Connect(IPEndPoint serverEndPoint)
        {
            _tcpClient.Connect(serverEndPoint);
            _connectionStream = _tcpClient.GetStream();
        }

        public void Close()
        {
            _tcpClient.Close();
        }

        public void Send(string message)
        {
            try
            {
                byte[] data = Encoding.ASCII.GetBytes(message);
                _connectionStream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: {0}", message);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

    }
}
