using LIB_.UERCTRL;
using Object;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SYSTEM;

namespace NSS_3310S{
    public partial class FormManual : Form{
        Label LBL = null;
        Button BTN = null;
        RadioButton RBTN = null;
        double dValue = 0.0;
        UCL_JOG[] uMT = null;
        RadioButton[] rMapBlock1 = null;
        RadioButton[] rMapBlock2 = null;
        ComboBox cbx = null;
        int mtStage = M.Table1;
        int mtHead = M.TRIGGER1;
        int mtPkTh = M.X1T;
        int mtPk = M.X1Z12;
        int mtTrayFeeder = M.TrayFeeder1;
        int nPkr = 0;
        int nHD = 0;
        int nStage = 0;
        int nTray = 0;
        bool bFirstView = true;
        Label[] Input = null;
        Label[] Output = null;
        Button[] Sol = null;
        Button[] Motor = null;
        Button[] CycleRun = null;
        enum ePage{
            Loading = 4,
            HandlerPk = 0,
            MapBlock = 1,
            Head = 2,
            Tray = 3,
        }

        public FormManual(){
            InitializeComponent();

            #region EVENT
            bMN_0.Click += (sender, e) => SCREEN_CHAGE(bMN_0);
            bMN_1.Click += (sender, e) => SCREEN_CHAGE(bMN_1);
            bMN_2.Click += (sender, e) => SCREEN_CHAGE(bMN_2);
            bMN_3.Click += (sender, e) => SCREEN_CHAGE(bMN_3);
            bMN_4.Click += (sender, e) => SCREEN_CHAGE(bMN_4);
            bMN_5.Click += (sender, e) => SCREEN_CHAGE(bMN_5);

            LBL_UNCLEMP_DELAY.DoubleClick += (sender, e) => INPUT_KEYPAD(LBL_UNCLEMP_DELAY);
            editREPEAT_TIME.DoubleClick += (sender, e) => REPEAT_TIME(editREPEAT_TIME);
            swRepeat.Click += (sender, e) => REPEAT();

            Pkr_Num.SelectedIndexChanged += (sender, e) => SetPkrNumber(Pkr_Num);

            swMotorSelect.Click += (sender, e) => ViewMotorTeaching();
            btn_Pitch_0.Click += (sender, e) => OffestPitchValue(btn_Pitch_0);
            btn_Pitch_1.Click += (sender, e) => OffestPitchValue(btn_Pitch_1);
            btn_Pitch_2.Click += (sender, e) => OffestPitchValue(btn_Pitch_2);
            btn_Pitch_3.Click += (sender, e) => OffestPitchValue(btn_Pitch_3);
            btn_Pitch_4.Click += (sender, e) => OffestPitchValue(btn_Pitch_4);
            btn_Pitch_5.Click += (sender, e) => OffestPitchValue(btn_Pitch_5);

            lbl_IncPitch.DoubleClick += (sender, e) => SetIncPitch(lbl_IncPitch);
#if _NSS3300
            uMT = new UCL_JOG[] { uElv_Y, uElv_Z, uBarcode_Y, uRail_Y1, uGripperX,
                                     uStripPkr_X, uStripPkr_Z,
                                     uUnitPkr_X, uUnitPkr_Z,
                                     uMappingTable, uMarkVisionX, uMarkVisionZ,
                                     uBtmCamY, uBtmCamZ, uHeadX, uHeadT, uHeadPkrZ, uTrayTransferY,
                                     uTrayPkr_X, uTrayPkr_Z, uEmptyLift
                                    };
#else
            uMT = new UCL_JOG[] { uElv_Y, uElv_Z, uBarcode_Y, uRail_Y1, uRail_Y2, uGripperX,
                                     uStripPkr_X, uStripPkr_Z, uPreAlign_Z,
                                     uUnitPkr_X, uUnitPkr_Z,
                                     uMappingTable, uMarkVisionX, uMarkVisionZ,
                                     uBtmCamY, uBtmCamZ, uHeadX, uHeadT, uHeadPkrZ, uTrayTransferY,
                                     uTrayPkr_X, uTrayPkr_Z, uEmptyLift
                                    };
#endif
            for (int i = 0; i < uMT.Length; i++){
                uMT[i].NumMT = M.MN_MT[i];
            }

#if _NSS3300
            Input = new Label[] { iMGZClamp_L, iMGZClamp_U, iPusher_F, iPusher_B, iRAIL_U, iRAIL_D, iGrp_O, iGrp_C,
                                  iStripVcm, iUnitVcm, iScrapVcm1, iScrapVcm2, iCleaner_L, iCleaner_R,
                                  iPallet_V, iBTMCalZig_F, iBTMCalZig_B,
                                  iGT1FClamp_U, iGT1BClamp_U, iGT2FClamp_U, iGT2BClamp_U, iGoodTrayTable_U, iGoodTrayTable_D, iRTClamp_U1, iRTClamp_U2, iGoodTrayPreAlign_F, iGoodTrayPreAlign_B, iReworkTrayTable_U, iReworkTrayTable_D,
                                  iFTRAY_PKR_L, iFTRAY_PKR_U, iEmptyStakerStopper_F1, iEmptyStakerStopper_F2, iEmptyStakerStopper_F3, iEmptyStakerStopper_F4, iEmptyStakerStopper_B1, iEmptyStakerStopper_B2, iEmptyStakerStopper_B3, iEmptyStakerStopper_B4, iEmptyTrayTransfer_U1, iEmptyTrayTransfer_U2, iEmptyTransfer_F, iEmptyTransfer_B
            };
#else
            Input = new Label[] { iMGZClamp_L, iMGZClamp_U, iPusher_F, iPusher_B, iRAIL_U, iRAIL_D, iGrp_O, iGrp_C,
                                  iStripVcm, iUnitVcm, iScrapVcm1, iScrapVcm2, iCleaner_L, iCleaner_R,
                                  iPallet_V, iBTMCalZig_F, iBTMCalZig_B,
                                  iGT1FClamp_U, iGT1BClamp_U, iGT2FClamp_U, iGT2BClamp_U, iGoodTrayTable_U, iGoodTrayTable_D, iRTClamp_U1, iRTClamp_U2, iGoodTrayPreAlign_F, iGoodTrayPreAlign_B, iReworkTrayTable_U, iReworkTrayTable_D,
                                  iFTRAY_PKR_L, iFTRAY_PKR_U, iEmptyStakerStopper_F1, iEmptyStakerStopper_F2, iEmptyStakerStopper_F3, iEmptyStakerStopper_F4, iEmptyStakerStopper_B1, iEmptyStakerStopper_B2, iEmptyStakerStopper_B3, iEmptyStakerStopper_B4, iEmptyTrayTransfer_U1, iEmptyTrayTransfer_U2, iEmptyTransfer_F, iEmptyTransfer_B,
                                  iRAIL_V
            };
#endif
            for (int i = 0; i < Input.Length; i++){
                Input[i].TabIndex = I.MNInput[i];
            }
#if _NSS3300
            Output = new Label[] { oMGZClamp_L, oMGZClamp_U, oPusher_F, oPusher_B, oRAIL_U, oRAIL_D, oGrp_O, oGrp_C,
                                   oStripVcmOn, oStripBlow, oUnitVcm, oUnitRej, oSCRAP1_V, oSCRAP2_V, oSCRAP1_R, oSCRAP2_R,oCleaner_W1,oCleaner_W2, oCleaner_A1, oCleaner_L, oCleaner_R,
                                   oPallet_Air, oPallet_V, oTopVision_A, oBTMCalZig_F, oBTMCalZig_B, oBTMVision_A,
                                   oGT1FClamp_L, oGT1FClamp_U, oGT1RClamp_L, oGT1RClamp_U, oGT2FClamp_L, oGT2FClamp_U, oGT2RClamp_L, oGT2RClamp_U, oGoodTrayTable_U, oGoodTrayTable_D, oRTClamp_L, oRTClamp_U, oGoodTrayPreAlign_F, oGoodTrayPreAlign_B, oReworkTrayTable_U, oReworkTrayTable_D,
                                   oFTRYPKR_L, oFTRYPKR_U, oEmptyStakerStopper_F1, oEmptyStakerStopper_F2, oEmptyStakerStopper_B1, oEmptyStakerStopper_B2, oEmptyTrayTransfer_L, oEmptyTrayTransfer_U, oEmptyTransfer_F, oEmptyTransfer_B,
                                   oUnitPkAirShower
            };
#else
            Output = new Label[] { oMGZClamp_L, oMGZClamp_U, oPusher_F, oPusher_B, oRAIL_U, oRAIL_D, oGrp_O, oGrp_C,
                                   oStripVcmOn, oStripVcmOff, oStripBlow, oStripPurge, oUnitVcm, oUnitRej, oSCRAP1_V, oSCRAP2_V, oSCRAP1_R, oSCRAP2_R,oCleaner_W1,oCleaner_W2, oCleaner_A1, oCleaner_A2, oCleaner_L, oCleaner_R,
                                   oPallet_Air, oPallet_V, oTopVision_A, oBTMCalZig_F, oBTMCalZig_B, oBTMVision_A,
                                   oGT1FClamp_L, oGT1FClamp_U, oGT1RClamp_L, oGT1RClamp_U, oGT2FClamp_L, oGT2FClamp_U, oGT2RClamp_L, oGT2RClamp_U, oGoodTrayTable_U, oGoodTrayTable_D, oRTClamp_L, oRTClamp_U, oGoodTrayPreAlign_F, oGoodTrayPreAlign_B, oReworkTrayTable_U, oReworkTrayTable_D,
                                   oFTRYPKR_L, oFTRYPKR_U, oEmptyStakerStopper_F1, oEmptyStakerStopper_F2, oEmptyStakerStopper_B1, oEmptyStakerStopper_B2, oEmptyTrayTransfer_L, oEmptyTrayTransfer_U, oEmptyTransfer_F, oEmptyTransfer_B,
                                   oRAIL_V, oRAIL_B, oUnitPkAirShower
            };
#endif

            for (int i = 0; i < Output.Length; i++){
                Output[i].TabIndex = O.MNOutput[i];
            }

            Sol = new Button[] { MGZClamp_LU, Pusher_FB, InLet_UD, Grip_OC,
                                 StripPk_Vac, StripPk_Blow, UnitPk_Vac, UnitPk_Blow, Scrap_Vac, Scrap_Blow, Cleaner_Water, Cleaner_Air, Cleaner_Swing,
                                 Stage_Airshower, Stage_Vac, TopCam_Blow, TopCam_Trigger, BtmCam_CalZig_FB, BtmCam_Blow,
                                 Pkr_Vac, Pkr_Blow, Pkr_Free, OKTray1_FrontClamp, OKTray1_RearClamp, OKTray2_FrontClamp, OKTray2_RearClamp, OKTrayStacker_UD, OKTrayPreAlign_FB, ReworkTray_Clamp, ReworkTrayStacker_UD, HD1_Trigger, HD2_Trigger,
                                 TrayPk_Clamp, EmptyStacker_Stopper, EmptyTransfer_Clamp, EmptyTransfer_FB,
                                 SawStageVac_On, SawStageVac_Off, SawStageRej_On, SawStageRej_Off,
                                 InLet_Vac, InLet_Blow, ucMGZ_CONV_B, ucMGZ_CONV_F, CleanZone_Airshower
            };
            for (int i = 0; i < Sol.Length; i++){
                Sol[i].TabIndex = ManualNumber.MNSol[i];
            }

            Motor = new Button[] {  MGZ_Rdy, MGZ_Unloading, MGZ_Loading, MGZ_StripLoading, 
                                    Rail_Rdy, Rail_Loading, Rail_Work, Barcode_Rdy, Barcode_Reading, 
                                    Gripper_Rdy, Gripper_Grip, Gripper_Barcode, Gripper_LD, Gripper_PickUp,
                                    StripPkrX_Pic, StripPkrX_Plc, StripPkrZ_Up, StripPkrZ_Dn, UnitPkrX_Pic, UnitPkrX_Clean, UnitPkrX_Plc1, UnitPkrX_Plc2, UnitPkrZ_Up, UnitPkrZ_Dn,
                                    Stage_Rdy, Stage_Receive,Stage_TopCamViewPos, TopCam_Rdy,
                                    HDX_Rdy, HDX_BtmCamHDCamCenter, HDX_BtmCamPkrCenter, HDX_RejectBox, PkZ_Rdy, PK_PickupTH, PK_PlaceTH, PkTh_Move, TrayFeeder_Rdy, TrayFeeder_Loading, TrayFeeder_UnitPlace, TrayFeeder_Stacker, TrayFeeder_Unloading, TrayFeeder_TrayPusher, TrayFeeder_ConvUnloading, Stage_HDCamViewPos, Tray_HDCamViewPos,
                                    TrayPkX_OKFeeder, TrayPkX_NGFeeder, TrayPkX_EmptyFeeder, TrayPkZ_Up, TrayPkZ_Dn, EmptyStacker_Hold, EmptyStacker_Loading, EmptyStacker_Down
            };
            for (int m = 0; m < Motor.Length; m++){
                Motor[m].TabIndex = ManualNumber.MNMotor[m];
            }

            CycleRun = new Button[] { MGZLoading, MGZUnloading, MGZStringLoading,
                                      Stage_RunAirshower, Stage_RunInspection,
                                      StripLoading, StripPickup, StripPlace, UnitPickup, ScrapReject, UnitCleanning, UnitBrushing, UnitAirshower, CycleRun_UnitPkr, UnitPlace,
                                      StageAirshower, StageUnitInspection,
                                      OKTray1_Unloading, OKTray2_Unloading, OKTray1_Loading, OKTray2_Loading,
                                      NGTray_Unloading, NGTray_Loading,
                                      EmptyLift_TrayLoading, EmptyTrayPickUp,
                                      btnPkCal, CycleRun_PRS_TEST, RunMarkInspection, btnAllPkCal, Pkr_Pic, Stage_CalZigCenter
            };
            for (int i = 0; i < CycleRun.Length; i++){
                CycleRun[i].TabIndex = ManualNumber.MNCycleRun[i];
            }

            StripBarcodeReading.TabIndex        = ManualNumber.StringBarcodeReading;
            HD_Stage_MasterZigCenter.TabIndex   = ManualNumber.MasterZigCenterHD;

            uElv_Y.OnMouseUp_Click += (sender, e) => MotorStop(uElv_Y);
            uElv_Z.OnMouseUp_Click += (sender, e) => MotorStop(uElv_Z);
            uBarcode_Y.OnMouseUp_Click += (sender, e) => MotorStop(uBarcode_Y);
            uRail_Y1.OnMouseUp_Click += (sender, e) => MotorStop(uRail_Y1);
            uRail_Y2.OnMouseUp_Click += (sender, e) => MotorStop(uRail_Y2);
            uGripperX.OnMouseUp_Click += (sender, e) => MotorStop(uGripperX);

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
            uTrayTransferY.OnMouseUp_Click += (sender, e) => MotorStop(uTrayTransferY);

            uTrayPkr_X.OnMouseUp_Click += (sender, e) => MotorStop(uTrayPkr_X);
            uTrayPkr_Z.OnMouseUp_Click += (sender, e) => MotorStop(uTrayPkr_Z);
            uEmptyLift.OnMouseUp_Click += (sender, e) => MotorStop(uEmptyLift);

            uElv_Y.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uElv_Y);
            uElv_Z.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uElv_Z);
            uBarcode_Y.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uBarcode_Y);
            uRail_Y1.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uRail_Y1);
            uRail_Y2.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uRail_Y2);
            uGripperX.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uGripperX);

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
            uTrayTransferY.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uTrayTransferY);

            uTrayPkr_X.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uTrayPkr_X);
            uTrayPkr_Z.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uTrayPkr_Z);
            uEmptyLift.OnCcwMouseDown_Click += (sender, e) => MotorCcw(uEmptyLift);

            uElv_Y.OnCwMouseDown_Click += (sender, e) => MotorCw(uElv_Y);
            uElv_Z.OnCwMouseDown_Click += (sender, e) => MotorCw(uElv_Z);
            uBarcode_Y.OnCwMouseDown_Click += (sender, e) => MotorCw(uBarcode_Y);
            uRail_Y1.OnCwMouseDown_Click += (sender, e) => MotorCw(uRail_Y1);
            uRail_Y2.OnCwMouseDown_Click += (sender, e) => MotorCw(uRail_Y2);
            uGripperX.OnCwMouseDown_Click += (sender, e) => MotorCw(uGripperX);

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
            uTrayTransferY.OnCwMouseDown_Click += (sender, e) => MotorCw(uTrayTransferY);

            uTrayPkr_X.OnCwMouseDown_Click += (sender, e) => MotorCw(uTrayPkr_X);
            uTrayPkr_Z.OnCwMouseDown_Click += (sender, e) => MotorCw(uTrayPkr_Z);
            uEmptyLift.OnCwMouseDown_Click += (sender, e) => MotorCw(uEmptyLift);


            rMapBlock1 = new RadioButton[] { SelectMapTable1, SelectStage1, Stage1, MapBlock1 };
            rMapBlock2 = new RadioButton[] { SelectMapTable2, SelectStage2, Stage2, MapBlock2 };

            SelectMapTable1.Click += (sender, e) => SelectMoter(SelectMapTable1);
            SelectStage1.Click += (sender, e) => SelectMoter(SelectStage1);
            Stage1.Click += (sender, e) => SelectMoter(Stage1);
            MapBlock1.Click += (sender, e) => SelectMoter(MapBlock1);
            SelectX1.Click += (sender, e) => SelectMoter(SelectX1);

            SelectMapTable2.Click += (sender, e) => SelectMoter(SelectMapTable2);
            SelectStage2.Click += (sender, e) => SelectMoter(SelectStage2);
            Stage2.Click += (sender, e) => SelectMoter(Stage2);
            MapBlock2.Click += (sender, e) => SelectMoter(MapBlock2);
            SelectX2.Click += (sender, e) => SelectMoter(SelectX2);

            SelectGoodTrayTransfer1.Click += (sender, e) => SelectMoter(SelectGoodTrayTransfer1);
            SelectGoodTrayTransfer2.Click += (sender, e) => SelectMoter(SelectGoodTrayTransfer2);
            SelectReworkTrayTransfer.Click += (sender, e) => SelectMoter(SelectReworkTrayTransfer);
