using freeLicence;
using LIB_.SubFROMLib;
using LightControler;
using Object;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WSTECH_;
using LIB_.DateType;

public class CNT_ : DATA_
{ // 프로젝트 사직 마다 확인 !
    public const int TriggerCnt     = 4;

    public const int MT             = 33; //33 -> 37;   // 모터 수량 (0 ~ 32->36)
    public const int POS            = 21;   // 위치값 버퍼 개수
    public const int ComPos         = 10;   // 공통 위치값 버퍼 개수
    public const int IndPos         = 10;   // 개별 위치값 버퍼 개수

    public const int IN             = 432 + 5;  // 입력 수량
    public static int numInLast;                                    // 입력 마지막 번호.

    public static int InSawStart;
    public static int InSawEnd;
    public static int InSortStart;
    public static int InSortEnd;

    public const int OUT            = 416;  // 출력 수량
    public static int numOutLast;                                   // 출력 마지막 번호.

    public static int OutSawStart;
    public static int OutSawEnd;
    public static int OutSortStart;
    public static int OutSortEnd;

    public const int TowerLamp      = 3; //타워램프 색상 개수 (1:빨강/2:노랑/3:초록/4:파랑)

    public const int MCPARA         = 500; // 공통 파라메타 버퍼 개수.
    public const int MDLPARA        = 500; // 개별 파라메타 버퍼 개수.

    public const int ERR            = 1000; // 설비 에러 버퍼 개수.
    public const int INTK           = 1000; // 설비 인터락 버퍼 개수.
    public const int RecERR         = 10;   // 에러 기록 DB를 위한 변수.

    public const int Memory         = 500;  // 메모리 버퍼 개수.
    public static void IniMemory(){
        for (int i = 0; i < Memory; i++){
            IsSTRING[i] = string.Empty;
            IsDOUBLE[i] = 0;
            IsLONG[i] = 0;
            IsBIT[i] = false;
        }
    }

    public const int THREAD = 28;   // 스레드 개수 = 27

    public static int HEAD  = 2;    // 헤드 개수
    public static int PKR   = 6;    // 피커 개수.
    public const int BCR    = 0;    //바코드 수량
    public const int Temp   = 2;    //온도콘트롤러 수량

}
public class PATH_
{
    //신규 프로젝트 마다 변경
    public const string verDLL                      = "[S191114] Dll.ver190412";

    public const string VERSION                     = "[NSS-3310S] SAW AND SORTER.CS (NEON TECH.co)" + "_" + verDLL;
    public const string MACHINE_NAME                = "NSS-3310S";
    public const string EXE_NAME                    = "NSS-3310S";
    public static string MCDir                      = "[DIR = FORWARD]"; //0=FORWARD정/1=REVERSE역

    //타설비 공유 폴더 경로
    public static string PathOffset                 = "D:\\ShareFile\\PlaceOffsetX.txt";
    public static string PathBarCode                = "D:\\Offset\\TrackInBarcode.txt";
    public static string PathUnitSearchXY           = "D:\\ShareFile\\UnitXY.txt";
    public static string PathMapBlock               = "D:\\ShareFile\\MapBlock.txt";
    public static string PathPrs                    = "D:\\ShareFile\\PRS.txt";
    public static string PreAlign                   = "D:\\ShareFile\\PreAlign.txt";
    public static string UnitSize                   = "D:\\ShareFile\\UNITSIZE.txt";
    public static string UnitPitch                  = "D:\\ShareFile\\UNITPITCH.txt";
    public static string MarkAlignResult            = "D:\\ShareFile\\MarkAlignResult.txt";
    public static string MapBlock1                  = "D:\\ShareFile\\MapBlock1.txt";
    public static string MapBlock2                  = "D:\\ShareFile\\MapBlock2.txt";
    public static string PRS                        = "D:\\ShareFile\\PRSResult.txt";
    public static string PkCAL                      = "D:\\ShareFile\\PickerCal.txt";
    public static string UNIT                       = "D:\\ShareFile\\UNITResult.txt";
    public static string TRAIN                      = "D:\\ShareFile\\TRAINNAME.txt";
    public static string UNIT_ALIGN                 = "D:\\ShareFile\\UnitAlignResult.txt"; //3POINT XYT 값
    public static string UNIT_OFFSET                = "D:\\ShareFile\\UNITPosition.txt";    //UNIT 검사시 우상단 모서리 XY값
    public static string LOT_ID                     = "D:\\ShareFile\\LOTID.txt";
    public static string ITS_ID                     = "D:\\ShareFile\\ITSID.txt";
    public static string LOT_STRIP_COUNT            = "D:\\ShareFile\\LOTStripCount.txt";  //현재 LOT STRIP 투입 수량
    public static string BARCODE                    = "D:\\ShareFile\\BARCODE.txt";
    public static string SAW_RECIPE                 = "D:\\ShareFile\\SawReipe.txt";
    public static string RePickAlign                = "D:\\ShareFile\\RePickAlign.txt"; // SAW STAGE 스트립 리픽업 피치값
    public static string PickerCal                  = "D:\\ShareFile\\PickerCal.txt";
    public static string SawLoadingOffset           = "D:\\ShareFile\\ProductAlignLoadingPosition.txt";
    public static string BladeThickness             = "D:\\ShareFile\\BladeThickness.txt";
    public static string ITSCount                   = "D:\\ShareFile\\ITSCount.txt";
    public static string ITSLocation                = "D:\\ShareFile\\ITSLocation.txt";
    public static string StripOverlap               = "D:\\ShareFile\\StripOverlap.txt";

    //기본 폴더 
    public const string BACKUP_FOLDER               = "D:\\BACKUP\\";
    public const string COMPANY                     = "NEONTECH";

    public static string LOCATION                   = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\";
    public const string STANDARD                    = "D:\\WORK\\OUTPUT\\" + COMPANY + "\\";
    public const string PROJECT                     = STANDARD + MACHINE_NAME + "\\";

    public const string DATA                        = PROJECT + "DATA\\";                   // 모델(RECIPE) 파라메타 폴더
    public const string VISION                      = PROJECT + "VISION\\";                 // 비전(RECIPE) 파라메타 폴더
    public const string SYSTEM                      = PROJECT + "SYSTEM\\";                 // 공통(MACHINE) 파라메타 폴더
    public const string InfoLABEL                   = PROJECT + "LAVEL\\";                  // UI 라벨 (MOTOR, IO, PARAMETER 등등 라벨) 폴더 
    public const string MCLOG                       = PROJECT + "MCLOG\\";                  // 설비 로그 경로 (
    public const string LOG                         = PROJECT + "LOG\\";                    // 로그 저장 폴더
    public const string IF                          = SYSTEM + "IF\\";                      // 기타 임시 버퍼 폴더

    public const string ErrLOG                      = "ERRORLOG\\";
    public const string ErrDEFINE                   = PROJECT + "ERRORDEFINE\\ERRNAME.ini"; //

    //MES
    public static string PPID                       = PROJECT + "PPID\\";
    public static string LotInfo                    = PROJECT + "LOTINFO\\LOTINFO_BACKUP.txt";

    //대덕전자 설비 전용 PARA
    public static string EQPCode                    = PROJECT + "EquipmentCode.txt";
    public static string MC_DIR                     = PROJECT + "MC_DIR.txt";   //0:정(STANDARD) , 1:역 
    public static string MsSql                      = PROJECT + "MSSQL.txt";

