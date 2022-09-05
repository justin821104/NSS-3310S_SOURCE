using Object;
using System;
using System.IO;

public class LogWR_ : DATA_
{
    public delegate void procAddMsgEvent(int nThread, string sMsg);
    public static event procAddMsgEvent ProMsgEvent;
    public static void AddMessage(int iThread, string message){
        ProMsgEvent?.Invoke(iThread, message);
        //if (ProMsgEvent != null)
        //    ProMsgEvent(iThread, message);
    }

    public static string[] arrMonth = new string[24];

    public static void DEBUG_PRINT(string msg){
        string sNOW = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");
        System.Diagnostics.Debug.WriteLine("[DEBUG] " + sNOW + " -> " + msg);
    }
    public static void LOG_CLEAR(){
        mLOG.sException     = string.Empty;
        mLOG.sWarnMsg       = string.Empty;
        mLOG.sSystem        = string.Empty;
        mLOG.sParaEvent     = string.Empty;
        mLOG.sOperate       = string.Empty;
        mLOG.sManual        = string.Empty;
        mLOG.sMars          = string.Empty;
        mLOG.sLogin         = string.Empty;
        mLOG.sMES           = string.Empty;
        mLOG.sMeasure       = string.Empty;
        mLOG.sProcessInfo   = string.Empty;
        mLOG.sCOUNT         = string.Empty;
        mLOG.sPROCESS       = string.Empty;
        mLOG.sTACK          = string.Empty;
        mLOG.sLOT           = string.Empty;
        mLOG.sOneCycleTime  = string.Empty;
    }

    public static void SAVE_LOG(){
        string fn = GET_DirNameDate(PATH_.LogEXCEPTION) + "EXCEPTION.log";
        LOG_WRITEFILE(fn, ref mLOG.sException);

        fn = GET_DirNameDate(PATH_.LogWARNING) + "WARNING.log";
        LOG_WRITEFILE(fn, ref mLOG.sWarnMsg);

        fn = GET_DirNameDate(PATH_.LogSYSTEM) + "SYSTEM.log";
        LOG_WRITEFILE(fn, ref mLOG.sSystem);

        fn = GET_DirNameDate(PATH_.LogEVENT) + "EVENT.log";
        LOG_WRITEFILE(fn, ref mLOG.sParaEvent);

        fn = GET_DirNameDate(PATH_.LogAppEVENT) + "MACHINE_EVENT.log";
        LOG_WRITEFILE(fn, ref mLOG.sOperate);

        fn = GET_DirNameDate(PATH_.LogProcMANUAL) + "MANUAL.log";
        LOG_WRITEFILE(fn, ref mLOG.sManual);

        fn = GET_DirNameDate(PATH_.LogPRINTMESSAGE) + "PRINT_MASSAGE.log";
        LOG_WRITEFILE(fn, ref mLOG.sPringMsg);

        fn = GET_DirNameDate(PATH_.LogMARS) + "MARS.log";
        LOG_WRITEFILE(fn, ref mLOG.sMars);

        fn = GET_DirNameDate(PATH_.LogLOGGING) + "LOGIN.log";
        LOG_WRITEFILE(fn, ref mLOG.sLogin);

        fn = GET_DirNameDate(PATH_.LogMEASURE) + "MEASURE.log";
        LOG_WRITEFILE(fn, ref mLOG.sMeasure);

        fn = GET_DirNameDate(PATH_.LogLotEnd) + "LOT.log";
        LOG_WRITEFILE(fn, ref mLOG.sLOT);

        //fn = GET_DirNameDate(PATH.LogCount) + "COUNT.log";
        //LOG_WRITEFILE(fn, ref mLOG.sCOUNT);
        if (prMACHINE[USE_LOG_SAVE] == (int)eUSE.USE){
            fn = GET_DirNameDate(PATH_.LogPROCESS) + "PROCESS.log";
            LOG_WRITEFILE(fn, ref mLOG.sPROCESS);
        }
        else mLOG.sPROCESS = "";

        fn = GET_DirNameDate(PATH_.LogCycleTack) + "TACK.log";
        LOG_WRITEFILE(fn, ref mLOG.sTACK);

        fn = GET_DirNameDate(PATH_.LogOneCycleTime) + "TACK.log";
        LOG_WRITEFILE(fn, ref mLOG.sOneCycleTime);
    }

