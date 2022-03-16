namespace LIB_.UERCTRL
{
    partial class UCL_JOG
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCL_JOG));
            this.JogCw = new System.Windows.Forms.Button();
            this.JogCcw = new System.Windows.Forms.Button();
            this.imgArrow = new System.Windows.Forms.ImageList(this.components);
            this.pINRAIL = new System.Windows.Forms.Panel();
            this.CurPOS = new Owf.Controls.DigitalDisplayControl();
            this.lblMotor = new System.Windows.Forms.Label();
            this.pINRAIL.SuspendLayout();
            this.SuspendLayout();
            // 
            // JogCw
            // 
            this.JogCw.BackColor = System.Drawing.Color.White;
            this.JogCw.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.JogCw.ForeColor = System.Drawing.Color.Black;
            this.JogCw.Location = new System.Drawing.Point(70, 44);
            this.JogCw.Name = "JogCw";
            this.JogCw.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.JogCw.Size = new System.Drawing.Size(70, 62);
            this.JogCw.TabIndex = 1203;
            this.JogCw.Tag = "3";
            this.JogCw.Text = "CW +";
            this.JogCw.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.JogCw.UseVisualStyleBackColor = false;
            this.JogCw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JogCw_MouseDown);
            this.JogCw.MouseUp += new System.Windows.Forms.MouseEventHandler(this.JogMove_MouseUp);
            // 
            // JogCcw
            // 
            this.JogCcw.BackColor = System.Drawing.Color.White;
            this.JogCcw.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.JogCcw.ForeColor = System.Drawing.Color.Black;
            this.JogCcw.ImageList = this.imgArrow;
            this.JogCcw.Location = new System.Drawing.Point(0, 44);
            this.JogCcw.Name = "JogCcw";
            this.JogCcw.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.JogCcw.Size = new System.Drawing.Size(70, 62);
            this.JogCcw.TabIndex = 1204;
            this.JogCcw.Tag = "3";
            this.JogCcw.Text = "CCW -";
            this.JogCcw.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.JogCcw.UseVisualStyleBackColor = false;
            this.JogCcw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JogCcw_MouseDown);
            this.JogCcw.MouseUp += new System.Windows.Forms.MouseEventHandler(this.JogMove_MouseUp);
            // 
            // imgArrow
            // 
            this.imgArrow.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgArrow.ImageStream")));
            this.imgArrow.TransparentColor = System.Drawing.Color.Transparent;
            this.imgArrow.Images.SetKeyName(0, "Right.ico");
            this.imgArrow.Images.SetKeyName(1, "Down.ico");
            this.imgArrow.Images.SetKeyName(2, "Left.ico");
            this.imgArrow.Images.SetKeyName(3, "Up.ico");
            this.imgArrow.Images.SetKeyName(4, "RightUp.ico");
            this.imgArrow.Images.SetKeyName(5, "RightDown.ico");
            this.imgArrow.Images.SetKeyName(6, "LeftDown.ico");
            this.imgArrow.Images.SetKeyName(7, "LeftUp.ico");
            this.imgArrow.Images.SetKeyName(8, "RotRight.ico");
            this.imgArrow.Images.SetKeyName(9, "RotLeft.ico");
            // 
            // pINRAIL
            // 
            this.pINRAIL.BackColor = System.Drawing.Color.Black;
            this.pINRAIL.Controls.Add(this.CurPOS);
            this.pINRAIL.Location = new System.Drawing.Point(1, 20);
            this.pINRAIL.Name = "pINRAIL";
            this.pINRAIL.Size = new System.Drawing.Size(138, 23);
            this.pINRAIL.TabIndex = 1202;
            // 
            // CurPOS
            // 
            this.CurPOS.BackColor = System.Drawing.Color.Black;
            this.CurPOS.DigitColor = System.Drawing.Color.White;
            this.CurPOS.DigitText = "0000.0000";
            this.CurPOS.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurPOS.Location = new System.Drawing.Point(5, 3);
            this.CurPOS.Name = "CurPOS";
            this.CurPOS.Size = new System.Drawing.Size(128, 16);
            this.CurPOS.TabIndex = 1187;
            // 
            // lblMotor
            // 
            this.lblMotor.BackColor = System.Drawing.Color.Lime;
            this.lblMotor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMotor.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMotor.ForeColor = System.Drawing.Color.Black;
            this.lblMotor.Location = new System.Drawing.Point(1, 1);
            this.lblMotor.Name = "lblMotor";
            this.lblMotor.Size = new System.Drawing.Size(138, 18);
            this.lblMotor.TabIndex = 1201;
            this.lblMotor.Text = "MOTOR (모터)";
            this.lblMotor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucJOG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.JogCw);
            this.Controls.Add(this.JogCcw);
            this.Controls.Add(this.pINRAIL);
            this.Controls.Add(this.lblMotor);
            this.Name = "ucJOG";
            this.Size = new System.Drawing.Size(140, 106);
            this.pINRAIL.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button JogCw;
        private System.Windows.Forms.Button JogCcw;
        private System.Windows.Forms.Panel pINRAIL;
        private Owf.Controls.DigitalDisplayControl CurPOS;
        private System.Windows.Forms.Label lblMotor;
        private System.Windows.Forms.ImageList imgArrow;
    }
}
