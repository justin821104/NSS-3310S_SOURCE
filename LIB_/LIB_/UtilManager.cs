using Object;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using LIB_.DateType;
using NSS_3310S;

public class UTIL_ : DATA_
{
    public delegate void DeleAddException(string sErr);
    public static event DeleAddException DeleException = null;

    public static DateTime DELAY(int ms){
        if (ms <= 0) return DateTime.Now;
        Thread.Sleep(ms);
        return DateTime.Now;
    }

    public static bool IsFINGER(int axis){
        for (int idxLOOP = 0; idxLOOP < CNT_.PKR; idxLOOP++){
            if (pkrX1 != null) { 
                if (axis == pkrX1[idxLOOP]) return true; 
            }
            if (pkrX2 != null) { 
                if (axis == pkrX2[idxLOOP]) return true; 
            }
            if (pkrX3 != null) { 
                if (axis == pkrX3[idxLOOP]) return true; 
            }
            if (pkrX4 != null) { 
                if (axis == pkrX4[idxLOOP]) return true; 
            }
        }
        return false;
    }

    public static bool ChkAllReadyRun(string ExeFileName){
        Process[] pLIST;
        pLIST = Process.GetProcessesByName(ExeFileName);
        int iCNT = 0;
        foreach (Process pr in pLIST) iCNT += 1;
        return (iCNT >= 2) ? false : true;
    }
    public static void KillProgram(string ExeFileName){
        Process[] pLIST;
        pLIST = Process.GetProcessesByName(ExeFileName + ".vshost");
        foreach (Process proc in pLIST) { proc.Kill(); }

        pLIST = Process.GetProcessesByName(ExeFileName + ".exe");
        foreach (Process proc in pLIST) { proc.Kill(); }

        pLIST = Process.GetProcessesByName(ExeFileName);
        foreach (Process proc in pLIST) { proc.Kill(); }
    }

    public static string GET_EQCode(){
        if (!File.Exists(PATH_.EQPCode)) return "";
        string[] sLINE = File.ReadAllText(PATH_.EQPCode).Split(ETC.CrLf);
        try{
            string[] sRslt = sLINE[0].Split(':');
            return sRslt[1];
        }
        catch (Exception ex){
            MessageBox.Show("Equipment code reading fail !" + ETC.NewLine + ex.ToString());
            return "";
        }
    }
    public static int GET_MACHINE_DIR(){
        if (!File.Exists(PATH_.MC_DIR)) return -1;
        string[] sLINE = File.ReadAllText(PATH_.MC_DIR).Split(ETC.CrLf);
        try{
            string[] sRslt = sLINE[0].Split(':');
            return int.Parse(sRslt[1]);
        }
        catch (Exception ex){
            MessageBox.Show("Machine Dir reading fail !" + ETC.NewLine + ex.ToString());
            return -1;
        }
    }

    public static string GET_MSSQL_ADD(ref string IP, ref string DBName, ref string ID, ref string Pwd){
        if (!File.Exists(PATH_.MsSql)) return "";
        string[] sLINE = File.ReadAllText(PATH_.MsSql).Split(ETC.CrLf);
        try{
            string[] sRslt  = sLINE[0].Split('=');
            string value    = sRslt[1].Replace("\r", "");
            IP              = value;
            
            sRslt           = sLINE[1].Split('=');
            value           = sRslt[1].Replace("\r", "");
            DBName          = value;
            
            sRslt           = sLINE[2].Split('=');
            value           = sRslt[1].Replace("\r", "");
            ID              = value;
            
            sRslt           = sLINE[3].Split('=');
            value           = sRslt[1].Replace("\r", "");
            Pwd             = value;
            return "OK";
        }
        catch (Exception ex){
            MessageBox.Show("Machine Ms-SQL address reading fail !" + ETC.NewLine + ex.ToString());
            return "";
        }
    }

    public static int GetSqlReading() {
        int rtnValue = 2;
        if (!File.Exists(PATH_.SQLRead)) return rtnValue;
        rtnValue = (int)prMACHINE[CP.DBReadMode];
        return rtnValue;
    } 
    public static void WriteSqlReading() {
        int value = (int)prMACHINE[CP.DBReadMode];
        FILE_.WR_File(PATH_.SQLRead, value.ToString(), false);
    }

    public static void WRITE_LOT_ID_INI(string lotId) {
        FILE_.WRString(PATH_.LOTID, "LOT_INFO", "LOTID", lotId);
    }
    public static string GET_LOT_ID_INI() {
        if (!File.Exists(PATH_.LOTID)) return "";
        string lotID = FILE_.RDString(PATH_.LOTID, "LOT_INFO", "LOTID" , "");
        return lotID;
    }
    public static string GET_LOT_ID(){
        if (!File.Exists(PATH_.LOT_ID)) return "";
        string[] sLINE = File.ReadAllText(PATH_.LOT_ID).Split(ETC.CrLf);
        try{
           return sLINE[0];
        }
        catch (Exception EX){
            MessageBox.Show("LotID reading fail !" + ETC.NewLine + EX.ToString());
            return "";
        }
    }
    public static string GET_ITS_ID(){
        if (!File.Exists(PATH_.ITS_ID)) return "";
        string[] sLINE = File.ReadAllText(PATH_.ITS_ID).Split(ETC.CrLf);
        try{
            return sLINE[0];
        }
        catch (Exception ex){
            MessageBox.Show("ITS ID reading fail !" + ETC.NewLine + ex.ToString());
            return "";
        }
    }

    public static string GET_MES_ABF() {
        if (!File.Exists(PATH_.MES_ABFMATERIAL)) return ""; //GZ41R2H
        string[] sLINE = File.ReadAllText(PATH_.MES_ABFMATERIAL).Split(ETC.CrLf);
        try {
            return sLINE[0];
        }
        catch (Exception ex) {
            MessageBox.Show("MES Abfmaterial reading fail !" + ETC.NewLine + ex.ToString());
            return "";
        }
    }

    public static void WR_VISION_RECEIP(){
        string sVisionRecipe = "";
        sVisionRecipe += "LOTID:" + CLOT.GET_LOT.LotID + ETC.NewLine;
        sVisionRecipe += "TOOLNO:" + CLOT.GET_LOT.ToolNo + ETC.NewLine;
        sVisionRecipe += "ITSLOTID_IN:" + CLOT.GET_LOT.ITS + ETC.NewLine;
        sVisionRecipe += "UNITSIZEX:" + CLOT.GET_LOT.UnitSizeX.ToString() + ETC.NewLine;
        sVisionRecipe += "UNITSIZEY:" + CLOT.GET_LOT.UnitSizeY.ToString() + ETC.NewLine;
        sVisionRecipe += "UNITSIZE_UPPER:" + CLOT.GET_LOT.UnitSize_USL.ToString() + ETC.NewLine;
        sVisionRecipe += "UNITSIZE_LOWER:" + CLOT.GET_LOT.UnitSize_LSL.ToString() + ETC.NewLine;
        sVisionRecipe += "THICK:" + CLOT.GET_LOT.Thick.ToString() + ETC.NewLine;
        sVisionRecipe += "THICK_UPPER:" + CLOT.GET_LOT.Thick_USL.ToString() + ETC.NewLine;
        sVisionRecipe += "THICK_LOWER:" + CLOT.GET_LOT.Thick_LSL.ToString() + ETC.NewLine;
        sVisionRecipe += "ABFMATERIAL:" + CLOT.GET_LOT.ABFMATERIAL.ToString() + ETC.NewLine;

        sVisionRecipe += "BOT_LANDTOPKG_X:" + CLOT.GET_LOT.BOT_LANDTOPKG_X.ToString() + ETC.NewLine;
        sVisionRecipe += "BOT_CHAMFERLEN_TM_X:" + CLOT.GET_LOT.BOT_CHAMFERLEN_TM_X.ToString() + ETC.NewLine;
        sVisionRecipe += "BOT_CHAMFERLEN_TP_X:" + CLOT.GET_LOT.BOT_CHAMFERLEN_TP_X.ToString() + ETC.NewLine;
        sVisionRecipe += "BOT_LANDTOPKG_Y:" + CLOT.GET_LOT.BOT_LANDTOPKG_Y.ToString() + ETC.NewLine;
        sVisionRecipe += "BOT_CHAMFERLEN_TM_Y:" + CLOT.GET_LOT.BOT_CHAMFERLEN_TM_Y.ToString() + ETC.NewLine;
        sVisionRecipe += "BOT_CHAMFERLEN_TP_Y:" + CLOT.GET_LOT.BOT_CHAMFERLEN_TP_Y.ToString() + ETC.NewLine;

        sVisionRecipe += "TOP_LANDTOPKG_X:" + CLOT.GET_LOT.TOP_LANDTOPKG_X.ToString() + ETC.NewLine;
        sVisionRecipe += "TOP_CHAMFERLEN_TM_X:" + CLOT.GET_LOT.TOP_CHAMFERLEN_TM_X.ToString() + ETC.NewLine;
        sVisionRecipe += "TOP_CHAMFERLEN_TP_X:" + CLOT.GET_LOT.TOP_CHAMFERLEN_TP_X.ToString() + ETC.NewLine;
        sVisionRecipe += "TOP_LANDTOPKG_Y:" + CLOT.GET_LOT.TOP_LANDTOPKG_Y.ToString() + ETC.NewLine;
        sVisionRecipe += "TOP_CHAMFERLEN_TM_Y:" + CLOT.GET_LOT.TOP_CHAMFERLEN_TM_Y.ToString() + ETC.NewLine;
        sVisionRecipe += "TOP_CHAMFERLEN_TP_Y:" + CLOT.GET_LOT.TOP_CHAMFERLEN_TP_Y.ToString() + ETC.NewLine;

        FILE_.WR_File(PATH_.VisionReciepe, sVisionRecipe, false);
    }

