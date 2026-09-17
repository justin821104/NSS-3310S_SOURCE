using Object;
using System.Threading;

namespace NSS_3310S
{
    #region >>DATA MEMORY
    public class S : DATA_
    {
        public static readonly int ErrTrace                     = 0;    // 에러추적용
        public static readonly int PgmBeginTime                 = 1;    // 프로그램 실행 시간
        public static readonly int MasLog                       = 2;    // 마스 로그
        public static readonly int Gem                          = 3;    // MES 메세지
        public static readonly int DeviceMessage                = 4;    // 디바이스 메세지
        public static readonly int ManualMessage                = 5;    // 메뉴얼 메세지
        public static readonly int SystemMessage                = 6;    // 시스템 파라 메세지
        public static readonly int MotorMessage                 = 7;    // 모터 메세지
        public static readonly int VisionMessage                = 8;    // 비전 메세지
        public static readonly int SawRecipeList                = 9;    // SAW 설비 RECIPE LIST
        public static readonly int VisionRecipeList             = 10;   // VISION RECIPE LIST
        public static readonly int SawRecieveMessage            = 11;   // [SAW] UDP RECIEVE MESSAGE 
        public static readonly int VisionRecieveMessage         = 12;   // [VISION] UDP RECIEVE MESSAGE
        public static readonly int RecieverErrMessage           = 13;   // UDP RECIEVER ERROR MESSAGE
        public static readonly int EmptyMessage                 = 14;   // 빈트레이 클래스 메세지
        public static readonly int MgzMessage                   = 15;   // 매거진 클래스 메세지
        public static readonly int GrpMessage                   = 16;   // 그리퍼 클래스 메세지
        public static readonly int StripPkMessage               = 17;   // 스트립 피커 클래스 메세지
        public static readonly int UnitPkMessage                = 18;   // 유닛 피커 클래스 메세지
        public static readonly int ReWorkTrayMessage            = 19;   // RE-WORK 클래스 메세지
        public static readonly int TrayPkMessage                = 20;   // 트레이 피커 클래스 메세지

        public static void Label(){
            mSName[ErrTrace]                                    = "ERR TRACE";
            mSName[PgmBeginTime]                                = "PROGRAM BEGIN TIME";
            mSName[MasLog]                                      = "MAS LOG";
            mSName[Gem]                                         = "GEM LOG";
            mSName[DeviceMessage]                               = "DEVICE MESSAGE";
            mSName[ManualMessage]                               = "MANUAL MESSAGE";
            mSName[SystemMessage]                               = "SYSTEM MESSAGE";
            mSName[MotorMessage]                                = "MOTOR MESSAGE";
            mSName[VisionMessage]                               = "VISION MESSAGE";
            mSName[SawRecipeList]                               = "SAW RECIPE LIST";
            mSName[VisionRecipeList]                            = "VISION RECIPE LIST";
            mSName[SawRecieveMessage]                           = "SAW RECIEVE LOG";
            mSName[VisionRecieveMessage]                        = "VISION RECIEVE LOG";
            mSName[RecieverErrMessage]                          = "RECIEVER ERR MSG";
            mSName[EmptyMessage]                                = "EMPTY MESSAGE";
            mSName[MgzMessage]                                  = "MGZ MESSAGE";
            mSName[GrpMessage]                                  = "GRIPPER MESSAGE";
            mSName[StripPkMessage]                              = "STRIP PK MESSAGE";
            mSName[UnitPkMessage]                               = "UNIT PK MESSAGE";
            mSName[ReWorkTrayMessage]                           = "REWORK TRAY MESSAGE";
            mSName[TrayPkMessage]                               = "TRAY PK MESSAGE";
        }
    } //STRING형 DEFINE

