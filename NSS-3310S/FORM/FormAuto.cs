using NSS_3310S.SEQ;
using Object;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using NSS_3310S.ITS;
using System.Diagnostics;
using LIB_.DateType;

namespace NSS_3310S{
    public partial class FormAuto : Form{
        Image imgMGZ = new Bitmap(74, 240);
        Image imgMAPBLOCK1 = new Bitmap(220, 555); //212, 522
        Image imgMAPBLOCK2 = new Bitmap(220, 555);
        Image imgTRAY1 = new Bitmap(220, 555);
        Image imgTRAY2 = new Bitmap(220, 555);

        Label[] iInterfaceState = null;
        Label[] oInterfaceState = null;
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
        bool bSetSkipView = false;
        bool bViewCassage = false;

        int uGW, uGH, y_offset, uX1, uX2, uY2, uW, uH;
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

        public bool bEES_DISPLY = false;
        
        public FormAuto(){
            InitializeComponent();
            MGZ = imgMGZ;
            STAGE = new Image[] { imgMAPBLOCK1, imgMAPBLOCK2 };
            TRAY = new Image[] { imgTRAY1, imgTRAY2 };

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
            EES_DISPLAY.Click       += (sender, e) => RunningEvent(EES_DISPLAY);
            WipCheck.Click          += (sender, e) => RunningEvent(WipCheck);

            bMAPBLOCK1_WORK_RESET.Click += (sender, e) => RunningEvent(bMAPBLOCK1_WORK_RESET);
            bMAPBLOCK2_WORK_RESET.Click += (sender, e) => RunningEvent(bMAPBLOCK2_WORK_RESET);

            swStart.MouseDown   += (sender, e) => StartEvent();
            swStart.MouseUp     += (sender, e) => DATA_.mIN[I.vtStart] = false;
            swStop.MouseDown    += (sender, e) => StopEvent();
            swStop.MouseUp      += (sender, e) => DATA_.mIN[I.vtStart] = false;
            swRest.MouseDown    += (sender, e) => ResetEvent();
            swRest.MouseUp      += (sender, e) => DATA_.mIN[I.vtReset] = false;
            btn_HOME.MouseDown  += (sender, e) => InitializeEvent();
            btn_HOME.MouseUp    += (sender, e) => DATA_.mIN[I.vtInitial] = false;

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

            WRITE_LOT_NUMBER.Click += (sender, e) => { 
                if (DATA_.eMCStatus != eMachineStatus.AUTO){
                    /*if (DATA_.prMACHINE[CP.UseMES] == (int)eUSE.USE){
                        COM_.ViewWarning(T.Manual, W.ManualErrMassage, "MES 사용 모드 입니다 !" + ETC.NewLine + "MES 미사용 모드에서만 적용 가능합니다 !");
                    }
                    else */if (CUSER.Current.ID == ""){
                        COM_.ViewWarning(T.Manual, W.ManualErrMassage, "USER ID  입력 안되어 있습니다 !" + ETC.NewLine + "USER ID 등록하셔야 진행 가능합니다 !");
                    }
                    else SUBFRM_.gLotID.INI_();
                }
            };
            LotWrite.Click += (sender, e) => {
                if (DATA_.eMCStatus != eMachineStatus.AUTO && DATA_.eMCStatus != eMachineStatus.INITIAL){
                    /*if (DATA_.prMACHINE[CP.UseMES] == (int)eUSE.USE){
                        COM_.ViewWarning(T.Manual, W.ManualErrMassage, "MES 사용 모드 입니다 !" + ETC.NewLine + "MES 미사용 모드에서만 적용 가능합니다 !");
                    }
                    else */if (CUSER.Current.ID == ""){
                        COM_.ViewWarning(T.Manual, W.ManualErrMassage, "USER ID  입력 안되어 있습니다 !" + ETC.NewLine + "USER ID 등록하셔야 진행 가능합니다 !");
                    }
                    else SUBFRM_.gLotID.INI_();
                }
            };

            USER_ID_CHANGE.Click += (sender, e) =>{
                if (DATA_.eMCStatus != eMachineStatus.AUTO){
                    SUBFRM_.gUserID.Initailize_View();
                }
            };

            btnLAMP.Click += (sender, e) => DATA_.mOUT[O.FLUORESENT_LIGHT] = !DATA_.mOUT[O.FLUORESENT_LIGHT];
            bSetSkipView = false;

            DocCogBarcode();
            DocMES();
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
                VIEW_CASSATE.Location = new Point(165, 397);
                VIEW_CASSATE.Size = new Size(83, 286);
            }//165, 396 / 83, 286
            else{
                VIEW_CASSATE.Location = new Point(192, 397);
                VIEW_CASSATE.Size = new Size(56, 20);
            }//192, 396 / 56, 20
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

