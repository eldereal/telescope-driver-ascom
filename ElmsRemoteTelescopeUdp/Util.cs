using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;
using System.Net;
using ASCOM.DeviceInterface;

namespace ASCOM.ElmsRemoteTelescopeUdp
{
    class Helpers
    {
        private const string PortReleaseGuid = "8875BD8E-4D5B-11DE-B2F4-691756D89593";

        /// <summary> 
        /// Check if startPort is available, incrementing and 
        /// checking again if it's in use until a free port is found 
        /// </summary> 
        /// <param name="startPort">The first port to check</param> 
        /// <returns>The first available port</returns> 
        public static int FindNextAvailableUDPPort(int startPort)
        {
            int port = startPort;
            bool isAvailable = true;

            var mutex = new Mutex(false,
                string.Concat("Global/", PortReleaseGuid));
            mutex.WaitOne();
            try
            {
                IPGlobalProperties ipGlobalProperties =
                    IPGlobalProperties.GetIPGlobalProperties();
                IPEndPoint[] endPoints =
                    ipGlobalProperties.GetActiveUdpListeners();

                do
                {
                    if (!isAvailable)
                    {
                        port++;
                        isAvailable = true;
                    }

                    foreach (IPEndPoint endPoint in endPoints)
                    {
                        if (endPoint.Port != port)
                            continue;
                        isAvailable = false;
                        break;
                    }

                } while (!isAvailable && port < IPEndPoint.MaxPort);

                if (!isAvailable)
                    throw new ApplicationException("Not able to find a free TCP port.");

                return port;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }

    enum CommandByte : byte
    {
        Ping = 0,
        SetTracking = 1,
        SetRaSpeed = 2,
        SetDecSpeed = 3,
        PulseGuide = 4,
        SetRaGuideSpeed = 5,
        SetDecGuideSpeed = 6,
        Sync = 7,
        Slew = 8,
        AbortSlew = 9,
        SetSideOfPier = 10
    }

    class Commands
    {
        public static byte[] CommandPing()
        {
            return new byte[] { (byte) CommandByte.Ping };
        }

        public static byte[] CommandSetTracking(bool tracking)
        {
            return new byte[] { (byte) CommandByte.SetTracking, tracking ? (byte)1 : (byte)0 };
        }

        public static byte[] CommandSetRaSpeed(double raSpeed)
        {
            int speedNum = (int)(raSpeed * 1000);
            byte[] r = new byte[5];
            r[0] = (byte)CommandByte.SetRaSpeed;
            Array.Copy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(speedNum)), 0, r, 1, 4);
            return r;
        }

        public static byte[] CommandSetRaGuideSpeed(double raGuideSpeed)
        {
            int speedNum = (int)(raGuideSpeed * 1000);
            byte[] r = new byte[5];
            r[0] = (byte)CommandByte.SetRaGuideSpeed;
            Array.Copy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(speedNum)), 0, r, 1, 4);
            return r;
        }

        public static byte[] CommandSetDecSpeed(double decSpeed)
        {
            int speedNum = (int)(decSpeed * 1000);
            byte[] r = new byte[5];
            r[0] = (byte) CommandByte.SetDecSpeed;
            Array.Copy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(speedNum)), 0, r, 1, 4);
            return r;
        }

        public static byte[] CommandSetDecGuideSpeed(double decGuideSpeed)
        {
            int speedNum = (int)(decGuideSpeed * 1000);
            byte[] r = new byte[5];
            r[0] = (byte)CommandByte.SetDecGuideSpeed;
            Array.Copy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(speedNum)), 0, r, 1, 4);
            return r;
        }

        public static byte[] CommandPulseGuidingSpeed(GuideDirections dir, short time)
        {
            Byte dirByte;
            switch (dir)
            {
                case GuideDirections.guideNorth:
                    dirByte = 1;
                    break;
                case GuideDirections.guideSouth:
                    dirByte = 2;
                    break;
                case GuideDirections.guideEast:
                    dirByte = 3;
                    break;
                case GuideDirections.guideWest:
                    dirByte = 4;
                    break;
                default:
                    dirByte = 0;
                    break;
            }
            byte[] r = new byte[4];
            r[0] = (byte)CommandByte.PulseGuide;
            r[1] = dirByte;
            Array.Copy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(time)), 0, r, 2, 2);
            return r;
        }

        public static byte[] CommandSyncToCoordinates(int targetRaMillis, int targetDecMillis)
        {
            byte[] r = new byte[9];
            r[0] = (byte)CommandByte.Sync;
            Array.Copy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(targetRaMillis)), 0, r, 1, 4);
            Array.Copy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(targetDecMillis)), 0, r, 5, 4);
            return r;
        }

        public static byte[] CommandSlewToCoordinates(int targetRaMillis, int targetDecMillis)
        {
            byte[] r = new byte[9];
            r[0] = (byte)CommandByte.Slew;
            Array.Copy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(targetRaMillis)), 0, r, 1, 4);
            Array.Copy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(targetDecMillis)), 0, r, 5, 4);
            return r;
        }

        public static byte[] CommandAbortSlew()
        {
            return new byte[] { (byte)CommandByte.AbortSlew };
        }

        public static byte[] CommandSetSideOfPier(bool sideOfPierIsWest)
        {
            return new byte[] { 
                (byte)CommandByte.SetSideOfPier,
                sideOfPierIsWest ? (byte)1 : (byte)0
            };
        }

    }
}
