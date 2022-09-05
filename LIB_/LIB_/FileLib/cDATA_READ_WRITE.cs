using LIB_.DateType;
using Object;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

public class TEACH_ : DATA_
{
    public static int iOldValue = 0;
    public static double dOldValue = 0;
    public static int newStatus = 0;
    public static int oldStatus = 0;
 
    public static void WRTIE_PRE_ALIGN(double x, double y, double t){
        string sWR = x.ToString() + "," + y.ToString() + "," + t.ToString() + ";";
        FILE_.WR_ASCLL_FILE(PATH_.PreAlign, sWR);
    }
    public static void READ_REPICK_ALIGN(ref double x, ref double y){
        StreamReader RePicXY = new StreamReader(PATH_.RePickAlign);
        string mStr = RePicXY.ReadLine();
        string[] aStr = mStr.Split(';');
        x = Convert.ToInt16(aStr[0]); //x 피치 값
        y = Convert.ToInt16(aStr[1]); //y 피치 값
        RePicXY.Close();
    }

    public static void WRITE_TRAIN(string mLastDev){
        string strVISION = mLastDev;
        FILE_.WR_ASCLL_FILE(PATH_.TRAIN, strVISION);
    }

    public static void WRITE_INFO_UNIT_SIZE(double x, double y, double px, double py){
        string sWR = x.ToString() + "," + y.ToString() + ";";
        FILE_.WR_ASCLL_FILE(PATH_.UnitSize, sWR);

        sWR = px.ToString() + "," + py.ToString() + ";";
        FILE_.WR_ASCLL_FILE(PATH_.UnitPitch, sWR);
    }

    public static void WRITE_INFO_PCB_TYPE(int TYPE){
        string str = TYPE.ToString();
        FILE_.WR_ASCLL_FILE(PATH_.PCBTYPE, str);
    }

    public static void WRITE_INFO_MAP_BLOCK(int GX, int GY, int MB1_X, int MB1_Y, int MB2_X, int MB2_Y){
        string sMessage = GX.ToString() + "," + GY.ToString() + "," + MB1_X.ToString() + "," + MB1_Y.ToString() + ";";
        FILE_.WR_ASCLL_FILE(PATH_.MapBlock1, sMessage);
        sMessage = GX.ToString() + "," + GY.ToString() + "," + MB2_X.ToString() + "," + MB2_Y.ToString() + ";";
        FILE_.WR_ASCLL_FILE(PATH_.MapBlock2, sMessage);
    }

    public static void WRITE_INFO_STIP_BARCODE(string Barcode){
        FILE_.WR_ASCLL_FILE(PATH_.BARCODE, Barcode);
    }

    public static void WRITE_INFO_MAPBLOCK_STIP_BARCODE(eMAP_BLOCK Stage, string Barcode){
        string path = Stage == eMAP_BLOCK.STAGE1 ? PATH_.MapBlock1StripBarcode : PATH_.MapBlock2StripBarcode;
        FILE_.WR_ASCLL_FILE(path, Barcode);
    }

    public static void WR_NewJobFile(){ // 설비 레스피 적아 두기
        mNotTeachSave = true;
        for (int i = 0; i < CNT_.MDLPARA; i++) WR_MDLPara(i, prMODEL[i]);
        for (int i = 0; i < CNT_.MT; i++){
            for (int j = 0; j < CNT_.POS; j++) { 
                WR_MTDATA(i, j); 
            }
        }
        mNotTeachSave = false;
    } // 설비 레스피 적아 두기

    public static void SaveITSInfo(string fName, string sLogs){
        //string fn = LogWR_.GET_DirNameDate(PATH_.LogITSInfo) + fName + "_" + string.Format("{0:00}", DateTime.Now.Hour) + string.Format("{0:00}", DateTime.Now.Minute) + string.Format("{0:00}", DateTime.Now.Second) + ".txt";
        string s = PATH_.LogITSInfo + DateTime.Now.Year.ToString() + "_" + string.Format("{0:00}", DateTime.Now.Month) + "\\";
        if (!Directory.Exists(s)) Directory.CreateDirectory(s);
        string fn = s + fName/* + "_" + string.Format("{0:00}", DateTime.Now.Hour) + string.Format("{0:00}", DateTime.Now.Minute) + string.Format("{0:00}", DateTime.Now.Second) */ + ".txt";
        try{
            FILE_.WR_File(fn, sLogs, false);

            //FileStream fs = new FileStream(fn, FileMode.Append);
            //StreamWriter sw = new StreamWriter(fs);
            //string[] sRslt = sLogs.Split(',');
            //for (int n = 0; n < sRslt.Length; n++){
            //    sw.WriteLine(sRslt[n]);
            //}
            //sw.Flush();
            //sw.Close();
        }
        catch (Exception E) { LogWR_.SaveLogException("SAVE ITS WRITE FAIL", E); }
    }

    public static void SaveStripDefectCount(string sITSID, string sLogs){
        string fn = LogWR_.GET_DirNameDate(PATH_.LogStripDefectCount) + sITSID + ".TXT";
        try{
            if (Directory.Exists(fn)) return;
            FILE_.WR_File(fn, sLogs, false);
        }
        catch (Exception e) { LogWR_.SaveLogException("SAVE STRIP DEFECT COUNT WRITE FAIL", e); }
    }
    public static void SaveStripDefectLocationList(string sITSID, string sLogs){
        string fn = LogWR_.GET_DirNameDate(PATH_.LogStripDefectLocationList) + sITSID + ".TXT";
        try{
            if (Directory.Exists(fn)) return;
            FILE_.WR_File(fn, sLogs, false);
        }
        catch (Exception e) { LogWR_.SaveLogException("SAVE STRIP DEFECT LOCATION LIST WRITE FAIL", e); }
    }

    public static void SaveITS_StripCount(string sData){
        FILE_.WR_File(PATH_.ITSCount, sData, false);
    }
    public static void SaveITS_StripLocation(string sData){
        FILE_.WR_File(PATH_.ITSLocation, sData, false);
    }

    public static void SAVE_DAY_COUNT(ref long CntMGZ, ref long CntStrip, ref long CntGood, ref long CntRework, ref long CntReject){
        string sTitle = "MAGAZINE,STRIP,GOOD,REWORK,REJECT" + ETC.CrLf;
        string sVal = CntMGZ.ToString() + "," + CntStrip.ToString() + "," + CntGood.ToString() + "," + CntRework.ToString() + "," + CntReject.ToString();
        try{
            string fn = LogWR_.GET_DirNameDate(PATH_.LogCount) + "_" + string.Format("{0:00}", DateTime.Now.Hour) + string.Format("{0:00}", DateTime.Now.Minute) + string.Format("{0:00}", DateTime.Now.Second) + "_COUNT.csv";
            FILE_.WR_File(fn, sTitle + sVal, false);
        }
        catch (Exception E) { LogWR_.SaveLogException("SAVE DAY COUNT FAIL", E); }
        RESET_DAY_COUNT(ref CntMGZ, ref CntStrip, ref CntGood, ref CntRework, ref CntReject);
    }
    public static void RESET_DAY_COUNT(ref long CntMGZ, ref long CntStrip, ref long CntGood, ref long CntRework, ref long CntReject){
        CntMGZ = 0;
        CntStrip = 0;
        CntGood = 0;
        CntRework = 0;
        CntReject = 0;
    }

    public static void SAVE_VISION_RESULT_WRITE_FAIL(int nPALLET, string[] sLOG){
        try{
            string sVal = "PALLET NUM : " + nPALLET.ToString() + ETC.NewLine;
            for (int i = 0; i < sLOG.Length; i++){
                sVal += sLOG[i];// + ETC.NewLine;
            }
            string fn = LogWR_.GET_DirNameDate(PATH_.LogVisionWriteFailData) + "_" + string.Format("{0:00}", DateTime.Now.Hour) + string.Format("{0:00}", DateTime.Now.Minute) + string.Format("{0:00}", DateTime.Now.Second) + "_VISION_DATA.csv";
            FILE_.WR_File(fn, sVal, false);
        }
        catch (Exception e) { LogWR_.SaveLogException("SAVE VISION DATA WRITE FAIL", e); }
    }