    public class D : DATA_
    {
        public static readonly int CycleTime                    = 0;    // ONE CYCLE. //STRIP PLACE -> UNIT PICK-UP CYCLE-TIME
        public static readonly int UPH                          = 1;    // UPH 시간 
        public static readonly int PkgUPH                       = 2;    // 자재 개별 로딩시간
        public static readonly int Tact_Avg                     = 3;    // 평균 택타임
        public static readonly int RunRate                      = 4;    // 런-가동율 
        public static readonly int MemoryCapa                   = 5;    // 메모리 사용량
        public static readonly int CpuCapa                      = 6;    // CPU 사용량
        public static readonly int MemoryCapa_Load              = 7;    // 초기 메모리 사용량
        public static readonly int CPUSpeed                     = 8;    // CPU 속도 'QueryPerformanceFrequency
        public static readonly int CPUClock                     = 9;    // CPU CLOCK
        public static readonly int ManRunTime                   = 10;   // 매뉴얼 동작시간
        public static readonly int ManualRepeat_Interval        = 11;   // 매뉴얼 반복동작 대기시간
        public static readonly int MGZCycle                     = 12;   // 매거진 구동 사이클 타임
        public static readonly int EmptyCycle                   = 13;   // 빈트레이 구동 사이클 타임
        public static readonly int GripperCycle                 = 14;   // 그리퍼 구동 사이클  타임
        public static readonly int StripPkCycle                 = 15;   // 스트립 피커 구동 사이클 타임
        public static readonly int UnitPkCycle                  = 16;   // 유닛 피커 구동 사이클 타임
        public static readonly int Stage1Cycle                  = 17;   // 맵-블록 테이블1 구동 사이클 타임
        public static readonly int Stage2Cycle                  = 18;   // 맵-블록 테이블2 구동 사이클 타임
        public static readonly int HD1Cycle                     = 19;   // 헤드1 구동 사이클 타임
        public static readonly int HD2Cycle                     = 20;   // 헤드2 구동 사이클 타임 
        public static readonly int TrayCycle                    = 21;   // GOOD 트레이 구동 사이클 타임
        public static readonly int Tray3Cycle                   = 23;   // REWORK 트레이 구동 사이클 타임
        public static readonly int TrayPkCycle                  = 24;   // 트레이 피커 구동 사이클 타임
        public static readonly int PreAlignX                    = 25;   // 프리-얼라인 OFFSET X
        public static readonly int PreAlignY                    = 26;   // 프리-얼라인 OFFSET Y
        public static readonly int PreAlignT                    = 27;   // 프리-얼라인 OFFSET T
        public static readonly int PkCal_OffsetX                = 28;   // 피커 CAL' OFFSET X
        public static readonly int PkCal_OffsetY                = 29;   // 피커 CAL' OFFSET Y
        public static readonly int PkCal_OffsetT                = 30;   // 피커 CAL' OFFSET T
        public static readonly int Stage1UnitOffsetX            = 31;   // 유닛 얼라인 OFFSET X
        public static readonly int Stage1UnitOffsetY            = 32;   // 유닛 얼라인 OFFSET Y
        public static readonly int Stage1UnitOffsetT            = 33;   // 유닛 얼라인 OFFSET T
        public static readonly int Stage2UnitOffsetX            = 34;   // 유닛 얼라인 OFFSET X
        public static readonly int Stage2UnitOffsetY            = 35;   // 유닛 얼라인 OFFSET Y
        public static readonly int Stage2UnitOffsetT            = 36;   // 유닛 얼라인 OFFSET T
        public static readonly int UnitPk_PicOffsetX            = 37;   // 유닛 피커 픽업 옵셋 
        public static readonly int StripPlcTime                 = 38;   // 다이싱 테이블에 스트립 내려놓은 시간
        public static readonly int UnitPicTime                  = 39;   // 다이싱 테이블에서 유닛 픽업한 시간

        public static readonly int UPH_                         = 40;

        #region ARRAY
        public static int[] UnitOffsetX                         = { Stage1UnitOffsetX, Stage2UnitOffsetX };
        public static int[] UnitOffsetY                         = { Stage1UnitOffsetY, Stage2UnitOffsetY };
        public static int[] UnitOffsetT                         = { Stage1UnitOffsetT, Stage2UnitOffsetT };
        public static int[] StageTack                           = { Stage1Cycle, Stage2Cycle };
        #endregion

        public static void Label(){
            mDName[CycleTime]                                   = "Oen CycleTime";
            mDName[UPH]                                         = "UPH";
            mDName[PkgUPH]                                      = "PACKAGE UPH";
            mDName[Tact_Avg]                                    = "TACT AVERAGE";
            mDName[RunRate]                                     = "RUN RATE";
            mDName[MemoryCapa]                                  = "MEMORY CAPACITY";
            mDName[CpuCapa]                                     = "CPU CAPACITY";
            mDName[MemoryCapa_Load]                             = "LOAD MEMORY CAPACITY";
            mDName[CPUSpeed]                                    = "CPU SPEED";
            mDName[CPUClock]                                    = "CPU CLOCK";
            mDName[ManRunTime]                                  = "MANUAL RUN TIME";
            mDName[ManualRepeat_Interval]                       = "MANUAL REPEAT DELAY";
            mDName[MGZCycle]                                    = "MGZ CYCLE-TIME";
            mDName[EmptyCycle]                                  = "EMPTY CYCLE-TIME";
            mDName[GripperCycle]                                = "GRIPPER CYCLE-TIME";
            mDName[StripPkCycle]                                = "STRIP PK CYCLE-TIME";
            mDName[UnitPkCycle]                                 = "UNIT PK CYCLE-TIME";
            mDName[Stage1Cycle]                                 = "STAGE1 CYCLE-TIME";
            mDName[Stage2Cycle]                                 = "STAGE2 CYCLE-TIME";
            mDName[HD1Cycle]                                    = "HD1 CYCLE-TIME";
            mDName[HD2Cycle]                                    = "HD2 CYCLE-TIME";
            mDName[TrayCycle]                                   = "GOOD TRAY CYCLE-TIME";
            mDName[Tray3Cycle]                                  = "REWORK TRAY CYCLE-TIME";
            mDName[TrayPkCycle]                                 = "TRAY PK CYCLE-TIME";
            mDName[PreAlignX]                                   = "PRE-ALIGN OFFSET X";
            mDName[PreAlignY]                                   = "PRE-ALIGN OFFSET Y";
            mDName[PreAlignT]                                   = "PRE-ALIGN OFFSET T";
            mDName[PkCal_OffsetX]                               = "PICKER CAL OFFSET X";
            mDName[PkCal_OffsetY]                               = "PICKER CAL OFFSET Y";
            mDName[PkCal_OffsetT]                               = "PICKER CAL OFFSET T";
            mDName[Stage1UnitOffsetX]                           = "STAGE1 UNIT OFFSET X";
            mDName[Stage1UnitOffsetY]                           = "STAGE1 UNIT OFFSET Y";
            mDName[Stage1UnitOffsetT]                           = "STAGE1 UNIT OFFSET T";
            mDName[Stage2UnitOffsetX]                           = "STAGE2 UNIT OFFSET X";
            mDName[Stage2UnitOffsetY]                           = "STAGE2 UNIT OFFSET Y";
            mDName[Stage2UnitOffsetT]                           = "STAGE2 UNIT OFFSET T";
            mDName[UnitPk_PicOffsetX]                           = "UNIT PICKER PICK-UP OFFSET";
        }
    } //DOUBLE형 DEFINE

