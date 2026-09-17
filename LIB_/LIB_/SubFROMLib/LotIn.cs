using System;
using System.Windows.Forms;
using Object;
using LIB_.DateType;
using NSS_3310S;

namespace LIB_.SubFROMLib{
    public partial class LotIn : Form {
        public bool bLotInView = false;
        public string OldLotInfo = "";
        public string OldITSInfo = "";
        public bool bLotValidationWait          = false;
        public bool bNotMesLotValidationWait    = false;
        public int nLotValidationWaitCnt = 0;
        public LotIn() {
            InitializeComponent();
        }

        public void MESMessage(string sMES){
            try{
                if (InvokeRequired){
                    this.Invoke((MethodInvoker)delegate (){
                        MESMessage(sMES);
                    });
                }
                else{
                    lblTerminalMsg.Text = sMES;
                }
            }
            catch (Exception EX){
                System.Diagnostics.Trace.WriteLine(EX.Message);
                //LogWR_.SaveLogException("[GEM] ADD MESSAGE FAIL!" + ETC.NewLine + sLOG, EX);
            }
        }

        public void INI_() {
            lblTerminalMsg.Text         = "";
            txtLotId.Text               = "";
            txtStripCounter.Text        = "";
            LBL_LossStartTime.Text      = "";
            LBL_LossEndTime.Text        = "";
            LOT_INFO.Visible            = false;
            CLOT.bLotValidationSusses   = false;
            bLotValidationWait          = false;
            nLotValidationWaitCnt       = 0;
            timer1.Enabled              = true;
            bLotInView                  = true;
            this.Show();
        }

