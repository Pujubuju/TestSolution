using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestSolution.Web.Tcp
{
    public class TcpServer
    {

        private const int DEFAULT_BUFFER_SIZE = 1024;
        private readonly TcpListener _tcpListener;
        private readonly byte[] _buffer = new byte[DEFAULT_BUFFER_SIZE];
        private TcpClient _client;
        private bool _isRunning;

        public TcpServer(IPAddress address, int port)
        {
            _tcpListener = new TcpListener(address, port);
        }

        public void Start()
        {
            Console.WriteLine("Server started...");
            _isRunning = true;
            try
            {
                _tcpListener.Start();
                _client = _tcpListener.AcceptTcpClient();
                while (_isRunning)
                {
                    NetworkStream stream = _client.GetStream();
                    int i = stream.Read(_buffer, 0, _buffer.Length);
                    string data = Encoding.ASCII.GetString(_buffer, 0, i);
                    Console.WriteLine("Received: {0}", data);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        public void Stop()
        {
            _isRunning = false;
            _tcpListener.Stop();
            if (_client != null)
            {
                _client.Close();
            }
        }
    }
}