    //SYSTEM PARA(공통)
    public const string MotFILE                     = SYSTEM + "AjinMotion.mot";            // 아진 MOT 데이터
    public const string CurrJOB                     = SYSTEM + "CURRJOB.TXT";               // 잡파일명.
    public const string CurrVISION                  = SYSTEM + "CURRVISION.TXT";            // 비전파일명
    public const string CurrPPID                    = SYSTEM + "CURRPPID.TXT";              // PPID명
    public const string DeviceID                    = SYSTEM + "DEVICE_ID.TXT";             // 설비 디바이스 네임
    public const string PASSWORD                    = SYSTEM + "PASSWORD.TXT";              // 로그인 패스워드
    public const string ChageDATA                   = SYSTEM + "CHANGEDATA.TXT";            // 작업 진행 중 가변되는 데이터 저장 
    public const string ToolUseDATA                 = SYSTEM + "TO0L_USE_DATA.TXT";         // 수량 저장 버퍼  
    public const string TOWERLAMP                   = SYSTEM + "TOWERLAMP.TXT";             // 시스템 타워램프 파라메타
    public const string SOFTLIMIT                   = SYSTEM + "SOFTLIMIT.TXT";             // 소프트 리미트 값 
    public const string IOCheck                     = SYSTEM + "LOG_IOCHECK.TXT";
    public const string InfoINPUT                   = SYSTEM + "INFO_INPUT.TXT";            // 체크 IO 입력 정보
    public const string InfoOUTPUT                  = SYSTEM + "INFO_OUT.TXT";              // 체크 IO 출력 정보
    public const string COMMON                      = SYSTEM + "COMMON.TXT";                // 공통 데이터
    public const string ANALOG                      = SYSTEM + "ANALOG.TXT";                // 아날로그 데이터 (T1)
    public const string USE_PKR_Z                   = SYSTEM + "USESKIP_PK.TXT";            // 피커 사용 유무
    public const string UNIT_COLOR                  = SYSTEM + "UNIT_COLOR.TXT";            // 유닛 상태 색상
    public const string COUNT                       = SYSTEM + "COUNT.TXT";                 // 현재 생산 수량 저장
    public const string UserID                      = SYSTEM + "USERID.TXT";                // USER ID 리스트
    public const string CurrUSER                    = SYSTEM + "CURRUSER.TXT";              // 현재 클릭된 user 
    
    //LABEL  
    public const string Tenkey                      = InfoLABEL + "TENKEY.TXT";             // TENKEY 리스트
    public const string inputLabel                  = InfoLABEL + "INPUT.TXT";              // INPUT 리스트
    public const string outputLabel                 = InfoLABEL + "OUTPUT.TXT";             // OUTPUT 리스트
    public const string MTName                      = InfoLABEL + "MT.TXT";                 // MOTOR 리스트 (설비 정방향 자재 투입 방향 좌->우) 
    public const string MTNameR                     = InfoLABEL + "MT_RIGHT.TXT";           // MOTOR 리스트 (설비 역방향 자재 투입 방향 우->좌)
    public const string MCName                      = InfoLABEL + "MC_NAME.TXT";            // 장비 파라메타 리스트
    public const string MDName                      = InfoLABEL + "MD_NAME.TXT";            // 모델 파라메타 리스트
    public const string AnalogName                  = InfoLABEL + "ANALOG.TXT";             // 아날로그 리스트
    public const string WarningName                 = InfoLABEL + "WARNING.TXT";            // 워닝 리스트
    public const string SystemError                 = InfoLABEL + "SYSTEM_ERROR.csv";       // 시스템 에러 리스트
    public const string InterlockError              = InfoLABEL + "INTERLOCK_ERROR.csv";    // 인터락 에러 리스트

    //기본 로그 폴더
    public static string PathStanderdRecipe         = "D:\\RecipeSystem\\";
    public static string PathSeqLog                 = "D:\\MonitoringSystem\\";

    public const string LogAppEVENT                 = LOG + "APPLICATIONEVENT\\";
    public const string LogEVENT                    = LOG + "EVENT\\";
    public const string LogEXCEPTION                = LOG + "EXCEPTION\\";
    public const string LogImage                    = LOG + "IMAGE\\";
    public const string LogLOGGING                  = LOG + "LOGGING\\";
    public const string LogProcMANUAL               = LOG + "MANUALRUN\\";
    public const string LogMARS                     = LOG + "MARS\\";
    public const string LogMEASURE                  = LOG + "MEASURE\\";
    public const string LogMovingERR                = LOG + "MOVINGERROR\\";
    public const string LogPCBDATA                  = LOG + "PCBDATA\\";
    public const string LogPRINTMESSAGE             = LOG + "PRINT MESSAGE\\";
    public const string LogSPC                      = LOG + "SPC\\";
    public const string LogSYSTEM                   = LOG + "SYSTEM\\";
    public const string LogWARNING                  = LOG + "WARNING\\";
    public const string LogMES                      = LOG + "MES\\";
    public const string LogCount                    = LOG + "COUTN\\";
    public const string ProcesInfoData              = LOG + "ProcessInfoData\\";       // 설비 데이터 값
    public const string LogCheckIO                  = LOG + "CHECKIO\\LogCHECK_IO.TXT"; // 체크IO 로그
    public const string LogVisionWriteFailData      = LOG + "VISION WRITE FAIL\\";
    public const string LogPROCESS                  = LOG + "PROCESS\\";
    public const string LogLotInfo                  = LOG + "LOTINFO\\";
    public const string LogITSInfo                  = LOG + "ITS\\";

    //프로세스 로그 경로
    public const string ProcMAGAINE                 = LOG + "MAGAINE\\";
    public const string ProcINRAIL                  = LOG + "INRAIL\\";
    public const string ProcSTRINGPKR               = LOG + "STRIP_PICKER\\";
    public const string ProcUNITPKR                 = LOG + "UNIT_PICKER\\";
    public const string ProcWORKTABLE               = LOG + "WORK_TABLE\\";
    public const string ProcULDPKR                  = LOG + "ULD_PKR\\";
    public const string ProcREJECT                  = LOG + "REJECT\\";

    public const string LogCycleTack                = LOG + "TACK\\";

    public const string LogStripDefectCount         = LOG + "STRIPDEFECTCOUNT\\";
    public const string LogStripDefectLocationList  = LOG + "STRIPDEFECTLOCATIONLIST\\";

    public static string[] PROCESS_FILENAME     = new string[] { "MAGAZINE.log", "INRAIL.log", "STRIPPICKER.log", "UNITPICKER.log", "WORKPICKER.log", "REJECT.log", "ULOADERPICKER.log" };
    public static string MARS_FILENAME          = "MARS.log";

    public static string[] aPath_DEL_LOGs       = {
                                                                LogAppEVENT, LogEVENT,LogEXCEPTION,LogImage,
                                                                LogLOGGING,LogProcMANUAL,LogMARS,LogMEASURE,
                                                                LogMovingERR,LogPCBDATA,LogPRINTMESSAGE,LogSPC,
                                                                LogSYSTEM,LogWARNING, ProcesInfoData, LogCount, LogVisionWriteFailData,
                                                                LogPROCESS, LogCycleTack, LogLotInfo, LogITSInfo, LogStripDefectCount, LogStripDefectLocationList
                                                            };
    public static string[] sPath_DEL_LOGs_ONE_MONTH = {

                                                            };


