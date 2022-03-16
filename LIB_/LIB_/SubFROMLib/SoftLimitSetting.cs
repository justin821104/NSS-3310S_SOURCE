using System;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class SoftLimitSetting : Form{
        DataGridView dgv = null;
        int mCol, mRow = 0;
        string sMassege = string.Empty;

        public SoftLimitSetting(){
            InitializeComponent();

            swClose.Click += (sender, e) => { CLOSE(); };
            swSAVE.Click += (sender, e) => { SAVE(); };
        }

        void GET_COLROW(DataGridView g){
            mCol = g.CurrentCell.ColumnIndex;
            mRow = g.CurrentRow.Index;
        }

        public void REFRESH_GRID(){
            try{
                dgvMTSOFTSET.Rows.Clear();
                dgvMTSOFTSET.RowCount = CNT_.MT;
                dgvMTSOFTSET.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvMTSOFTSET.AllowUserToResizeColumns = false;
                dgvMTSOFTSET.RowTemplate.Height = dgvMTSOFTSET.RowTemplate.Height;// 32;
                for (short i = 0; i < CNT_.MT; i++){
                    dgvMTSOFTSET.Rows[i].Cells[0].Value = i;
                    dgvMTSOFTSET.Rows[i].Cells[1].Value = DATA_.MtName[i];
                    dgvMTSOFTSET.Rows[i].Cells[2].Value = DATA_.mtSoftData[i].CcwSoftLimit;
                    dgvMTSOFTSET.Rows[i].Cells[3].Value = DATA_.mtSoftData[i].CwSoftLimit;
                    dgvMTSOFTSET.Rows[i].Cells[4].Value = DATA_.mtSoftData[i].MaxPitch;
                    dgvMTSOFTSET.Rows[i].Cells[5].Value = DATA_.mtSoftData[i].MinSpd;
                    dgvMTSOFTSET.Rows[i].Cells[6].Value = DATA_.mtSoftData[i].MaxSpd;
                    dgvMTSOFTSET.Rows[i].Cells[7].Value = DATA_.mtSoftData[i].MinAcc;
                    dgvMTSOFTSET.Rows[i].Cells[8].Value = DATA_.mtSoftData[i].MaxAcc;
                    dgvMTSOFTSET.Rows[i].Cells[9].Value = DATA_.mtSoftData[i].MinDec;
                    dgvMTSOFTSET.Rows[i].Cells[10].Value = DATA_.mtSoftData[i].MaxDec;
                    dgvMTSOFTSET.Rows[i].Cells[11].Value = DATA_.mtSoftData[i].JOG_LOW_SPD;
                    dgvMTSOFTSET.Rows[i].Cells[12].Value = DATA_.mtSoftData[i].JOG_MIDDLE_SPD;
                    dgvMTSOFTSET.Rows[i].Cells[13].Value = DATA_.mtSoftData[i].JOG_HIGH_SPD;
                }
                dgvMTSOFTSET.ClearSelection();
            }
            catch (Exception exp) { LogWR_.SaveLogException("FRMSoftLimitSetting -> REFRESH_GRID", exp); }
        }

        public void GRID_DATA(){
            for (short i = 0; i < CNT_.MT; i++){
                DATA_.mtSoftData[i].CcwSoftLimit = Convert.ToDouble(dgvMTSOFTSET.Rows[i].Cells[2].Value);
                DATA_.mtSoftData[i].CwSoftLimit = Convert.ToDouble(dgvMTSOFTSET.Rows[i].Cells[3].Value);
                DATA_.mtSoftData[i].MaxPitch = Convert.ToDouble(dgvMTSOFTSET.Rows[i].Cells[4].Value);
                DATA_.mtSoftData[i].MinSpd = Convert.ToDouble(dgvMTSOFTSET.Rows[i].Cells[5].Value);
                DATA_.mtSoftData[i].MaxSpd = Convert.ToDouble(dgvMTSOFTSET.Rows[i].Cells[6].Value);
                DATA_.mtSoftData[i].MinAcc = Convert.ToDouble(dgvMTSOFTSET.Rows[i].Cells[7].Value);
                DATA_.mtSoftData[i].MaxAcc = Convert.ToDouble(dgvMTSOFTSET.Rows[i].Cells[8].Value);
                DATA_.mtSoftData[i].MinDec = Convert.ToDouble(dgvMTSOFTSET.Rows[i].Cells[9].Value);
                DATA_.mtSoftData[i].MaxDec = Convert.ToDouble(dgvMTSOFTSET.Rows[i].Cells[10].Value);
                DATA_.mtSoftData[i].JOG_LOW_SPD = Convert.ToDouble(dgvMTSOFTSET.Rows[i].Cells[11].Value);
                DATA_.mtSoftData[i].JOG_MIDDLE_SPD = Convert.ToDouble(dgvMTSOFTSET.Rows[i].Cells[12].Value);
                DATA_.mtSoftData[i].JOG_HIGH_SPD = Convert.ToDouble(dgvMTSOFTSET.Rows[i].Cells[13].Value);
            }
        }

        public void INI_(){
            REFRESH_GRID();
            Show();
            tmrSoftLimitSetting.Enabled = true;
        }

        public void CLOSE(){
            this.Hide();
            tmrSoftLimitSetting.Enabled = false;
        }

        public void SAVE(){
            GRID_DATA();
            REFRESH_GRID();
            for (int i = 0; i < CNT_.MT; i++) TEACH_.WR_SoftLimitData(i);
        }

        private void TimerSoftLimitSetting_Tick(object sender, EventArgs e){
            for (int i = 0; i < CNT_.MT; i++){
                dgvMTSOFTSET.Rows[i].Cells[14].Value = DATA_.mtSTS[i].CurrentPosition;
            }
        }

        private void MTSOFTSET_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            GET_COLROW(dgv);
            if (mCol <= 1 || mRow < 0){
                UTIL_.CLEAR_GRID_SELECTED(ref dgv);
                return;
            }
            sMassege = DATA_.MtName[e.RowIndex];
            if (e.ColumnIndex == 2) sMassege = sMassege + "CCW SOFT LIMIT";
            else if (e.ColumnIndex == 3) sMassege = sMassege + "CW SOFT LIMIT";
            else if (e.ColumnIndex == 4) sMassege = sMassege + "MAX PITCH";
            else if (e.ColumnIndex == 5) sMassege = sMassege + "MIN SPEED";
            else if (e.ColumnIndex == 6) sMassege = sMassege + "MAX SPEED";
            else if (e.ColumnIndex == 7) sMassege = sMassege + "MIN ACC";
            else if (e.ColumnIndex == 8) sMassege = sMassege + "MAX ACC";
            else if (e.ColumnIndex == 9) sMassege = sMassege + "MIN DEC";
            else if (e.ColumnIndex == 10) sMassege = sMassege + "MAX DEC";
            else if (e.ColumnIndex == 11) sMassege = sMassege + "JOG LOW SPEED";
            else if (e.ColumnIndex == 12) sMassege = sMassege + "JOG MIDDLE SPEED";
            else if (e.ColumnIndex == 13) sMassege = sMassege + "JOG HIGH SPEED";

            UTIL_.OPEN_KEYPAD_GRID(sMassege, ref dgv, true);
        }
    }
}