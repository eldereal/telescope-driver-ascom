//tabs=4
// --------------------------------------------------------------------------------
// TODO fill in this information for your driver, then remove this line!
//
// ASCOM Telescope driver for ElmsRemoteTelescopeUdp
//
// Description:	Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam 
//				nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam 
//				erat, sed diam voluptua. At vero eos et accusam et justo duo 
//				dolores et ea rebum. Stet clita kasd gubergren, no sea takimata 
//				sanctus est Lorem ipsum dolor sit amet.
//
// Implements:	ASCOM Telescope interface version: <To be completed by driver developer>
// Author:		(XXX) Your N. Here <your@email.here>
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// dd-mmm-yyyy	XXX	6.0.0	Initial edit, created from ASCOM driver template
// --------------------------------------------------------------------------------
//


// This is used to define code in the template that is specific to one class implementation
// unused code canbe deleted and this definition removed.
#define Telescope

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;

using ASCOM;
using ASCOM.Astrometry;
using ASCOM.Astrometry.AstroUtils;
using ASCOM.Utilities;
using ASCOM.DeviceInterface;
using System.Globalization;
using System.Collections;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ASCOM.ElmsRemoteTelescopeUdp
{
    //
    // Your driver's DeviceID is ASCOM.ElmsRemoteTelescopeUdp.Telescope
    //
    // The Guid attribute sets the CLSID for ASCOM.ElmsRemoteTelescopeUdp.Telescope
    // The ClassInterface/None addribute prevents an empty interface called
    // _ElmsRemoteTelescopeUdp from being created and used as the [default] interface
    //
    // TODO Replace the not implemented exceptions with code to implement the function or
    // throw the appropriate ASCOM exception.
    //

    /// <summary>
    /// ASCOM Telescope Driver for ElmsRemoteTelescopeUdp.
    /// </summary>
    [Guid("382a5a13-41c6-4827-91ac-4ff9635bd184")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Telescope : ITelescopeV3
    {
        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal static string driverID = "ASCOM.ElmsRemoteTelescopeUdp.Telescope";

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        // TODO Change the descriptive string for your driver then remove this line
        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        private static string driverDescription = "Elm's Remote Telescope (UDP)";

        internal static string hostProfileName = "Host";
        internal static string hostDefault = "";
        internal static string portProfileName = "Port";
        internal static string portDefault = "9333";
        internal static string autoDetectProfileName = "AutoDetect";
        internal static string autoDetectDefault = "true";

        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "false";

        internal static string host;
        internal static int port;
        internal static bool autoDetect;

        /// <summary>
        /// Private variable to hold an ASCOM Utilities object
        /// </summary>
        private Util utilities;

        /// <summary>
        /// Private variable to hold an ASCOM AstroUtilities object to provide the Range method
        /// </summary>
        private AstroUtils astroUtilities;

        /// <summary>
        /// Variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
        /// </summary>
        internal static TraceLogger tl;

        EventWaitHandle nextCommand;
        EventWaitHandle autoDetectEvent;
        UdpClient client;

        private bool tracking;
        private bool pulseGuiding;
        private int raSpeedInt;
        private int decSpeedInt;
        private int raGuideSpeedInt;
        private int decGuideSpeedInt;

        FormControlPanel formControlPanel;

        Regex ipPortPattern = new Regex("(\\d+\\.\\d+\\.\\d+\\.\\d+):(\\d+)");

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmsRemoteTelescopeUdp"/> class.
        /// Must be public for COM registration.
        /// </summary>
        public Telescope()
        {
            tl = new TraceLogger("", "ElmsRemoteTelescopeUdp");
            ReadProfile(); // Read device configuration from the ASCOM Profile store

            tl.LogMessage("Telescope", "Starting initialisation");

            utilities = new Util(); //Initialise util object
            astroUtilities = new AstroUtils(); // Initialise astro utilities object
            //TODO: Implement your additional construction here
            nextCommand = new EventWaitHandle(false, EventResetMode.AutoReset);
            autoDetectEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
            tl.LogMessage("Telescope", "Completed initialisation");
        }

        void connect()
        {
            if (autoDetect)
            {
                UdpClient autoDetectClient = new UdpClient();
                int port = Helpers.FindNextAvailableUDPPort(9334);
                autoDetectClient.Client.Bind(new IPEndPoint(IPAddress.Any, port));
                autoDetectClient.BeginReceive(new AsyncCallback(AutoConnectReceived), autoDetectClient);
                if (!autoDetectEvent.WaitOne(10000))
                {   
                    throw new ASCOM.DriverException("Auto-find telescope timeout");
                }
            }
            client = new UdpClient(Helpers.FindNextAvailableUDPPort(19333));
            client.BeginReceive(new AsyncCallback(PackReceived), client);
            sendCommandAndWaitAck(Commands.CommandPing());
        }

        int SIDEREAL_DAY_MILLIS = 86164092;
        int DAY_MILLIS = 86400000;

        void AutoConnectReceived(IAsyncResult result)
        {
            UdpClient autoDetectClient = (UdpClient)result.AsyncState;
            try
            {                
                IPEndPoint from = null;
                byte[] res = autoDetectClient.EndReceive(result, ref from);
                Broadcast pack = Broadcast.Parse(res);
                host = pack.IP.ToString();
                port = pack.Port;
                int raMillis = pack.RightAscensionMillis;
                while (raMillis < 0)
                {
                    raMillis += DAY_MILLIS;
                }
                while (raMillis >= DAY_MILLIS)
                {
                    raMillis -= DAY_MILLIS;
                }
                int decMillis = pack.DeclinationMillis;
                this.RightAscension = raMillis / (3600 * 1000.0);
                this.Declination = decMillis * 360.0 / (double)DAY_MILLIS;
                this.Slewing = pack.Slewing;
                this.raSpeedInt = pack.RightAscensionRateMillis;
                this.decSpeedInt = pack.DeclinationRateMillis;
                this.tracking = pack.Tracking;
                this.sideOfPierIsWest = pack.SideOfPierIsWest;
                if (this.formControlPanel != null)
                {
                    this.formControlPanel.UpdateAsync();
                }
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
                autoDetectClient.BeginReceive(new AsyncCallback(AutoConnectReceived), autoDetectClient);
            }
        }

        void PackReceived(IAsyncResult result)
        {
            try
            {
                UdpClient client = (UdpClient)result.AsyncState;
                IPEndPoint from = null;
                byte[] res = client.EndReceive(result, ref from);
                ParseAck(res);
                client.BeginReceive(new AsyncCallback(PackReceived), client);
            }
            catch (ObjectDisposedException e)
            {
            }
            catch (SocketException e)
            {
                tl.LogMessage("Telescope", "Socket exception:" + e.ErrorCode + " " + e.Message);
            }
        }

        bool ParseAck(byte[] data)
        {
            if (data.Length != 18) return false;
            tracking = data[0] != 0;
            pulseGuiding = data[1] != 0;
            raSpeedInt = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(data, 2));
            decSpeedInt = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(data, 6));
            raGuideSpeedInt = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(data, 10));
            decGuideSpeedInt = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(data, 14));
            double raSpeed = raSpeedInt / 1000.0;
            double decSpeed = decSpeedInt / 1000.0;
            double raGuideSpeed = raGuideSpeedInt / 1000.0;
            double decGuideSpeed = decGuideSpeedInt / 1000.0;
            tl.LogMessage("Ack", "tracking: " + tracking + ", pulseGuiding: " + pulseGuiding + ", raSpeed: " + raSpeed + ", raGuideSpeed: " + raGuideSpeed + ", decSpeed: " + decSpeed + ", decGuideSpeed: " + decGuideSpeed);
            nextCommand.Set();
            return true;
        }

        void sendCommand(byte[] command)
        {
            if (!IsConnected)
            {
                throw new ASCOM.DriverException("Send message before connected");
            }
            client.Send(command, command.Length, host, port);
        }

        void sendCommandAndWaitAck(byte[] command)
        {
            if (!IsConnected)
            {
                throw new ASCOM.DriverException("Send message before connected");
            }
            nextCommand.Reset();
            client.Send(command, command.Length, host, port);
            waitAck();
        }

        void waitAck()
        {
            if (!nextCommand.WaitOne(1000))
            {
                throw new ASCOM.DriverException("Connection timeout");
            }
        }


        void disconnect()
        {
            if (client != null)
            {
                client.Close();
            }
        }

        //
        // PUBLIC COM INTERFACE ITelescopeV3 IMPLEMENTATION
        //

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialog form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public void SetupDialog()
        {
            // consider only showing the setup dialog if not connected
            // or call a different dialog if connected
            if (IsConnected)
                System.Windows.Forms.MessageBox.Show("Already connected, just press OK");

            using (SetupDialogForm F = new SetupDialogForm())
            {
                var result = F.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }

        public ArrayList SupportedActions
        {
            get
            {
                tl.LogMessage("SupportedActions Get", "Returning empty arraylist");
                return new ArrayList();
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            LogMessage("", "Action {0}, parameters {1} not implemented", actionName, actionParameters);
            throw new ASCOM.ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
        }

        public void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");
            // Call CommandString and return as soon as it finishes
            this.CommandString(command, raw);
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBlind");
            // DO NOT have both these sections!  One or the other
        }

        public bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            string ret = CommandString(command, raw);
            // TODO decode the return string and return true or false
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBool");
            // DO NOT have both these sections!  One or the other
        }

        public string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            // it's a good idea to put all the low level communication with the device here,
            // then all communication calls this function
            // you need something to ensure that only one command is in progress at a time

            throw new ASCOM.MethodNotImplementedException("CommandString");
        }

        public void Dispose()
        {
            // Clean up the tracelogger and util objects
            tl.Enabled = false;
            tl.Dispose();
            tl = null;
            utilities.Dispose();
            utilities = null;
            astroUtilities.Dispose();
            astroUtilities = null;
        }

        public bool Connected
        {
            get
            {
                LogMessage("Connected", "Get {0}", IsConnected);
                return IsConnected;
            }
            set
            {
                tl.LogMessage("Connected", "Set {0}", value);
                if (value == IsConnected)
                    return;

                if (value)
                {
                    connect();
                    IntPtr handle = GetForegroundWindow();
                    formControlPanel = new FormControlPanel(handle, this);
                    LogMessage("Connected Set", "Connecting to {0}:{1}", host, port);
                }
                else
                {
                    disconnect();
                    formControlPanel.Close();
                    formControlPanel = null;
                    LogMessage("Connected Set", "Disconnecting from {0}:{1}", host, port);
                }
            }
        }

        public string Description
        {
            // TODO customise this device description
            get
            {
                tl.LogMessage("Description Get", driverDescription);
                return driverDescription;
            }
        }

        public string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                // TODO customise this driver description
                string driverInfo = "Information about the driver itself. Version: " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        public short InterfaceVersion
        {
            // set by the driver wizard
            get
            {
                LogMessage("InterfaceVersion Get", "3");
                return Convert.ToInt16("3");
            }
        }

        public string Name
        {
            get
            {
                string name = "Elm's Remote Telescope (UDP)";
                tl.LogMessage("Name Get", name);
                return name;
            }
        }

        #endregion

        #region ITelescope Implementation
        public void AbortSlew()
        {
            tl.LogMessage("AbortSlew", "Slewing: " + Slewing);
            sendCommand(Commands.CommandAbortSlew());
        }

        public AlignmentModes AlignmentMode
        {
            get
            {
                return AlignmentModes.algGermanPolar;
            }
        }

        public double Altitude
        {
            get
            {
                tl.LogMessage("Altitude", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("Altitude", false);
            }
        }

        public double ApertureArea
        {
            get
            {
                tl.LogMessage("ApertureArea Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("ApertureArea", false);
            }
        }

        public double ApertureDiameter
        {
            get
            {
                tl.LogMessage("ApertureDiameter Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("ApertureDiameter", false);
            }
        }

        public bool AtHome
        {
            get
            {
                tl.LogMessage("AtHome", "Get - " + false.ToString());
                return false;
            }
        }

        public bool AtPark
        {
            get
            {
                tl.LogMessage("AtPark", "Get - " + false.ToString());
                return false;
            }
        }

        public IAxisRates AxisRates(TelescopeAxes Axis)
        {
            tl.LogMessage("AxisRates", "Get - " + Axis.ToString());
            return new AxisRates(Axis);
        }

        public double Azimuth
        {
            get
            {
                tl.LogMessage("Azimuth Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("Azimuth", false);
            }
        }

        public bool CanFindHome
        {
            get
            {
                tl.LogMessage("CanFindHome", "Get - " + false.ToString());
                return false;
            }
        }

        public bool CanMoveAxis(TelescopeAxes Axis)
        {
            tl.LogMessage("CanMoveAxis", "Get - " + Axis.ToString());
            switch (Axis)
            {
                case TelescopeAxes.axisPrimary: return false;
                case TelescopeAxes.axisSecondary: return false;
                case TelescopeAxes.axisTertiary: return false;
                default: throw new InvalidValueException("CanMoveAxis", Axis.ToString(), "0 to 2");
            }
        }

        public bool CanPark
        {
            get
            {
                tl.LogMessage("CanPark", "Get - " + false.ToString());
                return false;
            }
        }

        public bool CanPulseGuide
        {
            get
            {
                tl.LogMessage("CanPulseGuide", "Get - " + true.ToString());
                return true;
            }
        }

        public bool CanSetDeclinationRate
        {
            get
            {
                tl.LogMessage("CanSetDeclinationRate", "Get - " + true.ToString());
                return true;
            }
        }

        public bool CanSetGuideRates
        {
            get
            {
                tl.LogMessage("CanSetGuideRates", "Get - " + true.ToString());
                return true;
            }
        }

        public bool CanSetPark
        {
            get
            {
                tl.LogMessage("CanSetPark", "Get - " + false.ToString());
                return false;
            }
        }

        public bool CanSetPierSide
        {
            get
            {
                tl.LogMessage("CanSetPierSide", "Get - " + false.ToString());
                return false;
            }
        }

        public bool CanSetRightAscensionRate
        {
            get
            {
                tl.LogMessage("CanSetRightAscensionRate", "Get - " + true.ToString());
                return true;
            }
        }

        public bool CanSetTracking
        {
            get
            {
                tl.LogMessage("CanSetTracking", "Get - " + true.ToString());
                return true;
            }
        }

        public bool CanSlew
        {
            get
            {
                tl.LogMessage("CanSlew", "Get - " + false.ToString());
                return false;
            }
        }

        public bool CanSlewAltAz
        {
            get
            {
                tl.LogMessage("CanSlewAltAz", "Get - " + false.ToString());
                return false;
            }
        }

        public bool CanSlewAltAzAsync
        {
            get
            {
                tl.LogMessage("CanSlewAltAzAsync", "Get - " + false.ToString());
                return false;
            }
        }

        public bool CanSlewAsync
        {
            get
            {
                tl.LogMessage("CanSlewAsync", "Get - " + false.ToString());
                return false;
            }
        }

        public bool CanSync
        {
            get
            {
                return true;
            }
        }

        public bool CanSyncAltAz
        {
            get
            {
                tl.LogMessage("CanSyncAltAz", "Get - " + false.ToString());
                return false;
            }
        }

        public bool CanUnpark
        {
            get
            {
                tl.LogMessage("CanUnpark", "Get - " + false.ToString());
                return false;
            }
        }

        public double Declination
        {
            get;
            private set;
        }

        public double DeclinationRate
        {
            get
            {
                return decSpeedInt / 1000.0;
            }
            set
            {
                tl.LogMessage("DeclinationRate Set", value.ToString());
                decSpeedInt = (int)(value * 1000);
                formControlPanel.UpdateAsync();
                sendCommand(Commands.CommandSetDecSpeed(value));
            }
        }

        public PierSide DestinationSideOfPier(double RightAscension, double Declination)
        {
            tl.LogMessage("DestinationSideOfPier Get", "Not implemented");
            throw new ASCOM.PropertyNotImplementedException("DestinationSideOfPier", false);
        }

        public bool DoesRefraction
        {
            get
            {
                tl.LogMessage("DoesRefraction Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("DoesRefraction", false);
            }
            set
            {
                tl.LogMessage("DoesRefraction Set", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("DoesRefraction", true);
            }
        }

        public EquatorialCoordinateType EquatorialSystem
        {
            get
            {
                EquatorialCoordinateType equatorialSystem = EquatorialCoordinateType.equLocalTopocentric;
                tl.LogMessage("DeclinationRate", "Get - " + equatorialSystem.ToString());
                return equatorialSystem;
            }
        }

        public void FindHome()
        {
            tl.LogMessage("FindHome", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("FindHome");
        }

        public double FocalLength
        {
            get
            {
                tl.LogMessage("FocalLength Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("FocalLength", false);
            }
        }

        public double GuideRateDeclination
        {
            get
            {
                return decGuideSpeedInt / 1000.0;
            }
            set
            {
                tl.LogMessage("GuideRateDeclination Set", value.ToString());
                decGuideSpeedInt = (int)(value * 1000); 
                sendCommand(Commands.CommandSetDecGuideSpeed(value));
            }
        }

        public double GuideRateRightAscension
        {
            get
            {
                return raGuideSpeedInt / 1000.0;
            }
            set
            {
                raGuideSpeedInt = (int)(value * 1000);
                sendCommand(Commands.CommandSetRaGuideSpeed(value));
            }
        }

        public bool IsPulseGuiding
        {
            get
            {
                return pulseGuiding;
            }
        }

        public void MoveAxis(TelescopeAxes Axis, double Rate)
        {
            tl.LogMessage("MoveAxis", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("MoveAxis");
        }

        public void Park()
        {
            tl.LogMessage("Park", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("Park");
        }

        public void PulseGuide(GuideDirections Direction, int Duration)
        {
            if (Duration > short.MaxValue)
            {
                throw new ASCOM.InvalidValueException("PulseGuide Duration too long");
            }
            pulseGuiding = true;
            sendCommand(Commands.CommandPulseGuidingSpeed(Direction, (short)Duration));
        }

        public double RightAscension
        {
            get; private set;
        }

        public double RightAscensionRate
        {
            get
            {
                return raSpeedInt / 1000.0;
            }
            set
            {
                tl.LogMessage("RightAscensionRate Set", value.ToString());
                raSpeedInt = (int)(value * 1000);
                formControlPanel.UpdateAsync();
                sendCommand(Commands.CommandSetRaSpeed(value));
            }
        }

        public void SetPark()
        {
            tl.LogMessage("SetPark", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("SetPark");
        }

        bool sideOfPierIsWest;

        public PierSide SideOfPier
        {
            get
            {
                return sideOfPierIsWest ? PierSide.pierWest : PierSide.pierEast;
            }
            set
            {
                sideOfPierIsWest = value == PierSide.pierWest;
                formControlPanel.UpdateAsync();
                sendCommand(Commands.CommandSetSideOfPier(sideOfPierIsWest));
            }
        }

        public double SiderealTime
        {
            get
            {
                // get greenwich sidereal time: https://en.wikipedia.org/wiki/Sidereal_time
                //double siderealTime = (18.697374558 + 24.065709824419081 * (utilities.DateUTCToJulian(DateTime.UtcNow) - 2451545.0));

                // alternative using NOVAS 3.1
                double siderealTime = 0.0;
                using (var novas = new ASCOM.Astrometry.NOVAS.NOVAS31())
                {
                    var jd = utilities.DateUTCToJulian(DateTime.UtcNow);
                    novas.SiderealTime(jd, 0, novas.DeltaT(jd),
                        ASCOM.Astrometry.GstType.GreenwichApparentSiderealTime,
                        ASCOM.Astrometry.Method.EquinoxBased,
                        ASCOM.Astrometry.Accuracy.Reduced, ref siderealTime);
                }
                // allow for the longitude
                siderealTime += SiteLongitude / 360.0 * 24.0;
                // reduce to the range 0 to 24 hours
                siderealTime = siderealTime % 24.0;
                tl.LogMessage("SiderealTime", "Get - " + siderealTime.ToString());
                return siderealTime;
            }
        }

        public double SiteElevation
        {
            get
            {
                tl.LogMessage("SiteElevation Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("SiteElevation", false);
            }
            set
            {
                tl.LogMessage("SiteElevation Set", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("SiteElevation", true);
            }
        }

        public double SiteLatitude
        {
            get
            {
                tl.LogMessage("SiteLatitude Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("SiteLatitude", false);
            }
            set
            {
                tl.LogMessage("SiteLatitude Set", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("SiteLatitude", true);
            }
        }

        public double SiteLongitude
        {
            get
            {
                tl.LogMessage("SiteLongitude Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("SiteLongitude", false);
            }
            set
            {
                tl.LogMessage("SiteLongitude Set", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("SiteLongitude", true);
            }
        }

        public short SlewSettleTime
        {
            get
            {
                tl.LogMessage("SlewSettleTime Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("SlewSettleTime", false);
            }
            set
            {
                tl.LogMessage("SlewSettleTime Set", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("SlewSettleTime", true);
            }
        }

        public void SlewToAltAz(double Azimuth, double Altitude)
        {
            tl.LogMessage("SlewToAltAz", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("SlewToAltAz");
        }

        public void SlewToAltAzAsync(double Azimuth, double Altitude)
        {
            tl.LogMessage("SlewToAltAzAsync", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("SlewToAltAzAsync");
        }

        public void SlewToCoordinates(double RightAscension, double Declination)
        {
            tl.LogMessage("Slew", "Sync slewing");
            SlewToCoordinatesAsync(RightAscension, Declination);
            tl.LogMessage("Slew", "Sync slew finished");
        }

        public void SlewToCoordinatesAsync(double RightAscension, double Declination)
        {
            tl.LogMessage("Slew", "RA: " + RightAscension + ", Dec: " + Declination);
            int raMillis = (int)(RightAscension * (3600.0 * 1000.0));
            int decMillis = (int)(Declination * (double)DAY_MILLIS / 360.0);
            sendCommand(Commands.CommandSlewToCoordinates(raMillis, decMillis));
        }

        public void SlewToTarget()
        {
            this.SlewToCoordinates(TargetRightAscension, TargetDeclination);
        }

        public void SlewToTargetAsync()
        {
            this.SlewToCoordinatesAsync(TargetRightAscension, TargetDeclination);
        }

        public bool Slewing { get; set; }

        public void SyncToAltAz(double Azimuth, double Altitude)
        {
            tl.LogMessage("SyncToAltAz", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("SyncToAltAz");
        }

        public void SyncToCoordinates(double RightAscension, double Declination)
        {
            tl.LogMessage("Sync", "RA: " + RightAscension + ", Dec: " + Declination);
            int raMillis = (int)(RightAscension * (3600.0 * 1000.0));
            int decMillis = (int)(Declination * (double) DAY_MILLIS / 360.0);
            sendCommand(Commands.CommandSyncToCoordinates(raMillis, decMillis));
        }

        public void SyncToTarget()
        {
            this.SyncToCoordinates(TargetRightAscension, TargetDeclination);
        }

        public double TargetDeclination
        {
            get;
            set;
        }

        public double TargetRightAscension
        {
            get;
            set;
        }

        public bool Tracking
        {
            get
            {
                return tracking;
            }
            set
            {
                tracking = value;
                formControlPanel.UpdateAsync();
                sendCommand(Commands.CommandSetTracking(value));
            }
        }

        public DriveRates TrackingRate
        {
            get
            {
                return DriveRates.driveSidereal;
            }
            set
            {
                if (value != DriveRates.driveSidereal)
                {
                    throw new ASCOM.ValueNotSetException("TrackingRate");
                }
            }
        }

        public ITrackingRates TrackingRates
        {
            get
            {
                ITrackingRates trackingRates = new TrackingRates();
                tl.LogMessage("TrackingRates", "Get - ");
                foreach (DriveRates driveRate in trackingRates)
                {
                    tl.LogMessage("TrackingRates", "Get - " + driveRate.ToString());
                }
                return trackingRates;
            }
        }

        public DateTime UTCDate
        {
            get
            {
                DateTime utcDate = DateTime.UtcNow;
                tl.LogMessage("TrackingRates", "Get - " + String.Format("MM/dd/yy HH:mm:ss", utcDate));
                return utcDate;
            }
            set
            {
                tl.LogMessage("UTCDate Set", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("UTCDate", true);
            }
        }

        public void Unpark()
        {
            tl.LogMessage("Unpark", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("Unpark");
        }

        #endregion

        #region Private properties and methods
        // here are some useful properties and methods that can be used as required
        // to help with driver development

        #region ASCOM Registration

        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        /// <summary>
        /// Register or unregister the driver with the ASCOM Platform.
        /// This is harmless if the driver is already registered/unregistered.
        /// </summary>
        /// <param name="bRegister">If <c>true</c>, registers the driver, otherwise unregisters it.</param>
        private static void RegUnregASCOM(bool bRegister)
        {
            using (var P = new ASCOM.Utilities.Profile())
            {
                P.DeviceType = "Telescope";
                if (bRegister)
                {
                    P.Register(driverID, driverDescription);
                }
                else
                {
                    P.Unregister(driverID);
                }
            }
        }

        /// <summary>
        /// This function registers the driver with the ASCOM Chooser and
        /// is called automatically whenever this class is registered for COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is successfully built.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During setup, when the installer registers the assembly for COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually register a driver with ASCOM.
        /// </remarks>
        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        /// <summary>
        /// This function unregisters the driver from the ASCOM Chooser and
        /// is called automatically whenever this class is unregistered from COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is cleaned or prior to rebuilding.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During uninstall, when the installer unregisters the assembly from COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually unregister a driver from ASCOM.
        /// </remarks>
        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }

        #endregion

        /// <summary>
        /// Returns true if there is a valid connection to the driver hardware
        /// </summary>
        private bool IsConnected
        {
            get
            {
                return client != null;
            }
        }

        /// <summary>
        /// Use this function to throw an exception if we aren't connected to the hardware
        /// </summary>
        /// <param name="message"></param>
        private void CheckConnected(string message)
        {
            if (!IsConnected)
            {
                throw new ASCOM.NotConnectedException(message);
            }
        }

        /// <summary>
        /// Read the device configuration from the ASCOM Profile store
        /// </summary>
        internal void ReadProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Telescope";
                tl.Enabled = Convert.ToBoolean(driverProfile.GetValue(driverID, traceStateProfileName, string.Empty, traceStateDefault));
                host = driverProfile.GetValue(driverID, hostProfileName, string.Empty, hostDefault);
                port = int.Parse(driverProfile.GetValue(driverID, portProfileName, string.Empty, portDefault));
                autoDetect = !("false".Equals(driverProfile.GetValue(driverID, autoDetectProfileName, string.Empty, autoDetectDefault)));
            }
        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal void WriteProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Telescope";
                driverProfile.WriteValue(driverID, traceStateProfileName, tl.Enabled.ToString());
                driverProfile.WriteValue(driverID, hostProfileName, host);
                driverProfile.WriteValue(driverID, portProfileName, port.ToString());
                driverProfile.WriteValue(driverID, autoDetectProfileName, autoDetect.ToString());
            }
        }

        /// <summary>
        /// Log helper function that takes formatted strings and arguments
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        internal static void LogMessage(string identifier, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            tl.LogMessage(identifier, msg);
        }
        #endregion
    }
}