    public class L : DATA_
    {
        public static readonly int OffNumber                    = 0;    // 자동 OFF 할 스위치 번호(OP PANAL 스위치)
        public static readonly int TimeIntrkChk                 = 1;    // 정지 상태에서 인터록 조건 변경 확인을 위한 버퍼
        public static readonly int Time_100                     = 2;    // 프로그램 시작 후 계수시간 (100 msec)
        public static readonly int TmrPowerOn                   = 3;    // 전원 ON 누름 지연 시간 
        public static readonly int TmrPowerOff                  = 4;    // 전원 OFF 누름 지연 시간
        public static readonly int TowerLampFlog                = 5;    // 타워램프 상태 확인 플러그
        public static readonly int ErrorNumFlog                 = 6;    // 에러 발생 번호 
        public static readonly int CheckDoorSkipTime            = 7;    // 설비 RUN 진행 중 DOOR 스킵 상태 확인 시간.
        public static readonly int SeletTower                   = 9;    // 
        public static readonly int StripCnt                     = 11;   // 로딩 스트립 수량
        public static readonly int GoodCnt                      = 12;   // GOOD CHIP 수량
        public static readonly int ReworkCnt                    = 13;   // REWORK CHIP 수량
        public static readonly int NGCnt                        = 14;   // NG CHIP 수량
        public static readonly int GoodTrayCnt                  = 15;   // GOOD TRAY 수량
        public static readonly int ReworkTrayCnt                = 16;   // REWORK TRAY 수량
        public static readonly int EmptyTrayCnt                 = 17;   // EMPTY TRAY 수량
        public static readonly int LotCnt                       = 18;   // LOT 수량
        public static readonly int InCnt                        = 19;	// CHIP PICK-UP 총수량
        public static readonly int OutCnt                       = 20;   // CHIP PLACE 총수량
        public static readonly int UnitSizeNgCnt                = 21;   // 유닛 사이즈 검사 NG 수량
        public static readonly int UnitCnt                      = 22;   // 유닛 피커 saw에서 픽업 한 수량
        public static readonly int uiTray                       = 23;   // MMI TRAY STATUS DISPLAY
        public static readonly int CurSlotCount                 = 24;   // 현재 슬롯 번호
        public static readonly int WorkingTray                  = 25;   // 현재 작업 트레이 (0:GT1/1:GT2/2:NG)
        public static readonly int Stage1Reverse                = 26;   // 현재 셋팅되어 있는 테이블1 맵-핑 모드 (0:홀(ODD)/1:짝(EVEN)/2:전부(FULL))
        public static readonly int Stage2Reverse                = 27;   // 현재 셋팅되어 있는 테이블2 맵-핑 모드 (0:홀(ODD)/1:짝(EVEN)/2:전부(FULL))
        public static readonly int CurWorkXPic                  = 28;   // 현재 픽업 진행 헤드
        public static readonly int CurWorkStage                 = 29;   // 현재 픽업 작업 중인 테이블
        public static readonly int CurWorkTray                  = 30;   // 현재 플레이스 작업 중인 트레이
        public static readonly int CurWorkXPlc                  = 31;   // 현재 플레이스 진행 피커
        public static readonly int Stage1TackNow                = 32;
        public static readonly int Stage1TackEnd                = 33;
        public static readonly int Stage2TackNow                = 34;
        public static readonly int Stage2TackEnd                = 35;
        public static readonly int GoodTrayTackNow              = 36;
        public static readonly int GoodTrayTackEnd              = 37;
        public static readonly int ITSCount                     = 38;
        public static readonly int PowerSwitchOnDelayTime       = 39;
        public static readonly int PowerSwitchOffDelayTime      = 40;
        //24시간 기준 생산량 확인 
        public static readonly int DayMGZCnt                    = 41;
        public static readonly int DayStripCnt                  = 42;
        public static readonly int DayGoodUnit                  = 43;
        public static readonly int DayReworkUnit                = 44;
        public static readonly int DayRejectUnit                = 45;

        public static readonly int CurStagePlcGoodChip          = 46; // 현재 작업테이블에서 픽업 후 GOOD 플레이스하는 테이블 번호
        public static readonly int CurStagePlcNgChip            = 47; // 현재 작업테이블에서 픽업 후 NG 플레이스하는 테이블 번호
        public static readonly int CurStagePlcReworkChip        = 48; // 현재 작업테이블에서 픽업 후 REWORK 플레이스하는 테이블 번호

        public static readonly int Stage1_Unit                  = 50;
        public static readonly int Stage1_ITS                   = 51;
        public static readonly int Stage1_Good                  = 52;
        public static readonly int Stage1_NG                    = 53;
        public static readonly int Stage1_XOut                  = 54;

        public static readonly int Stage2_Unit                  = 55;
        public static readonly int Stage2_ITS                   = 56;
        public static readonly int Stage2_Good                  = 57;
        public static readonly int Stage2_NG                    = 58;
        public static readonly int Stage2_XOut                  = 59;

