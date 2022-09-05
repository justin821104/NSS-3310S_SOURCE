namespace NSS_3310S
{
    //0SORTER(128), 1HD1PICKER(128), 2HD2PICKER(128), 3HANDLER1(32), 4HANDLER2(32) 
    public class I
    {
        #region >>Virtual Operator Switch
        public const short vtStart                      = CNT_.IN - 1;
        public const short vtStop                       = CNT_.IN - 2;
        public const short vtReset                      = CNT_.IN - 3;
        public const short vtInitial                    = CNT_.IN - 4;
        public const short vtEms                        = CNT_.IN - 5;
        #endregion
#if _NSS3300
        #region >>SawHandlerInputList (0~63)
        //MODULE #0 - OFFSET #0 (0~15)
        public const short EMO_SAW_FRONT                = 0;    // X700 [SAW] FRONT EMO SWITCH
        public const short EMO_SAW_REAR                 = 1;    // X701 [SAW] REAR EMO SWITCH
        public const short DOOR_SAW_FRONT_LEFT          = 2;    // X702 [SAW] FRONT LEFT DOOR CHECK SENSOR
        public const short DOOR_SAW_FRONT_RIGHT         = 3;    // X703 [SAW] FRONT RIGHT DOOR CHECK SENSOR
        public const short LD_CONV_AREA_SENSOR          = 4;    // X704 [SAW] FRONT CONTER DOOR CHECK SENSOR
        public const short LD_CONV_MZ_CHECK1            = 5;    // X705 [SAW] LOADING CONVEYOR MAGAZINE IN FRONT CHECK SENSOR #1 (로더 매거진 투입부)
        public const short LD_CONV_MZ_CHECK2            = 6;    // X706 [SAW] LOADING CONVEYOR MAGAZINE IN REAR CHECK SENSOR #2 (로더 매거진 투입부)
        public const short LD_CONV_MZ_ARRIVAL_CHECK     = 7;    // X707 [SAW] LOADING CONVEYOR MAGAZINE OUT CHECK (매거진 로딩 컨베어 끝단)
        public const short ELV_MZ_EXIST1                = 8;    // X708 [SAW] LOADING ELV MAGAZINE CHECK SENSOR FRONT (매거진 안착 확인)
        public const short ELV_MZ_EXIST2                = 9;    // X709 [SAW] LOADING ELV MAGAZINE CHECK SENSOR BACK (매거진 안착 확인)
        public const short ELV_MZ_CLAMP                 = 10;   // X710 [SAW] LOADING ELV MAGAZINE CLAMP SENSOR
        public const short ELV_MZ_UNCLAMP               = 11;   // X711 [SAW] LOADING ELV MAGAZINE UNCLAMP SENSOR
        public const short RAIL_MOUTH                   = 12;   // X712 [SAW] ELV COLLISION CHECK SENSOR (매거진과 인-레일 사이에 PCB 돌출 됨 (매거진에서 스트립 투입구))
        public const short PUSHER_FWD                   = 13;   // X713 [SAW] PUSHER FORWARD CHECK SENSOR
        public const short PUSHER_BWD                   = 14;   // X714 [SAW] PUSHER BACKWARD CHECK SENSOR
        public const short PUSHER_OVERLOAD              = 15;   // X715 [SAW] PUSHER OVERLOAD CHECK SENSOR

        //OFFSET #1 (16~31)
        public const short ULD_CONV_MZ_FULL_CHECK1      = 16;   // X800 [SAW] UNLOADING MAGAZINE FULL CHECK SENSOR #1 NSS3300 A / NSS3310,3320 A
        public const short ULD_CONV_MZ_FULL_CHECK2      = 17;   // X801 [SAW] UNLOADING MAGAZINE FULL CHECK SENSOR #2 NSS3300 B / NSS3310,3320 A
        public const short GRIPPER_OPEN                 = 18;   // X802 [SAW] GRIP OPEN CHECK SENSOR
        public const short GRIPPER_CLOSE                = 19;   // X803 [SAW] GRIP CLOSE CHECK SENSOR
        public const short GRIPPER_DETECT               = 20;   // X804 [SAW] GRIP MATERIAL CHECK SENSOR
        public const short RAIL_EXIST1                  = 21;   // X805 [SAW] RAIL MATERIAL CHECK SENSOR FRONT (매거진 방향 [뒤])
        public const short RAIL_EXIST2                  = 22;   // X806 [SAW] RAIL MATERIAL CHECK SENSOR BACK (다이싱 테이블 방향 [앞]) 
        public const short INLET_TABLE_UP               = 23;   // X807 [SAW] RAIL MATERIAL SUPPORT UP SENSOR
        public const short INLET_TABLE_DN               = 24;   // X808 [SAW] RAIL MATERIAL SUPPORT DOWN SENSOR
        public const short HANDLER_PK_CRASH             = 25;   // X809 [SAW] HANDLER PICKER COLLISION CHECK SENSOR
        public const short STRIP_PK_VAC                 = 26;   // X810 [SAW] STRIP PICKER VACUUM CHECK SENSOR
        public const short UNIT_PK_VAC                  = 27;   // X811 [SAW] UNIT PICKER VACUUM CHECK SENSOR
        public const short SCRAP_VAC1                   = 28;   // X812 [SAW] UNIT PICKER SCRAP VACUUM CHECK SENSOR #1
        public const short SCRAP_VAC2                   = 29;   // X813 [SAW] UNIT PICKER SCRAP VACUUM CHECK SENSOR #2
        public const short SCRAP_BOX                    = 30;   // X814 [SAW] SCRAP BOX CHECK SENSOR
        public const short CLEANER_SWING_RIGHT          = 31;   // X815 [SAW] CLEANER SWING RIGHT CHECK SENSOR

        //OFFSET #2 (32~47)
        public const short CLEANER_SWING_LEFT           = 32;   // X900 [SAW] CLEANER SWING LEFT CHECK SENSOR
        public const short SERVO1_SAW                   = 33;   // X901 [SAW] SERVO PACK CP1 DOWN
        public const short SERVO2_SAW                   = 34;   // X902 [SAW] SERVO PACK CP2 DOWN
        public const short SERVO3_SAW                   = 35;   // X903 [SAW] SERVO PACK CP3 DOWN
        public const short i0904                        = 36;
        public const short i0905                        = 37;
        public const short i0906                        = 38;
        public const short i0907                        = 39;
        public const short i0908                        = 40;
        public const short i0909                        = 41;
        public const short i0910                        = 42;
        public const short i0911                        = 43;
        public const short i0912                        = 44;
        public const short i0913                        = 45;
        public const short i0914                        = 46;
        public const short i0915                        = 47;

        //OFFSET #3 (48~63)
        public const short SAW_READY                    = 48;   //X1000 [SAW] READY 프로그램 실행 상태
        public const short SAW_CUTTING                  = 49;   //X1001 [SAW] CUTTING 작업 중일 경우
        public const short SAW_SPARE_2                  = 50;   //X1002 [SAW] 설비 ERR 알람 발생 하였을 경우
        public const short SAW_INITIAL                  = 51;   //X1003 [SAW] INTIAL 초기화 동작 하거나 CUTTING 작업 중 JOB CANCEL 실행 시
        public const short SAW_LD_REQ                   = 52;   //X1004 [SAW] LOADING REQ 로딩 요청 (PCB 달라고 요청함)
        public const short SAW_LD_POS                   = 53;   //X1005 [SAW] STAGE LOADING POS 작업 테이블 로딩 위치에서 대기 중
        public const short SAW_STAGE_VAC_ON             = 54;   //X1006 [SAW] STAGE VAC ON 작업 테이블 진공 ON시
        public const short SAW_ULD_REQ                  = 55;   //X1007 [SAW] UNLOADING REQ 언로딩 요청 (PCB SAW 작업 완료 후 배출 요청함)
        public const short SAW_ULD_POS                  = 56;   //X1008 [SAW] STAGE UNLOADING POS 작업 테이블 언로딩 위치에서 대기 중
        public const short SAW_STAGE_BLOW               = 57;   //X1009 [SAW] STAGE BLOW 작업 테이블 파기 ON
        public const short SAW_LD_COMPLETE              = 58;   //X1010 [SAW] 설비 AUTO-RUN
        public const short SAW_REPICK_ALIGN             = 59;   //X1011
        public const short SAW_LD_REPICK_POS            = 60;   //X1012
        public const short SAW_SPARE_13                 = 61;   //X1013
        public const short SAW_SPARE_14                 = 62;   //X1014 [SAW] STAGE MOVING INTERLOCK 작업테이블 구동 확인 인터락
        public const short SAW_MAIN_AIR                 = 63;   //X1015 [SAW] MAIN AIR STATUS
        #endregion

        #region >>SorterInputList (64~175)
        //MODULE #1 - OFFSET #0 (64~79)
        public const short TENKEY1                          = 64;	//X000 [SORTER] TENKEY #1	
        public const short TENKEY2                          = 65;   //X001 [SORTER] TENKEY #2
        public const short TENKEY3                          = 66;   //X002 [SORTER] TENKEY #3
        public const short TENKEY4                          = 67;   //X003 [SORTER] TENKEY #4
        public const short TENKEY5                          = 68;   //X004 [SORTER] TENKEY #5
        public const short TENKEY6                          = 69;   //X005 [SORTER] TENKEY #6
        public const short TENKEY7                          = 70;   //X006 [SORTER] TENKEY #7
        public const short EMO_SORTER_FRONT                 = 71;   //X007 [SORTER] FRONT EMO SWITCH 
        public const short EMO_SORTER_RIGHT                 = 72;   //X008 [SORTER] RIGHT EMO SWITCH 
        public const short EMO_SORTER_BACK                  = 73;   //X009 [SORTER] BACK EMO SWITCH 
        public const short PC_POWER                         = 74;   //X010 [SORTER] PC POWER ON SWITCH
        public const short POWER_ON                         = 75;   //X011 [SORTER] POWER ON SWITCH
        public const short POWER_OFF                        = 76;   //X012 [SORTER] POWER OFF SWITCH
        public const short START                            = 77;   //X013 [SORTER] START SWITCH SIGNAL
        public const short STOP                             = 78;   //X014 [SORTER] STOP SWITCH SIGNAL
        public const short RESET                            = 79;   //X015 [SORTER] RESET SWITCH SIGNAL

        //OFFSET #1 (80~95)
        public const short DOOR_SORTER_FRONT_LEFT           = 80;   //X100 [SORTER] FRONT LEFT DOOR CHECK
        public const short DOOR_SORTER_FRONT_RIGHT          = 81;   //X101 [SORTER] FRONT RIGHT DOOR CHECK
        public const short DOOR_SORTER_RIGHT_SIDE_LEFT      = 82;   //X102 [SORTER] RIGHT SIDE OF LEFT DOOR CHECK
        public const short DOOR_SORTER_RIGHT_SIDE_RIGHT     = 83;   //X103 [SORTER] RIGHT SIDE OF RIGHT DOOR CHECK
        public const short DOOR_SORTER_BACK_LEFT            = 84;   //X104 [SORTER] BACK LEFT DOOR CHECK
        public const short DOOR_SORTER_BACK_RIGHT           = 85;   //X105 [SORTER] BACK RIGHT DOOR CHECK
        public const short SERVO1_SORTER                    = 86;   //X106 [SORTER] SERVO #1 MC30 TRIP SIGNAL
        public const short SERVO2_SORTE                     = 87;   //X107 [SORTER] SERVO #2 MC31 TRIP SIGNAL
        public const short SERVO3_SORTER                    = 88;   //X108 [SORTER] SERVO #3 MC32 TRIP SIGNAL
        public const short i109                             = 89;   //X109 [SORTER] 
        public const short TRAY_PKR_CLAMP                   = 90;   //X110 [SORTER] TRAY PICKER CLAMP CHECK SENSOR
        public const short TRAY_PKR_UNCLAMP                 = 91;   //X111 [SORTER] TRAY PICKER UNCALMP CHECK SENSOR
        public const short i112                             = 92;   //X112 [SORTER] 
        public const short i113                             = 93;   //X113 [SORTER] 
        public const short EMPTY_STACKER_LOCK1              = 94;   //X114 [SORTER] BIN TRAY STACKER FORWARD 1 CHECK SENSOR 
        public const short EMPTY_STACKER_UNLOCK1            = 95;   //X115 [SORTER] BIN TRAY STACKER BACKWARD 1 CHECK SENSOR

        //OFFFSET #2 (96~111)
        public const short EMPTY_STACKER_LOCK2              = 96;   //X200 [SORTER] BIN TRAY STACKER FORWARD 2 CHECK SENSOR  
        public const short EMPTY_STACKER_UNLOCK2            = 97;   //X201 [SORTER] BIN TRAY STACKER BACKWARD 2 CHECK SENSOR
        public const short EMPTY_STACKER_LOCK3              = 98;   //X202 [SORTER] BIN TRAY STACKER FORWARD 3 CHECK SENSOR  
        public const short EMPTY_STACKER_UNLOCK3            = 99;   //X203 [SORTER] BIN TRAY STACKER BACKWARD 3 CHECK SENSOR
        public const short EMPTY_STACKER_LOCK4              = 100;  //X204 [SORTER] BIN TRAY STACKER FORWARD 4 CHECK SENSOR  
        public const short EMPTY_STACKER_UNLOCK4            = 101;  //X205 [SORTER] BIN TRAY STACKER BACKWARD 4 CHECK SENSOR
        public const short GOOD_TRAY_STACKER_FULL           = 102;  //X206 [SORTER] GOOD TRAY STACKER FULL CHECK SENSOR
        public const short NG_TRAY_STACKER_FULL             = 103;  //X207 [SORTER] NG TRAY STACKER FULL CHECK SENSOR
        public const short EMPTY_STACKER_NONE               = 104;  //X208 [SORTER] EMPTY STACKER TRAY NONE CHECK SENSOR
        public const short GOOD_TRAY1_FEEDER_TRAY_CHECK     = 105;  //X209 [SORTER] GOOD TRAY 1 CHECK SENSOR
        public const short i0210                            = 106;  //X210 [SORTER] 
        public const short GOOD_TRAY2_FEEDER_TRAY_CHECK     = 107;  //X211 [SORTER] GOODD TRAY 2 CHECK SENSOR
        public const short i0212                            = 108;  //X212 [SORTER] 
        public const short NG_TRAY_FEEDER_TRAY_CHECK        = 109;  //X213 [SORTER] NG TRAY CHECK SENSOR
        public const short i0214                            = 110;  //X214 [SORTER]
        public const short EMPTY_FEEDER_TRAY_CHECK          = 111;  //X215 [SORTER] EMPTY TRAY CHECK

        //OFFSET #3 (112~127)
        public const short i0300                            = 112;  //X300 [SORTER] 
        public const short GOOD_TRAY1_UNGRIP_S              = 113;  //X301 [SORTER] GOOD TRAY TRANSFER 1 TRAY UNCLAMP 1 CHECK SENSOR
        public const short i0302                            = 114;  //X302 [SORTER] 
        public const short GOOD_TRAY1_UNGRIP_C              = 115;  //X303 [SORTER] GOOD TRAY TRANSFER 1 TRAY UNCLAMP 2 CHECK SENSOR
        public const short i0304                            = 116;  //X304 [SORTER] 
        public const short GOOD_TRAY2_UNGRIP_S              = 117;  //X305 [SORTER] GOOD TRAY TRANSFER 2 TRAY UNCLAMP 1 CHECK SENSOR
        public const short i0306                            = 118;  //X306 [SORTER] 
        public const short GOOD_TRAY2_UNGRIP_C              = 119;  //X307 [SORTER] GOOD TRAY TRANSFER 2 TRAY UNCLAMP 2 CHECK SENSOR
        public const short i0308                            = 120;  //X308 [SORTER] 
        public const short NG_TRAY_UNGRIP1                  = 121;  //X309 [SORTER] NG TRAY TRANSFER TRAY UNCLAMP 1 CHECK SENSOR
        public const short i0310                            = 122;  //X310 [SORTER] 
        public const short NG_TRAY_UNGRIP2                  = 123;  //X311 [SORTER] NG TRAY TRANSFER TRAY UNCLAMP 2 CHECK SENSOR
        public const short i0312                            = 124;  //X312 [SORTER] 
        public const short EMPTY_TRAY_UNGRIP1               = 125;  //X313 [SORTER] EMPTY TRANSFER TRAY UNCLAMP 1 CHECK SENSOR
        public const short i0314                            = 126;  //X314 [SORTER] 
        public const short EMPTY_TRAY_UNGRIP2               = 127;  //X315 [SORTER] EMPTY TRANSFER TRAY UNCLAMP 2 CHECK SENSOR

        //OFFSET #4 (128~143)
        public const short GOOD_STACKER_UP                  = 128;  //X400 [SORTER] GOOD TRAY STACKER TABLE UP CHECK SENSOR
        public const short GOOD_STACKER_DN                  = 129;  //X401 [SORTER] GOOD TRAY STACKER TABLE DOWN CHECK SENSOR
        public const short NG_STACKER_UP                    = 130;  //X402 [SORTER] NG TRAY STACKER TABLE UP CHECK SENSOR
        public const short NG_STACKER_DN                    = 131;  //X403 [SORTER] NG TRAY STACKER TABLE DOWN CHECK SENSOR
        public const short EMPTY_FEEDER_FWD                 = 132;  //X404 [SORTER] EMPTY TRAY TRANSFER FORWARD CHECK SENSOR
        public const short EMPTY_FEEDER_BWD                 = 133;  //X405 [SORTER] EMPTY TRAY TRANSFER BACKWARD CHECK SENSOR
        public const short GOOD_RAIL_CONVEYOR_CHECK         = 134;  //X406 [SORTER] GOOD TRAY UNLOADER CONVEYOR TRAY CHECK SENSOR
        public const short i0407                            = 135;  //X407 [SORTER]
        public const short REJECT_BOX_FULL_CHECK1           = 136;  //X408 [SORTER] REJECT BOX FULL CHECK SENSOR #1
        public const short REJECT_BOX_FULL_CHECK2           = 137;  //X409 [SORTER] REJECT BOX FULL CHECK SENSOR #2
        public const short CAM_CAL_ZIG_FWD                  = 138;  //X410 [SORTER] CAMERA CALIBRATION ZIG FORWARD (TEACHING)
        public const short CAM_CAL_ZIG_BWD                  = 139;  //X411 [SORTER] CAMERA CALIBRATION ZIG BACWARD (
        public const short ULD_CONV_READY                   = 140;  //X412 [SORTER] UNLOADER CONVEYOR READY (PLC)
        public const short ULD_CONV_LOADING                 = 141;  //X413 [SORTER] UNLOADER TRAY REPLY (PLC) -> /*UNLOADER_REPLY*/
        public const short ULD_CONV_LOADING_END             = 142;  //X414 [SORTER] UNLOADER INTERLOCK (PLC) -> /*UNLOADER_INTERLOCK*/ 
        public const short UNLAODER_SPARE                   = 143;  //X415 [SORTER] UNLOADER SPARE (PLC)

        //OFFSET #5 (144~159)
        public const short REJECT_BOX                       = 144;  //X500 [SORTER] REJECT BOX CHECK SENSOR
        public const short TRAY_PKR_TRAY_CHECK              = 145;  //X501 [SORTER] STPZ TRAY CHECK SENSOR
        public const short GOOD_RAIL_STACKER_CHECK          = 146;  //X502 [SORTER] OK TABLE TRAY CHECK SENSOR
        public const short NG_RAIL_STACKER_TRAY_CHECK       = 147;  //X503 [SORTER] NG TABLE TRAY CHECK SENSOR
        public const short EMPTY_RAIL_LD_TRAY_CHECK         = 148;  //X504 [SORTER] EMPTY TABLE TRAY CHECK SENSOR
        public const short GOOD_TRAY_PRE_ALIGN_FWD          = 149;  //X505 [SORTER] GOOD TRAY RAIL TRAY ALIGN FORWARD SENSOR
        public const short GOOD_TRAY_PRE_ALIGN_BWD          = 150;  //X506 [SORTER] GOOD TRAY RAIL TRAY ALIGN BACKWARD SENSOR
        public const short i507                             = 151;  //X507 [SORTER] 
        public const short i508                             = 152;  //X508 [SORTER] 
        public const short i0509                            = 153;  //X509 [SORTER]
        public const short STAGE_VACUUM1                    = 154;  //X510 [SORTER] WORK VACUUM #1
        public const short STAGE_VACUUM2                    = 155;  //X511 [SORTER] WORK VACUUM #2
        public const short DRIVER_AIR_PRESSURE              = 156;  //X512 [SORTER] DRIVER AIR PRESSURE
        public const short BLOW_AIR_PRESSURE                = 157;  //X513 [SORTER] BLOW AIR PRESSURE 
        public const short STAGE_AIR_PRESSURE               = 158;  //X514 [SORTER] STAGE AIR PRESSURE
        public const short PICKER_AIR_PRESSURE              = 159;  //X515 [SORTER] PICKER AIR PRESSURE

        //OFFSET #6 [VISION INTERFACE용 O] (160~175)
        public const short VisionRdy                        = 160;  // X600 [VISION INTERFACE] READY
        public const short SearchRoiWriting                 = 161;  // X601 [VISION INTERFACE] MAP-BLOCK UNIT SEARCH ROI TEACHING (ON일 경우 티칭 중 접근하면 안됨)
        public const short UnitDataWrite                    = 162;  // X602 [VISION INTERFACE] MAP-BLOCK UNIT INSPECTION DATA WRITE
        public const short PRSVisionWirte                   = 163;  // X603 [VISION INTERFACE] PRS DATA WRITE
        public const short UnitAlignWrite                   = 164;  // X604 [VISION INTERFACE] 비전 UNIT 얼라인 XYT 값 씀.
        public const short PRSCalWrite                      = 165;  // X605 [VISION INTERFACE] 비전 PICKER CALIBRATION XY 값 씀.
        public const short UnitInspectionReStart            = 166;  // X606 [VISION INTERFACE] 유닛 검사 재검사 신호
        public const short i0607                            = 167;
        public const short i0608                            = 168;
        public const short i0609                            = 169;
        public const short i0610                            = 170;
        public const short i0611                            = 171;
        public const short i0612                            = 172;
        public const short i0613                            = 173;
        public const short i0614                            = 174;
        public const short i0615                            = 175;
        #endregion

        #region >>HEAD1 PICKER VACUUM (176~303)
        ////MODULE #2 - HEAD1 PICKER VACUUM (176~303)
        public const short X1_VAC8 = 176;
        public const short X1_VAC7 = 192;
        public const short X1_VAC6 = 208;
        public const short X1_VAC5 = 224;
        public const short X1_VAC4 = 240;
        public const short X1_VAC3 = 256;
        public const short X1_VAC2 = 272;
        public const short X1_VAC1 = 288;
        #endregion

        #region >>HEAD2 PICKER VACUUM (304~431)
        //MODULE #3 - HEAD2 PICKER VACUUM (304~431)
        public const short X2_VAC1 = 304;
        public const short X2_VAC2 = 320;
        public const short X2_VAC3 = 336;
        public const short X2_VAC4 = 352;
        public const short X2_VAC5 = 368;
        public const short X2_VAC6 = 384;
        public const short X2_VAC7 = 400;
        public const short X2_VAC8 = 416;
        #endregion
#else
        #region >>SorterInputList (0~111)
        //MODULE #0 - OFFSET #0 (0~15)                                                         
        public const short TENKEY1                      = 0;	// X000 [SORTER] TENKEY #1	
        public const short TENKEY2                      = 1;    // X001 [SORTER] TENKEY #2
        public const short TENKEY3                      = 2;    // X002 [SORTER] TENKEY #3
        public const short TENKEY4                      = 3;    // X003 [SORTER] TENKEY #4
        public const short TENKEY5                      = 4;    // X004 [SORTER] TENKEY #5
        public const short TENKEY6                      = 5;    // X005 [SORTER] TENKEY #6
        public const short TENKEY7                      = 6;    // X006 [SORTER] TENKEY #7
        public const short EMO_SORTER_FRONT             = 7;    // X007 [SORTER] FRONT EMO SWITCH 
        public const short EMO_SORTER_RIGHT             = 8;    // X008 [SORTER] RIGHT EMO SWITCH 
        public const short EMO_SORTER_BACK              = 9;    // X009 [SORTER] BACK EMO SWITCH 
        public const short PC_POWER                     = 10;   // X010 [SORTER] PC POWER ON SWITCH
        public const short i0011                        = 11;   // X011 [SORTER] 
        public const short i0012                        = 12;   // X012 [SORTER] 
        public const short START                        = 13;   // X013 [SORTER] START SWITCH SIGNAL
        public const short STOP                         = 14;   // X014 [SORTER] STOP SWITCH SIGNAL
        public const short RESET                        = 15;   // X015 [SORTER] RESET SWITCH SIGNAL

        //OFFSET #1 (16~31)                                                                
        public const short DOOR_SORTER_FRONT_LEFT       = 16;   // X100 [SORTER] FRONT LEFT DOOR CHECK
        public const short DOOR_SORTER_FRONT_RIGHT      = 17;   // X101 [SORTER] FRONT RIGHT DOOR CHECK
        public const short DOOR_SORTER_RIGHT_SIDE_LEFT  = 18;   // X102 [SORTER] RIGHT SIDE OF LEFT DOOR CHECK
        public const short DOOR_SORTER_RIGHT_SIDE_RIGHT = 19;   // X103 [SORTER] RIGHT SIDE OF RIGHT DOOR CHECK
        public const short DOOR_SORTER_BACK_LEFT        = 20;   // X104 [SORTER] BACK LEFT DOOR CHECK
        public const short DOOR_SORTER_BACK_RIGHT       = 21;   // X105 [SORTER] BACK RIGHT DOOR CHECK
        public const short SERVO1_SORTER                = 22;   // X106 [SORTER] SERVO #1 MC30 TRIP SIGNAL
        public const short SERVO2_SORTER                = 23;   // X107 [SORTER] SERVO #2 MC31 TRIP SIGNAL
        public const short SERVO3_SORTER                = 24;   // X108 [SORTER] SERVO #3 MC32 TRIP SIGNAL
        public const short TRAY_PKR_TRAY_CHECK          = 25;   // X109 [SORTER] TRAY PICKER TRAY CHECK SENSOR 
        public const short TRAY_PKR_CLAMP               = 26;   // X110 [SORTER] TRAY PICKER TRAY CLAMP CHECK SENSOR
        public const short TRAY_PKR_UNCLAMP             = 27;   // X111 [SORTER] TRAY PICKER TRAY UNCLAMP CHECK SENSOR
        public const short EMPTY_STACKER_NONE           = 28;   // X112 [SORTER] EMPTY TRAY EMPTY CHECK SENSOR (NOT TRAY) 
        public const short EMPTY_RAIL_LD_TRAY_CHECK     = 29;   // X113 [SORTER] EMPTY TRAY STACKER LOCATION TRAY CHECK SENSOR (트레이 공급단)
        public const short EMPTY_RAIL_ULD_TRAY_CHECK    = 30;   // X114 [SORTER] EMPTY TRAY UNLOADING LOCATION TRAY CHECK SENSOR (트레이 배출단)
        public const short EMPTY_FEEDER_BWD             = 31;   // X115 [SORTER] EMPTY TRAY TRANSFER BACKWARD CHECK SENSOR 

        //OFFSET #2 (32~47)                                                             
        public const short EMPTY_FEEDER_FWD             = 32;   // X200 [SORTER] EMPTY TRAY TRANSFER FORWARD CHECK SENSOR
        public const short EMPTY_TRAY_UNGRIP1           = 33;   // X201 [SORTER] EMPTY TRAY TRANSFER FRONT UNCLAMP CHECK SENSOR
        public const short EMPTY_TRAY_UNGRIP2           = 34;   // X202 [SORTER] EMPTY TRAY TRANSFER REAR UNCLAMP CHECK SENSOR
        public const short EMPTY_FEEDER_TRAY_CHECK      = 35;   // X203 [SORTER] EMPTY TRAY TRANSFER TRAY CHECK SENSOR
        public const short EMPTY_STACKER_LOCK1          = 36;   // X204 [SORTER] EMPTY STACKER STOPPER LOCK1 (LEFT-FRONT) 
        public const short EMPTY_STACKER_UNLOCK1        = 37;   // X205 [SORTER] EMPTY STACKER STOPPER UNLOCK1 (LEFT-FRONT)
        public const short EMPTY_STACKER_LOCK2          = 38;   // X206 [SORTER] EMPTY STACKER STOPPER LOCK2 (LEFT-REAR)
        public const short EMPTY_STACKER_UNLOCK2        = 39;   // X207 [SORTER] EMPTY STACKER STOPPER UNLOCK2 (LEFT-REAR)
        public const short EMPTY_STACKER_LOCK3          = 40;   // X208 [SORTER] EMPTY STACKER STOPPER LOCK3 (RIGHT-FRONT)
        public const short EMPTY_STACKER_UNLOCK3        = 41;   // X209 [SORTER] EMPTY STACKER STOPPER UNLOCK3 (RIGHT-FRONT)
        public const short EMPTY_STACKER_LOCK4          = 42;   // X210 [SORTER] EMPTY STACKER STOPPER LOCK4 (RIGHT-REAR)
        public const short EMPTY_STACKER_UNLOCK4        = 43;   // X211 [SORTER] EMPTY STACKER STOPPER UNLOCK4 (RIGHT-REAR)
        public const short NG_TRAY_STACKER_FULL         = 44;   // X212 [SORTER] NG TRAY STACKER FULL CHECK SENSOR
        public const short NG_RAIL_STACKER_TRAY_CHECK   = 45;   // X213 [SORTER] NG TRAY RAIL STRACKER LOCATION TRAY CHECK SENSOR
        public const short NG_RAIL_LOADING_TRAY_CHECK   = 46;   // X214 [SORTER] NG TRAY RAIL TRAY LOAD LOCATION TRAY CHECK SENSOR
        public const short NG_RAIL_HEAD1_TRAY_CHECK     = 47;   // X215 [SORTER] NG TRAY RAIL HEAD1 PICKUP LOCATION TRAY CHECK SENSOR

        //OFFSET #3 (48~63)     
        public const short NG_RAIL_HEAD2_TRAY_CHECK     = 48;   // X300 [SORTER] NG TRAY RAIL HEAD2 PICKUP LOCATION TRAY CHECK SENSOR
        public const short NG_TRAY_UNGRIP1              = 49;   // X301 [SORTER] NG TRAY TRANSFER FRONT UNCHECK SENSOR
        public const short NG_TRAY_UNGRIP2              = 50;   // X302 [SORTER] NG TRAY TRANSFER REAR UNCHECK SENSOR
        public const short NG_TRAY_FEEDER_TRAY_CHECK    = 51;   // X303 [SORTER] NG TRAY TRANSFER TRAY CHECK SENSOR
        public const short NG_STACKER_UP                = 52;   // X304 [SORTER] NG TRAY STACKER TRAY UP CHECK SENSOR
        public const short NG_STACKER_DN                = 53;   // X305 [SORTER] NG TRAY STACKER TRAY DOWN CHECK SENSOR
        public const short GOOD_TRAY_STACKER_FULL       = 54;   // X306 [SORTER] GOOD TRAY FULL CHECK SENSOR
        public const short GOOD_RAIL_STACKER_CHECK      = 55;   // X307 [SORTER] GOOD TRAY RAIL STRACKER LOCATION TRAY CHECK SENSOR
        public const short GOOD_RAIL_TRAY_LOADING_CHECK = 56;   // X308 [SORTER] GOOD TRAY RAIL LOAD LOCATION TRAY CHECK SENSOR
        public const short GOOD_RAIL_HEAD1_CHECK        = 57;   // X309 [SORTER] GOOD TRAY RAIL HEAD1 PICKUP LOCATION TRAY CHECK SENSOR
        public const short GOOD_RAIL_HEAD2_CHECK        = 58;   // X310 [SORTER] GOOD TRAY RAIL HEAD2 PICKUP LOCATION TRAY CHECK SENSOR
        public const short GOOD_RAIL_CONVEYOR_CHECK     = 59;   // X311 [SORTER] GOOD TRAY RAIL CONVEYOR UNLAOD LOCATION TRAY CHECK SENSOR
        public const short GOOD_TRAY1_UNGRIP_S          = 60;   // X312 [SORTER] GOOD TRAY TRANSFER 1 RAER UNCLAMP SENSOR (CONVEYOR) -> stacker
        public const short GOOD_TRAY1_UNGRIP_C          = 61;   // X313 [SORTER] GOOD TRAY TRANSFER 1 FRONT UNCLAMP SENSOR (STACKER) -> conv
        public const short GOOD_TRAY1_FEEDER_TRAY_CHECK = 62;   // X314 [SORTER] GOOD TRAY TRANSFER 1 TRAY CHECK SENSOR
        public const short GOOD_TRAY2_UNGRIP_S          = 63;   // X315 [SORTER] GOOD TRAY TRANSFER 2 REAR UNCLAMP SENSOR (CONVEYOR) -> stacker

        //OFFSET #4 (64~143)                                                          
        public const short GOOD_TRAY2_UNGRIP_C          = 64;   // X400 [SORTER] GOOD TRAY TRANSFER 2 FRONT UNCLAMP SENSOR (STACKER) -> conv
        public const short GOOD_TRAY2_FEEDER_TRAY_CHECK = 65;   // X401 [SORTER] GOOD TRAY TRANSFER 2 TRAY CHECK SENSOR
        public const short GOOD_STACKER_UP              = 66;   // X402 [SORTER] GOOD TRAY STACKER TRAY UP CHECK SENSOR 
        public const short GOOD_STACKER_DN              = 67;   // X403 [SORTER] GOOD TRAY STACKER TRAY DOWN SENSOR
        public const short GOOD_TRAY_PRE_ALIGN_FWD      = 68;   // X404 [SORTER] GOOD TRAY PRE-ALIGN FORWARD SENSOR
        public const short GOOD_TRAY_PRE_ALIGN_BWD      = 69;   // X405 [SORTER] GOOD TRAY PRE-ALIGN BACKWARD SENSOR
        public const short CAM_CAL_ZIG_FWD              = 70;   // X406 [SORTER] HEAD-BOTTOM CAM CALIBRATION ZIG FORWARD SENSOR
        public const short CAM_CAL_ZIG_BWD              = 71;   // X407 [SORTER] HEAD-BOTTOM CAM CALIBRATION ZIG BACKWARD SENSOR
        public const short REJECT_BOX                   = 72;   // X408 [SORTER] REJECT BOX CHECK SENSOR  //NSS-3320 추가됨
        public const short REJECT_BOX_FULL_CHECK1       = 73;   // X409 [SORTER] REJECT BOX FULL CHECK SENSOR #1 //NSS-3320 추가됨
        public const short REJECT_BOX_FULL_CHECK2       = 74;   // X410 [SORTER] REJECT BOX FULL CHECK SENSOR #2 //NSS-3320 추가됨
        public const short i0411                        = 75;   // X411 [SORTER]
        public const short ULD_CONV_READY               = 76;   // X412 [SORTER] UNLOADER CONVEYOR 투입 허가
        public const short ULD_CONV_LOADING             = 77;   // X413 [SORTER] UNLOADER CONVEYOR 투입 중
        public const short ULD_CONV_LOADING_END         = 78;   // X414 [SORTER] UNLOADER CONVEYOR 투입 완료 
        public const short i0415                        = 79;   // X415 [SORTER] 

        //OFFSET #5 (80~95)
        public const short TRAY_PK_FRONT_ALIGN_UP       = 80;   // X500 [SORTER] TRAY PICKER TRAY FRONT ALING UP
        public const short TRAY_PK_FRONT_ALIGN_DN       = 81;   // X501 [SORTER] TRAY PICKER TRAY FRONT ALIGN DOWN
        public const short TRAY_PK_REAR_ALIGN_UP        = 82;   // X502 [SORTER] TRAY PICKER TRAY REAR ALIGN UP
        public const short TRAY_PK_REAR_ALIGN_DN        = 83;   // X503 [SORTER] TRAY PICKER TRAY REAR ALIGN DOWN
        public const short TRAY_PK_ALIGN_FWD            = 84;   // X504 [SORTER] TRAY PICKER TRAY ALIGN FORWARD
        public const short TRAY_PK_ALIGN_BWD            = 85;   // X505 [SORTER] TRAY PICKER TRAY ALIGN BACKWARD
        public const short i0506                        = 86;   // X506 [SORTER] 
        public const short i0507                        = 87;   // X507 [SORTER] 
        public const short i0508                        = 88;   // X508 [SORTER] 
        public const short i0509                        = 89;   // X509 [SORTER] 
        public const short STAGE_VACUUM1                = 90;   // X510 [SORTER] STAGE1 WORK VACUUM
        public const short STAGE_VACUUM2                = 91;   // X511 [SORTER] STAGE2 WORK VACUUM
        public const short DRIVER_AIR_PRESSURE          = 92;   // X512 [SORTER] DRIVER AIR PRESSURE
        public const short BLOW_AIR_PRESSURE            = 93;   // X513 [SORTER] BLOW AIR PRESSURE
        public const short STAGE_AIR_PRESSURE           = 94;   // X514 [SORTER] STAGE AIR PRESSURE
        public const short PICKER_AIR_PRESSURE          = 95;   // X515 [SORTER] PICKER AIR PRESSURE

        //OFFSET #6 (96~111) [VISION INTERFACE용 DIO] 
        public const short VisionRdy                    = 96;   // X600 [VISION INTERFACE] 비전 프로그램 실행 상태 확인
        public const short SearchRoiWriting             = 97;   // X601 [VISION INTERFACE] 비전 MAP BLOCK UNIT SEARCH ROI 티칭 중이면 ON (ON일 경우 접근하면 안됨)
        public const short UnitDataWrite                = 98;   // X602 [VISION INTERFACE] 비전 MAP BLOCK UNIT 검사 결과 작성 완료 신호
        public const short PRSVisionWirte               = 99;   // X603 [VISION INTERFACE] 비전 PRS 검사 결과 작성 완료 신호
        public const short UnitAlignWrite               = 100;  // X604 [VISION INTERFACE] 비전 UNIT 얼라인 XYT 값 씀.
        public const short PRSCalWrite                  = 101;  // X605 [VISION INTERFACE] 비전 PICKER CALIBRATION XY 값 씀.
        public const short UnitInspectionReStart        = 102;  // X606 [VISION INTERFACE] 유닛 검사 재검사 신호
        public const short i0607                        = 103;  // X607 [VISION INTERFACE]
        public const short i0608                        = 104;  // X608 [VISION INTERFACE]
        public const short i0609                        = 105;  // X609 [VISION INTERFACE]
        public const short i0610                        = 106;  // X610 [VISION INTERFACE]
        public const short i0611                        = 107;  // X611 [VISION INTERFACE]
        public const short i0612                        = 108;  // X612 [VISION INTERFACE]
        public const short i0613                        = 109;  // X613 [VISION INTERFACE]
        public const short i0614                        = 110;  // X614 [VISION INTERFACE]
        public const short i0615                        = 111;  // X615 [VISION INTERFACE]
        #endregion

        #region >>Head1 Picker Vacuum Module (112~239)
        //MODULE #1 - OFFSET #0~7 (112~239) 
        public const short X1_VAC1                      = 112;
        public const short X1_VAC2                      = 128;
        public const short X1_VAC3                      = 144;
        public const short X1_VAC4                      = 160;
        public const short X1_VAC5                      = 176;
        public const short X1_VAC6                      = 192;
        public const short X1_VAC7                      = 208;
        public const short X1_VAC8                      = 224;
        #endregion

        #region >>Head2 Picker Vacuum Module (240~367)
        //MODULE #2 - OFFSET #0~7 (240~367) 
        public const short X2_VAC1                      = 240;
        public const short X2_VAC2                      = 256;
        public const short X2_VAC3                      = 272;
        public const short X2_VAC4                      = 288;
        public const short X2_VAC5                      = 304;
        public const short X2_VAC6                      = 320;
        public const short X2_VAC7                      = 336;
        public const short X2_VAC8                      = 352;
        #endregion

        #region >>SawHandlerInputList (368~431)
        //MODULE #3 - OFFSET #0 (368~383)
        public const short EMO_SAW_FRONT                = 368;  // X700 [SAW] FRONT EMO SWITCH
        public const short EMO_SAW_REAR                 = 369;  // X701 [SAW] REAR EMO SWITCH
        public const short DOOR_SAW_FRONT_LEFT          = 370;  // X702 [SAW] FRONT LEFT DOOR CHECK SENSOR
        public const short DOOR_SAW_FRONT_RIGHT         = 371;  // X703 [SAW] FRONT RIGHT DOOR CHECK SENSOR
        public const short LD_CONV_AREA_SENSOR          = 372;  // X704 [SAW] LOAD CONVEYOR AREA SENSOR
        public const short LD_CONV_MZ_CHECK1            = 373;  // X705 [SAW] LOAD CONVEYOR MAGAZINE CHECK SENSOR1
        public const short LD_CONV_MZ_CHECK2            = 374;  // X706 [SAW] LOAD CONVEYOR MAGAZINE CHECK SENSOR2
        public const short LD_CONV_MZ_ARRIVAL_CHECK     = 375;  // X707 [SAW] LOAD CONVEYOR MAGAZINE FEEDING-END CHECK SENSOR
        public const short ELV_MZ_EXIST1                = 376;  // X708 [SAW] ELEVATOR MAGAZINE EXIST SENSOR1 (F) 
        public const short ELV_MZ_EXIST2                = 377;  // X709 [SAW] ELEVATOR MAGAZINE EXIST SENSOR2 (B)
        public const short ELV_MZ_CLAMP                 = 378;  // X710 [SAW] ELEVATOR MAGAZINE CLAMP SENSOR
        public const short ELV_MZ_UNCLAMP               = 379;  // X711 [SAW] ELEVATOR MAGAZINE UNCLAMP SENSOR
        public const short RAIL_MOUTH                   = 380;  // X712 [SAW] RAIL MOUTH STRIP CHECK SENSOR (STRIP OVERHANG)
        public const short PUSHER_FWD                   = 381;  // X713 [SAW] PUSHER FORWARD CHECK SENSOR
        public const short PUSHER_BWD                   = 382;  // X714 [SAW] PUSHER BACKWARD CHECK SENSOR
        public const short PUSHER_OVERLOAD              = 383;  // X715 [SAW] PUSHER OVERLOAD CHECK SENSOR

        //OFFSET #1 (384~399)
        public const short ULD_CONV_MZ_FULL_CHECK1      = 384;  // X800 [SAW] UNLOAD CONVEYOR MAGZINE FULL CHECK SENSOR1
        public const short ULD_CONV_MZ_FULL_CHECK2      = 385;  // X801 [SAW] UNLOAD CONVEYOR MAGZINE FULL CHECK SENSOR2
        public const short GRIPPER_OPEN                 = 386;  // X802 [SAW] GRIPPER OPEN CHECK SENSOR
        public const short GRIPPER_CLOSE                = 387;  // X803 [SAW] GRIPPER CLOSE CHECK SENSOR
        public const short GRIPPER_DETECT               = 388;  // X804 [SAW] GRIPPER STRIP DETECT CHECK SENSOR
        public const short RAIL_EXIST1                  = 389;  // X805 [SAW] RAIL STRIP CHECK SENSOR1 (F) //매거진 단
        public const short RAIL_EXIST2                  = 390;  // X806 [SAW] RAIL STRIP CHECK SENSOR2  (B) //스트립 픽업 단
        public const short INLET_TABLE_VAC              = 391;  // X807 [SAW] IN-LET TABLE VACUUM CHECK SENSOR
        public const short INLET_TABLE_UP               = 392;  // X808 [SAW] IN-LET TABLE UP CHECK SENSOR
        public const short INLET_TABLE_DN               = 393;  // X809 [SAW] IN-LET TABLE DOWN CHECK SENSOR
        public const short HANDLER_PK_CRASH             = 394;  // X810 [SAW] STRIP/UNIT PICKER COLLISION CHECK SENSOR
        public const short STRIP_PK_VAC                 = 395;  // X811 [SAW] STRIP PICKER VACUUM CHECK SENSOR
        public const short UNIT_PK_VAC                  = 396;  // X812 [SAW] UNIT PICKER VACUUM CHECK SENSOR
        public const short SCRAP_VAC1                   = 397;  // X813 [SAW] SCRAP VACUUM CHECK SENSOR1
        public const short SCRAP_VAC2                   = 398;  // X814 [SAW] SCRAP VACUUM CHECK SENSOR2
        public const short SCRAP_BOX                    = 399;  // X815 [SAW] SCRAP BOX ATTACH CHECK SENSOR

        ////MODULE #4 - OFFSET #0 (400~415)
        public const short CLEANER_SWING_RIGHT          = 400;   // X900 [SAW] CLEANER SWING RIGHT CHECK SENSOR
        public const short CLEANER_SWING_LEFT           = 401;   // X901 [SAW] CLEANER SWING LEFT CHECK SENSOR
        public const short SERVO1_SAW                   = 402;   // X902 [SAW] SAW SERVO POWER DOWN CHECK SIGNAL (SAW)
        public const short SERVO2_SAW                   = 403;   // X903 [SAW] SAW SERVO POWER DOWN CHECK SIGNAL (SAW)
        public const short SERVO3_SAW                   = 404;   // X904 [SAW] SAW SERVO POWER DOWN CHECK SIGNAL (HANDLER)
        public const short SERVO4_SAW                   = 405;   // X905 [SAW] SAW SERVO POWER DOWN CHECK SIGNAL (HANDLER)
        public const short CONV_TRIP                    = 406;   // X906 [SAW] CONVEYOR INVERTER ELCB TRIP CHECK
        public const short CONV_POWER                   = 407;   // X907 [SAW] CONVEYOR INVERTER POWER CHECK
        public const short CONV_ERR                     = 408;   // X908 [SAW] CONVEYOR INVERTER ERROR SIGNAL
        public const short DOOR_SAW_SIDE_LEFT           = 409;   // X909 [SAW] LEFT DOOR CHECK SENSOR (MGZ DOOR)
        public const short CLEANER_WATER_FLOW           = 410;   // X910 [SAW] CLEANER WATAR FLOW CHECK SENSOR // NSS-3320 추가
        public const short BLOW_AIR_1                   = 411;   // X911 [SAW] BLOW AIR #1 (STRIP/UNIT PICKER VACUUM) //NSS-3320 추가
        public const short BLOW_AIR_2                   = 412;   // X912 [SAW] BLOW AIR #2 (UNIT PK SCRAP 1/2 VACUUM) //NSS-3320 추가
        public const short i0913                        = 413;   // X913 [SAW]
        public const short i0914                        = 414;   // X914 [SAW]
        public const short i0915                        = 415;   // X915 [SAW]

        //OFFSET #1 (416~431)
        public const short SAW_READY                    = 416;   // X1000 [SAW INTERFACE] READY 프로그램 실행 상태
        public const short SAW_CUTTING                  = 417;   // X1001 [SAW INTERFACE] CUTTING 작업 중일 경우
        public const short SAW_SPARE_2                  = 418;   // X1002 [SAW INTERFACE] 
        public const short SAW_INITIAL                  = 419;   // X1003 [SAW INTERFACE] INTIAL 초기화 동작 하거나 CUTTING 작업 중 JOB CANCEL 실행 시
        public const short SAW_LD_REQ                   = 420;   // X1004 [SAW INTERFACE] LOADING REQ 로딩 요청 (PCB 달라고 요청함)
        public const short SAW_LD_POS                   = 421;   // X1005 [SAW INTERFACE] STAGE LOADING POS 작업 테이블 로딩 위치에서 대기 중
        public const short SAW_STAGE_VAC_ON             = 422;   // X1006 [SAW INTERFACE] STAGE VAC ON 작업 테이블 진공 ON시
        public const short SAW_ULD_REQ                  = 423;   // X1007 [SAW INTERFACE] UNLOADING REQ 언로딩 요청 (PCB SAW 작업 완료 후 배출 요청함)
        public const short SAW_ULD_POS                  = 424;   // X1008 [SAW INTERFACE] STAGE UNLOADING POS 작업 테이블 언로딩 위치에서 대기 중
        public const short SAW_STAGE_BLOW               = 425;   // X1009 [SAW INTERFACE] STAGE BLOW 작업 테이블 파기 ON
        public const short SAW_LD_COMPLETE              = 426;   // X1010 [SAW INTERFACE] SAW LOADING COMPLETE (다이싱 1/2CH 얼라인 정상일 경우)
        public const short SAW_REPICK_ALIGN             = 427;   // X1011 [SAW INTERFACE] RE-PICK ALIGN 
        public const short SAW_LD_REPICK_POS            = 428;   // X1012 [SAW INTERFACE] STAGE RE-PICKUP 위치에서 대기 중 
        public const short SAW_SPARE_13                 = 429;   // X1013 [SAW INTERFACE] 
        public const short SAW_SPARE_14                 = 430;   // X1014 [SAW INTERFACE] 
        public const short SAW_MAIN_AIR                 = 431;   // X1015 [SAW INTERFACE] MAIN AIR STATUS
        #endregion
#endif

        #region >>INTPUT ARRAY
        public static int[] Null                        = { };
        public static int[] StageVac                    = { STAGE_VACUUM1, STAGE_VACUUM2 };
        public static int[] EmptyStopperLock            = { EMPTY_STACKER_LOCK1, EMPTY_STACKER_LOCK2, EMPTY_STACKER_LOCK3, EMPTY_STACKER_LOCK4 };
        public static int[] EmptyStopperUnlock          = { EMPTY_STACKER_UNLOCK1, EMPTY_STACKER_UNLOCK2, EMPTY_STACKER_UNLOCK3, EMPTY_STACKER_UNLOCK4 };
        public static int[] EmptyFeederUnGrip           = { EMPTY_TRAY_UNGRIP1, EMPTY_TRAY_UNGRIP2 };
#if _NSS3300
#else
        public static int[] PlaceRailTrayCheck          = { GOOD_RAIL_HEAD1_CHECK, GOOD_RAIL_HEAD2_CHECK };
        public static int[] LDTrayFeeder                = { GOOD_RAIL_TRAY_LOADING_CHECK, GOOD_RAIL_TRAY_LOADING_CHECK, NG_RAIL_LOADING_TRAY_CHECK };
        public static int[] TrayAlignUp                 = { TRAY_PK_FRONT_ALIGN_UP, TRAY_PK_REAR_ALIGN_UP };
        public static int[] TrayAlignDn                 = { TRAY_PK_FRONT_ALIGN_DN, TRAY_PK_REAR_ALIGN_DN };
#endif
        public static int[] GoodFeeder1Grip             = { };
        public static int[] GoodFeeder1UnGrip           = { GOOD_TRAY1_UNGRIP_C, GOOD_TRAY1_UNGRIP_S };
        public static int[] GoodFeeder1UnGripC          = { GOOD_TRAY1_UNGRIP_C };
        public static int[] GoodFeeder1UnGripS          = { GOOD_TRAY1_UNGRIP_S };
        public static int[] GoodFeeder2Grip             = { };
        public static int[] GoodFeeder2UnGrip           = { GOOD_TRAY2_UNGRIP_C, GOOD_TRAY2_UNGRIP_S };
        public static int[] GoodFeeder2UnGripC          = { GOOD_TRAY2_UNGRIP_C };
        public static int[] GoodFeeder2UnGripB          = { GOOD_TRAY2_UNGRIP_S };
        public static int[] GoodTrayFrontUnGrip         = { GOOD_TRAY1_UNGRIP_C, GOOD_TRAY2_UNGRIP_C };
        public static int[] GoodTrayBackUnGrip          = { GOOD_TRAY1_UNGRIP_S, GOOD_TRAY2_UNGRIP_S };
        public static int[] ReworkFeederGrip            = { };
        public static int[] ReworkFeederUnGrip          = { NG_TRAY_UNGRIP1, NG_TRAY_UNGRIP2 };
        public static int[] TrayCheck                   = { GOOD_TRAY1_FEEDER_TRAY_CHECK, GOOD_TRAY2_FEEDER_TRAY_CHECK, NG_TRAY_FEEDER_TRAY_CHECK };

#if _NSS3300
        public static int[] MNInput = { ELV_MZ_CLAMP, ELV_MZ_UNCLAMP, PUSHER_FWD, PUSHER_BWD, INLET_TABLE_UP, INLET_TABLE_DN, GRIPPER_OPEN, GRIPPER_CLOSE,
                                        STRIP_PK_VAC, UNIT_PK_VAC, SCRAP_VAC1, SCRAP_VAC2, CLEANER_SWING_RIGHT, CLEANER_SWING_LEFT,
                                        STAGE_VACUUM1, CAM_CAL_ZIG_FWD, CAM_CAL_ZIG_BWD,
                                        GOOD_TRAY1_UNGRIP_C, GOOD_TRAY1_UNGRIP_S, GOOD_TRAY2_UNGRIP_C, GOOD_TRAY2_UNGRIP_S, GOOD_STACKER_UP, GOOD_STACKER_DN, NG_TRAY_UNGRIP1, NG_TRAY_UNGRIP2,GOOD_TRAY_PRE_ALIGN_FWD, GOOD_TRAY_PRE_ALIGN_BWD, NG_STACKER_UP, NG_STACKER_DN,
                                        TRAY_PKR_CLAMP, TRAY_PKR_UNCLAMP, EMPTY_STACKER_LOCK1, EMPTY_STACKER_LOCK2, EMPTY_STACKER_LOCK3, EMPTY_STACKER_LOCK4, EMPTY_STACKER_UNLOCK1, EMPTY_STACKER_UNLOCK2, EMPTY_STACKER_UNLOCK3, EMPTY_STACKER_UNLOCK4, EMPTY_TRAY_UNGRIP1, EMPTY_TRAY_UNGRIP2, EMPTY_FEEDER_FWD, EMPTY_FEEDER_BWD
        };
#else
        public static int[] MNInput = { ELV_MZ_CLAMP, ELV_MZ_UNCLAMP, PUSHER_FWD, PUSHER_BWD, INLET_TABLE_UP, INLET_TABLE_DN, GRIPPER_OPEN, GRIPPER_CLOSE,
                                        STRIP_PK_VAC, UNIT_PK_VAC, SCRAP_VAC1, SCRAP_VAC2, CLEANER_SWING_RIGHT, CLEANER_SWING_LEFT,
                                        STAGE_VACUUM1, CAM_CAL_ZIG_FWD, CAM_CAL_ZIG_BWD,
                                        GOOD_TRAY1_UNGRIP_C, GOOD_TRAY1_UNGRIP_S, GOOD_TRAY2_UNGRIP_C, GOOD_TRAY2_UNGRIP_S, GOOD_STACKER_UP, GOOD_STACKER_DN, NG_TRAY_UNGRIP1, NG_TRAY_UNGRIP2,GOOD_TRAY_PRE_ALIGN_FWD, GOOD_TRAY_PRE_ALIGN_BWD, NG_STACKER_UP, NG_STACKER_DN,
                                        TRAY_PKR_CLAMP, TRAY_PKR_UNCLAMP, EMPTY_STACKER_LOCK1, EMPTY_STACKER_LOCK2, EMPTY_STACKER_LOCK3, EMPTY_STACKER_LOCK4, EMPTY_STACKER_UNLOCK1, EMPTY_STACKER_UNLOCK2, EMPTY_STACKER_UNLOCK3, EMPTY_STACKER_UNLOCK4, EMPTY_TRAY_UNGRIP1, EMPTY_TRAY_UNGRIP2, EMPTY_FEEDER_FWD, EMPTY_FEEDER_BWD,
                                        INLET_TABLE_VAC
        };
#endif

        public static int[] InterfaceState = {
            SAW_LD_REQ, SAW_LD_POS, SAW_STAGE_VAC_ON, SAW_ULD_REQ, SAW_ULD_POS, SAW_STAGE_BLOW, ULD_CONV_READY
        };
#endregion

        public static void GET_MODULE_START_END(){
#if _NSS3300
            CNT_.InSortStart    = 64;
            CNT_.InSortEnd      = 175;

            CNT_.InSawStart     = 0;
            CNT_.InSawEnd       = 63;
#else
            CNT_.InSortStart    = 0;
            CNT_.InSortEnd      = 111;

            CNT_.InSawStart     = 368;
            CNT_.InSawEnd       = 431;
#endif
        }
    } //INPUT DEFINE
}