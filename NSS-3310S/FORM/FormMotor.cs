using LIB_.UERCTRL;
using Object;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SYSTEM;

namespace NSS_3310S{
    public partial class FormMotor : Form{
        Label LBL                   = null;
        Button BTN                  = null;
        RadioButton RBTN            = null;
        UCL_JOG[] uMT               = null;
        Label[] lbMC                = null;
        Label[] lbMD                = null;
        ComboBox[] CleanMode        = null;
        Label[] CleanRepeat         = null;
        ComboBox[] WorkedCleanMode  = null;
        Label[] WorkedCleanRepeat   = null;
        Button[] Motor              = null;
        DataGridView dgv            = null;
        int nValue                  = 0;
        double dValue               = 0.0;
        private bool bMMIChage      = true;
        int mtStage                 = M.Table1;
        int mtHead                  = M.TRIGGER1;
        int mtPkTh                  = M.X1T;
        int mtPk                    = M.X1Z12;
        int mtTrayFeeder            = M.TrayFeeder1;

        int pPK                     = 0;
        int nHD                     = 0;
        int pHD                     = 0;
        int nStage = 0;
        int pStage = 0;
        int nTray = 0;
        int pTray = 0;

        public double dRefCurPos_TopCam = 0, dRefCurPos_HeadX = 0, dRefCurPos_PalletY = 0, dRefCurPos_TrayY = 0;
        int nCol, nRow = 0;
        int nIndex = 0;

        enum ePage{
            Loading = 0,
            HandlerPk = 1,
            MapBlock = 2,
            Head = 3,
            Tray = 4,
            TrayPk = 5,
            EasyTeaching = 6,
        }

        public FormMotor(){
            InitializeComponent();

            #region EVENT
            bMT_0.Click += (sender, e) => ScreenChange(bMT_0);
            bMT_1.Click += (sender, e) => ScreenChange(bMT_1);
            bMT_2.Click += (sender, e) => ScreenChange(bMT_2);
            bMT_3.Click += (sender, e) => ScreenChange(bMT_3);
            bMT_4.Click += (sender, e) => ScreenChange(bMT_4);
            bMT_5.Click += (sender, e) => ScreenChange(bMT_5);
            bMT_6.Click += (sender, e) => ScreenChange(bMT_6);

            swMotorSelect.Click += (sender, e) => ViewMotorTeaching();

            btn_Pitch_0.Click += (sender, e) => OffestPitchValue(btn_Pitch_0);
            btn_Pitch_1.Click += (sender, e) => OffestPitchValue(btn_Pitch_1);
            btn_Pitch_2.Click += (sender, e) => OffestPitchValue(btn_Pitch_2);
            btn_Pitch_3.Click += (sender, e) => OffestPitchValue(btn_Pitch_3);
            btn_Pitch_4.Click += (sender, e) => OffestPitchValue(btn_Pitch_4);
            btn_Pitch_5.Click += (sender, e) => OffestPitchValue(btn_Pitch_5);

            lbl_IncPitch.DoubleClick += (sender, e) => SetIncPitch(lbl_IncPitch);

            GET_TOP_CAM_FIRST_POSITION.Click += (sender, e) => SetSimpleTeachingPosition(GET_TOP_CAM_FIRST_POSITION);
            GET_HEAD_MAPBLOCK_FIRST_POSITION.Click += (sender, e) => SetSimpleTeachingPosition(GET_HEAD_MAPBLOCK_FIRST_POSITION);
            GET_HEAD_TRAY_FIRST_POSITION.Click += (sender, e) => SetSimpleTeachingPosition(GET_HEAD_TRAY_FIRST_POSITION);

            SET_ALL_PICKUP_POSITION.Click += (sender, e) => SavePkPos(SET_ALL_PICKUP_POSITION);
            SET_ALL_PICKUP_PITCH.Click += (sender, e) => SavePkPos(SET_ALL_PICKUP_PITCH);
            SET_ALL_PLACE_POSITION.Click += (sender, e) => SavePkPos(SET_ALL_PLACE_POSITION);
            SET_ALL_PLACE_PITCH.Click += (sender, e) => SavePkPos(SET_ALL_PLACE_PITCH);

            swSave.Click += (sender, e) => SAVE(swSave);
#if _NSS3300
            uMT = new UCL_JOG[] { uElv_Y, uElv_Z, uBarcode_Y, uRail_Y1, uGripperX,
                                  uStripPkr_X, uStripPkr_Z,
                                  uUnitPkr_X, uUnitPkr_Z,
                                  uMappingTable, uMarkVisionX, uMarkVisionZ,
                                  uBtmCamY, uBtmCamZ, uHeadX, uHeadT, uHeadPkrZ,
                                  uGoodTrayTransferY1, uGoodTrayTransferY2, uReworkTrayTransferY,
                                  uTrayPkr_X, uTrayPkr_Z, uEMPTY_LIFT,
                                  CovMK_X, CovMK_Z, CovPALLET, CovHD, CovTRAY
                                };
#else
            uMT = new UCL_JOG[] { uElv_Y, uElv_Z, uBarcode_Y, uRail_Y1, uRail_Y2, uGripperX,
                                  uStripPkr_X, uStripPkr_Z, uPreAlign_Z,
                                  uUnitPkr_X, uUnitPkr_Z,
                                  uMappingTable, uMarkVisionX, uMarkVisionZ,
                                  uBtmCamY, uBtmCamZ, uHeadX, uHeadT, uHeadPkrZ,
                                  uGoodTrayTransferY1, uGoodTrayTransferY2, uReworkTrayTransferY,
                                  uTrayPkr_X, uTrayPkr_Z, uEMPTY_LIFT,
                                  CovMK_X, CovMK_Z, CovPALLET, CovHD, CovTRAY
                                };
#endif
            for (int i = 0; i < uMT.Length; i++)
            {
                uMT[i].NumMT = M.MT[i];
            }

            uElv_Y.OnMouseUp_Click += (sender, e) => MotorStop(uElv_Y);
            uElv_Z.OnMouseUp_Click += (sender, e) => MotorStop(uElv_Z);
            uRail_Y1.OnMouseUp_Click += (sender, e) => MotorStop(uRail_Y1);
            uRail_Y2.OnMouseUp_Click += (sender, e) => MotorStop(uRail_Y2);
            uGripperX.OnMouseUp_Click += (sender, e) => MotorStop(uGripperX);
            uBarcode_Y.OnMouseUp_Click += (sender, e) => MotorStop(uBarcode_Y);

            uStripPkr_X.OnMouseUp_Click += (sender, e) => MotorStop(uStripPkr_X);
            uStripPkr_Z.OnMouseUp_Click += (sender, e) => MotorStop(uStripPkr_Z);
            uPreAlign_Z.OnMouseUp_Click += (sender, e) => MotorStop(uPreAlign_Z);
            uUnitPkr_X.OnMouseUp_Click += (sender, e) => MotorStop(uUnitPkr_X);
            uUnitPkr_Z.OnMouseUp_Click += (sender, e) => MotorStop(uUnitPkr_Z);

            uMappingTable.OnMouseUp_Click += (sender, e) => MotorStop(uMappingTable);
            uMarkVisionX.OnMouseUp_Click += (sender, e) => MotorStop(uMarkVisionX);
            uMarkVisionZ.OnMouseUp_Click += (sender, e) => MotorStop(uMarkVisionZ);

            uBtmCamY.OnMouseUp_Click += (sender, e) => MotorStop(uBtmCamY);
            uBtmCamZ.OnMouseUp_Click += (sender, e) => MotorStop(uBtmCamZ);

            uHeadX.OnMouseUp_Click += (sender, e) => MotorStop(uHeadX);
            uHeadT.OnMouseUp_Click += (sender, e) => MotorStop(uHeadT);
            uHeadPkrZ.OnMouseUp_Click += (sender, e) => MotorStop(uHeadPkrZ);

            uGoodTrayTransferY1.OnMouseUp_Click += (sender, e) => MotorStop(uGoodTrayTransferY1);
            uGoodTrayTransferY2.OnMouseUp_Click += (sender, e) => MotorStop(uGoodTrayTransferY2);
            uReworkTrayTransferY.OnMouseUp_Click += (sender, e) => MotorStop(uReworkTrayTransferY);

            uTrayPkr_X.OnMouseUp_Click += (sender, e) => MotorStop(uTrayPkr_X);
            uTrayPkr_Z.OnMouseUp_Click += (sender, e) => MotorStop(uTrayPkr_Z);
            uEMPTY_LIFT.OnMouseUp_Click += (sender, e) => MotorStop(uEMPTY_LIFT);

            CovMK_X.OnMouseUp_Click += (sender, e) => MotorStop(CovMK_X);
            CovMK_Z.OnMouseUp_Click += (sender, e) => MotorStop(CovMK_Z);
            CovPALLET.OnMouseUp_Click += (sender, e) => MotorStop(CovPALLET);
            CovHD.OnMouseUp_Click += (sender, e) => MotorStop(CovHD);
            CovTRAY.OnMouseUp_Click += (sender, e) => MotorStop(CovTRAY);

            uElv_Y.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uElv_Y);
            uElv_Z.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uElv_Z);
            uRail_Y1.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uRail_Y1);
            uRail_Y2.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uRail_Y2);
            uGripperX.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uGripperX);
            uBarcode_Y.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uBarcode_Y);

            uStripPkr_X.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uStripPkr_X);
            uStripPkr_Z.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uStripPkr_Z);
            uPreAlign_Z.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uPreAlign_Z);
            uUnitPkr_X.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uUnitPkr_X);
            uUnitPkr_Z.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uUnitPkr_Z);

            uMappingTable.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uMappingTable);
            uMarkVisionX.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uMarkVisionX);
            uMarkVisionZ.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uMarkVisionZ);

            uBtmCamY.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uBtmCamY);
            uBtmCamZ.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uBtmCamZ);

            uHeadX.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uHeadX);
            uHeadT.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uHeadT);
            uHeadPkrZ.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uHeadPkrZ);

            uGoodTrayTransferY1.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uGoodTrayTransferY1);
            uGoodTrayTransferY2.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uGoodTrayTransferY2);
            uReworkTrayTransferY.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uReworkTrayTransferY);

            uTrayPkr_X.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uTrayPkr_X);
            uTrayPkr_Z.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uTrayPkr_Z);
            uEMPTY_LIFT.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uEMPTY_LIFT);

            CovMK_X.OnCcwMouseDown_Click += (sender, e) => MotorCcw(CovMK_X);
            CovMK_Z.OnCcwMouseDown_Click += (sender, e) => MotorCcw(CovMK_Z);
            CovPALLET.OnCcwMouseDown_Click += (sender, e) => MotorCcw(CovPALLET);
            CovHD.OnCcwMouseDown_Click += (sender, e) => MotorCcw(CovHD);
            CovTRAY.OnCcwMouseDown_Click += (sender, e) => MotorCcw(CovTRAY);

            uElv_Y.OnCwMouseDown_Click += (sender, e) => MotorCw(uElv_Y);
            uElv_Z.OnCwMouseDown_Click += (sender, e) => MotorCw(uElv_Z);
            uRail_Y1.OnCwMouseDown_Click += (sender, e) => MotorCw(uRail_Y1);
            uRail_Y2.OnCwMouseDown_Click += (sender, e) => MotorCw(uRail_Y2);
            uGripperX.OnCwMouseDown_Click += (sender, e) => MotorCw(uGripperX);
            uBarcode_Y.OnCwMouseDown_Click += (sender, e) => MotorCw(uBarcode_Y);

            uStripPkr_X.OnCwMouseDown_Click += (sender, e) => MotorCw(uStripPkr_X);
            uStripPkr_Z.OnCwMouseDown_Click += (sender, e) => MotorCw(uStripPkr_Z);
            uPreAlign_Z.OnCwMouseDown_Click += (sender, e) => MotorCw(uPreAlign_Z);
            uUnitPkr_X.OnCwMouseDown_Click += (sender, e) => MotorCw(uUnitPkr_X);
            uUnitPkr_Z.OnCwMouseDown_Click += (sender, e) => MotorCw(uUnitPkr_Z);

            uMappingTable.OnCwMouseDown_Click += (sender, e) => MotorCw(uMappingTable);
            uMarkVisionX.OnCwMouseDown_Click += (sender, e) => MotorCw(uMarkVisionX);
            uMarkVisionZ.OnCwMouseDown_Click += (sender, e) => MotorCw(uMarkVisionZ);

            uBtmCamY.OnCwMouseDown_Click += (sender, e) => MotorCw(uBtmCamY);
            uBtmCamZ.OnCwMouseDown_Click += (sender, e) => MotorCw(uBtmCamZ);

            uHeadX.OnCwMouseDown_Click += (sender, e) => MotorCw(uHeadX);
            uHeadT.OnCwMouseDown_Click += (sender, e) => MotorCw(uHeadT);
            uHeadPkrZ.OnCwMouseDown_Click += (sender, e) => MotorCw(uHeadPkrZ);

            uGoodTrayTransferY1.OnCwMouseDown_Click += (sender, e) => MotorCw(uGoodTrayTransferY1);
            uGoodTrayTransferY2.OnCwMouseDown_Click += (sender, e) => MotorCw(uGoodTrayTransferY2);
            uReworkTrayTransferY.OnCwMouseDown_Click += (sender, e) => MotorCw(uReworkTrayTransferY);

            uTrayPkr_X.OnCwMouseDown_Click += (sender, e) => MotorCw(uTrayPkr_X);
            uTrayPkr_Z.OnCwMouseDown_Click += (sender, e) => MotorCw(uTrayPkr_Z);
            uEMPTY_LIFT.OnCwMouseDown_Click += (sender, e) => MotorCw(uEMPTY_LIFT);

            CovMK_X.OnCwMouseDown_Click += (sender, e) => MotorCw(CovMK_X);
            CovMK_Z.OnCwMouseDown_Click += (sender, e) => MotorCw(CovMK_Z);
            CovPALLET.OnCwMouseDown_Click += (sender, e) => MotorCw(CovPALLET);
            CovHD.OnCwMouseDown_Click += (sender, e) => MotorCw(CovHD);
            CovTRAY.OnCwMouseDown_Click += (sender, e) => MotorCw(CovTRAY);

            SelectMapBlock1.Click += (sender, e) => SelectMoter(SelectMapBlock1);
            SelectStage1.Click += (sender, e) => SelectMoter(SelectStage1);
            SelectHD1.Click += (sender, e) => SelectMoter(SelectHD1);
            SelectX1.Click += (sender, e) => SelectMoter(SelectX1);

            SelectMapBlock2.Click += (sender, e) => SelectMoter(SelectMapBlock2);
            SelectStage2.Click += (sender, e) => SelectMoter(SelectStage2);
            SelectHD2.Click += (sender, e) => SelectMoter(SelectHD2);
            SelectX2.Click += (sender, e) => SelectMoter(SelectX2);

            SelectPkr12.Click += (sender, e) => SelectMoter(SelectPkr12);
            SelectPkr34.Click += (sender, e) => SelectMoter(SelectPkr34);
            SelectPkr56.Click += (sender, e) => SelectMoter(SelectPkr56);

            SelectGoodTrayTransfer1.Click += (sender, e) => SelectMoter(SelectGoodTrayTransfer1);
            SelectGoodTrayTransfer2.Click += (sender, e) => SelectMoter(SelectGoodTrayTransfer2);
            SelectReworkTrayTransfer.Click += (sender, e) => SelectMoter(SelectReworkTrayTransfer);