        public static readonly int Stage1NgCount                = 60; // 사이즈 ng - reject box로 버리는 옵션 상태면 사이즈 ng 몇개인지 확인 버퍼
        public static readonly int Stage2NgCount                = 61;

        public static readonly int PicCnt = 62;
        public static readonly int PlcCnt = 63;

        #region ARRAY
        public static int[] Production                          = { LotCnt, StripCnt, UnitCnt, GoodCnt, ReworkCnt, NGCnt, GoodTrayCnt, ReworkTrayCnt, EmptyTrayCnt, InCnt, OutCnt, ITSCount };
        public static int[] ReverseMode                         = { Stage1Reverse, Stage2Reverse };
        public static int[] StageTackNow                        = { Stage1TackNow, Stage2TackNow };
        public static int[] StageTackEnd                        = { Stage1TackEnd, Stage2TackEnd };

        public static int[] StageUnit                           = { Stage1_Unit, Stage2_Unit };
        public static int[] Stage_ITS                           = { Stage1_ITS, Stage2_ITS };
        public static int[] StageUnitGood                       = { Stage1_Good, Stage2_Good };
        public static int[] StageUnitNG                         = { Stage1_NG, Stage2_NG };
        public static int[] StageUnitXOut                       = { Stage1_XOut, Stage2_XOut };

        public static int[] StageNgCount                        = { Stage1NgCount, Stage2NgCount };
        #endregion

        public static void Label(){
            mLName[OffNumber]                                   = "OP OFF NUMBER";
            mLName[TimeIntrkChk]                                = "STOP INTERLOCK CHECK BUFFER";
            mLName[Time_100]                                    = "TIME 100";
            mLName[TmrPowerOn]                                  = "POWER ON DELAY";
            mLName[TmrPowerOff]                                 = "POWER OFF DELAY";
            mLName[TowerLampFlog]                               = "TOWER LAMP FLOG";
            mLName[ErrorNumFlog]                                = "ERROR NUMBER FLOG";
            mLName[CheckDoorSkipTime]                           = "DOOR SKIP DELAY TIME";
            mLName[SeletTower]                                  = "SELECT TOWER LAMP";
            mLName[StripCnt]                                    = "LOADING COUNT";
            mLName[GoodCnt]                                     = "GOOD COUNT";
            mLName[ReworkCnt]                                   = "REWORK COUNT";
            mLName[NGCnt]                                       = "NG COUNT";
            mLName[GoodTrayCnt]                                 = "GOOD TRAY COUNT";
            mLName[ReworkTrayCnt]                               = "REWORK TRAY COUNT";
            mLName[EmptyTrayCnt]                                = "EMPTY TRAY COUNT";
            mLName[LotCnt]                                      = "LOT COUNT";
            mLName[InCnt]                                       = "CHIP PIC COUNT";
            mLName[OutCnt]                                      = "CHIP PLACE COUNT";
            mLName[UnitSizeNgCnt]                               = "UNIT SIZE NG COUNT";
            mLName[UnitCnt]                                     = "UNIT PICKER PICUP COUNT";
            mLName[uiTray]                                      = "TRAY STATE VIEW";
            mLName[CurSlotCount]                                = "CURRENT MGZ SLOT COUNT";
            mLName[WorkingTray]                                 = "CURRENT TRAY INFO";
            mLName[Stage1Reverse]                               = "CURRENT STAGE1 INFO";
            mLName[Stage2Reverse]                               = "CURRENT STAGE2 INFO";
            mLName[CurWorkXPic]                                 = "CURRENT PIC HEAD INFO";
            mLName[CurWorkStage]                                = "CURRENT PIC STAGE INFO";
            mLName[CurWorkTray]                                 = "CURRENT PLC TRAY INFO";
            mLName[CurWorkXPlc]                                 = "CURRENT PLC HEAD INFO";
            mLName[Stage1TackNow]                               = "STAGE1 TACK START";
            mLName[Stage1TackEnd]                               = "STAGE1 TACK END";
            mLName[Stage2TackNow]                               = "STAGE2 TACK START";
            mLName[Stage2TackEnd]                               = "STAGE2 TACK END";
            mLName[GoodTrayTackNow]                             = "OK TRAY TACK START";
            mLName[GoodTrayTackEnd]                             = "OK TRAY TACK START";
            mLName[ITSCount]                                    = "ITS COUNT";

            mLName[PowerSwitchOnDelayTime]                      = "POWER ON PUSH DELAY TIME";
            mLName[PowerSwitchOffDelayTime]                     = "POWER OFF PUSH DELAY TIME";

            mLName[DayMGZCnt]                                   = "TODAY MAGAZINE ADD COUNT";
            mLName[DayStripCnt]                                 = "TODAY STRIP ADD COUNT";
            mLName[DayGoodUnit]                                 = "TODAY GOOD UNIT ADD COUNT";
            mLName[DayReworkUnit]                               = "TODAY REWORK UNIT ADD COUNT";
            mLName[DayRejectUnit]                               = "TODAY REJECT UNIT ADD COUNT";
        }

        public static void Reset(){
            if (Production == null) return;
            for (int i = 0; i < Production.Length; i++) { 
                IsLONG[Production[i]] = 0; 
            }
        }