    static public void LOG_WRITEFILE(string fn, ref string s){
        if (s == null) return;
        if (s.Length > 10){
            FILE_.WR_File(fn, s, true);
            s = "";
        }
    }
    public static void LOG_WRITE_FILE(string fn, ref string s, bool bAppeded){
        if (s == null) return;
        if (s.Length > 4){
            FILE_.WR_File(fn, s, bAppeded);
            s = "";
        }
    }

    public static string MAKE_DATE_FOLDER(string SubFOLDER){
        string sYEAR = DateTime.Now.Year.ToString() + "\\";
        string sMONTH = DateTime.Now.Month.ToString() + "\\";
        string sDAY = DateTime.Now.Day.ToString() + "\\";

        string sFOLDER = PATH_.MCLOG + sYEAR;
        if (!Directory.Exists(sFOLDER)) Directory.CreateDirectory(sFOLDER);
        sFOLDER = PATH_.MCLOG + sYEAR + sMONTH;
        if (!Directory.Exists(sFOLDER)) Directory.CreateDirectory(sFOLDER);
        sFOLDER = PATH_.MCLOG + sYEAR + sMONTH + sDAY;
        if (!Directory.Exists(sFOLDER)) Directory.CreateDirectory(sFOLDER);
        sFOLDER = PATH_.MCLOG + sYEAR + sMONTH + sDAY + SubFOLDER; // +"\\";
        if (!Directory.Exists(sFOLDER)) Directory.CreateDirectory(sFOLDER);
        return sFOLDER;
    }

    public static void DELETE_OLD_LOGs(){
        double NowDate = DateTime.Now.Date.ToOADate();
        double OldDate = NowDate - 90;

        //' ------------------------------------- Delete MARS LOG
        int cnt = GET_YearMountFolder(ref arrMonth);
        for (int i = 0; i < cnt - 6; i++){ // 6 개월 보관
            for (int j = 0; j < PATH_.aPath_DEL_LOGs.Length - 1; j++){
                for (int k = 0; k < arrMonth.Length; k++){
                    string ts = PATH_.aPath_DEL_LOGs[j] + arrMonth[k];
                    if (arrMonth[k] == null || arrMonth[k] == "") continue;
                        if (Directory.Exists(ts)){
                        try{
                            string FilePath = ts;
                            DirectoryInfo d = new DirectoryInfo(FilePath);
                            foreach (var file in d.GetFiles("*.*")){
                                file.Delete();
                            }
                            Directory.Delete(ts);
                        }
                        catch (Exception Exp){
                            SaveLogException("[LogWR] DELETE_OLD_LOGs", Exp);
                        }
                    }
                }
            }
        }

        arrMonth = new string[24];
        cnt = GET_YearMountFolder(ref arrMonth);
        for (int i = 0; i < cnt - 1; i++){ // 1 개월 보관
            for (int j = 0; j < PATH_.sPath_DEL_LOGs_ONE_MONTH.Length; j++){
                for (int k = 0; k < arrMonth.Length; k++){
                    string ts = PATH_.sPath_DEL_LOGs_ONE_MONTH[j] + arrMonth[k];
                    if (arrMonth[k] == null || arrMonth[k] == "") continue;
                    if (Directory.Exists(ts)){
                        try{
                            string FilePath = ts;
                            DirectoryInfo d = new DirectoryInfo(FilePath);
                            foreach (var file in d.GetFiles("*.*")){
                                file.Delete();
                            }
                            Directory.Delete(ts);
                        }
                        catch (Exception Exp){
                            SaveLogException("[LogWR] DELETE_OLD_LOGs", Exp);
                        }
                    }
                }
            }
        }
    }
    public static void DELETE_LOG_FOLDERs(){
        for (int i = 2007; i < DateTime.Now.Year; i++){
            string sFOLDER = PATH_.MCLOG + i.ToString() + "\\";
            if (Directory.Exists(sFOLDER)) Directory.Delete(sFOLDER, true);
        }
    }

