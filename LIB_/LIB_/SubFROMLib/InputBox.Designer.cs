namespace LIB_.SubFROMLib
{
    partial class InputBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputBox));
            this.lbTitle = new System.Windows.Forms.Label();
            this.editInput = new System.Windows.Forms.TextBox();
            this.swClose = new System.Windows.Forms.Button();
            this.swYes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitle.Font = new System.Drawing.Font("맑은 고딕", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbTitle.ForeColor = System.Drawing.Color.Gold;
            this.lbTitle.Location = new System.Drawing.Point(0, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(359, 54);
            this.lbTitle.TabIndex = 140;
            this.lbTitle.Text = "VCR read failure !!! 메세지 박스";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editInput
            // 
            this.editInput.BackColor = System.Drawing.Color.White;
            this.editInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editInput.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editInput.ForeColor = System.Drawing.Color.Black;
            this.editInput.Location = new System.Drawing.Point(0, 54);
            this.editInput.Name = "editInput";
            this.editInput.Size = new System.Drawing.Size(359, 23);
            this.editInput.TabIndex = 139;
            this.editInput.Text = "1235ABCDE";
            this.editInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.editInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EditInput_KeyPress);
            // 
            // swClose
            // 
            this.swClose.BackColor = System.Drawing.Color.White;
            this.swClose.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.swClose.ForeColor = System.Drawing.Color.Black;
            this.swClose.Image = ((System.Drawing.Image)(resources.GetObject("swClose.Image")));
            this.swClose.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swClose.Location = new System.Drawing.Point(179, 76);
            this.swClose.Name = "swClose";
            this.swClose.Size = new System.Drawing.Size(180, 38);
            this.swClose.TabIndex = 138;
            this.swClose.Text = "CLOSE";
            this.swClose.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swClose.UseVisualStyleBackColor = false;
            this.swClose.Click += new System.EventHandler(this.InputBox_Click);
            // 
            // swYes
            // 
            this.swYes.BackColor = System.Drawing.Color.White;
            this.swYes.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.swYes.Image = ((System.Drawing.Image)(resources.GetObject("swYes.Image")));
            this.swYes.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swYes.Location = new System.Drawing.Point(0, 76);
            this.swYes.Name = "swYes";
            this.swYes.Size = new System.Drawing.Size(180, 38);
            this.swYes.TabIndex = 137;
            this.swYes.Text = "O.K";
            this.swYes.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swYes.UseVisualStyleBackColor = false;
            this.swYes.Click += new System.EventHandler(this.InputBox_Click);
            // 
            // InputBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(359, 114);
            this.ControlBox = false;
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.editInput);
            this.Controls.Add(this.swClose);
            this.Controls.Add(this.swYes);
            this.Name = "InputBox";
            this.Text = "INPUT BOX";
            this.Shown += new System.EventHandler(this.InputBox_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lbTitle;
        internal System.Windows.Forms.TextBox editInput;
        internal System.Windows.Forms.Button swClose;
        internal System.Windows.Forms.Button swYes;
    }
}