        public static void ResetStageUnitInfo(eMAP_BLOCK eStage){
            LogWR_.DEBUG_PRINT(eStage.ToString() + " START");
            IsLONG[StageUnit[(int)eStage]]      = 0;
            IsLONG[Stage_ITS[(int)eStage]]      = 0;
            IsLONG[StageUnitGood[(int)eStage]]  = 0;
            IsLONG[StageUnitNG[(int)eStage]]    = 0;
            IsLONG[StageUnitXOut[(int)eStage]]  = 0;
        }
    } //LONG형 DEFINE

    public class B : DATA_
    {
        public static readonly int SawManualEvent               = CNT_.Memory - 1;  // SAW EVENT
        public static readonly int VisionManalEvent             = CNT_.Memory - 2;  // VISION EVENT

        public static readonly int WorkEndStop                  = 0;    // 투입 정지
        public static readonly int HomeStop                     = 1;    // HOMING STOP S/W 눌림(기억)
        public static readonly int ToolDataPause                = 2;    // 1초마다 저장 일시 정지
        public static readonly int InitFail                     = 3;    // 초기화 실패
        public static readonly int JobMiss                      = 4;    // JOB 파일 Miss
        public static readonly int Simulation                   = 5;    // 시뮬레이션 모드
        public static readonly int Arear                        = 6;    // 에어리어 센서 감지됨.
        public static readonly int Air                          = 7;    // 장비 에어 센서 감지됨.
        public static readonly int CPTrip                       = 8;    // CP Trip 감지됨.
        public static readonly int StopLoading                  = 9;    // 투입 정지
        public static readonly int SystemMSG                    = 10;   // 시스템 메세지 (FRMWarning 메세지) 
        public static readonly int Runrate_Each                 = 11;   // 개별 가동율 모드
        public static readonly int EndINITIAL                   = 12;   // 초기화 완료 비트
        public static readonly int StartEdge                    = 13;   // start switch on edge
        public static readonly int InitialEdge                  = 14;   // initial switch on edge
        public static readonly int HWJogRun                     = 15;   // 외부 스위치로 조그 이동 중인지 확인 비트
        public static readonly int TENKEY_JOG                   = 16;   // TENKEY JOG 이송 플러그
        public static readonly int MachineWaitProduct           = 17;   // 자재 없음 워닝
        public static readonly int PowerOnEdge                  = 18;   // POWER ON 엣지 (n초)
        public static readonly int PowerOffEdge                 = 19;   // POWER OFF 엣지 (n초)
        public static readonly int UnitEdgeInspection           = 20;   // 마크 검사시 UNIT 엣지 검사 경우. 
        public static readonly int Dry                          = 21;   // 더이상 투입 안하고 설비 안에 있는 소재 모두 배출.
        public static readonly int ChkEXE                       = 22;   // 프로그램 다중 실행 확인 플러그
        public static readonly int TowerLampFlog                = 23;   // 타워램프 상태 플러그
        public static readonly int LoadingStop                  = 24;   // STRIP LOADING STOP
        public static readonly int UnloadingStop                = 25;   // STRIP UNLOADING STOP
        public static readonly int WriteLog_UDP                 = 26;   // UDP DATA LOG WRITE
        public static readonly int PkrPickUpStop                = 27;   // 헤드 피커 픽업 정지
        public static readonly int PkrPlaceStop                 = 28;   // 헤드 피커 플레이스 후 일지 정지
        public static readonly int SawRecipeOpen                = 29;   // 다이싱 레스피 OPEN 상태 CHECK
        public static readonly int VisionRecipeOpen             = 30;   // 비전 레스피 OPEN 상태 CHECK
        public static readonly int StripPlacStop                = 31;   // 다이싱 스트립 공급 일시 정지
        public static readonly int UnitPickupStop               = 32;   // 다이싱 유닛 픽업 일시 정지
        public static readonly int PkrUnitPickUpStop            = 33;   // 피커 유닛 픽업 일시 정지
        public static readonly int PkrUnitPlaceStop             = 34;   // 피커 유닛 플레이스 일시 정지
        public static readonly int TrayStop                     = 35;   // 트레이 공급 일시 정지
        public static readonly int NotCheckCleanWater           = 36;   // 클린 WATER 인터락 CHECK 
        public static readonly int LotEnd                       = 37;   // LOT-END
        public static readonly int SkipDoorLock                 = 39;
        public static readonly int ElvLDLocation                = 40;   // 엘리베이터YZ축 로딩 위치
        public static readonly int ElvULDLocation               = 41;   // 엘리베이터 YZ축 언로딩 위치
        public static readonly int ElvStripLoading              = 42;   // 엘리베이터 YZ축 스트립 로딩 위치
        public static readonly int LotStart_KitCleanning        = 43;   // lot첫 시작 kit클린 진행 플로그
        public static readonly int KitCleaning                  = 44;
        