    public static string GET_WORK_UNIT(){
        int iHOURS = DateTime.Now.TimeOfDay.Hours;

        if (IsSHIFT == eSHIFT.ThreeSHIFT){
            if (iHOURS > 6 && iHOURS < 14)          return "A";
            else if (iHOURS > 14 && iHOURS < 20)    return "B";
            else                                    return "C";
        } //3교대조
        else if (IsSHIFT == eSHIFT.TwoSHIFT){
            if (iHOURS > 6 && iHOURS < 20)  return "A";
            else                            return "B";
        } //2교대조
        else{
            return "A";
        } //교대없음.
    }
    public static int GET_YearMountFolder(ref string[] arr){
        int iYEAR   = DateTime.Now.Year - 1;
        int iMONTH  = DateTime.Now.Month;

        for (int i = 0; i < 12; i++) arr[i] = iYEAR.ToString() + "_" + string.Format("{0:00}", i + 1);

        iYEAR = DateTime.Now.Year;
        int idx = 12;
        for (int i = 1; i < iMONTH - 2; i++){
            arr[idx] = iYEAR.ToString() + "_" + string.Format("{0:00}", i);
            idx += 1;
        }
        return idx;
    }
    public static string GET_NowDataTime(string DELI){
        string sRTN =   string.Format("{0:00}", DateTime.Now.Year) + DELI + string.Format("{0:00}", DateTime.Now.Month) + DELI
                        + string.Format("{0:00}", DateTime.Now.Day) + DELI + string.Format("{0:00}", DateTime.Now.Hour) + DELI
                        + string.Format("{0:00}", DateTime.Now.Minute) + DELI + string.Format("{0:00}", DateTime.Now.Second);
        return sRTN;
    }
    public static string GET_LOG_TAG(string sID){
        return eLoginLevel.ToString() + "," + eMCStatus.ToString() + "," + GET_WORK_UNIT() + "," + sID + "," + GET_NowDataTime(":") + ",";
    }
    public static string GET_DirNameDate(string sPreDir){
        string s = sPreDir + DateTime.Now.Year.ToString() + "_" + string.Format("{0:00}", DateTime.Now.Month) + "\\";
        if (!Directory.Exists(s)) Directory.CreateDirectory(s);
        return s + DateTime.Now.Year.ToString() + string.Format("{0:00}", DateTime.Now.Month) + string.Format("{0:00}", DateTime.Now.Day) + "_";
    }
    public static string GET_PathOperation(double dt, string preDir){
        DateTime d = DateTime.FromOADate(dt);
        string s = preDir + d.Year.ToString() + "_" + string.Format("{0:00}", d.Month) + "\\";
        return s + d.Year.ToString() + string.Format("{0:00}", d.Month) + string.Format("{0:00}", d.Day) + "_";
    }
    public static string GET_LogTime(string str){
        string s = DateTime.Now.Year.ToString() + "-" + string.Format("{0:00}", DateTime.Now.Month) + "-" + string.Format("{0:00}", DateTime.Now.Day) + ETC.cspTab;
        s += string.Format("{0:00}", DateTime.Now.Hour) + ":" + string.Format("{0:00}", DateTime.Now.Minute)
          + ":" + string.Format("{0:00}", DateTime.Now.Second) + "." + string.Format("{0:00}", DateTime.Now.Millisecond)
          + ETC.cspTab + str + ETC.NewLine;
        return s;
    }
    public static string GET_DATE(){ // format : 20180909120000
        return DateTime.Now.Year.ToString() + string.Format("{0:00}", DateTime.Now.Month) + string.Format("{0:00}", DateTime.Now.Day) + string.Format("{0:00}", DateTime.Now.Hour) + string.Format("{0:00}", DateTime.Now.Minute) + string.Format("{0:00}", DateTime.Now.Second);
    }

    public static void SAVE_ERROR_LOG(string sLOGS) { mLOG.sError += GET_LogTime(sLOGS); }

