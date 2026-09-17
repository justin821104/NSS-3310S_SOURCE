using Object;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LIB_.DateType;
using NSS_3310S;

namespace LIB_.SubFROMLib{
    public partial class LOG : Form{
        int[] arrTEMP;
        public int LogPAGE = -1;
        Button pBTN;
        DateTime dt;
        DataGridView DGV;
        private stERR[] cERR = new stERR[10000];
        private stERR[] rERR = new stERR[10000];
        private stERR[] LotCERR = new stERR[10000];
        private stERR[] LotRERR = new stERR[10000];
        int pos = 0;
        bool UpdateFlag = false;
        double sm = 0;
        double sDate = 0;
        double eDate = 0;
        int CntValid = 0;
        int eNUM = 0;
        int idx = 0;
        string sAll = string.Empty;
        string sUnit = string.Empty;
        double[] yVAL = new double[6];
        //string[] xVAL           = { "RUN TIME", "STOP TIME", "PAUSE TIME", "ERROR TIME", "RUN-READY TIME", "RUN-DOWN TIME" };
        double[] LotVAL = new double[3];
        string sCALENDAR = string.Empty;
        string sYEAR = string.Empty;
        string sMONTH = string.Empty;
        string sDAY = string.Empty;
        string sFile = string.Empty;
        string sPath = string.Empty;
        public bool bFRM_VIEW = false;
        
        void INI_GRIDVIEW(){
            gridError.RowCount = 10;
            for (int i = 0; i < gridError.RowCount; i++){
                gridError.Rows[i].Cells[0].Value = i.ToString();
            }

            gridHistory.RowCount = 1;
            for (int i = 0; i < gridHistory.RowCount; i++){
                gridHistory.Rows[i].Cells[0].Value = i.ToString();
            }

            gridPara.RowCount = 1;
            for (int i = 0; i < gridPara.RowCount; i++){
                gridPara.Rows[i].Cells[0].Value = i.ToString();
            }

            DGV_LOT_ERROR.RowCount = 1;
            for (int i = 0; i < DGV_LOT_ERROR.RowCount; i++){
                DGV_LOT_ERROR.Rows[i].Cells[0].Value = i.ToString();
            }
        }

        public LOG(){
            InitializeComponent();
            INI_GRIDVIEW();

            #region Event>>
            btnLOG_0.Click += LOG_CLICK;
            btnLOG_4.Click += LOG_CLICK;
            btnLOG_1.Click += LOG_CLICK;
            btnLOG_3.Click += LOG_CLICK;
            btnLOG_2.Click += LOG_CLICK;
            btnLOG_6.Click += LOG_CLICK;
            btnLOG_5.Click += LOG_CLICK;
            btnLOG_7.Click += LOG_CLICK;
            btnLOG_8.Click += LOG_CLICK;
            btnLOG_9.Click += LOG_CLICK;
            btnLOG_10.Click += LOG_CLICK;
            btnLOG_11.Click += LOG_CLICK;
            btnLOG_12.Click += LOG_CLICK;

            swLogView.Click += (sender, e) => LogView_Click(swLogView);
            swSave.Click    += (sender, e) => LogSAVE(swSave);
            swLotInfoSave.Click += (sender, e) => LotInfoLogSAVE();
            #endregion
        }

        public void INI(){
            if (LogPAGE < 0) LOG_CLICK(btnLOG_0, EventArgs.Empty);
            bFRM_VIEW = true;
            tmrHISTORY.Enabled = true;
            Show();
            BringToFront();
        }
        void LotInfoLogSAVE(){
            if (LOT_ID.Text == "") return;
            if (SD.ShowDialog() == DialogResult.Cancel) return;
            string sPath = SD.FileName + ".txt";
            string LotInfo = "LOT ID=" + LOT_ID.Text + ETC.NewLine + "SPINDLE1=" + LBL_SPINDLE1_ID.Text + ETC.NewLine + "" + LBL_SPINDLE2_ID.Text + ETC.NewLine;
            string LotInfoResult = "";
            foreach (var input_items in LBX_LOTINFO.Items){
                LotInfoResult += string.Format("{0} ", input_items) + ETC.NewLine;
            }
            LotInfo += LotInfoResult;
           
            try{
                FILE_.WRL_File(sPath, LotInfo, false);
            }
            catch (Exception e){
                LogWR_.SaveLogException("LOG LotInfoLogSAVE FAIL", e);
            }
        }

        void LogSAVE(object sender){
            if (SD.ShowDialog() == DialogResult.Cancel) return;
            string sPath = SD.FileName;

            if (LogPAGE == 0) DGV = gridHistory;
            else if (LogPAGE == 1) DGV = gridPara;
            else if (LogPAGE == 3) DGV = dgvManual;
            else if (LogPAGE == 4) DGV = dgvWarning;
            else if (LogPAGE == 5) DGV = dgvProcess;
            else if (LogPAGE == 6) DGV = dgwEvent;
            else if (LogPAGE == 7) DGV = gridDefectCount;
            else if (LogPAGE == 8) DGV = gridLocationList;
            else if (LogPAGE == 9) DGV = gridLot;
            else if (LogPAGE == 10) DGV = dgwOneCycle;
            else if (LogPAGE == 12) DGV = DGV_BLADE_INFO;
            else return;
            if (DGV == null || DGV.RowCount == 0) return;

            //using (StreamWriter writer = new StreamWriter(sPath, false, /*Encoding.GetEncoding("euc - kr""shift_jis")) */ Encoding.UTF8)){
            //    int rowCount = DGV.Rows.Count;
            //    if (DGV.AllowUserToAddRows == true) rowCount = rowCount - 1;
            //
            //    for (int i = 0; i < rowCount; i++){
            //        List<string> strList = new List<string>();
            //        for (int j = 0; j < DGV.Columns.Count; j++){
            //            strList.Add(DGV[j, i].Value.ToString());
            //        }
            //        string[] strArray = strList.ToArray();
            //        string strCsvData = string.Join(",", strArray);
            //        writer.WriteLine(strCsvData);
            //    }
            //    writer.Close();
            //}

            string fname = sPath + ".csv";
            int rowCount = DGV.Rows.Count;
            if (DGV.AllowUserToAddRows == true) rowCount -= 1;
            string strCsvData = "";
            for (int i = 0; i < rowCount; i++){
                List<string> strList = new List<string>();
                for (int j = 0; j < DGV.Columns.Count; j++){
                    if (DGV[j, i].Value == null) continue;
                    strList.Add(DGV[j, i].Value.ToString());
                }
                string[] strArray = strList.ToArray();
                strCsvData += string.Join(",", strArray);
                strCsvData += ETC.NewLine;
            }

            try{
                //FileStream fs = new FileStream(fname, FileMode.Append);
                StreamWriter sw = new StreamWriter(fname, false, Encoding.Default);
                sw.WriteLine(strCsvData);
                sw.Flush();
                sw.Close();
            }
            catch (Exception e){
                LogWR_.SaveLogException("WRITE LOG FAIL", e);
            }
        }

        void RESET_SCREEN_CHANGE(){
            for (int i = 0; i < 15; i++){
                pBTN = Controls.Find("btnLOG_" + i.ToString(), true).FirstOrDefault() as Button;
                if (pBTN != null){
                    pBTN.ForeColor = Color.Black;
                    pBTN.BackColor = Color.White;
                }
            }
        }
        void SUB_SCREEN_CHANGE(int nPage){
            RESET_SCREEN_CHANGE();
            pBTN = Controls.Find("btnLOG_" + nPage.ToString(), true).FirstOrDefault() as Button;
            if (pBTN != null){
                pBTN.ForeColor = Color.Black;
                pBTN.BackColor = Color.SeaShell;
            }
            tcMTPAGE.SelectedIndex = nPage;
        }