#endregion

            lbMC = new[] {
                             lblMGZ_UDPitch, lblMGZ_ULD_UDPitch, lblGripperBackPitch, lblRailLoaingOpenPitch,
                             lblStripPickUpCheckPitch, lblUnitPickUpCheckPitch, lblUnitWorkEndPitch,lblUnitWorkAirshowerSpd, lblUnitWorkBrushSpd,
                             BrushRepeatCnt, AirShowerRepeatCnt,
                             lblStageWorkEndPitch,
                             lblPickUpCheckUpPitch, lblPlaceCheckUpPitch, WorkedAirShowerRepeatCnt, lblMGZ_Pitch_Spd
            };
            lbMD = new[] {
                             lbFirstPickUpVacDelay, lbFirstLinePickUpVacDelay, lbPickUpVacDelay, lbPlaceBlowDelay, lbPlaceDelay,
                             lblUnitSizeX, lblUnitSizeY,
                             lblStageUnitCountX, lblStageUnitCountY,
                             lblStageUnitPitchX, lblStageUnitPitchY,
                             lblTrayCountX, lblTrayCountY,
                             lblTrayPitchX, lblTrayPitchY,
                             lblMGZClampUpPitch
            };
            for (int i = 0; i < lbMC.Length; i++){
                lbMC[i].TabIndex = CP.MTPara[i];
            }
            for (int i = 0; i < lbMD.Length; i++){
                lbMD[i].TabIndex = RP.MTPara[i];
            }

            CleanMode = new ComboBox[] { cbxCLEAN_MODE_0, cbxCLEAN_MODE_1, cbxCLEAN_MODE_2 };
            CleanRepeat = new Label[] { lblCLEAN_REPEAT_0, lblCLEAN_REPEAT_1, lblCLEAN_REPEAT_2 };

            WorkedCleanMode = new ComboBox[] { cbxWORKED_CLEAN_MODE_0, cbxWORKED_CLEAN_MODE_1, cbxWORKED_CLEAN_MODE_2 };
            WorkedCleanRepeat = new Label[] { lblWORKED_CLEAN_REPEAT_0, lblWORKED_CLEAN_REPEAT_1, lblWORKED_CLEAN_REPEAT_2 };

            Motor = new Button[] {  TopCam_StageTachingPos, TopCam_StageUnitViewPos,
                                    HD_StageTeachingPos, HD_StageUnitViewPos,
                                    TrayLoadingPos, TrayUnloadingPos, HD_TrayPocketView };
            for (int i = 0; i < Motor.Length; i++){
                Motor[i].TabIndex = ManualNumber.MTMotor[i];
            }

            SelectMapBlock1.Checked = true;
            SelectX1.Checked = true;
            SelectPkr12.Checked = true;
            SelectHD1.Checked = true;
            SelectStage1.Checked = true;
            SelectGoodTrayTransfer1.Checked = true;

            cbxZigHoleNum.SelectedIndex = 0;
            HD_StageZigViewPos.TabIndex = ManualNumber.StageZigView;

            ChkGripperLoadingPos.TabIndex   = RP.UseStripLoadngPos;
            lbPlaceCheckDelay.TabIndex      = CP.PlaceCheckDalay;
            lbRejectBlow.TabIndex           = CP.RejectBlowDelay;

#if _NSS3300
            uRail_Y2.Visible    = false;
            uPreAlign_Z.Visible = false;
            groupBox12.Visible  = false;

            label4.Visible      = false;
            panel2.Visible      = false;
#else
            uRail_Y2.Visible    = true;
            uPreAlign_Z.Visible = true;
            groupBox12.Visible  = true;

            label4.Visible      = true;
            panel2.Visible      = true;