    public static void SAVE_CHANGE_DATA(bool bUSE){
        string sLog;//         = "Name:TEST" + etc.NewLine;
        string sData = string.Empty;
        string sDate;
        string sFN;
        string sSawData = string.Empty;

        if (bUSE){
            sLog = "Name:" + sJobName + ETC.NewLine;
            for (int m = 0; m < CNT_.MT; m++){
                for (int p = 0; p < CNT_.POS; p++){
                    if (MtName[m] == "" || MtName[m] == null || PosName[m, p] == "" || PosName[m, p] == null) continue;

                    sData = sData + "[" + MtName[m] + "] " + PosName[m, p] + " LOCATION :" + mtDATA[m, p].Pos.ToString() + ETC.NewLine +
                            "[" + MtName[m] + "] " + PosName[m, p] + " SPEED :" + mtDATA[m, p].Spd.ToString() + ETC.NewLine +
                            "[" + MtName[m] + "] " + PosName[m, p] + " ACCELERATE :" + mtDATA[m, p].Acc.ToString() + ETC.NewLine +
                            "[" + MtName[m] + "] " + PosName[m, p] + " DECELERATE :" + mtDATA[m, p].Dec.ToString() + ETC.NewLine +
                            "[" + MtName[m] + "] " + PosName[m, p] + " MOVE TIME :" + mtDATA[m, p].MoveTime.ToString() + ETC.NewLine;
                }
            }
            sLog += sData;
            sData = "";
            for (int i = 0; i < CNT_.MCPARA; i++){
                if (MCParaName[i] == null || MCParaName[i] == "") continue;
                sData = sData + MCParaName[i] + ":" + prMACHINE[i].ToString() + ETC.NewLine;
            }
            sLog += sData;
            sData = "";
            for (int i = 0; i < CNT_.MDLPARA; i++){
                if (MDParaName[i] == null || MDParaName[i] == "") continue;
                sData = sData + MDParaName[i] + ":" + prMODEL[i].ToString() + ETC.NewLine;
            }
            sLog += sData;
            sDate = GET_DATE();
            sFN     = PATH_.PathStanderdRecipe + sDEVICE_ID + "_" + sDate + ".txt";
            //mFILE.WR_File(sFN, sLog, false);
        }
    } // 삼성 로그 저장 내용.

    public static void SAVE_ChangeDataEvent(string sLOGS){
        bAutoBackUp = true;
        mLOG.sParaEvent += GET_LOG_TAG("") + sLOGS + ETC.NewLine;
        SAVE_CHANGE_DATA(true);
    }
    public static void SAVE_LOG_PARAMETER(){
        string sFN = GET_DirNameDate(PATH_.LogEVENT) + "PARAMETER.log";
        Log_WRITE_FILE(sFN, ref mLOG.sParaEvent);
    }

    public static void SaveLogSystem(string sLogs, string sID)          { mLOG.sSystem += GET_LOG_TAG(sID) + sLogs + ETC.NewLine; }
    public static void SaveLogOperate(string sLogs, string sID)         { mLOG.sOperate += GET_LOG_TAG(sID) + sLogs + ETC.NewLine; }
    public static void SaveLogWarning(string sLogs, string sID)         { mLOG.sWarnMsg += GET_LOG_TAG(sID) + sLogs + ETC.NewLine; }
    public static void SaveLogManual(string sLogs, string sID)          { mLOG.sManual += GET_LOG_TAG(sID) + sLogs + ETC.NewLine; }
    public static void SaveLogPrintMessage(string sLogs, string sID)    { mLOG.sPringMsg += GET_LOG_TAG(sID) + sLogs + ETC.NewLine; }
    public static void SaveLogMARS(string sLogs)                        { mLOG.sMars += GET_LogTime(sLogs); }
    public static void SaveMARS(string LOG, string SECTION, string STATE, int iTH){
        //int iMINUTE = DateTime.Now.Minute;
        if (STATE == "STOP") STATE = "END";
        string time = DateTime.Now.ToString("HH:mm:ss.fff");
        string[] arrLOG = LOG.Split('#');
        string sLOG = arrLOG[0] + ETC.cspTab + STATE;
        SaveLogMARS(sLOG);
        if (iTH > CNT_.THREAD - 1) return;
        //SaveProcLog(iTH, time, sLOG);
    }
    //public static void SaveProcLog(int ith, string time, string log){
    //    string strPath = string.Empty;
    //    string strLogWrite = string.Empty;
    //
    //}

