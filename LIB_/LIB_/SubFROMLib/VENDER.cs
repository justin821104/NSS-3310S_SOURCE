using Object;
using System;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class VENDER : Form{
        Control ctlsender;
        DataGridView dgv = null;
        Button btn = null;
        int mCol, mRow = 0;

        public VENDER(){
            InitializeComponent();

            swClose.Click += (sender, e) => { CLOSE(); };
            gridBool.DoubleClick += (sender, e) => { BOOL_(gridBool); };
            gridLong.DoubleClick += (sender, e) => { LONG_(gridLong); };
            gridFloat.DoubleClick += (sender, e) => { FLOAT_(gridFloat); };
            gridInterlock.DoubleClick += (sender, e) => { INTERLOCK_(gridInterlock); };
            gridThread.DoubleClick += (sender, e) => { THREAD_(gridThread); };
            gridMotor.DoubleClick += (sender, e) => { MTHOME_(gridMotor); };
        }

        void GET_COLROW(DataGridView g){
            mCol = g.CurrentCell.ColumnIndex;
            mRow = g.CurrentRow.Index;
        }

        public void CLOSE(){
            DATA_.eLoginLevel = DATA_.eLoginLevelBuffer;
            tmrVender.Enabled = false;
            this.Hide();
        }

        public void INI(){
            GRID_VIEW();
            tmrVender.Enabled = true;
            Show();
            BringToFront();
        }

        void GRID_VIEW(){
            gridBool.RowCount = CNT_.Memory;
            gridLong.RowCount = CNT_.Memory;
            gridFloat.RowCount = CNT_.Memory;
            gridString.RowCount = CNT_.Memory;

            gridInterlock.RowCount = CNT_.INTK;
            gridError.RowCount = CNT_.ERR;
            gridThread.RowCount = CNT_.THREAD;
            gridMotor.RowCount = CNT_.MT;
            for (short i = 0; i < CNT_.Memory; i++){
                gridBool.Rows[i].Cells[0].Value = i.ToString();
                gridBool.Rows[i].Cells[1].Value = DATA_.mBName[i];

                gridLong.Rows[i].Cells[0].Value = i.ToString();
                gridLong.Rows[i].Cells[1].Value = DATA_.mLName[i];

                gridFloat.Rows[i].Cells[0].Value = i.ToString();
                gridFloat.Rows[i].Cells[1].Value = DATA_.mDName[i];

                gridString.Rows[i].Cells[0].Value = i.ToString();
                gridString.Rows[i].Cells[1].Value = DATA_.mSName[i];
            }
            for (int i = 0; i < CNT_.INTK; i++){
                gridInterlock.Rows[i].Cells[0].Value = i.ToString();
                gridInterlock.Rows[i].Cells[1].Value = DATA_.IntkName[i];
            }
            for (int i = 0; i < CNT_.ERR; i++){
                gridError.Rows[i].Cells[0].Value = i.ToString();
                gridError.Rows[i].Cells[1].Value = DATA_.ErrName[i];
            }
            for (int i = 0; i < CNT_.THREAD; i++){
                gridThread.Rows[i].Cells[0].Value = i.ToString();
                gridThread.Rows[i].Cells[1].Value = DATA_.ThreadName[i];
            }
            for (int i = 0; i < CNT_.MT; i++){
                gridMotor.Rows[i].Cells[0].Value = i.ToString();
                gridMotor.Rows[i].Cells[1].Value = DATA_.MtName[i];
            }
        }

        void VIEW_THREAD(){
            for (short i = 0; i < CNT_.THREAD; i++){
                gridThread.Rows[i].Cells[2].Value = DATA_.LogThread[i].thSTS;
                gridThread.Rows[i].Cells[3].Value = DATA_.UseThread[i].ToString();
            }
        }
        public void THREAD_(object sender){
            dgv = (DataGridView)sender;
            GET_COLROW(dgv);
            int num = dgv.CurrentRow.Index;
            DATA_.UseThread[num] = !DATA_.UseThread[num];
        }
        private void THREAD_Click(object sender, EventArgs e){
            btn = (Button)sender;
            for (short i = 0; i < CNT_.THREAD; i++) DATA_.UseThread[i] = btn.Name == "swThAllUse" ? true : false;
        }

        void VIEW_MTHOME(){
            for (short i = 0; i < CNT_.MT; i++){
                if (DATA_.enableHome[i]) gridMotor.Rows[i].Cells[2].Value = "EV";
                else gridMotor.Rows[i].Cells[2].Value = "DIS";
            }
        }
        public void MTHOME_(object sender){
            dgv = (DataGridView)sender;
            GET_COLROW(dgv);
            int num = dgv.CurrentRow.Index;
            DATA_.enableHome[num] = !DATA_.enableHome[num];
        }
        private void HOME_Click(object sender, EventArgs e){
            btn = (Button)sender;
            for (short i = 0; i < CNT_.MT; i++) DATA_.enableHome[i] = btn.Name == "swAllUse" ? true : false;
        }

        void VIEW_BOOL() { for (short i = 0; i < CNT_.Memory; i++) gridBool.Rows[i].Cells[2].Value = DATA_.IsBIT[i].ToString(); }
        public void BOOL_(object sender){
            dgv = (DataGridView)sender;
            GET_COLROW(dgv);
            int num = dgv.CurrentRow.Index;
            DATA_.IsBIT[num] = !DATA_.IsBIT[num];
        }

        void VIEW_LONG() { for (short i = 0; i < CNT_.Memory; i++) gridLong.Rows[i].Cells[2].Value = DATA_.IsLONG[i].ToString(); }
        public void LONG_(object sender){
            dgv = (DataGridView)sender;
            GET_COLROW(dgv);
            if (mRow < 0) return;
            if (mCol == 2){
                string s = dgv.Rows[mRow].Cells[1].Value.ToString();
                UTIL_.OPEN_KEYPAD_GRID(s, ref dgv, false);
                DATA_.IsLONG[mRow] = int.Parse(dgv.Rows[mRow].Cells[2].Value.ToString());
            }
        }

        void VIEW_FLOAT() { for (short i = 0; i < CNT_.Memory; i++) gridFloat.Rows[i].Cells[2].Value = DATA_.IsDOUBLE[i].ToString(); }
        public void FLOAT_(object sender){
            dgv = (DataGridView)sender;
            GET_COLROW(dgv);
            if (mRow < 0) return;
            if (mCol == 2){
                string s = dgv.Rows[mRow].Cells[1].Value.ToString();
                UTIL_.OPEN_KEYPAD_GRID(s, ref dgv, false);
                DATA_.IsDOUBLE[mRow] = double.Parse(dgv.Rows[mRow].Cells[2].Value.ToString());
            }
        }

        void VIEW_STRING() { for (short i = 0; i < CNT_.Memory; i++) gridString.Rows[i].Cells[2].Value = DATA_.IsSTRING[i]; }

        void VIEW_INTERLOCK() { for (short i = 0; i < CNT_.INTK; i++) gridInterlock.Rows[i].Cells[2].Value = DATA_.bINTRK[i].ToString(); }
        public void INTERLOCK_(object sender){
            dgv = (DataGridView)sender;
            GET_COLROW(dgv);
            if (mRow < 0) return;
            if (mCol == 2){
                //int num = dgv.CurrentRow.Index;
                //DATA_.bINTRK[num] = !DATA_.bINTRK[num];
            }
        }

        private void BTN_MOTOR_TEACHING_Click(object sender, EventArgs e)
        {
            SUBFRM_.gMTSelect.Width = 450;
            SUBFRM_.gMTSelect.Height = 530;
            SUBFRM_.gMTSelect.Show();
            SUBFRM_.gMTSelect.Left = 0;
            SUBFRM_.gMTSelect.BringToFront();
            SUBFRM_.gMTSelect.tmrMTSELECT.Enabled = true;
            DATA_.mCurTeachMotor = -1;
        }

        private void TimerVender_Tick(object sender, EventArgs e){
            if (DATA_.eLoginLevel != eLogLevel.SOFT){
                Hide();
                tmrVender.Enabled = false;
                return;
            }

            if (tabMain.SelectedIndex == 0){
                if (tabMemory.SelectedIndex == 0) VIEW_BOOL();
                else if (tabMemory.SelectedIndex == 1) VIEW_LONG();
                else if (tabMemory.SelectedIndex == 2) VIEW_FLOAT();
                else if (tabMemory.SelectedIndex == 3) VIEW_STRING();
                else if (tabMemory.SelectedIndex == 4) VIEW_INTERLOCK();
            }
            else if (tabMain.SelectedIndex == 1) VIEW_THREAD();
            else if (tabMain.SelectedIndex == 2) VIEW_MTHOME();
        }

        private void ChkSawInterface_Click(object sender, EventArgs e) { DATA_.bNotSAW = !DATA_.bNotSAW; }
        private void ChkPLCInterface_Click(object sender, EventArgs e) { DATA_.bNotPLC = !DATA_.bNotPLC; }

        private void VENDER_CLICK(object sender, EventArgs e){
            ctlsender = sender as Button;

            if (ctlsender.Name == "swIoMon") SUBFRM_.gMonIO.INI_();
            if (ctlsender.Name == "swMotorMon") SUBFRM_.gMonMT.INI_();
            if (ctlsender.Name == "swIO_CHECK") SUBFRM_.gIOCheck.INI_();
            if (ctlsender.Name == "btMTSOFT") SUBFRM_.gSoftLimit.INI_();
        }
    }
}