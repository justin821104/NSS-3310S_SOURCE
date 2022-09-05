namespace NSS_3310S
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panTitleBar = new System.Windows.Forms.Panel();
            this.lbAppVersion = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.editPPID = new System.Windows.Forms.Label();
            this.pOPPanel = new System.Windows.Forms.Panel();
            this.lbUDP = new System.Windows.Forms.Label();
            this.lbBARCODE = new System.Windows.Forms.Label();
            this.lbITS = new System.Windows.Forms.Label();
            this.swVisionSet = new System.Windows.Forms.Button();
            this.swSet = new System.Windows.Forms.Button();
            this.swMotionSet = new System.Windows.Forms.Button();
            this.swDEVICE = new System.Windows.Forms.Button();
            this.swIO = new System.Windows.Forms.Button();
            this.swHistory = new System.Windows.Forms.Button();
            this.swManual = new System.Windows.Forms.Button();
            this.swAuto = new System.Windows.Forms.Button();
            this.swLogIn = new System.Windows.Forms.Button();
            this.editRecipe = new System.Windows.Forms.Label();
            this.STRIP = new System.Windows.Forms.Label();
            this.UNLOADING = new System.Windows.Forms.Label();
            this.PALLET = new System.Windows.Forms.Label();
            this.HEAD = new System.Windows.Forms.Label();
            this.BTN_EXIT = new System.Windows.Forms.Button();
            this.pnlLOGO = new System.Windows.Forms.Panel();
            this.lblTENKEY = new System.Windows.Forms.Label();
            this.pnlTowerY = new System.Windows.Forms.Panel();
            this.lbl_CurrentRecipe = new System.Windows.Forms.Label();
            this.pblTowerR = new System.Windows.Forms.Panel();
            this.pnlTowerG = new System.Windows.Forms.Panel();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.lbSpeed = new System.Windows.Forms.Label();
            this.lbErrLanguage = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.editGroup = new System.Windows.Forms.Label();
            this.editErrName = new System.Windows.Forms.Label();
            this.lbLoginStatus = new System.Windows.Forms.Label();
            this.editErrCode = new System.Windows.Forms.Label();
            this.lbDryRunStatus = new System.Windows.Forms.Label();
            this.panClientView = new System.Windows.Forms.Panel();
            this.TmrMAIN = new System.Windows.Forms.Timer(this.components);
            this.SPC = new System.Windows.Forms.Timer(this.components);
            this.panTitleBar.SuspendLayout();
            this.pOPPanel.SuspendLayout();
            this.pnlLOGO.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panTitleBar
            // 
            this.panTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panTitleBar.Controls.Add(this.lbAppVersion);
            this.panTitleBar.Controls.Add(this.lbVersion);
            this.panTitleBar.Controls.Add(this.editPPID);
            this.panTitleBar.Controls.Add(this.pOPPanel);
            this.panTitleBar.Controls.Add(this.editRecipe);
            this.panTitleBar.Controls.Add(this.STRIP);
            this.panTitleBar.Controls.Add(this.UNLOADING);
            this.panTitleBar.Controls.Add(this.PALLET);
            this.panTitleBar.Controls.Add(this.HEAD);
            this.panTitleBar.Controls.Add(this.BTN_EXIT);
            this.panTitleBar.Controls.Add(this.pnlLOGO);
            this.panTitleBar.Controls.Add(this.pnlTowerY);
            this.panTitleBar.Controls.Add(this.lbl_CurrentRecipe);
            this.panTitleBar.Controls.Add(this.pblTowerR);
            this.panTitleBar.Controls.Add(this.pnlTowerG);
            this.panTitleBar.Controls.Add(this.GroupBox1);
            this.panTitleBar.Controls.Add(this.lbErrLanguage);
            this.panTitleBar.Controls.Add(this.lbTime);
            this.panTitleBar.Controls.Add(this.editGroup);
            this.panTitleBar.Controls.Add(this.editErrName);
            this.panTitleBar.Controls.Add(this.lbLoginStatus);
            this.panTitleBar.Controls.Add(this.editErrCode);
            this.panTitleBar.Controls.Add(this.lbDryRunStatus);
            this.panTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panTitleBar.Name = "panTitleBar";
            this.panTitleBar.Size = new System.Drawing.Size(1277, 121);
            this.panTitleBar.TabIndex = 1266;
            // 
            // lbAppVersion
            // 
            this.lbAppVersion.BackColor = System.Drawing.Color.White;
            this.lbAppVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbAppVersion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAppVersion.ForeColor = System.Drawing.Color.Black;
            this.lbAppVersion.Location = new System.Drawing.Point(104, 28);
            this.lbAppVersion.Name = "lbAppVersion";
            this.lbAppVersion.Size = new System.Drawing.Size(50, 30);
            this.lbAppVersion.TabIndex = 1259;
            this.lbAppVersion.Text = "18.0711";
            this.lbAppVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbVersion
            // 
            this.lbVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lbVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbVersion.Font = new System.Drawing.Font("Arial", 8.25F);
            this.lbVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lbVersion.Location = new System.Drawing.Point(104, 0);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(50, 29);
            this.lbVersion.TabIndex = 1258;
            this.lbVersion.Text = "VER";
            this.lbVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editPPID
            // 
            this.editPPID.BackColor = System.Drawing.Color.Black;
            this.editPPID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editPPID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editPPID.ForeColor = System.Drawing.Color.Lime;
            this.editPPID.Location = new System.Drawing.Point(906, 0);
            this.editPPID.Name = "editPPID";
            this.editPPID.Size = new System.Drawing.Size(144, 29);
            this.editPPID.TabIndex = 1269;
            this.editPPID.Text = "TOOL NO";
            this.editPPID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pOPPanel
            // 
            this.pOPPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.pOPPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pOPPanel.Controls.Add(this.lbUDP);
            this.pOPPanel.Controls.Add(this.lbBARCODE);
            this.pOPPanel.Controls.Add(this.lbITS);
            this.pOPPanel.Controls.Add(this.swVisionSet);
            this.pOPPanel.Controls.Add(this.swSet);
            this.pOPPanel.Controls.Add(this.swMotionSet);
            this.pOPPanel.Controls.Add(this.swDEVICE);
            this.pOPPanel.Controls.Add(this.swIO);
            this.pOPPanel.Controls.Add(this.swHistory);
            this.pOPPanel.Controls.Add(this.swManual);
            this.pOPPanel.Controls.Add(this.swAuto);
            this.pOPPanel.Controls.Add(this.swLogIn);
            this.pOPPanel.Location = new System.Drawing.Point(2, 59);
            this.pOPPanel.Name = "pOPPanel";
            this.pOPPanel.Size = new System.Drawing.Size(1273, 60);
            this.pOPPanel.TabIndex = 1267;
            // 
            // lbUDP
            // 
            this.lbUDP.BackColor = System.Drawing.Color.DarkGreen;
            this.lbUDP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbUDP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUDP.ForeColor = System.Drawing.Color.Red;
            this.lbUDP.Location = new System.Drawing.Point(0, 0);
            this.lbUDP.Name = "lbUDP";
            this.lbUDP.Size = new System.Drawing.Size(58, 20);
            this.lbUDP.TabIndex = 1261;
            this.lbUDP.Text = "UDP";
            this.lbUDP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbBARCODE
            // 
            this.lbBARCODE.BackColor = System.Drawing.Color.DarkGreen;
            this.lbBARCODE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbBARCODE.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBARCODE.ForeColor = System.Drawing.Color.Red;
            this.lbBARCODE.Location = new System.Drawing.Point(0, 38);
            this.lbBARCODE.Name = "lbBARCODE";
            this.lbBARCODE.Size = new System.Drawing.Size(58, 20);
            this.lbBARCODE.TabIndex = 1260;
            this.lbBARCODE.Text = "BARCODE";
            this.lbBARCODE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbITS
            // 
            this.lbITS.BackColor = System.Drawing.Color.DarkGreen;
            this.lbITS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbITS.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbITS.ForeColor = System.Drawing.Color.Red;
            this.lbITS.Location = new System.Drawing.Point(0, 19);
            this.lbITS.Name = "lbITS";
            this.lbITS.Size = new System.Drawing.Size(58, 20);
            this.lbITS.TabIndex = 1259;
            this.lbITS.Text = "ITS DB";
            this.lbITS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // swVisionSet
            // 
            this.swVisionSet.BackColor = System.Drawing.Color.White;
            this.swVisionSet.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.swVisionSet.ForeColor = System.Drawing.Color.Black;
            this.swVisionSet.Image = ((System.Drawing.Image)(resources.GetObject("swVisionSet.Image")));
            this.swVisionSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.swVisionSet.Location = new System.Drawing.Point(1001, 3);
            this.swVisionSet.Name = "swVisionSet";
            this.swVisionSet.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.swVisionSet.Size = new System.Drawing.Size(129, 53);
            this.swVisionSet.TabIndex = 932;
            this.swVisionSet.Text = "SET\r\nVISION";
            this.swVisionSet.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swVisionSet.UseVisualStyleBackColor = false;
            this.swVisionSet.Visible = false;
            // 
            // swSet
            // 
            this.swSet.BackColor = System.Drawing.Color.White;
            this.swSet.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.swSet.ForeColor = System.Drawing.Color.Black;
            this.swSet.Image = ((System.Drawing.Image)(resources.GetObject("swSet.Image")));
            this.swSet.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swSet.Location = new System.Drawing.Point(866, 3);
            this.swSet.Margin = new System.Windows.Forms.Padding(1);
            this.swSet.Name = "swSet";
            this.swSet.Padding = new System.Windows.Forms.Padding(2);
            this.swSet.Size = new System.Drawing.Size(129, 53);
            this.swSet.TabIndex = 931;
            this.swSet.Text = "SET\r\nSYSTEM DATA";
            this.swSet.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swSet.UseVisualStyleBackColor = false;
            // 
            // swMotionSet
            // 
            this.swMotionSet.BackColor = System.Drawing.Color.White;
            this.swMotionSet.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.swMotionSet.ForeColor = System.Drawing.Color.Black;
            this.swMotionSet.Image = ((System.Drawing.Image)(resources.GetObject("swMotionSet.Image")));
            this.swMotionSet.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swMotionSet.Location = new System.Drawing.Point(1136, 3);
            this.swMotionSet.Margin = new System.Windows.Forms.Padding(1);
            this.swMotionSet.Name = "swMotionSet";
            this.swMotionSet.Padding = new System.Windows.Forms.Padding(1, 1, 2, 2);
            this.swMotionSet.Size = new System.Drawing.Size(129, 53);
            this.swMotionSet.TabIndex = 929;
            this.swMotionSet.Text = "SET\r\nMOTOR DATA";
            this.swMotionSet.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swMotionSet.UseVisualStyleBackColor = false;
            // 
            // swDEVICE
            // 
            this.swDEVICE.BackColor = System.Drawing.Color.White;
            this.swDEVICE.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.swDEVICE.ForeColor = System.Drawing.Color.Black;
            this.swDEVICE.Image = ((System.Drawing.Image)(resources.GetObject("swDEVICE.Image")));
            this.swDEVICE.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swDEVICE.Location = new System.Drawing.Point(330, 3);
            this.swDEVICE.Margin = new System.Windows.Forms.Padding(1);
            this.swDEVICE.Name = "swDEVICE";
            this.swDEVICE.Padding = new System.Windows.Forms.Padding(2);
            this.swDEVICE.Size = new System.Drawing.Size(129, 53);
            this.swDEVICE.TabIndex = 930;
            this.swDEVICE.Text = "DEVICE";
            this.swDEVICE.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swDEVICE.UseVisualStyleBackColor = false;
            // 
            // swIO
            // 
            this.swIO.BackColor = System.Drawing.Color.White;
            this.swIO.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.swIO.ForeColor = System.Drawing.Color.Black;
            this.swIO.Image = ((System.Drawing.Image)(resources.GetObject("swIO.Image")));
            this.swIO.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swIO.Location = new System.Drawing.Point(732, 3);
            this.swIO.Margin = new System.Windows.Forms.Padding(1);
            this.swIO.Name = "swIO";
            this.swIO.Padding = new System.Windows.Forms.Padding(2);
            this.swIO.Size = new System.Drawing.Size(129, 53);
            this.swIO.TabIndex = 928;
            this.swIO.Text = "I/O\r\nVIEW";
            this.swIO.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swIO.UseVisualStyleBackColor = false;
            // 
            // swHistory
            // 
            this.swHistory.BackColor = System.Drawing.Color.White;
            this.swHistory.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.swHistory.ForeColor = System.Drawing.Color.Black;
            this.swHistory.Image = ((System.Drawing.Image)(resources.GetObject("swHistory.Image")));
            this.swHistory.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swHistory.Location = new System.Drawing.Point(598, 3);
            this.swHistory.Margin = new System.Windows.Forms.Padding(1);
            this.swHistory.Name = "swHistory";
            this.swHistory.Padding = new System.Windows.Forms.Padding(2);
            this.swHistory.Size = new System.Drawing.Size(129, 53);
            this.swHistory.TabIndex = 927;
            this.swHistory.Text = "LOG\r\nVIEW";
            this.swHistory.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swHistory.UseVisualStyleBackColor = false;
            // 
            // swManual
            // 
            this.swManual.BackColor = System.Drawing.Color.White;
            this.swManual.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.swManual.ForeColor = System.Drawing.Color.Black;
            this.swManual.Image = ((System.Drawing.Image)(resources.GetObject("swManual.Image")));
            this.swManual.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swManual.Location = new System.Drawing.Point(464, 3);
            this.swManual.Margin = new System.Windows.Forms.Padding(1);
            this.swManual.Name = "swManual";
            this.swManual.Padding = new System.Windows.Forms.Padding(2);
            this.swManual.Size = new System.Drawing.Size(129, 53);
            this.swManual.TabIndex = 926;
            this.swManual.Text = "MANUAL";
            this.swManual.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swManual.UseVisualStyleBackColor = false;
            // 
            // swAuto
            // 
            this.swAuto.BackColor = System.Drawing.Color.White;
            this.swAuto.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.swAuto.ForeColor = System.Drawing.Color.Black;
            this.swAuto.Image = ((System.Drawing.Image)(resources.GetObject("swAuto.Image")));
            this.swAuto.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swAuto.Location = new System.Drawing.Point(196, 3);
            this.swAuto.Margin = new System.Windows.Forms.Padding(1);
            this.swAuto.Name = "swAuto";
            this.swAuto.Padding = new System.Windows.Forms.Padding(2);
            this.swAuto.Size = new System.Drawing.Size(129, 53);
            this.swAuto.TabIndex = 925;
            this.swAuto.Text = "AUTO";
            this.swAuto.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swAuto.UseVisualStyleBackColor = false;
            // 
            // swLogIn
            // 
            this.swLogIn.BackColor = System.Drawing.Color.White;
            this.swLogIn.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.swLogIn.ForeColor = System.Drawing.Color.Black;
            this.swLogIn.Image = ((System.Drawing.Image)(resources.GetObject("swLogIn.Image")));
            this.swLogIn.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swLogIn.Location = new System.Drawing.Point(62, 3);
            this.swLogIn.Margin = new System.Windows.Forms.Padding(1);
            this.swLogIn.Name = "swLogIn";
            this.swLogIn.Padding = new System.Windows.Forms.Padding(2);
            this.swLogIn.Size = new System.Drawing.Size(129, 53);
            this.swLogIn.TabIndex = 924;
            this.swLogIn.Text = "LOG\r\nIN";
            this.swLogIn.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swLogIn.UseVisualStyleBackColor = false;
            // 
            // editRecipe
            // 
            this.editRecipe.BackColor = System.Drawing.Color.Black;
            this.editRecipe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editRecipe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editRecipe.ForeColor = System.Drawing.Color.Lime;
            this.editRecipe.Location = new System.Drawing.Point(498, 0);
            this.editRecipe.Name = "editRecipe";
            this.editRecipe.Size = new System.Drawing.Size(409, 29);
            this.editRecipe.TabIndex = 1252;
            this.editRecipe.Text = "RECIPE";
            this.editRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // STRIP
            // 
            this.STRIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.STRIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.STRIP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.STRIP.ForeColor = System.Drawing.Color.Aqua;
            this.STRIP.Location = new System.Drawing.Point(211, 0);
            this.STRIP.Name = "STRIP";
            this.STRIP.Size = new System.Drawing.Size(63, 29);
            this.STRIP.TabIndex = 1267;
            this.STRIP.Text = "MGZ";
            this.STRIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UNLOADING
            // 
            this.UNLOADING.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.UNLOADING.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNLOADING.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UNLOADING.ForeColor = System.Drawing.Color.Aqua;
            this.UNLOADING.Location = new System.Drawing.Point(273, 28);
            this.UNLOADING.Name = "UNLOADING";
            this.UNLOADING.Size = new System.Drawing.Size(64, 30);
            this.UNLOADING.TabIndex = 1265;
            this.UNLOADING.Text = "CONVEYOR";
            this.UNLOADING.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PALLET
            // 
            this.PALLET.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.PALLET.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PALLET.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PALLET.ForeColor = System.Drawing.Color.Aqua;
            this.PALLET.Location = new System.Drawing.Point(212, 28);
            this.PALLET.Name = "PALLET";
            this.PALLET.Size = new System.Drawing.Size(63, 30);
            this.PALLET.TabIndex = 1264;
            this.PALLET.Text = "PALLET 1/2";
            this.PALLET.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HEAD
            // 
            this.HEAD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.HEAD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HEAD.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HEAD.ForeColor = System.Drawing.Color.Aqua;
            this.HEAD.Location = new System.Drawing.Point(273, 0);
            this.HEAD.Name = "HEAD";
            this.HEAD.Size = new System.Drawing.Size(64, 29);
            this.HEAD.TabIndex = 1263;
            this.HEAD.Text = "HEAD 1/2";
            this.HEAD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTN_EXIT
            // 
            this.BTN_EXIT.BackColor = System.Drawing.Color.White;
            this.BTN_EXIT.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_EXIT.ForeColor = System.Drawing.Color.Black;
            this.BTN_EXIT.Image = ((System.Drawing.Image)(resources.GetObject("BTN_EXIT.Image")));
            this.BTN_EXIT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTN_EXIT.Location = new System.Drawing.Point(1168, 2);
            this.BTN_EXIT.Name = "BTN_EXIT";
            this.BTN_EXIT.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.BTN_EXIT.Size = new System.Drawing.Size(108, 56);
            this.BTN_EXIT.TabIndex = 1249;
            this.BTN_EXIT.Text = "EXIT";
            this.BTN_EXIT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BTN_EXIT.UseVisualStyleBackColor = false;
            // 
            // pnlLOGO
            // 
            this.pnlLOGO.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlLOGO.BackColor = System.Drawing.Color.White;
            this.pnlLOGO.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlLOGO.BackgroundImage")));
            this.pnlLOGO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLOGO.Controls.Add(this.lblTENKEY);
            this.pnlLOGO.Location = new System.Drawing.Point(0, -1);
            this.pnlLOGO.Name = "pnlLOGO";
            this.pnlLOGO.Size = new System.Drawing.Size(105, 44);
            this.pnlLOGO.TabIndex = 1257;
            // 
            // lblTENKEY
            // 
            this.lblTENKEY.BackColor = System.Drawing.Color.White;
            this.lblTENKEY.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTENKEY.ForeColor = System.Drawing.Color.Black;
            this.lblTENKEY.Location = new System.Drawing.Point(-1, 33);
            this.lblTENKEY.Name = "lblTENKEY";
            this.lblTENKEY.Size = new System.Drawing.Size(29, 10);
            this.lblTENKEY.TabIndex = 0;
            this.lblTENKEY.Text = "9999";
            this.lblTENKEY.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // pnlTowerY
            // 
            this.pnlTowerY.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.pnlTowerY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTowerY.Location = new System.Drawing.Point(1150, 19);
            this.pnlTowerY.Name = "pnlTowerY";
            this.pnlTowerY.Size = new System.Drawing.Size(18, 19);
            this.pnlTowerY.TabIndex = 1247;
            // 
            // lbl_CurrentRecipe
            // 
            this.lbl_CurrentRecipe.BackColor = System.Drawing.Color.Black;
            this.lbl_CurrentRecipe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_CurrentRecipe.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CurrentRecipe.ForeColor = System.Drawing.Color.Lime;
            this.lbl_CurrentRecipe.Location = new System.Drawing.Point(336, 0);
            this.lbl_CurrentRecipe.Name = "lbl_CurrentRecipe";
            this.lbl_CurrentRecipe.Size = new System.Drawing.Size(54, 29);
            this.lbl_CurrentRecipe.TabIndex = 1251;
            this.lbl_CurrentRecipe.Text = "DEVICE";
            this.lbl_CurrentRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pblTowerR
            // 
            this.pblTowerR.BackColor = System.Drawing.Color.Maroon;
            this.pblTowerR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pblTowerR.Location = new System.Drawing.Point(1150, 2);
            this.pblTowerR.Name = "pblTowerR";
            this.pblTowerR.Size = new System.Drawing.Size(18, 19);
            this.pblTowerR.TabIndex = 1246;
            // 
            // pnlTowerG
            // 
            this.pnlTowerG.BackColor = System.Drawing.Color.DarkGreen;
            this.pnlTowerG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTowerG.Location = new System.Drawing.Point(1150, 37);
            this.pnlTowerG.Name = "pnlTowerG";
            this.pnlTowerG.Size = new System.Drawing.Size(18, 19);
            this.pnlTowerG.TabIndex = 1248;
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox1.Controls.Add(this.lbSpeed);
            this.GroupBox1.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox1.ForeColor = System.Drawing.Color.Red;
            this.GroupBox1.Location = new System.Drawing.Point(1052, 2);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(96, 56);
            this.GroupBox1.TabIndex = 1250;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "OPERATING RATE";
            // 
            // lbSpeed
            // 
            this.lbSpeed.BackColor = System.Drawing.Color.Red;
            this.lbSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbSpeed.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSpeed.ForeColor = System.Drawing.Color.White;
            this.lbSpeed.Location = new System.Drawing.Point(4, 14);
            this.lbSpeed.Name = "lbSpeed";
            this.lbSpeed.Size = new System.Drawing.Size(90, 38);
            this.lbSpeed.TabIndex = 71;
            this.lbSpeed.Text = "속도 100 %";
            this.lbSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbErrLanguage
            // 
            this.lbErrLanguage.BackColor = System.Drawing.Color.Black;
            this.lbErrLanguage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbErrLanguage.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbErrLanguage.ForeColor = System.Drawing.Color.Red;
            this.lbErrLanguage.Location = new System.Drawing.Point(336, 28);
            this.lbErrLanguage.Name = "lbErrLanguage";
            this.lbErrLanguage.Size = new System.Drawing.Size(32, 30);
            this.lbErrLanguage.TabIndex = 1256;
            this.lbErrLanguage.Text = "ERR.";
            this.lbErrLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTime
            // 
            this.lbTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbTime.BackColor = System.Drawing.Color.Lime;
            this.lbTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTime.ForeColor = System.Drawing.Color.Black;
            this.lbTime.Location = new System.Drawing.Point(0, 43);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(104, 15);
            this.lbTime.TabIndex = 1262;
            this.lbTime.Text = "00/00/00 000:00:00";
            this.lbTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editGroup
            // 
            this.editGroup.BackColor = System.Drawing.Color.Black;
            this.editGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editGroup.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editGroup.ForeColor = System.Drawing.Color.Lime;
            this.editGroup.Location = new System.Drawing.Point(389, 0);
            this.editGroup.Name = "editGroup";
            this.editGroup.Size = new System.Drawing.Size(110, 29);
            this.editGroup.TabIndex = 1253;
            this.editGroup.Text = "GROUP";
            this.editGroup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editErrName
            // 
            this.editErrName.BackColor = System.Drawing.Color.White;
            this.editErrName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editErrName.ForeColor = System.Drawing.Color.Red;
            this.editErrName.Location = new System.Drawing.Point(408, 28);
            this.editErrName.Name = "editErrName";
            this.editErrName.Size = new System.Drawing.Size(642, 29);
            this.editErrName.TabIndex = 1255;
            this.editErrName.Text = "ERROR MESSAGE";
            this.editErrName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLoginStatus
            // 
            this.lbLoginStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lbLoginStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLoginStatus.Font = new System.Drawing.Font("Arial", 8.25F);
            this.lbLoginStatus.ForeColor = System.Drawing.Color.Yellow;
            this.lbLoginStatus.Location = new System.Drawing.Point(153, 0);
            this.lbLoginStatus.Name = "lbLoginStatus";
            this.lbLoginStatus.Size = new System.Drawing.Size(59, 29);
            this.lbLoginStatus.TabIndex = 1260;
            this.lbLoginStatus.Text = "MASTER";
            this.lbLoginStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editErrCode
            // 
            this.editErrCode.BackColor = System.Drawing.Color.White;
            this.editErrCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editErrCode.ForeColor = System.Drawing.Color.Red;
            this.editErrCode.Location = new System.Drawing.Point(368, 28);
            this.editErrCode.Name = "editErrCode";
            this.editErrCode.Size = new System.Drawing.Size(39, 29);
            this.editErrCode.TabIndex = 1254;
            this.editErrCode.Text = "00000";
            this.editErrCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDryRunStatus
            // 
            this.lbDryRunStatus.BackColor = System.Drawing.Color.White;
            this.lbDryRunStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDryRunStatus.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbDryRunStatus.ForeColor = System.Drawing.Color.Black;
            this.lbDryRunStatus.Location = new System.Drawing.Point(153, 28);
            this.lbDryRunStatus.Name = "lbDryRunStatus";
            this.lbDryRunStatus.Size = new System.Drawing.Size(58, 30);
            this.lbDryRunStatus.TabIndex = 1261;
            this.lbDryRunStatus.Text = "DRY-RUN";
            this.lbDryRunStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panClientView
            // 
            this.panClientView.BackColor = System.Drawing.Color.White;
            this.panClientView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panClientView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panClientView.ForeColor = System.Drawing.Color.Black;
            this.panClientView.Location = new System.Drawing.Point(0, 123);
            this.panClientView.Name = "panClientView";
            this.panClientView.Size = new System.Drawing.Size(1277, 875);
            this.panClientView.TabIndex = 1268;
            // 
            // TmrMAIN
            // 
            this.TmrMAIN.Interval = 200;
            this.TmrMAIN.Tick += new System.EventHandler(this.TmrMAIN_Tick);
            // 
            // SPC
            // 
            this.SPC.Enabled = true;
            this.SPC.Interval = 1000;
            this.SPC.Tick += new System.EventHandler(this.SPC_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1277, 998);
            this.ControlBox = false;
            this.Controls.Add(this.panClientView);
            this.Controls.Add(this.panTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.Name = "FormMain";
            this.Text = "MAIN";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panTitleBar.ResumeLayout(false);
            this.pOPPanel.ResumeLayout(false);
            this.pnlLOGO.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panTitleBar;
        internal System.Windows.Forms.Label editRecipe;
        private System.Windows.Forms.Label STRIP;
        private System.Windows.Forms.Label UNLOADING;
        private System.Windows.Forms.Label PALLET;
        private System.Windows.Forms.Label HEAD;
        internal System.Windows.Forms.Button BTN_EXIT;
        private System.Windows.Forms.Panel pnlLOGO;
        private System.Windows.Forms.Label lblTENKEY;
        internal System.Windows.Forms.Panel pnlTowerY;
        internal System.Windows.Forms.Label lbl_CurrentRecipe;
        internal System.Windows.Forms.Panel pblTowerR;
        internal System.Windows.Forms.Panel pnlTowerG;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Label lbSpeed;
        internal System.Windows.Forms.Label lbErrLanguage;
        private System.Windows.Forms.Label lbTime;
        internal System.Windows.Forms.Label editGroup;
        internal System.Windows.Forms.Label editErrName;
        private System.Windows.Forms.Label lbLoginStatus;
        private System.Windows.Forms.Label lbAppVersion;
        internal System.Windows.Forms.Label editErrCode;
        private System.Windows.Forms.Label lbDryRunStatus;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Panel pOPPanel;
        private System.Windows.Forms.Button swVisionSet;
        private System.Windows.Forms.Button swSet;
        private System.Windows.Forms.Button swMotionSet;
        private System.Windows.Forms.Button swDEVICE;
        private System.Windows.Forms.Button swIO;
        private System.Windows.Forms.Button swHistory;
        private System.Windows.Forms.Button swManual;
        private System.Windows.Forms.Button swAuto;
        private System.Windows.Forms.Button swLogIn;
        private System.Windows.Forms.Panel panClientView;
        private System.Windows.Forms.Timer TmrMAIN;
        private System.Windows.Forms.Timer SPC;
        internal System.Windows.Forms.Label editPPID;
        private System.Windows.Forms.Label lbITS;
        private System.Windows.Forms.Label lbBARCODE;
        private System.Windows.Forms.Label lbUDP;
    }
}

