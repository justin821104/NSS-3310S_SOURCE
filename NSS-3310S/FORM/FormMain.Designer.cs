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
            this.editPCBType = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.lbLoginStatus = new System.Windows.Forms.Label();
            this.lbAppVersion = new System.Windows.Forms.Label();
            this.editPPID = new System.Windows.Forms.Label();
            this.pOPPanel = new System.Windows.Forms.Panel();
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
            this.panTitleBar.Controls.Add(this.editPCBType);
            this.panTitleBar.Controls.Add(this.lbVersion);
            this.panTitleBar.Controls.Add(this.lbLoginStatus);
            this.panTitleBar.Controls.Add(this.lbAppVersion);
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
            this.panTitleBar.Controls.Add(this.editErrCode);
            this.panTitleBar.Controls.Add(this.lbDryRunStatus);
            this.panTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panTitleBar.Name = "panTitleBar";
            this.panTitleBar.Size = new System.Drawing.Size(1277, 111);
            this.panTitleBar.TabIndex = 1266;
            // 
            // editPCBType
            // 
            this.editPCBType.BackColor = System.Drawing.Color.Black;
            this.editPCBType.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editPCBType.ForeColor = System.Drawing.Color.Lime;
            this.editPCBType.Location = new System.Drawing.Point(391, 0);
            this.editPCBType.Name = "editPCBType";
            this.editPCBType.Size = new System.Drawing.Size(110, 26);
            this.editPCBType.TabIndex = 1270;
            this.editPCBType.Text = "PCB TYPE";
            this.editPCBType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbVersion
            // 
            this.lbVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lbVersion.Font = new System.Drawing.Font("Arial", 8.25F);
            this.lbVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lbVersion.Location = new System.Drawing.Point(104, 0);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(50, 26);
            this.lbVersion.TabIndex = 1258;
            this.lbVersion.Text = "VER";
            this.lbVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLoginStatus
            // 
            this.lbLoginStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lbLoginStatus.Font = new System.Drawing.Font("Arial", 8.25F);
            this.lbLoginStatus.ForeColor = System.Drawing.Color.Yellow;
            this.lbLoginStatus.Location = new System.Drawing.Point(153, 0);
            this.lbLoginStatus.Name = "lbLoginStatus";
            this.lbLoginStatus.Size = new System.Drawing.Size(59, 26);
            this.lbLoginStatus.TabIndex = 1260;
            this.lbLoginStatus.Text = "MASTER";
            this.lbLoginStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAppVersion
            // 
            this.lbAppVersion.BackColor = System.Drawing.Color.White;
            this.lbAppVersion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAppVersion.ForeColor = System.Drawing.Color.Black;
            this.lbAppVersion.Location = new System.Drawing.Point(104, 25);
            this.lbAppVersion.Name = "lbAppVersion";
            this.lbAppVersion.Size = new System.Drawing.Size(50, 28);
            this.lbAppVersion.TabIndex = 1259;
            this.lbAppVersion.Text = "18.0711";
            this.lbAppVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editPPID
            // 
            this.editPPID.BackColor = System.Drawing.Color.Black;
            this.editPPID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editPPID.ForeColor = System.Drawing.Color.Lime;
            this.editPPID.Location = new System.Drawing.Point(906, 0);
            this.editPPID.Name = "editPPID";
            this.editPPID.Size = new System.Drawing.Size(147, 26);
            this.editPPID.TabIndex = 1269;
            this.editPPID.Text = "TOOL NO";
            this.editPPID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pOPPanel
            // 
            this.pOPPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.pOPPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pOPPanel.Controls.Add(this.swVisionSet);
            this.pOPPanel.Controls.Add(this.swSet);
            this.pOPPanel.Controls.Add(this.swMotionSet);
            this.pOPPanel.Controls.Add(this.swDEVICE);
            this.pOPPanel.Controls.Add(this.swIO);
            this.pOPPanel.Controls.Add(this.swHistory);
            this.pOPPanel.Controls.Add(this.swManual);
            this.pOPPanel.Controls.Add(this.swAuto);
            this.pOPPanel.Controls.Add(this.swLogIn);
            this.pOPPanel.Location = new System.Drawing.Point(2, 54);
            this.pOPPanel.Name = "pOPPanel";
            this.pOPPanel.Size = new System.Drawing.Size(1272, 55);
            this.pOPPanel.TabIndex = 1267;
            // 
            // swVisionSet
            // 
            this.swVisionSet.BackColor = System.Drawing.Color.White;
            this.swVisionSet.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.swVisionSet.ForeColor = System.Drawing.Color.Black;
            this.swVisionSet.Image = ((System.Drawing.Image)(resources.GetObject("swVisionSet.Image")));
            this.swVisionSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.swVisionSet.Location = new System.Drawing.Point(986, 3);
            this.swVisionSet.Name = "swVisionSet";
            this.swVisionSet.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.swVisionSet.Size = new System.Drawing.Size(136, 48);
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
            this.swSet.Location = new System.Drawing.Point(846, 3);
            this.swSet.Margin = new System.Windows.Forms.Padding(1);
            this.swSet.Name = "swSet";
            this.swSet.Padding = new System.Windows.Forms.Padding(2);
            this.swSet.Size = new System.Drawing.Size(136, 48);
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
            this.swMotionSet.Location = new System.Drawing.Point(1126, 3);
            this.swMotionSet.Margin = new System.Windows.Forms.Padding(1);
            this.swMotionSet.Name = "swMotionSet";
            this.swMotionSet.Padding = new System.Windows.Forms.Padding(1, 1, 2, 2);
            this.swMotionSet.Size = new System.Drawing.Size(136, 48);
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
            this.swDEVICE.Location = new System.Drawing.Point(286, 3);
            this.swDEVICE.Margin = new System.Windows.Forms.Padding(1);
            this.swDEVICE.Name = "swDEVICE";
            this.swDEVICE.Padding = new System.Windows.Forms.Padding(2);
            this.swDEVICE.Size = new System.Drawing.Size(136, 48);
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
            this.swIO.Location = new System.Drawing.Point(706, 3);
            this.swIO.Margin = new System.Windows.Forms.Padding(1);
            this.swIO.Name = "swIO";
            this.swIO.Padding = new System.Windows.Forms.Padding(2);
            this.swIO.Size = new System.Drawing.Size(136, 48);
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
            this.swHistory.Location = new System.Drawing.Point(566, 3);
            this.swHistory.Margin = new System.Windows.Forms.Padding(1);
            this.swHistory.Name = "swHistory";
            this.swHistory.Padding = new System.Windows.Forms.Padding(2);
            this.swHistory.Size = new System.Drawing.Size(136, 48);
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
            this.swManual.Location = new System.Drawing.Point(426, 3);
            this.swManual.Margin = new System.Windows.Forms.Padding(1);
            this.swManual.Name = "swManual";
            this.swManual.Padding = new System.Windows.Forms.Padding(2);
            this.swManual.Size = new System.Drawing.Size(136, 48);
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
            this.swAuto.Location = new System.Drawing.Point(146, 3);
            this.swAuto.Margin = new System.Windows.Forms.Padding(1);
            this.swAuto.Name = "swAuto";
            this.swAuto.Padding = new System.Windows.Forms.Padding(2);
            this.swAuto.Size = new System.Drawing.Size(136, 48);
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
            this.swLogIn.Location = new System.Drawing.Point(6, 3);
            this.swLogIn.Margin = new System.Windows.Forms.Padding(1);
            this.swLogIn.Name = "swLogIn";
            this.swLogIn.Padding = new System.Windows.Forms.Padding(2);
            this.swLogIn.Size = new System.Drawing.Size(136, 48);
            this.swLogIn.TabIndex = 924;
            this.swLogIn.Text = "LOG\r\nIN";
            this.swLogIn.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swLogIn.UseVisualStyleBackColor = false;
            // 
            // editRecipe
            // 
            this.editRecipe.BackColor = System.Drawing.Color.Black;
            this.editRecipe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editRecipe.ForeColor = System.Drawing.Color.Lime;
            this.editRecipe.Location = new System.Drawing.Point(621, 0);
            this.editRecipe.Name = "editRecipe";
            this.editRecipe.Size = new System.Drawing.Size(286, 26);
            this.editRecipe.TabIndex = 1252;
            this.editRecipe.Text = "RECIPE";
            this.editRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // STRIP
            // 
            this.STRIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.STRIP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.STRIP.ForeColor = System.Drawing.Color.Aqua;
            this.STRIP.Location = new System.Drawing.Point(211, 0);
            this.STRIP.Name = "STRIP";
            this.STRIP.Size = new System.Drawing.Size(63, 26);
            this.STRIP.TabIndex = 1267;
            this.STRIP.Text = "MGZ";
            this.STRIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UNLOADING
            // 
            this.UNLOADING.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.UNLOADING.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UNLOADING.ForeColor = System.Drawing.Color.Aqua;
            this.UNLOADING.Location = new System.Drawing.Point(273, 25);
            this.UNLOADING.Name = "UNLOADING";
            this.UNLOADING.Size = new System.Drawing.Size(65, 28);
            this.UNLOADING.TabIndex = 1265;
            this.UNLOADING.Text = "CONVEYOR";
            this.UNLOADING.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PALLET
            // 
            this.PALLET.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.PALLET.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PALLET.ForeColor = System.Drawing.Color.Aqua;
            this.PALLET.Location = new System.Drawing.Point(212, 25);
            this.PALLET.Name = "PALLET";
            this.PALLET.Size = new System.Drawing.Size(63, 28);
            this.PALLET.TabIndex = 1264;
            this.PALLET.Text = "PALLET 1/2";
            this.PALLET.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HEAD
            // 
            this.HEAD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.HEAD.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HEAD.ForeColor = System.Drawing.Color.Aqua;
            this.HEAD.Location = new System.Drawing.Point(273, 0);
            this.HEAD.Name = "HEAD";
            this.HEAD.Size = new System.Drawing.Size(65, 26);
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
            this.BTN_EXIT.Size = new System.Drawing.Size(108, 52);
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
            this.pnlLOGO.Size = new System.Drawing.Size(105, 41);
            this.pnlLOGO.TabIndex = 1257;
            // 
            // lblTENKEY
            // 
            this.lblTENKEY.BackColor = System.Drawing.Color.White;
            this.lblTENKEY.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTENKEY.ForeColor = System.Drawing.Color.Black;
            this.lblTENKEY.Location = new System.Drawing.Point(-1, 30);
            this.lblTENKEY.Name = "lblTENKEY";
            this.lblTENKEY.Size = new System.Drawing.Size(29, 9);
            this.lblTENKEY.TabIndex = 0;
            this.lblTENKEY.Text = "9999";
            this.lblTENKEY.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // pnlTowerY
            // 
            this.pnlTowerY.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.pnlTowerY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTowerY.Location = new System.Drawing.Point(1150, 18);
            this.pnlTowerY.Name = "pnlTowerY";
            this.pnlTowerY.Size = new System.Drawing.Size(18, 17);
            this.pnlTowerY.TabIndex = 1247;
            // 
            // lbl_CurrentRecipe
            // 
            this.lbl_CurrentRecipe.BackColor = System.Drawing.Color.Black;
            this.lbl_CurrentRecipe.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CurrentRecipe.ForeColor = System.Drawing.Color.Lime;
            this.lbl_CurrentRecipe.Location = new System.Drawing.Point(337, 0);
            this.lbl_CurrentRecipe.Name = "lbl_CurrentRecipe";
            this.lbl_CurrentRecipe.Size = new System.Drawing.Size(54, 26);
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
            this.pblTowerR.Size = new System.Drawing.Size(18, 17);
            this.pblTowerR.TabIndex = 1246;
            // 
            // pnlTowerG
            // 
            this.pnlTowerG.BackColor = System.Drawing.Color.DarkGreen;
            this.pnlTowerG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTowerG.Location = new System.Drawing.Point(1150, 34);
            this.pnlTowerG.Name = "pnlTowerG";
            this.pnlTowerG.Size = new System.Drawing.Size(18, 17);
            this.pnlTowerG.TabIndex = 1248;
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox1.Controls.Add(this.lbSpeed);
            this.GroupBox1.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox1.ForeColor = System.Drawing.Color.Red;
            this.GroupBox1.Location = new System.Drawing.Point(1058, 2);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(91, 52);
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
            this.lbSpeed.Location = new System.Drawing.Point(3, 13);
            this.lbSpeed.Name = "lbSpeed";
            this.lbSpeed.Size = new System.Drawing.Size(85, 35);
            this.lbSpeed.TabIndex = 71;
            this.lbSpeed.Text = "속도 100 %";
            this.lbSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbErrLanguage
            // 
            this.lbErrLanguage.BackColor = System.Drawing.Color.Black;
            this.lbErrLanguage.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbErrLanguage.ForeColor = System.Drawing.Color.Red;
            this.lbErrLanguage.Location = new System.Drawing.Point(337, 25);
            this.lbErrLanguage.Name = "lbErrLanguage";
            this.lbErrLanguage.Size = new System.Drawing.Size(31, 28);
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
            this.lbTime.Location = new System.Drawing.Point(0, 40);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(104, 14);
            this.lbTime.TabIndex = 1262;
            this.lbTime.Text = "00/00/00 000:00:00";
            this.lbTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editGroup
            // 
            this.editGroup.BackColor = System.Drawing.Color.Black;
            this.editGroup.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editGroup.ForeColor = System.Drawing.Color.Lime;
            this.editGroup.Location = new System.Drawing.Point(500, 0);
            this.editGroup.Name = "editGroup";
            this.editGroup.Size = new System.Drawing.Size(120, 26);
            this.editGroup.TabIndex = 1253;
            this.editGroup.Text = "GROUP";
            this.editGroup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editErrName
            // 
            this.editErrName.BackColor = System.Drawing.Color.White;
            this.editErrName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editErrName.ForeColor = System.Drawing.Color.Red;
            this.editErrName.Location = new System.Drawing.Point(409, 25);
            this.editErrName.Name = "editErrName";
            this.editErrName.Size = new System.Drawing.Size(645, 28);
            this.editErrName.TabIndex = 1255;
            this.editErrName.Text = "ERROR MESSAGE";
            this.editErrName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editErrCode
            // 
            this.editErrCode.BackColor = System.Drawing.Color.White;
            this.editErrCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editErrCode.ForeColor = System.Drawing.Color.Red;
            this.editErrCode.Location = new System.Drawing.Point(369, 25);
            this.editErrCode.Name = "editErrCode";
            this.editErrCode.Size = new System.Drawing.Size(38, 26);
            this.editErrCode.TabIndex = 1254;
            this.editErrCode.Text = "00000";
            this.editErrCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDryRunStatus
            // 
            this.lbDryRunStatus.BackColor = System.Drawing.Color.White;
            this.lbDryRunStatus.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbDryRunStatus.ForeColor = System.Drawing.Color.Black;
            this.lbDryRunStatus.Location = new System.Drawing.Point(153, 25);
            this.lbDryRunStatus.Name = "lbDryRunStatus";
            this.lbDryRunStatus.Size = new System.Drawing.Size(58, 28);
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
            this.panClientView.Location = new System.Drawing.Point(0, 110);
            this.panClientView.Name = "panClientView";
            this.panClientView.Size = new System.Drawing.Size(1277, 888);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 11F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1277, 998);
            this.ControlBox = false;
            this.Controls.Add(this.panClientView);
            this.Controls.Add(this.panTitleBar);
            this.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
        internal System.Windows.Forms.Label editPCBType;
    }
}

