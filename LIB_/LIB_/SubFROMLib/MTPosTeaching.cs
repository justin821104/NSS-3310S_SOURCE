using Object;
using System;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class MTPosTeaching : Form{
        DataGridView dgv = null;
        int mCol, mRow = 0;
        public int dTOP = 0, dLEFT = 0;
        int iStendard = 0;
        int nMT = 0;
        double dOFFSET_PITCH = 0;

        public MTPosTeaching(){
            InitializeComponent();

            #region "CONTROL EVENT"
            swInfo.Click            += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO) panOffset.Visible = !panOffset.Visible; };
            swCopyPos1ToAll.Click   += (sender, e) => { COPY_PosNum1Data(); };
            btnClose.Click          += (sender, e) => { CLOSE(); };
            swSaveOffset.Click      += (sender, e) => { SAVE_OFFSET(); };
            #endregion "CONTROL EVENT"
        }

        public void GET_ColRow(DataGridView g){
            mCol = g.CurrentCell.ColumnIndex;
            mRow = g.CurrentRow.Index;
        }

        public void CLOSE(){
            tmrMonMotion.Enabled = false;
            panOffset.Visible = false;
            SUBFRM_.gMTPosTechingData.Visible = false;
            Hide();
        }

        private void MTPosTeaching_Load(object sender, EventArgs e) { LD_FRM(); }

        void LD_FRM(){
            gridComTeaching.AllowUserToResizeColumns = false;
            gridComTeaching.RowTemplate.Height = 30;
            gridComTeaching.Rows.Clear();
            gridComTeaching.RowCount = CNT_.ComPos;
            for (int i = 0; i < CNT_.ComPos; i++) gridComTeaching.Rows[i].Cells[0].Value = i.ToString();

            gridIndTeaching.AllowUserToResizeColumns = false;
            gridIndTeaching.RowTemplate.Height = 30;
            gridIndTeaching.Rows.Clear();
            gridIndTeaching.RowCount = CNT_.IndPos;
            for (int i = 0; i < CNT_.IndPos; i++) gridIndTeaching.Rows[i].Cells[0].Value = (CNT_.ComPos + i).ToString();

            panOffset.Visible = false;
        }
        public void INI_(){
            panOffset.Visible = false;

            Show();
            BringToFront();
            tmrMonMotion.Enabled = true;
        }

        public void WR_TEACHINGVALUE(){
            for (int i = 0; i < CNT_.ComPos; i++){
                DATA_.mtOLD = DATA_.mtDATA[DATA_.mCurTeachMotor, i];

                DATA_.mtDATA[DATA_.mCurTeachMotor, i].Pos = double.Parse(gridComTeaching.Rows[i].Cells[2].Value.ToString());
                DATA_.mtDATA[DATA_.mCurTeachMotor, i].Spd = double.Parse(gridComTeaching.Rows[i].Cells[3].Value.ToString());
                DATA_.mtDATA[DATA_.mCurTeachMotor, i].Acc = double.Parse(gridComTeaching.Rows[i].Cells[4].Value.ToString());
                DATA_.mtDATA[DATA_.mCurTeachMotor, i].Dec = double.Parse(gridComTeaching.Rows[i].Cells[5].Value.ToString());
                DATA_.mtDATA[DATA_.mCurTeachMotor, i].MoveTime = int.Parse(gridComTeaching.Rows[i].Cells[6].Value.ToString());
                DATA_.mtDATA[DATA_.mCurTeachMotor, i].Delay = int.Parse(gridComTeaching.Rows[i].Cells[7].Value.ToString());
                DATA_.mtSAVE = DATA_.mtDATA[DATA_.mCurTeachMotor, i];
                TEACH_.WR_MTDATA(DATA_.mCurTeachMotor, i);
            }

            for (int i = 0; i < CNT_.IndPos; i++){
                DATA_.mtOLD = DATA_.mtDATA[DATA_.mCurTeachMotor, CNT_.ComPos + i];

                DATA_.mtDATA[DATA_.mCurTeachMotor, CNT_.ComPos + i].Pos = double.Parse(gridIndTeaching.Rows[i].Cells[2].Value.ToString());
                DATA_.mtDATA[DATA_.mCurTeachMotor, CNT_.ComPos + i].Spd = double.Parse(gridIndTeaching.Rows[i].Cells[3].Value.ToString());
                DATA_.mtDATA[DATA_.mCurTeachMotor, CNT_.ComPos + i].Acc = double.Parse(gridIndTeaching.Rows[i].Cells[4].Value.ToString());
                DATA_.mtDATA[DATA_.mCurTeachMotor, CNT_.ComPos + i].Dec = double.Parse(gridIndTeaching.Rows[i].Cells[5].Value.ToString());
                DATA_.mtDATA[DATA_.mCurTeachMotor, CNT_.ComPos + i].MoveTime = int.Parse(gridIndTeaching.Rows[i].Cells[6].Value.ToString());
                DATA_.mtDATA[DATA_.mCurTeachMotor, CNT_.ComPos + i].Delay = int.Parse(gridIndTeaching.Rows[i].Cells[7].Value.ToString());
                DATA_.mtSAVE = DATA_.mtDATA[DATA_.mCurTeachMotor, i];
                TEACH_.WR_MTDATA(DATA_.mCurTeachMotor, CNT_.ComPos + i);
            }
        }

        private void Teaching_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            GET_ColRow(dgv);
            if (mCol < 0 || mRow < 0) return;
            iStendard = 0;
            if (dgv.Name == "gridIndTeaching") iStendard = CNT_.ComPos;

            SUBFRM_.gMTPosTechingData.numAxis = DATA_.mCurTeachMotor;
            SUBFRM_.gMTPosTechingData.numPos = mRow + iStendard;
            SUBFRM_.gMTPosTechingData.bFIRST = false;
            SUBFRM_.gMTPosTechingData.INI_();
        }

        private void TimerMonMotion_Tick(object sender, EventArgs e){
            if (DATA_.mCurTeachMotor < 0) return;
            if (Text == DATA_.MtName[DATA_.mCurTeachMotor] && !DATA_.mTeachChanged) return;
            DATA_.mTeachChanged = false;

            nMT = DATA_.mCurTeachMotor;
            Text = DATA_.MtName[nMT];
            for (int i = 0; i < CNT_.ComPos; i++){
                gridComTeaching.Rows[i].Cells[1].Value = DATA_.PosName[nMT, i];
                gridComTeaching.Rows[i].Cells[2].Value = string.Format("{0:0.000}", DATA_.mtDATA[nMT, i].Pos);
                gridComTeaching.Rows[i].Cells[3].Value = string.Format("{0:0.000}", DATA_.mtDATA[nMT, i].Spd);
                gridComTeaching.Rows[i].Cells[4].Value = DATA_.mtDATA[nMT, i].Acc;
                gridComTeaching.Rows[i].Cells[5].Value = DATA_.mtDATA[nMT, i].Dec;
                gridComTeaching.Rows[i].Cells[6].Value = DATA_.mtDATA[nMT, i].MoveTime;
                gridComTeaching.Rows[i].Cells[7].Value = DATA_.mtDATA[nMT, i].Delay;
            }
            for (int i = 0; i < CNT_.IndPos; i++){
                gridIndTeaching.Rows[i].Cells[1].Value = DATA_.PosName[nMT, CNT_.ComPos + i];
                gridIndTeaching.Rows[i].Cells[2].Value = string.Format("{0:0.000}", DATA_.mtDATA[nMT, CNT_.ComPos + i].Pos);
                gridIndTeaching.Rows[i].Cells[3].Value = string.Format("{0:0.000}", DATA_.mtDATA[nMT, CNT_.ComPos + i].Spd);
                gridIndTeaching.Rows[i].Cells[4].Value = DATA_.mtDATA[nMT, CNT_.ComPos + i].Acc;
                gridIndTeaching.Rows[i].Cells[5].Value = DATA_.mtDATA[nMT, CNT_.ComPos + i].Dec;
                gridIndTeaching.Rows[i].Cells[6].Value = DATA_.mtDATA[nMT, CNT_.ComPos + i].MoveTime;
                gridIndTeaching.Rows[i].Cells[7].Value = DATA_.mtDATA[nMT, CNT_.ComPos + i].Delay;
            }
            dTOP = SUBFRM_.gMTSelect.Top < 0 ? 0 : SUBFRM_.gMTSelect.Top + 200;
            dLEFT = SUBFRM_.gMTSelect.Left < 0 ? 0 : SUBFRM_.gMTSelect.Left + 250;
            Top = dTOP;
            Left = dLEFT;
        }

        public void COPY_PosNum1Data(){
            if (!UTIL_.PRINT_MASSAGE("CHANGE ALL PROPERTIES ?" + ETC.NewLine + "1번 위치의 구동 속성을 모든 포지션에 적용 하시겠습니까 ?", false, false, false)) return;

            for (int i = 1; i < gridComTeaching.RowCount; i++){
                gridComTeaching.Rows[i].Cells[3].Value = gridComTeaching.Rows[0].Cells[3].Value;
                gridComTeaching.Rows[i].Cells[4].Value = gridComTeaching.Rows[0].Cells[4].Value;
                gridComTeaching.Rows[i].Cells[5].Value = gridComTeaching.Rows[0].Cells[5].Value;
                gridComTeaching.Rows[i].Cells[6].Value = gridComTeaching.Rows[0].Cells[6].Value;
                //gridComTeaching.Rows[i].Cells[7].Value = gridComTeaching.Rows[0].Cells[7].Value;
            }
            for (int i = 0; i < gridIndTeaching.RowCount; i++){
                gridIndTeaching.Rows[i].Cells[3].Value = gridComTeaching.Rows[0].Cells[3].Value;
                gridIndTeaching.Rows[i].Cells[4].Value = gridComTeaching.Rows[0].Cells[4].Value;
                gridIndTeaching.Rows[i].Cells[5].Value = gridComTeaching.Rows[0].Cells[5].Value;
                gridIndTeaching.Rows[i].Cells[6].Value = gridComTeaching.Rows[0].Cells[6].Value;
                //gridIndTeaching.Rows[i].Cells[7].Value = gridComTeaching.Rows[0].Cells[7].Value;
            }
            WR_TEACHINGVALUE();
        }

        public void SAVE_OFFSET(){
            if (!UTIL_.PRINT_MASSAGE("포지션 전체 옵셋값을 설정 하시겠습니까 ?" + ETC.CrLf + "(APPLY ALL POSITION OFFSET ?)", false, false, false)) return;
            dOFFSET_PITCH = double.Parse(editOffset.Text);
            for (int i = 0; i < CNT_.ComPos; i++){
                DATA_.mtDATA[DATA_.mCurTeachMotor, i].Pos += dOFFSET_PITCH;
                gridComTeaching.Rows[i].Cells[2].Value = DATA_.mtDATA[DATA_.mCurTeachMotor, i].Pos;
            }
            for (int i = 0; i < CNT_.IndPos; i++){
                DATA_.mtDATA[DATA_.mCurTeachMotor, CNT_.ComPos + i].Pos += dOFFSET_PITCH;
                gridIndTeaching.Rows[i].Cells[2].Value = DATA_.mtDATA[DATA_.mCurTeachMotor, CNT_.ComPos + i].Pos;
            }
            WR_TEACHINGVALUE();
        }
    }
}