        private void LOG_CLICK(object sender, EventArgs e){
            Button nBTN = sender as Button;
            SUB_SCREEN_CHANGE(Convert.ToInt32(nBTN.Tag));
            LogPAGE = Convert.ToInt32(nBTN.Tag);
        }
        private void LogView_Click(object sender){
            if (LogPAGE == 0) VIEW_ERROR();
            if (LogPAGE == 1) VIEW_TEACHING();
            if (LogPAGE == 2) VIEW_SPC();
            if (LogPAGE == 3) VIEW_MANUAL();
            if (LogPAGE == 4) VIEW_WARNING();
            if (LogPAGE == 5) VIEW_PROCESS();
            if (LogPAGE == 6) VIEW_MC_EVENT();
            if (LogPAGE == 7) VIEW_STRIP_DEFECT_COUNT();
            if (LogPAGE == 8) VIEW_STRIP_DEFECT_LOCATION_LIST();
            if (LogPAGE == 9) VIEW_LOT();
            if (LogPAGE == 10) VIEW_ONECYCLE();
            if (LogPAGE == 11) VIEW_LOTLOG();
            if (LogPAGE == 12) VIEW_BLADELOG();
        }
        string GET_LOG_FILE_PATH(double dDATA){
            dt = DateTime.FromOADate(dDATA);
            sCALENDAR = dt.ToString("yyyyMMdd");
            sYEAR = dt.Year.ToString() + "\\";
            sMONTH = dt.Month.ToString() + "\\";
            sDAY = dt.Day.ToString() + "\\";
            sFile = PATH_.MCLOG + sYEAR + sMONTH + sDAY + PATH_.ErrLOG + "ERROR_DB_" + sCALENDAR + ".lgf";

            return sFile;
        }
        void VIEW_ERROR_HISTOGRAM(int idx){
            CntValid = 0;
            for (int i = 0; i < idx; i++){
                if (cERR[i].ErrorNumber < 0) break;
                eNUM = cERR[i].ErrorNumber;
                arrTEMP[eNUM] += 1;
            }
            for (int i = 0; i < CNT_.ERR; i++){
                if (arrTEMP[i] > 0) CntValid += 1;
            }

            gridErrorHisto.RowCount = CntValid;
            for (int i = 0; i < CntValid; i++){
                eNUM = DATA_.cMATH.IsGetArrMaxIndex(arrTEMP);
                if (eNUM < 0) return;
                gridErrorHisto.Rows[i].Cells[0].Value = eNUM.ToString();
                gridErrorHisto.Rows[i].Cells[1].Value = E.GET_ERROR_NAME(eNUM);
                gridErrorHisto.Rows[i].Cells[2].Value = arrTEMP[eNUM];
                arrTEMP[eNUM] = 0;
            }
        } // 발생 빈도.
        void VIEW_ERROR(){
            sDate = dtBegin.Value.Date.ToOADate();
            eDate = dtEnd.Value.Date.ToOADate();
            idx = 0;
            try{
                for (double d = sDate; d < eDate + 1; d++){
                    sPath = GET_LOG_FILE_PATH(d);
                    if (!File.Exists(sPath)) continue;
                    int rCNT = FILE_.RDInt(sPath, "ERROR", "COUNT", 0);
                    for (int i = 0; i < rCNT; i++){
                        cERR[idx].ErrorNumber   = FILE_.RDInt(sPath, "ERROR NUMBER", i.ToString(), 0);
                        cERR[idx].BeginTime     = FILE_.RDString(sPath, "BEGIN TIME", i.ToString(), "");
                        cERR[idx].EndTime       = FILE_.RDString(sPath, "END TIME", i.ToString(), "");

                        idx += 1;
                        if (idx >= cERR.Length) break;
                    }
                    if (idx >= cERR.Length) break;
                }
                gridHistory.RowCount = idx;
                for (int i = 0; i < idx; i++){
                    gridHistory.Rows[i].Cells[0].Value = i.ToString();
                    gridHistory.Rows[i].Cells[1].Value = cERR[i].ErrorNumber;
                    gridHistory.Rows[i].Cells[2].Value = E.GET_ERROR_NAME(cERR[i].ErrorNumber);
                    gridHistory.Rows[i].Cells[3].Value = cERR[i].BeginTime;
                    gridHistory.Rows[i].Cells[4].Value = cERR[i].EndTime;
                }
                VIEW_ERROR_HISTOGRAM(idx);
            }
            catch (Exception ex) { LogWR_.SaveLogException("frmLG->VIEW_ERROR", ex); }
        }
        void VIEW_WARNING(){
            sDate = dtBegin.Value.Date.ToOADate();
            eDate = dtEnd.Value.Date.ToOADate();
            sAll = "";
            sUnit = "All";
            if (rbUnitA.Checked) sUnit = "A";
            if (rbUnitB.Checked) sUnit = "B";
            if (rbUnitC.Checked) sUnit = "C";
            idx = 0;
            try{
                for (double i = sDate; i < eDate + 1; i++){
                    string fn = LogWR_.GET_PathOperation(i, PATH_.LogWARNING) + "WARNING.log";
                    if (!File.Exists(fn)) continue;
                    string[] sARR = File.ReadAllLines(fn);
                    for (int cnt = 0; cnt < sARR.Length; cnt++){
                        if (idx > 10000) break;
                        string[] subarr = sARR[cnt].Split(',');
                        if (sUnit != "All" && sUnit != subarr[2]) continue;
                        sAll += sARR[cnt] + ETC.CrLf;
                        idx += 1;
                    }
                }

                dgvWarning.RowCount = idx;
                string[] allARR = sAll.Split(ETC.CrLf);
                int wIdx = 0;
                for (int i = 0; i < idx; i++){
                    string[] subARR = allARR[i].Split(',');
                    //if (subARR.Length < 1) continue;
                    //if (subARR.Length == 1){
                    //    dgvWarning.Rows[wIdx].Cells[0].Value = i.ToString();
                    //    dgvWarning.Rows[wIdx].Cells[5].Value = dgvWarning.Rows[wIdx].Cells[5].Value.ToString() + " " + subARR[0];
                    //}
                    //else if (subARR.Length > 5){
                    //    dgvWarning.Rows[wIdx].Cells[0].Value = i.ToString();
                    //    dgvWarning.Rows[wIdx].Cells[1].Value = subARR[4];
                    //    dgvWarning.Rows[wIdx].Cells[2].Value = subARR[0];
                    //    dgvWarning.Rows[wIdx].Cells[3].Value = subARR[1];
                    //    dgvWarning.Rows[wIdx].Cells[4].Value = subARR[2];
                    //    dgvWarning.Rows[wIdx].Cells[5].Value = subARR[5];
                    //    wIdx++;
                    //}
                    if (subARR.Length < 5){
                        //if (subARR.Length == 1){
                        //    dgvWarning.Rows[wIdx].Cells[5].Value = dgvWarning.Rows[wIdx].Cells[5].Value + " " + subARR[0];
                        //}
                        continue;
                    }
                    dgvWarning.Rows[wIdx].Cells[0].Value = i.ToString();
                    dgvWarning.Rows[wIdx].Cells[1].Value = subARR[4];
                    dgvWarning.Rows[wIdx].Cells[2].Value = subARR[0];
                    dgvWarning.Rows[wIdx].Cells[3].Value = subARR[1];
                    dgvWarning.Rows[wIdx].Cells[4].Value = subARR[2];
                    dgvWarning.Rows[wIdx].Cells[5].Value = subARR[5];
                    wIdx++;
                }
                if (dgvWarning.RowCount <= 0) return;
            }
            catch (Exception ex) { LogWR_.SaveLogException("frmLG->VIEW_WARNING", ex); }
        }
        void VIEW_TEACHING(){
            string fLOG = "Parameter.log";
            sDate = dtBegin.Value.Date.ToOADate();
            eDate = dtEnd.Value.Date.ToOADate();
            sAll = "";
            sUnit = "All";
            if (rbUnitA.Checked) sUnit = "A";
            if (rbUnitB.Checked) sUnit = "B";
            if (rbUnitC.Checked) sUnit = "C";
            int idx = 0;

            try{
                for (double i = sDate; i < eDate + 1; i++){
                    string fn = LogWR_.GET_PathOperation(i, PATH_.LogEVENT) + fLOG;
                    if (!File.Exists(fn)) continue;
                    string[] sARR = File.ReadAllLines(fn);
                    for (int cnt = 0; cnt < sARR.Length; cnt++){
                        if (idx > 10000) break;
                        string[] subarr = sARR[cnt].Split(',');
                        if (sUnit != "All" && sUnit != subarr[2]) continue;
                        sAll += sARR[cnt] + ETC.CrLf;
                        idx += 1;
                    }
                }

                gridPara.RowCount = idx;
                string[] allARR = sAll.Split(ETC.CrLf);
                int wIdx = 0;
                for (int i = 0; i < idx; i++){
                    string[] subARR = allARR[i].Split(',');
                    if (subARR.Length < 4) continue;
                    gridPara.Rows[wIdx].Cells[0].Value = i.ToString();
                    gridPara.Rows[wIdx].Cells[1].Value = subARR[4];
                    gridPara.Rows[wIdx].Cells[2].Value = subARR[0];
                    gridPara.Rows[wIdx].Cells[3].Value = subARR[1];
                    gridPara.Rows[wIdx].Cells[4].Value = subARR[2];
                    gridPara.Rows[wIdx].Cells[5].Value = subARR[5];
                    wIdx++;
                }
                if (gridPara.RowCount <= 0) return;
                //DataGridViewCell _dgvCell = gridPara.Rows[gridPara.RowCount].Cells[1];
                //gridPara.FirstDisplayedCell = _dgvCell;
                //gridPara.CurrentCell = _dgvCell;
            }
            catch (Exception ex) { LogWR_.SaveLogException("frmLOG->VIEW_TEACHING", ex); }
        }
        void VIEW_MANUAL(){
            string fLOG = "MANUAL.log";
            sDate = dtBegin.Value.Date.ToOADate();
            eDate = dtEnd.Value.Date.ToOADate();
            sAll = "";
            sUnit = "All";
            if (rbUnitA.Checked) sUnit = "A";
            if (rbUnitB.Checked) sUnit = "B";
            if (rbUnitC.Checked) sUnit = "C";
            idx = 0;

            try{
                for (double i = sDate; i < eDate + 1; i++){
                    string fn = LogWR_.GET_PathOperation(i, PATH_.LogProcMANUAL) + fLOG;
                    if (!File.Exists(fn)) continue;
                    string[] sARR = File.ReadAllLines(fn);
                    for (int cnt = 0; cnt < sARR.Length; cnt++){
                        if (idx > 5000) break;
                        string[] subarr = sARR[cnt].Split(',');
                        if (sUnit != "All" && sUnit != subarr[2]) continue;
                        sAll += sARR[cnt] + ETC.CrLf;
                        idx += 1;
                    }
                }

                dgvManual.RowCount = idx;
                string[] allARR = sAll.Split(ETC.CrLf);
                int wIdx = 0;
                for (int i = 0; i < idx; i++){
                    string[] subARR = allARR[i].Split(',');
                    if (subARR.Length < 5) continue;
                    dgvManual.Rows[wIdx].Cells[0].Value = i.ToString();

                    dgvManual.Rows[wIdx].Cells[1].Value = subARR[4];
                    dgvManual.Rows[wIdx].Cells[2].Value = subARR[0];
                    dgvManual.Rows[wIdx].Cells[3].Value = subARR[1];
                    dgvManual.Rows[wIdx].Cells[4].Value = subARR[2];
                    dgvManual.Rows[wIdx].Cells[5].Value = subARR[5];
                    wIdx++;
                }
                if (dgvManual.RowCount <= 0) return;
            }
            catch (Exception ex) { LogWR_.SaveLogException("frmLG->MANUALRUN", ex); }
        }
        void VIEW_SPC(){

        }
        void VIEW_STRIP_DEFECT_COUNT(){
            if (CLOT.GET_LOT.ItsID == "") return;

            string fLOG = CLOT.GET_LOT.ItsID + ".TXT";
            sDate = dtBegin.Value.Date.ToOADate();
            eDate = dtEnd.Value.Date.ToOADate();
            sAll = "";
            sUnit = "All";
            if (rbUnitA.Checked) sUnit = "A";
            if (rbUnitB.Checked) sUnit = "B";
            if (rbUnitC.Checked) sUnit = "C";
            idx = 0;

            try{
                for (double i = sDate; i < eDate + 1; i++){
                    string fn = LogWR_.GET_PathOperation(i, PATH_.LogStripDefectCount) + fLOG;
                    if (!File.Exists(fn)) continue;
                    string[] sARR = File.ReadAllLines(fn);
                    for (int cnt = 0; cnt < sARR.Length; cnt++){
                        if (idx > 5000) break;
                        string[] subarr = sARR[cnt].Split(',');
                        if (sUnit != "All" && sUnit != subarr[2]) continue;
                        sAll += sARR[cnt] + ETC.CrLf;
                        idx += 1;
                    }
                }
                gridDefectCount.RowCount = idx;
                string[] allARR = sAll.Split(ETC.CrLf);
                int wIdx = 0;
                for (int i = 0; i < idx; i++){
                    string[] subARR = allARR[i].Split(',');
                    if (subARR.Length < 3) continue;
                    gridDefectCount.Rows[wIdx].Cells[0].Value = i.ToString();

                    gridDefectCount.Rows[wIdx].Cells[1].Value = subARR[0];
                    gridDefectCount.Rows[wIdx].Cells[2].Value = subARR[1];
                    gridDefectCount.Rows[wIdx].Cells[3].Value = subARR[2];
                    wIdx++;
                }
                if (gridDefectCount.RowCount <= 0) return;
            }
            catch(Exception ex){
                LogWR_.SaveLogException("frmLG->VIEW_STRIP_DEFECT_COUNT", ex);
            }
        }
        void VIEW_STRIP_DEFECT_LOCATION_LIST(){
            if (CLOT.GET_LOT.ItsID == "") return;

            string fLOG = CLOT.GET_LOT.ItsID + ".TXT";
            sDate = dtBegin.Value.Date.ToOADate();
            eDate = dtEnd.Value.Date.ToOADate();
            sAll = "";
            sUnit = "All";
            if (rbUnitA.Checked) sUnit = "A";
            if (rbUnitB.Checked) sUnit = "B";
            if (rbUnitC.Checked) sUnit = "C";
            idx = 0;

            try{
                for (double i = sDate; i < eDate + 1; i++){
                    string fn = LogWR_.GET_PathOperation(i, PATH_.LogStripDefectLocationList) + fLOG;
                    if (!File.Exists(fn)) continue;
                    string[] sARR = File.ReadAllLines(fn);
                    for (int cnt = 0; cnt < sARR.Length; cnt++){
                        if (idx > 5000) break;
                        string[] subarr = sARR[cnt].Split(',');
                        if (sUnit != "All" && sUnit != subarr[2]) continue;
                        sAll += sARR[cnt] + ETC.CrLf;
                        idx += 1;
                    }
                }
                gridLocationList.RowCount = idx;
                string[] allARR = sAll.Split(ETC.CrLf);
                int wIdx = 0;
                for (int i = 0; i < idx; i++){
                    string[] subARR = allARR[i].Split(',');
                    if (subARR.Length < 3) continue;
                    gridLocationList.Rows[wIdx].Cells[0].Value = i.ToString();

                    gridLocationList.Rows[wIdx].Cells[1].Value = subARR[0];
                    gridLocationList.Rows[wIdx].Cells[2].Value = subARR[1];
                    gridLocationList.Rows[wIdx].Cells[3].Value = subARR[2];
                    gridLocationList.Rows[wIdx].Cells[4].Value = subARR[3];
                    gridLocationList.Rows[wIdx].Cells[5].Value = subARR[4];
                    gridLocationList.Rows[wIdx].Cells[6].Value = subARR[5];
                    gridLocationList.Rows[wIdx].Cells[7].Value = subARR[6];
                    gridLocationList.Rows[wIdx].Cells[8].Value = subARR[7];
                    wIdx++;
                }
                if (gridLocationList.RowCount <= 0) return;
            }
            catch (Exception ex){
                LogWR_.SaveLogException("frmLG->VIEW_STRIP_DEFECT_LOCATION_LIST", ex);
            }
        }
        void VIEW_LOT(){
            string fLOG = "LOT.log";
            sDate = dtBegin.Value.Date.ToOADate();
            eDate = dtEnd.Value.Date.ToOADate();
            sAll = "";
            sUnit = "All";
            if (rbUnitA.Checked) sUnit = "A";
            if (rbUnitB.Checked) sUnit = "B";
            if (rbUnitC.Checked) sUnit = "C";
            idx = 0;

            try{
                for (double i = sDate; i < eDate + 1; i++){
                    string fn = LogWR_.GET_PathOperation(i, PATH_.LogLotEnd) + fLOG;
                    if (!File.Exists(fn)) continue;
                    string[] sARR = File.ReadAllLines(fn);
                    for (int cnt = 0; cnt < sARR.Length; cnt++){
                        if (idx > 5000) break;
                        string[] subarr = sARR[cnt].Split(',');
                        if (sUnit != "All" && sUnit != subarr[2]) continue;
                        sAll += sARR[cnt] + ETC.CrLf;
                        idx += 1;
                    }
                }

                gridLot.RowCount = idx;
                string[] allARR = sAll.Split(ETC.CrLf);
                int wIdx = 0;
                for (int i = 0; i < idx; i++){
                    string[] subARR = allARR[i].Split(',');
                    if (subARR.Length < 9) continue;
                    gridLot.Rows[wIdx].Cells[0].Value = i.ToString();

                    gridLot.Rows[wIdx].Cells[1].Value = subARR[4];
                    gridLot.Rows[wIdx].Cells[2].Value = subARR[0];
                    gridLot.Rows[wIdx].Cells[3].Value = subARR[1];
                    gridLot.Rows[wIdx].Cells[4].Value = subARR[2];
                    gridLot.Rows[wIdx].Cells[5].Value = subARR[3];
                    gridLot.Rows[wIdx].Cells[6].Value = subARR[5];
                    gridLot.Rows[wIdx].Cells[7].Value = subARR[6];
                    gridLot.Rows[wIdx].Cells[8].Value = subARR[7];
                    gridLot.Rows[wIdx].Cells[9].Value = subARR[8];
                    gridLot.Rows[wIdx].Cells[10].Value = subARR[9];
                    gridLot.Rows[wIdx].Cells[11].Value = subARR[10];

                    wIdx++;
                }
                if (dgvManual.RowCount <= 0) return;
            }
            catch (Exception ex) { LogWR_.SaveLogException("frmLOG->MANUALRUN", ex); }
        }
        void VIEW_ONECYCLE(){
            string fLOG = "TACK.log";
            sDate = dtBegin.Value.Date.ToOADate();
            eDate = dtEnd.Value.Date.ToOADate();
            sAll = "";
            sUnit = "All";
            if (rbUnitA.Checked) sUnit = "A";
            if (rbUnitB.Checked) sUnit = "B";
            if (rbUnitC.Checked) sUnit = "C";
            idx = 0;
            
            try{
                for (double i = sDate; i < eDate + 1; i++){
                    string fn = LogWR_.GET_PathOperation(i, PATH_.LogOneCycleTime) + fLOG;
                    if (!File.Exists(fn)) continue;
                    string[] sARR = File.ReadAllLines(fn);
                    for (int cnt = 0; cnt < sARR.Length; cnt++){
                        if (idx > 5000) break;
                        string[] subarr = sARR[cnt].Split(',');
                        if (sUnit != "All" && sUnit != subarr[2]) continue;
                        sAll += sARR[cnt] + ETC.CrLf;
                        idx += 1;
                    }
                }

                dgwOneCycle.RowCount = idx;
                string[] allARR = sAll.Split(ETC.CrLf);
                int wIdx = 0;
                for (int i = 0; i < idx; i++){
                    string[] subARR = allARR[i].Split(',');
                    if (subARR.Length < 9) continue;
                    dgwOneCycle.Rows[wIdx].Cells[0].Value = i.ToString();

                    dgwOneCycle.Rows[wIdx].Cells[1].Value = subARR[4];
                    dgwOneCycle.Rows[wIdx].Cells[2].Value = subARR[0];
                    dgwOneCycle.Rows[wIdx].Cells[3].Value = subARR[1];
                    dgwOneCycle.Rows[wIdx].Cells[4].Value = subARR[2];
                    dgwOneCycle.Rows[wIdx].Cells[5].Value = subARR[5];
                    dgwOneCycle.Rows[wIdx].Cells[6].Value = subARR[6];
                    dgwOneCycle.Rows[wIdx].Cells[7].Value = subARR[7];
                    dgwOneCycle.Rows[wIdx].Cells[8].Value = subARR[8];
                    dgwOneCycle.Rows[wIdx].Cells[9].Value = subARR[9];
                    
                    wIdx++;
                }
                if (dgwOneCycle.RowCount <= 0) return;
            }
            catch (Exception ex) { LogWR_.SaveLogException("frmLG->ONECYCLE", ex); }
        }
        void VIEW_LOTLOG(){
            string fLOG = "LOT.log";
            sDate = dtBegin.Value.Date.ToOADate();
            eDate = dtEnd.Value.Date.ToOADate();
            sAll = "";
            sUnit = "All";
            if (rbUnitA.Checked) sUnit = "A";
            if (rbUnitB.Checked) sUnit = "B";
            if (rbUnitC.Checked) sUnit = "C";
            idx = 0;

            try{
                lvwLOTIDLIST.Items.Clear();
                for (double i = sDate; i < eDate + 1; i++){
                    string fn = LogWR_.GET_PathOperation(i, PATH_.LogLotLogList) + fLOG;
                    if (!File.Exists(fn)) continue;
                    string[] sARR = File.ReadAllLines(fn);
                    for (int cnt = 0; cnt < sARR.Length; cnt++){
                        string[] subarr = sARR[cnt].Split(',');
                        if (sUnit != "All" && sUnit != subarr[2]) continue;
                        sAll += sARR[cnt] + ETC.CrLf;
                        idx += 1;
                    }
                }
                string[] allARR = sAll.Split(ETC.CrLf);
                for (int i = 0; i < idx; i++){
                    string[] subARR = allARR[i].Split(',');
                    if (subARR.Length < 3) continue;
                    lvwLOTIDLIST.Items.Add(subARR[3].Trim());
                }
                lvwLOTIDLIST.EndUpdate();
            }
            catch (Exception EX) { LogWR_.SaveLogException("frmLOG->VIEW_LOTLOG", EX); }
        }
        void VIEW_BLADELOG(){
            string fLOG = "BLADE.log";
            sDate = dtBegin.Value.Date.ToOADate();
            eDate = dtEnd.Value.Date.ToOADate();
            sAll = "";
            sUnit = "All";
            if (rbUnitA.Checked) sUnit = "A";
            if (rbUnitB.Checked) sUnit = "B";
            if (rbUnitC.Checked) sUnit = "C";
            idx = 0;

            try{
                for (double i = sDate; i < eDate + 1; i++){
                    string fn = LogWR_.GET_PathOperation(i, PATH_.LogBladeInfo) + fLOG;
                    if (!File.Exists(fn)) continue;
                    string[] sARR = File.ReadAllLines(fn);
                    for (int cnt = 0; cnt < sARR.Length; cnt++){
                        if (idx > 7000) break;
                        string[] subarr = sARR[cnt].Split(',');
                        if (sUnit != "All" && sUnit != subarr[2]) continue;
                        sAll += sARR[cnt] + ETC.CrLf;
                        idx += 1;
                    }
                }
                DGV_BLADE_INFO.RowCount = idx;
                string[] allARR = sAll.Split(ETC.CrLf);
                int wIdx = 0;
                for (int i = 0; i < idx; i++){
                    string[] subARR = allARR[i].Split(',');
                    if (subARR.Length < 12) continue;
                    DGV_BLADE_INFO.Rows[wIdx].Cells[0].Value = wIdx.ToString();
                    DGV_BLADE_INFO.Rows[wIdx].Cells[1].Value = subARR[4]; //TIME
                    DGV_BLADE_INFO.Rows[wIdx].Cells[2].Value = subARR[3]; //LOT ID
                    DGV_BLADE_INFO.Rows[wIdx].Cells[3].Value = subARR[5]; //ABF

                    DGV_BLADE_INFO.Rows[wIdx].Cells[4].Value = subARR[6]; //SP1 BLADE BARCODE
                    DGV_BLADE_INFO.Rows[wIdx].Cells[5].Value = (subARR.Length - 1) > 12 ? subARR[13] : ""; //SP1 BLADE ID -> 13
                    DGV_BLADE_INFO.Rows[wIdx].Cells[6].Value = subARR[7]; //SP2 BLADE BARCODE
                    DGV_BLADE_INFO.Rows[wIdx].Cells[7].Value = (subARR.Length - 1) > 13 ? subARR[14] : ""; //SP2 BLADE ID -> 14

                    DGV_BLADE_INFO.Rows[wIdx].Cells[8].Value = subARR[9]; //상태
                    DGV_BLADE_INFO.Rows[wIdx].Cells[9].Value = subARR[8]; //소재 바코드
                    DGV_BLADE_INFO.Rows[wIdx].Cells[10].Value = subARR[10]; //SP1 마모량
                    DGV_BLADE_INFO.Rows[wIdx].Cells[11].Value = subARR[11]; //SP2 마모량
                    DGV_BLADE_INFO.Rows[wIdx].Cells[12].Value = subARR[12]; //투입 수량
                    wIdx++;
                }
                if (DGV_BLADE_INFO.RowCount <= 0) return;
            }
            catch (Exception ex) { LogWR_.SaveLogException("frmLG->VIEW_BLADELOG", ex); }
        }

