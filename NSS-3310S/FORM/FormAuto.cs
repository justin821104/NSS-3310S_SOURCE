using LIB_.DateType;
using NSS_3310S.ITS;
using NSS_3310S.SEQ;
using Object;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NSS_3310S
{
    public partial class FormAuto : Form{
        Image imgMGZ = new Bitmap(63, 240); //63, 240
        Image imgMAPBLOCK1 = new Bitmap(215, 505); //212, 522 -> 220, 555 -> 220, 525
        Image imgMAPBLOCK2 = new Bitmap(215, 505);
        Image imgTRAY1 = new Bitmap(215, 505);
        Image imgTRAY2 = new Bitmap(215, 505);

        Label[] iInterfaceState = null;
        Label[] oInterfaceState = null;
        Label[] StripIndexNum   = null;
        public PictureBox PIC_X1 { get; set; }
        public PictureBox PIC_X2 { get; set; }
        int nPK = 0;
        Image MGZ { get; set; }
        Image[] STAGE { get; set; }
        Image[] TRAY { get; set; }
        SolidBrush mBrush = new SolidBrush(Color.White);
        Label[] PkrVac { get; set; }
        Button[] Pause { get; set; }
        DataGridViewRow DGV_ROW { get; set; }
        bool bSetSkipView           = false;
        bool bViewCassage           = false;
        bool bViewMachineCassage    = false;

        int uGW, uGH, uX1, uX2, uY2, uW, uH;
        Label LBL;
        Button BTN;

        ToolStripStatusLabel[] ThStatus;
        int isDryOn = 0, isDryOff = 0, isLDOn = 0, isLDOff = 0, isULDOn = 0, isULDOff = 0, isPicOn = 0, isPicOff = 0, isPlcOn = 0, isPlcOff = 0, isTrayOn = 0, isTrayOff = 0;
        int[] PauseOn;
        int[] PauseOff;

        int iswCstSupplyOn = 0, iswCstSupplyOff = 0;

        int EmptyTraySupplyCheckCount = 0;
        int GoodTrayDischargeCount = 0;
        int ReworkTrayDischargeCount = 0;

        public bool bMC_DISPLY  = false;
        public bool bEES_DISPLY = false;
        public bool bEES_FDC    = false;

        public bool bLogView_Display = false;
        private DateTime _uphBaseTime = DateTime.Now;

        public FormAuto(){
            InitializeComponent();
            MGZ = imgMGZ;
            STAGE = new Image[] { imgMAPBLOCK1, imgMAPBLOCK2 };
            TRAY = new Image[] { imgTRAY1, imgTRAY2 };

            StripIndexNum = new Label[] { LBL_BRCD_RAIL_IDX, LBL_BRCD_STRIP_PK_IDX, LBL_BRCD_SAW_IDX, LBL_BRCD_UNIT_PK_IDX, LBL_BRCD_MB_1_IDX, LBL_BRCD_MB_2_IDX };

            iInterfaceState = new Label[] { X_LD_REQ, X_SAW_LD_POS,  X_SAW_STAGE_VAC,
                                           X_ULD_REQ, X_SAW_ULD_POS,  X_SAW_STAGE_REJECT,
                                           CONVER_READY };
            oInterfaceState = new Label[] { Y_SAW_LD_X, Y_SAW_LD_Z, Y_LD_COMPLETE,
                                            Y_SAW_ULD_X, Y_SAW_ULD_Z, Y_ULD_COMPLETE,
                                            CONVER_PASS
            };

            for (int i = 0; i < iInterfaceState.Length; i++){
                iInterfaceState[i].TabIndex = I.InterfaceState[i];
            }
            for (int i = 0; i < oInterfaceState.Length; i++){
                oInterfaceState[i].TabIndex = O.InterfaceState[i];
            }

            PkrVac = new Label[] { lbHD1_PK1, lbHD1_PK2, lbHD1_PK3, lbHD1_PK4, lbHD1_PK5, lbHD1_PK6, lbHD1_PK7, lbHD1_PK8,
                                   lbHD2_PK1, lbHD2_PK2, lbHD2_PK3, lbHD2_PK4, lbHD2_PK5, lbHD2_PK6, lbHD2_PK7, lbHD2_PK8
            };

            Pause = new Button[] { StripStop, PickUpStop, PlaceStop,
                                   PKPickUpStop, PKPlaceStop, TrayStop

            };
            for (int i = 0; i < Pause.Length; i++){
                Pause[i].TabIndex = B.PauseOption[i];
            }

            ThStatus = new ToolStripStatusLabel[] { th_0, th_1, th_2, th_3, th_4, th_5, th_6, th_7, th_8, th_9, th_10, th_11, th_12
            };
            PauseOn = new int[] { isDryOn, isLDOn, isULDOn, isPicOn, isPlcOn, isTrayOn
            };
            PauseOff = new int[] { isDryOff, isLDOff, isULDOff, isPicOff, isPlcOff, isTrayOff
            };


            dgvInfo.RowCount = 4;
            for (int i = 0; i < dgvInfo.RowCount; i++){
                DGV_ROW = dgvInfo.Rows[i];
                DGV_ROW.Height = 20;
            }

            dgvInfo[0, 0].Value = "● MGZ";
            dgvInfo[1, 0].Value = "● RAIL";
            dgvInfo[2, 0].Value = "● STRIP PK";
            dgvInfo[0, 1].Value = "● SAWING";
            dgvInfo[1, 1].Value = "● UNIT PK";
            dgvInfo[2, 1].Value = "● STAGE1";
            dgvInfo[0, 2].Value = "● STAGE2";
            dgvInfo[1, 2].Value = "● HEAD";
            dgvInfo[2, 2].Value = "● OK-TRAY1";
            dgvInfo[0, 3].Value = "● OK-TRAY2";
            dgvInfo[1, 3].Value = "● NG-TRAY";

            
            OptionSkip.Click        += (sender, e) => SettingSkipPanel();
            OptionCstInfo.Click     += (sender, e) => ViewCassage();
            OptionMachineInfo.Click += (sender, e) => ViewMachineInfo();

            lbAIR.DoubleClick       += (sender, e) => EventSkip(lbAIR);
            lbDoor.DoubleClick      += (sender, e) => EventSkip(lbDoor);
            lbTrip.DoubleClick      += (sender, e) => EventSkip(lbTrip);

            StripStop.Click         += (sender, e) => RunningEvent(StripStop);
            PickUpStop.Click        += (sender, e) => RunningEvent(PickUpStop);
            PlaceStop.Click         += (sender, e) => RunningEvent(PlaceStop);
            PKPickUpStop.Click      += (sender, e) => RunningEvent(PKPickUpStop);
            PKPlaceStop.Click       += (sender, e) => RunningEvent(PKPlaceStop);
            TrayStop.Click          += (sender, e) => RunningEvent(TrayStop);
            TrayUnloading.Click     += (sender, e) => RunningEvent(TrayUnloading);
            NGTrayUnloading.Click   += (sender, e) => RunningEvent(NGTrayUnloading);
            swCst_SUPPLY.Click      += (sender, e) => RunningEvent(swCst_SUPPLY);
            PIC_PLC_DISPLAY.Click   += (sender, e) => RunningEvent(PIC_PLC_DISPLAY);
            EES_DISPLAY.Click       += (sender, e) => RunningEvent(EES_DISPLAY);
            FDC_DISPLAY.Click       += (sender, e) => RunningEvent(FDC_DISPLAY);
            WipCheck.Click          += (sender, e) => RunningEvent(WipCheck);

            bMAPBLOCK1_WORK_RESET.Click += (sender, e) => RunningEvent(bMAPBLOCK1_WORK_RESET);
            bMAPBLOCK2_WORK_RESET.Click += (sender, e) => RunningEvent(bMAPBLOCK2_WORK_RESET);

            btnStart.MouseDown      += (sender, e) => StartEvent();
            btnStart.MouseUp        += (sender, e) => DATA_.mIN[I.vtStart] = false;
            btnStop.MouseDown       += (sender, e) => StopEvent();
            btnStop.MouseUp         += (sender, e) => DATA_.mIN[I.vtStart] = false;
            btnRest.MouseDown       += (sender, e) => ResetEvent();
            btnRest.MouseUp         += (sender, e) => DATA_.mIN[I.vtReset] = false;
            btnHOME.MouseDown       += (sender, e) => InitializeEvent();
            btnHOME.MouseUp         += (sender, e) => DATA_.mIN[I.vtInitial] = false;

            BTN_BarCodeRead.MouseDown   += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO){
                    SUBFRM_.cBarcode.bLiveOff = true;
                    UTIL_.DELAY(100);
                    SUBFRM_.cBarcode.TriggerMouseDown();
                }
            };
            BTN_BarCodeRead.MouseUp     += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO) {
                    SUBFRM_.cBarcode.TriggerMouseUp();
                }
            };

            BTN_RFRead.Click += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO) SUBFRM_.gRFID.Read(); };

            LotWrite.Click += (sender, e) => {
                if (DATA_.eMCStatus != eMachineStatus.AUTO && DATA_.eMCStatus != eMachineStatus.INITIAL){
                    /*if (DATA_.prMACHINE[CP.UseMES] == (int)eUSE.USE){
                        W.ViewWarning(T.Manual, W.ManualErrMassage, "MES 사용 모드 입니다 !" + ETC.NewLine + "MES 미사용 모드에서만 적용 가능합니다 !");
                    }
                    else */
                    if (CUSER.Current.ID == ""){
                        W.ViewWarning(T.Manual, W.ManualErrMassage, "USER ID  입력 안되어 있습니다 !" + ETC.NewLine + "USER ID 등록하셔야 진행 가능합니다 !");
                    }
                    else{
                        C.SendSaw.SEND("GET_SVID,*");
                        SUBFRM_.gLotID.INI_();
                    }
                }
            };

            USER_ID_CHANGE.Click += (sender, e) =>{
                if (DATA_.eMCStatus != eMachineStatus.AUTO){
                    SUBFRM_.gUserID.Initailize_View();
                }
            };

            btnLAMP.Click += (sender, e) => DATA_.mOUT[O.FLUORESENT_LIGHT] = !DATA_.mOUT[O.FLUORESENT_LIGHT];

            LOG_VIEW_DISPLAY_0.Click += (sender, e) => SCREEN_CHAGE(LOG_VIEW_DISPLAY_0);
            LOG_VIEW_DISPLAY_1.Click += (sender, e) => SCREEN_CHAGE(LOG_VIEW_DISPLAY_1);

            bSetSkipView = false;

            DocCogBarcode();
            DocMES();

#if _NSS3300
            lbInletVac.Visible  = false;
            lbX310.Visible      = false;
            lbX309.Visible      = false;
            lbX308.Visible      = false;
            lbX300.Visible      = false;
            lbX215.Visible      = false;
            lbX214.Visible      = false;
#else
            lbInletVac.Visible  = true;
            lbX310.Visible      = true;
            lbX309.Visible      = true;
            lbX308.Visible      = true;
            lbX300.Visible      = true;
            lbX215.Visible      = true;
            lbX214.Visible      = true;
