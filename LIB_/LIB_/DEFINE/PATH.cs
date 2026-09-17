using System.IO;

public class PATH_
{
    //신규 프로젝트 마다 변경
    public const string verDLL                  = "[S191114] Dll.ver190412";

    public const string VERSION                 = "[NSS-3310S] SAW AND SORTER.CS (NEON TECH.co)" + "_" + verDLL;
    public const string MACHINE_NAME            = "NSS-3310S";
    public const string EXE_NAME                = "NSS-3310S";
    public static string MCDir                  = "[DIR = FORWARD]"; //0=FORWARD정/1=REVERSE역

    //타설비 공유 폴더 경로
    public static string ShareFolder            = "D:\\ShareFile\\";
    public static string ShareFolderTemp        = "D:\\ShareFileTemp\\";

    //현재 사용안함.
    public static string PathMapBlock           = "D:\\ShareFile\\MapBlock.txt";
    public static string PathPrs                = "D:\\ShareFile\\PRS.txt";
    public static string MarkAlignResult        = "D:\\ShareFile\\MarkAlignResult.txt";
    public static string PickerCal              = "D:\\ShareFile\\PickerCal.txt";
    public static string SawLoadingOffset       = "D:\\ShareFile\\ProductAlignLoadingPosition.txt";

    //읽기만
    public static string PathUnitSearchXY       = "D:\\ShareFile\\UnitXY.txt";
    public static string PathOffset             = "D:\\ShareFile\\PlaceOffsetX.txt";
    public static string PRS                    = "D:\\ShareFile\\PRSResult.txt";
    public static string PkCAL                  = "D:\\ShareFile\\PickerCal.txt";
    public static string RePickAlign            = "D:\\ShareFile\\RePickAlign.txt"; // SAW STAGE 스트립 리픽업 피치값
    public static string UNIT_ALIGN             = "D:\\ShareFile\\UnitAlignResult.txt"; //3POINT XYT 값
    public static string BladeThickness         = "D:\\ShareFile\\BladeThickness.txt";
    public static string NewBladeBarcodeSp1     = "D:\\ShareFile\\SP1NewBladeData.txt";         // 블레이드 바코드 정보
    public static string NewBladeBarcodeSp2     = "D:\\ShareFile\\SP2NewBladeData.txt";
    public static string OldBladeBarcodeSp1     = "D:\\ShareFile\\SP1OldBladeData.txt";
    public static string OldBladeBarcodeSp2     = "D:\\ShareFile\\SP2OldBladeData.txt";
    public static string MES_ABFMATERIAL        = "D:\\ShareFile\\ABF.txt";

    //쓰기
    public static string PathBarCode            = "D:\\ShareFile\\TrackInBarcode.txt";
    public static string PreAlign               = "D:\\ShareFile\\PreAlign.txt";
    public static string UnitSize               = "D:\\ShareFile\\UNITSIZE.txt";
    public static string UnitPitch              = "D:\\ShareFile\\UNITPITCH.txt";
    public static string MapBlock1              = "D:\\ShareFile\\MapBlock1.txt";
    public static string MapBlock2              = "D:\\ShareFile\\MapBlock2.txt";
    public static string UNIT                   = "D:\\ShareFile\\UNITResult.txt";
    public static string UNIT_OFFSET            = "D:\\ShareFile\\UNITPosition.txt";    //UNIT 검사시 우상단 모서리 XY값
    public static string TRAIN                  = "D:\\ShareFile\\TRAINNAME.txt";
    public static string LOT_ID                 = "D:\\ShareFile\\LOTID.txt";
    public static string ITS_ID                 = "D:\\ShareFile\\ITSID.txt";
    public static string LOT_STRIP_COUNT        = "D:\\ShareFile\\LOTStripCount.txt";  //현재 LOT STRIP 투입 수량
    public static string BARCODE                = "D:\\ShareFile\\BARCODE.txt";
    public static string SAW_RECIPE             = "D:\\ShareFile\\SawReipe.txt";
    public static string VisionReciepe          = "D:\\ShareFile\\VisionReceip.txt";        // LOT INFO VISION RECIPE
    public static string ITSCount               = "D:\\ShareFile\\ITSCount.txt";
    public static string ITSLocation            = "D:\\ShareFile\\ITSLocation.txt";
    public static string StripOverlap           = "D:\\ShareFile\\StripOverlap.txt";
    public static string MapBlock1StripBarcode  = "D:\\ShareFile\\MapBlock1StripBarcode.txt";   // 맵블럭에 안착 되어 있는 스트립 바코드
    public static string MapBlock2StripBarcode  = "D:\\ShareFile\\MapBlock2StripBarcode.txt";   // 맵블럭에 안착 되어 있는 스트립 바코드 
    public static string PCBTYPE                = "D:\\ShareFile\\PCBType.txt";                 // 설비 PCB TYPE = 0: STRIP / 1: QURD  [VISION 에서는 IF (0) {STRIP} else {QUAD} ]
    
