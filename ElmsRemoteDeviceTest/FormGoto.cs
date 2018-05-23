using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ASCOM.ElmsRemoteDevice
{
    public partial class FormGoto : Form
    {
        Regex[] Patterns = new Regex[]{
            new Regex("赤经/赤纬\\s*\\(当前\\):\\s*(\\d+)h(\\d+)m([0-9\\.]+)s/([+-]?\\d+)°(\\d+)'([0-9\\.]+)\""),
            new Regex("赤经/赤纬\\s*\\(J2000.0\\):\\s*(\\d+)h(\\d+)m([0-9\\.]+)s/([+-]?\\d+)°(\\d+)'([0-9\\.]+)\"")
        };

        

        double RaUnitSpeed = 360 / 86164.1;
        double DecUnitSpeed = 360 / 86400.0;

        double raSync;
        double decSync;
        bool syncReady; 
        double raGoto;
        double decGoto;
        bool gotoReady;

        Form1 main;

        public FormGoto(Form1 main)
        {            
            InitializeComponent();
            this.main = main;
        }

        private void textBoxSync_TextChanged(object sender, EventArgs e)
        {
            foreach (Regex p in Patterns) {
                Match m = p.Match(textBoxSync.Text);
                if (m.Success)
                {
                    double raH = double.Parse(m.Groups[1].Value);
                    double raM = double.Parse(m.Groups[2].Value);
                    double raS = double.Parse(m.Groups[3].Value);
                    double decD = double.Parse(m.Groups[4].Value);
                    double decM = double.Parse(m.Groups[5].Value);
                    double decS = double.Parse(m.Groups[6].Value);
                    double raDegree = raH * 15 + raM * 15 / 60 + raS * 15 / 3600;
                    double decDegree = decD + decM / 60 + decS / 3600;
                    raSync = raDegree;
                    decSync = decDegree;
                    syncReady = true;
                    labelSync.Text = raSync.ToString("0.0000") + "/" + decSync.ToString("0.0000");
                    calc();
                    return;
                }
            }
            labelSync.Text = "N/A";
            syncReady = false;
            calc();
        }

        private void textBoxGoto_TextChanged(object sender, EventArgs e)
        {
            foreach (Regex p in Patterns)
            {
                Match m = p.Match(textBoxGoto.Text);
                if (m.Success)
                {
                    double raH = double.Parse(m.Groups[1].Value);
                    double raM = double.Parse(m.Groups[2].Value);
                    double raS = double.Parse(m.Groups[3].Value);
                    double decD = double.Parse(m.Groups[4].Value);
                    double decM = double.Parse(m.Groups[5].Value);
                    double decS = double.Parse(m.Groups[6].Value);
                    double raDegree = raH * 15 + raM * 15 / 60 + raS * 15 / 3600;
                    double decDegree = decD + decM / 60 + decS / 3600;
                    raGoto = raDegree;
                    decGoto = decDegree;
                    gotoReady = true;
                    calc();
                    labelGoto.Text = raGoto.ToString("0.0000") + "/" + decGoto.ToString("0.0000");
                    return;
                }
            }
            labelGoto.Text = "N/A";
            gotoReady = false;
            calc();
        }

        double raSpeedScale;
        double decSpeedScale;
        double raTime;
        double decTime;
        double raSpeed;
        double decSpeed;
        bool canGoto;

        void calc()
        {
            if (!gotoReady || !syncReady)
            {
                labelRATime.Text = "RA Time: N/A";
                labelDecTime.Text = "Dec Time: N/A";
                canGoto = false;
            }
            else
            {
                canGoto = false;
                if (double.TryParse(comboBoxRaSpeed.Text, out raSpeedScale))
                {
                    raTime = (raGoto - raSync) / RaUnitSpeed / raSpeedScale;
                    if (raTime < 0)
                    {
                        raSpeed = -raSpeedScale;
                        raTime = -raTime;
                    }
                    else
                    {
                        raSpeed = raSpeedScale;
                    }
                    labelRATime.Text = "RA Time: " + raTime.ToString("0");
                    canGoto = true;
                }
                else
                {
                    labelRATime.Text = "RA Time: N/A";
                    canGoto = false;
                }

                if (double.TryParse(comboBoxDecSpeed.Text, out decSpeedScale))
                {
                    decTime = Math.Abs((decGoto - decSync) / DecUnitSpeed / decSpeedScale);
                    if (decTime < 0)
                    {
                        decSpeed = -decSpeedScale;
                        decTime = -decTime;
                    }
                    else
                    {
                        decSpeed = decSpeedScale;
                    }
                    labelDecTime.Text = "Dec Time: " + decTime.ToString("0");
                }
                else
                {
                    labelDecTime.Text = "Dec Time: N/A";
                    canGoto = false;
                }
            }            
            buttonGoto.Enabled = canGoto;
        }

        private void comboBoxDecSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            calc();
        }

        private void comboBoxRaSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            calc();
        }

        private void textBoxSync_Enter(object sender, EventArgs e)
        {
            textBoxSync.Focus();
            textBoxSync.SelectAll();
        }

        private void textBoxGoto_Enter(object sender, EventArgs e)
        {
            textBoxGoto.Focus();
            textBoxGoto.SelectAll();
        }

        private void textBoxSync_DoubleClick(object sender, EventArgs e)
        {
            textBoxSync.SelectAll();
        }

        private void textBoxGoto_DoubleClick(object sender, EventArgs e)
        {
            textBoxGoto.SelectAll();
        }

        private void buttonGoto_Click(object sender, EventArgs e)
        {
            calc();
            if (!canGoto) return;
            main.raSpeed = raSpeed;
            main.decSpeed = decSpeed;
            main.updateLabel();
            textBoxGoto.Enabled = false;
            textBoxSync.Enabled = false;
            comboBoxDecSpeed.Enabled = false;
            comboBoxRaSpeed.Enabled = false;
            buttonGoto.Enabled = false;
            buttonCancel.Text = "Stop";
            timer.Enabled = true;
        }

        private void FormGoto_Load(object sender, EventArgs e)
        {
            calc();
        }

        void stop()
        {
            textBoxGoto.Enabled = true;
            textBoxSync.Enabled = true;
            comboBoxDecSpeed.Enabled = true;
            comboBoxRaSpeed.Enabled = true;
            buttonGoto.Enabled = true;
            buttonCancel.Text = "Close";
            timer.Enabled = false;
            main.raSpeed = 0;
            main.decSpeed = 0;
            main.updateLabel();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                stop();
            }
            else
            {
                this.Close();
            }
            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (0 < raTime && raTime < 1)
            {
                main.raSpeed = 0;
                main.updateLabel();
            }
            if (0 < decTime && decTime < 1)
            {
                main.decSpeed = 0;
                main.updateLabel();
            }
            raTime -= 1;
            decTime -= 1;
            if (raTime > 0)
            {
                labelRATime.Text = "RA Time: " + raTime.ToString("0");
            }
            else
            {
                labelRATime.Text = "RA Time: 0";
            }
            if (decTime > 0)
            {
                labelDecTime.Text = "Dec Time: " + decTime.ToString("0");
            }
            else
            {
                labelDecTime.Text = "Dec Time: 0";
            }
            if (raTime <= 0 && decTime <= 0)
            {
                stop();
            }
        }
    }
}