        void VIEW_LOT_ERROR_HISTOGRAM(int idx){
            CntValid = 0;
            for (int i = 0; i < idx; i++){
                if (LotCERR[i].ErrorNumber < 0) break;
                eNUM = LotCERR[i].ErrorNumber;
                arrTEMP[eNUM] += 1;
            }
            for (int i = 0; i < CNT_.ERR; i++){
                if (arrTEMP[i] > 0) CntValid += 1;
            }

            DGV_LOT_ERROR_LIST.RowCount = CntValid;
            for (int i = 0; i < CntValid; i++){
                eNUM = DATA_.cMATH.IsGetArrMaxIndex(arrTEMP);
                if (eNUM < 0) return;
                DGV_LOT_ERROR_LIST.Rows[i].Cells[0].Value = eNUM.ToString();
                DGV_LOT_ERROR_LIST.Rows[i].Cells[1].Value = E.GET_ERROR_NAME(eNUM);
                DGV_LOT_ERROR_LIST.Rows[i].Cells[2].Value = arrTEMP[eNUM];
                arrTEMP[eNUM] = 0;
            }
        } // 발생 빈도.
        public void AddLotInfo(string Logs){
            try{
                if (InvokeRequired){
                    Invoke((MethodInvoker)delegate(){
                        AddLotInfo(Logs);
                    });
                }
                else{
                    LBX_LOTINFO.BeginUpdate();
                    LBX_LOTINFO.Items.Add(Logs);
                    LBX_LOTINFO.EndUpdate();
                }
            }
            catch(Exception ex){
                LogWR_.SaveLogException("frmLOG->AddLotInfo", ex);
            }
        }
        private void BTN_LOTINFO_OPEN_Click(object sender, EventArgs e){
            try{
                if (lvwLOTIDLIST.SelectedIndices.Count <= 0){
                    MessageBox.Show("You must select a LOT ID list !");
                    return;
                }
                string fLOG = "LOT.log";
                string SelectLotID = lvwLOTIDLIST.SelectedItems[0].Text;
                sDate = dtBegin.Value.Date.ToOADate();
                eDate = dtEnd.Value.Date.ToOADate();
                sAll = "";
                sUnit = "All";
                if (rbUnitA.Checked) sUnit = "A";
                if (rbUnitB.Checked) sUnit = "B";
                if (rbUnitC.Checked) sUnit = "C";
                idx = 0;
                try{

                    for (double i = sDate; i < eDate + 1; i++){
                        string fn = LogWR_.GET_PathOperation(i, PATH_.LogLotLogList) + fLOG;
                        if (!File.Exists(fn)) continue;
                        string[] sARR = File.ReadAllLines(fn);
                        for (int cnt = 0; cnt < sARR.Length; cnt++){
                            string[] subarr = sARR[cnt].Split(',');
                            if (sUnit != "All" && sUnit != subarr[2]) continue;
                            sAll += sARR[cnt] + ETC.CrLf;
                            idx += 1;
                        }
                    }
                    string[] allARR = sAll.Split(ETC.CrLf);
                    string LotLogs = "";
                    bool LotLogView = false;
                    for (int i = 0; i < idx; i++){
                        string[] subARR = allARR[i].Split(',');
                        if (subARR.Length < 3) continue;
                        if (subARR[3] == SelectLotID){
                            LotLogView = true;
                            LotLogs = allARR[i];
                            break;
                        }
                    }
                    if (LotLogView){
                        LBX_LOTINFO.Items.Clear();
                        string[] LotLogList = LotLogs.Split(',');
                        if (LotLogList.Length < 55) return;
                        LOT_ID.Text                 = LotLogList[3];
                        LOT_ABF.Text                = LotLogList[29];
                        LBL_SPINDLE1_ID.Text        = LotLogList[54];
                        LBL_SPINDLE2_ID.Text        = LotLogList[55];
                        dcLOT_START.Text            = LotLogList[11];
                        dcLOT_END.Text              = LotLogList[12];
                        lbLOT_RUN.Text              = DATA_.cMATH.IntToTime(int.Parse(LotLogList[14]));
                        lbLOT_STOP.Text             = DATA_.cMATH.IntToTime(int.Parse(LotLogList[15]));
                        lbLOT_ERROR.Text            = DATA_.cMATH.IntToTime(int.Parse(LotLogList[16]));

                        LB_STRIP_CNT.Text           = LotLogList[36];
                        LB_GOOD_CNT.Text            = LotLogList[46];
                        LB_ITS_CNT.Text             = LotLogList[49];
                        LB_GOODTRAY_CNT.Text        = LotLogList[50];
                        LB_UNIT_CNT.Text            = LotLogList[52];
                        LB_NG_CNT.Text              = LotLogList[47];
                        LB_XOUT_CNT.Text            = LotLogList[48];
                        LB_NGTRAY_CNT.Text          = LotLogList[51];

                        try{
                            AddLotInfo("RECIPE = " + LotLogList[7]);
                            AddLotInfo("LOT TYPE = " + LotLogList[19]);
                            AddLotInfo("PROUDUCT TYPE = " + LotLogList[57]);
                            AddLotInfo("TOOL NO = " + LotLogList[8]);
                            AddLotInfo("ITS LOTID IN = " + LotLogList[9]);
                            AddLotInfo("ITS LOTID CT = " + LotLogList[10]);
                            AddLotInfo("UNIT SIZE X = " + LotLogList[22]);
                            AddLotInfo("UNIT SIZE Y = " + LotLogList[23]);
                            AddLotInfo("UNITSIZE UPPER = " + LotLogList[24]);
                            AddLotInfo("UNITSIZE LOWER = " + LotLogList[25]);
                            AddLotInfo("THICK = " + LotLogList[26]);
                            AddLotInfo("THICK UPPER = " + LotLogList[26]);
                            AddLotInfo("THICK LOWER = " + LotLogList[27]);
                            AddLotInfo("ABFMATERIAL = " + LotLogList[29]);

                            //22.1129 HK.PARK 추가
                            AddLotInfo("BOT LANDTOPKG X = " + LotLogList[59]);
                            AddLotInfo("BOT CHAMFERLEN TM X = " + LotLogList[59]);
                            AddLotInfo("BOT CHAMFERLEN TP X = " + LotLogList[60]);
                            AddLotInfo("BOT LANDTOPKG Y = " + LotLogList[61]);
                            AddLotInfo("BOT CHAMFERLEN TM Y = " + LotLogList[62]);
                            AddLotInfo("BOT CHAMFERLEN TP Y = " + LotLogList[63]);

                            AddLotInfo("TOP LANDTOPKG X = " + LotLogList[64]);
                            AddLotInfo("TOP CHAMFERLEN TM X = " + LotLogList[65]);
                            AddLotInfo("TOP CHAMFERLEN TP X = " + LotLogList[66]);
                            AddLotInfo("TOP LANDTOPKG Y = " + LotLogList[67]);
                            AddLotInfo("TOP CHAMFERLEN TM Y = " + LotLogList[68]);
                            AddLotInfo("TOP CHAMFERLEN TP Y = " + LotLogList[69]);
                        }
                        catch (Exception EX){
                            LogWR_.SaveLogException("FrmLOG -> BTN_LOTINFO_OPEN_Click Dislay Fail", EX);
                        }

                        string[] xVAL = { "RUN TIME", "STOP TIME", "ERROR TIME" };
                        try{
                            LotVAL[0] = (double)(((double)(int.Parse(LotLogList[14])) / (double)(int.Parse(LotLogList[13]))) * 100);
                            LotVAL[1] = (double)(((double)(int.Parse(LotLogList[15])) / (double)(int.Parse(LotLogList[13]))) * 100);
                            LotVAL[2] = (double)(((double)(int.Parse(LotLogList[16])) / (double)(int.Parse(LotLogList[13]))) * 100);
                            
                            for (int i = 0; i < xVAL.Length; i++){
                                xVAL[i] = xVAL[i] + ":" + string.Format("{0:0.00}", LotVAL[i]) + "%";
                                if (LotVAL[i] < 0.001) xVAL[i] = "0";
                            }
                        }
                        catch (Exception ex) { LogWR_.SaveLogException("frmLOG->DRAW_CHART(1)", ex); }

                        DV.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
                        DV.Series["Series1"]["PieLabelStyle"]   = "inside";
                        DV.Series["Series1"]["DoughnutRadius"]  = "60";
                        DV.Series["Series1"]["PieDrawingStyle"] = "Concave";
                        try{
                            DV.Series["Series1"].Points.DataBindXY(xVAL, LotVAL);
                        }
                        catch (Exception exp) { LogWR_.SaveLogException("frmLOG->DRAW_CHART(2)", exp); }
                        sm = 0;
                        for (int i = 0; i < LotVAL.Length; i++){
                            sm += LotVAL[i];
                        }

                        idx = 0;
                        DateTime LotStartTime = DateTime.Parse(dcLOT_START.Text);
                        DateTime LotEndTime = DateTime.Parse(dcLOT_END.Text);
                        double LotSDate = LotStartTime.Date.ToOADate();
                        double LotEDate = LotEndTime.Date.ToOADate();
                        try{
                            for (double d = sDate; d < eDate + 1; d++){
                                sPath = GET_LOG_FILE_PATH(d);
                                if (!File.Exists(sPath)) continue;
                                if (LotSDate == d || LotEDate == d){
                                    int rCNT = FILE_.RDInt(sPath, "ERROR", "COUNT", 0);
                                    for (int i = 0; i < rCNT; i++){
                                        LotCERR[idx].BeginTime = FILE_.RDString(sPath, "BEGIN TIME", i.ToString(), "");
                                        DateTime ErrorTime  = DateTime.Parse(LotCERR[idx].BeginTime);
                                        long ErrorTicks     = ErrorTime.Ticks;
                                        long LotStartTicks  = LotStartTime.Ticks;
                                        long LotEndTicks    = LotEndTime.Ticks;

                                        if (LotStartTicks <= ErrorTicks && LotEndTicks >= ErrorTicks){
                                            LotCERR[idx].EndTime = FILE_.RDString(sPath, "END TIME", i.ToString(), "");
                                            LotCERR[idx].ErrorNumber = FILE_.RDInt(sPath, "ERROR NUMBER", i.ToString(), 0);
                                            idx += 1;
                                            if (idx >= cERR.Length) break;
                                        }
                                    }
                                }
                                if (idx >= cERR.Length) break;
                            }

                            DGV_LOT_ERROR.RowCount = idx;
                            for (int i = 0; i < idx; i++){
                                DGV_LOT_ERROR.Rows[i].Cells[0].Value = i.ToString();
                                DGV_LOT_ERROR.Rows[i].Cells[1].Value = LotCERR[i].ErrorNumber;
                                DGV_LOT_ERROR.Rows[i].Cells[2].Value = E.GET_ERROR_NAME(LotCERR[i].ErrorNumber);
                                DGV_LOT_ERROR.Rows[i].Cells[3].Value = LotCERR[i].BeginTime;
                                DGV_LOT_ERROR.Rows[i].Cells[4].Value = LotCERR[i].EndTime;
                            }
                            VIEW_LOT_ERROR_HISTOGRAM(idx);
                        }
                        catch (Exception ex) { LogWR_.SaveLogException("frmLG->VIEW_ERROR", ex); }
                    }
                }
                catch (Exception EX) { LogWR_.SaveLogException("frmLOG->VIEW_LOTLOG", EX); }
            }
            catch (Exception ex) { LogWR_.SaveLogException("frmLOG->BTN_LOTINFO_OPEN_Click", ex); }
        }

