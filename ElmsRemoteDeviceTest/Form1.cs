﻿using System;
using System.Windows.Forms;
using SharpDX.DirectInput;

namespace ASCOM.ElmsRemoteDevice
{
    public partial class Form1 : Form
    {
        private long lastGuideStart = -1;

        private ASCOM.DriverAccess.Telescope driver;

        public Form1()
        {
            InitializeComponent();
            SetUIState();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsConnected)
                driver.Connected = false;

            Properties.Settings.Default.Save();
        }

        private void buttonChoose_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DriverId = ASCOM.DriverAccess.Telescope.Choose(Properties.Settings.Default.DriverId);
            SetUIState();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (IsConnected)
            {
                driver.Connected = false;
            }
            else
            {
                driver = new ASCOM.DriverAccess.Telescope(Properties.Settings.Default.DriverId);
                driver.Connected = true;
            }
            SetUIState();
        }

        private void SetUIState()
        {
            buttonConnect.Enabled = !string.IsNullOrEmpty(Properties.Settings.Default.DriverId);
            buttonChoose.Enabled = !IsConnected;
            buttonConnect.Text = IsConnected ? "Disconnect" : "Connect";
            updateLabel();
        }

        private bool IsConnected
        {
            get
            {
                return ((this.driver != null) && (driver.Connected == true));
            }
        }

        public double raSpeed
        {
            get
            {
                if (!IsConnected) return 0;
                return driver.RightAscensionRate / 15;
            }
            set
            {
                if (IsConnected)
                {
                    driver.RightAscensionRate = value * 15;
                }
            }
        }

        double raGuideSpeed
        {
            get
            {
                if (!IsConnected) return 0;
                return driver.GuideRateRightAscension / 15;
            }
            set
            {
                if (IsConnected)
                {
                    driver.GuideRateRightAscension = value * 15;
                }
            }
        }

        public double decSpeed
        {
            get
            {
                if (!IsConnected) return 0;
                return driver.DeclinationRate / 15;
            }
            set
            {
                if (IsConnected)
                {
                    driver.DeclinationRate = value * 15;
                }
            }
        }

        double decGuideSpeed
        {
            get
            {
                if (!IsConnected) return 0;
                return driver.GuideRateDeclination / 15;
            }
            set
            {
                if (IsConnected)
                {
                    driver.GuideRateDeclination = value * 15;
                }
            }
        }

        bool tracking
        {
            get
            {
                if (!IsConnected) return false;
                return driver.Tracking;
            }
            set
            {
                if (IsConnected)
                {
                    driver.Tracking = value;
                }
            }
        }

        public void updateLabel()
        {
            raZero.Enabled = IsConnected;
            raOne.Enabled = IsConnected;
            raNegOne.Enabled = IsConnected;
            decZero.Enabled = IsConnected;
            raTrack.Enabled = IsConnected;
            decTrack.Enabled = IsConnected;
            raTrackingCheck.Enabled = IsConnected;
            btnPulseGuideEast.Enabled = IsConnected;
            btnPulseGuideWest.Enabled = IsConnected;
            btnPulseGuideNorth.Enabled = IsConnected;
            btnPulseGuideSouth.Enabled = IsConnected;
            txtRAGuideSpeed.Enabled = IsConnected;
            txtDecGuideSpeed.Enabled = IsConnected;
            txtPulseTime.Enabled = IsConnected;
            buttonGoto.Enabled = IsConnected;
            raLabel.Text = "RA: " + raSpeed;
            decLabel.Text = "Dec: " + decSpeed;
            raTrack.Value = Math.Max(Math.Min((int)((raSpeed + 15) * 100), raTrack.Maximum), raTrack.Minimum);
            decTrack.Value = Math.Max(Math.Min((int)((decSpeed + 15) * 100), decTrack.Maximum), decTrack.Minimum);
            raTrackingCheck.Checked = tracking;
            txtRAGuideSpeed.Text = raGuideSpeed.ToString();
            txtDecGuideSpeed.Text = decGuideSpeed.ToString();
        }

        private void raNegOne_Click(object sender, EventArgs e)
        {
            raSpeed = -1;
            updateLabel();
        }

        private void raZero_Click(object sender, EventArgs e)
        {
            raSpeed = 0;
            updateLabel();
        }

        private void raOne_Click(object sender, EventArgs e)
        {
            raSpeed = 1;
            updateLabel();
        }

        private void decZero_Click(object sender, EventArgs e)
        {
            decSpeed = 0;
            updateLabel();
        }

        private void raTrackingCheck_CheckedChanged(object sender, EventArgs e)
        {
            tracking = raTrackingCheck.Checked;
        }

        private void raTrack_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double minGap = 0.05;
            double raToSet = (raTrack.Value - 1500) / 100.0;
            bool set = false;
            if (Math.Abs(raToSet - raSpeed) >= minGap)
            {
                raSpeed = raToSet;
                set = true;
            }
            double decToSet = (decTrack.Value - 1500) / 100.0;
            if (Math.Abs(decToSet - decSpeed) >= minGap)
            {
                decSpeed = decToSet;
                set = true;
            }
            if (set)
            {
                updateLabel();
            }
        }

        private void btnPulseGuideNorth_Click(object sender, EventArgs e)
        {
            if (driver.IsPulseGuiding)
            {
                MessageBox.Show("Guiding");
            }
            driver.PulseGuide(DeviceInterface.GuideDirections.guideNorth, int.Parse(txtPulseTime.Text));
        }

        private void btnPulseGuideSouth_Click(object sender, EventArgs e)
        {
            if (driver.IsPulseGuiding)
            {
                MessageBox.Show("Guiding");
            }
            driver.PulseGuide(DeviceInterface.GuideDirections.guideSouth, int.Parse(txtPulseTime.Text));
        }

        private void btnPulseGuideWest_Click(object sender, EventArgs e)
        {
            if (driver.IsPulseGuiding)
            {
                MessageBox.Show("Guiding");
            }
            driver.PulseGuide(DeviceInterface.GuideDirections.guideWest, int.Parse(txtPulseTime.Text));
        }

        private void btnPulseGuideEast_Click(object sender, EventArgs e)
        {
            if (driver.IsPulseGuiding)
            {
                MessageBox.Show("Guiding");
            }
            driver.PulseGuide(DeviceInterface.GuideDirections.guideEast, int.Parse(txtPulseTime.Text));
        }

        private void txtRAGuideSpeed_Leave(object sender, EventArgs e)
        {
            double s;
            if (double.TryParse(txtRAGuideSpeed.Text, out s))
            {
                raGuideSpeed = s;
            }
        }

        private void txtDecGuideSpeed_Leave(object sender, EventArgs e)
        {
            double s;
            if (double.TryParse(txtDecGuideSpeed.Text, out s))
            {
                decGuideSpeed = s;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            bool guiding = driver != null && driver.IsPulseGuiding;
            if (guiding)
            {
                if (lastGuideStart < 0)
                {
                    lastGuideStart = DateTime.Now.Ticks;
                    labelGuiding.Text = "Guiding";
                }
            }
            else
            {
                if (lastGuideStart > 0)
                {
                    long ticks = DateTime.Now.Ticks - lastGuideStart;
                    labelGuiding.Text = "Guide Finished in " + (ticks/10000) + " ms";
                    lastGuideStart = -1;
                }
            }
            
        }

        Joystick joystick;

        private void button1_Click(object sender, EventArgs e)
        {
            if (timerJoystick.Enabled)
            {
                timerJoystick.Enabled = false;
                joystick.Dispose();
                joystick = null;
                button1.Text = "Joystick";
                joystickLabel.Text = "";
                return;

            } 
            var directInput = new DirectInput();
            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                joystickGuid = deviceInstance.InstanceGuid;

            // If Gamepad not found, look for a Joystick
            if (joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                    joystickGuid = deviceInstance.InstanceGuid;

            // If Joystick not found, throws an error
            if (joystickGuid == Guid.Empty)
            {
                MessageBox.Show("No Joystick/Gamepad found.");
                return;
            }

            joystick = new Joystick(directInput, joystickGuid);
            timerJoystick.Enabled = true;
            button1.Text = "Stop Joystick";
            
        }

        double joystickSpeed = 4;
        double joystickAccler = 4;
        double max = 32767.5;
        double min = 2767;

        private void timerJoystick_Tick(object sender, EventArgs e)
        {
            var joystickState = new JoystickState();
            joystick.Acquire();
            joystick.GetCurrentState(ref joystickState);
            double jx = joystickState.X - max;
            double accler = joystickState.Buttons[4] ? joystickAccler : 1;
            if (jx > min)
            {
                jx = accler * joystickSpeed * (jx - min) / (max - min);
            }
            else if (jx < -min)
            {
                jx = accler * joystickSpeed * (jx + min) / (max - min);
            }
            else
            {
                jx = 0;
            }
            double jy = joystickState.Y - max;
            if (jy > min)
            {
                jy = accler * joystickSpeed * (jy - min) / (max - min);
            }
            else if (jy < -min)
            {
                jy = accler * joystickSpeed * (jy + min) / (max - min);
            }
            else
            {
                jy = 0;
            }
            joystickLabel.Text = "X: " + jx + ", Y: " + jy;// +", " + string.Join("|", joystickState.Buttons);
            if (this.IsConnected)
            {
                if (Math.Abs(raSpeed - jx) > 0.01)
                {
                    raSpeed = jx;
                }
                if (Math.Abs(decSpeed - jy) > 0.01)
                {
                    decSpeed = jy;
                }
                updateLabel();
            }
        }

        private void buttonGoto_Click(object sender, EventArgs e)
        {
            FormGoto g = new FormGoto(this);
            g.Show(this);
        }

        

    }
}
