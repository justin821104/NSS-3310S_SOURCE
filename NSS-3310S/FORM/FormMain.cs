using LIB_.DateType;
using LIB_.SubFROMLib;
using NSS_3310S.ITS;
using NSS_3310S.SEQ;
using Object;
using System;
using System.Drawing;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace NSS_3310S
{
    public partial class FormMain : Form{
        Multimedia.Timer mmTimer = new Multimedia.Timer();
        Label LBL;
        Button BTN;
        int BeforTime = 0;
        int nERR;
        private bool bChageLabel = false;
        int isDryOn = 0, isDryOff = 0;
        int nSendPVIDCont = 0;
        int nBLADE_CHANGE_RESET = 0;
        public FormMain(){
            InitializeComponent();

            #region EVENT
            BTN_EXIT.Click += (sender, e) => { EXIT(); };

            pnlLOGO.DoubleClick += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO) SUBFRM_.gPM.RD_PM(); };
            lbLoginStatus.DoubleClick += (sender, e) => OPTION(lbLoginStatus);
            lbDryRunStatus.DoubleClick += (sender, e) => OPTION(lbDryRunStatus);
            lbSpeed.DoubleClick += (sender, e) => OPTION(lbSpeed);

            swLogIn.Click += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO && DATA_.eMCStatus != eMachineStatus.INITIAL && DATA_.eLevelMainSw != eMainLevel.LOGIN) OP(swLogIn); };
            swAuto.Click += (sender, e) => { if (/*DATA_.eMCStatus != eMachineStatus.AUTO &&*/ DATA_.eLevelMainSw != eMainLevel.AUTO) OP(swAuto); };
            swDEVICE.Click += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO && DATA_.eMCStatus != eMachineStatus.INITIAL && DATA_.eLevelMainSw != eMainLevel.DEVICE) OP(swDEVICE); };
            swManual.Click += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO && DATA_.eMCStatus != eMachineStatus.INITIAL && DATA_.eLevelMainSw != eMainLevel.MANUALRUN) OP(swManual); };
            swHistory.Click += (sender, e) => { if (/*DATA_.eMCStatus != eMachineStatus.AUTO &&*/ DATA_.eLevelMainSw != eMainLevel.LOGVIEW) OP(swHistory); };
            swIO.Click += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO && DATA_.eMCStatus != eMachineStatus.INITIAL && DATA_.eLevelMainSw != eMainLevel.IOVIEW) OP(swIO); };
            swSet.Click += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO && DATA_.eLevelMainSw != eMainLevel.SETTING) OP(swSet); };
            swVisionSet.Click += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO && DATA_.eMCStatus != eMachineStatus.INITIAL && DATA_.eLevelMainSw != eMainLevel.VISION) OP(swVisionSet); };
            swMotionSet.Click += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO && DATA_.eMCStatus != eMachineStatus.INITIAL && DATA_.eLevelMainSw != eMainLevel.MOTION) OP(swMotionSet); };

            lbTime.DoubleClick += (sender, e) => { ChangeLabel(); };
            #endregion
        }

        #region >>Event
        void EXIT(){
            if (DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.INITIAL || DATA_.bMF) return;
            Close();
        }
        void OPTION(object sender){
            LBL = sender as Label;
            if (LBL.Name == "lbLoginStatus"){
                if (UTIL_.INPUT_MESSAGE("OFF-LIMITS ZONE", "SOFTWATER ACCESS AREA !", "", true) != DATA_.stPassWord.SOFT) return;
                DATA_.eLoginLevelBuffer = DATA_.eLoginLevel;
                DATA_.eLoginLevel = eLogLevel.SOFT;
                SUBFRM_.gVENDER.INI();
            }
            if (LBL.Name == "lbDryRunStatus"){
                if (((int)DATA_.eLoginLevel < (int)eLogLevel.ENG) || DATA_.eMCStatus == eMachineStatus.AUTO) return;
                if (!DATA_.bDRYRUN)
                    if (!UTIL_.PRINT_MASSAGE("MACHINE DRY-RUN ?", false, false, false)) return;
                DATA_.bDRYRUN = !DATA_.bDRYRUN;
            }
            if (LBL.Name == "lbSpeed"){
                if (DATA_.eLoginLevel < eLogLevel.ENG) return;
                double dVALUE = UTIL_.OPEN_KEYPAD("SPEED RATE", DATA_.prMACHINE[CP.RunRate], false);
                try{
                    if (dVALUE > 100){
                        UTIL_.PRINT_MASSAGE("가동율은 100%를 넘을 수 없습니다 !" + ETC.CrLf + "MAX RATE VALUE IS 100 !", false, true, true);
                        return;
                    }
                    if (dVALUE < 1){
                        UTIL_.PRINT_MASSAGE("가동율은 1% 이하로 설정 할 수 없습니다 !" + ETC.CrLf + "MIN RATE VALUE IS 1 !", false, true, true);
                        return;
                    }
                    TEACH_.WR_MCPara(CP.RunRate, dVALUE);
                    DATA_.prMACHINE[CP.RunRate] = dVALUE;
                    DATA_.dRunRate = DATA_.prMACHINE[CP.RunRate];
                }
                catch (Exception ex) { LogWR_.SaveLogException("frmMAIN->lbSpeed_DoubleClick Fail", ex); }
            }
        }
        void Event_Click(eMainLevel MC){
            DATA_.eLevelMainSw = MC;
            Form_Hide();
        }
        void OP(object sender){
            BTN = sender as Button;
            if (BTN.Name == "swLogIn"){
                Event_Click(eMainLevel.LOGIN);
                SUBFRM_.gLogin.INI();
            }
            if (BTN.Name == "swAuto"){
                Event_Click(eMainLevel.AUTO);
                F.Auto.Initailize_View();
            }
            if (BTN.Name == "swDEVICE"){
                Event_Click(eMainLevel.DEVICE);
                F.Device.Initailize_View();
            }
            if (BTN.Name == "swManual"){
                Event_Click(eMainLevel.MANUALRUN);
                F.Manual.Initailize_View();
            }
            if (BTN.Name == "swHistory"){
                Event_Click(eMainLevel.LOGVIEW);
                SUBFRM_.gLOG.INI();
            }
            if (BTN.Name == "swIO"){
                Event_Click(eMainLevel.IOVIEW);
                SUBFRM_.gIO.INI();
            }
            if (BTN.Name == "swSet"){
                Event_Click(eMainLevel.SETTING);
                F.System.Initailize_View();
            }
            if (BTN.Name == "swTenkey"){
                Event_Click(eMainLevel.TENKEY);
                SUBFRM_.gTenkeyList.INI();
            }
            if (BTN.Name == "swVisionSet"){
                Event_Click(eMainLevel.VISION);
                F.Vision.Initailize_View();
            }
            if (BTN.Name == "swMotionSet"){
                Event_Click(eMainLevel.MOTION);
                F.Motor.Initailize_View();
            }
        }

        void Form_Hide(){
            F.Auto.Hide();
            F.Manual.Hide();
            F.Motor.Hide();
            F.Device.Hide();
            F.System.Hide();
            F.Vision.Hide();

            SUBFRM_.gIO.Hide();
            SUBFRM_.gLogin.Hide();
            SUBFRM_.gLOG.Hide();
            SUBFRM_.gTenkeyList.Hide();

            F.Auto.TmrAUTO.Enabled = false;
            F.Auto.TRearTime.Enabled = false;
            F.Manual.TmrMANUAL.Enabled = false;
            F.Motor.TmrMT.Enabled = false;
            F.Device.TmrRECIPE.Enabled = false;
            F.System.TmrSYSTEM.Enabled = false;
            F.Vision.TmrVISION.Enabled = false;
            SUBFRM_.gIO.tmrIO.Enabled = false;
            SUBFRM_.gLogin.tmrLogin.Enabled = false;
            SUBFRM_.gLOG.bFRM_VIEW = false;

            SUBFRM_.cBarcode.cbLiveDisplay.Checked = false;

        }
        void FRM_(eMainLevel SW){
            DATA_.eLevelMainSw = eMainLevel.Null;
            switch (SW){
                case eMainLevel.AUTO:
                    OP(swAuto);
                    break;
                case eMainLevel.LOGIN:
                    OP(swLogIn);
                    break;
            }
        }

        public void GET_TENKEY(){
            COM_.RUN_MANUAL(SUBFRM_.gTenkeyList.mKEY_VAL, "FRM TENKEY LIST CLICK");
            /*Manual_TenkeyOperation(mFRM.gTenkeyList.mKEY_VAL);*/
        }
        public void OPEN_LOGIN() { FRM_(DATA_.eLevelMainSw); }

        public void ChangeLabel() { bChageLabel = true; }

        void Refresh_Picker(){
            int iPK = 0;
            int iEND;
            for (int i = M.X1Z12; i <= M.X2Z56; i++){
                iEND = iPK + 1;
                for (int j = iPK; j <= iEND; j++){
                    if (DATA_.PK_Z[iPK] == eSTATUS.EMPTY) DATA_.ARR_[iPK, 0] = DATA_.ARR_[iPK, 0];
                    else DATA_.ARR_[iPK, 0] = DATA_.PK_Z[iPK];
                }
            }
            F.Auto.CreatePicker();
        }
        void SET_LANGUAGE(){
            F.Auto.Set_Language();
            F.Manual.Set_Language();
            F.Motor.Set_Language();
            F.Device.Set_Language();
            F.System.Set_Language();
            F.Vision.Set_Language();

        }

        //TOP PANEL
        void TopPanel(){
            lbLoginStatus.Text  = DATA_.eLoginLevel.ToString();
            editPCBType.Text    = ((ePCB)(int)DATA_.prMODEL[RP.PCB_TYPE]).ToString();
            editGroup.Text      = DATA_.sGroupName;
            editRecipe.Text     = DATA_.sJobName;
            editPPID.Text       =  CMES.CurPPID;
            lbSpeed.Text        = "SPEED " + DATA_.prMACHINE[CP.RunRate].ToString() + " %";
        }

        //OP SWITCH
        void SetOPPanel(bool bLOGIN, bool bAUTO, bool bDEVICE, bool bMANUAL, bool bLOG, bool bIO, bool bPARA, bool bVISION, bool bMT){
            swLogIn.Enabled = bLOGIN;
            swAuto.Enabled = bAUTO;
            swDEVICE.Enabled = bDEVICE;
            swManual.Enabled = bMANUAL;
            swHistory.Enabled = bLOG;
            swIO.Enabled = bIO;
            swSet.Enabled = bPARA;
            swVisionSet.Enabled = bVISION;
            swMotionSet.Enabled = bMT;

            swLogIn.BackColor = DATA_.eLevelMainSw == eMainLevel.LOGIN ? Color.LightGoldenrodYellow : Color.White;
            swAuto.BackColor = DATA_.eLevelMainSw == eMainLevel.AUTO ? Color.LightGoldenrodYellow : Color.White;
            swDEVICE.BackColor = DATA_.eLevelMainSw == eMainLevel.DEVICE ? Color.LightGoldenrodYellow : Color.White;
            swManual.BackColor = DATA_.eLevelMainSw == eMainLevel.MANUALRUN ? Color.LightGoldenrodYellow : Color.White;
            swHistory.BackColor = DATA_.eLevelMainSw == eMainLevel.LOGVIEW ? Color.LightGoldenrodYellow : Color.White;
            swIO.BackColor = DATA_.eLevelMainSw == eMainLevel.IOVIEW ? Color.LightGoldenrodYellow : Color.White;
            swSet.BackColor = DATA_.eLevelMainSw == eMainLevel.SETTING ? Color.LightGoldenrodYellow : Color.White;
            swMotionSet.BackColor = DATA_.eLevelMainSw == eMainLevel.MOTION ? Color.LightGoldenrodYellow : Color.White;
        }
        void ViewOPPanel(){
            DATA_.mOUT[O.START] = DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.DRY || DATA_.eMCStatus == eMachineStatus.INITIAL ? true : false;
            DATA_.mOUT[O.RESET] = DATA_.eMCStatus == eMachineStatus.ERRSTOP || DATA_.eMCStatus == eMachineStatus.EMSSTOP ? true : false;
            DATA_.mOUT[O.STOP] = DATA_.eMCStatus == eMachineStatus.USERSTOP || DATA_.eMCStatus == eMachineStatus.WAITRUN ? true : false;

            if (DATA_.eLevelMainSw == eMainLevel.LOGIN)                                             { SetOPPanel(true, false, false, false, false, false, false, false, false); }
            else{
                if (DATA_.eLoginLevel == eLogLevel.OP)                                              { SetOPPanel(true, true, true, true, true, true, true, false, true); }
                if (DATA_.eLoginLevel == eLogLevel.ENG)                                             { SetOPPanel(true, true, true, true, true, true, true, true, true); }
                if (DATA_.eLoginLevel == eLogLevel.ADMIN || DATA_.eLoginLevel == eLogLevel.SOFT)    { SetOPPanel(true, true, true, true, true, true, true, true, true); }
            }
        }
        #endregion

        public void OpenDevice(){
            P.GetHandlerPkZSafetyPos();
            DEF.ReadSearchRoiUnitCnt();
            DEF.ReadMapBlockPickUp();
            DEF.ReadTrayPlace();

            TEACH_.WRITE_TRAIN(DATA_.sGroupName + "_" + DATA_.sJobName);
            TEACH_.WRITE_INFO_UNIT_SIZE(DATA_.prMODEL[RP.UnitSizeX], DATA_.prMODEL[RP.UnitSizeY], DATA_.prMODEL[RP.UnitPitchX], DATA_.prMODEL[RP.UnitPitchY]);
            TEACH_.WRITE_INFO_MAP_BLOCK((int)DATA_.prMODEL[RP.GroupCntX], (int)DATA_.prMODEL[RP.GroupCntY], (int)DATA_.prMODEL[RP.UnitX[(int)eMAP_BLOCK.STAGE1]], (int)DATA_.prMODEL[RP.UnitY[(int)eMAP_BLOCK.STAGE1]], (int)DATA_.prMODEL[RP.UnitX[(int)eMAP_BLOCK.STAGE2]], (int)DATA_.prMODEL[RP.UnitY[(int)eMAP_BLOCK.STAGE1]]);
            TEACH_.WRITE_INFO_PCB_TYPE((int)DATA_.prMODEL[RP.PCB_TYPE]);
        }
        public void SaveDevice(){
            Refresh_Picker();
        }

        void InitializeFrom(){
            F.Auto.MdiParent = this;
            F.Auto.Parent = panClientView;
            F.Auto.Dock = DockStyle.Fill;

            F.Manual.MdiParent = this;
            F.Manual.Parent = panClientView;
            F.Manual.Dock = DockStyle.Fill;

            F.Motor.MdiParent = this;
            F.Motor.Parent = panClientView;
            F.Motor.Dock = DockStyle.Fill;

            F.Device.MdiParent = this;
            F.Device.Parent = panClientView;
            F.Device.Dock = DockStyle.Fill;

            F.System.MdiParent = this;
            F.System.Parent = panClientView;
            F.System.Dock = DockStyle.Fill;

            F.Vision.MdiParent = this;
            F.Vision.Parent = panClientView;
            F.Vision.Dock = DockStyle.Fill;

            SUBFRM_.gIO.MdiParent = this;
            SUBFRM_.gIO.Parent = panClientView;
            SUBFRM_.gIO.Dock = DockStyle.Fill;

            SUBFRM_.gLogin.MdiParent = this;
            SUBFRM_.gLogin.Parent = panClientView;
            SUBFRM_.gLogin.Dock = DockStyle.Fill;

            SUBFRM_.gLOG.MdiParent = this;
            SUBFRM_.gLOG.Parent = panClientView;
            SUBFRM_.gLOG.Dock = DockStyle.Fill;

            SUBFRM_.gTenkeyList.MdiParent = this;
            SUBFRM_.gTenkeyList.Parent = panClientView;
            SUBFRM_.gTenkeyList.Dock = DockStyle.Fill;
        }

        void Splash_Screen() { Application.Run(new Splash()); }
        void SetSplashStatus(int nPercent, string sMessage){
            DATA_.SPLASH_PROGRESS = nPercent;
            DATA_.SPLASH_STATUS = sMessage;
        }
        
        private void FormMain_Load(object sender, EventArgs e){
            //1293, 1037
            if (!UTIL_.ChkAllReadyRun(PATH_.EXE_NAME)){
                string sMsg = "Program is Multiple Running" + ETC.CrLf + "Usig [ TaskManager ] → All " + PATH_.EXE_NAME + ".exe Close →ReExecute This Program";
                UTIL_.PRINT_MASSAGE(sMsg, false, true, false);
                DATA_.IsBIT[B.ChkEXE] = true;
                Close();
                return;
            }

            DEF.MCProcess += Environment.Is64BitProcess ? "64Bit" : "32Bit";
            Thread t = new Thread(new ThreadStart(Splash_Screen));
            t.Start();
            CNT_.IniMemory();
            DATA_.SET_MACINE_INFO();
            DATA_.EQPCode   = UTIL_.GET_EQCode();
            DATA_.MC_DIR    = UTIL_.GET_MACHINE_DIR();         
            if (DATA_.MC_DIR == 1) PATH_.MCDir = "[DIR = REVERSE]";
            CUSER.READ_CURRENT_USER();
            
            Text = PATH_.VERSION + "/" + PATH_.MCDir;
            lbAppVersion.Text = DEF.MCVersion;

            if ("" == UTIL_.GET_MSSQL_ADD(ref MsSQL.sIP, ref MsSQL.sDBName, ref MsSQL.sID, ref MsSQL.sPwd)) { }
            I.SET_INI();
            O.SET_INI();
            E.SET_INI();
            M.SET_INI();
            P.INI_PICKER_INFO();
            DATA_.cMATH.GET_CPU_SPEED(ref DATA_.lCPUSpeed);

            mmTimer.Start(); SetSplashStatus(0, "MULTIMEDIA TIMER RUN");
            W.InitailizeWarnning();

            PATH_.Make_Folders(); SetSplashStatus(10, "MAEK FOLDER");
            DATA_.IniCleanData();
            DATA_.IniWorkedCleanData();
            DEF.ReadName(); SetSplashStatus(20, "READ LABEL");
            DEF.ReadMachine();

            CLOT.CLEAR_FINISH_LOT();
            CLOT.CLEAR_GET_LOT();
            CLOT.GET_LOT.ABFMATERIAL = UTIL_.GET_MES_ABF();
            
            CLOT.ClearStripBarcodeInfo();
            UTIL_.GET_LOT_INFO();
            UTIL_.READ_WORKED_LOT_INFO();
            CLOT.bABF               = UTIL_.GET_ABF_LIST();
            CLOT.bBladeInfo_Sp1     = UTIL_.GET_SPINDLE_BLADE_BARCODE(eSPINDLE.SP1);
            CLOT.bBladeInfo_Sp2     = UTIL_.GET_SPINDLE_BLADE_BARCODE(eSPINDLE.SP2);
            DATA_.sCurrJobName      = UTIL_.GET_JOB_FILE_NAME();
            DATA_.sCurrVisionName   = UTIL_.GET_VISION_FILE_NAME();
            CMES.CurPPID            = UTIL_.GET_PPID_NAME();
            //UTIL_.READ_WORKED_LOT_INFO();  //완료 낫 정보 읽어오기

            if (UTIL_.OpenJobFile()){
                OpenDevice();
                TEACH_.Read_ManualInspection((int)DATA_.prMODEL[RP.GroupCntX], (int)DATA_.prMODEL[RP.GroupCntY], (int)DATA_.prMODEL[RP.UnitCntX], (int)DATA_.prMODEL[RP.UnitCntY], (int)DATA_.prMODEL[RP.UnitCntX], (int)DATA_.prMODEL[RP.UnitCntY]);
            }
            UTIL_.GET_PPID_RECIPE_NAME(PATH_.PPID + CMES.CurPPID + ".txt", ref CMES.CUR_GROUP, ref CMES.CUR_DEVICE, ref CMES.CUR_VISION, ref CMES.CUR_SAW);
            SetSplashStatus(40, "READ RECIPE DATA");

            DEF.ResetPallet(eMAP_BLOCK.STAGE1);
            DEF.ResetPallet(eMAP_BLOCK.STAGE2);

            DEF.ResetTray(eTRAY.GOOD1);
            DEF.ResetTray(eTRAY.GOOD2);
            DEF.ResetTray(eTRAY.REWORK);

            SetSplashStatus(50, "READ PALLET & TRAY POS");

            InitializeFrom();
            I.INI_OnlyCheckArray();
            O.INI_OnlyCheckArray();
            SetSplashStatus(55, "INITIALIZE AZIN");
            if (DEF.CreateClass()) lbTime.BackColor = Color.Lime;
            else lbTime.BackColor = Color.Red;
            SetSplashStatus(60, "INITIALIZE FORM AND CLASS, MOTION");

            #region >>EVENT
            SUBFRM_.gTenkeyList.SEND_TENKEY += new EventHandler_(GET_TENKEY);
            SUBFRM_.gLogin.EVENT_OPEN += new EventHandler_OPEN(OPEN_LOGIN);
            F.Device.EVENT_OPEN += new EventHandler_DEVICE_OPEN(OpenDevice);
            SetSplashStatus(70, "INITAIL EVENT");

            DATA_.cSPC.LOAD_SPC();
            SPC.Enabled = true;
            SetSplashStatus(85, "MACHINE POWER ON"); //SUCCESS EVENT
            UTIL_.DELAY(100);

            OP(swLogIn);
            
            DEF.ReadSearchRoiUnitCnt();
            DEF.ReadMapBlockPickUp();
            DEF.ReadTrayPlace();
            DEF.ChangeVisionRecipe();
            SetSplashStatus(90, "READ SEARCH ROI AND RECIPE CHANGE FLOG"); //SUCCESS EVENT
            UTIL_.DELAY(100);
            TmrMAIN.Enabled = true;
            DATA_.mNotTeachSave = false;
            DATA_.TW_TIME = Environment.TickCount + ((long)DATA_.prMACHINE[CP.BlinkTime]);
            LogWR_.SaveLogOperate("PROGRAM START", "MC");

            DATA_.IsAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            lbVersion.ForeColor = DATA_.IsAdmin ? Color.FromArgb(255, 192, 192) : Color.FromArgb(192, 255, 192);
            #endregion

            DATA_.cPM.Open((int)DATA_.prMACHINE[CP.PowerMeterComPort], Baudrate.bps19200);
            DATA_.cLightController.OPEN((short)DATA_.prMACHINE[CP.LightComPort]);
            SUBFRM_.gRFID.Conntect();
            //SUBFRM_.cBarcode.Conect();

            F.Auto.bMC_DISPLY = true;
           
            O.POWER(stBIT.ON);
            P.GetHandlerPkZSafetyPos();
            
            MsSQL.Open(MsSQL.sIP, MsSQL.sDBName, MsSQL.sID, MsSQL.sPwd);

            //잠시
            CLOT.GET_LOT.LotID = UTIL_.GET_LOT_ID_INI(); //UTIL_.GET_LOT_ID(); 
            CLOT.GET_LOT.ItsID = UTIL_.GET_ITS_ID();
            BASE.RD_LOT_INF();


            LAB_.BIT_OUT(O.PC_POWER_LAMP, true);
            DATA_.mOUT[O.HANDLER_READY] = true;
            DATA_.mOUT[O.BRUSH_WATER] = true;

            DATA_.prMACHINE[CP.DBReadMode] = UTIL_.GetSqlReading();

            SetSplashStatus(100, "SUCCESS EVENT");
            t.Abort();

            LogWR_.SaveLogOperate("PROGRAM START", "MC");
            TmrMAIN.Enabled = true;
        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e){
            if (DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.INITIAL || DATA_.bMF) return;
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e){
            if (DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.INITIAL){
                e.Cancel = true;
                return;
            }
            if (DATA_.eLoginLevel < eLogLevel.ENG && !DATA_.IsBIT[B.ChkEXE]){
                e.Cancel = true;
                UTIL_.PRINT_MASSAGE("You must be logged in to exit !" + ETC.CrLf + "(종료하려면 LOG IN을 하세요 !)", false, true, true);
                return;
            }

            if (!DATA_.IsBIT[B.ChkEXE]){
                if (!UTIL_.PRINT_MASSAGE("Exit PROGRAM ?" + ETC.NewLine + "(프로그램 종료 하시겠습니까 ?)", false, false, false)){
                    e.Cancel = true;
                    return;
                }
                BASE.WR_LOT_INF();
                UTIL_.SAVE_WORKED_LOT_INFO();

                //Base.SAVE_CURRENT_COUNT();
                //LogWR_.SaveLogOperate("PROGRAM EXIT", "MC");
                //
                //LAB_.ALL_SVOFF();
                //COM_.RESET_DOORLOCK();
                //SUBFRM_.gSecsGem.SetHMI("0");
                //
                #region "EVENT CLOSE"
                SUBFRM_.gTenkeyList.SEND_TENKEY -= new EventHandler_(GET_TENKEY);
                SUBFRM_.gLogin.EVENT_OPEN -= new EventHandler_OPEN(OPEN_LOGIN);
                //
                //F.fPara.EVENT_PK -= new EventHandler_SETTING_PK(Refresh_PK);
                //F.fRecipe.EVENT_SAVE -= new EventHandler_DEVICE_SAVE(SaveDevice);
                F.Device.EVENT_OPEN -= new EventHandler_DEVICE_OPEN(OpenDevice);
                //F.fRecipe.EVENT_LANGUAGE -= new EventHandler_LANGUAGE(SET_LANGUAGE);
                //F.fMT.EVENT_OPEN -= new EventHandler_DEVICE_OPEN(OpenDevice);
                //F.fLOTID.EVENT_OPEN -= new EventHandler_DEVICE_OPEN(OpenDevice);
#endregion "EVENT CLOSE"

                SUBFRM_.cBarcode.Disconnect();
                DATA_.cPM.Close();
                DATA_.cLightController.CLOSE();
                SUBFRM_.gRFID.DisConntect();
                MsSQL.Close();

                mmTimer.Stop();
                UTIL_.DELAY(100);
                //TEACH_.SAVE_COUNT((int)DATA_.IsLONG[L.EMPTY_TRAY_CNT], (int)DATA_.IsLONG[L.LD_CNT], (int)DATA_.IsLONG[L.GOOD_TRAY_CNT], (int)DATA_.IsLONG[L.GODD_CNT], (int)DATA_.IsLONG[L.REWORK_TRAY_CNT], (int)DATA_.IsLONG[L.REWORK_CNT], (int)DATA_.IsLONG[L.NG_CNT], (int)DATA_.IsLONG[L.LOT_CNT], (int)DATA_.IsLONG[L.IN_CNT], (int)DATA_.IsLONG[L.OUT_CNT], (int)DATA_.IsLONG[L.TOTAL_CNT]);
                for (int i = 0; i < CNT_.THREAD; i++){
                    if (DATA_.mcTH[i] == null) continue;
                    try{
                        DATA_.mcTH[i].Abort();

                        //// 검증 필요 
                        //if (DATA_.mcTH[i].IsAlive) {
                        //    if ((DATA_.mcTH[i].ThreadState & ThreadState.AbortRequested) == 0) {
                        //        DATA_.mcTH[i].Abort();
                        //        DATA_.mcTH[i].Join(1000);
                        //    }
                        //}
                        //// 검증 필요
                    }
                    catch (Exception ex){
                        LogWR_.SaveLogException("MAIN_FORM->FrmMain_FormClosing", ex);
                        MessageBox.Show("EXIT_CLICK -> TRHEAD ABORT FAIL !" + ETC.NewLine + ex.ToString());
                        UTIL_.KillProgram(PATH_.EXE_NAME);
                    }
                    DATA_.mcTH[i] = null;
                }
                UTIL_.KillProgram(PATH_.EXE_NAME);
            }
        }

        private void TmrMAIN_Tick(object sender, EventArgs e){
            TmrMAIN.Enabled = false;
            Invoke();
            TmrMAIN.Enabled = true;
        }
        void Invoke(){
            TopPanel();
            ViewOPPanel();
            I.ScenCheckSensing();
            O.GetTowerLamp();
            pblTowerR.BackColor = DATA_.mOUT[O.TOWER_RED] ? Color.Red : Color.Maroon;
            pnlTowerY.BackColor = DATA_.mOUT[O.TOWER_YELLOW] ? Color.Yellow : Color.DarkGoldenrod;
            pnlTowerG.BackColor = DATA_.mOUT[O.TOWER_GREEN] ? Color.Lime : Color.DarkGreen;

            if (DATA_.IsBIT[B.JobMiss]){ // 잡파일 확인
                DATA_.IsBIT[B.JobMiss] = false;
                UTIL_.PRINT_MASSAGE("PLEASE OPEN JOB !" + ETC.NewLine + "존재하지 않는 JOB 파일입니다. OPEN JOB 하세요.", false, true, false);
            }

            if (DATA_.bViewConfirm){ // 경고 메세지 처리
                DATA_.bViewConfirm = false;
                SUBFRM_.gConf.Show();
                SUBFRM_.gConf.SET_ACTION();
            }

            if (!DATA_.bViewConfirm && DATA_.ConfirmG.useable && !SUBFRM_.gConf.meLive) DATA_.bViewConfirm = true;

            if (DATA_.bViewInitialStatus){ // 이니셜 상태 창 보기
                DATA_.bViewInitialStatus = false;
                SUBFRM_.gIni.Show();
                SUBFRM_.gIni.SetBounds(1, 1, SUBFRM_.gIni.Width, SUBFRM_.gIni.Height);
            }

            if (DATA_.bEndInitial){
                DATA_.bEndInitial = false;
                W.ViewWarning(-1, W.EndInitial);
            }

            nERR = E.CHK_ERR();
            if (nERR > -1){
                F.Auto.tclAUTOVIEW.SelectedIndex = 1;
                editErrCode.Text = nERR.ToString();
                editErrName.Text = E.GET_ERROR_NAME(nERR);
                DATA_.editErrName = editErrName.Text;
                DATA_.editErrTitle_1 = E.GET_ERROR_TITLE_1(nERR);
                DATA_.editErrTitle_2 = E.GET_ERROR_TITLE_2(nERR);

                if (!DATA_.bOnERROR) SUBFRM_.gErrPopUp.VIEW_ERROR_POPUP();
                DATA_.bOnERROR = true;
            }
            else{
                editErrCode.Text = "";
                editErrName.Text = "NONE";
                DATA_.editErrName = "NONE";
                DATA_.editErrTitle_1 = "NONE";
                DATA_.editErrTitle_2 = "NONE";

                if (F.Auto.bEES_DISPLY) F.Auto.tclAUTOVIEW.SelectedIndex = 2;
                else if (F.Auto.bEES_FDC) F.Auto.tclAUTOVIEW.SelectedIndex = 3;
                else{
                    F.Auto.bEES_DISPLY  = false;
                    F.Auto.bEES_FDC     = false;
                    F.Auto.bMC_DISPLY   = true;
                    F.Auto.tclAUTOVIEW.SelectedIndex = 0;
                }
            }

            if (DATA_.prMACHINE[CP.RunRate] > 80) lbSpeed.BackColor = Color.Red;
            else if (DATA_.prMACHINE[CP.RunRate] < 40) lbSpeed.BackColor = Color.Gray;
            else lbSpeed.BackColor = Color.Green;

            for (int i = 0; i < CNT_.THREAD; i++) COM_.GetInfoThread(i);
            I.CHK_EMO();
            I.CHK_AIR();
            I.CHK_TRIP();

            PALLET.Text = ((eMAP_BLOCK)((int)DATA_.prMACHINE[CP.SelectStage])).ToString();
            HEAD.Text = ((eHD)((int)DATA_.prMACHINE[CP.SelectHead])).ToString();
            UNLOADING.Text = ((eULD_TRAY)((int)DATA_.prMACHINE[CP.TrayUnloadingMode])).ToString();

            Blink();
            if (bChageLabel){
                bChageLabel = false;
                DEF.ReadName();
            }

#if _NSS3300
            if (DATA_.mIN[I.POWER_ON]){
                DATA_.IsLONG[L.PowerSwitchOnDelayTime]++;
                if (DATA_.IsLONG[L.PowerSwitchOnDelayTime] > 10){
                    DATA_.IsLONG[L.PowerSwitchOnDelayTime] = 0;
                    DEF.SevoPower(stBIT.ON);
                }
            }
            else DATA_.IsLONG[L.PowerSwitchOnDelayTime] = 0;

            if (DATA_.mIN[I.POWER_OFF]){
                DATA_.IsLONG[L.PowerSwitchOffDelayTime]++;
                if (DATA_.IsLONG[L.PowerSwitchOffDelayTime] > 20){
                    DATA_.IsLONG[L.PowerSwitchOffDelayTime] = 0;
                    DEF.SevoPower(stBIT.OFF);
                }
            }
            else DATA_.IsLONG[L.PowerSwitchOffDelayTime] = 0;
#else
            if (DATA_.prMODEL[RP.PCB_TYPE] == (int)ePCB.STRIP) DATA_.mOUT[O.QUAD_PCB] = false;
            else DATA_.mOUT[O.QUAD_PCB] = true;
#endif
            //SkipDoorLock
            if (DATA_.eLoginLevel >= eLogLevel.ADMIN){}
            else DATA_.IsBIT[B.SkipDoorLock] = false;
        }
        void Blink(){
            if (DATA_.bDRYRUN){
                if (isDryOff > 5){
                    if (isDryOn > 5){
                        isDryOn = 0;
                        isDryOff = 0;
                    }
                    else{
                        lbDryRunStatus.BackColor = Color.Red;
                        isDryOn++;
                    }
                }
                else{
                    lbDryRunStatus.BackColor = Color.White;
                    isDryOff++;
                }
            }
            else lbDryRunStatus.BackColor = Color.White;
        }

        private void SPC_Tick(object sender, EventArgs e){
            SPC.Enabled = false;
            lbTime.Text = DateTime.Now.ToString("yy.MM.dd HH:mm:ss");
            DATA_.cSPC.RUN_SPC();
            if (DATA_.TW_TIME >= DATA_.prMACHINE[CP.BlinkTime]){
                DATA_.TW_TIME = 0;
                DATA_.mCheckFlag = !DATA_.mCheckFlag;
            }
            else DATA_.TW_TIME++;

            if (BeforTime != DateTime.Now.Hour && DateTime.Now.Hour == DATA_.START_HOUR){
                //DEF.CountReset(false);
            }
            BeforTime = DateTime.Now.Hour;
            
            if (DATA_.mIN[I.SAW_READY]){
                if (nSendPVIDCont > 60){
                    nSendPVIDCont = 0;
                    DEF.CurDataFDC();
                    C.SendSaw.SEND("GET_PVID,*");
                }
                else nSendPVIDCont++;
            }

            if (DATA_.mOUT[O.HANDLER_BLADE_CHANGE_RESET]){
                nBLADE_CHANGE_RESET++;
                if (nBLADE_CHANGE_RESET > 5){
                    DATA_.mOUT[O.HANDLER_BLADE_CHANGE_RESET] = false;
                }
            }
            else nBLADE_CHANGE_RESET = 0;
            SPC.Enabled = true;
        }
    }
}