        private void btnRegLotNo_Click(object sender, EventArgs e) {
            if (MessageBox.Show("LOT REQUEST 진행 하시겠습니까 ?", "Save", MessageBoxButtons.YesNo) == DialogResult.No) return;

            LogWR_.SaveProgramCheck("LOT VALIDATION CLICK", "");
            if (txtLotId.Text == "") {
                LogWR_.SaveProgramCheck("LOT VALIDATION - LOT ID 미등록", "");
                MessageBox.Show("LOT ID 등록 하셔야 합니다.");
                return;
            }
            if (txtITSId.Text == ""){
                LogWR_.SaveProgramCheck("LOT VALIDATION - ITS ID 미등록", "");
                MessageBox.Show("ITS ID 등록 하셔야 합니다.");
                return;
            }
            if (DATA_.prMACHINE[CP.ITSLength] > 0){
                int chkCnt = txtITSId.Text.Length;
                if (DATA_.prMACHINE[CP.ITSLength] != chkCnt){
                    LogWR_.SaveProgramCheck("LOT VALIDATION - ITS ID 자리수 확인", "");
                    MessageBox.Show("ITS ID 자리수 맞지 않습니다." + ETC.NewLine + "ITS ID 자리수 " + DATA_.prMACHINE[CP.ITSLength].ToString() + " 입니다.");
                    return;
                }
            }
            if (txtStripCounter.Text == ""){
                LogWR_.SaveProgramCheck("LOT VALIDATION - LOT STRIP 투입 수량 미입력", "");
                MessageBox.Show("LOT STRIP 투입 수량 입력 하셔야 합니다!");
                //txtStripCounter.Text = "0";
                return;
            }
            if (CUSER.Current.ID == ""){
                LogWR_.SaveProgramCheck("LOT VALIDATION - USE ID  미입력", "");
                MessageBox.Show("USE ID 먼저 확인 후 진행 하셔야 합니다!");
                return;
            }
            if (RBT_LOT_TYPE_1.Checked) CLOT.nLotType = 1;
            else if (RBT_LOT_TYPE_2.Checked) CLOT.nLotType = 2;
            else if (RBT_LOT_TYPE_3.Checked) CLOT.nLotType = 3;
            else if (RBT_LOT_TYPE_4.Checked) CLOT.nLotType = 4;
            else if (RBT_LOT_TYPE_5.Checked) CLOT.nLotType = 5;
            else{
                LogWR_.SaveProgramCheck("LOT VALIDATION - LOT 종류 미선택", "");
                MessageBox.Show("LOT 종류 선택 하셔야 합니다 !");
                return;
            }

            //LOT ID와 ITS ID 비교
            string sLotID = txtLotId.Text;
            string sITSID = txtITSId.Text;
            string[] sList = sITSID.Split(' ');
            //if (sLotID != sList[0]){
            //    MessageBox.Show("현재 등록 LOT ID의 ITS ID가 아닙니다 !");
            //    return;
            //} //LOT ID하고 ITS ID가 고객사 ID면 서로 달라 삭제 (211104 HK)
            try {
                CLOT.CurLotStripCnt = int.Parse(txtStripCounter.Text);
            }
            catch (Exception exc) {
                MessageBox.Show("STRIP 수량 입력을 잘못 하셨습니다 !" + '\n' + "다시 입력 하십시오." + '\n' + exc.ToString());
                LogWR_.SaveLogException("STRIP COUNT INPUT FAIL", exc);
                return;
            }
            try {
                LogWR_.SaveProgramCheck("LOT VALIDATION START", "");
                LOT_INFO.Visible = true;

                FILE_.WR_File(PATH_.LOT_ID, txtLotId.Text, false);
                UTIL_.WRITE_LOT_ID_INI(txtLotId.Text);

                FILE_.WR_File(PATH_.ITS_ID, txtITSId.Text, false);
                FILE_.WR_File(PATH_.LOT_STRIP_COUNT, txtStripCounter.Text, false);
                OldLotInfo = CLOT.GET_LOT.LotID;
                OldITSInfo = CLOT.GET_LOT.ItsID;
                CLOT.CurLotID = txtLotId.Text;
                CLOT.GET_LOT.LotID = txtLotId.Text;
                CLOT.GET_LOT.ItsID = txtITSId.Text;
                CLOT.GET_LOT.LoadingCount = CLOT.CurLotStripCnt;

                if (DATA_.prMACHINE[CP.UseLotEnd] == (int)eUSE.NotUSE) {
                    DATA_.bWriteLotInfo = true; // LOT 수량 정보 리셋 !
                }
                string sLogMessage = OldLotInfo + " -> " + CLOT.GET_LOT.LotID;
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                sLogMessage = OldITSInfo + " -> " + CLOT.GET_LOT.ItsID;
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);


                if (DATA_.prMACHINE[CP.UseMES] == (int)eUSE.NotUSE) {
                    LogWR_.SaveProgramCheck("LOT VALIDATION NOT USE", "");
                    LogWR_.SaveBarcodeHistory("LOT-VALIDATION", "");

                    DATA_.bStripDefect = true; // ITS 정보 읽기 !
                    LOT_INFO.Visible = false;
                    bLotInView = false;
                    CLOT.ClearStripBarcodeInfo();
                    LogWR_.SaveProgramCheck("LOT VALIDATION - LOT ID WRITE", "");
                ReWirting:
                    // lot info 정보
                    bool Wlot = LogWR_.WriteLotInfo(CLOT.GET_LOT.LotID);
                    if (!Wlot) {
                        UTIL_.DELAY(1000);
                        goto ReWirting;
                    }
                    LogWR_.SaveProgramCheck("LOT VALIDATION - LOT ID WRITE END", "");
                    bNotMesLotValidationWait = true;
                } //mes 미사용
                else {
                    LogWR_.SaveProgramCheck("LOT VALIDATION USE", "");
                    TEACH_.Wirte_Para(CP.UseMsSQL, "MC", (int)eUSE.USE);
                    TEACH_.Wirte_Para(CP.UseITSData, "MC", (int)eUSE.USE);
                    LogWR_.SaveProgramCheck("LOT VALIDATION - LOT ID WRITE", "");
                ReWirting:
                    // lot info 정보
                    bool Wlot = LogWR_.WriteLotInfo(CLOT.GET_LOT.LotID);
                    if (!Wlot) {
                        UTIL_.DELAY(1000);
                        goto ReWirting;
                    }
                    LogWR_.SaveProgramCheck("LOT VALIDATION - LOT ID WRITE END", "");
                    // LOT초기진행 시 KIT 클린 진행 플로그 25.0623
                    DATA_.IsBIT[B.LotStart_KitCleanning] = true;
                    DATA_.IsBIT[B.KitCleaning] = true;

                    LogWR_.SaveProgramCheck("LOT VALIDATION - SEND EES", "");
                    SUBFRM_.gSecsGem.SetLotRequest(CLOT.CurLotID, CLOT.nLotType, CLOT.CurLotStripCnt);
                    bLotValidationWait = true;
                } //mes 사용

                // lot id 공유  폴더에 기록함. 비전에 확인 비트 추가 26.0514 HK.PARK 
                //lot validation 후 첫 시작 일 경우.
                DATA_.mOUT[O.LotChange] = true;
                UTIL_.DELAY(300);
                DATA_.mOUT[O.LotChange] = false;   
                
                if (bNotMesLotValidationWait) {
                    UTIL_.DELAY(300);
                    bNotMesLotValidationWait = false;
                    this.Hide();
                }
            }
            catch (Exception ex){
                MessageBox.Show("스트립 투입 수량 입력 오류 !" + ETC.NewLine + ex.ToString());
                LogWR_.SaveLogException("LOT VALIDATION FAIL", ex);
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            timer1.Enabled      = false;
            LOT_INFO.Visible    = false;
            bLotInView          = false;
            this.Hide(); 
        } 

