using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

public class FILE_ : DATA_
{
    //public delegate void DeleException(string strErr);
    //public event DeleException deleException = null;

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(String section, String key, String val, String filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(String section, String key, String def, StringBuilder retVal, int size, String filePath);

    #region "INTEGER WR/RD"
    /// <summary>
    /// INTEGER VALUE INI 파일로 쓰기
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="Section"></param>
    /// <param name="key"></param>
    /// <param name="Value"></param>
    public static void WRInt(String FileName, String Section, String key, int Value) { WritePrivateProfileString(Section, key, Value.ToString("0"), FileName); }
    /// <summary>
    /// INTEGER VALUE INI 파일에서 읽기
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="Section"></param>
    /// <param name="key"></param>
    /// <param name="Value"></param>
    /// <returns></returns>
    public static int RDInt(String FileName, String Section, String key, int Value){
        StringBuilder sb = new StringBuilder(255);
        GetPrivateProfileString(Section, key, "", sb, sb.Capacity, FileName);
        if (sb.Length <= 0) return Value;
        return int.Parse(sb.ToString());
    }
    #endregion "INTEGER WR/RD"

    #region "DOUBLE WR/RD"
    /// <summary>
    /// DOUBLE VALUE INI 파일로 쓰기
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="Section"></param>
    /// <param name="key"></param>
    /// <param name="Value"></param>
    public static void WRDouble(String FileName, String Section, String key, double Value) { WritePrivateProfileString(Section, key, Value.ToString("0.000000"), FileName); }
    /// <summary>
    /// DOUBLE VALUE INI 파일에서 읽기
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="Section"></param>
    /// <param name="key"></param>
    /// <param name="Value"></param>
    /// <returns></returns>
    public static double RDDouble(String FileName, String Section, String key, double Value){
        StringBuilder sb = new StringBuilder(255);
        GetPrivateProfileString(Section, key, "", sb, 255, FileName);
        if (double.TryParse(sb.ToString(), out double dValue)) dValue = double.Parse(sb.ToString());
        return dValue;
    }
    #endregion "DOUBLE WR/RD"

    #region "STRING WR/RD"
    /// <summary>
    /// STRING VALUE INI 파일로 쓰기
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="Section"></param>
    /// <param name="key"></param>
    /// <param name="Value"></param>
    public static void WRString(String FileName, String Section, String key, String Value) { WritePrivateProfileString(Section, key, Value, FileName); }
    /// <summary>
    /// STRING VALUE INI 파일에서 읽기
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="Section"></param>
    /// <param name="key"></param>
    /// <param name="Value"></param>
    /// <returns></returns>
    public static String RDString(String FileName, String Section, String key, String Value){
        StringBuilder sb = new StringBuilder(255);
        GetPrivateProfileString(Section, key, "", sb, 255, FileName);

        string sValue;
        sValue = sb.ToString();
        return sValue;
    }
    #endregion "STRING WR/RD"

    #region "BOOL WR/RD"
    /// <summary>
    /// BOOL VALUE INI 파일에 쓰기
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="Section"></param>
    /// <param name="key"></param>
    /// <param name="Value"></param>
    public static void WRBool(String FileName, String Section, String key, bool Value){
        int i = Value ? 1 : 0;
        WritePrivateProfileString(Section, key, i.ToString(), FileName);
    }
    /// <summary>
    /// BOOL VALUE INI 파일에서 읽기
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="Section"></param>
    /// <param name="key"></param>
    /// <param name="Value"></param>
    /// <returns></returns>
    public static bool RDBool(String FileName, String Section, String key, bool Value){
        StringBuilder sb = new StringBuilder(255);
        GetPrivateProfileString(Section, key, "", sb, 255, FileName);

        if (int.TryParse(sb.ToString(), out int iValue)) iValue = int.Parse(sb.ToString());
        return iValue == 1 ? true : false;
    }
    #endregion "BOOL WR/RD"

    #region "WRITE FILE"
    public static void WR_File(string path, string s, bool appeded){
        //try{
        //    if (appeded)    { File.AppendAllText(path, s, Encoding.Default); }  // 글씨 깨짐 발생??
        //    else            { File.WriteAllText(path, s); }
        //}
        //catch (Exception e) { LogWR_.SaveLogException("WR FILE FAIL [" + path + "]", e); }

        int fm = (int)FileMode.Create;          // 파일 만듬. 같은 이름 파일이 있으면 이전 파일 지우고 만듬.
        if (appeded) fm = (int)FileMode.Append; // 추가모드로 OPEN. 파일 없으면 만듬.
        try
        {
            FileStream fs = new FileStream(path, (FileMode)fm);
            StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
            sw.Write(s);
            sw.Flush();
            sw.Close();
        }
        catch (Exception e) { LogWR_.SaveLogException("WR FILE FAIL [" + path + "]", e); }

    }
    public static void WRL_File(string path, string s, bool appeded){
        int fm = (int)FileMode.Create;          // 파일 만듬. 같은 이름 파일이 있으면 이전 파일 지우고 만듬.
        if (appeded) fm = (int)FileMode.Append; // 추가모드로 OPEN. 파일 없으면 만듬.
        try{
            FileStream fs   = new FileStream(path, (FileMode)fm);
            StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
            sw.WriteLine(s);
            sw.Flush();
            sw.Close();
        }
        catch (Exception e) { LogWR_.SaveLogException("WR FILE FAIL [" + path + "]", e); }
    }

    public static void WR_ASCLL_FILE(string path, string msg){
        try{
            StreamWriter sw = new StreamWriter(path, false, Encoding.ASCII);
            sw.Write(msg);
            sw.Close();
            sw.Dispose();
        }
        catch (Exception ex) { LogWR_.SaveLogException("WR FILE FAIL [" + path + "]", ex); }
    }
    #endregion "WRITE FILE"
}
public class ETC
{
    public const char CrLf      = '\n'; //다음줄
    public const char cspCr     = '\r';
    public const char cspTab    = '\t';
    //public const char Next      = '\r\n';
    public const string NewLine = Microsoft.VisualBasic.Constants.vbNewLine;

    public const byte STX   = 0x02;
    public const byte ETX   = 0x03;
    public const byte ENQ   = 0x05;
    public const byte ACK   = 0x06;
    public const byte LF    = 0x0A;
    public const byte CR    = 0x0D;
    public const byte NAK   = 0x15;
}