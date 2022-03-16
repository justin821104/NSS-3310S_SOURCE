using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LIB_.DateType
{
    public class CUSER
    {
        public static List<CUSER_INFO> List = new List<CUSER_INFO>();
        public static CUSER_INFO Current    = new CUSER_INFO();

        public static void READ(){
            string sPath = PATH_.UserID;
            if (!File.Exists(sPath)) return;
            
            string[] sLine = File.ReadAllText(sPath, Encoding.Default).Split(ETC.CrLf);
            for (int i = 0; i < sLine.Length - 1; i++){
                sLine[i]        = sLine[i].Replace("\n", "");
                sLine[i]        = sLine[i].Replace("\r", "");
                string[] sRslt  = sLine[i].Split(',');

                List.Add(new CUSER_INFO
                {
                    ID          = sRslt[0],
                    Name        = sRslt[1],
                    Password    = sRslt[2]
                });
            }
        }

        public static bool WRITR(){
            string sPath    = PATH_.UserID;
            string s        = string.Empty;
            for (int i = 0; i < List.Count; i++){
                s += List[i].ID + "," + List[i].Name + "," + List[i].Password + ETC.NewLine;
            }

            if (File.Exists(sPath)){
                File.Delete(sPath);
                UTIL_.DELAY(100);
            }
            FILE_.WR_File(sPath, s, true);
            return true;
        }

        public static bool READ_CURRENT_USER(){
            if (!File.Exists(PATH_.CurrUSER)) return false;
            string[] sLine = File.ReadAllText(PATH_.CurrUSER).Split(ETC.CrLf);
            try{
                if (sLine == null || sLine.Length <= 1) return false;
                for (int i = 0; i < sLine.Length; i++){
                    sLine[i] = sLine[i].Replace("\r", "");
                }
                Current.ID      = sLine[0];
                Current.Name    = sLine[1];
                return true;
            }
            catch (Exception e){
                LogWR_.SaveLogException("READ CURRENT USER INFO", e);
                Current.ID      = "";
                Current.Name    = "";
                return false;
            }
            
        }
        public static void WRITE_CURRENT_USER(){
            string sPaht = PATH_.CurrUSER;
            if (File.Exists(sPaht)){
                File.Delete(sPaht);
                UTIL_.DELAY(100);
            }//파일 존해하여 삭제 후 다시 생성함.(모디파이 할 경우만 적용함)
            string CurUserID = Current.ID + ETC.NewLine + Current.Name;
            FILE_.WR_File(sPaht, CurUserID, true);
        }
    }

    public class CUSER_INFO
    {
        public string ID        { get; set; }
        public string Name      { get; set; }
        public string Password  { get; set; }
    }
}