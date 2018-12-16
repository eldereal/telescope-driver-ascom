using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace Me.Eldereal.ElmsRemoteDriverBase
{
    public abstract class BaseDriver : IDisposable
    {
        EventWaitHandle nextCommand;
        EventWaitHandle autoDetectEvent;
        UdpClient client;
        UdpClient autoDetectClient;

        private string host;
        private int port;

        public BaseDriver()
        {
            nextCommand = new EventWaitHandle(false, EventResetMode.AutoReset);
            autoDetectEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
        }

        protected bool IsConnected
        {
            get
            {
                return client != null;
            }
        }

        protected void Connect()
        {
            autoDetectClient = new UdpClient();
            int port = Helpers.FindNextAvailableUDPPort(9334);
            autoDetectClient.Client.Bind(new IPEndPoint(IPAddress.Any, port));
            autoDetectClient.BeginReceive(new AsyncCallback(AutoConnectReceived), autoDetectClient);
            if (!autoDetectEvent.WaitOne(10000))
            {
                Disconnect();
                throw new ASCOM.DriverException("Auto-find telescope timeout");
            }
            client = new UdpClient(Helpers.FindNextAvailableUDPPort(19333));
            client.BeginReceive(new AsyncCallback(PackReceived), client);
            SendCommand(Commands.CommandPing());
            LogMessage("Connect", "Connecting to {0}:{1}", host, port);
        }

        abstract protected void AfterBroadcastPackReceived(Broadcast pack);
        abstract protected void LogMessage(string identifier, string message, params object[] args);

        protected void SendCommand(byte[] command)
        {
            if (!IsConnected)
            {
                throw new ASCOM.DriverException("Send message before connected");
            }
            client.Send(command, command.Length, host, port);
        }

        protected void SendCommandAndWaitAck(byte[] command)
        {
            if (!IsConnected)
            {
                throw new ASCOM.DriverException("Send message before connected");
            }
            nextCommand.Reset();
            client.Send(command, command.Length, host, port);
            WaitAck();
        }

        void WaitAck()
        {
            if (!nextCommand.WaitOne(1000))
            {
                throw new ASCOM.DriverException("Connection timeout");
            }
        }

        protected void Disconnect()
        {

            LogMessage("Disconnect", "Disconnecting from {0}:{1}", host, port);
            if (client != null)
            {
                try
                {
                    client.Close();
                }
                catch (SocketException)
                {
                }
                finally
                {
                    client = null;
                }
            }
            if (autoDetectClient != null)
            {
                try
                {
                    autoDetectClient.Close();
                }
                catch (SocketException)
                {
                }
                finally
                {
                    autoDetectClient = null;
                }
            }
        }

        void AutoConnectReceived(IAsyncResult result)
        {
            if (autoDetectClient == null) return;
            try
            {
                IPEndPoint from = null;
                byte[] res = autoDetectClient.EndReceive(result, ref from);
                Broadcast pack = Broadcast.Parse(res);
                host = pack.IP.ToString();
                port = pack.Port;
                AfterBroadcastPackReceived(pack);
                
                autoDetectEvent.Set();
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
                if (autoDetectClient != null)
                {
                    autoDetectClient.BeginReceive(new AsyncCallback(AutoConnectReceived), autoDetectClient);
                }
            }
        }

        void PackReceived(IAsyncResult result)
        {
            if (client == null) return;
            try
            {
                IPEndPoint from = null;
                byte[] res = client.EndReceive(result, ref from);
                ParseAck(res);
            }
            catch (ObjectDisposedException e)
            {
                LogMessage("Telescope", "ObjectDisposedException: " + e.Message);
            }
            catch (SocketException e)
            {
                LogMessage("Telescope", "Socket exception: " + e.ErrorCode + " " + e.Message);
            }
            finally
            {
                if (client != null)
                {
                    client.BeginReceive(new AsyncCallback(PackReceived), client);
                }
            }
        }

        private void ParseAck(byte[] data)
        {
            nextCommand.Set();
        }

        public void Dispose()
        {
            Disconnect();
        }
    }
}