        //시퀸스 비트 메모리 !
        public static readonly int MGZWorking                   = 50; // 엘리베이터 매거진 작업 진행
        public static readonly int CstRequest                   = 51;   // 매거진 공급/배출 상태 확인 
        public static readonly int InRailRequest                = 52;   // 그리퍼 스트립 공급 요청
        public static readonly int GripperWorking               = 53;   // 그리퍼 작업 진행 플로그
        public static readonly int StripPkRequest               = 54;   // 스트립 피커 스트립 픽업 요청
        public static readonly int StripPkMask                  = 55;   // 스트립 피커 스트립 픽업 대기
        public static readonly int UnitPkMask                   = 56;
        public static readonly int StripPkPlc                   = 57;   // 스트립 피커 플레이스 작업 중
        public static readonly int UnitPkPic                    = 58;   // 유닛 피커 픽업 작업 중
        public static readonly int PreAligning                  = 59;   // 프리 얼라인 작업 중
        public static readonly int StripPickUp                  = 60;   // 스트립 픽업 작업 진행
        public static readonly int StripPlace                   = 61;   // 스트립 플레이스 작업 진행
        public static readonly int UnitPickUp                   = 62;   // 유닛 픽업 작업 진행
        public static readonly int UnitPlace                    = 63;   // 유닛 플레이스 작업 진행
        public static readonly int Stage1_UnitReceive           = 64;   // 맵-블록 테이블1 유닛 공급 대기
        public static readonly int Stage2_UnitReceive           = 65;   // 맵-블록 테이블2 유닛 공급 대기
        public static readonly int Stage1Working                = 66;   // 맵-블록 테이블1 작업 진행
        public static readonly int Stage2Working                = 67;   // 맵-블록 테이블2 작업 진행
        public static readonly int Stage1Inspection             = 68;   // 맵-블록 테이블1 유닛 검사 진행
        public static readonly int Stage2Inspection             = 69;   // 맵-블록 테이블2 유닛 검사 진행
        public static readonly int Stage1Busy                   = 70;   // 맵-블록 테이블1 유닛 픽업 작업  
        public static readonly int Stage2Busy                   = 71;   // 맵-블록 테이블2 유닛 픽업 작업 
        public static readonly int Stage1Pic                    = 72;   // 맵-블록 테이블1 픽업 작업 진행 
        public static readonly int Stage2Pic                    = 73;   // 맵-블록 테이블2 픽업 작업 진행 
        public static readonly int Stage1PickUp                 = 74;   // 맵-블록 테이블1 픽업 중 home 작업 진행하여 중간부터 재픽업
        public static readonly int Stage2PickUp                 = 75;   // 맵-블록 테이블2 픽업 중 home 작업 진행하여 중간부터 재픽업
        public static readonly int StageAirshowerWait           = 76;   // 맵-블록 에어샤워 작업 일시 정지 
        public static readonly int Stage1JobCancel              = 77;   // 테이블 1 작업 취소 플로그
        public static readonly int Stage2JobCancel              = 78;   // 테이블 2 작업 취소 플로그
        public static readonly int Stage1UnitExist              = 79;   // 테이블1 유닛 완료 후 유닛 존재 여부
        public static readonly int Stage2UnitExist              = 80;   // 테이블2 유닛 완료 후 유닛 존재 여부
        public static readonly int EmptyStackerSupply           = 81;   // 빈-트레이 공급 확인
        public static readonly int EmptyTrayPicRequest          = 82;   // 빈-트레이 트레이 피커 전달 플로그 
        public static readonly int GoodTray1TrayRequest         = 83;   //GOOD TRAY1 트레이 투입 요청
        public static readonly int GoodTray2TrayRequest         = 84;   //GOOD TRAY2 트레이 투입 요청
        public static readonly int ReWorkTrayRequest            = 85;   //REWORK 트레이 투입 요청
        public static readonly int GoodTray1Loading             = 86;   // GOOD TRAY1 트레이 로딩 진행 플로그
        public static readonly int GoodTray2Loading             = 87;   // GOOD TRAY2 트레이 로딩 진행 플로그
        public static readonly int GoodTray1Place               = 88;   // GOOD TRAY1 플레이스 작업 진행
        public static readonly int GoodTray2Place               = 89;   // GOOD TRAY2 플레이스 작업 진행
        public static readonly int GoodTrayWork                 = 90;   // GOOD TRAY UNIT 플레이스 작업 진행
        public static readonly int GoodTray1Unloading           = 91;
        public static readonly int GoodTray2Unloading           = 92;
        public static readonly int GoodTray1ULDConv             = 93;   // good tray1 트레이 콘베어로 배출 진행
        public static readonly int GoodTray2ULDConv             = 94;   // good tray2 트레이 콘베어로 배출 진행
        public static readonly int GoodTray1ULDEnd              = 95;   // GOOD TRAY1 콘베어 언로딩 완료 플로그
        public static readonly int GoodTray2ULDEnd              = 96;   // GOOD TRAY2 콘베어 언로딩 완료 플로그
        public static readonly int ReWorkTrayLoading            = 97;   // REWORK TRAY 트레이 로딩 진행 플로그
        public static readonly int ReWorkTrayWork               = 98;   // REWORK TRAY UNIT 플레이스 작업 진행
        public static readonly int GoodTrayUnloadingMode        = 99;   // AUTO 진행 중 GOOD TRAY 언로딩 선택
        public static readonly int GoodTrayAutoUnloading        = 100;   // 설비 런닝 중 good tray 언로딩
        public static readonly int ReWorkTrayUnloadingMode      = 101;   // AUTO 진행 중 REWORK TRAY 언로딩 선택
        public static readonly int ReWorkTrayStackerUldRequest  = 102;   // REWORK 트레이 스태커 배출 
        public static readonly int GoodTrayStackerUldRequest    = 103;   // GOOD 트레이 스태커 배출
        public static readonly int X1PicBusy                    = 104;   // X1 피커 픽업 진행 플로그
        public static readonly int X2PicBusy                    = 105;   // X2 피커 픽업 진행 플로그
        public static readonly int X1PRSBusy                    = 106;   // X1 PRS 진행 플로그
        public static readonly int X2PRSBusy                    = 107;  // X2 PRS 진행 플로그
        public static readonly int X1PlcBusy                    = 108;  // X1 피커 플레이스 진행 플로그
        public static readonly int X2PlcBusy                    = 109;  // X2 피커 플레이스 진행 플로그
        public static readonly int X1NGPlcBusy                  = 110;  // X1 피커 NG 유닛 플레이스 진행 플로그
        public static readonly int X2NGPlcBusy                  = 111;  // X2 피커 NG 유닛 플레이스 진행 플로그
        public static readonly int X1_NG                        = 112;
        public static readonly int X2_NG                        = 113;
        public static readonly int X1_REJECT                    = 114;
        public static readonly int X2_REJECT                    = 115;
        public static readonly int X1_INSPECTION                = 116;
        public static readonly int X2_INSPECTION                = 117;
        public static readonly int X1PkPlc                      = 118; // X1 피커 플레이스 중 HOME 작업 진행 하여 나머지 유닛 플레이스
        public static readonly int X2PkPlc                      = 119; // X2 피커 플레이스 중 HOME 작업 진행 하여 나머지 유닛 플레이스
        public static readonly int X1Working                    = 120;  // X1 Pic And Plc 작업 진행
        public static readonly int X2Working                    = 121;  // X2 Pic And Plc 작업 진행
        public static readonly int SawVacuumInterface           = 122; // 다이싱 UDP 통신 진공 부분 응답 확인 비트 SawIOInterface
        public static readonly int SawManualRun                 = 123; // SAW PROGRAM에서 HANDLER 메뉴얼 동작 실행 플러그
        public static readonly int VisionManualRun              = 124;
        public static readonly int TrayConveyorUnloadingWork    = 125;  // 굿트레이 배출 중
        public static readonly int ManualPkrAutoCalView         = 126;
        public static readonly int TrayPkPic                    = 127;
        public static readonly int TrayPkWorking                = 128;
        public static readonly int DirectUnitPlace              = 129;  // 유닛 피커 바로 맵블록에 내려놓음

