using LIB_.DateType;
using LIB_.SubFROMLib;
using LightControler;
using Object;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WSTECH_;

public class CNT_ : DATA_
{ // 프로젝트 사직 마다 확인 !
    public const int TriggerCnt     = 4;
    public const int MT             = 33;   //모터 수량 [NSS-3310/3320]0~32=33
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

    public const int THREAD = /*28*/27;   // 스레드 개수 = 27 -> 28

    public static int HEAD  = 2;    // 헤드 개수
    public static int PKR   = 6;    // 피커 개수.
    public const int BCR    = 0;    //바코드 수량
    public const int Temp   = 2;    //온도콘트롤러 수량

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
public class DATA_
{
    public static CMATH cMATH                   = new CMATH();
    public static SPC_ cSPC                     = new SPC_();
    public static PowerMeter cPM                = new PowerMeter();
    public static Sirius_2R cLightController    = new Sirius_2R();

    public static int iDay                      = DateTime.Now.Day;

    public static string sDEVICE_ID             = string.Empty;

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
    public static bool IsAdmin                  = false;    // t:관리자 권한 실행 / f:일반 권한 실행.

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

    // 다이싱 정보 변수
    public static string Sp1BladeAmountOfUse    = string.Empty; // sp1 블레이드 사용량
    public static string Sp2BladeAmountOfUse    = string.Empty;
    public static string Sp1BladeCuttingCnt     = string.Empty; // sp1 블레이드 컷팅 회수
    public static string Sp2BladeCuttingCnt     = string.Empty;

    // 다이싱 정보 변수

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

    public static Color ComBackColor            = Color.Lime;
    public static Color ComForeColor            = Color.Lime;

    public static string EQPCode                = "";   //EQUIPMENT CODE 
    public static string MachineInfo            = "";   //설비 정보 (NSS-3300 / NSS-3320)
    public static int SelectMachine;
    public static string LibProcess;
    public static void SET_MACINE_INFO(){
#if _NSS3300
        MachineInfo = "NSS-3300 (MACHINE 1호기)";     
        SelectMachine = (int)eSetMC.MachineNum_1;
#else
        MachineInfo = "NSS-3320 (MACHINE 2~6호기)";
        SelectMachine = (int)eSetMC.MachineNum_2;
#endif
        LibProcess = "Lib";
        LibProcess += Environment.Is64BitProcess ? "64Bit" : "32Bit";
    }

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
  
    public static int[,,,,] PALLET          = new int[2, 10, 10, 100, 100];
    public static dxy[,,,,] OFFSET          = new dxy[2, 10, 10, 100, 100];

    public static dxy[,,] MapCalPos_        = new dxy[2, 5, 9999];
    public static dxy[,,] TryCalPos_        = new dxy[2, 5, 9999];

    public static int[] nStep               = new int[100];
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