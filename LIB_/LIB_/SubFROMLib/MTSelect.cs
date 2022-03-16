using Object;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class MTSelect : Form{
        public int[] MT_LIST;
        bool bCHANGE = false;
        double dSPD = 0, dPITCH = 0;
        string sTITLE = "";
        public dxy dRefPos = new dxy();
        public double dRefCurPos = 0;
        Button BTN;
        Button gBTN;
        int nMT = 0;
        double RefGap = 0;

        public MTSelect(){
            InitializeComponent();

            btnClose.Click += (sender, e) => { CLOSE_(); };
        }

        private void MTSelect_Load(object sender, EventArgs e){
            Width = 450;
            Height = 530;
            btnGROUP_0.Text = DATA_.MT_GROUP_0_NAME;
            btnGROUP_1.Text = DATA_.MT_GROUP_1_NAME;
            btnGROUP_2.Text = DATA_.MT_GROUP_2_NAME;
            btnGROUP_3.Text = DATA_.MT_GROUP_3_NAME;
            btnGROUP_4.Text = DATA_.MT_GROUP_4_NAME;

            //btnJogCCW.Image = ArrowImageList
            //btnJogCCW.Image = ArrowImageList.Images[0];
        }

        void CLOSE_(){
            tmrMTSELECT.Enabled = false;
            SUBFRM_.gMTPosTeching.Hide();
            SUBFRM_.gMTPosTechingData.Hide();
            Hide();
        }

        void RESET_SUB_SCREEN(){
            for (int nCtrlCnt = 0; nCtrlCnt < 10; nCtrlCnt++){
                gBTN = Controls.Find("btnGROUP_" + nCtrlCnt.ToString(), true).FirstOrDefault() as Button;
                if (gBTN != null){
                    gBTN.BackColor = Color.DarkGray;
                    gBTN.ForeColor = Color.Black;
                }
            }
        }
        void SUB_SCREEN_CHAGNE(int nNUM){
            RESET_SUB_SCREEN();
            gBTN = Controls.Find("btnGROUP_" + nNUM.ToString(), true).FirstOrDefault() as Button;
            if (gBTN != null){
                gBTN.BackColor = Color.Green;
                gBTN.ForeColor = Color.White;
            }
        }

        void GET_MT_LIST(int index){
            if (index == 0) DATA_.cMATH.Resize_n_Copy(DATA_.MT_GROUP_0, ref MT_LIST);
            if (index == 1) DATA_.cMATH.Resize_n_Copy(DATA_.MT_GROUP_1, ref MT_LIST);
            if (index == 2) DATA_.cMATH.Resize_n_Copy(DATA_.MT_GROUP_2, ref MT_LIST);
            if (index == 3) DATA_.cMATH.Resize_n_Copy(DATA_.MT_GROUP_3, ref MT_LIST);
            if (index == 4) DATA_.cMATH.Resize_n_Copy(DATA_.MT_GROUP_4, ref MT_LIST);
        }

        void SET_MT_LIST(){
            gridSelect.RowCount = MT_LIST.Length;
            for (int i = 0; i < MT_LIST.Length; i++){
                gridSelect.Rows[i].Height = 55;
                nMT = MT_LIST[i];
                gridSelect.Rows[i].Cells[0].Value = (nMT + 1).ToString() + "." + DATA_.MtName[nMT];
                gridSelect.Rows[i].Cells[0].Style.BackColor = Color.White;
            }
            UTIL_.CLEAR_GRID_SELECTED(ref gridSelect);
        }

        private void MT_GROUP_CLICK(object sender, EventArgs e){
            BTN = sender as Button;
            SUB_SCREEN_CHAGNE(Convert.ToInt16(BTN.Tag));
            GET_MT_LIST(int.Parse(BTN.Tag.ToString()));
            SET_MT_LIST();
            DATA_.mCurTeachMotor = -1;
            bCHANGE = true;
        }

        private void BTN_JOG_UP_MOUSE_UP(object sender, MouseEventArgs e){
            if (DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.INITIAL || DATA_.bMF) return;
            if (CHK_USE_INC.Checked) return;

            nMT = DATA_.mCurTeachMotor;
            LAB_.MTSSTOP(nMT, "fMTSelect -> BTN_JOG_UP_MOUSE_UP");
        }

        bool CHK_RETURN(ref double dSPD, ref double dPITCH){
            try{
                dSPD = double.Parse(cbJogSpd.Text);
                dPITCH = double.Parse(txtIncPitch.Text);
                if (dPITCH <= 0 || dSPD <= 0) return false;
                return true;
            }
            catch (Exception ex){
                MessageBox.Show("Only number are allowed !", ex.ToString());
                return false;
            }
        }
        private void BTN_JOG_CCW_MOUSE_DOWN(object sender, MouseEventArgs e){
            if (DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.INITIAL || !DATA_.mtSTS[DATA_.mCurTeachMotor].bSvOn) return;
            if (!CHK_RETURN(ref dSPD, ref dPITCH)) return;

            if (CHK_USE_INC.Checked) { LAB_.MT_PITCH_CCW(DATA_.mCurTeachMotor, dSPD, dPITCH); }
            else { LAB_.MT_JOG_CCW(DATA_.mCurTeachMotor, dSPD); }
        }

        private void BTN_JOG_CW_MOUSE_DOWN(object sender, MouseEventArgs e){
            if (DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.INITIAL || !DATA_.mtSTS[DATA_.mCurTeachMotor].bSvOn) return;
            if (!CHK_RETURN(ref dSPD, ref dPITCH)) return;

            if (CHK_USE_INC.Checked) { LAB_.MT_PITCH_CW(DATA_.mCurTeachMotor, dSPD, dPITCH); }
            else { LAB_.MT_JOG_CW(DATA_.mCurTeachMotor, dSPD); }
        }

        void DATA_CLEAN(){
            gbxMT.Enabled = false;
            SUBFRM_.gMTPosTeching.Hide();
            SUBFRM_.gMTPosTechingData.Hide();
            SUBFRM_.gMTPosTechingData.tmrMTPositionTeachingData.Enabled = false;

            txtCmd.Text = "-";
            txtEnc.Text = "-";
            txtGap.Text = "-";
            lblLimitN.BackColor = Color.Maroon;
            lblLimitP.BackColor = Color.Maroon;
            lblAlarm.BackColor = Color.Maroon;
            lblOrg.BackColor = Color.DarkGreen;
            lblBusy.BackColor = Color.DarkGreen;

            btnSvOn.BackColor = Color.White;
            btnAlarmClear.BackColor = Color.White;
            btnHome.BackColor = Color.White;
            btnStop.BackColor = Color.White;

            if (gbxMT.Text != "SELECT MOTOR NAME") { gbxMT.Text = "SELECT MOTOR NAME"; }
        }

        private void Select_CellClick(object sender, DataGridViewCellEventArgs e){
            string[] sMT = gridSelect.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Split('.');
            DATA_.mCurTeachMotor = int.Parse(sMT[0]) - 1;
            bCHANGE = true;
        }

        private void Select_CellDoubleClick(object sender, DataGridViewCellEventArgs e){
            string[] sMT = gridSelect.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Split('.');
            DATA_.mCurTeachMotor = int.Parse(sMT[0]) - 1;
            for (int i = 0; i < gridSelect.RowCount; i++) { gridSelect.Rows[i].Cells[0].Style.BackColor = Color.White; }
            //gridSelect.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Blue;
            SUBFRM_.gMTPosTeching.INI_();
            bCHANGE = true;
        }

        private void SetRef_Click(object sender, EventArgs e){
            if (DATA_.mCurTeachMotor < 0) return;
            dRefCurPos = LAB_.GET_ACTPOS(DATA_.mCurTeachMotor);
        }

        private void MT_CLICK(object sender, EventArgs e){
            BTN = (Button)sender;
            if (DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.INITIAL || DATA_.mCurTeachMotor < 0) return;
            if (BTN.Name == "btnSvOn"){
                bool bZERO = false;
                if (DATA_.mtSTS[DATA_.mCurTeachMotor].bSvOn){
                    if (DATA_.mtHD != null){
                        for (int i = 0; i < DATA_.mtHD.Length; i++){
                            if (DATA_.mtHD[i] == DATA_.mCurTeachMotor){
                                if (DATA_.mtHD[i] == DATA_.mCurTeachMotor){
                                    for (int j = 0; j < CNT_.PKR; j++){
                                        if (i == 0){
                                            bZERO = ((-1 < DATA_.mtSTS[DATA_.mtHD1_PK[i]].CurrentPosition) && (DATA_.mtSTS[DATA_.mtHD1_PK[i]].CurrentPosition) < 1) ? true : false;
                                            if (!DATA_.mtSTS[DATA_.mtHD1_PK[i]].bHomeComplete || (!DATA_.mtDATA[DATA_.mtHD1_PK[i], 0].bPOS && !bZERO)){
                                                //MessageBox.Show("HEAD X1 축 서보 OFF!" + ETC.NewLine + "PICKER 대기 위치 아님");
                                            }
                                        } //head1
                                        else{
                                            bZERO = ((-1 < DATA_.mtSTS[DATA_.mtHD2_PK[i]].CurrentPosition) && (DATA_.mtSTS[DATA_.mtHD2_PK[i]].CurrentPosition) < 1) ? true : false;
                                            if (!DATA_.mtSTS[DATA_.mtHD2_PK[i]].bHomeComplete || (!DATA_.mtDATA[DATA_.mtHD2_PK[i], 0].bPOS && !bZERO)){
                                                //MessageBox.Show("HEAD X2 축 서보 OFF!" + ETC.NewLine + "PICKER 대기 위치 아님");
                                            }
                                        } //head2
                                    }
                                }
                            }
                        }
                    }
                    LAB_.SVOFF(DATA_.mCurTeachMotor);
                }
                else LAB_.SVON(DATA_.mCurTeachMotor);
            }
            if (BTN.Name == "btnAlarmClear"){
                if (DATA_.mtSTS[DATA_.mCurTeachMotor].bAlram){
                    DATA_.mtCMD[DATA_.mCurTeachMotor].CMDReset = true;
                    LAB_.ALARMRESET(DATA_.mCurTeachMotor);
                }
            }
            if (BTN.Name == "btnHome"){
                if (!UTIL_.PRINT_MASSAGE("이 동작은 (HOME) 인터락이 걸려 있지 않습니다. 진행 하시겠습니까 ?" + ETC.CrLf + "This behavior is dongerous. Excute ?", false, false, false)) return;
                switch (DATA_.mCurTeachMotor){
                    default:
                        bool bZERO = false;
                        if (DATA_.mtHD != null){
                            for (int i = 0; i < DATA_.mtHD.Length; i++){
                                if (DATA_.mtHD[i] == DATA_.mCurTeachMotor){
                                    for (int j = 0; j < CNT_.PKR; j++){
                                        if (i == 0){
                                            bZERO = ((-1 < DATA_.mtSTS[DATA_.mtHD1_PK[i]].CurrentPosition) && (DATA_.mtSTS[DATA_.mtHD1_PK[i]].CurrentPosition) < 1) ? true : false;
                                            if (!DATA_.mtSTS[DATA_.mtHD1_PK[i]].bHomeComplete || (!DATA_.mtDATA[DATA_.mtHD1_PK[i], 0].bPOS && !bZERO)){
                                                MessageBox.Show("HEAD X1 축 홈 동작 할 수 없습니다." + ETC.NewLine + "PICKER 대기 위치 상태인지 확인 후 홈 동작 하셔야 합니다.");
                                                return;
                                            }
                                        } //head1
                                        else{
                                            bZERO = ((-1 < DATA_.mtSTS[DATA_.mtHD2_PK[i]].CurrentPosition) && (DATA_.mtSTS[DATA_.mtHD2_PK[i]].CurrentPosition) < 1) ? true : false;
                                            if (!DATA_.mtSTS[DATA_.mtHD2_PK[i]].bHomeComplete || (!DATA_.mtDATA[DATA_.mtHD2_PK[i], 0].bPOS && !bZERO)){
                                                MessageBox.Show("HEAD X2 축 홈 동작 할 수 없습니다." + ETC.NewLine + "PICKER 대기 위치 상태인지 확인 후 홈 동작 하셔야 합니다.");
                                                return;
                                            }
                                        } //head2
                                    }
                                }
                            }
                        }
                        
                        LAB_.MT_HOME(DATA_.mCurTeachMotor);
                        break;
                }
            }
            if (BTN.Name == "btnStop") LAB_.MTSSTOP(DATA_.mCurTeachMotor, "fMTSelect->swMT_CLICK(STOP)");
        }

        private void TimerMTSELECT_Tick(object sender, EventArgs e){
            if (DATA_.eLoginLevel < eLogLevel.ENG) CLOSE_();
            if (DATA_.mCurTeachMotor < 0){
                DATA_CLEAN();
                return;
            }
            else { gbxMT.Enabled = true; }

            btnSvOn.BackColor       = DATA_.mtSTS[DATA_.mCurTeachMotor].bSvOn == true ? Color.Lime : Color.White;
            btnHome.BackColor       = DATA_.mtSTS[DATA_.mCurTeachMotor].bHomeComplete == true ? Color.YellowGreen : Color.White;
            btnAlarmClear.BackColor = DATA_.mtSTS[DATA_.mCurTeachMotor].bAlram == true ? Color.Red : Color.White;
            lblLimitN.BackColor     = DATA_.mtSTS[DATA_.mCurTeachMotor].bSensorCCW == true ? Color.Red : Color.Maroon;
            lblLimitP.BackColor     = DATA_.mtSTS[DATA_.mCurTeachMotor].bSensorCW == true ? Color.Red : Color.Maroon;
            lblOrg.BackColor        = DATA_.mtSTS[DATA_.mCurTeachMotor].bSensorHome == true ? Color.Lime : Color.DarkGreen;
            lblBusy.BackColor       = DATA_.mtSTS[DATA_.mCurTeachMotor].bBusy == true ? Color.Lime : Color.DarkGreen;
            lblAlarm.BackColor      = DATA_.mtSTS[DATA_.mCurTeachMotor].bAlram == true ? Color.Red : Color.Maroon;
            txtCmd.Text             = string.Format("{0:0.000}", DATA_.mtSTS[DATA_.mCurTeachMotor].CmdPosition);
            txtEnc.Text             = string.Format("{0:0.000}", DATA_.mtSTS[DATA_.mCurTeachMotor].CurrentPosition);
            RefGap                  = Math.Abs(DATA_.mtSTS[DATA_.mCurTeachMotor].CurrentPosition - DATA_.mtSTS[DATA_.mCurTeachMotor].CmdPosition);
            txtGap.Text             = string.Format("{0:0.000}", RefGap);
            txtSpd.Text             = DATA_.mtSTS[DATA_.mCurTeachMotor].dCurSpeed.ToString();
            lbHomeSts.Text          = DATA_.mtSTS[DATA_.mCurTeachMotor].strHome;
            sTITLE                  = DATA_.MtName[DATA_.mCurTeachMotor];

            lblRefPos.Text          = string.Format("{0:0.###}", DATA_.mtSTS[DATA_.mCurTeachMotor].CurrentPosition - dRefCurPos);
            lblRefHalfPos.Text      = string.Format("{0:0.###}", (DATA_.mtSTS[DATA_.mCurTeachMotor].CurrentPosition - dRefCurPos) / 2);

            if (bCHANGE){
                bCHANGE = false;
                gbxMT.Text = sTITLE;
            }
        }
    }
}