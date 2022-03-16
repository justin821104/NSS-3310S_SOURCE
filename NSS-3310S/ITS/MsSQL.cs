using Object;
using System;
using System.Data;
using System.Data.SqlClient;

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
                COM_.ViewWarning(T.Manual, W.DllWarnning);
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
            try{
                bITSDataReading = false;
                if (DATA_.prMACHINE[CP.UseMsSQL] == (int)eUSE.NotUSE) return "";
                if (!IsConnected()){
                    UTIL_.OnERROR(E.emsITSServoNotConnecting);
                    return "";
                }
                if (sqlConnection.State != ConnectionState.Open){
                    sqlConnection.ConnectionString = strConnection;
                    sqlConnection.Open();
                }
                DataSet ds = new DataSet();

                //LotNumber, StripID, DefectCount
                using (SqlCommand sqlCmd = new SqlCommand("pts_Get_StripDefectCount", sqlConnection)){
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@vchLotNumber", sLotID);
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                    adapter.Fill(ds);
                    string rtnValue = "";
                    if (ds.Tables.Count > 0){
                        int nIndex = ds.Tables[0].Rows.Count;
                        if (nIndex <= 0){
                            UTIL_.OnERROR(E.emsITSCountDataReadingFail);
                            return "";
                        }
                        for (int n = 0; n < nIndex; n++){
                            DataRow dr = ds.Tables[0].Rows[n];
                            if (dr.ItemArray[0] != DBNull.Value){

                            }
                            for (int i = 0; i < dr.ItemArray.Length; i++){
                                rtnValue += dr.ItemArray[i];
                                if (i < dr.ItemArray.Length - 1) rtnValue += ",";
                            }
                            rtnValue += ETC.NewLine;
                        }
                    }
                    else{
                        UTIL_.OnERROR(E.emsITSCountDataReadingFail);
                        return "";
                    }
                    TEACH_.SaveITSInfo(sLotID + "_StripDefectCount", rtnValue);
                    TEACH_.SaveStripDefectCount(sLotID, rtnValue);
                    TEACH_.SaveITS_StripCount(rtnValue);

                    bITSDataReading = true;
                }

                //LotNumber, SHIPTO, StripID, StripX, StripY, XOUT_X, XOUT_Y, LAngle
                ds = new DataSet();
                using (SqlCommand sqlCmd = new SqlCommand("pts_Get_StripDefectLocationList", sqlConnection)){
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@vchLotNumber", sLotID);
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                    adapter.Fill(ds);
                    string rtnValue = "";
                    if (ds.Tables.Count > 0){
                        int nIndex = ds.Tables[0].Rows.Count;
                        if (nIndex <= 0){
                            UTIL_.OnERROR(E.emsITSLocationDataReadingFail);
                            return "";
                        }
                        for (int n = 0; n < nIndex; n++){
                            DataRow dr = ds.Tables[0].Rows[n];
                            if (dr.ItemArray[0] != DBNull.Value){

                            }
                            for (int i = 0; i < dr.ItemArray.Length; i++){
                                rtnValue += dr.ItemArray[i];
                                if (i < dr.ItemArray.Length - 1) rtnValue += ",";
                            }
                            rtnValue += ETC.NewLine;
                        }
                    }
                    else{
                        UTIL_.OnERROR(E.emsITSLocationDataReadingFail);
                        return "";
                    }
                    TEACH_.SaveITSInfo(sLotID + "_StripDefectLocationList", rtnValue);
                    TEACH_.SaveStripDefectLocationList(sLotID, rtnValue);
                    TEACH_.SaveITS_StripLocation(rtnValue);
                }
            }
            catch (Exception e){
                LogWR_.SaveLogException("MsSql -> GetPrevProcResult FAIL", e);
                DATA_.ConfirmUser[W.DllWarningMessage].msg = e.Message + ETC.NewLine + "ITS ID 확인 후 다시 LOT 등록 하셔야 합니다!";
                COM_.ViewWarning(T.Manual, W.DllWarningMessage);
            }
            return "";
        }
    }
}