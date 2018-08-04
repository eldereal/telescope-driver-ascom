using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ASCOM.DeviceInterface;

namespace ASCOM.ElmsRemoteTelescopeUdp
{
    public struct Rect
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
    }

    public partial class FormControlPanel : Form
    {

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);
        
        [DllImport("user32.dll")]
        static extern bool IsWindowVisible(IntPtr hWnd);

        public IntPtr OwnerWindowHandle { get; set; }
        public Telescope Driver { get; set; }
        public bool Ready = false;

        Control[] SlewDisabledCotrols;

        public FormControlPanel(IntPtr owner, Telescope driver)
        {
            InitializeComponent();
            SlewDisabledCotrols = new Control[]{
                buttonDec1,
                buttonDec2,
                buttonDec4,
                buttonDec8,
                buttonDecN1,
                buttonDecN2,
                buttonDecN4,
                buttonDecN8,
                buttonDecZero,
                buttonRa1,
                buttonRa2,
                buttonRa4,
                buttonRa8,
                buttonRaN1,
                buttonRaN2,
                buttonRaN4,
                buttonRaN8,
                buttonRaZero,
                btnResetAllSpeed,
                raTrack,
                decTrack,
                radioSideOfPierEast,
                radioSideOfPierWest
            };
            OwnerWindowHandle = owner;
            Driver = driver;
            Rect rect = new Rect();
            GetWindowRect(OwnerWindowHandle, ref rect);
            this.Top = rect.Top;
            this.Left = rect.Right;
            this.Height = rect.Bottom - rect.Top;
            IWin32Window window = Control.FromHandle(OwnerWindowHandle);
            this.Show(window);
        }

        private void FormControlPanel_Load(object sender, EventArgs e)
        {
            Ready = true;
            UpdateView();
        }

        private void FormControlPanel_Shown(object sender, EventArgs e)
        {
            timerPosition.Enabled = true;
        }

        private void timerPosition_Tick(object sender, EventArgs e)
        {
            Rect rect = new Rect();
            GetWindowRect(OwnerWindowHandle, ref rect);
            this.Top = rect.Top;
            this.Left = rect.Right;
            this.Height = rect.Bottom - rect.Top;
            this.Visible = IsWindowVisible(OwnerWindowHandle);
        }

        public double RASpeed
        {
            get
            {
                return Driver.RightAscensionRate / 15;
            }
            set
            {
                Driver.RightAscensionRate = value * 15;
            }
        }

        public double DecSpeed
        {
            get
            {
                return Driver.DeclinationRate / 15;
            }
            set
            {
                Driver.DeclinationRate = value * 15;
            }
        }

        bool Tracking
        {
            get
            {
                return Driver.Tracking;
            }
            set
            {
                Driver.Tracking = value;
            }
        }

        public void UpdateView()
        {
            raLabel.Text = "RA: " + RASpeed + " cycles/sidereal day";
            decLabel.Text = "Dec: " + DecSpeed + " cycles/day";
            raTrack.Value = Math.Max(Math.Min((int)((RASpeed + 15) * 100), raTrack.Maximum), raTrack.Minimum);
            decTrack.Value = Math.Max(Math.Min((int)((DecSpeed + 15) * 100), decTrack.Maximum), decTrack.Minimum);
            raTrackingCheck.Checked = Tracking;
            if (Driver.SideOfPier == PierSide.pierEast)
            {
                labelDecLarge.Text = "N";
                labelDecLess.Text = "S";
                radioSideOfPierEast.Checked = true;
                radioSideOfPierWest.Checked = false;
            }
            else
            {
                labelDecLarge.Text = "S";
                labelDecLess.Text = "N";
                radioSideOfPierEast.Checked = false;
                radioSideOfPierWest.Checked = true;
            }
            foreach (Control c in SlewDisabledCotrols)
            {
                c.Enabled = !Driver.Slewing;
            }
        }

        delegate void UpdateViewDelegate();

        public void UpdateAsync()
        {
            if (!Ready) return;
            UpdateViewDelegate d = this.UpdateView;
            Invoke(d);
        }

        private void setSideOfPierEvent(object sender, EventArgs e)
        {
            Driver.SideOfPier = radioSideOfPierWest.Checked ? PierSide.pierWest : PierSide.pierEast;
        }

        private void setRaSpeedEvent(object sender, EventArgs e)
        {
            RASpeed = double.Parse(((Control)sender).Tag.ToString());
            UpdateView();
        }

        private void setDecSpeedEvent(object sender, EventArgs e)
        {
            DecSpeed = double.Parse(((Control)sender).Tag.ToString());
            UpdateView();
        }

        private void btnResetAllSpeed_Click(object sender, EventArgs e)
        {
            RASpeed = 0;
            DecSpeed = 0;
            UpdateView();
        }

        private void timerRaTrack_Tick(object sender, EventArgs e)
        {
            timerRaTrack.Enabled = false;
            double minGap = 0.05;
            double raToSet = (raTrack.Value - 1500) / 100.0;
            if (Math.Abs(raToSet - RASpeed) >= minGap)
            {
                RASpeed = raToSet;
                UpdateView();
            }
        }

        private void raTrack_Scroll(object sender, ScrollEventArgs e)
        {
            timerRaTrack.Enabled = true;
        }

        private void timerDecTrack_Tick(object sender, EventArgs e)
        {
            timerDecTrack.Enabled = false;
            double decToSet = (decTrack.Value - 1500) / 100.0;
            double minGap = 0.05;
            if (Math.Abs(decToSet - DecSpeed) >= minGap)
            {
                DecSpeed = decToSet;
                UpdateView();
            }
        }

        private void decTrack_Scroll(object sender, ScrollEventArgs e)
        {
            timerDecTrack.Enabled = true;
        }
    }
}
