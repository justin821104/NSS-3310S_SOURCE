using Object;
using System;
using System.IO;
using System.Windows.Forms;

namespace LIB_.DateType
{
    public class SPC_ : DATA_
    {
        public eMachineStatus STATE, BEFORE_STATE;
        public int ERR_OVERTIME = 0;
        public int WAIT_RUNTIME = 0;
        public int BEFORE_TIME  = 0;

        public void LOAD_SPC(){
            double sDATE        = DateTime.Now.ToOADate();
            string WORK_UNIT    = LogWR_.GET_WORK_UNIT();
            string FileName     = LogWR_.GET_PathOperation(sDATE, PATH_.LogSPC) + WORK_UNIT + "_SPC.csv";

            if (File.Exists(FileName)){
                string[] sFIRST = File.ReadAllText(FileName).Split(ETC.CrLf);
                try{
                    string[] sSECOND = sFIRST[1].Split(',');

                    oSPC.mlWorkTime     = int.Parse(sSECOND[0]);
                    oSPC.mlRunTime      = int.Parse(sSECOND[1]);
                    oSPC.mlStopTime     = int.Parse(sSECOND[2]);
                    oSPC.mlPauseTime    = int.Parse(sSECOND[3]);
                    oSPC.mlErrorTime    = int.Parse(sSECOND[4]);
                    oSPC.mlRunWaitTime  = int.Parse(sSECOND[5]);
                    oSPC.mlRunDownTime  = int.Parse(sSECOND[6]);
                    oSPC.mlPauseCount   = int.Parse(sSECOND[7]);
                }
                catch (Exception ex) { MessageBox.Show("CLS_SPC -> LOAD_SPC FAIL" + ETC.NewLine + ex.ToString()); }
            }
        }
        void SAVE_SPC(){
            string sTitle   = "설비 온-시간,가동시간,정지시간,순간정지시간,고장시간,가동대기시간,런-다운시간,순간정지횟수" + ETC.CrLf;
            string sVal     = string.Empty;

            sVal += oSPC.mlWorkTime.ToString() + ",";
            sVal += oSPC.mlRunTime.ToString() + ",";
            sVal += oSPC.mlStopTime.ToString() + ",";
            sVal += oSPC.mlPauseTime.ToString() + ",";
            sVal += oSPC.mlErrorTime.ToString() + ",";
            sVal += oSPC.mlRunWaitTime.ToString() + ",";
            sVal += oSPC.mlRunDownTime.ToString() + ",";
            sVal += oSPC.mlPauseCount.ToString() + ",";

            string wookUnit = LogWR_.GET_WORK_UNIT();
            string fn       = LogWR_.GET_DirNameDate(PATH_.LogSPC) + wookUnit + "_SPC.csv";
            FILE_.WR_File(fn, sTitle + sVal, false);
        }
        void CLEAN_SPC(){
            oSPC.mlWorkTime     = 0;
            oSPC.mlRunTime      = 0;
            oSPC.mlStopTime     = 0;
            oSPC.mlPauseTime    = 0;
            oSPC.mlErrorTime    = 0;
            oSPC.mlRunWaitTime  = 0;
            oSPC.mlRunDownTime  = 0;
            oSPC.mlPauseCount   = 0;
        }
        public void LD_SPC_DATA(DateTime dt, string WorkUnit){
            double sDATE    = dt.ToOADate();
            string fn       = LogWR_.GET_PathOperation(sDATE, PATH_.LogSPC) + WorkUnit + "_SPC.csv";
            if (File.Exists(fn)){
                string[] sFIRST = File.ReadAllText(fn).Split(ETC.CrLf);
                if (sFIRST.Length < 2) return;
                string[] sSECOND = sFIRST[1].Split(',');
                try{
                    dateSPC.mlWorkTime      += int.Parse(sSECOND[0]);
                    dateSPC.mlRunTime       += int.Parse(sSECOND[1]);
                    dateSPC.mlStopTime      += int.Parse(sSECOND[2]);
                    dateSPC.mlPauseTime     += int.Parse(sSECOND[3]);
                    dateSPC.mlErrorTime     += int.Parse(sSECOND[4]);
                    dateSPC.mlRunWaitTime   += int.Parse(sSECOND[5]);
                    dateSPC.mlRunDownTime   += int.Parse(sSECOND[6]);
                    dateSPC.mlPauseCount    += int.Parse(sSECOND[7]);
                }
                catch (Exception exp) { LogWR_.SaveLogException("CLS_SPC->LD_SPC_DATE", exp); }
            }
        }