        public const int Simulation_Sawing                      = 130;  // 시뮬레이션 구동 시 쏘잉 작업 진행~/

        public const int WaitLotEndProcessing                   = 131;  // lot end 진행 중!
        public const int WaitITSReading                         = 132;  // its 좌표계 리딩 중!
        #region ARRAY
        public static int[] PauseOption                         = { Dry, StripPlacStop, UnitPickupStop, PkrUnitPickUpStop, PkrUnitPlaceStop, TrayStop };
        public static int[] Stage_Receive                       = { Stage1_UnitReceive, Stage2_UnitReceive };
        public static int[] Stage_Work                          = { Stage1Working, Stage2Working };
        public static int[] UnitInspection                      = { Stage1Inspection, Stage2Inspection };
        public static int[] StageBusy                           = { Stage1Busy, Stage2Busy };
        public static int[] Stage_Pic                           = { Stage1Pic, Stage2Pic };
        public static int[] JobCancel                           = { Stage1JobCancel, Stage2JobCancel };
        public static int[] StageUnitExist                      = { Stage1UnitExist, Stage2UnitExist };
        public static int[] XPicBusy                            = { X1PicBusy, X2PicBusy };
        public static int[] XPRSBusy                            = { X1PRSBusy, X2PRSBusy };
        public static int[] XPlcBusy                            = { X1PlcBusy, X2PlcBusy };
        public static int[] NGPlcBusy                           = { X1NGPlcBusy, X2NGPlcBusy };
        public static int[] TrayRequest                         = { GoodTray1TrayRequest, GoodTray2TrayRequest, ReWorkTrayRequest };
        public static int[] TrayLoading                         = { GoodTray1Loading, GoodTray2Loading };
        public static int[] TrayPlc                             = { GoodTray1Place, GoodTray2Place };
        public static int[] TrayUldConv                         = { GoodTray1ULDConv, GoodTray2ULDConv };
        public static int[] WaitTrayUldConv                     = { GoodTray2ULDConv, GoodTray1ULDConv };
        public static int[] HD_NG                               = { X1_NG, X2_NG };
        public static int[] HD_REJECT                           = { X1_REJECT, X2_REJECT };
        public static int[] HD_INSPECTION                       = { X1_INSPECTION, X2_INSPECTION };
        public static int[] GoodTrayUldEnd                      = { GoodTray1ULDEnd, GoodTray2ULDEnd };//
        public static int[] GoodTrayUnlaidng                    = { GoodTray1Unloading, GoodTray2Unloading };//
        public static int[] XWorking                            = { X1Working, X2Working };
        #endregion

