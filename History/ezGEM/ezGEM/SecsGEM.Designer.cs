namespace ezGEM
{
    partial class SecsGEM
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbTerminalMsg = new System.Windows.Forms.GroupBox();
            this.lblTerminalMsg = new System.Windows.Forms.Label();
            this.btnLocal = new System.Windows.Forms.Button();
            this.lblUseMes = new System.Windows.Forms.Label();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.btnRemote = new System.Windows.Forms.Button();
            this.btnOffline = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblControlState = new System.Windows.Forms.Label();
            this.lblCommState = new System.Windows.Forms.Label();
            this.lblConnectState = new System.Windows.Forms.Label();
            this.timerGem = new System.Windows.Forms.Timer(this.components);
            this.timer_SetClock = new System.Windows.Forms.Timer(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.lbPort = new System.Windows.Forms.Label();
            this.lbDevice = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbLinkTest = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbModeSelect = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbT3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbT5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbT6 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbT7 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lbT8 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lbCommRequest = new System.Windows.Forms.Label();
            this.txtLotID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbEquipmentState = new System.Windows.Forms.ComboBox();
            this.bEQSTATE = new System.Windows.Forms.Button();
            this.ChkEQ_PM = new JCS.ToggleSwitch();
            this.SetLotID = new System.Windows.Forms.Button();
            this.SetUserID = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtGroup = new System.Windows.Forms.TextBox();
            this.txtRecipe = new System.Windows.Forms.TextBox();
            this.SetRecipe = new System.Windows.Forms.Button();
            this.BTN_LOT_REQUEST = new System.Windows.Forms.Button();
            this.BTN_EXIT = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RBT_LOT_TYPE_5 = new System.Windows.Forms.RadioButton();
            this.RBT_LOT_TYPE_4 = new System.Windows.Forms.RadioButton();
            this.RBT_LOT_TYPE_3 = new System.Windows.Forms.RadioButton();
            this.RBT_LOT_TYPE_2 = new System.Windows.Forms.RadioButton();
            this.RBT_LOT_TYPE_1 = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLotCnt = new System.Windows.Forms.TextBox();
            this.LBL_EQP_CODE = new System.Windows.Forms.Label();
            this.TXT_ALRAM_NUM = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.BTN_ALARM_SET = new System.Windows.Forms.Button();
            this.BTN_ALARM_RESET = new System.Windows.Forms.Button();
            this.BTN_LOT_CANCELED = new System.Windows.Forms.Button();
            this.GBX_LOT_LOSS = new System.Windows.Forms.GroupBox();
            this.BTN_LOT_LOSS = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.TXT_LOSS_MEMO = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.RDB_LOSS_6 = new System.Windows.Forms.RadioButton();
            this.RDB_LOSS_5 = new System.Windows.Forms.RadioButton();
            this.RDB_LOSS_4 = new System.Windows.Forms.RadioButton();
            this.RDB_LOSS_3 = new System.Windows.Forms.RadioButton();
            this.RDB_LOSS_2 = new System.Windows.Forms.RadioButton();
            this.RDB_LOSS_1 = new System.Windows.Forms.RadioButton();
            this.txtLossEndTime = new System.Windows.Forms.TextBox();
            this.txtLossStartTime = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BTN_LOWER_DF_KITTING = new System.Windows.Forms.Button();
            this.txtNewLowerBladeBarcode = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtOldLowerBladeBarcode = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.BTN_UPPER_DF_KITTING = new System.Windows.Forms.Button();
            this.txtNewUpperBladeBarcode = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtOldUpperBladeBarcode = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.BTN_LOT_LOADING = new System.Windows.Forms.Button();
            this.BTN_LOT_COMPLETE = new System.Windows.Forms.Button();
            this.BTN_PANEL_LINE_IN = new System.Windows.Forms.Button();
            this.BTN_PANEL_LINE_OUT = new System.Windows.Forms.Button();
            this.BTN_PANEL_MODULE_OUT = new System.Windows.Forms.Button();
            this.BTN_PANEL_MODULE_IN = new System.Windows.Forms.Button();
            this.txtPanelInCount = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txtStripBarcode = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.GBX_EQP_CHANGE = new System.Windows.Forms.GroupBox();
            this.BTN_EQP_CHANGE_COMPLETE = new System.Windows.Forms.Button();
            this.TXT_EQP_CHANGE_COMMENT = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.CBX_EQP_CHANGE_CODE = new System.Windows.Forms.ComboBox();
            this.lblUserID = new System.Windows.Forms.Label();
            this.LBL_EQP_CODE_1 = new System.Windows.Forms.Label();
            this.cbx_ModuleName = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.lblProcessWorkingCondition_1 = new System.Windows.Forms.Label();
            this.lblProcessWorkingCondition_Value1 = new System.Windows.Forms.Label();
            this.lblProcessWorkingCondition_Value2 = new System.Windows.Forms.Label();
            this.lblProcessWorkingCondition_2 = new System.Windows.Forms.Label();
            this.lblProcessWorkingCondition_Value3 = new System.Windows.Forms.Label();
            this.lblProcessWorkingCondition_Value4 = new System.Windows.Forms.Label();
            this.lblProcessWorkingCondition_3 = new System.Windows.Forms.Label();
            this.BTN_RUN = new System.Windows.Forms.Button();
            this.gbTerminalMsg.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.GBX_LOT_LOSS.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.GBX_EQP_CHANGE.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbTerminalMsg
            // 
            this.gbTerminalMsg.Controls.Add(this.lblTerminalMsg);
            this.gbTerminalMsg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTerminalMsg.Location = new System.Drawing.Point(12, 12);
            this.gbTerminalMsg.Name = "gbTerminalMsg";
            this.gbTerminalMsg.Size = new System.Drawing.Size(453, 51);
            this.gbTerminalMsg.TabIndex = 843;
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
            this.lblTerminalMsg.Size = new System.Drawing.Size(443, 30);
            this.lblTerminalMsg.TabIndex = 817;
            this.lblTerminalMsg.Tag = "24";
            this.lblTerminalMsg.Text = "TERMINAL MESSAGAE";
            this.lblTerminalMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLocal
            // 
            this.btnLocal.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLocal.Location = new System.Drawing.Point(376, 64);
            this.btnLocal.Name = "btnLocal";
            this.btnLocal.Size = new System.Drawing.Size(91, 24);
            this.btnLocal.TabIndex = 854;
            this.btnLocal.Text = "LOCAL";
            this.btnLocal.UseVisualStyleBackColor = true;
            this.btnLocal.Click += new System.EventHandler(this.btnLocal_Click);
            // 
            // lblUseMes
            // 
            this.lblUseMes.BackColor = System.Drawing.SystemColors.Control;
            this.lblUseMes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUseMes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUseMes.Location = new System.Drawing.Point(13, 173);
            this.lblUseMes.Name = "lblUseMes";
            this.lblUseMes.Size = new System.Drawing.Size(101, 20);
            this.lblUseMes.TabIndex = 853;
            this.lblUseMes.Tag = "24";
            this.lblUseMes.Text = "USE MES";
            this.lblUseMes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstLog
            // 
            this.lstLog.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLog.ForeColor = System.Drawing.Color.Black;
            this.lstLog.FormattingEnabled = true;
            this.lstLog.HorizontalScrollbar = true;
            this.lstLog.Location = new System.Drawing.Point(12, 90);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(453, 69);
            this.lstLog.TabIndex = 852;
            // 
            // btnRemote
            // 
            this.btnRemote.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRemote.Location = new System.Drawing.Point(285, 64);
            this.btnRemote.Name = "btnRemote";
            this.btnRemote.Size = new System.Drawing.Size(91, 24);
            this.btnRemote.TabIndex = 851;
            this.btnRemote.Text = "REMOTE";
            this.btnRemote.UseVisualStyleBackColor = true;
            this.btnRemote.Click += new System.EventHandler(this.btnRemote_Click);
            // 
            // btnOffline
            // 
            this.btnOffline.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOffline.Location = new System.Drawing.Point(194, 64);
            this.btnOffline.Name = "btnOffline";
            this.btnOffline.Size = new System.Drawing.Size(91, 24);
            this.btnOffline.TabIndex = 850;
            this.btnOffline.Text = "OFFLINE";
            this.btnOffline.UseVisualStyleBackColor = true;
            this.btnOffline.Click += new System.EventHandler(this.btnOffline_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStop.Location = new System.Drawing.Point(103, 64);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(91, 24);
            this.btnStop.TabIndex = 849;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStart.Location = new System.Drawing.Point(12, 64);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(91, 24);
            this.btnStart.TabIndex = 848;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblControlState
            // 
            this.lblControlState.BackColor = System.Drawing.SystemColors.Control;
            this.lblControlState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblControlState.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControlState.Location = new System.Drawing.Point(363, 173);
            this.lblControlState.Name = "lblControlState";
            this.lblControlState.Size = new System.Drawing.Size(102, 20);
            this.lblControlState.TabIndex = 847;
            this.lblControlState.Tag = "24";
            this.lblControlState.Text = "OFFLINE";
            this.lblControlState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCommState
            // 
            this.lblCommState.BackColor = System.Drawing.SystemColors.Control;
            this.lblCommState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCommState.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommState.Location = new System.Drawing.Point(238, 173);
            this.lblCommState.Name = "lblCommState";
            this.lblCommState.Size = new System.Drawing.Size(126, 20);
            this.lblCommState.TabIndex = 846;
            this.lblCommState.Tag = "24";
            this.lblCommState.Text = "NOT COMMUNICATING";
            this.lblCommState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConnectState
            // 
            this.lblConnectState.BackColor = System.Drawing.SystemColors.Control;
            this.lblConnectState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblConnectState.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectState.Location = new System.Drawing.Point(113, 173);
            this.lblConnectState.Name = "lblConnectState";
            this.lblConnectState.Size = new System.Drawing.Size(126, 20);
            this.lblConnectState.TabIndex = 845;
            this.lblConnectState.Tag = "24";
            this.lblConnectState.Text = "NOT CONNECTED";
            this.lblConnectState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerGem
            // 
            this.timerGem.Interval = 5000;
            this.timerGem.Tick += new System.EventHandler(this.timerGem_Tick);
            // 
            // timer_SetClock
            // 
            this.timer_SetClock.Interval = 1000;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.SystemColors.Control;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label12.Location = new System.Drawing.Point(13, 192);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 20);
            this.label12.TabIndex = 855;
            this.label12.Text = "PORT";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPort
            // 
            this.lbPort.BackColor = System.Drawing.Color.White;
            this.lbPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPort.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbPort.Location = new System.Drawing.Point(52, 192);
            this.lbPort.Name = "lbPort";
            this.lbPort.Size = new System.Drawing.Size(40, 20);
            this.lbPort.TabIndex = 856;
            this.lbPort.Text = "0000";
            this.lbPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDevice
            // 
            this.lbDevice.BackColor = System.Drawing.Color.White;
            this.lbDevice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDevice.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbDevice.Location = new System.Drawing.Point(130, 192);
            this.lbDevice.Name = "lbDevice";
            this.lbDevice.Size = new System.Drawing.Size(35, 20);
            this.lbDevice.TabIndex = 858;
            this.lbDevice.Text = "00";
            this.lbDevice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label3.Location = new System.Drawing.Point(91, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 20);
            this.label3.TabIndex = 857;
            this.label3.Text = "DEVID";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLinkTest
            // 
            this.lbLinkTest.BackColor = System.Drawing.Color.White;
            this.lbLinkTest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLinkTest.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbLinkTest.Location = new System.Drawing.Point(310, 211);
            this.lbLinkTest.Name = "lbLinkTest";
            this.lbLinkTest.Size = new System.Drawing.Size(35, 20);
            this.lbLinkTest.TabIndex = 860;
            this.lbLinkTest.Text = "000";
            this.lbLinkTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label4.Location = new System.Drawing.Point(224, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 20);
            this.label4.TabIndex = 859;
            this.label4.Text = "LINK TEST";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbModeSelect
            // 
            this.lbModeSelect.BackColor = System.Drawing.Color.White;
            this.lbModeSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbModeSelect.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbModeSelect.Location = new System.Drawing.Point(130, 211);
            this.lbModeSelect.Name = "lbModeSelect";
            this.lbModeSelect.Size = new System.Drawing.Size(95, 20);
            this.lbModeSelect.TabIndex = 862;
            this.lbModeSelect.Text = "PASSIVE";
            this.lbModeSelect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label6.Location = new System.Drawing.Point(13, 211);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 20);
            this.label6.TabIndex = 861;
            this.label6.Text = "MODE SELECT";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbT3
            // 
            this.lbT3.BackColor = System.Drawing.Color.White;
            this.lbT3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbT3.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbT3.Location = new System.Drawing.Point(190, 192);
            this.lbT3.Name = "lbT3";
            this.lbT3.Size = new System.Drawing.Size(35, 20);
            this.lbT3.TabIndex = 864;
            this.lbT3.Text = "00";
            this.lbT3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label8.Location = new System.Drawing.Point(164, 192);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 20);
            this.label8.TabIndex = 863;
            this.label8.Text = "T3";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbT5
            // 
            this.lbT5.BackColor = System.Drawing.Color.White;
            this.lbT5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbT5.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbT5.Location = new System.Drawing.Point(250, 192);
            this.lbT5.Name = "lbT5";
            this.lbT5.Size = new System.Drawing.Size(35, 20);
            this.lbT5.TabIndex = 866;
            this.lbT5.Text = "00";
            this.lbT5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.Control;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label10.Location = new System.Drawing.Point(224, 192);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 20);
            this.label10.TabIndex = 865;
            this.label10.Text = "T5";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbT6
            // 
            this.lbT6.BackColor = System.Drawing.Color.White;
            this.lbT6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbT6.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbT6.Location = new System.Drawing.Point(310, 192);
            this.lbT6.Name = "lbT6";
            this.lbT6.Size = new System.Drawing.Size(35, 20);
            this.lbT6.TabIndex = 868;
            this.lbT6.Text = "00";
            this.lbT6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.SystemColors.Control;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label13.Location = new System.Drawing.Point(284, 192);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 20);
            this.label13.TabIndex = 867;
            this.label13.Text = "T6";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbT7
            // 
            this.lbT7.BackColor = System.Drawing.Color.White;
            this.lbT7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbT7.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbT7.Location = new System.Drawing.Point(370, 192);
            this.lbT7.Name = "lbT7";
            this.lbT7.Size = new System.Drawing.Size(35, 20);
            this.lbT7.TabIndex = 870;
            this.lbT7.Text = "00";
            this.lbT7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.SystemColors.Control;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label15.Location = new System.Drawing.Point(344, 192);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 20);
            this.label15.TabIndex = 869;
            this.label15.Text = "T7";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbT8
            // 
            this.lbT8.BackColor = System.Drawing.Color.White;
            this.lbT8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbT8.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbT8.Location = new System.Drawing.Point(430, 192);
            this.lbT8.Name = "lbT8";
            this.lbT8.Size = new System.Drawing.Size(35, 20);
            this.lbT8.TabIndex = 872;
            this.lbT8.Text = "00";
            this.lbT8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.SystemColors.Control;
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label17.Location = new System.Drawing.Point(404, 192);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(27, 20);
            this.label17.TabIndex = 871;
            this.label17.Text = "T8";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.SystemColors.Control;
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label18.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label18.Location = new System.Drawing.Point(344, 211);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(87, 20);
            this.label18.TabIndex = 873;
            this.label18.Text = "COMM";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCommRequest
            // 
            this.lbCommRequest.BackColor = System.Drawing.Color.White;
            this.lbCommRequest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCommRequest.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbCommRequest.Location = new System.Drawing.Point(430, 211);
            this.lbCommRequest.Name = "lbCommRequest";
            this.lbCommRequest.Size = new System.Drawing.Size(35, 20);
            this.lbCommRequest.TabIndex = 874;
            this.lbCommRequest.Text = "00";
            this.lbCommRequest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtLotID
            // 
            this.txtLotID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotID.Location = new System.Drawing.Point(53, 284);
            this.txtLotID.Name = "txtLotID";
            this.txtLotID.Size = new System.Drawing.Size(199, 22);
            this.txtLotID.TabIndex = 875;
            this.txtLotID.Text = "Q0580490001S";
            this.txtLotID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(6, 290);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 876;
            this.label1.Text = "LOT ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(278, 315);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 878;
            this.label2.Text = "USER ID";
            // 
            // txtUserID
            // 
            this.txtUserID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserID.Location = new System.Drawing.Point(330, 310);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(85, 22);
            this.txtUserID.TabIndex = 877;
            this.txtUserID.Text = "E210712";
            this.txtUserID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(6, 262);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 879;
            this.label5.Text = "EQUIPMENT STATE";
            // 
            // cbEquipmentState
            // 
            this.cbEquipmentState.FormattingEnabled = true;
            this.cbEquipmentState.Items.AddRange(new object[] {
            "1,INIT",
            "2,IDLE",
            "3,SETUP",
            "4,READY",
            "5,RUN",
            "6,DOWN",
            "7,MANUAL"});
            this.cbEquipmentState.Location = new System.Drawing.Point(116, 259);
            this.cbEquipmentState.Name = "cbEquipmentState";
            this.cbEquipmentState.Size = new System.Drawing.Size(78, 20);
            this.cbEquipmentState.TabIndex = 880;
            // 
            // bEQSTATE
            // 
            this.bEQSTATE.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bEQSTATE.Location = new System.Drawing.Point(195, 256);
            this.bEQSTATE.Name = "bEQSTATE";
            this.bEQSTATE.Size = new System.Drawing.Size(70, 25);
            this.bEQSTATE.TabIndex = 881;
            this.bEQSTATE.Text = "EQ STATE";
            this.bEQSTATE.UseVisualStyleBackColor = true;
            this.bEQSTATE.Click += new System.EventHandler(this.bEQSTATE_Click);
            // 
            // ChkEQ_PM
            // 
            this.ChkEQ_PM.BackColor = System.Drawing.Color.Transparent;
            this.ChkEQ_PM.Location = new System.Drawing.Point(274, 259);
            this.ChkEQ_PM.Name = "ChkEQ_PM";
            this.ChkEQ_PM.OffFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkEQ_PM.OffForeColor = System.Drawing.Color.Red;
            this.ChkEQ_PM.OnFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkEQ_PM.OnForeColor = System.Drawing.Color.Yellow;
            this.ChkEQ_PM.OnText = "EQUIPMENT PM";
            this.ChkEQ_PM.Size = new System.Drawing.Size(186, 24);
            this.ChkEQ_PM.Style = JCS.ToggleSwitch.ToggleSwitchStyle.Android;
            this.ChkEQ_PM.TabIndex = 1163;
            this.ChkEQ_PM.Tag = "MC";
            this.ChkEQ_PM.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.ChkEQ_PM_CheckedChanged);
            // 
            // SetLotID
            // 
            this.SetLotID.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SetLotID.Location = new System.Drawing.Point(415, 283);
            this.SetLotID.Name = "SetLotID";
            this.SetLotID.Size = new System.Drawing.Size(45, 25);
            this.SetLotID.TabIndex = 1164;
            this.SetLotID.Text = "SET";
            this.SetLotID.UseVisualStyleBackColor = true;
            this.SetLotID.Click += new System.EventHandler(this.SetLotID_Click);
            // 
            // SetUserID
            // 
            this.SetUserID.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SetUserID.Location = new System.Drawing.Point(415, 308);
            this.SetUserID.Name = "SetUserID";
            this.SetUserID.Size = new System.Drawing.Size(45, 25);
            this.SetUserID.TabIndex = 1165;
            this.SetUserID.Text = "SET";
            this.SetUserID.UseVisualStyleBackColor = true;
            this.SetUserID.Click += new System.EventHandler(this.SetUserID_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.Location = new System.Drawing.Point(10, 334);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(212, 13);
            this.label7.TabIndex = 1166;
            this.label7.Text = "-CURRENT INFOMATION ---------------";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.Location = new System.Drawing.Point(6, 315);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 1167;
            this.label9.Text = "DEVICE";
            // 
            // txtGroup
            // 
            this.txtGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGroup.Location = new System.Drawing.Point(53, 310);
            this.txtGroup.Name = "txtGroup";
            this.txtGroup.Size = new System.Drawing.Size(78, 22);
            this.txtGroup.TabIndex = 1168;
            this.txtGroup.Text = "13X10";
            this.txtGroup.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRecipe
            // 
            this.txtRecipe.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecipe.Location = new System.Drawing.Point(133, 310);
            this.txtRecipe.Name = "txtRecipe";
            this.txtRecipe.Size = new System.Drawing.Size(97, 22);
            this.txtRecipe.TabIndex = 1169;
            this.txtRecipe.Text = "PCP00646-02";
            this.txtRecipe.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SetRecipe
            // 
            this.SetRecipe.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SetRecipe.Location = new System.Drawing.Point(231, 309);
            this.SetRecipe.Name = "SetRecipe";
            this.SetRecipe.Size = new System.Drawing.Size(45, 25);
            this.SetRecipe.TabIndex = 1170;
            this.SetRecipe.Text = "SET";
            this.SetRecipe.UseVisualStyleBackColor = true;
            this.SetRecipe.Click += new System.EventHandler(this.SetRecipe_Click);
            // 
            // BTN_LOT_REQUEST
            // 
            this.BTN_LOT_REQUEST.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_LOT_REQUEST.Location = new System.Drawing.Point(6, 390);
            this.BTN_LOT_REQUEST.Name = "BTN_LOT_REQUEST";
            this.BTN_LOT_REQUEST.Size = new System.Drawing.Size(102, 46);
            this.BTN_LOT_REQUEST.TabIndex = 1171;
            this.BTN_LOT_REQUEST.Text = "LOT REQUEST\r\n(601)";
            this.BTN_LOT_REQUEST.UseVisualStyleBackColor = true;
            this.BTN_LOT_REQUEST.Click += new System.EventHandler(this.BTN_LOT_REQUEST_Click);
            // 
            // BTN_EXIT
            // 
            this.BTN_EXIT.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_EXIT.Location = new System.Drawing.Point(357, 338);
            this.BTN_EXIT.Name = "BTN_EXIT";
            this.BTN_EXIT.Size = new System.Drawing.Size(101, 53);
            this.BTN_EXIT.TabIndex = 1172;
            this.BTN_EXIT.Text = "EXIT";
            this.BTN_EXIT.UseVisualStyleBackColor = true;
            this.BTN_EXIT.Click += new System.EventHandler(this.BTN_EXIT_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RBT_LOT_TYPE_5);
            this.groupBox1.Controls.Add(this.RBT_LOT_TYPE_4);
            this.groupBox1.Controls.Add(this.RBT_LOT_TYPE_3);
            this.groupBox1.Controls.Add(this.RBT_LOT_TYPE_2);
            this.groupBox1.Controls.Add(this.RBT_LOT_TYPE_1);
            this.groupBox1.Location = new System.Drawing.Point(5, 347);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 43);
            this.groupBox1.TabIndex = 1173;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LOT 종류";
            // 
            // RBT_LOT_TYPE_5
            // 
            this.RBT_LOT_TYPE_5.AutoSize = true;
            this.RBT_LOT_TYPE_5.Location = new System.Drawing.Point(233, 17);
            this.RBT_LOT_TYPE_5.Name = "RBT_LOT_TYPE_5";
            this.RBT_LOT_TYPE_5.Size = new System.Drawing.Size(59, 16);
            this.RBT_LOT_TYPE_5.TabIndex = 4;
            this.RBT_LOT_TYPE_5.TabStop = true;
            this.RBT_LOT_TYPE_5.Text = "재작업";
            this.RBT_LOT_TYPE_5.UseVisualStyleBackColor = true;
            // 
            // RBT_LOT_TYPE_4
            // 
            this.RBT_LOT_TYPE_4.AutoSize = true;
            this.RBT_LOT_TYPE_4.Location = new System.Drawing.Point(168, 17);
            this.RBT_LOT_TYPE_4.Name = "RBT_LOT_TYPE_4";
            this.RBT_LOT_TYPE_4.Size = new System.Drawing.Size(59, 16);
            this.RBT_LOT_TYPE_4.TabIndex = 3;
            this.RBT_LOT_TYPE_4.TabStop = true;
            this.RBT_LOT_TYPE_4.Text = "재초도";
            this.RBT_LOT_TYPE_4.UseVisualStyleBackColor = true;
            // 
            // RBT_LOT_TYPE_3
            // 
            this.RBT_LOT_TYPE_3.AutoSize = true;
            this.RBT_LOT_TYPE_3.Location = new System.Drawing.Point(115, 17);
            this.RBT_LOT_TYPE_3.Name = "RBT_LOT_TYPE_3";
            this.RBT_LOT_TYPE_3.Size = new System.Drawing.Size(47, 16);
            this.RBT_LOT_TYPE_3.TabIndex = 2;
            this.RBT_LOT_TYPE_3.TabStop = true;
            this.RBT_LOT_TYPE_3.Text = "더미";
            this.RBT_LOT_TYPE_3.UseVisualStyleBackColor = true;
            // 
            // RBT_LOT_TYPE_2
            // 
            this.RBT_LOT_TYPE_2.AutoSize = true;
            this.RBT_LOT_TYPE_2.Location = new System.Drawing.Point(62, 17);
            this.RBT_LOT_TYPE_2.Name = "RBT_LOT_TYPE_2";
            this.RBT_LOT_TYPE_2.Size = new System.Drawing.Size(47, 16);
            this.RBT_LOT_TYPE_2.TabIndex = 1;
            this.RBT_LOT_TYPE_2.TabStop = true;
            this.RBT_LOT_TYPE_2.Text = "본낫";
            this.RBT_LOT_TYPE_2.UseVisualStyleBackColor = true;
            // 
            // RBT_LOT_TYPE_1
            // 
            this.RBT_LOT_TYPE_1.AutoSize = true;
            this.RBT_LOT_TYPE_1.Location = new System.Drawing.Point(9, 17);
            this.RBT_LOT_TYPE_1.Name = "RBT_LOT_TYPE_1";
            this.RBT_LOT_TYPE_1.Size = new System.Drawing.Size(47, 16);
            this.RBT_LOT_TYPE_1.TabIndex = 0;
            this.RBT_LOT_TYPE_1.TabStop = true;
            this.RBT_LOT_TYPE_1.Text = "초도";
            this.RBT_LOT_TYPE_1.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.Location = new System.Drawing.Point(259, 290);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 1175;
            this.label11.Text = "LOT 수량";
            // 
            // txtLotCnt
            // 
            this.txtLotCnt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotCnt.Location = new System.Drawing.Point(316, 285);
            this.txtLotCnt.Name = "txtLotCnt";
            this.txtLotCnt.Size = new System.Drawing.Size(99, 22);
            this.txtLotCnt.TabIndex = 1174;
            this.txtLotCnt.Text = "60";
            this.txtLotCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LBL_EQP_CODE
            // 
            this.LBL_EQP_CODE.AutoSize = true;
            this.LBL_EQP_CODE.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LBL_EQP_CODE.Location = new System.Drawing.Point(6, 235);
            this.LBL_EQP_CODE.Name = "LBL_EQP_CODE";
            this.LBL_EQP_CODE.Size = new System.Drawing.Size(113, 13);
            this.LBL_EQP_CODE.TabIndex = 1176;
            this.LBL_EQP_CODE.Text = "EQUIPMENT CODE : ";
            // 
            // TXT_ALRAM_NUM
            // 
            this.TXT_ALRAM_NUM.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_ALRAM_NUM.Location = new System.Drawing.Point(293, 234);
            this.TXT_ALRAM_NUM.Name = "TXT_ALRAM_NUM";
            this.TXT_ALRAM_NUM.Size = new System.Drawing.Size(78, 22);
            this.TXT_ALRAM_NUM.TabIndex = 1178;
            this.TXT_ALRAM_NUM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label14.Location = new System.Drawing.Point(245, 239);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 13);
            this.label14.TabIndex = 1177;
            this.label14.Text = "ALARM";
            // 
            // BTN_ALARM_SET
            // 
            this.BTN_ALARM_SET.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_ALARM_SET.Location = new System.Drawing.Point(371, 233);
            this.BTN_ALARM_SET.Name = "BTN_ALARM_SET";
            this.BTN_ALARM_SET.Size = new System.Drawing.Size(46, 25);
            this.BTN_ALARM_SET.TabIndex = 1179;
            this.BTN_ALARM_SET.Text = "SET";
            this.BTN_ALARM_SET.UseVisualStyleBackColor = true;
            this.BTN_ALARM_SET.Click += new System.EventHandler(this.BTN_ALARM_SET_Click);
            // 
            // BTN_ALARM_RESET
            // 
            this.BTN_ALARM_RESET.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_ALARM_RESET.Location = new System.Drawing.Point(417, 233);
            this.BTN_ALARM_RESET.Name = "BTN_ALARM_RESET";
            this.BTN_ALARM_RESET.Size = new System.Drawing.Size(46, 25);
            this.BTN_ALARM_RESET.TabIndex = 1180;
            this.BTN_ALARM_RESET.Text = "RESET";
            this.BTN_ALARM_RESET.UseVisualStyleBackColor = true;
            this.BTN_ALARM_RESET.Click += new System.EventHandler(this.BTN_ALARM_RESET_Click);
            // 
            // BTN_LOT_CANCELED
            // 
            this.BTN_LOT_CANCELED.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_LOT_CANCELED.Location = new System.Drawing.Point(113, 390);
            this.BTN_LOT_CANCELED.Name = "BTN_LOT_CANCELED";
            this.BTN_LOT_CANCELED.Size = new System.Drawing.Size(102, 46);
            this.BTN_LOT_CANCELED.TabIndex = 1181;
            this.BTN_LOT_CANCELED.Text = "LOT CANCELED\r\n(331)";
            this.BTN_LOT_CANCELED.UseVisualStyleBackColor = true;
            this.BTN_LOT_CANCELED.Click += new System.EventHandler(this.BTN_LOT_CANCELED_Click);
            // 
            // GBX_LOT_LOSS
            // 
            this.GBX_LOT_LOSS.Controls.Add(this.BTN_LOT_LOSS);
            this.GBX_LOT_LOSS.Controls.Add(this.label20);
            this.GBX_LOT_LOSS.Controls.Add(this.TXT_LOSS_MEMO);
            this.GBX_LOT_LOSS.Controls.Add(this.label19);
            this.GBX_LOT_LOSS.Controls.Add(this.RDB_LOSS_6);
            this.GBX_LOT_LOSS.Controls.Add(this.RDB_LOSS_5);
            this.GBX_LOT_LOSS.Controls.Add(this.RDB_LOSS_4);
            this.GBX_LOT_LOSS.Controls.Add(this.RDB_LOSS_3);
            this.GBX_LOT_LOSS.Controls.Add(this.RDB_LOSS_2);
            this.GBX_LOT_LOSS.Controls.Add(this.RDB_LOSS_1);
            this.GBX_LOT_LOSS.Controls.Add(this.txtLossEndTime);
            this.GBX_LOT_LOSS.Controls.Add(this.txtLossStartTime);
            this.GBX_LOT_LOSS.Controls.Add(this.label16);
            this.GBX_LOT_LOSS.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBX_LOT_LOSS.Location = new System.Drawing.Point(224, 390);
            this.GBX_LOT_LOSS.Name = "GBX_LOT_LOSS";
            this.GBX_LOT_LOSS.Size = new System.Drawing.Size(256, 158);
            this.GBX_LOT_LOSS.TabIndex = 1182;
            this.GBX_LOT_LOSS.TabStop = false;
            this.GBX_LOT_LOSS.Text = "LOT LOSS";
            // 
            // BTN_LOT_LOSS
            // 
            this.BTN_LOT_LOSS.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_LOT_LOSS.Location = new System.Drawing.Point(130, 103);
            this.BTN_LOT_LOSS.Name = "BTN_LOT_LOSS";
            this.BTN_LOT_LOSS.Size = new System.Drawing.Size(120, 52);
            this.BTN_LOT_LOSS.TabIndex = 1182;
            this.BTN_LOT_LOSS.Text = "LOT EQP LOSS COMPLETE\r\n(315)";
            this.BTN_LOT_LOSS.UseVisualStyleBackColor = true;
            this.BTN_LOT_LOSS.Click += new System.EventHandler(this.BTN_LOT_LOSS_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label20.Location = new System.Drawing.Point(151, 17);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(15, 13);
            this.label20.TabIndex = 1181;
            this.label20.Text = "~";
            // 
            // TXT_LOSS_MEMO
            // 
            this.TXT_LOSS_MEMO.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_LOSS_MEMO.Location = new System.Drawing.Point(68, 81);
            this.TXT_LOSS_MEMO.Name = "TXT_LOSS_MEMO";
            this.TXT_LOSS_MEMO.Size = new System.Drawing.Size(183, 22);
            this.TXT_LOSS_MEMO.TabIndex = 1180;
            this.TXT_LOSS_MEMO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label19.Location = new System.Drawing.Point(11, 86);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(51, 13);
            this.label19.TabIndex = 1179;
            this.label19.Text = "특이사항";
            // 
            // RDB_LOSS_6
            // 
            this.RDB_LOSS_6.AutoSize = true;
            this.RDB_LOSS_6.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RDB_LOSS_6.Location = new System.Drawing.Point(171, 59);
            this.RDB_LOSS_6.Name = "RDB_LOSS_6";
            this.RDB_LOSS_6.Size = new System.Drawing.Size(79, 19);
            this.RDB_LOSS_6.TabIndex = 1178;
            this.RDB_LOSS_6.TabStop = true;
            this.RDB_LOSS_6.Text = "TOOL교체";
            this.RDB_LOSS_6.UseVisualStyleBackColor = true;
            // 
            // RDB_LOSS_5
            // 
            this.RDB_LOSS_5.AutoSize = true;
            this.RDB_LOSS_5.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RDB_LOSS_5.Location = new System.Drawing.Point(92, 59);
            this.RDB_LOSS_5.Name = "RDB_LOSS_5";
            this.RDB_LOSS_5.Size = new System.Drawing.Size(73, 19);
            this.RDB_LOSS_5.TabIndex = 1177;
            this.RDB_LOSS_5.TabStop = true;
            this.RDB_LOSS_5.Text = "청소시간";
            this.RDB_LOSS_5.UseVisualStyleBackColor = true;
            // 
            // RDB_LOSS_4
            // 
            this.RDB_LOSS_4.AutoSize = true;
            this.RDB_LOSS_4.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RDB_LOSS_4.Location = new System.Drawing.Point(13, 59);
            this.RDB_LOSS_4.Name = "RDB_LOSS_4";
            this.RDB_LOSS_4.Size = new System.Drawing.Size(73, 19);
            this.RDB_LOSS_4.TabIndex = 1176;
            this.RDB_LOSS_4.TabStop = true;
            this.RDB_LOSS_4.Text = "인원부족";
            this.RDB_LOSS_4.UseVisualStyleBackColor = true;
            // 
            // RDB_LOSS_3
            // 
            this.RDB_LOSS_3.AutoSize = true;
            this.RDB_LOSS_3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RDB_LOSS_3.Location = new System.Drawing.Point(172, 37);
            this.RDB_LOSS_3.Name = "RDB_LOSS_3";
            this.RDB_LOSS_3.Size = new System.Drawing.Size(73, 19);
            this.RDB_LOSS_3.TabIndex = 1175;
            this.RDB_LOSS_3.TabStop = true;
            this.RDB_LOSS_3.Text = "휴게시간";
            this.RDB_LOSS_3.UseVisualStyleBackColor = true;
            // 
            // RDB_LOSS_2
            // 
            this.RDB_LOSS_2.AutoSize = true;
            this.RDB_LOSS_2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RDB_LOSS_2.Location = new System.Drawing.Point(93, 37);
            this.RDB_LOSS_2.Name = "RDB_LOSS_2";
            this.RDB_LOSS_2.Size = new System.Drawing.Size(73, 19);
            this.RDB_LOSS_2.TabIndex = 1174;
            this.RDB_LOSS_2.TabStop = true;
            this.RDB_LOSS_2.Text = "자재부족";
            this.RDB_LOSS_2.UseVisualStyleBackColor = true;
            // 
            // RDB_LOSS_1
            // 
            this.RDB_LOSS_1.AutoSize = true;
            this.RDB_LOSS_1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RDB_LOSS_1.Location = new System.Drawing.Point(14, 37);
            this.RDB_LOSS_1.Name = "RDB_LOSS_1";
            this.RDB_LOSS_1.Size = new System.Drawing.Size(73, 19);
            this.RDB_LOSS_1.TabIndex = 1173;
            this.RDB_LOSS_1.TabStop = true;
            this.RDB_LOSS_1.Text = "재공부족";
            this.RDB_LOSS_1.UseVisualStyleBackColor = true;
            // 
            // txtLossEndTime
            // 
            this.txtLossEndTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLossEndTime.Location = new System.Drawing.Point(168, 12);
            this.txtLossEndTime.Name = "txtLossEndTime";
            this.txtLossEndTime.Size = new System.Drawing.Size(83, 22);
            this.txtLossEndTime.TabIndex = 1172;
            this.txtLossEndTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtLossStartTime
            // 
            this.txtLossStartTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLossStartTime.Location = new System.Drawing.Point(67, 12);
            this.txtLossStartTime.Name = "txtLossStartTime";
            this.txtLossStartTime.Size = new System.Drawing.Size(83, 22);
            this.txtLossStartTime.TabIndex = 1171;
            this.txtLossStartTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label16.Location = new System.Drawing.Point(10, 17);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 13);
            this.label16.TabIndex = 1170;
            this.label16.Text = "Loss기간";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BTN_LOWER_DF_KITTING);
            this.groupBox3.Controls.Add(this.txtNewLowerBladeBarcode);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.txtOldLowerBladeBarcode);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.BTN_UPPER_DF_KITTING);
            this.groupBox3.Controls.Add(this.txtNewUpperBladeBarcode);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.txtOldUpperBladeBarcode);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(5, 438);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(217, 113);
            this.groupBox3.TabIndex = 1183;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "BLADE 장착";
            // 
            // BTN_LOWER_DF_KITTING
            // 
            this.BTN_LOWER_DF_KITTING.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_LOWER_DF_KITTING.Location = new System.Drawing.Point(156, 62);
            this.BTN_LOWER_DF_KITTING.Name = "BTN_LOWER_DF_KITTING";
            this.BTN_LOWER_DF_KITTING.Size = new System.Drawing.Size(58, 48);
            this.BTN_LOWER_DF_KITTING.TabIndex = 1190;
            this.BTN_LOWER_DF_KITTING.Text = "우측 BLADE\r\n(502)";
            this.BTN_LOWER_DF_KITTING.UseVisualStyleBackColor = true;
            this.BTN_LOWER_DF_KITTING.Click += new System.EventHandler(this.BTN_LOWER_DF_KITTING_Click);
            // 
            // txtNewLowerBladeBarcode
            // 
            this.txtNewLowerBladeBarcode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewLowerBladeBarcode.Location = new System.Drawing.Point(38, 86);
            this.txtNewLowerBladeBarcode.Name = "txtNewLowerBladeBarcode";
            this.txtNewLowerBladeBarcode.Size = new System.Drawing.Size(117, 22);
            this.txtNewLowerBladeBarcode.TabIndex = 1189;
            this.txtNewLowerBladeBarcode.Text = "211222-0039";
            this.txtNewLowerBladeBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label23.Location = new System.Drawing.Point(4, 91);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(31, 13);
            this.label23.TabIndex = 1188;
            this.label23.Text = "NEW";
            // 
            // txtOldLowerBladeBarcode
            // 
            this.txtOldLowerBladeBarcode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOldLowerBladeBarcode.Location = new System.Drawing.Point(38, 63);
            this.txtOldLowerBladeBarcode.Name = "txtOldLowerBladeBarcode";
            this.txtOldLowerBladeBarcode.Size = new System.Drawing.Size(117, 22);
            this.txtOldLowerBladeBarcode.TabIndex = 1187;
            this.txtOldLowerBladeBarcode.Text = "211222-0009";
            this.txtOldLowerBladeBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label24.Location = new System.Drawing.Point(5, 68);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(29, 13);
            this.label24.TabIndex = 1186;
            this.label24.Text = "OLD";
            // 
            // BTN_UPPER_DF_KITTING
            // 
            this.BTN_UPPER_DF_KITTING.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_UPPER_DF_KITTING.Location = new System.Drawing.Point(156, 15);
            this.BTN_UPPER_DF_KITTING.Name = "BTN_UPPER_DF_KITTING";
            this.BTN_UPPER_DF_KITTING.Size = new System.Drawing.Size(58, 48);
            this.BTN_UPPER_DF_KITTING.TabIndex = 1185;
            this.BTN_UPPER_DF_KITTING.Text = "좌측 BLADE\r\n(501)";
            this.BTN_UPPER_DF_KITTING.UseVisualStyleBackColor = true;
            this.BTN_UPPER_DF_KITTING.Click += new System.EventHandler(this.BTN_UPPER_DF_KITTING_Click);
            // 
            // txtNewUpperBladeBarcode
            // 
            this.txtNewUpperBladeBarcode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewUpperBladeBarcode.Location = new System.Drawing.Point(38, 39);
            this.txtNewUpperBladeBarcode.Name = "txtNewUpperBladeBarcode";
            this.txtNewUpperBladeBarcode.Size = new System.Drawing.Size(117, 22);
            this.txtNewUpperBladeBarcode.TabIndex = 1184;
            this.txtNewUpperBladeBarcode.Text = "211222-0041";
            this.txtNewUpperBladeBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label22.Location = new System.Drawing.Point(4, 44);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(31, 13);
            this.label22.TabIndex = 1183;
            this.label22.Text = "NEW";
            // 
            // txtOldUpperBladeBarcode
            // 
            this.txtOldUpperBladeBarcode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOldUpperBladeBarcode.Location = new System.Drawing.Point(38, 16);
            this.txtOldUpperBladeBarcode.Name = "txtOldUpperBladeBarcode";
            this.txtOldUpperBladeBarcode.Size = new System.Drawing.Size(117, 22);
            this.txtOldUpperBladeBarcode.TabIndex = 1182;
            this.txtOldUpperBladeBarcode.Text = "211222-0010";
            this.txtOldUpperBladeBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label21.Location = new System.Drawing.Point(5, 21);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 13);
            this.label21.TabIndex = 1181;
            this.label21.Text = "OLD";
            // 
            // BTN_LOT_LOADING
            // 
            this.BTN_LOT_LOADING.Enabled = false;
            this.BTN_LOT_LOADING.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_LOT_LOADING.Location = new System.Drawing.Point(0, 698);
            this.BTN_LOT_LOADING.Name = "BTN_LOT_LOADING";
            this.BTN_LOT_LOADING.Size = new System.Drawing.Size(77, 52);
            this.BTN_LOT_LOADING.TabIndex = 1184;
            this.BTN_LOT_LOADING.Text = "LOT LOADING\r\n(301)";
            this.BTN_LOT_LOADING.UseVisualStyleBackColor = true;
            this.BTN_LOT_LOADING.Click += new System.EventHandler(this.BTN_LOT_LOADING_Click);
            // 
            // BTN_LOT_COMPLETE
            // 
            this.BTN_LOT_COMPLETE.Enabled = false;
            this.BTN_LOT_COMPLETE.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_LOT_COMPLETE.Location = new System.Drawing.Point(407, 698);
            this.BTN_LOT_COMPLETE.Name = "BTN_LOT_COMPLETE";
            this.BTN_LOT_COMPLETE.Size = new System.Drawing.Size(77, 52);
            this.BTN_LOT_COMPLETE.TabIndex = 1185;
            this.BTN_LOT_COMPLETE.Text = "LOT COMPLETE\r\n(302)";
            this.BTN_LOT_COMPLETE.UseVisualStyleBackColor = true;
            this.BTN_LOT_COMPLETE.Click += new System.EventHandler(this.BTN_LOT_COMPLETE_Click);
            // 
            // BTN_PANEL_LINE_IN
            // 
            this.BTN_PANEL_LINE_IN.Enabled = false;
            this.BTN_PANEL_LINE_IN.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_PANEL_LINE_IN.Location = new System.Drawing.Point(77, 698);
            this.BTN_PANEL_LINE_IN.Name = "BTN_PANEL_LINE_IN";
            this.BTN_PANEL_LINE_IN.Size = new System.Drawing.Size(77, 52);
            this.BTN_PANEL_LINE_IN.TabIndex = 1186;
            this.BTN_PANEL_LINE_IN.Text = "PANEL LINE IN\r\n(306)";
            this.BTN_PANEL_LINE_IN.UseVisualStyleBackColor = true;
            this.BTN_PANEL_LINE_IN.Click += new System.EventHandler(this.BTN_PANEL_LINE_IN_Click);
            // 
            // BTN_PANEL_LINE_OUT
            // 
            this.BTN_PANEL_LINE_OUT.Enabled = false;
            this.BTN_PANEL_LINE_OUT.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_PANEL_LINE_OUT.Location = new System.Drawing.Point(330, 698);
            this.BTN_PANEL_LINE_OUT.Name = "BTN_PANEL_LINE_OUT";
            this.BTN_PANEL_LINE_OUT.Size = new System.Drawing.Size(77, 52);
            this.BTN_PANEL_LINE_OUT.TabIndex = 1187;
            this.BTN_PANEL_LINE_OUT.Text = "PANEL LINE OUT\r\n(307)";
            this.BTN_PANEL_LINE_OUT.UseVisualStyleBackColor = true;
            this.BTN_PANEL_LINE_OUT.Click += new System.EventHandler(this.BTN_PANEL_LINE_OUT_Click);
            // 
            // BTN_PANEL_MODULE_OUT
            // 
            this.BTN_PANEL_MODULE_OUT.Enabled = false;
            this.BTN_PANEL_MODULE_OUT.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_PANEL_MODULE_OUT.Location = new System.Drawing.Point(242, 698);
            this.BTN_PANEL_MODULE_OUT.Name = "BTN_PANEL_MODULE_OUT";
            this.BTN_PANEL_MODULE_OUT.Size = new System.Drawing.Size(88, 52);
            this.BTN_PANEL_MODULE_OUT.TabIndex = 1189;
            this.BTN_PANEL_MODULE_OUT.Text = "PANEL MODULE OUT\r\n(309)";
            this.BTN_PANEL_MODULE_OUT.UseVisualStyleBackColor = true;
            this.BTN_PANEL_MODULE_OUT.Click += new System.EventHandler(this.BTN_PANEL_MODULE_OUT_Click);
            // 
            // BTN_PANEL_MODULE_IN
            // 
            this.BTN_PANEL_MODULE_IN.Enabled = false;
            this.BTN_PANEL_MODULE_IN.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_PANEL_MODULE_IN.Location = new System.Drawing.Point(154, 698);
            this.BTN_PANEL_MODULE_IN.Name = "BTN_PANEL_MODULE_IN";
            this.BTN_PANEL_MODULE_IN.Size = new System.Drawing.Size(88, 52);
            this.BTN_PANEL_MODULE_IN.TabIndex = 1188;
            this.BTN_PANEL_MODULE_IN.Text = "PANEL MODULE IN\r\n(308)";
            this.BTN_PANEL_MODULE_IN.UseVisualStyleBackColor = true;
            this.BTN_PANEL_MODULE_IN.Click += new System.EventHandler(this.BTN_PANEL_MODULE_IN_Click);
            // 
            // txtPanelInCount
            // 
            this.txtPanelInCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPanelInCount.Location = new System.Drawing.Point(83, 628);
            this.txtPanelInCount.Name = "txtPanelInCount";
            this.txtPanelInCount.Size = new System.Drawing.Size(49, 22);
            this.txtPanelInCount.TabIndex = 1190;
            this.txtPanelInCount.Text = "1";
            this.txtPanelInCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label25.Location = new System.Drawing.Point(2, 633);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(80, 13);
            this.label25.TabIndex = 1191;
            this.label25.Text = "PANEL IN 수량";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label26.Location = new System.Drawing.Point(2, 679);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(80, 13);
            this.label26.TabIndex = 1192;
            this.label26.Text = "Module Name";
            // 
            // txtStripBarcode
            // 
            this.txtStripBarcode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStripBarcode.Location = new System.Drawing.Point(83, 651);
            this.txtStripBarcode.Name = "txtStripBarcode";
            this.txtStripBarcode.Size = new System.Drawing.Size(139, 22);
            this.txtStripBarcode.TabIndex = 1195;
            this.txtStripBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label27.Location = new System.Drawing.Point(2, 656);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(76, 13);
            this.label27.TabIndex = 1194;
            this.label27.Text = "Strip Barcode";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GBX_EQP_CHANGE
            // 
            this.GBX_EQP_CHANGE.Controls.Add(this.BTN_EQP_CHANGE_COMPLETE);
            this.GBX_EQP_CHANGE.Controls.Add(this.TXT_EQP_CHANGE_COMMENT);
            this.GBX_EQP_CHANGE.Controls.Add(this.label29);
            this.GBX_EQP_CHANGE.Controls.Add(this.label28);
            this.GBX_EQP_CHANGE.Controls.Add(this.CBX_EQP_CHANGE_CODE);
            this.GBX_EQP_CHANGE.Controls.Add(this.lblUserID);
            this.GBX_EQP_CHANGE.Controls.Add(this.LBL_EQP_CODE_1);
            this.GBX_EQP_CHANGE.Location = new System.Drawing.Point(224, 549);
            this.GBX_EQP_CHANGE.Name = "GBX_EQP_CHANGE";
            this.GBX_EQP_CHANGE.Size = new System.Drawing.Size(256, 149);
            this.GBX_EQP_CHANGE.TabIndex = 1196;
            this.GBX_EQP_CHANGE.TabStop = false;
            this.GBX_EQP_CHANGE.Text = "지정 호기 변경 사유 입력";
            // 
            // BTN_EQP_CHANGE_COMPLETE
            // 
            this.BTN_EQP_CHANGE_COMPLETE.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_EQP_CHANGE_COMPLETE.Location = new System.Drawing.Point(130, 94);
            this.BTN_EQP_CHANGE_COMPLETE.Name = "BTN_EQP_CHANGE_COMPLETE";
            this.BTN_EQP_CHANGE_COMPLETE.Size = new System.Drawing.Size(120, 52);
            this.BTN_EQP_CHANGE_COMPLETE.TabIndex = 1183;
            this.BTN_EQP_CHANGE_COMPLETE.Text = "LOT EQP CHANGE COMPLETE\r\n(312)";
            this.BTN_EQP_CHANGE_COMPLETE.UseVisualStyleBackColor = true;
            this.BTN_EQP_CHANGE_COMPLETE.Click += new System.EventHandler(this.BTN_EQP_CHANGE_COMPLETE_Click);
            // 
            // TXT_EQP_CHANGE_COMMENT
            // 
            this.TXT_EQP_CHANGE_COMMENT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_EQP_CHANGE_COMMENT.Location = new System.Drawing.Point(69, 72);
            this.TXT_EQP_CHANGE_COMMENT.Name = "TXT_EQP_CHANGE_COMMENT";
            this.TXT_EQP_CHANGE_COMMENT.Size = new System.Drawing.Size(181, 22);
            this.TXT_EQP_CHANGE_COMMENT.TabIndex = 1182;
            this.TXT_EQP_CHANGE_COMMENT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label29.Location = new System.Drawing.Point(10, 77);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(55, 13);
            this.label29.TabIndex = 1181;
            this.label29.Text = "변경 사유";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label28.Location = new System.Drawing.Point(10, 55);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(55, 13);
            this.label28.TabIndex = 1180;
            this.label28.Text = "변경 원인";
            // 
            // CBX_EQP_CHANGE_CODE
            // 
            this.CBX_EQP_CHANGE_CODE.FormattingEnabled = true;
            this.CBX_EQP_CHANGE_CODE.Items.AddRange(new object[] {
            "1,설비 돌발 다운",
            "2,모델 연속 생산",
            "3,원/부자재 연속 사용",
            "4,인원 부족",
            "5,엔지니어 요청",
            "6,지급납기 요청",
            "7,제약설비",
            "8,WIP 증가",
            "9,AUTO TKIN"});
            this.CBX_EQP_CHANGE_CODE.Location = new System.Drawing.Point(69, 51);
            this.CBX_EQP_CHANGE_CODE.Name = "CBX_EQP_CHANGE_CODE";
            this.CBX_EQP_CHANGE_CODE.Size = new System.Drawing.Size(181, 20);
            this.CBX_EQP_CHANGE_CODE.TabIndex = 1179;
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblUserID.Location = new System.Drawing.Point(10, 34);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(59, 13);
            this.lblUserID.TabIndex = 1178;
            this.lblUserID.Text = "USER ID : ";
            // 
            // LBL_EQP_CODE_1
            // 
            this.LBL_EQP_CODE_1.AutoSize = true;
            this.LBL_EQP_CODE_1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LBL_EQP_CODE_1.Location = new System.Drawing.Point(10, 16);
            this.LBL_EQP_CODE_1.Name = "LBL_EQP_CODE_1";
            this.LBL_EQP_CODE_1.Size = new System.Drawing.Size(113, 13);
            this.LBL_EQP_CODE_1.TabIndex = 1177;
            this.LBL_EQP_CODE_1.Text = "EQUIPMENT CODE : ";
            // 
            // cbx_ModuleName
            // 
            this.cbx_ModuleName.FormattingEnabled = true;
            this.cbx_ModuleName.Items.AddRange(new object[] {
            "1,IN-LET",
            "2,STRIP PICKER",
            "3,DICING SAW STAE",
            "4,UNIT PICKER",
            "5,MAP-BLOCK 1",
            "6,MAP-BLOCK 2"});
            this.cbx_ModuleName.Location = new System.Drawing.Point(83, 676);
            this.cbx_ModuleName.Name = "cbx_ModuleName";
            this.cbx_ModuleName.Size = new System.Drawing.Size(139, 20);
            this.cbx_ModuleName.TabIndex = 1197;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label30.Location = new System.Drawing.Point(2, 612);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(167, 13);
            this.label30.TabIndex = 1198;
            this.label30.Text = "-MACHINE RUN ---------------";
            // 
            // lblProcessWorkingCondition_1
            // 
            this.lblProcessWorkingCondition_1.AutoSize = true;
            this.lblProcessWorkingCondition_1.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblProcessWorkingCondition_1.Location = new System.Drawing.Point(2, 759);
            this.lblProcessWorkingCondition_1.Name = "lblProcessWorkingCondition_1";
            this.lblProcessWorkingCondition_1.Size = new System.Drawing.Size(86, 13);
            this.lblProcessWorkingCondition_1.TabIndex = 1201;
            this.lblProcessWorkingCondition_1.Text = "LOTID#공정명#";
            // 
            // lblProcessWorkingCondition_Value1
            // 
            this.lblProcessWorkingCondition_Value1.AutoSize = true;
            this.lblProcessWorkingCondition_Value1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblProcessWorkingCondition_Value1.Location = new System.Drawing.Point(3, 777);
            this.lblProcessWorkingCondition_Value1.Name = "lblProcessWorkingCondition_Value1";
            this.lblProcessWorkingCondition_Value1.Size = new System.Drawing.Size(143, 13);
            this.lblProcessWorkingCondition_Value1.TabIndex = 1202;
            this.lblProcessWorkingCondition_Value1.Text = "Q05XXXX#SAWING(UNIT)#";
            // 
            // lblProcessWorkingCondition_Value2
            // 
            this.lblProcessWorkingCondition_Value2.AutoSize = true;
            this.lblProcessWorkingCondition_Value2.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblProcessWorkingCondition_Value2.Location = new System.Drawing.Point(3, 819);
            this.lblProcessWorkingCondition_Value2.Name = "lblProcessWorkingCondition_Value2";
            this.lblProcessWorkingCondition_Value2.Size = new System.Drawing.Size(94, 13);
            this.lblProcessWorkingCondition_Value2.TabIndex = 1204;
            this.lblProcessWorkingCondition_Value2.Text = "Q05XXXX#6450#";
            // 
            // lblProcessWorkingCondition_2
            // 
            this.lblProcessWorkingCondition_2.AutoSize = true;
            this.lblProcessWorkingCondition_2.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblProcessWorkingCondition_2.Location = new System.Drawing.Point(2, 801);
            this.lblProcessWorkingCondition_2.Name = "lblProcessWorkingCondition_2";
            this.lblProcessWorkingCondition_2.Size = new System.Drawing.Size(97, 13);
            this.lblProcessWorkingCondition_2.TabIndex = 1203;
            this.lblProcessWorkingCondition_2.Text = "LOTID#공정순서#";
            // 
            // lblProcessWorkingCondition_Value3
            // 
            this.lblProcessWorkingCondition_Value3.AutoSize = true;
            this.lblProcessWorkingCondition_Value3.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblProcessWorkingCondition_Value3.Location = new System.Drawing.Point(3, 836);
            this.lblProcessWorkingCondition_Value3.Name = "lblProcessWorkingCondition_Value3";
            this.lblProcessWorkingCondition_Value3.Size = new System.Drawing.Size(94, 13);
            this.lblProcessWorkingCondition_Value3.TabIndex = 1205;
            this.lblProcessWorkingCondition_Value3.Text = "Q05XXXX#6450#";
            // 
            // lblProcessWorkingCondition_Value4
            // 
            this.lblProcessWorkingCondition_Value4.AutoSize = true;
            this.lblProcessWorkingCondition_Value4.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblProcessWorkingCondition_Value4.Location = new System.Drawing.Point(3, 880);
            this.lblProcessWorkingCondition_Value4.Name = "lblProcessWorkingCondition_Value4";
            this.lblProcessWorkingCondition_Value4.Size = new System.Drawing.Size(76, 13);
            this.lblProcessWorkingCondition_Value4.TabIndex = 1207;
            this.lblProcessWorkingCondition_Value4.Text = "7050#R1341#";
            // 
            // lblProcessWorkingCondition_3
            // 
            this.lblProcessWorkingCondition_3.AutoSize = true;
            this.lblProcessWorkingCondition_3.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblProcessWorkingCondition_3.Location = new System.Drawing.Point(2, 862);
            this.lblProcessWorkingCondition_3.Name = "lblProcessWorkingCondition_3";
            this.lblProcessWorkingCondition_3.Size = new System.Drawing.Size(109, 13);
            this.lblProcessWorkingCondition_3.TabIndex = 1206;
            this.lblProcessWorkingCondition_3.Text = "공정순서#공정코드#";
            // 
            // BTN_RUN
            // 
            this.BTN_RUN.Enabled = false;
            this.BTN_RUN.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_RUN.Location = new System.Drawing.Point(9, 554);
            this.BTN_RUN.Name = "BTN_RUN";
            this.BTN_RUN.Size = new System.Drawing.Size(101, 53);
            this.BTN_RUN.TabIndex = 1208;
            this.BTN_RUN.Text = "RUN";
            this.BTN_RUN.UseVisualStyleBackColor = true;
            this.BTN_RUN.Click += new System.EventHandler(this.BTN_RUN_Click);
            // 
            // SecsGEM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 901);
            this.Controls.Add(this.BTN_RUN);
            this.Controls.Add(this.lblProcessWorkingCondition_Value4);
            this.Controls.Add(this.lblProcessWorkingCondition_3);
            this.Controls.Add(this.lblProcessWorkingCondition_Value3);
            this.Controls.Add(this.lblProcessWorkingCondition_Value2);
            this.Controls.Add(this.lblProcessWorkingCondition_2);
            this.Controls.Add(this.lblProcessWorkingCondition_Value1);
            this.Controls.Add(this.lblProcessWorkingCondition_1);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.cbx_ModuleName);
            this.Controls.Add(this.GBX_EQP_CHANGE);
            this.Controls.Add(this.txtStripBarcode);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.txtPanelInCount);
            this.Controls.Add(this.BTN_PANEL_MODULE_OUT);
            this.Controls.Add(this.BTN_PANEL_MODULE_IN);
            this.Controls.Add(this.BTN_PANEL_LINE_OUT);
            this.Controls.Add(this.BTN_PANEL_LINE_IN);
            this.Controls.Add(this.BTN_LOT_COMPLETE);
            this.Controls.Add(this.BTN_LOT_LOADING);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.GBX_LOT_LOSS);
            this.Controls.Add(this.BTN_LOT_CANCELED);
            this.Controls.Add(this.BTN_ALARM_RESET);
            this.Controls.Add(this.BTN_ALARM_SET);
            this.Controls.Add(this.TXT_ALRAM_NUM);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.LBL_EQP_CODE);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtLotCnt);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BTN_EXIT);
            this.Controls.Add(this.BTN_LOT_REQUEST);
            this.Controls.Add(this.SetRecipe);
            this.Controls.Add(this.txtRecipe);
            this.Controls.Add(this.txtGroup);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.SetUserID);
            this.Controls.Add(this.SetLotID);
            this.Controls.Add(this.ChkEQ_PM);
            this.Controls.Add(this.bEQSTATE);
            this.Controls.Add(this.cbEquipmentState);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLotID);
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
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.btnRemote);
            this.Controls.Add(this.btnOffline);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblControlState);
            this.Controls.Add(this.lblCommState);
            this.Controls.Add(this.lblConnectState);
            this.Controls.Add(this.gbTerminalMsg);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "SecsGEM";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SecsGEM_FormClosing);
            this.Load += new System.EventHandler(this.SecsGEM_Load);
            this.gbTerminalMsg.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GBX_LOT_LOSS.ResumeLayout(false);
            this.GBX_LOT_LOSS.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.GBX_EQP_CHANGE.ResumeLayout(false);
            this.GBX_EQP_CHANGE.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbTerminalMsg;
        private System.Windows.Forms.Label lblTerminalMsg;
        private System.Windows.Forms.Button btnLocal;
        private System.Windows.Forms.Label lblUseMes;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.Button btnRemote;
        private System.Windows.Forms.Button btnOffline;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblControlState;
        private System.Windows.Forms.Label lblCommState;
        private System.Windows.Forms.Label lblConnectState;
        private System.Windows.Forms.Timer timerGem;
        private System.Windows.Forms.Timer timer_SetClock;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbPort;
        private System.Windows.Forms.Label lbDevice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbLinkTest;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbModeSelect;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbT3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbT5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbT6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbT7;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lbT8;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lbCommRequest;
        private System.Windows.Forms.TextBox txtLotID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbEquipmentState;
        private System.Windows.Forms.Button bEQSTATE;
        private JCS.ToggleSwitch ChkEQ_PM;
        private System.Windows.Forms.Button SetLotID;
        private System.Windows.Forms.Button SetUserID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtGroup;
        private System.Windows.Forms.TextBox txtRecipe;
        private System.Windows.Forms.Button SetRecipe;
        private System.Windows.Forms.Button BTN_LOT_REQUEST;
        private System.Windows.Forms.Button BTN_EXIT;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RBT_LOT_TYPE_5;
        private System.Windows.Forms.RadioButton RBT_LOT_TYPE_4;
        private System.Windows.Forms.RadioButton RBT_LOT_TYPE_3;
        private System.Windows.Forms.RadioButton RBT_LOT_TYPE_2;
        private System.Windows.Forms.RadioButton RBT_LOT_TYPE_1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtLotCnt;
        private System.Windows.Forms.Label LBL_EQP_CODE;
        private System.Windows.Forms.TextBox TXT_ALRAM_NUM;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button BTN_ALARM_SET;
        private System.Windows.Forms.Button BTN_ALARM_RESET;
        private System.Windows.Forms.Button BTN_LOT_CANCELED;
        private System.Windows.Forms.GroupBox GBX_LOT_LOSS;
        private System.Windows.Forms.TextBox TXT_LOSS_MEMO;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.RadioButton RDB_LOSS_6;
        private System.Windows.Forms.RadioButton RDB_LOSS_5;
        private System.Windows.Forms.RadioButton RDB_LOSS_4;
        private System.Windows.Forms.RadioButton RDB_LOSS_3;
        private System.Windows.Forms.RadioButton RDB_LOSS_2;
        private System.Windows.Forms.RadioButton RDB_LOSS_1;
        private System.Windows.Forms.TextBox txtLossEndTime;
        private System.Windows.Forms.TextBox txtLossStartTime;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button BTN_LOT_LOSS;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BTN_LOWER_DF_KITTING;
        private System.Windows.Forms.TextBox txtNewLowerBladeBarcode;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtOldLowerBladeBarcode;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button BTN_UPPER_DF_KITTING;
        private System.Windows.Forms.TextBox txtNewUpperBladeBarcode;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtOldUpperBladeBarcode;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button BTN_LOT_LOADING;
        private System.Windows.Forms.Button BTN_LOT_COMPLETE;
        private System.Windows.Forms.Button BTN_PANEL_LINE_IN;
        private System.Windows.Forms.Button BTN_PANEL_LINE_OUT;
        private System.Windows.Forms.Button BTN_PANEL_MODULE_OUT;
        private System.Windows.Forms.Button BTN_PANEL_MODULE_IN;
        private System.Windows.Forms.TextBox txtPanelInCount;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtStripBarcode;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox GBX_EQP_CHANGE;
        private System.Windows.Forms.Label LBL_EQP_CODE_1;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Button BTN_EQP_CHANGE_COMPLETE;
        private System.Windows.Forms.TextBox TXT_EQP_CHANGE_COMMENT;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox CBX_EQP_CHANGE_CODE;
        private System.Windows.Forms.ComboBox cbx_ModuleName;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label lblProcessWorkingCondition_1;
        private System.Windows.Forms.Label lblProcessWorkingCondition_Value1;
        private System.Windows.Forms.Label lblProcessWorkingCondition_Value2;
        private System.Windows.Forms.Label lblProcessWorkingCondition_2;
        private System.Windows.Forms.Label lblProcessWorkingCondition_Value3;
        private System.Windows.Forms.Label lblProcessWorkingCondition_Value4;
        private System.Windows.Forms.Label lblProcessWorkingCondition_3;
        private System.Windows.Forms.Button BTN_RUN;
    }
}

