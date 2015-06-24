using System.Net.NetworkInformation;

namespace TestSolution.Web.Tcp.Utils
{
    public interface IPinger
    {
        PingReply Ping(string host);
        PingReply[] PingAdressess(string startAddress, string endAddress);
    }
}