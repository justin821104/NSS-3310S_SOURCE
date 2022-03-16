using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ezGEM;
using System.IO;

public class DATA_ {
    public const string Version                     = "SECS-GEM_0127.22.64";
    public const string COMPANY                     = "NEONTECH";
    public const string MACHINE_NAME                = "NSS-3310S";

    public static string LOCATION                   = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\";
    public const string STANDARD                    = "D:\\WORK\\OUTPUT\\" + COMPANY + "\\";
    public const string PROJECT                     = STANDARD + MACHINE_NAME + "\\";
    public const string LOG                         = PROJECT + "LOG\\";                    // 로그 저장 폴더
    public const string LogMES                      = LOG + "MES\\";

    //MES DIFNE
    public static int m_nControlState               = ControlValue.CONTROL_EQ_OFFLINE;
    public static int m_nPrevControlState           = ControlValue.CONTROL_EQ_OFFLINE;
    public static int m_nEqpState                   = EquipmentValue.EQUIPMENT_IDLE;
    public static int m_nPrevEqpState               = EquipmentValue.EQUIPMENT_IDLE;
    public static bool m_bConnected                 = Connection.bDISCONNECTION;
    public static short m_nCommunication            = Communication.nCom;

    public static string sConnectState              = "NOT CONNECTED";
    public static string sCommState                 = "NOT COMMUNICATING";
    public static string sControlState              = "OFFLINE";

    public static Color cConnectState               = SystemColors.Control;
    public static Color cCommState                  = SystemColors.Control;
    public static Color cControlState               = SystemColors.Control;

    public static string CurUserID                  = "";
    public static int CurLotCnt                     = 0;
    public static int CurLotType                    = 0;

    public static sLOT_INFO GET_LOT                 = new sLOT_INFO(); //MES에서 받은 LOT 정보

    public static bool bLotLoss                     = false;
    public static string LotLossStartTime           = "";
    public static string LotLossEndTime             = "";
    public static bool bEqpChange                   = false;

    public static string EQPCode                    = "NSS2"; //EQUIPMENT CODE 
    public static string CurRecipe                  = "";
}

public struct sLOT_INFO {
    public string LotID;                //LOT ID
    public int LotType;                 //LOT TYPE (1=초도, 2=본낫, 3=더미, 4=재초도, 5=재작업)
    public int QTY;                     //수량 (STRIP or QUAD)
    public string ProductType;          //PRODUCT TYPE (STRIP or QUAD)
    public string ToolNo;               //TOOL NO
    public int ITS;                     //ITS 진행 여부 : 0 진행, 1 미진행
    public string ItsLotID_In;          //ITS LOT ID 내부
    public string ItsLotID_CT;          //ITS LOT ID 고객
    public double UnitSizeX;            //유닛 SIZE X
    public double UnitSizeY;            //유닛 SIZE Y
    public double THICK;                //두께
    public string ABFMATERIAL;          //ABF 자재

    public string ProcCD;               //
    public string ProcName;
    public string WorkCondition;
    public string ProcCondition_1;
    public string ProcCondition_2;
    public string ProcCondition_3;
    public string ProcCondition_4;

}