#endif

            if (DATA_.MC_DIR == 0){
                //uStripPkr_X.ImgJogCcw = iltDir.Images(2)
            } //정
            else{

            } //역
        }
        public void Set_Language(){

        }

        public void Initailize_View(){
            if (DEF.MotorPage < 0) ScreenChange(bMT_0);
            InfoDataGridView();
            InfoComboBox();
            SetMTData();
            ReadPara();

            lbMAPBLOCK_TEACHING.Text = DATA_.MC_DIR == 0 ? "맵블록 좌하단 위치" : "맵블록 우하단 위치";
            lbMAPBLOCK_TEACHING1.Text = lbMAPBLOCK_TEACHING.Text;
            lbTRAY_TEACHING.Text = DATA_.MC_DIR == 0 ? "트레이 좌상단 위치" : "트레이 우상단 위치";
            lbTRAY_TEACHING1.Text = lbTRAY_TEACHING.Text;
            TopCam_StageTachingPos.Text = DATA_.MC_DIR == 0 ? "[MOVE]" + ETC.NewLine + "좌하단 위치" : "[MOVE]" + ETC.NewLine + "우하단 위치";
            HD_StageTeachingPos.Text = DATA_.MC_DIR == 0 ? "[MOVE]" + ETC.NewLine + "좌하단 위치" : "[MOVE]" + ETC.NewLine + "우하단 위치";
            if (DATA_.MC_DIR == 0){
                pnlMapBlackTeachingDir0.Visible = true;
                pnlMapBlackTeaching1Dir0.Visible = true;
                pnlMapBlackTeaching2Dir0.Visible = true;
                pnlTrayTeachingDir0.Visible = true;
                pnlTrayTeaching1Dir0.Visible = true;
                pnlTrayTeaching2Dir0.Visible = true;
                pnlTrayTeaching3Dir0.Visible = true;
                pnlMapBlackTeachingDir1.Visible = false;
                pnlMapBlackTeaching1Dir1.Visible = false;
                pnlMapBlackTeaching2Dir1.Visible = false;
                pnlTrayTeachingDir1.Visible = false;
                pnlTrayTeaching1Dir1.Visible = false;
                pnlTrayTeaching2Dir1.Visible = false;
                pnlTrayTeaching3Dir1.Visible = false;
            }
            else{
                pnlMapBlackTeachingDir0.Visible = false;
                pnlMapBlackTeaching2Dir0.Visible = false;
                pnlTrayTeachingDir0.Visible = false;
                pnlMapBlackTeaching1Dir0.Visible = false;
                pnlTrayTeaching1Dir0.Visible = false;
                pnlTrayTeaching2Dir0.Visible = false;
                pnlTrayTeaching3Dir0.Visible = false;
                pnlMapBlackTeachingDir1.Visible = true;
                pnlMapBlackTeaching1Dir1.Visible = true;
                pnlMapBlackTeaching2Dir1.Visible = true;
                pnlTrayTeachingDir1.Visible = true;
                pnlTrayTeaching1Dir1.Visible = true;
                pnlTrayTeaching2Dir1.Visible = true;
                pnlTrayTeaching3Dir1.Visible = true;
            }

            lbTrayDownX.Text = "0";
            lbTrayDownY.Text = "0";

            if (DATA_.eLoginLevel > eLogLevel.ENG){
                label21.Visible = true;
                lblStripPickUpCheckPitch.Visible = true;

                label22.Visible = true;
                lblUnitPickUpCheckPitch.Visible = true;
            }
            else{
                label21.Visible = false;
                lblStripPickUpCheckPitch.Visible = false;

                label22.Visible = false;
                lblUnitPickUpCheckPitch.Visible = false;
            }

            TmrMT.Enabled = true;
            Show();
            BringToFront();
        }

        private void OptionTeaching_Click(object sender, EventArgs e){
            BTN = (Button)sender;
            if (BTN.Name == "RailOptionReset"){
                tbxRail_F.Text = "0";
                tbxRail_R.Text = "0";
                tbxGripper_R.Text = "0";
                tbxGripper_L.Text = "0";
            }
            if (BTN.Name == "RailOptionSet"){
                try{
                    double dGX_M = double.Parse(tbxGripper_R.Text); //스트립 X방향 옵셋
                    double dGX_P = double.Parse(tbxGripper_L.Text);
                    double dRY_F = double.Parse(tbxRail_F.Text); //스트립 FORWARD 방향으로 
                    double dRY_B = double.Parse(tbxRail_R.Text); //스트립 BACKWARD 방향으로

                    double dOffestPositionX = 0;
                    double dOffestPositionY1 = 0;
                    double dOffestPositionY2 = 0;

                    double dSetX = 0;
                    double dSetY1 = 0;
                    double dSetY2 = 0;

                    int nXPOS = P.StripLoad;
                    if (DATA_.prMODEL[RP.UseStripLoadngPos] == (int)ePARA.RECIPE) nXPOS = P.RecipStripLoad;

#if _NSS3300
#else
                    if (RailOptionSelect.Checked){
                        if (DialogResult.OK == MessageBox.Show("스트립 소재 안착 위치 값 바로 변경 하시겠습니까", "Select", MessageBoxButtons.OKCancel)){
                            if (dRY_F == 0 && dRY_B == 0) return;
                            dOffestPositionY1 = DATA_.mtDATA[M.RailF, P.StripIn].Pos;
                            dOffestPositionY2 = DATA_.mtDATA[M.RailR, P.StripIn].Pos;

                            dSetY1 = DATA_.mtDATA[M.RailF, P.StripIn].Pos - (dRY_F + dRY_B); //-
                            dSetY2 = DATA_.mtDATA[M.RailR, P.StripIn].Pos + (dRY_F - dRY_B); //+

                            TEACH_.SaveMotorPos(M.RailF, P.StripIn, dSetY1);
                            TEACH_.SaveMotorPos(M.RailR, P.StripIn, dSetY1);

                            MessageBox.Show("옵셋 적용 하였습니다!" + ETC.NewLine + "RY1 = " + dOffestPositionY1.ToString() + " -> " + dSetY1.ToString() + "/ RY2 = " + dOffestPositionY2.ToString() + " -> " + dSetY2.ToString());
                            bMMIChage = true;
                        }
                    } //소재 안착 위치
                    else{
                        if (DialogResult.OK == MessageBox.Show("스트립 작업 위치 값 바로 변경 하시겠습니까", "Select", MessageBoxButtons.OKCancel)){
                            if (dGX_M == 0 && dGX_P == 0 && dRY_F == 0 && dRY_B == 0) return;
                            dOffestPositionX = DATA_.mtDATA[M.GrpX, nXPOS].Pos;
                            dOffestPositionY1 = DATA_.mtDATA[M.RailF, P.StripAlign].Pos;
                            dOffestPositionY2 = DATA_.mtDATA[M.RailR, P.StripAlign].Pos;

                            dSetX = DATA_.mtDATA[M.GrpX, nXPOS].Pos + (dGX_P - dGX_M);
                            dSetY1 = DATA_.mtDATA[M.RailF, P.StripAlign].Pos - (dRY_F + dRY_B); //-
                            dSetY2 = DATA_.mtDATA[M.RailR, P.StripAlign].Pos + (dRY_F - dRY_B); //+

                            TEACH_.SaveMotorPos(M.GrpX, nXPOS, dSetX);
                            TEACH_.SaveMotorPos(M.RailF, P.StripAlign, dSetY1);
                            TEACH_.SaveMotorPos(M.RailR, P.StripAlign, dSetY2);

                            MessageBox.Show("옵셋 적용 하였습니다!" + ETC.NewLine + "X = " + dOffestPositionX.ToString() + " -> " + dSetX.ToString() + "/ RY1 = " + dOffestPositionY1.ToString() + " -> " + dSetY1.ToString() + "/ RY2 = " + dOffestPositionY2.ToString() + " -> " + dSetY2.ToString());
                            bMMIChage = true;
                        }
                    } //작업 위치
#endif
                    
                }
                catch (Exception ex){
                    MessageBox.Show("fMOTOR -> RailOptionSet_Click Fail !" + ETC.NewLine + ex.ToString());
                    return;
                }
            }

            if (BTN.Name == "IniPoketCenterPitch"){
                double dHalfSizeX = Math.Round((DATA_.prMODEL[RP.UnitSizeY/*UnitSizeX*/] / 2)/* - 1*/, 3);
                double dHalfSizeY = Math.Round((DATA_.prMODEL[RP.UnitSizeX/*UnitSizeY*/] / 2)/* - 1*/, 3);
                tbxPocketPitchX.Text = dHalfSizeX.ToString();
                tbxPocketPitchY.Text = dHalfSizeY.ToString();
            }
            if (BTN.Name == "bCENTER_MOVE"){
                //포켓 중심 위치로 이송
                //Select X View  -> TRUE TOP CAM / FALSE HD CAM
                DATA_.IsSTRING[S.MotorMessage] = "TOP(MARK) 카메라 X축 / ";
                int mtX = M.TopVisionX;
                int mtY = CovPALLET.NumMT;

                try{
                    double XPitch = double.Parse(tbxPocketPitchX.Text);
                    double YPitch = double.Parse(tbxPocketPitchY.Text);

                    if (!SelectView.Checked){
                        if (CovHD.NumMT == M.TRIGGER1){
                            DATA_.IsSTRING[S.MotorMessage] = "HEAD X1 카메라 X축 / ";
                            mtX = M.TRIGGER1;
                        }
                        else if (CovHD.NumMT == M.TRIGGER2){
                            DATA_.IsSTRING[S.MotorMessage] = "HEAD X2 카메라 X축 / ";
                            mtX = M.TRIGGER2;
                        }
                        else return;
                    }

                    if (CovPALLET.NumMT == M.Table1) DATA_.IsSTRING[S.MotorMessage] += "드라이블록 Y1축 ";
                    else DATA_.IsSTRING[S.MotorMessage] += "드라이블록 Y2축 ";
                    if (XPitch == 0 && YPitch == 0){
                        MessageBox.Show("X/Y PITCH 입력 하셔야 합니다.");
                        return;
                    }
                    if (DialogResult.OK == MessageBox.Show(DATA_.IsSTRING[S.MotorMessage] + "피치 이송 하시겠습니까 ?", "Select", MessageBoxButtons.OKCancel)){
                        LAB_.MT_PITCH_CCW(mtX, 50, XPitch);
                        LAB_.MT_PITCH_CW(mtY, 50, YPitch);
                    }
                }
                catch (Exception ex){
                    MessageBox.Show("fMOTOR -> bCENTER_MOVE_Click Fail !" + ETC.NewLine + ex.ToString());
                    return;
                }
            }

            if (BTN.Name == "btnIniPlaceOffsetValue"){
                lbPLACE_PY.Text = "0";
                lbPLACE_MY.Text = "0";
                lbPLACE_PX.Text = "0";
                lbPLACE_MX.Text = "0";
            }
            if (BTN.Name == "btnPlaceOptionOffset"){
                DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[CovHD.NumMT] + "축 " + DATA_.MtName[CovTRAY.NumMT] + "축 내려 놓는 위치 바로 변경 하시겠습니까?" + ETC.NewLine +
                "[PY=" + lbPLACE_PY.Text + "/MY=" + lbPLACE_MY.Text + "/PX=" + lbPLACE_PX.Text + "/MX=" + lbPLACE_MX.Text + "]";

                try{
                    double dPX = double.Parse(lbPLACE_PX.Text); //유닛 트레이 오른쪽 걸림 -x
                    double dMX = double.Parse(lbPLACE_MX.Text); //유닛 트레이 왼쪽 걸림 +x
                    double dPY = double.Parse(lbPLACE_PY.Text); //유닛 트레이 위 걸림 -y
                    double dMY = double.Parse(lbPLACE_MY.Text); //유닛 트레이 아래 걸림 +y

                    double dOffestPositionX = 0;
                    double dOffestPositionY = 0;

                    double dSetX = 0;
                    double dSetY = 0;

                    int nXPOS = P.Feeder1;
                    int nYPOS = P.HD1TrayPocket;

                    if (DialogResult.OK == MessageBox.Show(DATA_.IsSTRING[S.MotorMessage], "Select", MessageBoxButtons.OKCancel)){
                        if (dPX == 0 && dMX == 0 && dPY == 0 && dMY == 0) return;

                        if (CovTRAY.NumMT == M.TrayFeeder1) nXPOS = P.Feeder1;
                        else if (CovTRAY.NumMT == M.TrayFeeder2) nXPOS = P.Feeder2;
                        else nXPOS = P.Feeder3;

                        if (CovHD.NumMT == M.TRIGGER1) nYPOS = P.HD1TrayPocket;
                        else nYPOS = P.HD2TrayPocket;

                        dOffestPositionX = DATA_.mtDATA[CovHD.NumMT, nXPOS].Pos;
                        dOffestPositionY = DATA_.mtDATA[CovTRAY.NumMT, nYPOS].Pos;
                        dSetX = DATA_.mtDATA[CovHD.NumMT, nXPOS].Pos + (-dPX + dMX);
                        dSetY = DATA_.mtDATA[CovTRAY.NumMT, nYPOS].Pos + (-dPY + dMY);

                        TEACH_.SaveMotorPos(M.TopVisionX, nXPOS, dSetX);
                        TEACH_.SaveMotorPos(CovTRAY.NumMT, nYPOS, dSetY);

                        MessageBox.Show("옵셋 적용 하였습니다!" + ETC.NewLine + "X = " + dOffestPositionX.ToString() + " -> " + dSetX.ToString() + "/ Y = " + dOffestPositionY.ToString() + " -> " + dSetY.ToString());
                        bMMIChage = true;
                    }
                }
                catch (Exception ex){
                    MessageBox.Show("fMOTOR -> bCENTER_MOVE_Click Fail !" + ETC.NewLine + ex.ToString());
                    return;
                }
            }
        }

        void SetSimpleTeachingPosition(object sender){
            BTN = (Button)sender;
            if (BTN.Name == "GET_TOP_CAM_FIRST_POSITION"){
                DATA_.IsSTRING[S.MotorMessage] = GT_MarkCamMapBlockTeaching.Text;
                DATA_.IsSTRING[S.MotorMessage] += "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "GET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    LBL_MARK_X_POS.Text = LAB_.GET_ACTPOS(M.TopVisionX).ToString();
                    LBL_MARK_Z_POS.Text = LAB_.GET_ACTPOS(M.TopVisionZ).ToString();
                    LBL_MB_Y_POS.Text = LAB_.GET_ACTPOS(CovPALLET.NumMT).ToString();

                    lblTopCamXPos.Text = LAB_.GET_ACTPOS(M.TopVisionX).ToString();
                    lblTopCamZPos.Text = LAB_.GET_ACTPOS(M.TopVisionZ).ToString();
                    lblMapBlockYPos.Text = LAB_.GET_ACTPOS(CovPALLET.NumMT).ToString();
                }
            }
            if (BTN.Name == "GT_HeadMapBlockTeaching"){
                DATA_.IsSTRING[S.MotorMessage] = GT_MarkCamMapBlockTeaching.Text;
                DATA_.IsSTRING[S.MotorMessage] += "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "GET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    LBL_HEAD_X_POS.Text = LAB_.GET_ACTPOS(CovHD.NumMT).ToString();
                    LBL_MB_Y_POS1.Text = LAB_.GET_ACTPOS(CovPALLET.NumMT).ToString();
                }
            }
            if (BTN.Name == "GT_HeadTrayTeaching"){
                DATA_.IsSTRING[S.MotorMessage] = GT_MarkCamMapBlockTeaching.Text;
                DATA_.IsSTRING[S.MotorMessage] += "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "GET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    LBL_HEAD_X_POS1.Text = LAB_.GET_ACTPOS(CovHD.NumMT).ToString();
                    LBL_TRAY_Y_POS.Text = LAB_.GET_ACTPOS(CovTRAY.NumMT).ToString();
                    lbTrayXPos.Text = LAB_.GET_ACTPOS(CovHD.NumMT).ToString();
                    lbTrayYPos.Text = LAB_.GET_ACTPOS(CovTRAY.NumMT).ToString();
                }
            }
        }

        void SavePkPos(object sender){
            BTN = (Button)sender;
            int m;
            double ps;
            string sr;
            if (BTN.Name == "SET_ALL_PICKUP_POSITION"){
                if (MessageBox.Show(DATA_.MtName[mtHead] + " 모든 피커 유닛 PICK-UP 위치 (+/-) " + lblPickerAllPicPos.Text + "mm로 변경 하시겠습니까 ?", "Save", MessageBoxButtons.YesNo) == DialogResult.No) return;
                ps = double.Parse(lblPickerAllPicPos.Text);
                for (int i = 0; i < CNT_.PKR; i++){
                    if (mtHead == M.TRIGGER1) m = M.HD1Pk[i];
                    else m = M.HD2Pk[i];
                    dValue = DATA_.cMATH.IsEven(i) ? -ps : ps;
                    TEACH_.SaveMotorPos(m, P.PK_PIC[i], dValue);
                }
                bMMIChage = true;
            }
            if (BTN.Name == "SET_ALL_PICKUP_PITCH"){
                sr = double.Parse(lblPickerAllPicOffsetPitch.Text) > 0 ? "다운" : "업";
                if (MessageBox.Show(DATA_.MtName[mtHead] + "모든 피커 유닛 PICK-UP 위치에서 " + lblPickerAllPicOffsetPitch.Text + "mm " + sr + " 하시겠습니까 ?", "Save", MessageBoxButtons.YesNo) == DialogResult.No) return;
                ps = double.Parse(lblPickerAllPicOffsetPitch.Text);
                for (int i = 0; i < CNT_.PKR; i++){
                    if (mtHead == M.TRIGGER1) m = M.HD1Pk[i];
                    else m = M.HD2Pk[i];
                    dValue = DATA_.cMATH.IsEven(i) ? -ps : ps;
                    dValue = DATA_.mtDATA[m, P.PK_PIC[i]].Pos + dValue;
                    TEACH_.SaveMotorPos(m, P.PK_PIC[i], dValue);
                }
                bMMIChage = true;
            }
            if (BTN.Name == "SET_ALL_PLACE_POSITION"){
                if (MessageBox.Show(DATA_.MtName[mtHead] + "모든 피커 유닛 PLACE 위치 (+/-) " + lblPickerAllPlacePos.Text + "mm로 변경 하시겠습니까 ?", "Save", MessageBoxButtons.YesNo) == DialogResult.No) return;
                ps = double.Parse(lblPickerAllPlacePos.Text);
                for (int i = 0; i < CNT_.PKR; i++){
                    if (mtHead == M.TRIGGER1) m = M.HD1Pk[i];
                    else m = M.HD2Pk[i];
                    dValue = DATA_.cMATH.IsEven(i) ? -ps : ps;
                    TEACH_.SaveMotorPos(m, P.PK_PLC[i], dValue);
                }
                bMMIChage = true;
            }
            if (BTN.Name == "SET_ALL_PLACE_PITCH"){
                sr = double.Parse(lblPickerAllPlaceOffsetPitch.Text) > 0 ? "다운" : "업";
                if (MessageBox.Show(DATA_.MtName[mtHead] + "모든 피커 유닛 PLACE 위치에서 " + lblPickerAllPicOffsetPitch.Text + "mm " + sr + " 하시겠습니까 ?", "Save", MessageBoxButtons.YesNo) == DialogResult.No) return;
                ps = double.Parse(lblPickerAllPlaceOffsetPitch.Text);
                for (int i = 0; i < CNT_.PKR; i++){
                    if (mtHead == M.TRIGGER1) m = M.HD1Pk[i];
                    else m = M.HD2Pk[i];
                    dValue = DATA_.cMATH.IsEven(i) ? -ps : ps;
                    dValue = DATA_.mtDATA[m, P.PK_PLC[i]].Pos + dValue;
                    TEACH_.SaveMotorPos(m, P.PK_PLC[i], dValue);
                }
                bMMIChage = true;
            }
        }

        void SAVE(object sender){
            if (MessageBox.Show("Do you want to save teaching data ?", "Save", MessageBoxButtons.YesNo) == DialogResult.No) return;
            if (DEF.MotorPage == (long)ePage.Loading){
                TEACH_.Write_MotorPos(dgvCassetteYZ, M.MAGZINE);
                TEACH_.Write_MotorPos(dgvRail, M.RAIL);
                TEACH_.Write_MotorPos(dgvGripperX, M.GrpX);
                TEACH_.Write_MotorPos(dgvBarcode, M.Barcode);

                TEACH_.Write_Parameter(lblMGZClampUpPitch);
                TEACH_.Write_Parameter(lblMGZ_UDPitch);
                TEACH_.Write_Parameter(lblMGZ_ULD_UDPitch);
                TEACH_.Write_Parameter(lblRailLoaingOpenPitch);
                TEACH_.Write_Parameter(lblGripperBackPitch);

                DATA_.iMANUAL.int_1 = ChkGripperLoadingPos.Checked ? (int)ePARA.RECIPE : (int)ePARA.COM;
                TEACH_.Write_ModelPara(RP.UseStripLoadngPos, DATA_.iMANUAL.int_1);
            }
            if (DEF.MotorPage == (long)ePage.HandlerPk){
                TEACH_.Write_MotorPos(dgvStripPkrXZ, M.STRIP_PK);
#if _NSS3300
#else
                TEACH_.Write_MotorPos(dgvPreAlignZ, M.PreAlign);
#endif
                TEACH_.Write_MotorPos(dgvUnitPkrXZ, M.UNIT_PK);

                TEACH_.Write_Parameter(lblStripPickUpCheckPitch);
                TEACH_.Write_Parameter(lblUnitPickUpCheckPitch);
                TEACH_.Write_Parameter(lblUnitWorkEndPitch);
                TEACH_.Write_Parameter(lblUnitWorkAirshowerSpd);
                TEACH_.Write_Parameter(lblUnitWorkBrushSpd);
                TEACH_.Write_Parameter(BrushRepeatCnt);
                TEACH_.Write_Parameter(AirShowerRepeatCnt);
                TEACH_.Write_Parameter(WorkedAirShowerRepeatCnt);

                nIndex = cbxCLEAN_MODE_0.SelectedIndex < 0 ? 0 : cbxCLEAN_MODE_0.SelectedIndex;
                nValue = lblCLEAN_REPEAT_0.Text == "" ? 0 : int.Parse(lblCLEAN_REPEAT_0.Text);
                TEACH_.WR_CLEAN_DATA(0, nIndex, nValue, 0);
                nIndex = cbxCLEAN_MODE_1.SelectedIndex < 0 ? 0 : cbxCLEAN_MODE_1.SelectedIndex;
                nValue = lblCLEAN_REPEAT_1.Text == "" ? 0 : int.Parse(lblCLEAN_REPEAT_1.Text);
                TEACH_.WR_CLEAN_DATA(1, nIndex, nValue, 0);
                nIndex = cbxCLEAN_MODE_2.SelectedIndex < 0 ? 0 : cbxCLEAN_MODE_2.SelectedIndex;
                nValue = lblCLEAN_REPEAT_2.Text == "" ? 0 : int.Parse(lblCLEAN_REPEAT_2.Text);
                TEACH_.WR_CLEAN_DATA(2, nIndex, nValue, 0);

                //WR_WORKED_CLEAN_DATA
                nIndex = cbxWORKED_CLEAN_MODE_0.SelectedIndex < 0 ? 0 : cbxWORKED_CLEAN_MODE_0.SelectedIndex;
                nValue = lblWORKED_CLEAN_REPEAT_0.Text == "" ? 0 : int.Parse(lblWORKED_CLEAN_REPEAT_0.Text);
                TEACH_.WR_WORKED_CLEAN_DATA(0, nIndex, nValue, 0);

                nIndex = cbxWORKED_CLEAN_MODE_1.SelectedIndex < 0 ? 0 : cbxWORKED_CLEAN_MODE_1.SelectedIndex;
                nValue = lblWORKED_CLEAN_REPEAT_1.Text == "" ? 0 : int.Parse(lblWORKED_CLEAN_REPEAT_1.Text);
                TEACH_.WR_WORKED_CLEAN_DATA(1, nIndex, nValue, 0);

                nIndex = cbxWORKED_CLEAN_MODE_2.SelectedIndex < 0 ? 0 : cbxWORKED_CLEAN_MODE_2.SelectedIndex;
                nValue = lblWORKED_CLEAN_REPEAT_2.Text == "" ? 0 : int.Parse(lblWORKED_CLEAN_REPEAT_2.Text);
                TEACH_.WR_WORKED_CLEAN_DATA(2, nIndex, nValue, 0);
            }
            if (DEF.MotorPage == (long)ePage.MapBlock){
                TEACH_.Write_MotorPos(dgvMappingTable, mtStage);
                TEACH_.Write_MotorPos(dgvMarkVision, M.TOP_CAM);
                TEACH_.Write_MotorPos(dgvPRSVision, M.BTM_CAM);

                TEACH_.Write_Parameter(lblStageWorkEndPitch);

                DEF.ReadSearchRoiUnitCnt();
                DEF.ReadMapBlockPickUp();
            }
            if (DEF.MotorPage == (long)ePage.Head){
                TEACH_.Write_MotorPos(dgvHeadX, mtHead);
                TEACH_.Write_MotorPos(dgvPkrZ, mtPk);
                TEACH_.Write_MotorPos(dgvPkrTh, mtPkTh);

                TEACH_.Write_Parameter(lblPickUpCheckUpPitch);
                TEACH_.Write_Parameter(lblPlaceCheckUpPitch);
                TEACH_.Write_Parameter(lbFirstPickUpVacDelay);
                TEACH_.Write_Parameter(lbFirstLinePickUpVacDelay);
                TEACH_.Write_Parameter(lbPickUpVacDelay);
                TEACH_.Write_Parameter(lbPlaceBlowDelay);
                TEACH_.Write_Parameter(lbPlaceDelay);
                TEACH_.Write_Parameter(lbPlaceCheckDelay);
                TEACH_.Write_Parameter(lbRejectBlow);

                DEF.ReadMapBlockPickUp();
                DEF.ReadTrayPlace();
            }
            if (DEF.MotorPage == (long)ePage.Tray){
                TEACH_.Write_MotorPos(dgvGTRAY_TRANSFER_1, M.TrayFeeder1);
                TEACH_.Write_MotorPos(dgvGTRAY_TRANSFER_2, M.TrayFeeder2);
                TEACH_.Write_MotorPos(dgvRTRAY_TRANSFER, M.TrayFeeder3);

                DEF.ReadTrayPlace();
            }
            if (DEF.MotorPage == (long)ePage.TrayPk){
                TEACH_.Write_MotorPos(dgvTrayPkrXZ, M.TRAY_PK);
                TEACH_.Write_MotorPos(dgvEmptyTrayLift, M.EmptyElv);
            }
            if (DEF.MotorPage == (long)ePage.EasyTeaching){
                //Top camera stage view position save
                //TEACH_.SaveMotorPos(M.TopVisionX, P.TopCam_Pallet[pStage], double.Parse(LBL_MARK_X_POS.Text));
                //TEACH_.SaveMotorPos(M.TopVisionZ, P.TopCam_Pallet[pStage], double.Parse(LBL_MARK_Z_POS.Text));
                //TEACH_.SaveMotorPos(CovPALLET.NumMT, P.TopVision_Unit, double.Parse(LBL_MB_Y_POS.Text));
                TEACH_.SaveMotorPos(M.TopVisionX, P.TopCam_Pallet[pStage], double.Parse(lblTopCamXPos.Text));
                TEACH_.SaveMotorPos(M.TopVisionZ, P.TopCam_Pallet[pStage], double.Parse(lblTopCamZPos.Text));
                TEACH_.SaveMotorPos(CovPALLET.NumMT, P.TopVision_Unit, double.Parse(lblMapBlockYPos.Text));
                //Head stage view position save
                TEACH_.SaveMotorPos(CovHD.NumMT, P.HD_PIC[pStage], double.Parse(LBL_HEAD_X_POS.Text));
                TEACH_.SaveMotorPos(CovPALLET.NumMT, P.HD_Pallet[pHD], double.Parse(LBL_MB_Y_POS1.Text));
                //Head Tray view position save
                //TEACH_.SaveMotorPos(CovHD.NumMT, P.HD_PLC[pTray], double.Parse(LBL_HEAD_X_POS1.Text));
                //TEACH_.SaveMotorPos(CovTRAY.NumMT, P.Tray_Place[pHD], double.Parse(LBL_TRAY_Y_POS.Text));
                TEACH_.SaveMotorPos(CovHD.NumMT, P.HD_PLC[pTray], double.Parse(lbTrayXPos.Text));
                TEACH_.SaveMotorPos(CovTRAY.NumMT, P.Tray_Place[pHD], double.Parse(lbTrayYPos.Text));

                TEACH_.Write_Parameter(lblUnitSizeX);
                TEACH_.Write_Parameter(lblUnitSizeY);
                TEACH_.Write_Parameter(lblStageUnitCountX);
                TEACH_.Write_Parameter(lblStageUnitCountY);
                TEACH_.Write_Parameter(lblStageUnitPitchX);
                TEACH_.Write_Parameter(lblStageUnitPitchY);
                TEACH_.Write_Parameter(lblTrayCountX);
                TEACH_.Write_Parameter(lblTrayCountY);
                TEACH_.Write_Parameter(lblTrayPitchX);
                TEACH_.Write_Parameter(lblTrayPitchY);

                if (rbnAttachZigStage1.Checked) nValue = (int)eMAP_BLOCK.STAGE1;
                else                            nValue = (int)eMAP_BLOCK.STAGE2;
                TEACH_.WR_MCPara(CP.ZigAttachStage, nValue);

                TEACH_.Write_MachinePara(CP.Zig1stPosX[nHD], double.Parse(lbZigX1Pos.Text));
                TEACH_.Write_MachinePara(CP.Zig1stPosY[nHD], double.Parse(lbZigY1Pos.Text));
                TEACH_.Write_MachinePara(CP.Zig2ndPosX[nHD], double.Parse(lbZigX2Pos.Text));
                TEACH_.Write_MachinePara(CP.Zig2ndPosY[nHD], double.Parse(lbZigY2Pos.Text));
                TEACH_.Write_MachinePara(CP.ZigLastPosX[nHD], double.Parse(lbZigX3Pos.Text));
                TEACH_.Write_MachinePara(CP.ZigLastPosY[nHD], double.Parse(lbZigY3Pos.Text));

                DEF.ReadSearchRoiUnitCnt();
                DEF.ReadMapBlockPickUp();
                DEF.ReadTrayPlace();
                InfoComboBox();
            }
            P.GetHandlerPkZSafetyPos();
            DEF.SetParaFDC();
            MessageBox.Show("Save Success");
        }

