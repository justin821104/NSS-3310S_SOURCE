namespace LIB_.UERCTRL
{
    partial class UCL_MOVE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCL_MOVE));
            this.btnMOVE = new System.Windows.Forms.Button();
            this.led = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnMOVE
            // 
            this.btnMOVE.BackColor = System.Drawing.Color.White;
            this.btnMOVE.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMOVE.Image = ((System.Drawing.Image)(resources.GetObject("btnMOVE.Image")));
            this.btnMOVE.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnMOVE.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMOVE.Location = new System.Drawing.Point(1, 0);
            this.btnMOVE.Name = "btnMOVE";
            this.btnMOVE.Padding = new System.Windows.Forms.Padding(3, 0, 1, 0);
            this.btnMOVE.Size = new System.Drawing.Size(170, 70);
            this.btnMOVE.TabIndex = 108;
            this.btnMOVE.Tag = "103";
            this.btnMOVE.Text = "TATLE";
            this.btnMOVE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMOVE.UseVisualStyleBackColor = false;
            // 
            // led
            // 
            this.led.BackColor = System.Drawing.Color.Lime;
            this.led.Location = new System.Drawing.Point(7, 55);
            this.led.Name = "led";
            this.led.Size = new System.Drawing.Size(38, 9);
            this.led.TabIndex = 109;
            // 
            // ucMOVE
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.led);
            this.Controls.Add(this.btnMOVE);
            this.Name = "ucMOVE";
            this.Size = new System.Drawing.Size(173, 71);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMOVE;
        private System.Windows.Forms.Label led;
    }
}
