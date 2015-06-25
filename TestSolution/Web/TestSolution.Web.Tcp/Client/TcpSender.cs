using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestSolution.Web.Tcp.Client
{
    /// <summary>
    /// Sends messages using TCP connection.
    /// </summary>
    public class TcpSender : ITcpSender
    {

        #region Fields and Properties

        private readonly TcpClient _tcpClient;
        private NetworkStream _connectionStream;

        #endregion Fields and Properties

        #region Constructor

        public TcpSender(IPAddress address, int port)
        {
            var endPoint = new IPEndPoint(address, port);
            _tcpClient = new TcpClient(endPoint);
        }

        #endregion Constructor

        #region ITcpSender

        /// <summary>
        /// Establish TCP connection.
        /// </summary>
        /// <param name="serverEndPoint"></param>
        public void Connect(IPEndPoint serverEndPoint)
        {
            _tcpClient.Connect(serverEndPoint);
            _connectionStream = _tcpClient.GetStream();
        }

        /// <summary>
        /// Closes TCP connection.
        /// </summary>
        public void Close()
        {
            _tcpClient.Close();
        }

        /// <summary>
        /// Sends message via TCP connection.
        /// </summary>
        /// <param name="message"></param>
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

        #endregion ITcpSender
    }
}
