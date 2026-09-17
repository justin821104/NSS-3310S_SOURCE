namespace LIB_.UERCTRL
{
    partial class UCL_JogControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCL_JogControl));
            this.lblMotorName = new System.Windows.Forms.Label();
            this.pINRAIL = new System.Windows.Forms.Panel();
            this.JogCw = new System.Windows.Forms.Button();
            this.imgArrow = new System.Windows.Forms.ImageList(this.components);
            this.JogCcw = new System.Windows.Forms.Button();
            this.groupBoxIncMove = new System.Windows.Forms.GroupBox();
            this.chkIncMove = new System.Windows.Forms.CheckBox();
            this.buttonInc1000um = new System.Windows.Forms.Button();
            this.buttonInc100um = new System.Windows.Forms.Button();
            this.buttonInc10um = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxIncMove = new System.Windows.Forms.TextBox();
            this.panelSpeedHigh = new System.Windows.Forms.Panel();
            this.buttonSpeedHigh = new System.Windows.Forms.Button();
            this.panelSpeedMiddle = new System.Windows.Forms.Panel();
            this.buttonSpeedMiddle = new System.Windows.Forms.Button();
            this.panelSpeedLow = new System.Windows.Forms.Panel();
            this.buttonSpeedLow = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ALARM = new System.Windows.Forms.Label();
            this.LIMIT_M = new System.Windows.Forms.Label();
            this.LIMIT_P = new System.Windows.Forms.Label();
            this.INPOS = new System.Windows.Forms.Label();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.lbCurPOS = new System.Windows.Forms.Label();
            this.pINRAIL.SuspendLayout();
            this.groupBoxIncMove.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMotorName
            // 
            this.lblMotorName.BackColor = System.Drawing.Color.Lime;
            this.lblMotorName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMotorName.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMotorName.ForeColor = System.Drawing.Color.Black;
            this.lblMotorName.Location = new System.Drawing.Point(1, 1);
            this.lblMotorName.Name = "lblMotorName";
            this.lblMotorName.Size = new System.Drawing.Size(138, 22);
            this.lblMotorName.TabIndex = 1197;
            this.lblMotorName.Text = "MOTOR (모터)";
            this.lblMotorName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pINRAIL
            // 
            this.pINRAIL.BackColor = System.Drawing.Color.Black;
            this.pINRAIL.Controls.Add(this.lbCurPOS);
            this.pINRAIL.Location = new System.Drawing.Point(1, 24);
            this.pINRAIL.Name = "pINRAIL";
            this.pINRAIL.Size = new System.Drawing.Size(138, 27);
            this.pINRAIL.TabIndex = 1198;
            // 
            // JogCw
            // 
            this.JogCw.BackColor = System.Drawing.Color.White;
            this.JogCw.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.JogCw.ForeColor = System.Drawing.Color.Black;
            this.JogCw.ImageList = this.imgArrow;
            this.JogCw.Location = new System.Drawing.Point(70, 51);
            this.JogCw.Name = "JogCw";
            this.JogCw.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.JogCw.Size = new System.Drawing.Size(70, 62);
            this.JogCw.TabIndex = 1199;
            this.JogCw.Tag = "3";
            this.JogCw.Text = "CW +";
            this.JogCw.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.JogCw.UseVisualStyleBackColor = false;
            this.JogCw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JogCw_MouseDown);
            this.JogCw.MouseUp += new System.Windows.Forms.MouseEventHandler(this.JogMove_MouesUp);
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
            // JogCcw
            // 
            this.JogCcw.BackColor = System.Drawing.Color.White;
            this.JogCcw.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.JogCcw.ForeColor = System.Drawing.Color.Black;
            this.JogCcw.ImageList = this.imgArrow;
            this.JogCcw.Location = new System.Drawing.Point(0, 51);
            this.JogCcw.Name = "JogCcw";
            this.JogCcw.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.JogCcw.Size = new System.Drawing.Size(70, 62);
            this.JogCcw.TabIndex = 1200;
            this.JogCcw.Tag = "3";
            this.JogCcw.Text = "CCW -";
            this.JogCcw.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.JogCcw.UseVisualStyleBackColor = false;
            this.JogCcw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JogCcw_MouseDown);
            this.JogCcw.MouseUp += new System.Windows.Forms.MouseEventHandler(this.JogMove_MouesUp);
            // 
            // groupBoxIncMove
            // 
            this.groupBoxIncMove.Controls.Add(this.chkIncMove);
            this.groupBoxIncMove.Controls.Add(this.buttonInc1000um);
            this.groupBoxIncMove.Controls.Add(this.buttonInc100um);
            this.groupBoxIncMove.Controls.Add(this.buttonInc10um);
            this.groupBoxIncMove.Controls.Add(this.label2);
            this.groupBoxIncMove.Controls.Add(this.textBoxIncMove);
            this.groupBoxIncMove.Enabled = false;
            this.groupBoxIncMove.ForeColor = System.Drawing.Color.White;
            this.groupBoxIncMove.Location = new System.Drawing.Point(142, 1);
            this.groupBoxIncMove.Name = "groupBoxIncMove";
            this.groupBoxIncMove.Size = new System.Drawing.Size(126, 71);
            this.groupBoxIncMove.TabIndex = 1201;
            this.groupBoxIncMove.TabStop = false;
            // 
            // chkIncMove
            // 
            this.chkIncMove.AutoSize = true;
            this.chkIncMove.BackColor = System.Drawing.Color.Black;
            this.chkIncMove.ForeColor = System.Drawing.Color.White;
            this.chkIncMove.Location = new System.Drawing.Point(0, 0);
            this.chkIncMove.Name = "chkIncMove";
            this.chkIncMove.Size = new System.Drawing.Size(85, 16);
            this.chkIncMove.TabIndex = 1202;
            this.chkIncMove.Text = "INC MOVE";
            this.chkIncMove.UseVisualStyleBackColor = false;
            // 
            // buttonInc1000um
            // 
            this.buttonInc1000um.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInc1000um.ForeColor = System.Drawing.Color.Black;
            this.buttonInc1000um.Location = new System.Drawing.Point(83, 38);
            this.buttonInc1000um.Name = "buttonInc1000um";
            this.buttonInc1000um.Size = new System.Drawing.Size(40, 28);
            this.buttonInc1000um.TabIndex = 5;
            this.buttonInc1000um.Tag = "2";
            this.buttonInc1000um.Text = "1";
            this.buttonInc1000um.UseVisualStyleBackColor = true;
            this.buttonInc1000um.Click += new System.EventHandler(this.IncPitch_Click);
            // 
            // buttonInc100um
            // 
            this.buttonInc100um.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInc100um.ForeColor = System.Drawing.Color.Black;
            this.buttonInc100um.Location = new System.Drawing.Point(43, 38);
            this.buttonInc100um.Name = "buttonInc100um";
            this.buttonInc100um.Size = new System.Drawing.Size(40, 28);
            this.buttonInc100um.TabIndex = 4;
            this.buttonInc100um.Tag = "1";
            this.buttonInc100um.Text = "0.1";
            this.buttonInc100um.UseVisualStyleBackColor = true;
            this.buttonInc100um.Click += new System.EventHandler(this.IncPitch_Click);
            // 
            // buttonInc10um
            // 
            this.buttonInc10um.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInc10um.ForeColor = System.Drawing.Color.Black;
            this.buttonInc10um.Location = new System.Drawing.Point(3, 38);
            this.buttonInc10um.Name = "buttonInc10um";
            this.buttonInc10um.Size = new System.Drawing.Size(40, 28);
            this.buttonInc10um.TabIndex = 3;
            this.buttonInc10um.Tag = "0";
            this.buttonInc10um.Text = "0.01";
            this.buttonInc10um.UseVisualStyleBackColor = true;
            this.buttonInc10um.Click += new System.EventHandler(this.IncPitch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "mm";
            // 
            // textBoxIncMove
            // 
            this.textBoxIncMove.Location = new System.Drawing.Point(6, 16);
            this.textBoxIncMove.Name = "textBoxIncMove";
            this.textBoxIncMove.Size = new System.Drawing.Size(79, 21);
            this.textBoxIncMove.TabIndex = 1;
            this.textBoxIncMove.Text = "0.1";
            this.textBoxIncMove.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panelSpeedHigh
            // 
            this.panelSpeedHigh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSpeedHigh.Location = new System.Drawing.Point(286, 82);
            this.panelSpeedHigh.Name = "panelSpeedHigh";
            this.panelSpeedHigh.Size = new System.Drawing.Size(9, 17);
            this.panelSpeedHigh.TabIndex = 1207;
            this.panelSpeedHigh.Tag = "I18";
            // 
            // buttonSpeedHigh
            // 
            this.buttonSpeedHigh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonSpeedHigh.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonSpeedHigh.ForeColor = System.Drawing.Color.White;
            this.buttonSpeedHigh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonSpeedHigh.Location = new System.Drawing.Point(280, 75);
            this.buttonSpeedHigh.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSpeedHigh.Name = "buttonSpeedHigh";
            this.buttonSpeedHigh.Size = new System.Drawing.Size(69, 37);
            this.buttonSpeedHigh.TabIndex = 1206;
            this.buttonSpeedHigh.Tag = "O18";
            this.buttonSpeedHigh.Text = "   HIGH";
            this.buttonSpeedHigh.UseVisualStyleBackColor = false;
            this.buttonSpeedHigh.Click += new System.EventHandler(this.SelectSpd_Click);
            // 
            // panelSpeedMiddle
            // 
            this.panelSpeedMiddle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSpeedMiddle.Location = new System.Drawing.Point(217, 82);
            this.panelSpeedMiddle.Name = "panelSpeedMiddle";
            this.panelSpeedMiddle.Size = new System.Drawing.Size(9, 17);
            this.panelSpeedMiddle.TabIndex = 1205;
            this.panelSpeedMiddle.Tag = "I18";
            // 
            // buttonSpeedMiddle
            // 
            this.buttonSpeedMiddle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonSpeedMiddle.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonSpeedMiddle.ForeColor = System.Drawing.Color.White;
            this.buttonSpeedMiddle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonSpeedMiddle.Location = new System.Drawing.Point(211, 75);
            this.buttonSpeedMiddle.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSpeedMiddle.Name = "buttonSpeedMiddle";
            this.buttonSpeedMiddle.Size = new System.Drawing.Size(69, 37);
            this.buttonSpeedMiddle.TabIndex = 1204;
            this.buttonSpeedMiddle.Tag = "O18";
            this.buttonSpeedMiddle.Text = "   MIDDLE";
            this.buttonSpeedMiddle.UseVisualStyleBackColor = false;
            this.buttonSpeedMiddle.Click += new System.EventHandler(this.SelectSpd_Click);
            // 
            // panelSpeedLow
            // 
            this.panelSpeedLow.BackColor = System.Drawing.Color.Lime;
            this.panelSpeedLow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSpeedLow.Location = new System.Drawing.Point(148, 82);
            this.panelSpeedLow.Name = "panelSpeedLow";
            this.panelSpeedLow.Size = new System.Drawing.Size(9, 17);
            this.panelSpeedLow.TabIndex = 1203;
            this.panelSpeedLow.Tag = "I18";
            // 
            // buttonSpeedLow
            // 
            this.buttonSpeedLow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonSpeedLow.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonSpeedLow.ForeColor = System.Drawing.Color.White;
            this.buttonSpeedLow.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonSpeedLow.Location = new System.Drawing.Point(142, 75);
            this.buttonSpeedLow.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSpeedLow.Name = "buttonSpeedLow";
            this.buttonSpeedLow.Size = new System.Drawing.Size(69, 37);
            this.buttonSpeedLow.TabIndex = 1202;
            this.buttonSpeedLow.Tag = "O18";
            this.buttonSpeedLow.Text = "   LOW";
            this.buttonSpeedLow.UseVisualStyleBackColor = false;
            this.buttonSpeedLow.Click += new System.EventHandler(this.SelectSpd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(276, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 12);
            this.label1.TabIndex = 1208;
            this.label1.Text = "label1";
            // 
            // ALARM
            // 
            this.ALARM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ALARM.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ALARM.ForeColor = System.Drawing.Color.White;
            this.ALARM.Location = new System.Drawing.Point(271, 38);
            this.ALARM.Margin = new System.Windows.Forms.Padding(0);
            this.ALARM.Name = "ALARM";
            this.ALARM.Size = new System.Drawing.Size(77, 17);
            this.ALARM.TabIndex = 1212;
            this.ALARM.Text = "ALARM";
            this.ALARM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LIMIT_M
            // 
            this.LIMIT_M.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LIMIT_M.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LIMIT_M.ForeColor = System.Drawing.Color.White;
            this.LIMIT_M.Location = new System.Drawing.Point(271, 2);
            this.LIMIT_M.Margin = new System.Windows.Forms.Padding(0);
            this.LIMIT_M.Name = "LIMIT_M";
            this.LIMIT_M.Size = new System.Drawing.Size(77, 17);
            this.LIMIT_M.TabIndex = 1211;
            this.LIMIT_M.Text = "LIMIT-";
            this.LIMIT_M.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LIMIT_P
            // 
            this.LIMIT_P.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LIMIT_P.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LIMIT_P.ForeColor = System.Drawing.Color.White;
            this.LIMIT_P.Location = new System.Drawing.Point(271, 20);
            this.LIMIT_P.Margin = new System.Windows.Forms.Padding(0);
            this.LIMIT_P.Name = "LIMIT_P";
            this.LIMIT_P.Size = new System.Drawing.Size(77, 17);
            this.LIMIT_P.TabIndex = 1210;
            this.LIMIT_P.Text = "LIMIT+";
            this.LIMIT_P.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // INPOS
            // 
            this.INPOS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(0)))));
            this.INPOS.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.INPOS.ForeColor = System.Drawing.Color.White;
            this.INPOS.Location = new System.Drawing.Point(271, 56);
            this.INPOS.Margin = new System.Windows.Forms.Padding(0);
            this.INPOS.Name = "INPOS";
            this.INPOS.Size = new System.Drawing.Size(77, 17);
            this.INPOS.TabIndex = 1209;
            this.INPOS.Text = "INPOS";
            this.INPOS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmr
            // 
            this.tmr.Enabled = true;
            this.tmr.Interval = 150;
            this.tmr.Tick += new System.EventHandler(this.TMR_Tick);
            // 
            // lbCurPOS
            // 
            this.lbCurPOS.BackColor = System.Drawing.Color.Black;
            this.lbCurPOS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurPOS.ForeColor = System.Drawing.Color.White;
            this.lbCurPOS.Location = new System.Drawing.Point(6, 3);
            this.lbCurPOS.Name = "lbCurPOS";
            this.lbCurPOS.Size = new System.Drawing.Size(129, 20);
            this.lbCurPOS.TabIndex = 1213;
            this.lbCurPOS.Text = "0000.000";
            this.lbCurPOS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UCL_JogControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.Controls.Add(this.ALARM);
            this.Controls.Add(this.LIMIT_M);
            this.Controls.Add(this.LIMIT_P);
            this.Controls.Add(this.INPOS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelSpeedHigh);
            this.Controls.Add(this.buttonSpeedHigh);
            this.Controls.Add(this.panelSpeedMiddle);
            this.Controls.Add(this.buttonSpeedMiddle);
            this.Controls.Add(this.panelSpeedLow);
            this.Controls.Add(this.buttonSpeedLow);
            this.Controls.Add(this.groupBoxIncMove);
            this.Controls.Add(this.JogCw);
            this.Controls.Add(this.JogCcw);
            this.Controls.Add(this.pINRAIL);
            this.Controls.Add(this.lblMotorName);
            this.Name = "UCL_JogControl";
            this.Size = new System.Drawing.Size(349, 113);
            this.pINRAIL.ResumeLayout(false);
            this.groupBoxIncMove.ResumeLayout(false);
            this.groupBoxIncMove.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMotorName;
        private System.Windows.Forms.Panel pINRAIL;
        private System.Windows.Forms.Button JogCw;
        private System.Windows.Forms.Button JogCcw;
        private System.Windows.Forms.ImageList imgArrow;
        private System.Windows.Forms.GroupBox groupBoxIncMove;
        private System.Windows.Forms.CheckBox chkIncMove;
        private System.Windows.Forms.Button buttonInc1000um;
        private System.Windows.Forms.Button buttonInc100um;
        private System.Windows.Forms.Button buttonInc10um;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxIncMove;
        private System.Windows.Forms.Panel panelSpeedHigh;
        private System.Windows.Forms.Button buttonSpeedHigh;
        private System.Windows.Forms.Panel panelSpeedMiddle;
        private System.Windows.Forms.Button buttonSpeedMiddle;
        private System.Windows.Forms.Panel panelSpeedLow;
        private System.Windows.Forms.Button buttonSpeedLow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ALARM;
        private System.Windows.Forms.Label LIMIT_M;
        private System.Windows.Forms.Label LIMIT_P;
        private System.Windows.Forms.Label INPOS;
        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.Label lbCurPOS;
    }
}
