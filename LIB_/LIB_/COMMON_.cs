using LIB_.DateType;
using Object;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

public class COM_ : DATA_
{
    public static void SetDoubleBuffered(Control control){
        // set instance non-public property with name "DoubleBuffered" to true
        typeof(Control).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, control, new object[] { true });
    }

    public static bool ResetDay(){
        bool bRESULT = true;
        if (iDay != DateTime.Now.Day) bRESULT = false; // 날짜 바뀜
        iDay = DateTime.Now.Day;
        return bRESULT;
    }

    public static void SetFrame(GroupBox g, bool b, int x, int y, int w, int h){
        g.Visible = b;
        g.Location = new Point(x, y);
        g.Size = new Size(w, h);
        g.BringToFront();
    }

    public static void CheckDevice(ref string GetGroup, ref string GetLastDivce){
        GetGroup        = string.Empty;
        GetLastDivce    = string.Empty;
        if (sCurrJobName == null || sCurrJobName == "") return;
        string[] ArrPathCurJog  = sCurrJobName.Split('\\');
        GetGroup                = ArrPathCurJog[6];
        GetLastDivce            = ArrPathCurJog[7];
    }

    public static void SetDetailedInfomation(){
        CMES.SELECT_PPID    = CMES.CurPPID;
        CMES.SELECT_GROUP   = CMES.CUR_GROUP;
        CMES.SELECT_DEVICE  = CMES.CUR_DEVICE;
        CMES.SELECT_VISION  = CMES.CUR_VISION;
        CMES.SELECT_SAW     = CMES.CUR_SAW;
    }

    public static bool CheckSomeGroupName(string SelectGroupName){
        string FullName;
        string GroupName;
        DirectoryInfo di    = new DirectoryInfo(PATH_.DATA);
        if (di.Exists){
            DirectoryInfo[] gInfo = di.GetDirectories("*", SearchOption.AllDirectories);
            foreach (DirectoryInfo fn in gInfo)
            {
                FullName = di.FullName;
                string[] sArr = FullName.Split('\\');
                int idx = sArr.Length - 1;
                GroupName = sArr[idx].Trim();
                if (SelectGroupName == GroupName) return false;
            }
        }
        return true;
    }
    public static bool CheckSomeRecipeName(string Recipe, string NewRecipe){
        string[] s = Directory.GetFiles(PATH_.DATA + Recipe);
        for (int i = 0; i < s.Length - 1; i++){
            string[] sArr   = s[i].Split('\\');
            int idx         = sArr.Length - 1;
            string sTemp    = sArr[idx].Replace(".jog", "");
            if (NewRecipe == sTemp) return false;
        }
        return true;
    }

    public static void MakePickerVac(DataGridView dgv, int RowCnt){
        string[] sLabel                 = new string[] { "INDEX", "CUR", "SET" };
        dgv.DefaultCellStyle.Alignment  = DataGridViewContentAlignment.MiddleCenter;
        dgv.RowCount                    = RowCnt;
        dgv.DefaultCellStyle.ForeColor  = Color.Black;
        dgv.DefaultCellStyle.BackColor  = Color.White;
        for (int i = 0; i < 3; i++) { 
            dgv[i, 0].Value = sLabel[i]; 
        }
    }

    public static void MakePickerOffset(DataGridView dgv, int rowcnt){
        string[] mStrPkrOffset          = new string[] { "DIR", "PK 1", "PK 2", "PK 3", "PK 4", "PK 5", "PK 6", "PK 7", "PK 8" };
        dgv.DefaultCellStyle.Alignment  = DataGridViewContentAlignment.MiddleCenter;
        dgv.RowCount                    = rowcnt;
        dgv.DefaultCellStyle.ForeColor  = Color.Black;
        dgv.DefaultCellStyle.BackColor  = Color.White;
        for (int i = 0; i < 9; i++) { 
            dgv[i, 0].Value = mStrPkrOffset[i]; 
        }
    }

    public static void ViewMotorSelect(){
        SUBFRM_.gMTSelect.Width     = 450;
        SUBFRM_.gMTSelect.Height    = 530;
        SUBFRM_.gMTSelect.Show();
        SUBFRM_.gMTSelect.Left = 0;
        SUBFRM_.gMTSelect.BringToFront();
        SUBFRM_.gMTSelect.tmrMTSELECT.Enabled = true;
        mCurTeachMotor = -1;
    }

    public static void SetGridData(DataGridView dgv, int mt){
        for (int i = 0; i < dgv.RowCount; i++){
            dgv.Rows[i].Cells[2].Value = mtDATA[mt, int.Parse(dgv.Rows[i].Cells[0].Value.ToString())].Pos;
        }
    }
    public static void SetGridData(DataGridView dgv, int mt1, int mt2){
        for (int i = 0; i < dgv.RowCount; i++){
            dgv.Rows[i].Cells[2].Value = mtDATA[mt1, int.Parse(dgv.Rows[i].Cells[0].Value.ToString())].Pos;
            dgv.Rows[i].Cells[3].Value = mtDATA[mt2, int.Parse(dgv.Rows[i].Cells[0].Value.ToString())].Pos;
        }
    }

    public static void SetGridData(DataGridView dgv, int row, int mt, int index){
        dgv[2 + index, row].Value = LAB_.GET_ACTPOS(mt);
    }

    public static bool NotCheckThread(int nTH){
        if (thNotSeqThread == null) return false;
        for (int i = 0; i < thNotSeqThread.Length; i++){
            if (nTH == thNotSeqThread[i]) return true;
        }
        return false;
    }

    public static bool CheckSeqThread(int nTh){
        if (thSeqThrad == null) return false;
        for (int i = 0; i < thSeqThrad.Length; i++){
            if (nTh == thSeqThrad[i]) return true;
        }
        return false;
    }

    public static void GetInfoThread(int nTHEAD){
        int i = nTHEAD;
        LogThread[i].USE = UseThread[i];
        if (GetThreadState(nTHEAD, ThreadState.Aborted))            LogThread[i].thSTS = "Aborted";
        if (GetThreadState(nTHEAD, ThreadState.AbortRequested))     LogThread[i].thSTS = "AbortRequested";
        if (GetThreadState(nTHEAD, ThreadState.Background))         LogThread[i].thSTS = "Background";
        if (GetThreadState(nTHEAD, ThreadState.Running))            LogThread[i].thSTS = "Running";
        if (GetThreadState(nTHEAD, ThreadState.Stopped))            LogThread[i].thSTS = "Stopped";
        if (GetThreadState(nTHEAD, ThreadState.StopRequested))      LogThread[i].thSTS = "StopRequested";
        if (GetThreadState(nTHEAD, ThreadState.Suspended))          LogThread[i].thSTS = "Suspended";
        if (GetThreadState(nTHEAD, ThreadState.SuspendRequested))   LogThread[i].thSTS = "SuspendRequested";
        if (GetThreadState(nTHEAD, ThreadState.Unstarted))          LogThread[i].thSTS = "Unstarted";
        if (GetThreadState(nTHEAD, ThreadState.WaitSleepJoin))      LogThread[i].thSTS = "WaitSleepJoin";
    }

    public static void RUN_THREAD(ref Thread t, ThreadStart ts){
        t = new Thread(new ThreadStart(ts));
        //t.IsBackground = true;
        t.Start();
    }
    public static void MAKE_THRAED(ref Thread t, ThreadStart ts){
        t = new Thread(new ThreadStart(ts));
        //t.IsBackground = true;
    }
    public static bool RECREATE_THREAD(ref Thread t, ThreadStart ts){
        if (t != null) t.Abort(); //삭제후 다시
        t = new Thread(new ThreadStart(ts));
        //t.IsBackground = true;
        t.Start();
        return true;
    }
    public static bool GetThreadState(int t, ThreadState thState){
        if (mcTH[t] == null) return false;
        if ((mcTH[t].ThreadState & thState) == thState) return true;
        return false;
    }

    public static bool IsNotEncMotor(int m){
        if (mtNotEncoder == null) return false;
        for (int i = 0; i < mtNotEncoder.Length; i++) { 
            if (m == mtNotEncoder[i]) return true; 
        }
        return false;
    }
    public static bool IsNotMotor(int m){
        if (mtSPARE == null) return false;
        for (int i = 0; i < mtSPARE.Length; i++) { 
            if (m == mtSPARE[i]) return true; 
        }
        return false;
    }

    public static bool CurrentLocationHome(int m){
        bool bFLAG = false;
        if (mtCurrentHome == null) return false;
        for (int i = 0; i < mtCurrentHome.Length; i++){
            if (m == mtCurrentHome[i]){
                bFLAG = true;
                break;
            }
        }
        if (!bFLAG) return false;
        LAB_.SET_POSZERO(m, 10);
        mtSTS[m].bHomeComplete = true;
        return true;
    }

    public static bool MTCEP(int m, int p){
        if (mtSTS[m].bAlram)    return false;
        if (bBD)                return true;
        if (mtSTS[m].CurrentPosition != 0 && Math.Abs(mtSTS[m].CurrentPosition - mtDATA[m, p].Pos) < 0.1)   return true;
        else                                                                                                return false;
    }

    public static void SetWarnning(int iThread, int nWarnning, bool bState, string comment){
        LogWR_.SaveMarsLog(iThread, eLogTYPE.FNC, comment, "WAR");
        ConfirmUser[nWarnning].useable = bState;
    }
    public static void ViewWarning(int iThread, int num){
        ConfirmUser[num].useable    = true;
        bWF                         = true;
        if (iThread > 0) LogWR_.SaveMarsLog(iThread, eLogTYPE.FNC, ConfirmUser[num].msg, "WARNNING");
    }
    public static void ViewWarning(int iThread, int num, string msg){
        ConfirmUser[num].msg        = msg;
        ConfirmUser[num].useable    = true;
        bWF                         = true;
        LogWR_.SaveMarsLog(iThread, eLogTYPE.FNC, ConfirmUser[num].msg, "WARNNING");
    }

    public static void SetBit(int iThread, int nBit, bool bState, string comment){
        if (bMF) return;
        IsBIT[nBit] = bState;
        LogWR_.SaveMarsLog(iThread, eLogTYPE.FNC, comment + "_" + mBName[nBit] + " = " + IsBIT[nBit].ToString(), "BIT");
    }
    public static void SetBit(int iThread, int nBit1, int nBit2, bool bState1, bool bState2, string comment){
        if (bMF) return;
        LogWR_.SaveMarsLog(iThread, eLogTYPE.FNC, comment, "BIT");
        IsBIT[nBit1] = bState1;
        IsBIT[nBit2] = bState2;
    }
    public static void SetOutput(int iThread, short nOut, bool bState, string comment){
        LogWR_.SaveMarsLog(iThread, eLogTYPE.FNC, comment, "OUT");
        LAB_.BIT_OUT(nOut, bState);
    }

    public static void RUN_MANUAL(int num, string sMassage){
        if (bMF) return;
        iMANUAL.Number  = num;
        iMANUAL.CMD     = sMassage;
        bMF             = true;
    }
    public static bool RUN_MANUAL(int num, string sMassage, bool bManualView){
        if (bManualView) { 
            if (!UTIL_.PRINT_MASSAGE("[ " + sMassage + " ] Excute Manual Axtion ? ", false, false, false)) return false;
        }
        iMANUAL.Number  = num;
        iMANUAL.Label   = sMassage;
        try{
            SUBFRM_.gManualRepeat.lbManualName.Text = sMassage;
            RUN_MANUAL(num, sMassage);
        }
        catch (Exception ex){
            MessageBox.Show("Manual Running Fail !" + ETC.NewLine + ex.ToString());
            LogWR_.SaveLogException("RunManaul_Click Fail [" + num + "]", ex);
        }
        return true;
    }

    public static bool[] arrDoorINPUT;
    public static bool[] arrDoorOUTPUT;
    public static bool[] arrAearSENSOR;
    public static bool[] arrEMO;
    public static bool[] arrTRIP;
    public static bool[] arrAIR;
    public static void INI_OnlyCheckArray(){
        if (iDOOR != null)  Array.Resize(ref arrDoorINPUT, iDOOR.Length);
        if (oDOOR != null)  Array.Resize(ref arrDoorOUTPUT, oDOOR.Length);
        if (iAEAR != null)  Array.Resize(ref arrAearSENSOR, iAEAR.Length);
        if (iEMO != null)   Array.Resize(ref arrEMO, iEMO.Length);
        if (iTRIP != null)  Array.Resize(ref arrTRIP, iTRIP.Length);
        if (iAIR != null)   Array.Resize(ref arrAIR, iAIR.Length);
    }

    public static void ScenCheckSensing(){
        if (bBD) return;
        int iNUM;
        eRTN eRETURN = eRTN.NULL;

        if (iDOOR != null){
            for (int i = 0; i < iDOOR.Length; i++){
                iNUM                = iDOOR[i];
                arrDoorINPUT[i]     = LAB_.GET_INPUT(iNUM, eRETURN);
            }
        }
        if (iAEAR != null){
            for (int i = 0; i < iAEAR.Length; i++){
                iNUM                = iAEAR[i];
                arrAearSENSOR[i]    = LAB_.GET_INPUT(iNUM, eRETURN);
            }
        }
        if (iEMO != null){
            for (int i = 0; i < iEMO.Length; i++){
                iNUM                = iEMO[i];
                arrEMO[i]           = LAB_.GET_INPUT(iNUM, eRETURN);
            }
        }
        if (iTRIP != null){
            for (int i = 0; i < iTRIP.Length; i++){
                iNUM                = iTRIP[i];
                arrTRIP[i]          = LAB_.GET_INPUT(iNUM, eRETURN);
            }
        }
        if (iAIR != null){
            for (int i = 0; i < iAIR.Length; i++){
                iNUM                = iAIR[i];
                arrAIR[i]           = LAB_.GET_INPUT(iNUM, eRETURN);
            }
        }
    }

    public static bool CHK_AEAR(){
        if (bBD) return true;
        for (int i = 0; i < iAEAR.Length; i++){
            if (!arrAearSENSOR[i]){
                LAB_.MT_ALL_STOP(true, "COMMON_->CHK_ARAR" + ETC.NewLine + "AEAR. PUCH");
                UTIL_.OnERROR(eAEAR[i], 50);
                return false;
            }
        }
        return true;
    }
    public static bool CHK_EMO(){
        if (bBD || iEMO == null) return true;
        for (int i = 0; i < iEMO.Length; i++){
            if (!arrEMO[i]){
                LAB_.MT_ALL_STOP(true, "COMMON_->CHK_EMO" + ETC.NewLine + "EMO. PUCH");
                UTIL_.OnERROR(eEMO[i], 50);
                return false;
            }
        }
        return true;
    }
    public static bool CHK_TRIP(){
        if (bBD || mTRIP_SKIP || iTRIP == null) return true;
        for (int i = 0; i < iTRIP.Length; i++){
            if (!arrTRIP[i]){
                LAB_.MT_ALL_STOP(true, "COMMON_->CHK_TRIP" + ETC.NewLine + "TRIP. PUCH");
                UTIL_.OnERROR(eTRIP[i], 50);
                return false;
            }
        }
        return true;
    }
    public static bool CHK_AIR(){
        if (bBD || mAIR_SKIP || (iAIR == null)) return true;
        for (int i = 0; i < iAIR.Length; i++){
            if (!arrAIR[i]){
                LAB_.MT_ALL_STOP(true, "COMMON_ -> CHK_AIR" + ETC.NewLine + "AIR. PUCH");
                UTIL_.OnERROR(eAIR[i], 50);
                return false;
            }
        }
        return true;
    }
    public static bool CHK_DOOR(){
        if (mDOOR_SKIP || bBD){
            //mDOOR_SKIP = false;
            return true;
        }
        if (arrDoorINPUT == null) return true;
        for (int i = 0; i < arrDoorINPUT.Length; i++){
            if (!mDOOR_LOCK) break;
            if (!arrDoorINPUT[i]){
                WarMESSAGE = InputName[iDOOR[i]].ToString();
                mDOOR_OPEN = true;
                sWarnningMessage = "DOOR CHECK FAIL" + ETC.NewLine + WarMESSAGE;
                return false;
            }
        }
        //mDOOR_SKIP = false;
        return true;
    }

    public static void SET_DOORLOCK(){
        if (arrDoorOUTPUT == null) return;
        for (int i = 0; i < arrDoorOUTPUT.Length; i++) mOUT[oDOOR[i]] = true;
        UTIL_.DELAY(500);
        mDOOR_LOCK = true;
    }

    public static void RESET_DOORLOCK(){
        if (arrDoorOUTPUT == null) return;
        for (int i = 0; i < arrDoorOUTPUT.Length; i++) mOUT[oDOOR[i]] = false;
        mDOOR_LOCK = false;
    }

    public static void STOP_ACMOTOR(){
        if (oACMT == null) return;
        for (int i = 0; i < oACMT.Length; i++) mOUT[oACMT[i]] = false;
    }

    public static void BZ_OFF(){
        if (oBZ == null) return;
        for (int i = 0; i < oBZ.Length; i++) { mOUT[oBZ[i]] = false; }
    }

    public static bool CurPkPosition(int mt, int pos){
        if (2 < Math.Abs(DATA_.mtSTS[mt].CurrentPosition - DATA_.mtDATA[mt, pos].Pos)) return false;
        return true;
    }

    public static void LabelDEFINE_MOTION_ERR()
    {
        eMTEnd = CNT_.MT * eMTGap;
        string[] sMT = { "CW LIMIT", "CCW LIMIT", "NOT HOME", "CW+ SOFT LIMIT POSITION",
                           "CCW- SOFT LIMIT POSITION", "MOVING TIME OVER (CMD/ACT NotSame)", "NOT SERVO ON", "SERVO ALARM", "MOVING", "INITIALIZE FAIL"};

        for (int m = 0; m < eMTEnd; m++){
            int MtNUM   = (m - eMTBegin) / 10;
            int ErrNUM  = (m - eMTBegin) % 10;
            ErrName[m]  = "[" + MtName[MtNUM] + "] " + sMT[ErrNUM] + " ERROR";
        }

        for (int e = 0; e < CNT_.ERR; e++){
            if (e >= eErrBegin) ErrName[e] = FILE_.RDString(PATH_.ErrDEFINE, "ERROR DEFINE", "NAME_" + e.ToString(), "");
            ErrINFO[e].enRec    = FILE_.RDBool(PATH_.ErrDEFINE, "ERROR DEFINE", "EN_" + e.ToString(), true);
            ErrINFO[e].kind     = FILE_.RDInt(PATH_.ErrDEFINE, "ERROR DEFINE", "KINK_" + e.ToString(), 0);
            int rstLevel        = FILE_.RDInt(PATH_.ErrDEFINE, "ERROR DEFINE", "LEVEL_" + e.ToString(), 0);
            ErrINFO[e].rstLevel = (eLogLevel)rstLevel;
        }
    } //모션 에러 리스트

    public static void RunRepaeatManual(int n1, int n2){
        RUN_MANUAL(n1, iMANUAL.Label);
        UTIL_.DELAY((int)IsDOUBLE[MANUAL_REPEAT_DLAY]);
        RUN_MANUAL(n2, iMANUAL.Label);
        UTIL_.DELAY((int)IsDOUBLE[MANUAL_REPEAT_DLAY]);
    }


    public static void GetTowerLamp(){
        int nFLAG = 0; //0:RUN, 1:STOP, 2:INI, 3:WAITRUN, 4:ERROR, 5:MANUAL-RUN, 6:LOT-END, 7:WARNNIG
        bool bRES = false;

        if (eMCStatus == eMachineStatus.AUTO && !bLotEnd && !bWF)                                                                       nFLAG = 0;
        else if (eMCStatus == eMachineStatus.USERSTOP || eMCStatus == eMachineStatus.NONE)                                              nFLAG = 1;
        else if (eMCStatus == eMachineStatus.INITIAL)                                                                                   nFLAG = 2;
        else if (eMCStatus == eMachineStatus.WAITRUN && !bMF && !bWF)                                                                   nFLAG = 3;
        else if (eMCStatus == eMachineStatus.EMSSTOP || eMCStatus == eMachineStatus.ERRSTOP || eMCStatus == eMachineStatus.READYSTOP)   nFLAG = 4;
        else if (bMF)                                                                                                                   nFLAG = 5;
        else if (bLotEnd)                                                                                                               nFLAG = 6;
        else if (bWF)                                                                                                                   nFLAG = 7;

        for (int i = 0; i < CNT_.TowerLamp; i++){
            if (BackupTowerStatus[nFLAG] == null) continue;
            switch (Convert.ToInt16(BackupTowerStatus[nFLAG].Substring(i, 1))){
                case 0:
                    bRES = true;
                    break;
                case 1:
                    bRES = mCheckFlag;
                    break;
                case 2:
                    bRES = false;
                    break;
            }
            switch (i){
                case 0:
                    TW_RED = bRES;
                    break;
                case 1:
                    TW_YELLOW = bRES;
                    break;
                case 2:
                    TW_GREEN = bRES;
                    break;
                case 3:
                    TW_BLUE = bRES;
                    break;
            }
        }
        SetTowerLamp(TW_RED, TW_YELLOW, TW_GREEN, TW_BLUE);
    }
    static void SetTowerLamp(bool bRed, bool bYellow, bool bGreen, bool bBlue){
        if (TOWER_RED >= 0)     mOUT[TOWER_RED] = bRed;
        if (TOWER_YELLOW >= 0)  mOUT[TOWER_YELLOW] = bYellow;
        if (TOWER_GREEN >= 0)   mOUT[TOWER_GREEN] = bGreen;
        if (TOWER_BLUE >= 0)    mOUT[TOWER_BLUE] = bBlue;
    }
}