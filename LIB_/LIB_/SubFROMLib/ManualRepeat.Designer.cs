namespace LIB_.SubFROMLib
{
    partial class ManualRepeat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManualRepeat));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRepeatStop = new System.Windows.Forms.Button();
            this.btnRepeatStart = new System.Windows.Forms.Button();
            this.lbManualName = new System.Windows.Forms.Label();
            this.tmrManualRepeat = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(252, 84);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(5);
            this.btnClose.Size = new System.Drawing.Size(127, 44);
            this.btnClose.TabIndex = 48;
            this.btnClose.Text = "CLOSE";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.Close_Click);
            // 
            // btnRepeatStop
            // 
            this.btnRepeatStop.BackColor = System.Drawing.Color.White;
            this.btnRepeatStop.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepeatStop.Image = ((System.Drawing.Image)(resources.GetObject("btnRepeatStop.Image")));
            this.btnRepeatStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepeatStop.Location = new System.Drawing.Point(126, 84);
            this.btnRepeatStop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRepeatStop.Name = "btnRepeatStop";
            this.btnRepeatStop.Padding = new System.Windows.Forms.Padding(5);
            this.btnRepeatStop.Size = new System.Drawing.Size(127, 44);
            this.btnRepeatStop.TabIndex = 47;
            this.btnRepeatStop.Text = "STOP";
            this.btnRepeatStop.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnRepeatStop.UseVisualStyleBackColor = false;
            this.btnRepeatStop.Click += new System.EventHandler(this.RepeatStop_Click);
            // 
            // btnRepeatStart
            // 
            this.btnRepeatStart.BackColor = System.Drawing.Color.White;
            this.btnRepeatStart.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepeatStart.Image = ((System.Drawing.Image)(resources.GetObject("btnRepeatStart.Image")));
            this.btnRepeatStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepeatStart.Location = new System.Drawing.Point(0, 84);
            this.btnRepeatStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRepeatStart.Name = "btnRepeatStart";
            this.btnRepeatStart.Padding = new System.Windows.Forms.Padding(5);
            this.btnRepeatStart.Size = new System.Drawing.Size(127, 44);
            this.btnRepeatStart.TabIndex = 46;
            this.btnRepeatStart.Text = "START";
            this.btnRepeatStart.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnRepeatStart.UseVisualStyleBackColor = false;
            this.btnRepeatStart.Click += new System.EventHandler(this.RepeatStart_Click);
            // 
            // lbManualName
            // 
            this.lbManualName.BackColor = System.Drawing.Color.Black;
            this.lbManualName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbManualName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbManualName.ForeColor = System.Drawing.Color.Orange;
            this.lbManualName.Location = new System.Drawing.Point(0, 0);
            this.lbManualName.Name = "lbManualName";
            this.lbManualName.Size = new System.Drawing.Size(379, 84);
            this.lbManualName.TabIndex = 45;
            this.lbManualName.Text = "SOL\' NAME OUTPUT 명칭";
            this.lbManualName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrManualRepeat
            // 
            this.tmrManualRepeat.Interval = 500;
            this.tmrManualRepeat.Tick += new System.EventHandler(this.ManualRepeat_Tick);
            // 
            // ManualRepeat
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(379, 128);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRepeatStop);
            this.Controls.Add(this.btnRepeatStart);
            this.Controls.Add(this.lbManualName);
            this.Name = "ManualRepeat";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "REPEAT RUN";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btnRepeatStop;
        internal System.Windows.Forms.Button btnRepeatStart;
        public System.Windows.Forms.Label lbManualName;
        public System.Windows.Forms.Timer tmrManualRepeat;
    }
}