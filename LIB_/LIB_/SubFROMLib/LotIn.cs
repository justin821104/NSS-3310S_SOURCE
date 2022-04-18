using System;
using System.Windows.Forms;
using Object;
using LIB_.DateType;

namespace LIB_.SubFROMLib{
    public partial class LotIn : Form {
        public bool bLotInView = false;
        public string OldLotInfo = "";
        public string OldITSInfo = "";
        public bool bLotValidationWait  = false;
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

            if (txtLotId.Text == "") {
                MessageBox.Show("LOT ID 등록 하셔야 합니다.");
                return;
            }
            if (txtITSId.Text == ""){
                MessageBox.Show("ITS ID 등록 하셔야 합니다.");
                return;
            }
            if (txtStripCounter.Text == ""){
                MessageBox.Show("LOT STRIP 투입 수량 입력 하셔야 합니다!");
                //txtStripCounter.Text = "0";
                return;
            }
            if (CUSER.Current.ID == ""){
                MessageBox.Show("USE ID 먼저 확인 후 진행 하셔야 합니다!");
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

            //LOT ID와 ITS ID 비교
            string sLotID = txtLotId.Text;
            string sITSID = txtITSId.Text;
            string[] sList = sITSID.Split(' ');
            //if (sLotID != sList[0]){
            //    MessageBox.Show("현재 등록 LOT ID의 ITS ID가 아닙니다 !");
            //    return;
            //} //LOT ID하고 ITS ID가 고객사 ID면 서로 달라 삭제 (211104 HK)
            CLOT.CurLotStripCnt = int.Parse(txtStripCounter.Text);
            try{
                LOT_INFO.Visible = true;
                FILE_.WR_File(PATH_.LOT_ID, txtLotId.Text, false);
                FILE_.WR_File(PATH_.ITS_ID, txtITSId.Text, false);
                FILE_.WR_File(PATH_.LOT_STRIP_COUNT, txtStripCounter.Text, false);
                OldLotInfo                  = CLOT.GET_LOT.LotID;
                OldITSInfo                  = CLOT.GET_LOT.ItsID;
                CLOT.CurLotID               = txtLotId.Text;
                CLOT.GET_LOT.LotID          = txtLotId.Text;
                CLOT.GET_LOT.ItsID          = txtITSId.Text;
                CLOT.GET_LOT.LoadingCount   = CLOT.CurLotStripCnt;

                if (DATA_.prMACHINE[DATA_.UseLotEnd] == (int)eUSE.NotUSE){
                    DATA_.bWriteLotInfo = true; // LOT 수량 정보 리셋 !
                }
                string sLogMessage = OldLotInfo + " -> " + CLOT.GET_LOT.LotID;
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                sLogMessage = OldITSInfo + " -> " + CLOT.GET_LOT.ItsID;
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                if (DATA_.prMACHINE[DATA_.UseMES] == (int)eUSE.NotUSE){
                    DATA_.bStripDefect  = true; // ITS 정보 읽기 !
                    LOT_INFO.Visible    = false;
                    bLotInView          = false;
                    CLOT.ClearStripBarcodeInfo();
                    this.Hide();
                } //mes 미사용
                else{
                    SUBFRM_.gSecsGem.SetLotRequest(CLOT.CurLotID, CLOT.nLotType, CLOT.CurLotStripCnt);
                    bLotValidationWait = true;
                } //mes 사용
            }
            catch (Exception ex){
                MessageBox.Show("스트립 투입 수량 입력 오류 !" + ETC.NewLine + ex.ToString());
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

            btnCanceledLotNo.Enabled = DATA_.prMACHINE[DATA_.UseMES] == (int)eUSE.USE ? true : false;

            if (DATA_.prMACHINE[DATA_.UseMES] == (int)eUSE.USE && CLOT.bLotValidationSusses){
                CLOT.bLotValidationSusses   = false;
                DATA_.bStripDefect          = true; // ITS 정보 읽기 !
                LOT_INFO.Visible            = false;
                bLotInView                  = false;
                bLotValidationWait          = false;
                CLOT.bFirstLot              = true;
                CLOT.ClearStripBarcodeInfo();
                this.Hide();
            }
            if (CLOT.bLotCanceled){
                CLOT.bLotCanceled = false;
                LOT_INFO.Visible = false;
            }

            if (bLotValidationWait){
                nLotValidationWaitCnt++;
                if (nLotValidationWaitCnt > 30){
                    LOT_INFO.Visible = false;
                    bLotValidationWait = false;
                    MessageBox.Show("LOT VALIDATION 응답 없습니다 !");
                }
            }
            else{
                nLotValidationWaitCnt = 0;
            }
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