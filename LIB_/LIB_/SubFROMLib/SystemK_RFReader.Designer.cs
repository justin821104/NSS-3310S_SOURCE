namespace LIB_.SubFROMLib
{
    partial class SystemK_RFReader
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
            this.LBL_IP = new System.Windows.Forms.Label();
            this.IP_ADD = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PORT_NO = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.BtnRead = new System.Windows.Forms.Button();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // LBL_IP
            // 
            this.LBL_IP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.LBL_IP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_IP.Location = new System.Drawing.Point(1, 1);
            this.LBL_IP.Name = "LBL_IP";
            this.LBL_IP.Size = new System.Drawing.Size(76, 23);
            this.LBL_IP.TabIndex = 0;
            this.LBL_IP.Text = "IP ADDRESS";
            this.LBL_IP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IP_ADD
            // 
            this.IP_ADD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.IP_ADD.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IP_ADD.Location = new System.Drawing.Point(79, 1);
            this.IP_ADD.Name = "IP_ADD";
            this.IP_ADD.Size = new System.Drawing.Size(105, 23);
            this.IP_ADD.TabIndex = 1;
            this.IP_ADD.Text = "000.000.000.000";
            this.IP_ADD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(187, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "PORT";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PORT_NO
            // 
            this.PORT_NO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.PORT_NO.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PORT_NO.Location = new System.Drawing.Point(234, 1);
            this.PORT_NO.Name = "PORT_NO";
            this.PORT_NO.Size = new System.Drawing.Size(46, 23);
            this.PORT_NO.TabIndex = 3;
            this.PORT_NO.Text = "0000";
            this.PORT_NO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.BackColor = System.Drawing.Color.White;
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.ForeColor = System.Drawing.Color.Black;
            this.btnDisconnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisconnect.Location = new System.Drawing.Point(93, 25);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(94, 29);
            this.btnDisconnect.TabIndex = 1266;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = false;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.White;
            this.btnConnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.Black;
            this.btnConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConnect.Location = new System.Drawing.Point(0, 25);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(94, 29);
            this.btnConnect.TabIndex = 1265;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            // 
            // BtnRead
            // 
            this.BtnRead.BackColor = System.Drawing.Color.White;
            this.BtnRead.Enabled = false;
            this.BtnRead.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRead.ForeColor = System.Drawing.Color.Black;
            this.BtnRead.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnRead.Location = new System.Drawing.Point(186, 25);
            this.BtnRead.Name = "BtnRead";
            this.BtnRead.Size = new System.Drawing.Size(94, 29);
            this.BtnRead.TabIndex = 1267;
            this.BtnRead.Text = "READ";
            this.BtnRead.UseVisualStyleBackColor = false;
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(0, 54);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(281, 82);
            this.tbResult.TabIndex = 1268;
            this.tbResult.DoubleClick += new System.EventHandler(this.tbResult_DoubleClick);
            // 
            // SystemK_RFReader
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(281, 137);
            this.ControlBox = false;
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.BtnRead);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.PORT_NO);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IP_ADD);
            this.Controls.Add(this.LBL_IP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SystemK_RFReader";
            this.Text = "RF READER";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBL_IP;
        private System.Windows.Forms.Label IP_ADD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label PORT_NO;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button BtnRead;
        private System.Windows.Forms.TextBox tbResult;
    }
}