        private void LOG_Load(object sender, EventArgs e){
            arrTEMP = new int[CNT_.ERR];
            if (LogPAGE < 0) LOG_CLICK(btnLOG_0, EventArgs.Empty);
            tmrHISTORY.Enabled = true;
        }
        void VIEW_PROCESS(){
            string fLOG = "PROCESS.log";
            sDate = dtBegin.Value.Date.ToOADate();
            eDate = dtEnd.Value.Date.ToOADate();
            sAll = "";
            sUnit = "All";
            if (rbUnitA.Checked) sUnit = "A";
            if (rbUnitB.Checked) sUnit = "B";
            if (rbUnitC.Checked) sUnit = "C";
            int idx = 0;
            try{
                for (double d = sDate; d < eDate + 1; d++){
                    string fn = LogWR_.GET_PathOperation(d, PATH_.LogPROCESS) + fLOG;
                    if (!File.Exists(fn)) continue;
                    string[] sARR = File.ReadAllLines(fn);
                    for (int cnt = 0; cnt < sARR.Length; cnt++){
                        if (idx > 5000) break;
                        string[] subarr = sARR[cnt].Split(',');
                        if (sUnit != "All" && sUnit != subarr[2]) continue;
                        sAll += sARR[cnt] + ETC.CrLf;
                        idx += 1;
                    }
                }

                dgvProcess.RowCount = idx;
                string[] allARR = sAll.Split(ETC.CrLf);
                int wIdx = 0;
                for (int i = 0; i < idx; i++){
                    string[] subARR = allARR[i].Split(',');
                    if (subARR.Length < 5) continue;
                    dgvProcess.Rows[wIdx].Cells[0].Value = i.ToString();

                    dgvProcess.Rows[wIdx].Cells[1].Value = subARR[4];
                    dgvProcess.Rows[wIdx].Cells[2].Value = subARR[0];
                    dgvProcess.Rows[wIdx].Cells[3].Value = subARR[1];
                    dgvProcess.Rows[wIdx].Cells[4].Value = subARR[2];
                    dgvProcess.Rows[wIdx].Cells[5].Value = subARR[5];

                    wIdx++;
                }
                if (dgvProcess.RowCount <= 0) return;

                //DataGridViewCell _dgvCell = dataGridView1.Rows[dataGridView1.RowCount].Cells[1];
                //dataGridView1.FirstDisplayedCell = _dgvCell;
                //dataGridView1.CurrentCell = _dgvCell;
            }
            catch (Exception ex) { LogWR_.SaveLogException("frmLOG->VIEW_MEASURE", ex); }
        }
        void VIEW_MC_EVENT(){
            string fLOG = "MACHINE_EVENT.log";
            sDate = dtBegin.Value.Date.ToOADate();
            eDate = dtEnd.Value.Date.ToOADate();
            sAll = "";
            sUnit = "All";
            if (rbUnitA.Checked) sUnit = "A";
            if (rbUnitB.Checked) sUnit = "B";
            if (rbUnitC.Checked) sUnit = "C";
            idx = 0;

            try{
                for (double i = sDate; i < eDate + 1; i++){
                    string fn = LogWR_.GET_PathOperation(i, PATH_.LogAppEVENT) + fLOG;
                    if (!File.Exists(fn)) continue;
                    string[] sARR = File.ReadAllLines(fn);
                    for (int cnt = 0; cnt < sARR.Length; cnt++){
                        if (idx > 5000) break;
                        string[] subarr = sARR[cnt].Split(',');
                        if (sUnit != "All" && sUnit != subarr[2]) continue;
                        sAll += sARR[cnt] + ETC.CrLf;
                        idx += 1;
                    }
                }

                dgwEvent.RowCount = idx;
                string[] allARR = sAll.Split(ETC.CrLf);
                int wIdx = 0;
                for (int i = 0; i < idx; i++){
                    string[] subARR = allARR[i].Split(',');
                    if (subARR.Length < 5) continue;
                    dgwEvent.Rows[wIdx].Cells[0].Value = i.ToString();

                    dgwEvent.Rows[wIdx].Cells[1].Value = subARR[4];
                    dgwEvent.Rows[wIdx].Cells[2].Value = subARR[0];
                    dgwEvent.Rows[wIdx].Cells[3].Value = subARR[1];
                    dgwEvent.Rows[wIdx].Cells[4].Value = subARR[2];
                    dgwEvent.Rows[wIdx].Cells[5].Value = subARR[5];
                    wIdx++;
                }
                if (dgwEvent.RowCount <= 0) return;
            }
            catch (Exception ex) { LogWR_.SaveLogException("frmLOG->EVENT", ex); }
        }