    public static bool GET_SPINDLE_BLADE_BARCODE(eSPINDLE SPINDLE){
        string sFileName = eSPINDLE.SP1 == SPINDLE ? PATH_.NewBladeBarcodeSp1 : PATH_.NewBladeBarcodeSp2;
        if (!File.Exists(sFileName)){
            if (eSPINDLE.SP1 == SPINDLE)    CLOT.GET_LOT.BarcodeSp1 = "";
            else                            CLOT.GET_LOT.BarcodeSp2 = "";
            return false;
        }
        try{
            string[] sLine = File.ReadAllText(sFileName).Split(ETC.CrLf);
            string[] sRslt = sLine[0].Split(',');
            sRslt[0] = sRslt[0].Replace("\n", "");
            sRslt[0] = sRslt[0].Replace("\r", "");
            if (sRslt.Length > 1){
                sRslt[1] = sRslt[1].Replace("\n", "");
                sRslt[1] = sRslt[1].Replace("\r", "");
            }

            if (eSPINDLE.SP1 == SPINDLE){
                SUBFRM_.gSecsGem.SetBladeChange(eSPINDLE.SP1, sRslt[0], CLOT.GET_LOT.BarcodeSp1);
                CLOT.GET_LOT.BarcodeSp1 = sRslt[0];
                CLOT.GET_LOT.IDSp1      = (sRslt.Length > 1) ? sRslt[1] : "";
            }
            else{
                SUBFRM_.gSecsGem.SetBladeChange(eSPINDLE.SP2, sRslt[0], CLOT.GET_LOT.BarcodeSp2);
                CLOT.GET_LOT.BarcodeSp2 = sRslt[0];
                CLOT.GET_LOT.IDSp2      = (sRslt.Length > 1) ? sRslt[1] : "";
            }
            return true;
        }
        catch (Exception EX){
            LogWR_.SaveLogException("GET_SPINDLE_BLADE_BARCODE FAIL", EX);
            if (eSPINDLE.SP1 == SPINDLE){
                CLOT.GET_LOT.BarcodeSp1 = "";
                CLOT.GET_LOT.IDSp1 = "";
            }
            else { 
                CLOT.GET_LOT.BarcodeSp2 = "";
                CLOT.GET_LOT.IDSp2 = ""; 
            }
        }
        return false;
    }
    public static void GET_SPINDLE_OLD_BLADE_BARCODE(eSPINDLE SPINDLE){
        string sFileName = eSPINDLE.SP1 == SPINDLE ? PATH_.OldBladeBarcodeSp1 : PATH_.OldBladeBarcodeSp2;
        if (!File.Exists(sFileName)) return;
        try{
            string[] sLine = File.ReadAllText(PATH_.CurrLot).Split(ETC.CrLf);
            string[] sRslt = sLine[0].Split(',');
        
        }
        catch (Exception EX){
            LogWR_.SaveLogException("GET_SPINDLE_OLD_BLADE_BARCODE FAIL", EX);
        }
    }

    public static bool GET_ABF_LIST(){
        if (!File.Exists(PATH_.ABF)){
            return false;
        }
        try{
            string[] sLine = File.ReadAllText(PATH_.ABF).Split(ETC.CrLf);
            if (sLine.Length > 1){
                for (int i = 0; i < sLine.Length; i++){
                    if (sLine[i].Length <= 0) continue;
                    string[] sTitle = sLine[i].Split(':');
                    string[] sRslt = sTitle[1].Split(',');
                    if (i == 0){
                        sRslt[0] = sRslt[0].Replace("\n", "");
                        sRslt[0] = sRslt[0].Replace("\r", "");
                        int IDX = int.Parse(sRslt[0]);
                        CLOT.ABF_LIST = IDX;
                        CLOT.ABF_TEMP = new string[IDX, IDX];
                        for (int m = 0; m < IDX; m++){
                            for (int z = 0; z < IDX; z++){
                                CLOT.ABF_TEMP[m, z] = string.Empty;
                            }
                        }
                    }
                    else{
                        if (sRslt.Length <= 1) continue;
                        sRslt[0] = sRslt[0].Replace("\n", "");
                        sRslt[0] = sRslt[0].Replace("\r", "");
                        sRslt[1] = sRslt[1].Replace("\n", "");
                        sRslt[1] = sRslt[1].Replace("\r", "");
                        CLOT.ABF_TEMP[i - 1, 0] = sRslt[0];
                        CLOT.ABF_TEMP[i - 1, 1] = sRslt[1];
                    }
                }
            }
            return true;
        }
        catch(Exception ex){
            LogWR_.SaveLogException("GET_ABF_LIST FAIL", ex);
        }
        return false;
    }

