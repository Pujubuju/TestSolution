using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TestSolution.Tests.Ftp
{

    [TestFixture]
    public class FtpClientTests
    {

        [Test]
        public void Test1()
        {
            Assert.IsTrue(true);
            //var localFile = "";
            //var bufferSize = 1024;

            //var ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create("ftp://localhost:21");
            //ftpWebRequest.Credentials = new NetworkCredential("IUSR", "");

            ///* When in doubt, use these options */
            //ftpWebRequest.UseBinary = true;
            //ftpWebRequest.UsePassive = true;
            //ftpWebRequest.KeepAlive = true;
            ///* Specify the Type of FTP Request */
            //ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            ///* Establish Return Communication with the FTP Server */
            //var ftpResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
            ///* Get the FTP Server's Response Stream */
            //var ftpStream = ftpResponse.GetResponseStream();
            ///* Open a File Stream to Write the Downloaded File */
            //FileStream localFileStream = new FileStream(localFile, FileMode.Create);
            ///* Buffer for the Downloaded Data */
            //byte[] byteBuffer = new byte[bufferSize];
            //int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
            ///* Download the File by Writing the Buffered Data Until the Transfer is Complete */
            //try
            //{
            //    while (bytesRead > 0)
            //    {
            //        localFileStream.Write(byteBuffer, 0, bytesRead);
            //        bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
            //    }
            //}
            //catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            ///* Resource Cleanup */
            //localFileStream.Close();
            //ftpStream.Close();
            //ftpResponse.Close();
            //ftpWebRequest = null;

        }


    }
}
