using System.Collections.Generic;

namespace TestSolution.Web.Tcp.Utils
{
    public interface IIpGenerator
    {
        /// <summary>
        /// Generates IPv4 adressess from given range.
        /// </summary>
        /// <param name="start">Start adress. (194.168.0.1)</param>
        /// <param name="end">End adress. (194.168.0.1)</param>
        /// <returns></returns>
        List<string> GetAddressesFromRange(string start,  string end);
    }
}