    public static string LOTID                  = "D:\\ShareFile\\LOTID_.ini";
    public static string BackUpBarcodeData      = "D:\\ShareFile\\BarcodeInfo.ini"; // 핸들러에서만 확인!   


    //대덕전자 요청
    public static string ABF                    = "D:\\ABFMATERIAL.txt"; //블레이드 정보?
    public static string InStrip                = "D:\\ShareFile\\InStrip\\"; //투입된 strip 정보.
    public static string LotStrip               = InStrip + "OutputFile\\"; //lot-end 후 투입 strip 로그 정보 폴더.

    //기본 폴더 
    public const string BACKUP_FOLDER           = "D:\\BACKUP\\";
    public const string COMPANY                 = "NEONTECH";

    public static string LOCATION               = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\";
    public const string STANDARD                = "D:\\WORK\\OUTPUT\\" + COMPANY + "\\";
    public const string PROJECT                 = STANDARD + MACHINE_NAME + "\\";

    public const string DATA                    = PROJECT + "DATA\\";                   // 모델(RECIPE) 파라메타 폴더
    public const string VISION                  = PROJECT + "VISION\\";                 // 비전(RECIPE) 파라메타 폴더
    public const string SYSTEM                  = PROJECT + "SYSTEM\\";                 // 공통(MACHINE) 파라메타 폴더
    public const string InfoLABEL               = PROJECT + "LAVEL\\";                  // UI 라벨 (MOTOR, IO, PARAMETER 등등 라벨) 폴더 
    public const string MCLOG                   = PROJECT + "MCLOG\\";                  // 설비 로그 경로 (
    public const string LOG                     = PROJECT + "LOG\\";                    // 로그 저장 폴더
    public const string ABF_LIST                = PROJECT + "ABF\\";                    // 자재명에 대한 블레이드 정보 리스트 폴더
    public const string IF                      = SYSTEM + "IF\\";                      // 기타 임시 버퍼 폴더

    public const string ErrLOG                  = "ERRORLOG\\";
    public const string ErrDEFINE               = PROJECT + "ERRORDEFINE\\ERRNAME.ini"; //

    //MES
    public static string PPID                   = PROJECT + "PPID\\";
    public static string LotInfo                = PROJECT + "LOTINFO\\LOTINFO_BACKUP.txt";

    //대덕전자 설비 전용 PARA
    public static string EQPCode                = PROJECT + "EquipmentCode.txt";
    public static string MC_DIR                 = PROJECT + "MC_DIR.txt";   //0:정(STANDARD) , 1:역 
    public static string MsSql                  = PROJECT + "MSSQL.txt";

    public static string SQLRead                = PROJECT + "OptionRead.txt";   // CP.DBReadMode = 0 / 1 / 2

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
    public const string DAY_COUNT                   = SYSTEM + "DAY_COUNT.TXT";             // 하루 생산 수량 확인
    public const string UserID                      = SYSTEM + "USERID.TXT";                // USER ID 리스트
    public const string CurrUSER                    = SYSTEM + "CURRUSER.TXT";              // 현재 클릭된 user 
    public const string CurrLot                     = SYSTEM + "CURRLOT.TXT";               // 현재 LOT 정보
    public const string WorkedLot                   = SYSTEM + "WORKEDLOT.TXT";             // 작업 완료 LOT 정보