        public void ERR_CLEAR(){
            for (int i = 0; i < CNT_.ERR; i++){
                if (!rERR[i].use) break;
                rERR[i].EndTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            }
            ERR_SAVE();
            for (int i = 0; i < CNT_.ERR; i++) rERR[i].use = false;
        }
        public void ERR_SAVE(){
            sCALENDAR = DateTime.Now.ToString("yyyyMMdd");
            string fn = LogWR_.MAKE_DATE_FOLDER(PATH_.ErrLOG) + "ERROR_DB_" + sCALENDAR + ".lgf";
            int idx = FILE_.RDInt(fn, "ERROR", "COUNT", 0);
            for (int i = 0; i < CNT_.RecERR; i++){
                if (!rERR[i].use) break;
                FILE_.WRInt(fn, "ERROR NUMBER", idx.ToString(), rERR[i].ErrorNumber);
                FILE_.WRString(fn, "BEGIN TIME", idx.ToString(), rERR[i].BeginTime);
                FILE_.WRString(fn, "END TIME", idx.ToString(), rERR[i].EndTime);
                idx++;
            }
            FILE_.WRInt(fn, "ERROR", "COUNT", idx);
        }

        void ERR_RECORD(int idx){
            for (int i = 0; i < CNT_.RecERR; i++){
                if (rERR[i].use) continue;
                rERR[i].use = true;
                rERR[i].BeginTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                rERR[i].ErrorNumber = idx;
                return;
            }
        }
        void UPDATA_ERROR_HISTORY(int idx){
            if (DATA_.bErrNotSave) return;
            if (DATA_.ErrINFO[idx].enRec) return;
            ERR_RECORD(idx);
            string sLOG = "[" + idx.ToString() + "]" + ETC.cspCr + "[" + idx.ToString() + "]" + DATA_.ErrName[idx];
            LogWR_.SAVE_ERROR_LOG(sLOG);
        }

