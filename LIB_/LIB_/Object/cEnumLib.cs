using System;

namespace Object
{
    public enum ePARA{
        COM = 0,
        RECIPE = 1
    }

    public enum eGridDataViewPara{
        DELAY = 0,
        DATA = 1,
    }

    public enum eArrow{
        Right = 0,
        Down,
        Left,
        Up,
        RightUp,
        RightDown,
        LeftDown,
        LeftUp,
        rotRight,
        RotLeft
    }

    public enum uVAL{
        High = 1,
        Low = 0
    }

    public enum eSTATUS{
        NONE = 10,
        EMPTY = 0, //없음.
        MARK = 1, //GOOD.
        NG = 2, //NG (사이즈 불량)
        FAIL = 3, //불량 (X MARK)
        X_MARK = 3,//불량 (X MARK)

        PICFAIL = 4, //픽업 실패
        WORKING = 5, //작업 중
        WORKEND = 6, //작업 완료
        FIRST = 7, //INSPECTION 검사 유닛 
        SECOND = 8, //INSPECTION 검사 유닛
    }

    public enum eConv{
        STOP = 0,
        FWD = 1,
        BWD = 2
    }
    public enum eClamp{
        Lock = 0,
        Unlock = 1
    }

    public enum eMGZ_TYPE{
        FULL = 0,
        HALF,
        SMALL_HALF
    }

    public enum eLD_STRIP{
        AUTO = 0,
        MANUAL = 1
    }

    public enum eSTRIP{
        MGZ_HALF = 0,
        MGZ_QUARTER = 1,
        BOAT_HALF = 2,
        BOAT_QUARTER
    }

    public enum ePCB{
        STRIP = 0,
        QUAD = 1
    }

    public enum eUnitPK{
        Ch1 = 0,
        Ch2 = 1,
        All
    }
    public enum eSCRAP{
        Ch1 = 0,
        Ch2 = 1,
        //Ch3 = 2,
        All
    }

    public enum eREVERSE_TABLE{
        REVERSE1 = 0,
        REVERSE2 = 1
    }
    public enum eMAP_BLOCK{
        STAGE1 = 0,
        STAGE2 = 1,
        ALL = 2,
    }
    public enum eMAP_DATA{
        ODD = 0,        //홀방
        EVEN,       //짝방
        FULL   //전부
    }
    public enum eHD{
        HD1 = 0,
        HD2 = 1,
        ALL = 2,
    }
    public enum ePK{
        PKR1 = 0,
        PKR2 = 1,
        PKR3 = 2,
        PKR4 = 3,
        PKR5 = 4,
        PKR6 = 5,
        PKR7 = 6,
        PKR8 = 7
    }

    public enum eMAPBLOCK_TABLE{
        FULL = 0,
        HALF
    }

    public enum eTRAY{
        GOOD1 = 0,
        GOOD2 = 1,
        REWORK = 2,
        ALL = 3
    }
    public enum eGOOD_TRAY{
        GD1 = 0,
        GD2 = 1,
        ALL = 2,
    }
    public enum eULD_TRAY{
        CONVEYOR = 0,
        STACKER = 1,
    }

    public enum eScrapAlarm{
        USE = 0,
        NOT_USE = 1
    }

    public enum eScrapVacuum{
        NOT_USE = 0,
        USE = 1
    }

    public enum eTRIGGER{
        HD1 = 0,
        HD2 = 1,
        MARK = 2,
        BTM = 0
    }
    public enum eCLEANER{
        NOT = 0,
        WASH,
        DRY,
        RINSE,
    }

    public enum eCHK_INTERLOCK{
        FULL = 0,
        AUTO = 1,
        INITIAL = 2,
        MANUAL = 3,
        NOT
    }

    public enum eALL_INSPECTION{
        ALL = 0, //4포인트 및 전수
        POINT = 1, //4포인트
        FULL //전수
    }
    public enum eIMAGE_SAVE{
        ALL = 0,
        GOOD = 1,
        NG = 2,
        NOT = 3
    }

    public enum eUSE : int{
        NotUSE = 0,
        USE = 1,
    }
    public enum eXMARK{
        TRAY = 0,
        REJECT = 1,
    }

    public enum eSHIFT{
        NutSHIFT = 1,
        TwoSHIFT,
        ThreeSHIFT
    }

    public enum Baudrate{
        bps9600,
        bps14400,
        bps19200,
        bps38400,
        bps57600,
        bps115200,
    }

    #region "MACHINE ENUM"
    /// <summary>
    /// 장비 상태
    /// NONE        = 최초 상태
    /// WAITRUN     = 초기화 끝나고 런-대기 상태
    /// DRY         = 드라이-런 상태
    /// AUTO        = 런 상태
    /// USERSTOP    = 사용자가 STOP SW' 눌려서 정지 상태
    /// ERRSTOP     = 오토런 중 에러 발생 후 정지 상태
    /// EMSSTOP     = 비상 정지 눌려서 정지 상태
    /// READYSTOP   = 파워 공급 스위치를 누르면 발생되는 상태
    /// INITIAL     = 장비 초기화 진행 중인 상태
    /// </summary>
    [Flags]
    public enum eMachineStatus{
        NONE = 0,
        WAITRUN,
        DRY,
        AUTO,
        USERSTOP,
        ERRSTOP,
        EMSSTOP,
        READYSTOP,
        INITIAL
    }

