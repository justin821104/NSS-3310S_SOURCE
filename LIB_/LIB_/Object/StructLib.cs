using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Object
{
    #region "MACHINE STRUCT"
    public struct stBIT{
        public const bool FWD           = true;
        public const bool BWD           = false;

        public const bool OPEN          = true;
        public const bool CLOSE         = false;

        public const bool LEFT          = true;
        public const bool RIGHT         = false;

        public const bool LOCK          = true;
        public const bool UNLOCK        = false;

        public const bool ON            = true;
        public const bool OFF           = false;

        public const bool SEARCH        = true;
        public const bool NotSEARCH     = false;

        public const bool SENSING       = true;
        public const bool NotSENSING    = false;

        public const bool CAM           = true;
        public const bool NotCAM        = false;

        public const bool UnitOffset    = true;
        public const bool NotUnitOffset = false;

        public const bool HOME          = true;
        public const bool ROTATE        = false;

        public const bool UP            = true;
        public const bool DOWN          = false;

        public const bool Loader        = true;
        public const bool NotLoader     = false;

        public const bool Conveyor      = true;
        public const bool Stacker       = false;

        public const bool AND           = true;
        public const bool OR            = false;

        public const bool TABLE_1       = true;
        public const bool TABLE_2       = false;

        public const bool Delay         = true;
        public const bool NotDelay      = false;
    }

    public struct stTackTime{
        public long sUph, eUph, cntUph;
        public long tmNow, tmOld;
        public long iDay;
    }
    /// <summary>
    /// 설비 패스워드 구조체
    /// OP      = 오퍼레이터 패스워드
    /// ENG     = 엔지니어 패스워드
    /// SUP     = 관리자 패스워드
    /// SOFT    = 개발자 패스워드
    /// </summary>
    public struct stPassword{
        public string OP;
        public string ENG;
        public string SUP;
        public string SOFT;
    }

    /// <summary>
    /// 쓰레드 정보
    /// </summary>
    public struct stThreadInfo{
        public stThreadFunctionInfo[] pfLOGIC;
        public stThreadFunctionInfo[] pfRAWFUNC;

        public string sLOGIC, sRAWFUNC; // 현재 실행중인 로직/구동함수 이름
        public string waitSTS;          // 스레드 대기 상태
        public string RETURN;           // 액션에서 반환하는 리턴값
        public string CMD;              // 스레드 명령어
        public bool USE;                // 가동 상태
        public string thSTS;            // 스레드 상태
        public string sqeSTS;           // 현재 시퀀스 상태

        //스레드 스위치문 인덱스 번호
        public int iSTEP;
        public int iSTEP1;
        public int iSTEP2;
        public int iSTEP3;
    }
    public struct stThreadFunctionInfo{
        public string sTime, eTime;
        public double RunTime;
        public string name;
        public long sTick, eTick;
    }

    /// <summary>
    /// 로그 정보 
    /// </summary>
    public struct stLOGInfo{
        public string sException;       // 예외처리 로그
        public string sMars;            // 마스 로그
        public string sParaEvent;       // RECIPE(PARAMETER) 변경 로그
        public string sError;           //
        public string sMES;             //
        public string sSystem;          // 시스템/인터록 메세지
        public string sManual;          //
        public string sOperate;         //
        public string sLogin;
        public string sWarnMsg;         // 경고 메세지 발생시 로그
        public string sPringMsg;        // 메세지 발생시 로그
        public string sProcessInfo;
        public string sMeasure; //
        public string sCOUNT;
        public string sPROCESS;
        public string sTACK;
        public string sLOT;
        public string sOneCycleTime;
        public string sBlade;

        public string sBorcodeHistory;
        public string sITSBarcodeHistory;
        public string sPCBUnitInfo;
        public string sProgramCheck;
        public string sPRSData;
    }

    /// <summary>
    /// SPC
    /// </summary>
    public struct stSPC{
        public string strWorkTime;      // 조업시간 (장비 ON 시간)
        public string strRunTime;       // 가동시간 (런닝[RUN] 시간)
        public string strStopTime;      // 정지시간 (정지[IDLE] 시간) 
        public string strPauseTime;     // 순간정지시간(에러타임[ERROR])
        public string strErrorTime;     // 고장시간(에러발생후 5분[ERROR])
        public string strRunWaitTime;   // 가동대기시간 (런닝 중 대기 시간)
        public string strRunDownTime;   // 런-다운시간 (런닝 중 가동대기후 5분)
        public string strPauseCount;    // 순간정지횟수

        public int mlWorkTime;
        public int mlRunTime;
        public int mlStopTime;
        public int mlPauseTime;
        public int mlErrorTime;
        public int mlRunWaitTime;
        public int mlRunDownTime;
        public int mlPauseCount;
        public int mlTotalProduct;
    }

    /// <summary>
    /// 에러 관련
    /// </summary>
    public struct stErrInfo{
        public int kind;            // 에러종류
        public eLogLevel rstLevel;  //= enmLOG_LEVEL.lvNull; // 해제 레벨
        public bool enRec;           // 기록여부
    }

    public struct stERR{
        public int ErrorNumber;
        public string BeginTime;
        public string EndTime;
        public bool use;
    }

    public struct stErrOCCURED{
        public int numPage; //
        //string errmessage;  //에러명
        //string errProcess;  //에러조치내용
        //int errNum;         //에러번호
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct stInfoClean{
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public int[] MODE; // 세척 종류 0="", 1=WASH ,2=DRY ,3=RINSE .4=(null)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public int[] COUNTER; // NOZZLE
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public int[] TIME; // TIME
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct stInfoPCB{
        public string sPCBBarcode;
        public string sPCBInTime;
        public string sPCBOutTime;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct stInfoSeq{
        public string sBARCODE;
        public string sTrackInTime;
        public string sTrackOutTime;
    }

    //USER 정보
    public struct sUSER_INFO{
        public string ID;
        public string Name;
        public string Password;
    }


    #endregion "MACHINE STRUCT"

    #region "COUNTER AGENT"
    [StructLayout(LayoutKind.Sequential)]
    public struct stCounterStatus{
        public double CurrentPosition;

    }
    #endregion "COUNTER AGENT"

    #region "MOTION STRUCT"
    /// <summary>
    /// 모션 이송 정보 구조체
    /// </summary>
    public struct stMoveInfo{
        public int axis;        // 축번호
        public double Pos, Spd, Acc, Dec;
        public int MoveTime, Delay;

        public bool bPOS;
        public bool bMINUS;
        public bool bPLUS;
    }

    /// <summary>
    /// 모션 상태 확인 구조체 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct stMotionStatus{
        public bool bAlram;                         // 서보알람 상태
        public bool bInposition;                    // 인포지션
        public bool bSvOn;                          // 서보 온 출력상태
        public bool bReady;                         // 서보레디
        public bool bDone;                          // 드라이버 상태
        public bool bSensorCW;                      // +리미트 센서 상태
        public bool bSensorCCW;                     // -리미트 센서 상태
        public bool bSensorHome;                    // 홈 센서 상태
        public bool bEMO;                           // 비상 정지
        public bool bBusy;                          // 모타가 움직이고 있는중임
        public bool bMoving;                        // 모타가 포지션 이동중임
        public bool bMoveComplete;                  // 위치이동 완료
        public bool bHomming;                       // 모타가 홈서치 동작중임
        public bool bHomeComplete;                  // 원점복귀 완료
        public bool bJoggingCW;                     // 모타가 CW방향으로 조그동작중이다.
        public bool bJoggingCCW;                    // 모타가 CCW방향으로 조그동작중이다.

        public bool bErrMove;                       // 이동중 에러발생
        public bool bErrTime;                       // 시간초과 에러발생
        public bool bErrHome;                       // 홈서치중 에러발생
        public bool bErrCwLimit;                    // CW리미트감지 에러발생
        public bool bErrCCwLimit;                   // CCW리미트감지 에러발생
        public bool bErrSwLimitP;                   // +방향 SW리미트감지 에러발생
        public bool bErrSwLimitM;                   // -방향 SW리미트감지 에러발생
        public bool bErrSVAlarm;                    // 서보드라이버 알람발생 에러
        public bool bErrParam;                      // 모타파라미터(s/w) 설정에러

        public double CurrentPosition;              // 모타의 현재위치
        public double CurrentGap;                   // 모타의 1D보정시 갭
        public double CmdPosition;                  // CMD 위치
        public double CurrentRatio;                 // 모터 현재 부하율 

        public bool[] thisPosition;                 // 현재 위치에 있음.

        public string Status;                       // 마지막 구동명령

        public string strHome;                      // 홈 문자열
        public string strMove;                      // 구동 문자열
        public string strStop;                      // 정지명령 문자열
        public string strOther;                     //

        public double dCurSpeed;                    // 모터 현재 속도
    }

    /// <summary>
    /// 구동 명령 구조체 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct stMotorCMD{
        public bool CMDJogPitchCw;                  // CW 방향으로 피치 이동
        public bool CMDJogPitchCcw;                 // CCW 방향으로 피치 이동
        public bool CMDJogCW;                       // CW방향으로 조그이동 명령
        public bool CMDJogCCW;                      // CCW방향으로 조그이동 명령
        public bool CMDRMove;                       // 상대좌표 이동명령
        public bool CMDMove;                        // 절대좌표 이동명령
        public bool CMDHome;                        // 홈명령
        public bool CMDEStop;                       // 서보 급정지 명령
        public bool CMDErrorClear;                  // 모든 에러리셋
        public bool CMDReset;                       // 드라이버 에러리셋
        public bool spdNoChange;                    // 가동율 적용안함.
    }

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct stMotorCheck{
        public int axis;                            // 축번호
        public double pos, spd, acc, dcc;           // 이송
        public int time;                            // 이송 시간
        public string sLog, errLog, fLog;           // 시작/에러/종료 로그
        public long sTime, eTime, timeStop, rTime;  // 시작/종료/중단 시간
        public string coment;                       // 코멘트
        public bool OnBusy;                         // 비지 발생
        public double posBegin, posStop, posGap;    // 목표위치값/중단위치값
        public string cmd;
        public double toller;                       // 위치편차값
        public bool onlyStart, noChange;            // 정지 후 확인 안함 /구동 속도 가동율 X
        public string sts;                          // 구동 진행 상태
        public string Ev;                           // 정지/ 에러 발생 (외부 이벤트)
        public string rslt;                         // 이동 결과
    }

    /// <summary>
    /// 모션 구동 옵션 기능 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct stMotorOption{
        public bool SpeedNoChange;                  // 구동 속도 변환 안함.
        public bool DontStop;                       // CYCLE STOP 조건(일시정지 기능 안함)
        public double posTarget;                    // 
    }

    /// <summary>
    /// 모션 소프트 리미트 데이터
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct stMotorSoftData{
        public double CwSoftLimit;
        public double CcwSoftLimit;
        public double MaxPitch;

        public double MinSpd;
        public double MaxSpd;

        public double MinAcc;
        public double MaxAcc;
        public double MinDec;
        public double MaxDec;

        public double JOG_HIGH_SPD;
        public double JOG_MIDDLE_SPD;
        public double JOG_LOW_SPD;

        public double ShortLength;
        public double ShortSpeed;
    }

    /// <summary>
    /// 모터 티칭값 리미트 설정
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct stMotorTeachingLimit{
        public double PosPLimit, PosNLimit;
        public bool Enable;
    }

    /// <summary>
    /// IO 옵션 기능 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct stIO{
        public bool ContactB;   // A접점이면 TRUE;
        public bool Checked;    // 체크 완료면 TRUE;
        public bool Virtual;    // 가상 IO면 TRUE;
    }

    /// <summary>
    /// WARNNING MESSAGE 정의
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct stCONFIRM{
        public string msg;
        public bool useable;
        public bool result;
        public bool process;
        public bool AfterReset;
        public int bz;
        public int[] AllBzNum;
        public bool TypeOk;
        public bool MoveOK;
        public int num;

        public bool CheckBox;
        public bool CheckBoxResult;
        public string CheckBoxTitle;

        public bool isShowEmptPocket;
    }

    /// <summary>
    /// 메뉴얼 상태 확인 구조체 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct stInfoMANUAL{
        public string CMD;
        public int iVAL;
        public double dVAL;
        public string sVAL;

        public int RunManual;
        public int Number;
        public bool bRESULT;
        public double dRESULT;
        public eRTN eRESULT;
        public string ManualCmd;

        public string Label;
        public string Message;

        public bool bLABEL;
        public bool bBUTTON;

        public int iMT1;
        public int iMT2;
        public int iMT3;
        public int iMT4;
        public int iMT5;
        public int iMT6;
        public int iMT7;
        public int iMT8;

        public int int_1;
        public int int_2;
        public int int_3;
        public int int_4;
        public int int_5;
        public int int_6;
        public int int_7;
        public int int_8;

        public bool bool_1;
        public bool bool_2;
        public bool bool_3;
        public bool bool_4;
        public bool bool_5;
        public bool bool_6;
        public bool bool_7;
        public bool bool_8;

        public double double_1;
        public double double_2;
        public double double_3;
        public double double_4;
        public double double_5;
        public double double_6;
        public double double_7;
        public double double_8;

        public Color Color1;
        public Color Color2;
        public Color Color3;
        public Color Color4;
        public Color Color5;
        public Color Color6;
        public Color Color7;
        public Color Color8;

        public Color BackColor1;
        public Color BackColor2;
        public Color BackColor3;
        public Color BackColor4;
        public Color BackColor5;
        public Color BackColor6;
        public Color BackColor7;
        public Color BackColor8;

        public dxy XY_1;
        public dxy XY_2;
        public dxy XY_3;
        public dxy XY_4;
        public dxy XY_5;
        public dxy XY_6;
        public dxy XY_7;
        public dxy XY_8;

        public dxyzt Pos1;
        public dxyzt Pos2;
        public dxyzt Pos3;
        public dxyzt Pos4;
        public dxyzt Pos5;
        public dxyzt Pos6;
        public dxyzt Pos7;
        public dxyzt Pos8;

        public bool Option;
    }

    public struct stPkr{
        public stFinger[] finger;  // 핑거 8개
        public string CMD;  // 특수 명령어
        public double PrsOffset_X, PrsOffset_Y, PrsOffset_T;
        public int StagePnP; // 픽 앤 플레이스 테이블번호
    }
    public struct stFinger{
        public bool valid;  // 자재있음
        public bool skip;  // 사용안함
        public double vacuum, InVacuum;  // 설정된 진공값
        public int pocketX, pocketY;// 집을포켙번호
        public bool pickMarking; // 집을포켙 있음표시
        public int iResult; // 유닛 검사 정보 
        public double prsX, prsY, prsR; // 얼라인 보정값
        public double prsOffsetX, prsOffsetY, prsOffsetT;  // 1번 핑거 기준옵셋
        public double OffsetX, OffsetY; //맵블록 유닛 옵셋
        public string strPkg2d;
    }
    #endregion "MOTION STRUCT"

    #region COGNEX
    public struct CommunicationType{
        public const bool GigaE = true;
        public const bool Serial = false;
    }
    #endregion COGNEX

    #region "MATH STRUCT"
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MATRIX3X3{
        public double m11, m12, m13;
        public double m21, m22, m23;
        public double m31, m32, m33;

        //생성자에서 초기화
        public MATRIX3X3(double d11, double d12, double d13, double d21, double d22, double d23, double d31, double d32, double d33){
            this.m11 = d11;
            this.m12 = d12;
            this.m13 = d13;
            this.m21 = d21;
            this.m22 = d22;
            this.m23 = d23;
            this.m31 = d31;
            this.m32 = d32;
            this.m33 = d33;
        }
    };


    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct dxy{
        public double x;
        public double y;

        public dxy(double dX, double dY){
            x = dX;
            y = dY;
        }

        public dxy(dxy xy){
            x = xy.x;
            y = xy.y;
        }

        public bool IsSamePoint(dxy xy, double dLimitSame = 0.001){
            if (System.Math.Abs(x - xy.x) > dLimitSame) return false;
            if (System.Math.Abs(y - xy.y) > dLimitSame) return false;
            return true;
        }
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct dxyz{
        public double x;
        public double y;
        public double z;
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct dxyzt{
        public double x;
        public double y;
        public double z;
        public double t;
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct dxyt{
        public double x;
        public double y;
        public double t;
    };


    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct nxy{
        public int x;
        public int y;
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct fxy{
        public float x;
        public float y;
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct dLine{
        public int Center;
        public int End;
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct dSize{
        public double Width;
        public double Height;
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct dMinMax{
        public double MinX;
        public double MinY;
        public double MidX;
        public double MidY;
        public double MaxX;
        public double MaxY;
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct dLength{
        public double TopWidth;
        public double BtmWidth;
        public double LeftHeight;
        public double RightHeight;
        public double Width;
        public double Height;
    }
    #endregion "MATH STRUCT"
}