#endregion

            cbxScrap.SelectedIndex = 2;
            SelectMapTable1.Checked = true;
            SelectX1.Checked = true;
            Pkr_Num.SelectedIndex = 0;
            nPkr = Pkr_Num.SelectedIndex;
            Pk_ThSet.SelectedIndex = 0;
            SelectGoodTrayTransfer1.Checked = true;
            SelectStage1.Checked = true;

            SelectMoter(SelectMapTable1);

            DocRFReader();

#if _NSS3300
            uPreAlign_Z.Visible = false;
            uRail_Y2.Visible = false;
#else
            uPreAlign_Z.Visible = true;
            uRail_Y2.Visible = true;
#endif
        }

        public void DocRFReader(){
            SUBFRM_.gRFID.FormBorderStyle = FormBorderStyle.None;
            SUBFRM_.gRFID.Size = pRFReader.Size;
            SUBFRM_.gRFID.Top = 0;
            SUBFRM_.gRFID.Left = 0;
            SUBFRM_.gRFID.TopLevel = false;
            pRFReader.Controls.Add(SUBFRM_.gRFID);
            SUBFRM_.gRFID.Show();
        }

        public void Initailize_View(){
            bFirstView = false;
            DATA_.IsDOUBLE[D.ManualRepeat_Interval] = Convert.ToDouble(editREPEAT_TIME.Text);
            if (DEF.ManualPage < 0) SCREEN_CHAGE(bMN_4);
            IniComboBox();

            CHK_BTM_Z_POSITION.Checked = false;

            TmrMANUAL.Enabled = true;
            Show();
            BringToFront();
        }
        public void Set_Language(){

        }

        void IniComboBox(){
            cbxSlotCnt.Items.Clear();
            cSlotCnt.Items.Clear();
            cbMapBlock_GroupX.Items.Clear();
            cbMapBlock_GroupY.Items.Clear();
            cbMapBlock_UnitX.Items.Clear();
            cbMapBlock_UnitY.Items.Clear();
            cMB_Group_X.Items.Clear();
            cMB_Group_Y.Items.Clear();
            cMB_UNIT_X.Items.Clear();
            cMB_UNIT_Y.Items.Clear();
            cbxTRAY_X.Items.Clear();
            cbxTRAY_Y.Items.Clear();

            for (int i = 0; i < DATA_.prMODEL[RP.MGZSlotCnt]; i++){
                cbxSlotCnt.Items.Add(i + 1);
                cSlotCnt.Items.Add(i + 1);
            }

            for (int i = 0; i < DATA_.prMODEL[RP.GroupX[nStage]]; i++){
                cbMapBlock_GroupX.Items.Add(i + 1);
                cMB_Group_X.Items.Add(i + 1);
            }
            for (int i = 0; i < DATA_.prMODEL[RP.GroupY[nStage]]; i++){
                cbMapBlock_GroupY.Items.Add(i + 1);
                cMB_Group_Y.Items.Add(i + 1);
            }

            for (int i = 0; i < DATA_.prMODEL[RP.UnitX[nStage]]; i++){
                cbMapBlock_UnitX.Items.Add(i + 1);
                cMB_UNIT_X.Items.Add(i + 1);
            }
            for (int i = 0; i < DATA_.prMODEL[RP.UnitY[nStage]]; i++){
                cbMapBlock_UnitY.Items.Add(i + 1);
                cMB_UNIT_Y.Items.Add(i + 1);
            }

            for (int i = 0; i < DATA_.prMODEL[RP.TrayCntX]; i++){
                cbxTRAY_X.Items.Add(i + 1);
            }
            for (int i = 0; i < DATA_.prMODEL[RP.TrayCntY]; i++){
                cbxTRAY_Y.Items.Add(i + 1);
            }

            cbxSlotCnt.SelectedIndex = 0;
            cSlotCnt.SelectedIndex = 0;
            cbMapBlock_GroupX.SelectedIndex = 0;
            cMB_Group_X.SelectedIndex = 0;
            cbMapBlock_GroupY.SelectedIndex = 0;
            cMB_Group_Y.SelectedIndex = 0;
            cbMapBlock_UnitX.SelectedIndex = 0;
            cMB_UNIT_X.SelectedIndex = 0;
            cbMapBlock_UnitY.SelectedIndex = 0;
            cMB_UNIT_Y.SelectedIndex = 0;
            cbxTRAY_X.SelectedIndex = 0;
            cbxTRAY_Y.SelectedIndex = 0;
        }

