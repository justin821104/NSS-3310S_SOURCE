using System.Runtime.InteropServices;

class T1
{
    // #if DEBUG
    // T1WinDrv가 동작하는 지를 알려주는 함수이다. 메카트로링크 통신을 하려면 언제
    //나 T1WinDrv가 먼저 실행되어 있어야 한다.
    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsRunDrv();

    //T1WinDrv가 메모리 통신을 하기 위해서 메모리맵 파일을 연결한다. 이 함수를 통해서
    //메모리맴과 연결이 되어야만 다른 모든 함수들의 사용이 가능합니다.
    //0x0000 OK 0x0008 T1WinDrv 미 실행 0x0002 PC 문제 끄고 다시 시작
    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern uint ConnectDrv();

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void ClearSlvType();
    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetSlvType(short ushPort, short ushMt, char uchType);
    //public static extern void SetSlvType(short ushPort, short ushMt, int uchType);
    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern uint StartDrv2();
    /*
	슬레이브 정보 배열 입력 0번 PORT
	UCHAR uchSlvType[MAX_MASTER_NUM][MAX_SLAVE_NUM];
	uchSlvType[0][0]=1; // 서보모터
	uchSlvType[0][1]=3; // DAISY32입력
	uchSlvType[0][2]=3; // DAISY32출력
	uchSlvType[0][3]=1; // 서보모터
	uchSlvType[0][4]=4; // DAISY128(입력모듈2개, 촐력모듈3개)
	uchSlvType[0][5]=1; // 서보모터
	UINT nRet = StartDrv(uchSlvType);
	 */
    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern uint StartDrv(ref sbyte[,] puchSlvType);

    //메카트로링크 연결상태 값을 반환하는 함수
    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern short GetSlvType(ref short pErr);

    /*DAISY-R128 랙 타입 I/O 모듈 사용 시에 해당 모듈에 입력과 출력 모듈이 몇 개씩 장
	착되어 있는지를 설정하는 함수.
	▶ ushPort : 원하는 서보드라이버의 포트 값.LILY-101이면 언제나 0으로 설정 LILY-301이면 0,1,2으로 설정.
	▶ ushMt : 원하는 서보드라이버의 ID 값.
	▶ nInModuleNum : 입력 모듈 개수. (0~8개)
	▶ nOutModuleNum : 출력 모듈 개수.(0~8개)
	 */
    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetRackIOType(short ushPort, short ushSlv, int nInModuleNum, int nOutModuleNum);

    //DAISY-RACK128에서 입력 모듈 사용시, 해당 모듈의 입력 값을 읽어오는 함수
    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern short RackReadInputData(short ushPort, short ushSlv, int ModuleNum);

    //DAISY-RACK128에서 출력 모듈 사용시, 해당 모듈의 출력 값을 써주는 함수.
    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void RackWriteOutputData(short ushPort, short ushSlv, int ModuleNum, int ushData16);

    //MOTION

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void MotionUpdate(short ushPort, short ushSlv);

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetRealCount(short ushPort, short ushSlv);

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetLatchCount(short ushPort, short ushSlv);

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SetRealCount(short ushPort, short ushSlv, int iData);

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsALM(short ushPort, short ushSlv);

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsSVON(short ushPort, short ushSlv);

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SERVO_ON(short ushPort, short ushSlv);

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool SERVO_OFF(short ushPort, short ushSlv);

    //inposition내에 있는가?
    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsPSET(short ushPort, short ushSlv);

    //motion done
    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsDEN(short ushPort, short ushSlv);

    //type 0-> 감속 정지 , type-> 1 즉시 정지
    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool HOLD(short ushPort, short ushSlv, char type);

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsL_CMP(short ushPort, short ushSlv);

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsP_OT(short ushPort, short ushSlv);

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsN_OT(short ushPort, short ushSlv);

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LTMOD_ON(short ushPort, short ushSlv, char LtSgn);
    //public static extern bool LTMOD_ON(short ushPort, short ushSlv, int LtSgn);

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LTMOD_OFF(short ushPort, short ushSlv);

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool ALM_CLR(short ushPort, short ushSlv);

    // ushPosRefFilter = 0 : NO use Filter
    // ushPosRefFilter = 1 : Exponential 필터 사용.
    // ushPosRefFilter = 2 : 이동 평균 필터 사용.
    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetPosRefFilter(short ushPort, short ushSlv, short ushPosRefFilter);

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetAccelDecel(short ushPort, short ushSlv, short ushAccStep1, short ushAccStep2, short ushAccSwVal, short ushDecStep1, short ushDecStep2, short ushDecSwVal);

    //bOn -> true
    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void EnableSCurve(short ushPort, short ushSlv, bool bOn);

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void FEED(short ushPort, short ushSlv, long spd);