#region >>EVENT
        void ResetScreen(){
            for (int i = 0; i < 7; i++){
                if (Controls.Find("bMT_" + i.ToString(), true).FirstOrDefault() is Button bt) bt.BackColor = Color.White;
            }
        }
        void ScreenView(int nPage){
            ResetScreen();
            if (Controls.Find("bMT_" + nPage.ToString(), true).FirstOrDefault() is Button bt) bt.BackColor = Color.SeaShell;
            tcMTPAGE.SelectedIndex = nPage;
            bMMIChage = true;
        }
        void ScreenChange(object sender){
            BTN = sender as Button;
            DEF.MotorPage = Convert.ToInt16(BTN.Tag);
            ScreenView(DEF.MotorPage);
        }

        void ViewMotorTeaching() { COM_.ViewMotorSelect(); }

        void OffestPitchValue(object sender){
            BTN = (Button)sender;
            dValue = double.Parse(lbl_IncPitch.Text) + double.Parse(BTN.Tag.ToString());
            if (dValue < 0) dValue = 0;
            lbl_IncPitch.Text = dValue.ToString();
        }
        void SetIncPitch(object sender){
            LBL = (Label)sender;
            LBL.BackColor = Color.Lime;
            UTIL_.OPEN_KEYPAD_LABEL("RELATIVE MOVING PITCH", ref LBL, true);
            LBL.BackColor = Color.White;
        }

        void MotorStop(object sender){
            if (DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.INITIAL || DATA_.bMF || CHK_USE_INC.Checked) return;
            UCL_JOG uJOG = (UCL_JOG)sender;
            LAB_.MTSSTOP(uJOG.NumMT, "FormManual -> OnMouseUp_Click");
        }
        double GetJogSpeed(int m){
            double dSpd = 0;
            if (rbnJogSpd_High.Checked) dSpd = DATA_.mtSoftData[m].JOG_HIGH_SPD;
            if (rbnJogSpd_Middle.Checked) dSpd = DATA_.mtSoftData[m].JOG_MIDDLE_SPD;
            if (rbnJogSpd_Low.Checked) dSpd = DATA_.mtSoftData[m].JOG_LOW_SPD;
            return dSpd;
        }
        void MotorCcw(object sender){
            if (DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.INITIAL || DATA_.bMF) return;
            UCL_JOG uJOG = (UCL_JOG)sender;
            double dSpd = GetJogSpeed(uJOG.NumMT);
            dValue = Math.Abs(double.Parse(lbl_IncPitch.Text));

            if (CHK_USE_INC.Checked) LAB_.MT_PITCH_CCW(uJOG.NumMT, dSpd, dValue);
            else LAB_.MT_JOG_CCW(uJOG.NumMT, dSpd);
        }
        void MotorCw(object sender){
            if (DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.INITIAL || DATA_.bMF) return;
            UCL_JOG uJOG = (UCL_JOG)sender;
            double dSpd = GetJogSpeed(uJOG.NumMT);
            dValue = Math.Abs(double.Parse(lbl_IncPitch.Text));

            if (CHK_USE_INC.Checked) LAB_.MT_PITCH_CW(uJOG.NumMT, dSpd, dValue);
            else LAB_.MT_JOG_CW(uJOG.NumMT, dSpd);
        }

        void SelectMoter(object sender){
            RBTN = (RadioButton)sender;

            if (RBTN.Name == "SelectMapBlock1" || RBTN.Name == "SelectStage1"){
                mtStage = M.Table1;
                nStage = (int)eMAP_BLOCK.STAGE1;
                LBL_MAPBLOCK.Text = "맵-블록 테이블 Y1";
                LBL_STAGE.Text = "맵-블록 테이블 Y1";
                SelectMapBlock1.Checked = true;
                SelectStage1.Checked = true;
                SelectMapBlock1.ForeColor = Color.Lime;
                SelectMapBlock2.ForeColor = Color.White;
                pStage = RBTN.TabIndex;
            }
            if (RBTN.Name == "SelectMapBlock2" || RBTN.Name == "SelectStage2"){
                mtStage = M.Table2;
                nStage = (int)eMAP_BLOCK.STAGE2;
                LBL_MAPBLOCK.Text = "맵-블록 테이블 Y2";
                LBL_STAGE.Text = "맵-블록 테이블 Y2";
                SelectMapBlock2.Checked = true;
                SelectStage2.Checked = true;
                SelectMapBlock1.ForeColor = Color.White;
                SelectMapBlock2.ForeColor = Color.Lime;
                pStage = RBTN.TabIndex;
            }

            if (RBTN.Name == "SelectX1" || RBTN.Name == "SelectHD1"){
                mtHead = M.TRIGGER1;
                pHD = RBTN.TabIndex;
                mtPkTh = M.X1T;
                nHD = (int)eHD.HD1;
                LBL_HEAD.Text = "헤드 X1";
                SelectX1.Checked = true;
                SelectHD1.Checked = true;

                SelectX1.ForeColor = Color.Lime;
                SelectX2.ForeColor = Color.White;
            }
            if (RBTN.Name == "SelectX2" || RBTN.Name == "SelectHD2"){
                mtHead = M.TRIGGER2;
                pHD = RBTN.TabIndex;
                mtPkTh = M.X2T;
                nHD = (int)eHD.HD2;
                LBL_HEAD.Text = "헤드 X2";
                SelectX2.Checked = true;
                SelectHD2.Checked = true;

                SelectX1.ForeColor = Color.White;
                SelectX2.ForeColor = Color.Lime;
            }

            if (RBTN.Name == "SelectPkr12"){
                pPK = RBTN.TabIndex;
                SelectPkr12.ForeColor = Color.Lime;
                SelectPkr34.ForeColor = Color.White;
                SelectPkr56.ForeColor = Color.White;
            }
            if (RBTN.Name == "SelectPkr34"){
                pPK = RBTN.TabIndex;
                SelectPkr12.ForeColor = Color.White;
                SelectPkr34.ForeColor = Color.Lime;
                SelectPkr56.ForeColor = Color.White;
            }
            if (RBTN.Name == "SelectPkr56"){
                pPK = RBTN.TabIndex;
                SelectPkr12.ForeColor = Color.White;
                SelectPkr34.ForeColor = Color.White;
                SelectPkr56.ForeColor = Color.Lime;
            }
            if (mtHead == M.TRIGGER1) mtPk = M.HD1_PK[pPK];
            else mtPk = M.HD2_PK[pPK];

            if (RBTN.Name == "SelectGoodTrayTransfer1"){
                mtTrayFeeder = M.TrayFeeder1;
                nTray = (int)eTRAY.GOOD1;
                pTray = RBTN.TabIndex;
                LBL_TRAY_TRANSFER.Text = "OK 트레이 Y1";
                SelectGoodTrayTransfer1.Checked = true;
            }
            if (RBTN.Name == "SelectGoodTrayTransfer2"){
                mtTrayFeeder = M.TrayFeeder2;
                nTray = (int)eTRAY.GOOD2;
                pTray = RBTN.TabIndex;
                LBL_TRAY_TRANSFER.Text = "OK 트레이 Y2";
                SelectGoodTrayTransfer2.Checked = true;
            }
            if (RBTN.Name == "SelectReworkTrayTransfer"){
                mtTrayFeeder = M.TrayFeeder3;
                nTray = (int)eTRAY.REWORK;
                pTray = RBTN.TabIndex;
                LBL_TRAY_TRANSFER.Text = "REWORK 트레이 Y";
                SelectReworkTrayTransfer.Checked = true;
            }

            SetMTData();
            InfoDataGridView();
            ReadPara();
        }
        void SetMTData(){
            GT_MarkCamMapBlockTeaching.Text = "[상부 카메라] " + DATA_.PosName[M.TopVisionX, P.TopCam_Pallet[nStage]];
            gbxTopCamTeaching.Text = "[상부 카메라]" + ETC.NewLine + DATA_.PosName[M.TopVisionX, P.TopCam_Pallet[nStage]];
            if (SelectHD1.Checked){
                GT_HeadMapBlockTeaching.Text    = "[헤드 X1번  카메라] " + DATA_.PosName[M.TRIGGER1, P.HD_PIC[nStage]];
                GT_HeadTrayTeaching.Text        = "[헤드 X1번  카메라] " + DATA_.PosName[M.TRIGGER1, P.HD_PLC[nTray]];
                gbxTrayTeaching.Text            = "[헤드1 카메라]" + ETC.NewLine + DATA_.PosName[M.TRIGGER1, P.HD_PLC[nTray]];
                gbxZigLocation.Text             = "ZIG LOCATION  [헤드1 카메라 " + ((eMAP_BLOCK)DATA_.prMACHINE[CP.ZigAttachStage]).ToString() + " 지그 중심 위치]";
            }
            else{
                GT_HeadMapBlockTeaching.Text    = "[헤드 X2번  카메라] " + DATA_.PosName[M.TRIGGER1, P.HD_PIC[nHD]];
                GT_HeadTrayTeaching.Text        = "[헤드 X2번  카메라] " + DATA_.PosName[M.TRIGGER1, P.HD_PLC[nTray]];
                gbxTrayTeaching.Text            = "[헤드2 카메라]" + ETC.NewLine + DATA_.PosName[M.TRIGGER1, P.HD_PLC[nTray]];
                gbxZigLocation.Text             = "ZIG LOCATION  [헤드2 카메라 " + ((eMAP_BLOCK)DATA_.prMACHINE[CP.ZigAttachStage]).ToString() + " 지그 중심 위치]";
            }

            CovMK_X.TEXT = DATA_.MtName[M.TopVisionX];
            CovMK_Z.TEXT = DATA_.MtName[M.TopVisionZ];

            uMappingTable.NumMT = mtStage;
            uMappingTable.TEXT = DATA_.MtName[mtStage];
            CovPALLET.NumMT = mtStage;
            CovPALLET.TEXT = DATA_.MtName[mtStage];

            uHeadX.NumMT = mtHead;
            uHeadX.TEXT = DATA_.MtName[mtHead];
            uHeadPkrZ.NumMT = mtPk;
            uHeadPkrZ.TEXT = DATA_.MtName[mtPk];
            uHeadT.NumMT = mtPkTh;
            uHeadT.TEXT = DATA_.MtName[mtPkTh];

            CovHD.NumMT = mtHead;
            CovHD.TEXT = DATA_.MtName[mtHead];
            CovTRAY.NumMT = mtTrayFeeder;
            CovTRAY.TEXT = DATA_.MtName[mtTrayFeeder];

            LBL_MARK_X_POS.TabIndex = M.TopVisionX;
            LBL_MARK_Z_POS.TabIndex = M.TopVisionZ;
            lblTopCamXPos.TabIndex = M.TopVisionX;
            lblTopCamZPos.TabIndex = M.TopVisionZ;
            lblMapBlockYPos.TabIndex = mtStage;
            LBL_MB_Y_POS.TabIndex = mtStage;
            LBL_MB_Y_POS1.TabIndex = mtStage;
            LBL_HEAD_X_POS.TabIndex = mtHead;
            LBL_HEAD_X_POS1.TabIndex = mtHead;
            LBL_TRAY_Y_POS.TabIndex = mtTrayFeeder;
            lbTrayXPos.TabIndex = mtHead;
            lbTrayYPos.TabIndex = mtTrayFeeder;
            lbTrayDownX.TabIndex = mtHead;
            lbTrayDownY.TabIndex = mtTrayFeeder;

            if (mtTrayFeeder == M.TrayFeeder1 || mtTrayFeeder == M.TrayFeeder2) TrayUnloadingPos.Enabled = true;
            else TrayUnloadingPos.Enabled = false;
        }
