namespace LIB_.SubFROMLib
{
    partial class LotIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LotIn));
            this.txtLotId = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRegLotNo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStripCounter = new System.Windows.Forms.TextBox();
            this.txtITSId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LOT_INFO = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RBT_LOT_TYPE_5 = new System.Windows.Forms.RadioButton();
            this.RBT_LOT_TYPE_4 = new System.Windows.Forms.RadioButton();
            this.RBT_LOT_TYPE_3 = new System.Windows.Forms.RadioButton();
            this.RBT_LOT_TYPE_2 = new System.Windows.Forms.RadioButton();
            this.RBT_LOT_TYPE_1 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.GBX_LOT_LOSS = new System.Windows.Forms.GroupBox();
            this.LBL_LossEndTime = new System.Windows.Forms.Label();
            this.BTN_LOT_LOSS = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.TXT_LOSS_MEMO = new System.Windows.Forms.TextBox();
            this.LBL_LossStartTime = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.RDB_LOSS_6 = new System.Windows.Forms.RadioButton();
            this.RDB_LOSS_5 = new System.Windows.Forms.RadioButton();
            this.RDB_LOSS_4 = new System.Windows.Forms.RadioButton();
            this.RDB_LOSS_3 = new System.Windows.Forms.RadioButton();
            this.RDB_LOSS_2 = new System.Windows.Forms.RadioButton();
            this.RDB_LOSS_1 = new System.Windows.Forms.RadioButton();
            this.label16 = new System.Windows.Forms.Label();
            this.GBX_EQP_CHANGE = new System.Windows.Forms.GroupBox();
            this.BTN_EQP_CHANGE_COMPLETE = new System.Windows.Forms.Button();
            this.TXT_EQP_CHANGE_COMMENT = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.CBX_EQP_CHANGE_CODE = new System.Windows.Forms.ComboBox();
            this.lblUserID = new System.Windows.Forms.Label();
            this.LBL_EQP_CODE_1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.lblTerminalMsg = new System.Windows.Forms.Label();
            this.btnCanceledLotNo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.LOT_INFO)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.GBX_LOT_LOSS.SuspendLayout();
            this.GBX_EQP_CHANGE.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLotId
            // 
            this.txtLotId.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtLotId.Location = new System.Drawing.Point(87, 27);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Size = new System.Drawing.Size(504, 30);
            this.txtLotId.TabIndex = 197;
            // 
            // btnCancel
            // 
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(452, 133);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 44);
            this.btnCancel.TabIndex = 196;
            this.btnCancel.Text = "EXIT";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRegLotNo
            // 
            this.btnRegLotNo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegLotNo.Location = new System.Drawing.Point(174, 133);
            this.btnRegLotNo.Name = "btnRegLotNo";
            this.btnRegLotNo.Size = new System.Drawing.Size(139, 44);
            this.btnRegLotNo.TabIndex = 195;
            this.btnRegLotNo.Text = "등록";
            this.btnRegLotNo.UseVisualStyleBackColor = true;
            this.btnRegLotNo.Click += new System.EventHandler(this.btnRegLotNo_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 198;
            this.label2.Text = "STRIP 수량 : ";
            // 
            // txtStripCounter
            // 
            this.txtStripCounter.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStripCounter.Location = new System.Drawing.Point(87, 134);
            this.txtStripCounter.Name = "txtStripCounter";
            this.txtStripCounter.Size = new System.Drawing.Size(87, 27);
            this.txtStripCounter.TabIndex = 199;
            this.txtStripCounter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtITSId
            // 
            this.txtITSId.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtITSId.Location = new System.Drawing.Point(87, 58);
            this.txtITSId.Name = "txtITSId";
            this.txtITSId.Size = new System.Drawing.Size(504, 30);
            this.txtITSId.TabIndex = 200;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 15);
            this.label3.TabIndex = 201;
            this.label3.Text = "ITS ID : ";
            // 
            // LOT_INFO
            // 
            this.LOT_INFO.Dock = System.Windows.Forms.DockStyle.Top;
            this.LOT_INFO.Image = ((System.Drawing.Image)(resources.GetObject("LOT_INFO.Image")));
            this.LOT_INFO.Location = new System.Drawing.Point(0, 0);
            this.LOT_INFO.Name = "LOT_INFO";
            this.LOT_INFO.Size = new System.Drawing.Size(594, 26);
            this.LOT_INFO.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LOT_INFO.TabIndex = 202;
            this.LOT_INFO.TabStop = false;
            this.LOT_INFO.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RBT_LOT_TYPE_5);
            this.groupBox1.Controls.Add(this.RBT_LOT_TYPE_4);
            this.groupBox1.Controls.Add(this.RBT_LOT_TYPE_3);
            this.groupBox1.Controls.Add(this.RBT_LOT_TYPE_2);
            this.groupBox1.Controls.Add(this.RBT_LOT_TYPE_1);
            this.groupBox1.Location = new System.Drawing.Point(87, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(504, 43);
            this.groupBox1.TabIndex = 1174;
            this.groupBox1.TabStop = false;
            // 
            // RBT_LOT_TYPE_5
            // 
            this.RBT_LOT_TYPE_5.AutoSize = true;
            this.RBT_LOT_TYPE_5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBT_LOT_TYPE_5.Location = new System.Drawing.Point(416, 12);
            this.RBT_LOT_TYPE_5.Name = "RBT_LOT_TYPE_5";
            this.RBT_LOT_TYPE_5.Size = new System.Drawing.Size(69, 23);
            this.RBT_LOT_TYPE_5.TabIndex = 4;
            this.RBT_LOT_TYPE_5.TabStop = true;
            this.RBT_LOT_TYPE_5.Text = "재작업";
            this.RBT_LOT_TYPE_5.UseVisualStyleBackColor = true;
            // 
            // RBT_LOT_TYPE_4
            // 
            this.RBT_LOT_TYPE_4.AutoSize = true;
            this.RBT_LOT_TYPE_4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBT_LOT_TYPE_4.Location = new System.Drawing.Point(302, 12);
            this.RBT_LOT_TYPE_4.Name = "RBT_LOT_TYPE_4";
            this.RBT_LOT_TYPE_4.Size = new System.Drawing.Size(69, 23);
            this.RBT_LOT_TYPE_4.TabIndex = 3;
            this.RBT_LOT_TYPE_4.TabStop = true;
            this.RBT_LOT_TYPE_4.Text = "재초도";
            this.RBT_LOT_TYPE_4.UseVisualStyleBackColor = true;
            // 
            // RBT_LOT_TYPE_3
            // 
            this.RBT_LOT_TYPE_3.AutoSize = true;
            this.RBT_LOT_TYPE_3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBT_LOT_TYPE_3.Location = new System.Drawing.Point(201, 12);
            this.RBT_LOT_TYPE_3.Name = "RBT_LOT_TYPE_3";
            this.RBT_LOT_TYPE_3.Size = new System.Drawing.Size(55, 23);
            this.RBT_LOT_TYPE_3.TabIndex = 2;
            this.RBT_LOT_TYPE_3.TabStop = true;
            this.RBT_LOT_TYPE_3.Text = "더미";
            this.RBT_LOT_TYPE_3.UseVisualStyleBackColor = true;
            // 
            // RBT_LOT_TYPE_2
            // 
            this.RBT_LOT_TYPE_2.AutoSize = true;
            this.RBT_LOT_TYPE_2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBT_LOT_TYPE_2.Location = new System.Drawing.Point(102, 12);
            this.RBT_LOT_TYPE_2.Name = "RBT_LOT_TYPE_2";
            this.RBT_LOT_TYPE_2.Size = new System.Drawing.Size(55, 23);
            this.RBT_LOT_TYPE_2.TabIndex = 1;
            this.RBT_LOT_TYPE_2.TabStop = true;
            this.RBT_LOT_TYPE_2.Text = "본낫";
            this.RBT_LOT_TYPE_2.UseVisualStyleBackColor = true;
            // 
            // RBT_LOT_TYPE_1
            // 
            this.RBT_LOT_TYPE_1.AutoSize = true;
            this.RBT_LOT_TYPE_1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBT_LOT_TYPE_1.Location = new System.Drawing.Point(14, 12);
            this.RBT_LOT_TYPE_1.Name = "RBT_LOT_TYPE_1";
            this.RBT_LOT_TYPE_1.Size = new System.Drawing.Size(55, 23);
            this.RBT_LOT_TYPE_1.TabIndex = 0;
            this.RBT_LOT_TYPE_1.TabStop = true;
            this.RBT_LOT_TYPE_1.Text = "초도";
            this.RBT_LOT_TYPE_1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 15);
            this.label4.TabIndex = 1175;
            this.label4.Text = "LOT 종류 : ";
            // 
            // GBX_LOT_LOSS
            // 
            this.GBX_LOT_LOSS.Controls.Add(this.LBL_LossEndTime);
            this.GBX_LOT_LOSS.Controls.Add(this.BTN_LOT_LOSS);
            this.GBX_LOT_LOSS.Controls.Add(this.label20);
            this.GBX_LOT_LOSS.Controls.Add(this.TXT_LOSS_MEMO);
            this.GBX_LOT_LOSS.Controls.Add(this.LBL_LossStartTime);
            this.GBX_LOT_LOSS.Controls.Add(this.label19);
            this.GBX_LOT_LOSS.Controls.Add(this.RDB_LOSS_6);
            this.GBX_LOT_LOSS.Controls.Add(this.RDB_LOSS_5);
            this.GBX_LOT_LOSS.Controls.Add(this.RDB_LOSS_4);
            this.GBX_LOT_LOSS.Controls.Add(this.RDB_LOSS_3);
            this.GBX_LOT_LOSS.Controls.Add(this.RDB_LOSS_2);
            this.GBX_LOT_LOSS.Controls.Add(this.RDB_LOSS_1);
            this.GBX_LOT_LOSS.Controls.Add(this.label16);
            this.GBX_LOT_LOSS.Enabled = false;
            this.GBX_LOT_LOSS.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBX_LOT_LOSS.Location = new System.Drawing.Point(1, 179);
            this.GBX_LOT_LOSS.Name = "GBX_LOT_LOSS";
            this.GBX_LOT_LOSS.Size = new System.Drawing.Size(294, 183);
            this.GBX_LOT_LOSS.TabIndex = 1183;
            this.GBX_LOT_LOSS.TabStop = false;
            this.GBX_LOT_LOSS.Text = "LOT LOSS";
            // 
            // LBL_LossEndTime
            // 
            this.LBL_LossEndTime.BackColor = System.Drawing.Color.Transparent;
            this.LBL_LossEndTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_LossEndTime.Location = new System.Drawing.Point(160, 39);
            this.LBL_LossEndTime.Name = "LBL_LossEndTime";
            this.LBL_LossEndTime.Size = new System.Drawing.Size(122, 17);
            this.LBL_LossEndTime.TabIndex = 1199;
            this.LBL_LossEndTime.Text = "YYYYMMDDhhmmsscc";
            this.LBL_LossEndTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BTN_LOT_LOSS
            // 
            this.BTN_LOT_LOSS.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_LOT_LOSS.Location = new System.Drawing.Point(3, 136);
            this.BTN_LOT_LOSS.Name = "BTN_LOT_LOSS";
            this.BTN_LOT_LOSS.Size = new System.Drawing.Size(284, 42);
            this.BTN_LOT_LOSS.TabIndex = 1182;
            this.BTN_LOT_LOSS.Text = "LOT EQP LOSS COMPLETE (315)";
            this.BTN_LOT_LOSS.UseVisualStyleBackColor = true;
            this.BTN_LOT_LOSS.Click += new System.EventHandler(this.BTN_LOT_LOSS_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(136, 37);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(19, 16);
            this.label20.TabIndex = 1181;
            this.label20.Text = "~";
            // 
            // TXT_LOSS_MEMO
            // 
            this.TXT_LOSS_MEMO.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_LOSS_MEMO.Location = new System.Drawing.Point(70, 112);
            this.TXT_LOSS_MEMO.Name = "TXT_LOSS_MEMO";
            this.TXT_LOSS_MEMO.Size = new System.Drawing.Size(212, 22);
            this.TXT_LOSS_MEMO.TabIndex = 1180;
            this.TXT_LOSS_MEMO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LBL_LossStartTime
            // 
            this.LBL_LossStartTime.BackColor = System.Drawing.Color.Transparent;
            this.LBL_LossStartTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_LossStartTime.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LBL_LossStartTime.Location = new System.Drawing.Point(67, 20);
            this.LBL_LossStartTime.Name = "LBL_LossStartTime";
            this.LBL_LossStartTime.Size = new System.Drawing.Size(123, 16);
            this.LBL_LossStartTime.TabIndex = 1198;
            this.LBL_LossStartTime.Text = "YYYYMMDDhhmmsscc";
            this.LBL_LossStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(3, 114);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 16);
            this.label19.TabIndex = 1179;
            this.label19.Text = "특이사항 : ";
            // 
            // RDB_LOSS_6
            // 
            this.RDB_LOSS_6.AutoSize = true;
            this.RDB_LOSS_6.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RDB_LOSS_6.Location = new System.Drawing.Point(185, 85);
            this.RDB_LOSS_6.Name = "RDB_LOSS_6";
            this.RDB_LOSS_6.Size = new System.Drawing.Size(91, 22);
            this.RDB_LOSS_6.TabIndex = 1178;
            this.RDB_LOSS_6.TabStop = true;
            this.RDB_LOSS_6.Text = "TOOL교체";
            this.RDB_LOSS_6.UseVisualStyleBackColor = true;
            // 
            // RDB_LOSS_5
            // 
            this.RDB_LOSS_5.AutoSize = true;
            this.RDB_LOSS_5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RDB_LOSS_5.Location = new System.Drawing.Point(95, 85);
            this.RDB_LOSS_5.Name = "RDB_LOSS_5";
            this.RDB_LOSS_5.Size = new System.Drawing.Size(78, 22);
            this.RDB_LOSS_5.TabIndex = 1177;
            this.RDB_LOSS_5.TabStop = true;
            this.RDB_LOSS_5.Text = "청소시간";
            this.RDB_LOSS_5.UseVisualStyleBackColor = true;
            // 
            // RDB_LOSS_4
            // 
            this.RDB_LOSS_4.AutoSize = true;
            this.RDB_LOSS_4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RDB_LOSS_4.Location = new System.Drawing.Point(6, 86);
            this.RDB_LOSS_4.Name = "RDB_LOSS_4";
            this.RDB_LOSS_4.Size = new System.Drawing.Size(78, 22);
            this.RDB_LOSS_4.TabIndex = 1176;
            this.RDB_LOSS_4.TabStop = true;
            this.RDB_LOSS_4.Text = "인원부족";
            this.RDB_LOSS_4.UseVisualStyleBackColor = true;
            // 
            // RDB_LOSS_3
            // 
            this.RDB_LOSS_3.AutoSize = true;
            this.RDB_LOSS_3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RDB_LOSS_3.Location = new System.Drawing.Point(185, 58);
            this.RDB_LOSS_3.Name = "RDB_LOSS_3";
            this.RDB_LOSS_3.Size = new System.Drawing.Size(78, 22);
            this.RDB_LOSS_3.TabIndex = 1175;
            this.RDB_LOSS_3.TabStop = true;
            this.RDB_LOSS_3.Text = "휴게시간";
            this.RDB_LOSS_3.UseVisualStyleBackColor = true;
            // 
            // RDB_LOSS_2
            // 
            this.RDB_LOSS_2.AutoSize = true;
            this.RDB_LOSS_2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RDB_LOSS_2.Location = new System.Drawing.Point(95, 58);
            this.RDB_LOSS_2.Name = "RDB_LOSS_2";
            this.RDB_LOSS_2.Size = new System.Drawing.Size(78, 22);
            this.RDB_LOSS_2.TabIndex = 1174;
            this.RDB_LOSS_2.TabStop = true;
            this.RDB_LOSS_2.Text = "자재부족";
            this.RDB_LOSS_2.UseVisualStyleBackColor = true;
            // 
            // RDB_LOSS_1
            // 
            this.RDB_LOSS_1.AutoSize = true;
            this.RDB_LOSS_1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RDB_LOSS_1.Location = new System.Drawing.Point(6, 59);
            this.RDB_LOSS_1.Name = "RDB_LOSS_1";
            this.RDB_LOSS_1.Size = new System.Drawing.Size(78, 22);
            this.RDB_LOSS_1.TabIndex = 1173;
            this.RDB_LOSS_1.TabStop = true;
            this.RDB_LOSS_1.Text = "재공부족";
            this.RDB_LOSS_1.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(5, 28);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 16);
            this.label16.TabIndex = 1170;
            this.label16.Text = "Loss기간 : ";
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
            this.GBX_EQP_CHANGE.Enabled = false;
            this.GBX_EQP_CHANGE.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GBX_EQP_CHANGE.Location = new System.Drawing.Point(297, 179);
            this.GBX_EQP_CHANGE.Name = "GBX_EQP_CHANGE";
            this.GBX_EQP_CHANGE.Size = new System.Drawing.Size(294, 183);
            this.GBX_EQP_CHANGE.TabIndex = 1197;
            this.GBX_EQP_CHANGE.TabStop = false;
            this.GBX_EQP_CHANGE.Text = "지정 호기 변경 사유 입력";
            // 
            // BTN_EQP_CHANGE_COMPLETE
            // 
            this.BTN_EQP_CHANGE_COMPLETE.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_EQP_CHANGE_COMPLETE.Location = new System.Drawing.Point(4, 136);
            this.BTN_EQP_CHANGE_COMPLETE.Name = "BTN_EQP_CHANGE_COMPLETE";
            this.BTN_EQP_CHANGE_COMPLETE.Size = new System.Drawing.Size(283, 42);
            this.BTN_EQP_CHANGE_COMPLETE.TabIndex = 1183;
            this.BTN_EQP_CHANGE_COMPLETE.Text = "LOT EQP CHANGE COMPLETE (312)";
            this.BTN_EQP_CHANGE_COMPLETE.UseVisualStyleBackColor = true;
            this.BTN_EQP_CHANGE_COMPLETE.Click += new System.EventHandler(this.BTN_EQP_CHANGE_COMPLETE_Click);
            // 
            // TXT_EQP_CHANGE_COMMENT
            // 
            this.TXT_EQP_CHANGE_COMMENT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_EQP_CHANGE_COMMENT.Location = new System.Drawing.Point(70, 110);
            this.TXT_EQP_CHANGE_COMMENT.Name = "TXT_EQP_CHANGE_COMMENT";
            this.TXT_EQP_CHANGE_COMMENT.Size = new System.Drawing.Size(212, 22);
            this.TXT_EQP_CHANGE_COMMENT.TabIndex = 1182;
            this.TXT_EQP_CHANGE_COMMENT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(3, 114);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(69, 16);
            this.label29.TabIndex = 1181;
            this.label29.Text = "변경 사유 : ";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(3, 88);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(65, 16);
            this.label28.TabIndex = 1180;
            this.label28.Text = "변경 원인 :";
            // 
            // CBX_EQP_CHANGE_CODE
            // 
            this.CBX_EQP_CHANGE_CODE.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.CBX_EQP_CHANGE_CODE.Location = new System.Drawing.Point(70, 85);
            this.CBX_EQP_CHANGE_CODE.Name = "CBX_EQP_CHANGE_CODE";
            this.CBX_EQP_CHANGE_CODE.Size = new System.Drawing.Size(212, 24);
            this.CBX_EQP_CHANGE_CODE.TabIndex = 1179;
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserID.Location = new System.Drawing.Point(3, 43);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(68, 16);
            this.lblUserID.TabIndex = 1178;
            this.lblUserID.Text = "USER ID : ";
            // 
            // LBL_EQP_CODE_1
            // 
            this.LBL_EQP_CODE_1.AutoSize = true;
            this.LBL_EQP_CODE_1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_EQP_CODE_1.Location = new System.Drawing.Point(3, 19);
            this.LBL_EQP_CODE_1.Name = "LBL_EQP_CODE_1";
            this.LBL_EQP_CODE_1.Size = new System.Drawing.Size(125, 16);
            this.LBL_EQP_CODE_1.TabIndex = 1177;
            this.LBL_EQP_CODE_1.Text = "EQUIPMENT CODE : ";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 15);
            this.label5.TabIndex = 1198;
            this.label5.Text = "LOT ID : ";
            // 
            // lblTerminalMsg
            // 
            this.lblTerminalMsg.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTerminalMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTerminalMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTerminalMsg.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerminalMsg.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblTerminalMsg.Location = new System.Drawing.Point(0, 362);
            this.lblTerminalMsg.Name = "lblTerminalMsg";
            this.lblTerminalMsg.Size = new System.Drawing.Size(594, 30);
            this.lblTerminalMsg.TabIndex = 1199;
            this.lblTerminalMsg.Tag = "24";
            this.lblTerminalMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCanceledLotNo
            // 
            this.btnCanceledLotNo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCanceledLotNo.Location = new System.Drawing.Point(313, 133);
            this.btnCanceledLotNo.Name = "btnCanceledLotNo";
            this.btnCanceledLotNo.Size = new System.Drawing.Size(139, 44);
            this.btnCanceledLotNo.TabIndex = 1200;
            this.btnCanceledLotNo.Text = "등록 취소";
            this.btnCanceledLotNo.UseVisualStyleBackColor = true;
            this.btnCanceledLotNo.Click += new System.EventHandler(this.btnCanceledLotNo_Click);
            // 
            // LotIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 392);
            this.ControlBox = false;
            this.Controls.Add(this.btnCanceledLotNo);
            this.Controls.Add(this.lblTerminalMsg);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.GBX_EQP_CHANGE);
            this.Controls.Add(this.GBX_LOT_LOSS);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LOT_INFO);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtITSId);
            this.Controls.Add(this.txtStripCounter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLotId);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRegLotNo);
            this.Name = "LotIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LOT ID WINDOW";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.LOT_INFO)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GBX_LOT_LOSS.ResumeLayout(false);
            this.GBX_LOT_LOSS.PerformLayout();
            this.GBX_EQP_CHANGE.ResumeLayout(false);
            this.GBX_EQP_CHANGE.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLotId;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRegLotNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStripCounter;
        private System.Windows.Forms.TextBox txtITSId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox LOT_INFO;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RBT_LOT_TYPE_5;
        private System.Windows.Forms.RadioButton RBT_LOT_TYPE_4;
        private System.Windows.Forms.RadioButton RBT_LOT_TYPE_3;
        private System.Windows.Forms.RadioButton RBT_LOT_TYPE_2;
        private System.Windows.Forms.RadioButton RBT_LOT_TYPE_1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox GBX_LOT_LOSS;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox TXT_LOSS_MEMO;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.RadioButton RDB_LOSS_6;
        private System.Windows.Forms.RadioButton RDB_LOSS_5;
        private System.Windows.Forms.RadioButton RDB_LOSS_4;
        private System.Windows.Forms.RadioButton RDB_LOSS_3;
        private System.Windows.Forms.RadioButton RDB_LOSS_2;
        private System.Windows.Forms.RadioButton RDB_LOSS_1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button BTN_LOT_LOSS;
        private System.Windows.Forms.GroupBox GBX_EQP_CHANGE;
        private System.Windows.Forms.Button BTN_EQP_CHANGE_COMPLETE;
        private System.Windows.Forms.TextBox TXT_EQP_CHANGE_COMMENT;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox CBX_EQP_CHANGE_CODE;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label LBL_EQP_CODE_1;
        private System.Windows.Forms.Label LBL_LossStartTime;
        private System.Windows.Forms.Label LBL_LossEndTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTerminalMsg;
        private System.Windows.Forms.Button btnCanceledLotNo;
    }
}