    /// <summary>
    /// 로그인 레벨 상태
    /// Null    = null 상태
    /// OP      = 오퍼레이터 상태
    /// ENG     = 엔지니어 상태 (관리자)
    /// ADMIN   = 마스트 (메인급 관리자)
    /// SOFT    = 개발자
    /// </summary>
    [Flags]
    public enum eLogLevel{
        Null = -99,
        OP = 0,
        ENG,
        ADMIN,
        SOFT
    }

    /// <summary>
    /// 메인 스위치 눌린 상태
    /// Null    = null 상태
    /// LOGIN   = 로그인 스위치 눌린 상태
    /// AUTO = 오토창 스위치 눌린 상태
    /// MOTION = 모터 파라메타 티칭 스위치 눌린 상태
    /// VISION = 비전 파라메타 티칭 스위치 눌린 상태
    /// DEVICE = 레스피 변경 스위치 눌린 상태
    /// MANUALRUN = 메뉴얼-런 스위치 눌린 상태
    /// LOGVIEW = 로그 뷰어 스위치 눌린 상태
    /// IOVIEW = IO 뷰어 스위치 눌린 상태
    /// SETTING = 셋팅 파라메타 스위치 눌린 상태
    /// CALBRATION = 켈리브레이션 스위치 눌린 상태
    /// MAP - 맵핑 눌린 상태
    /// OPTION = 옵션 스위치 눌린 상태
    /// </summary>
    [Flags]
    public enum eMainLevel{
        Null = -99,

        LOGIN = 0,
        AUTO,
        MOTION,
        VISION,
        DEVICE,
        MANUALRUN,
        LOGVIEW,
        IOVIEW,
        SETTING,
        CALIBRATION,
        MAP,
        TENKEY,
        OPTION
    }

    /// <summary>
    /// 리미트 센서 상태
    /// </summary>
    [Flags]
    public enum eSEBSOR{
        senLimit_N = 0,
        senLimit_P,
        senHome
    }

    /// <summary>
    /// 위치 편차 상태 확인.
    /// </summary>
    [Flags]
    public enum eCOMP{
        Same = 0,
        Plus,
        Minus,
        NotHome
    }


    /// <summary>
    /// 함수 리터 값.
    /// </summary>
    [Flags]
    public enum eRTN{
        FAIL = 0,               // 구동 실패
        SUCESS,                 // 구동 성공
        NULL,                   // 결과 없음
        EMS,                    // 인터록 발생
        EXCEPTION,              // 예외발생 종료
        TIMEOVER,               // 결과 대기 시간 초과
        MOTION_LIB_FAIL,        // 모션 라이브러리 리턴값 실패
        THREAD_NOT_MATCH,       // 스레드 번호 불일치 (잘못된 함수 호출)
        PUSH_STOP,              // STOP 정지
        ERROR_STOP,             // 에러 정지 

        DOOR_OPEN = 10,         // 도어 열림
        PLUS_LIMIT,             // 플러스 리미트 감지
        MINUS_LIMIT,            // 마이너스 리미트 감지
        LOSE,                   // 유실됨
        MISTAKE,                // 오류,잘못됨
        NotMOVE,                // 이송 할 수 없음.
        JOB_CANCEL,             // 작업 취소

        BUSY = 30,
        TimeOver,   // 시간 초과
        NothingBarcode, // 바코드 정보 없음

        NotDataFile = 50,           // FILE 없음.
        ResponseOverTime,           // 응답시간 오버
        WrittingFail,               // 결과 쓰기 실패
        ReadingDataFail,            // 검사 결과 값 오류
        IndexFail,                  // 결과 인덱스 오버됨
        NGOverCnt,                  // INSPECTION NG OVER COUNT

        NotITSCountFile,    //ITS 수량 정보 FILE 없음
        NotITSLocationFile, //ITS 좌표 정보 FILE 없음
        FailITSCountDataParsingFail,
        FailITSLocationDataParsingFail,

        NOT_MODULE = 100,  // IO 모듈 번호 없음.
        ERR_OUT_MODULE,         // OUTPUT 모듈 통신 에러.
        ERR_IN_MODULE,          // INTPUT 모듈 통신 에러.