        public static void Label(){
            mBName[WorkEndStop]         = "WORK END";
            mBName[HomeStop]            = "HOME STOP";
            mBName[ToolDataPause]       = "";
            mBName[InitFail]            = "";
            mBName[JobMiss]             = "";
            mBName[Simulation]          = "";
            mBName[Arear]               = "";
            mBName[Air]                 = "";
            mBName[CPTrip]              = "";
            mBName[StopLoading]         = "";
            mBName[SystemMSG]           = "";
            mBName[Runrate_Each]        = "";
            mBName[EndINITIAL]          = "";
            mBName[StartEdge]           = "";
            mBName[InitialEdge]         = "";
            mBName[HWJogRun]            = "";
            mBName[TENKEY_JOG]          = "";
            mBName[MachineWaitProduct]  = "";
            mBName[PowerOnEdge]         = "";
            mBName[PowerOffEdge]        = "";
            mBName[UnitEdgeInspection]  = "";
            mBName[Dry]                 = "";
            mBName[ChkEXE]              = "";
            mBName[TowerLampFlog]       = "";
            mBName[LoadingStop]         = "";
            mBName[UnloadingStop]       = "";
            mBName[WriteLog_UDP]        = "";
            mBName[PkrPickUpStop]       = "";
            mBName[PkrPlaceStop]        = "";
            mBName[SawRecipeOpen]       = "";
            mBName[VisionRecipeOpen]    = "";
            mBName[StripPlacStop]       = "";
            mBName[UnitPickupStop]      = "";
            mBName[PkrUnitPickUpStop]   = "";
            mBName[PkrUnitPlaceStop]    = "";
            mBName[TrayStop]            = "";
            mBName[NotCheckCleanWater]  = "";
            mBName[LotEnd]              = "";
            mBName[SkipDoorLock]        = "";
            mBName[ElvLDLocation]       = "";
            mBName[ElvULDLocation]      = "";
            mBName[ElvStripLoading]     = "";

        }

        #region >> FUNCTION 
        public static void Initial(){
            for (int idx = 50; idx < CNT_.Memory; idx++) {
                IsBIT[idx] = false; //버퍼 초기화
            }
            bLotEnd         = false;
            bLotEndProcess  = false;
        }

        public static void Bit(int iThread, int nBIT, bool bSTATE, string comment){
            IsBIT[nBIT] = bSTATE;
            LogWR_.SaveMarsLog(iThread, eLogTYPE.FNC, comment + "_" + mBName[nBIT] + " = " + IsBIT[nBIT].ToString(), "BIT");
        }
        public static void SetBit(int iThread, int nBit, bool bState, string comment){
            if (bMF) return;
            IsBIT[nBit] = bState;
            LogWR_.SaveMarsLog(iThread, eLogTYPE.FNC, comment + "_" + mBName[nBit] + " = " + IsBIT[nBit].ToString(), "BIT");
        }
        public static void SetBit(int iThread, int nBit1, int nBit2, bool bState1, bool bState2, string comment){
            if (bMF) return;
            LogWR_.SaveMarsLog(iThread, eLogTYPE.FNC, comment, "BIT");
            IsBIT[nBit1] = bState1;
            IsBIT[nBit2] = bState2;
        }

        public static bool WaitBIT(int iTH, int nBIT, bool bSTS, string comment){
            if (bMF) return false;
            LogThread[iTH].sqeSTS = comment;
            Thread.Sleep(3);
            string s = "[" + nBIT.ToString() + "] " + mBName[nBIT] + " = " + bSTS.ToString() + " , " + comment + " = ";
            if (IsBIT[nBIT] == bSTS){
                LogThread[iTH].waitSTS = s + "WAIT";
                return true;
            }
            LogThread[iTH].waitSTS = s + "END";
            return false;
        }

        public static bool WaitBIT(int iTH, int nBIT1, int nBIT2, bool bSTS1, bool bSTS2, string comment, bool bAND){
            LogThread[iTH].sqeSTS = comment;
            Thread.Sleep(3);
            string s = "[" + nBIT1.ToString() + "] " + mBName[nBIT1] + " = " + bSTS1.ToString()
                        + "&&" + nBIT2.ToString() + "] " + mBName[nBIT2] + " = " + bSTS2.ToString()
                        + " , " + comment + " = ";

            if (bAND){
                if ((IsBIT[nBIT1] == bSTS1) && (IsBIT[nBIT2] == bSTS2)){
                    LogThread[iTH].waitSTS = s + "WAIT";
                    return true;
                }
            }
            else{
                if ((IsBIT[nBIT1] == bSTS1) || (IsBIT[nBIT2] == bSTS2)){
                    LogThread[iTH].waitSTS = s + "WAIT";
                    return true;
                }
            }
            LogThread[iTH].waitSTS = s + "END";
            return false;
        }
        public static bool WaitBIT(int iTH, int nBIT1, int nBIT2, int nBIT3, bool bSTS1, bool bSTS2, bool bSTS3, string comment, bool bAND){
            LogThread[iTH].sqeSTS = comment;
            Thread.Sleep(3);
            string s = "[" + nBIT1.ToString() + "] " + mBName[nBIT1] + " = " + bSTS1.ToString()
                        + "&&" + nBIT2.ToString() + "] " + mBName[nBIT2] + " = " + bSTS2.ToString()
                        + " , " + comment + " = ";

            if (bAND){
                if ((IsBIT[nBIT1] == bSTS1) && (IsBIT[nBIT2] == bSTS2) && (IsBIT[nBIT3] == bSTS3)){
                    LogThread[iTH].waitSTS = s + "WAIT";
                    return true;
                }
            }
            else{
                if ((IsBIT[nBIT1] == bSTS1) || (IsBIT[nBIT2] == bSTS2) || (IsBIT[nBIT3] == bSTS3)){
                    LogThread[iTH].waitSTS = s + "WAIT";
                    return true;
                }
            }
            LogThread[iTH].waitSTS = s + "END";
            return false;
        }
        #endregion << FUNCTION
    } //BOOL(BIT)형 DEFINE
    #endregion
}