    public static void Make_Folders(){
        if (!Directory.Exists(PathStanderdRecipe))          Directory.CreateDirectory(PathStanderdRecipe);
        if (!Directory.Exists(PROJECT))                     Directory.CreateDirectory(PROJECT);
        if (!Directory.Exists(DATA))                        Directory.CreateDirectory(DATA);
        if (!Directory.Exists(VISION))                      Directory.CreateDirectory(VISION);
        if (!Directory.Exists(SYSTEM))                      Directory.CreateDirectory(SYSTEM);
        if (!Directory.Exists(InfoLABEL))                   Directory.CreateDirectory(InfoLABEL);
        if (!Directory.Exists(MCLOG))                       Directory.CreateDirectory(MCLOG);
        if (!Directory.Exists(LOG))                         Directory.CreateDirectory(LOG);
        if (!Directory.Exists(LogSPC))                      Directory.CreateDirectory(LogSPC);
        if (!Directory.Exists(LogEXCEPTION))                Directory.CreateDirectory(LogEXCEPTION);
        if (!Directory.Exists(LogEVENT))                    Directory.CreateDirectory(LogEVENT);
        if (!Directory.Exists(LogMARS))                     Directory.CreateDirectory(LogMARS);
        if (!Directory.Exists(LogLOGGING))                  Directory.CreateDirectory(LogLOGGING);
        if (!Directory.Exists(LogAppEVENT))                 Directory.CreateDirectory(LogAppEVENT);
        if (!Directory.Exists(LogMovingERR))                Directory.CreateDirectory(LogMovingERR);
        if (!Directory.Exists(LogCheckIO))                  Directory.CreateDirectory(LogCheckIO);
        if (!Directory.Exists(LogProcMANUAL))               Directory.CreateDirectory(LogProcMANUAL);
        if (!Directory.Exists(LogImage))                    Directory.CreateDirectory(LogImage);
        if (!Directory.Exists(IF))                          Directory.CreateDirectory(IF);
        if (!Directory.Exists(PPID))                        Directory.CreateDirectory(PPID);
        if (!Directory.Exists(LogLotInfo))                  Directory.CreateDirectory(LogLotInfo);
        if (!Directory.Exists(LogITSInfo))                  Directory.CreateDirectory(LogITSInfo);
        if (!Directory.Exists(LogStripDefectCount))         Directory.CreateDirectory(LogStripDefectCount);
        if (!Directory.Exists(LogStripDefectLocationList))  Directory.CreateDirectory(LogStripDefectLocationList);
    }
}
public class SUBFRM_
{
    public static CogBarcoder cBarcode                      = new CogBarcoder();
    public static ConfirmProcess gConf                      = new ConfirmProcess();
    public static ErrorList gErrList                        = new ErrorList();
    public static ErrorPopUp gErrPopUp                      = new ErrorPopUp();
    public static InitialStatus gIni                        = new InitialStatus();
    public static InputBox gInputBox                        = new InputBox();
    public static IOCheck gIOCheck                          = new IOCheck();
    public static IOView gIO                                = new IOView();
    public static LOG gLOG                                  = new LOG();
    public static Login gLogin                              = new Login();
    public static LoginChangePassWord gLogChangePwd         = new LoginChangePassWord();
    public static LotIn gLotID                              = new LotIn();
    public static ManualRepeat gManualRepeat                = new ManualRepeat();
    public static MessageBoxView gMSGBOX                    = new MessageBoxView();
    public static MonIO gMonIO                              = new MonIO();
    public static MonMotion gMonMT                          = new MonMotion();
    public static MTPosTeaching gMTPosTeching               = new MTPosTeaching();
    public static MTPosTeachingData gMTPosTechingData       = new MTPosTeachingData();
    public static MTSelect gMTSelect                        = new MTSelect();
    public static SecsGem gSecsGem                          = new SecsGem();
    public static SecGemTerminalMessage gGemMessage         = new SecGemTerminalMessage();
    public static PM gPM                                    = new PM();
    public static SoftLimitSetting gSoftLimit               = new SoftLimitSetting();
    public static Splash gSplash                            = new Splash();
    public static SystemK_RFReader gRFID                    = new SystemK_RFReader();
    public static TENKEY gTENKEY                            = new TENKEY();
    public static UserID gUserID                            = new UserID();
    public static UserReqister gUserReqister                = new UserReqister();
    public static VENDER gVENDER                            = new VENDER();
    public static TenkeyList gTenkeyList                    = new TenkeyList();
}
public class CLS_{
    public PowerMeter PM_                               = new PowerMeter();

    //public static OMRON_.TEMP_[] modTEMP = new OMRON_.TEMP_[CNT_.Temp];
    //public static void IniTemp(){
    //    for (int i = 0; i < CNT_.Temp; i++){
    //        modTEMP[i] = new OMRON_.TEMP_();
    //    }
    //}
}

public class DATA_
{
    public static CMATH cMATH                   = new CMATH();
    public static SPC_ cSPC                     = new SPC_();
    public static PowerMeter cPM                = new PowerMeter();
    public static Sirius_2R cLightController    = new Sirius_2R();

    public static int iDay                      = DateTime.Now.Day;

    public static string sDEVICE_ID             = string.Empty;

    public static Class1 Dll_                   = new Class1();
    public static string sPath                  = string.Empty;
    public static MOTION_INFO MTStatu;
    public static eSHIFT IsSHIFT                = eSHIFT.ThreeSHIFT;   // 교대근무 확인 변수 (1:교대 없음, 2:2교대(주/야간조), 3:3교대조)
    public static int START_HOUR                = 20;    //시작조 첫 시간.
    public static int COUNT_RESET_HOUR          = 0;

    public static int SPLASH_PROGRESS           = 0;
    public static string SPLASH_STATUS          = string.Empty;

    public static string mTenkeyResult;                     // Tenkey 화면 동적생성으로 바꾸면서 Return 문자열 담을 전역변수
    public static string mKeyBoardResult;                   // Keyboard 화면 동적생성으로 바꾸면서 Return 문자열 전역변수.

    public static bool gExit;                               // 프로그램 유무
    public static string sJobName;                          // 현재 DEVICE 명
    public static string sGroupName;                        // 현재 GROUP 명
    public static string sLastDevice;                       // 현재 GROPU + DEVICE 명
    public static string sCurrJobName;                      // 현재 디바이스 경로
    public static string sCurrVisionName;                   // 현재 비전 경로
    public static string sCurrProcessName;                  // 현재 다이싱 공정 RECIPE 경로                                                                                                                                                                                                                                                  
    public static string[] RecipeList; // GROUP 안 레스피 리스트
    
    //OP 스위치
    public static bool bPushStart               = false;   // START S/W 눌림(기억)
    public static bool bPushStop                = false;   // STOP S/W 눌림(기억)
    public static bool bPushInitial             = false;   // INITIAL S/W 눌림(기억)
    public static bool bPushReset               = false;   // RESET S/W 눌림(기억)
    public static bool bPushEms                 = false;   // 비상정지 S/W 눌림(기억)
    public static bool bPushStop_Rec            = false;   // STOP S/W 눌림(기억) 0.5초 유지
    public static bool bLanguage                = false;    // 언어 변경
    public static bool bBD                      = false;    // BOARD 유무 (T:무/F:유)
    public static bool bMOT                     = false;    // MOT 파일.
    public static bool bJobMiss                 = false;    // 잡파일 오픈 상태 확인 
    public static bool bDRYRUN                  = false;    // 드라이-런 플래그(ONLY-main)
    public static bool bNotSAW                  = false;    // 임의 테스트 saw 인터페이스 안하고 드라이-런 하려고
    public static bool bNotPLC                  = false;    // 임의 테스트 PLC 인터페이스 안하고 드라이-런 하려고
    public static bool bMF                      = false;    // 메뉴얼 실행 플러그
    public static bool bWF                      = false;    // 경고 메세지 플러그
    public static bool bMAINT                   = false;    // 메인트 설정 플러그
    public static bool bDoorOpen                = false;    // 도어 열림 상태 확인 플러그
    public static bool bDoorLock                = false;    // 도어락 ON 상태 확인 플러그 
    public static bool bAutoBackUp              = false;    // RECIPE 변경시 백업 확인 플러그
    public static bool bOnERROR                 = false;    // 에러발생 플래그
    public static bool bOnWARNING               = false;    // 워닝 플래그
    public static bool bErrNotSave              = false;    // 메뉴얼 동작 중 발생한 에러 로그 기록 안함.
    public static bool bAllHomeComplete         = false;    // 전체 홈 동작 실행
    public static bool bInitialComplete         = false;    // 초기화 완료됨
    public static bool bEndInitial              = false;    // 초기화 완료 메세지 뷰어
    public static bool bViewConfirm             = false;    // 워닝 메세지 VIEW
    public static bool bViewInitialStatus       = false;    // 초기화 상태 VIEW
    public static bool bBzSTOP                  = false;    // 부져 OFF 비트
    public static bool bWaitProduct             = false;    // 제품대기 5분 지났을 경우
    public static bool bOVER_5MINUTE            = false;    // 에러 발생수 4분 지났을 경우
    public static bool bSTART_INTRK_CHK         = false;    // 자동 운전시 인터록 확인 
    public static bool bMANUAL_REPEAT           = false;    // 메뉴얼 반복 동작 플러그 
    public static bool bLotEnd                  = false;    // LOT-END 플러그
    public static bool bLotEndProcess           = false;    // LOT-END 처리 플러그
    public static bool bWriteLotInfo            = false;    // LOT-END 후 정보 저장
    public static bool bStripDefect             = false;    // [ITS] STRIP 정보 읽어오기
    public static bool bWriteBarcode            = false;    // 바코드 리딩 

    public static long lCPUSpeed                = 0;    // 
    public static long lTimeInitialStartTime    = 0;    //
    public static int MC_DIR                    = 0;    // 설비 방향 (0:정방향[자재 투입 외쪽->오른쪽] / 1:역방향[자재 투입 오른쪽->왼쪽])

