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
        public int DeclinationMillis { get; private set; }
        public bool Slewing { get; private set; }
        public bool Tracking { get; private set; }
        public int RightAscensionRateMillis { get; private set; }
        public int DeclinationRateMillis { get; private set; }
        public bool SideOfPierIsWest { get; private set; }

        private Broadcast()
        {
        }

        public static Broadcast Parse(byte[] data)
        {
            uint ip = BitConverter.ToUInt32(data, 0);
            short port = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(data, 4));
            int ratick = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(data, 6));
            int dectick = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(data, 10));
            Broadcast br = new Broadcast();
            br.IP = new IPAddress(ip);
            br.Port = port;
            br.RightAscensionMillis = ratick;
            br.DeclinationMillis = dectick;
            br.Slewing = data[14] != 0;
            br.Tracking = data[15] != 0;
            br.RightAscensionRateMillis = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(data, 16));
            br.DeclinationRateMillis = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(data, 20));
            br.SideOfPierIsWest = data[24] != 0;
            return br;
        }
    }
}
