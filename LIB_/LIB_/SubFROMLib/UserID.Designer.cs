namespace LIB_.SubFROMLib
{
    partial class UserID
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserID));
            this.OK = new System.Windows.Forms.Button();
            this.CLOSE = new System.Windows.Forms.Button();
            this.USER_ID = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.USER_PASSWORD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.BackColor = System.Drawing.Color.White;
            this.OK.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OK.ForeColor = System.Drawing.Color.Black;
            this.OK.Image = ((System.Drawing.Image)(resources.GetObject("OK.Image")));
            this.OK.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.OK.Location = new System.Drawing.Point(5, 114);
            this.OK.Margin = new System.Windows.Forms.Padding(1);
            this.OK.Name = "OK";
            this.OK.Padding = new System.Windows.Forms.Padding(5, 5, 2, 2);
            this.OK.Size = new System.Drawing.Size(112, 49);
            this.OK.TabIndex = 1363;
            this.OK.Text = "OK";
            this.OK.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.OK.UseVisualStyleBackColor = false;
            // 
            // CLOSE
            // 
            this.CLOSE.BackColor = System.Drawing.Color.White;
            this.CLOSE.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CLOSE.ForeColor = System.Drawing.Color.Black;
            this.CLOSE.Image = ((System.Drawing.Image)(resources.GetObject("CLOSE.Image")));
            this.CLOSE.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.CLOSE.Location = new System.Drawing.Point(118, 114);
            this.CLOSE.Margin = new System.Windows.Forms.Padding(1);
            this.CLOSE.Name = "CLOSE";
            this.CLOSE.Padding = new System.Windows.Forms.Padding(5, 5, 2, 2);
            this.CLOSE.Size = new System.Drawing.Size(112, 49);
            this.CLOSE.TabIndex = 1362;
            this.CLOSE.Text = "CANCEL";
            this.CLOSE.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.CLOSE.UseVisualStyleBackColor = false;
            // 
            // USER_ID
            // 
            this.USER_ID.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.USER_ID.FormattingEnabled = true;
            this.USER_ID.Items.AddRange(new object[] {
            "MAGAZINE-HALF",
            "MAGAZINE-QUARTER",
            "BOAT-HALF",
            "BOAT-QUARTER"});
            this.USER_ID.Location = new System.Drawing.Point(83, 43);
            this.USER_ID.Name = "USER_ID";
            this.USER_ID.Size = new System.Drawing.Size(147, 31);
            this.USER_ID.TabIndex = 1361;
            this.USER_ID.Tag = "MD";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(5, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 36);
            this.label1.TabIndex = 1360;
            this.label1.Text = "USER ID 입력";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // USER_PASSWORD
            // 
            this.USER_PASSWORD.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.USER_PASSWORD.Location = new System.Drawing.Point(83, 77);
            this.USER_PASSWORD.Name = "USER_PASSWORD";
            this.USER_PASSWORD.PasswordChar = '*';
            this.USER_PASSWORD.Size = new System.Drawing.Size(147, 33);
            this.USER_PASSWORD.TabIndex = 1359;
            this.USER_PASSWORD.Text = "0000";
            this.USER_PASSWORD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Font = new System.Drawing.Font("굴림체", 9F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(5, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 33);
            this.label2.TabIndex = 1358;
            this.label2.Text = "PASSWORD";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(5, 43);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 33);
            this.label4.TabIndex = 1357;
            this.label4.Text = "USER ID";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserID
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(234, 166);
            this.ControlBox = false;
            this.Controls.Add(this.OK);
            this.Controls.Add(this.CLOSE);
            this.Controls.Add(this.USER_ID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.USER_PASSWORD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UserID";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "USER ID";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button CLOSE;
        private System.Windows.Forms.ComboBox USER_ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox USER_PASSWORD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}