    public static bool mAIR_SKIP;                           //장비 에어 상태 확인 스킵
    public static bool mTRIP_SKIP;                          // CP 트립 상태 확인 스킵
    public static bool mDOOR_SKIP;                          // 도어 스킵.
    public static bool mDOOR_OPEN;                          // 현재 도어 상태.
    public static bool mDOOR_LOCK;                          // 도어 락 상태.
    public static bool mAREA_SKIP;                          // 에어리어 스킵
    public static bool mTeachChanged;                       // n축의 티칭값이 변경 상태
    public static int mCurTeachMotor;                       // 현재 선택된 모터 번호.
    public static bool mNotTeachSave;                       // 파라메타 로그 저장 사용 유무. 

    public static string sSystemMessage         = string.Empty; // 시스템 메세지
    public static bool bSystemMessage           = false;        // 시스템 메세지
    public static string sWarnningMessage       = string.Empty; // 위닝 메세지
    public static string sLOG                   = string.Empty; // 로그
    public static string sINIT                  = string.Empty; // 초기화 메세지

    public static double dRunRate; // 런-가동율 버퍼

    public static eMachineStatus eMCStatus      = eMachineStatus.NONE;  //장비 상태
    public static eLogLevel eLoginLevel         = eLogLevel.Null;       //로그인 레벨 상태
    public static eLogLevel eLoginLevelBuffer   = eLogLevel.Null;       //로그인 레벨 버퍼
    public static eMainLevel eLevelMainSw       = eMainLevel.Null;      //메인 스위치 눌린 상태
    public static eMainLevel eOldLevelMainSw    = eMainLevel.Null;      //앞전 메인 스위치 눌린 상태
    public static stPassword stPassWord;                    //로그인 패스워드
    public static stLOGInfo mLOG;                           // 통합 로그
    public static stInfoMANUAL iMANUAL;                     // 메뉴얼 실행 정보

    public static stSPC oSPC, dateSPC;
    public static stSPC viewSPC;

    public static Label lbDumy                  = new Label();
    public static Button btDumy                 = new Button();

    public static stTackTime TactTime;

    public static string editErrName            = string.Empty;
    public static string editErrTitle_1         = string.Empty;
    public static string editErrTitle_2         = string.Empty;

    public static Color ComBackColor = Color.Lime;
    public static Color ComForeColor = Color.Lime;

    public static string EQPCode                = ""; //EQUIPMENT CODE 
    

    #region "배열"
    //TEXT 관련
    public static string[] ThreadName       = new string[CNT_.THREAD];
    public static string[] MtName           = new string[CNT_.MT];
    public static string[,] PosName         = new string[CNT_.MT, CNT_.POS];
    public static string[] InputName        = new string[CNT_.IN];
    public static string[] OutputName       = new string[CNT_.OUT];
    public static string[] MCParaName       = new string[CNT_.MCPARA];
    public static string[] MDParaName       = new string[CNT_.MDLPARA];
    public static string[] ErrName          = new string[CNT_.ERR];
    public static string[] ErrTitle_1       = new string[CNT_.ERR];
    public static string[] ErrTitle_2       = new string[CNT_.ERR];
    public static string[] IntkName         = new string[CNT_.INTK];
    public static string[] mSName           = new string[CNT_.Memory];
    public static string[] mDName           = new string[CNT_.Memory];
    public static string[] mLName           = new string[CNT_.Memory];
    public static string[] mBName           = new string[CNT_.Memory];

    //스레드 관련
    public static Thread[] mcTH             = new Thread[CNT_.THREAD];
    public static bool[] UseThread          = new bool[CNT_.THREAD];
    public static stThreadInfo[] LogThread  = new stThreadInfo[CNT_.THREAD];
    public static short[] mSTEP             = new short[CNT_.THREAD];
    public static double[] mT               = new double[CNT_.THREAD];
    public static Stopwatch[] tSeqTack      = new Stopwatch[CNT_.THREAD];
    public static int[] thSeqThrad;
    public static int[] thNotSeqThread;

    //모션 관련
    public static stMoveInfo[,] mtDATA                      = new stMoveInfo[100, 50];
    public static stMoveInfo mtSAVE;
    public static stMoveInfo mtOLD;
    public static stMotorTeachingLimit[,] mtTeachingLimit   = new stMotorTeachingLimit[100, 50];
    public static stMotorTeachingLimit mtOldTeachingLimit;
    public static stMotorSoftData[] mtSoftData              = new stMotorSoftData[100];
    public static stMotorSoftData mtOldSoftData;

    public static int[] mtTrigger;
    public static int[] SubTrigger;
    public static stCounterStatus[] cntSTS                  = new stCounterStatus[100];

    public static int[] mtSPARE;
    public static int[] mtNotEncoder;
    public static int[] mtCurrentHome;
    public static int[] mtRingCount;

    public static int[] pkrX1;
    public static int[] pkrX2;
    public static int[] pkrX3;
    public static int[] pkrX4;

    public static stMotionStatus[] mtSTS                    = new stMotionStatus[100];
    public static stMotorCMD[] mtCMD                        = new stMotorCMD[100];
    public static stMotorCheck[] mtCHK                      = new stMotorCheck[100];
    public static stMotorOption[] mtOPTION                  = new stMotorOption[100];

    public static bool[] enableHome                         = new bool[100];
    public static int[] tmReset                             = new int[100];
    public static int[] tmHome                              = new int[100];
    public static int[] tmHomeZero                          = new int[100];
    public static bool[] bHomeZero                          = new bool[100];

    public static void IniHomeBuffer(){
        for (int i = 0; i < CNT_.MT; i++){
            mtSTS[i].bErrHome = false;
            enableHome[i] = true;
            mtSTS[i].bHomeComplete = false;
        }
    }

    //fMTSelect
    public static int[] MT_GROUP_0;
    public static int[] MT_GROUP_1;
    public static int[] MT_GROUP_2;
    public static int[] MT_GROUP_3;
    public static int[] MT_GROUP_4;
    public static string MT_GROUP_0_NAME;
    public static string MT_GROUP_1_NAME;
    public static string MT_GROUP_2_NAME;
    public static string MT_GROUP_3_NAME;
    public static string MT_GROUP_4_NAME;

    public static void CLEAR_MOVECHKECK(int m, stMoveInfo mv){
        mtCHK[m].axis           = m;
        mtCHK[m].posBegin       = LAB_.GET_ACTPOS(m);
        mtCHK[m].pos            = mv.Pos;
        mtCHK[m].spd            = mv.Spd;
        mtCHK[m].acc            = mv.Acc;
        mtCHK[m].dcc            = mv.Dec;
        mtCHK[m].time           = mv.MoveTime;
        mtCHK[m].sLog           = "";
        mtCHK[m].errLog         = "";
        mtCHK[m].fLog           = "";
        mtCHK[m].sTime          = Environment.TickCount;
        mtCHK[m].eTime          = Environment.TickCount;
        mtCHK[m].timeStop       = Environment.TickCount;
        mtCHK[m].coment         = "";
        mtCHK[m].OnBusy         = false;
        mtCHK[m].posStop        = -1;
        mtCHK[m].cmd            = "";
        mtCHK[m].sts            = "";
        mtCHK[m].onlyStart      = false;
        mtCHK[m].noChange       = false;
        mtCHK[m].Ev             = "";
        mtCHK[m].rslt           = "";
    }
    public static double GetPosData(int axis, int ipos){
        return mtDATA[axis, ipos].Pos;
    }// 모타 데이타값
    public static stMoveInfo GetMoveInfo(int m, int pos){
        stMoveInfo mv = mtDATA[m, pos];
        return mv;
    } //해당 모터 정보 구조체 가져옴.
    public static void GetMoveInfo(int[] m, int[] p, ref double dSPD, ref double dACC, ref double dDEC, ref int tm){
        dSPD = 0; dACC = 0; dDEC = 0; tm = 0;
        for (int i = 0; i < m.Length; i++){
            if (dSPD < mtDATA[m[i], p[i]].Spd)      dSPD = mtDATA[m[i], p[i]].Spd;
            if (dACC < mtDATA[m[i], p[i]].Acc)      dACC = mtDATA[m[i], p[i]].Acc;
            if (dDEC < mtDATA[m[i], p[i]].Dec)      dDEC = mtDATA[m[i], p[i]].Dec;
            if (tm < mtDATA[m[i], p[i]].MoveTime)   tm = mtDATA[m[i], p[i]].MoveTime;
        }
    }
    public static void GetMoveInfoRaw(int m, int p, ref double pos, ref double spd, ref double acc, ref double dcc, ref int tm){
        pos = mtDATA[m, p].Pos;
        spd = mtDATA[m, p].Spd;
        acc = mtDATA[m, p].Acc;
        dcc = mtDATA[m, p].Dec;
        tm  = mtDATA[m, p].MoveTime;
    }
    public static void SetMoveInfoRaw(int m, int GET_POS, int SET_POS) { mtDATA[m, SET_POS] = mtDATA[m, GET_POS]; }