    public const string StripData                   = "D:\\STRIPDATA\\";
    public const string DBLog                       = "D:\\DB_LOG\\";

    //LABEL  
    public const string Tenkey                      = InfoLABEL + "TENKEY.TXT";             // TENKEY 리스트
    public const string inputLabel                  = InfoLABEL + "INPUT.TXT";              // INPUT 리스트
    public const string inputLabel3300              = InfoLABEL + "INPUT_3300.TXT";
    public const string outputLabel                 = InfoLABEL + "OUTPUT.TXT";             // OUTPUT 리스트a
    public const string outputLabel3300             = InfoLABEL + "OUTPUT_3300.TXT";
    public const string MTName                      = InfoLABEL + "MT.TXT";                 // MOTOR 리스트 (설비 정방향 자재 투입 방향 좌->우) 
    public const string MTNameR                     = InfoLABEL + "MT_RIGHT.TXT";           // MOTOR 리스트 (설비 역방향 자재 투입 방향 우->좌)
    public const string MTName3300                  = InfoLABEL + "MT_3300.TXT";            // MOTOR 리스트 (광주 NSS-3300)
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
    public const string LogLotEnd                   = LOG + "LOTEND\\";
    public const string LogLotLogList               = LOG + "LOTLogList\\";     //lot별 로그 확인 추가 22.1121 HK.PARK
    public const string LogBladeInfo                = LOG + "BLADE INFO\\";     //블레이드 정보 로그
    public const string LogPCBUnitInfo              = LOG + "PCB UNIT INFO\\";  //장단 유닛 정보 !

    //프로세스 로그 경로
    public const string ProcMAGAINE                 = LOG + "MAGAINE\\";
    public const string ProcINRAIL                  = LOG + "INRAIL\\";
    public const string ProcSTRINGPKR               = LOG + "STRIP_PICKER\\";
    public const string ProcUNITPKR                 = LOG + "UNIT_PICKER\\";
    public const string ProcWORKTABLE               = LOG + "WORK_TABLE\\";
    public const string ProcULDPKR                  = LOG + "ULD_PKR\\";
    public const string ProcREJECT                  = LOG + "REJECT\\";

    public const string LogCycleTack                = LOG + "TACK\\";
    public const string LogOneCycleTime             = LOG + "ONECYCLE\\";

    public const string LogStripDefectCount         = LOG + "STRIPDEFECTCOUNT\\";
    public const string LogStripDefectLocationList  = LOG + "STRIPDEFECTLOCATIONLIST\\";

    public const string LogBarcodeHistory           = LOG + "BARCODE_HISTORY\\";
    public const string LogITSBarcodeHistory        = LOG + "ITS_BARCODE_HISTORY\\";

    public const string SpindleCurrent              = LOG + "SPINDLE CURRENT\\";

    public const string LogProgram                  = LOG + "PROGRAM CHECK\\";
    public const string LogPRS                      = LOG + "PRS CHECK\\";

    public static string[] PROCESS_FILENAME = new string[] { "MAGAZINE.log", "INRAIL.log", "STRIPPICKER.log", "UNITPICKER.log", "WORKPICKER.log", "REJECT.log", "ULOADERPICKER.log" };
    public static string MARS_FILENAME = "MARS.log";

    public static string[] aPath_DEL_LOGs = {
                                                                LogAppEVENT, LogEVENT,LogEXCEPTION,LogImage,
                                                                LogLOGGING,LogProcMANUAL,LogMARS,LogMEASURE,
                                                                LogMovingERR,LogPCBDATA,LogPRINTMESSAGE,LogSPC,
                                                                LogSYSTEM,LogWARNING, ProcesInfoData, LogCount, LogVisionWriteFailData,
                                                                LogPROCESS, LogCycleTack, LogLotInfo, LogITSInfo, LogStripDefectCount, LogStripDefectLocationList, LogLotEnd, LogLotLogList,
                                                                LogBladeInfo, LogBarcodeHistory, LogITSBarcodeHistory, LogPCBUnitInfo, LogOneCycleTime,
                                                                StripData, DBLog, LogProgram, LogPRS
                                                            };
    public static string[] sPath_DEL_LOGs_ONE_MONTH = {

                                                            };


