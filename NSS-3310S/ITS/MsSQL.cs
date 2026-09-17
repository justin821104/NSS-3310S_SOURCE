using LIB_.DateType;
using Object;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace NSS_3310S.ITS
{
    public class MsSQL{
        static string strConnection = ""; // "server=192.168.6.160;" +
                                      //"database=MCDB;" +
                                      //"uid=mes_If;" +
                                      //"pwd=essys365;";
        static SqlConnection sqlConnection = new SqlConnection();

        public static string sIP        = "";//"192.168.6.160";
        public static string sDBName    = "";//"APERIO_ITS";
        public static string sID        = "";//"Sawing_Service";
        public static string sPwd       = "";//"ApDMxjkdpRLA#%";
        public static bool bOpen        = false;
        public static bool bITSDataReading = false;

        public static bool IsConnected() {
            if (sqlConnection.State != ConnectionState.Open){
                sqlConnection.ConnectionString = strConnection;
                sqlConnection.Open();
            }
            if (sqlConnection.State == ConnectionState.Open) bOpen = true;
            else bOpen = false;

            return sqlConnection.State == ConnectionState.Open; 
        }

        public static bool Open(string strServer, string strDbName, string strID, string strPass){
            strConnection = string.Format("server={0};database={1};uid={2};pwd={3}", strServer, strDbName, strID, strPass);
            try{
#if _MsSQL
                if (DATA_.prMACHINE[CP.UseMsSQL] == (int)eUSE.NotUSE) return false;
                sqlConnection.ConnectionString = strConnection;
                sqlConnection.Open();
                bOpen = true;
                return true;
#else
            bOpen = false;
            return false;
#endif
            }
            catch (Exception e){
                LogWR_.SaveLogException("MsSql -> OPEN FAIL", e);
                DATA_.ConfirmUser[W.DllWarnning].msg = e.Message;
                W.ViewWarning(T.Manual, W.DllWarnning);
            }
            bOpen = false;
            return false;
        }

        public static void Close(){
            if (sqlConnection.State == ConnectionState.Open){
                sqlConnection.Close();
                bOpen = false;
            }
        }

        public static string GetPrevProcResult(string sLotID){
            LogWR_.SaveProgramCheck("SQL DB READING START", "");
            try {
                Stopwatch sw = Stopwatch.StartNew();
                string tmr = $"Query Time : {sw.ElapsedMilliseconds} ms";
                LogWR_.Log_DBWrite(sLotID, "START READING" + ETC.cspTab + tmr);
                bITSDataReading = false;
                if (DATA_.prMACHINE[CP.UseMsSQL] == (int)eUSE.NotUSE) {
                    LogWR_.SaveProgramCheck("SQL DB NOT READING", "");
                    return ""; 
                }
                if (!IsConnected()) {
                    LogWR_.SaveProgramCheck("SQL DB READING FAIL (1)", "");
                    E.OnERROR(E.emsITSServoNotConnecting);
                    return "";
                }
                //DB 연결 상태 체크 및 오픈!
                if (sqlConnection.State != ConnectionState.Open) {
                    sqlConnection.ConnectionString = strConnection;
                    sqlConnection.Open();
                    bOpen = true;
                }

                if (DATA_.prMACHINE[CP.DBReadMode] == 0) {
                    #region First...
                    DataSet ds = new DataSet();
                    //LotNumber, StripID, DefectCount
                    LogWR_.SaveProgramCheck("SQL DB READING OPTION 0 COUNT START", "");
                    using (SqlCommand sqlCmd = new SqlCommand("pts_Get_StripDefectCount", sqlConnection)) {
                        //sqlCmd.CommandTimeout = 0;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@vchLotNumber", sLotID);
                    
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                        adapter.Fill(ds);
                        string rtnValue = "";
                        if (ds.Tables.Count > 0) {
                            int nIndex = ds.Tables[0].Rows.Count;
                            if (nIndex <= 0) {
                                LogWR_.SaveProgramCheck("SQL DB READING FAIL (2)", "");
                                E.OnERROR(E.emsITSCountDataReadingFail);
                                return "";
                            }
                            for (int n = 0; n < nIndex; n++) {
                                DataRow dr = ds.Tables[0].Rows[n];
                                if (dr.ItemArray[0] != DBNull.Value) { }
                                for (int i = 0; i < dr.ItemArray.Length; i++) {
                                    rtnValue += dr.ItemArray[i];
                                    if (i < dr.ItemArray.Length - 1) rtnValue += ",";
                                }
                                rtnValue += ETC.NewLine;
                            }
                        }
                        else {
                            LogWR_.SaveProgramCheck("SQL DB READING FAIL (3)", "");
                            E.OnERROR(E.emsITSCountDataReadingFail);
                            return "";
                        }
                        TEACH_.SaveITSInfo(sLotID + "_StripDefectCount", rtnValue);
                        TEACH_.SaveStripDefectCount(sLotID, rtnValue);
                        TEACH_.SaveITS_StripCount(rtnValue);
                        bITSDataReading = true;
                    }
                    LogWR_.SaveProgramCheck("SQL DB READING OPTION 0 COUNT END", "");

                    //LotNumber, SHIPTO, StripID, StripX, StripY, XOUT_X, XOUT_Y, LAngle
                    LogWR_.SaveProgramCheck("SQL DB READING OPTION 0 LOCATION START", "");
                    ds = new DataSet();
                    using (SqlCommand sqlCmd = new SqlCommand("pts_Get_StripDefectLocationList", sqlConnection)) {
                        //sqlCmd.CommandTimeout = 0;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@vchLotNumber", sLotID);
                        //SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                        //adapter.Fill(ds);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd)) {
                            adapter.Fill(ds);
                        }
                    
                        string rtnValue = "";
                        if (ds.Tables.Count > 0) {
                            int nIndex = ds.Tables[0].Rows.Count;
                            if (nIndex <= 0){
                                LogWR_.SaveProgramCheck("SQL DB READING FAIL (4)", "");
                                E.OnERROR(E.emsITSLocationDataReadingFail);
                                return "";
                            }
                            for (int n = 0; n < nIndex; n++) {
                                DataRow dr = ds.Tables[0].Rows[n];
                                if (dr.ItemArray[0] != DBNull.Value) { }
                                for (int i = 0; i < dr.ItemArray.Length; i++) {
                                    rtnValue += dr.ItemArray[i];
                                    if (i < dr.ItemArray.Length - 1) rtnValue += ",";
                                }
                                rtnValue += ETC.NewLine;
                            }
                        }
                        else {
                            LogWR_.SaveProgramCheck("SQL DB READING FAIL (5)", "");
                            E.OnERROR(E.emsITSLocationDataReadingFail);
                            return "";
                        }
                        TEACH_.SaveITSInfo(sLotID + "_StripDefectLocationList", rtnValue);
                        TEACH_.SaveStripDefectLocationList(sLotID, rtnValue);
                        TEACH_.SaveITS_StripLocation(rtnValue);
                    }
                    LogWR_.SaveProgramCheck("SQL DB READING OPTION 0 LOCATION END", "");
                    #endregion First...
                }
                else if (DATA_.prMACHINE[CP.DBReadMode] == 1) {
                    #region >> 변경 대덕 검증 필요!
                    // 1. 첫 번째 작업: StripDefectCount
                    LogWR_.SaveProgramCheck("SQL DB READING OPTION 1 COUNT START", "");
                    ProcessDatabaseTask(sLotID, "pts_Get_StripDefectCount", "_StripDefectCount",
                        TEACH_.SaveStripDefectCount, TEACH_.SaveITS_StripCount);
                    LogWR_.SaveProgramCheck("SQL DB READING OPTION 1 COUNT END", "");

                    // 2. 두 번째 작업: StripDefectLocationList
                    LogWR_.SaveProgramCheck("SQL DB READING OPTION 1 LOCATION START", "");
                    ProcessDatabaseTask(sLotID, "pts_Get_StripDefectLocationList", "_StripDefectLocationList",
                        TEACH_.SaveStripDefectLocationList, TEACH_.SaveITS_StripLocation);
                    LogWR_.SaveProgramCheck("SQL DB READING OPTION 1 LOCATION END", "");
                    bITSDataReading = true;
                    #endregion >> 변경 대덕 검증 필요!
                }
                else {
                    #region >> 마지막 체크 대덕 검증 필요!
                    // 1. 첫 번째 작업: Defect Count
                    LogWR_.SaveProgramCheck("SQL DB READING OPTION 2 COUNT START", "");
                    string defectCountData = GetQueryResultStream("pts_Get_StripDefectCount", sLotID);
                    if (!string.IsNullOrEmpty(defectCountData)) {
                        TEACH_.SaveITSInfo(sLotID + "_StripDefectCount", defectCountData);
                        TEACH_.SaveStripDefectCount(sLotID, defectCountData);
                        TEACH_.SaveITS_StripCount(defectCountData);
                    }
                    else {
                        LogWR_.SaveProgramCheck("SQL DB READING FAIL (6)", "");
                        E.OnERROR(E.emsITSCountDataReadingFail);
                        return "";
                    }
                    LogWR_.SaveProgramCheck("SQL DB READING OPTION 2 COUNT END", "");

                    // 2. 두 번째 작업: Defect Location List
                    LogWR_.SaveProgramCheck("SQL DB READING OPTION 2 LOCATION START", "");
                    string locationListData = GetQueryResultStream("pts_Get_StripDefectLocationList", sLotID);
                    if (!string.IsNullOrEmpty(locationListData)) {
                        TEACH_.SaveITSInfo(sLotID + "_StripDefectLocationList", locationListData);
                        TEACH_.SaveStripDefectLocationList(sLotID, locationListData);
                        TEACH_.SaveITS_StripLocation(locationListData);
                    }
                    else {
                        LogWR_.SaveProgramCheck("SQL DB READING FAIL (7)", "");
                        E.OnERROR(E.emsITSLocationDataReadingFail);
                        return "";
                    }
                    LogWR_.SaveProgramCheck("SQL DB READING OPTION 2 LOCATION END", "");
                    bITSDataReading = true;
                    #endregion >> 마지막 체크 대덕 검증 필요!
                }
                sw.Stop();
                tmr = $"Query Time : {sw.ElapsedMilliseconds} ms"; 
                LogWR_.Log_DBWrite(sLotID, "END READING" + ETC.cspTab + tmr);
            }
            catch (Exception e){
                LogWR_.SaveLogException("MsSql -> GetPrevProcResult FAIL", e);
                LogWR_.SaveMsSQLReadingFail("Lot Card ITS : " + CLOT.GET_LOT.ItsID  + " / MES ITS : " + CLOT.GET_LOT.ITS + " ERR : " + e.Message, "");
                DATA_.ConfirmUser[W.DllWarnning].msg = e.Message + ETC.NewLine + "ITS ID 확인 후 다시 LOT 등록 하셔야 합니다!";
                W.ViewWarning(T.Manual, W.DllWarnning);
            }
            DATA_.IsBIT[B.WaitITSReading] = false;
            LogWR_.SaveProgramCheck("SQL DB READING END", "");
            return "";
        }

        //중북 코드를 방지하고 지원을 안전하게 관리하기 위한 헬퍼 메서드.
        private static void ProcessDatabaseTask(string lotId, string spName, string suffix, Action<string, string> saveAction1, Action<string> saveAction2) {
            // StringBuilder와 DataSet을 using으로 관리하여 즉시 해제
            StringBuilder sb = new StringBuilder();
            using (DataSet ds = new DataSet())
            using (SqlCommand sqlCmd = new SqlCommand(spName, sqlConnection)) {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@vchLotNumber", lotId);

                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd)) {
                    adapter.Fill(ds);
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows) {
                        for (int i = 0; i < dt.Columns.Count; i++) {
                            sb.Append(dr[i].ToString());
                            if (i < dt.Columns.Count - 1) sb.Append(",");
                        }
                        sb.AppendLine();
                    }

                    string result = sb.ToString();
                    TEACH_.SaveITSInfo(lotId + suffix, result);
                    saveAction1(lotId, result);
                    saveAction2(result);
                }
                else { 
                    // db에 its 좌표 데이터 없음.
                }
            }
        }
    
        //SqlDataReader를 사용하는 핵심 로직
        private static string GetQueryResultStream(string spName, string lotId) {
            StringBuilder sb = new StringBuilder(); 
            using (SqlCommand sqlCmd = new SqlCommand(spName, sqlConnection)) {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@vchLotNumber", lotId);

                // CommandBehavior.SingleResult: 하나의 결과 집합만 사용함을 명시하여 최적화
                using (SqlDataReader reader = sqlCmd.ExecuteReader()) {
                    if (reader.HasRows) {
                        int fieldCount = reader.FieldCount;
                        while (reader.Read()) { // 서버에서 한 줄씩 가져옴 (매우 빠름)
                            for (int i = 0; i < fieldCount; i++) {
                                // DBNull 체크 후 문자열 추가
                                sb.Append(reader.IsDBNull(i) ? "" : Convert.ToString(reader[i])); //기존(초기) -> reader.GetValue(i).ToString()); 에서 -> Convert.ToString(reader[i]) 변경함 
                                if (i < fieldCount - 1) sb.Append(",");
                            }
                            sb.AppendLine(); //sb.Append('\n'); //\r\n
                        }
                    }
                }
            }
            return sb.ToString();
        }
    }
}