    public static void GetPosXYInfo(int mX, int mY, int pX, int pY, ref dxy dXY){
        dXY.x = mtDATA[mX, pX].Pos;
        dXY.y = mtDATA[mY, pY].Pos;
    }
    public static void GetPosXYZInfo(int mX, int mY, int mZ, int pX, int pY, int pZ, ref dxyz dXYZ){
        dXYZ.x = mtDATA[mX, pX].Pos;
        dXYZ.y = mtDATA[mY, pY].Pos;
        dXYZ.z = mtDATA[mZ, pZ].Pos;
    }
    public static void GetPosXYTInfo(int mX, int mY, int mT, int pX, int pY, int pT, ref dxyt dXYT){
        dXYT.x = mtDATA[mX, pX].Pos;
        dXYT.y = mtDATA[mY, pY].Pos;
        dXYT.t = mtDATA[mT, pT].Pos;
    }
    public static void GetPosXYZTInfo(int mX, int mY, int mZ, int mT, int pX, int pY, int pZ, int pT, ref dxyzt dXYZT){
        dXYZT.x = mtDATA[mX, pX].Pos;
        dXYZT.y = mtDATA[mY, pY].Pos;
        dXYZT.z = mtDATA[mZ, pZ].Pos;
        dXYZT.t = mtDATA[mT, pT].Pos;
    }

    public static void GetPosXYInfo(double dX, double dY, ref dxy dXY){
        dXY.x = dX;
        dXY.y = dY;
    }
    public static void GetPosXYZInfo(double dX, double dY, double dZ, ref dxyz dXYZ){
        dXYZ.x = dX;
        dXYZ.y = dY;
        dXYZ.z = dZ;
    }
    public static void GetPosXYTInfo(double dX, double dY, double dT, ref dxyt dXYT){
        dXYT.x = dX;
        dXYT.y = dY;
        dXYT.t = dT;
    }
    public static void GetPosXYZTInfo(double dX, double dY, double dZ, double dT, ref dxyzt dXYZT){
        dXYZT.x = dX;
        dXYZT.y = dY;
        dXYZT.z = dZ;
        dXYZT.t = dT;
    }

    public static void GetPosXYInfo(double dX, double dY, ref double rX, ref double rY){
        rX = dX;
        rY = dY;
    }
    public static void GetPosXYZInfo(double dX, double dY, double dZ, ref double rX, ref double rY, ref double rZ){
        rX = dX;
        rY = dY;
        rZ = dZ;
    }
    public static void GetPosXYTInfo(double dX, double dY, double dT, ref double rX, ref double rY, ref double rT){
        rX = dX;
        rY = dY;
        rT = dT;
    }
    public static void GetPosXYZTInfo(double dX, double dY, double dZ, double dT, ref double rX, ref double rY, ref double rZ, ref double rT){
        rX = dX;
        rY = dY;
        rZ = dZ;
        rT = dT;
    }

    public static string GET_MOVE_RESULT(int m){
        string s = "[" + MtName[m] + "]" + ETC.CrLf;
        s += "CMD = " + mtCHK[m].cmd + ETC.CrLf;
        s += "COMENT = " + mtCHK[m].coment + ETC.CrLf;

        s += "========================================================" + ETC.CrLf + ETC.CrLf;
        s += "POS = " + string.Format("{0:0.000}", mtCHK[m].pos) + ETC.CrLf;
        s += "SPEED = " + string.Format("{0:0.000}", mtCHK[m].spd) + ETC.CrLf;
        s += "ACC = " + mtCHK[m].acc.ToString() + ETC.CrLf;
        s += "DCC = " + mtCHK[m].dcc.ToString() + ETC.CrLf;
        s += "MOVETIME = " + mtCHK[m].time.ToString() + ETC.CrLf;
        s += "TOLLER = " + string.Format("{0:0.000}", mtCHK[m].toller) + ETC.CrLf;
        s += "MOVE-ONLY = " + mtCHK[m].onlyStart.ToString() + ETC.CrLf;
        s += "NO-CHANGE = " + mtCHK[m].noChange.ToString() + ETC.CrLf;

        s += "========================================================" + ETC.CrLf + ETC.CrLf;
        s += "POS-BEGIN = " + string.Format("{0:0.000}", mtCHK[m].posBegin) + ETC.CrLf;
        s += "POS-END = " + string.Format("{0:0.000}", mtCHK[m].posStop) + ETC.CrLf;
        s += "POS-GAP = " + string.Format("{0:0.000}", mtCHK[m].posGap) + ETC.CrLf;
        s += "OnBusy = " + mtCHK[m].time.ToString() + ETC.CrLf;
        s += "RUN-TIME = " + mtCHK[m].rTime.ToString() + ETC.CrLf;

        s += "========================================================" + ETC.CrLf + ETC.CrLf;
        s += "ERROR = " + mtCHK[m].errLog + ETC.CrLf;
        s += "STATUS = " + mtCHK[m].sts + ETC.CrLf;
        s += "EVENT = " + mtCHK[m].Ev + ETC.CrLf;
        return s;
    }

    //IO 관련
    public static bool[] mIN        = new bool[CNT_.IN]; //현재 입력 상태
    public static bool[] mOUT       = new bool[CNT_.OUT]; //현재 출력 상태
    public static bool[] mOLD_OUT   = new bool[CNT_.OUT]; //앞전 출력 상태

    public static stIO[] chkIN      = new stIO[CNT_.IN];
    public static stIO[] chkOUT     = new stIO[CNT_.OUT];

    public static int[] inModNum;                           // 입력 모듈 번호
    public static int[] InputModule;                        // 입력 모듈 번호
    public static int[] InputOffset;                        // 입력 옵셋 번호
    public static int[] InputNum;                           // 입력 모듈의 마지막 입력값 넘버

    public static int[] outModNum;
    public static int[] OutputModule;
    public static int[] OutputOffset;
    public static int[] OutputNum;

    public static short[] iEMO;
    public static short[] iDOOR;
    public static short[] iAEAR;
    public static short[] iTRIP;
    public static short[] iAIR;
    public static short[] iVAC;

    public static short[] aVAC;

    public static short[] oDOOR;
    public static short[] oACMT;
    public static short[] oVAC;
    public static short[] oREJ;
    public static short[] oREJ_TMR;
    public static short[] oREJ_DELAY;
    public static short[] oBZ;

    public static int[] mtHD;
    public static int[] mtHD1_PK;
    public static int[] mtHD2_PK;

    public static short[] eEMO;
    public static short[] eDOOR;
    public static short[] eAEAR;
    public static short[] eTRIP;
    public static short[] eAIR;

    //TENKEY 관련.
    public static bool mKeyInputEnd;
    public static bool[] mKeyPress  = new bool[200];
    public static String mKeyResult = "0";
    static public String mBuf_KEY   = "00";
    public static short TENK_NUM    = 0;

    public static void SET_COMMAND(string s, string s1){
        mBuf_KEY    = s;// "";
        mKeyResult  = s1;// "000";
    }
    public static bool DECODEKEY(){
        if (mBuf_KEY.Length > 1){
            mBuf_KEY = mBuf_KEY.Trim();
            if (mBuf_KEY.Length >= 3){
                mBuf_KEY = mBuf_KEY.Substring(mBuf_KEY.Length - 3, 3);
                Trace.WriteLine(Convert.ToString(mBuf_KEY));
                return true;
            }
        }
        return false;
    }

    //아날로그 (T1)
    public static int[] aiModNum;
    public static int[] aoModNum;
    public static int[] aiOffset;
    public static int[] aoOffset;
    public static int[] vcModNum;
    public static int[] vcOffset;