    public static void DEL_LOT_INFO(){
        if (File.Exists(PATH_.CurrLot)) File.Delete(PATH_.CurrLot);
    }
    public static void SET_LOT_INFO(string CurrLotInfoList){
        FILE_.WR_File(PATH_.CurrLot, CurrLotInfoList, false);
    }
    public static void GET_LOT_INFO(){
        if (!File.Exists(PATH_.CurrLot)) return;
        try {
            string[] sLine = File.ReadAllText(PATH_.CurrLot).Split(ETC.CrLf);
            string[] sRslt = sLine[0].Split(',');
            if (sRslt.Length > 32){
                CLOT.GET_LOT.LotID                  = sRslt[0];
                CLOT.GET_LOT.LotType                = int.Parse(sRslt[1]);
                CLOT.GET_LOT.Qty                    = int.Parse(sRslt[2]);
                CLOT.GET_LOT.ProductType            = sRslt[3];
                CLOT.GET_LOT.ToolNo                 = sRslt[4];
                CLOT.GET_LOT.ITS                    = int.Parse(sRslt[5]);
                CLOT.GET_LOT.ITS_LotID_IN           = sRslt[6];
                CLOT.GET_LOT.ITS_LotID_CT           = sRslt[7];
                CLOT.GET_LOT.UnitSizeX              = double.Parse(sRslt[8]);
                CLOT.GET_LOT.UnitSizeY              = double.Parse(sRslt[9]);
                CLOT.GET_LOT.UnitSize_USL           = double.Parse(sRslt[10]);
                CLOT.GET_LOT.UnitSize_LSL           = double.Parse(sRslt[11]);
                CLOT.GET_LOT.ABFMATERIAL            = sRslt[12];
                CLOT.GET_LOT.LANDPKGX               = double.Parse(sRslt[13]);
                CLOT.GET_LOT.LANDPKGX_UPPER         = double.Parse(sRslt[14]);
                CLOT.GET_LOT.LANDPKGX_LOWER         = double.Parse(sRslt[15]);
                CLOT.GET_LOT.LANDPKGY               = double.Parse(sRslt[16]);
                CLOT.GET_LOT.LANDPKGY_UPPER         = double.Parse(sRslt[17]);
                CLOT.GET_LOT.LANDPKGY_LOWER         = double.Parse(sRslt[18]);
                CLOT.GET_LOT.WorkSort               = sRslt[19];
                CLOT.GET_LOT.WorkScope              = sRslt[20];
                CLOT.GET_LOT.BarcodeSp1             = sRslt[21];
                CLOT.GET_LOT.BarcodeSp2             = sRslt[22];

                //22.0927 HK.PARK 추가
                CLOT.GET_LOT.Thick                  = double.Parse(sRslt[23]);
                CLOT.GET_LOT.Thick_USL              = double.Parse(sRslt[24]);
                CLOT.GET_LOT.Thick_LSL              = double.Parse(sRslt[25]);

                CLOT.GET_LOT.ProcCD                 = sRslt[26];
                CLOT.GET_LOT.ProcName               = sRslt[27];
                CLOT.GET_LOT.WorkCondition          = sRslt[28];
                CLOT.GET_LOT.ProcCondition_1        = sRslt[29];
                CLOT.GET_LOT.ProcCondition_2        = sRslt[30];
                CLOT.GET_LOT.ProcCondition_3        = sRslt[31];
                CLOT.GET_LOT.ProcCondition_4        = sRslt[32];

                //22.1121 HK.PARK 추가
                CLOT.GET_LOT.BeginTime              = sRslt[33];

                //22.1128 HK.PARK 추가
                CLOT.GET_LOT.BOT_LANDTOPKG_X        = double.Parse(sRslt[34]);
                CLOT.GET_LOT.BOT_CHAMFERLEN_TM_X    = double.Parse(sRslt[35]);
                CLOT.GET_LOT.BOT_CHAMFERLEN_TP_X    = double.Parse(sRslt[36]);
                CLOT.GET_LOT.BOT_LANDTOPKG_Y        = double.Parse(sRslt[37]);
                CLOT.GET_LOT.BOT_CHAMFERLEN_TM_Y    = double.Parse(sRslt[38]);
                CLOT.GET_LOT.BOT_CHAMFERLEN_TP_Y    = double.Parse(sRslt[39]);

                CLOT.GET_LOT.TOP_LANDTOPKG_X        = double.Parse(sRslt[40]);
                CLOT.GET_LOT.TOP_CHAMFERLEN_TM_X    = double.Parse(sRslt[41]);
                CLOT.GET_LOT.TOP_CHAMFERLEN_TP_X    = double.Parse(sRslt[42]);
                CLOT.GET_LOT.TOP_LANDTOPKG_Y        = double.Parse(sRslt[43]);
                CLOT.GET_LOT.TOP_CHAMFERLEN_TM_Y    = double.Parse(sRslt[44]);
                CLOT.GET_LOT.TOP_CHAMFERLEN_TP_Y    = double.Parse(sRslt[45]);

                CLOT.GET_LOT.IDSp1                  = sRslt[46];
                CLOT.GET_LOT.IDSp2                  = sRslt[47];
            }
        }
        catch(Exception EX){
            LogWR_.SaveLogException("LOT INFO READING FAIL", EX);
        }
    }