    public static void Make_Folders()
    {
        if (!Directory.Exists(ShareFolder)) Directory.CreateDirectory(ShareFolder);
        if (!Directory.Exists(ShareFolderTemp)) Directory.CreateDirectory(ShareFolderTemp);

        if (!Directory.Exists(PathStanderdRecipe)) Directory.CreateDirectory(PathStanderdRecipe);
        if (!Directory.Exists(PROJECT)) Directory.CreateDirectory(PROJECT);
        if (!Directory.Exists(DATA)) Directory.CreateDirectory(DATA);
        if (!Directory.Exists(VISION)) Directory.CreateDirectory(VISION);
        if (!Directory.Exists(SYSTEM)) Directory.CreateDirectory(SYSTEM);
        if (!Directory.Exists(InfoLABEL)) Directory.CreateDirectory(InfoLABEL);
        if (!Directory.Exists(MCLOG)) Directory.CreateDirectory(MCLOG);
        if (!Directory.Exists(LOG)) Directory.CreateDirectory(LOG);
        if (!Directory.Exists(ABF_LIST)) Directory.CreateDirectory(ABF_LIST);
        if (!Directory.Exists(InStrip)) Directory.CreateDirectory(InStrip);
        if (!Directory.Exists(LotStrip)) Directory.CreateDirectory(LotStrip);

        if (!Directory.Exists(LogSPC)) Directory.CreateDirectory(LogSPC);
        if (!Directory.Exists(LogEXCEPTION)) Directory.CreateDirectory(LogEXCEPTION);
        if (!Directory.Exists(LogEVENT)) Directory.CreateDirectory(LogEVENT);
        if (!Directory.Exists(LogMARS)) Directory.CreateDirectory(LogMARS);
        if (!Directory.Exists(LogLOGGING)) Directory.CreateDirectory(LogLOGGING);
        if (!Directory.Exists(LogAppEVENT)) Directory.CreateDirectory(LogAppEVENT);
        if (!Directory.Exists(LogMovingERR)) Directory.CreateDirectory(LogMovingERR);
        if (!Directory.Exists(LogCheckIO)) Directory.CreateDirectory(LogCheckIO);
        if (!Directory.Exists(LogProcMANUAL)) Directory.CreateDirectory(LogProcMANUAL);
        if (!Directory.Exists(LogImage)) Directory.CreateDirectory(LogImage);
        if (!Directory.Exists(IF)) Directory.CreateDirectory(IF);
        if (!Directory.Exists(PPID)) Directory.CreateDirectory(PPID);
        if (!Directory.Exists(LogLotInfo)) Directory.CreateDirectory(LogLotInfo);
        if (!Directory.Exists(LogITSInfo)) Directory.CreateDirectory(LogITSInfo);
        if (!Directory.Exists(LogStripDefectCount)) Directory.CreateDirectory(LogStripDefectCount);
        if (!Directory.Exists(LogStripDefectLocationList)) Directory.CreateDirectory(LogStripDefectLocationList);
        if (!Directory.Exists(LogLotEnd)) Directory.CreateDirectory(LogLotEnd);
        if (!Directory.Exists(LogLotLogList)) Directory.CreateDirectory(LogLotLogList);
        if (!Directory.Exists(LogBladeInfo)) Directory.CreateDirectory(LogBladeInfo);
        if (!Directory.Exists(LogBarcodeHistory)) Directory.CreateDirectory(LogBarcodeHistory);
        if (!Directory.Exists(LogITSBarcodeHistory)) Directory.CreateDirectory(LogITSBarcodeHistory);
        if (!Directory.Exists(LogPCBUnitInfo)) Directory.CreateDirectory(LogPCBUnitInfo);

        if (!Directory.Exists(StripData))   Directory.CreateDirectory(StripData);
        if (!Directory.Exists(DBLog))       Directory.CreateDirectory(DBLog);
        if (!Directory.Exists(LogProgram))  Directory.CreateDirectory(LogProgram);
        if (!Directory.Exists(LogPRS))      Directory.CreateDirectory(LogPRS);
    }
}