    public static void SaveLogTack(string sLogs, string sID)    { mLOG.sTACK += GET_LOG_TAG(sID) + sLogs + ETC.NewLine; }
    public static void SaveLogOneCyle(string sLogs, string sID) { mLOG.sOneCycleTime += GET_LOG_TAG(sID) + sLogs + ETC.NewLine; }
    public static void SaveLogLogin(string sLogs, string sID)   { mLOG.sLogin += GET_LOG_TAG(sID) + sLogs + ETC.NewLine; }
    public static void SaveLogProcess(string sLogs, string sID) { mLOG.sPROCESS += GET_LOG_TAG(sID) + sLogs + ETC.NewLine; }

    public static string LogPos(int m, double pos){
        return "[" + mtSTS[m].CurrentPosition.ToString("0.000") + "->" + pos.ToString("0.000") + "]";
    }
    public static string LogPos(int m, int pn){
        return "[" + mtSTS[m].CurrentPosition.ToString("0.000") + "->" + mtDATA[m, pn].Pos.ToString("0.000") + "]";
    }
    public static string LogPos(int m, stMoveInfo mi){
        return "[" + mtSTS[m].CurrentPosition.ToString("0.000") + "->" + mi.Pos.ToString("0.000") + "]";
    }
    public static string LogPos(int[] m, int[] pn){
        string sRTN = "[";
        string sLOG;
        for (int i = 0; i < m.Length; i++){
            sLOG = mtSTS[m[i]].CurrentPosition.ToString("0.000") + "->" + mtDATA[m[i], pn[i]].Pos.ToString("0.000");
            if (i != m.Length) sLOG += "/";
        }
        sRTN += "]";
        return sRTN;
    }
    public static string LogPos(int[] m, stMoveInfo[] mi){
        string sRTN = "[";
        string sLOG;
        for (int i = 0; i < m.Length; i++){
            sLOG = mtSTS[m[i]].CurrentPosition.ToString("0.000") + "->" + mi[i].Pos.ToString("0.000");
            if (i != m.Length) sLOG += "/";
        }
        sRTN += "]";
        return sRTN;
    }
    public static string SaveMarsLog(int iTH, eLogTYPE LogType, string sMars, string sState){
        string sLOG = sMars + " " + sState;
        AddMessage(iTH, sLOG);
        if (!bMF){
            for (int i = 0; i < thSeqThrad.Length; i++){
                if (thSeqThrad[i] == iTH){
                    SAVE_STANDARD_LOG(iTH, LogType, sMars, sState);
                }
            }
        }
        return sMars;
    }
    public static void SAVE_STANDARD_LOG(int iTH, eLogTYPE LogType, string sLOG, string sSTATE){
        string sPath        = PATH_.PathSeqLog + DateTime.Now.Year.ToString() + string.Format("{0:00}", DateTime.Now.Month) + string.Format("{0:00}", DateTime.Now.Day) + "\\";
        string sFILE        = PATH_.MACHINE_NAME + "_" + sDEVICE_ID + "_";
        string sDateTime    = DateTime.Now.Year.ToString() + "-" + string.Format("{0:00}", DateTime.Now.Month) + "-" + string.Format("{0:00}", DateTime.Now.Day) + " " + string.Format("{0:00}", DateTime.Now.Hour) + ":" + string.Format("{0:00}", DateTime.Now.Minute) + ":" + string.Format("{0:00}", DateTime.Now.Second) + "." + string.Format("{0:000}", DateTime.Now.Millisecond) + ETC.cspTab;
        string sLogType     = LogType.ToString() + ETC.cspTab;
        string sDEVICE      = ThreadName[iTH] + ETC.cspTab;
        string sEVENT       = "Start";
        string sBarcode     = SeqData[iTH].sBARCODE;
        string sMonitoringLog;

        if (!Directory.Exists(sPath)) Directory.CreateDirectory(sPath);

        if (sSTATE == "STOP" || sSTATE == "Stop" || sSTATE == "END" || sSTATE == "End")     sEVENT = "End";
        else if (sSTATE == "IN" || sSTATE == "OUT" || sSTATE == "BIT" || sSTATE == "WAR")   sEVENT = sSTATE;

        if (sBarcode == "" || sBarcode == null) sFILE += "NONE_" + string.Format("{0:00}", DateTime.Now.Hour) + ".txt";
        else                                    sFILE +=  sBarcode + "_" + SeqData[iTH].sTrackInTime + ".txt";

        sMonitoringLog      = sDateTime + sLogType + sDEVICE + sLOG + ETC.cspTab + sEVENT + ETC.NewLine; //etc.cspTab;
        LogThread[iTH].CMD  = sLOG + " => " + sEVENT;

        //저장!
        //try{
        //    mFILE.WR_File(sPath + sFILE, sMonitoringLog, true);
        //}
        //catch (Exception exp){
        //    System.Diagnostics.Debug.WriteLine(exp.Message);
        //    return;
        //}
    }