            if (BTN.Name == "EES_DISPLAY"){
                bEES_DISPLY = !bEES_DISPLY;
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

        void StartEvent(){
            DATA_.mIN[I.vtStart] = true;
            DATA_.IsLONG[L.OffNumber] = I.vtStart;
        }
        void StopEvent(){
            DATA_.mIN[I.vtStop] = true;
            DATA_.IsLONG[L.OffNumber] = I.vtStop;
        }
        void ResetEvent(){
            DATA_.mIN[I.vtReset] = true;
            DATA_.IsLONG[L.OffNumber] = I.vtReset;
        }

        private void BTN_CNT_RESET_Click(object sender, EventArgs e){
            if (DATA_.eMCStatus == eMachineStatus.AUTO) return;
            DEF.CountReset(false);
        }

        private void BTN_TEST_Click(object sender, EventArgs e){
            //C.RecieveVision.ManualSecuss();
            //BASE.ReadPRS(T.Head1, eHD.HD1);
            //double dValue = 0;
            //BASE.RD_BladeThickness(ref dValue);
            //DEF.SaveProduction();
            if (lbITS_ID.Text == ""){
                MessageBox.Show("ITS ID 없습니다.");
                return;
            }
            MsSQL.GetPrevProcResult(lbITS_ID.Text);
        }

        private void DGV_INFO_BARCODE_CellDoubleClick(object sender, DataGridViewCellEventArgs e){
            DataGridView dgv = (DataGridView)sender;
            if (DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eLoginLevel < eLogLevel.ADMIN || e.RowIndex < 0 || e.ColumnIndex < 0){
                UTIL_.CLEAR_GRID_SELECTED(ref dgv);
                return;
            }

            int nCol = 0, nRow = 0;
            UTIL_.GET_GRID_NUMBER(dgv, ref nCol, ref nRow);
            string sOldValue = dgv[nCol, nRow].Value == null ? "" : dgv[nCol, nRow].Value.ToString();
            string sLabel = dgv.Columns[nCol].HeaderText;
            string sValue = UTIL_.INPUT_MESSAGE("BARCODE", sLabel + " ZONE STRIP BARCODE", sOldValue, false);
            if (sValue != ""){
                if (nCol == 0) CLOT.InfoStrip[T.Gripper].Barcode    = sValue;
                if (nCol == 1) CLOT.InfoStrip[T.StripPk].Barcode    = sValue;
                if (nCol == 2) CLOT.SawStageStripBarcode            = sValue;
                if (nCol == 3) CLOT.InfoStrip[T.UnitPk].Barcode     = sValue;
                if (nCol == 4) CLOT.InfoStrip[T.DryTable1].Barcode  = sValue;
                if (nCol == 5) CLOT.InfoStrip[T.DryTable2].Barcode  = sValue;
            }
            UTIL_.CLEAR_GRID_SELECTED(ref dgv);
        }
        private void DGV_INFO_BARCODE_CellClick(object sender, DataGridViewCellEventArgs e){
            DataGridView dgv = (DataGridView)sender;
            UTIL_.CLEAR_GRID_SELECTED(ref dgv);
        }

        private void ModuleBarcodeInput_DoubleClick(object sender, EventArgs e){
            LBL = (Label)sender;
            LBL.BackColor = Color.Lime;
            string sOldValue = LBL.Text == null ? "" : LBL.Text;
            string sLabel = LBL.Tag.ToString();
            string sValue = UTIL_.INPUT_MESSAGE("BARCODE", sLabel + " ZONE STRIP BARCODE", sOldValue, false);
            //if (sValue != ""){
            if (LBL.Name == "LBL_BRCD_RAIL")        CLOT.InfoStrip[T.Gripper].Barcode   = sValue;
            if (LBL.Name == "LBL_BRCD_STRIP_PK")    CLOT.InfoStrip[T.StripPk].Barcode   = sValue;
            if (LBL.Name == "LBL_BRCD_SAW")         CLOT.SawStageStripBarcode           = sValue;
            if (LBL.Name == "LBL_BRCD_UNIT_PK")     CLOT.InfoStrip[T.UnitPk].Barcode    = sValue;
            if (LBL.Name == "LBL_BRCD_MB_1")        CLOT.InfoStrip[T.DryTable1].Barcode = sValue;
            if (LBL.Name == "LBL_BRCD_MB_2")        CLOT.InfoStrip[T.DryTable2].Barcode = sValue;
            //LBL.Text = sValue;
            //}
            LBL.Text = sValue;
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
            TmrAUTO.Enabled = true;
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
            try{
                if (InvokeRequired){
                    Invoke((MethodInvoker)delegate (){
                        AddProcessMessage(tn, msg);
                    });
                }
                else{
                    listLog.BeginUpdate();
                    if (listLog.Items.Count > 1000) listLog.Items.RemoveAt(0);
                    ListViewItem lvi = new ListViewItem(DateTime.Now.ToString("HH:mm:ss.ff"));
                    if (GetProcLog(tn)) lvi.ForeColor = Color.Gold; //return; //LOG SKIP일 경우 
                    lvi.SubItems.Add(DATA_.ThreadName[tn]);
                    lvi.SubItems.Add(msg);

                    listLog.Items.Add(lvi);
                    listLog.Items[listLog.Items.Count - 1].EnsureVisible();
                    listLog.EndUpdate();
                    //mLogWR.SAVE_PROCESS_LOG(tn, "", lvi.Text, msg);
                }
            }
            catch (Exception EX){
                LogWR_.SaveLogException("[AUTO] ADD MESSAGE FAIL!" + ETC.NewLine + "THREAD NUMBER = " + tn.ToString() + " / " + msg, EX);
            }
        }

        private void MMI_THREAD(){
            double TAG;
            int PK;
            do{
                if (DATA_.gExit) return;
                nPK = 0;
                foreach (Control mControl in gbxHD1.Controls){
                    if (mControl.GetType().Name == "PictureBox"){
                        if (nPK >= CNT_.PKR) break;
                        TAG = Math.Truncate((double)(nPK / 2));
                        PK = (int)(M.X1Z12 + TAG);

                        if (COM_.CurPkPosition(PK, P.Ready) && DATA_.PK_Z[nPK] == eSTATUS.MARK) mControl.BackgroundImage = PIC_UP_MASK.BackgroundImage;     //피커 픽업 UP OK
                        else if (COM_.CurPkPosition(PK, P.Ready) && DATA_.PK_Z[nPK] == eSTATUS.EMPTY) mControl.BackgroundImage = PIC_UP_EMPTY.BackgroundImage;    //피커 READY

                        else if ((COM_.CurPkPosition(PK, P.PK_PIC[nPK])) && DATA_.PK_Z[nPK] == eSTATUS.MARK) mControl.BackgroundImage = PIC_DOWN_MASK.BackgroundImage;   //피커 픽업 DOWN Y
                        else if ((COM_.CurPkPosition(PK, P.PK_PLC[nPK])) && DATA_.PK_Z[nPK] == eSTATUS.MARK) mControl.BackgroundImage = PIC_DOWN_MASK.BackgroundImage;   //피커 플레이스 DOWN Y
                        else if ((COM_.CurPkPosition(PK, P.PK_PIC[nPK])) && DATA_.PK_Z[nPK] == eSTATUS.EMPTY) mControl.BackgroundImage = PIC_DOWN_EMPTY.BackgroundImage;  //피커 픽업 DOWN N
                        else if ((COM_.CurPkPosition(PK, P.PK_PLC[nPK])) && DATA_.PK_Z[nPK] == eSTATUS.EMPTY) mControl.BackgroundImage = PIC_DOWN_EMPTY.BackgroundImage;   //피커 플레이스 DOWN N

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

                        if (COM_.CurPkPosition(PK, P.Ready) && DATA_.PK_Z[nPK + (CNT_.PKR * 1)] == eSTATUS.MARK)                mControl.BackgroundImage = PIC_UP_MASK.BackgroundImage;
                        else if (COM_.CurPkPosition(PK, P.Ready) && DATA_.PK_Z[nPK + (CNT_.PKR * 1)] == eSTATUS.EMPTY)          mControl.BackgroundImage = PIC_UP_EMPTY.BackgroundImage;

                        else if ((COM_.CurPkPosition(PK, P.PK_PIC[nPK])) && DATA_.PK_Z[nPK + (CNT_.PKR * 1)] == eSTATUS.MARK)   mControl.BackgroundImage = PIC_DOWN_MASK.BackgroundImage;
                        else if ((COM_.CurPkPosition(PK, P.PK_PLC[nPK])) && DATA_.PK_Z[nPK + (CNT_.PKR * 1)] == eSTATUS.MARK)   mControl.BackgroundImage = PIC_DOWN_MASK.BackgroundImage;
                        else if ((COM_.CurPkPosition(PK, P.PK_PIC[nPK])) && DATA_.PK_Z[nPK + (CNT_.PKR * 1)] == eSTATUS.EMPTY)  mControl.BackgroundImage = PIC_DOWN_EMPTY.BackgroundImage;
                        else if ((COM_.CurPkPosition(PK, P.PK_PLC[nPK])) && DATA_.PK_Z[nPK + (CNT_.PKR * 1)] == eSTATUS.EMPTY)  mControl.BackgroundImage = PIC_DOWN_EMPTY.BackgroundImage;

                        else if (DATA_.PK_Z[nPK + (CNT_.PKR * 1)] == eSTATUS.NONE) mControl.BackgroundImage = PIC_NONE.BackgroundImage;
                        nPK++;
                    }
                }
                Thread.Sleep(3);
            } while (true);
        }
        public void CreatePicker(){
            for (int i = 0; i < CNT_.PKR; i++){
                this.PIC_X1 = new PictureBox();
                PIC_X1.Location = new Point(16 + (i * 43), 16);
                PIC_X1.Size = new Size(30, 55); //33, 65
                PIC_X1.Tag = i;//= cMT.MT_HD1_Z12 + dTAG;                     
                PIC_X1.BackgroundImageLayout = ImageLayout.Stretch;
                gbxHD1.Controls.Add(PIC_X1);
            }
            for (short i = 0; i < CNT_.PKR; i++){
                this.PIC_X2 = new PictureBox();
                PIC_X2.Location = new Point(16 + (i * 43), 16); //15 + (k * 43), 15
                PIC_X2.Size = new Size(30, 55); //32, 60
                PIC_X2.Tag = i;//= cMT.MT_HD2_Z12 + dTAG;  //= CLS_DEF.MT_Z10 + k;
                PIC_X2.BackgroundImageLayout = ImageLayout.Stretch;
                gbxHD2.Controls.Add(PIC_X2);
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
            GridWorkLot.RowCount = CLOT.WORK_LOT.Length;
            for (int i = 0; i < GridWorkLot.RowCount; i++){
                //GridWorkLot[0, i].Value = (i + 1).ToString();
                row = GridWorkLot.Rows[i];
                row.Height = 20;
                nHeight += row.Height;
            }
            UTIL_.CLEAR_GRID_SELECTED(ref GridWorkLot);
            GridWorkLot.Height = nHeight;

            nHeight = 20 + 2;
            GridProcCondition_1.RowCount = 1;
            for (int i = 0; i < GridProcCondition_1.RowCount; i++){
                row = GridProcCondition_1.Rows[i];
                row.Height = 20;
                nHeight += row.Height;
            }
            UTIL_.CLEAR_GRID_SELECTED(ref GridProcCondition_1);
            GridProcCondition_1.Height = nHeight + 17;

            nHeight = 20 + 2;
            GridProcCondition_2.RowCount = 2;
            for (int i = 0; i < GridProcCondition_2.RowCount; i++){
                row = GridProcCondition_2.Rows[i];
                row.Height = 20;
                nHeight += row.Height;
            }
            UTIL_.CLEAR_GRID_SELECTED(ref GridProcCondition_2);
            GridProcCondition_2.Height = nHeight + 17;
        } 

        private void FormAuto_Load(object sender, EventArgs e){
            CreatePicker();
            COM_.RECREATE_THREAD(ref DATA_.mcTH[T.Picker], MMI_THREAD);

             LogWR_.ProMsgEvent  += AddProcessMessage;
            BASE.ProMsgEvent    += AddProcessMessage;
             
            SUBFRM_.cBarcode.Conect();
            INI_GRID();
            IniInfoStripBarcoder(DGV_INFO_BARCODE, 1, 20);
            RunningEvent(EES_DISPLAY);

            gbxOP.Text = DEF.UpdataMemo;
        }

        private void FormAuto_FormClosing(object sender, FormClosingEventArgs e){
            LogWR_.ProMsgEvent  -= AddProcessMessage;
            BASE.ProMsgEvent    -= AddProcessMessage;
        }

        void InvokeSignal(){
            lbComSawMachine.BackColor = DATA_.mIN[I.SAW_READY] ? Color.Lime : Color.White;
            lbComSorterVision.BackColor = DATA_.mIN[I.VisionRdy] ? Color.Lime : Color.White;

            LBL_MESSAGE2.Text = DATA_.editErrTitle_1;
            LBL_MESSAGE1.Text = DATA_.editErrTitle_2;

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

            lbVAC_MB1.BackColor = DATA_.mOUT[O.STAGE1_VAC] ? Color.Red : Color.White;
            lbDRAIN_MB1.BackColor = DATA_.mOUT[O.STAGE1_DRAIN] ? Color.Red : Color.White;
            lbBACKVAC_MB1.BackColor = DATA_.mOUT[O.STAGE1_BACK_VAC] ? Color.Red : Color.White;
            iVAC_MB1.BackColor = DATA_.mIN[I.STAGE_VACUUM1] ? Color.Lime : Color.White;

            lbVAC_MB2.BackColor = DATA_.mOUT[O.STAGE2_VAC] ? Color.Red : Color.White;
            lbDRAIN_MB2.BackColor = DATA_.mOUT[O.STAGE2_DRAIN] ? Color.Red : Color.White;
            lbBACKVAC_MB2.BackColor = DATA_.mOUT[O.STAGE2_BACK_VAC] ? Color.Red : Color.White;
            iVAC_MB2.BackColor = DATA_.mIN[I.STAGE_VACUUM2] ? Color.Lime : Color.White;

            lbGT1.BackColor = (int)eTRAY.GOOD1 == (int)DATA_.IsLONG[L.CurWorkTray] ? Color.Lime : Color.White;
            lbGT2.BackColor = (int)eTRAY.GOOD2 == (int)DATA_.IsLONG[L.CurWorkTray] ? Color.Lime : Color.White;

            cpHD1.Text = DATA_.mtSTS[M.TRIGGER1].CurrentPosition.ToString("0.0");
            cpHD2.Text = DATA_.mtSTS[M.TRIGGER2].CurrentPosition.ToString("0.0");

            cpT1.Text = DATA_.cntSTS[DATA_.SubTrigger[0]].CurrentPosition.ToString("0.0");
            cpT2.Text = DATA_.cntSTS[DATA_.SubTrigger[1]].CurrentPosition.ToString("0.0");

            PowerMeter.BackColor = DATA_.cPM.IsOpen() ? Color.Lime : Color.White;
            Barcode.BackColor = SUBFRM_.cBarcode.bOpen ? Color.Lime : Color.White;
            RFReader.BackColor = SUBFRM_.gRFID.bOpen ? Color.Lime : Color.White;

            lbRFID.Text         = SUBFRM_.gRFID.ReadRFID;
            lbBarcode.Text      = SUBFRM_.cBarcode.ReadResult;

            LBL_EQPCODE.Text    = DATA_.EQPCode;
            USER_ID.Text        = CUSER.Current.ID;
            LOT_ID.Text         = CLOT.GET_LOT.LotID;
            lbLOT_ID.Text       = CLOT.GET_LOT.LotID;
            lbITS_ID.Text       = CLOT.GET_LOT.ItsID;

            LBL_USER_ID.Text    = CUSER.Current.ID;
            LBL_USER_NAME.Text  = CUSER.Current.Name;

            for (int i = 0; i < iInterfaceState.Length; i++){
                iInterfaceState[i].ForeColor = DATA_.mIN[/*I.MNInput[i]*/ iInterfaceState[i].TabIndex] ? Color.Lime : Color.White;
            }
            for (int i = 0; i < oInterfaceState.Length; i++){
                oInterfaceState[i].ForeColor = DATA_.mOUT[oInterfaceState[i].TabIndex] ? Color.Red : Color.White;
            }

            lblSAW_MAIN_AIR.BackColor   = DATA_.mIN[I.SAW_MAIN_AIR] ? Color.Lime : Color.DarkGreen;
            lbStripPkrVac.BackColor     = DATA_.mIN[I.STRIP_PK_VAC] ? Color.Lime : Color.White;
            lbInletVac.BackColor        = DATA_.mIN[I.INLET_TABLE_VAC] ? Color.Lime : Color.White;
            lbUnitPkrVac1.BackColor     = DATA_.mIN[I.UNIT_PK_VAC] ? Color.Lime : Color.White;
            lbUnitPkrScrap.BackColor    = DATA_.mIN[I.SCRAP_VAC1] ? Color.Lime : Color.White;
            lbUnitPkrScrap2.BackColor   = DATA_.mIN[I.SCRAP_VAC2] ? Color.Lime : Color.White;
            lbSORTER_MAIN_AIR.BackColor = DATA_.mIN[I.DRIVER_AIR_PRESSURE] && DATA_.mIN[I.BLOW_AIR_PRESSURE] && DATA_.mIN[I.STAGE_AIR_PRESSURE] && DATA_.mIN[I.PICKER_AIR_PRESSURE] ? Color.Lime : Color.White;
            lbMapBlock1.BackColor       = DATA_.mIN[I.STAGE_VACUUM1] ? Color.Lime : Color.White;
            lbMapBlock2.BackColor       = DATA_.mIN[I.STAGE_VACUUM2] ? Color.Lime : Color.White;

            lbStripPkrXSafety.BackColor = DATA_.prMACHINE[CP.StripPkSafetyPosition] < DATA_.mtSTS[M.StripPkX].CurrentPosition ? Color.Red : Color.White;
            lbUnitPkrXSafety.BackColor  = DATA_.prMACHINE[CP.UnitPkSafetyPosition] < DATA_.mtSTS[M.UnitPkX].CurrentPosition ? Color.Red : Color.White;

            UseHD1.BackColor            = (DATA_.prMACHINE[CP.SelectHead] == (int)eHD.ALL || DATA_.prMACHINE[CP.SelectHead] == (int)eHD.HD1) ? Color.Lime : Color.Red;
            UseHD2.BackColor            = (DATA_.prMACHINE[CP.SelectHead] == (int)eHD.ALL || DATA_.prMACHINE[CP.SelectHead] == (int)eHD.HD2) ? Color.Lime : Color.Red;

            if (DATA_.bWriteLotInfo){
                DATA_.bWriteLotInfo = false;
                DEF.CountReset(true);
            }

            if (DATA_.bStripDefect){
                MsSQL.GetPrevProcResult(CLOT.GET_LOT.ItsID);
                DATA_.bStripDefect = false;
            }

            EES_DISPLAY.BackColor = bEES_DISPLY ? Color.Lime : Color.White;

            lbTEST_POS1.Text = DATA_.mtSTS[M.X1T].CurrentPosition.ToString();
            lbTEST_POS2.Text = DATA_.mtSTS[M.X2T].CurrentPosition.ToString();

            lbX401.BackColor = DATA_.mIN[I.GOOD_TRAY2_FEEDER_TRAY_CHECK] ? Color.Lime : Color.White;
            lbX314.BackColor = DATA_.mIN[I.GOOD_TRAY1_FEEDER_TRAY_CHECK] ? Color.Lime : Color.White;

            lbX310.BackColor = DATA_.mIN[I.GOOD_RAIL_HEAD2_CHECK] ? Color.Lime : Color.White;
            lbX309.BackColor = DATA_.mIN[I.GOOD_RAIL_HEAD1_CHECK] ? Color.Lime : Color.White;
            lbX308.BackColor = DATA_.mIN[I.GOOD_RAIL_TRAY_LOADING_CHECK] ? Color.Lime : Color.White;
            lbX307.BackColor = DATA_.mIN[I.GOOD_RAIL_STACKER_CHECK] ? Color.Lime : Color.White;

            lbX303.BackColor = DATA_.mIN[I.NG_TRAY_FEEDER_TRAY_CHECK] ? Color.Lime : Color.White;

            lbX300.BackColor = DATA_.mIN[I.NG_RAIL_HEAD2_TRAY_CHECK] ? Color.Lime : Color.White;
            lbX215.BackColor = DATA_.mIN[I.NG_RAIL_HEAD1_TRAY_CHECK] ? Color.Lime : Color.White;
            lbX214.BackColor = DATA_.mIN[I.NG_RAIL_LOADING_TRAY_CHECK] ? Color.Lime : Color.White;
            lbX213.BackColor = DATA_.mIN[I.NG_RAIL_STACKER_TRAY_CHECK] ? Color.Lime : Color.White;

            lbSTAGE1_OFFSET_X.Text = DATA_.IsDOUBLE[D.Stage1UnitOffsetX].ToString();
            lbSTAGE1_OFFSET_Y.Text = DATA_.IsDOUBLE[D.Stage1UnitOffsetY].ToString();

            lbSTAGE2_OFFSET_X.Text = DATA_.IsDOUBLE[D.Stage2UnitOffsetX].ToString();
            lbSTAGE2_OFFSET_Y.Text = DATA_.IsDOUBLE[D.Stage2UnitOffsetY].ToString();
			
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

            label26.Text = DATA_.IsDOUBLE[D.UnitPk_PicOffsetX].ToString();
            lbMGZ_CHECK_1.BackColor = DATA_.mIN[I.LD_CONV_MZ_CHECK1] ? Color.Lime : Color.White;
            lbMGZ_CHECK_2.BackColor = DATA_.mIN[I.LD_CONV_MZ_CHECK2] ? Color.Lime : Color.White;

            LBL_CNT_PANEL.Text = CLOT.CurLotStripCnt.ToString();
        }

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

            if (DATA_.IsBIT[B.CstRequest])
            {
                if (iswCstSupplyOn > 5)
                {
                    if (iswCstSupplyOff > 10)
                    {
                        iswCstSupplyOn = 0;
                        iswCstSupplyOff = 0;
                    }
                    else
                    {
                        swCst_SUPPLY.BackColor = Color.Red;
                        iswCstSupplyOff += 1;
                    }
                }
                else
                {
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
                    COM_.SET_DOORLOCK();
                    if (!COM_.CHK_DOOR()){
                        UTIL_.OnERROR(E.emsDoorOpen);
                    } //300000ms = 300sec = 5min
                } // * 100ms
                if (!DATA_.IsBIT[B.SkipDoorLock]) DATA_.IsLONG[L.CheckDoorSkipTime]++;
            }
            else DATA_.IsLONG[L.CheckDoorSkipTime] = 0;
            lbDoorSkipTime.Text = DATA_.IsLONG[L.CheckDoorSkipTime].ToString() + "/3000";
        }
        void InvokeTack(){
            editStatus.Text = DATA_.eMCStatus.ToString();

            DATA_.viewSPC = DATA_.oSPC;
            dclRUN.DigitText = DATA_.cMATH.IntToTime(DATA_.viewSPC.mlRunTime);
            dclSTOP.DigitText = DATA_.cMATH.IntToTime(DATA_.viewSPC.mlStopTime);
            dclERROR.DigitText = DATA_.cMATH.IntToTime(DATA_.viewSPC.mlErrorTime);

            ddcTack.DigitText = string.Format("{0:0.00}", DATA_.IsDOUBLE[D.PkgUPH]);
            ddcAvg.DigitText = string.Format("{0:0.00}", DATA_.IsDOUBLE[D.Tact_Avg]);
            DATA_.IsDOUBLE[D.UPH] = (3600 / DATA_.IsDOUBLE[D.Tact_Avg]) / 1000;
            ddcUPH.DigitText = string.Format("{0:0.00}", DATA_.IsDOUBLE[D.UPH]);
            ddcTrayCycle.DigitText = DATA_.cMATH.IntToTime((long)DATA_.IsDOUBLE[D.TrayCycle]); //string.Format("{0:0.0}", mDATA.IsDOUBLE[mD.mdCycleTime]);//IsDOUBLE[D.TrayCycle]
            StripPlaceTack.DigitText = DATA_.cMATH.IntToTime((long)DATA_.IsDOUBLE[D.StripPkCycle]);
            dDsp_UnitPkrCycleTime.DigitText = DATA_.cMATH.IntToTime((long)DATA_.IsDOUBLE[D.UnitPkCycle]);
            ddcMapBlock1CycleTime.DigitText = DATA_.cMATH.IntToTime((long)DATA_.IsDOUBLE[D.Stage1Cycle]);
            ddcMapBlock2CycleTime.DigitText = DATA_.cMATH.IntToTime((long)DATA_.IsDOUBLE[D.Stage2Cycle]);

            dcStripLDCnt.DigitText = DATA_.IsLONG[L.StripCnt].ToString();
            dcUnitULDCnt.DigitText = DATA_.IsLONG[L.UnitCnt].ToString();
            dcEmptyCnt.DigitText = DATA_.IsLONG[L.EmptyTrayCnt].ToString();
            dcGoodTryCnt.DigitText = DATA_.IsLONG[L.GoodTrayCnt].ToString();
            dcReworkTryCnt.DigitText = DATA_.IsLONG[L.ReworkTrayCnt].ToString();
            dcLOTCnt.DigitText = DATA_.IsLONG[L.LotCnt].ToString();
            dcUnitSizeNGCnt.DigitText = DATA_.IsLONG[L.UnitSizeNgCnt].ToString();

            dcTotalCnt.DigitText = DATA_.IsLONG[L.OutCnt].ToString();
            dcGoodChipCnt.DigitText = DATA_.IsLONG[L.GoodCnt].ToString();
            dcReworkChipCnt.DigitText = DATA_.IsLONG[L.ReworkCnt].ToString();
            dcXMarkChipCnt.DigitText = DATA_.IsLONG[L.NGCnt].ToString();
            dcITScnt.DigitText = DATA_.IsLONG[L.ITSCount].ToString();
        }

        private void TmrAUTO_Tick(object sender, EventArgs e){
            TmrAUTO.Enabled = false;
            Invoke();
            InvokeTack();
            Blink();
            Option();
            InvokeSignal();
            TmrAUTO.Enabled = true;
        }
        void Invoke(){
            lbCurPkr.Text = nPK.ToString("00");
            for (int i = 0; i < PkrVac.Length; i++){
                PkrVac[i].Text = (DATA_.mAI[i]).ToString();
                PkrVac[i].ForeColor = DATA_.mAI[i] > DATA_.mSET_AI[i] ? Color.Lime : Color.Black;
            }

            DrawMGZ(pbxCST, (int)DATA_.prMODEL[RP.MGZSlotCnt], M.ElvZ);

            DrawStage(pbxPALLET_1, (int)DATA_.prMODEL[RP.UnitCntX], (int)DATA_.prMODEL[RP.UnitCntY], (int)DATA_.prMODEL[RP.GroupCntX], (int)DATA_.prMODEL[RP.GroupCntY], (int)eMAP_BLOCK.STAGE1);
            DrawStage(pbxPALLET_2, (int)DATA_.prMODEL[RP.UnitCntX], (int)DATA_.prMODEL[RP.UnitCntY], (int)DATA_.prMODEL[RP.GroupCntX], (int)DATA_.prMODEL[RP.GroupCntY], (int)eMAP_BLOCK.STAGE2);

            DrawTray(pbxGOOD, (int)DATA_.prMODEL[RP.TrayCntX], (int)DATA_.prMODEL[RP.TrayCntY], (int)DATA_.IsLONG[L.CurWorkTray]);
            DrawTray(pbxREWORK, (int)DATA_.prMODEL[RP.TrayCntX], (int)DATA_.prMODEL[RP.TrayCntY], (int)eTRAY.REWORK);

            lbPRS_START.ForeColor = DATA_.mOUT[O.PRSStart] ? Color.Red : Color.Black;
            lbPRS_READ.ForeColor = DATA_.mOUT[O.PRSReading] ? Color.Red : Color.Black;
            lbPRS_RESULT.ForeColor = DATA_.mIN[I.PRSVisionWirte] ? Color.Lime : Color.Black;
            WipCheck.BackColor = DATA_.prMACHINE[CP.UseMES] == (int)eUSE.USE ? Color.Lime : Color.White;

            for (int n = 0; n < CLOT.WORK_LOT.Length; n++){
                CLOT.WORK_LOT[n].LoadingCount   = CLOT.WORK_LOT[0].LoadingCount;
                CLOT.WORK_LOT[n].UnloadingCount = CLOT.WORK_LOT[0].UnloadingCount;

                GridEndLot.Rows[n].Cells[1].Value = CLOT.FINISH_LOT[n].WorkScope;
                GridEndLot.Rows[n].Cells[2].Value = CLOT.FINISH_LOT[n].ToolNo;
                GridEndLot.Rows[n].Cells[3].Value = CLOT.FINISH_LOT[n].Recipe;
                GridEndLot.Rows[n].Cells[4].Value = CLOT.FINISH_LOT[n].LotID;

                GridEndLot.Rows[n].Cells[5].Value = CLOT.FINISH_LOT[n].WorkScope == "" || CLOT.FINISH_LOT[n].WorkScope == null ? "" : CLOT.FINISH_LOT[n].CurCnt.ToString();
                GridEndLot.Rows[n].Cells[6].Value = CLOT.FINISH_LOT[n].WorkScope == "" || CLOT.FINISH_LOT[n].WorkScope == null ? "" : CLOT.FINISH_LOT[n].LoadingCount.ToString();
                GridEndLot.Rows[n].Cells[7].Value = CLOT.FINISH_LOT[n].WorkScope == "" || CLOT.FINISH_LOT[n].WorkScope == null ? "" : CLOT.FINISH_LOT[n].ExceptCount.ToString();
                GridEndLot.Rows[n].Cells[8].Value = CLOT.FINISH_LOT[n].WorkScope == "" || CLOT.FINISH_LOT[n].WorkScope == null ? "" : CLOT.FINISH_LOT[n].UnloadingCount.ToString();
                GridEndLot.Rows[n].Cells[9].Value = CLOT.FINISH_LOT[n].WorkSort;
            }
        }

        void DrawMGZ(PictureBox image, int cnt, int m){
            if (image == null || (cnt == 0)) return;
            try{
                Graphics G;

                G = Graphics.FromImage(MGZ);
                G.FillRectangle(Brushes.White, 0, 0, MGZ.Width, MGZ.Height);

                uGW = (int)image.Width;
                uGH = (int)(image.Height / cnt);

                if (uH < 1) return;
                for (int i = 0; i < cnt; i++){
                    y_offset = i / 1;

                    uX1 = 0;
                    uX2 = uX1 * uGW;

                    uY2 = y_offset * uGH;

                    mBrush.Color = TEACH_.COLOR_CODE(MAP_.ARR_MGZ[m, i]);

                    G.FillRectangle(mBrush, uX2 + 1, uY2 + 1, uGW - 2, uGH - 2);
                }
                image.Image = MGZ;
                G.Dispose();
            }
            catch (Exception ex){
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
        void DrawStage(PictureBox image, int unit_x, int unit_y, int group_x, int group_y, int stage){
            if (image == null || (unit_x == 0 || unit_y == 0)) return;
            try{
                Graphics G;

                G = Graphics.FromImage(STAGE[stage]);
                G.FillRectangle(Brushes.White, 0, 0, STAGE[stage].Width, STAGE[stage].Height);

                uGW = (int)(image.Width / group_x) - 3;
                uGH = (int)(image.Height / group_y) - 3;

                uW = (int)(uGW / unit_x);
                uH = (int)(uGH / unit_y);

                if (uGW < 1 || uGH < 1 || uW < 1 || uH < 1) return;

                for (int gy = 0; gy < group_y; gy++){
                    for (int gx = 0; gx < group_x; gx++){
                        for (int uy = 0; uy < unit_y; uy++){
                            for (int ux = 0; ux < unit_x; ux++){
                                y_offset = uy;

                                uX1 = ux % unit_x;
                                uX2 = (uX1 * uW) + (gx * uGW) + (gx * 5);

                                uY2 = (y_offset * uH) + (gy * uGH) + (gy * 5);

                                if (stage > 1) mBrush.Color = TEACH_.COLOR_CODE(eSTATUS.EMPTY);
                                else mBrush.Color = TEACH_.COLOR_CODE(MAP_.ARR_PALLET[stage, gy, gx, ux, uy]);

                                G.FillRectangle(mBrush, uX2, uY2, uW - 2, uH - 2);
                            }
                        }
                    }
                }
                image.Image = STAGE[stage];
                G.Dispose();
            }
            catch (Exception e) { System.Diagnostics.Trace.WriteLine(e.Message); }
        }
        void DrawTray(PictureBox image, int x, int y, int tray){
            if (image == null || x == 0 || y == 0) return;
            try{
                Graphics G;
                if (tray == (int)eTRAY.GOOD1 || tray == (int)eTRAY.GOOD2) DATA_.IsLONG[L.uiTray] = 0;
                else DATA_.IsLONG[L.uiTray] = 1;

                G = Graphics.FromImage(TRAY[DATA_.IsLONG[L.uiTray]]);
                G.FillRectangle(Brushes.White, 0, 0, TRAY[DATA_.IsLONG[L.uiTray]].Width, TRAY[DATA_.IsLONG[L.uiTray]].Height);

                uGW = (int)(image.Width / x);
                uGH = (int)(image.Height / y);
                if (uGW < 1 || uGH < 1) return;

                for (int i = 0; i < (y * x); i++){
                    y_offset = i / x;

                    uX1 = i % x;
                    uX2 = uX1 * uGW;

                    uY2 = y_offset * uGH;

                    if (tray > 2) mBrush.Color = TEACH_.COLOR_CODE(eSTATUS.EMPTY);
                    else mBrush.Color = TEACH_.COLOR_CODE(MAP_.ARR_TRAY[tray, i]);

                    //System.Diagnostics.Trace.WriteLine(uX2.ToString() + "(" + uW.ToString() + ") " + uY2.ToString() + "(" + uH.ToString() + ")");
                    G.FillRectangle(mBrush, uX2, uY2, uGW - 2, uGH - 2);
                }
                image.Image = TRAY[DATA_.IsLONG[L.uiTray]];
                G.Dispose();
            }
            catch (Exception e) { System.Diagnostics.Trace.WriteLine(e.Message); }
        }
    }
}