namespace LIB_.UERCTRL
{
    partial class UCL_OUTPUT_INPUT2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCL_OUTPUT_INPUT2));
            this.outLED = new System.Windows.Forms.Label();
            this.inLED2 = new System.Windows.Forms.Label();
            this.btnOUT = new System.Windows.Forms.Button();
            this.inLED1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // outLED
            // 
            this.outLED.BackColor = System.Drawing.Color.Red;
            this.outLED.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.outLED.Location = new System.Drawing.Point(18, 34);
            this.outLED.Name = "outLED";
            this.outLED.Size = new System.Drawing.Size(16, 32);
            this.outLED.TabIndex = 137;
            this.outLED.Tag = "0";
            // 
            // inLED2
            // 
            this.inLED2.BackColor = System.Drawing.Color.Lime;
            this.inLED2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.inLED2.Location = new System.Drawing.Point(18, 1);
            this.inLED2.Name = "inLED2";
            this.inLED2.Size = new System.Drawing.Size(16, 32);
            this.inLED2.TabIndex = 136;
            this.inLED2.Tag = "0";
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
            this.btnOUT.Location = new System.Drawing.Point(34, 1);
            this.btnOUT.Margin = new System.Windows.Forms.Padding(0);
            this.btnOUT.Name = "btnOUT";
            this.btnOUT.Size = new System.Drawing.Size(125, 65);
            this.btnOUT.TabIndex = 135;
            this.btnOUT.Tag = "131";
            this.btnOUT.Text = "OUTPUT\r\nNAME";
            this.btnOUT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOUT.UseVisualStyleBackColor = false;
            this.btnOUT.Click += new System.EventHandler(this.OUT_Click);
            // 
            // inLED1
            // 
            this.inLED1.BackColor = System.Drawing.Color.Lime;
            this.inLED1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.inLED1.Location = new System.Drawing.Point(1, 1);
            this.inLED1.Name = "inLED1";
            this.inLED1.Size = new System.Drawing.Size(16, 32);
            this.inLED1.TabIndex = 138;
            this.inLED1.Tag = "0";
            // 
            // ucOUTPUT_INPUT2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.inLED1);
            this.Controls.Add(this.outLED);
            this.Controls.Add(this.inLED2);
            this.Controls.Add(this.btnOUT);
            this.Name = "ucOUTPUT_INPUT2";
            this.Size = new System.Drawing.Size(160, 67);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label outLED;
        private System.Windows.Forms.Label inLED2;
        private System.Windows.Forms.Button btnOUT;
        private System.Windows.Forms.Label inLED1;
    }
}
