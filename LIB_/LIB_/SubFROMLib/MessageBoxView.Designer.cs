namespace LIB_.SubFROMLib
{
    partial class MessageBoxView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.swNo = new System.Windows.Forms.Button();
            this.swOk = new System.Windows.Forms.Button();
            this.swYes = new System.Windows.Forms.Button();
            this.editMsg = new System.Windows.Forms.Label();
            this.tmrMessageBox = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.swNo);
            this.panel1.Controls.Add(this.swOk);
            this.panel1.Controls.Add(this.swYes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 105);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(501, 43);
            this.panel1.TabIndex = 196;
            // 
            // swNo
            // 
            this.swNo.BackColor = System.Drawing.Color.White;
            this.swNo.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.swNo.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.swNo.ForeColor = System.Drawing.Color.Black;
            this.swNo.Location = new System.Drawing.Point(333, 2);
            this.swNo.Name = "swNo";
            this.swNo.Size = new System.Drawing.Size(163, 37);
            this.swNo.TabIndex = 192;
            this.swNo.Text = "NO";
            this.swNo.UseVisualStyleBackColor = false;
            this.swNo.Click += new System.EventHandler(this.Msg_Click);
            // 
            // swOk
            // 
            this.swOk.BackColor = System.Drawing.Color.White;
            this.swOk.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.swOk.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.swOk.ForeColor = System.Drawing.Color.Black;
            this.swOk.Location = new System.Drawing.Point(168, 2);
            this.swOk.Name = "swOk";
            this.swOk.Size = new System.Drawing.Size(163, 37);
            this.swOk.TabIndex = 191;
            this.swOk.Text = "OK";
            this.swOk.UseVisualStyleBackColor = false;
            this.swOk.Click += new System.EventHandler(this.Msg_Click);
            // 
            // swYes
            // 
            this.swYes.BackColor = System.Drawing.Color.White;
            this.swYes.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.swYes.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.swYes.ForeColor = System.Drawing.Color.Black;
            this.swYes.Location = new System.Drawing.Point(3, 2);
            this.swYes.Name = "swYes";
            this.swYes.Size = new System.Drawing.Size(163, 37);
            this.swYes.TabIndex = 190;
            this.swYes.Text = "YES";
            this.swYes.UseVisualStyleBackColor = false;
            this.swYes.Click += new System.EventHandler(this.Msg_Click);
            // 
            // editMsg
            // 
            this.editMsg.BackColor = System.Drawing.Color.Black;
            this.editMsg.Dock = System.Windows.Forms.DockStyle.Top;
            this.editMsg.Font = new System.Drawing.Font("맑은 고딕", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.editMsg.ForeColor = System.Drawing.Color.Gold;
            this.editMsg.Location = new System.Drawing.Point(0, 0);
            this.editMsg.Name = "editMsg";
            this.editMsg.Size = new System.Drawing.Size(501, 104);
            this.editMsg.TabIndex = 195;
            this.editMsg.Text = "VCR read failure !!! 메세지 박스";
            this.editMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrMessageBox
            // 
            this.tmrMessageBox.Interval = 500;
            this.tmrMessageBox.Tick += new System.EventHandler(this.TimerMassageBox_Tick);
            // 
            // MessageBoxView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(501, 148);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.editMsg);
            this.Name = "MessageBoxView";
            this.Text = "MESSGE VIEW";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MassageBox_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button swNo;
        internal System.Windows.Forms.Button swOk;
        internal System.Windows.Forms.Button swYes;
        internal System.Windows.Forms.Label editMsg;
        public System.Windows.Forms.Timer tmrMessageBox;
    }
}