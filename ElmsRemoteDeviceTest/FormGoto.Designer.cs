namespace ASCOM.ElmsRemoteDevice
{
    partial class FormGoto
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSync = new System.Windows.Forms.TextBox();
            this.textBoxGoto = new System.Windows.Forms.TextBox();
            this.buttonGoto = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelSync = new System.Windows.Forms.Label();
            this.labelGoto = new System.Windows.Forms.Label();
            this.comboBoxRaSpeed = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxDecSpeed = new System.Windows.Forms.ComboBox();
            this.labelRATime = new System.Windows.Forms.Label();
            this.labelDecTime = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.buttonSwitch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sync (Current Target)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(249, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Goto (Move To)";
            // 
            // textBoxSync
            // 
            this.textBoxSync.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxSync.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSync.Location = new System.Drawing.Point(15, 38);
            this.textBoxSync.Multiline = true;
            this.textBoxSync.Name = "textBoxSync";
            this.textBoxSync.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxSync.Size = new System.Drawing.Size(193, 192);
            this.textBoxSync.TabIndex = 2;
            this.textBoxSync.TextChanged += new System.EventHandler(this.textBoxSync_TextChanged);
            this.textBoxSync.DoubleClick += new System.EventHandler(this.textBoxSync_DoubleClick);
            this.textBoxSync.Enter += new System.EventHandler(this.textBoxSync_Enter);
            // 
            // textBoxGoto
            // 
            this.textBoxGoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxGoto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxGoto.Location = new System.Drawing.Point(251, 38);
            this.textBoxGoto.Multiline = true;
            this.textBoxGoto.Name = "textBoxGoto";
            this.textBoxGoto.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxGoto.Size = new System.Drawing.Size(193, 192);
            this.textBoxGoto.TabIndex = 3;
            this.textBoxGoto.TextChanged += new System.EventHandler(this.textBoxGoto_TextChanged);
            this.textBoxGoto.DoubleClick += new System.EventHandler(this.textBoxGoto_DoubleClick);
            this.textBoxGoto.Enter += new System.EventHandler(this.textBoxGoto_Enter);
            // 
            // buttonGoto
            // 
            this.buttonGoto.BackColor = System.Drawing.Color.DimGray;
            this.buttonGoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGoto.Location = new System.Drawing.Point(357, 305);
            this.buttonGoto.Name = "buttonGoto";
            this.buttonGoto.Size = new System.Drawing.Size(72, 21);
            this.buttonGoto.TabIndex = 4;
            this.buttonGoto.Text = "Goto";
            this.buttonGoto.UseVisualStyleBackColor = false;
            this.buttonGoto.Click += new System.EventHandler(this.buttonGoto_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.DimGray;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location = new System.Drawing.Point(279, 305);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 21);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelSync
            // 
            this.labelSync.AutoSize = true;
            this.labelSync.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelSync.Location = new System.Drawing.Point(15, 237);
            this.labelSync.Name = "labelSync";
            this.labelSync.Size = new System.Drawing.Size(0, 12);
            this.labelSync.TabIndex = 6;
            // 
            // labelGoto
            // 
            this.labelGoto.AutoSize = true;
            this.labelGoto.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelGoto.Location = new System.Drawing.Point(249, 237);
            this.labelGoto.Name = "labelGoto";
            this.labelGoto.Size = new System.Drawing.Size(0, 12);
            this.labelGoto.TabIndex = 7;
            // 
            // comboBoxRaSpeed
            // 
            this.comboBoxRaSpeed.BackColor = System.Drawing.Color.DimGray;
            this.comboBoxRaSpeed.FormattingEnabled = true;
            this.comboBoxRaSpeed.Items.AddRange(new object[] {
            "15",
            "8",
            "4",
            "2",
            "1"});
            this.comboBoxRaSpeed.Location = new System.Drawing.Point(74, 269);
            this.comboBoxRaSpeed.Name = "comboBoxRaSpeed";
            this.comboBoxRaSpeed.Size = new System.Drawing.Size(60, 20);
            this.comboBoxRaSpeed.TabIndex = 8;
            this.comboBoxRaSpeed.Text = "15";
            this.comboBoxRaSpeed.SelectedIndexChanged += new System.EventHandler(this.comboBoxRaSpeed_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(15, 272);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "RA Speed";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(140, 272);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Dec Speed";
            // 
            // comboBoxDecSpeed
            // 
            this.comboBoxDecSpeed.BackColor = System.Drawing.Color.DimGray;
            this.comboBoxDecSpeed.FormattingEnabled = true;
            this.comboBoxDecSpeed.Items.AddRange(new object[] {
            "15",
            "8",
            "4",
            "2",
            "1"});
            this.comboBoxDecSpeed.Location = new System.Drawing.Point(205, 269);
            this.comboBoxDecSpeed.Name = "comboBoxDecSpeed";
            this.comboBoxDecSpeed.Size = new System.Drawing.Size(60, 20);
            this.comboBoxDecSpeed.TabIndex = 11;
            this.comboBoxDecSpeed.Text = "15";
            this.comboBoxDecSpeed.SelectedIndexChanged += new System.EventHandler(this.comboBoxDecSpeed_SelectedIndexChanged);
            // 
            // labelRATime
            // 
            this.labelRATime.AutoSize = true;
            this.labelRATime.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelRATime.Location = new System.Drawing.Point(15, 305);
            this.labelRATime.Name = "labelRATime";
            this.labelRATime.Size = new System.Drawing.Size(77, 12);
            this.labelRATime.TabIndex = 12;
            this.labelRATime.Text = "RA Time: N/A";
            // 
            // labelDecTime
            // 
            this.labelDecTime.AutoSize = true;
            this.labelDecTime.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelDecTime.Location = new System.Drawing.Point(140, 305);
            this.labelDecTime.Name = "labelDecTime";
            this.labelDecTime.Size = new System.Drawing.Size(83, 12);
            this.labelDecTime.TabIndex = 13;
            this.labelDecTime.Text = "Dec Time: N/A";
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // buttonSwitch
            // 
            this.buttonSwitch.Location = new System.Drawing.Point(213, 122);
            this.buttonSwitch.Name = "buttonSwitch";
            this.buttonSwitch.Size = new System.Drawing.Size(33, 23);
            this.buttonSwitch.TabIndex = 14;
            this.buttonSwitch.Text = "<->";
            this.buttonSwitch.UseVisualStyleBackColor = true;
            this.buttonSwitch.Click += new System.EventHandler(this.buttonSwitch_Click);
            // 
            // FormGoto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(451, 338);
            this.Controls.Add(this.buttonSwitch);
            this.Controls.Add(this.labelDecTime);
            this.Controls.Add(this.labelRATime);
            this.Controls.Add(this.comboBoxDecSpeed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxRaSpeed);
            this.Controls.Add(this.labelGoto);
            this.Controls.Add(this.labelSync);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonGoto);
            this.Controls.Add(this.textBoxGoto);
            this.Controls.Add(this.textBoxSync);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FormGoto";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Goto";
            this.Load += new System.EventHandler(this.FormGoto_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSync;
        private System.Windows.Forms.TextBox textBoxGoto;
        private System.Windows.Forms.Button buttonGoto;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelSync;
        private System.Windows.Forms.Label labelGoto;
        private System.Windows.Forms.ComboBox comboBoxRaSpeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxDecSpeed;
        private System.Windows.Forms.Label labelRATime;
        private System.Windows.Forms.Label labelDecTime;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button buttonSwitch;
    }
}