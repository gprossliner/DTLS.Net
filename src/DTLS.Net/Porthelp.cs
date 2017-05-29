using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace DTLS
{
    static class Porthelp
    {
        public static SecureRandom CreateSecureRandom() {
            return new SecureRandom();
        }

        public static Socket SetupSocket(AddressFamily addressFamily) {
            var result = new Socket(addressFamily, SocketType.Dgram, ProtocolType.Udp);
            if(addressFamily == AddressFamily.InterNetworkV6) {
                result.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, true);
            }
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                // Do not throw SocketError.ConnectionReset by ignoring ICMP Port Unreachable
                const Int32 SIO_UDP_CONNRESET = -1744830452;
                result.IOControl(SIO_UDP_CONNRESET, new Byte[] { 0 }, null);
            }
            return result;
        }

    }
}