#endregion

        void InfoDataGridView(){
            UTIL_.PosGrid(dgvCassetteYZ, M.ElvY, M.ElvZ, P.MAGZINE, ref nValue);
            UTIL_.PosGrid(dgvGripperX, M.GrpX, P.GRIPPER, ref nValue);
            UTIL_.PosGrid(dgvBarcode, M.Barcode, P.BARCODE, ref nValue);
            UTIL_.PosGrid(dgvStripPkrXZ, M.StripPkX, M.StripPkZ, P.STRIP_PK, ref nValue);
#if _NSS3300
            UTIL_.PosGridOption1(dgvRail, M.Rail, P.RAIL, ref nValue);
#else
            UTIL_.PosGridOption(dgvRail, M.RailF, M.RailR, P.RAIL, ref nValue);
            UTIL_.PosGrid(dgvPreAlignZ, M.PreAlign, P.PRE_AIGN, ref nValue);
#endif
            UTIL_.PosGrid(dgvUnitPkrXZ, M.UnitPkX, M.UnitPkZ, P.UNIT_PK, ref nValue);
            UTIL_.PosGrid(dgvMappingTable, mtStage, P.DRY_TABLE, ref nValue);
            UTIL_.PosGridOption(dgvMarkVision, M.TopVisionX, M.TopVisionZ, P.TOP_CAM, ref nValue);
            UTIL_.PosGridOption(dgvPRSVision, M.BtnVisionY, M.BtnVisionZ, P.BTM_CAM, ref nValue);
            UTIL_.PosGrid(dgvHeadX, mtHead, P.HD, ref nValue);
            UTIL_.PosGrid(dgvPkrZ, mtPk, P.HD_PK, ref nValue);
            UTIL_.PosGrid(dgvPkrTh, mtPkTh, P.HD_TH, ref nValue);
            UTIL_.PosGrid(dgvGTRAY_TRANSFER_1, M.TrayFeeder1, P.GOOD_TRAY_FEEDER, ref nValue);
            UTIL_.PosGrid(dgvGTRAY_TRANSFER_2, M.TrayFeeder2, P.GOOD_TRAY_FEEDER, ref nValue);
            UTIL_.PosGrid(dgvRTRAY_TRANSFER, M.TrayFeeder3, P.REWORK_TRAY_FEEDER, ref nValue);
            UTIL_.PosGrid(dgvTrayPkrXZ, M.TrayPickerX, M.TrayPickerZ, P.TRAY_PK, ref nValue);
            UTIL_.PosGrid(dgvEmptyTrayLift, M.EmptyElv, P.EMPTY, ref nValue);
        }
        void ReadPara(){
            COM_.SetGridData(dgvCassetteYZ, M.ElvY, M.ElvZ);
            COM_.SetGridData(dgvGripperX, M.GrpX);
            COM_.SetGridData(dgvBarcode, M.Barcode);
            COM_.SetGridData(dgvStripPkrXZ, M.StripPkX, M.StripPkZ);
#if _NSS3300
            COM_.SetGridData(dgvRail, M.Rail);
#else
            COM_.SetGridData(dgvRail, M.RailF, M.RailR);
            COM_.SetGridData(dgvPreAlignZ, M.PreAlign);
#endif

            COM_.SetGridData(dgvUnitPkrXZ, M.UnitPkX, M.UnitPkZ);
            COM_.SetGridData(dgvMappingTable, mtStage);
            COM_.SetGridData(dgvMarkVision, M.TopVisionX, M.TopVisionZ);
            COM_.SetGridData(dgvPRSVision, M.BtnVisionY, M.BtnVisionZ);
            COM_.SetGridData(dgvHeadX, mtHead);
            COM_.SetGridData(dgvPkrZ, mtPk);
            COM_.SetGridData(dgvPkrTh, mtPkTh);
            COM_.SetGridData(dgvGTRAY_TRANSFER_1, M.TrayFeeder1);
            COM_.SetGridData(dgvGTRAY_TRANSFER_2, M.TrayFeeder2);
            COM_.SetGridData(dgvRTRAY_TRANSFER, M.TrayFeeder3);
            COM_.SetGridData(dgvTrayPkrXZ, M.TrayPickerX, M.TrayPickerZ);
            COM_.SetGridData(dgvEmptyTrayLift, M.EmptyElv);

            ChkGripperLoadingPos.Checked = DATA_.prMODEL[RP.UseStripLoadngPos] == (int)ePARA.RECIPE ? true : false;

            //cleaner para
            for (int i = 0; i < CleanMode.Length; i++){
                CleanMode[i].SelectedIndex = DATA_.CleanData.MODE[i];
                if (DATA_.CleanData.MODE[i] == 0)   CleanRepeat[i].Text = "";
                else                                CleanRepeat[i].Text = DATA_.CleanData.COUNTER[i].ToString();
                CleanRepeat[i].Text = DATA_.CleanData.COUNTER[i].ToString();
            }
            //worked cleaner para
            for (int i = 0; i < WorkedCleanMode.Length; i++){
                WorkedCleanMode[i].SelectedIndex = DATA_.WorkedCleanData.MODE[i];
                if (DATA_.WorkedCleanData.MODE[i] == 0) WorkedCleanRepeat[i].Text = "";
                else                                    WorkedCleanRepeat[i].Text = DATA_.WorkedCleanData.COUNTER[i].ToString();
                WorkedCleanRepeat[i].Text = DATA_.WorkedCleanData.COUNTER[i].ToString();
            }

            //top camera
            LBL_MARK_X_POS.Text     = DATA_.mtDATA[M.TopVisionX, P.TopCam_Pallet[pStage]].Pos.ToString();
            LBL_MARK_Z_POS.Text     = DATA_.mtDATA[M.TopVisionZ, P.TopCam_Pallet[pStage]].Pos.ToString();
            LBL_MB_Y_POS.Text       = DATA_.mtDATA[mtStage, P.TopVision_Unit].Pos.ToString();
            lblTopCamXPos.Text      = DATA_.mtDATA[M.TopVisionX, P.TopCam_Pallet[pStage]].Pos.ToString();
            lblTopCamZPos.Text      = DATA_.mtDATA[M.TopVisionZ, P.TopCam_Pallet[pStage]].Pos.ToString();
            lblMapBlockYPos.Text    = DATA_.mtDATA[mtStage, P.TopVision_Unit].Pos.ToString();
            //head pick-up
            LBL_HEAD_X_POS.Text     = DATA_.mtDATA[mtHead, P.HD_PIC[pStage]].Pos.ToString();
            LBL_MB_Y_POS1.Text      = DATA_.mtDATA[mtStage, P.HD_Pallet[pHD]].Pos.ToString();
            //head place
            LBL_HEAD_X_POS1.Text    = DATA_.mtDATA[mtHead, P.HD_PLC[pTray]].Pos.ToString();
            LBL_TRAY_Y_POS.Text     = DATA_.mtDATA[mtTrayFeeder, P.Tray_Place[pHD]].Pos.ToString();

            lbTrayXPos.Text         = DATA_.mtDATA[mtHead, P.HD_PLC[pTray]].Pos.ToString();
            lbTrayYPos.Text         = DATA_.mtDATA[mtTrayFeeder, P.Tray_Place[pHD]].Pos.ToString();

            lbTrayDownX.Text        = "0";
            lbTrayDownY.Text        = "0";

            for (int i = 0; i < lbMC.Length; i++){
                lbMC[i].Text = DATA_.prMACHINE[CP.MTPara[i]].ToString();
            }
            for (int i = 0; i < lbMD.Length; i++){
                lbMD[i].Text = DATA_.prMODEL[RP.MTPara[i]].ToString();
            }

            lbPlaceCheckDelay.Text = DATA_.prMACHINE[CP.PlaceCheckDalay].ToString();
            lbRejectBlow.Text = DATA_.prMACHINE[CP.RejectBlowDelay].ToString();

            if (DATA_.prMACHINE[CP.ZigAttachStage] == (int)eMAP_BLOCK.STAGE1)   rbnAttachZigStage1.Checked = true;
            else                                                                rbnAttachZigStage2.Checked = true;

            lbZigX1Pos.Text = DATA_.prMACHINE[CP.Zig1stPosX[nHD]].ToString();
            lbZigY1Pos.Text = DATA_.prMACHINE[CP.Zig1stPosY[nHD]].ToString();
            lbZigX2Pos.Text = DATA_.prMACHINE[CP.Zig2ndPosX[nHD]].ToString();
            lbZigY2Pos.Text = DATA_.prMACHINE[CP.Zig2ndPosY[nHD]].ToString();
            lbZigX3Pos.Text = DATA_.prMACHINE[CP.ZigLastPosX[nHD]].ToString();
            lbZigY3Pos.Text = DATA_.prMACHINE[CP.ZigLastPosY[nHD]].ToString();

        }
        void InfoComboBox(){
            cbxTRAY_X.Items.Clear();
            cbxTRAY_Y.Items.Clear();
            for (int i = 0; i < DATA_.prMODEL[RP.TrayCntX]; i++) cbxTRAY_X.Items.Add(i + 1);
            for (int i = 0; i < DATA_.prMODEL[RP.TrayCntY]; i++) cbxTRAY_Y.Items.Add(i + 1);
            if (DATA_.prMODEL[RP.TrayCntX] > 0) cbxTRAY_X.SelectedIndex = 0;
            if (DATA_.prMODEL[RP.TrayCntY] > 0) cbxTRAY_Y.SelectedIndex = 0;

            cbMapBlock_GroupX.Items.Clear();
            cbMapBlock_GroupY.Items.Clear();
            cbMapBlock_UnitX.Items.Clear();
            cbMapBlock_UnitY.Items.Clear();

            for (int i = 0; i < DATA_.prMODEL[RP.GroupX[pStage]]; i++) cbMapBlock_GroupX.Items.Add(i + 1);
            for (int i = 0; i < DATA_.prMODEL[RP.GroupY[pStage]]; i++) cbMapBlock_GroupY.Items.Add(i + 1);
            for (int i = 0; i < DEF.Utx[pStage]; i++) cbMapBlock_UnitX.Items.Add(i + 1);
            for (int i = 0; i < DEF.Uty[pStage]; i++) cbMapBlock_UnitY.Items.Add(i + 1);

            if (DATA_.prMODEL[RP.GroupX[pStage]] > 0) cbMapBlock_GroupX.SelectedIndex = 0;
            if (DATA_.prMODEL[RP.GroupY[pStage]] > 0) cbMapBlock_GroupY.SelectedIndex = 0;
            if (DEF.Utx[pStage] > 0) cbMapBlock_UnitX.SelectedIndex = 0;
            if (DEF.Uty[pStage] > 0) cbMapBlock_UnitY.SelectedIndex = 0;
        }

        private void JigPosData_Click(object sender, EventArgs e){
            LBL = (Label)sender;
            DEF.TenKeyOption = true;

            if (LBL.Name == "lbZigX1Pos") DATA_.IsSTRING[S.MotorMessage] = DATA_.MCParaName[CP.Zig1stPosX[nHD]];
            if (LBL.Name == "lbZigY1Pos") DATA_.IsSTRING[S.MotorMessage] = DATA_.MCParaName[CP.Zig1stPosY[nHD]];
            if (LBL.Name == "lbZigX2Pos") DATA_.IsSTRING[S.MotorMessage] = DATA_.MCParaName[CP.Zig2ndPosX[nHD]];
            if (LBL.Name == "lbZigY2Pos") DATA_.IsSTRING[S.MotorMessage] = DATA_.MCParaName[CP.Zig2ndPosY[nHD]];
            if (LBL.Name == "lbZigX3Pos") DATA_.IsSTRING[S.MotorMessage] = DATA_.MCParaName[CP.ZigLastPosX[nHD]];
            if (LBL.Name == "lbZigY3Pos") DATA_.IsSTRING[S.MotorMessage] = DATA_.MCParaName[CP.ZigLastPosY[nHD]];
            UTIL_.OPEN_KEYPAD_LABEL(DATA_.IsSTRING[S.MotorMessage], ref LBL, DEF.TenKeyOption);
        }

        private void ParaData_Click(object sender, EventArgs e){
            LBL = (Label)sender;
            DEF.TenKeyOption = true;
            if (LBL.Tag.ToString() == "MC") DATA_.IsSTRING[S.MotorMessage] = DATA_.MCParaName[LBL.TabIndex];
            else                            DATA_.IsSTRING[S.MotorMessage] = DATA_.MDParaName[LBL.TabIndex];
            UTIL_.OPEN_KEYPAD_LABEL(DATA_.IsSTRING[S.MotorMessage], ref LBL, DEF.TenKeyOption);
        }

        private void MotorData_Click(object sender, EventArgs e){
            LBL = (Label)sender;
            DEF.TenKeyOption = true;
            DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[LBL.TabIndex] + " Position Value Cange !";
            UTIL_.OPEN_KEYPAD_LABEL(DATA_.IsSTRING[S.MotorMessage], ref LBL, DEF.TenKeyOption);
        }

        private void CleanerData_Click(object sender, EventArgs e){
            LBL = sender as Label;
            DEF.TenKeyOption = true;
            /*if (LBL.Tag.ToString() == "COUNT")*/
            DATA_.IsSTRING[S.MotorMessage] = groupBox7.Text + " " + LBL.Text;
            UTIL_.OPEN_KEYPAD_LABEL(DATA_.IsSTRING[S.MotorMessage], ref LBL, DEF.TenKeyOption);
        }
        private void WorkedCleanerData_Click(object sender, EventArgs e){
            LBL = sender as Label;
            DEF.TenKeyOption = true;
            DATA_.IsSTRING[S.MotorMessage] = groupBox3.Text + " " + LBL.Text;
            UTIL_.OPEN_KEYPAD_LABEL(DATA_.IsSTRING[S.MotorMessage], ref LBL, DEF.TenKeyOption);
        }
        private void TagOption_Click(object sender, EventArgs e){
            LBL = sender as Label;
            DEF.TenKeyOption = true;
            DATA_.IsSTRING[S.MotorMessage] = LBL.Tag.ToString();
            UTIL_.OPEN_KEYPAD_LABEL(DATA_.IsSTRING[S.MotorMessage], ref LBL, DEF.TenKeyOption);
        }

        bool GetCellClickNumber(DataGridView g, int row, int col){
            if (DATA_.eMCStatus == eMachineStatus.AUTO || row < 0 || col <= 1){
                UTIL_.CLEAR_GRID_SELECTED(ref g);
                return false;
            }
            UTIL_.GET_GRID_NUMBER(g, ref nCol, ref nRow);
            return true;
        }
        private void dgvCassetteYZ_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
            int[] mt = { M.ElvY, M.ElvZ };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);

            if (nCol == 2 || nCol == 4) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];
            else if (nCol == 3 || nCol == 5 || nCol == 6) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[1]] + " axis - " + DATA_.PosName[mt[1], nIndex];
            else if (nCol == 7) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " / " + DATA_.MtName[mt[1]] + " aixs - " + DATA_.PosName[mt[0], nIndex];
            else return;

            if (nCol == 2 || nCol == 3) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += " 현재 위치값으로 변경 하시겠습니까 ?" + ETC.CrLf + "Set current position?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    COM_.SetGridData(dgv, nRow, mt[0], 0);
            }
            else if (nCol == 5){
                DATA_.IsSTRING[S.MotorMessage] += " 현재 위치값으로 변경 하시겠습니까 ?" + ETC.CrLf + "Set current position?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    COM_.SetGridData(dgv, nRow, mt[1], 1);
            }
            else{
                DATA_.IsSTRING[S.MotorMessage] += " 이송 하시겠습니까 ?" + ETC.CrLf + "Motion move ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    if (nCol == 7) DATA_.iMANUAL.Option = true;
                    else DATA_.iMANUAL.Option = false;
                    DATA_.iMANUAL.int_1 = 0; //첫번째 슬롯 위치로 

                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.MGZRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.MGZLoading;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.MGZUnloading;
                    if (nRow == 3) DATA_.iMANUAL.RunManual = ManualNumber.MGZSlot;
                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
        }
        private void dgvRail_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
