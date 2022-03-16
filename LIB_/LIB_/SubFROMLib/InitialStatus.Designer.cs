namespace LIB_.SubFROMLib
{
    partial class InitialStatus
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitialStatus));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridMotor = new System.Windows.Forms.DataGridView();
            this.swClose = new System.Windows.Forms.Button();
            this.lbMsg = new System.Windows.Forms.Label();
            this.lbInitSts = new System.Windows.Forms.Label();
            this.tmrInitialStatus = new System.Windows.Forms.Timer(this.components);
            this.editLOG = new System.Windows.Forms.RichTextBox();
            this.DataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridMotor)).BeginInit();
            this.SuspendLayout();
            // 
            // gridMotor
            // 
            this.gridMotor.AllowUserToAddRows = false;
            this.gridMotor.AllowUserToDeleteRows = false;
            this.gridMotor.AllowUserToResizeColumns = false;
            this.gridMotor.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.gridMotor.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridMotor.BackgroundColor = System.Drawing.Color.FloralWhite;
            this.gridMotor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.gridMotor.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridMotor.ColumnHeadersHeight = 37;
            this.gridMotor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridMotor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataGridViewTextBoxColumn5,
            this.Column2});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridMotor.DefaultCellStyle = dataGridViewCellStyle5;
            this.gridMotor.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridMotor.GridColor = System.Drawing.Color.Silver;
            this.gridMotor.Location = new System.Drawing.Point(0, 0);
            this.gridMotor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridMotor.MultiSelect = false;
            this.gridMotor.Name = "gridMotor";
            this.gridMotor.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridMotor.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridMotor.RowHeadersVisible = false;
            this.gridMotor.RowHeadersWidth = 35;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.gridMotor.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.gridMotor.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gridMotor.RowTemplate.Height = 23;
            this.gridMotor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridMotor.ShowCellErrors = false;
            this.gridMotor.ShowEditingIcon = false;
            this.gridMotor.ShowRowErrors = false;
            this.gridMotor.Size = new System.Drawing.Size(417, 402);
            this.gridMotor.StandardTab = true;
            this.gridMotor.TabIndex = 843;
            // 
            // swClose
            // 
            this.swClose.BackColor = System.Drawing.Color.White;
            this.swClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.swClose.Image = ((System.Drawing.Image)(resources.GetObject("swClose.Image")));
            this.swClose.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swClose.Location = new System.Drawing.Point(323, 406);
            this.swClose.Name = "swClose";
            this.swClose.Padding = new System.Windows.Forms.Padding(5);
            this.swClose.Size = new System.Drawing.Size(93, 49);
            this.swClose.TabIndex = 842;
            this.swClose.Tag = "0";
            this.swClose.Text = "CLOSE";
            this.swClose.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swClose.UseVisualStyleBackColor = false;
            this.swClose.Click += new System.EventHandler(this.Close_Click);
            // 
            // lbMsg
            // 
            this.lbMsg.BackColor = System.Drawing.Color.Black;
            this.lbMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbMsg.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMsg.ForeColor = System.Drawing.Color.Orange;
            this.lbMsg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbMsg.Location = new System.Drawing.Point(0, 456);
            this.lbMsg.Name = "lbMsg";
            this.lbMsg.Size = new System.Drawing.Size(417, 38);
            this.lbMsg.TabIndex = 841;
            this.lbMsg.Text = "Home OK";
            this.lbMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbInitSts
            // 
            this.lbInitSts.BackColor = System.Drawing.Color.Black;
            this.lbInitSts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbInitSts.Font = new System.Drawing.Font("맑은 고딕", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbInitSts.ForeColor = System.Drawing.Color.Orange;
            this.lbInitSts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbInitSts.Location = new System.Drawing.Point(0, 406);
            this.lbInitSts.Name = "lbInitSts";
            this.lbInitSts.Size = new System.Drawing.Size(321, 49);
            this.lbInitSts.TabIndex = 840;
            this.lbInitSts.Text = "Home OK";
            this.lbInitSts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrInitialStatus
            // 
            this.tmrInitialStatus.Tick += new System.EventHandler(this.TimerInitialStatus_Tick);
            // 
            // editLOG
            // 
            this.editLOG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editLOG.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.editLOG.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.editLOG.Location = new System.Drawing.Point(0, 496);
            this.editLOG.Name = "editLOG";
            this.editLOG.Size = new System.Drawing.Size(417, 69);
            this.editLOG.TabIndex = 844;
            this.editLOG.Text = "LOG(로그)";
            // 
            // DataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridViewTextBoxColumn5.HeaderText = "MOTOR NAME";
            this.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5";
            this.DataGridViewTextBoxColumn5.ReadOnly = true;
            this.DataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn5.Width = 278;
            // 
            // Column2
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column2.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column2.HeaderText = "STATUS";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 120;
            // 
            // InitialStatus
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(417, 565);
            this.ControlBox = false;
            this.Controls.Add(this.editLOG);
            this.Controls.Add(this.gridMotor);
            this.Controls.Add(this.swClose);
            this.Controls.Add(this.lbMsg);
            this.Controls.Add(this.lbInitSts);
            this.Name = "InitialStatus";
            this.Text = "INITIAL MESSAGE";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.InitialStatus_Activated);
            this.Shown += new System.EventHandler(this.InitialStatus_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gridMotor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataGridView gridMotor;
        private System.Windows.Forms.Button swClose;
        internal System.Windows.Forms.Label lbMsg;
        internal System.Windows.Forms.Label lbInitSts;
        private System.Windows.Forms.RichTextBox editLOG;
        public System.Windows.Forms.Timer tmrInitialStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}