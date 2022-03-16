namespace LIB_.SubFROMLib
{
    partial class MTPosTeachingData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MTPosTeachingData));
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.swRMove = new System.Windows.Forms.Button();
            this.edit_RMove = new System.Windows.Forms.TextBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.swOffsetTeach = new System.Windows.Forms.Button();
            this.editOffset = new System.Windows.Forms.TextBox();
            this.panRange = new System.Windows.Forms.GroupBox();
            this.cbEnable = new System.Windows.Forms.CheckBox();
            this.swApply = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.editPlus = new System.Windows.Forms.TextBox();
            this.lbMinus = new System.Windows.Forms.Label();
            this.editMinus = new System.Windows.Forms.TextBox();
            this.swCurrentSet = new System.Windows.Forms.Button();
            this.swSave = new System.Windows.Forms.Button();
            this.swInfo = new System.Windows.Forms.Button();
            this.swClose = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.lbUNIT_DELAY = new System.Windows.Forms.Label();
            this.lbUNIT_MOVE = new System.Windows.Forms.Label();
            this.lbUNIT_DEC = new System.Windows.Forms.Label();
            this.lbUNIT_ACC = new System.Windows.Forms.Label();
            this.lbUNIT_SPD = new System.Windows.Forms.Label();
            this.lbUNIT_POS = new System.Windows.Forms.Label();
            this.lbDelay = new System.Windows.Forms.Label();
            this.lbPosName = new System.Windows.Forms.Label();
            this.lbMotorName = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.lbDeccel = new System.Windows.Forms.Label();
            this.lbPosition = new System.Windows.Forms.Label();
            this.lbAccel = new System.Windows.Forms.Label();
            this.lbSpeed = new System.Windows.Forms.Label();
            this.tmrMTPositionTeachingData = new System.Windows.Forms.Timer(this.components);
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.panRange.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox1.Controls.Add(this.swRMove);
            this.GroupBox1.Controls.Add(this.edit_RMove);
            this.GroupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox1.ForeColor = System.Drawing.Color.Black;
            this.GroupBox1.Location = new System.Drawing.Point(2, 145);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupBox1.Size = new System.Drawing.Size(137, 55);
            this.GroupBox1.TabIndex = 69;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "TEACHING MOVE";
            // 
            // swRMove
            // 
            this.swRMove.BackColor = System.Drawing.Color.White;
            this.swRMove.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.swRMove.ForeColor = System.Drawing.Color.Black;
            this.swRMove.Location = new System.Drawing.Point(2, 14);
            this.swRMove.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.swRMove.Name = "swRMove";
            this.swRMove.Size = new System.Drawing.Size(86, 38);
            this.swRMove.TabIndex = 12;
            this.swRMove.Text = "R-MOVE";
            this.swRMove.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swRMove.UseVisualStyleBackColor = false;
            this.swRMove.Click += new System.EventHandler(this.RMove_Click);
            // 
            // edit_RMove
            // 
            this.edit_RMove.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edit_RMove.Location = new System.Drawing.Point(89, 16);
            this.edit_RMove.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.edit_RMove.Name = "edit_RMove";
            this.edit_RMove.Size = new System.Drawing.Size(45, 22);
            this.edit_RMove.TabIndex = 11;
            this.edit_RMove.Text = "0.1";
            this.edit_RMove.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.swOffsetTeach);
            this.GroupBox2.Controls.Add(this.editOffset);
            this.GroupBox2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox2.ForeColor = System.Drawing.Color.Black;
            this.GroupBox2.Location = new System.Drawing.Point(138, 145);
            this.GroupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupBox2.Size = new System.Drawing.Size(137, 55);
            this.GroupBox2.TabIndex = 70;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "OFF-SET TEACHING";
            // 
            // swOffsetTeach
            // 
            this.swOffsetTeach.BackColor = System.Drawing.Color.White;
            this.swOffsetTeach.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.swOffsetTeach.ForeColor = System.Drawing.Color.Black;
            this.swOffsetTeach.Location = new System.Drawing.Point(2, 14);
            this.swOffsetTeach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.swOffsetTeach.Name = "swOffsetTeach";
            this.swOffsetTeach.Size = new System.Drawing.Size(86, 38);
            this.swOffsetTeach.TabIndex = 12;
            this.swOffsetTeach.Text = "OFF-SET\r\nTEACHING";
            this.swOffsetTeach.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swOffsetTeach.UseVisualStyleBackColor = false;
            this.swOffsetTeach.Click += new System.EventHandler(this.OffsetTeach_Click);
            // 
            // editOffset
            // 
            this.editOffset.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editOffset.Location = new System.Drawing.Point(89, 16);
            this.editOffset.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editOffset.Name = "editOffset";
            this.editOffset.Size = new System.Drawing.Size(45, 22);
            this.editOffset.TabIndex = 11;
            this.editOffset.Text = "0.1";
            this.editOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panRange
            // 
            this.panRange.Controls.Add(this.cbEnable);
            this.panRange.Controls.Add(this.swApply);
            this.panRange.Controls.Add(this.Label1);
            this.panRange.Controls.Add(this.editPlus);
            this.panRange.Controls.Add(this.lbMinus);
            this.panRange.Controls.Add(this.editMinus);
            this.panRange.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panRange.ForeColor = System.Drawing.Color.Black;
            this.panRange.Location = new System.Drawing.Point(274, 145);
            this.panRange.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panRange.Name = "panRange";
            this.panRange.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panRange.Size = new System.Drawing.Size(269, 55);
            this.panRange.TabIndex = 71;
            this.panRange.TabStop = false;
            this.panRange.Text = "Teaching Range";
            // 
            // cbEnable
            // 
            this.cbEnable.AutoSize = true;
            this.cbEnable.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEnable.ForeColor = System.Drawing.Color.Red;
            this.cbEnable.Location = new System.Drawing.Point(177, 15);
            this.cbEnable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbEnable.Name = "cbEnable";
            this.cbEnable.Size = new System.Drawing.Size(45, 17);
            this.cbEnable.TabIndex = 17;
            this.cbEnable.Text = "USE";
            this.cbEnable.UseVisualStyleBackColor = true;
            // 
            // swApply
            // 
            this.swApply.BackColor = System.Drawing.Color.White;
            this.swApply.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.swApply.ForeColor = System.Drawing.Color.Red;
            this.swApply.Location = new System.Drawing.Point(223, 10);
            this.swApply.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.swApply.Name = "swApply";
            this.swApply.Size = new System.Drawing.Size(43, 42);
            this.swApply.TabIndex = 16;
            this.swApply.Text = "Apply";
            this.swApply.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swApply.UseVisualStyleBackColor = false;
            this.swApply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.ForeColor = System.Drawing.Color.Brown;
            this.Label1.Location = new System.Drawing.Point(84, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(63, 13);
            this.Label1.TabIndex = 15;
            this.Label1.Text = "+ Limit(mm)";
            // 
            // editPlus
            // 
            this.editPlus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editPlus.Location = new System.Drawing.Point(87, 28);
            this.editPlus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editPlus.Name = "editPlus";
            this.editPlus.Size = new System.Drawing.Size(76, 22);
            this.editPlus.TabIndex = 14;
            this.editPlus.Text = "0.0";
            this.editPlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbMinus
            // 
            this.lbMinus.AutoSize = true;
            this.lbMinus.ForeColor = System.Drawing.Color.Brown;
            this.lbMinus.Location = new System.Drawing.Point(4, 13);
            this.lbMinus.Name = "lbMinus";
            this.lbMinus.Size = new System.Drawing.Size(59, 13);
            this.lbMinus.TabIndex = 13;
            this.lbMinus.Text = "- Limit(mm)";
            // 
            // editMinus
            // 
            this.editMinus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editMinus.Location = new System.Drawing.Point(7, 28);
            this.editMinus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editMinus.Name = "editMinus";
            this.editMinus.Size = new System.Drawing.Size(76, 22);
            this.editMinus.TabIndex = 12;
            this.editMinus.Text = "0.0";
            this.editMinus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // swCurrentSet
            // 
            this.swCurrentSet.BackColor = System.Drawing.Color.White;
            this.swCurrentSet.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.swCurrentSet.ForeColor = System.Drawing.Color.Black;
            this.swCurrentSet.Image = ((System.Drawing.Image)(resources.GetObject("swCurrentSet.Image")));
            this.swCurrentSet.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swCurrentSet.Location = new System.Drawing.Point(238, 102);
            this.swCurrentSet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.swCurrentSet.Name = "swCurrentSet";
            this.swCurrentSet.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.swCurrentSet.Size = new System.Drawing.Size(153, 43);
            this.swCurrentSet.TabIndex = 66;
            this.swCurrentSet.Text = "SET\r\nCURRETN POSITION";
            this.swCurrentSet.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swCurrentSet.UseVisualStyleBackColor = false;
            this.swCurrentSet.Click += new System.EventHandler(this.CurrentSet_Click);
            // 
            // swSave
            // 
            this.swSave.BackColor = System.Drawing.Color.White;
            this.swSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.swSave.ForeColor = System.Drawing.Color.Black;
            this.swSave.Image = ((System.Drawing.Image)(resources.GetObject("swSave.Image")));
            this.swSave.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swSave.Location = new System.Drawing.Point(84, 102);
            this.swSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.swSave.Name = "swSave";
            this.swSave.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.swSave.Size = new System.Drawing.Size(154, 43);
            this.swSave.TabIndex = 67;
            this.swSave.Text = "SAVE";
            this.swSave.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swSave.UseVisualStyleBackColor = false;
            this.swSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // swInfo
            // 
            this.swInfo.BackColor = System.Drawing.Color.White;
            this.swInfo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.swInfo.FlatAppearance.BorderSize = 3;
            this.swInfo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.swInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.swInfo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.swInfo.ImageIndex = 0;
            this.swInfo.Location = new System.Drawing.Point(2, 102);
            this.swInfo.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.swInfo.Name = "swInfo";
            this.swInfo.Padding = new System.Windows.Forms.Padding(5);
            this.swInfo.Size = new System.Drawing.Size(82, 43);
            this.swInfo.TabIndex = 68;
            this.swInfo.Text = "INFO ↓";
            this.swInfo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swInfo.UseVisualStyleBackColor = false;
            this.swInfo.Click += new System.EventHandler(this.Info_Click);
            // 
            // swClose
            // 
            this.swClose.BackColor = System.Drawing.Color.White;
            this.swClose.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.swClose.ForeColor = System.Drawing.Color.Black;
            this.swClose.Image = ((System.Drawing.Image)(resources.GetObject("swClose.Image")));
            this.swClose.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.swClose.Location = new System.Drawing.Point(390, 102);
            this.swClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.swClose.Name = "swClose";
            this.swClose.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.swClose.Size = new System.Drawing.Size(153, 43);
            this.swClose.TabIndex = 65;
            this.swClose.Text = "CLOSE";
            this.swClose.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.swClose.UseVisualStyleBackColor = false;
            this.swClose.Click += new System.EventHandler(this.Close_Click);
            // 
            // Panel1
            // 
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.lbUNIT_DELAY);
            this.Panel1.Controls.Add(this.lbUNIT_MOVE);
            this.Panel1.Controls.Add(this.lbUNIT_DEC);
            this.Panel1.Controls.Add(this.lbUNIT_ACC);
            this.Panel1.Controls.Add(this.lbUNIT_SPD);
            this.Panel1.Controls.Add(this.lbUNIT_POS);
            this.Panel1.Controls.Add(this.lbDelay);
            this.Panel1.Controls.Add(this.lbPosName);
            this.Panel1.Controls.Add(this.lbMotorName);
            this.Panel1.Controls.Add(this.lbTime);
            this.Panel1.Controls.Add(this.lbDeccel);
            this.Panel1.Controls.Add(this.lbPosition);
            this.Panel1.Controls.Add(this.lbAccel);
            this.Panel1.Controls.Add(this.lbSpeed);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(545, 101);
            this.Panel1.TabIndex = 64;
            // 
            // lbUNIT_DELAY
            // 
            this.lbUNIT_DELAY.BackColor = System.Drawing.Color.Black;
            this.lbUNIT_DELAY.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUNIT_DELAY.ForeColor = System.Drawing.Color.Lime;
            this.lbUNIT_DELAY.Location = new System.Drawing.Point(452, 43);
            this.lbUNIT_DELAY.Name = "lbUNIT_DELAY";
            this.lbUNIT_DELAY.Size = new System.Drawing.Size(89, 15);
            this.lbUNIT_DELAY.TabIndex = 48;
            this.lbUNIT_DELAY.Text = "DELAY (msec)";
            this.lbUNIT_DELAY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbUNIT_MOVE
            // 
            this.lbUNIT_MOVE.BackColor = System.Drawing.Color.Black;
            this.lbUNIT_MOVE.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUNIT_MOVE.ForeColor = System.Drawing.Color.Lime;
            this.lbUNIT_MOVE.Location = new System.Drawing.Point(362, 43);
            this.lbUNIT_MOVE.Name = "lbUNIT_MOVE";
            this.lbUNIT_MOVE.Size = new System.Drawing.Size(89, 15);
            this.lbUNIT_MOVE.TabIndex = 47;
            this.lbUNIT_MOVE.Text = "MOVE (msec)";
            this.lbUNIT_MOVE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbUNIT_DEC
            // 
            this.lbUNIT_DEC.BackColor = System.Drawing.Color.Black;
            this.lbUNIT_DEC.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUNIT_DEC.ForeColor = System.Drawing.Color.Lime;
            this.lbUNIT_DEC.Location = new System.Drawing.Point(272, 43);
            this.lbUNIT_DEC.Name = "lbUNIT_DEC";
            this.lbUNIT_DEC.Size = new System.Drawing.Size(89, 15);
            this.lbUNIT_DEC.TabIndex = 46;
            this.lbUNIT_DEC.Text = "DEC (mm/sec^2)";
            this.lbUNIT_DEC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbUNIT_ACC
            // 
            this.lbUNIT_ACC.BackColor = System.Drawing.Color.Black;
            this.lbUNIT_ACC.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUNIT_ACC.ForeColor = System.Drawing.Color.Lime;
            this.lbUNIT_ACC.Location = new System.Drawing.Point(182, 43);
            this.lbUNIT_ACC.Name = "lbUNIT_ACC";
            this.lbUNIT_ACC.Size = new System.Drawing.Size(89, 15);
            this.lbUNIT_ACC.TabIndex = 45;
            this.lbUNIT_ACC.Text = "ACC (mm/sec^2)";
            this.lbUNIT_ACC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbUNIT_SPD
            // 
            this.lbUNIT_SPD.BackColor = System.Drawing.Color.Black;
            this.lbUNIT_SPD.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUNIT_SPD.ForeColor = System.Drawing.Color.Lime;
            this.lbUNIT_SPD.Location = new System.Drawing.Point(92, 43);
            this.lbUNIT_SPD.Name = "lbUNIT_SPD";
            this.lbUNIT_SPD.Size = new System.Drawing.Size(89, 15);
            this.lbUNIT_SPD.TabIndex = 44;
            this.lbUNIT_SPD.Text = "SPEED (mm/sec)";
            this.lbUNIT_SPD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbUNIT_POS
            // 
            this.lbUNIT_POS.BackColor = System.Drawing.Color.Black;
            this.lbUNIT_POS.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUNIT_POS.ForeColor = System.Drawing.Color.Lime;
            this.lbUNIT_POS.Location = new System.Drawing.Point(2, 43);
            this.lbUNIT_POS.Name = "lbUNIT_POS";
            this.lbUNIT_POS.Size = new System.Drawing.Size(89, 15);
            this.lbUNIT_POS.TabIndex = 43;
            this.lbUNIT_POS.Text = "POSITION (mm)";
            this.lbUNIT_POS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDelay
            // 
            this.lbDelay.BackColor = System.Drawing.Color.White;
            this.lbDelay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDelay.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbDelay.Location = new System.Drawing.Point(452, 58);
            this.lbDelay.Name = "lbDelay";
            this.lbDelay.Size = new System.Drawing.Size(89, 40);
            this.lbDelay.TabIndex = 42;
            this.lbDelay.Tag = "613";
            this.lbDelay.Text = "0000";
            this.lbDelay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbDelay.Click += new System.EventHandler(this.MT_Data_Click);
            // 
            // lbPosName
            // 
            this.lbPosName.BackColor = System.Drawing.Color.DimGray;
            this.lbPosName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lbPosName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lbPosName.Location = new System.Drawing.Point(1, 24);
            this.lbPosName.Name = "lbPosName";
            this.lbPosName.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lbPosName.Size = new System.Drawing.Size(541, 17);
            this.lbPosName.TabIndex = 41;
            this.lbPosName.Text = "POSITION NAME (위치 정보)";
            this.lbPosName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbMotorName
            // 
            this.lbMotorName.BackColor = System.Drawing.Color.Black;
            this.lbMotorName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbMotorName.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMotorName.ForeColor = System.Drawing.Color.Yellow;
            this.lbMotorName.Location = new System.Drawing.Point(0, 0);
            this.lbMotorName.Margin = new System.Windows.Forms.Padding(3);
            this.lbMotorName.Name = "lbMotorName";
            this.lbMotorName.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lbMotorName.Size = new System.Drawing.Size(543, 23);
            this.lbMotorName.TabIndex = 40;
            this.lbMotorName.Text = "POSITION NAME (위치 정보)";
            this.lbMotorName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTime
            // 
            this.lbTime.BackColor = System.Drawing.Color.White;
            this.lbTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTime.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbTime.Location = new System.Drawing.Point(362, 58);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(89, 40);
            this.lbTime.TabIndex = 39;
            this.lbTime.Tag = "613";
            this.lbTime.Text = "0000";
            this.lbTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbTime.Click += new System.EventHandler(this.MT_Data_Click);
            // 
            // lbDeccel
            // 
            this.lbDeccel.BackColor = System.Drawing.Color.White;
            this.lbDeccel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDeccel.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbDeccel.Location = new System.Drawing.Point(272, 58);
            this.lbDeccel.Name = "lbDeccel";
            this.lbDeccel.Size = new System.Drawing.Size(89, 40);
            this.lbDeccel.TabIndex = 38;
            this.lbDeccel.Tag = "613";
            this.lbDeccel.Text = "0000";
            this.lbDeccel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbDeccel.Click += new System.EventHandler(this.MT_Data_Click);
            // 
            // lbPosition
            // 
            this.lbPosition.BackColor = System.Drawing.Color.White;
            this.lbPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPosition.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbPosition.Location = new System.Drawing.Point(2, 58);
            this.lbPosition.Name = "lbPosition";
            this.lbPosition.Size = new System.Drawing.Size(89, 40);
            this.lbPosition.TabIndex = 35;
            this.lbPosition.Tag = "613";
            this.lbPosition.Text = "0000.000";
            this.lbPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbPosition.Click += new System.EventHandler(this.MT_Data_Click);
            // 
            // lbAccel
            // 
            this.lbAccel.BackColor = System.Drawing.Color.White;
            this.lbAccel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbAccel.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbAccel.Location = new System.Drawing.Point(182, 58);
            this.lbAccel.Name = "lbAccel";
            this.lbAccel.Size = new System.Drawing.Size(89, 40);
            this.lbAccel.TabIndex = 37;
            this.lbAccel.Tag = "613";
            this.lbAccel.Text = "0000";
            this.lbAccel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbAccel.Click += new System.EventHandler(this.MT_Data_Click);
            // 
            // lbSpeed
            // 
            this.lbSpeed.BackColor = System.Drawing.Color.White;
            this.lbSpeed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbSpeed.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbSpeed.Location = new System.Drawing.Point(92, 58);
            this.lbSpeed.Name = "lbSpeed";
            this.lbSpeed.Size = new System.Drawing.Size(89, 40);
            this.lbSpeed.TabIndex = 36;
            this.lbSpeed.Tag = "613";
            this.lbSpeed.Text = "0000";
            this.lbSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbSpeed.Click += new System.EventHandler(this.MT_Data_Click);
            // 
            // tmrMTPositionTeachingData
            // 
            this.tmrMTPositionTeachingData.Interval = 500;
            this.tmrMTPositionTeachingData.Tick += new System.EventHandler(this.TimerMTPositionTeachingData_Tick);
            // 
            // MTPosTeachingData
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(545, 203);
            this.ControlBox = false;
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.panRange);
            this.Controls.Add(this.swCurrentSet);
            this.Controls.Add(this.swSave);
            this.Controls.Add(this.swInfo);
            this.Controls.Add(this.swClose);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MTPosTeachingData";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "POSITION TEACHING";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MTPosTeachingData_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.panRange.ResumeLayout(false);
            this.panRange.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Button swRMove;
        internal System.Windows.Forms.TextBox edit_RMove;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.Button swOffsetTeach;
        internal System.Windows.Forms.TextBox editOffset;
        internal System.Windows.Forms.GroupBox panRange;
        internal System.Windows.Forms.CheckBox cbEnable;
        internal System.Windows.Forms.Button swApply;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox editPlus;
        internal System.Windows.Forms.Label lbMinus;
        internal System.Windows.Forms.TextBox editMinus;
        internal System.Windows.Forms.Button swCurrentSet;
        internal System.Windows.Forms.Button swSave;
        internal System.Windows.Forms.Button swInfo;
        internal System.Windows.Forms.Button swClose;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Label lbDelay;
        internal System.Windows.Forms.Label lbPosName;
        internal System.Windows.Forms.Label lbMotorName;
        internal System.Windows.Forms.Label lbTime;
        internal System.Windows.Forms.Label lbDeccel;
        internal System.Windows.Forms.Label lbPosition;
        internal System.Windows.Forms.Label lbAccel;
        internal System.Windows.Forms.Label lbSpeed;
        internal System.Windows.Forms.Timer tmrMTPositionTeachingData;
        private System.Windows.Forms.Label lbUNIT_POS;
        private System.Windows.Forms.Label lbUNIT_SPD;
        private System.Windows.Forms.Label lbUNIT_DELAY;
        private System.Windows.Forms.Label lbUNIT_MOVE;
        private System.Windows.Forms.Label lbUNIT_DEC;
        private System.Windows.Forms.Label lbUNIT_ACC;
    }
}