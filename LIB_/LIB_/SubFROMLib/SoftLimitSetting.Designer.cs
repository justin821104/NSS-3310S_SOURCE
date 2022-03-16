namespace LIB_.SubFROMLib
{
    partial class SoftLimitSetting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoftLimitSetting));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvMTSOFTSET = new System.Windows.Forms.DataGridView();
            this.NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.swSAVE = new System.Windows.Forms.Button();
            this.swClose = new System.Windows.Forms.Button();
            this.tmrSoftLimitSetting = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMTSOFTSET)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.ItemSize = new System.Drawing.Size(71, 25);
            this.tabControl1.Location = new System.Drawing.Point(1, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1240, 749);
            this.tabControl1.TabIndex = 47;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvMTSOFTSET);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1232, 716);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "MOTOR SOFT LIMIT";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvMTSOFTSET
            // 
            this.dgvMTSOFTSET.AllowUserToAddRows = false;
            this.dgvMTSOFTSET.AllowUserToDeleteRows = false;
            this.dgvMTSOFTSET.AllowUserToOrderColumns = true;
            this.dgvMTSOFTSET.AllowUserToResizeColumns = false;
            this.dgvMTSOFTSET.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMTSOFTSET.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMTSOFTSET.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMTSOFTSET.ColumnHeadersHeight = 37;
            this.dgvMTSOFTSET.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NO,
            this.NAME,
            this.CCW,
            this.CW,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMTSOFTSET.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMTSOFTSET.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvMTSOFTSET.Location = new System.Drawing.Point(3, 3);
            this.dgvMTSOFTSET.Name = "dgvMTSOFTSET";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMTSOFTSET.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMTSOFTSET.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMTSOFTSET.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvMTSOFTSET.RowTemplate.Height = 23;
            this.dgvMTSOFTSET.Size = new System.Drawing.Size(1227, 710);
            this.dgvMTSOFTSET.TabIndex = 42;
            this.dgvMTSOFTSET.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MTSOFTSET_CellClick);
            // 
            // NO
            // 
            this.NO.HeaderText = "NO";
            this.NO.Name = "NO";
            this.NO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NO.Width = 32;
            // 
            // NAME
            // 
            this.NAME.HeaderText = "NAME";
            this.NAME.Name = "NAME";
            this.NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NAME.Width = 215;
            // 
            // CCW
            // 
            this.CCW.HeaderText = "CCW SOFT LIMIT";
            this.CCW.Name = "CCW";
            this.CCW.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CCW.Width = 75;
            // 
            // CW
            // 
            this.CW.HeaderText = "CW SOFT LIMIT";
            this.CW.Name = "CW";
            this.CW.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CW.Width = 75;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "MAX PITCH";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 75;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "MIN SPEED";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 75;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "MAX SPEED";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 75;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "MIN ACC";
            this.Column4.Name = "Column4";
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 75;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "MAX ACC";
            this.Column5.Name = "Column5";
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 75;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "MIN DEC";
            this.Column6.Name = "Column6";
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 75;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "MAX DEC";
            this.Column7.Name = "Column7";
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column7.Width = 75;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "JOG LOW";
            this.Column8.Name = "Column8";
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column8.Width = 70;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "JOG MIDDLE";
            this.Column9.Name = "Column9";
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column9.Width = 70;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "JOG HIGH";
            this.Column10.Name = "Column10";
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column10.Width = 70;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "CUR POS";
            this.Column11.Name = "Column11";
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column11.Width = 73;
            // 
            // swSAVE
            // 
            this.swSAVE.BackColor = System.Drawing.Color.White;
            this.swSAVE.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.swSAVE.Image = ((System.Drawing.Image)(resources.GetObject("swSAVE.Image")));
            this.swSAVE.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swSAVE.Location = new System.Drawing.Point(755, 752);
            this.swSAVE.Name = "swSAVE";
            this.swSAVE.Padding = new System.Windows.Forms.Padding(5);
            this.swSAVE.Size = new System.Drawing.Size(240, 47);
            this.swSAVE.TabIndex = 136;
            this.swSAVE.Text = "SAVE";
            this.swSAVE.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swSAVE.UseVisualStyleBackColor = false;
            // 
            // swClose
            // 
            this.swClose.BackColor = System.Drawing.Color.White;
            this.swClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.swClose.ForeColor = System.Drawing.Color.Black;
            this.swClose.Image = ((System.Drawing.Image)(resources.GetObject("swClose.Image")));
            this.swClose.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swClose.Location = new System.Drawing.Point(997, 752);
            this.swClose.Name = "swClose";
            this.swClose.Padding = new System.Windows.Forms.Padding(5);
            this.swClose.Size = new System.Drawing.Size(240, 47);
            this.swClose.TabIndex = 137;
            this.swClose.Text = "CLOSE";
            this.swClose.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swClose.UseVisualStyleBackColor = false;
            // 
            // tmrSoftLimitSetting
            // 
            this.tmrSoftLimitSetting.Interval = 500;
            this.tmrSoftLimitSetting.Tick += new System.EventHandler(this.TimerSoftLimitSetting_Tick);
            // 
            // SoftLimitSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1244, 801);
            this.ControlBox = false;
            this.Controls.Add(this.swClose);
            this.Controls.Add(this.swSAVE);
            this.Controls.Add(this.tabControl1);
            this.Name = "SoftLimitSetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "SOFT LIMIT SETTING";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMTSOFTSET)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvMTSOFTSET;
        internal System.Windows.Forms.Button swSAVE;
        internal System.Windows.Forms.Button swClose;
        public System.Windows.Forms.Timer tmrSoftLimitSetting;
        private System.Windows.Forms.DataGridViewTextBoxColumn NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCW;
        private System.Windows.Forms.DataGridViewTextBoxColumn CW;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
    }
}