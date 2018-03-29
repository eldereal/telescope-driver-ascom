namespace ASCOM.ElmsRemoteDevice
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonChoose = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.labelDriverId = new System.Windows.Forms.Label();
            this.decTrack = new System.Windows.Forms.VScrollBar();
            this.raTrack = new System.Windows.Forms.HScrollBar();
            this.raLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelGuiding = new System.Windows.Forms.Label();
            this.txtDecGuideSpeed = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRAGuideSpeed = new System.Windows.Forms.TextBox();
            this.txtPulseTime = new System.Windows.Forms.TextBox();
            this.btnPulseGuideEast = new System.Windows.Forms.Button();
            this.btnPulseGuideSouth = new System.Windows.Forms.Button();
            this.btnPulseGuideWest = new System.Windows.Forms.Button();
            this.btnPulseGuideNorth = new System.Windows.Forms.Button();
            this.decLabel = new System.Windows.Forms.Label();
            this.raNegOne = new System.Windows.Forms.Button();
            this.raZero = new System.Windows.Forms.Button();
            this.raOne = new System.Windows.Forms.Button();
            this.decZero = new System.Windows.Forms.Button();
            this.raTrackingCheck = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.txtRaSet = new System.Windows.Forms.TextBox();
            this.btnRaSet = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonChoose
            // 
            this.buttonChoose.BackColor = System.Drawing.Color.DimGray;
            this.buttonChoose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonChoose.Location = new System.Drawing.Point(309, 9);
            this.buttonChoose.Name = "buttonChoose";
            this.buttonChoose.Size = new System.Drawing.Size(72, 21);
            this.buttonChoose.TabIndex = 0;
            this.buttonChoose.Text = "Choose";
            this.buttonChoose.UseVisualStyleBackColor = false;
            this.buttonChoose.Click += new System.EventHandler(this.buttonChoose_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.BackColor = System.Drawing.Color.DimGray;
            this.buttonConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConnect.Location = new System.Drawing.Point(309, 36);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(72, 21);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = false;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // labelDriverId
            // 
            this.labelDriverId.BackColor = System.Drawing.Color.DimGray;
            this.labelDriverId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDriverId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.ElmsRemoteDevice.Properties.Settings.Default, "DriverId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.labelDriverId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelDriverId.Location = new System.Drawing.Point(12, 37);
            this.labelDriverId.Name = "labelDriverId";
            this.labelDriverId.Size = new System.Drawing.Size(291, 20);
            this.labelDriverId.TabIndex = 2;
            this.labelDriverId.Text = global::ASCOM.ElmsRemoteDevice.Properties.Settings.Default.DriverId;
            this.labelDriverId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // decTrack
            // 
            this.decTrack.LargeChange = 100;
            this.decTrack.Location = new System.Drawing.Point(364, 112);
            this.decTrack.Maximum = 3000;
            this.decTrack.Name = "decTrack";
            this.decTrack.Size = new System.Drawing.Size(17, 263);
            this.decTrack.SmallChange = 10;
            this.decTrack.TabIndex = 3;
            // 
            // raTrack
            // 
            this.raTrack.LargeChange = 100;
            this.raTrack.Location = new System.Drawing.Point(9, 356);
            this.raTrack.Maximum = 3000;
            this.raTrack.Name = "raTrack";
            this.raTrack.Size = new System.Drawing.Size(352, 19);
            this.raTrack.SmallChange = 10;
            this.raTrack.TabIndex = 4;
            this.raTrack.Scroll += new System.Windows.Forms.ScrollEventHandler(this.raTrack_Scroll);
            // 
            // raLabel
            // 
            this.raLabel.AutoSize = true;
            this.raLabel.BackColor = System.Drawing.Color.DimGray;
            this.raLabel.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.raLabel.Location = new System.Drawing.Point(3, 8);
            this.raLabel.Name = "raLabel";
            this.raLabel.Size = new System.Drawing.Size(65, 21);
            this.raLabel.TabIndex = 5;
            this.raLabel.Text = "RA: 1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.labelGuiding);
            this.panel1.Controls.Add(this.txtDecGuideSpeed);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtRAGuideSpeed);
            this.panel1.Controls.Add(this.txtPulseTime);
            this.panel1.Controls.Add(this.btnPulseGuideEast);
            this.panel1.Controls.Add(this.btnPulseGuideSouth);
            this.panel1.Controls.Add(this.btnPulseGuideWest);
            this.panel1.Controls.Add(this.btnPulseGuideNorth);
            this.panel1.Controls.Add(this.decLabel);
            this.panel1.Controls.Add(this.raLabel);
            this.panel1.Location = new System.Drawing.Point(9, 112);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 241);
            this.panel1.TabIndex = 6;
            // 
            // labelGuiding
            // 
            this.labelGuiding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGuiding.AutoSize = true;
            this.labelGuiding.Location = new System.Drawing.Point(143, 57);
            this.labelGuiding.Name = "labelGuiding";
            this.labelGuiding.Size = new System.Drawing.Size(71, 12);
            this.labelGuiding.TabIndex = 15;
            this.labelGuiding.Text = "Not Guiding";
            this.labelGuiding.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDecGuideSpeed
            // 
            this.txtDecGuideSpeed.Location = new System.Drawing.Point(102, 211);
            this.txtDecGuideSpeed.Name = "txtDecGuideSpeed";
            this.txtDecGuideSpeed.Size = new System.Drawing.Size(100, 21);
            this.txtDecGuideSpeed.TabIndex = 14;
            this.txtDecGuideSpeed.Leave += new System.EventHandler(this.txtDecGuideSpeed_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "Dec Guide Speed";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "RA Guide Speed";
            // 
            // txtRAGuideSpeed
            // 
            this.txtRAGuideSpeed.Location = new System.Drawing.Point(102, 178);
            this.txtRAGuideSpeed.Name = "txtRAGuideSpeed";
            this.txtRAGuideSpeed.Size = new System.Drawing.Size(100, 21);
            this.txtRAGuideSpeed.TabIndex = 0;
            this.txtRAGuideSpeed.Leave += new System.EventHandler(this.txtRAGuideSpeed_Leave);
            // 
            // txtPulseTime
            // 
            this.txtPulseTime.Location = new System.Drawing.Point(139, 108);
            this.txtPulseTime.Name = "txtPulseTime";
            this.txtPulseTime.Size = new System.Drawing.Size(74, 21);
            this.txtPulseTime.TabIndex = 11;
            this.txtPulseTime.Text = "750";
            this.txtPulseTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnPulseGuideEast
            // 
            this.btnPulseGuideEast.Location = new System.Drawing.Point(219, 106);
            this.btnPulseGuideEast.Name = "btnPulseGuideEast";
            this.btnPulseGuideEast.Size = new System.Drawing.Size(75, 23);
            this.btnPulseGuideEast.TabIndex = 10;
            this.btnPulseGuideEast.Text = "East";
            this.btnPulseGuideEast.UseVisualStyleBackColor = true;
            this.btnPulseGuideEast.Click += new System.EventHandler(this.btnPulseGuideEast_Click);
            // 
            // btnPulseGuideSouth
            // 
            this.btnPulseGuideSouth.Location = new System.Drawing.Point(139, 135);
            this.btnPulseGuideSouth.Name = "btnPulseGuideSouth";
            this.btnPulseGuideSouth.Size = new System.Drawing.Size(75, 23);
            this.btnPulseGuideSouth.TabIndex = 9;
            this.btnPulseGuideSouth.Text = "South";
            this.btnPulseGuideSouth.UseVisualStyleBackColor = true;
            this.btnPulseGuideSouth.Click += new System.EventHandler(this.btnPulseGuideSouth_Click);
            // 
            // btnPulseGuideWest
            // 
            this.btnPulseGuideWest.Location = new System.Drawing.Point(58, 106);
            this.btnPulseGuideWest.Name = "btnPulseGuideWest";
            this.btnPulseGuideWest.Size = new System.Drawing.Size(75, 23);
            this.btnPulseGuideWest.TabIndex = 8;
            this.btnPulseGuideWest.Text = "West";
            this.btnPulseGuideWest.UseVisualStyleBackColor = true;
            this.btnPulseGuideWest.Click += new System.EventHandler(this.btnPulseGuideWest_Click);
            // 
            // btnPulseGuideNorth
            // 
            this.btnPulseGuideNorth.Location = new System.Drawing.Point(139, 81);
            this.btnPulseGuideNorth.Name = "btnPulseGuideNorth";
            this.btnPulseGuideNorth.Size = new System.Drawing.Size(75, 23);
            this.btnPulseGuideNorth.TabIndex = 7;
            this.btnPulseGuideNorth.Text = "North";
            this.btnPulseGuideNorth.UseVisualStyleBackColor = true;
            this.btnPulseGuideNorth.Click += new System.EventHandler(this.btnPulseGuideNorth_Click);
            // 
            // decLabel
            // 
            this.decLabel.AutoSize = true;
            this.decLabel.BackColor = System.Drawing.Color.DimGray;
            this.decLabel.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.decLabel.Location = new System.Drawing.Point(3, 29);
            this.decLabel.Name = "decLabel";
            this.decLabel.Size = new System.Drawing.Size(76, 21);
            this.decLabel.TabIndex = 6;
            this.decLabel.Text = "Dec: 1";
            // 
            // raNegOne
            // 
            this.raNegOne.BackColor = System.Drawing.Color.DimGray;
            this.raNegOne.FlatAppearance.BorderSize = 0;
            this.raNegOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.raNegOne.Location = new System.Drawing.Point(127, 378);
            this.raNegOne.Name = "raNegOne";
            this.raNegOne.Size = new System.Drawing.Size(28, 23);
            this.raNegOne.TabIndex = 9;
            this.raNegOne.Text = "-1";
            this.raNegOne.UseVisualStyleBackColor = false;
            this.raNegOne.Click += new System.EventHandler(this.raNegOne_Click);
            // 
            // raZero
            // 
            this.raZero.BackColor = System.Drawing.Color.DimGray;
            this.raZero.FlatAppearance.BorderSize = 0;
            this.raZero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.raZero.Location = new System.Drawing.Point(161, 378);
            this.raZero.Name = "raZero";
            this.raZero.Size = new System.Drawing.Size(28, 23);
            this.raZero.TabIndex = 10;
            this.raZero.Text = "0";
            this.raZero.UseVisualStyleBackColor = false;
            this.raZero.Click += new System.EventHandler(this.raZero_Click);
            // 
            // raOne
            // 
            this.raOne.BackColor = System.Drawing.Color.DimGray;
            this.raOne.FlatAppearance.BorderSize = 0;
            this.raOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.raOne.Location = new System.Drawing.Point(195, 378);
            this.raOne.Name = "raOne";
            this.raOne.Size = new System.Drawing.Size(28, 23);
            this.raOne.TabIndex = 11;
            this.raOne.Text = "1";
            this.raOne.UseVisualStyleBackColor = false;
            this.raOne.Click += new System.EventHandler(this.raOne_Click);
            // 
            // decZero
            // 
            this.decZero.BackColor = System.Drawing.Color.DimGray;
            this.decZero.FlatAppearance.BorderSize = 0;
            this.decZero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.decZero.Location = new System.Drawing.Point(384, 235);
            this.decZero.Name = "decZero";
            this.decZero.Size = new System.Drawing.Size(17, 23);
            this.decZero.TabIndex = 12;
            this.decZero.Text = "0";
            this.decZero.UseVisualStyleBackColor = false;
            this.decZero.Click += new System.EventHandler(this.decZero_Click);
            // 
            // raTrackingCheck
            // 
            this.raTrackingCheck.AutoSize = true;
            this.raTrackingCheck.BackColor = System.Drawing.Color.DimGray;
            this.raTrackingCheck.Location = new System.Drawing.Point(3, 4);
            this.raTrackingCheck.Name = "raTrackingCheck";
            this.raTrackingCheck.Size = new System.Drawing.Size(72, 16);
            this.raTrackingCheck.TabIndex = 13;
            this.raTrackingCheck.Text = "Tracking";
            this.raTrackingCheck.UseVisualStyleBackColor = false;
            this.raTrackingCheck.CheckedChanged += new System.EventHandler(this.raTrackingCheck_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.Controls.Add(this.raTrackingCheck);
            this.panel2.Location = new System.Drawing.Point(229, 378);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(74, 23);
            this.panel2.TabIndex = 14;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 50;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // txtRaSet
            // 
            this.txtRaSet.Location = new System.Drawing.Point(9, 380);
            this.txtRaSet.Name = "txtRaSet";
            this.txtRaSet.Size = new System.Drawing.Size(68, 21);
            this.txtRaSet.TabIndex = 15;
            // 
            // btnRaSet
            // 
            this.btnRaSet.Location = new System.Drawing.Point(83, 378);
            this.btnRaSet.Name = "btnRaSet";
            this.btnRaSet.Size = new System.Drawing.Size(38, 23);
            this.btnRaSet.TabIndex = 16;
            this.btnRaSet.Text = "Set";
            this.btnRaSet.UseVisualStyleBackColor = true;
            this.btnRaSet.Click += new System.EventHandler(this.btnRaSet_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(407, 423);
            this.Controls.Add(this.btnRaSet);
            this.Controls.Add(this.txtRaSet);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.decZero);
            this.Controls.Add(this.raOne);
            this.Controls.Add(this.raZero);
            this.Controls.Add(this.raNegOne);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.raTrack);
            this.Controls.Add(this.decTrack);
            this.Controls.Add(this.labelDriverId);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.buttonChoose);
            this.Name = "Form1";
            this.Text = "Telescope Controller";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonChoose;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label labelDriverId;
        private System.Windows.Forms.VScrollBar decTrack;
        private System.Windows.Forms.HScrollBar raTrack;
        private System.Windows.Forms.Label raLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label decLabel;
        private System.Windows.Forms.Button raNegOne;
        private System.Windows.Forms.Button raZero;
        private System.Windows.Forms.Button raOne;
        private System.Windows.Forms.Button decZero;
        private System.Windows.Forms.CheckBox raTrackingCheck;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtPulseTime;
        private System.Windows.Forms.Button btnPulseGuideEast;
        private System.Windows.Forms.Button btnPulseGuideSouth;
        private System.Windows.Forms.Button btnPulseGuideWest;
        private System.Windows.Forms.Button btnPulseGuideNorth;
        private System.Windows.Forms.TextBox txtDecGuideSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRAGuideSpeed;
        private System.Windows.Forms.Label labelGuiding;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.TextBox txtRaSet;
        private System.Windows.Forms.Button btnRaSet;
    }
}