        public void DRAW_CHART(){
            if (rbSelected.Checked) DATA_.viewSPC = DATA_.dateSPC;
            else DATA_.viewSPC = DATA_.oSPC;
            string[] xVAL = { "RUN TIME", "STOP TIME", "PAUSE TIME", "ERROR TIME", "RUN-READY TIME", "RUN-DOWN TIME" };
            try{
                yVAL[0] = (double)(((double)(DATA_.viewSPC.mlRunTime) / (double)(DATA_.viewSPC.mlWorkTime)) * 100);
                yVAL[1] = (double)(((double)(DATA_.viewSPC.mlStopTime) / (double)(DATA_.viewSPC.mlWorkTime)) * 100);
                yVAL[2] = (double)(((double)(DATA_.viewSPC.mlPauseTime) / (double)(DATA_.viewSPC.mlWorkTime)) * 100);
                yVAL[3] = (double)(((double)(DATA_.viewSPC.mlErrorTime) / (double)(DATA_.viewSPC.mlWorkTime)) * 100);
                yVAL[4] = (double)(((double)(DATA_.viewSPC.mlRunWaitTime) / (double)(DATA_.viewSPC.mlWorkTime)) * 100);
                yVAL[5] = (double)(((double)(DATA_.viewSPC.mlRunDownTime) / (double)(DATA_.viewSPC.mlWorkTime)) * 100);

                for (int i = 0; i < xVAL.Length; i++){
                    xVAL[i] = xVAL[i] + ":" + string.Format("{0:0.0}", yVAL[i]) + "%";
                    if (yVAL[i] < 1) xVAL[i] = "0";
                }
            }
            catch (Exception ex) { LogWR_.SaveLogException("frmLOG->DRAW_CHART(1)", ex); }

            chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            chart1.Series["Series1"]["PieLabelStyle"] = "inside";
            chart1.Series["Series1"]["DoughnutRadius"] = "60";
            chart1.Series["Series1"]["PieDrawingStyle"] = "Concave";
            try{
                chart1.Series["Series1"].Points.DataBindXY(xVAL, yVAL);
            }
            catch (Exception exp) { LogWR_.SaveLogException("frmLOG->DRAW_CHART(2)", exp); }

            sm = 0;
            for (int i = 0; i < yVAL.Length; i++){
                sm += yVAL[i];
            }
        }

