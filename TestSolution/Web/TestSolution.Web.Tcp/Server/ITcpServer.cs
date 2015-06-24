using System;
using TestSolution.Web.Tcp.Server.Models;

namespace TestSolution.Web.Tcp.Server
{
    public interface ITcpServer
    {
        event EventHandler<TcpData> DataRecivedEvent; 

        void Start();
        void Stop();
    }
}