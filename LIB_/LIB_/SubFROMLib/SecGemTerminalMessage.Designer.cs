namespace LIB_.SubFROMLib
{
    partial class SecGemTerminalMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecGemTerminalMessage));
            this.lbTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lbTitle1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.Red;
            this.lbTitle.Location = new System.Drawing.Point(0, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(1084, 41);
            this.lbTitle.TabIndex = 141;
            this.lbTitle.Text = "MES MESSAGE!!! 메세지 뷰어 !!!";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbTitle.DoubleClick += new System.EventHandler(this.lbTitle_DoubleClick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnClose.ForeColor = System.Drawing.Color.Red;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnClose.Location = new System.Drawing.Point(1085, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(5);
            this.btnClose.Size = new System.Drawing.Size(97, 85);
            this.btnClose.TabIndex = 844;
            this.btnClose.Text = "CLOSE";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbTitle1
            // 
            this.lbTitle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbTitle1.Font = new System.Drawing.Font("맑은 고딕", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbTitle1.ForeColor = System.Drawing.Color.Gold;
            this.lbTitle1.Location = new System.Drawing.Point(0, 41);
            this.lbTitle1.Name = "lbTitle1";
            this.lbTitle1.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.lbTitle1.Size = new System.Drawing.Size(1084, 47);
            this.lbTitle1.TabIndex = 845;
            this.lbTitle1.Text = "MES MESSAGE!!! 메세지 뷰어 !!!";
            this.lbTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbTitle1.DoubleClick += new System.EventHandler(this.lbTitle1_DoubleClick);
            // 
            // SecGemTerminalMessage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1184, 88);
            this.ControlBox = false;
            this.Controls.Add(this.lbTitle1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbTitle);
            this.Location = new System.Drawing.Point(0, 880);
            this.Name = "SecGemTerminalMessage";
            this.Text = "SecGemTerminalMessage";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SecGemTerminalMessage_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Label lbTitle1;
    }
}