#if _NSS3300
            int[] mt = { M.Rail };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);
            DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];
            if (nCol == 2) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "SET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    dgv[2, nRow].Value = LAB_.GET_ACTPOS(mt[0]);
            }
            else if (nCol == 6){
                DATA_.IsSTRING[S.MotorMessage] += "로 이송하시겠습니까?" + ETC.CrLf + "MOTION MOVING ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    DATA_.iMANUAL.Option = false;
                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.RailRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.RailLoading;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.RailWork;
                    if (nRow == 3) DATA_.iMANUAL.RunManual = ManualNumber.RailPicOpen;

                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
#else
            int[] mt = { M.RailF, M.RailR };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);
            if (nCol == 2 || nCol == 4 || nCol == 6) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];
            else if (nCol == 3 || nCol == 5) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[1]] + " axis - " + DATA_.PosName[mt[1], nIndex];
            else if (nCol == 7) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " / " + DATA_.MtName[mt[1]] + " aixs - " + DATA_.PosName[mt[0], nIndex];
            else return;

            if (nCol == 2 || nCol == 3) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += " 현재 위치값으로 변경 하시겠습니까 ?" + ETC.CrLf + "Set current position?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    COM_.SetGridData(dgv, nRow, mt[0], 0);
            }
            else if (nCol == 5){
                DATA_.IsSTRING[S.MotorMessage] += " 현재 위치값으로 변경 하시겠습니까 ?" + ETC.CrLf + "Set current position?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    COM_.SetGridData(dgv, nRow, mt[1], 1);
            }
            else{
                DATA_.IsSTRING[S.MotorMessage] += " 이송 하시겠습니까 ?" + ETC.CrLf + "Motion move ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    DATA_.iMANUAL.Option = true;

                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.RailRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.RailLoading;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.RailWork;
                    if (nRow == 3) DATA_.iMANUAL.RunManual = ManualNumber.RailPicOpen;

                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
#endif
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
        }
        private void dgvGripperX_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
            int[] mt = { M.GrpX };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);

            DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];

            if (nCol == 2) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 3){
                DATA_.IsSTRING[S.MotorMessage] += "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "SET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    dgv[2, nRow].Value = LAB_.GET_ACTPOS(mt[0]);
            }
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += "로 이송하시겠습니까?" + ETC.CrLf + "MOTION MOVING ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    DATA_.iMANUAL.Option = false;
                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.GripperRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.GripperCatch;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.GripperBarcodeReading;
                    if (nRow == 3) DATA_.iMANUAL.RunManual = ManualNumber.GripperLoading;
                    if (nRow == 4) DATA_.iMANUAL.RunManual = ManualNumber.GripperStripPic;

                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
        }
        private void dgvBarcode_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
            int[] mt = { M.Barcode };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);

            DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];

            if (nCol == 2) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 3){
                DATA_.IsSTRING[S.MotorMessage] += "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "SET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    dgv[2, nRow].Value = LAB_.GET_ACTPOS(mt[0]);
            }
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += "로 이송하시겠습니까?" + ETC.CrLf + "MOTION MOVING ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.BarcodeRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.BarcodeReading;

                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
        }
        private void dgvStripPkrXZ_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
            int[] mt = { M.StripPkX, M.StripPkZ };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);

            if (nCol == 2 || nCol == 4 || nCol == 6) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];
            else if (nCol == 3 || nCol == 5) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[1]] + " axis - " + DATA_.PosName[mt[1], nIndex];
            else if (nCol == 7) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " / " + DATA_.MtName[mt[1]] + " aixs - " + DATA_.PosName[mt[0], nIndex];
            else return;

            if (nCol == 2 || nCol == 3) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += " 현재 위치값으로 변경 하시겠습니까 ?" + ETC.CrLf + "Set current position?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    COM_.SetGridData(dgv, nRow, mt[0], 0);
            }
            else if (nCol == 5){
                DATA_.IsSTRING[S.MotorMessage] += " 현재 위치값으로 변경 하시겠습니까 ?" + ETC.CrLf + "Set current position?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    COM_.SetGridData(dgv, nRow, mt[1], 1);
            }
            else{
                DATA_.IsSTRING[S.MotorMessage] += " 이송 하시겠습니까 ?" + ETC.CrLf + "Motion move ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    if (nCol == 7) DATA_.iMANUAL.Option = true;
                    else DATA_.iMANUAL.Option = false;

                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.StripPkRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.StripPkPic;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.StripPkPlc;
                    if (nRow == 3) DATA_.iMANUAL.RunManual = ManualNumber.StripPkFristAlign;
                    if (nRow == 4) DATA_.iMANUAL.RunManual = ManualNumber.StripPkSecondAlign;

                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
        }
        private void dgvPreAlignZ_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
#if _NSS3300
#else
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
            int[] mt = { M.PreAlign };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);

            DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];

            if (nCol == 2) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 3){
                DATA_.IsSTRING[S.MotorMessage] += "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "SET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    dgv[2, nRow].Value = LAB_.GET_ACTPOS(mt[0]);
            }
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += "로 이송하시겠습니까?" + ETC.CrLf + "MOTION MOVING ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.PreAlignRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.PreAlignFirst;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.PreAlignSecond;

                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