#region >>EVENT
        void RESET_SCREEN(){
            for (int i = 0; i < 6; i++){
                if (Controls.Find("bMN_" + i.ToString(), true).FirstOrDefault() is Button nBtn) nBtn.BackColor = Color.White; //bMN_3
            }
        }
        void SCREEN_VIEW(int nPage){
            RESET_SCREEN();
            if (Controls.Find("bMN_" + nPage.ToString(), true).FirstOrDefault() is Button nBtn) nBtn.BackColor = Color.SeaShell;
            tcMLPAGE.SelectedIndex = nPage;
        }
        void SCREEN_CHAGE(object sender){
            BTN = sender as Button;
            DEF.ManualPage = Convert.ToInt32(BTN.Tag);
            SCREEN_VIEW(DEF.ManualPage);
        }

        void INPUT_KEYPAD(object sender){
            LBL = (Label)sender;
            LBL.BackColor = Color.Lime;
            string msg = (LBL.Tag).ToString();
            double backup = Convert.ToDouble(LBL.Text);
            UTIL_.OPEN_KEYPAD_LABEL(msg, ref LBL, false);
        }

        void REPEAT_TIME(object sender){
            LBL = (Label)sender;
            LBL.BackColor = Color.Lime;
            double backup = Convert.ToDouble(LBL.Text);
            UTIL_.OPEN_KEYPAD_LABEL("REPEAT TIME", ref LBL, false);
            try{
                DATA_.IsDOUBLE[D.ManualRepeat_Interval] = Convert.ToDouble(LBL.Text);
                LBL.BackColor = Color.White;
            }
            catch (Exception ex){
                MessageBox.Show("You have entered the wrong value. (REPEAT TIME VALUE)" + ETC.NewLine + ex.ToString());
                LBL.Text = backup.ToString();
                DATA_.IsDOUBLE[D.ManualRepeat_Interval] = Convert.ToInt32(backup);
                LBL.BackColor = Color.White;
            }
        }
        void REPEAT(){
            SUBFRM_.gManualRepeat.sMSG = DATA_.IsSTRING[S.ManualMessage];
            SUBFRM_.gManualRepeat.INI_();
        }

        void SetPkrNumber(object sender){
            cbx = (ComboBox)sender;
            nPkr = cbx.SelectedIndex;
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

            if (RBTN.Name == "SelectMapTable1" || RBTN.Name == "SelectStage1" || RBTN.Name == "Stage1" || RBTN.Name == "MapBlock1"){
                mtStage = M.Table1;
                nStage = (int)eMAP_BLOCK.STAGE1;
                for (int i = 0; i < rMapBlock1.Length; i++){
                    rMapBlock1[i].Checked = true;
                }
            }
            if (RBTN.Name == "SelectMapTable2" || RBTN.Name == "SelectStage2" || RBTN.Name == "Stage2" || RBTN.Name == "MapBlock2"){
                mtStage = M.Table2;
                nStage = (int)eMAP_BLOCK.STAGE2;
                for (int i = 0; i < rMapBlock2.Length; i++){
                    rMapBlock2[i].Checked = true;
                }
            }
            iPallet_V.TabIndex = I.StageVac[nStage];
            oPallet_Air.TabIndex = O.StageAirshowr[nStage];
            oPallet_V.TabIndex = O.StageVac[nStage];


            if (RBTN.Name == "SelectX1"){
                mtHead = M.TRIGGER1;
                mtPkTh = M.X1T;
                nHD = (int)eHD.HD1;
                SelectX1.Checked = true;
            }
            if (RBTN.Name == "SelectX2"){
                mtHead = M.TRIGGER2;
                mtPkTh = M.X2T;
                nHD = (int)eHD.HD2;
                SelectX2.Checked = true;
            }

            if (RBTN.Name == "SelectGoodTrayTransfer1"){
                mtTrayFeeder = M.TrayFeeder1;
                nTray = (int)eTRAY.GOOD1;
                SelectGoodTrayTransfer1.Checked = true;

                TrayFeeder_Unloading.Enabled = true;
                TrayFeeder_TrayPusher.Enabled = true;
                TrayFeeder_ConvUnloading.Enabled = true;
            }
            if (RBTN.Name == "SelectGoodTrayTransfer2"){
                mtTrayFeeder = M.TrayFeeder2;
                nTray = (int)eTRAY.GOOD2;
                SelectGoodTrayTransfer2.Checked = true;

                TrayFeeder_Unloading.Enabled = true;
                TrayFeeder_TrayPusher.Enabled = true;
                TrayFeeder_ConvUnloading.Enabled = true;
            }
            if (RBTN.Name == "SelectReworkTrayTransfer"){
                mtTrayFeeder = M.TrayFeeder3;
                nTray = (int)eTRAY.REWORK;
                SelectReworkTrayTransfer.Checked = true;

                TrayFeeder_Unloading.Enabled = false;
                TrayFeeder_TrayPusher.Enabled = false;
                TrayFeeder_ConvUnloading.Enabled = false;
            }

            uMappingTable.NumMT = mtStage;
            uMappingTable.TEXT = DATA_.MtName[mtStage];

            uHeadX.NumMT = mtHead;
            uHeadX.TEXT = DATA_.MtName[mtHead];
            uHeadT.NumMT = mtPkTh;
            uHeadT.TEXT = DATA_.MtName[mtPkTh];
            if (mtHead == M.TRIGGER1) mtPk = M.HD1Pk[nPkr];
            else mtPk = M.HD2Pk[nPkr];
            uHeadPkrZ.NumMT = mtPk;
            uHeadPkrZ.TEXT = DATA_.MtName[mtPk];
            uTrayTransferY.NumMT = mtTrayFeeder;
            uTrayTransferY.TEXT = DATA_.MtName[mtTrayFeeder];

            if (!bFirstView) IniComboBox();
        }
