namespace LIB_.SubFROMLib
{
    partial class SecsGem
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
            this.btnLocal = new System.Windows.Forms.Button();
            this.lblUseMes = new System.Windows.Forms.Label();
            this.gbTerminalMsg = new System.Windows.Forms.GroupBox();
            this.lblTerminalMsg = new System.Windows.Forms.Label();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.btnRemote = new System.Windows.Forms.Button();
            this.btnOffline = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblControlState = new System.Windows.Forms.Label();
            this.lblCommState = new System.Windows.Forms.Label();
            this.lblConnectState = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbCommRequest = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lbT8 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lbT7 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lbT6 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbT5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbT3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbModeSelect = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbLinkTest = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbDevice = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbPort = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.gbTerminalMsg.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLocal
            // 
            this.btnLocal.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLocal.Location = new System.Drawing.Point(364, 51);
            this.btnLocal.Name = "btnLocal";
            this.btnLocal.Size = new System.Drawing.Size(91, 22);
            this.btnLocal.TabIndex = 844;
            this.btnLocal.Text = "LOCAL";
            this.btnLocal.UseVisualStyleBackColor = true;
            this.btnLocal.Click += new System.EventHandler(this.GEM_Local_Click);
            // 
            // lblUseMes
            // 
            this.lblUseMes.BackColor = System.Drawing.SystemColors.Control;
            this.lblUseMes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUseMes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUseMes.Location = new System.Drawing.Point(1, 157);
            this.lblUseMes.Name = "lblUseMes";
            this.lblUseMes.Size = new System.Drawing.Size(100, 20);
            this.lblUseMes.TabIndex = 843;
            this.lblUseMes.Tag = "24";
            this.lblUseMes.Text = "USE MES";
            this.lblUseMes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbTerminalMsg
            // 
            this.gbTerminalMsg.Controls.Add(this.lblTerminalMsg);
            this.gbTerminalMsg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTerminalMsg.Location = new System.Drawing.Point(2, 0);
            this.gbTerminalMsg.Name = "gbTerminalMsg";
            this.gbTerminalMsg.Size = new System.Drawing.Size(451, 51);
            this.gbTerminalMsg.TabIndex = 842;
            this.gbTerminalMsg.TabStop = false;
            this.gbTerminalMsg.Text = "Terminal Message";
            // 
            // lblTerminalMsg
            // 
            this.lblTerminalMsg.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTerminalMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTerminalMsg.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerminalMsg.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblTerminalMsg.Location = new System.Drawing.Point(5, 16);
            this.lblTerminalMsg.Name = "lblTerminalMsg";
            this.lblTerminalMsg.Size = new System.Drawing.Size(440, 30);
            this.lblTerminalMsg.TabIndex = 817;
            this.lblTerminalMsg.Tag = "24";
            this.lblTerminalMsg.Text = "TERMINAL MESSAGAE";
            this.lblTerminalMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTerminalMsg.DoubleClick += new System.EventHandler(this.lblTerminalMsg_DoubleClick);
            // 
            // lstLog
            // 
            this.lstLog.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLog.FormattingEnabled = true;
            this.lstLog.HorizontalScrollbar = true;
            this.lstLog.Location = new System.Drawing.Point(2, 74);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(451, 82);
            this.lstLog.TabIndex = 840;
            // 
            // btnRemote
            // 
            this.btnRemote.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRemote.Location = new System.Drawing.Point(273, 51);
            this.btnRemote.Name = "btnRemote";
            this.btnRemote.Size = new System.Drawing.Size(91, 22);
            this.btnRemote.TabIndex = 839;
            this.btnRemote.Text = "REMOTE";
            this.btnRemote.UseVisualStyleBackColor = true;
            this.btnRemote.Click += new System.EventHandler(this.GEM_Remote_Click);
            // 
            // btnOffline
            // 
            this.btnOffline.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOffline.Location = new System.Drawing.Point(182, 51);
            this.btnOffline.Name = "btnOffline";
            this.btnOffline.Size = new System.Drawing.Size(91, 22);
            this.btnOffline.TabIndex = 838;
            this.btnOffline.Text = "OFFLINE";
            this.btnOffline.UseVisualStyleBackColor = true;
            this.btnOffline.Click += new System.EventHandler(this.GEM_Offline_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStop.Location = new System.Drawing.Point(91, 51);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(91, 22);
            this.btnStop.TabIndex = 837;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.GEM_Stop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStart.Location = new System.Drawing.Point(0, 51);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(91, 22);
            this.btnStart.TabIndex = 836;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.GEM_Start_Click);
            // 
            // lblControlState
            // 
            this.lblControlState.BackColor = System.Drawing.SystemColors.Control;
            this.lblControlState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblControlState.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControlState.Location = new System.Drawing.Point(354, 157);
            this.lblControlState.Name = "lblControlState";
            this.lblControlState.Size = new System.Drawing.Size(100, 20);
            this.lblControlState.TabIndex = 835;
            this.lblControlState.Tag = "24";
            this.lblControlState.Text = "OFFLINE";
            this.lblControlState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCommState
            // 
            this.lblCommState.BackColor = System.Drawing.SystemColors.Control;
            this.lblCommState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCommState.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommState.Location = new System.Drawing.Point(227, 157);
            this.lblCommState.Name = "lblCommState";
            this.lblCommState.Size = new System.Drawing.Size(128, 20);
            this.lblCommState.TabIndex = 834;
            this.lblCommState.Tag = "24";
            this.lblCommState.Text = "NOT COMMUNICATING";
            this.lblCommState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConnectState
            // 
            this.lblConnectState.BackColor = System.Drawing.SystemColors.Control;
            this.lblConnectState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblConnectState.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectState.Location = new System.Drawing.Point(100, 157);
            this.lblConnectState.Name = "lblConnectState";
            this.lblConnectState.Size = new System.Drawing.Size(128, 20);
            this.lblConnectState.TabIndex = 833;
            this.lblConnectState.Tag = "24";
            this.lblConnectState.Text = "NOT CONNECTED";
            this.lblConnectState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbCommRequest
            // 
            this.lbCommRequest.BackColor = System.Drawing.Color.White;
            this.lbCommRequest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCommRequest.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbCommRequest.Location = new System.Drawing.Point(419, 195);
            this.lbCommRequest.Name = "lbCommRequest";
            this.lbCommRequest.Size = new System.Drawing.Size(35, 20);
            this.lbCommRequest.TabIndex = 894;
            this.lbCommRequest.Text = "00";
            this.lbCommRequest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.SystemColors.Control;
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label18.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label18.Location = new System.Drawing.Point(333, 195);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(87, 20);
            this.label18.TabIndex = 893;
            this.label18.Text = "COMM";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbT8
            // 
            this.lbT8.BackColor = System.Drawing.Color.White;
            this.lbT8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbT8.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbT8.Location = new System.Drawing.Point(419, 176);
            this.lbT8.Name = "lbT8";
            this.lbT8.Size = new System.Drawing.Size(35, 20);
            this.lbT8.TabIndex = 892;
            this.lbT8.Text = "00";
            this.lbT8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.SystemColors.Control;
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label17.Location = new System.Drawing.Point(393, 176);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(27, 20);
            this.label17.TabIndex = 891;
            this.label17.Text = "T8";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbT7
            // 
            this.lbT7.BackColor = System.Drawing.Color.White;
            this.lbT7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbT7.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbT7.Location = new System.Drawing.Point(359, 176);
            this.lbT7.Name = "lbT7";
            this.lbT7.Size = new System.Drawing.Size(35, 20);
            this.lbT7.TabIndex = 890;
            this.lbT7.Text = "00";
            this.lbT7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.SystemColors.Control;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label15.Location = new System.Drawing.Point(333, 176);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 20);
            this.label15.TabIndex = 889;
            this.label15.Text = "T7";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbT6
            // 
            this.lbT6.BackColor = System.Drawing.Color.White;
            this.lbT6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbT6.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbT6.Location = new System.Drawing.Point(299, 176);
            this.lbT6.Name = "lbT6";
            this.lbT6.Size = new System.Drawing.Size(35, 20);
            this.lbT6.TabIndex = 888;
            this.lbT6.Text = "00";
            this.lbT6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.SystemColors.Control;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label13.Location = new System.Drawing.Point(273, 176);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 20);
            this.label13.TabIndex = 887;
            this.label13.Text = "T6";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbT5
            // 
            this.lbT5.BackColor = System.Drawing.Color.White;
            this.lbT5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbT5.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbT5.Location = new System.Drawing.Point(239, 176);
            this.lbT5.Name = "lbT5";
            this.lbT5.Size = new System.Drawing.Size(35, 20);
            this.lbT5.TabIndex = 886;
            this.lbT5.Text = "00";
            this.lbT5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.Control;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label10.Location = new System.Drawing.Point(213, 176);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 20);
            this.label10.TabIndex = 885;
            this.label10.Text = "T5";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbT3
            // 
            this.lbT3.BackColor = System.Drawing.Color.White;
            this.lbT3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbT3.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbT3.Location = new System.Drawing.Point(179, 176);
            this.lbT3.Name = "lbT3";
            this.lbT3.Size = new System.Drawing.Size(35, 20);
            this.lbT3.TabIndex = 884;
            this.lbT3.Text = "00";
            this.lbT3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label8.Location = new System.Drawing.Point(153, 176);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 20);
            this.label8.TabIndex = 883;
            this.label8.Text = "T3";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbModeSelect
            // 
            this.lbModeSelect.BackColor = System.Drawing.Color.White;
            this.lbModeSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbModeSelect.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbModeSelect.Location = new System.Drawing.Point(118, 195);
            this.lbModeSelect.Name = "lbModeSelect";
            this.lbModeSelect.Size = new System.Drawing.Size(96, 20);
            this.lbModeSelect.TabIndex = 882;
            this.lbModeSelect.Text = "PASSIVE";
            this.lbModeSelect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label6.Location = new System.Drawing.Point(1, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 20);
            this.label6.TabIndex = 881;
            this.label6.Text = "MODE SELECT";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLinkTest
            // 
            this.lbLinkTest.BackColor = System.Drawing.Color.White;
            this.lbLinkTest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLinkTest.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbLinkTest.Location = new System.Drawing.Point(299, 195);
            this.lbLinkTest.Name = "lbLinkTest";
            this.lbLinkTest.Size = new System.Drawing.Size(35, 20);
            this.lbLinkTest.TabIndex = 880;
            this.lbLinkTest.Text = "000";
            this.lbLinkTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label4.Location = new System.Drawing.Point(213, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 20);
            this.label4.TabIndex = 879;
            this.label4.Text = "LINK TEST";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDevice
            // 
            this.lbDevice.BackColor = System.Drawing.Color.White;
            this.lbDevice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDevice.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbDevice.Location = new System.Drawing.Point(119, 176);
            this.lbDevice.Name = "lbDevice";
            this.lbDevice.Size = new System.Drawing.Size(35, 20);
            this.lbDevice.TabIndex = 878;
            this.lbDevice.Text = "00";
            this.lbDevice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label3.Location = new System.Drawing.Point(80, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 20);
            this.label3.TabIndex = 877;
            this.label3.Text = "DEVID";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPort
            // 
            this.lbPort.BackColor = System.Drawing.Color.White;
            this.lbPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPort.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbPort.Location = new System.Drawing.Point(40, 176);
            this.lbPort.Name = "lbPort";
            this.lbPort.Size = new System.Drawing.Size(41, 20);
            this.lbPort.TabIndex = 876;
            this.lbPort.Text = "0000";
            this.lbPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.SystemColors.Control;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label12.Location = new System.Drawing.Point(1, 176);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 20);
            this.label12.TabIndex = 875;
            this.label12.Text = "PORT";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SecsGem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(456, 220);
            this.ControlBox = false;
            this.Controls.Add(this.lbCommRequest);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.lbT8);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.lbT7);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lbT6);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lbT5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lbT3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lbModeSelect);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbLinkTest);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbDevice);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbPort);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnLocal);
            this.Controls.Add(this.lblUseMes);
            this.Controls.Add(this.gbTerminalMsg);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.btnRemote);
            this.Controls.Add(this.btnOffline);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblControlState);
            this.Controls.Add(this.lblCommState);
            this.Controls.Add(this.lblConnectState);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SecsGem";
            this.Text = "fSecsGem";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SecsGem_FormClosed);
            this.Load += new System.EventHandler(this.SecsGem_Load);
            this.gbTerminalMsg.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLocal;
        private System.Windows.Forms.Label lblUseMes;
        private System.Windows.Forms.GroupBox gbTerminalMsg;
        private System.Windows.Forms.Label lblTerminalMsg;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.Button btnRemote;
        private System.Windows.Forms.Button btnOffline;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblControlState;
        private System.Windows.Forms.Label lblCommState;
        private System.Windows.Forms.Label lblConnectState;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbCommRequest;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lbT8;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lbT7;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lbT6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbT5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbT3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbModeSelect;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbLinkTest;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbDevice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbPort;
        private System.Windows.Forms.Label label12;
    }
}