        public void RUN_SPC(){
            STATE = eMCStatus;
            oSPC.mlWorkTime += 1;
            if ((STATE == eMachineStatus.EMSSTOP || STATE == eMachineStatus.ERRSTOP) && (BEFORE_STATE != eMachineStatus.EMSSTOP && BEFORE_STATE != eMachineStatus.ERRSTOP)){
                oSPC.mlPauseCount += 1;
                ERR_OVERTIME = 0;
            } // 순간 정지 횟수.
            if (STATE == eMachineStatus.AUTO){
                ERR_OVERTIME = 0;
                oSPC.mlRunTime += 1;
                if (!bWaitProduct) WAIT_RUNTIME = 0;
                else{
                    WAIT_RUNTIME += 1;
                    if (WAIT_RUNTIME > (60 * 5)) oSPC.mlRunDownTime += 1; //5분초과
                    else oSPC.mlRunWaitTime += 1;
                }
            } // AUTO-RUN 상태
            else oSPC.mlStopTime += 1;
            if (STATE == eMachineStatus.EMSSTOP || STATE == eMachineStatus.ERRSTOP){
                ERR_OVERTIME += 1;
                if (ERR_OVERTIME > (60 * 5)) oSPC.mlErrorTime += 1; //5분초과
                else oSPC.mlPauseTime += 1;
            } // 에러 발생

            //SPC 결과
            oSPC.strWorkTime    = cMATH.IntToTime(oSPC.mlWorkTime);
            oSPC.strRunTime     = cMATH.IntToTime(oSPC.mlRunTime) + cMATH.GetRate(oSPC.mlWorkTime, oSPC.mlRunTime);
            oSPC.strStopTime    = cMATH.IntToTime(oSPC.mlStopTime) + cMATH.GetRate(oSPC.mlWorkTime, oSPC.mlStopTime);
            oSPC.strPauseTime   = cMATH.IntToTime(oSPC.mlPauseTime) + cMATH.GetRate(oSPC.mlWorkTime, oSPC.mlPauseTime);
            oSPC.strErrorTime   = cMATH.IntToTime(oSPC.mlErrorTime) + cMATH.GetRate(oSPC.mlWorkTime, oSPC.mlErrorTime);
            oSPC.strRunWaitTime = cMATH.IntToTime(oSPC.mlRunWaitTime) + cMATH.GetRate(oSPC.mlWorkTime, oSPC.mlRunWaitTime);
            oSPC.strRunDownTime = cMATH.IntToTime(oSPC.mlRunDownTime) + cMATH.GetRate(oSPC.mlWorkTime, oSPC.mlRunDownTime);
            oSPC.strPauseCount  = oSPC.mlPauseCount.ToString();

            LogWR_.SAVE_LOG_PARAMETER();
            SAVE_SPC();
            LogWR_.SAVE_LOG();

            LogWR_.DELETE_OLD_LOGs();
            LogWR_.DELETE_LOG_FOLDERs();

            if (BEFORE_TIME != DateTime.Now.Hour && DateTime.Now.Hour == START_HOUR) CLEAN_SPC();
            BEFORE_STATE    = eMCStatus;
            BEFORE_TIME     = DateTime.Now.Hour;
        }
    }  
}
