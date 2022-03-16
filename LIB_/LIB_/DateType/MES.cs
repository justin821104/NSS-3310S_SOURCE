using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace LIB_.DateType
{
    public class CMES
    {
        public static int m_nControlState           = ControlValue.CONTROL_EQ_OFFLINE;
        public static int m_nPrevControlState       = ControlValue.CONTROL_EQ_OFFLINE;

        public static int m_nEqpState               = EquipmentValue.EQUIPMENT_IDLE;
        public static int m_nPrevEqpState           = EquipmentValue.EQUIPMENT_IDLE;
        
        public static bool m_bConnected             = Connection.bDISCONNECTION;
        public static short m_nCommunication        = Communication.nCom;

        public static string sConnectState          = "NOT CONNECTED";
        public static string sCommState             = "NOT COMMUNICATING";
        public static string sControlState          = "OFFLINE";

        public static Color cConnectState           = SystemColors.Control;
        public static Color cCommState              = SystemColors.Control;
        public static Color cControlState           = SystemColors.Control;

        public static string m_strIP                = "127.0.0.1";
        public static string m_strMODE              = "PASSIVE";
        public static uint m_nPort                  = 5000;
        public static uint m_nDeviceID              = 0;
        public static uint m_nModeSelect            = 1;
        public static uint m_nLinkInterval          = 30;
        public static uint m_nRetry                 = 0;
        public static uint m_nT3                    = 30;
        public static uint m_nT5                    = 30;
        public static uint m_nT6                    = 30;
        public static uint m_nT7                    = 30;
        public static uint m_nT8                    = 30;
        public static uint m_nCTTime                = 0;    //ConversationTimeout
        public static uint m_nCommReqeustTimeout    = 5;
        public static uint m_nFormatTime            = 0;
        public static uint m_EstablishTimeout       = 30;

        public static string m_strModelName         = ""; //MDLN (모델)
        public static string m_strSoftRev           = "V1.0.0"; //버전
        
        public int m_nLastAlarmNo                   = 0;
        public int m_nLastAlarmClrNo                = 0;

        public static string PPID                   = string.Empty;
        public static string PPID_SAW_GROUP         = string.Empty;
        public static string PPID_SAW_DEVICE        = string.Empty;
        public static string PPID_SORTER_GROUP      = string.Empty;
        public static string PPID_SORTER_DEVICE     = string.Empty;
        public static string PPID_VISION_DEVICE     = string.Empty;

        public static int MES_FIRST;
        public static string MES_LOT_ID             = string.Empty;
        public static string MES_MASTER_ID          = string.Empty;
        public static string MES_RECIPE_ID          = string.Empty;
        public static string MES_JIG_ID             = string.Empty;
        public static int MES_COUNT;
        public static int MES_MGZ_COUNT;
        public static string MES_MGZ_ID             = string.Empty;
        public static string MES_FRIST_LOT          = string.Empty;
        public static bool MES_WIP_CHECK            = true;
        public static string SAW_GROUP              = string.Empty;
        public static string SAW_RECIPE             = string.Empty;

        public static string CurPPID                = string.Empty;
        public static string CUR_GROUP              = string.Empty;
        public static string CUR_DEVICE             = string.Empty;
        public static string CUR_VISION             = string.Empty;
        public static string CUR_SAW                = string.Empty;

        public static string SELECT_PPID            = string.Empty;
        public static string SELECT_GROUP           = string.Empty;
        public static string SELECT_DEVICE          = string.Empty;
        public static string SELECT_VISION          = string.Empty;
        public static string SELECT_SAW             = string.Empty;

        public static string[] PPID_LIST;
        public static eRTN_MES RETURN_MES           = eRTN_MES.NULL;    // MES 응답 결과
        public static string RETURN_RECIPE          = string.Empty;     // MES에서 받은 RECIPE (PPID)
        public static bool MESReq                   = false;            // MES 응답 플러그

        public const int eER                        = 2700;

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(    // GetIniValue 를 위해
            String section,
            String key,
            String def,
            StringBuilder retVal,
            int size,
            String filePath);

        public static String GetIniValue(String Section, String Key, String iniPath){
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", temp, 255, iniPath);
            return temp.ToString();
        }
        public static void INI2VAL(string ss, string s, ref uint val){
            string strValue = GetIniValue(ss, s, PATH_.PROJECT + "GEM.INI");
            val = uint.Parse(strValue);
        }
        
        [Flags]
        public enum eRTN_MES
        {
            NULL = -1,
            FAIL = 0,
            SUCESS,
            ACCEPTED, //응답

            NOT_RECIPE,

            DISPATCH,
            DISPATCH_1,
            DISPATCH_2,

            TimeOver = 100
        }

        public struct ControlValue
        {
            public const short CONTROL_UNKOWN           = 0;
            public const short CONTROL_EQ_OFFLINE       = 1;
            public const short CONTROL_ATTEMPT_ONLINE   = 2;
            public const short CONTROL_HOST_OFFLINE     = 3;
            public const short CONTROL_LOCAL            = 4;
            public const short CONTROL_REMOTE           = 5;
            public const short RESERVED                 = 6;
        }
        public struct EquipmentValue
        {
            public const short EQUIPMENT_INIT           = 1;
            public const short EQUIPMENT_IDLE           = 2;
            public const short EQUIPMENT_SETUP          = 3;
            public const short EQUIPMENT_READY          = 4;
            public const short EQUIPMENT_RUN            = 5;
            public const short EQUIPMENT_DOWN           = 6;
            public const short EQUIPMENT_MANUAL         = 7;
            public const short RESERVED                 = 8;
        }
        public struct Comm
        {
            public const int COMM_DISABLE               = 1;
            public const int COMM_ENABLE_NOT_COMM       = 2;
            public const int COMM_ENABLE_CUMM           = 3;
        }
        public struct Communication
        {
            public const short nNotCom                  = 0;
            public const short nCom                     = 1;
        }
        public struct Connection
        {
            public const bool bDISCONNECTION            = false;
            public const bool bCONNECTION               = true;

            public const string DISCONNECTION_STRING    = "DISCONNECT";
            public const string CONNECTION_STRING       = "CONNECT";
        }

        public struct ModuleID{
            public const int IN_LET     = 1;
            public const int STRIP_PK   = 2;
            public const int SAW_STAGE  = 3;
            public const int UNIT_PK    = 4;
            public const int MB_1       = 5;
            public const int MB_2       = 6;
            public const int Etc        = 7;
        }
    }

    public class CSVID
    {
        public const int CONTROL_STATE                  = 10002;    //U2    
        public const int EQUIPMENT_STATE                = 10003;    //U2    

        public const int USER_ID                        = 10031;    //A[20] 
        public const int PROCESS_USER_ID                = 10032;    //A[20] LOT 예약 작업자 사번

        public const int MODULE_ID                      = 10034;    //A[20] Module ID
        public const int MODULE_NAME                    = 10035;    //A     Module Name

        public const int LOT_ID                         = 10040;    //A     Lot ID
        public const int LOT_LIST                       = 10041;    //L     Lot List

        public const int PANEL_INDEX                    = 10053;    //U2    In,Out Panel Index
        public const int PANEL_ID                       = 10054;    //A[20] Panel ID

        public const int RECIPE_ID                      = 13001;    //A     RECIPE ID
        public const int PP_ERROR                       = 13002;    //A
        public const int PP_ERROR_DATA                  = 13003;    //L

        public const int RESERVE_PANEL_QTY              = 13044;    //U2    예약 수량
        public const int COMPLETE_PANEL_QTY             = 13045;    //U2    완료 수량

        public const int PARAMETER_LIST                 = 13061;    //L     추가입력 항목

        public const int EQP_CHANGE_REASON              = 13071;    //A[20] 설비호기저정변경 원인명
        public const int EQP_CHANGE_COMMENT             = 13072;    //A[20] 설비호기지정변경 사유
        public const int EQP_CHANGE_CODE                = 13073;    //U2    

        public const int LOT_TYPE                       = 13077;    //U2    1=초도, 2=본랏, 3=더미, 4=재초도, 5=재작업

        public const int START_TIME                     = 14001;    //A[16] 가동 Loss기간 시작 (TimeFormat = YYYYMMDDhhmmsscc)
        public const int END_TIME                       = 14002;    //A[16] 가동 Loss기간 종료 (TimeFormat = YYYYMMDDhhmmsscc)
        public const int OPERATION_LOSS_COMMENT         = 14004;    //A[30] 가동 Loss 특이사항
        public const int OPERATION_LOSS_CODE            = 14005;    //A     가동 Loss 코드

        public const int CUR_LEFT_BLADE                 = 15001;    //A     좌:교체 후 장착된 BLADE
        public const int OLD_LEFT_BLADE                 = 15002;    //A     좌:탈착된 BALDE

        public const int CUR_RIGHT_BLADE                = 15011;    //A     우:교체 후 장착된 BLADE
        public const int OLD_RIGHT_BLADE                = 15012;    //A     우:탈착된 BALDE
        public const int JIG                            = 15013;    //A     DICING 테이블


        //FDC 정의

        //HANDLER
        public const int ULD_CONV_RADY                      = 31001;
        public const int ULD_CONV_LOADING                   = 31002;
        public const int ULD_CONV_END                       = 31003;
        public const int MAP_BLOCK1_VAC                     = 31004;
        public const int MAP_BLOCK2_VAC                     = 31005;
        public const int DRIVER_AIR                         = 31006;
        public const int BLOW_AIR                           = 31007;
        public const int STAGE_AIR                          = 31008;
        public const int PICKER_AIR                         = 31009;
        public const int HD1_PK1_VAC                        = 31010;
        public const int HD1_PK2_VAC                        = 31011;
        public const int HD1_PK3_VAC                        = 31012;
        public const int HD1_PK4_VAC                        = 31013;
        public const int HD1_PK5_VAC                        = 31014;
        public const int HD1_PK6_VAC                        = 31015;
        public const int HD2_PK1_VAC                        = 31016;
        public const int HD2_PK2_VAC                        = 31017;
        public const int HD2_PK3_VAC                        = 31018;
        public const int HD2_PK4_VAC                        = 31019;
        public const int HD2_PK5_VAC                        = 31020;
        public const int HD2_PK6_VAC                        = 31021;

        public const int MGZ_SLOT_CNT                       = 31100;
        public const int MGZ_SLOT_PITCH                     = 31101;
        public const int UNIT_SIZE_X                        = 31102;
        public const int UNIT_SIZE_Y                        = 31103;
        public const int UNIT_THICKNES                      = 31104;
        public const int TRAY_CNT_X                         = 31105;
        public const int TRAY_CNT_Y                         = 31106;
        public const int TRAY_PITCH_X                       = 31107;
        public const int TRAY_PITCH_Y                       = 31108;
        public const int MAP_BLOCK_CNT_X                    = 31109;
        public const int MAP_BLOCK_CNT_Y                    = 31110;
        public const int MAP_BLOCK_PITCH_X                  = 31111;
        public const int MAP_BLOCK_PITCH_Y                  = 31112;
        public const int MAP_BLOCK_GROUP_CNT_X              = 31113;
        public const int MAP_BLOCK_GROUP_CNT_Y              = 31114;
        public const int MAP_BLOCK_GROUP_PITCH_X            = 31115;
        public const int MAP_BLOCK_GROUP_PITCH_Y            = 31116;
        public const int MAP_BLOCK_BLOW_PICKUP_CNT          = 31117;
        public const int MAP_BLOCK_VAC_ON_PICKUP            = 31118;
        public const int UNIT_INSPECTION_NG_COUNT           = 31119;
        public const int SELECT_MAP_BLOCK                   = 31120;
        public const int SELECT_HEAD                        = 31121;
        public const int USE_HD1_PK1                        = 31122;
        public const int USE_HD1_PK2                        = 31123;
        public const int USE_HD1_PK3                        = 31124;
        public const int USE_HD1_PK4                        = 31125;
        public const int USE_HD1_PK5                        = 31126;
        public const int USE_HD1_PK6                        = 31127;
        public const int USE_HD2_PK1                        = 31128;
        public const int USE_HD2_PK2                        = 31129;
        public const int USE_HD2_PK3                        = 31130;
        public const int USE_HD2_PK4                        = 31131;
        public const int USE_HD2_PK5                        = 31132;
        public const int USE_HD2_PK6                        = 31133;
        public const int SELECT_TRAY_UNLOADER_MODE          = 31134;
        public const int SELECT_TRAY_STACKER_MODE           = 31135;
        public const int SELECT_TRAY_CONV_MODE              = 31136;
        public const int MAP_BLOCK1_TOP_CAM_FIRST_POS_X     = 31137;
        public const int MAP_BLOCK1_TOP_CAM_FIRST_POS_Y     = 31138;
        public const int MAP_BLOCK2_TOP_CAM_FIRST_POS_X     = 31139;
        public const int MAP_BLOCK2_TOP_CAM_FIRST_POS_Y     = 31140;
        public const int MAP_BLOCK1_HEAD_CAM_FIRST_POS_X    = 31141;
        public const int MAP_BLOCK1_HEAD_CAM_FIRST_POS_Y    = 31142;
        public const int MAP_BLOCK2_HEAD_CAM_FIRST_POS_X    = 31143;
        public const int MAP_BLOCK2_HEAD_CAM_FIRST_POS_Y    = 31144;
        public const int GOOD_TRAY1_FIRST_POS_X             = 31145;
        public const int GOOD_TRAY1_FIRST_POS_Y             = 31146;
        public const int GOOD_TRAY2_FIRST_POS_X             = 31147;
        public const int GOOD_TRAY2_FIRST_POS_Y             = 31148;
        public const int REWORK_TRAY_FIRST_POS_X            = 31149;
        public const int REWORK_TRAY_FIRST_POS_Y            = 31150;

    }

    public class CCEID
    {
        //시스템 바이트를 저장할 전역 변수 선언
        public static double m_nLotStart_System         = 0;
        public static double m_nLotEnd_System           = 0;
        public static double m_nPPSelected              = 0;

        //state event
        public const int CONTROL_STATE_OFFLINE          = 111;  //Offline으로 변경
        public const int CONTROL_STATE_ONLINE_LOCAL     = 112;  //Online-Local로 변경
        public const int CONTROL_STATE_ONLINE_REMOTE    = 113;  //Online-Remote로 변경

        public const int EQUIPMENT_STATE_MANUAL         = 115;  //Manual로 변경 -> IDLE로 대체
        public const int EQUIPMENT_STATE_INIT           = 116;  //Init으로 변경 -> IDLE로 대체
        public const int EQUIPMENT_STATE_IDLE           = 117;  //Idle으로 변경
        public const int EQUIPMENT_STATE_SETUP          = 118;  //Setup으로 변경 -> IDLE로 대체
        public const int EQUIPMENT_STATE_READY          = 119;  //Ready으로 변경 -> IDLE로 대체
        public const int EQUIPMENT_STATE_RUN            = 120;  //Run으로 변경
        public const int EQUIPMENT_STATE_DOWN           = 121;  //Down으로 변경 -> IDLE로 대체
        public const int EQUIPMENT_STATE_PM             = 122;  //PM으로 변경 -> IDLE로 대체

        //tracking event
        public const int LOT_REQUEST                    = 601;  //LOT CARD READING 시 보고 (RECIPE VALIDATION)
        public const int LOT_PAUSE                      = 602;  //LOT PAUSE (AFTER LOT_PAUSE)
        public const int LOT_STARTED                    = 603;  //LOT STARTED (AFTER LOT_START)
        public const int LOT_RESUME                     = 604;  //LOT RESUME
        public const int LOT_CREATED                    = 605;
        public const int LOT_CREATED_FAIL               = 606;

        public const int LOT_LOADING                    = 301;  //LOT START 시 보고
        public const int LOT_COMPLETE                   = 302;  //LOT END 시 보고 
        public const int LOT_ABORT                      = 320;  //LOT ABORT 시 보고
        public const int LOT_CANCELED                   = 331;

        //equipment event
        public const int PANEL_LINE_IN                  = 306;  //Equipment에 대한 Event, 매 제품이 본체에 투입될 때
        public const int PANEL_LINE_OUT                 = 307;  //Equipment에 대한 Event, 매 제품이 본체에 배출될 때
        public const int PANEL_MODULE_IN                = 308;  //Equipment에 대한 Event, 매 제품이 Module에 투입될 때
        public const int PANEL_MODULE_OUT               = 309;  //Equipment에 대한 Event, 매 제품이 Module에서 배출될 때

        //
        public const int LOT_START_REQUEST              = 310;  //LOT START 요청
        public const int PARA_INPUT_COMPLETE            = 311;  //파라미터 입력 완료 할때
        public const int LOT_EQP_CHANGE_COMPLETE        = 312;  //설비 호기 변경 완료
        public const int LOT_MODIFIED                   = 313;  //대기열 수량 변경
        public const int PP_SELECTED                    = 314;  //PPSelect 완료
        public const int LOT_EQP_LOSS_COMPLETE          = 315;  //가동 LOSS 입력
        public const int LOT_LIST_MODIFIED              = 316;  //대기열 LOT 순서 변경

        //Dry Film Change
        public const int UPPER_DF_KITTING               = 501;  //상단 BLADE 장착
        public const int LOWER_DF_KITTING               = 502;  //하단 BLADE 장착

        public const int POP_CONDITION_COMPLETE         = 332;  //Pop Condition 확인 완료
    }

    public class CECID
    {
        public const int ECID_PORT_ID                   = 50001;
        public const int ECID_DEVICEID                  = 50002;
        public const int ECID_T3                        = 50003;
        public const int ECID_T5                        = 50004;
        public const int ECID_T6                        = 50005;
        public const int ECID_T7                        = 50006;
        public const int ECID_T8                        = 50007;
        public const int ECID_LINKTEST                  = 50008;
        public const int ECID_RETRY                     = 50009;

        public const int ECID_ESTABLISH_TIMEOUT         = 50011;
        public const int ECID_TIME_FORMAT               = 50012;
        public const int ECID_EQUIPMENT_NAME            = 50013;
        
    }

    public class RCMD
    {
        public const string PPSELECT                    = "PP-SELECT";
        //TRACKING
        public const string LOT_CREATE                  = "LOT_CREATE";     //HOST에서 LOT ID 전송
        public const string POP_CONDITION               = "POP_CONDITION";
        public const string LOT_INFO                    = "LOT_INFO";       //LOT 정보 전달
        public const string LOT_START                   = "LOT_START";      //설비 START
        public const string LOT_CANCEL                  = "LOT_CANCEL";     //대기열 LOT 삭제
        public const string LOT_PAUSE                   = "LOT_PAUSE";      //설비 투입기 일시 정지
        public const string LOT_RESUME                  = "LOT_RESUME";     //설비 투입기 동작

        public const string LOT_EQP_CHANGE              = "LOT_EQP_CHANGE"; //설비호기지정변경
        public const string LOT_EQP_LOSS                = "LOT_EQP_LOSS";   //가동 LOSS 입력
        public const string PARAMETER_INFO              = "PARAMETER_INFO"; //필수 입력 파라미터 입력
    }

    public class CPNAME
    {
        public const string PPID                        = "PPID";
        public const string PORTID                      = "PORTID";
        public const string CARRIERID                   = "CARRIERID";
        public const string WAFERMAP                    = "WAFERMAP";

        public const string LOSS_START_TIME             = "START_TIME";
        public const string LOSS_END_TIME               = "END_TIME";

        public const string LOTID                       = "LOTID";                  //LOT ID
        public const string QTY                         = "QTY";                    //STRIP or QUAD 수량
        public const string LOTTYPE                     = "LOTTYPE";                //1=초도,2=본낫,3=재초도,4=재작업
        public const string PRODUCTTYPE                 = "PRODUCTTYPE";            //STRIP or QUAD
        public const string TOOLNO                      = "TOOLNO";                 //TOOL NO
        public const string ITS                         = "ITS";                    //ITS 진행 여부 : 0 진행, 1 미진행
        public const string ITSLOTID_IN                 = "ITSLOTID_IN";            //ITS LOTID 내부
        public const string ITSLOTID_CT                 = "ITSLOTID_CT";            //ITS LOTID 고객
        public const string UNITSIZEX                   = "UNITSIZEX";              //유닛 SIZE X
        public const string UNITSIZEY                   = "UNITSIZEY";              //유닛 SIZE Y
        public const string UNITSIZE_UPPER              = "UNITSIZE_UPPER";
        public const string UNITSIZE_LOWER              = "UNITSIZE_LOWER";
        public const string THICK                       = "THICK";                  //두께
        public const string THICK_UPPER                 = "THICK_UPPER";
        public const string THICK_LOWER                 = "THICK_LOWER";
        public const string ABFMATERIAL                 = "ABFMATERIAL";             //ABF 자재
        public const string LANDPKGX                    = "LANDPKGX";
        public const string LANDPKGX_UPPER              = "LANDPKGX_UPPER";
        public const string LANDPKGX_LOWER              = "LANDPKGX_LOWER";
        public const string LANDPKGY                    = "LANDPKGY";
        public const string LANDPKGY_UPPER              = "LANDPKGY_UPPER";
        public const string LANDPKGY_LOWER              = "LANDPKGY_LOWER";

        public const string PROCCD                      = "PROCCD";
        public const string PROCNAME                    = "PROCNAME";
        public const string WORKCONDITION               = "WORKCONDITION";
        public const string PROCESSCONDITION_1          = "LOT ID#공정명#이벤트명#메세지 타입#작업지침#팝업여부#FILE_ATTACH#생성자#생성자명#생성 일자#등록자#등록자명#수정일자#FILE_GRP_ID";
        public const string PROCESSCONDITION_2          = "LOT ID#작업/액티비티 번호#공정명#시작/종료층#이벤트명#보류 코드#보류명#특이사항#생성자#생성자명#생성 일자";
        public const string PROCESSCONDITION_3          = "공정순서#공정코드#공정명#Hole To Hole X#공차 -#공차 +#Hole To Hole Y#공차 -#공차 +";

        public const string LOT_EQP_LOSSREASON_         = "LOT_EQP_LOSSREASON_";    //LOSSREASON NAME (n개)
        public const string PARAMETER_                  = "PARAMETER_";             //PARAMETER NAME (n개)
    }
}