    public static void WRLogException(string Logs, string ID) { mLOG.sException += GET_LOG_TAG(ID) + Logs + ETC.CrLf; } //예외 처리 로그
    public static void SaveLogException(string sMSG, Exception exc){
        string fn = GET_DirNameDate(PATH_.LogEXCEPTION) + "EXCEPTION_"
            + string.Format("{0:00}", DateTime.Now.Day) + string.Format("{0:00}", DateTime.Now.Hour)
            + string.Format("{0:00}", DateTime.Now.Minute) + string.Format("{0:00}", DateTime.Now.Second) + ".txt";

        string ss = sMSG + " | " + exc.Message + " / " + exc.ToString();
        try{
            FILE_.WR_File(fn, ss, false);
        }
        catch (Exception exp){
            System.Diagnostics.Debug.WriteLine(exp.Message);
            return;
        }
        WRLogException(sMSG + ETC.CrLf + "☞" + fn, "Exception");
    }

    public static void SaveLotEnd(string sLOT_ID, string sITS_ID, int StripCount, int GoodUnit, int ReworkUnit, int NGUnit, int ITSUnit){
        mLOG.sLOT += GET_LOG_TAG(sLOT_ID) + sITS_ID + "," + StripCount.ToString() + "," + GoodUnit.ToString() + "," + ReworkUnit.ToString() + "," + NGUnit.ToString() + "," + ITSUnit.ToString() + ETC.NewLine;
    }

    public static void SAVE_MEASURE_DATA(string sLogs, string sID){
        mLOG.sMeasure += GET_LOG_TAG(sID) + sLogs + ETC.NewLine;
    } // barcode, right height, left height, top width, btm width, width 1, width 2, width 3, width 4, width 5, Hight 1, Hight 2, Hight 3, Hight 4, Hight 5, RESULT  ; 

    public static void SaveLogPCBDATA(string sLOG, string sSysLog, string sBarcode, bool bRESULT){
        string fn = GET_DirNameDate(PATH_.LogPCBDATA) + "PCBDATA_";
        string sSyLog;
        string sRST = bRESULT ? "OK" : "NG";
        string sPCB_ID = sBarcode;
        if (sPCB_ID == null || sPCB_ID == "") sPCB_ID = "NONE";
        if (bMF){
            fn += "RUN_MANUAL_" + string.Format("{0:00}", DateTime.Now.Hour)
                + string.Format("{0:00}", DateTime.Now.Minute) + string.Format("{0:00}", DateTime.Now.Second) + "_" + sRST + ".txt";
            sSyLog = "MANUAL/" + sSysLog + sRST;
        }
        else{
            fn += sBarcode + "_" + sRST + ".txt";
            sSyLog = sBarcode + "/" + sSysLog + sRST;
        }
        SAVE_MEASURE_DATA(sSyLog, "");
        string ss = sLOG + "_" + sRST + ETC.CrLf;
        try{
            FILE_.WR_File(fn, ss, false);
        }
        catch (Exception exp){
            System.Diagnostics.Debug.WriteLine(exp.Message);
            return;
        }
    }

    public static string SaveImageFolder(){
        string fn = GET_DirNameDate(PATH_.LogImage);
        fn += string.Format("{0:00}", DateTime.Now.Hour)
                + string.Format("{0:00}", DateTime.Now.Minute) + string.Format("{0:00}", DateTime.Now.Second) + "_";
        return fn;
    }

