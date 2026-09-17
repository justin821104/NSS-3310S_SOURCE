using LIB_.DateType;
using NSS_3310S;
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

    public static void SetFrame(Panel p, bool b, int x, int y, int w, int h) {
        p.Visible = b;
        p.Location = new Point(x, y);
        p.Size = new Size(w, h);
        p.BringToFront();
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

    public static void RunRepaeatManual(int n1, int n2){
        RUN_MANUAL(n1, iMANUAL.Label);
        UTIL_.DELAY((int)IsDOUBLE[D.ManualRepeat_Interval]);
        RUN_MANUAL(n2, iMANUAL.Label);
        UTIL_.DELAY((int)IsDOUBLE[D.ManualRepeat_Interval]);
    }
}