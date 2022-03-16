// Referred by MOTION_DEFINE.h

/*
typedef union
{
	DWORD	dwValue;
	struct
	{
		unsigned	FFLAG_ERRORALL			: 1; // = 0x00000001;
		unsigned	FFLAG_HWPOSILMT			: 1; // = 0x00000002;
		unsigned	FFLAG_HWNEGALMT			: 1; // = 0x00000004;
		unsigned	FFLAG_SWPOGILMT			: 1; // = 0x00000008;
		unsigned	FFLAG_SWNEGALMT			: 1; // = 0x00000010;
		unsigned	FFLAG_RESERVED0			: 1; // = 0x00000020;
		unsigned	FFLAG_RESERVED1			: 1; // = 0x00000040;
		unsigned	FFLAG_ERRPOSOVERFLOW	: 1; // = 0x00000080;
		unsigned	FFLAG_ERROVERCURRENT	: 1; // = 0x00000100;
		unsigned	FFLAG_ERROVERSPEED		: 1; // = 0x00000200;
		unsigned	FFLAG_ERRPOSTRACKING	: 1; // = 0x00000400;
		unsigned	FFLAG_ERROVERLOAD		: 1; // = 0x00000800;
		unsigned	FFLAG_ERROVERHEAT		: 1; // = 0x00001000;
		unsigned	FFLAG_ERRBACKEMF		: 1; // = 0x00002000;
		unsigned	FFLAG_ERRMOTORPOWER		: 1; // = 0x00004000;
		unsigned	FFLAG_ERRINPOSITION		: 1; // = 0x00008000;
		unsigned	FFLAG_EMGSTOP			: 1; // = 0x00010000;
		unsigned	FFLAG_SLOWSTOP			: 1; // = 0x00020000;
		unsigned	FFLAG_ORIGINRETURNING	: 1; // = 0x00040000;
		unsigned	FFLAG_INPOSITION		: 1; // = 0x00080000;
		unsigned	FFLAG_SERVOON			: 1; // = 0x00100000;
		unsigned	FFLAG_ALARMRESET		: 1; // = 0x00200000;
		unsigned	FFLAG_PTSTOPPED			: 1; // = 0x00400000;
		unsigned	FFLAG_ORIGINSENSOR		: 1; // = 0x00800000;
		unsigned	FFLAG_ZPULSE			: 1; // = 0x01000000;
		unsigned	FFLAG_ORIGINRETOK		: 1; // = 0x02000000;
		unsigned	FFLAG_MOTIONDIR			: 1; // = 0x04000000;
		unsigned	FFLAG_MOTIONING			: 1; // = 0x08000000;
		unsigned	FFLAG_MOTIONPAUSE		: 1; // = 0x10000000;
		unsigned	FFLAG_MOTIONACCEL		: 1; // = 0x20000000;
		unsigned	FFLAG_MOTIONDECEL		: 1; // = 0x40000000;
		unsigned	FFLAG_MOTIONCONST		: 1; // = 0x80000000;
	};
} EZISERVO_AXISSTATUS;
*/

internal class EziSERVOPlusR
{
    // EZISERVO_AXISSTATUS
    public const uint FFLAG_ERRORALL = 0x00000001;       //여러 에러중 하나 이상의 에러가 발생함