        private void timer1_Tick(object sender, EventArgs e){
            if (CLOT.bLotLoss)      GBX_LOT_LOSS.Enabled = true;
            else                    GBX_LOT_LOSS.Enabled = false;

            if (CLOT.bEqpChange)    GBX_EQP_CHANGE.Enabled = true;
            else                    GBX_EQP_CHANGE.Enabled = false;

            btnCanceledLotNo.Enabled = DATA_.prMACHINE[CP.UseMES] == (int)eUSE.USE ? true : false;

            if (DATA_.prMACHINE[CP.UseMES] == (int)eUSE.USE && CLOT.bLotValidationSusses){
                CLOT.bLotValidationSusses   = false;
                DATA_.bStripDefect          = true; // ITS 정보 읽기 !
                LOT_INFO.Visible            = false;
                bLotInView                  = false;
                bLotValidationWait          = false;
                CLOT.bFirstLot              = true;
                CLOT.ClearStripBarcodeInfo();
                LogWR_.SaveProgramCheck("LOT VALIDATION - RECIVE EES", "");

                //CLOT.GET_LOT.ProcCondition_1 
                //CLOT.GET_LOT.ProcCondition_2
                //CLOT.GET_LOT.ProcCondition_3
                this.Hide();
            }
            if (DATA_.prMACHINE[CP.UseMES] == (int)eUSE.USE && CLOT.bLotCanceled){
                CLOT.bLotCanceled = false;
                bLotValidationWait = false;
                LOT_INFO.Visible = false;
                LogWR_.SaveProgramCheck("LOT VALIDATION - EES CANCEL !", "");
            }

            if (bLotValidationWait){
                nLotValidationWaitCnt++;
                if (nLotValidationWaitCnt >= 30){
                    LOT_INFO.Visible = false;
                    bLotValidationWait = false;
                    LogWR_.SaveProgramCheck("LOT VALIDATION - EES RETURN MESSAGE NOT RECIVE [TIME-OVER] !", "");
                    MessageBox.Show("LOT VALIDATION 응답 없습니다 !");
                }
            } //500
            else nLotValidationWaitCnt = 0;
            
            
        }

        private void BTN_LOT_LOSS_Click(object sender, EventArgs e){
            if (!RDB_LOSS_1.Checked && !RDB_LOSS_2.Checked && !RDB_LOSS_3.Checked && !RDB_LOSS_4.Checked && !RDB_LOSS_5.Checked && !RDB_LOSS_6.Checked){
                MessageBox.Show("LOSS CODE 체크 하셔야 합니다 !");
                return;
            }
            LBL_LossStartTime.Text  = CLOT.LotLossStartTime;
            LBL_LossEndTime.Text    = CLOT.LotLossEndTime;

            string sLossCode = "";
            if (RDB_LOSS_1.Checked) sLossCode = RDB_LOSS_1.Text;
            if (RDB_LOSS_2.Checked) sLossCode = RDB_LOSS_2.Text;
            if (RDB_LOSS_3.Checked) sLossCode = RDB_LOSS_3.Text;
            if (RDB_LOSS_4.Checked) sLossCode = RDB_LOSS_4.Text;
            if (RDB_LOSS_5.Checked) sLossCode = RDB_LOSS_5.Text;
            if (RDB_LOSS_6.Checked) sLossCode = RDB_LOSS_6.Text;
            string sLossMemo = TXT_LOSS_MEMO.Text;

            SUBFRM_.gSecsGem.SetLotLoss(sLossCode, sLossMemo);
        }

        private void BTN_EQP_CHANGE_COMPLETE_Click(object sender, EventArgs e){
            if (CBX_EQP_CHANGE_CODE.Text == "" || CBX_EQP_CHANGE_CODE.SelectedIndex < 0){
                MessageBox.Show("변경 원인 선택 하셔야 합니다 !");
                return;
            }
            string sSelect = CBX_EQP_CHANGE_CODE.Text;
            string[] sRslt = sSelect.Split(',');
            int nCODE = int.Parse(sRslt[0]);
            string sEQP_CHANGE_COMMENT = TXT_EQP_CHANGE_COMMENT.Text;
            SUBFRM_.gSecsGem.SetLotEqpChangeComplete(nCODE, sEQP_CHANGE_COMMENT);
        }

        private void btnCanceledLotNo_Click(object sender, EventArgs e){
            if (txtLotId.Text == ""){
                MessageBox.Show("LOT ID 등록 하셔야 합니다.");
                return;
            }
            if (RBT_LOT_TYPE_1.Checked) CLOT.nLotType = 1;
            else if (RBT_LOT_TYPE_2.Checked) CLOT.nLotType = 2;
            else if (RBT_LOT_TYPE_3.Checked) CLOT.nLotType = 3;
            else if (RBT_LOT_TYPE_4.Checked) CLOT.nLotType = 4;
            else if (RBT_LOT_TYPE_5.Checked) CLOT.nLotType = 5;
            else{
                MessageBox.Show("LOT 종류 선택 하셔야 합니다 !");
                return;
            }
            CLOT.CurLotID = txtLotId.Text;
            SUBFRM_.gSecsGem.SetLotCanceled(CLOT.CurLotID, CLOT.nLotType);
        }
    }
}