namespace ASCOM.ElmsRemoteTelescopeUdp
{
    partial class FormControlPanel
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerPosition = new System.Windows.Forms.Timer(this.components);
            this.raLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.decTrack = new System.Windows.Forms.HScrollBar();
            this.decLabel = new System.Windows.Forms.Label();
            this.buttonRaZero = new System.Windows.Forms.Button();
            this.buttonRa1 = new System.Windows.Forms.Button();
            this.buttonRaN1 = new System.Windows.Forms.Button();
            this.buttonRa2 = new System.Windows.Forms.Button();
            this.buttonRa4 = new System.Windows.Forms.Button();
            this.buttonRa8 = new System.Windows.Forms.Button();
            this.buttonRaN2 = new System.Windows.Forms.Button();
            this.buttonRaN4 = new System.Windows.Forms.Button();
            this.buttonRaN8 = new System.Windows.Forms.Button();
            this.buttonDecN2 = new System.Windows.Forms.Button();
            this.buttonDecN4 = new System.Windows.Forms.Button();
            this.buttonDecN8 = new System.Windows.Forms.Button();
            this.buttonDec8 = new System.Windows.Forms.Button();
            this.buttonDec4 = new System.Windows.Forms.Button();
            this.buttonDec2 = new System.Windows.Forms.Button();
            this.buttonDecN1 = new System.Windows.Forms.Button();
            this.buttonDec1 = new System.Windows.Forms.Button();
            this.buttonDecZero = new System.Windows.Forms.Button();
            this.btnResetAllSpeed = new System.Windows.Forms.Button();
            this.raTrackingCheck = new System.Windows.Forms.CheckBox();
            this.raTrack = new System.Windows.Forms.HScrollBar();
            this.labelDecLarge = new System.Windows.Forms.Label();
            this.labelDecLess = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioSideOfPierEast = new System.Windows.Forms.RadioButton();
            this.radioSideOfPierWest = new System.Windows.Forms.RadioButton();
            this.timerRaTrack = new System.Windows.Forms.Timer(this.components);
            this.timerDecTrack = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerPosition
            // 
            this.timerPosition.Tick += new System.EventHandler(this.timerPosition_Tick);
            // 
            // raLabel
            // 
            this.raLabel.AutoSize = true;
            this.raLabel.Location = new System.Drawing.Point(6, 22);
            this.raLabel.Name = "raLabel";
            this.raLabel.Size = new System.Drawing.Size(155, 12);
            this.raLabel.TabIndex = 0;
            this.raLabel.Text = "RA: 1 cycles/sidereal day";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.labelDecLess);
            this.groupBox1.Controls.Add(this.labelDecLarge);
            this.groupBox1.Controls.Add(this.btnResetAllSpeed);
            this.groupBox1.Controls.Add(this.buttonDecN2);
            this.groupBox1.Controls.Add(this.buttonDecN4);
            this.groupBox1.Controls.Add(this.buttonDecN8);
            this.groupBox1.Controls.Add(this.buttonDec8);
            this.groupBox1.Controls.Add(this.buttonDec4);
            this.groupBox1.Controls.Add(this.buttonDec2);
            this.groupBox1.Controls.Add(this.buttonDecN1);
            this.groupBox1.Controls.Add(this.buttonDec1);
            this.groupBox1.Controls.Add(this.buttonDecZero);
            this.groupBox1.Controls.Add(this.buttonRaN2);
            this.groupBox1.Controls.Add(this.buttonRaN4);
            this.groupBox1.Controls.Add(this.buttonRaN8);
            this.groupBox1.Controls.Add(this.buttonRa8);
            this.groupBox1.Controls.Add(this.buttonRa4);
            this.groupBox1.Controls.Add(this.buttonRa2);
            this.groupBox1.Controls.Add(this.buttonRaN1);
            this.groupBox1.Controls.Add(this.buttonRa1);
            this.groupBox1.Controls.Add(this.buttonRaZero);
            this.groupBox1.Controls.Add(this.decTrack);
            this.groupBox1.Controls.Add(this.decLabel);
            this.groupBox1.Controls.Add(this.raTrack);
            this.groupBox1.Controls.Add(this.raLabel);
            this.groupBox1.Location = new System.Drawing.Point(-1, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 203);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Speed";
            // 
            // decTrack
            // 
            this.decTrack.LargeChange = 100;
            this.decTrack.Location = new System.Drawing.Point(22, 115);
            this.decTrack.Maximum = 3000;
            this.decTrack.Name = "decTrack";
            this.decTrack.Size = new System.Drawing.Size(194, 19);
            this.decTrack.SmallChange = 10;
            this.decTrack.TabIndex = 7;
            this.decTrack.Scroll += new System.Windows.Forms.ScrollEventHandler(this.decTrack_Scroll);
            // 
            // decLabel
            // 
            this.decLabel.AutoSize = true;
            this.decLabel.Location = new System.Drawing.Point(6, 97);
            this.decLabel.Name = "decLabel";
            this.decLabel.Size = new System.Drawing.Size(107, 12);
            this.decLabel.TabIndex = 6;
            this.decLabel.Text = "Dec: 1 cycles/day";
            // 
            // buttonRaZero
            // 
            this.buttonRaZero.Location = new System.Drawing.Point(107, 61);
            this.buttonRaZero.Name = "buttonRaZero";
            this.buttonRaZero.Size = new System.Drawing.Size(26, 23);
            this.buttonRaZero.TabIndex = 8;
            this.buttonRaZero.Tag = "0";
            this.buttonRaZero.Text = "0";
            this.buttonRaZero.UseVisualStyleBackColor = true;
            this.buttonRaZero.Click += new System.EventHandler(this.setRaSpeedEvent);
            // 
            // buttonRa1
            // 
            this.buttonRa1.Location = new System.Drawing.Point(132, 61);
            this.buttonRa1.Name = "buttonRa1";
            this.buttonRa1.Size = new System.Drawing.Size(26, 23);
            this.buttonRa1.TabIndex = 9;
            this.buttonRa1.Tag = "1";
            this.buttonRa1.Text = "1";
            this.buttonRa1.UseVisualStyleBackColor = true;
            this.buttonRa1.Click += new System.EventHandler(this.setRaSpeedEvent);
            // 
            // buttonRaN1
            // 
            this.buttonRaN1.Location = new System.Drawing.Point(82, 61);
            this.buttonRaN1.Name = "buttonRaN1";
            this.buttonRaN1.Size = new System.Drawing.Size(26, 23);
            this.buttonRaN1.TabIndex = 10;
            this.buttonRaN1.Tag = "-1";
            this.buttonRaN1.Text = "-1";
            this.buttonRaN1.UseVisualStyleBackColor = true;
            this.buttonRaN1.Click += new System.EventHandler(this.setRaSpeedEvent);
            // 
            // buttonRa2
            // 
            this.buttonRa2.Location = new System.Drawing.Point(157, 61);
            this.buttonRa2.Name = "buttonRa2";
            this.buttonRa2.Size = new System.Drawing.Size(26, 23);
            this.buttonRa2.TabIndex = 11;
            this.buttonRa2.Tag = "2";
            this.buttonRa2.Text = "2";
            this.buttonRa2.UseVisualStyleBackColor = true;
            this.buttonRa2.Click += new System.EventHandler(this.setRaSpeedEvent);
            // 
            // buttonRa4
            // 
            this.buttonRa4.Location = new System.Drawing.Point(182, 61);
            this.buttonRa4.Name = "buttonRa4";
            this.buttonRa4.Size = new System.Drawing.Size(26, 23);
            this.buttonRa4.TabIndex = 12;
            this.buttonRa4.Tag = "4";
            this.buttonRa4.Text = "4";
            this.buttonRa4.UseVisualStyleBackColor = true;
            this.buttonRa4.Click += new System.EventHandler(this.setRaSpeedEvent);
            // 
            // buttonRa8
            // 
            this.buttonRa8.Location = new System.Drawing.Point(207, 61);
            this.buttonRa8.Name = "buttonRa8";
            this.buttonRa8.Size = new System.Drawing.Size(26, 23);
            this.buttonRa8.TabIndex = 13;
            this.buttonRa8.Tag = "8";
            this.buttonRa8.Text = "8";
            this.buttonRa8.UseVisualStyleBackColor = true;
            this.buttonRa8.Click += new System.EventHandler(this.setRaSpeedEvent);
            // 
            // buttonRaN2
            // 
            this.buttonRaN2.Location = new System.Drawing.Point(57, 61);
            this.buttonRaN2.Name = "buttonRaN2";
            this.buttonRaN2.Size = new System.Drawing.Size(26, 23);
            this.buttonRaN2.TabIndex = 16;
            this.buttonRaN2.Tag = "-2";
            this.buttonRaN2.Text = "-2";
            this.buttonRaN2.UseVisualStyleBackColor = true;
            this.buttonRaN2.Click += new System.EventHandler(this.setRaSpeedEvent);
            // 
            // buttonRaN4
            // 
            this.buttonRaN4.Location = new System.Drawing.Point(32, 61);
            this.buttonRaN4.Name = "buttonRaN4";
            this.buttonRaN4.Size = new System.Drawing.Size(26, 23);
            this.buttonRaN4.TabIndex = 15;
            this.buttonRaN4.Tag = "-4";
            this.buttonRaN4.Text = "-4";
            this.buttonRaN4.UseVisualStyleBackColor = true;
            this.buttonRaN4.Click += new System.EventHandler(this.setRaSpeedEvent);
            // 
            // buttonRaN8
            // 
            this.buttonRaN8.Location = new System.Drawing.Point(7, 61);
            this.buttonRaN8.Name = "buttonRaN8";
            this.buttonRaN8.Size = new System.Drawing.Size(26, 23);
            this.buttonRaN8.TabIndex = 14;
            this.buttonRaN8.Tag = "-8";
            this.buttonRaN8.Text = "-8";
            this.buttonRaN8.UseVisualStyleBackColor = true;
            this.buttonRaN8.Click += new System.EventHandler(this.setRaSpeedEvent);
            // 
            // buttonDecN2
            // 
            this.buttonDecN2.Location = new System.Drawing.Point(57, 142);
            this.buttonDecN2.Name = "buttonDecN2";
            this.buttonDecN2.Size = new System.Drawing.Size(26, 23);
            this.buttonDecN2.TabIndex = 25;
            this.buttonDecN2.Tag = "-2";
            this.buttonDecN2.Text = "-2";
            this.buttonDecN2.UseVisualStyleBackColor = true;
            this.buttonDecN2.Click += new System.EventHandler(this.setDecSpeedEvent);
            // 
            // buttonDecN4
            // 
            this.buttonDecN4.Location = new System.Drawing.Point(32, 142);
            this.buttonDecN4.Name = "buttonDecN4";
            this.buttonDecN4.Size = new System.Drawing.Size(26, 23);
            this.buttonDecN4.TabIndex = 24;
            this.buttonDecN4.Tag = "-4";
            this.buttonDecN4.Text = "-4";
            this.buttonDecN4.UseVisualStyleBackColor = true;
            this.buttonDecN4.Click += new System.EventHandler(this.setDecSpeedEvent);
            // 
            // buttonDecN8
            // 
            this.buttonDecN8.Location = new System.Drawing.Point(7, 142);
            this.buttonDecN8.Name = "buttonDecN8";
            this.buttonDecN8.Size = new System.Drawing.Size(26, 23);
            this.buttonDecN8.TabIndex = 23;
            this.buttonDecN8.Tag = "-8";
            this.buttonDecN8.Text = "-8";
            this.buttonDecN8.UseVisualStyleBackColor = true;
            this.buttonDecN8.Click += new System.EventHandler(this.setDecSpeedEvent);
            // 
            // buttonDec8
            // 
            this.buttonDec8.Location = new System.Drawing.Point(207, 142);
            this.buttonDec8.Name = "buttonDec8";
            this.buttonDec8.Size = new System.Drawing.Size(26, 23);
            this.buttonDec8.TabIndex = 22;
            this.buttonDec8.Tag = "8";
            this.buttonDec8.Text = "8";
            this.buttonDec8.UseVisualStyleBackColor = true;
            this.buttonDec8.Click += new System.EventHandler(this.setDecSpeedEvent);
            // 
            // buttonDec4
            // 
            this.buttonDec4.Location = new System.Drawing.Point(182, 142);
            this.buttonDec4.Name = "buttonDec4";
            this.buttonDec4.Size = new System.Drawing.Size(26, 23);
            this.buttonDec4.TabIndex = 21;
            this.buttonDec4.Tag = "4";
            this.buttonDec4.Text = "4";
            this.buttonDec4.UseVisualStyleBackColor = true;
            this.buttonDec4.Click += new System.EventHandler(this.setDecSpeedEvent);
            // 
            // buttonDec2
            // 
            this.buttonDec2.Location = new System.Drawing.Point(157, 142);
            this.buttonDec2.Name = "buttonDec2";
            this.buttonDec2.Size = new System.Drawing.Size(26, 23);
            this.buttonDec2.TabIndex = 20;
            this.buttonDec2.Tag = "2";
            this.buttonDec2.Text = "2";
            this.buttonDec2.UseVisualStyleBackColor = true;
            this.buttonDec2.Click += new System.EventHandler(this.setDecSpeedEvent);
            // 
            // buttonDecN1
            // 
            this.buttonDecN1.Location = new System.Drawing.Point(82, 142);
            this.buttonDecN1.Name = "buttonDecN1";
            this.buttonDecN1.Size = new System.Drawing.Size(26, 23);
            this.buttonDecN1.TabIndex = 19;
            this.buttonDecN1.Tag = "-1";
            this.buttonDecN1.Text = "-1";
            this.buttonDecN1.UseVisualStyleBackColor = true;
            this.buttonDecN1.Click += new System.EventHandler(this.setDecSpeedEvent);
            // 
            // buttonDec1
            // 
            this.buttonDec1.Location = new System.Drawing.Point(132, 142);
            this.buttonDec1.Name = "buttonDec1";
            this.buttonDec1.Size = new System.Drawing.Size(26, 23);
            this.buttonDec1.TabIndex = 18;
            this.buttonDec1.Tag = "1";
            this.buttonDec1.Text = "1";
            this.buttonDec1.UseVisualStyleBackColor = true;
            this.buttonDec1.Click += new System.EventHandler(this.setDecSpeedEvent);
            // 
            // buttonDecZero
            // 
            this.buttonDecZero.Location = new System.Drawing.Point(107, 142);
            this.buttonDecZero.Name = "buttonDecZero";
            this.buttonDecZero.Size = new System.Drawing.Size(26, 23);
            this.buttonDecZero.TabIndex = 17;
            this.buttonDecZero.Tag = "0";
            this.buttonDecZero.Text = "0";
            this.buttonDecZero.UseVisualStyleBackColor = true;
            this.buttonDecZero.Click += new System.EventHandler(this.setDecSpeedEvent);
            // 
            // btnResetAllSpeed
            // 
            this.btnResetAllSpeed.Location = new System.Drawing.Point(8, 172);
            this.btnResetAllSpeed.Name = "btnResetAllSpeed";
            this.btnResetAllSpeed.Size = new System.Drawing.Size(225, 23);
            this.btnResetAllSpeed.TabIndex = 26;
            this.btnResetAllSpeed.Text = "Stop RA and Dec";
            this.btnResetAllSpeed.UseVisualStyleBackColor = true;
            this.btnResetAllSpeed.Click += new System.EventHandler(this.btnResetAllSpeed_Click);
            // 
            // raTrackingCheck
            // 
            this.raTrackingCheck.AutoSize = true;
            this.raTrackingCheck.Location = new System.Drawing.Point(6, 8);
            this.raTrackingCheck.Name = "raTrackingCheck";
            this.raTrackingCheck.Size = new System.Drawing.Size(126, 16);
            this.raTrackingCheck.TabIndex = 0;
            this.raTrackingCheck.Text = "Sidereal Tracking";
            this.raTrackingCheck.UseVisualStyleBackColor = true;
            // 
            // raTrack
            // 
            this.raTrack.LargeChange = 100;
            this.raTrack.Location = new System.Drawing.Point(22, 39);
            this.raTrack.Maximum = 3000;
            this.raTrack.Name = "raTrack";
            this.raTrack.Size = new System.Drawing.Size(194, 19);
            this.raTrack.SmallChange = 10;
            this.raTrack.TabIndex = 5;
            this.raTrack.Scroll += new System.Windows.Forms.ScrollEventHandler(this.raTrack_Scroll);
            // 
            // labelDecLarge
            // 
            this.labelDecLarge.AutoSize = true;
            this.labelDecLarge.Location = new System.Drawing.Point(219, 119);
            this.labelDecLarge.Name = "labelDecLarge";
            this.labelDecLarge.Size = new System.Drawing.Size(11, 12);
            this.labelDecLarge.TabIndex = 27;
            this.labelDecLarge.Text = "N";
            // 
            // labelDecLess
            // 
            this.labelDecLess.AutoSize = true;
            this.labelDecLess.Location = new System.Drawing.Point(8, 120);
            this.labelDecLess.Name = "labelDecLess";
            this.labelDecLess.Size = new System.Drawing.Size(11, 12);
            this.labelDecLess.TabIndex = 28;
            this.labelDecLess.Text = "S";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(219, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 29;
            this.label5.Text = "W";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 30;
            this.label6.Text = "E";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioSideOfPierWest);
            this.groupBox2.Controls.Add(this.radioSideOfPierEast);
            this.groupBox2.Location = new System.Drawing.Point(-2, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 63);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mechanical Declination";
            // 
            // radioSideOfPierEast
            // 
            this.radioSideOfPierEast.AutoSize = true;
            this.radioSideOfPierEast.Location = new System.Drawing.Point(11, 18);
            this.radioSideOfPierEast.Name = "radioSideOfPierEast";
            this.radioSideOfPierEast.Size = new System.Drawing.Size(155, 16);
            this.radioSideOfPierEast.TabIndex = 0;
            this.radioSideOfPierEast.TabStop = true;
            this.radioSideOfPierEast.Text = "In Range [-90°, 90°]";
            this.radioSideOfPierEast.UseVisualStyleBackColor = true;
            this.radioSideOfPierEast.Click += new System.EventHandler(this.setSideOfPierEvent);
            // 
            // radioSideOfPierWest
            // 
            this.radioSideOfPierWest.AutoSize = true;
            this.radioSideOfPierWest.Location = new System.Drawing.Point(11, 38);
            this.radioSideOfPierWest.Name = "radioSideOfPierWest";
            this.radioSideOfPierWest.Size = new System.Drawing.Size(95, 16);
            this.radioSideOfPierWest.TabIndex = 1;
            this.radioSideOfPierWest.TabStop = true;
            this.radioSideOfPierWest.Text = "Out of Range";
            this.radioSideOfPierWest.UseVisualStyleBackColor = true;
            this.radioSideOfPierWest.Click += new System.EventHandler(this.setSideOfPierEvent);
            // 
            // timerRaTrack
            // 
            this.timerRaTrack.Interval = 500;
            this.timerRaTrack.Tick += new System.EventHandler(this.timerRaTrack_Tick);
            // 
            // timerDecTrack
            // 
            this.timerDecTrack.Interval = 500;
            this.timerDecTrack.Tick += new System.EventHandler(this.timerDecTrack_Tick);
            // 
            // FormControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 420);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.raTrackingCheck);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormControlPanel";
            this.ShowInTaskbar = false;
            this.Text = "Elm\'s Remote Telescope";
            this.Load += new System.EventHandler(this.FormControlPanel_Load);
            this.Shown += new System.EventHandler(this.FormControlPanel_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerPosition;
        private System.Windows.Forms.Label raLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonRaN2;
        private System.Windows.Forms.Button buttonRaN4;
        private System.Windows.Forms.Button buttonRaN8;
        private System.Windows.Forms.Button buttonRa8;
        private System.Windows.Forms.Button buttonRa4;
        private System.Windows.Forms.Button buttonRa2;
        private System.Windows.Forms.Button buttonRaN1;
        private System.Windows.Forms.Button buttonRa1;
        private System.Windows.Forms.Button buttonRaZero;
        private System.Windows.Forms.HScrollBar decTrack;
        private System.Windows.Forms.Label decLabel;
        private System.Windows.Forms.Button btnResetAllSpeed;
        private System.Windows.Forms.Button buttonDecN2;
        private System.Windows.Forms.Button buttonDecN4;
        private System.Windows.Forms.Button buttonDecN8;
        private System.Windows.Forms.Button buttonDec8;
        private System.Windows.Forms.Button buttonDec4;
        private System.Windows.Forms.Button buttonDec2;
        private System.Windows.Forms.Button buttonDecN1;
        private System.Windows.Forms.Button buttonDec1;
        private System.Windows.Forms.Button buttonDecZero;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelDecLess;
        private System.Windows.Forms.Label labelDecLarge;
        private System.Windows.Forms.HScrollBar raTrack;
        private System.Windows.Forms.CheckBox raTrackingCheck;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioSideOfPierWest;
        private System.Windows.Forms.RadioButton radioSideOfPierEast;
        private System.Windows.Forms.Timer timerRaTrack;
        private System.Windows.Forms.Timer timerDecTrack;
    }
}