    public static double[] mSET_AI              = new double[500]; //셋팅 아날로그 값(%)
    public static double[] mSET_CONVERSION      = new double[500];
    public static double[] mAI                  = new double[500];
    public static List<uint>[] mAI_READ_WORD    = new List<uint>[100]; //아날로그 워드 값
    public static string[] AI_NAME              = new string[500];  //아날로그 이름

    //에러
    public static bool[] IsERR                  = new bool[1000];
    public static bool[] IsChkErr               = new bool[1000];
    public static bool[] bINTRK                 = new bool[CNT_.INTK];
    public static stErrOCCURED ErrViewInfo;
    public static stErrInfo[] ErrINFO           = new stErrInfo[1000];

    //모션 에러 정의
    public static int eMTEnd;
    public const int eMTBegin                   = 0;
    public const int eMTGap                     = 10;
    public const int eErrBegin                  = 350; //시스템 에러 시작 번호
    public const int eEMSBegin                  = 450; // 인터락 에러 시작 번호

    public const int eLimitP                    = 0;    // +LIMIT(CW) ERROR
    public const int eLimitM                    = 1;    // -LIMIT(CCW) ERROR
    public const int eNotHome                   = 2;    // 원점 복귀 미실시 (원점 실행)
    public const int eCwSoftLime                = 3;    // CW SOFT LIMIT 위치 알람 발생 (+)
    public const int eCcwSoftLime               = 4;    // CCW SOFT LIMIT 위치 알람 발생 (-)
    public const int eTimeOver                  = 5;    // 이동시간 초과 ERROR (CMD/ACT 값 불일치)eCmdActNotSame
    public const int eSVOff                     = 6;    // SV-OFF 됨
    public const int eALARM                     = 7;    // 서보알람
    public const int eMotMOVE                   = 8;    // MOTOR MOVE ERROR
    public const int eHOME                      = 9;    // HOME SEARCH ERROR

    //파라메타
    public static double[] prMACHINE            = new double[1000];
    public static double[] prMODEL              = new double[1000];

    public const int CleanCnt = 3;
    public static stInfoClean CleanData;
    public static void IniCleanData(){
        CleanData.MODE      = new int[10];
        CleanData.COUNTER   = new int[10];
        CleanData.TIME      = new int[10];
    }

    public static stInfoClean WorkedCleanData;
    public static void IniWorkedCleanData(){
        WorkedCleanData.MODE    = new int[10];
        WorkedCleanData.COUNTER = new int[10];
        WorkedCleanData.TIME    = new int[10];
    }

    //워닝 메세지
    public static stCONFIRM[] ConfirmUser   = new stCONFIRM[100];
    public static stCONFIRM ConfirmG        = new stCONFIRM();
    public static string WarMESSAGE         = string.Empty;
    public static void InitailizeWarnning(){
        for (int i = 0; i < ConfirmUser.Length; i++){
            ConfirmUser[i].useable = false;
            ConfirmUser[i].process = false;
        }
    }

    //메모리
    public static string[] IsSTRING             = new string[500];
    public static double[] IsDOUBLE             = new double[500];
    public static long[] IsLONG                 = new long[500];
    public static bool[] IsBIT                  = new bool[500];

    //타워램프
    public static int LampStatus                = 0; //타워 램프 셋팅 장비 모드 선택
    public static string[] DataTowerStatus      = new string[8]; //RUN,STOP,INI,LOTEND,ERROR,MANUAL,LOT-END,WARNING
    public static string[] BackupTowerStatus    = new string[8];
    public static long TW_TIME                  = 0;

    public static bool mCheckFlag               = false;
    public static bool TW_RED                   = false;
    public static bool TW_YELLOW                = false;
    public static bool TW_GREEN                 = false;
    public static bool TW_BLUE                  = false;

    //장비 관련 
    public static dxy[] PkOffset                = new dxy[16]; //picker 8개 기준으로 head 2개임.
    public static dxy[] PkOffset_0              = new dxy[16]; //picker 0도 기준 
    public static dxy[] PkOffset_P90            = new dxy[16]; //picker 90도 기준 
    public static dxy[] PkOffset_P180           = new dxy[16]; //picker 180도 기준 
    public static dxy[] PkOffset_P270           = new dxy[16]; //picker 270도 기준 
    public static dxy[] PkOffset_M90            = new dxy[16]; //picker -90도 기준 
    public static dxy[] PkOffset_M180           = new dxy[16]; //picker -180도 기준 
    public static dxy[] PkOffset_M270           = new dxy[16]; //picker -270도 기준 

    public static eSTATUS[,] PK_                = new eSTATUS[50, 1000];
    public static eSTATUS[,] ARR_               = new eSTATUS[50, 9999];    //상태
    public static eSTATUS[] PK_Z                = new eSTATUS[99];          //피커 사용 유무 
    public static string[] C_CODE               = new string[50];           //유닛 상태 색상.
    public static stInfoSeq[] SeqData           = new stInfoSeq[99];        //구간별 자재 정보

    public static dxy[,,,] MarkCalPos_          = new dxy[2, 50, 50, 9999];  //PALLET STAGE NUM, GROUP X, GROUP Y, UNIT XY
    public static eSTATUS[,,,] Pallet           = new eSTATUS[2, 50, 50, 9999];  //PALLET STAGE NUM, GROUP X, GROUP Y, UNIT XY
    static public stPkr[] PICKER                = new stPkr[CNT_.HEAD];
    public static void INI_PICKER_INFO(){
        for (int n = 0; n < PICKER.Length; n++){
            PICKER[n].finger    = new stFinger[CNT_.PKR];
            PICKER[n].CMD       = "";
        } // 피커정보 초기화
    }
    public static void INFO_PICKER_DATA_CLEANER(eHD HEAD, int nLONG){
        for (int z = 0; z < CNT_.PKR; z++){
            PICKER[(int)HEAD].finger[z].valid       = false;
            PICKER[(int)HEAD].finger[z].pocketX     = 0;
            PICKER[(int)HEAD].finger[z].pocketY     = 0;
            PICKER[(int)HEAD].finger[z].iResult     = 0;
            PICKER[(int)HEAD].finger[z].prsX        = 0;
            PICKER[(int)HEAD].finger[z].prsY        = 0;
            PICKER[(int)HEAD].finger[z].prsR        = 0;
            PICKER[(int)HEAD].finger[z].prsOffsetX  = 0;
            PICKER[(int)HEAD].finger[z].prsOffsetY  = 0;
            PICKER[(int)HEAD].finger[z].prsOffsetT  = 0;
        }
        IsLONG[nLONG] = (int)HEAD;
    }
    public static void HDBufferClear(ref bool NG, ref bool REJECT, ref bool INSPECTION){
        NG          = false;
        REJECT      = false;
        INSPECTION  = false;
    }

    public static int[,,,,] PALLET          = new int[2, 10, 10, 100, 100];
    public static dxy[,,,,] OFFSET          = new dxy[2, 10, 10, 100, 100];

    public static dxy[,,] MapCalPos_        = new dxy[2, 5, 9999];
    public static dxy[,,] TryCalPos_        = new dxy[2, 5, 9999];

    public static int[] nStep               = new int[100];

    //프로젝트에서 알아야 하는 변수
    public static int DllWarningMessage     { get; set; }
    public static int WarningMessageBox     { get; set; }
    public static int WAR_EndInitial        { get; set; }
    public static int VT_STOP               { get; set; }
    public static int VT_START              { get; set; }
    public static int VT_RESET              { get; set; }
    public static int STOP                  { get; set; }
    public static int START                 { get; set; }
    public static int RESET                 { get; set; }
    public static int TOWER_RED             { get; set; }
    public static int TOWER_YELLOW          { get; set; }
    public static int TOWER_GREEN           { get; set; }
    public static int TOWER_BLUE            { get; set; }
    public static int CYLINDER_OVERTIME     { get; set; }
    public static int BZOffTime             { get; set; }
    public static int USE_LOG_SAVE          { get; set; }
    public static int MANUAL_REPEAT_DLAY    { get; set; }
    public static int UseLotEnd             { get; set; }
    public static int SelectMTSpd           { get; set; }
    public static int UseMES                { get; set; }
    #endregion "배열"
}