        private void ErrorEdit_CheckedChanged(object sender, EventArgs e) { SUBFRM_.gErrList.RD_ERRORLIST(); }

        private void ViewSpc_Click(object sender, EventArgs e){
            DATA_.dateSPC.mlWorkTime = 1;
            DATA_.dateSPC.mlRunTime = 0;
            DATA_.dateSPC.mlStopTime = 0;
            DATA_.dateSPC.mlPauseTime = 0;
            DATA_.dateSPC.mlErrorTime = 0;
            DATA_.dateSPC.mlRunWaitTime = 0;
            DATA_.dateSPC.mlRunDownTime = 0;
            DATA_.dateSPC.mlPauseCount = 0;

            sDate = dtBegin.Value.Date.ToOADate();
            eDate = dtEnd.Value.Date.ToOADate();
            sUnit = "All";
            if (rbUnitA.Checked) sUnit = "A";
            if (rbUnitB.Checked) sUnit = "B";
            if (rbUnitC.Checked) sUnit = "C";

            for (double i = sDate; i < eDate + 1; i++){
                dt = DateTime.FromOADate(i);
                if (sUnit == "All"){
                    DATA_.cSPC.LD_SPC_DATA(dt, "A");
                    DATA_.cSPC.LD_SPC_DATA(dt, "B");
                    DATA_.cSPC.LD_SPC_DATA(dt, "C");
                }
                else{
                    DATA_.cSPC.LD_SPC_DATA(dt, sUnit);
                }
            }
        }

