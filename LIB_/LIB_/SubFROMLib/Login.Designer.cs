namespace LIB_.SubFROMLib
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.tmrLogin = new System.Windows.Forms.Timer(this.components);
            this.BTN_PasswordChenge = new System.Windows.Forms.Button();
            this.BTN_LOGIN = new System.Windows.Forms.Button();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.btn_Master = new System.Windows.Forms.Button();
            this.btn_Maint = new System.Windows.Forms.Button();
            this.btn_Opp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tmrLogin
            // 
            this.tmrLogin.Interval = 500;
            this.tmrLogin.Tick += new System.EventHandler(this.TimerLogin_Tick);
            // 
            // BTN_PasswordChenge
            // 
            this.BTN_PasswordChenge.BackColor = System.Drawing.Color.White;
            this.BTN_PasswordChenge.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_PasswordChenge.ForeColor = System.Drawing.Color.Black;
            this.BTN_PasswordChenge.Image = ((System.Drawing.Image)(resources.GetObject("BTN_PasswordChenge.Image")));
            this.BTN_PasswordChenge.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BTN_PasswordChenge.Location = new System.Drawing.Point(636, 603);
            this.BTN_PasswordChenge.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.BTN_PasswordChenge.Name = "BTN_PasswordChenge";
            this.BTN_PasswordChenge.Padding = new System.Windows.Forms.Padding(5);
            this.BTN_PasswordChenge.Size = new System.Drawing.Size(156, 89);
            this.BTN_PasswordChenge.TabIndex = 417;
            this.BTN_PasswordChenge.Text = "비밀번호 변경";
            this.BTN_PasswordChenge.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.BTN_PasswordChenge.UseVisualStyleBackColor = false;
            this.BTN_PasswordChenge.Click += new System.EventHandler(this.LOGIN_CLICK);
            // 
            // BTN_LOGIN
            // 
            this.BTN_LOGIN.BackColor = System.Drawing.Color.White;
            this.BTN_LOGIN.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_LOGIN.ForeColor = System.Drawing.Color.Black;
            this.BTN_LOGIN.Image = ((System.Drawing.Image)(resources.GetObject("BTN_LOGIN.Image")));
            this.BTN_LOGIN.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BTN_LOGIN.Location = new System.Drawing.Point(464, 603);
            this.BTN_LOGIN.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.BTN_LOGIN.Name = "BTN_LOGIN";
            this.BTN_LOGIN.Padding = new System.Windows.Forms.Padding(5);
            this.BTN_LOGIN.Size = new System.Drawing.Size(156, 89);
            this.BTN_LOGIN.TabIndex = 416;
            this.BTN_LOGIN.Text = "로그인";
            this.BTN_LOGIN.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.BTN_LOGIN.UseVisualStyleBackColor = false;
            this.BTN_LOGIN.Click += new System.EventHandler(this.LOGIN_CLICK);
            // 
            // txtPassWord
            // 
            this.txtPassWord.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassWord.Location = new System.Drawing.Point(456, 556);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.Size = new System.Drawing.Size(343, 36);
            this.txtPassWord.TabIndex = 415;
            this.txtPassWord.Text = "0000";
            this.txtPassWord.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPassWord.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PassWord_KeyPress);
            // 
            // btn_Master
            // 
            this.btn_Master.BackColor = System.Drawing.Color.White;
            this.btn_Master.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Master.Image = ((System.Drawing.Image)(resources.GetObject("btn_Master.Image")));
            this.btn_Master.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_Master.Location = new System.Drawing.Point(688, 409);
            this.btn_Master.Name = "btn_Master";
            this.btn_Master.Padding = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.btn_Master.Size = new System.Drawing.Size(114, 114);
            this.btn_Master.TabIndex = 414;
            this.btn_Master.Tag = "2";
            this.btn_Master.Text = "관리자 모드";
            this.btn_Master.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btn_Master.UseVisualStyleBackColor = false;
            this.btn_Master.Click += new System.EventHandler(this.LogInLevel_CLICK);
            // 
            // btn_Maint
            // 
            this.btn_Maint.BackColor = System.Drawing.Color.White;
            this.btn_Maint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Maint.Image = ((System.Drawing.Image)(resources.GetObject("btn_Maint.Image")));
            this.btn_Maint.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_Maint.Location = new System.Drawing.Point(572, 409);
            this.btn_Maint.Name = "btn_Maint";
            this.btn_Maint.Padding = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.btn_Maint.Size = new System.Drawing.Size(114, 114);
            this.btn_Maint.TabIndex = 413;
            this.btn_Maint.Tag = "1";
            this.btn_Maint.Text = "엔지니어 모드";
            this.btn_Maint.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btn_Maint.UseVisualStyleBackColor = false;
            this.btn_Maint.Click += new System.EventHandler(this.LogInLevel_CLICK);
            // 
            // btn_Opp
            // 
            this.btn_Opp.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btn_Opp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Opp.Image = ((System.Drawing.Image)(resources.GetObject("btn_Opp.Image")));
            this.btn_Opp.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_Opp.Location = new System.Drawing.Point(456, 409);
            this.btn_Opp.Name = "btn_Opp";
            this.btn_Opp.Padding = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.btn_Opp.Size = new System.Drawing.Size(114, 114);
            this.btn_Opp.TabIndex = 412;
            this.btn_Opp.Tag = "0";
            this.btn_Opp.Text = "작업자 모드";
            this.btn_Opp.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btn_Opp.UseVisualStyleBackColor = false;
            this.btn_Opp.Click += new System.EventHandler(this.LogInLevel_CLICK);
            // 
            // Login
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1261, 831);
            this.Controls.Add(this.BTN_PasswordChenge);
            this.Controls.Add(this.BTN_LOGIN);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.btn_Master);
            this.Controls.Add(this.btn_Maint);
            this.Controls.Add(this.btn_Opp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "LOGIN VIEW";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Button BTN_PasswordChenge;
        internal System.Windows.Forms.Button BTN_LOGIN;
        internal System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Button btn_Master;
        private System.Windows.Forms.Button btn_Maint;
        private System.Windows.Forms.Button btn_Opp;
        public System.Windows.Forms.Timer tmrLogin;
    }
}