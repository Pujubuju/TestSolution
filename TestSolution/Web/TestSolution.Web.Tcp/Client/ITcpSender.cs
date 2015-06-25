using System.Net;

namespace TestSolution.Web.Tcp.Client
{

    /// <summary>
    /// Sends messages using TCP connection.
    /// </summary>
    public interface ITcpSender
    {
        /// <summary>
        /// Establish TCP connection.
        /// </summary>
        /// <param name="serverEndPoint"></param>
        void Connect(IPEndPoint serverEndPoint);

        /// <summary>
        /// Closes TCP connection.
        /// </summary>
        void Close();

        /// <summary>
        /// Sends message via TCP connection.
        /// </summary>
        /// <param name="message"></param>
        void Send(string message);
    }
}