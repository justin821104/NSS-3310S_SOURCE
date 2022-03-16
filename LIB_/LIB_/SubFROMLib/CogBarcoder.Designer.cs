namespace LIB_.SubFROMLib
{
    partial class CogBarcoder
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
            this.cbLiveDisplay = new System.Windows.Forms.CheckBox();
            this.chkQR = new System.Windows.Forms.CheckBox();
            this.chkDataMatrix = new System.Windows.Forms.CheckBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblRETUNE = new System.Windows.Forms.Label();
            this.btnTrigger = new System.Windows.Forms.Button();
            this.picResultImage = new System.Windows.Forms.PictureBox();
            this.tmrBarcode = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picResultImage)).BeginInit();
            this.SuspendLayout();
            // 
            // cbLiveDisplay
            // 
            this.cbLiveDisplay.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLiveDisplay.ForeColor = System.Drawing.Color.Black;
            this.cbLiveDisplay.Location = new System.Drawing.Point(0, 177);
            this.cbLiveDisplay.Name = "cbLiveDisplay";
            this.cbLiveDisplay.Size = new System.Drawing.Size(86, 18);
            this.cbLiveDisplay.TabIndex = 1267;
            this.cbLiveDisplay.Text = "Live Display";
            this.cbLiveDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkQR
            // 
            this.chkQR.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkQR.ForeColor = System.Drawing.Color.Black;
            this.chkQR.Location = new System.Drawing.Point(175, 177);
            this.chkQR.Name = "chkQR";
            this.chkQR.Size = new System.Drawing.Size(69, 18);
            this.chkQR.TabIndex = 1266;
            this.chkQR.Text = "QR";
            this.chkQR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkQR.Visible = false;
            this.chkQR.CheckedChanged += new System.EventHandler(this.chkQR_CheckedChanged);
            // 
            // chkDataMatrix
            // 
            this.chkDataMatrix.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDataMatrix.ForeColor = System.Drawing.Color.Black;
            this.chkDataMatrix.Location = new System.Drawing.Point(87, 177);
            this.chkDataMatrix.Name = "chkDataMatrix";
            this.chkDataMatrix.Size = new System.Drawing.Size(86, 18);
            this.chkDataMatrix.TabIndex = 1265;
            this.chkDataMatrix.Text = "Data Matrix";
            this.chkDataMatrix.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDataMatrix.Visible = false;
            this.chkDataMatrix.CheckedChanged += new System.EventHandler(this.chkDataMatrix_CheckedChanged);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.BackColor = System.Drawing.Color.White;
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.ForeColor = System.Drawing.Color.Black;
            this.btnDisconnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisconnect.Location = new System.Drawing.Point(82, 0);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(81, 21);
            this.btnDisconnect.TabIndex = 1264;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = false;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.White;
            this.btnConnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.Black;
            this.btnConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConnect.Location = new System.Drawing.Point(0, 0);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(81, 21);
            this.btnConnect.TabIndex = 1263;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            // 
            // lblRETUNE
            // 
            this.lblRETUNE.BackColor = System.Drawing.Color.White;
            this.lblRETUNE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRETUNE.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRETUNE.Location = new System.Drawing.Point(1, 196);
            this.lblRETUNE.Name = "lblRETUNE";
            this.lblRETUNE.Size = new System.Drawing.Size(243, 22);
            this.lblRETUNE.TabIndex = 1262;
            this.lblRETUNE.Text = "00-0000-00";
            this.lblRETUNE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRETUNE.DoubleClick += new System.EventHandler(this.lblRETUNE_DoubleClick);
            // 
            // btnTrigger
            // 
            this.btnTrigger.BackColor = System.Drawing.Color.White;
            this.btnTrigger.Enabled = false;
            this.btnTrigger.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrigger.ForeColor = System.Drawing.Color.Black;
            this.btnTrigger.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrigger.Location = new System.Drawing.Point(164, 0);
            this.btnTrigger.Name = "btnTrigger";
            this.btnTrigger.Size = new System.Drawing.Size(81, 21);
            this.btnTrigger.TabIndex = 1261;
            this.btnTrigger.Text = "TRIGGER";
            this.btnTrigger.UseVisualStyleBackColor = false;
            // 
            // picResultImage
            // 
            this.picResultImage.BackColor = System.Drawing.Color.Black;
            this.picResultImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picResultImage.Location = new System.Drawing.Point(0, 22);
            this.picResultImage.Name = "picResultImage";
            this.picResultImage.Size = new System.Drawing.Size(244, 154);
            this.picResultImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picResultImage.TabIndex = 1260;
            this.picResultImage.TabStop = false;
            // 
            // tmrBarcode
            // 
            this.tmrBarcode.Enabled = true;
            this.tmrBarcode.Interval = 70;
            this.tmrBarcode.Tick += new System.EventHandler(this.tmrBarcode_Tick);
            // 
            // CogBarcoder
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(245, 220);
            this.ControlBox = false;
            this.Controls.Add(this.cbLiveDisplay);
            this.Controls.Add(this.chkQR);
            this.Controls.Add(this.chkDataMatrix);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblRETUNE);
            this.Controls.Add(this.btnTrigger);
            this.Controls.Add(this.picResultImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CogBarcoder";
            this.Text = "Cognex Barcoder";
            this.Load += new System.EventHandler(this.CogBarcoder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picResultImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox chkQR;
        private System.Windows.Forms.CheckBox chkDataMatrix;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblRETUNE;
        private System.Windows.Forms.Button btnTrigger;
        private System.Windows.Forms.PictureBox picResultImage;
        public System.Windows.Forms.CheckBox cbLiveDisplay;
        private System.Windows.Forms.Timer tmrBarcode;
    }
}