    [DllImport("T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void POSING(short ushPort, short ushSlv, int Pos, int spd);

    //#else 
    //    // T1WinDrv가 동작하는 지를 알려주는 함수이다. 메카트로링크 통신을 하려면 언제
    //    //나 T1WinDrv가 먼저 실행되어 있어야 한다.
    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern bool IsRunDrv();

    //    //T1WinDrv가 메모리 통신을 하기 위해서 메모리맵 파일을 연결한다. 이 함수를 통해서
    //    //메모리맴과 연결이 되어야만 다른 모든 함수들의 사용이 가능합니다.
    //    //0x0000 OK 0x0008 T1WinDrv 미 실행 0x0002 PC 문제 끄고 다시 시작
    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern uint ConnectDrv();

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern void ClearSlvType();
    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern void SetSlvType(short ushPort, short ushMt, char uchType);
    //    //public static extern void SetSlvType(short ushPort, short ushMt, int uchType);
    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern uint StartDrv2();
    //    /*
    //    슬레이브 정보 배열 입력 0번 PORT
    //    UCHAR uchSlvType[MAX_MASTER_NUM][MAX_SLAVE_NUM];
    //    uchSlvType[0][0]=1; // 서보모터
    //    uchSlvType[0][1]=3; // DAISY32입력
    //    uchSlvType[0][2]=3; // DAISY32출력
    //    uchSlvType[0][3]=1; // 서보모터
    //    uchSlvType[0][4]=4; // DAISY128(입력모듈2개, 촐력모듈3개)
    //    uchSlvType[0][5]=1; // 서보모터
    //    UINT nRet = StartDrv(uchSlvType);
    //     */
    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern uint StartDrv(ref sbyte[,] puchSlvType);

    //    //메카트로링크 연결상태 값을 반환하는 함수
    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern short GetSlvType(ref short pErr);

    //    /*DAISY-R128 랙 타입 I/O 모듈 사용 시에 해당 모듈에 입력과 출력 모듈이 몇 개씩 장
    //    착되어 있는지를 설정하는 함수.
    //    ▶ ushPort : 원하는 서보드라이버의 포트 값.LILY-101이면 언제나 0으로 설정 LILY-301이면 0,1,2으로 설정.
    //    ▶ ushMt : 원하는 서보드라이버의 ID 값.
    //    ▶ nInModuleNum : 입력 모듈 개수. (0~8개)
    //    ▶ nOutModuleNum : 출력 모듈 개수.(0~8개)
    //     */
    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern void SetRackIOType(short ushPort, short ushSlv, int nInModuleNum, int nOutModuleNum);

    //    //DAISY-RACK128에서 입력 모듈 사용시, 해당 모듈의 입력 값을 읽어오는 함수
    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern short RackReadInputData(short ushPort, short ushSlv, int ModuleNum);

    //    //DAISY-RACK128에서 출력 모듈 사용시, 해당 모듈의 출력 값을 써주는 함수.
    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern void RackWriteOutputData(short ushPort, short ushSlv, int ModuleNum, int ushData16);

    //    //MOTION

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern void MotionUpdate(short ushPort, short ushSlv);

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern int GetRealCount(short ushPort, short ushSlv);

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern int GetLatchCount(short ushPort, short ushSlv);

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern bool SetRealCount(short ushPort, short ushSlv, int iData);

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern bool IsALM(short ushPort, short ushSlv);

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern bool IsSVON(short ushPort, short ushSlv);

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern bool SERVO_ON(short ushPort, short ushSlv);

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern bool SERVO_OFF(short ushPort, short ushSlv);

    //    //inposition내에 있는가?
    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern bool IsPSET(short ushPort, short ushSlv);

    //    //motion done
    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern bool IsDEN(short ushPort, short ushSlv);

    //    //type 0-> 감속 정지 , type-> 1 즉시 정지
    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern bool HOLD(short ushPort, short ushSlv, char type);

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern bool IsL_CMP(short ushPort, short ushSlv);

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern bool IsP_OT(short ushPort, short ushSlv);

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern bool IsN_OT(short ushPort, short ushSlv);

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern bool LTMOD_ON(short ushPort, short ushSlv, char LtSgn);
    //    //public static extern bool LTMOD_ON(short ushPort, short ushSlv, int LtSgn);

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern bool LTMOD_OFF(short ushPort, short ushSlv);

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern bool ALM_CLR(short ushPort, short ushSlv);

    //    // ushPosRefFilter = 0 : NO use Filter
    //    // ushPosRefFilter = 1 : Exponential 필터 사용.
    //    // ushPosRefFilter = 2 : 이동 평균 필터 사용.
    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern void SetPosRefFilter(short ushPort, short ushSlv, short ushPosRefFilter);

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern void SetAccelDecel(short ushPort, short ushSlv, short ushAccStep1, short ushAccStep2, short ushAccSwVal, short ushDecStep1, short ushDecStep2, short ushDecSwVal);

    //    //bOn -> true
    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern void EnableSCurve(short ushPort, short ushSlv, bool bOn);

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern void FEED(short ushPort, short ushSlv, long spd);

    //    [DllImport("C:\\Windows\\T1Lib.dll", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern void POSING(short ushPort, short ushSlv, int Pos ,int spd);
    //#endif
}