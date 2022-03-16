namespace LIB_.UERCTRL
{
    partial class UCL_O
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCL_O));
            this.led = new System.Windows.Forms.Label();
            this.btnOUT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // led
            // 
            this.led.BackColor = System.Drawing.Color.Red;
            this.led.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.led.Dock = System.Windows.Forms.DockStyle.Left;
            this.led.Location = new System.Drawing.Point(0, 0);
            this.led.Name = "led";
            this.led.Size = new System.Drawing.Size(13, 81);
            this.led.TabIndex = 123;
            // 
            // btnOUT
            // 
            this.btnOUT.BackColor = System.Drawing.Color.White;
            this.btnOUT.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOUT.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOUT.Font = new System.Drawing.Font("굴림체", 9F);
            this.btnOUT.Image = ((System.Drawing.Image)(resources.GetObject("btnOUT.Image")));
            this.btnOUT.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnOUT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOUT.Location = new System.Drawing.Point(12, 0);
            this.btnOUT.Name = "btnOUT";
            this.btnOUT.Padding = new System.Windows.Forms.Padding(3, 0, 1, 0);
            this.btnOUT.Size = new System.Drawing.Size(129, 81);
            this.btnOUT.TabIndex = 122;
            this.btnOUT.Tag = "117";
            this.btnOUT.Text = "TATLE";
            this.btnOUT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOUT.UseVisualStyleBackColor = false;
            this.btnOUT.Click += new System.EventHandler(this.OUT_Click);
            // 
            // UCL_O
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.led);
            this.Controls.Add(this.btnOUT);
            this.Name = "UCL_O";
            this.Size = new System.Drawing.Size(141, 81);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label led;
        private System.Windows.Forms.Button btnOUT;
    }
}