        private void TimerHISTORY_Tick(object sender, EventArgs e){
            //if (DATA_.eMCStatus == eMachineStatus.AUTO) return;
            pos = 0;
            UpdateFlag = false;
            for (int i = 0; i < CNT_.ERR; i++){
                if (DATA_.IsERR[i]){
                    UpdateFlag = false;
                    for (int j = 0; j < gridError.RowCount; j++){
                        if (gridError.Rows[j].Cells[0].Value.ToString() == i.ToString()){
                            UpdateFlag = true;
                            break;
                        }
                    }
                    if (!UpdateFlag) UPDATA_ERROR_HISTORY(i);
                    if (pos < gridError.RowCount){
                        gridError.Rows[pos].Cells[0].Value = i.ToString();
                        gridError.Rows[pos].Cells[1].Value = E.GET_ERROR_NAME(i);
                        pos += 1;
                    }
                }
            }
            for (int i = pos; i < gridError.RowCount; i++){
                gridError.Rows[pos].Cells[0].Value = "";
                gridError.Rows[pos].Cells[1].Value = "";
                pos += 1;
            }

            if (DATA_.eMCStatus == eMachineStatus.AUTO) return;
            if (!bFRM_VIEW) return;
            lbWorkTime.Text             = DATA_.cMATH.IntToTime(DATA_.viewSPC.mlWorkTime);
            lbRUNTime.Text              = DATA_.cMATH.IntToTime(DATA_.viewSPC.mlRunTime) + DATA_.cMATH.GetRate(DATA_.viewSPC.mlWorkTime, DATA_.viewSPC.mlRunTime);
            lbSTOPTime.Text             = DATA_.cMATH.IntToTime(DATA_.viewSPC.mlStopTime) + DATA_.cMATH.GetRate(DATA_.viewSPC.mlWorkTime, DATA_.viewSPC.mlStopTime);
            lbPAUSETime.Text            = DATA_.cMATH.IntToTime(DATA_.viewSPC.mlPauseTime) + DATA_.cMATH.GetRate(DATA_.viewSPC.mlWorkTime, DATA_.viewSPC.mlPauseTime);
            lbDOWNTime.Text             = DATA_.cMATH.IntToTime(DATA_.viewSPC.mlErrorTime) + DATA_.cMATH.GetRate(DATA_.viewSPC.mlWorkTime, DATA_.viewSPC.mlErrorTime);
            lbPRODUCT_WAIT_TIME.Text    = DATA_.cMATH.IntToTime(DATA_.viewSPC.mlRunWaitTime) + DATA_.cMATH.GetRate(DATA_.viewSPC.mlWorkTime, DATA_.viewSPC.mlRunWaitTime);
            lbRUN_DOWN_TIME.Text        = DATA_.cMATH.IntToTime(DATA_.viewSPC.mlRunDownTime) + DATA_.cMATH.GetRate(DATA_.viewSPC.mlWorkTime, DATA_.viewSPC.mlRunDownTime);
            lbPAUSE_COUNT.Text          = DATA_.viewSPC.mlPauseCount.ToString();

            try{
                if (DATA_.viewSPC.mlPauseCount > 0){
                    lbMtba.Text = "MTBA = " + string.Format("{0:0.00}", DATA_.viewSPC.mlRunTime / DATA_.viewSPC.mlPauseCount);
                    lbMtbf.Text = "MTBF = " + string.Format("", (DATA_.viewSPC.mlRunTime - DATA_.viewSPC.mlStopTime) / DATA_.viewSPC.mlPauseCount);
                }
            }
            catch (Exception exc) { LogWR_.SaveLogException("frmLOG -> tmrHISTORY_Tick(MTB)", exc); }

            DRAW_CHART();
        }
    }
}