    public const uint FFLAG_HWPOSILMT = 0x00000002;       //+방향 리미트 센서가 ON된 경우
    public const uint FFLAG_HWNEGALMT = 0x00000004;       //-방향 리미트 센서가 ON된 경우
    public const uint FFLAG_SWPOGILMT = 0x00000008;       //+방향 소프트웨어 리미트를 초과한 경우
    public const uint FFLAG_SWNEGALMT = 0x00000010;       //-방향 소프트웨어 리미트를 초과한 경우
    public const uint FFLAG_RESERVED0 = 0x00000020;       //
    public const uint FFLAG_RESERVED1 = 0x00000040;       //
    public const uint FFLAG_ERRPOSOVERFLOW = 0x00000080;       //
    public const uint FFLAG_ERROVERCURRENT = 0x00000100;
    public const uint FFLAG_ERROVERSPEED = 0x00000200;
    public const uint FFLAG_ERRPOSTRACKING = 0x00000400;
    public const uint FFLAG_ERROVERLOAD = 0x00000800;
    public const uint FFLAG_ERROVERHEAT = 0x00001000;
    public const uint FFLAG_ERRBACKEMF = 0x00002000;
    public const uint FFLAG_ERRMOTORPOWER = 0x00004000;
    public const uint FFLAG_ERRINPOSITION = 0x00008000;
    public const uint FFLAG_EMGSTOP = 0x00010000;
    public const uint FFLAG_SLOWSTOP = 0x00020000;
    public const uint FFLAG_ORIGINRETURNING = 0x00040000;
    public const uint FFLAG_INPOSITION = 0x00080000;
    public const uint FFLAG_SERVOON = 0x00100000;
    public const uint FFLAG_ALARMRESET = 0x00200000;
    public const uint FFLAG_PTSTOPPED = 0x00400000;
    public const uint FFLAG_ORIGINSENSOR = 0x00800000;
    public const uint FFLAG_ZPULSE = 0x01000000;
    public const uint FFLAG_ORIGINRETOK = 0x02000000;
    public const uint FFLAG_MOTIONDIR = 0x04000000;
    public const uint FFLAG_MOTIONING = 0x08000000;
    public const uint FFLAG_MOTIONPAUSE = 0x10000000;
    public const uint FFLAG_MOTIONACCEL = 0x20000000;
    public const uint FFLAG_MOTIONDECEL = 0x40000000;
    public const uint FFLAG_MOTIONCONST = 0x80000000;

    //typedef enum
    //{
    //    SERVO_PULSEPERREVOLUTION = 0,
    //    SERVO_AXISMAXSPEED,
    //    SERVO_AXISSTARTSPEED,
    //    SERVO_AXISACCTIME,
    //    SERVO_AXISDECTIME,

    //    SERVO_SPEEDOVERRIDE,
    //    SERVO_JOGHIGHSPEED,
    //    SERVO_JOGLOWSPEED,
    //    SERVO_JOGACCDECTIME,

    //    SERVO_SERVOALARMLOGIC,
    //    SERVO_SERVOONLOGIC,
    //    SERVO_SERVORESETLOGIC,

    //    SERVO_SWLMTPLUSVALUE,
    //    SERVO_SWLMTMINUSVALUE,
    //    SERVO_SOFTLMTSTOPMETHOD,
    //    SERVO_HARDLMTSTOPMETHOD,
    //    SERVO_LIMITSENSORLOGIC,

    //    SERVO_ORGSPEED,
    //    SERVO_ORGSEARCHSPEED,
    //    SERVO_ORGACCDECTIME,
    //    SERVO_ORGMETHOD,
    //    SERVO_ORGDIR,
    //    SERVO_ORGOFFSET,
    //    SERVO_ORGPOSITIONSET,
    //    SERVO_ORGSENSORLOGIC,

    //    SERVO_POSITIONLOOPGAIN,
    //    SERVO_INPOSITIONVALUE,
    //    SERVO_POSTRACKINGLIMIT,
    //    SERVO_MOTIONDIR,

    //    SERVO_LIMITSENSORDIR,
    //    SERVO_ORGTORQUERATIO,

    //    SERVO_POSERROVERFLOWLIMIT,

    //    MAX_SERVO_PARAM

    //} FM_EZISERVO_PARAM;
    public enum PARAM
    {
        PULSEPERREVOLUTION = 0,
        AXISMAXSPEED,
        AXISSTARTSPEED,
        AXISACCTIME,
        AXISDECTIME,

        SPEEDOVERRIDE,
        JOGHIGHSPEED,
        JOGLOWSPEED,
        JOGACCDECTIME,

        SERVOALARMLOGIC,
        SERVOONLOGIC,
        SERVORESETLOGIC,

        SWLMTPLUSVALUE,
        SWLMTMINUSVALUE,
        SOFTLMTSTOPMETHOD,
        HARDLMTSTOPMETHOD,
        LIMITSENSORLOGIC,

        ORGSPEED,
        ORGSEARCHSPEED,
        ORGACCDECTIME,
        ORGMETHOD,
        ORGDIR,
        ORGOFFSET,
        ORGPOSITIONSET,
        ORGSENSORLOGIC,

        POSITIONLOOPGAIN,
        INPOSITIONVALUE,
        POSTRACKINGLIMIT,
        MOTIONDIR,

        LIMITSENSORDIR,
        ORGTORQUERATIO,

        POSERROVERFLOWLIMIT,

        MAX_PARAM_CNT
    };
}