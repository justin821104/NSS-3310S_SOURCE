namespace NSS_3310S
{
    partial class FormRecipeCreate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRecipeCreate));
            this.CHECK_DELAY_TIME = new System.Windows.Forms.Timer(this.components);
            this.pnlLinkFrom = new System.Windows.Forms.Panel();
            this.DEVICE = new System.Windows.Forms.Label();
            this.DEVICE_INDEX = new System.Windows.Forms.ComboBox();
            this.GROUP = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.lbPattern = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SET_SAW_RECIPE = new System.Windows.Forms.Button();
            this.SAW_DEVICE = new System.Windows.Forms.ComboBox();
            this.SAW_GROUP = new System.Windows.Forms.ComboBox();
            this.SAW_RECIPE = new System.Windows.Forms.TextBox();
            this.HANDLER_PPID = new System.Windows.Forms.TextBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.OK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbxLoading = new System.Windows.Forms.PictureBox();
            this.pnlLinkFrom.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // CHECK_DELAY_TIME
            // 
            this.CHECK_DELAY_TIME.Interval = 300;
            this.CHECK_DELAY_TIME.Tick += new System.EventHandler(this.CHECK_DELAY_TIME_Tick);
            // 
            // pnlLinkFrom
            // 
            this.pnlLinkFrom.BackColor = System.Drawing.Color.Lavender;
            this.pnlLinkFrom.Controls.Add(this.DEVICE);
            this.pnlLinkFrom.Controls.Add(this.DEVICE_INDEX);
            this.pnlLinkFrom.Controls.Add(this.GROUP);
            this.pnlLinkFrom.Controls.Add(this.label27);
            this.pnlLinkFrom.Controls.Add(this.label28);
            this.pnlLinkFrom.Controls.Add(this.lbPattern);
            this.pnlLinkFrom.Location = new System.Drawing.Point(2, 2);
            this.pnlLinkFrom.Name = "pnlLinkFrom";
            this.pnlLinkFrom.Size = new System.Drawing.Size(440, 150);
            this.pnlLinkFrom.TabIndex = 1;
            // 
            // DEVICE
            // 
            this.DEVICE.BackColor = System.Drawing.Color.White;
            this.DEVICE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DEVICE.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DEVICE.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DEVICE.Location = new System.Drawing.Point(59, 109);
            this.DEVICE.Name = "DEVICE";
            this.DEVICE.Size = new System.Drawing.Size(376, 36);
            this.DEVICE.TabIndex = 1326;
            this.DEVICE.Tag = "MD";
            this.DEVICE.Text = "-";
            this.DEVICE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DEVICE_INDEX
            // 
            this.DEVICE_INDEX.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DEVICE_INDEX.FormattingEnabled = true;
            this.DEVICE_INDEX.Location = new System.Drawing.Point(59, 84);
            this.DEVICE_INDEX.Name = "DEVICE_INDEX";
            this.DEVICE_INDEX.Size = new System.Drawing.Size(376, 24);
            this.DEVICE_INDEX.TabIndex = 1;
            this.DEVICE_INDEX.SelectedIndexChanged += new System.EventHandler(this.DEVICE_INDEX_SelectedIndexChanged);
            // 
            // GROUP
            // 
            this.GROUP.BackColor = System.Drawing.Color.White;
            this.GROUP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GROUP.Enabled = false;
            this.GROUP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GROUP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.GROUP.Location = new System.Drawing.Point(59, 46);
            this.GROUP.Name = "GROUP";
            this.GROUP.Size = new System.Drawing.Size(376, 36);
            this.GROUP.TabIndex = 1325;
            this.GROUP.Tag = "MD";
            this.GROUP.Text = "-";
            this.GROUP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.Black;
            this.label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label27.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label27.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label27.Location = new System.Drawing.Point(4, 85);
            this.label27.Margin = new System.Windows.Forms.Padding(0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(54, 60);
            this.label27.TabIndex = 1321;
            this.label27.Text = "DEVICE";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.Black;
            this.label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label28.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label28.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label28.Location = new System.Drawing.Point(4, 46);
            this.label28.Margin = new System.Windows.Forms.Padding(0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(54, 36);
            this.label28.TabIndex = 1320;
            this.label28.Text = "GROUP";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPattern
            // 
            this.lbPattern.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPattern.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPattern.Image = ((System.Drawing.Image)(resources.GetObject("lbPattern.Image")));
            this.lbPattern.Location = new System.Drawing.Point(0, 0);
            this.lbPattern.Name = "lbPattern";
            this.lbPattern.Size = new System.Drawing.Size(438, 42);
            this.lbPattern.TabIndex = 1317;
            this.lbPattern.Text = "LINK FROM";
            this.lbPattern.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Lavender;
            this.panel1.Controls.Add(this.SET_SAW_RECIPE);
            this.panel1.Controls.Add(this.SAW_DEVICE);
            this.panel1.Controls.Add(this.SAW_GROUP);
            this.panel1.Controls.Add(this.SAW_RECIPE);
            this.panel1.Controls.Add(this.HANDLER_PPID);
            this.panel1.Controls.Add(this.Cancel);
            this.panel1.Controls.Add(this.OK);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(2, 153);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(440, 201);
            this.panel1.TabIndex = 2;
            // 
            // SET_SAW_RECIPE
            // 
            this.SET_SAW_RECIPE.Location = new System.Drawing.Point(390, 84);
            this.SET_SAW_RECIPE.Name = "SET_SAW_RECIPE";
            this.SET_SAW_RECIPE.Size = new System.Drawing.Size(45, 26);
            this.SET_SAW_RECIPE.TabIndex = 1337;
            this.SET_SAW_RECIPE.Text = "적용";
            this.SET_SAW_RECIPE.UseVisualStyleBackColor = true;
            this.SET_SAW_RECIPE.Click += new System.EventHandler(this.SET_SAW_RECIPE_Click);
            // 
            // SAW_DEVICE
            // 
            this.SAW_DEVICE.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SAW_DEVICE.FormattingEnabled = true;
            this.SAW_DEVICE.Location = new System.Drawing.Point(208, 85);
            this.SAW_DEVICE.Name = "SAW_DEVICE";
            this.SAW_DEVICE.Size = new System.Drawing.Size(182, 24);
            this.SAW_DEVICE.TabIndex = 1336;
            // 
            // SAW_GROUP
            // 
            this.SAW_GROUP.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SAW_GROUP.FormattingEnabled = true;
            this.SAW_GROUP.Location = new System.Drawing.Point(78, 85);
            this.SAW_GROUP.Name = "SAW_GROUP";
            this.SAW_GROUP.Size = new System.Drawing.Size(129, 24);
            this.SAW_GROUP.TabIndex = 1335;
            this.SAW_GROUP.SelectedIndexChanged += new System.EventHandler(this.SAW_GROUP_SelectedIndexChanged);
            // 
            // SAW_RECIPE
            // 
            this.SAW_RECIPE.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SAW_RECIPE.Location = new System.Drawing.Point(78, 110);
            this.SAW_RECIPE.Name = "SAW_RECIPE";
            this.SAW_RECIPE.Size = new System.Drawing.Size(357, 36);
            this.SAW_RECIPE.TabIndex = 1334;
            this.SAW_RECIPE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // HANDLER_PPID
            // 
            this.HANDLER_PPID.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HANDLER_PPID.Location = new System.Drawing.Point(78, 46);
            this.HANDLER_PPID.Name = "HANDLER_PPID";
            this.HANDLER_PPID.Size = new System.Drawing.Size(357, 36);
            this.HANDLER_PPID.TabIndex = 1333;
            this.HANDLER_PPID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.Color.White;
            this.Cancel.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancel.ForeColor = System.Drawing.Color.Black;
            this.Cancel.Image = ((System.Drawing.Image)(resources.GetObject("Cancel.Image")));
            this.Cancel.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Cancel.Location = new System.Drawing.Point(221, 147);
            this.Cancel.Margin = new System.Windows.Forms.Padding(1);
            this.Cancel.Name = "Cancel";
            this.Cancel.Padding = new System.Windows.Forms.Padding(5, 5, 2, 2);
            this.Cancel.Size = new System.Drawing.Size(214, 49);
            this.Cancel.TabIndex = 1332;
            this.Cancel.Text = "CANCEL";
            this.Cancel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.Cancel.UseVisualStyleBackColor = false;
            // 
            // OK
            // 
            this.OK.BackColor = System.Drawing.Color.White;
            this.OK.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OK.ForeColor = System.Drawing.Color.Black;
            this.OK.Image = ((System.Drawing.Image)(resources.GetObject("OK.Image")));
            this.OK.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.OK.Location = new System.Drawing.Point(4, 147);
            this.OK.Margin = new System.Windows.Forms.Padding(1);
            this.OK.Name = "OK";
            this.OK.Padding = new System.Windows.Forms.Padding(5, 5, 2, 2);
            this.OK.Size = new System.Drawing.Size(214, 49);
            this.OK.TabIndex = 1331;
            this.OK.Text = "OK";
            this.OK.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.OK.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(5, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 60);
            this.label3.TabIndex = 1323;
            this.label3.Text = "SAWING\r\n(다이싱)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(5, 46);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 36);
            this.label4.TabIndex = 1322;
            this.label4.Text = "HANDLER\r\n(관리번호)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(438, 42);
            this.label2.TabIndex = 1318;
            this.label2.Text = "RECIPE NAME (PPID)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbxLoading
            // 
            this.pbxLoading.BackColor = System.Drawing.Color.White;
            this.pbxLoading.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbxLoading.Image = ((System.Drawing.Image)(resources.GetObject("pbxLoading.Image")));
            this.pbxLoading.Location = new System.Drawing.Point(0, 353);
            this.pbxLoading.Name = "pbxLoading";
            this.pbxLoading.Size = new System.Drawing.Size(445, 15);
            this.pbxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxLoading.TabIndex = 8;
            this.pbxLoading.TabStop = false;
            this.pbxLoading.Visible = false;
            // 
            // FormRecipeCreate
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(445, 368);
            this.ControlBox = false;
            this.Controls.Add(this.pbxLoading);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlLinkFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormRecipeCreate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Recipe Create";
            this.pnlLinkFrom.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Timer CHECK_DELAY_TIME;
        private System.Windows.Forms.Panel pnlLinkFrom;
        private System.Windows.Forms.Label DEVICE;
        private System.Windows.Forms.ComboBox DEVICE_INDEX;
        private System.Windows.Forms.Label GROUP;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label lbPattern;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SET_SAW_RECIPE;
        private System.Windows.Forms.ComboBox SAW_DEVICE;
        private System.Windows.Forms.ComboBox SAW_GROUP;
        private System.Windows.Forms.TextBox SAW_RECIPE;
        private System.Windows.Forms.TextBox HANDLER_PPID;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbxLoading;
    }
}