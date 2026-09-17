namespace LIB_.SubFROMLib
{
    partial class IOCheck
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
            this.tmrIOCheck = new System.Windows.Forms.Timer(this.components);
            this.panMenu = new System.Windows.Forms.Panel();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.rb16 = new System.Windows.Forms.RadioButton();
            this.rb10 = new System.Windows.Forms.RadioButton();
            this.swReport = new System.Windows.Forms.Button();
            this.swSave = new System.Windows.Forms.Button();
            this.swClose = new System.Windows.Forms.Button();
            this.tapIO = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.gridIn = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridOut = new System.Windows.Forms.DataGridView();
            this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.editReport = new System.Windows.Forms.RichTextBox();
            this.panMenu.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.tapIO.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOut)).BeginInit();
            this.TabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrIOCheck
            // 
            this.tmrIOCheck.Interval = 150;
            this.tmrIOCheck.Tick += new System.EventHandler(this.TimerIOCheck_Tick);
            // 
            // panMenu
            // 
            this.panMenu.BackColor = System.Drawing.Color.Silver;
            this.panMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panMenu.Controls.Add(this.Panel1);
            this.panMenu.Controls.Add(this.swReport);
            this.panMenu.Controls.Add(this.swSave);
            this.panMenu.Controls.Add(this.swClose);
            this.panMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panMenu.Location = new System.Drawing.Point(0, 0);
            this.panMenu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panMenu.Name = "panMenu";
            this.panMenu.Size = new System.Drawing.Size(1124, 47);
            this.panMenu.TabIndex = 6;
            // 
            // Panel1
            // 
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.rb16);
            this.Panel1.Controls.Add(this.rb10);
            this.Panel1.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Panel1.Location = new System.Drawing.Point(848, 5);
            this.Panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(173, 34);
            this.Panel1.TabIndex = 12;
            // 
            // rb16
            // 
            this.rb16.AutoSize = true;
            this.rb16.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb16.ForeColor = System.Drawing.Color.White;
            this.rb16.Location = new System.Drawing.Point(97, 4);
            this.rb16.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rb16.Name = "rb16";
            this.rb16.Size = new System.Drawing.Size(73, 24);
            this.rb16.TabIndex = 1;
            this.rb16.TabStop = true;
            this.rb16.Text = "16진수";
            this.rb16.UseVisualStyleBackColor = true;
            this.rb16.Click += new System.EventHandler(this.Numeral_Click);
            // 
            // rb10
            // 
            this.rb10.AutoSize = true;
            this.rb10.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb10.ForeColor = System.Drawing.Color.White;
            this.rb10.Location = new System.Drawing.Point(5, 4);
            this.rb10.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rb10.Name = "rb10";
            this.rb10.Size = new System.Drawing.Size(73, 24);
            this.rb10.TabIndex = 0;
            this.rb10.TabStop = true;
            this.rb10.Text = "10진수";
            this.rb10.UseVisualStyleBackColor = true;
            this.rb10.Click += new System.EventHandler(this.Numeral_Click);
            // 
            // swReport
            // 
            this.swReport.BackColor = System.Drawing.Color.Black;
            this.swReport.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.swReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.swReport.Location = new System.Drawing.Point(1, 1);
            this.swReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.swReport.Name = "swReport";
            this.swReport.Size = new System.Drawing.Size(92, 42);
            this.swReport.TabIndex = 11;
            this.swReport.Text = "REPORT";
            this.swReport.UseVisualStyleBackColor = false;
            this.swReport.Click += new System.EventHandler(this.Report_Click);
            // 
            // swSave
            // 
            this.swSave.BackColor = System.Drawing.Color.Black;
            this.swSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.swSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.swSave.Location = new System.Drawing.Point(94, 1);
            this.swSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.swSave.Name = "swSave";
            this.swSave.Size = new System.Drawing.Size(92, 42);
            this.swSave.TabIndex = 10;
            this.swSave.Text = "SAVE";
            this.swSave.UseVisualStyleBackColor = false;
            this.swSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // swClose
            // 
            this.swClose.BackColor = System.Drawing.Color.Black;
            this.swClose.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.swClose.ForeColor = System.Drawing.Color.Red;
            this.swClose.Location = new System.Drawing.Point(1024, 1);
            this.swClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.swClose.Name = "swClose";
            this.swClose.Size = new System.Drawing.Size(97, 41);
            this.swClose.TabIndex = 6;
            this.swClose.Text = "CLOSE";
            this.swClose.UseVisualStyleBackColor = false;
            this.swClose.Click += new System.EventHandler(this.Close_Click);
            // 
            // tapIO
            // 
            this.tapIO.Controls.Add(this.TabPage1);
            this.tapIO.Controls.Add(this.TabPage2);
            this.tapIO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tapIO.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tapIO.Location = new System.Drawing.Point(0, 47);
            this.tapIO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tapIO.Name = "tapIO";
            this.tapIO.SelectedIndex = 0;
            this.tapIO.Size = new System.Drawing.Size(1124, 554);
            this.tapIO.TabIndex = 9;
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.gridIn);
            this.TabPage1.Controls.Add(this.gridOut);
            this.TabPage1.Location = new System.Drawing.Point(4, 24);
            this.TabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TabPage1.Size = new System.Drawing.Size(1116, 526);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "I/O";
            this.TabPage1.UseVisualStyleBackColor = true;
            // 
            // gridIn
            // 
            this.gridIn.AllowUserToAddRows = false;
            this.gridIn.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gridIn.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridIn.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.gridIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridIn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column4,
            this.Column1,
            this.Column5,
            this.Column6});
            this.gridIn.Dock = System.Windows.Forms.DockStyle.Left;
            this.gridIn.Location = new System.Drawing.Point(3, 4);
            this.gridIn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridIn.MultiSelect = false;
            this.gridIn.Name = "gridIn";
            this.gridIn.RowHeadersVisible = false;
            this.gridIn.RowHeadersWidth = 20;
            this.gridIn.RowTemplate.Height = 30;
            this.gridIn.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridIn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridIn.Size = new System.Drawing.Size(549, 518);
            this.gridIn.TabIndex = 2;
            this.gridIn.DoubleClick += new System.EventHandler(this.In_DoubleClick);
            // 
            // Column2
            // 
            this.Column2.FillWeight = 80F;
            this.Column2.HeaderText = "No";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 34;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 80F;
            this.Column4.HeaderText = "INPUT NAME";
            this.Column4.Name = "Column4";
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 362;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Num";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 47;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "접점";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 43;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "확인";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 43;
            // 
            // gridOut
            // 
            this.gridOut.AllowUserToAddRows = false;
            this.gridOut.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gridOut.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridOut.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.gridOut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOut.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataGridViewTextBoxColumn1,
            this.DataGridViewTextBoxColumn2,
            this.DataGridViewTextBoxColumn3,
            this.DataGridViewTextBoxColumn5,
            this.Column8});
            this.gridOut.Dock = System.Windows.Forms.DockStyle.Right;
            this.gridOut.Location = new System.Drawing.Point(559, 4);
            this.gridOut.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridOut.MultiSelect = false;
            this.gridOut.Name = "gridOut";
            this.gridOut.RowHeadersVisible = false;
            this.gridOut.RowHeadersWidth = 20;
            this.gridOut.RowTemplate.Height = 30;
            this.gridOut.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridOut.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridOut.Size = new System.Drawing.Size(554, 518);
            this.gridOut.TabIndex = 3;
            this.gridOut.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Out_CellClick);
            this.gridOut.DoubleClick += new System.EventHandler(this.Out_DoubleClick);
            // 
            // DataGridViewTextBoxColumn1
            // 
            this.DataGridViewTextBoxColumn1.FillWeight = 80F;
            this.DataGridViewTextBoxColumn1.HeaderText = "No";
            this.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
            this.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn1.Width = 34;
            // 
            // DataGridViewTextBoxColumn2
            // 
            this.DataGridViewTextBoxColumn2.FillWeight = 80F;
            this.DataGridViewTextBoxColumn2.HeaderText = "OUTPUT NAME";
            this.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2";
            this.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn2.Width = 360;
            // 
            // DataGridViewTextBoxColumn3
            // 
            this.DataGridViewTextBoxColumn3.HeaderText = "Num";
            this.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3";
            this.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn3.Width = 45;
            // 
            // DataGridViewTextBoxColumn5
            // 
            this.DataGridViewTextBoxColumn5.HeaderText = "확인";
            this.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5";
            this.DataGridViewTextBoxColumn5.ReadOnly = true;
            this.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn5.Width = 40;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "온/오프";
            this.Column8.Name = "Column8";
            this.Column8.Width = 55;
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.editReport);
            this.TabPage2.Location = new System.Drawing.Point(4, 24);
            this.TabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TabPage2.Size = new System.Drawing.Size(1116, 526);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "REPORT";
            this.TabPage2.UseVisualStyleBackColor = true;
            // 
            // editReport
            // 
            this.editReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editReport.Location = new System.Drawing.Point(3, 4);
            this.editReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editReport.Name = "editReport";
            this.editReport.Size = new System.Drawing.Size(1110, 518);
            this.editReport.TabIndex = 0;
            this.editReport.Text = "";
            // 
            // IOCheck
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1124, 601);
            this.ControlBox = false;
            this.Controls.Add(this.tapIO);
            this.Controls.Add(this.panMenu);
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Name = "IOCheck";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "IO CHECK";
            this.panMenu.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.tapIO.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOut)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Panel panMenu;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.RadioButton rb16;
        internal System.Windows.Forms.RadioButton rb10;
        internal System.Windows.Forms.Button swReport;
        internal System.Windows.Forms.Button swSave;
        internal System.Windows.Forms.Button swClose;
        internal System.Windows.Forms.TabControl tapIO;
        internal System.Windows.Forms.TabPage TabPage1;
        internal System.Windows.Forms.DataGridView gridIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        internal System.Windows.Forms.DataGridView gridOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewButtonColumn Column8;
        internal System.Windows.Forms.TabPage TabPage2;
        internal System.Windows.Forms.RichTextBox editReport;
        public System.Windows.Forms.Timer tmrIOCheck;
    }
}