        NOT_STRIP_PK_PICKUP,    //스트립 피커 X축 픽업 위치가 아님
        NOT_STRIP_PK_PLACE,     //스트립 피커 X축 플레이스 위치가 아님
        CANCEL_SAW_STRIP_REQ,   //다이싱 테이블에서 스트립 로딩 요청 취소됨
        NOT_UNIT_PK_PICKUP,     //유닛 피커 X축 픽업 위치가 아님
        NOT_UNIT_PK_PLACE,      //유닛 피커 X축 플레이스 위치가 아님.
        CANCEL_SAW_UNIT_REQ,    //다이싱 테이블에서 유닛 픽업 배출 요청 취소됨
        NOT_MB1_RECEIVE,        //맵-블록1 유닛 받는 위치가 아님
        NOT_MB2_RECEIVE,        //맵-블록2 유닛 받는 위치가 아님
        NOT_TRAY_PK_PICKUP,     //
        NOT_TRAY_PK_PLACE,       //

        FAIL_PICKUP_DOWN,
        FAIL_PICKUP_UP,
        FAIL_PLACE_DOWN,
        FAIL_PLACE_UP,

        PRS_MATCH_FAIL,

        VANISH, // 사라짐

        NotLoadingMagazine,

        UnloadingStacker, // 

        AllPkrCalFail // 전체 피커 오토 cal 동작 실패
    }

    public enum eChkDelay{
        execute = 0,
        check,
        delay
    }

    public enum eBCR{
        TrackIn = 0,
        TrackOut
    }; //BCR CHANAL 
    public enum eBcrCompare{
        Mach = 0,
        NotMach,
        NoData,
        Duplicate,
        Count
    }; //BCR 결과 


    public enum eLogTYPE{
        PRS = 0,    //[PROCESS ] EVENT RECORD ABOUT PROCESS PROGRESS : PROCESS STANT/END
        TRN = 1,    //[TRANSFER] TRANSPORTATION OF PRODUCTS OR MATERIALS AMONG PROCESSES : MOTION
        FNC = 2,    //[FUNCTION] MINIMUM UNIT OF EQUIPMENT MOTIONS : FWD/BWD, UP/DOWN, ON/OFF, PUSH/PULL, BARCODE, etc,,
        EVT = 3,    //[EVENT   ] SIMPLE SET VALUE THAT DOENS NOT HAVE START/END : TEMPERATURE SET, PID, SET etc
        ALM = 4     //[ALARM   ] LOG OF ALARM THAT RINGS 
    }

    #endregion "MACHINE ENUM"

    //public enum eALIGN_RESULT
    //{
    //    NONE,
    //    SUCCESS,
    //    PASS,
    //    RETRY,
    //    REMOVE,
    //}

    #region "VISION ENUM"
    public enum InspectionMode{
        Step = 0,
        Flying = 1
    }

    [Flags]
    public enum INS_DRAW{
        NORMAL = 0x00,
        SCH_ROI_FRAME_ON = 0x01,
        SCH_ROI_FRAME_OFF = 0x02,

        EDGE_RESULT = 0x04,

        CAL_ROI_FRAME_ON = 0x07,
        CAL_MRK_FIND_RESULT = 0x08,

        FIND_ROI_FRAME_ON = 0x10,
        FIND_RESULT = 0x12,
        FIND_SEARCH_ROI_FRAME_ON = 0x14,

        //INSPECTION MANUAL 
        INSP_MANUAL_SCH_FRAME_ON = 0x16,
        INSP_MANUAL_RESULT = 0x17,

        //RIGHT TOP
        INSP_SCH_FRAME_ON_1 = 0x20,
        INSP_RESULT_1 = 0x22,
        //LEFT TOP
        INSP_SCH_FRAME_ON_2 = 0x30,
        INSP_RESULT_2 = 0x32,
        //LEFT BTM
        INSP_SCH_FRAME_ON_3 = 0x40,
        INSP_RESULT_3 = 0x42,
        //RIGHT BTM
        INSP_SCH_FRAME_ON_4 = 0x50,
        INSP_RESULT_4 = 0x52,
        //TOP
        INSP_SCH_FRAME_ON_5 = 0x60,
        INSP_RESULT_5 = 0x62,
        //BTM
        INSP_SCH_FRAME_ON_6 = 0x70,
        INSP_RESULT_6 = 0x72,
        //LEFT
        INSP_SCH_FRAME_ON_7 = 0x80,
        INSP_RESULT_7 = 0x82,
        //RIGHT
        INSP_SCH_FRAME_ON_8 = 0x90,
        INSP_RESULT_8 = 0x92,

        ALL = 0xFFFF,
    }


    public enum DRAW_ROI : int{
        none = 0,
        drawFrameOn,
        drawFrameOff
    };

    public enum DRAW_FINDER : int{
        none = 0,
        draw
    };

    public enum eCamPara{
        BlackLevel = 0,
        ContrastLevel,
        SharpnessLevel,
        ShutterLevel
    }
    public enum eInspChannel{
        RT_H = 0,
        RT_V = 1,
        LT_H = 2,
        LT_V = 3,
        LB_H = 4,
        LB_V = 5,
        RB_H = 6,
        RB_V = 7,

        TOP = 8,
        BTM = 9,
        LT = 10,
        RT = 11
    }
    public enum eInspTarget{
        RT = 0,
        LT = 1,
        LB = 2,
        RB = 3
    }
    #endregion "VISION ENUM"
}