    public static void SAVE_WORKED_LOT_INFO(){
        try{
            for (int n = 0; n < CLOT.FINISH_LOT.Length; n++){
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "LotID_" + n.ToString(), CLOT.FINISH_LOT[n].LotID);
                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "LotType_" + n.ToString(), CLOT.FINISH_LOT[n].LotType);
                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "Qty_" + n.ToString(), CLOT.FINISH_LOT[n].Qty);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "ProductType_" + n.ToString(), CLOT.FINISH_LOT[n].ProductType);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "ToolNo_" + n.ToString(), CLOT.FINISH_LOT[n].ToolNo);
                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "ITS_" + n.ToString(), CLOT.FINISH_LOT[n].ITS);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "ITS_LotID_IN_" + n.ToString(), CLOT.FINISH_LOT[n].ITS_LotID_IN);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "ITS_LotID_CT_" + n.ToString(), CLOT.FINISH_LOT[n].ITS_LotID_CT);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "UnitSizeX_" + n.ToString(), CLOT.FINISH_LOT[n].UnitSizeX);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "UnitSizeY_" + n.ToString(), CLOT.FINISH_LOT[n].UnitSizeY);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "UnitSize_USL_" + n.ToString(), CLOT.FINISH_LOT[n].UnitSize_USL);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "UnitSize_LSL_" + n.ToString(), CLOT.FINISH_LOT[n].UnitSize_LSL);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "ABFMATERIAL_" + n.ToString(), CLOT.FINISH_LOT[n].ABFMATERIAL);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "LANDPKGX_" + n.ToString(), CLOT.FINISH_LOT[n].LANDPKGX);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "LANDPKGX_UPPER_" + n.ToString(), CLOT.FINISH_LOT[n].LANDPKGX_UPPER);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "LANDPKGX_LOWER_" + n.ToString(), CLOT.FINISH_LOT[n].LANDPKGX_LOWER);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "LANDPKGY_" + n.ToString(), CLOT.FINISH_LOT[n].LANDPKGY);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "LANDPKGY_UPPER_" + n.ToString(), CLOT.FINISH_LOT[n].LANDPKGY_UPPER);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "LANDPKGY_LOWER_" + n.ToString(), CLOT.FINISH_LOT[n].LANDPKGY_LOWER);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "WorkSort_" + n.ToString(), CLOT.FINISH_LOT[n].WorkSort);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "WorkScope_" + n.ToString(), CLOT.FINISH_LOT[n].WorkScope);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "BarcodeSp1_" + n.ToString(), CLOT.FINISH_LOT[n].BarcodeSp1);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "BarcodeSp2_" + n.ToString(), CLOT.FINISH_LOT[n].BarcodeSp2);

                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "IDSp1_" + n.ToString(), CLOT.FINISH_LOT[n].IDSp1);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "IDSp2_" + n.ToString(), CLOT.FINISH_LOT[n].IDSp2);

                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "Thick_" + n.ToString(), CLOT.FINISH_LOT[n].Thick);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "Thick_USL_" + n.ToString(), CLOT.FINISH_LOT[n].Thick_USL);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "Thick_LSL_" + n.ToString(), CLOT.FINISH_LOT[n].Thick_LSL);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "ProcCD_" + n.ToString(), CLOT.FINISH_LOT[n].ProcCD);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "ProcName_" + n.ToString(), CLOT.FINISH_LOT[n].ProcName);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "WorkCondition_" + n.ToString(), CLOT.FINISH_LOT[n].WorkCondition);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "ProcCondition_1_" + n.ToString(), CLOT.FINISH_LOT[n].ProcCondition_1);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "ProcCondition_2_" + n.ToString(), CLOT.FINISH_LOT[n].ProcCondition_2);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "ProcCondition_3_" + n.ToString(), CLOT.FINISH_LOT[n].ProcCondition_3);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "ProcCondition_4_" + n.ToString(), CLOT.FINISH_LOT[n].ProcCondition_4);

                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "InCnt_" + n.ToString(), CLOT.FINISH_LOT[n].InCnt);
                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "OutCnt_" + n.ToString(), CLOT.FINISH_LOT[n].OutCnt);
                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "CurCnt_" + n.ToString(), CLOT.FINISH_LOT[n].CurCnt);
                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "PassCnt_" + n.ToString(), CLOT.FINISH_LOT[n].PassCnt);

                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "LoadingCount_" + n.ToString(), CLOT.FINISH_LOT[n].LoadingCount);
                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "ExceptCount_" + n.ToString(), CLOT.FINISH_LOT[n].ExceptCount);
                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "UnloadingCount_" + n.ToString(), CLOT.FINISH_LOT[n].UnloadingCount);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "WorkSort_" + n.ToString(), CLOT.FINISH_LOT[n].WorkSort);

                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "BarcodeSp1_" + n.ToString(), CLOT.FINISH_LOT[n].BarcodeSp1);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "BarcodeSp2_" + n.ToString(), CLOT.FINISH_LOT[n].BarcodeSp2);

                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "LotCnt_" + n.ToString(), CLOT.FINISH_LOT[n].LotCnt);
                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "StripCnt_" + n.ToString(), CLOT.FINISH_LOT[n].StripCnt);
                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "UnitCnt_" + n.ToString(), CLOT.FINISH_LOT[n].UnitCnt);
                
                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "GoodUnit_" + n.ToString(), CLOT.FINISH_LOT[n].GoodUnit);
                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "ReworkUnit_" + n.ToString(), CLOT.FINISH_LOT[n].ReworkUnit);
                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "NGUnit_" + n.ToString(), CLOT.FINISH_LOT[n].NGUnit);

                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "ITSCount_" + n.ToString(), CLOT.FINISH_LOT[n].ITSCount);

                //22.1121 HK.PARK 추가
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "BeginTime_" + n.ToString(), CLOT.FINISH_LOT[n].BeginTime);
                FILE_.WRString(PATH_.WorkedLot, "FINISH_LOT", "EndTime_" + n.ToString(), CLOT.FINISH_LOT[n].EndTime);

                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "GoodTray_" + n.ToString(), CLOT.FINISH_LOT[n].GoodTray);
                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "NGTray_" + n.ToString(), CLOT.FINISH_LOT[n].NGTray);

                FILE_.WRInt(PATH_.WorkedLot, "FINISH_LOT", "TotalUnit_" + n.ToString(), CLOT.FINISH_LOT[n].TotalUnit);

                //22.1128 HK.PARK 추가
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "BOT_LANDTOPKG_X_" + n.ToString(), CLOT.FINISH_LOT[n].BOT_LANDTOPKG_X);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "BOT_CHAMFERLEN_TM_X_" + n.ToString(), CLOT.FINISH_LOT[n].BOT_CHAMFERLEN_TM_X);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "BOT_CHAMFERLEN_TP_X_" + n.ToString(), CLOT.FINISH_LOT[n].BOT_CHAMFERLEN_TP_X);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "BOT_LANDTOPKG_Y_" + n.ToString(), CLOT.FINISH_LOT[n].BOT_LANDTOPKG_Y);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "BOT_CHAMFERLEN_TM_Y_" + n.ToString(), CLOT.FINISH_LOT[n].BOT_CHAMFERLEN_TM_Y);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "BOT_CHAMFERLEN_TP_Y_" + n.ToString(), CLOT.FINISH_LOT[n].BOT_CHAMFERLEN_TP_Y);

                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "TOP_LANDTOPKG_X_" + n.ToString(), CLOT.FINISH_LOT[n].TOP_LANDTOPKG_X);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "TOP_CHAMFERLEN_TM_X_" + n.ToString(), CLOT.FINISH_LOT[n].TOP_CHAMFERLEN_TM_X);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "TOP_CHAMFERLEN_TP_X_" + n.ToString(), CLOT.FINISH_LOT[n].TOP_CHAMFERLEN_TP_X);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "TOP_LANDTOPKG_Y_" + n.ToString(), CLOT.FINISH_LOT[n].TOP_LANDTOPKG_Y);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "TOP_CHAMFERLEN_TM_Y_" + n.ToString(), CLOT.FINISH_LOT[n].TOP_CHAMFERLEN_TM_Y);
                FILE_.WRDouble(PATH_.WorkedLot, "FINISH_LOT", "TOP_CHAMFERLEN_TP_Y_" + n.ToString(), CLOT.FINISH_LOT[n].TOP_CHAMFERLEN_TP_Y);
            }
        }
        catch(Exception EX){
            LogWR_.SaveLogException("WORKED LOT INFO WRITE FAIL", EX);
        }        
    }

    public static void READ_WORKED_LOT_INFO(){
        try{
            for (int n = 0; n < CLOT.FINISH_LOT.Length; n++){
                CLOT.FINISH_LOT[n].LotID                = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "LotID_" + n.ToString(), "");
                CLOT.FINISH_LOT[n].LotType              = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "LotType_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].Qty                  = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "Qty_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].ProductType          = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "ProductType_" + n.ToString(), "");
                CLOT.FINISH_LOT[n].ToolNo               = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "ToolNo_" + n.ToString(), "");
                CLOT.FINISH_LOT[n].ITS                  = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "ITS_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].ITS_LotID_IN         = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "ITS_LotID_IN_" + n.ToString(), "");
                CLOT.FINISH_LOT[n].ITS_LotID_CT         = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "ITS_LotID_CT_" + n.ToString(), "");
                CLOT.FINISH_LOT[n].UnitSizeX            = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "UnitSizeX_" + n.ToString(), 0.0);
                CLOT.FINISH_LOT[n].UnitSizeY            = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "UnitSizeY_" + n.ToString(), 0.0);
                CLOT.FINISH_LOT[n].UnitSize_USL         = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "UnitSize_USL_" + n.ToString(), 0.0);
                CLOT.FINISH_LOT[n].UnitSize_LSL         = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "UnitSize_LSL_" + n.ToString(), 0.0);
                CLOT.FINISH_LOT[n].ABFMATERIAL          = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "ABFMATERIAL_" + n.ToString(), "");
                CLOT.FINISH_LOT[n].LANDPKGX             = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "LANDPKGX_" + n.ToString(), 0.0);
                CLOT.FINISH_LOT[n].LANDPKGX_UPPER       = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "LANDPKGX_UPPER_" + n.ToString(), 0.0);
                CLOT.FINISH_LOT[n].LANDPKGX_LOWER       = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "LANDPKGX_LOWER_" + n.ToString(), 0.0);
                CLOT.FINISH_LOT[n].LANDPKGY             = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "LANDPKGY_" + n.ToString(), 0.0);
                CLOT.FINISH_LOT[n].LANDPKGY_UPPER       = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "LANDPKGY_UPPER_" + n.ToString(), 0.0);
                CLOT.FINISH_LOT[n].LANDPKGY_LOWER       = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "LANDPKGY_LOWER_" + n.ToString(), 0.0);
                CLOT.FINISH_LOT[n].WorkSort             = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "WorkSort_" + n.ToString(), "");
                CLOT.FINISH_LOT[n].WorkScope            = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "WorkScope_" + n.ToString(), "");
                CLOT.FINISH_LOT[n].BarcodeSp1           = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "BarcodeSp1_" + n.ToString(), "");
                CLOT.FINISH_LOT[n].BarcodeSp2           = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "BarcodeSp2_" + n.ToString(), "");

                CLOT.FINISH_LOT[n].IDSp1                = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "IDSp1_" + n.ToString(), "");
                CLOT.FINISH_LOT[n].IDSp2                = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "IDSp2_" + n.ToString(), "");
                
                CLOT.FINISH_LOT[n].Thick                = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "Thick_" + n.ToString(), 0.0);
                CLOT.FINISH_LOT[n].Thick_USL            = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "Thick_USL_" + n.ToString(), 0.0);
                CLOT.FINISH_LOT[n].Thick_LSL            = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "Thick_LSL_" + n.ToString(), 0.0);
                CLOT.FINISH_LOT[n].ProcCD               = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "ProcCD_" + n.ToString(), "");
                CLOT.FINISH_LOT[n].ProcName             = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "ProcName_" + n.ToString(), "");
                CLOT.FINISH_LOT[n].WorkCondition        = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "WorkCondition_" + n.ToString(), "");
                CLOT.FINISH_LOT[n].ProcCondition_1      = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "ProcCondition_1_" + n.ToString(), "");
                CLOT.FINISH_LOT[n].ProcCondition_2      = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "ProcCondition_2_" + n.ToString(), "");
                CLOT.FINISH_LOT[n].ProcCondition_3      = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "ProcCondition_3_" + n.ToString(), "");
                CLOT.FINISH_LOT[n].ProcCondition_4      = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "ProcCondition_4_" + n.ToString(), "");

                CLOT.FINISH_LOT[n].InCnt                = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "InCnt_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].OutCnt               = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "OutCnt_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].CurCnt               = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "CurCnt_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].PassCnt              = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "PassCnt_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].LoadingCount         = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "LoadingCount_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].ExceptCount          = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "ExceptCount_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].UnloadingCount       = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "UnloadingCount_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].WorkSort             = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "WorkSort_" + n.ToString(), "");

                CLOT.FINISH_LOT[n].BarcodeSp1           = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "BarcodeSp1_" + n.ToString(), "");
                CLOT.FINISH_LOT[n].BarcodeSp2           = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "BarcodeSp2_" + n.ToString(), "");

                CLOT.FINISH_LOT[n].LotCnt               = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "LotCnt_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].StripCnt             = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "StripCnt_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].UnitCnt              = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "UnitCnt_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].GoodUnit             = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "GoodUnit_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].ReworkUnit           = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "ReworkUnit_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].NGUnit               = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "NGUnit_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].ITSCount             = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "ITSCount_" + n.ToString(), 0);

                //22.1121 HK.PARK 추가
                CLOT.FINISH_LOT[n].BeginTime            = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "BeginTime_" + n.ToString(), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                CLOT.FINISH_LOT[n].EndTime              = FILE_.RDString(PATH_.WorkedLot, "FINISH_LOT", "EndTime_" + n.ToString(), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                CLOT.FINISH_LOT[n].GoodTray             = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "GoodTray_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].NGTray               = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "NGTray_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].TotalUnit            = FILE_.RDInt(PATH_.WorkedLot, "FINISH_LOT", "TotalUnit_" + n.ToString(), 0);

                //22.1128 HK.PARK 추가
                CLOT.FINISH_LOT[n].BOT_LANDTOPKG_X      = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "BOT_LANDTOPKG_X_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].BOT_CHAMFERLEN_TM_X  = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "BOT_CHAMFERLEN_TM_X_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].BOT_CHAMFERLEN_TP_X  = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "BOT_CHAMFERLEN_TP_X_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].BOT_LANDTOPKG_Y      = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "BOT_LANDTOPKG_Y_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].BOT_CHAMFERLEN_TM_Y  = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "BOT_CHAMFERLEN_TM_Y_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].BOT_CHAMFERLEN_TP_Y  = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "BOT_CHAMFERLEN_TP_Y_" + n.ToString(), 0);

                CLOT.FINISH_LOT[n].TOP_LANDTOPKG_X      = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "TOP_LANDTOPKG_X_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].TOP_CHAMFERLEN_TM_X  = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "TOP_CHAMFERLEN_TM_X_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].TOP_CHAMFERLEN_TP_X  = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "TOP_CHAMFERLEN_TP_X_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].TOP_LANDTOPKG_Y      = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "TOP_LANDTOPKG_Y_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].TOP_CHAMFERLEN_TM_Y  = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "TOP_CHAMFERLEN_TM_Y_" + n.ToString(), 0);
                CLOT.FINISH_LOT[n].TOP_CHAMFERLEN_TP_Y  = FILE_.RDDouble(PATH_.WorkedLot, "FINISH_LOT", "TOP_CHAMFERLEN_TP_Y_" + n.ToString(), 0);
            }
        }
        catch (Exception EX){
            LogWR_.SaveLogException("WORKED LOT INFO READ FAIL", EX);
        }
    }
    
    public static string GET_JOB_FILE_NAME(){
        if (!File.Exists(PATH_.CurrJOB)) return "";
        return File.ReadAllText(PATH_.CurrJOB);
    }
    public static string GET_VISION_FILE_NAME(){
        if (!File.Exists(PATH_.CurrVISION)) return "";
        return File.ReadAllText(PATH_.CurrVISION);
    }
    public static string GET_PPID_NAME(){
        if (!File.Exists(PATH_.CurrPPID)) return "";
        try{
            string s = File.ReadAllText(PATH_.CurrPPID);
            FileInfo fi = new FileInfo(s);
            return fi.Name.Replace(".txt", "");
        }
        catch (Exception ex){
            MessageBox.Show("PPID list reading fail !" + ETC.NewLine + ex.ToString());
            return "";
        }
    }

    public static bool GET_PPID_RECIPE_NAME(string mPATH, ref string fGROUP, ref string fDEVICE, ref string fVISION, ref string fSAW){
        if (!File.Exists(mPATH)) return false;
        string[] sLine = File.ReadAllText(mPATH).Split(ETC.CrLf);
        for (int i = 0; i < sLine.Length; i++){
            sLine[i] = sLine[i].Replace("\r", "");
        }
        fGROUP  = sLine[0];
        fDEVICE = sLine[1];
        fVISION = sLine[2];
        fSAW    = sLine[3];
        return true;
    }

    public static string GET_RECIPE_FILE_NAME(){
        if (!File.Exists(PATH_.CurrJOB)) return "";
        string s            = File.ReadAllText(PATH_.CurrJOB);
        FileInfo fi         = new FileInfo(s);
        string sJOB_NAME    = fi.Name.Replace(".job", "");
        string[] sARR       = s.Split('\\');
        int iCNT            = sARR.Length - 1;
        return PATH_.DATA + sARR[iCNT - 1] + "\\" + sJOB_NAME;
    }

    public static bool OpenJobFile(){
        if (!CHK_JOB_FILE()){
            bJobMiss = true;
            return false;
        }
        sCurrProcessName = GET_RECIPE_FILE_NAME();
        LD_JOB_FILE();
        return true;
    }

    public static void GetPPID(DataGridView dgv){
        try{
            DataGridViewRow row;
            string[] Temp = Directory.GetFiles(PATH_.PPID);
            if (Temp.Length <= 0) return;
            dgv.RowCount = Temp.Length;
            CMES.PPID_LIST = new string[Temp.Length];
            for (int i = 0; i < Temp.Length; i++){
                string[] arr                = Temp[i].Split('\\');
                int idx                     = arr.Length - 1;
                string[] sPPID              = arr[idx].Split('.');
                dgv.Rows[i].Cells[0].Value  = (i + 1).ToString();
                dgv.Rows[i].Cells[1].Value  = sPPID[0];
                CMES.PPID_LIST[i]           = sPPID[0];
                row                         = dgv.Rows[i];
                row.Height                  = 40;
            }
            CLEAR_GRID_SELECTED(ref dgv);
        }
        catch (Exception ex){
            MessageBox.Show("PPID LIST OPEN FAIL (GetPPID) " + ex.ToString());
        }
    }
    public static void GetWorkGroup(ListView lv){
        lv.Items.Clear();
        DirectoryInfo di = new DirectoryInfo(PATH_.DATA);
        if (di.Exists){
            DirectoryInfo[] gInfo = di.GetDirectories("*", SearchOption.AllDirectories);
            foreach (DirectoryInfo fn in gInfo){
                string sFullName = fn.FullName;
                string[] sArr   = sFullName.Split('\\');
                int nArr        = sArr.Length - 1;
                lv.Items.Add(sArr[nArr].Trim());
            }
        }
    }
    public static void GetWorkRecipe(ListView lGroup, ListView lDevice, DataGridView gDevice, string sRecipe, bool DelFile){
        lDevice.Items.Clear();
        string[] s = Directory.GetFiles(PATH_.DATA + sRecipe);
        if (s.Length == 0){
            if (DelFile) GetWorkGroup(lGroup);
            return;
        }
        for (int i = 0; i < s.Length; i++){
            string[] arr    = s[i].Split('\\');
            int idx         = arr.Length - 1;
            lDevice.Items.Add(arr[idx].Trim());
        }
        lDevice.EndUpdate();

        gDevice.RowCount    = s.Length;
        RecipeList          = new string[s.Length];
        for (int i = 0; i < s.Length; i++){
            gDevice.Rows[i].Cells[0].Value  = i.ToString();
            string[] aDev                   = s[i].Split('\\');
            int nIndx                       = aDev.Length - 1;
            gDevice.Rows[i].Cells[1].Value  = aDev[nIndx].Trim();
            RecipeList[i]                   = aDev[nIndx].Replace(".jog", "");
        }
        CLEAR_GRID_SELECTED(ref gDevice);
    }

    public static bool GetSomeWorkGroup(string newrecipe){
        string sFullName;
        string sGroupName;

        DirectoryInfo DI = new DirectoryInfo(PATH_.DATA);
        if (DI.Exists){
            DirectoryInfo[] CInfo = DI.GetDirectories("*", SearchOption.AllDirectories);
            foreach (DirectoryInfo di in CInfo){
                sFullName       = di.FullName;
                string[] sARR   = sFullName.Split('\\');
                int iARR        = sARR.Length - 1;
                sGroupName      = sARR[iARR].Trim();
                if (newrecipe == sGroupName) return false;
            }
        }
        return true;
    }
    public static bool GetSomeWorkDevice(string group, string newrecipe){
        string[] s = Directory.GetFiles(PATH_.DATA + group);
        for (int i = 0; i <= s.Length - 1; i++){
            string[] arr    = s[i].Split('\\');
            int idx         = arr.Length - 1;
            string temp     = arr[idx].Replace(".jog", "");
            if (newrecipe == temp) return false;
        }
        return true;
    }

    public static void GetABF(ListView LV){
        LV.Items.Clear();
        DirectoryInfo di = new DirectoryInfo(PATH_.ABF_LIST);
        if (di.Exists){
            DirectoryInfo[] gInfo = di.GetDirectories("*", SearchOption.AllDirectories);
            foreach (DirectoryInfo fn in gInfo){
                string sFullName = fn.FullName;
                string[] sArr = sFullName.Split('\\');
                int nArr = sArr.Length - 1;
                LV.Items.Add(sArr[nArr].Trim());
            }
        }
    }
    public static bool GetSomeABF(string newABF){
        string sFullName;
        string sABF;

        DirectoryInfo DI = new DirectoryInfo(PATH_.ABF_LIST);
        if (DI.Exists){
            DirectoryInfo[] CurDI = DI.GetDirectories("*", SearchOption.AllDirectories);
            foreach (DirectoryInfo di in CurDI){
                sFullName = di.FullName;
                string[] sARR = sFullName.Split('\\');
                int iARR = sARR.Length - 1;
                sABF = sARR[iARR].Trim();
                if (newABF == sABF) return false;
            }
        }
        return true;
    }

    public static void GetBladeBarcode(ListView lvABF, ListView lvBlade, string sABF, bool DelFile){
        lvBlade.Items.Clear();
        string[] s = Directory.GetFiles(PATH_.ABF_LIST + sABF);
        if (s.Length == 0){
            if (DelFile) GetABF(lvABF);
            return;
        }
        for (int i = 0; i < s.Length; i++){
            string[] arr    = s[i].Split('\\');
            int idx         = arr.Length - 1;
            string temp     = arr[idx].Replace(".jog", "");
            string tmep1    = $"{ Path.GetFileNameWithoutExtension(arr[idx])}";
            lvBlade.Items.Add(/*arr[idx].Trim()*/tmep1);
        }
        lvBlade.EndUpdate();
    }
    public static bool GetSomeBladeBarcode(string abf, string newblade){
        string[] s = Directory.GetFiles(PATH_.ABF_LIST + abf);
        for (int i = 0; i <= s.Length - 1; i++){
            string[] arr = s[i].Split('\\');
            int idx = arr.Length - 1;
            string temp = arr[idx].Replace(".jog", "");
            if (newblade == temp) return false;
        }
        return true;
    }

    public static bool CHK_FILE(string path){
        if (File.Exists(path)) return true;
        return false;
    }
    public static bool CHK_JOB_FILE(){
        string fn = sCurrJobName;
        if (File.Exists(fn)) return true;
        bJobMiss = true;
        return false;
    }

    public static bool LD_JOB_FILE(){
        if (sCurrJobName == "") return false;
        FileInfo fi     = new FileInfo(sCurrJobName);
        sJobName        = fi.Name.Replace(".job", "");
        string sDIR     = fi.Directory.FullName;
        string[] arrDIR = sDIR.Split('\\');
        int iCNT        = arrDIR.Length - 1;
        sGroupName      = arrDIR[iCNT];
        TEACH_.Read_UsePicker();
        TEACH_.RD_MDLPara();
        TEACH_.Read_UnitCleanData();
        TEACH_.RD_MTDATA();
        return true;
    }

    static public bool InCmd(string cmd, string str){
        if (cmd.IndexOf(str) > -1) return true;
        return false;
    }//Fend

    public static void SET_GRID_COLOR(object GRID, int r, int c, Color col){
        DataGridView g = GRID as DataGridView;
        if (g.Rows[r].Cells[c].Style.BackColor != col) g.Rows[r].Cells[c].Style.BackColor = col;
    }

    public static void SET_GRID_DRAW(ref DataGridView g, int CntX, int CntY, int OrgWidth, int OrgHeight){
        int width = 0;
        int height = 0;
        try{
            width           = OrgWidth / CntX;
            height          = OrgHeight / CntY;  //(int)Math.Round((double)OrgHeight / CntY);//

            g.RowCount      = CntY;
            g.ColumnCount   = CntX;
        }
        catch (Exception ex){
            MessageBox.Show("[mUTIL] SET_GRID_DRAW FAIL (" + g.Name + ")" + ETC.NewLine + ex.ToString());
            LogWR_.SaveLogException("[mUTIL] SET_GRID_DRAW FAIL (" + g.Name + ")", ex);
        }

        for (int i = 0; i < CntY; i++){
            g.Rows[i].Height = height;
        }
        for (int i = 0; i < CntX; i++){
            g.Columns[i].Width                      = width;
            g.Columns[i].SortMode                   = DataGridViewColumnSortMode.NotSortable;
            g.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        g.Columns[CntX - 1].SortMode    = DataGridViewColumnSortMode.NotSortable;
        g.Width                         = width * CntX;
        g.Height                        = height * CntY + 3;
        g.ClearSelection();
    }

    public static void GET_GRID_NUMBER(DataGridView g, ref int Col, ref int Row){
        if (g.CurrentCell == null || g.CurrentRow == null) return;
        Col = g.CurrentCell.ColumnIndex;
        Row = g.CurrentRow.Index;
    }

    public static void CLEAR_GRID_SELECTED(ref DataGridView g){
        for (int i = 0; i < g.RowCount; i++){
            for (int j = 0; j < g.ColumnCount; j++) { 
                g.Rows[i].Cells[j].Selected = false; 
            }
        }
        g.ClearSelection();
    }

    public static void GET_GRID_MOTOR_DATA(DataGridView g, int row, ref int posnum, ref double[] posdata){
        posnum = int.Parse(g[0, row].Value.ToString());
        for (int i = 0; i < posdata.Length; i++) { 
            posdata[i] = double.Parse(g[2 + i, row].Value.ToString());
        }
    }
    public static bool CLEAR_GRID_MOTOR_SELECTED(DataGridView g, int Row, double[] posdata){
        bool bRtn = true;
        for (int i = 0; i < posdata.Length; i++){
            double dvalue = Convert.ToDouble(g[2 + i, Row].Value);
            if (dvalue != posdata[i]) bRtn = false;
        }
        CLEAR_GRID_SELECTED(ref g);
        return bRtn;
    }

    public static void MCGrid(DataGridView grd, int[] iMCPara, ref int iHeight){
        DataGridViewRow row;
        iHeight = 3;
        grd.Rows.Clear();
        grd.RowCount = iMCPara.Length;
        try{
            for (int i = 0; i < iMCPara.Length; i++){
                grd[0, i].Value             = iMCPara[i].ToString();
                grd[1, i].Value             = MCParaName[iMCPara[i]];
                grd[2, i].Value             = prMACHINE[iMCPara[i]];
                row                         = grd.Rows[i];
                row.Height                  = 30;
                iHeight                     += row.Height;

                grd[0, i].Style.BackColor   = ComBackColor;
                grd[1, i].Style.BackColor   = ComBackColor;
            }
            //grd.AllowUserToAddRows      = false;
            //grd.AllowUserToDeleteRows   = false;
            //grd.AllowUserToOrderColumns = true;
            //grd.ReadOnly = true;
            //grd.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            //grd.AllowUserToResizeColumns = false;
            //grd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //grd.AllowUserToResizeRows = false;
            //grd.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            CLEAR_GRID_SELECTED(ref grd);
            grd.Height = iHeight;
        }
        catch (Exception ex) { MessageBox.Show(ex.ToString()); }
    }
    public static void MDGrid(DataGridView grd, int[] iMDPara, ref int iHeight){
        DataGridViewRow row;
        iHeight = 3;
        grd.Rows.Clear();
        grd.RowCount = iMDPara.Length;
        try{
            for (int i = 0; i < iMDPara.Length; i++){
                grd[0, i].Value = iMDPara[i].ToString();
                grd[1, i].Value = MDParaName[iMDPara[i]];
                grd[2, i].Value = prMODEL[iMDPara[i]];
                row             = grd.Rows[i];
                row.Height      = 30;
                iHeight         += row.Height;
            }
            CLEAR_GRID_SELECTED(ref grd);
            grd.Height = iHeight;
        }
        catch (Exception ex) { MessageBox.Show(ex.ToString()); }
    }

    public static void PosGrid(DataGridView grd, int mt, int[] pos, ref int iHeight){
        DataGridViewRow row;
        iHeight = 3 + 26;
        grd.Rows.Clear();
        grd.RowCount = pos.Length;
        try{
            for (int i = 0; i < pos.Length; i++){
                grd[0, i].Value = pos[i].ToString();
                grd[1, i].Value = PosName[mt, pos[i]];
                grd[2, i].Value = mtDATA[mt, pos[i]].Pos;
                grd[3, i].Value = "GET";
                grd[4, i].Value = "GO";
                row             = grd.Rows[i];
                row.Height      = 30;
                iHeight         += row.Height;

                if (pos[i] < CNT_.ComPos){
                    grd[0, i].Style.BackColor = ComBackColor;
                    grd[1, i].Style.BackColor = ComBackColor;
                }
            }
            CLEAR_GRID_SELECTED(ref grd);
            grd.Height = iHeight;
        }
        catch (Exception ex) { MessageBox.Show(ex.ToString()); }
    }
    public static void PosGrid(DataGridView grd, int mt1, int mt2, int[] pos, ref int iHeight){
        DataGridViewRow row;
        iHeight = 3 + 26;
        grd.Rows.Clear();
        grd.RowCount = pos.Length;
        try{
            for (int i = 0; i < pos.Length; i++){
                grd[0, i].Value = pos[i].ToString();
                grd[1, i].Value = PosName[mt1, pos[i]];
                grd[2, i].Value = mtDATA[mt1, pos[i]].Pos;
                grd[3, i].Value = mtDATA[mt2, pos[i]].Pos;
                grd[4, i].Value = "GET";
                grd[5, i].Value = "GET";
                grd[6, i].Value = "GO";
                grd[7, i].Value = "GO";
                row             = grd.Rows[i];
                row.Height      = 30;
                iHeight         += row.Height;

                if (pos[i] < CNT_.ComPos){
                    grd[0, i].Style.BackColor = ComBackColor;
                    grd[1, i].Style.BackColor = ComBackColor;
                }
            }
            CLEAR_GRID_SELECTED(ref grd);
        }
        catch (Exception ex) { MessageBox.Show(ex.ToString()); }
    }
    public static void PosGrid(DataGridView grd, int mt1, int mt2, int mt3, int[] pos, ref int height){
        DataGridViewRow row;
        height = 3 + 26;
        grd.Rows.Clear();
        grd.RowCount = pos.Length;
        try{
            for (int i = 0; i < pos.Length; i++){
                grd[0, i].Value     = pos[i].ToString();
                grd[1, i].Value     = PosName[mt1, pos[i]];
                grd[2, i].Value     = mtDATA[mt1, pos[i]].Pos;
                grd[3, i].Value     = mtDATA[mt2, pos[i]].Pos;
                grd[4, i].Value     = mtDATA[mt3, pos[i]].Pos;
                grd[5, i].Value     = "GET";
                grd[6, i].Value     = "GET";
                grd[7, i].Value     = "GET";
                grd[8, i].Value     = "GO";
                grd[9, i].Value     = "GO";
                grd[10, i].Value    = "GO";
                row                 = grd.Rows[i];
                row.Height          = 30;
                height              += row.Height;

                if (pos[i] < CNT_.ComPos){
                    grd[0, i].Style.BackColor = ComBackColor;
                    grd[1, i].Style.BackColor = ComBackColor;
                }
            }
            CLEAR_GRID_SELECTED(ref grd);
            grd.Height = height;
        }
        catch (Exception ex) { MessageBox.Show(ex.ToString()); }
    }

    public static void PosGridOption1(DataGridView grd, int mt, int[] pos, ref int iHeight){
        DataGridViewRow row;
        iHeight = 3 + 26;
        grd.Rows.Clear();
        grd.RowCount = pos.Length;

        try{
            for (int i = 0; i < pos.Length; i++){
                grd[0, i].Value = pos[i].ToString();
                grd[1, i].Value = PosName[mt, pos[i]];
                grd[2, i].Value = mtDATA[mt, pos[i]].Pos;
                //grd[3, i].Value = mtDATA[mt2, pos[i]].Pos;
                grd[4, i].Value = "GET";
                //grd[5, i].Value = "GET";
                grd[6, i].Value = "GO";
                row = grd.Rows[i];
                row.Height = 30;
                iHeight += row.Height;

                if (pos[i] < CNT_.ComPos){
                    grd[0, i].Style.BackColor = ComBackColor;
                    grd[1, i].Style.BackColor = ComBackColor;
                }
            }
            CLEAR_GRID_SELECTED(ref grd);
        }
        catch (Exception ex) { MessageBox.Show(ex.ToString()); }
    }   

    public static void PosGridOption(DataGridView grd, int mt1, int mt2, int[] pos, ref int iHeight){
        DataGridViewRow row;
        iHeight = 3 + 26;
        grd.Rows.Clear();
        grd.RowCount = pos.Length;
        try{
            for (int i = 0; i < pos.Length; i++){
                grd[0, i].Value = pos[i].ToString();
                grd[1, i].Value = PosName[mt1, pos[i]];
                grd[2, i].Value = mtDATA[mt1, pos[i]].Pos;
                grd[3, i].Value = mtDATA[mt2, pos[i]].Pos;
                grd[4, i].Value = "GET";
                grd[5, i].Value = "GET";
                grd[6, i].Value = "GO";
                row             = grd.Rows[i];
                row.Height      = 30;
                iHeight += row.Height;

                if (pos[i] < CNT_.ComPos){
                    grd[0, i].Style.BackColor = ComBackColor;
                    grd[1, i].Style.BackColor = ComBackColor;
                }
            }
            CLEAR_GRID_SELECTED(ref grd);
        }
        catch (Exception ex) { MessageBox.Show(ex.ToString()); }
    }

    public static void ADD_LOG(string s){
        sLOG = sLOG + s + " -> " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second + ETC.CrLf;
    }

    public static void LOG_HOME(int m, string sACTION){
        ADD_LOG(sACTION);
        mtSTS[m].strHome = sACTION;
    }

    public static bool SYSTEM_MESSAGE(int nERR, bool b){
        sSystemMessage += DateTime.Now.ToString() + " ▶ " + "<" + nERR.ToString() + ">" + ErrName[nERR] + ETC.CrLf;
        LAB_.MT_ALL_STOP(false, "mUTIL -> SYSTEM_MESSAGE()" + ETC.CrLf + sSystemMessage);
        mIN[I.vtStop]    = true;
        bSystemMessage  = true;
        //try{
        //    string sLOG = DateTime.Now.ToString() + " ▶ " + " < " + nERR.ToString() + " > " + ErrName[nERR];
        //    mLogWR.SaveLogSystem(sLOG, "");
        //}
        //catch (Exception ex){
        //    mLogWR.SaveLogException("mUTIL -> SYSTEM_MESSAGE()", ex);
        //}
        return b;
    }
    public static bool SYSTEM_MESSAGE(string Message, bool b){
        mIN[I.vtStop] = true;
        Thread.Sleep(1000);
        sSystemMessage += DateTime.Now.ToString() + " ▶ " + Message + ETC.CrLf;
        bSystemMessage = true;
        try { LogWR_.SaveLogSystem(sSystemMessage, ""); }
        catch (Exception ex) { LogWR_.SaveLogException("mUtil -> SYSTEM_MESSAGE", ex); }
        return b;
    }

    public static string INPUT_MESSAGE(string sTITLE, string sSUBJECT, string dfit, bool bPASSWORD){
        if (bPASSWORD) SUBFRM_.gInputBox.editInput.PasswordChar = '*';
        SUBFRM_.gInputBox.Text              = sTITLE;
        SUBFRM_.gInputBox.lbTitle.Text      = sSUBJECT;
        SUBFRM_.gInputBox.editInput.Text    = dfit;
        SUBFRM_.gInputBox.ShowDialog();
        return SUBFRM_.gInputBox.sRESULT;
    }

    public static bool PRINT_MASSAGE(string Message, bool bDefault, bool bTypeOK, bool bAutoClose){
        try{
            if (SUBFRM_.gMSGBOX.Visible){
                W.ViewWarning(-1, W.ChkMessageBox, "메세지 창이 띄어 있습니다. 메세지 창 닫고 다시 하세요.");
                return false;
            }
            //517, 187
            SUBFRM_.gMSGBOX.Width   = 517;
            SUBFRM_.gMSGBOX.Height  = 187;
            if (bTypeOK){
                SUBFRM_.gMSGBOX.swOk.Visible    = true;
                SUBFRM_.gMSGBOX.swYes.Visible   = false;
                SUBFRM_.gMSGBOX.swNo.Visible    = false;
                if (bAutoClose) SUBFRM_.gMSGBOX.tmrMessageBox.Enabled = true;
            }
            else{
                SUBFRM_.gMSGBOX.swOk.Visible    = false;
                SUBFRM_.gMSGBOX.swYes.Visible   = true;
                SUBFRM_.gMSGBOX.swNo.Visible    = true;
            }
            SUBFRM_.gMSGBOX.bDEFAULT        = bDefault;
            SUBFRM_.gMSGBOX.editMsg.Text    = Message;
            LogWR_.SaveLogPrintMessage(Message, "");
            DialogResult dr = SUBFRM_.gMSGBOX.ShowDialog();
            if (dr == DialogResult.Yes || dr == DialogResult.OK) return true;
        }
        catch (Exception e){
            //MessageBox.Show(e.ToString() + "PRINT_MESSAGE FAIL !");
            DeleException?.Invoke("PRINT_MESSAGE FAIL" + "\n\n" + e.Message);
            //if (DeleException != null)
            //    DeleException("PRINT_MESSAGE FAIL" + "\n\n" + e.Message);
        }
        return false;
    }

    public static double OPEN_KEYPAD(string sTitle, double dValue, bool bOption){
        SUBFRM_.gTENKEY.Text                = "KEYPAD [" + sTitle + "]";
        SUBFRM_.gTENKEY.bOPTION             = bOption;
        SUBFRM_.gTENKEY.TXT_MINUS.Visible   = bOption;
        SUBFRM_.gTENKEY.editValue.Text      = dValue.ToString();
        SUBFRM_.gTENKEY.INI();
        return double.Parse(mTenkeyResult);
    }
    public static void OPEN_KEYPAD_LABEL(string sTitle, ref Label lbDmy, bool bOption){
        lbDmy.BackColor                     = Color.Lime;
        SUBFRM_.gTENKEY.Text                = "KEYPAD [" + sTitle + "]";
        SUBFRM_.gTENKEY.bOPTION             = bOption;
        SUBFRM_.gTENKEY.TXT_MINUS.Visible   = bOption;
        SUBFRM_.gTENKEY.editValue.Text      = lbDmy.Text;
        SUBFRM_.gTENKEY.INI();
        lbDmy.BackColor = Color.White;
        lbDmy.Text      = mTenkeyResult;
    }
    public static void OPEN_KEYPAD_TEXT(string sTitle, ref TextBox txDmy, bool bOption){
        SUBFRM_.gTENKEY.Text                = "KEYPAD [" + sTitle + "]";
        SUBFRM_.gTENKEY.bOPTION             = bOption;
        SUBFRM_.gTENKEY.TXT_MINUS.Visible   = bOption;
        SUBFRM_.gTENKEY.editValue.Text      = txDmy.Text;
        SUBFRM_.gTENKEY.INI();
        txDmy.Text = mTenkeyResult;
    }

    public static string GET_GRID_ITEM(DataGridView g, int iRow, int iCel){
        if (g.Rows[iRow].Cells[iCel].Value == null) return "";
        return g.Rows[iRow].Cells[iCel].Value.ToString();
    }
    public static void SET_GRID_ITEM(ref DataGridView g, int iRow, int iCel, string sVal){
        string val                      = sVal.ToString();
        g.Rows[iRow].Cells[iCel].Value  = val;
    }
    public static void OPEN_KEYPAD_GRID(string sTitle, ref DataGridView gDmy, bool bOption){
        int row                             = gDmy.CurrentCell.RowIndex;
        int cel                             = gDmy.CurrentCell.ColumnIndex;
        SUBFRM_.gTENKEY.Text                = "KEYPAD [" + sTitle + "]";
        SUBFRM_.gTENKEY.bOPTION             = bOption;
        SUBFRM_.gTENKEY.TXT_MINUS.Visible   = bOption;
        SUBFRM_.gTENKEY.editValue.Text      = GET_GRID_ITEM(gDmy, row, cel);
        SUBFRM_.gTENKEY.INI();
        SET_GRID_ITEM(ref gDmy, row, cel, mTenkeyResult);
    }

    /// <summary>
    /// 폴더를 복사합니다
    /// </summary>
    /// <param name="sourceDirName"></param>
    /// <param name="destDirName"></param>
    /// <param name="copySubDirs"></param>
    public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, bool overWrite = true){
        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);

        //if (!dir.Exists)
        //{
        //    throw new DirectoryNotFoundException(
        //        "Source directory does not exist or could not be found: "
        //        + sourceDirName);
        //}

        DirectoryInfo[] dirs = dir.GetDirectories();
        // If the destination directory doesn't exist, create it.
        if (!Directory.Exists(destDirName)){
            Directory.CreateDirectory(destDirName);
        }

        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string temppath = Path.Combine(destDirName, file.Name);
            file.CopyTo(temppath, overWrite);
        }

        // If copying subdirectories, copy them and their contents to new location.
        if (copySubDirs){
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath, copySubDirs);
            }
        }
    }

    /// <summary>
    /// Design Mode에서 실행 중인지
    /// UserConrol 안에 UserControl을 또 사용한다면 DesignMode가 정상 인식되지 않는다
    /// https://support.microsoft.com/ko-kr/kb/839202
    /// </summary>
    public static bool IsInDesigner{
        get { return (System.Reflection.Assembly.GetEntryAssembly() == null); }
    }
}