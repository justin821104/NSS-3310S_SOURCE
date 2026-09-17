using LIB_.DateType;
using NSS_3310S;
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
        mLOG.sException         = string.Empty;
        mLOG.sWarnMsg           = string.Empty;
        mLOG.sSystem            = string.Empty;
        mLOG.sParaEvent         = string.Empty;
        mLOG.sOperate           = string.Empty;
        mLOG.sManual            = string.Empty;
        mLOG.sMars              = string.Empty;
        mLOG.sLogin             = string.Empty;
        mLOG.sMES               = string.Empty;
        mLOG.sMeasure           = string.Empty;
        mLOG.sProcessInfo       = string.Empty;
        mLOG.sCOUNT             = string.Empty;
        mLOG.sPROCESS           = string.Empty;
        mLOG.sTACK              = string.Empty;
        mLOG.sLOT               = string.Empty;
        mLOG.sOneCycleTime      = string.Empty;
        mLOG.sBlade             = string.Empty;

        mLOG.sBorcodeHistory    = string.Empty;
        mLOG.sITSBarcodeHistory = string.Empty;
        mLOG.sPCBUnitInfo       = string.Empty;

        mLOG.sProgramCheck      = string.Empty;
        mLOG.sPRSData           = string.Empty;
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

        fn = GET_DirNameDate(PATH_.LogBladeInfo) + "BLADE.log";
        LOG_WRITEFILE(fn, ref mLOG.sBlade);

        fn = GET_DirNameDate(PATH_.LogPCBUnitInfo) + "UNIT.log";
        LOG_WRITEFILE(fn, ref mLOG.sPCBUnitInfo);

        //fn = GET_DirNameDate(PATH.LogCount) + "COUNT.log";
        //LOG_WRITEFILE(fn, ref mLOG.sCOUNT);
        if (prMACHINE[CP.LogSaveSkip] == (int)eUSE.USE){
            fn = GET_DirNameDate(PATH_.LogPROCESS) + "PROCESS.log";
            LOG_WRITEFILE(fn, ref mLOG.sPROCESS);
        }
        else mLOG.sPROCESS = "";

        fn = GET_DirNameDate(PATH_.LogCycleTack) + "TACK.log";
        LOG_WRITEFILE(fn, ref mLOG.sTACK);

        fn = GET_DirNameDate(PATH_.LogOneCycleTime) + "TACK.log";
        LOG_WRITEFILE(fn, ref mLOG.sOneCycleTime);

        if (prMACHINE[CP.UseLogBarcodeHistory] == (int)eUSE.USE){
            fn = GET_DirNameDate(PATH_.LogBarcodeHistory) + "BarcodeHistory.log";
            LOG_WRITEFILE(fn, ref mLOG.sBorcodeHistory);
        }
        else mLOG.sBorcodeHistory = "";

        fn = GET_DirNameDate(PATH_.LogITSBarcodeHistory) + "ITSBarcodeHistory.log";
        LOG_WRITEFILE(fn, ref mLOG.sITSBarcodeHistory);

        fn = GET_DirNameDate(PATH_.LogProgram) + "ProgramCheck.log";
        LOG_WRITEFILE(fn, ref mLOG.sProgramCheck);

        fn = GET_DirNameDate(PATH_.LogPRS) + "PRS.log";
        LOG_WRITEFILE(fn, ref mLOG.sPRSData);
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
        //double NowDate = DateTime.Now.Date.ToOADate();
        //double OldDate = NowDate - 90;

        //' ------------------------------------- Delete MARS LOG
        int cnt = GET_YearMountFolder(ref arrMonth);
        if (cnt - 12 > 0){
            for (int j = 0; j < PATH_.aPath_DEL_LOGs.Length; j++){
                //if (j == PATH_.aPath_DEL_LOGs.Length - 1) {
                //    UTIL_.DELAY(5);
                //}
                for (int k = 0; k < /*arrMonth.Length*/ cnt - 12; k++){ //12개월치 저장!
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
                
                string rootFolderPath = PATH_.aPath_DEL_LOGs[j];
                DateTime thresholdDate = DateTime.Now.AddMonths(-12);  // 12개월 전 날짜
                if (Directory.Exists(rootFolderPath)){
                    foreach (string folderPath in Directory.GetDirectories(rootFolderPath)) {
                        try{
                            DateTime creationTime = Directory.GetCreationTime(folderPath);
                            if (creationTime < thresholdDate) {
                                Directory.Delete(folderPath, true); // true: 하위 파일과 폴더 포함 삭제
                            }
                        }
                        catch (Exception ex) {
                            SaveLogException($"오류 발생 :{folderPath} - ", ex);
                        }
                    }
                }
            }
        }
        

        arrMonth = new string[24];
        cnt = GET_YearMountFolder(ref arrMonth);
        if (cnt - 1 > 0){
            for (int j = 0; j < PATH_.sPath_DEL_LOGs_ONE_MONTH.Length; j++){
                for (int k = 0; k < /*arrMonth.Length*/cnt - 1; k++){ //1개월치 저장!
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
        for (int i = 2007; i < DateTime.Now.Year - 2; i++){
            string sFOLDER = PATH_.MCLOG + i.ToString() + "\\";
            if (Directory.Exists(sFOLDER)) Directory.Delete(sFOLDER, true);
        }
    }
    public static void DelectAccessFile(string sPath, int nDeletedDay = 60) {
        try {
            //MonitoringSystem 폴더안에 폴더명이 년도월일로 되어있어서 수정한 날짜 기준으로 'Deleted_Day'일 이전꺼 삭제함!
            DirectoryInfo di = new DirectoryInfo(sPath);
            if (di.Exists) {
                DirectoryInfo[] dirInfo = di.GetDirectories();
                string lDate = DateTime.Today.AddDays(-nDeletedDay).ToString("yyyyMMdd");
                foreach (DirectoryInfo dir in dirInfo) {
                    if (lDate.CompareTo(dir.LastWriteTime.ToString("yyyyMMdd")) > 0) {
                        dir.Attributes = FileAttributes.Normal;
                        dir.Delete(true);
                    }
                }
            }
        }
        catch (Exception Exp){
            //TEXT 파일로 기록 추가 ?
            //CMessage.Exception("[CLogWR] DELETE->CPATH.SeqLog", "CFile", Exp);
            LogWR_.DEBUG_PRINT("CFile->DelectAccessFile => " + Exp.Message);
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
        for (int i = 0; i < iMONTH; i++){
            arr[idx] = iYEAR.ToString() + "_" + string.Format("{0:00}", i + 1);
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
    
    public static void SaveLogLotInfo(){
        string fn = GET_DirNameDate(PATH_.LogLotLogList) + "LOT.log";

        //public int nNUM;                    //LOT 등록 순서 
        //public string WorkScope;            //작업 구분  => 대기,진행,완료,강제완료
        //public string ProcCondition_1;
        //public string ProcCondition_2;
        //public string ProcCondition_3;
        //public string ProcCondition_4;

        //LOT END  처리 전 LOT 정보 저장!
        string Logs = GET_LOG_TAG(CLOT.GET_LOT.LotID) + "," + CLOT.GET_LOT.ItsID + "," + CLOT.GET_LOT.Recipe + "," + CLOT.GET_LOT.ToolNo + "," + CLOT.GET_LOT.ITS_LotID_IN + "," + CLOT.GET_LOT.ITS_LotID_CT + "," + CLOT.GET_LOT.BeginTime + "," + CLOT.GET_LOT.EndTime + "," +
            CLOT.GET_LOT.WorkTime + "," + CLOT.GET_LOT.RunTime + "," + CLOT.GET_LOT.StopTime + "," + CLOT.GET_LOT.ErrorTime + "," + CLOT.GET_LOT.ProcCD + "," + CLOT.GET_LOT.ProcName + "," + CLOT.GET_LOT.LotType + "," + CLOT.GET_LOT.Qty + "," + CLOT.GET_LOT.ITS + "," +
            CLOT.GET_LOT.UnitSizeX + "," + CLOT.GET_LOT.UnitSizeY + "," + CLOT.GET_LOT.UnitSize_USL + "," + CLOT.GET_LOT.UnitSize_LSL + "," + CLOT.GET_LOT.Thick + "," + CLOT.GET_LOT.Thick_USL + "," + CLOT.GET_LOT.Thick_LSL + "," +
            CLOT.GET_LOT.ABFMATERIAL + "," + CLOT.GET_LOT.LANDPKGX + "," + CLOT.GET_LOT.LANDPKGX_UPPER + "," + CLOT.GET_LOT.LANDPKGX_LOWER + "," + CLOT.GET_LOT.LANDPKGY + "," + CLOT.GET_LOT.LANDPKGY_UPPER + "," + CLOT.GET_LOT.LANDPKGY_LOWER + "," +
            CLOT.GET_LOT.InCnt + "," + CLOT.GET_LOT.OutCnt + "," + CLOT.GET_LOT.CurCnt + "," + CLOT.GET_LOT.PassCnt + "," + CLOT.GET_LOT.LoadingCount + "," + CLOT.GET_LOT.ExceptCount + "," + CLOT.GET_LOT.UnloadingCount + "," + CLOT.GET_LOT.WorkSort + "," +
            CLOT.GET_LOT.LotCnt + "," + CLOT.GET_LOT.StripCnt + "," + CLOT.GET_LOT.UnitCnt + "," + CLOT.GET_LOT.GoodUnit + "," + CLOT.GET_LOT.ReworkUnit + "," + CLOT.GET_LOT.NGUnit + "," + CLOT.GET_LOT.ITSCount + "," + CLOT.GET_LOT.GoodTray + "," + CLOT.GET_LOT.NGTray + "," + CLOT.GET_LOT.TotalUnit + "," +
            CLOT.GET_LOT.BarcodeSp1 + "," + CLOT.GET_LOT.BarcodeSp2 + "," +
            CLOT.GET_LOT.WorkScope + "," + CLOT.GET_LOT.WorkCondition + "," + CLOT.GET_LOT.ProductType + "," +
            CLOT.GET_LOT.BOT_LANDTOPKG_X + "," + CLOT.GET_LOT.BOT_CHAMFERLEN_TM_X + "," + CLOT.GET_LOT.BOT_CHAMFERLEN_TP_X + "," + CLOT.GET_LOT.BOT_LANDTOPKG_Y + "," + CLOT.GET_LOT.BOT_CHAMFERLEN_TM_Y + "," + CLOT.GET_LOT.BOT_CHAMFERLEN_TP_Y + "," +
            CLOT.GET_LOT.TOP_LANDTOPKG_X + "," + CLOT.GET_LOT.TOP_CHAMFERLEN_TM_X + "," + CLOT.GET_LOT.TOP_CHAMFERLEN_TP_X + "," + CLOT.GET_LOT.TOP_LANDTOPKG_Y + "," + CLOT.GET_LOT.TOP_CHAMFERLEN_TM_Y + "," + CLOT.GET_LOT.TOP_CHAMFERLEN_TP_Y + "," +
            CLOT.GET_LOT.IDSp1 + "," + CLOT.GET_LOT.IDSp2 +
            ETC.NewLine;

        FILE_.WR_File(fn, Logs, true);
        //Logs = "";

        //TEST !!
        //string Logs = GET_LOG_TAG(CLOT.GET_LOT.LotID) + "," + CLOT.FINISH_LOT[0].ItsID + "," + CLOT.FINISH_LOT[0].Recipe + "," + CLOT.FINISH_LOT[0].ToolNo + "," + CLOT.FINISH_LOT[0].ITS_LotID_IN + "," + CLOT.FINISH_LOT[0].ITS_LotID_CT + "," + CLOT.FINISH_LOT[0].BeginTime + "," + CLOT.FINISH_LOT[0].EndTime + "," +
        //    CLOT.FINISH_LOT[0].WorkTime + "," + CLOT.FINISH_LOT[0].RunTime + "," + CLOT.FINISH_LOT[0].StopTime + "," + CLOT.FINISH_LOT[0].ErrorTime + "," + CLOT.FINISH_LOT[0].ProcCD + "," + CLOT.FINISH_LOT[0].ProcName + "," + CLOT.FINISH_LOT[0].LotType + "," + CLOT.FINISH_LOT[0].Qty + "," + CLOT.FINISH_LOT[2].ITS + "," +
        //    CLOT.FINISH_LOT[0].UnitSizeX + "," + CLOT.FINISH_LOT[0].UnitSizeY + "," + CLOT.FINISH_LOT[0].UnitSize_USL + "," + CLOT.FINISH_LOT[0].UnitSize_LSL + "," + CLOT.FINISH_LOT[0].Thick + "," + CLOT.FINISH_LOT[0].Thick_USL + "," + CLOT.FINISH_LOT[0].Thick_LSL + "," +
        //    CLOT.FINISH_LOT[0].ABFMATERIAL + "," + CLOT.FINISH_LOT[0].LANDPKGX + "," + CLOT.FINISH_LOT[0].LANDPKGX_UPPER + "," + CLOT.FINISH_LOT[0].LANDPKGX_LOWER + "," + CLOT.FINISH_LOT[0].LANDPKGY + "," + CLOT.FINISH_LOT[0].LANDPKGY_UPPER + "," + CLOT.FINISH_LOT[0].LANDPKGY_LOWER + "," +
        //    CLOT.FINISH_LOT[0].InCnt + "," + CLOT.FINISH_LOT[0].OutCnt + "," + CLOT.FINISH_LOT[0].CurCnt + "," + CLOT.FINISH_LOT[0].PassCnt + "," + CLOT.FINISH_LOT[0].LoadingCount + "," + CLOT.FINISH_LOT[0].ExceptCount + "," + CLOT.FINISH_LOT[0].UnloadingCount + "," + CLOT.FINISH_LOT[0].WorkSort + "," +
        //    CLOT.FINISH_LOT[0].LotCnt + "," + CLOT.FINISH_LOT[0].StripCnt + "," + CLOT.FINISH_LOT[0].UnitCnt + "," + CLOT.FINISH_LOT[0].GoodUnit + "," + CLOT.FINISH_LOT[0].ReworkUnit + "," + CLOT.FINISH_LOT[0].NGUnit + "," + CLOT.FINISH_LOT[0].ITSCount + "," + CLOT.FINISH_LOT[0].GoodTray + "," + CLOT.FINISH_LOT[0].NGTray + "," + CLOT.FINISH_LOT[0].TotalUnit + "," +
        //    CLOT.FINISH_LOT[0].BarcodeSp1 + "," + CLOT.FINISH_LOT[0].BarcodeSp2 + "," +
        //    CLOT.FINISH_LOT[0].WorkScope + "," + CLOT.FINISH_LOT[0].WorkCondition + "," + CLOT.FINISH_LOT[0].ProductType + "," +
        //    CLOT.FINISH_LOT[0].BOT_LANDTOPKG_X + "," + CLOT.FINISH_LOT[0].BOT_CHAMFERLEN_TM_X + "," + CLOT.FINISH_LOT[0].BOT_CHAMFERLEN_TP_X + "," + CLOT.FINISH_LOT[0].BOT_LANDTOPKG_Y + "," + CLOT.FINISH_LOT[0].BOT_CHAMFERLEN_TM_Y + "," + CLOT.FINISH_LOT[0].BOT_CHAMFERLEN_TP_Y + "," +
        //    CLOT.FINISH_LOT[0].TOP_LANDTOPKG_X + "," + CLOT.FINISH_LOT[0].TOP_CHAMFERLEN_TM_X + "," + CLOT.FINISH_LOT[0].TOP_CHAMFERLEN_TP_X + "," + CLOT.FINISH_LOT[0].TOP_LANDTOPKG_Y + "," + CLOT.FINISH_LOT[0].TOP_CHAMFERLEN_TM_Y + "," + CLOT.FINISH_LOT[0].TOP_CHAMFERLEN_TP_Y +
        //    ETC.NewLine;
        //
        //FILE_.WR_File(fn, Logs, true);
        //
        //Logs = GET_LOG_TAG(CLOT.GET_LOT.LotID) + "," + CLOT.FINISH_LOT[1].ItsID + "," + CLOT.FINISH_LOT[1].Recipe + "," + CLOT.FINISH_LOT[1].ToolNo + "," + CLOT.FINISH_LOT[1].ITS_LotID_IN + "," + CLOT.FINISH_LOT[1].ITS_LotID_CT + "," + CLOT.FINISH_LOT[1].BeginTime + "," + CLOT.FINISH_LOT[1].EndTime + "," +
        //    CLOT.FINISH_LOT[1].WorkTime + "," + CLOT.FINISH_LOT[1].RunTime + "," + CLOT.FINISH_LOT[1].StopTime + "," + CLOT.FINISH_LOT[1].ErrorTime + "," + CLOT.FINISH_LOT[1].ProcCD + "," + CLOT.FINISH_LOT[1].ProcName + "," + CLOT.FINISH_LOT[1].LotType + "," + CLOT.FINISH_LOT[1].Qty + "," + CLOT.FINISH_LOT[1].ITS + "," +
        //    CLOT.FINISH_LOT[1].UnitSizeX + "," + CLOT.FINISH_LOT[1].UnitSizeY + "," + CLOT.FINISH_LOT[1].UnitSize_USL + "," + CLOT.FINISH_LOT[1].UnitSize_LSL + "," + CLOT.FINISH_LOT[1].Thick + "," + CLOT.FINISH_LOT[1].Thick_USL + "," + CLOT.FINISH_LOT[1].Thick_LSL + "," +
        //    CLOT.FINISH_LOT[1].ABFMATERIAL + "," + CLOT.FINISH_LOT[1].LANDPKGX + "," + CLOT.FINISH_LOT[1].LANDPKGX_UPPER + "," + CLOT.FINISH_LOT[1].LANDPKGX_LOWER + "," + CLOT.FINISH_LOT[1].LANDPKGY + "," + CLOT.FINISH_LOT[1].LANDPKGY_UPPER + "," + CLOT.FINISH_LOT[1].LANDPKGY_LOWER + "," +
        //    CLOT.FINISH_LOT[1].InCnt + "," + CLOT.FINISH_LOT[1].OutCnt + "," + CLOT.FINISH_LOT[1].CurCnt + "," + CLOT.FINISH_LOT[1].PassCnt + "," + CLOT.FINISH_LOT[1].LoadingCount + "," + CLOT.FINISH_LOT[1].ExceptCount + "," + CLOT.FINISH_LOT[1].UnloadingCount + "," + CLOT.FINISH_LOT[1].WorkSort + "," +
        //    CLOT.FINISH_LOT[1].LotCnt + "," + CLOT.FINISH_LOT[1].StripCnt + "," + CLOT.FINISH_LOT[1].UnitCnt + "," + CLOT.FINISH_LOT[1].GoodUnit + "," + CLOT.FINISH_LOT[1].ReworkUnit + "," + CLOT.FINISH_LOT[1].NGUnit + "," + CLOT.FINISH_LOT[1].ITSCount + "," + CLOT.FINISH_LOT[1].GoodTray + "," + CLOT.FINISH_LOT[1].NGTray + "," + CLOT.FINISH_LOT[1].TotalUnit + "," +
        //    CLOT.FINISH_LOT[1].BarcodeSp1 + "," + CLOT.FINISH_LOT[1].BarcodeSp2 + "," +
        //    CLOT.FINISH_LOT[1].WorkScope + "," + CLOT.FINISH_LOT[1].WorkCondition + "," + CLOT.FINISH_LOT[1].ProductType +"," +
        //    CLOT.FINISH_LOT[1].BOT_LANDTOPKG_X + "," + CLOT.FINISH_LOT[1].BOT_CHAMFERLEN_TM_X + "," + CLOT.FINISH_LOT[1].BOT_CHAMFERLEN_TP_X + "," + CLOT.FINISH_LOT[1].BOT_LANDTOPKG_Y + "," + CLOT.FINISH_LOT[1].BOT_CHAMFERLEN_TM_Y + "," + CLOT.FINISH_LOT[1].BOT_CHAMFERLEN_TP_Y + "," +
        //    CLOT.FINISH_LOT[1].TOP_LANDTOPKG_X + "," + CLOT.FINISH_LOT[1].TOP_CHAMFERLEN_TM_X + "," + CLOT.FINISH_LOT[1].TOP_CHAMFERLEN_TP_X + "," + CLOT.FINISH_LOT[1].TOP_LANDTOPKG_Y + "," + CLOT.FINISH_LOT[1].TOP_CHAMFERLEN_TM_Y + "," + CLOT.FINISH_LOT[1].TOP_CHAMFERLEN_TP_Y +
        //    ETC.NewLine;
        //
        //FILE_.WR_File(fn, Logs, true);
        //
        //Logs = GET_LOG_TAG(CLOT.GET_LOT.LotID) + "," + CLOT.FINISH_LOT[2].ItsID + "," + CLOT.FINISH_LOT[2].Recipe + "," + CLOT.FINISH_LOT[2].ToolNo + "," + CLOT.FINISH_LOT[2].ITS_LotID_IN + "," + CLOT.FINISH_LOT[2].ITS_LotID_CT + "," + CLOT.FINISH_LOT[2].BeginTime + "," + CLOT.FINISH_LOT[2].EndTime + "," +
        //    CLOT.FINISH_LOT[2].WorkTime + "," + CLOT.FINISH_LOT[2].RunTime + "," + CLOT.FINISH_LOT[2].StopTime + "," + CLOT.FINISH_LOT[2].ErrorTime + "," + CLOT.FINISH_LOT[2].ProcCD + "," + CLOT.FINISH_LOT[2].ProcName + "," + CLOT.FINISH_LOT[2].LotType + "," + CLOT.FINISH_LOT[2].Qty + "," + CLOT.FINISH_LOT[2].ITS + "," +
        //    CLOT.FINISH_LOT[2].UnitSizeX + "," + CLOT.FINISH_LOT[2].UnitSizeY + "," + CLOT.FINISH_LOT[2].UnitSize_USL + "," + CLOT.FINISH_LOT[2].UnitSize_LSL + "," + CLOT.FINISH_LOT[2].Thick + "," + CLOT.FINISH_LOT[2].Thick_USL + "," + CLOT.FINISH_LOT[2].Thick_LSL + "," +
        //    CLOT.FINISH_LOT[2].ABFMATERIAL + "," + CLOT.FINISH_LOT[2].LANDPKGX + "," + CLOT.FINISH_LOT[2].LANDPKGX_UPPER + "," + CLOT.FINISH_LOT[2].LANDPKGX_LOWER + "," + CLOT.FINISH_LOT[2].LANDPKGY + "," + CLOT.FINISH_LOT[2].LANDPKGY_UPPER + "," + CLOT.FINISH_LOT[2].LANDPKGY_LOWER + "," +
        //    CLOT.FINISH_LOT[2].InCnt + "," + CLOT.FINISH_LOT[2].OutCnt + "," + CLOT.FINISH_LOT[2].CurCnt + "," + CLOT.FINISH_LOT[2].PassCnt + "," + CLOT.FINISH_LOT[2].LoadingCount + "," + CLOT.FINISH_LOT[2].ExceptCount + "," + CLOT.FINISH_LOT[2].UnloadingCount + "," + CLOT.FINISH_LOT[2].WorkSort + "," +
        //    CLOT.FINISH_LOT[2].LotCnt + "," + CLOT.FINISH_LOT[2].StripCnt + "," + CLOT.FINISH_LOT[2].UnitCnt + "," + CLOT.FINISH_LOT[2].GoodUnit + "," + CLOT.FINISH_LOT[2].ReworkUnit + "," + CLOT.FINISH_LOT[2].NGUnit + "," + CLOT.FINISH_LOT[2].ITSCount + "," + CLOT.FINISH_LOT[2].GoodTray + "," + CLOT.FINISH_LOT[2].NGTray + "," + CLOT.FINISH_LOT[2].TotalUnit + "," +
        //    CLOT.FINISH_LOT[2].BarcodeSp1 + "," + CLOT.FINISH_LOT[2].BarcodeSp2 + "," +
        //    CLOT.FINISH_LOT[2].WorkScope + "," + CLOT.FINISH_LOT[2].WorkCondition + "," + CLOT.FINISH_LOT[2].ProductType +"," +
        //    CLOT.FINISH_LOT[2].BOT_LANDTOPKG_X + "," + CLOT.FINISH_LOT[2].BOT_CHAMFERLEN_TM_X + "," + CLOT.FINISH_LOT[2].BOT_CHAMFERLEN_TP_X + "," + CLOT.FINISH_LOT[2].BOT_LANDTOPKG_Y + "," + CLOT.FINISH_LOT[2].BOT_CHAMFERLEN_TM_Y + "," + CLOT.FINISH_LOT[2].BOT_CHAMFERLEN_TP_Y + "," +
        //    CLOT.FINISH_LOT[2].TOP_LANDTOPKG_X + "," + CLOT.FINISH_LOT[2].TOP_CHAMFERLEN_TM_X + "," + CLOT.FINISH_LOT[2].TOP_CHAMFERLEN_TP_X + "," + CLOT.FINISH_LOT[2].TOP_LANDTOPKG_Y + "," + CLOT.FINISH_LOT[2].TOP_CHAMFERLEN_TM_Y + "," + CLOT.FINISH_LOT[2].TOP_CHAMFERLEN_TP_Y +
        //    ETC.NewLine;
        //
        //FILE_.WR_File(fn, Logs, true);
    }

    public static void SaveProgramCheck(string logs, string id) { mLOG.sProgramCheck += GET_LOG_TAG(id) + logs + ETC.NewLine; }
    public static void SavePRSLog(string logs, string id) { mLOG.sPRSData += GET_LOG_TAG(id) + logs + ETC.NewLine; }
    public static void SaveMsSQLReadingFail(string logs, string id) { mLOG.sITSBarcodeHistory += GET_LOG_TAG(id) + logs + ETC.NewLine; }
    public static void SaveBarcodeHistory(string logs, string id){
        string EES = (prMACHINE[CP.UseMES] == (int)eUSE.USE) ? "TRUE" : "FALSE";
        mLOG.sBorcodeHistory += GET_LOG_TAG(id) + "EES," + EES + ",LOTID," + CLOT.GET_LOT.LotID + ",ITSID," + CLOT.GET_LOT.ItsID + "," + logs + ETC.NewLine;
    }
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
        string sLOG = "";
        for (int i = 0; i < m.Length; i++){
            sLOG = mtSTS[m[i]].CurrentPosition.ToString("0.000") + "->" + mtDATA[m[i], pn[i]].Pos.ToString("0.000");
            if (i != m.Length) sLOG += "/";
        }
        sRTN += sLOG + "]";
        return sRTN;
    }
    public static string LogPos(int[] m, stMoveInfo[] mi){
        string sRTN = "[";
        string sLOG = "";
        for (int i = 0; i < m.Length; i++){
            sLOG = mtSTS[m[i]].CurrentPosition.ToString("0.000") + "->" + mi[i].Pos.ToString("0.000");
            if (i != m.Length) sLOG += "/";
        }
        sRTN += sLOG + "]";
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

        string ss = sMSG + '\n' + "|" + exc.Message + '\n' + "|" + exc.ToString();
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

    public static void SaveBladeInfo(string PCBBarcode, string sState, int InCount){
        string SetState;
        if (sState == "Plc" || sState == "In")      SetState = "시작";
        else                                        SetState = "완료";

        mLOG.sBlade += GET_LOG_TAG(CLOT.GET_LOT.LotID) + CLOT.GET_LOT.ABFMATERIAL + "," + CLOT.GET_LOT.BarcodeSp1 + "," + CLOT.GET_LOT.BarcodeSp2 + "," + PCBBarcode + "," + SetState + "," + Sp1BladeAmountOfUse + "," + Sp2BladeAmountOfUse + "," + InCount.ToString() + "," + CLOT.GET_LOT.IDSp1 + "," + CLOT.GET_LOT.IDSp2 + ETC.NewLine;
    }

    public static void SavePCBUnitInfo(int nThread, eMAP_BLOCK eStage){
        mLOG.sPCBUnitInfo += GET_LOG_TAG(CLOT.GET_LOT.LotID) + "," + CLOT.InfoStrip[nThread].Barcode + "," + IsLONG[L.StageUnit[(int)eStage]].ToString() + "," + IsLONG[L.StageUnitGood[(int)eStage]].ToString() + "," + IsLONG[L.StageUnitNG[(int)eStage]].ToString() + "," + IsLONG[L.StageUnitXOut[(int)eStage]].ToString() + "," + IsLONG[L.Stage_ITS[(int)eStage]].ToString();

        //csv파일로 저장
        DEBUG_PRINT(eStage.ToString() + " SAVE");
        Log_StripData(CLOT.InfoStrip[nThread].Barcode, CLOT.GET_LOT.Qty, (int)IsLONG[L.StageUnit[(int)eStage]], (int)IsLONG[L.StageUnitGood[(int)eStage]], (int)IsLONG[L.StageUnitNG[(int)eStage]], (int)IsLONG[L.StageUnitXOut[(int)eStage]], (int)IsLONG[L.Stage_ITS[(int)eStage]]);
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
        sLOGWRITE   += sMESSAGE;
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


    public static void CreateCsvFile(string name){
        name = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + name;
        DirectoryInfo di = new DirectoryInfo(name);
        if (di.Exists == false) di.Create();
    }

    public static void Log_DBWrite(string lotId, string log) {
        string  fn = GET_DirNameDate(PATH_.DBLog) + "DBWrite.log";
        string mes = lotId + ETC.cspTab + log;
        FILE_.WR_File(fn, mes, true);
    }

    public static bool WriteLotInfo(string curlotid){
        string fn = PATH_.StripData + "LOT INFO.txt"; //
        bool rtn = FILE_.WR_LotInfo(fn, curlotid);
        if (!rtn) return false;
        return true;    
    }
    public static void Log_StripData(string sStripID, int totalstrip, int Unit, int Good, int NG, int XOut, int ITS){
        string fn = PATH_.StripData + DateTime.Now.Year.ToString() + "_" + string.Format("{0:00}", DateTime.Now.Month) + "\\";
        if (!Directory.Exists(fn)) Directory.CreateDirectory(fn);

        string fileName = CLOT.GET_LOT.LotID == "" ? "Not LotID" : CLOT.GET_LOT.LotID;
        fileName += ".csv";

        string title1 = "";  
        string title2 = "";
        if (sStripID == null || sStripID == "") {
            if (bBD) title1 = "Simulation";
        }
        else {
            string[] sRslt = sStripID.Split(' ');
            if (sRslt.Length > 1) {
                title1 = sRslt[0];
                title2 = sRslt[1];
            }
            else title1 = sRslt[0];
            
        }
        string logs = /*sStripID*/ title1 + ETC.cspTab + title2 + ETC.cspTab + totalstrip.ToString(); //Unit.ToString() + ETC.cspTab + Good.ToString() + ETC.cspTab + NG.ToString() + ETC.cspTab + XOut.ToString() + ETC.cspTab + ITS.ToString();
        FILE_.WRL_Csv(fn + fileName, logs, true);
    }

    public static void WriteInStrip(string barcode) {
        string fn = PATH_.InStrip + CLOT.GET_LOT.ToolNo + "_" + /*CLOT.GET_LOT.LotID*/CLOT.GET_LOT.ItsID +  "_" + EQPCode + ".txt";
        FILE_.WRL_InStrip(fn, CLOT.GET_LOT.ItsID + "," + barcode, true); //WRL_File
    } // barcode 리딩 후 -> unit picker pic-up 후 
    public static void WriteLotStrip() {
        string fn = PATH_.InStrip + CLOT.GET_LOT.ToolNo + "_" + /*CLOT.GET_LOT.LotID*/CLOT.GET_LOT.ItsID + "_" + EQPCode + ".txt";
        if (File.Exists(fn)) {
            string[] lines = File.ReadAllLines(fn);
            //foreach(string line in lines) {
            //    string[] parts = line.Split(',');
            //
            //}
            if (lines.Length > 0) {
                //lines[lines.Length] = CLOT.GET_LOT.ItsID + ",EOL";
                File.Delete(fn);
                string logs = "";
                for (int n = 0; n < lines.Length; n++) {
                    logs += lines[n] + '\n';
                }
                logs += CLOT.GET_LOT.ItsID + ",EOL";
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                fn = PATH_.LotStrip + CLOT.GET_LOT.ToolNo + "_" + /*CLOT.GET_LOT.LotID*/CLOT.GET_LOT.ItsID + "_" + EQPCode + "_" + timestamp + ".txt";
                FILE_.WRL_InStrip(fn, logs, true);
            }
        } // 파일 존재함!.
    } //LOT-END 클릭 시 !!

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