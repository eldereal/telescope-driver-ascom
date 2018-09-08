using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ASCOM.DeviceInterface;

namespace ASCOM.ElmsRemoteTelescopeUdp
{
    public partial class FormControlPanel : Form
    {
        [DllImport(@"dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, uint dwAttribute, out Rect pvAttribute, int cbAttribute);

        [Serializable, StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [Flags]
        public enum DwmWindowAttribute : uint
        {
            DWMWA_NCRENDERING_ENABLED = 1,
            DWMWA_NCRENDERING_POLICY,
            DWMWA_TRANSITIONS_FORCEDISABLED,
            DWMWA_ALLOW_NCPAINT,
            DWMWA_CAPTION_BUTTON_BOUNDS,
            DWMWA_NONCLIENT_RTL_LAYOUT,
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            DWMWA_FLIP3D_POLICY,
            DWMWA_EXTENDED_FRAME_BOUNDS,
            DWMWA_HAS_ICONIC_BITMAP,
            DWMWA_DISALLOW_PEEK,
            DWMWA_EXCLUDED_FROM_PEEK,
            DWMWA_CLOAK,
            DWMWA_CLOAKED,
            DWMWA_FREEZE_REPRESENTATION,
            DWMWA_LAST
        }

        private static Rect getFrameBounds(IntPtr handle)
        {
            Rect rect;
            var result = DwmGetWindowAttribute(handle, (uint) DwmWindowAttribute.DWMWA_EXTENDED_FRAME_BOUNDS,
                out rect, Marshal.SizeOf(typeof(Rect)));
            return rect;
        }

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);
        
        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

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
            Rect rect = getFrameBounds(OwnerWindowHandle);
            this.Left = rect.Right - (int)SystemParameters.FixedFrameVerticalBorderWidth;
            this.Top = rect.Top;
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
            Rect rect = getFrameBounds(OwnerWindowHandle);
            this.Left = rect.Right - (int)SystemParameters.FixedFrameVerticalBorderWidth;
            this.Top = rect.Top;
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

        private void buttonSetSpeedRatio_Click(object sender, EventArgs e)
        {
            double ratio;
            if (!double.TryParse(txtSpeedRatio.Text, out ratio))
            {
                System.Windows.Forms.MessageBox.Show("请输入一个数字");
            }
            Driver.SetSpeedRatio(ratio);
        }
    }
}