    //                                        LotCnt, StripCnt, UnitCnt, GoodCnt, ReworkCnt, NGCnt, GoodTrayCnt, ReworkTrayCnt, EmptyTrayCnt, InCnt, OutCnt
    //public static int[] Production      = { LotCnt, StripCnt, UnitCnt, GoodCnt, ReworkCnt, NGCnt, GoodTrayCnt, ReworkTrayCnt, EmptyTrayCnt, InCnt OutCnt };
    public static void SaveLotInfoCount(int[] LotInfo){
        if (LotInfo == null) return;
        string fn = LogWR_.GET_DirNameDate(PATH_.LogLotInfo) + string.Format("{0:00}", DateTime.Now.Hour) + string.Format("{0:00}", DateTime.Now.Minute) + string.Format("{0:00}", DateTime.Now.Second) + "_" + CLOT.GET_LOT.LotID + ".csv";
        //string s = SUBFRM_.gLotID.OldLotInfo + ",";
        //for (int i = 0; i < LotInfo.Length; i++){
        //    s += IsLONG[LotInfo[i]];
        //    if (i == LotInfo.Length - 1){
        //        s += ETC.NewLine;
        //        break;
        //    }
        //    s += ",";
        //}
        //FILE_.WR_File(fn, s, true);
        try{
            FileStream fs = new FileStream(fn, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            string s = "LOT, STRIP LOADING, STRIP IN, GOOD UNIT, REWORK UNIT, NG UTNIT, GOOD TRAY, REWORK TRAY, EMPTY TRAY, UNIT PICK-UP, UNIT PLACE, ITS COUNT";
            sw.WriteLine(s);
            s = "";
            for (int i = 0; i < LotInfo.Length; i++){
                s += IsLONG[LotInfo[i]];
                if (i == LotInfo.Length - 1) break;
                s += ",";
            }
            sw.WriteLine(s);
            sw.Flush();
            sw.Close();
        }
        catch (Exception e){
            LogWR_.SaveLogException("WRITE LOT INFO FAIL [" + CLOT.GET_LOT.LotID + "]", e);
        }
    }

    public static void SAVE_COUNT(long cLOT, long cSTRIP, long cUNIT, long cGOOD, long cREWORK, long cNG, long cGOODTRAY, long cREWORKTRAY, long cTOTAL, long cITS){
        try{
            StreamWriter sw = new StreamWriter(PATH_.COUNT, false, Encoding.UTF8);
            sw.WriteLine(cLOT);
            sw.WriteLine(cSTRIP);
            sw.WriteLine(cUNIT);
            sw.WriteLine(cGOOD);
            sw.WriteLine(cREWORK);
            sw.WriteLine(cNG);
            sw.WriteLine(cGOODTRAY);
            sw.WriteLine(cREWORKTRAY);
            sw.WriteLine(cTOTAL);
            sw.WriteLine(cITS);
            sw.Close();
        }
        catch (Exception ex) { LogWR_.SaveLogException("SAVE COUNT FAIL", ex); }
    }
    public static void LOAD_COUNT(ref long cntLOT, ref long cntSTRIP, ref long cntUNIT, ref long cntGOOD, ref long cntREWORK, ref long cntNG, ref long cntGOODTRAY, ref long cntREWORKTRAY, ref long cntTOTAL, ref long cntITS){
        try{
            string[] sLine = File.ReadAllText(PATH_.COUNT).Split(ETC.CrLf);
            if (sLine.Length - 1 >= 9){
                cntLOT          = long.Parse(sLine[0].Replace("\r", ""));
                cntSTRIP        = long.Parse(sLine[1].Replace("\r", ""));
                cntUNIT         = long.Parse(sLine[2].Replace("\r", ""));
                cntGOOD         = long.Parse(sLine[3].Replace("\r", ""));
                cntREWORK       = long.Parse(sLine[4].Replace("\r", ""));
                cntNG           = long.Parse(sLine[5].Replace("\r", ""));
                cntGOODTRAY     = long.Parse(sLine[6].Replace("\r", ""));
                cntREWORKTRAY   = long.Parse(sLine[7].Replace("\r", ""));
                cntTOTAL        = long.Parse(sLine[8].Replace("\r", ""));
                cntITS          = long.Parse(sLine[9].Replace("\r", ""));
            }
        }
        catch (Exception ex){
            LogWR_.SaveLogException("lOAD COUNT FAIL", ex);
            cntLOT = 0;
            cntSTRIP = 0;
            cntUNIT = 0;
            cntGOOD = 0;
            cntREWORK = 0;
            cntNG = 0;
            cntGOODTRAY = 0;
            cntREWORKTRAY = 0;
            cntTOTAL = 0;
        }
    }

    public static void SAVE_INFO_COUNT(long CntMGZ, long CntStrip, long CntGoodUnit, long CntRework, long CntReject){
        try
        {
            StreamWriter sw = new StreamWriter(PATH_.DAY_COUNT, false, Encoding.UTF8);
            sw.WriteLine(CntMGZ);
            sw.WriteLine(CntStrip);
            sw.WriteLine(CntGoodUnit);
            sw.WriteLine(CntRework);
            sw.WriteLine(CntReject);
            sw.Close();
        }
        catch (Exception ex) { LogWR_.SaveLogException("SAVE DAY COUNT INFO FAIL", ex); }
    }
    public static void LOAD_INFO_COUNT(ref long CntMGZ, ref long CntStrip, ref long CntGoodUnit, ref long CntReWork, ref long CntReject){
        try
        {
            string[] sLine = File.ReadAllText(PATH_.DAY_COUNT).Split(ETC.CrLf);
            if (sLine.Length - 1 >= 4)
            {
                CntMGZ = long.Parse(sLine[0].Replace("\r", ""));
                CntStrip = long.Parse(sLine[1].Replace("\r", ""));
                CntGoodUnit = long.Parse(sLine[2].Replace("\r", ""));
                CntReWork = long.Parse(sLine[3].Replace("\r", ""));
                CntReject = long.Parse(sLine[4].Replace("\r", ""));
            }
        }
        catch (Exception ex) {
            LogWR_.SaveLogException("LOAD DAY COUNT INFO FAIL", ex);
            CntMGZ = 0;
            CntStrip = 0;
            CntGoodUnit = 0;
            CntReWork = 0;
            CntReject = 0;
        }
    }

    public static void RD_GRD_DATA(string FileName, DataGridView g){
        string mStr;
        short i = 0;
        try{
            StreamReader sr = new StreamReader(FileName);
            g.RowCount = Convert.ToUInt16(sr.ReadLine());
            while (sr.Peek() != -1){
                mStr = sr.ReadLine().Trim();
                if (string.IsNullOrEmpty(mStr) || mStr.Length < 3) break;
                string[] strArray = mStr.Split(',');
                g[0, i].Value = strArray[0].Trim();
                g[0, i].Style.BackColor = Color.FromName(strArray[1]);
                i++;
            }
            sr.Close();
        }
        catch (Exception ex) { MessageBox.Show("RD_GRID_FAIL" + ETC.NewLine + ex.ToString()); }
    }

    public static Color COLOR_CODE(eSTATUS sts) { return Color.FromName(C_CODE[(short)sts]); }
    public static void Read_Color(){
        string mSTR;
        short i = 0;
        try{
            StreamReader sr = new StreamReader(PATH_.UNIT_COLOR);
            while (sr.Peek() != -1){
                mSTR = sr.ReadLine().Trim();
                if (string.IsNullOrEmpty(mSTR) || mSTR.Length < 3) break;
                string[] strARRAY = mSTR.Split(',');
                C_CODE[i] = strARRAY[1].Trim();
                i++;
            }
            sr.Close();
        }
        catch (Exception e) { MessageBox.Show("READ COLOR CODE FAIL" + ETC.NewLine + e.ToString()); }
    }

    #region "RD/WR USE PICKER Z"
    //설비 파라로
    public static void Read_SkipPicker(){
        short i = 0;
        try{
            StreamReader sr = new StreamReader(PATH_.USE_PKR_Z);
            while (sr.Peek() != -1){
                ARR_[i, 0] = sr.ReadLine() == "F" ? eSTATUS.NONE : eSTATUS.EMPTY;
                i++;
            }
            sr.Close();
            for (short j = 0; j < (CNT_.PKR * 2); j++){
                PK_Z[j] = ARR_[j, 0];
            }
        }
        catch (Exception e) { MessageBox.Show("READ_USE_SKIP_PK FAIL." + ETC.CrLf + e.ToString()); }
    }
    public static bool WR_USE_SKIP_PK(){
        try{
            StreamWriter sw = new StreamWriter(PATH_.USE_PKR_Z, false, Encoding.UTF8);
            for (short i = 0; i < (CNT_.PKR * 2); i++){
                sw.WriteLine(PK_Z[i] == eSTATUS.NONE ? "F" : "T");
            }
            sw.Close();
            return true;
        }
        catch (Exception e) { MessageBox.Show("WRITE_USE_SKIP_PK FAIL" + ETC.CrLf + e.ToString()); }
        return false;
    }

    //모델 파라로
    public static void Read_UsePicker(){
        if (!File.Exists(sCurrJobName)) return;

        for (int i = 0; i < CNT_.PKR * CNT_.HEAD; i++){
            ARR_[i, 0] = (eSTATUS)FILE_.RDInt(sCurrJobName, "USE PICKER", "PK_NUM" + i.ToString(), 0);
            PK_Z[i] = ARR_[i, 0];
        }
    }
    public static bool WR_UsePKR(int Picker, eSTATUS Status){
        int nHD = (Picker / CNT_.PKR) + 1;
        int nPK = (Picker % CNT_.PKR) + 1;
        eSTATUS eOLD = PK_Z[Picker];
        if (Status != eOLD){
            PK_Z[Picker] = Status;
            FILE_.WRInt(sCurrJobName, "USE PICKER", "PK_NUM" + Picker.ToString(), (int)PK_Z[Picker]);
            LogWR_.SAVE_ChangeDataEvent("[HEAD" + nHD.ToString() + "] PICKER_" + nPK.ToString() + " : " + eOLD.ToString() + " -> " + Status.ToString());
        }
        return true;
    }
    #endregion "RD/WR USE PICKER Z";

    #region "SAW 데이터 전달"
    public static double READ_OFFSET_FILE(){
        string mStr;
        double dRtn;
        try{
            if (!File.Exists(PATH_.PathOffset)) return 0;
            StreamReader sr = new StreamReader(PATH_.PathOffset);
            mStr            = sr.ReadLine();
            dRtn            = double.Parse(mStr);
            if (Math.Abs(dRtn) > 1) dRtn = 0;
        }
        catch (Exception ex){
            LogWR_.SaveLogException("Offset File Read Fail", ex);
            return 0;
        }
        //DEL_OFFSET_FILE();
        return dRtn;
    } // SAW에서 픽업 옵셋값 가져오기
    public static void DEL_OFFSET_FILE(){
        if (File.Exists(PATH_.PathOffset)) File.Delete(PATH_.PathOffset); //OFFSET FILE 있으면 삭제함.
    } // SAW가 전달한 픽업 옵셋값 지우기/

    public static void WRITE_BARCODE_FILE(string sBARCODE){
        try{
            FILE_.WR_File(PATH_.PathBarCode, sBARCODE, true);
        }
        catch (Exception exp){
            LogWR_.SaveLogException("WRITE BARCODE FILE FAIL", exp);
            return;
        }
    } // 바코드 정보 공유폴더에 작성.
    public static void DEL_BARCODE_FILE(){
        if (File.Exists(PATH_.PathBarCode)) File.Delete(PATH_.PathBarCode); //FILE 있으면 삭제함.
    } // 바코드 저장 파일 삭제
    #endregion "SAW 데이터 전달"

    public static void DEL_STRIP_INFO(){
        if (File.Exists(PATH_.StripOverlap)) File.Delete(PATH_.StripOverlap); //OFFSET FILE 있으면 삭제함.
    }
    public static void WRITE_STRIP_INFO(string StripInfo){
        try{
            FILE_.WRL_File(PATH_.StripOverlap, StripInfo, true);
        }
        catch (Exception exp) {
            LogWR_.SaveLogException("WRITE STRIP INFO FAIL", exp);
            return;
        }
    }
    public static string READ_STRIP_INFO(){
        string mStr;
        try{
            if (!File.Exists(PATH_.StripOverlap)) return "";
            StreamReader sr = new StreamReader(PATH_.StripOverlap);
            mStr = sr.ReadLine();
        }
        catch (Exception ex){
            LogWR_.SaveLogException("Offset File Read Fail", ex);
            return "";
        }
        return mStr;
    }

    #region "RD/WR 설비 디바이스"
    public static string RD_DEIVCE_ID(){
        if (!File.Exists(PATH_.DeviceID)) return "";
        string s = File.ReadAllText(PATH_.DeviceID);
        return s;
    }
    public static bool WR_DEVICE_ID(string sDeviceID){
        if (sDeviceID == sDEVICE_ID) return false;
        sDEVICE_ID = sDeviceID;
        FILE_.WR_File(PATH_.DeviceID, sDEVICE_ID, false);
        return true;
    }

    public static string RD_LOTINFO(){
        if (!File.Exists(PATH_.LotInfo)) return "";
        string s = File.ReadAllText(PATH_.LotInfo);
        return s;
    }
    public static bool WR_LOTINFO(string LOT_LIST){
        FILE_.WR_File(PATH_.LotInfo, LOT_LIST, false);
        return true;
    }

    public static string RD_PPID(string fn){
        string sPath = PATH_.PPID + fn + ".txt";
        if (!File.Exists(sPath)) return "";
        string s = File.ReadAllText(sPath);
        return s;
    }
    public static bool WR_PPID(string fn, string sPPID_LIST){
        string sPaht = PATH_.PPID + fn + ".txt";
        if (File.Exists(sPaht)){
            File.Delete(sPaht);
            UTIL_.DELAY(100);
        }//파일 존해하여 삭제 후 다시 생성함.(모디파이 할 경우만 적용함)
        FILE_.WR_File(sPaht, sPPID_LIST, true);
        return true;
    }
    public static bool DEL_PPID(string fn){
        string sPaht = PATH_.PPID + fn + ".txt";
        File.Delete(sPaht);
        return true;
    }
    #endregion "RD/WR 설비 디바이스"

    #region "RD/WR LOGIN PASSWORD"
    public static bool Read_LoginPassword(){
        int IDX = 0;
        StreamReader sr = new StreamReader(PATH_.PASSWORD, Encoding.UTF8);

        try{
            while (sr.Peek() != -1){
                string rl = sr.ReadLine();
                string[] sTEMP = rl.Split(':');
                switch (IDX){
                    case 0:
                        stPassWord.OP = sTEMP[1];
                        break;
                    case 1:
                        stPassWord.ENG = sTEMP[1];
                        break;
                    case 2:
                        stPassWord.SUP = sTEMP[1];
                        break;
                    case 3:
                        stPassWord.SOFT = sTEMP[1];
                        break;
                }
                IDX++;
            }
        }
        catch (Exception ex) { MessageBox.Show("LOGIN PASSWORD READ FAIL !" + ETC.CrLf + ex.ToString()); }
        return false;
    }
    public static bool WR_LoginPassword(){
        StreamWriter sw = new StreamWriter(PATH_.PASSWORD);
        try{
            sw.WriteLine("OP:" + "");
            string sPassWord = stPassWord.ENG ?? "";
            sw.WriteLine("ENG:" + sPassWord.Trim());
            sPassWord = stPassWord.SUP ?? "";
            sw.WriteLine("ADMIN:" + sPassWord.Trim());
            sPassWord = stPassWord.SOFT ?? "";
            sw.WriteLine("SOFT:" + sPassWord.Trim());
            sw.Close();
            return true;
        }
        catch (Exception ex){
            MessageBox.Show("LOGIN PASSWORD WRITE FAIL !" + ETC.CrLf + ex.ToString());
            sw.Close();
        }
        return false;
    }
    #endregion "RD/WR LOGIN PASSWORD"

    #region "RD/WR TOWER LAMP STATUE"
    public static bool Read_TowerLamp(){
        int idx = 0;
        if (!File.Exists(PATH_.TOWERLAMP)){
            MessageBox.Show("NOT FIND TOWER LAMP DATA FILE!");
            return false;
        }
        StreamReader sw = new StreamReader(PATH_.TOWERLAMP, Encoding.UTF8);
        try{
            while (sw.Peek() != -1){
                string input = sw.ReadLine();
                string[] mTemp = input.Split(':');
                switch (idx){
                    case 0:
                        DataTowerStatus[idx] = mTemp[1];
                        BackupTowerStatus[idx] = DataTowerStatus[idx];
                        break;
                    case 1:
                        DataTowerStatus[idx] = mTemp[1];
                        BackupTowerStatus[idx] = DataTowerStatus[idx];
                        break;
                    case 2:
                        DataTowerStatus[idx] = mTemp[1];
                        BackupTowerStatus[idx] = DataTowerStatus[idx];
                        break;
                    case 3:
                        DataTowerStatus[idx] = mTemp[1];
                        BackupTowerStatus[idx] = DataTowerStatus[idx];
                        break;
                    case 4:
                        DataTowerStatus[idx] = mTemp[1];
                        BackupTowerStatus[idx] = DataTowerStatus[idx];
                        break;
                    case 5:
                        DataTowerStatus[idx] = mTemp[1];
                        BackupTowerStatus[idx] = DataTowerStatus[idx];
                        break;
                    case 6:
                        DataTowerStatus[idx] = mTemp[1];
                        BackupTowerStatus[idx] = DataTowerStatus[idx];
                        break;
                    case 7:
                        DataTowerStatus[idx] = mTemp[1];
                        BackupTowerStatus[idx] = DataTowerStatus[idx];
                        break;
                }
                idx++;
            }
            return true;
        }
        catch (Exception ex) { MessageBox.Show("RD TOWERLAMP FAIL !" + ETC.CrLf + ex.ToString()); }
        return false;
    }
    public static bool WR_TowerLamp(){
        StreamWriter sw = new StreamWriter(PATH_.TOWERLAMP);
        try{
            sw.WriteLine("RUN:" + DataTowerStatus[0]);
            sw.WriteLine("STOP:" + DataTowerStatus[1]);
            sw.WriteLine("INITIALIZING:" + DataTowerStatus[2]);
            sw.WriteLine("WAIT RUN:" + DataTowerStatus[3]);
            sw.WriteLine("ERROR:" + DataTowerStatus[4]);
            sw.WriteLine("MANUAL:" + DataTowerStatus[5]);
            sw.WriteLine("LOT-END:" + DataTowerStatus[6]);
            sw.WriteLine("WARNING:" + DataTowerStatus[7]);
            sw.Close();
            return true;
        }
        catch (Exception ex){
            MessageBox.Show("LOGIN PASSWORD WRITE FAIL = " + ex.ToString());
            sw.Close();
        }
        return false;
    }
    #endregion "RD/WR TOWER LAMP STATUE"

    #region "RD/WR ANALOG & IO"
    public static void Read_AnalogLabel(){
        string sSTR;
        short i = 0;
        StreamReader sr = new StreamReader(PATH_.AnalogName);
        try{
            while (sr.Peek() != -1){
                if (AI_NAME.Length < i) break;
                sSTR        = sr.ReadLine();
                AI_NAME[i]  = sSTR.Trim();
                i++;
            }
        }
        catch (Exception ex){
            sr.Close();
            MessageBox.Show("READ ANALOG LABEL FAIL !" + ETC.NewLine + ex.ToString());
        }
    }

    public static bool Read_Analog(){
        short i = 0;
        try{
            StreamReader ReadFile = new StreamReader(PATH_.ANALOG);
            while (ReadFile.Peek() != -1){
                mSET_AI[i] = Convert.ToDouble(ReadFile.ReadLine().Trim());
                i++;
            }
            ReadFile.Close();
        }
        catch (Exception ex) { MessageBox.Show("READ ANALOG DATA FAIL" + ETC.NewLine + ex.ToString()); }
        return false;
    }

    public static bool WR_Analog(){
        try{
            StreamWriter writeFile = new StreamWriter(PATH_.ANALOG, false, System.Text.Encoding.UTF8);
            for (int i = 0; i < mSET_AI.Length; i++) writeFile.WriteLine(mSET_AI[i]);
            writeFile.Close();
            return true;
        }
        catch (Exception ex) { MessageBox.Show("WRITE ANALOG DATA FAIL" + ETC.NewLine + ex.ToString()); }
        return false;
    }

    public static void Read_InputLabel(){
        string mSTR;
        short i = 0;
#if _NSS3300
        StreamReader sr = new StreamReader(PATH_.inputLabel3300);
#else
        StreamReader sr = new StreamReader(PATH_.inputLabel);
#endif

        try
        {
            while (sr.Peek() != -1){
                if (InputName.Length < i) break;
                mSTR = sr.ReadLine();
                if (CNT_.InSortEnd < i) { InputName[(i - (CNT_.InSortEnd + 1)) + CNT_.InSawStart] = mSTR.Trim(); }
                else                    { InputName[i] = mSTR.Trim(); }
                i++;
            }
            sr.Close();
        }
        catch (Exception ex){
            sr.Close();
            MessageBox.Show("READ INPUT LABEL FAIL !" + ETC.NewLine + ex.ToString());
        }
    }
    public static void Read_OutputLabel(){
        string mSTR;
        short i = 0;
#if _NSS3300
        StreamReader sr = new StreamReader(PATH_.outputLabel3300);
#else
        StreamReader sr = new StreamReader(PATH_.outputLabel);
#endif
        try{
            while (sr.Peek() != -1){
                if (OutputName.Length < i) break;
                mSTR = sr.ReadLine();
                //OutputName[i] = mSTR.Trim();
                if (CNT_.OutSortEnd < i)    { OutputName[(i - (CNT_.OutSortEnd + 1)) + CNT_.OutSawStart] = mSTR.Trim(); }
                else                        { OutputName[i] = mSTR.Trim(); }
                i++;
            }
            sr.Close();
        }
        catch (Exception ex){
            sr.Close();
            MessageBox.Show("READ OUTPUT LABEL FAIL !" + ETC.NewLine + ex.ToString());
        }
    }

    public static void Read_InfoIO(){
        if (!File.Exists(PATH_.InfoINPUT)){
            for (int i = 0; i < CNT_.IN; i++){
                chkIN[i].ContactB   = false;
                chkIN[i].Checked    = false;
                chkIN[i].Virtual    = false;
            }
        }
        else{
            string[] iArr = File.ReadAllLines(PATH_.InfoINPUT);
            for (short i = 0; i < iArr.Length - 1; i++){
                string[] subArr     = iArr[i].Split(',');
                chkIN[i].ContactB   = bool.Parse(subArr[0].Trim());
                chkIN[i].Checked    = bool.Parse(subArr[1]);
                chkIN[i].Virtual    = bool.Parse(subArr[2]);
            }
        }

        if (!File.Exists(PATH_.InfoOUTPUT)){
            for (int i = 0; i < CNT_.OUT; i++){
                chkOUT[i].ContactB  = false;
                chkOUT[i].Checked   = false;
                chkOUT[i].Virtual   = false;
            }
        }
        else{
            string[] oArr = File.ReadAllLines(PATH_.InfoOUTPUT);
            for (short i = 0; i < oArr.Length - 1; i++){
                string[] subArr = oArr[i].Split(',');
                chkOUT[i].ContactB  = bool.Parse(subArr[0].Trim());
                chkOUT[i].Checked   = bool.Parse(subArr[1]);
                chkOUT[i].Virtual   = bool.Parse(subArr[2]);
            }
        }
    }

    public static void WR_InfoIO(){
        string sIN = ""; string sOUT = "";
        for (short i = 0; i < CNT_.IN; i++){
            sIN += chkIN[i].ContactB.ToString() + ",";
            sIN += chkIN[i].Checked.ToString() + ",";
            sIN += chkIN[i].Virtual.ToString() + ",";
            sIN += ETC.CrLf;
        }
        Microsoft.VisualBasic.FileIO.FileSystem.WriteAllText(PATH_.InfoINPUT, sIN, false);

        for (short i = 0; i < CNT_.OUT; i++){
            sOUT += chkOUT[i].ContactB.ToString() + ",";
            sOUT += chkOUT[i].Checked.ToString() + ",";
            sOUT += chkOUT[i].Virtual.ToString() + ",";
            sOUT += Microsoft.VisualBasic.Constants.vbCrLf;
        }
        Microsoft.VisualBasic.FileIO.FileSystem.WriteAllText(PATH_.InfoOUTPUT, sOUT, false);
    }
#endregion "RD/WR ANALOG & IO"

#region "RD/WR PARAMETERS"
    public static void Write_Parameter(NumericUpDown nup){
        int value = (int)nup.Value;
        if (nup.Tag.ToString() == "MC" || nup.Tag.ToString() == "mc")   WR_MCPara(nup.TabIndex, value);
        else                                                            WR_MDLPara(nup.TabIndex, value);
    }
    public static void Write_Parameter(Label lbl){
        double value = double.Parse(lbl.Text);
        if (lbl.Tag.ToString() == "MC" || lbl.Tag.ToString() == "mc")   WR_MCPara(lbl.TabIndex, value);
        else                                                            WR_MDLPara(lbl.TabIndex, value);
    }
    public static void Write_Parameter(JCS.ToggleSwitch ts){
        int value = ts.Checked ? 1 : 0;
        if (ts.Tag.ToString() == "MC" || ts.Tag.ToString() == "mc")     WR_MCPara(ts.TabIndex, value);
        else                                                            WR_MDLPara(ts.TabIndex, value);
    }
    public static void Write_Parameter(int nPara, string Para, RadioButton[] rbtn){
        int value = 0;
        for (int i = 0; i < rbtn.Length; i++){
            if (rbtn[i].Checked) value = i;
        }
        if (Para == "MC" || Para == "mc")   WR_MCPara(nPara, value);
        else                                WR_MDLPara(nPara, value);
    }

    public static void Write_MachinePara(int nPara, double dValue)  { WR_MCPara(nPara, dValue); }
    public static void Write_ModelPara(int nPara, double dValue)    { WR_MDLPara(nPara, dValue); }

    public static void Read_MachineParaLabel(){
        string mSTR;
        short n;
        try{
            StreamReader sr = new StreamReader(PATH_.MCName);
            while (sr.Peek() != -1){
                mSTR = sr.ReadLine();
                if (mSTR.Length > 1){
                    string[] sARR = mSTR.Split(',');
                    if (sARR.Length > 1){
                        n               = Convert.ToInt16(sARR[0]);
                        MCParaName[n]   = sARR[1].Trim();
                    }
                }
            }
            sr.Close();
        }
        catch (Exception ex) { MessageBox.Show("READ_MC_PARA_LABEL FAIL" + ETC.NewLine + ex.ToString()); }
    }
    public static void Read_ModelParaLabel(){
        string mSTR;
        short n;
        try{
            StreamReader sr = new StreamReader(PATH_.MDName);
            while (sr.Peek() != -1){
                mSTR = sr.ReadLine();
                if (mSTR.Length > 1){
                    string[] sARR = mSTR.Split(',');
                    if (sARR.Length >= 2){
                        n               = Convert.ToInt16(sARR[0]);
                        MDParaName[n]   = sARR[1].Trim();
                    }
                }
            }
            sr.Close();
        }
        catch (Exception ex) { MessageBox.Show("READ_MD_PARA_LABEL FAIL" + ETC.NewLine + ex.ToString()); }
    }

    public static void Read_MachinePara(){
        if (!File.Exists(PATH_.COMMON)) return;
        for (int i = 0; i < CNT_.MCPARA; i++) { 
            prMACHINE[i] = FILE_.RDDouble(PATH_.COMMON, "PARAMETER", "MCPARA_" + i.ToString(), 0.0); 
        }
    }
    public static void RD_SelectMCPara(int iPARA){
        if (!File.Exists(PATH_.COMMON)) return;
        prMACHINE[iPARA] = FILE_.RDDouble(PATH_.COMMON, "PARAMETER", "MCPARA_" + iPARA.ToString(), 0.0);
    }
    public static bool WR_MCPara(int iPARA, double dVALUE){
        if (prMACHINE[iPARA] != dVALUE){
            dOldValue           = prMACHINE[iPARA];
            prMACHINE[iPARA]    = dVALUE;
            FILE_.WRDouble(PATH_.COMMON, "PARAMETER", "MCPARA_" + iPARA.ToString(), prMACHINE[iPARA]);
            if (!mNotTeachSave) LogWR_.SAVE_ChangeDataEvent("[MCPARA_" + iPARA.ToString() + "]" + MCParaName[iPARA] + " : " + dOldValue.ToString() + " -> " + dVALUE.ToString());
            return true;
        }
        return false;
    }

    public static void RD_MDLPara(){
        if (!File.Exists(sCurrJobName)) return;
        for (int i = 0; i < CNT_.MDLPARA; i++) { 
            prMODEL[i] = FILE_.RDDouble(sCurrJobName, "PARAMETER", "MDLPARA_" + i.ToString(), 0.0); 
        }
    }
    public static void RD_SelectMDLPara(int iPARA){
        if (!File.Exists(sCurrJobName)) return;
        prMODEL[iPARA] = FILE_.RDDouble(sCurrJobName, "PARAMETER", "MDLPARA_" + iPARA.ToString(), 0.0);
    }
    public static bool WR_MDLPara(int iPARA, double dVALUE){
        if (prMODEL[iPARA] != dVALUE){
            dOldValue       = prMODEL[iPARA];
            prMODEL[iPARA]  = dVALUE;
            FILE_.WRDouble(sCurrJobName, "PARAMETER", "MDLPARA_" + iPARA.ToString(), prMODEL[iPARA]);
            if (!mNotTeachSave) LogWR_.SAVE_ChangeDataEvent("[MDLPARA_" + iPARA.ToString() + "]" + MDParaName[iPARA] + " : " + dOldValue.ToString() + " -> " + dVALUE.ToString());
            return true;
        }
        return false;
    }

    public static void Read_PickerOffset(){
        if (!File.Exists(PATH_.COMMON)) return;
        for (int i = 0; i < 16; i++){
            PkOffset[i].x = FILE_.RDDouble(PATH_.COMMON, "PK_OFFSET", "X_" + i.ToString(), 0.0);
            PkOffset[i].y = FILE_.RDDouble(PATH_.COMMON, "PK_OFFSET", "Y_" + i.ToString(), 0.0);
        }
    }
    public static bool WR_PickerOffset(int i, dxy Offset){
        bool bRETUN = false;
        if (PkOffset[i].x != Offset.x){
            dOldValue       = PkOffset[i].x;
            PkOffset[i].x   = Offset.x;
            FILE_.WRDouble(PATH_.COMMON, "PK_OFFSET", "X_" + i.ToString(), PkOffset[i].x);
            if (!mNotTeachSave){
                LogWR_.SAVE_ChangeDataEvent("[PICKER OFFSET] X [" + i.ToString() + "] = " + dOldValue + " -> " + PkOffset[i].x.ToString());
                bRETUN = true;
            }
        }
        if (PkOffset[i].y != Offset.y){
            dOldValue       = PkOffset[i].y;
            PkOffset[i].y   = Offset.y;
            FILE_.WRDouble(PATH_.COMMON, "PK_OFFSET", "Y_" + i.ToString(), PkOffset[i].y);
            if (!mNotTeachSave){
                LogWR_.SAVE_ChangeDataEvent("[PICKER OFFSET] Y [" + i.ToString() + "] = " + dOldValue + " -> " + PkOffset[i].y.ToString());
                bRETUN = true;
            }
        }
        return bRETUN;
    }
    public static void Read_AllPickerOffset(){
        if (!File.Exists(PATH_.COMMON)) return;
        for (int i = 0; i < 16; i++){
            PkOffset_0[i].x     = FILE_.RDDouble(PATH_.COMMON, "PK_OFFSET0", "X_" + i.ToString(), 0.0);
            PkOffset_0[i].y     = FILE_.RDDouble(PATH_.COMMON, "PK_OFFSET0", "Y_" + i.ToString(), 0.0);

            PkOffset_P90[i].x   = FILE_.RDDouble(PATH_.COMMON, "PK_OFFSETP90", "X_" + i.ToString(), 0.0);
            PkOffset_P90[i].y   = FILE_.RDDouble(PATH_.COMMON, "PK_OFFSETP90", "Y_" + i.ToString(), 0.0);

            PkOffset_P180[i].x  = FILE_.RDDouble(PATH_.COMMON, "PK_OFFSETP180", "X_" + i.ToString(), 0.0);
            PkOffset_P180[i].y  = FILE_.RDDouble(PATH_.COMMON, "PK_OFFSETP180", "Y_" + i.ToString(), 0.0);

            PkOffset_P270[i].x  = FILE_.RDDouble(PATH_.COMMON, "PK_OFFSETP270", "X_" + i.ToString(), 0.0);
            PkOffset_P270[i].y  = FILE_.RDDouble(PATH_.COMMON, "PK_OFFSETP270", "Y_" + i.ToString(), 0.0);

            PkOffset_M90[i].x   = FILE_.RDDouble(PATH_.COMMON, "PK_OFFSETM90", "X_" + i.ToString(), 0.0);
            PkOffset_M90[i].y   = FILE_.RDDouble(PATH_.COMMON, "PK_OFFSETM90", "Y_" + i.ToString(), 0.0);

            PkOffset_M180[i].x  = FILE_.RDDouble(PATH_.COMMON, "PK_OFFSETM180", "X_" + i.ToString(), 0.0);
            PkOffset_M180[i].y  = FILE_.RDDouble(PATH_.COMMON, "PK_OFFSETM180", "Y_" + i.ToString(), 0.0);

            PkOffset_M270[i].x  = FILE_.RDDouble(PATH_.COMMON, "PK_OFFSETM270", "X_" + i.ToString(), 0.0);
            PkOffset_M270[i].y  = FILE_.RDDouble(PATH_.COMMON, "PK_OFFSETM270", "Y_" + i.ToString(), 0.0);
        }
    }
    public static bool SavePkrOffset(string sRot, int nPK, dxy OldOffset, dxy NewOffset){
        bool bRTN = false;
        if (OldOffset.x != NewOffset.x){
            dOldValue = OldOffset.x;
            FILE_.WRDouble(PATH_.COMMON, "PK_OFFSET" + sRot, "X_" + nPK, NewOffset.x);
            if (!mNotTeachSave){
                LogWR_.SAVE_ChangeDataEvent("[PICKER OFFSET " + sRot + "] X [" + nPK.ToString() + "] = " + dOldValue + " -> " + NewOffset.x.ToString());
                bRTN = true;
            }
        }
        if (OldOffset.y != NewOffset.y){
            dOldValue = OldOffset.y;
            FILE_.WRDouble(PATH_.COMMON, "PK_OFFSET" + sRot, "Y_" + nPK, NewOffset.y);
            if (!mNotTeachSave){
                LogWR_.SAVE_ChangeDataEvent("[PICKER OFFSET " + sRot + "] Y [" + nPK.ToString() + "] = " + dOldValue + " -> " + NewOffset.y.ToString());
                bRTN = true;
            }
        }
        return bRTN;
    }
    public static bool WR_PickerOffset(string sRot, int nPK, dxy offset){
        bool bRtn = false;
        if (sRot == "0"){
            bRtn = SavePkrOffset(sRot, nPK, PkOffset_0[nPK], offset);
            if (bRtn) PkOffset_0[nPK] = offset;
        }
        else if (sRot == "P90"){
            bRtn = SavePkrOffset(sRot, nPK, PkOffset_P90[nPK], offset);
            if (bRtn) PkOffset_P90[nPK] = offset;
        }
        else if (sRot == "P180"){
            bRtn = SavePkrOffset(sRot, nPK, PkOffset_P180[nPK], offset);
            if (bRtn) PkOffset_P180[nPK] = offset;
        }
        else if (sRot == "P270"){
            bRtn = SavePkrOffset(sRot, nPK, PkOffset_P270[nPK], offset);
            if (bRtn) PkOffset_P270[nPK] = offset;
        }
        else if (sRot == "M90"){
            bRtn = SavePkrOffset(sRot, nPK, PkOffset_M90[nPK], offset);
            if (bRtn) PkOffset_M90[nPK] = offset;
        }
        else if (sRot == "M180"){
            bRtn = SavePkrOffset(sRot, nPK, PkOffset_M180[nPK], offset);
            if (bRtn) PkOffset_M180[nPK] = offset;
        }
        else if (sRot == "M270"){
            bRtn = SavePkrOffset(sRot, nPK, PkOffset_M270[nPK], offset);
            if (bRtn) PkOffset_M270[nPK] = offset;
        }
        return bRtn;
    }
    public static void Read_UnitCleanData(){
        if (!File.Exists(sCurrJobName)) return;
        for (int i = 0; i < 10; i++){
            CleanData.MODE[i]       = FILE_.RDInt(/*mPATH.COMMON*/sCurrJobName, "CLEAN_DATA", "MODE_" + i.ToString(), 0);
            CleanData.COUNTER[i]    = FILE_.RDInt(/*mPATH.COMMON*/sCurrJobName, "CLEAN_DATA", "COUNTER_" + i.ToString(), 0);
            CleanData.TIME[i]       = FILE_.RDInt(/*mPATH.COMMON*/sCurrJobName, "CLEAN_DATA", "TIME_" + i.ToString(), 0);
        }
    }
    public static bool WR_CLEAN_DATA(int iNUM, int iModeValue, int iCount, int iTime){
        bool bRETUN = false;
        if (CleanData.MODE[iNUM] != iModeValue){
            iOldValue               = CleanData.MODE[iNUM];
            CleanData.MODE[iNUM]    = iModeValue;
            FILE_.WRInt(/*mPATH.COMMON*/sCurrJobName, "CLEAN_DATA", "MODE_" + iNUM.ToString(), CleanData.MODE[iNUM]);
            if (!mNotTeachSave){
                LogWR_.SAVE_ChangeDataEvent("[CLEAN_DATA] MODE " + iNUM.ToString() + " = " + iOldValue + " -> " + iModeValue.ToString());
                bRETUN = true;
            }
        }
        if (CleanData.COUNTER[iNUM] != iCount){
            iOldValue               = CleanData.COUNTER[iNUM];
            CleanData.COUNTER[iNUM] = iCount;
            FILE_.WRInt(/*mPATH.COMMON*/sCurrJobName, "CLEAN_DATA", "COUNTER_" + iNUM.ToString(), CleanData.COUNTER[iNUM]);
            if (!mNotTeachSave){
                LogWR_.SAVE_ChangeDataEvent("[CLEAN_DATA] COUNTER " + iNUM.ToString() + " = " + iOldValue + " -> " + iCount.ToString());
                bRETUN = true;
            }
        }
        if (CleanData.TIME[iNUM] != iTime){
            iOldValue               = CleanData.TIME[iNUM];
            CleanData.TIME[iNUM]    = iTime;
            FILE_.WRInt(/*mPATH.COMMON*/sCurrJobName, "CLEAN_DATA", "TIME_" + iNUM.ToString(), CleanData.TIME[iNUM]);
            if (!mNotTeachSave){
                LogWR_.SAVE_ChangeDataEvent("[CLEAN_DATA] TIME " + iNUM.ToString() + " = " + iOldValue + " -> " + iTime.ToString());
                bRETUN = true;
            }
        }
        return bRETUN;
    }

    public static void Read_UnitPkWorkedCleanData(){
        if (!File.Exists(PATH_.COMMON)) return;
        for (int i = 0; i < 10; i++){
            WorkedCleanData.MODE[i]     = FILE_.RDInt(PATH_.COMMON, "WORKED_CLEAN_DATA", "MODE_" + i.ToString(), 0);
            WorkedCleanData.COUNTER[i]  = FILE_.RDInt(PATH_.COMMON, "WORKED_CLEAN_DATA", "COUNTER_" + i.ToString(), 0);
            WorkedCleanData.TIME[i]     = FILE_.RDInt(PATH_.COMMON, "WORKED_CLEAN_DATA", "TIME_" + i.ToString(), 0);
        }
    }
    public static bool WR_WORKED_CLEAN_DATA(int iNUM, int iModeValue, int iCount, int iTime){
        bool bRETUN = false;

        if (WorkedCleanData.MODE[iNUM] != iModeValue){
            iOldValue                   = WorkedCleanData.MODE[iNUM];
            WorkedCleanData.MODE[iNUM]  = iModeValue;
            FILE_.WRInt(PATH_.COMMON, "WORKED_CLEAN_DATA", "MODE_" + iNUM.ToString(), WorkedCleanData.MODE[iNUM]);
            if (!mNotTeachSave){
                LogWR_.SAVE_ChangeDataEvent("[WORKED_CLEAN_DATA] MODE " + iNUM.ToString() + " = " + iOldValue + " -> " + iModeValue.ToString());
                bRETUN = true;
            }
        }
        if (WorkedCleanData.COUNTER[iNUM] != iCount){
            iOldValue                       = WorkedCleanData.COUNTER[iNUM];
            WorkedCleanData.COUNTER[iNUM]   = iCount;
            FILE_.WRInt(PATH_.COMMON, "WORKED_CLEAN_DATA", "COUNTER_" + iNUM.ToString(), WorkedCleanData.COUNTER[iNUM]);
            if (!mNotTeachSave){
                LogWR_.SAVE_ChangeDataEvent("[WORKED_CLEAN_DATA] COUNTER " + iNUM.ToString() + " = " + iOldValue + " -> " + iCount.ToString());
                bRETUN = true;
            }
        }
        if (CleanData.TIME[iNUM] != iTime){
            iOldValue                   = WorkedCleanData.TIME[iNUM];
            WorkedCleanData.TIME[iNUM]  = iTime;
            FILE_.WRInt(PATH_.COMMON, "WORKED_CLEAN_DATA", "TIME_" + iNUM.ToString(), WorkedCleanData.TIME[iNUM]);
            if (!mNotTeachSave){
                LogWR_.SAVE_ChangeDataEvent("[WORKED_CLEAN_DATA] TIME " + iNUM.ToString() + " = " + iOldValue + " -> " + iTime.ToString());
                bRETUN = true;
            }
        }
        return bRETUN;
    }

    public static void Read_ManualInspection(int GroupX, int GroupY, int UnitX1, int UnitY1, int UnitX2, int UnitY2){
        if (!File.Exists(sCurrJobName)) return;

        for (int gy = 0; gy < GroupY; gy++){
            for (int gx = 0; gx < GroupX; gx++){
                for (int idxY = 0; idxY < UnitY1; idxY++){
                    for (int idxX = 0; idxX < UnitX1; idxX++){
                        MAP_.Inspection[(int)eMAP_BLOCK.STAGE1, gx, gy, idxX, idxY] = (eSTATUS)FILE_.RDInt(sCurrJobName, "MANUAL_INSPECTION", "MAPBLOCK" + ((int)eMAP_BLOCK.STAGE1).ToString() + "_GX" + gx.ToString() + "_GY" + gy.ToString() + "_UX" + idxX.ToString() + "_UY" + idxY.ToString(), 0);
                        //if (mMAP.Inspection[(int)eMAP_BLOCK.PALLET_1, gx, gy, idxX, idxY] == eSTATUS.csFIRST){
                        //    mLogWR.DEBUG_PRINT("[MAPBLOCK1]GX=" + gx.ToString() + "/GY=" + gy.ToString() + "/UX=" + idxX.ToString() + "/UY=" + idxY.ToString() + " -> FIRST");
                        //}
                        //else if (mMAP.Inspection[(int)eMAP_BLOCK.PALLET_1, gx, gy, idxX, idxY] == eSTATUS.csSECOND){
                        //    mLogWR.DEBUG_PRINT("[MAPBLOCK1]GX=" + gx.ToString() + "/GY=" + gy.ToString() + "/UX=" + idxX.ToString() + "/UY=" + idxY.ToString() + " -> SECOND");
                        //}
                    }
                }
                for (int idxY = 0; idxY < UnitY2; idxY++){
                    for (int idxX = 0; idxX < UnitX2; idxX++){
                        MAP_.Inspection[(int)eMAP_BLOCK.STAGE2, gx, gy, idxX, idxY] = (eSTATUS)FILE_.RDInt(sCurrJobName, "MANUAL_INSPECTION", "MAPBLOCK" + ((int)eMAP_BLOCK.STAGE2).ToString() + "_GX" + gx.ToString() + "_GY" + gy.ToString() + "_UX" + idxX.ToString() + "_UY" + idxY.ToString(), 0);
                        //if (mMAP.Inspection[(int)eMAP_BLOCK.PALLET_1, gx, gy, idxX, idxY] == eSTATUS.csFIRST){
                        //    mLogWR.DEBUG_PRINT("RD=[MAPBLOCK2]GX=" + gx.ToString() + "/GY=" + gy.ToString() + "/UX=" + idxX.ToString() + "/UY=" + idxY.ToString() + " -> FIRST(7)");
                        //}
                        //else if (mMAP.Inspection[(int)eMAP_BLOCK.PALLET_1, gx, gy, idxX, idxY] == eSTATUS.csSECOND){
                        //    mLogWR.DEBUG_PRINT("RD=[MAPBLOCK2]GX=" + gx.ToString() + "/GY=" + gy.ToString() + "/UX=" + idxX.ToString() + "/UY=" + idxY.ToString() + " -> SECOND(8)");
                        //}
                    }
                }
            }
        }
    }

    public static bool WR_ManualInspection(int nMAP_BLOCK, int GroupX, int GroupY, int UnitX, int UnitY){
        oldStatus = (int)MAP_.OldInspection;
        newStatus = (int)MAP_.Inspection[nMAP_BLOCK, GroupX, GroupY, UnitX, UnitY];

        if (oldStatus != newStatus){
            FILE_.WRInt(sCurrJobName, "MANUAL_INSPECTION", "MAPBLOCK" + nMAP_BLOCK.ToString() + "_GX" + GroupX.ToString() + "_GY" + GroupY.ToString() + "_UX" + UnitX.ToString() + "_UY" + UnitY.ToString(), newStatus);
            //mLogWR.DEBUG_PRINT("WR=[MAPBLOCK" + nMAP_BLOCK.ToString() + "]GX=" + GroupX.ToString() + "/GY=" + GroupY.ToString() + "/UX=" + UnitX.ToString() + "/UY=" + UnitY.ToString() + " -> " + newStatus.ToString());
        }
        return true;
    }
#endregion "RD/WR PARAMETERS"

#region "RD/WR MOTOR DATA"
    public static void Read_MotorLabel(){
        string mSTR;
        string sPath = PATH_.MTName;
        short m = 0;
        short p;
#if _NSS3300
        sPath = PATH_.MTName3300;
#else
        if (MC_DIR == 1) sPath = PATH_.MTNameR;
#endif
        try
        {
            StreamReader sr = new StreamReader(sPath);
            while (sr.Peek() != -1){
                mSTR = sr.ReadLine();
                if (mSTR.Length > 1){
                    if (mSTR.Substring(0, 1) == "/"){
                        string[] sARR1 = mSTR.Split('/');
                        m = Convert.ToInt16(sARR1[1]);
                        MtName[m] = sARR1[2].Trim();
                    }
                    else{
                        string[] sARR2 = mSTR.Split(',');
                        if (sARR2.Length >= 2){
                            p = Convert.ToInt16(sARR2[0]);
                            PosName[m, p] = sARR2[1].Trim();
                        }
                    }
                }
            }
            sr.Close();
        }
        catch (Exception ex) { MessageBox.Show("READ_MOTOR_LABEL FAIL" + ETC.NewLine + ex.ToString()); }
    }

    public static stMotorSoftData RD_SoftLimitData(string fn, int m){
        stMotorSoftData ms              = mtSoftData[0];
        mtSoftData[m].CwSoftLimit       = FILE_.RDDouble(fn, "MT_" + m.ToString(), "CwLIMIT", 5000);
        mtSoftData[m].CcwSoftLimit      = FILE_.RDDouble(fn, "MT_" + m.ToString(), "CcwLIMIT", -100);
        mtSoftData[m].MaxPitch          = FILE_.RDDouble(fn, "MT_" + m.ToString(), "MaxPITCH", 100);
        mtSoftData[m].MinSpd            = FILE_.RDDouble(fn, "MT_" + m.ToString(), "MinSPD", 1);
        mtSoftData[m].MaxSpd            = FILE_.RDDouble(fn, "MT_" + m.ToString(), "MaxSPD", 1000);
        mtSoftData[m].MinAcc            = FILE_.RDDouble(fn, "MT_" + m.ToString(), "MinAcc", 1);
        mtSoftData[m].MaxAcc            = FILE_.RDDouble(fn, "MT_" + m.ToString(), "MaxAcc", 10000);
        mtSoftData[m].MinDec            = FILE_.RDDouble(fn, "MT_" + m.ToString(), "MinDec", 1);
        mtSoftData[m].MaxDec            = FILE_.RDDouble(fn, "MT_" + m.ToString(), "MaxDec", 10000);
        mtSoftData[m].JOG_LOW_SPD       = FILE_.RDDouble(fn, "MT_" + m.ToString(), "JogLowSPD", 1);
        mtSoftData[m].JOG_MIDDLE_SPD    = FILE_.RDDouble(fn, "MT_" + m.ToString(), "JogMiddleSPD", 10);
        mtSoftData[m].JOG_HIGH_SPD      = FILE_.RDDouble(fn, "MT_" + m.ToString(), "JogHighSPD", 100);
        return ms;
    }
    public static stMotorTeachingLimit RD_SoftLimitData(string fn, int m, int p){
        stMotorTeachingLimit ml         = mtTeachingLimit[0, 0];
        mtTeachingLimit[m, p].PosPLimit = FILE_.RDDouble(fn, "MT_" + m.ToString() + "POS_" + p.ToString(), "PosPLimit", 1000);
        mtTeachingLimit[m, p].PosNLimit = FILE_.RDDouble(fn, "MT_" + m.ToString() + "POS_" + p.ToString(), "PosNLimit", -1000);
        mtTeachingLimit[m, p].Enable    = FILE_.RDBool(fn, "MT_" + m.ToString() + "POS_" + p.ToString(), "ENABLE", false);
        return ml;
    }

    public static void Read_SoftLimit(){
        string fn = PATH_.SOFTLIMIT;
        if (!File.Exists(fn)) return;

        for (int i = 0; i < CNT_.MT; i++){
            RD_SoftLimitData(fn, i);
            for (int j = 0; j < CNT_.POS; j++){
                RD_SoftLimitData(fn, i, j);
            }
        }
    }
    public static bool WR_SoftLimitData(int m){
        stMotorSoftData oldmd   = mtOldSoftData;
        stMotorSoftData md      = mtSoftData[m];
        string fn               = PATH_.SOFTLIMIT;
        string sLogMessage;
        bool bRTN = false;

        FILE_.WRDouble(fn, "MT_" + m.ToString(), "CwLIMIT", md.CwSoftLimit);
        FILE_.WRDouble(fn, "MT_" + m.ToString(), "CcwLIMIT", md.CcwSoftLimit);
        FILE_.WRDouble(fn, "MT_" + m.ToString(), "MaxPITCH", md.MaxPitch);
        FILE_.WRDouble(fn, "MT_" + m.ToString(), "MinSPD", md.MinSpd);
        FILE_.WRDouble(fn, "MT_" + m.ToString(), "MaxSPD", md.MaxSpd);
        FILE_.WRDouble(fn, "MT_" + m.ToString(), "MinAcc", md.MinAcc);
        FILE_.WRDouble(fn, "MT_" + m.ToString(), "MaxAcc", md.MaxAcc);
        FILE_.WRDouble(fn, "MT_" + m.ToString(), "MinDec", md.MinDec);
        FILE_.WRDouble(fn, "MT_" + m.ToString(), "MaxDec", md.MaxDec);

        FILE_.WRDouble(fn, "MT_" + m.ToString(), "JogLowSPD", md.JOG_LOW_SPD);
        FILE_.WRDouble(fn, "MT_" + m.ToString(), "JogMiddleSPD", md.JOG_MIDDLE_SPD);
        FILE_.WRDouble(fn, "MT_" + m.ToString(), "JogHighSPD", md.JOG_HIGH_SPD);

        if (!mNotTeachSave){
            if (oldmd.CwSoftLimit != md.CwSoftLimit){
                sLogMessage = MtName[m] + "-" + "CW SOFT LIMIT = " + oldmd.CwSoftLimit.ToString("0.000") + " -> " + md.CwSoftLimit.ToString("0.000");
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
            if (oldmd.CcwSoftLimit != md.CcwSoftLimit){
                sLogMessage = MtName[m] + "-" + "CCW SOFT LIMIT = " + oldmd.CcwSoftLimit.ToString("0.000") + " -> " + md.CcwSoftLimit.ToString("0.000");
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
            if (oldmd.MaxPitch != md.MaxPitch){
                sLogMessage = MtName[m] + "-" + "MAX PITCH = " + oldmd.MaxPitch.ToString("0.000") + " -> " + md.MaxPitch.ToString("0.000");
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
            if (oldmd.MinSpd != md.MinSpd){
                sLogMessage = MtName[m] + "-" + "MIN SPEED = " + oldmd.MinSpd.ToString("0.000") + " -> " + md.MinSpd.ToString("0.000");
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
            if (oldmd.MaxSpd != md.MaxSpd){
                sLogMessage = MtName[m] + "-" + "MAX SPEED = " + oldmd.MaxSpd.ToString("0.000") + " -> " + md.MaxSpd.ToString("0.000");
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
            if (oldmd.MinAcc != md.MinAcc){
                sLogMessage = MtName[m] + "-" + "MIN ACC = " + oldmd.MinAcc.ToString("0.000") + " -> " + md.MinAcc.ToString("0.000");
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
            if (oldmd.MaxAcc != md.MaxAcc){
                sLogMessage = MtName[m] + "-" + "MAX ACC = " + oldmd.MaxAcc.ToString("0.000") + " -> " + md.MaxAcc.ToString("0.000");
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
            if (oldmd.MinDec != md.MinDec){
                sLogMessage = MtName[m] + "-" + "MIN DEC = " + oldmd.MinDec.ToString("0.000") + " -> " + md.MinDec.ToString("0.000");
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
            if (oldmd.MaxDec != md.MaxDec){
                sLogMessage = MtName[m] + "-" + "MAX DEC = " + oldmd.MaxDec.ToString("0.000") + " -> " + md.MaxDec.ToString("0.000");
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
        }
        return bRTN;
    }
    public static bool WR_TeachingLimit(int m, int p){
        stMotorTeachingLimit oldml  = mtOldTeachingLimit;
        stMotorTeachingLimit ml     = mtTeachingLimit[m, p];
        string fn                   = PATH_.SOFTLIMIT;
        string sLogMessage;
        bool bRTN = false;

        FILE_.WRDouble(fn, "MT_" + m.ToString() + "POS_" + p.ToString(), "PosPLimit", ml.PosPLimit);
        FILE_.WRDouble(fn, "MT_" + m.ToString() + "POS_" + p.ToString(), "PosNLimit", ml.PosNLimit);
        FILE_.WRBool(fn, "MT_" + m.ToString() + "POS_" + p.ToString(), "ENABLE", ml.Enable);

        if (!mNotTeachSave){
            if (oldml.PosPLimit != ml.PosPLimit){
                sLogMessage = MtName[m] + "-" + "POS +LIMIT = " + oldml.PosPLimit.ToString("0.000") + " -> " + ml.PosPLimit.ToString("0.000");
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
            if (oldml.PosNLimit != ml.PosNLimit){
                sLogMessage = MtName[m] + "-" + "POS -LIMIT = " + oldml.PosNLimit.ToString("0.000") + " -> " + ml.PosNLimit.ToString("0.000");
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
            if (oldml.Enable != ml.Enable){
                sLogMessage = MtName[m] + "-" + "ENABLE = " + oldml.Enable.ToString() + " -> " + ml.Enable.ToString();
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
        }
        return bRTN;
    }

    public static stMoveInfo RD_MTDATA(int m, int p){
        stMoveInfo mi   = mtDATA[0, 0];
        string fn       = PATH_.COMMON;
        if (p >= CNT_.ComPos) fn = sCurrJobName;

        mi.Pos = FILE_.RDDouble(fn, "MT_" + m.ToString(), "POS_" + p.ToString(), 0.1);
        if (p >= CNT_.ComPos){
            if (prMACHINE[SelectMTSpd] == (int)ePARA.COM) fn = PATH_.COMMON;
        }
        mi.Spd      = FILE_.RDDouble(fn, "MT_" + m.ToString(), "SPD_" + p.ToString(), 10);
        mi.Acc      = FILE_.RDDouble(fn, "MT_" + m.ToString(), "ACC_" + p.ToString(), 1000);
        mi.Dec      = FILE_.RDDouble(fn, "MT_" + m.ToString(), "DEC_" + p.ToString(), 1000);
        mi.MoveTime = FILE_.RDInt(fn, "MT_" + m.ToString(), "MOVETIME_" + p.ToString(), 10000);
        mi.Delay    = FILE_.RDInt(fn, "MT_" + m.ToString(), "DELAY_" + p.ToString(), 0);
        return mi;
    }
    public static void RD_MTDATA(){
        for (int i = 0; i < CNT_.MT; i++){
            for (int j = 0; j < CNT_.POS; j++) { 
                mtDATA[i, j] = RD_MTDATA(i, j); 
            }
        }
    }

    public static void Write_MotorPos(DataGridView g, int mt){
        int num;
        double pos;
        for (int i = 0; i < g.RowCount; i++){
            num = int.Parse(g[0, i].Value.ToString());
            pos = double.Parse(g[2, i].Value.ToString());
            SaveMotorPos(mt, num, pos);
        }
    }
    public static void Write_MotorPos(DataGridView g, int[] mt){
        int num;
        double[] pos = new double[mt.Length];
        for (int i = 0; i < g.RowCount; i++){
            for (int j = 0; j < mt.Length; j++){
                num     = int.Parse(g[0, i].Value.ToString());
                pos[j]  = double.Parse(g[2 + j, i].Value.ToString());
                SaveMotorPos(mt[j], num, pos[j]);
            }
        }
    }
    public static void SaveMotorPos(int mt, int index, double pos){
        mtOLD                   = mtDATA[mt, index];
        mtDATA[mt, index].Pos   = pos;
        mtSAVE                  = mtDATA[mt, index];
        WR_MTDATA(mt, index);
    }

    public static bool WR_MTDATA(int m, int p){
        stMoveInfo oldmi    = mtOLD;
        stMoveInfo mi       = mtDATA[m, p];
        string fn           = PATH_.COMMON;
        string sLogMessage;
        bool bRTN = false;
        if (p >= CNT_.ComPos) fn = sCurrJobName;

        FILE_.WRDouble(fn, "MT_" + m.ToString(), "POS_" + p.ToString(), mi.Pos);
        if (p >= CNT_.ComPos){
            if (prMACHINE[SelectMTSpd] == (int)ePARA.COM) fn = PATH_.COMMON;
        }
        FILE_.WRDouble(fn, "MT_" + m.ToString(), "SPD_" + p.ToString(), mi.Spd);
        FILE_.WRDouble(fn, "MT_" + m.ToString(), "ACC_" + p.ToString(), mi.Acc);
        FILE_.WRDouble(fn, "MT_" + m.ToString(), "DEC_" + p.ToString(), mi.Dec);
        FILE_.WRInt(fn, "MT_" + m.ToString(), "MOVETIME_" + p.ToString(), mi.MoveTime);
        FILE_.WRInt(fn, "MT_" + m.ToString(), "DELAY_" + p.ToString(), mi.Delay);

        mTeachChanged = true;
        if (!mNotTeachSave){
            if (oldmi.Pos != mi.Pos){
                sLogMessage = MtName[m] + "-" + PosName[m, p] + " POSITION = " + oldmi.Pos.ToString("0.000") + " -> " + mi.Pos.ToString("0.000");
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
            if (oldmi.Spd != mi.Spd){
                sLogMessage = MtName[m] + "-" + PosName[m, p] + " SPEED = " + oldmi.Spd.ToString("0.000") + " -> " + mi.Spd.ToString("0.000");
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
            if (oldmi.Acc != mi.Acc){
                sLogMessage = MtName[m] + "-" + PosName[m, p] + " ACC = " + oldmi.Acc.ToString("0.0") + " -> " + mi.Acc.ToString("0.0");
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
            if (oldmi.Dec != mi.Dec){
                sLogMessage = MtName[m] + "-" + PosName[m, p] + " DEC = " + oldmi.Dec.ToString("0.0") + " -> " + mi.Dec.ToString("0.0");
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
            if (oldmi.MoveTime != mi.MoveTime){
                sLogMessage = MtName[m] + "-" + PosName[m, p] + " MOVE TIME = " + oldmi.MoveTime.ToString("0") + " -> " + mi.MoveTime.ToString("0");
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
            if (oldmi.Delay != mi.Delay){
                sLogMessage = MtName[m] + "-" + PosName[m, p] + " DELAY = " + oldmi.Delay.ToString("0") + " -> " + mi.Delay.ToString("0");
                LogWR_.SAVE_ChangeDataEvent(sLogMessage);
                bRTN = true;
            }
        }
        return bRTN;
    }
#endregion "RD/WR MOTOR DATA"

#region "RD ERROR LIST"
    public static void Read_ErrorLabel(){
        try{
            if (File.Exists(PATH_.SystemError)){
                int num = 0;
                StreamReader sr = new StreamReader(PATH_.SystemError, Encoding.GetEncoding("euc-kr"));
                while (sr.Peek() != -1){
                    if (CNT_.ERR <= num) break;
                    string mSTR         = sr.ReadLine();
                    string[] sSecond    = mSTR.Split(',');

                    for (int i = 0; i < sSecond.Length; i++){
                        if (i == 1) ErrName[num + eErrBegin] = sSecond[i];
                        if (i == 2) ErrTitle_1[num + eErrBegin] = sSecond[i];
                        if (i == 3) ErrTitle_2[num + eErrBegin] = sSecond[i];
                    }
                    num++;
                }
                sr.Close();
            }
            else { /* 파일없음 */ }
        }
        catch (Exception ex) { MessageBox.Show("RD_SYSTEM_ERROR FAIL" + ETC.NewLine + ex.ToString()); }
    }
    public static void Read_InterlockErrorLabel(){
        try{
            if (File.Exists(PATH_.InterlockError)){
                int num = 0;
                StreamReader sr = new StreamReader(PATH_.InterlockError, Encoding.GetEncoding("euc-kr"));
                while (sr.Peek() != -1){
                    if (CNT_.ERR <= num) break;
                    string mSTR         = sr.ReadLine();
                    string[] sSecond    = mSTR.Split(',');

                    if (sSecond.Length >= 2){
                        ErrName[num + eEMSBegin]    = sSecond[1];
                        IntkName[num + eEMSBegin]   = sSecond[1];
                    }
                    num++;
                }
                sr.Close();
            }
            else { /* 파일없음 */}
        }
        catch (Exception ex) { MessageBox.Show("RD_INTERLOCK_ERROR FAIL" + ETC.NewLine + ex.ToString()); }
    }
#endregion "RD ERROR LIST"

#region "WARNING LIST"
    public static void Read_WarningLabel(){
        string mSTR;
        string mLabel;
        short n;
        try{
            StreamReader sr = new StreamReader(PATH_.WarningName, Encoding.GetEncoding("euc-kr"));
            while (sr.Peek() != -1){
                mSTR = sr.ReadLine();
                if (mSTR.Length > 1){
                    string[] sARR = mSTR.Split(',');
                    mLabel = string.Empty;
                    if (sARR.Length >= 2){
                        n               = Convert.ToInt16(sARR[0]);
                        string[] sLabel = sARR[1].Split('/');
                        for (int i = 0; i < sLabel.Length; i++){
                            mLabel += sLabel[i];
                            if (i < sLabel.Length - 1) mLabel += ETC./*NewLine*/CrLf;
                        }
                        ConfirmUser[n].msg = mLabel;
                    }
                }
            }
            sr.Close();
        }
        catch (Exception ex) { MessageBox.Show("READ_MD_PARA_LABEL FAIL" + ETC.NewLine + ex.ToString()); }
    }
#endregion "WARNING LIST"
}