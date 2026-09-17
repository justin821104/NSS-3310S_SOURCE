namespace LIB_.SubFROMLib
{
    partial class ConfirmProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmProcess));
            this.lblMESSAGE = new System.Windows.Forms.Label();
            this.lbNum = new System.Windows.Forms.Label();
            this.swOk = new System.Windows.Forms.Button();
            this.swNo1 = new System.Windows.Forms.Button();
            this.swYes1 = new System.Windows.Forms.Button();
            this.tmrConfirmProcess = new System.Windows.Forms.Timer(this.components);
            this.CheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblMESSAGE
            // 
            this.lblMESSAGE.BackColor = System.Drawing.Color.Maroon;
            this.lblMESSAGE.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMESSAGE.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMESSAGE.ForeColor = System.Drawing.Color.Yellow;
            this.lblMESSAGE.Location = new System.Drawing.Point(0, 47);
            this.lblMESSAGE.Name = "lblMESSAGE";
            this.lblMESSAGE.Size = new System.Drawing.Size(496, 127);
            this.lblMESSAGE.TabIndex = 53;
            this.lblMESSAGE.Text = "웨이퍼 로딩동작을 재실행 하시겠습니까 ?\r\nWAFER LOADING START?";
            this.lblMESSAGE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbNum
            // 
            this.lbNum.BackColor = System.Drawing.Color.Maroon;
            this.lbNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbNum.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNum.ForeColor = System.Drawing.Color.Yellow;
            this.lbNum.Location = new System.Drawing.Point(3, 3);
            this.lbNum.Name = "lbNum";
            this.lbNum.Size = new System.Drawing.Size(79, 41);
            this.lbNum.TabIndex = 52;
            this.lbNum.Text = "000";
            this.lbNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // swOk
            // 
            this.swOk.BackColor = System.Drawing.Color.White;
            this.swOk.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.swOk.ForeColor = System.Drawing.Color.Black;
            this.swOk.Image = ((System.Drawing.Image)(resources.GetObject("swOk.Image")));
            this.swOk.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swOk.Location = new System.Drawing.Point(183, 2);
            this.swOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.swOk.Name = "swOk";
            this.swOk.Padding = new System.Windows.Forms.Padding(10, 0, 15, 0);
            this.swOk.Size = new System.Drawing.Size(206, 43);
            this.swOk.TabIndex = 51;
            this.swOk.Text = "OK";
            this.swOk.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swOk.UseVisualStyleBackColor = false;
            // 
            // swNo1
            // 
            this.swNo1.BackColor = System.Drawing.Color.White;
            this.swNo1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.swNo1.ForeColor = System.Drawing.Color.Black;
            this.swNo1.Image = ((System.Drawing.Image)(resources.GetObject("swNo1.Image")));
            this.swNo1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swNo1.Location = new System.Drawing.Point(291, 2);
            this.swNo1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.swNo1.Name = "swNo1";
            this.swNo1.Padding = new System.Windows.Forms.Padding(10, 0, 15, 0);
            this.swNo1.Size = new System.Drawing.Size(202, 43);
            this.swNo1.TabIndex = 50;
            this.swNo1.Text = "NO";
            this.swNo1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swNo1.UseVisualStyleBackColor = false;
            // 
            // swYes1
            // 
            this.swYes1.BackColor = System.Drawing.Color.White;
            this.swYes1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.swYes1.ForeColor = System.Drawing.Color.Black;
            this.swYes1.Image = ((System.Drawing.Image)(resources.GetObject("swYes1.Image")));
            this.swYes1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swYes1.Location = new System.Drawing.Point(83, 2);
            this.swYes1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.swYes1.Name = "swYes1";
            this.swYes1.Padding = new System.Windows.Forms.Padding(5);
            this.swYes1.Size = new System.Drawing.Size(202, 43);
            this.swYes1.TabIndex = 49;
            this.swYes1.Text = "YES";
            this.swYes1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swYes1.UseVisualStyleBackColor = false;
            // 
            // tmrConfirmProcess
            // 
            this.tmrConfirmProcess.Interval = 500;
            this.tmrConfirmProcess.Tick += new System.EventHandler(this.TimerConfirmProcess_Tick);
            // 
            // CheckBox
            // 
            this.CheckBox.BackColor = System.Drawing.Color.Maroon;
            this.CheckBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBox.Location = new System.Drawing.Point(1, 153);
            this.CheckBox.Name = "CheckBox";
            this.CheckBox.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.CheckBox.Size = new System.Drawing.Size(493, 21);
            this.CheckBox.TabIndex = 54;
            this.CheckBox.Text = "OPTION CHECK BOX";
            this.CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckBox.UseVisualStyleBackColor = false;
            this.CheckBox.Visible = false;
            // 
            // ConfirmProcess
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(496, 174);
            this.ControlBox = false;
            this.Controls.Add(this.CheckBox);
            this.Controls.Add(this.lblMESSAGE);
            this.Controls.Add(this.lbNum);
            this.Controls.Add(this.swOk);
            this.Controls.Add(this.swNo1);
            this.Controls.Add(this.swYes1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfirmProcess";
            this.Text = "CONFIRM PROCESS";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.ConfirmProcess_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfirmProcess_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lblMESSAGE;
        internal System.Windows.Forms.Label lbNum;
        internal System.Windows.Forms.Button swOk;
        internal System.Windows.Forms.Button swNo1;
        internal System.Windows.Forms.Button swYes1;
        public System.Windows.Forms.Timer tmrConfirmProcess;
        private System.Windows.Forms.CheckBox CheckBox;
    }
}