#endif
        }

        void RESET_SCREEN(){
            for (int i = 0; i < 6; i++){
                if (Controls.Find("LOG_VIEW_DISPLAY_" + i.ToString(), true).FirstOrDefault() is Button nBtn) nBtn.BackColor = Color.White;
            }
        }
        void SCREEN_VIEW(int nPage){
            RESET_SCREEN();
            if (Controls.Find("LOG_VIEW_DISPLAY_" + nPage.ToString(), true).FirstOrDefault() is Button nBtn) nBtn.BackColor = Color.SeaShell;
            tcLogPage.SelectedIndex = nPage;
        }
        void SCREEN_CHAGE(object sender){
            BTN = sender as Button;
            DEF.AutoLog = Convert.ToInt32(BTN.Tag);
            SCREEN_VIEW(DEF.AutoLog);
        }


        public void IniInfoStripBarcoder(DataGridView grd, int nRowCount, int nHeight){
            DataGridViewRow row;
            grd.Rows.Clear();
            grd.RowCount = nRowCount;
            //grd.Height = 28 + 23;
            for (int i = 0; i < grd.RowCount; i++){
                row = GridEndLot.Rows[i];
                row.Height = nHeight;
                nHeight += row.Height;
            }
            UTIL_.CLEAR_GRID_SELECTED(ref grd);
        }

        public void DocCogBarcode(){
            SUBFRM_.cBarcode.FormBorderStyle = FormBorderStyle.None;
            SUBFRM_.cBarcode.Size = pBARCODE.Size;
            SUBFRM_.cBarcode.Top = 0;
            SUBFRM_.cBarcode.Left = 0;
            SUBFRM_.cBarcode.TopLevel = false;
            pBARCODE.Controls.Add(SUBFRM_.cBarcode);
            SUBFRM_.cBarcode.Show();
        }

        public void DocMES(){
            SUBFRM_.gSecsGem.FormBorderStyle = FormBorderStyle.None;
            SUBFRM_.gSecsGem.Size = pMES.Size;
            SUBFRM_.gSecsGem.Top = 0;
            SUBFRM_.gSecsGem.Left = 0;
            SUBFRM_.gSecsGem.TopLevel = false;
            pMES.Controls.Add(SUBFRM_.gSecsGem);
            SUBFRM_.gSecsGem.Show();
        }
        public void UnDocMes() { pMES.Controls.Remove(SUBFRM_.gSecsGem); }

        void SettingSkipPanel(){
            bSetSkipView = !bSetSkipView;
            if (bSetSkipView){
                SETTING_SKIP.Location = new Point(82, 0);
                SETTING_SKIP.Size = new Size(170, 75);
            } // 82, 0 / 170, 75 
            else{
                SETTING_SKIP.Location = new Point(194, 0);
                SETTING_SKIP.Size = new Size(53, 19);
            } // 194, 0 / 56, 20 53, 19
        }
        void ViewCassage(){
            bViewCassage = !bViewCassage;

            if (bViewCassage){
                VIEW_CASSATE.Location   = new Point(175, 397);
                VIEW_CASSATE.Size       = new Size(75, 268);
            }//165, 396 / 83, 286
            else{
                VIEW_CASSATE.Location   = new Point(175, 397);
                VIEW_CASSATE.Size       = new Size(75, 20);
            }//192, 396 / 56, 20
        }
        void ViewMachineInfo(){
            bViewMachineCassage = !bViewMachineCassage;

            if (bViewMachineCassage){
                VIEW_MACHINE_INFO.Location = new Point(1, 0);
                VIEW_MACHINE_INFO.Size = new Size(362, 165);
            }
            else{
                VIEW_MACHINE_INFO.Location = new Point(300, 0);
                VIEW_MACHINE_INFO.Size = new Size(63, 20);
            }
        }
        void EventSkip(object sender){
            LBL = (Label)sender;
            if (!(DATA_.eLoginLevel >= eLogLevel.ENG/*ADMIN*/)) return;

            if (LBL.Name == "lbAIR"){
                if (DATA_.eLoginLevel < eLogLevel.ENG){
                    DATA_.mAIR_SKIP = false;
                    return;
                }
                if (!DATA_.mAIR_SKIP){
                    if (!UTIL_.PRINT_MASSAGE("Do you want to skip the check machine air ?" + ETC.NewLine + "설비 에어 체크 스킵 하시겠습니까 ?", false, false, false)) return;
                }
                DATA_.mAIR_SKIP = !DATA_.mAIR_SKIP;
            }
            if (LBL.Name == "lbDoor"){
                if (DATA_.eLoginLevel < eLogLevel.ENG){
                    DATA_.mDOOR_SKIP = false;
                    return;
                }
                if (!DATA_.mDOOR_SKIP){
                    if (!UTIL_.PRINT_MASSAGE("Do you want to skip the check door ?" + ETC.NewLine + "도어 체크 스킵 하시겠습니까 ?", false, false, false)) return;
                }
                DATA_.mDOOR_SKIP = !DATA_.mDOOR_SKIP;
            }
            if (LBL.Name == "lbTrip"){
                if (DATA_.eLoginLevel < eLogLevel.ENG){
                    DATA_.mTRIP_SKIP = false;
                    return;
                }
                if (!DATA_.mTRIP_SKIP){
                    if (!UTIL_.PRINT_MASSAGE("Do you want to skip the check trip ?" + ETC.NewLine + "CP TRIP 신호 체크 스킵 하시겠습니까 ?", false, false, false)) return;
                }
                DATA_.mTRIP_SKIP = !DATA_.mTRIP_SKIP;
            }
        }
        void RunningEvent(object sender){
            BTN = (Button)sender;

            if (BTN.Name == "StripStop"){
                if (DATA_.IsBIT[BTN.TabIndex]){
                    LogWR_.SaveLogOperate("스트립 공급 일시 정지 해제", "MC");
                    DATA_.IsBIT[BTN.TabIndex] = false;
                    return;
                }
                if (!UTIL_.PRINT_MASSAGE("스트립 공급 일시 정지 하시겠습니까 ?", false, false, false)) return;
                LogWR_.SaveLogOperate("스트립 공급 일시 정지 활성화", "MC");
                DATA_.IsBIT[BTN.TabIndex] = true;
            }
            if (BTN.Name == "PickUpStop"){
                if (DATA_.IsBIT[BTN.TabIndex]){
                    LogWR_.SaveLogOperate("다이싱 테이블 스트립 공급 일시 정지 해제", "MC");
                    DATA_.IsBIT[BTN.TabIndex] = false;
                    return;
                }
                LogWR_.SaveLogOperate("다이싱 테이블 스트립 공급 일시 정지 활성화", "MC");
                DATA_.IsBIT[BTN.TabIndex] = true;
            }
            if (BTN.Name == "PlaceStop"){
                if (DATA_.IsBIT[BTN.TabIndex]){
                    LogWR_.SaveLogOperate("다이싱 테이블 유닛 픽업 일시 정지 해제", "MC");
                    DATA_.IsBIT[BTN.TabIndex] = false;
                    return;
                }
                LogWR_.SaveLogOperate("다이싱 테이블 유닛 픽업 일시 정지 활성화", "MC");
                DATA_.IsBIT[BTN.TabIndex] = true;
            }
            if (BTN.Name == "PKPickUpStop"){
                if (DATA_.IsBIT[BTN.TabIndex]){
                    LogWR_.SaveLogOperate("맵-블록 유닛 픽업 일시 정지 해제", "MC");
                    DATA_.IsBIT[BTN.TabIndex] = false;
                    return;
                }
                LogWR_.SaveLogOperate("맵-블록 유닛 픽업 일시 정지 활성화", "MC");
                DATA_.IsBIT[BTN.TabIndex] = true;
            }
            if (BTN.Name == "PKPlaceStop"){
                if (DATA_.IsBIT[BTN.TabIndex]){
                    LogWR_.SaveLogOperate("트레이 유닛 플레이스 일시 정지 해제", "MC");
                    DATA_.IsBIT[BTN.TabIndex] = false;
                    return;
                }
                LogWR_.SaveLogOperate("트레이 유닛 플레이스 일시 정지 활성화", "MC");
                DATA_.IsBIT[BTN.TabIndex] = true;
            }
            if (BTN.Name == "TrayStop"){
                if (DATA_.IsBIT[BTN.TabIndex]){
                    LogWR_.SaveLogOperate("트레이 공급 일시 정지 해제", "MC");
                    DATA_.IsBIT[BTN.TabIndex] = false;
                    return;
                }
                LogWR_.SaveLogOperate("트레이 공급 일시 정지 활성화", "MC");
                DATA_.IsBIT[BTN.TabIndex] = true;
            }

            if (BTN.Name == "TrayUnloading"){
                if (!DATA_.IsBIT[B.GoodTrayWork]) return;
                if (DATA_.IsBIT[B.TrayConveyorUnloadingWork]) return;
                if (DATA_.IsBIT[B.GoodTrayUnloadingMode]){
                    DATA_.IsBIT[B.GoodTrayUnloadingMode] = false;
                    return;
                }
                if (DATA_.IsBIT[B.X1PlcBusy] || DATA_.IsBIT[B.X2PlcBusy]){
                    DATA_.IsBIT[B.GoodTrayUnloadingMode] = true;
                } //피커 PLACE 작업 진행 중 TRAY 배출 못함.
                else{
                    DATA_.IsBIT[B.GoodTrayAutoUnloading] = true;
                    DATA_.IsBIT[B.GoodTrayWork] = false;
                }
            }
            if (BTN.Name == "NGTrayUnloading"){
                if (!DATA_.IsBIT[B.ReWorkTrayWork]) return;
                if (DATA_.IsBIT[B.ReWorkTrayUnloadingMode]){
                    DATA_.IsBIT[B.ReWorkTrayUnloadingMode] = false;
                    return;
                }
                if (DATA_.IsBIT[B.X1NGPlcBusy] || DATA_.IsBIT[B.X2NGPlcBusy]){
                    DATA_.IsBIT[B.ReWorkTrayUnloadingMode] = true;
                } //피커 PLACE 작업 진행 중 TRAY 배출 못함.
                else{
                    DATA_.IsBIT[B.ReWorkTrayWork] = false;
                }
            }

            if (BTN.Name == "bMAPBLOCK1_WORK_RESET"){
                if (DATA_.IsBIT[B.Stage1JobCancel]){
                    DATA_.IsBIT[B.Stage1JobCancel] = false;
                    return;
                }
                if (!UTIL_.PRINT_MASSAGE("테이블1 작업 취소 ?" + ETC.NewLine + "(작업 취소)", false, false, false)) return;
                DATA_.IsBIT[B.Stage1JobCancel] = true;
            }
            if (BTN.Name == "bMAPBLOCK2_WORK_RESET"){
                if (DATA_.IsBIT[B.Stage2JobCancel]){
                    DATA_.IsBIT[B.Stage2JobCancel] = false;
                    return;
                }
                if (!UTIL_.PRINT_MASSAGE("테이블2 작업 취소 ?" + ETC.NewLine + "(작업 취소)", false, false, false)) return;
                DATA_.IsBIT[B.Stage2JobCancel] = true;
            }

            if (BTN.Name == "swCst_SUPPLY"){
                if (!DATA_.IsBIT[B.CstRequest]) return;
                if (!UTIL_.PRINT_MASSAGE("CST. SUPPLY ?", false, false, false)) return;
                DATA_.IsBIT[B.CstRequest] = false;
            }

            if (BTN.Name == "PIC_PLC_DISPLAY"){
                ResetDisplay();
                bMC_DISPLY = true;
            }
            if (BTN.Name == "EES_DISPLAY"){
                ResetDisplay();
                bEES_DISPLY = true;
            }
            if (BTN.Name == "FDC_DISPLAY"){
                ResetDisplay();
                bEES_FDC = true;
            }

            if (BTN.Name == "WipCheck"){
                if (DATA_.prMACHINE[CP.UseMES] != (int)eUSE.USE){
                    TEACH_.WR_MCPara(CP.UseMES, (int)eUSE.USE);
                }
                else{
                    if (!UTIL_.PRINT_MASSAGE("MES 스킵 하시고 설비 구동 하시겠습니까 ?", false, false, false)) return;
                    TEACH_.WR_MCPara(CP.UseMES, (int)eUSE.NotUSE);
                }
            }
        }

        void ResetDisplay(){
            bMC_DISPLY = false;
            bEES_DISPLY = false;
            bEES_FDC = false;
        }

        void StartEvent(){
            DATA_.mIN[I.vtStart] = true;
            DATA_.IsLONG[L.OffNumber] = I.vtStart;
        }
        void StopEvent(){
            if (DATA_.IsBIT[B.WaitLotEndProcessing]) return;

            DATA_.mIN[I.vtStop] = true;
            DATA_.IsLONG[L.OffNumber] = I.vtStop;
        }
        void ResetEvent(){
            DATA_.mIN[I.vtReset] = true;
            DATA_.IsLONG[L.OffNumber] = I.vtReset;
            DATA_.IsBIT[B.WaitLotEndProcessing] = false;
        }

        private void BTN_CNT_RESET_Click(object sender, EventArgs e){
            if (DATA_.eMCStatus == eMachineStatus.AUTO) return;
            DEF.CountReset(false);
        }

        private void ClickEvent_LotEndProcess(object sender, EventArgs e) {
            DATA_.IsBIT[B.WaitLotEndProcessing] = false;
        }
        private void BTN_TEST_Click(object sender, EventArgs e){

            //C.RecieveVision.ManualSecuss();
            //BASE.ReadPRS(T.Head1, eHD.HD1);
            //double dValue = 0;
            //BASE.RD_BladeThickness(ref dValue);
            //DEF.SaveProduction();
            //if (lbITS_ID.Text == ""){
            //   MessageBox.Show("ITS ID 없습니다.");
            //    return;
            //}
            //MsSQL.GetPrevProcResult(lbITS_ID.Text);

            //C.SendSaw.SEND("GET_SVID,*");
            //C.SendSaw.SEND("GET_PVID,*");
            //DEF.SetParaFDC();
            //DEF.CurDataFDC();
            //C.SendVision.SEND("GET_SVID_VISION,*");

            //TEACH_.DEL_STRIP_INFO();

            //CLOT.GET_LOT.ProcCondition_1 = "";
            //CLOT.GET_LOT.ProcCondition_2 = "1287129-009SB#2300#QC GATE (SAP)#1/4#Lot 보류#1170#SPC_Y_RULEOUT#WIP증가#SPC##2022-04-18 오후 2:21:04";
            //CLOT.GET_LOT.ProcCondition_3 = "1287129-009SB#2300#QC GATE (SAP)#1/4#Lot 보류#1170#SPC_Y_RULEOUT#WIP증가#SPC##2022-04-18 오후 2:21:04";
            //CLOT.GET_LOT.ProcCondition_4 = "3050#R1341#SAWING(UNIT)#0.7000#0.0750#0.0750#0.7000#0.0750#0.0750";
            //AddConditionMassage();

            //
            //UTIL_.SAVE_WORKED_LOT_INFO();

            //로그 테스트
            //LogWR_.SaveLogOperate("LOT-END SIGNAL ON-OFF", "MC");
            //LOT END 메세지 
            //COM_.ViewWarning(-1, W.LotEndComplete);


            //LOT별 로그 확인!
            //LogWR_.SaveLogLotInfo();

            //LOT INFO 저장
            //UTIL_.WR_VISION_RECEIP();

            ////blade 정보 테스트
            //LogWR_.SaveBladeInfo(CLOT.SawStageStripBarcode, "In", CLOT.SawStageStripIndex);
            //LogWR_.SaveBladeInfo(CLOT.InfoStrip[T.UnitPk].Barcode, "Out", CLOT.InfoStrip[T.UnitPk].Index);

            ////stripdata 
            //bool Wlot = LogWR_.WriteLotInfo(CLOT.GET_LOT.LotID);
            //LogWR_.Log_StripData(textBox1.Text, CLOT.GET_LOT.Qty, 30, 25, 0, 0, 5);

            ////KIT CLEANING 
            //DATA_.IsBIT[B.LotStart_KitCleanning] = true;
            //DATA_.IsBIT[B.KitCleaning] = true;

            ////Output파일 
            ////// saw place 후 //_13370420003B 0
            //CLOT.GET_LOT.ToolNo = "22FCB016-04"; //"22FCB104-03"; //테스트
            //CLOT.GET_LOT.LotID  = textBox2.Text; //테스트
            //CLOT.GET_LOT.ItsID = textBox2.Text + " 0";
            //LogWR_.WriteInStrip(textBox1.Text);
            ////// lot-end 클릭 후
            //LogWR_.WriteLotStrip();

            //
            //double tVal = 1;
            //LBL_TEST.Text = tVal.ToString("G");  //"F3", CultureInfo.InvariantCulture

            //C.SendSaw.SEND("GET_SVID,*");


            //int pkZNum = 1;
            //double pkZOffset = textBox1.Text == "" ? 0 : double.Parse(textBox1.Text);  //0.05;
            //string pkZCmd = pkZOffset == 0 ? "" : "offset=" + pkZOffset.ToString();
            //LBL_TEST.Text = pkZNum.ToString() + "/" + pkZCmd;

            if (textBox1.Text != "") {
                FILE_.WR_File(PATH_.MES_ABFMATERIAL, textBox1.Text, false);  // GZ41R2H 공유폴더 안에 저장
            }
        }

        private void ModuleBarcodeInput_DoubleClick(object sender, EventArgs e){
            LBL                 = (Label)sender;
            int nNumber = LBL.TabIndex;
            string sOldValue    = LBL.Text ?? "";
            string sLabel       = LBL.Tag.ToString();
            
            LBL.BackColor = Color.Lime;
            StripIndexNum[nNumber].BackColor = Color.Lime;
            string sValue       = UTIL_.INPUT_MESSAGE("BARCODE", sLabel + " ZONE STRIP BARCODE", sOldValue, false);
            
            if (LBL.Name == "LBL_BRCD_RAIL")        CLOT.InfoStrip[T.Gripper].Barcode   = sValue;
            if (LBL.Name == "LBL_BRCD_STRIP_PK")    CLOT.InfoStrip[T.StripPk].Barcode   = sValue;
            if (LBL.Name == "LBL_BRCD_SAW")         CLOT.SawStageStripBarcode           = sValue;
            if (LBL.Name == "LBL_BRCD_UNIT_PK")     CLOT.InfoStrip[T.UnitPk].Barcode    = sValue;
            if (LBL.Name == "LBL_BRCD_MB_1")        CLOT.InfoStrip[T.DryTable1].Barcode = sValue;
            if (LBL.Name == "LBL_BRCD_MB_2")        CLOT.InfoStrip[T.DryTable2].Barcode = sValue;
            
            LBL.Text = sValue;
            StripIndexNum[nNumber].BackColor = Color.White;
            LBL.BackColor = Color.White;
        }

        public void QueryInitialize(){
            if (DATA_.eMCStatus == eMachineStatus.AUTO) return;
            if (!UTIL_.PRINT_MASSAGE("EXCUTE INITIALIZE ?" + ETC.CrLf + "설비 초기화 하시겠습니까 ?", false, false, false)) return;
            DATA_.mIN[I.vtInitial] = true;
            DATA_.IsLONG[L.OffNumber] = I.vtInitial;
            SUBFRM_.gIni.Show();
            SUBFRM_.gIni.SetBounds(1, 1, SUBFRM_.gIni.Width, SUBFRM_.gIni.Height);
        }

        void InitializeEvent(){
            if (DATA_.editErrName != "NONE"){
                MessageBox.Show("YOU MUST RESET THE ALARM.");
                return;
            }
            QueryInitialize();
        }

        public void Initailize_View(){
            if (DEF.AutoLog < 0) SCREEN_CHAGE(LOG_VIEW_DISPLAY_0);
            TmrAUTO.Enabled = true;
            TRearTime.Enabled = true;
            Show();
            BringToFront();
        }
        public void Set_Language(){

        }

        bool GetProcLog(int thread){
            bool bSkip = false;
            switch (thread){

                default: break;
            }
            return bSkip;
        }

        void AddProcessMessage(int tn, string msg){
            if (tn < 0) return;
            if (this.InvokeRequired) {
                this.BeginInvoke(new Action(() => AddProcessMessage(tn, msg)));
                return;
            }

            try {
                listLog.BeginUpdate();
                if (listLog.Items.Count > 1000) listLog.Items.RemoveAt(0);
                ListViewItem lvi = new ListViewItem(DateTime.Now.ToString("HH:mm:ss.ff"));
                if (GetProcLog(tn)) lvi.ForeColor = Color.Gold; //return; //LOG SKIP일 경우 
                lvi.SubItems.Add(DATA_.ThreadName[tn]);
                lvi.SubItems.Add(msg);
                listLog.Items.Add(lvi);

                if (listLog.Items.Count > 0) listLog.Items[listLog.Items.Count - 1].EnsureVisible();
                listLog.EndUpdate();
            }
            catch (Exception EX){
                LogWR_.SaveLogException("[AUTO] ADD MESSAGE FAIL!" + ETC.NewLine + "THREAD NUMBER = " + tn.ToString() + " / " + msg, EX);
            }
        }

        void AddConditionMassage(){
            try{
                LBX_PROC_CONDITION_MESSAGE.Items.Clear();
                LBX_PROC_CONDITION_MESSAGE.Items.Add(CLOT.GET_LOT.ProcCondition_1);
                LBX_PROC_CONDITION_MESSAGE.Items.Add(CLOT.GET_LOT.ProcCondition_2);
                LBX_PROC_CONDITION_MESSAGE.Items.Add(CLOT.GET_LOT.ProcCondition_3);
                LBX_PROC_CONDITION_MESSAGE.Items.Add(CLOT.GET_LOT.ProcCondition_4);
            }
            catch (Exception EX){
                LogWR_.SaveLogException("[AUTO] ADD CONDITION MESSAGE FAIL!", EX);
            }
        }

        List<PictureBox> _hd1List = new List<PictureBox>();
        List<PictureBox> _hd2List = new List<PictureBox>();
       
        public void CreatePicker(){
            _hd1List.Clear();
            _hd2List.Clear();

            for (int i = 0; i < CNT_.PKR; i++){
                this.PIC_X1 = new PictureBox();
                PIC_X1.Location = new Point(16 + (i * 43), 16);
                PIC_X1.Size = new Size(30, 55); //33, 65
                PIC_X1.Tag = i;//= cMT.MT_HD1_Z12 + dTAG;                     
                PIC_X1.BackgroundImageLayout = ImageLayout.Stretch;
                gbxHD1.Controls.Add(PIC_X1);
                _hd1List.Add(PIC_X1);
            }
            for (short i = 0; i < CNT_.PKR; i++){
                this.PIC_X2 = new PictureBox();
                PIC_X2.Location = new Point(16 + (i * 43), 16); //15 + (k * 43), 15
                PIC_X2.Size = new Size(30, 55); //32, 60
                PIC_X2.Tag = i;//= cMT.MT_HD2_Z12 + dTAG;  //= CLS_DEF.MT_Z10 + k;
                PIC_X2.BackgroundImageLayout = ImageLayout.Stretch;
                gbxHD2.Controls.Add(PIC_X2);
                _hd2List.Add(PIC_X2);
            }
            INFO_HD1_PICKER.SendToBack();
            INFO_HD2_PICKER.SendToBack();
        }

        void INI_GRID(){
            DataGridViewRow row;
            int nHeight = 20 + 2;
            GridEndLot.RowCount = CLOT.FINISH_LOT.Length;
            for (int i = 0; i < GridEndLot.RowCount; i++){
                //GridEndLot[0, i].Value = (i + 1).ToString();
                row = GridEndLot.Rows[i];
                row.Height = 20;
                nHeight += row.Height;
            }
            UTIL_.CLEAR_GRID_SELECTED(ref GridEndLot);
            GridEndLot.Height = nHeight;
            GridEndLot.CurrentCell.Selected = false; // CELL 선택 안되게

            nHeight = 20 + 2;
            GridWorkLot.RowCount =  1;
            for (int i = 0; i < GridWorkLot.RowCount; i++){
                //GridWorkLot[0, i].Value = (i + 1).ToString();
                row = GridWorkLot.Rows[i];
                row.Height = 20;
                nHeight += row.Height;
            }
            UTIL_.CLEAR_GRID_SELECTED(ref GridWorkLot);
            GridWorkLot.Height = nHeight;
        } 

        void IniGridSvid(){
            DGV_FDC_INFO.RowCount = CSVID.FDC_NUM.Length;
            for (int n = 0; n < CSVID.FDC_NUM.Length; n++){
                DGV_FDC_INFO.Rows[n].Cells[0].Value = CSVID.FDC_NUM[n];
            }

            DGV_VID_INFO.RowCount = CSVID.VID_NUM.Length;
            for (int n = 0; n < CSVID.VID_NUM.Length; n++){
                DGV_VID_INFO.Rows[n].Cells[0].Value = CSVID.VID_NUM[n];
            }
        }

        private void FormAuto_Load(object sender, EventArgs e){
            CreatePicker();
            //COM_.RECREATE_THREAD(ref DATA_.mcTH[T.Picker], MMI_THREAD);

            LogWR_.ProMsgEvent  += AddProcessMessage;
            BASE.ProMsgEvent    += AddProcessMessage;
             
            SUBFRM_.cBarcode.Conect();
            INI_GRID();
            RunningEvent(EES_DISPLAY);

            gbxOP.Text = DEF.UpdataMemo;

            COM_.SetFrame(pLotEndProcessing, false, 750, 65, 500, 150);

            LBX_PROC_CONDITION_MESSAGE.Items.Clear();
            IniGridSvid();
        }

        private void FormAuto_FormClosing(object sender, FormClosingEventArgs e){
            LogWR_.ProMsgEvent  -= AddProcessMessage;
            BASE.ProMsgEvent    -= AddProcessMessage;
        }

        void InvokeSignal(){
            PowerMeter.BackColor        = DATA_.cPM.IsOpen() ? Color.Lime : Color.White;
            RFReader.BackColor          = SUBFRM_.gRFID.bOpen ? Color.Lime : Color.White;
            Barcode.BackColor           = SUBFRM_.cBarcode.bOpen ? Color.Lime : Color.White;
            lbComSawMachine.BackColor   = DATA_.mIN[I.SAW_READY] ? Color.Lime : Color.White;
            lbComSorterVision.BackColor = DATA_.mIN[I.VisionRdy] ? Color.Lime : Color.White;

            lblSAW_MAIN_AIR.BackColor   = DATA_.mIN[I.SAW_MAIN_AIR] ? Color.Lime : Color.DarkGreen;
            lbStripPkrVac.BackColor     = DATA_.mIN[I.STRIP_PK_VAC] ? Color.Lime : Color.White;
            lbUnitPkrVac1.BackColor     = DATA_.mIN[I.UNIT_PK_VAC] ? Color.Lime : Color.White;
            lbUnitPkrScrap.BackColor    = DATA_.mIN[I.SCRAP_VAC1] ? Color.Lime : Color.White;
            lbUnitPkrScrap2.BackColor   = DATA_.mIN[I.SCRAP_VAC2] ? Color.Lime : Color.White;
            lbSORTER_MAIN_AIR.BackColor = DATA_.mIN[I.DRIVER_AIR_PRESSURE] && DATA_.mIN[I.BLOW_AIR_PRESSURE] && DATA_.mIN[I.STAGE_AIR_PRESSURE] && DATA_.mIN[I.PICKER_AIR_PRESSURE] ? Color.Lime : Color.White;
            lbMapBlock1.BackColor       = DATA_.mIN[I.STAGE_VACUUM1] ? Color.Lime : Color.White;
            lbMapBlock2.BackColor       = DATA_.mIN[I.STAGE_VACUUM2] ? Color.Lime : Color.White;

            for (int i = 0; i < iInterfaceState.Length; i++){
                iInterfaceState[i].ForeColor = DATA_.mIN[iInterfaceState[i].TabIndex] ? Color.Lime : Color.White;
            }
            for (int i = 0; i < oInterfaceState.Length; i++){
                oInterfaceState[i].ForeColor = DATA_.mOUT[oInterfaceState[i].TabIndex] ? Color.Red : Color.White;
            }

            lbRFID.Text = SUBFRM_.gRFID.ReadRFID;
            lbBarcode.Text = SUBFRM_.cBarcode.ReadResult;

            if (tclAUTOVIEW.SelectedIndex == 0) AUTOVIEW_0();
            if (tclAUTOVIEW.SelectedIndex == 1) AUTOVIEW_1();
            if (tclAUTOVIEW.SelectedIndex == 2) AUTOVIEW_2();
            
            lbDoor.BackColor = DATA_.mDOOR_SKIP ? Color.Red : Color.DarkRed;
            lbAIR.BackColor = DATA_.mAIR_SKIP ? Color.Red : Color.DarkRed;
            lbTrip.BackColor = DATA_.mTRIP_SKIP ? Color.Red : Color.DarkRed;

            lbDoor.Text = DATA_.mDOOR_SKIP ? "DOOR ERROR SKIP" : "DOOR ERROR CHECK";
            lbAIR.Text = DATA_.mAIR_SKIP ? "MAIN AIR SKIP" : "MAIN AIR CHECK";
            lbTrip.Text = DATA_.mTRIP_SKIP ? "TRIP ERROR SKIP" : "TRIP ERROR CHECK";

            bMAPBLOCK1_WORK_RESET.BackColor = DATA_.IsBIT[B.Stage1JobCancel] ? Color.Red : Color.White;
            bMAPBLOCK2_WORK_RESET.BackColor = DATA_.IsBIT[B.Stage2JobCancel] ? Color.Red : Color.White;

            btnLAMP.BackColor = DATA_.mOUT[O.FLUORESENT_LIGHT] ? Color.LightYellow : Color.White;

            for (int i = 0; i < ThStatus.Length; i++){
                ThStatus[i].Text = DATA_.LogThread[DATA_.thSeqThrad[i]].thSTS;
            }

            lbGT1.BackColor = (int)eTRAY.GOOD1 == (int)DATA_.IsLONG[L.CurWorkTray] ? Color.Lime : Color.White;
            lbGT2.BackColor = (int)eTRAY.GOOD2 == (int)DATA_.IsLONG[L.CurWorkTray] ? Color.Lime : Color.White;

            lbDly_X1.Text                   = DATA_.mtSTS[M.TRIGGER1].CurrentPosition.ToString("0.0");
            lbDly_X2.Text                   = DATA_.mtSTS[M.TRIGGER2].CurrentPosition.ToString("0.0");

            lbDlyCAXC_X1.Text               = DATA_.cntSTS[DATA_.SubTrigger[0]].CurrentPosition.ToString("0.0");
            lbDlyCAXC_X2.Text               = DATA_.cntSTS[DATA_.SubTrigger[1]].CurrentPosition.ToString("0.0");

            lbDly_T1.Text                   = DATA_.mtSTS[M.X1T].CurrentPosition.ToString();
            lbDly_T2.Text                   = DATA_.mtSTS[M.X2T].CurrentPosition.ToString();

            lbDly_UnitPkPicOffset_X.Text    = DATA_.IsDOUBLE[D.UnitPk_PicOffsetX].ToString();

            lbDly_Stage1Offset_X.Text       = DATA_.IsDOUBLE[D.Stage1UnitOffsetX].ToString();
            lbDly_Stage1Offset_Y.Text       = DATA_.IsDOUBLE[D.Stage1UnitOffsetY].ToString();

            lbDly_Stage2Offset_X.Text       = DATA_.IsDOUBLE[D.Stage2UnitOffsetX].ToString();
            lbDly_Stage2Offset_Y.Text       = DATA_.IsDOUBLE[D.Stage2UnitOffsetY].ToString();

            lb_MGZ_CNT.Text                 = DATA_.IsLONG[L.DayMGZCnt].ToString();
            lb_STRIP_CNT.Text               = DATA_.IsLONG[L.DayStripCnt].ToString();
            lb_GOOD_CNT.Text                = DATA_.IsLONG[L.DayGoodUnit].ToString();
            lb_REWORK_CNT.Text              = DATA_.IsLONG[L.DayReworkUnit].ToString();
            //lb_REJECT_CNT.Text              = DATA_.IsLONG[L.DayRejectUnit].ToString();
            
            LBL_EQPCODE.Text                = DATA_.EQPCode;
            lbLOT_ID.Text                   = CLOT.GET_LOT.LotID;
            lbITS_ID.Text                   = CLOT.GET_LOT.ItsID;

            LBL_USER_ID.Text                = CUSER.Current.ID;
            LBL_USER_NAME.Text              = CUSER.Current.Name;

            lbStripPkrXSafety.BackColor     = DATA_.prMACHINE[CP.StripPkSafetyPosition] < DATA_.mtSTS[M.StripPkX].CurrentPosition ? Color.Red : Color.White;
            lbUnitPkrXSafety.BackColor      = DATA_.prMACHINE[CP.UnitPkSafetyPosition] < DATA_.mtSTS[M.UnitPkX].CurrentPosition ? Color.Red : Color.White;

            UseHD1.BackColor                = (DATA_.prMACHINE[CP.SelectHead] == (int)eHD.ALL || DATA_.prMACHINE[CP.SelectHead] == (int)eHD.HD1) ? Color.Lime : Color.Red;
            UseHD2.BackColor                = (DATA_.prMACHINE[CP.SelectHead] == (int)eHD.ALL || DATA_.prMACHINE[CP.SelectHead] == (int)eHD.HD2) ? Color.Lime : Color.Red;

            if (DATA_.bWriteLotInfo){
                DATA_.bWriteLotInfo = false;
                UTIL_.SAVE_WORKED_LOT_INFO();
                DEF.CountReset(true);
            }

            if (DATA_.bStripDefect){
                DATA_.bStripDefect  = false;
                AddConditionMassage();
                DATA_.IsBIT[B.WaitITSReading] = true;

                MsSQL.GetPrevProcResult(CLOT.GET_LOT.ItsID);  //CLOT.GET_LOT.ITS
                                                              //if (DATA_.prMACHINE[DATA_.UseMES] == (int)eUSE.USE){
            }

            PIC_PLC_DISPLAY.BackColor   = bMC_DISPLY ? Color.Lime : Color.White;
            EES_DISPLAY.BackColor       = bEES_DISPLY ? Color.Lime : Color.White;
            FDC_DISPLAY.BackColor       = bEES_FDC ? Color.Lime: Color.White;

            lbX314.BackColor = DATA_.mIN[I.GOOD_TRAY1_FEEDER_TRAY_CHECK] ? Color.Lime : Color.White;
            lbX401.BackColor = DATA_.mIN[I.GOOD_TRAY2_FEEDER_TRAY_CHECK] ? Color.Lime : Color.White;
            lbX307.BackColor = DATA_.mIN[I.GOOD_RAIL_STACKER_CHECK] ? Color.Lime : Color.White;
            lbX303.BackColor = DATA_.mIN[I.NG_TRAY_FEEDER_TRAY_CHECK] ? Color.Lime : Color.White;
            lbX213.BackColor = DATA_.mIN[I.NG_RAIL_STACKER_TRAY_CHECK] ? Color.Lime : Color.White;
#if _NSS3300
#else
            lbInletVac.BackColor = DATA_.mIN[I.INLET_TABLE_VAC] ? Color.Lime : Color.White;

            lbX310.BackColor = DATA_.mIN[I.GOOD_RAIL_HEAD2_CHECK] ? Color.Lime : Color.White;
            lbX309.BackColor = DATA_.mIN[I.GOOD_RAIL_HEAD1_CHECK] ? Color.Lime : Color.White;
            lbX308.BackColor = DATA_.mIN[I.GOOD_RAIL_TRAY_LOADING_CHECK] ? Color.Lime : Color.White;

            lbX300.BackColor = DATA_.mIN[I.NG_RAIL_HEAD2_TRAY_CHECK] ? Color.Lime : Color.White;
            lbX215.BackColor = DATA_.mIN[I.NG_RAIL_HEAD1_TRAY_CHECK] ? Color.Lime : Color.White;
            lbX214.BackColor = DATA_.mIN[I.NG_RAIL_LOADING_TRAY_CHECK] ? Color.Lime : Color.White;
#endif      
			dgvInfo[0, 0].Style.BackColor = DATA_.IsBIT[B.MGZWorking] ? Color.Lime : Color.White;
            dgvInfo[1, 0].Style.BackColor = DATA_.IsBIT[B.GripperWorking] ? Color.Lime : Color.White;
            dgvInfo[2, 0].Style.BackColor = DATA_.IsBIT[B.StripPkMask] ? Color.Lime : Color.White;
            dgvInfo[0, 1].Style.BackColor = DATA_.mIN[I.SAW_CUTTING] ? Color.Lime : Color.White;
            dgvInfo[1, 1].Style.BackColor = DATA_.IsBIT[B.UnitPkMask] ? Color.Lime : Color.White;
            dgvInfo[2, 1].Style.BackColor = DATA_.IsBIT[B.Stage1Working] ? Color.Lime : Color.White;
            dgvInfo[0, 2].Style.BackColor = DATA_.IsBIT[B.Stage2Working] ? Color.Lime : Color.White;
            dgvInfo[1, 2].Style.BackColor = DATA_.IsBIT[B.X1Working] || DATA_.IsBIT[B.X2Working] ? Color.Lime : Color.White;
            dgvInfo[2, 2].Style.BackColor = DATA_.IsBIT[B.GoodTray1Place] ? Color.Lime : Color.White;
            dgvInfo[0, 3].Style.BackColor = DATA_.IsBIT[B.GoodTray2Place] ? Color.Lime : Color.White;
            dgvInfo[1, 3].Style.BackColor = DATA_.IsBIT[B.ReWorkTrayWork] ? Color.Lime : Color.White;

            LBL_BRCD_RAIL.Text              = CLOT.InfoStrip[T.Gripper].Barcode;
            LBL_BRCD_STRIP_PK.Text          = CLOT.InfoStrip[T.StripPk].Barcode;
            LBL_BRCD_SAW.Text               = CLOT.SawStageStripBarcode;
            LBL_BRCD_UNIT_PK.Text           = CLOT.InfoStrip[T.UnitPk].Barcode;
            LBL_BRCD_MB_1.Text              = CLOT.InfoStrip[T.DryTable1].Barcode;
            LBL_BRCD_MB_2.Text              = CLOT.InfoStrip[T.DryTable2].Barcode;

            LBL_BRCD_RAIL_IDX.Text          = CLOT.InfoStrip[T.Gripper].Index == 0 ? "" : CLOT.InfoStrip[T.Gripper].Index.ToString();
            LBL_BRCD_STRIP_PK_IDX.Text      = CLOT.InfoStrip[T.StripPk].Index == 0 ? "" : CLOT.InfoStrip[T.StripPk].Index.ToString();
            LBL_BRCD_SAW_IDX.Text           = CLOT.SawStageStripIndex == 0 ? "" : CLOT.SawStageStripIndex.ToString();
            LBL_BRCD_UNIT_PK_IDX.Text       = CLOT.InfoStrip[T.UnitPk].Index == 0 ? "" : CLOT.InfoStrip[T.UnitPk].Index.ToString();
            LBL_BRCD_MB_1_IDX.Text          = CLOT.InfoStrip[T.DryTable1].Index == 0 ? "" : CLOT.InfoStrip[T.DryTable1].Index.ToString();
            LBL_BRCD_MB_2_IDX.Text          = CLOT.InfoStrip[T.DryTable2].Index == 0 ? "" : CLOT.InfoStrip[T.DryTable2].Index.ToString();

            lbMGZ_CHECK_1.BackColor = DATA_.mIN[I.LD_CONV_MZ_CHECK1] ? Color.Lime : Color.White;
            lbMGZ_CHECK_2.BackColor = DATA_.mIN[I.LD_CONV_MZ_CHECK2] ? Color.Lime : Color.White;

            LBL_CNT_PANEL.Text = CLOT.CurLotStripCnt.ToString();
        }
        void AUTOVIEW_0(){
            lbVAC_MB1.BackColor     = DATA_.mOUT[O.STAGE1_VAC] ? Color.Red : Color.White;
            lbDRAIN_MB1.BackColor   = DATA_.mOUT[O.STAGE1_DRAIN] ? Color.Red : Color.White;
            lbBACKVAC_MB1.BackColor = DATA_.mOUT[O.STAGE1_BACK_VAC] ? Color.Red : Color.White;
            iVAC_MB1.BackColor      = DATA_.mIN[I.STAGE_VACUUM1] ? Color.Lime : Color.White;

            lbVAC_MB2.BackColor     = DATA_.mOUT[O.STAGE2_VAC] ? Color.Red : Color.White;
            lbDRAIN_MB2.BackColor   = DATA_.mOUT[O.STAGE2_DRAIN] ? Color.Red : Color.White;
            lbBACKVAC_MB2.BackColor = DATA_.mOUT[O.STAGE2_BACK_VAC] ? Color.Red : Color.White;
            iVAC_MB2.BackColor      = DATA_.mIN[I.STAGE_VACUUM2] ? Color.Lime : Color.White;

        } //
        void AUTOVIEW_1(){
            LBL_MESSAGE2.Text = DATA_.editErrTitle_1;
            LBL_MESSAGE1.Text = DATA_.editErrTitle_2;
        } //ERROR VEIW
        void AUTOVIEW_2(){

        } //EES VEIW

        void Blink(){
            for (int i = 0; i < Pause.Length; i++){
                if (DATA_.IsBIT[B.PauseOption[i]]){
                    if (PauseOff[i] > 3){
                        if (PauseOn[i] > 3){
                            PauseOn[i] = 0;
                            PauseOff[i] = 0;
                        }
                        else{
                            PauseOn[i]++;
                            Pause[i].BackColor = Color.Red;
                        }
                    }
                    else{
                        Pause[i].BackColor = Color.White;
                        PauseOff[i]++;
                    }
                }
                else Pause[i].BackColor = Color.White;
            }

            if (DATA_.IsBIT[B.CstRequest]){
                if (iswCstSupplyOn > 5){
                    if (iswCstSupplyOff > 10){
                        iswCstSupplyOn = 0;
                        iswCstSupplyOff = 0;
                    }
                    else{
                        swCst_SUPPLY.BackColor = Color.Red;
                        iswCstSupplyOff += 1;
                    }
                }
                else{
                    swCst_SUPPLY.BackColor = Color.White;
                    iswCstSupplyOn += 1;
                }
            }
            else swCst_SUPPLY.BackColor = Color.White;
        }
        void Option(){
            if (DATA_.IsBIT[B.EmptyStackerSupply]){
                lbEMPTY_TRAY_SUPPLY.Visible = true;
                if (DATA_.mIN[I.EMPTY_STACKER_NONE]){
                    EmptyTraySupplyCheckCount++;
                    if (EmptyTraySupplyCheckCount > 5){
                        DATA_.IsBIT[B.EmptyStackerSupply] = false;
                    }
                }
                else EmptyTraySupplyCheckCount = 0;
            }
            else{
                lbEMPTY_TRAY_SUPPLY.Visible = false;
                EmptyTraySupplyCheckCount = 0;
            }//empty tray 공급
            
            if (DATA_.IsBIT[B.GoodTrayStackerUldRequest]){
                lbGOOD_TRAY_DISCHARGE.Visible = true;
                if (!DATA_.mIN[I.GOOD_TRAY_STACKER_FULL]){
                    GoodTrayDischargeCount++;
                    if (GoodTrayDischargeCount > 5) DATA_.IsBIT[B.GoodTrayStackerUldRequest] = false;
                }
                else GoodTrayDischargeCount = 0;
            }
            else{
                lbGOOD_TRAY_DISCHARGE.Visible = false;
                GoodTrayDischargeCount = 0;
            }//good tray 배출
            
            if (DATA_.IsBIT[B.ReWorkTrayStackerUldRequest]){
                lbREWORK_TRAY_DISCHARGE.Visible = true;
                if (!DATA_.mIN[I.NG_TRAY_STACKER_FULL]){
                    ReworkTrayDischargeCount++;
                    if (ReworkTrayDischargeCount > 5) DATA_.IsBIT[B.ReWorkTrayStackerUldRequest] = false;
                }
                else ReworkTrayDischargeCount = 0;
            }
            else{
                lbREWORK_TRAY_DISCHARGE.Visible = false;
                ReworkTrayDischargeCount = 0;
            } //ng tray 배출

            if (DATA_.IsBIT[B.GoodTrayUnloadingMode]){
                if (!DATA_.IsBIT[B.X1PlcBusy] && !DATA_.IsBIT[B.X2PlcBusy]){
                    DATA_.IsBIT[B.GoodTrayUnloadingMode] = false;
                    DATA_.IsBIT[B.GoodTrayAutoUnloading] = true;
                    DATA_.IsBIT[B.GoodTrayWork] = false;
                }
            } //GOOD 트레이 배출 대기
            if (DATA_.IsBIT[B.ReWorkTrayUnloadingMode]){
                if (!DATA_.IsBIT[B.X1NGPlcBusy] && !DATA_.IsBIT[B.X2NGPlcBusy]){
                    DATA_.IsBIT[B.ReWorkTrayUnloadingMode] = false;
                    DATA_.IsBIT[B.ReWorkTrayWork] = false;
                }
            } //ReWORK TRAY 대기 

            if (DATA_.eMCStatus == eMachineStatus.AUTO && DATA_.mDOOR_SKIP){
                if (DATA_.IsLONG[L.CheckDoorSkipTime] > 3000){
                    DATA_.mDOOR_SKIP = false;
                    DATA_.IsLONG[L.CheckDoorSkipTime] = 0;
                    O.SET_DOORLOCK();
                    if (!I.CHK_DOOR()){
                        E.OnERROR(E.emsDoorOpen);
                    } //300000ms = 300sec = 5min
                } // * 100ms
                if (!DATA_.IsBIT[B.SkipDoorLock]) DATA_.IsLONG[L.CheckDoorSkipTime]++;
            }
            else DATA_.IsLONG[L.CheckDoorSkipTime] = 0;
            lbDoorSkipTime.Text = DATA_.IsLONG[L.CheckDoorSkipTime].ToString() + "/3000";
        }
        void InvokeTack(){
            editStatus.Text             = DATA_.eMCStatus.ToString();

            DATA_.viewSPC               = DATA_.oSPC;
            lbRUNTime.Text              = DATA_.cMATH.IntToTime(DATA_.viewSPC.mlRunTime);
            lbSTOPTime.Text             = DATA_.cMATH.IntToTime(DATA_.viewSPC.mlStopTime);
            lbERRORTime.Text            = DATA_.cMATH.IntToTime(DATA_.viewSPC.mlErrorTime);

            lbTackTime.Text             = string.Format("{0:0.00}", DATA_.IsDOUBLE[D.PkgUPH]);
            lbAvgTime.Text              = string.Format("{0:0.00}", DATA_.IsDOUBLE[D.Tact_Avg]);
            DATA_.IsDOUBLE[D.UPH]       = (3600 / DATA_.IsDOUBLE[D.Tact_Avg]) / 1000;
            lbUPHTime.Text              = string.Format("{0:0.00}", /*DATA_.IsDOUBLE[D.UPH]*/DATA_.IsDOUBLE[D.UPH_]);
            lbUPH_1.Text                = string.Format("{0:0.00}", DATA_.IsDOUBLE[D.UPH]);
            lbTrayCycleTime.Text        = DATA_.cMATH.IntToTime((long)DATA_.IsDOUBLE[D.TrayCycle]); //string.Format("{0:0.0}", mDATA.IsDOUBLE[mD.mdCycleTime]);//IsDOUBLE[D.TrayCycle]
            lbStripPlaceTackTime.Text   = DATA_.cMATH.IntToTime((long)DATA_.IsDOUBLE[D.StripPkCycle]);
            lbUnitPkrCycleTime.Text     = DATA_.cMATH.IntToTime((long)DATA_.IsDOUBLE[D.UnitPkCycle]);
            lbMapBlock1CycleTime.Text   = DATA_.cMATH.IntToTime((long)DATA_.IsDOUBLE[D.Stage1Cycle]);
            lbMapBlock2CycleTime.Text   = DATA_.cMATH.IntToTime((long)DATA_.IsDOUBLE[D.Stage2Cycle]);

            lbStripLDCount.Text         = DATA_.IsLONG[L.StripCnt].ToString();
            lbUnitULDCnt.Text           = DATA_.IsLONG[L.UnitCnt].ToString();
            lbEmptyCnt.Text             = DATA_.IsLONG[L.EmptyTrayCnt].ToString();
            lbGoodTryCnt.Text           = DATA_.IsLONG[L.GoodTrayCnt].ToString();
            lbReworkTryCnt.Text         = DATA_.IsLONG[L.ReworkTrayCnt].ToString();
            lbLOTCnt.Text               = DATA_.IsLONG[L.LotCnt].ToString();
            lbUnitSizeNGCnt.Text        = DATA_.IsLONG[L.UnitSizeNgCnt].ToString();

            lblTotalCount.Text          = DATA_.IsLONG[L.OutCnt].ToString();
            lbGoodChipCount.Text        = DATA_.IsLONG[L.GoodCnt].ToString();
            lbReworkChipCount.Text      = DATA_.IsLONG[L.ReworkCnt].ToString();
            lbXMarkChipCnt.Text         = DATA_.IsLONG[L.NGCnt].ToString();
            lbITScnt.Text               = DATA_.IsLONG[L.ITSCount].ToString();
        }

        private void TmrAUTO_Tick(object sender, EventArgs e){
            TmrAUTO.Enabled = false;
            Invoke();
            InvokeTack();
            Blink();
            Option();
            InvokeSignal();

            if (DATA_.IsBIT[B.WaitLotEndProcessing]) {
                lbEventMessage.Text = "LOT-END PROCESSING !!!" + '\n' + "PLEASE WAIT !!";
                lbLotEndMessage.Visible = true;
            }
            else lbLotEndMessage.Visible = false;
            if (DATA_.IsBIT[B.WaitITSReading]) {
                lbEventMessage.Text = "ITS 좌표계 리딩 중입니다. !!!" + '\n' + "SQL 서버 연결하여 DB 리딩 중..." + '\n' + "기다려 주십시오.!!";
            }
            bLotEndProcess.Visible      = DATA_.IsBIT[B.WaitLotEndProcessing];
            pLotEndProcessing.Visible   = DATA_.IsBIT[B.WaitLotEndProcessing] || DATA_.IsBIT[B.WaitITSReading];

            lbProcess.Text = DEF.MCProcess + "/" + DATA_.LibProcess;
            //!! 테스트 !!//
            textBox2.Visible = false;
            textBox1.Visible = false;

            BTN_TEST.Visible = false;
            LBL_TEST.Visible = BTN_TEST.Visible;
            //!! 테스트 !!//

            double elapsedSec = (DateTime.Now - _uphBaseTime).TotalSeconds;
            if (elapsedSec >= 60.0) {
                _uphBaseTime = DateTime.Now;
                if (L.IsLONG[L.PicCnt] > 0) {
                    D.IsDOUBLE[D.UPH_] = ((double)(L.IsLONG[L.PicCnt] * 60)) / 1000;
                    L.IsLONG[L.PicCnt] = 0;
                }
            } // uph 확인 용도!
            TmrAUTO.Enabled = true;
        }
        void Invoke(){
            lbCurPkr.Text = nPK.ToString("00");
            for (int i = 0; i < PkrVac.Length; i++){
                PkrVac[i].Text = (DATA_.mAI[i]).ToString();
                PkrVac[i].ForeColor = DATA_.mAI[i] > DATA_.mSET_AI[i] ? Color.Lime : Color.Black;
            }

            LB_DIRECT_PLACE.BackColor = DATA_.IsBIT[B.DirectUnitPlace] ? Color.Red : Color.White;

            lbPRS_START.ForeColor = DATA_.mOUT[O.PRSStart] ? Color.Red : Color.Black;
            lbPRS_READ.ForeColor = DATA_.mOUT[O.PRSReading] ? Color.Red : Color.Black;
            lbPRS_RESULT.ForeColor = DATA_.mIN[I.PRSVisionWirte] ? Color.Lime : Color.Black;
            WipCheck.BackColor = DATA_.prMACHINE[CP.UseMES] == (int)eUSE.USE ? Color.Lime : Color.White;
            DATA_.mOUT[O.UseMES] = DATA_.prMACHINE[CP.UseMES] == (int)eUSE.USE ? true : false;

            lbITS.BackColor = MsSQL.bOpen ? Color.Lime : Color.DarkGreen;
            lbBarcodeState.BackColor = SUBFRM_.cBarcode.bOpen ? Color.Lime : Color.DarkGreen;
            lbUDP.BackColor = DEF.bUDP ? Color.Lime : Color.DarkGreen;

            if (CLOT.GET_LOT.LotType < 1)       GridWorkLot.Rows[0].Cells[0].Value = "";
            else                                GridWorkLot.Rows[0].Cells[0].Value = "0";
            GridWorkLot.Rows[0].Cells[1].Value = CLOT.GET_LOT.WorkScope;   //작업 구분
            GridWorkLot.Rows[0].Cells[2].Value = CLOT.GET_LOT.ToolNo;    //관리 번호
            GridWorkLot.Rows[0].Cells[3].Value = CLOT.GET_LOT.ABFMATERIAL;    //자재명
            GridWorkLot.Rows[0].Cells[4].Value = CLOT.GET_LOT.LotID;       //LOT ID
            
            GridWorkLot.Rows[0].Cells[5].Value = CLOT.GET_LOT.LotType < 1 ? "" : CLOT.GET_LOT.Qty.ToString(); //MES 매수 (상위에서 받은 수량)
            GridWorkLot.Rows[0].Cells[6].Value = CLOT.GET_LOT.LotType < 1 ? "" : CLOT.GET_LOT.LoadingCount.ToString(); //STRIP (gripper 로딩 수량 동일 스트립 반복 투입 확인 함)
            GridWorkLot.Rows[0].Cells[7].Value = CLOT.GET_LOT.LotType < 1 ? "" : CLOT.GET_LOT.InCnt.ToString(); //투입 매수 (gripper 로딩 수량 무조건 그리퍼 로딩 하면 수량 증가)
            GridWorkLot.Rows[0].Cells[8].Value = CLOT.GET_LOT.LotType < 1 ? "" : CLOT.GET_LOT.ExceptCount.ToString(); //제외 매수
            GridWorkLot.Rows[0].Cells[9].Value = CLOT.GET_LOT.LotType < 1 ? "" : CLOT.GET_LOT.OutCnt.ToString(); //배출 매수 (맵블록 작업 완료 후 배출 수량)
            GridWorkLot.Rows[0].Cells[10].Value = CLOT.GET_LOT.LotType < 1 ? "" : CLOT.GET_LOT.WorkSort; //작업 종류

            for (int n = 0; n < CLOT.FINISH_LOT.Length; n++){
                if (CLOT.FINISH_LOT[n].LotType < 1)     GridEndLot.Rows[n].Cells[0].Value = "";
                else                                    GridEndLot.Rows[n].Cells[0].Value = CLOT.FINISH_LOT[n].nNUM;

                GridEndLot.Rows[n].Cells[0].Value = CLOT.FINISH_LOT[n].LotType < 1 ? "-" : (n + 1).ToString();
                GridEndLot.Rows[n].Cells[1].Value = CLOT.FINISH_LOT[n].WorkScope;
                GridEndLot.Rows[n].Cells[2].Value = CLOT.FINISH_LOT[n].ToolNo;
                GridEndLot.Rows[n].Cells[3].Value = CLOT.FINISH_LOT[n].ABFMATERIAL;
                GridEndLot.Rows[n].Cells[4].Value = CLOT.FINISH_LOT[n].LotID;

                GridEndLot.Rows[n].Cells[5].Value = CLOT.FINISH_LOT[n].LotType < 1 ? "" : CLOT.FINISH_LOT[n].Qty.ToString();
                GridEndLot.Rows[n].Cells[6].Value = CLOT.FINISH_LOT[n].LotType < 1 ? "" : CLOT.FINISH_LOT[n].LoadingCount.ToString();
                GridEndLot.Rows[n].Cells[7].Value = CLOT.FINISH_LOT[n].LotType < 1 ? "" : CLOT.FINISH_LOT[n].InCnt.ToString();
                GridEndLot.Rows[n].Cells[8].Value = CLOT.FINISH_LOT[n].LotType < 1 ? "" : CLOT.FINISH_LOT[n].ExceptCount.ToString();
                GridEndLot.Rows[n].Cells[9].Value = CLOT.FINISH_LOT[n].LotType < 1 ? "" : CLOT.FINISH_LOT[n].OutCnt.ToString();
                GridEndLot.Rows[n].Cells[10].Value = CLOT.FINISH_LOT[n].LotType < 1 ? "" : CLOT.FINISH_LOT[n].WorkSort;

                GridEndLot.Rows[n].Cells[11].Value = CLOT.FINISH_LOT[n].LotType < 1 ? "" : CLOT.FINISH_LOT[n].GoodUnit.ToString();
                GridEndLot.Rows[n].Cells[12].Value = CLOT.FINISH_LOT[n].LotType < 1 ? "" : CLOT.FINISH_LOT[n].ReworkUnit.ToString();
                GridEndLot.Rows[n].Cells[13].Value = CLOT.FINISH_LOT[n].LotType < 1 ? "" : CLOT.FINISH_LOT[n].NGUnit.ToString();
            }

            LB_LOT_NO_1.Text            = CLOT.FINISH_LOT[0].LotID;
            LB_LOT_NO_2.Text            = CLOT.FINISH_LOT[1].LotID;
            LB_LOT_NO_3.Text            = CLOT.FINISH_LOT[2].LotID;

            lb_STRIP_CNT_1.Text         = CLOT.FINISH_LOT[0].StripCnt.ToString();
            lb_STRIP_CNT_2.Text         = CLOT.FINISH_LOT[1].StripCnt.ToString();
            lb_STRIP_CNT_3.Text         = CLOT.FINISH_LOT[2].StripCnt.ToString();

            lb_UNIT_CNT_1.Text          = CLOT.FINISH_LOT[0].TotalUnit.ToString();
            lb_UNIT_CNT_2.Text          = CLOT.FINISH_LOT[1].TotalUnit.ToString();
            lb_UNIT_CNT_3.Text          = CLOT.FINISH_LOT[2].TotalUnit.ToString();

            lb_GOOD_CNT_1.Text          = CLOT.FINISH_LOT[0].GoodUnit.ToString();
            lb_GOOD_CNT_2.Text          = CLOT.FINISH_LOT[1].GoodUnit.ToString();
            lb_GOOD_CNT_3.Text          = CLOT.FINISH_LOT[2].GoodUnit.ToString();

            lb_NG_CNT_1.Text            = CLOT.FINISH_LOT[0].ReworkUnit.ToString();
            lb_NG_CNT_2.Text            = CLOT.FINISH_LOT[1].ReworkUnit.ToString();
            lb_NG_CNT_3.Text            = CLOT.FINISH_LOT[2].ReworkUnit.ToString();

            lb_ITS_CNT_1.Text           = CLOT.FINISH_LOT[0].ITSCount.ToString();
            lb_ITS_CNT_2.Text           = CLOT.FINISH_LOT[1].ITSCount.ToString();
            lb_ITS_CNT_3.Text           = CLOT.FINISH_LOT[2].ITSCount.ToString();

            lb_XOUT_CNT_1.Text          = CLOT.FINISH_LOT[0].NGUnit.ToString();
            lb_XOUT_CNT_2.Text          = CLOT.FINISH_LOT[1].NGUnit.ToString();
            lb_XOUT_CNT_3.Text          = CLOT.FINISH_LOT[2].NGUnit.ToString();

            lb_GOODTRAY_CNT_1.Text      = CLOT.FINISH_LOT[0].GoodTray.ToString();
            lb_GOODTRAY_CNT_2.Text      = CLOT.FINISH_LOT[1].GoodTray.ToString();
            lb_GOODTRAY_CNT_3.Text      = CLOT.FINISH_LOT[2].GoodTray.ToString();

            lb_NGTRAY_CNT_1.Text        = CLOT.FINISH_LOT[0].NGTray.ToString();
            lb_NGTRAY_CNT_2.Text        = CLOT.FINISH_LOT[1].NGTray.ToString();
            lb_NGTRAY_CNT_3.Text        = CLOT.FINISH_LOT[2].NGTray.ToString();

            RAIL_OVERLAP.BackColor      = CLOT.InfoStrip[T.Gripper].Overlap ? Color.Red : Color.White;
            STRIP_OVERLAP.BackColor     = CLOT.InfoStrip[T.StripPk].Overlap ? Color.Red : Color.White;
            SAW_OVERLAP.BackColor       = CLOT.SawStageStripOverlap ? Color.Red : Color.White;
            UNIT_OVERLAP.BackColor      = CLOT.InfoStrip[T.UnitPk].Overlap ? Color.Red : Color.White;
            MAPBLOCK1_OVERLAP.BackColor = CLOT.InfoStrip[T.DryTable1].Overlap ? Color.Red : Color.White;
            MAPBLOCK2_OVERLAP.BackColor = CLOT.InfoStrip[T.DryTable2].Overlap ? Color.Red : Color.White;

            LB_ABF_CHECK.ForeColor      = DATA_.prMACHINE[CP.UseABF] == (int)eUSE.USE ? Color.Lime : Color.Red;
            LB_ABF_LIST.ForeColor       = CLOT.bABF ? Color.Lime : Color.Red;

            LBL_SPINDLE1_ID.Text        = CLOT.GET_LOT.BarcodeSp1 + " / " + CLOT.GET_LOT.IDSp1;
            LBL_SPINDLE2_ID.Text        = CLOT.GET_LOT.BarcodeSp2 + " / " + CLOT.GET_LOT.IDSp2;

            LBL_SPINDLE1_INFO.Text      = DATA_.Sp1BladeAmountOfUse + "," + DATA_.Sp1BladeCuttingCnt;
            LBL_SPINDLE2_INFO.Text      = DATA_.Sp2BladeAmountOfUse + "," + DATA_.Sp2BladeCuttingCnt;

            LB_DOOR_SKIP.BackColor = DATA_.mDOOR_SKIP ? Color.Red : Color.White;
            if (DATA_.eLoginLevel >= eLogLevel.ADMIN){
                BTN_CNT_RESET.Visible   = true;
                SETTING_SKIP.Visible    = true;
            }
            else { 
                BTN_CNT_RESET.Visible   = false;
                SETTING_SKIP.Visible    = false;
                DATA_.mTRIP_SKIP        = false;
                DATA_.mAIR_SKIP         = false;
            }
            pnlBladeChange_1.BackColor = DATA_.mIN[I.SAW_BLADE_CHANGE] ? Color.Lime : Color.White;
            pnlBladeChange_2.BackColor = DATA_.mOUT[O.HANDLER_BLADE_CHANGE_RESET] ? Color.Red : Color.White;

            LB_UNITPK_MOVING.BackColor = DATA_.mOUT[O.HANDLER_PK_MOVING] ? Color.Red : Color.White;

            LB_KIT_CLEANING.BackColor = DATA_.IsBIT[B.LotStart_KitCleanning] ? Color.Lime : Color.White;
            ViewFDCList();
        }
        private void TRearTime_Tick(object sender, EventArgs e) {
            
            TRearTime.Enabled = false;
            DrawMGZ(pbxCST, (int)DATA_.prMODEL[RP.MGZSlotCnt], M.ElvZ);
            DrawPk(); //DrawPkr(); 
            DrawStage(pbxPALLET_1, (int)DATA_.prMODEL[RP.UnitCntX], (int)DATA_.prMODEL[RP.UnitCntY], (int)DATA_.prMODEL[RP.GroupCntX], (int)DATA_.prMODEL[RP.GroupCntY], (int)eMAP_BLOCK.STAGE1);
            DrawStage(pbxPALLET_2, (int)DATA_.prMODEL[RP.UnitCntX], (int)DATA_.prMODEL[RP.UnitCntY], (int)DATA_.prMODEL[RP.GroupCntX], (int)DATA_.prMODEL[RP.GroupCntY], (int)eMAP_BLOCK.STAGE2);
            DrawTray(pbxGOOD, (int)DATA_.prMODEL[RP.TrayCntX], (int)DATA_.prMODEL[RP.TrayCntY], (int)DATA_.IsLONG[L.CurWorkTray]);
            DrawTray(pbxREWORK, (int)DATA_.prMODEL[RP.TrayCntX], (int)DATA_.prMODEL[RP.TrayCntY], (int)eTRAY.REWORK);
            TRearTime.Enabled = true;
        }

        void DrawMGZ(PictureBox image, int cnt, int m){
            if (image == null || cnt <= 0 || image.Width <= 0 || image.Height <= 0 || !bViewCassage) return;
            Bitmap newBmp = null;
            try {
                newBmp = new Bitmap(image.Width, image.Height);  // 새 비트맵 생성 (이 시점에서 메모리가 부족하면 catch로 이동)
                using (Graphics G = Graphics.FromImage(newBmp)) {
                    uGW = image.Width;
                    uGH = image.Height / cnt;

                    //G.FillRectangle(Brushes.White, 0, 0, newBmp.Width, newBmp.Height);
                    G.Clear(Color.White);
                    if (uGH >= 1) {
                        for (int i = 0; i < cnt; i++) {
                            using (SolidBrush br = new SolidBrush(TEACH_.COLOR_CODE(MAP_.ARR_MGZ[m, i]))) {
                                G.FillRectangle(br, 1, (i * uGH) + 1, uGW - 2, uGH - 2);
                            }
                        }
                    }
                    else return;
                }

                // UI 스레드 작업: 기존 이미지 교체
                var oldImage = image.Image;
                image.Image = newBmp; // 새 이미지를 먼저 할당
                if (image.Image != null) {
                   if (oldImage != null) oldImage.Dispose(); // 명시적 해제.
                    oldImage = null;
                }
            }
            catch (Exception ex){
                // 에러 발생 시 생성했던 비트맵이 image에 할당되지 않았다면 여기서 해제
                if (newBmp != null && image.Image != newBmp) newBmp.Dispose();
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
        
        void DrawPkr(){
            double TAG;
            int PK;
            try{
                nPK = 0;
                foreach (Control mControl in gbxHD1.Controls){
                    if (mControl.GetType().Name == "PictureBox"){
                        if (nPK >= CNT_.PKR) break;
                        TAG = Math.Truncate((double)(nPK / 2));
                        PK = (int)(M.X1Z12 + TAG);

                        if (M.CurPkPosition(PK, P.Ready) && DATA_.PK_Z[nPK] == eSTATUS.MARK) mControl.BackgroundImage = PIC_UP_MASK.BackgroundImage;     //피커 픽업 UP OK
                        else if (M.CurPkPosition(PK, P.Ready) && DATA_.PK_Z[nPK] == eSTATUS.EMPTY) mControl.BackgroundImage = PIC_UP_EMPTY.BackgroundImage;    //피커 READY

                        else if ((M.CurPkPosition(PK, P.PK_PIC[nPK])) && DATA_.PK_Z[nPK] == eSTATUS.MARK) mControl.BackgroundImage = PIC_DOWN_MASK.BackgroundImage;   //피커 픽업 DOWN Y
                        else if ((M.CurPkPosition(PK, P.PK_PLC[nPK])) && DATA_.PK_Z[nPK] == eSTATUS.MARK) mControl.BackgroundImage = PIC_DOWN_MASK.BackgroundImage;   //피커 플레이스 DOWN Y
                        else if ((M.CurPkPosition(PK, P.PK_PIC[nPK])) && DATA_.PK_Z[nPK] == eSTATUS.EMPTY) mControl.BackgroundImage = PIC_DOWN_EMPTY.BackgroundImage;  //피커 픽업 DOWN N
                        else if ((M.CurPkPosition(PK, P.PK_PLC[nPK])) && DATA_.PK_Z[nPK] == eSTATUS.EMPTY) mControl.BackgroundImage = PIC_DOWN_EMPTY.BackgroundImage;   //피커 플레이스 DOWN N

                        else if (DATA_.PK_Z[nPK] == eSTATUS.NONE) mControl.BackgroundImage = PIC_NONE.BackgroundImage; //피커 사용안함
                        nPK++;
                    }
                }
                nPK = 0;
                foreach (Control mControl in gbxHD2.Controls){
                    if (mControl.GetType().Name == "PictureBox"){
                        if (nPK >= CNT_.PKR) break;
                        TAG = Math.Truncate((double)(nPK / 2));
                        PK = (int)(M.X2Z12 + TAG);

                        if (M.CurPkPosition(PK, P.Ready) && DATA_.PK_Z[nPK + (CNT_.PKR * 1)] == eSTATUS.MARK) mControl.BackgroundImage = PIC_UP_MASK.BackgroundImage;
                        else if (M.CurPkPosition(PK, P.Ready) && DATA_.PK_Z[nPK + (CNT_.PKR * 1)] == eSTATUS.EMPTY) mControl.BackgroundImage = PIC_UP_EMPTY.BackgroundImage;

                        else if ((M.CurPkPosition(PK, P.PK_PIC[nPK])) && DATA_.PK_Z[nPK + (CNT_.PKR * 1)] == eSTATUS.MARK) mControl.BackgroundImage = PIC_DOWN_MASK.BackgroundImage;
                        else if ((M.CurPkPosition(PK, P.PK_PLC[nPK])) && DATA_.PK_Z[nPK + (CNT_.PKR * 1)] == eSTATUS.MARK) mControl.BackgroundImage = PIC_DOWN_MASK.BackgroundImage;
                        else if ((M.CurPkPosition(PK, P.PK_PIC[nPK])) && DATA_.PK_Z[nPK + (CNT_.PKR * 1)] == eSTATUS.EMPTY) mControl.BackgroundImage = PIC_DOWN_EMPTY.BackgroundImage;
                        else if ((M.CurPkPosition(PK, P.PK_PLC[nPK])) && DATA_.PK_Z[nPK + (CNT_.PKR * 1)] == eSTATUS.EMPTY) mControl.BackgroundImage = PIC_DOWN_EMPTY.BackgroundImage;

                        else if (DATA_.PK_Z[nPK + (CNT_.PKR * 1)] == eSTATUS.NONE) mControl.BackgroundImage = PIC_NONE.BackgroundImage;
                        nPK++;
                    }
                }
            }
            catch (Exception e) { System.Diagnostics.Trace.WriteLine(e.Message); }
        }
        void DrawPk() {
            try {
                UpdataPkrState(gbxHD1, 0);
                UpdataPkrState(gbxHD2, CNT_.PKR); ///*CNT_.PKR*/ 
            }
            catch (Exception ex) {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
        void UpdataPkrState(GroupBox head, int offset) {
            nPK = 0;
            foreach (Control mControl in head.Controls) {
                if (mControl.GetType().Name == "PictureBox") {
                    if (nPK >= CNT_.PKR) break;
                    double TAG = Math.Truncate((double)(nPK / 2));
                    int PK = (int)(offset == 0 ? M.X1Z12 : M.X2Z12) + (int)TAG;
                    var status = DATA_.PK_Z[nPK + offset];

                    if (M.CurPkPosition(PK, P.Ready) && status != eSTATUS.NONE) {
                        mControl.BackgroundImage = (status == eSTATUS.MARK)
                            ? PIC_UP_MASK.BackgroundImage : PIC_UP_EMPTY.BackgroundImage;
                    }
                    else if ((M.CurPkPosition(PK, P.PK_PIC[nPK]) || M.CurPkPosition(PK, P.PK_PIC[nPK])) && status != eSTATUS.NONE) {
                        mControl.BackgroundImage = (status == eSTATUS.MARK)
                            ? PIC_DOWN_MASK.BackgroundImage : PIC_DOWN_EMPTY.BackgroundImage;
                    }
                    else if ((M.CurPkPosition(PK, P.PK_PIC[nPK]) || M.CurPkPosition(PK, P.PK_PIC[nPK])) && status != eSTATUS.NONE) {
                        mControl.BackgroundImage = (status == eSTATUS.MARK)
                            ? PIC_DOWN_MASK.BackgroundImage : PIC_DOWN_EMPTY.BackgroundImage;
                    }
                    else if (status == eSTATUS.NONE) mControl.BackgroundImage = PIC_NONE.BackgroundImage; //피커 사용안함
                    nPK++;
                }
            }
        }

        void DrawStage(PictureBox image, int unit_x, int unit_y, int group_x, int group_y, int stage){
            if (image == null || unit_x == 0 || unit_y == 0 || !bMC_DISPLY) return;
            try{
                using (Graphics G = Graphics.FromImage(STAGE[stage])) {
                    G.Clear(Color.White); //G.FillRectangle(Brushes.White, 0, 0, STAGE[stage].Width, STAGE[stage].Height);

                    uGW = (image.Width / group_x) - 3;
                    uGH = (image.Height / group_y) - 3;
                    uW = (uGW / unit_x);
                    uH = (uGH / unit_y);
                    if (uGW < 1 || uGH < 1 || uW < 1 || uH < 1) return;

                    for (int gy = 0; gy < group_y; gy++) {
                        for (int gx = 0; gx < group_x; gx++) {
                            for (int uy = 0; uy < unit_y; uy++) {
                                for (int ux = 0; ux < unit_x; ux++) {
                                    uX1 = ux % unit_x;
                                    uX2 = (uX1 * uW) + (gx * uGW) + (gx * 5);
                                    uY2 = (uy * uH) + (gy * uGH) + (gy * 5);

                                    Color targetColor = (stage > 1)
                                    ? TEACH_.COLOR_CODE(eSTATUS.EMPTY) : TEACH_.COLOR_CODE(MAP_.ARR_PALLET[stage, gy, gx, ux, uy]);

                                    // 멤버 변수 mBrush 대신 지역 변수 using 사용 (누수 방지 핵심)
                                    using (SolidBrush tempBrush = new SolidBrush(targetColor)) {
                                        G.FillRectangle(tempBrush, uX2, uY2, uW - 2, uH - 2);
                                    }
                                }
                            }
                        }
                    } 
                }
                // UI 스레드 안전을 위해 Invoke 고려 및 복사본 전달
                if (image.InvokeRequired) image.Invoke(new Action(() => { UpdateUIStage(image, STAGE[stage]); }));
                else UpdateUIStage(image, STAGE[stage]);    
            }
            catch (Exception e) { System.Diagnostics.Trace.WriteLine(e.Message); }
        }
        // 별도의 업데이트 함수를 만들어 기존 이미지를 확실히 Dispose
        void UpdateUIStage(PictureBox pb, Image source) {
            if (pb.InvokeRequired) {
                pb.Invoke(new Action(() => UpdateUIStage(pb, source)));
                return;
            }
            Image old = pb.Image;
            pb.Image = (Image)source.Clone();
            if (old != null) old.Dispose();
        }

        void DrawTray(PictureBox image, int x, int y, int tray){
            if (image == null || x == 0 || y == 0 || !bMC_DISPLY) return;
            try{
                DATA_.IsLONG[L.uiTray] = (tray <= (int)eTRAY.GOOD2) ? 0 : 1;
                int trayIndex = (int)DATA_.IsLONG[L.uiTray];

                if (TRAY[trayIndex] == null || TRAY[trayIndex].Width != image.Width || TRAY[trayIndex].Height != image.Height) {
                    if (TRAY[trayIndex] != null) TRAY[trayIndex].Dispose();
                    TRAY[trayIndex] = new Bitmap(image.Width, image.Height);
                }

                using (Graphics G = Graphics.FromImage(TRAY[trayIndex])) {
                    G.Clear(Color.White);

                    int uGW = image.Width / x;
                    int uGH = image.Height / y;
                    if (uGW < 1 || uGH < 1) return;
                    using (SolidBrush mBrush = new SolidBrush(Color.Black)) {
                        for (int i = 0; i < x * y; i++) {
                            int y_offset = i / x;
                            int uX2 = (i % x) * uGW;
                            int uY2 = y_offset * uGH;

                            mBrush.Color = (tray > 2)
                                ? TEACH_.COLOR_CODE(eSTATUS.EMPTY) : TEACH_.COLOR_CODE(MAP_.ARR_TRAY[tray, i]);

                            G.FillRectangle(mBrush, uX2, uY2, uGW - 2, uGH - 2);
                        }
                    }
                }

                UpdateUITry(image, TRAY[trayIndex]);
            }
            catch (Exception e) { System.Diagnostics.Trace.WriteLine(e.Message); }
        }
        void UpdateUITry(PictureBox pb, Image source) {
            if (pb == null || source == null) return;
            if (pb.InvokeRequired) {
                pb.Invoke(new Action(() => UpdateUITry(pb, source)));
                return;
            }

            Image oldImg = pb.Image;
            try {
                pb.Image = (Image)source.Clone();
            }
            catch (Exception ex) {
                System.Diagnostics.Trace.WriteLine("UpdateUI Clone Error: " + ex.Message);
            }

            if (oldImg != null) {
                oldImg.Dispose();
                oldImg = null; // 참조 해제
            }
        }

        void ViewFDCList(){
            if (!bEES_FDC || !CMES.bIni) return;

            for (int n = 0; n < CSVID.FDC_NUM.Length; n++){
                DGV_FDC_INFO.Rows[n].Cells[1].Value = CSVID.Type[CSVID.FDC_NUM[n]];
                DGV_FDC_INFO.Rows[n].Cells[2].Value = CSVID.Name[CSVID.FDC_NUM[n]];
                DGV_FDC_INFO.Rows[n].Cells[3].Value = CSVID.Value[CSVID.FDC_NUM[n]];
            }

            for (int n = 0; n < CSVID.VID_NUM.Length; n++){
                DGV_VID_INFO.Rows[n].Cells[1].Value = CSVID.Type[CSVID.VID_NUM[n]];
                DGV_VID_INFO.Rows[n].Cells[2].Value = CSVID.Name[CSVID.VID_NUM[n]];
                DGV_VID_INFO.Rows[n].Cells[3].Value = CSVID.Value[CSVID.VID_NUM[n]];
            }
        }
    }
}