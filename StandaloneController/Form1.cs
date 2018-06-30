using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Net.NetworkInformation;

namespace StandaloneController
{
    public partial class Form1 : Form
    {

        UdpClient client;
        SynchronizationContext context = null; 

        public Form1()
        {
            InitializeComponent();
            context = SynchronizationContext.Current;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new UdpClient();
            int port = Helpers.FindNextAvailableUDPPort(9334);
            client.Client.Bind(new IPEndPoint(IPAddress.Any, port));
            label4.Text = "Bound " + port;
            client.BeginReceive(new AsyncCallback(PackReceived), client);
        }

        void PackReceived(IAsyncResult result)
        {
            UdpClient client = (UdpClient)result.AsyncState;
            try
            {   
                IPEndPoint from = null;
                byte[] res = client.EndReceive(result, ref from);
                Broadcast br = Broadcast.Parse(res);
                context.Post(DataReceived, br);
            }
            catch (ObjectDisposedException e)
            {
                Console.Error.WriteLine(e);
            }
            catch (SocketException e)
            {
                Console.Error.WriteLine(e);
            }
            finally
            {
                client.BeginReceive(new AsyncCallback(PackReceived), client);
            }
        }

        void DataReceived(object data)
        {
            Broadcast br = (Broadcast)data;
            label1.Text = br.IP.ToString() + ":" + br.Port;
            label2.Text = br.RightAscensionTicks.ToString();
            label3.Text = br.DeclinationTicks.ToString();
        }
    }

    public class Broadcast
    {
        public IPAddress IP { get; private set; }
        public int Port { get; private set; }
        public int RightAscensionTicks { get; private set; }
        public int RightAscensionDirection { get; private set; }
        public int DeclinationTicks { get; private set; }
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
            br.RightAscensionTicks = ratick;
            //br.RightAscensionDirection = radir ? 1 : -1;
            br.DeclinationTicks = dectick;
            //br.DeclinationDirection = decdir ? 1 : -1;
            return br;
        }
    }

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
}
