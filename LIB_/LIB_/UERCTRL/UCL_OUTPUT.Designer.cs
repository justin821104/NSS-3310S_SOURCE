namespace LIB_.UERCTRL
{
    partial class UCL_OUTPUT
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCL_OUTPUT));
            this.btnOUT = new System.Windows.Forms.Button();
            this.inLED = new System.Windows.Forms.Label();
            this.outLED = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOUT
            // 
            this.btnOUT.BackColor = System.Drawing.Color.White;
            this.btnOUT.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOUT.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOUT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnOUT.Image = ((System.Drawing.Image)(resources.GetObject("btnOUT.Image")));
            this.btnOUT.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnOUT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOUT.Location = new System.Drawing.Point(18, 1);
            this.btnOUT.Margin = new System.Windows.Forms.Padding(0);
            this.btnOUT.Name = "btnOUT";
            this.btnOUT.Size = new System.Drawing.Size(125, 65);
            this.btnOUT.TabIndex = 132;
            this.btnOUT.Tag = "131";
            this.btnOUT.Text = "OUTPUT\r\nNAME";
            this.btnOUT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOUT.UseVisualStyleBackColor = false;
            this.btnOUT.Click += new System.EventHandler(this.OUT_Click);
            // 
            // inLED
            // 
            this.inLED.BackColor = System.Drawing.Color.Lime;
            this.inLED.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.inLED.Location = new System.Drawing.Point(1, 1);
            this.inLED.Name = "inLED";
            this.inLED.Size = new System.Drawing.Size(16, 32);
            this.inLED.TabIndex = 133;
            this.inLED.Tag = "0";
            // 
            // outLED
            // 
            this.outLED.BackColor = System.Drawing.Color.Red;
            this.outLED.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.outLED.Location = new System.Drawing.Point(1, 34);
            this.outLED.Name = "outLED";
            this.outLED.Size = new System.Drawing.Size(16, 32);
            this.outLED.TabIndex = 134;
            this.outLED.Tag = "0";
            // 
            // ucOUTPUT
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.outLED);
            this.Controls.Add(this.inLED);
            this.Controls.Add(this.btnOUT);
            this.Name = "ucOUTPUT";
            this.Size = new System.Drawing.Size(144, 67);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOUT;
        private System.Windows.Forms.Label inLED;
        private System.Windows.Forms.Label outLED;
    }
}