    public static string GET_PROCESS_LOG_TAG(int tn, string sTIME){
        string sDATE    = DateTime.Now.Year.ToString() + "-" + string.Format("{0:00}", DateTime.Now.Month) + "-" + string.Format("{0:00}", DateTime.Now.Day);
        sDATE           = sDATE + " " + sTIME;
        string sRecipe  = sJobName;
        string sLogin   = eLoginLevel.ToString();
        string sProc    = ThreadName[tn];
        string sMcSts   = eMCStatus.ToString();
        string strRtn   = sDATE + "," + sRecipe + "," + sLogin + "," + sProc + "," + sMcSts + ",";
        return strRtn;
    }
    public static void SAVE_PROCESS_LOG(int tn, string sPATH_THREAD, string sTIME, string sMESSAGE){
        string sPATH = sPATH_THREAD;
        string sMARS = PATH_.LogMARS;
        string sLOGWRITE;
        if (sPATH == "" || sPATH == null || sMESSAGE == "" || sMESSAGE == null && (sMARS == "" || sMARS == null)) return;
        sPATH       = GET_DirNameDate(sPATH) + PATH_.PROCESS_FILENAME[tn];
        sMARS       = GET_DirNameDate(sMARS) + PATH_.MARS_FILENAME;
        sLOGWRITE   = GET_PROCESS_LOG_TAG(tn, sTIME);
        sLOGWRITE   = sLOGWRITE + sMESSAGE;
        FILE_.WRL_File(sPATH, sLOGWRITE, true);
        FILE_.WRL_File(sMARS, sLOGWRITE, true);
    }

    public static void SAVE_MOVING_ERROR_LOG(int mt, string sLOG){
        if (sLOG == "" || sLOG == null) return;
        string sPATH        = GET_DirNameDate(PATH_.LogMovingERR) + "MTERROR.log";
        string sTIME        = DateTime.Now.ToString("HH:mm:ss.fff");
        string sLOG_WRITE   = sTIME + " - " + MtName[mt] + " [ " + mt.ToString("00") + " ] = " + sLOG.ToString();
        FILE_.WRL_File(sPATH, sLOG_WRITE, true);
    }

    public static void Log_WRITE_FILE(string Path, ref string msg){
        if (msg == null) return;
        if (msg.Length > 10){
            Microsoft.VisualBasic.FileIO.FileSystem.WriteAllText(Path, msg, true);
            msg = "";
        }
    }

    #region "PROCESS INFO 데이터 READ/WRITE"
    public static void SaveProcessInfoData(){
        string fn;
        fn = GET_DirNameDate(PATH_.ProcesInfoData) + "ProcessInfo.log";
        LOG_WRITE_FILE(fn, ref mLOG.sProcessInfo, false);
    }
    public static void WRProcessInfoData(int iTOTAL, int iGOOD, int iNG){
        string s;
        s = iTOTAL.ToString() + "," + iGOOD.ToString() + "," + iNG.ToString();
        mLOG.sProcessInfo = s;
        try{
            SaveProcessInfoData();
        }
        catch (Exception exc) { SaveLogException("LogWR -> WRProcessInfoData", exc); }
    }
    public static void RDProcessInfoData(ref long iTOTAL, ref long iGOOD, ref long iNG){
        try{
            double sDATE    = DateTime.Now.ToOADate();
            string FileName = GET_PathOperation(sDATE, PATH_.ProcesInfoData) + "ProcessInfo.log";
            if (!File.Exists(FileName)) return;
            string[] sARR = File.ReadAllLines(FileName);

            for (int cnt = 0; cnt < sARR.Length; cnt++){
                string[] subarr = sARR[cnt].Split(',');
                iTOTAL          = long.Parse(subarr[0]);
                iGOOD           = long.Parse(subarr[1]);
                iNG             = long.Parse(subarr[2]);
            }
        }
        catch (Exception exc){
            iTOTAL      = 0;
            iGOOD       = 0;
            iNG         = 0;
            SaveLogException("LogWR -> RDProcessInfoData", exc);
        }
    }
    #endregion "PROCESS INFO 데이터 저장테이터 READ/WRITE"
}