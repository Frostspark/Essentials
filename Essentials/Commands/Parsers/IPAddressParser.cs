using SharedUtils.Commands.Attributes.Parsers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Essentials.Commands.Parsers
{
    public class IPAddressParser
    {
        private const string UsageWarning = "Invalid IP address. An IP address must be in the following format: xxx.xxx.xxx.xxx, where xxx is a number >= 0 and <= 255.";
        private const string MustBeV4Warning = "Only IPv4 addresses are supported by Terraria.";

        [TypeParser(typeof(IPAddress))]
        public static IPAddress ParseIP(string s)
        {
            if(!IPAddress.TryParse(s, out var ip))
            {
                throw new InvalidOperationException(UsageWarning);
            }
            else if(ip.AddressFamily != AddressFamily.InterNetwork)
            {
                throw new InvalidOperationException(MustBeV4Warning);
            }
            else
            {
                return ip;
            }
        }
    }
}
