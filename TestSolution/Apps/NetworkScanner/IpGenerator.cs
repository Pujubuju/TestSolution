﻿using System.Collections.Generic;
using System.Text;

namespace NetworkScanner
{
    public class IpGenerator
    {

        private const int MAX_ADDRESS = 256;
        private const string DOT = ".";

        public IpGenerator()
        {
            
        }

        public List<string> GetAddressesFromRange(string start,  string end)
        {
            string[] startAddress = start.Split(new[] {'.'});
            string[] endAddress = end.Split(new[] { '.' });

            int start1 = int.Parse(startAddress[0]);
            int start2 = int.Parse(startAddress[1]);
            int start3 = int.Parse(startAddress[2]);
            int start4 = int.Parse(startAddress[3]);

            int end1 = int.Parse(endAddress[0]);
            int end2 = int.Parse(endAddress[1]);
            int end3 = int.Parse(endAddress[2]);
            int end4 = int.Parse(endAddress[3]);

            var adressess = new List<string>(1000);
            var builder = new StringBuilder(12);
            for (int i = start1; i <= end1; i++)
            {
                for (int j = start2; j <= end2; j++)
                {
                    for (int k = start3; k <= end3; k++)
                    {
                        for (int l = start4; l < end4; l++)
                        {
                            builder.Clear();
                            builder
                                .Append(i).Append(DOT)
                                .Append(j).Append(DOT)
                                .Append(k).Append(DOT)
                                .Append(l);
                            adressess.Add(builder.ToString());
                        }
                    }
                }
            }
            return adressess;
        }

        public List<string> GetAllAddressess()
        {
            var adressess = new List<string>(1000);
            var builder = new StringBuilder(12);
            for (int i = 0; i < MAX_ADDRESS; i++)
            {
                for (int j = 0; j < MAX_ADDRESS; j++)
                {
                    for (int k = 0; k < MAX_ADDRESS; k++)
                    {
                        for (int l = 0; l < MAX_ADDRESS; l++)
                        {
                            builder.Clear();
                            builder
                                .Append(i).Append(DOT)
                                .Append(j).Append(DOT)
                                .Append(k).Append(DOT)
                                .Append(l);
                            adressess.Add(builder.ToString());
                        }
                    }
                }
            }
            return adressess;
        }
    }
}