#endif
        }
        private void dgvUnitPkrXZ_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
            int[] mt = { M.UnitPkX, M.UnitPkZ };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);

            if (nCol == 2 || nCol == 4 || nCol == 6) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];
            else if (nCol == 3 || nCol == 5) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[1]] + " axis - " + DATA_.PosName[mt[1], nIndex];
            else if (nCol == 7) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " / " + DATA_.MtName[mt[1]] + " aixs - " + DATA_.PosName[mt[0], nIndex];
            else return;

            if (nCol == 2 || nCol == 3) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += " 현재 위치값으로 변경 하시겠습니까 ?" + ETC.CrLf + "Set current position?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    COM_.SetGridData(dgv, nRow, mt[0], 0);
            }
            else if (nCol == 5){
                DATA_.IsSTRING[S.MotorMessage] += " 현재 위치값으로 변경 하시겠습니까 ?" + ETC.CrLf + "Set current position?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    COM_.SetGridData(dgv, nRow, mt[1], 1);
            }
            else{
                DATA_.IsSTRING[S.MotorMessage] += " 이송 하시겠습니까 ?" + ETC.CrLf + "Motion move ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    if (nCol == 7) DATA_.iMANUAL.Option = true;
                    else DATA_.iMANUAL.Option = false;

                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.UnitPkRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.UnitPkPic;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.UnitPkScrap1;
                    if (nRow == 3) DATA_.iMANUAL.RunManual = ManualNumber.UnitPkScrap2;
                    if (nRow == 4) DATA_.iMANUAL.RunManual = ManualNumber.UnitPkBrush;
                    if (nRow == 5) DATA_.iMANUAL.RunManual = ManualNumber.UnitPkCleaner;
                    if (nRow == 6) DATA_.iMANUAL.RunManual = ManualNumber.UnitPkAirshower;
                    if (nRow == 7) DATA_.iMANUAL.RunManual = ManualNumber.UnitPkStage1;
                    if (nRow == 8) DATA_.iMANUAL.RunManual = ManualNumber.UnitPkStage2;

                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
        }
        private void dgvMappingTable_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
            int[] mt = { mtStage };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);

            DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];

            if (nCol == 2) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 3){
                DATA_.IsSTRING[S.MotorMessage] += "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "SET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    dgv[2, nRow].Value = LAB_.GET_ACTPOS(mt[0]);
            }
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += "로 이송하시겠습니까?" + ETC.CrLf + "MOTION MOVING ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    DATA_.iMANUAL.iMT1 = mtStage;
                    DATA_.iMANUAL.int_2 = mtStage == M.Table1 ? (int)eMAP_BLOCK.STAGE1 : (int)eMAP_BLOCK.STAGE2;
                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.StageRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.StageReceive;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.StageAirshowerStart;
                    if (nRow == 3) DATA_.iMANUAL.RunManual = ManualNumber.StageTopCamView;
                    if (nRow == 4) DATA_.iMANUAL.RunManual = ManualNumber.StageHDCam1View;
                    if (nRow == 5) DATA_.iMANUAL.RunManual = ManualNumber.StageHDCam2View;

                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
        }
        private void dgvMarkVision_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
            int[] mt = { M.TopVisionX, M.TopVisionZ };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);

            if (nCol == 2 || nCol == 4 || nCol == 6) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];
            else if (nCol == 3 || nCol == 5) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[1]] + " axis - " + DATA_.PosName[mt[1], nIndex];
            else if (nCol == 7) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " / " + DATA_.MtName[mt[1]] + " aixs - " + DATA_.PosName[mt[0], nIndex];
            else return;

            if (nCol == 2 || nCol == 3) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += " 현재 위치값으로 변경 하시겠습니까 ?" + ETC.CrLf + "Set current position?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    COM_.SetGridData(dgv, nRow, mt[0], 0);
            }
            else if (nCol == 5){
                DATA_.IsSTRING[S.MotorMessage] += " 현재 위치값으로 변경 하시겠습니까 ?" + ETC.CrLf + "Set current position?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    COM_.SetGridData(dgv, nRow, mt[1], 1);
            }
            else{
                DATA_.IsSTRING[S.MotorMessage] += " 이송 하시겠습니까 ?" + ETC.CrLf + "Motion move ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    if (nCol == 7) DATA_.iMANUAL.Option = true;
                    else DATA_.iMANUAL.Option = false;

                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.TopCamRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.TopCamStage1View;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.TopCamStage2View;

                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
        }
        private void dgvPRSVision_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
            int[] mt = { M.BtnVisionY, M.BtnVisionZ };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);

            if (nCol == 2 || nCol == 4 || nCol == 6) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];
            else if (nCol == 3 || nCol == 5) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[1]] + " axis - " + DATA_.PosName[mt[1], nIndex];
            else if (nCol == 7) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " / " + DATA_.MtName[mt[1]] + " aixs - " + DATA_.PosName[mt[0], nIndex];
            else return;

            if (nCol == 2 || nCol == 3) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += " 현재 위치값으로 변경 하시겠습니까 ?" + ETC.CrLf + "Set current position?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    COM_.SetGridData(dgv, nRow, mt[0], 0);
            }
            else if (nCol == 5){
                DATA_.IsSTRING[S.MotorMessage] += " 현재 위치값으로 변경 하시겠습니까 ?" + ETC.CrLf + "Set current position?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    COM_.SetGridData(dgv, nRow, mt[1], 1);
            }
            else{
                DATA_.IsSTRING[S.MotorMessage] += " 이송 하시겠습니까 ?" + ETC.CrLf + "Motion move ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    if (nCol == 7) DATA_.iMANUAL.Option = true;
                    else DATA_.iMANUAL.Option = false;

                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.BtmCamRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.BtmCamHD1CamCenter;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.BtmCamHD1PkCenter;
                    if (nRow == 3) DATA_.iMANUAL.RunManual = ManualNumber.BtmCamHD2CamCenter;
                    if (nRow == 4) DATA_.iMANUAL.RunManual = ManualNumber.BtmCamHD2PkCenter;

                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
        }
        private void dgvHeadX_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
            int[] mt = { mtHead };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);

            DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];

            if (nCol == 2) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 3){
                DATA_.IsSTRING[S.MotorMessage] += "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "SET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    dgv[2, nRow].Value = LAB_.GET_ACTPOS(mt[0]);
            }
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += "로 이송하시겠습니까?" + ETC.CrLf + "MOTION MOVING ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    DATA_.iMANUAL.iMT1 = mtHead;
                    DATA_.iMANUAL.int_1 = (int)ePK.PKR1;

                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.HDXRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.HDXCamCenter;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.HDXPkCenter;
                    if (nRow == 3) DATA_.iMANUAL.RunManual = ManualNumber.HDXReject;
                    if (nRow == 4) DATA_.iMANUAL.RunManual = ManualNumber.HDXStage1View;
                    if (nRow == 5) DATA_.iMANUAL.RunManual = ManualNumber.HDXStage2View;
                    if (nRow == 6) DATA_.iMANUAL.RunManual = ManualNumber.HDXGoodTray1View;
                    if (nRow == 7) DATA_.iMANUAL.RunManual = ManualNumber.HDXGoodTray2View;
                    if (nRow == 8) DATA_.iMANUAL.RunManual = ManualNumber.HDXReworkTrayView;

                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
        }
        private void dgvPkrZ_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
            int[] mt = { mtPk };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);

            DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];

            if (nCol == 2) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 3){
                DATA_.IsSTRING[S.MotorMessage] += "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "SET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    dgv[2, nRow].Value = LAB_.GET_ACTPOS(mt[0]);
            }
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += "로 이송하시겠습니까?" + ETC.CrLf + "MOTION MOVING ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    DATA_.iMANUAL.iMT1 = mtPk;

                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.PkZRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.PkOddZPic;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.PkEvenPic;
                    if (nRow == 3) DATA_.iMANUAL.RunManual = ManualNumber.PkOddZPlc;
                    if (nRow == 4) DATA_.iMANUAL.RunManual = ManualNumber.PkEvenPlc;

                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
        }
        private void dgvPkrTh_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
            int[] mt = { mtPkTh };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);

            DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];

            if (nCol == 2) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 3){
                DATA_.IsSTRING[S.MotorMessage] += "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "SET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    dgv[2, nRow].Value = LAB_.GET_ACTPOS(mt[0]);
            }
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += "로 이송하시겠습니까?" + ETC.CrLf + "MOTION MOVING ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    DATA_.iMANUAL.iMT1 = mtPkTh;
                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.PkThRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.PkPicTh;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.PkPlcTh;

                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
        }
        private void dgvGTRAY_TRANSFER_1_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
            int[] mt = { M.TrayFeeder1 };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);

            DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];

            if (nCol == 2) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 3){
                DATA_.IsSTRING[S.MotorMessage] += "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "SET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    dgv[2, nRow].Value = LAB_.GET_ACTPOS(mt[0]);
            }
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += "로 이송하시겠습니까?" + ETC.CrLf + "MOTION MOVING ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    DATA_.iMANUAL.iMT1 = M.TrayFeeder1;
                    DATA_.iMANUAL.int_1 = (int)eTRAY.GOOD1;

                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederLoading;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederStacker;
                    if (nRow == 3) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederConvULDStart;
                    if (nRow == 4) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederTrayPusherStart;
                    if (nRow == 5) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederConvUnloading;
                    if (nRow == 6) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederHD1Plc;
                    if (nRow == 7) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederHD2Plc;

                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
        }
        private void dgvGTRAY_TRANSFER_2_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
            int[] mt = { M.TrayFeeder2 };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);

            DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];

            if (nCol == 2) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 3){
                DATA_.IsSTRING[S.MotorMessage] += "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "SET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    dgv[2, nRow].Value = LAB_.GET_ACTPOS(mt[0]);
            }
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += "로 이송하시겠습니까?" + ETC.CrLf + "MOTION MOVING ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    DATA_.iMANUAL.iMT1 = M.TrayFeeder2;
                    DATA_.iMANUAL.int_1 = (int)eTRAY.GOOD2;

                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederLoading;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederStacker;
                    if (nRow == 3) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederConvULDStart;
                    if (nRow == 4) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederTrayPusherStart;
                    if (nRow == 5) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederConvUnloading;
                    if (nRow == 6) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederHD1Plc;
                    if (nRow == 7) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederHD2Plc;

                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
        }
        private void dgvRTRAY_TRANSFER_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
            int[] mt = { M.TrayFeeder3 };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);

            DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];

            if (nCol == 2) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 3){
                DATA_.IsSTRING[S.MotorMessage] += "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "SET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    dgv[2, nRow].Value = LAB_.GET_ACTPOS(mt[0]);
            }
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += "로 이송하시겠습니까?" + ETC.CrLf + "MOTION MOVING ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    DATA_.iMANUAL.iMT1 = M.TrayFeeder3;
                    DATA_.iMANUAL.int_1 = (int)eTRAY.REWORK;

                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederLoading;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederStacker;
                    if (nRow == 3) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederHD1Plc;
                    if (nRow == 4) DATA_.iMANUAL.RunManual = ManualNumber.TrayFeederHD2Plc;
                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
        }
        private void dgvTrayPkrXZ_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
            int[] mt = { M.TrayPickerX, M.TrayPickerZ };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);

            if (nCol == 2 || nCol == 4 || nCol == 6) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];
            else if (nCol == 3 || nCol == 5) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[1]] + " axis - " + DATA_.PosName[mt[1], nIndex];
            else if (nCol == 7) DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " / " + DATA_.MtName[mt[1]] + " aixs - " + DATA_.PosName[mt[0], nIndex];
            else return;

            if (nCol == 2 || nCol == 3) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += " 현재 위치값으로 변경 하시겠습니까 ?" + ETC.CrLf + "Set current position?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    COM_.SetGridData(dgv, nRow, mt[0], 0);
            }
            else if (nCol == 5){
                DATA_.IsSTRING[S.MotorMessage] += " 현재 위치값으로 변경 하시겠습니까 ?" + ETC.CrLf + "Set current position?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    COM_.SetGridData(dgv, nRow, mt[1], 1);
            }
            else{
                DATA_.IsSTRING[S.MotorMessage] += " 이송 하시겠습니까 ?" + ETC.CrLf + "Motion move ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    if (nCol == 7) DATA_.iMANUAL.Option = true;
                    else DATA_.iMANUAL.Option = false;

                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.TrayPkRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.TrayPkEmptyFeeder;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.TrayPkGoodFeeder;
                    if (nRow == 3) DATA_.iMANUAL.RunManual = ManualNumber.TrayPkReworkFeeder;

                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
        }
        private void dgvEmptyTrayLift_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            if (!GetCellClickNumber(dgv, e.RowIndex, e.ColumnIndex)) return;
            int[] mt = { M.EmptyElv };
            double[] GetPos = new double[mt.Length];
            UTIL_.GET_GRID_MOTOR_DATA(dgv, nRow, ref nIndex, ref GetPos);

            DATA_.IsSTRING[S.MotorMessage] = DATA_.MtName[mt[0]] + " axis - " + DATA_.PosName[mt[0], nIndex];

            if (nCol == 2) UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.MotorMessage], ref dgv, true);
            else if (nCol == 3){
                DATA_.IsSTRING[S.MotorMessage] += "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "SET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false))
                    dgv[2, nRow].Value = LAB_.GET_ACTPOS(mt[0]);
            }
            else if (nCol == 4){
                DATA_.IsSTRING[S.MotorMessage] += "로 이송하시겠습니까?" + ETC.CrLf + "MOTION MOVING ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.MotorMessage], false, false, false)){
                    if (nRow == 0) DATA_.iMANUAL.RunManual = ManualNumber.EmptyStackerRdy;
                    if (nRow == 1) DATA_.iMANUAL.RunManual = ManualNumber.EmptyStackerLoading;
                    if (nRow == 2) DATA_.iMANUAL.RunManual = ManualNumber.EmptyStackerLock;
                    if (nRow == 3) DATA_.iMANUAL.RunManual = ManualNumber.EmptyStackerWork;

                    COM_.RUN_MANUAL(DATA_.iMANUAL.RunManual, DATA_.IsSTRING[S.MotorMessage]);
                }
            }
            UTIL_.CLEAR_GRID_MOTOR_SELECTED(dgv, nRow, GetPos);
        }

        private void SetReferencePos_Click(object sender, EventArgs e){
            BTN = (Button)sender;
            if (BTN.Name == "btnSetRef_TopCamX") dRefCurPos_TopCam = LAB_.GET_ACTPOS(M.TopVisionX);
            if (BTN.Name == "btnSetRef_HeadX") dRefCurPos_HeadX = LAB_.GET_ACTPOS(CovHD.NumMT);
            if (BTN.Name == "btnSetRef_PalletY") dRefCurPos_PalletY = LAB_.GET_ACTPOS(CovPALLET.NumMT);
            if (BTN.Name == "btnSetRef_TrayY") dRefCurPos_TrayY = LAB_.GET_ACTPOS(CovTRAY.NumMT);
        }
        private void GET_SIMPLE_TEACHING_CURRENT_POS_Click(object sender, EventArgs e){
            BTN = (Button)sender;
            if (BTN.Name == "GET_TOP_CAM_FIRST_POSITION" || BTN.Name == "GetTopCamMapBlockFirstPocetCenter"){
                if (SelectStage1.Checked)       DATA_.iMANUAL.CMD = "상부 카메라 맵-블록 테이블1번 ";
                else if (SelectStage2.Checked)  DATA_.iMANUAL.CMD = "상부 카메라 맵-블록 테이블2번 ";
                else return;
                DATA_.iMANUAL.CMD += DATA_.MC_DIR == 0 ? "좌하단 포켓 중심 위치" : "우하단 포켓 중심 위치";
                DATA_.iMANUAL.CMD = DATA_.iMANUAL.CMD + "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "GET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.iMANUAL.CMD, false, false, false)){
                    LBL_MARK_X_POS.Text     = LAB_.GET_ACTPOS(M.TopVisionX).ToString();
                    LBL_MARK_Z_POS.Text     = LAB_.GET_ACTPOS(M.TopVisionZ).ToString();
                    LBL_MB_Y_POS.Text       = LAB_.GET_ACTPOS(CovPALLET.NumMT).ToString();

                    lblTopCamXPos.Text      = LAB_.GET_ACTPOS(M.TopVisionX).ToString();
                    lblTopCamZPos.Text      = LAB_.GET_ACTPOS(M.TopVisionZ).ToString();
                    lblMapBlockYPos.Text    = LAB_.GET_ACTPOS(CovPALLET.NumMT).ToString();
                }
            }
            if (BTN.Name == "GET_HEAD_MAPBLOCK_FIRST_POSITION"){
                if (SelectHD1.Checked && SelectStage1.Checked)      DATA_.iMANUAL.CMD = "헤드1 카메라 맵-블록1번 ";
                else if (SelectHD1.Checked && SelectStage2.Checked) DATA_.iMANUAL.CMD = "헤드1 카메라 맵-블록2번 ";
                else if (SelectHD2.Checked && SelectStage1.Checked) DATA_.iMANUAL.CMD = "헤드2 카메라 맵-블록1번 ";
                else if (SelectHD2.Checked && SelectStage2.Checked) DATA_.iMANUAL.CMD = "헤드2 카메라 맵-블록2번 ";
                else return;
                DATA_.iMANUAL.CMD += DATA_.MC_DIR == 0 ? "좌하단 포켓 중심 위치" : "우하단 포켓 중심 위치";
                DATA_.iMANUAL.CMD = DATA_.iMANUAL.CMD + "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "GET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.iMANUAL.CMD, false, false, false)){
                    LBL_HEAD_X_POS.Text = LAB_.GET_ACTPOS(CovHD.NumMT).ToString();
                    LBL_MB_Y_POS1.Text  = LAB_.GET_ACTPOS(CovPALLET.NumMT).ToString();
                }
            }
            if (BTN.Name == "GET_HEAD_TRAY_FIRST_POSITION" || BTN.Name == "GetTrayPlacePos"){
                if (SelectHD1.Checked && SelectGoodTrayTransfer1.Checked)       DATA_.iMANUAL.CMD = "헤드1 카메라 굿트레이 1번 ";
                else if (SelectHD1.Checked && SelectGoodTrayTransfer2.Checked)  DATA_.iMANUAL.CMD = "헤드1 카메라 굿트레이 2번 ";
                else if (SelectHD1.Checked && SelectReworkTrayTransfer.Checked) DATA_.iMANUAL.CMD = "헤드1 카메라 리워크 트레이 ";
                else if (SelectHD2.Checked && SelectGoodTrayTransfer1.Checked)  DATA_.iMANUAL.CMD = "헤드2 카메라 굿트레이 1번 ";
                else if (SelectHD2.Checked && SelectGoodTrayTransfer2.Checked)  DATA_.iMANUAL.CMD = "헤드2 카메라 굿트레이 2번 ";
                else if (SelectHD2.Checked && SelectReworkTrayTransfer.Checked) DATA_.iMANUAL.CMD = "헤드2 카메라 리워크 트레이 ";
                else return;
                DATA_.iMANUAL.CMD += DATA_.MC_DIR == 0 ? "좌상단 포켓 중심 위치" : "우상단 포켓 중심 위치";
                DATA_.iMANUAL.CMD = DATA_.iMANUAL.CMD + "를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "GET CURRENT POSITION ?";
                if (UTIL_.PRINT_MASSAGE(DATA_.iMANUAL.CMD, false, false, false)){
                    LBL_HEAD_X_POS1.Text    = LAB_.GET_ACTPOS(CovHD.NumMT).ToString();
                    LBL_TRAY_Y_POS.Text     = LAB_.GET_ACTPOS(CovTRAY.NumMT).ToString();

                    lbTrayXPos.Text         = LAB_.GET_ACTPOS(CovHD.NumMT).ToString();
                    lbTrayYPos.Text         = LAB_.GET_ACTPOS(CovTRAY.NumMT).ToString();
                }
            }
            if (BTN.Name == "GetTrayDownPos"){
                if (SelectHD1.Checked && SelectGoodTrayTransfer1.Checked)       DATA_.iMANUAL.CMD = "헤드1 카메라 굿트레이 1번 ";
                else if (SelectHD1.Checked && SelectGoodTrayTransfer2.Checked)  DATA_.iMANUAL.CMD = "헤드1 카메라 굿트레이 2번 ";
                else if (SelectHD1.Checked && SelectReworkTrayTransfer.Checked) DATA_.iMANUAL.CMD = "헤드1 카메라 리워크 트레이 ";
                else if (SelectHD2.Checked && SelectGoodTrayTransfer1.Checked)  DATA_.iMANUAL.CMD = "헤드2 카메라 굿트레이 1번 ";
                else if (SelectHD2.Checked && SelectGoodTrayTransfer2.Checked)  DATA_.iMANUAL.CMD = "헤드2 카메라 굿트레이 2번 ";
                else if (SelectHD2.Checked && SelectReworkTrayTransfer.Checked) DATA_.iMANUAL.CMD = "헤드2 카메라 리워크 트레이 ";
                else return;
                DATA_.iMANUAL.CMD += DATA_.MC_DIR == 0 ? "우하단 포켓 중심 위치" : "좌하단 포켓 중심 위치";
                if (UTIL_.PRINT_MASSAGE(DATA_.iMANUAL.CMD, false, false, false)){
                    lbTrayDownX.Text = LAB_.GET_ACTPOS(CovHD.NumMT).ToString();
                    lbTrayDownY.Text = LAB_.GET_ACTPOS(CovTRAY.NumMT).ToString();
                }
            }
            if (BTN.Name == "GetZIG1stPos"){
                DATA_.iMANUAL.CMD = "지그 첫번째 홀 위치를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "GET CURRENT POSITION";
                if (UTIL_.PRINT_MASSAGE(DATA_.iMANUAL.CMD, false, false, false)){
                    int mStage = M.Table1;
                    if (DATA_.prMACHINE[CP.ZigAttachStage] == (int)eMAP_BLOCK.STAGE2) mStage = M.Table2;
                    lbZigX1Pos.Text = LAB_.GET_ACTPOS(CovHD.NumMT).ToString();
                    lbZigY1Pos.Text = LAB_.GET_ACTPOS(mStage).ToString();
                }
            }
            if (BTN.Name == "GetZIG2ndPos"){
                DATA_.iMANUAL.CMD = "지그 두번째 홀 위치를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "GET CURRENT POSITION";
                if (UTIL_.PRINT_MASSAGE(DATA_.iMANUAL.CMD, false, false, false)){
                    int mStage = M.Table1;
                    if (DATA_.prMACHINE[CP.ZigAttachStage] == (int)eMAP_BLOCK.STAGE2) mStage = M.Table2;
                    lbZigX2Pos.Text = LAB_.GET_ACTPOS(CovHD.NumMT).ToString();
                    lbZigY2Pos.Text = LAB_.GET_ACTPOS(mStage).ToString();
                }
            }
            if (BTN.Name == "GetZIGLastPos"){
                DATA_.iMANUAL.CMD = "지그 세번째 홀 위치를(을) 현재 위치값으로 변경 하시겠습니까?" + ETC.CrLf + "GET CURRENT POSITION";
                if (UTIL_.PRINT_MASSAGE(DATA_.iMANUAL.CMD, false, false, false)){
                    int mStage = M.Table1;
                    if (DATA_.prMACHINE[CP.ZigAttachStage] == (int)eMAP_BLOCK.STAGE2) mStage = M.Table2;
                    lbZigX3Pos.Text = LAB_.GET_ACTPOS(CovHD.NumMT).ToString();
                    lbZigY3Pos.Text = LAB_.GET_ACTPOS(mStage).ToString();
                }
            }
        }

        private void GET_TRAY_PITCH(object sender, EventArgs e){
            if (UTIL_.PRINT_MASSAGE("현재 위치값 기준으로 트레이 X/Y 피치 연산하시겠습니까 ?", false, false, false)){
                double dx = double.Parse(lbTrayDownX.Text);
                double dy = double.Parse(lbTrayDownY.Text);

                if (dx <= 0 || dy <= 0){
                    MessageBox.Show("하단 위치값이 0보다 작거나 0과 같으면 연산 할 수 없습니다 !");
                    return;
                }
                double x = Math.Abs(double.Parse(lbTrayXPos.Text) - dx);
                double y = Math.Abs(double.Parse(lbTrayYPos.Text) - dy);
                // TrayCntX, TrayCntY
                double PitchX = Math.Truncate((x / (DATA_.prMODEL[RP.TrayCntX] - 1)) * 1000) / 1000;
                double PitchY = Math.Truncate((y / (DATA_.prMODEL[RP.TrayCntY] - 1)) * 1000) / 1000;

                lblTrayPitchX.Text = PitchX.ToString();
                lblTrayPitchY.Text = PitchY.ToString();
            }
        }

        private void ManualRun_Click(object sender, EventArgs e){
            BTN = (Button)sender;
            DATA_.iMANUAL.Number = BTN.TabIndex;
            DATA_.IsSTRING[S.ManualMessage] = BTN.Text;
            string[] arr = DATA_.IsSTRING[S.ManualMessage].Split('\n');
            DATA_.IsSTRING[S.ManualMessage] = string.Empty;
            try{
                for (int i = 0; i < arr.Length; i++){
                    string[] temp = arr[i].Split('\r');
                    for (int j = 0; j < temp.Length; j++)
                        DATA_.IsSTRING[S.ManualMessage] += temp[j] + " ";
                }

                if ((BTN.Name == "HD_StageTeachingPos" || BTN.Name == "HD_StageUnitViewPos" || BTN.Name == "HD_TrayPocketView") && CHK_VISION_VIEW.Checked){
                    if (CovHD.NumMT == M.TRIGGER1) NSS_3310S.C.SendVision.SEND("HD1_LIVE,*");
                    else NSS_3310S.C.SendVision.SEND("HD2_LIVE,*");
                }
                if ((BTN.Name == "TopCam_StageTachingPos" || BTN.Name == "TopCam_StageUnitViewPos") && CHK_VISION_VIEW.Checked) NSS_3310S.C.SendVision.SEND("TOP_LIVE,*");

                switch (DATA_.iMANUAL.Number){
                    case ManualNumber.TopCamStaeView:
                        if (BTN.Name == "TopCam_StageTachingPos"){
                            DATA_.iMANUAL.iMT1 = mtHead;
                            DATA_.iMANUAL.iMT2 = mtStage;
                            DATA_.iMANUAL.int_1 = mtHead == M.TRIGGER1 ? (int)eHD.HD1 : (int)eHD.HD2;
                            DATA_.iMANUAL.int_2 = mtStage == M.Table1 ? (int)eMAP_BLOCK.STAGE1 : (int)eMAP_BLOCK.STAGE2;
                            DATA_.iMANUAL.int_3 = 0;
                            DATA_.iMANUAL.int_4 = 0;
                            DATA_.iMANUAL.int_5 = (int)(DATA_.prMODEL[RP.UnitCntX] - 1);
                            DATA_.iMANUAL.int_6 = (int)(DATA_.prMODEL[RP.UnitCntY] - 1);
                            DATA_.iMANUAL.bool_1 = ChkMarkCam_Offset.Checked;
                        }
                        else{
                            DATA_.iMANUAL.iMT1 = mtHead;
                            DATA_.iMANUAL.iMT2 = mtStage;
                            DATA_.iMANUAL.int_1 = mtHead == M.TRIGGER1 ? (int)eHD.HD1 : (int)eHD.HD2;
                            DATA_.iMANUAL.int_2 = mtStage == M.Table1 ? (int)eMAP_BLOCK.STAGE1 : (int)eMAP_BLOCK.STAGE2;
                            DATA_.iMANUAL.int_3 = cbMapBlock_GroupX.SelectedIndex;
                            DATA_.iMANUAL.int_4 = cbMapBlock_GroupY.SelectedIndex;
                            DATA_.iMANUAL.int_5 = cbMapBlock_UnitX.SelectedIndex;
                            DATA_.iMANUAL.int_6 = cbMapBlock_UnitY.SelectedIndex;
                            DATA_.iMANUAL.bool_1 = ChkMarkCam_Offset.Checked;
                        }
                        break;

                    case ManualNumber.StageHDCamView:
                        if (BTN.Name == "HD_StageTeachingPos"){
                            DATA_.iMANUAL.int_1 = nHD;
                            DATA_.iMANUAL.int_2 = nStage;
                            DATA_.iMANUAL.int_3 = 0;
                            DATA_.iMANUAL.int_4 = 0;
                            DATA_.iMANUAL.int_5 = 0;
                            DATA_.iMANUAL.int_6 = 0;
                            DATA_.iMANUAL.int_7 = 0;
                            DATA_.iMANUAL.bool_1 = ChkTRYVIEW.Checked;
                        }
                        else{
                            DATA_.iMANUAL.int_1 = nHD;
                            DATA_.iMANUAL.int_2 = nStage;
                            DATA_.iMANUAL.int_3 = 0;
                            DATA_.iMANUAL.int_4 = cbMapBlock_GroupX.SelectedIndex;
                            DATA_.iMANUAL.int_5 = cbMapBlock_GroupY.SelectedIndex;
                            DATA_.iMANUAL.int_6 = cbMapBlock_UnitX.SelectedIndex;
                            DATA_.iMANUAL.int_7 = cbMapBlock_UnitY.SelectedIndex;
                            DATA_.iMANUAL.bool_1 = ChkTRYVIEW.Checked;
                        }
                        break;

                    case ManualNumber.TrayFeederLoading:
                        DATA_.iMANUAL.int_1 = nTray;
                        break;

                    case ManualNumber.TrayFeederConvULDStart:
                        DATA_.iMANUAL.int_1 = nTray;
                        if (nTray == (int)eTRAY.REWORK){
                            DATA_.iMANUAL.Number = ManualNumber.TrayFeederStacker;
                        }
                        break;
                    case ManualNumber.TrayHDCamView:
                        DATA_.iMANUAL.int_1 = nHD;
                        DATA_.iMANUAL.int_2 = 0;
                        DATA_.iMANUAL.int_3 = nTray;
                        DATA_.iMANUAL.int_4 = cbxTRAY_X.SelectedIndex;
                        DATA_.iMANUAL.int_5 = cbxTRAY_Y.SelectedIndex;
                        DATA_.iMANUAL.bool_1 = ChkTRYVIEW.Checked;
                        break;

                    case ManualNumber.StageZigView:
                        DATA_.iMANUAL.int_1     = nHD;
                        DATA_.iMANUAL.int_2     = cbxZigHoleNum.SelectedIndex;
                        DATA_.iMANUAL.bool_1    = ChkTRYVIEW.Checked;
                        break;

                    default: break;
                }
                DATA_.iMANUAL.bRESULT = COM_.RUN_MANUAL(DATA_.iMANUAL.Number, DATA_.IsSTRING[S.ManualMessage], true);
            }
            catch (Exception ex){
                MessageBox.Show(DATA_.IsSTRING[S.ManualMessage] + " Click Fail !" + ETC.NewLine + ex.ToString());
                LogWR_.SaveLogException("MANUAL RUN FAIL", ex);
            }
        }

        private void TmrMT_Tick(object sender, EventArgs e){
            TmrMT.Enabled = false;
            Invoke();
            TmrMT.Enabled = true;
        }
        void Invoke(){
            if (DEF.MotorPage == (long)ePage.Loading) ViewLoading();
            if (DEF.MotorPage == (long)ePage.HandlerPk) ViewHeandlerPicker();
            if (DEF.MotorPage == (long)ePage.MapBlock) ViewMapBlock();
            if (DEF.MotorPage == (long)ePage.Head) ViewHead();
            if (DEF.MotorPage == (long)ePage.Tray) ViewTray();
            if (DEF.MotorPage == (long)ePage.TrayPk) ViewTrayPk();
            if (DEF.MotorPage == (long)ePage.EasyTeaching) ViewEasyTeaching();

            if (bMMIChage){
                bMMIChage = false;
                ReadPara();
            }
        }

        void ViewLoading(){
            uElv_Y.CurPosition = DATA_.mtSTS[M.ElvY].CurrentPosition;
            uElv_Z.CurPosition = DATA_.mtSTS[M.ElvZ].CurrentPosition;
#if _NSS3300
            uRail_Y1.CurPosition = DATA_.mtSTS[M.Rail].CurrentPosition;
#else
            uRail_Y1.CurPosition = DATA_.mtSTS[M.RailF].CurrentPosition;
            uRail_Y2.CurPosition = DATA_.mtSTS[M.RailR].CurrentPosition;
#endif
            uGripperX.CurPosition = DATA_.mtSTS[M.GrpX].CurrentPosition;
            uBarcode_Y.CurPosition = DATA_.mtSTS[M.Barcode].CurrentPosition;

        }
        void ViewHeandlerPicker(){
            uStripPkr_X.CurPosition = DATA_.mtSTS[M.StripPkX].CurrentPosition;
            uStripPkr_Z.CurPosition = DATA_.mtSTS[M.StripPkZ].CurrentPosition;
#if _NSS3300
#else
            uPreAlign_Z.CurPosition = DATA_.mtSTS[M.PreAlign].CurrentPosition;
#endif
            uUnitPkr_X.CurPosition = DATA_.mtSTS[M.UnitPkX].CurrentPosition;
            uUnitPkr_Z.CurPosition = DATA_.mtSTS[M.UnitPkZ].CurrentPosition;

        }
        void ViewMapBlock(){
            uMappingTable.CurPosition = DATA_.mtSTS[mtStage].CurrentPosition;
            uMarkVisionX.CurPosition = DATA_.mtSTS[M.TopVisionX].CurrentPosition;
            uMarkVisionZ.CurPosition = DATA_.mtSTS[M.TopVisionZ].CurrentPosition;
            uBtmCamY.CurPosition = DATA_.mtSTS[M.BtnVisionY].CurrentPosition;
            uBtmCamZ.CurPosition = DATA_.mtSTS[M.BtnVisionZ].CurrentPosition;

        }

        void ViewHead(){
            uHeadX.CurPosition = DATA_.mtSTS[mtHead].CurrentPosition;
            uHeadPkrZ.CurPosition = DATA_.mtSTS[mtPk].CurrentPosition;
            uHeadT.CurPosition = DATA_.mtSTS[mtPkTh].CurrentPosition;

        }
        void ViewTray(){
            uGoodTrayTransferY1.CurPosition = DATA_.mtSTS[M.TrayFeeder1].CurrentPosition;
            uGoodTrayTransferY2.CurPosition = DATA_.mtSTS[M.TrayFeeder2].CurrentPosition;
            uReworkTrayTransferY.CurPosition = DATA_.mtSTS[M.TrayFeeder3].CurrentPosition;
        }
        void ViewTrayPk(){
            uTrayPkr_X.CurPosition = DATA_.mtSTS[M.TrayPickerX].CurrentPosition;
            uTrayPkr_Z.CurPosition = DATA_.mtSTS[M.TrayPickerZ].CurrentPosition;
            uEMPTY_LIFT.CurPosition = DATA_.mtSTS[M.EmptyElv].CurrentPosition;
        }
        void ViewEasyTeaching(){
            CovMK_X.CurPosition = DATA_.mtSTS[M.TopVisionX].CurrentPosition;
            CovMK_Z.CurPosition = DATA_.mtSTS[M.TopVisionZ].CurrentPosition;
            CovPALLET.CurPosition = DATA_.mtSTS[mtStage].CurrentPosition;
            CovHD.CurPosition = DATA_.mtSTS[mtHead].CurrentPosition;
            CovTRAY.CurPosition = DATA_.mtSTS[mtTrayFeeder].CurrentPosition;

            lblRefPos_TopCamX.Text = string.Format("{0:0.###}", DATA_.mtSTS[M.TopVisionX].CurrentPosition - dRefCurPos_TopCam);
            lblRefHalfPos_TopCamX.Text = string.Format("{0:0.###}", (DATA_.mtSTS[M.TopVisionX].CurrentPosition - dRefCurPos_TopCam) / 2);
            lblRefPos_HeadX.Text = string.Format("{0:0.###}", DATA_.mtSTS[CovHD.NumMT].CurrentPosition - dRefCurPos_HeadX);
            lblRefHalfPos_HeadX.Text = string.Format("{0:0.###}", (DATA_.mtSTS[CovHD.NumMT].CurrentPosition - dRefCurPos_HeadX) / 2);
            lblRefPos_PalletY.Text = string.Format("{0:0.###}", DATA_.mtSTS[CovPALLET.NumMT].CurrentPosition - dRefCurPos_PalletY);
            lblRefHalfPos_PalletY.Text = string.Format("{0:0.###}", (DATA_.mtSTS[CovPALLET.NumMT].CurrentPosition - dRefCurPos_PalletY) / 2);
            lblRefPos_TrayY.Text = string.Format("{0:0.###}", DATA_.mtSTS[CovTRAY.NumMT].CurrentPosition - dRefCurPos_TrayY);
            lblRefHalfPos_TrayY.Text = string.Format("{0:0.###}", (DATA_.mtSTS[CovTRAY.NumMT].CurrentPosition - dRefCurPos_TrayY) / 2);
        }
    }
}