#endregion

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

                switch (DATA_.iMANUAL.Number){
                    case ManualNumber.MGZRdy:
                    case ManualNumber.MGZUnloading:
                    case ManualNumber.MGZLoading:
                    case ManualNumber.MGZSlot:
                        DATA_.iMANUAL.int_1 = cbxSlotCnt.SelectedIndex;
                        DATA_.iMANUAL.Option = true;
                        break;
                    case ManualNumber.RunMGZSlotLocation:
                        DATA_.iMANUAL.int_1 = cSlotCnt.SelectedIndex;
                        break;
                    case ManualNumber.MGZClamp:
                        DATA_.iMANUAL.bool_1 = CHK_UNCLEMP_DELAY.Checked;
                        try{
                            DATA_.iMANUAL.int_1 = int.Parse(LBL_UNCLEMP_DELAY.Text);
                        }
                        catch (Exception E){
                            LogWR_.SaveLogException("MANUAL MAGAZINE UNCLAMP DELAY TIME WRITE FAIL", E);
                            LBL_UNCLEMP_DELAY.Text = "0";
                            DATA_.iMANUAL.int_1 = 0;
                        }
                        break;

                    case ManualNumber.StripPkPic:
                    case ManualNumber.StripPkPlc:
                        DATA_.iMANUAL.Option = false;
                        break;

                    case ManualNumber.UnitPkPic:
                    case ManualNumber.UnitPkCleaner:
                    case ManualNumber.UnitPkStage1:
                    case ManualNumber.UnitPkStage2:
                        DATA_.iMANUAL.Option = false;
                        break;

                    case ManualNumber.ScrapVac:
                    case ManualNumber.ScrapBlow:
                        DATA_.iMANUAL.int_1 = cbxScrap.SelectedIndex;
                        break;

                    case ManualNumber.StageAirshower:
                    case ManualNumber.StageVac:
                        DATA_.iMANUAL.int_1 = nStage;
                        break;
                    case ManualNumber.StageRdy:
                    case ManualNumber.StageReceive:
                    case ManualNumber.HDXReject:
                    case ManualNumber.TopCamStaeView:
                        DATA_.iMANUAL.iMT1 = mtHead;
                        DATA_.iMANUAL.iMT2 = mtStage;
                        DATA_.iMANUAL.int_1 = nHD;
                        DATA_.iMANUAL.int_2 = nStage;
                        DATA_.iMANUAL.int_3 = cbMapBlock_GroupX.SelectedIndex;
                        DATA_.iMANUAL.int_4 = cbMapBlock_GroupY.SelectedIndex;
                        DATA_.iMANUAL.int_5 = cbMapBlock_UnitX.SelectedIndex;
                        DATA_.iMANUAL.int_6 = cbMapBlock_UnitY.SelectedIndex;
                        DATA_.iMANUAL.bool_1 = ChkMarkCam_Offset.Checked;
                        break;

                    case ManualNumber.Trigger:
                        if (BTN.Name == "TopCam_Trigger") DATA_.iMANUAL.int_1 = (int)eTRIGGER.MARK;
                        else if (BTN.Name == "HD1_Trigger"){
                            DATA_.iMANUAL.int_1 = (int)eTRIGGER.HD1;
                        }
                        else{
                            DATA_.iMANUAL.int_1 = (int)eTRIGGER.HD2;
                        }
                        break;

                    case ManualNumber.PkrVac:
                    case ManualNumber.PkrBlow:
                    case ManualNumber.PkrFree:
                        DATA_.iMANUAL.int_1 = nHD;
                        DATA_.iMANUAL.int_2 = nPkr;
                        break;

                    case ManualNumber.HDXRdy:
                    case ManualNumber.HDCamCenter:
                    case ManualNumber.HDPkCenter:
                    case ManualNumber.AllPkZRdy:
                        DATA_.iMANUAL.iMT1 = mtHead;
                        DATA_.iMANUAL.int_1 = nHD;
                        DATA_.iMANUAL.int_2 = Pkr_Num.SelectedIndex;
                        DATA_.iMANUAL.bool_1 = CHK_BTM_Z_POSITION.Checked;
                        DATA_.iMANUAL.bool_2 = CHK_PK_Z_OPTION.Checked;
                        break;
                    case ManualNumber.StageHDCamView:
                        DATA_.iMANUAL.int_1 = nHD;
                        DATA_.iMANUAL.int_2 = nStage;
                        DATA_.iMANUAL.int_3 = Pkr_Num.SelectedIndex;
                        DATA_.iMANUAL.int_4 = cMB_Group_X.SelectedIndex;
                        DATA_.iMANUAL.int_5 = cMB_Group_Y.SelectedIndex;
                        DATA_.iMANUAL.int_6 = cMB_UNIT_X.SelectedIndex;
                        DATA_.iMANUAL.int_7 = cMB_UNIT_Y.SelectedIndex;
                        DATA_.iMANUAL.bool_1 = ChkTRYVIEW.Checked;
                        break;
                    case ManualNumber.TrayHDCamView:
                        DATA_.iMANUAL.int_1 = nHD;
                        DATA_.iMANUAL.int_2 = Pkr_Num.SelectedIndex;
                        DATA_.iMANUAL.int_3 = nTray;
                        DATA_.iMANUAL.int_4 = cbxTRAY_X.SelectedIndex;
                        DATA_.iMANUAL.int_5 = cbxTRAY_Y.SelectedIndex;
                        DATA_.iMANUAL.bool_1 = ChkTRYVIEW.Checked;
                        break;

                    case ManualNumber.PkPicTh:
                    case ManualNumber.PkPlcTh:
                    case ManualNumber.PkTh:
                        DATA_.iMANUAL.iMT1 = mtPkTh;
                        DATA_.iMANUAL.double_1 = double.Parse(Pk_ThSet.Text);
                        break;

                    case ManualNumber.MasterZigCenterHD:
                        DATA_.iMANUAL.iMT1 = mtHead;
                        DATA_.iMANUAL.iMT2 = mtStage;
                        DATA_.iMANUAL.int_1 = nHD;
                        DATA_.iMANUAL.int_2 = nStage;
                        DATA_.iMANUAL.int_3 = nPkr;
                        DATA_.iMANUAL.bool_1 = ChkTRYVIEW.Checked;
                        break;

                    case ManualNumber.GoodTray1_Clamp:
                    case ManualNumber.GoodTray2_Clamp:
                        DATA_.iMANUAL.bool_1 = ChkTransferClamp.Checked;
                        if (BTN.Name == "OKTray1_FrontClamp" || BTN.Name == "OKTray2_FrontClamp") DATA_.iMANUAL.bool_2 = true;
                        else DATA_.iMANUAL.bool_2 = false;
                        break;

                    case ManualNumber.TrayFeederRdy:
                    case ManualNumber.TrayFeederLoading:
                    case ManualNumber.TrayFeederPlc:
                    case ManualNumber.TrayFeederStacker:
                    case ManualNumber.TrayFeederConvULDStart:
                    case ManualNumber.TrayFeederTrayPusherStart:
                    case ManualNumber.TrayFeederConvUnloading:
                        DATA_.iMANUAL.int_1 = nTray;
                        DATA_.iMANUAL.int_2 = nHD;
                        break;

                    case ManualNumber.TrayPkGoodFeeder:
                    case ManualNumber.TrayPkReworkFeeder:
                    case ManualNumber.TrayPkEmptyFeeder:
                        DATA_.iMANUAL.Option = false;
                        break;


                    case ManualNumber.RunUnitPlc:
                    case ManualNumber.RunStageAirshower:
                    case ManualNumber.RunUnitInspection:
                        DATA_.iMANUAL.iMT1 = mtStage;
                        DATA_.iMANUAL.int_1 = nStage;
                        break;

                    case ManualNumber.SawStageVac:
                        if (BTN.Name == "SawStageVac_On") DATA_.iMANUAL.bool_1 = true;
                        else DATA_.iMANUAL.bool_1 = false;
                        break;
                    case ManualNumber.SawStageRej:
                        if (BTN.Name == "SawStageRej_On") DATA_.iMANUAL.bool_1 = true;
                        else DATA_.iMANUAL.bool_1 = false;
                        break;

                    case ManualNumber.RunPickerCal:
                        DATA_.iMANUAL.iMT1 = mtHead;
                        DATA_.iMANUAL.iMT2 = uHeadT.NumMT;
                        DATA_.iMANUAL.int_1 = nHD;
                        DATA_.iMANUAL.int_2 = Pkr_Num.SelectedIndex;
                        DATA_.iMANUAL.bool_1 = CHK_PK_CAL.Checked;
                        break;

                    case ManualNumber.RunPRS:
                        DATA_.iMANUAL.iMT1 = mtHead;
                        DATA_.iMANUAL.int_1 = nHD;
                        break;

                    case ManualNumber.PkrPic:
                        DATA_.iMANUAL.int_1 = nHD; //eHD
                        DATA_.iMANUAL.int_2 = nStage; //eMAP_BLOCK
                        DATA_.iMANUAL.int_3 = Pkr_Num.SelectedIndex; //PKR NUM
                        DATA_.iMANUAL.int_4 = cMB_Group_X.SelectedIndex;
                        DATA_.iMANUAL.int_5 = cMB_Group_Y.SelectedIndex;
                        DATA_.iMANUAL.int_6 = cMB_UNIT_X.SelectedIndex;
                        DATA_.iMANUAL.int_7 = cMB_UNIT_Y.SelectedIndex;

                        break;

                    case ManualNumber.TopCamCalZigCenter:
                        DATA_.iMANUAL.iMT1 = mtStage;
                        DATA_.iMANUAL.int_1 = nStage;
                        break;

                    default: break;
                }
                DATA_.iMANUAL.bRESULT = COM_.RUN_MANUAL(DATA_.iMANUAL.Number, DATA_.IsSTRING[S.ManualMessage], !cbkNotMSG.Checked);
                if (DATA_.iMANUAL.bRESULT) lbRunManual.Text = DATA_.IsSTRING[S.ManualMessage];
            }
            catch (Exception ex){
                MessageBox.Show(DATA_.IsSTRING[S.ManualMessage] + " Click Fail !" + ETC.NewLine + ex.ToString());
                LogWR_.SaveLogException("MANUAL RUN FAIL", ex);
            }
        }

        private void TmrMANUAL_Tick(object sender, EventArgs e){
            TmrMANUAL.Enabled = false;
            RunMarkInspection.Visible = true;
            Invoke();
            TmrMANUAL.Enabled = true;
        }
        void Invoke(){
            if (DEF.ManualPage == (long)ePage.Loading) ViewLoading();
            if (DEF.ManualPage == (long)ePage.HandlerPk) ViewHandlerPicker();
            if (DEF.ManualPage == (long)ePage.MapBlock) ViewMapBlock();
            if (DEF.ManualPage == (long)ePage.Head) ViewHead();
            if (DEF.ManualPage == (long)ePage.Tray) ViewTray();

            if (DATA_.bMF) lblManualRunning.BackColor = Color.Lime;
            else lblManualRunning.BackColor = Color.White;

            for (int i = 0; i < Input.Length; i++){
                Input[i].BackColor = DATA_.mIN[/*I.MNInput[i]*/ Input[i].TabIndex] ? Color.Lime : Color.DarkGreen;
            }
            for (int o = 0; o < Output.Length; o++){
                Output[o].BackColor = DATA_.mOUT[/*O.MNOutput[o]*/Output[o].TabIndex] ? Color.Red : Color.DarkRed;
            }
#if _NSS3300
            ledMGZ_CONV_B.BackColor = (DATA_.mOUT[O.LD_MGZ_CONVEYOR_CCW] && DATA_.mOUT[O.LD_MGZ_CONVEYOR_BRAKE]) ? Color.Red : Color.DarkRed;
            ledMGZ_CONV_F.BackColor = (DATA_.mOUT[O.LD_MGZ_CONVEYOR_CW] && DATA_.mOUT[O.LD_MGZ_CONVEYOR_BRAKE]) ? Color.Red : Color.DarkRed;
#else
            ledMGZ_CONV_B.BackColor = (DATA_.mOUT[O.LD_CONV_CCW] && !DATA_.mOUT[O.LD_CONV_STOP]) ? Color.Red : Color.DarkRed;
            ledMGZ_CONV_F.BackColor = (DATA_.mOUT[O.LD_CONV_CW] && !DATA_.mOUT[O.LD_CONV_STOP]) ? Color.Red : Color.DarkRed;
#endif
            lbBarcode.Text = SUBFRM_.cBarcode.ReadResult;
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
            uBarcode_Y.CurPosition = DATA_.mtSTS[M.Barcode].CurrentPosition;
            uGripperX.CurPosition = DATA_.mtSTS[M.GrpX].CurrentPosition;
        }
        void ViewHandlerPicker(){
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
        }
        void ViewHead(){
            uBtmCamY.CurPosition = DATA_.mtSTS[M.BtnVisionY].CurrentPosition;
            uBtmCamZ.CurPosition = DATA_.mtSTS[M.BtnVisionZ].CurrentPosition;
            uHeadX.CurPosition = DATA_.mtSTS[mtHead].CurrentPosition;
            uHeadT.CurPosition = DATA_.mtSTS[mtPkTh].CurrentPosition;
            uHeadPkrZ.CurPosition = DATA_.mtSTS[mtPk].CurrentPosition;
            uTrayTransferY.CurPosition = DATA_.mtSTS[mtTrayFeeder].CurrentPosition;
        }
        void ViewTray(){
            uTrayPkr_X.CurPosition = DATA_.mtSTS[M.TrayPickerX].CurrentPosition;
            uTrayPkr_Z.CurPosition = DATA_.mtSTS[M.TrayPickerZ].CurrentPosition;
            uEmptyLift.CurPosition = DATA_.mtSTS[M.EmptyElv].CurrentPosition;
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}