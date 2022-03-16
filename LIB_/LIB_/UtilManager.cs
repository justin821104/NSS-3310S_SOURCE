using Object;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using LIB_.DateType;

public class UTIL_ : DATA_
{
    public delegate void DeleAddException(string sErr);
    public static event DeleAddException DeleException = null;

    public static DateTime DELAY(int ms){
        if (ms <= 0) return DateTime.Now;
        Thread.Sleep(ms);
        return DateTime.Now;
    }

    public static bool WaitWarning(int iTH, int nWarning, string comment){
        LogThread[iTH].sqeSTS = comment;
        Thread.Sleep(3);
        string s = "[" + nWarning.ToString() + "] " + ConfirmUser[nWarning].msg + " , " + comment + " = ";
        if (ConfirmUser[nWarning].useable){
            LogThread[iTH].waitSTS = s + "WAIT";
            return true;
        }
        LogThread[iTH].waitSTS = s + "END";
        return false;
    }
    public static bool WaitInput(int iTH, int nINPUT, bool bSTS, string comment){
        if (bDRYRUN && bMF) return false;
        LogThread[iTH].sqeSTS = comment;
        Thread.Sleep(3);
        string s = "[" + nINPUT.ToString() + "] " + InputName[nINPUT] + " = " + bSTS.ToString() + " , " + comment + " = ";
        if (mIN[nINPUT] == bSTS){
            LogThread[iTH].waitSTS = s + "WAIT";
            return true;
        }
        LogThread[iTH].waitSTS = s + "END";
        return false;
    }
    public static bool WaitBIT(int iTH, int nBIT, bool bSTS, string comment){
        if (bMF) return false;
        LogThread[iTH].sqeSTS = comment;
        Thread.Sleep(3);
        string s = "[" + nBIT.ToString() + "] " + mBName[nBIT] + " = " + bSTS.ToString() + " , " + comment + " = ";
        if (IsBIT[nBIT] == bSTS){
            LogThread[iTH].waitSTS = s + "WAIT";
            return true;
        }
        LogThread[iTH].waitSTS = s + "END";
        return false;
    }
    public static bool WaitBIT(int iTH, int nBIT1, int nBIT2, bool bSTS1, bool bSTS2, string comment, bool bAND){
        LogThread[iTH].sqeSTS = comment;
        Thread.Sleep(3);
        string s = "[" + nBIT1.ToString() + "] " + mBName[nBIT1] + " = " + bSTS1.ToString()
                    + "&&" + nBIT2.ToString() + "] " + mBName[nBIT2] + " = " + bSTS2.ToString()
                    + " , " + comment + " = ";

        if (bAND){
            if ((IsBIT[nBIT1] == bSTS1) && (IsBIT[nBIT2] == bSTS2)){
                LogThread[iTH].waitSTS = s + "WAIT";
                return true;
            }
        }
        else{
            if ((IsBIT[nBIT1] == bSTS1) || (IsBIT[nBIT2] == bSTS2)){
                LogThread[iTH].waitSTS = s + "WAIT";
                return true;
            }
        }
        LogThread[iTH].waitSTS = s + "END";
        return false;
    }
    public static bool WaitBIT(int iTH, int nBIT1, int nBIT2, int nBIT3, bool bSTS1, bool bSTS2, bool bSTS3, string comment, bool bAND){
        LogThread[iTH].sqeSTS = comment;
        Thread.Sleep(3);
        string s = "[" + nBIT1.ToString() + "] " + mBName[nBIT1] + " = " + bSTS1.ToString()
                    + "&&" + nBIT2.ToString() + "] " + mBName[nBIT2] + " = " + bSTS2.ToString()
                    + " , " + comment + " = ";

        if (bAND){
            if ((IsBIT[nBIT1] == bSTS1) && (IsBIT[nBIT2] == bSTS2) && (IsBIT[nBIT3] == bSTS3)){
                LogThread[iTH].waitSTS = s + "WAIT";
                return true;
            }
        }
        else{
            if ((IsBIT[nBIT1] == bSTS1) || (IsBIT[nBIT2] == bSTS2) || (IsBIT[nBIT3] == bSTS3)){
                LogThread[iTH].waitSTS = s + "WAIT";
                return true;
            }
        }
        LogThread[iTH].waitSTS = s + "END";
        return false;
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
        string sFullName = string.Empty;
        DirectoryInfo di = new DirectoryInfo(PATH_.DATA);
        if (di.Exists){
            DirectoryInfo[] gInfo = di.GetDirectories("*", SearchOption.AllDirectories);
            foreach (DirectoryInfo fn in gInfo){
                sFullName       = fn.FullName;
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
    public static bool GetSomeWorkDevice(string recipe, string newrecipe){
        string[] s = Directory.GetFiles(PATH_.DATA + recipe);
        for (int i = 0; i <= s.Length - 1; i++){
            string[] arr    = s[i].Split('\\');
            int idx         = arr.Length - 1;
            string temp     = arr[idx].Replace(".jog", "");
            if (newrecipe == temp) return false;
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
        double dvalue = 0.0;
        for (int i = 0; i < posdata.Length; i++){
            dvalue = Convert.ToDouble(g[2 + i, Row].Value);
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
        mIN[VT_STOP]    = true;
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
        mIN[VT_STOP] = true;
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
                COM_.ViewWarning(-1, WarningMessageBox);
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

    public static void OnERROR_MOTION(int m, int kind, int OnERR_DELAY){
        OnERROR((eMTBegin + (eMTGap * m) + kind), OnERR_DELAY);
    }
    public static void OnERROR(int num){
        if (!IsERR[num]){
            //mes alarm 보고
            SUBFRM_.gSecsGem.OnAlarmSet(CMES.eER + num);
        }
        IsERR[num] = true;
        COM_.STOP_ACMOTOR();
        DELAY(500);
    }
    public static void OnERROR(int num, int OnERR_DELAY){
        if (!IsERR[num]){
            //mes alarm 보고
            SUBFRM_.gSecsGem.OnAlarmSet(CMES.eER + num);
        }
        IsERR[num] = true;
        COM_.STOP_ACMOTOR();
        DELAY(OnERR_DELAY);
    }
    public static void OnERROR_MESSAGE(int Num, string Message, int Delay)
    {
        ErrName[Num] = Message;
        IsERR[Num] = true;
        COM_.STOP_ACMOTOR();
        DELAY(Delay);
    }
    public static void OnERROR_EXCEPT(string message, int delay){
        int eNUM        = CNT_.ERR;
        ErrName[eNUM]   = message;
        IsERR[eNUM]     = true;
        COM_.STOP_ACMOTOR();
        DELAY(delay);
    }
    public static void CLEAR_ERROR(){
        for (int i = 0; i < CNT_.ERR; i++){
            if (IsERR[i]){
                if (eLoginLevel >= ErrINFO[i].rstLevel){
                    if (IsERR[i]){
                        // MES 에러 보고
                        SUBFRM_.gSecsGem.OnAlarmClear(CMES.eER + i);
                    }
                    IsERR[i] = false;
                }
            }
        }
        bOnERROR = false;
        COM_.BZ_OFF();
    }
    public static int CHK_ERR(){
        for (int i = 0; i < CNT_.ERR; i++){
            if (IsERR[i]) return i;
        }
        return -1;
    }
    public static string GET_ERROR_NAME(int iERR){
        try{
            return ErrName[iERR];
        }
        catch (Exception ex) { LogWR_.SaveLogException("mUTIL -> GET_ERROR_NAME", ex); }
        return "";
    }
    public static string GET_ERROR_TITLE_1(int iERR){
        try{
            return ErrTitle_1[iERR];
        }
        catch (Exception ex) { LogWR_.SaveLogException("mUTIL -> GET_ERROR_TITLE_1", ex); }
        return "";
    }
    public static string GET_ERROR_TITLE_2(int iERR){
        try{
            return ErrTitle_2[iERR];
        }
        catch (Exception ex) { LogWR_.SaveLogException("mUTIL -> GET_ERROR_TITLE_2", ex); }
        return "";
    }

    public static bool OnINTERLOCK(int iNUM, bool WithError){
        if (bINTRK[iNUM - eEMSBegin]){
            if (bINTRK[iNUM - eEMSBegin]){
                //if (iNUM == 482 || iNUM == 483 || iNUM == 484){
                //    double dCP = LAB_.GET_ACTPOS(9);
                //    LogWR_.DEBUG_PRINT("유닛 피커 에러 " + iNUM.ToString() + " - " + dCP.ToString());
                //}
                if (WithError) OnERROR(iNUM, 500);
                return true; // 인터락 발생 구동 금지
            }
        }
        return false; // 정상
    }
    public static bool OnINTERLOCK(int iNUM, eCHK_INTERLOCK CHK){
        if (bINTRK[iNUM - eEMSBegin]){
            if (bINTRK[iNUM - eEMSBegin]){

            }
        }
        return false;   // 정상
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