public class MAP_ : DATA_
{
    #region "MAGAZINE"
    public static int nLDSlot           = 0;
    public static int nULDSlot          = 0;
    public static eSTATUS[] nState      = new eSTATUS[30]; // 슬롯 최대 30개.
    public static eSTATUS[] nState_View = new eSTATUS[30];
    static public eSTATUS[,] ARR_MGZ    = new eSTATUS[50, 9999];

    public static void SET_CstMapAllEmpty(int m){
        for (int i = 0; i < nState.Length; i++){
            nState[i]       = eSTATUS.EMPTY;
            ARR_MGZ[m, i]   = eSTATUS.EMPTY;
        }
    } //빈카세트

    public static void SET_CstMapAllExists(int m){
        for (int i = 0; i < nState.Length; i++){
            nState[i]       = eSTATUS.MARK;
            ARR_MGZ[m, i]   = eSTATUS.MARK;
        }
    } //FULL CASSETE (자재 존재함.)

    public static void CHK_SLOT_NUM(ref int nSlot){
        if (nSlot < 0)              nSlot = 0;
        if (nSlot >= nState.Length) nSlot = nState.Length - 1;
    }

    public static void SET_CstMapSlotEmpty(int m, int nSlot){
        CHK_SLOT_NUM(ref nSlot);
        nState[nSlot]       = eSTATUS.EMPTY;
        ARR_MGZ[m, nSlot]   = eSTATUS.EMPTY;
    }

    public static void SET_CstMapSlotWorking(int m, int nSlot){
        CHK_SLOT_NUM(ref nSlot);
        nState[nSlot]       = eSTATUS.WORKING;
        ARR_MGZ[m, nSlot]   = eSTATUS.WORKING;
    }

    public static void SET_CstMapSlotExists(int m, int nSlot){
        CHK_SLOT_NUM(ref nSlot);
        nState[nSlot]       = eSTATUS.MARK;
        ARR_MGZ[m, nSlot]   = eSTATUS.MARK;
    }

    public static void SET_CstMapSlotWorkEnd(int m, int nSlot){
        CHK_SLOT_NUM(ref nSlot);
        nState[nSlot]       = eSTATUS.WORKEND;
        ARR_MGZ[m, nSlot]   = eSTATUS.WORKEND;
    }

    public static bool IsExistsLoadSlot(int nMaxSlot){
        for (int i = 0; i < nMaxSlot; i++){
            if (nState[i] == eSTATUS.MARK) return true;
        }
        return false;
    }

    public static bool IsExistsUnloadSlot(int nMaxSlot){
        for (int i = 0; i < nMaxSlot; i++){
            if (nState[i] == eSTATUS.WORKING) return true;
        }
        return false;
    }

    public static int GET_LoadSlotNo(){
        for (int i = 0; i < nState.Length; i++){
            if (nState[i] == eSTATUS.MARK) return i;
        }
        return nState.Length - 1;
    }

    public static int GET_UnloadSlotNo(){
        for (int i = 0; i < nState.Length; i++){
            if (nState[i] == eSTATUS.WORKING) return i;
        }
        return nState.Length - 1;
    }
    #endregion  "MAGAZINE"

    #region "TRAY"
    static public bool[,,] mapTray      = new bool[3, 100, 100]; //TRAY TRANSFER AXIS, Unit Y, Unit X
    static public bool[,,] mapTray_View = new bool[3, 100, 100]; //TRAY TRANSFER AXIS, Unit Y, Unit X
    static public eSTATUS[,] ARR_TRAY   = new eSTATUS[3, 9999];
    static public void TrayMap_Reset(eTRAY tray, int CntX, int CntY){
        int idx;
        for (int idxY = 0; idxY < CntY; idxY++){
            for (int idxX = 0; idxX < CntX; idxX++){
                mapTray[(int)tray, idxY, idxX] = false;
                if ((idxY + 1) > 1) idx = (int)(CntX * (idxY)) + (idxX + 1);
                else                idx = (idxY + 1) * (idxX + 1);
                //mLogWR.DEBUG_PRINT("MT = " + iMT.ToString("00") + " INDEX = " + (idx - 1).ToString("000"));
                ARR_TRAY[(int)tray, idx - 1] = eSTATUS.EMPTY;//csMARK;
            }
        }
    }//Fend
    public static void TrayMap_Work(int eTRAY, int trayX, int trayY, int cntX, int cntY){
        int idx;
        if ((cntY + 1) > 1) idx = (int)(trayX * (cntY)) + (cntX + 1);
        else                idx = (cntY + 1) * (cntX + 1);
        ARR_TRAY[eTRAY, idx - 1] = eSTATUS.WORKING;
        //ARR_TRAY[eTRAY, ((trayX * trayY) - idx)] = eSTATUS.csWORK;
    }
    public static void TrayMap_Reject(int eTRAY, int trayX, int trayY, int cntX, int cntY){
        int idx;
        if ((cntY + 1) > 1) idx = (int)(trayX * (cntY)) + (cntX + 1);
        else                idx = (cntY + 1) * (cntX + 1);
        ARR_TRAY[eTRAY, idx - 1] = eSTATUS.NG;
        //ARR_TRAY[eTRAY, ((trayX * trayY) - idx)] = eSTATUS.csNG;
    }
    public static int GetTrayPocket(eTRAY eTay, int TrayX, int TrayY, ref int nPocketX, ref int nPocketY){
        for (int y = 0; y < TrayY; y++){
            for (int x = 0; x < TrayX; x++){
                if (mapTray[(int)eTay, y, x]) continue;
                nPocketX = x;
                nPocketY = y;
                return 0;
            }
        }
        return -1;
    }
    #endregion "TRAY"

    #region "MAPBLOCK"
    public static bool[,,,,] mapPallet      = new bool[2, 10, 10, 100, 100];    //PALLET AXIS, GROUP Y, GROUP X, UNIT Y, UINT X
    public static bool[,,,,] mapPallet_View = new bool[2, 10, 10, 100, 100];    //PALLET AXIS, GROUP Y, GROUP X, UNIT Y, UINT X
    public static eSTATUS[,,,,] ARR_PALLET  = new eSTATUS[2, 10, 10, 100, 100]; //PALLET AXIS, GROUP Y, GROUP X, UNIT X, UNIT Y(INDEX)
    public static eSTATUS[,,,,] Inspection  = new eSTATUS[2, 10, 10, 100, 100]; //PALLET AXIS, GROUP Y, GROUP X, UNIT XY(INDEX)
    public static eSTATUS OldInspection;

    public static void PalleMap_BitSet(eMAP_BLOCK pallet, eMAP_DATA state, int GroupX, int GroupY, int UnitX, int UnitY){
        for (int gy = 0; gy < GroupY; gy++){
            for (int gx = 0; gx < GroupX; gx++){
                for (int idxY = 0; idxY < UnitY; idxY++){
                    for (int idxX = 0; idxX < UnitX; idxX++){
                        mapPallet[(int)pallet, gy, gx, idxY, idxX] = true;
                    }
                }
            }
        }
    }

    public static void PalletMap_Reset(eMAP_BLOCK pallet, eMAP_DATA state, int GroupX, int GroupY, int UnitX, int UnitY){
        int idx;
        for (int gy = 0; gy < GroupY; gy++){
            for (int gx = 0; gx < GroupX; gx++){
                for (int idxY = 0; idxY < UnitY; idxY++){
                    for (int idxX = 0; idxX < UnitX; idxX++){
                        //mapPallet[(int)pallet, gy, gx, idxY, idxX] = true;
                        if ((idxY + 1) > 1) idx = (int)(UnitX * (idxY)) + (idxX + 1);
                        else                idx = (idxY + 1) * (idxX + 1);

                        if (pallet == eMAP_BLOCK.ALL){
                            ARR_PALLET[(int)eMAP_BLOCK.STAGE1, gy, gx, idxX, idxY] = eSTATUS.EMPTY;
                            ARR_PALLET[(int)eMAP_BLOCK.STAGE2, gy, gx, idxX, idxY] = eSTATUS.EMPTY;
                        }
                        else{
                            ARR_PALLET[(int)pallet, gy, gx, idxX, idxY] = eSTATUS.EMPTY;
                        }

                        //if (state == eREVERSE.FULL){
                        //    ARR_PALLET[(int)pallet, gy, gx, idxX, idxY] = eSTATUS.EMPTY;
                        //}
                        //else{
                        //    if (state == eREVERSE.ODD){
                        //        if (!mDATA.cMATH.isEven((int)(idx)))  ARR_PALLET[(int)pallet, gy, gx, idx - 1] = eSTATUS.csEMPTY;
                        //        else                            ARR_PALLET[(int)pallet, gy, gx, idx - 1] = eSTATUS.csNONE;
                        //    } //홀
                        //    else{
                        //        if (mDATA.cMATH.isEven((int)(idx))) ARR_PALLET[(int)pallet, gy, gx, idx - 1] = eSTATUS.csEMPTY;
                        //        else                            ARR_PALLET[(int)pallet, gy, gx, idx - 1] = eSTATUS.csNONE;
                        //    } //짝
                        //}
                    }
                }
            }
        }
    } // 맵블록 데이터 리셋

