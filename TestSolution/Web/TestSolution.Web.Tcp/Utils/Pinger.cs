using System.Net.NetworkInformation;
using TestSolution.Common;

namespace TestSolution.Web.Tcp.Utils
{
    public class Pinger : DisposableObject, IPinger
    {

        #region Fields and Properties

        private readonly Ping _ping;
        private readonly IIpGenerator _ipGenerator;
        private readonly int _timeoutMiliseconds;
        private readonly int _attempts;
        private bool _disposed;

        #endregion Fields and Properties

        #region Constructor

        public Pinger(
            int attempts, 
            int timeoutMiliseconds,
            IIpGenerator ipGenerator)
        {
            _attempts = attempts;
            _timeoutMiliseconds = timeoutMiliseconds;
            _ipGenerator = ipGenerator;
            _ping = new Ping();
        }

        #endregion Constructor

        #region IPinger

        public PingReply Ping(string host)
        {
            for (int i = 0; i < _attempts; i++)
            {
                try
                {
                    return _ping.Send(host, _timeoutMiliseconds);
                }
                catch
                {
                }
            }
            return null;
        }

        public PingReply[] PingAdressess(string startAddress, string endAddress)
        {
            var addresses = _ipGenerator.GetAddressesFromRange(startAddress, endAddress);
            var replays = new PingReply[addresses.Count];
            for (int i = 0; i < addresses.Count; i++)
            {
                replays[i] = Ping(addresses[i]);
            }
            return replays;
        }

        #endregion IPinger

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (!disposing || _disposed)
            {
                return;
            }
            _ping.Dispose();
            base.Dispose(true);
            _disposed = true;
        }

        #endregion IDisposable

    }
}