using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ASCOM.ElmsRemoteTelescopeUdp
{
    public class Broadcast
    {
        public IPAddress IP { get; private set; }
        public int Port { get; private set; }
        public int RightAscensionMillis { get; private set; }
        public int RightAscensionDirection { get; private set; }
        public int DeclinationMillis { get; private set; }
        public int DeclinationDirection { get; private set; }

        private Broadcast()
        {
        }

        public static Broadcast Parse(byte[] data)
        {
            uint ip = BitConverter.ToUInt32(data, 0);
            short port = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(data, 4));
            //bool radir = BitConverter.ToBoolean(data, 6);
            int ratick = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(data, 6));
            //bool decdir = BitConverter.ToBoolean(data, 11);
            int dectick = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(data, 10));
            Broadcast br = new Broadcast();
            br.IP = new IPAddress(ip);
            br.Port = port;
            br.RightAscensionMillis = ratick;
            //br.RightAscensionDirection = radir ? 1 : -1;
            br.DeclinationMillis = dectick;
            //br.DeclinationDirection = decdir ? 1 : -1;
            return br;
        }
    }
}