    public static void PalletInspection_Reset(eMAP_BLOCK pallet, eMAP_DATA state, int GroupX, int GroupY, int UnitX, int UnitY){
        int idx;
        for (int gy = 0; gy < GroupY; gy++){
            for (int gx = 0; gx < GroupX; gx++){
                for (int idxY = 0; idxY < UnitY; idxY++){
                    for (int idxX = 0; idxX < UnitX; idxX++){
                        if ((idxY + 1) > 1) idx = (int)(UnitX * (idxY)) + (idxX + 1);
                        else                idx = (idxY + 1) * (idxX + 1);
                        Inspection[(int)pallet, gy, gx, idxX, idxY] = eSTATUS.EMPTY;
                    }
                }
            }
        }
    } // 마이크로뷰어 데이터 리셋
    public static void PalletInspection_Set(eMAP_BLOCK pallet, eMAP_DATA state, int GroupX, int GroupY, int UnitX, int UnitY, eSTATUS eSTS) { Inspection[(int)pallet, GroupX, GroupY, UnitX, UnitY] = eSTS; } // 마이크로뷰어 포켓 번호 셋팅
    public static void PalletMap_ChkMarkCam(eMAP_BLOCK pallet, eMAP_DATA state, int GroupX, int GroupY, int palletX, int palletY){
        int idx;
        int setX;
        int setY;

        for (int gy = 0; gy < GroupY; gy++){
            for (int gx = 0; gx < GroupX; gx++){
                for (int idxY = 0; idxY < palletY; idxY++){
                    for (int idxX = 0; idxX < palletX; idxX++){
                        mapPallet[(int)pallet, gy, gx, idxY, idxX] = true;

                        if ((idxY + 1) > 1) idx = (int)(palletX * (idxY)) + (idxX + 1);
                        else                idx = (idxY + 1) * (idxX + 1);
                        if (!bDRYRUN){
                            //if (state == eREVERSE.FULL)
                            //    ARR_PALLET[(int)pallet, gy, gx, idx - 1] = eSTATUS.csMARK;
                            //else{
                            //    if (state == eREVERSE.ODD){
                            //        if (!mDATA.cMATH.isEven(idx)) ARR_PALLET[(int)pallet, gy, gx, idx - 1] = eSTATUS.csMARK;
                            //    } //홀
                            //    else{
                            //        if (mDATA.cMATH.isEven(idx)) ARR_PALLET[(int)pallet, gy, gx, idx - 1] = eSTATUS.csMARK;
                            //    } //짝
                            //}

                            //TOP 검사는 상부 PICKUP는 하부에서 시작하고 우에서 좌로 갈 경우.
                            setX = (palletX - 1) - idxX;
                            setY = (palletY - 1) - idxY;
                            if (PALLET[(int)pallet, gx, gy, /*setX*/idxX, setY/*idxY*/] == (int)eSTATUS.MARK)           ARR_PALLET[(int)pallet, gy, gx, idxX, idxY] = eSTATUS.MARK;
                            else if (PALLET[(int)pallet, gx, gy, /*setX*/idxX, setY/*idxY*/] == (int)eSTATUS.NG)        ARR_PALLET[(int)pallet, gy, gx, idxX, idxY] = eSTATUS.NG;
                            else if (PALLET[(int)pallet, gx, gy, /*setX*/idxX, setY/*idxY*/] == (int)eSTATUS.FAIL)      ARR_PALLET[(int)pallet, gy, gx, idxX, idxY] = eSTATUS.FAIL;
                            else if (PALLET[(int)pallet, gx, gy, /*setX*/idxX, setY/*idxY*/] == (int)eSTATUS.FIRST)     ARR_PALLET[(int)pallet, gy, gx, idxX, idxY] = eSTATUS.FIRST;
                            else if (PALLET[(int)pallet, gx, gy, /*setX*/idxX, setY/*idxY*/] == (int)eSTATUS.SECOND)    ARR_PALLET[(int)pallet, gy, gx, idxX, idxY] = eSTATUS.SECOND;
                            else ARR_PALLET[(int)pallet, gy, gx, idxX, idxY] = eSTATUS.NONE;
                        }
                        else{
                            ARR_PALLET[(int)pallet, gy, gx, idxX, idxY] = eSTATUS.MARK;
                        }
                    }
                }
            }
        }
    } // TOP VISION 검사 결과 포켓 셋트 

    public static int GetPalletInspentionPocket(eMAP_BLOCK ePALLET, int GroupX, int GroupY, int UnitX, int UnitY, ref int CurentGroupX, ref int CurentGroupY, ref int PocketX, ref int PocketY, eSTATUS eSTS){
        int setX;
        int setY;
        for (int gy = 0; gy < GroupY; gy++){
            for (int gx = 0; gx < GroupX; gx++){
                for (int uy = 0; uy < UnitY; uy++){
                    for (int ux = 0; ux < UnitX; ux++){
                        setX = (UnitX - 1) - ux;
                        setY = (UnitY - 1) - uy;
                        if (Inspection[(int)ePALLET, gx, gy, ux, /*uy*/setY] != eSTS || !mapPallet[(int)ePALLET, gy, gx, uy, ux]) continue;
                        CurentGroupX = gx;
                        CurentGroupY = gy;
                        PocketX = ux;
                        PocketY = uy;
                        return 0;
                    }
                }
            }
        }
        return -1;
    }

    public static int GetPalletPocket(eMAP_BLOCK Pallet, int GroupX, int GroupY, int UnitX, int UnitY, ref int CurentGroup_X, ref int CurentGroup_Y, ref int PocketX, ref int PocketY){
        
        for (int gy = 0; gy < GroupY; gy++){
            for (int gx = 0; gx < GroupX; gx++){
                for (int uy = 0; uy < UnitY; uy++){
                    for (int ux = 0; ux < UnitX; ux++){
                        if (!mapPallet[(int)Pallet, gy, gx, uy, ux]) continue;
                        CurentGroup_X = gx;
                        CurentGroup_Y = gy;
                        PocketX = ux;
                        PocketY = uy;
                        return 0;
                    }
                }
            }
        }
        return -1;
    }

    public static void GetIndex(int X, int Y, int UX, int UY, ref int nIndex){
        if ((Y + 1) > 1) nIndex = (int)UX + (X + 1);
        else nIndex = (Y + 1) * (X + 1);
    }

    public static bool CheckStageStatus(eMAP_BLOCK eStage, int X, int Y, int UX, int UY){
        int gx = 0, gy = 0, x = 0, y = 0;
        if (-1 != GetPalletPocket(eStage, X, Y, UX, UY, ref gx, ref gy, ref x, ref y)) return true;
        return false;
    }

    public static void CancelPalletData(eMAP_BLOCK Pallet, eMAP_DATA state, int GroupX, int GroupY, int UnitX, int UnitY){
        for (int gy = 0; gy < GroupY; gy++){
            for (int gx = 0; gx < GroupX; gx++){
                for (int uy = 0; uy < UnitY; uy++){
                    for (int ux = 0; ux < UnitX; ux++){
                        if (Pallet == eMAP_BLOCK.ALL){
                            mapPallet[(int)eMAP_BLOCK.STAGE1, gy, gx, uy, ux] = false;
                            mapPallet[(int)eMAP_BLOCK.STAGE2, gy, gx, uy, ux] = false;
                        }
                        else{
                            mapPallet[(int)Pallet, gy, gx, uy, ux] = false;
                        }
                        //mapPallet[(int)Pallet, gy, gx, uy, ux] = false;
                    }
                }
            }
        }
    }
    #endregion "MAPBLOCK"
}