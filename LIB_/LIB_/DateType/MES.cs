using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace LIB_.DateType
{
    public class CMES
    {
        public static bool bIni = false;    // gem 초기화 진행 여부

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
        public static string[] Name     = new string[40000];
        public static string[] Value    = new string[40000];
        public static string[] Type     = new string[40000];

        public static int[] FDC_NUM      = { Sp1Current, Sp2Current, Sp1WaterCooling, Sp2WaterCooling, Sp1WaterJet, Sp2WaterJet, Sp1DicingWater, Sp2DicingWater, Sp1WaterShower, Sp2WaterShower, Ch1CuttingSpeed, Ch1CuttingHeight, Ch1Sp1CuttingRPM, Ch1Sp2CuttingRPM, Ch2CuttingSpeed, Ch2CuttingHeight, Ch2Sp1CuttingRPM, Ch2Sp2CuttingRPM,
                                            HD1_PK1_VAC, HD1_PK2_VAC, HD1_PK3_VAC, HD1_PK4_VAC, HD1_PK5_VAC, HD1_PK6_VAC, HD2_PK1_VAC, HD2_PK2_VAC, HD2_PK3_VAC, HD2_PK4_VAC, HD2_PK5_VAC, HD2_PK6_VAC
        };

        public static int[] VID_NUM = { CONTROL_STATE, EQUIPMENT_STATE, USER_ID, PROCESS_USER_ID, MODULE_ID, MODULE_NAME, LOT_ID, PANEL_INDEX, PANEL_ID, RECIPE_ID, RESERVE_PANEL_QTY, COMPLETE_PANEL_QTY, LOT_TYPE, START_TIME, END_TIME, CUR_LEFT_BLADE, CUR_RIGHT_BLADE,
                                        Sp1RPM, Sp2RPM, Sp1BladeAvailableLength, Sp2BladeAvailableLength, Sp1BladeStandard, Sp2BladeStandard, SawStageVac, Spindle1RPM, Spindle2RPM, RubberThickness, Ch1StartMargin, Ch2StartMargin, Ch1EndMargin, Ch2EndMargin, ProductThickness,
                                        WaterOnDelay, SawingWaterOnDelay, SawingContactCondition, SawingContactAxisSetting, SawingContactConditionSetting, SawingContactSp1Length, SawingContactSp2Length, SawingContactSp1Line, SawingContactSp2Line, SawingContactSp1Ea, SawingContactSp2Ea,
                                        Sp1BladeAmountOfUseError, Sp2BladeAmountOfUseError, Sp1BladeAmountOfUseErrorPercent, Sp2BladeAmountOfUseErrorPercent, Sp1BladeAmountOfUseMaxPercent, Sp2BladeAmountOfUseMaxPercent, UseChanelcount, ChanelSkip, CuttingChenalDir, 
                                        Ch1CuttingMode, Ch1StageDir, Ch1CuttingDistance, Ch1CuttingDir, Ch1CuttingCount, Ch1CuttingPitch, Ch1IndexSkipSetting, Ch1StartSkip, Ch1EndSkip, Ch1IndexSkip, 
                                        Ch1AddCutSetting, Ch1AddCutStart, Ch1AddCutEnd, Ch1AlignWay, Ch1AlignPoint, Ch1TAxisAngle, Ch1AlignCwLimit, Ch1AlignCcwLimit, Ch1AlignMarkCuttingOffsetX, Ch1AlignMarkCuttingOffsetY, Ch1IndexCuttingOffsetY,
                                        Ch2CuttingMode, Ch2StageDir, Ch2CuttingDistance, Ch2CuttingDir, Ch2CuttingCount, Ch2CuttingPitch, Ch2IndexSkipSetting, Ch2StartSkip, Ch2EndSkip, Ch2IndexSkip,
                                        Ch2AddCutSetting, Ch2AddCutStart, Ch2AddCutEnd, Ch2AlignWay, Ch2AlignPoint, Ch2TAxisAngle, Ch2AlignCwLimit, Ch2AlignCcwLimit, Ch2AlignMarkCuttingOffsetX, Ch2AlignMarkCuttingOffsetY, Ch2IndexCuttingOffsetY,
                                        StandardAlignCam, AlignMode, AlignPatternCheckMode, CamHighLowSetting, CamBlowTime, AngleCorrection, AlignAngleCheckRange, AlignAnglecheckCount, AlignPatternMatchingRepeatCnt, 
                                        PatternSearchCount, PatternSearchIndexX, PatternSearchIndexY, KerfCheckMode, KerfCheckLengthOfUse, KerfCheckCycle, Sp1BladeAmountOfUse, Sp2BladeAmountOfUse, Sp1BladeCuttingCnt, Sp2BladeCuttingCnt,
                                        ULD_CONV_RADY, ULD_CONV_LOADING, ULD_CONV_END, MAP_BLOCK1_VAC, MAP_BLOCK2_VAC, DRIVER_AIR, BLOW_AIR, STAGE_AIR, PICKER_AIR
        };

        public const int CONTROL_STATE                      = 10002;    //U2    1:OFF-LINE/EQUOPMENT OFF-LINE, 2:OFF-LINE/ATTEMPT ON-LINE, 3:OFF-LINE/HOST OFF-LINE, 4:ON-LINE/LOCAL, 5:ON-LINE/REMOTE 
        public const int EQUIPMENT_STATE                    = 10003;    //U2    1:INIT, 2:ILDE, 3:SETUP, 4:REDAY, 5:RUN, 6:DOWN, 7:MANUAL

        public const int USER_ID                            = 10031;    //A[20] 작업자 사번
        public const int PROCESS_USER_ID                    = 10032;    //A[20] LOT 예약 작업자 사번

        public const int MODULE_ID                          = 10034;    //A[20] Module ID
        public const int MODULE_NAME                        = 10035;    //A     Module Name

        public const int LOT_ID                             = 10040;    //A     Lot ID
        public const int LOT_LIST                           = 10041;    //L     Lot List

        public const int PANEL_INDEX                        = 10053;    //U2    In,Out Panel Index
        public const int PANEL_ID                           = 10054;    //A     Panel ID

        public const int RECIPE_ID                          = 13001;    //A     RECIPE ID
        public const int PP_ERROR                           = 13002;    //A
        public const int PP_ERROR_DATA                      = 13003;    //L

        public const int RESERVE_PANEL_QTY                  = 13044;    //U2    예약 수량
        public const int COMPLETE_PANEL_QTY                 = 13045;    //U2    완료 수량

        public const int PARAMETER_LIST                     = 13061;    //L     추가입력 항목

        public const int EQP_CHANGE_REASON                  = 13071;    //A     설비호기저정변경 원인명
        public const int EQP_CHANGE_COMMENT                 = 13072;    //A     설비호기지정변경 사유
        public const int EQP_CHANGE_CODE                    = 13073;    //U2    

        public const int LOT_TYPE                           = 13077;    //U2    1=초도, 2=본랏, 3=더미, 4=재초도, 5=재작업

        public const int START_TIME                         = 14001;    //A     가동 Loss기간 시작 (TimeFormat = YYYYMMDDhhmmsscc)
        public const int END_TIME                           = 14002;    //A     가동 Loss기간 종료 (TimeFormat = YYYYMMDDhhmmsscc)
        public const int OPERATION_LOSS_COMMENT             = 14004;    //A     가동 Loss 특이사항
        public const int OPERATION_LOSS_CODE                = 14005;    //A     가동 Loss 코드

        public const int CUR_LEFT_BLADE                     = 15001;    //A     좌:교체 후 장착된 BLADE (SP1)
        public const int OLD_LEFT_BLADE                     = 15002;    //A     좌:탈착된 BALDE
        public const int CUR_RIGHT_BLADE                    = 15011;    //A     우:교체 후 장착된 BLADE (SP2)
        public const int OLD_RIGHT_BLADE                    = 15012;    //A     우:탈착된 BALDE
        public const int JIG                                = 15013;    //A     DICING 테이블

        //FDC 정의

        //SAW
        public const int Sp1RPM                             = 30001;    // 스핀들1 RPM
        public const int Sp2RPM                             = 30002;    // 스핀들2 RPM
        public const int Sp1Current                         = 30003;    // 스핀들1 전류값 v
        public const int Sp2Current                         = 30004;    // 스핀들2 전류값 v
        public const int Sp1BladeAvailableLength            = 30005;    // 스핀들1 블레이드 사용 가능 길이
        public const int Sp2BladeAvailableLength            = 30006;    // 스핀들2 블레이드 사용 가능 길이
        public const int Sp1BladeStandard                   = 30007;    // 스핀들1 블레이드 규격
        public const int Sp2BladeStandard                   = 30008;    // 스핀들2 블레이드 규격
        public const int Sp1WaterCooling                    = 30009;    // 스핀들1 워터 쿨링 v
        public const int Sp2WaterCooling                    = 30010;    // 스핀들2 워터 쿨링 v
        public const int Sp1WaterJet                        = 30011;    // 스핀들1 워터젯 v
        public const int Sp2WaterJet                        = 30012;    // 스핀들2 워터젯 v
        public const int Sp1DicingWater                     = 30013;    // 스핀들1 다이싱 워터 v
        public const int Sp2DicingWater                     = 30014;    // 스핀들2 다이싱 워터 v
        public const int Sp1WaterShower                     = 30015;    // 스핀들1 워터 샤워 v
        public const int Sp2WaterShower                     = 30016;    // 스핀들2 워터 샤워 v
        public const int SawStageVac                        = 30017;    // 다이싱 테이블 진공 값

        public const int Spindle1RPM                        = 30100;    // 스핀들1 RPM 허용 오차
        public const int Spindle2RPM                        = 30101;    // 스핀들2 RPM 허용 오차
        public const int RubberThickness                    = 30102;    // 고무 두께
        public const int Ch1StartMargin                     = 30103;    // 1채널 시작 마진
        public const int Ch2StartMargin                     = 30104;    // 2채널 시작 마진
        public const int Ch1EndMargin                       = 30105;    // 1채널 끝 마진
        public const int Ch2EndMargin                       = 30106;    // 2채널 끝 마진
        public const int ProductThickness                   = 30107;    // 제품 두께
        public const int WaterOnDelay                       = 30108;    // 물 공급 딜레이
        public const int SawingWaterOnDelay                 = 30109;    // 가공 중 물 공급 딜레이
        public const int SawingContactCondition             = 30110;    // 가공 중 콘텍 조건
        public const int SawingContactAxisSetting           = 30111;    // 가공 중 콘텍 축 설정
        public const int SawingContactConditionSetting      = 30112;    // 가공 중 콘텍 조건 설정
        public const int SawingContactSp1Length             = 30113;    // 가공 중 컨텍 sp1 Length 설정값
        public const int SawingContactSp2Length             = 30114;    // 가공 중 컨텍 sp2 Length 설정값
        public const int SawingContactSp1Line               = 30115;    // 가공 중 컨텍 sp1 Line 설정값
        public const int SawingContactSp2Line               = 30116;    // 가공 중 컨텍 sp2 Line 설정값
        public const int SawingContactSp1Ea                 = 30117;    // 가공 중 컨텍 sp1 EA 설정값
        public const int SawingContactSp2Ea                 = 30118;    // 가공 중 컨텍 sp2 EA 설정값
        public const int Sp1BladeAmountOfUseError           = 30119;    // 스핀들1 블레이드 마모 에러 활성화
        public const int Sp2BladeAmountOfUseError           = 30120;    // 스핀들2 블레이드 마모 에러 활성화
        public const int Sp1BladeAmountOfUseErrorPercent    = 30121;    // 스핀들1 블레이드 마모 에러 퍼센트
        public const int Sp2BladeAmountOfUseErrorPercent    = 30122;    // 스핀들2 블레이드 마모 에러 퍼센트
        public const int Sp1BladeAmountOfUseMaxPercent      = 30123;    // 스핀들1 블레이드 마모 최대 퍼센트
        public const int Sp2BladeAmountOfUseMaxPercent      = 30124;    // 스핀들2 블레이드 마모 최대 퍼센트
        public const int UseChanelcount                     = 30125;    // 사용 채널 개수
        public const int ChanelSkip                         = 30126;    // 채널 스킵
        public const int CuttingChenalDir                   = 30127;    // 컷팅 채널 방향
        public const int Ch1CuttingMode                     = 30128;    // 1채널 컷팅 모드
        public const int Ch1StageDir                        = 30129;    // 1채널 스테이지 방향
        public const int Ch1CuttingDistance                 = 30130;    // 1채널 컷팅 거리
        public const int Ch1CuttingSpeed                    = 30131;    // 1채널 컷팅 속도 v
        public const int Ch1CuttingDir                      = 30132;    // 1채널 컷팅 방향
        public const int Ch1CuttingCount                    = 30133;    // 1채널 컷팅 가공 개수
        public const int Ch1CuttingPitch                    = 30134;    // 1채널 컷팅 피치
        public const int Ch1CuttingHeight                   = 30135;    // 1채널 높이 v
        public const int Ch1Sp1CuttingRPM                   = 30136;    // 1채널 sp1 컷팅 RPM v
        public const int Ch1Sp2CuttingRPM                   = 30137;    // 1채널 sp2 컷팅 RPM v
        public const int Ch1IndexSkipSetting                = 30138;    // 1채널 인덱스 스킵 설정 유무
        public const int Ch1StartSkip                       = 30139;    // 1채널 시작 스킵
        public const int Ch1EndSkip                         = 30140;    // 1채널 끝 스킵
        public const int Ch1IndexSkip                       = 30141;    // 1채널 인덱스 스킵
        public const int Ch1AddCutSetting                   = 30142;    // 1채널 추가 컷 사용 모드
        public const int Ch1AddCutStart                     = 30143;    // 1채널 추가 컷 시작 값
        public const int Ch1AddCutEnd                       = 30144;    // 1채널 추가 컷 끝 값
        public const int Ch1AlignWay                        = 30145;    // 1채널 얼라인 방식
        public const int Ch1AlignPoint                      = 30146;    // 1채널 얼라인 포인트
        public const int Ch1TAxisAngle                      = 30147;    // 1채널 T축 각도
        public const int Ch1AlignCwLimit                    = 30148;    // 1채널 얼라인 +리미트
        public const int Ch1AlignCcwLimit                   = 30149;    // 1채널 얼라인 -리미트
        public const int Ch1AlignMarkCuttingOffsetX         = 30150;    // 1채널 얼라인 마크 컷 오프셋 X
        public const int Ch1AlignMarkCuttingOffsetY         = 30151;    // 1채널 얼라인 마크 컷 오프셋 Y
        public const int Ch1IndexCuttingOffsetY             = 30152;    // 1채널 인덱스별 컷 옵셋 Y
        public const int Ch2CuttingMode                     = 30153;    // 2채널 컷팅 모드
        public const int Ch2StageDir                        = 30154;    // 2채널 스테이지 방향
        public const int Ch2CuttingDistance                 = 30155;    // 2채널 컷팅 거리
        public const int Ch2CuttingSpeed                    = 30156;    // 2채널 컷팅 속도 v
        public const int Ch2CuttingDir                      = 30157;    // 2채널 컷팅 방향
        public const int Ch2CuttingCount                    = 30158;    // 2채널 컷팅 가공 개수
        public const int Ch2CuttingPitch                    = 30159;    // 2채널 컷팅 피치
        public const int Ch2CuttingHeight                   = 30160;    // 2채널 높이 v
        public const int Ch2Sp1CuttingRPM                   = 30161;    // 2채널 sp1 컷팅 RPM v
        public const int Ch2Sp2CuttingRPM                   = 30162;    // 2채널 sp2 컷팅 RPM v
        public const int Ch2IndexSkipSetting                = 30163;    // 2채널 인덱스 스킵 설정 유무
        public const int Ch2StartSkip                       = 30164;    // 2채널 시작 스킵
        public const int Ch2EndSkip                         = 30165;    // 2채널 끝 스킵
        public const int Ch2IndexSkip                       = 30166;    // 2채널 인덱스 스킵
        public const int Ch2AddCutSetting                   = 30167;    // 2채널 추가 컷 사용 모드
        public const int Ch2AddCutStart                     = 30168;    // 2채널 추가 컷 시작 값
        public const int Ch2AddCutEnd                       = 30169;    // 2채널 추가 컷 끝 값
        public const int Ch2AlignWay                        = 30170;    // 2채널 얼라인 방식
        public const int Ch2AlignPoint                      = 30171;    // 2채널 얼라인 포인트
        public const int Ch2TAxisAngle                      = 30172;    // 2채널 T축 각도
        public const int Ch2AlignCwLimit                    = 30173;    // 2채널 얼라인 +리미트
        public const int Ch2AlignCcwLimit                   = 30174;    // 2채널 얼라인 -리미트
        public const int Ch2AlignMarkCuttingOffsetX         = 30175;    // 2채널 얼라인 마크 컷 오프셋 X
        public const int Ch2AlignMarkCuttingOffsetY         = 30176;    // 2채널 얼라인 마크 컷 오프셋 Y
        public const int Ch2IndexCuttingOffsetY             = 30177;    // 2채널 인덱스별 컷 옵셋 Y
        public const int StandardAlignCam                   = 30178;    // 기준 얼라인 카메라
        public const int AlignMode                          = 30179;    // 얼라인 모드
        public const int AlignPatternCheckMode              = 30180;    // 얼라인 패턴 체크 모드
        public const int CamHighLowSetting                  = 30181;    // 카메라 HIGH, LOW 설정
        public const int CamBlowTime                        = 30182;    // 카메라 블로우 타임
        public const int AngleCorrection                    = 30183;    // 앵글 보정 사용 유무
        public const int AlignAngleCheckRange               = 30184;    // 얼라인 앵글 체크 허용치 값
        public const int AlignAnglecheckCount               = 30185;    // 얼라인 앵글 체크 카운터 값
        public const int AlignPatternMatchingRepeatCnt      = 30186;    // 얼라인 패턴 매칭 반복 회수
        public const int PatternSearchCount                 = 30187;    // 패턴 서치 카운터
        public const int PatternSearchIndexX                = 30188;    // 패턴 서치 인덱스 X
        public const int PatternSearchIndexY                = 30189;    // 패턴 서치 인덱스 Y
        public const int KerfCheckMode                      = 30190;    // 커프 체크 모드 X
        public const int KerfCheckLengthOfUse               = 30191;    // 커프 체크 사용 길이
        public const int KerfCheckCycle                     = 30192;    // 커프 체크 사이클
        public const int Sp1BladeAmountOfUse                = 30193;    // sp1 블레이드 사용량
        public const int Sp2BladeAmountOfUse                = 30194;    // sp2 블레이드 사용량
        public const int Sp1BladeCuttingCnt                 = 30195;    // sp1 블레이드 컷팅 회수
        public const int Sp2BladeCuttingCnt                 = 30196;    // sp2 블레이드 컷팅 회수

        //HANDLER
        public const int ULD_CONV_RADY                      = 31001;    // 언로더 콘베어 투입 허가 비트
        public const int ULD_CONV_LOADING                   = 31002;    // 언로더 콘베어 투입 진행 비트
        public const int ULD_CONV_END                       = 31003;    // 언로더 콘베어 투입 완료 비트
        public const int MAP_BLOCK1_VAC                     = 31004;    // MAP-BLOCK TABLE1 WORK VACUUM
        public const int MAP_BLOCK2_VAC                     = 31005;    // MAP-BLOCK TABLE2 WORK VACUUM
        public const int DRIVER_AIR                         = 31006;    // DRIVER AIR PRESSURE
        public const int BLOW_AIR                           = 31007;    // BLOW AIR PRESSURE
        public const int STAGE_AIR                          = 31008;    // STAGE AIR PRESSURE
        public const int PICKER_AIR                         = 31009;    // PICKER AIR PRESSURE
        public const int HD1_PK1_VAC                        = 31010;    // HEAD1 PICKER1 진공 값 v
        public const int HD1_PK2_VAC                        = 31011;    // HEAD1 PICKER2 진공 값 v
        public const int HD1_PK3_VAC                        = 31012;    // HEAD1 PICKER3 진공 값 v
        public const int HD1_PK4_VAC                        = 31013;    // HEAD1 PICKER4 진공 값 v
        public const int HD1_PK5_VAC                        = 31014;    // HEAD1 PICKER5 진공 값 v
        public const int HD1_PK6_VAC                        = 31015;    // HEAD1 PICKER6 진공 값 v
        public const int HD2_PK1_VAC                        = 31016;    // HEAD2 PICKER1 진공 값 v
        public const int HD2_PK2_VAC                        = 31017;    // HEAD2 PICKER2 진공 값 v
        public const int HD2_PK3_VAC                        = 31018;    // HEAD2 PICKER3 진공 값 v
        public const int HD2_PK4_VAC                        = 31019;    // HEAD2 PICKER4 진공 값 v
        public const int HD2_PK5_VAC                        = 31020;    // HEAD2 PICKER5 진공 값 v
        public const int HD2_PK6_VAC                        = 31021;    // HEAD2 PICKER6 진공 값 v

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
        // VISION 
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
        public const int EQUIPMENT_STATE_DOWN           = 121;  //Down으로 변경  
        public const int EQUIPMENT_STATE_PM             = 122;  //PM으로 변경 -> IDLE로 대체

        //tracking event
        public const int LOT_CREATED                    = 605;  //
        public const int LOT_CREATED_FAIL               = 606;  //
        public const int LOT_REQUEST                    = 601;  //LOT CARD READING 시 보고 (RECIPE VALIDATION)
        public const int LOT_PAUSE                      = 602;  //LOT PAUSE (AFTER LOT_PAUSE)
        public const int LOT_STARTED                    = 603;  //LOT STARTED (AFTER LOT_START)
        public const int LOT_RESUME                     = 604;  //LOT RESUME
        
        public const int LOT_LOADING                    = 301;  //LOT START 시 보고
        public const int LOT_COMPLETE                   = 302;  //LOT END 시 보고 
        public const int LOT_ABORT                      = 320;  //LOT ABORT 시 보고

        //equipment event
        public const int PANEL_LINE_IN                  = 306;  //Equipment에 대한 Event, 매 제품이 본체에 투입될 때
        public const int PANEL_LINE_OUT                 = 307;  //Equipment에 대한 Event, 매 제품이 본체에 배출될 때
        public const int PANEL_MODULE_IN                = 308;  //Equipment에 대한 Event, 매 제품이 Module에 투입될 때
        public const int PANEL_MODULE_OUT               = 309;  //Equipment에 대한 Event, 매 제품이 Module에서 배출될 때


        public const int LOT_CANCELED                   = 331;
        public const int PARA_INPUT_COMPLETE            = 311;  //파라미터 입력 완료 할때
        public const int LOT_EQP_CHANGE_COMPLETE        = 312;  //설비 호기 변경 완료
        public const int LOT_MODIFIED                   = 313;  //대기열 수량 변경
        public const int LOT_LIST_MODIFIED              = 316;  //대기열 LOT 순서 변경
        public const int LOT_START_REQUEST              = 310;  //LOT START 요청
        public const int PP_SELECTED                    = 314;  //PPSelect 완료
        
        //RECIPE 관련 
        public const int RecipeDownloadComplete         = 400;  //Recipe Download 완료
        public const int RecipeDownloadFail             = 401;  //Recipe Download 실패


        public const int LOT_EQP_LOSS_COMPLETE          = 315;  //가동 LOSS 입력

        public const int Upper_DF_Kitting               = 501;  //상단 BLADE 장착
        public const int Lower_DF_Kitting               = 502;  //하단 BLADE 장착

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
        //TRACKING
        public const string LOT_CREATE                  = "LOT_CREATE";     //HOST에서 LOT ID 전송
        public const string PPSELECT                    = "PP-SELECT";      //RECIPE 설비 LOADING
        public const string LOT_INFO                    = "LOT_INFO";       //LOT 정보 전달
        public const string LOT_START                   = "LOT_START";      //설비 START
        public const string LOT_CANCEL                  = "LOT_CANCEL";     //대기열 LOT 삭제
        public const string LOT_PAUSE                   = "LOT_PAUSE";      //설비 투입기 일시 정지
        public const string LOT_RESUME                  = "LOT_RESUME";     //설비 투입기 동작
        //설비호기변경
        public const string LOT_EQP_CHANGE              = "LOT_EQP_CHANGE"; //설비호기지정변경
        //가동LOSS입력
        public const string LOT_EQP_LOSS                = "LOT_EQP_LOSS";   //가동 LOSS 입력
        //파라미터입력
        public const string PARAMETER_INFO              = "PARAMETER_INFO"; //필수 입력 파라미터 입력
        //공정작업조건
        public const string POP_CONDITION               = "POP_CONDITION";  //공정작업조건 표시
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

        //OLD
        public const string LANDPKGX                    = "LANDPKGX";
        public const string LANDPKGX_UPPER              = "LANDPKGX_UPPER";
        public const string LANDPKGX_LOWER              = "LANDPKGX_LOWER";
        public const string LANDPKGY                    = "LANDPKGY";
        public const string LANDPKGY_UPPER              = "LANDPKGY_UPPER";
        public const string LANDPKGY_LOWER              = "LANDPKGY_LOWER";

        //변경 >> 22.1128 HK.PARK 추가
        public const string BOT_LANDTOPKG_X             = "BOT_LANDTOPKG_X";
        public const string BOT_CHAMFERLEN_TM_X         = "BOT_CHAMFERLEN_TM_X";
        public const string BOT_CHAMFERLEN_TP_X         = "BOT_CHAMFERLEN_TP_X";
        public const string BOT_LANDTOPKG_Y             = "BOT_LANDTOPKG_Y";
        public const string BOT_CHAMFERLEN_TM_Y         = "BOT_CHAMFERLEN_TM_Y";
        public const string BOT_CHAMFERLEN_TP_Y         = "BOT_CHAMFERLEN_TP_Y";

        public const string TOP_LANDTOPKG_X             = "TOP_LANDTOPKG_X";
        public const string TOP_CHAMFERLEN_TM_X         = "TOP_CHAMFERLEN_TM_X";
        public const string TOP_CHAMFERLEN_TP_X         = "TOP_CHAMFERLEN_TP_X";
        public const string TOP_LANDTOPKG_Y             = "TOP_LANDTOPKG_Y";
        public const string TOP_CHAMFERLEN_TM_Y         = "TOP_CHAMFERLEN_TM_Y";
        public const string TOP_CHAMFERLEN_TP_Y         = "TOP_CHAMFERLEN_TP_Y";
        //변경 << 22.1128 HK.PARK 추가
        
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