namespace NSS_3310S
{
    public class O
    {
        #region >>SoterOutputList (0~95)
        //MODULE #0 - OFFSET #0 (0~15)
        public const short TENKEY1                      = 0;    // Y000 [SORTER] TENKEY #1								
        public const short TENKEY2                      = 1;    // Y001 [SORTER] TENKEY #2       							
        public const short TENKEY3                      = 2;    // Y002 [SORTER] TENKEY #3
        public const short TENKEY4                      = 3;    // Y003 [SORTER] TENKEY #4
        public const short TENKEY5                      = 4;    // Y004 [SORTER] TENKEY #5
        public const short TENKEY6                      = 5;    // Y005 [SORTER] TENKEY #6
        public const short TENKEY7                      = 6;    // Y006 [SORTER] TENKEY #7
        public const short TENKEY8                      = 7;    // Y007 [SORTER] TENKEY #8
        public const short TENKEY9                      = 8;    // Y008 [SORTER] TENKEY #9
        public const short TENKEY10                     = 9;    // Y009 [SORTER] TENKEY #10
        public const short PC_POWER_LAMP                = 10;   // Y010 [SORTER] HANDLER PC POWER SWITCH LED 
        public const short PC_VISION_LAMP               = 11;   // Y011 [SORTER] VISION PC POWER SITCH LED         							
        public const short o0012                        = 12;   // Y012 [SORTER]         							
        public const short START                        = 13;   // Y013 [SORTER] START SWITCH LED        							
        public const short STOP                         = 14;   // Y014 [SORTER] STOP SWITCH LED
        public const short RESET                        = 15;   // Y015 [SORTER] RESET SWITCH LED

        //OFFSET #1 (16~31)        
        public const short DOOR_LOCK                    = 16;   // Y100 [SORTER] DOOR LOCK SIGNAL
        public const short o0101                        = 17;   // Y101 [SORTER] 
        public const short FLUORESENT_LIGHT             = 18;   // Y102 [SORTER] FLUORESCENT LIGHT ON/OFF SIGNAL
        public const short SERVO_CONTROL_POWER          = 19;   // Y103 [SORTER] SERVO CONTROL POWER SIGNAL
        public const short o0104                        = 20;   // Y104 [SORTER] 
        public const short o0105                        = 21;   // Y105 [SORTER]
        public const short TOWER_RED                    = 22;   // Y106 [SORTER] TOWER LAMP LED : RED
        public const short TOWER_YELLOW                 = 23;   // Y107 [SORTER] TOWER LAMP LED : YELLOW
        public const short TOWER_GREEN                  = 24;   // Y108 [SORTER] TOWRE LAMP LED : GREEN
        public const short BUZZER_ERR                   = 25;   // Y109 [SORTER] TOWER LAMP BUZZER : ERROR
        public const short BUZZER_END                   = 26;   // Y110 [SORTER] TOWER LAMP BUZZER : END
        public const short o0111                        = 27;   // Y111 [SORTER]
        public const short o0112                        = 28;   // Y112 [SORTER]
        public const short o0113                        = 29;   // Y113 [SORTER]
        public const short o0114                        = 30;   // Y114 [SORTER]
        public const short o0115                        = 31;   // Y115 [SORTER]

        //OFFSET #2 (32~47)    
        public const short EMPTY_STACKER_LOCK1          = 32;   // Y200 [SORTER] EMPTY STACKER STOPPER LOCK #1 
        public const short EMPTY_STACKER_UNLOCK1        = 33;   // Y201 [SORTER] EMPTY STACKER STOPPER UNLOCK #1
        public const short EMPTY_STACKER_LOCK2          = 34;   // Y202 [SORTER] EMPTY STACKER STOPPER LOCK #2
        public const short EMPTY_STACKER_UNLOCK2        = 35;   // Y203 [SORTER] EMPTY STACKER STOPPER UNLCOK #2
        public const short GOOD_TRAY1_UNGRIP_C          = 36;   // Y204 [SORTER] GOOD TRAY TRANSFER1 FRONT UNCLAMP (conv')
        public const short GOOD_TRAY1_GRIP_C            = 37;   // Y205 [SORTER] GOOD TRAY TRANSFER1 FRONT CLAMP (conv')
        public const short GOOD_TRAY1_UNGRIP_S          = 38;   // Y206 [SORTER] GOOD TRAY TRANSFER1 BACK UNCLAMP (stacker)
        public const short GOOD_TRAY1_GRIP_S            = 39;   // Y207 [SORTER] GOOD TRAY TRANSFER1 BACK CLAMP (stacker)
        public const short GOOD_TRAY2_UNGRIP_C          = 40;   // Y208 [SORTER] GOOD TRAY TRANSFER2 FRONT UNCLAMP (conv')
        public const short GOOD_TRAY2_GRIP_C            = 41;   // Y209 [SORTER] GOOD TRAY TRANSFER2 FRONT CLAMP (conv')
        public const short GOOD_TRAY2_UNGRIP_S          = 42;   // Y210 [SORTER] GOOD TRAY TRANSFER2 BACK UNCLAMP (stacker)
        public const short GOOD_TRAY2_GRIP_S            = 43;   // Y211 [SORTER] GOOD TRAY TRANSFER2 BACK CLAMP (stacker)
        public const short NG_TRAY_UNGRIP               = 44;   // Y212 [SORTER] NG TRAY TRANSFER UNCLAMP
        public const short NG_TRAY_GRIP                 = 45;   // Y213 [SORTER] NG TRAY TRANSFER CLAMP
        public const short EMPTY_TRAY_UNGRIP            = 46;   // Y214 [SORTER] EMPTY TRAY TRANSFER UNCLAMP 
        public const short EMPTY_TRAY_GRIP              = 47;   // Y215 [SORTER] EMPTY TRAY TRANSFER CLAMP

        //OFFSET #3 (48~63)
        public const short GOOD_STACKER_UP              = 48;   // Y300 [SORTER] GOOD TRAY TABLE UP
        public const short GOOD_STACKER_DN              = 49;   // Y301 [SORTER] GOOD TRAY TABLE DOWN
        public const short NG_STACKER_UP                = 50;   // Y302 [SORTER] NG TRAY TABLE UP
        public const short NG_STACKER_DN                = 51;   // Y303 [SORTER] NG TRAY TABLE DOWN
        public const short EMPTY_TRAY_FWD               = 52;   // Y304 [SORTER] EMPTY TRAY TRANSFER TRAY FORWARD 
        public const short EMPTY_TRAY_BWD               = 53;   // Y305 [SORTER] EMPTY TRAY TRANSEFR TRAY BACKWARD 
        public const short TRAY_PK_UNGRIP               = 54;   // Y306 [SORTER] TRAY PICKER TRAY UNCLAMP
        public const short TRAY_PK_GRIP                 = 55;   // Y307 [SORTER] TRAY PICKER TRAY CLAMP
        public const short STAGE1_BLOW                  = 56;   // Y308 [SORTER] STAGE1 BLOW
        public const short STAGE2_BLOW                  = 57;   // Y309 [SORTER] STAGE2 BLOW
        public const short TOP_VISION_BLOW              = 58;   // Y310 [SORTER] TOP VISION BLOW
        public const short BTM_VISION_BLOW              = 59;   // Y311 [SORTER] BOTTOM VISION BLOW 
        public const short STAGE1_VAC                   = 60;   // Y312 [SORTER] STAGE1 WORK VACUUM
        public const short STAGE1_BACK_VAC              = 61;   // Y313 [SORTER] STAGE1 WORK BACK-VACUUM
        public const short STAGE1_DRAIN                 = 62;   // Y314 [SORTER] STAGE1 WATER DRAIN
        public const short STAGE2_VAC                   = 63;   // Y315 [SORTER] STAGE2 WORK VACUUM 

        //OFFSET #4 (64~79)  
        public const short STAGE2_BACK_VAC              = 64;   // Y400 [SORTER] STAGE2 WORK BACK-VACUUM
        public const short STAGE2_DRAIN                 = 65;   // Y401 [SORTER] STAGE2 WATER DRAIN
        public const short STAGE1_VAC_OFF               = 66;   // Y402 [SORTER] STAGE1 VACUUM OFF
        public const short STAGE2_VAC_OFF               = 67;   // Y403 [SORTER] STAGE2 VACUUM OFF
        public const short CAM_CAL_ZIG_FWD              = 68;   // Y404 [SORTER] CAMERA CALIBRATION ZIG FORWARD
        public const short CAM_CAL_ZIG_BWD              = 69;   // Y405 [SORTER] CAMERA CALIBRATION ZIG BACKWARD
        public const short GOOD_TRAY_PRE_ALIGN_FWD      = 70;   // Y406 [SORTER] GOOD TRAY PRE-ALIGN FORWARD SENSOR
        public const short GOOD_TRAY_PRE_ALIGN_BWD      = 71;   // Y407 [SORTER] GOOD TRAY PRE-ALIGN BACKWARD SENSOR
        public const short TRAY_PK_TRAY_ALIGN_UP        = 72;   // Y408 [SORTER] TRAY PICKER TRAY ALIGN UP
        public const short TRAY_PK_TRAY_ALIGN_DN        = 73;   // Y409 [SORTER] TRAY PICKER TRAY ALIGN DOWN
        public const short TRAY_PK_TRAY_ALIGN_FWD       = 74;   // Y410 [SORTER] TRAY PICKER TRAY ALIGN FORWARD
        public const short TRAY_PK_TRAY_ALIGN_BWD       = 75;   // Y411 [SORTER] TRAY PICKER TRAY ALIGN BACKWARD
        public const short o0412                        = 76;   // Y412 [SORTER] UNLAODING CONVEYOR 1   
        public const short o0413                        = 77;   // Y413 [SORTER] UNLAODING CONVEYOR 2 
        public const short UldConveyorTrayUnloading     = 78;   // Y414 [SORTER] TRAY UNLOADING END
        public const short o0415                        = 79;   // Y415 [SORTER] UNLAODING CONVEYOR 3

        //OFFSET #5 (80~95)
        public const short MapBlockUnitInfoWriting      = 80;   // Y500 [VISION INTERFACE] MAP BLOCK INFO WRITTING 1
        public const short MapBlockInspectionStart      = 81;   // Y501 [VISION INTERFACE] MAP BLOCK INSPECTION START 2
        public const short UnitReading                  = 82;   // Y502 [VISION INTERFACE] MAP BLOCK UNIT INSPECTION 결과 값 읽음 3
        public const short PRSStart                     = 83;   // Y503 [VISION INTERFACE] PRS INPECTION START 4
        public const short PRSReading                   = 84;   // Y504 [VISION INTERFACE] PRS INSPECTION RESULT 읽음. 5
        public const short RECIPE_CHANGE                = 85;   // Y505 [VISION INTERFACE] RECIPE CHANGE 6
        public const short SelectMapBlock               = 86;   // Y506 [VISION INTERFACE] MAP-BLOCK (ON : TABLE 1 / OFF : TABLE 2) 7
        public const short UnitAlignReading             = 87;   // Y507 [VISION INTERFACE] UNIT 맵칭 XYT 값 읽음. 8
        public const short PkCalStart                   = 88;   // Y508 [VISION INTERFACE] PICKER CALIBRATION STAR T  9
        public const short PkCalReading                 = 89;   // Y509 [VISION INTERFACE] PICKER CALIBRATION 결과 값 읽음 10
        public const short UsePkCal                     = 90;   // Y510 [VISION INTERFACE] USE PICKER CALIBRATION (조명 ON) 11
        public const short NotUsePkCal                  = 91;   // Y511 [VISION INTERFACE] NOT USE PICKER CALIBRATION (조명 OFF) 12
        public const short UnitReStart                  = 92;   // Y512 [VISION INTERFACE] UNIT RESTART 13
        public const short o0513                        = 93;   // Y513 [VISION INTERFACE]
        public const short o0514                        = 94;   // Y514 [VISION INTERFACE]
        public const short o0515                        = 95;   // Y515 [VISION INTERFACE]
        #endregion

        #region >>Head1 Picker Vacuum Module (96~223)
        //MODULE #1 - OFFSET #0 (96~111)
        public const short X1_BLOW1                     = 106;
        public const short X1_VAC1                      = 107;
        //OFFSET #1 (112~127)
        public const short X1_BLOW2                     = 122;
        public const short X1_VAC2                      = 123;
        //OFFSET #2 (128~143)
        public const short X1_BLOW3                     = 138;
        public const short X1_VAC3                      = 139;
        //OFFSET #3 (144~159)
        public const short X1_BLOW4                     = 154;
        public const short X1_VAC4                      = 155;
        //OFFSET #4 (160~175)
        public const short X1_BLOW5                     = 170;
        public const short X1_VAC5                      = 171;
        //OFFSET #5 (176~191)
        public const short X1_BLOW6                     = 186;
        public const short X1_VAC6                      = 187;
        //OFFSET #6 (192~207)
        public const short X1_BLOW7                     = 202;
        public const short X1_VAC7                      = 203;
        //OFFSET #7 (208~223)
        public const short X1_BLOW8                     = 218;
        public const short X1_VAC8                      = 219;
        #endregion

        #region >>Head2 Picker Vacuum Module (224~351)
        //MODULE #2 - OFFSET #0 (244~239)
        public const short X2_BLOW1                     = 234;
        public const short X2_VAC1                      = 235;
        //OFFSET #1 (240~255)                                                                       
        public const short X2_BLOW2                     = 250;
        public const short X2_VAC2                      = 251;
        //OFFSET #2 (256~271)                                                                       
        public const short X2_BLOW3                     = 266;
        public const short X2_VAC3                      = 267;
        //OFFSET #3 (272~287)                                                                       
        public const short X2_BLOW4                     = 282;
        public const short X2_VAC4                      = 283;
        //OFFSET #4 (288~303)                                                                       
        public const short X2_BLOW5                     = 298;
        public const short X2_VAC5                      = 299;
        //OFFSET #5 (304~319)                                                                       
        public const short X2_BLOW6                     = 314;
        public const short X2_VAC6                      = 315;
        //OFFSET #6 (320~335)                                                       
        public const short X2_BLOW7                     = 330;
        public const short X2_VAC7                      = 331;
        //OFFSET #7 (336~351)                                                                       
        public const short X2_BLOW8                     = 346;
        public const short X2_VAC8                      = 347;

        ////MODULE #2 - OFFSET #0 (244~239) //s
        //public const short X2_BLOW8                       = 234;
        //public const short X2_VAC8                        = 235;
        ////OFFSET #1 (240~255)
        //public const short X2_BLOW7                       = 250;
        //public const short X2_VAC7                        = 251;
        ////OFFSET #2 (256~271)
        //public const short X2_BLOW6                       = 266;
        //public const short X2_VAC6                        = 267;
        ////OFFSET #3 (272~287)
        //public const short X2_BLOW5                       = 282;
        //public const short X2_VAC5                        = 283;
        ////OFFSET #4 (288~303)
        //public const short X2_BLOW4                       = 298;
        //public const short X2_VAC4                        = 299;
        ////OFFSET #5 (304~319)
        //public const short X2_BLOW3                       = 314;
        //public const short X2_VAC3                        = 315;
        ////OFFSET #6 (320~335)
        //public const short X2_BLOW2                       = 330;
        //public const short X2_VAC2                        = 331;
        ////OFFSET #7 (336~351)
        //public const short X2_BLOW1                       = 346;
        //public const short X2_VAC1                        = 347;
        #endregion

        #region >>SawHandlerOuputList (352~415)
        //MODULE #5 - OFFSET #0 (352~367)
        public const short SAW_DOOR_LOCK                = 352;  // Y0600 [SAW] SAW SIDE LEFT DOOR LOCK/UNLOCK
        public const short o0601                        = 353;  // Y0601 [SAW]
        public const short LD_CONV_INVERTER_POWER       = 354;  // Y0602 [SAW] LOAD CONVEYOR INVERTER POWER SIGNAL
        public const short LD_CONV_CW                   = 355;  // Y0603 [SAW] LOAD CONVEYOR CW
        public const short LD_CONV_CCW                  = 356;  // Y0604 [SAW] LOAD CONVEYOR CCW
        public const short LD_CONV_STOP                 = 357;  // Y0605 [SAW] LOAD CONVEYOR STOP
        public const short LD_CONV_INVERTER_RESET       = 358;  // Y0606 [SAW] LOAD CONVEYOR INVERTER RESET
        public const short ELV_UNCLAMP                  = 359;  // Y0607 [SAW] ELEVATOR MAGAZINE UNCLAMP
        public const short ELV_CLAMP                    = 360;  // Y0608 [SAW] ELEVATOR MAGAZINE CLAMP
        public const short PUSHER_FWD                   = 361;  // Y0609 [SAW] PUSHER FORWARD
        public const short PUSHER_BWD                   = 362;  // Y0610 [SAW] PUSHER BACKWARD
        public const short INLET_TABLE_UP               = 363;  // Y0611 [SAW] IN-LET TABLE UP
        public const short INLET_TABLE_DN               = 364;  // Y0612 [SAW] IN-LET TABLE DOWN
        public const short INLET_TABLE_VAC              = 365;  // Y0613 [SAW] IN-LET TABLE VACUUM
        public const short INLET_TABLE_BACK_VAC         = 366;  // Y0614 [SAW] IN-LET TABLE BACK-VACUUM
        public const short GRIPPER_OPEN                 = 367;  // Y0615 [SAW] GRIPPER OPEN

        //OFFSET #1 (368~383)    
        public const short GRIPPER_CLOSE                = 368;  // Y0700 [SAW] GIPPER CLOSE
        public const short CLEANER_SWING_R              = 369;  // Y0701 [SAW] CLEANER SWING RIGHT SOL
        public const short CLEANER_SWING_L              = 370;  // Y0702 [SAW] CLEANER SWING LEFT SOL
        public const short CLEANER_AIR_KNIFE            = 371;  // Y0703 [SAW] CLEANER AIR KNIFE SOL
        public const short BRUSH_WATER                  = 372;  // Y0704 [SAW] 
        public const short CLEANER_WATER_1              = 373;  // Y0705 [SAW] CLEANER UNIT WATER CLEAN 1
        public const short CLEANER_AIR_1                = 374;  // Y0706 [SAW] CLEANER UNIT AIR CLEAN 1
        public const short CLEANER_WATER_2              = 375;  // Y0707 [SAW] CLEANER UNIT WATER CLEAN 2
        public const short CLEANER_AIR_2                = 376;  // Y0708 [SAW] CLEANER UNIT AIR CLEAN 2
        public const short STRIP_PK_VAC                 = 377;  // Y0709 [SAW] STRIP PICKER VACCUM
        public const short STRIP_PK_PURGE               = 378;  // Y0710 [SAW] STRIP PICKER PURGE
        public const short STRIP_PK_BLOW                = 379;  // Y0711 [SAW] STRIP PICKER BLOW 
        public const short UNIT_PK_VAC                  = 380;  // Y0712 [SAW] UNIT PICKER VACCUM 
        public const short UNIT_PK_PURGE                = 381;  // Y0713 [SAW] UNIT PICKER PURGE 
        public const short UNIT_PK_BLOW                 = 382;  // Y0714 [SAW] UNIT PICKER BLOW
        public const short SCRAP_VAC_1                  = 383;  // Y0715 [SAW] SCRAP VACUUM 1 

        //MODULE #6 - OFFSET #0 (384~399)
        public const short SCRAP_PURGE_1                = 384;  // Y0800 [SAW] SCRAP PURGE 1 
        public const short SCRAP_BLOW_1                 = 385;  // Y0801 [SAW] SCRAP BLOW 1
        public const short SCRAP_VAC_2                  = 386;  // Y0802 [SAW] SCRAP VACCUM 2
        public const short SCRAP_PURGE_2                = 387;  // Y0803 [SAW] SCRAP PURGE 2
        public const short SCRAP_BLOW_2                 = 388;  // Y0804 [SAW] SCRAP BLOW 2
        public const short STRIP_PK_VAC_OFF             = 389;  // Y0805 [SAW] STRIP PICKER VACUUM OFF //200609 추가됨
        public const short UNIT_PK_VAC_OFF              = 390;  // Y0806 [SAW] UNIT PICKER VACUUM OFF
        public const short SCRAP1_VAC_OFF               = 391;  // Y0807 [SAW] STRAP VACUUM OFF
        public const short SCRAP2_VAC_OFF               = 392;  // Y0808 [SAW] STRAP VACUUM OFF
        public const short QUAD_PCB                     = 393;  // Y0809 [SAW] QUAD STRIP TYPE
        public const short o0810                        = 394;  // Y0810 [SAW] 
        public const short o0811                        = 395;  // Y0811 [SAW]
        public const short o0812                        = 396;  // Y0812 [SAW]   
        public const short o0813                        = 397;  // Y0813 [SAW] 
        public const short o0814                        = 398;  // Y0814 [SAW] 
        public const short o0815                        = 399;  // Y0815 [SAW] 

        //OFFSET #1 (400~415)
        public const short HANDLER_READY                = 400;  // Y0900 [SAW INTERFACCE] HANDLER READY 핸들러 프로그램 실행 상태
        public const short HANDLER_SCRAP_CHECK          = 401;  // Y0901 [SAW INTERFACCE] HANDLER UNIT 픽업시 스크랩 남아 있으면 500msec ON
        public const short HANDLER_INITIAL_OK           = 402;  // Y0902 [SAW INTERFACCE] HANDLER INITIAL OK 다이싱 로딩/언로딩 요청 비트 초기화 요청하여 완료 확인
        public const short HANDLER_STRIP_PK_X_PLACE_POS = 403;  // Y0903 [SAW INTERFACCE] HANDLER STRIP PICKER X PLACE POSITION 스트립 피커 X축 자재 스테이즈에 내려 놓는 위치
        public const short HANDLER_STRIP_PK_Z_PLACE_POS = 404;  // Y0904 [SAW INTERFACCE] HANDLER STRIP PICKER Z PLACE POSITION 스트립 피커 Z축 자재 스테이즈에 내려 놓는 위치
        public const short HANDLER_LD_COMPLETE          = 405;  // Y0905 [SAW INTERFACCE] HANDLER LOADING COMPLETE 스트립 피커 스테이즈에 로딩 완료 되었을 경우
        public const short HANDLER_UNIT_PK_X_PICKUP_POS = 406;  // Y0906 [SAW INTERFACCE] HANDLER UNIT PICKER X PICK UP POSITION 유닛 피커 X축 자재 스테이즈에서 잡아가는 위치
        public const short HANDLER_UNIT_PK_Z_PICKUP_POS = 407;  // Y0907 [SAW INTERFACCE] HANDLER UNIT PICKER Z PICK UP POSITION 유닛 피커 Z축 자재 스테이즈에서 잡아가는 위치
        public const short HANDLER_UNIT_COMPLETE        = 408;  // Y0908 [SAW INTERFACCE] HANDLER UNIT PICK-UP COMPLETE 유닛 피커 스테이즈에 픽업 완료 되었을 경우
        public const short HANDLER_SAW_REJECT_OFF       = 409;  // Y0909 [SAW INTERFACCE] HANDLER SAW REJECT OFF 신호 (100ms 이상 ON 후 OFF)
        public const short HANDLER_SP10                 = 410;  // Y0910 [SAW INTERFACCE] 
        public const short HANDLER_SP11                 = 411;  // Y0911 [SAW INTERFACCE]
        public const short HANDLER_SP12                 = 412;  // Y0912 [SAW INTERFACCE]
        public const short HANDLER_RECIPE_CHANGE        = 413;  // Y0913 [SAW INTERFACCE] SAW RECIPE CHANGE
        public const short HANDLER_SP14                 = 414;  // Y0914 [SAW INTERFACCE] 
        public const short HANDLER_PICKER_Z_INTERLOCK   = 415;  // Y0915 [SAW INTERFACCE] HANDLER STRIP/UNIT PICKER DOWN INTERLOCK 스트립/유닛 피커 다이싱 테이블 위치에 다운되어 있는지 확인 인터락
        #endregion

        #region >>OUTPUT ARRAY
        public static int[] Null                        = { };
        public static short[] ScrapVac                  = { SCRAP_VAC_1, SCRAP_VAC_2 };
        public static short[] ScrapVacOff               = { SCRAP1_VAC_OFF, SCRAP2_VAC_OFF };
        public static short[] ScrapBlow                 = { SCRAP_BLOW_1, SCRAP_BLOW_2 };
        public static short[] ScrapPurge                = { SCRAP_PURGE_1, SCRAP_PURGE_2 };
        public static short[] StageVac                  = { STAGE1_VAC, STAGE2_VAC };
        public static short[] StageDrain                = { STAGE1_DRAIN, STAGE2_DRAIN };
        public static short[] StageBackVac              = { STAGE1_BACK_VAC, STAGE2_BACK_VAC };
        public static short[] StageAirshowr             = { STAGE1_BLOW, STAGE2_BLOW };
        public static int[] EmptyStopperLock            = { EMPTY_STACKER_LOCK1, EMPTY_STACKER_LOCK2 };
        public static int[] EmptyStopperUnlock          = { EMPTY_STACKER_UNLOCK1, EMPTY_STACKER_UNLOCK2 };
        public static int[] EmptyFeederGrip             = { EMPTY_TRAY_GRIP };
        public static int[] EmptyFeederUnGrip           = { EMPTY_TRAY_UNGRIP };
        public static int[] GoodFeeder1Grip             = { GOOD_TRAY1_GRIP_C, GOOD_TRAY1_GRIP_S };
        public static int[] GoodFeeder1UnGrip           = { GOOD_TRAY1_UNGRIP_C, GOOD_TRAY1_UNGRIP_S };
        public static int[] GoodFeeder1GripC            = { GOOD_TRAY1_GRIP_C };
        public static int[] GoodFeeder1UngGripC         = { GOOD_TRAY1_UNGRIP_C };
        public static int[] GoodFeeder1GripS            = { GOOD_TRAY1_GRIP_S };
        public static int[] GoodFeeder1UngGripS         = { GOOD_TRAY1_UNGRIP_S };
        public static int[] GoodFeeder2Grip             = { GOOD_TRAY2_GRIP_C, GOOD_TRAY2_GRIP_S };
        public static int[] GoodFeeder2UnGrip           = { GOOD_TRAY2_UNGRIP_C, GOOD_TRAY2_UNGRIP_S };
        public static int[] GoodFeeder2GripC            = { GOOD_TRAY2_GRIP_C };
        public static int[] GoodFeeder2UngGripC         = { GOOD_TRAY2_UNGRIP_C };
        public static int[] GoodFeeder2GripS            = { GOOD_TRAY2_GRIP_S };
        public static int[] GoodFeeder2UngGripS         = { GOOD_TRAY2_UNGRIP_S };
        public static int[] GoodTrayFrontGrip           = { GOOD_TRAY1_GRIP_C, GOOD_TRAY2_GRIP_C };
        public static int[] GoodTrayBackGrip            = { GOOD_TRAY1_GRIP_S, GOOD_TRAY2_GRIP_S };
        public static int[] GoodTrayFrontUnGrip         = { GOOD_TRAY1_UNGRIP_C, GOOD_TRAY2_UNGRIP_C };
        public static int[] GoodTrayBackUnGrip          = { GOOD_TRAY1_UNGRIP_S, GOOD_TRAY2_UNGRIP_S };
        public static int[] ReWorkFeederGrip            = { NG_TRAY_GRIP };
        public static int[] ReWorkFeederUnGrip          = { NG_TRAY_UNGRIP };
        public static int[] TrayAlignUp                 = { TRAY_PK_TRAY_ALIGN_UP };
        public static int[] TrayAlignDn                 = { TRAY_PK_TRAY_ALIGN_DN };

        //CHK_MCDIR 함수에서 정의! (정/역 설비 방향 때문)
        public static int[] HD1PkVac;    //= { X1_VAC1, X1_VAC2, X1_VAC3, X1_VAC4, X1_VAC5, X1_VAC6, X1_VAC7, X1_VAC8 };
        public static int[] HD1PkRej;    //= { X1_BLOW1, X1_BLOW2, X1_BLOW3, X1_BLOW4, X1_BLOW5, X1_BLOW6, X1_BLOW7, X1_BLOW8 };
        public static int[] HD2PkVac;    //= { X2_VAC1, X2_VAC2, X2_VAC3, X2_VAC4, X2_VAC5, X2_VAC6, X2_VAC7, X2_VAC8 };
        public static int[] HD2PkRej;    //= { X2_BLOW1, X2_BLOW2, X2_BLOW3, X2_BLOW4, X2_BLOW5, X2_BLOW6, X2_BLOW7, X2_BLOW8 };


        public static int[] MNOutput = { ELV_CLAMP, ELV_UNCLAMP, PUSHER_FWD, PUSHER_BWD, INLET_TABLE_UP, INLET_TABLE_DN, GRIPPER_OPEN, GRIPPER_CLOSE,
                                         STRIP_PK_VAC, STRIP_PK_VAC_OFF, STRIP_PK_BLOW, STRIP_PK_PURGE, UNIT_PK_VAC, UNIT_PK_BLOW, SCRAP_VAC_1, SCRAP_VAC_2, SCRAP_BLOW_1, SCRAP_BLOW_2, CLEANER_WATER_1, CLEANER_WATER_2, CLEANER_AIR_1, CLEANER_AIR_2, CLEANER_SWING_R, CLEANER_SWING_L,
                                         STAGE1_BLOW, STAGE1_VAC, TOP_VISION_BLOW, CAM_CAL_ZIG_FWD, CAM_CAL_ZIG_BWD, BTM_VISION_BLOW,
                                         GOOD_TRAY1_GRIP_C, GOOD_TRAY1_UNGRIP_C, GOOD_TRAY1_GRIP_S, GOOD_TRAY1_UNGRIP_S, GOOD_TRAY2_GRIP_C, GOOD_TRAY2_UNGRIP_C, GOOD_TRAY2_GRIP_S, GOOD_TRAY2_UNGRIP_S, GOOD_STACKER_UP, GOOD_STACKER_DN, NG_TRAY_GRIP, NG_TRAY_UNGRIP, GOOD_TRAY_PRE_ALIGN_FWD, GOOD_TRAY_PRE_ALIGN_BWD, NG_STACKER_UP, NG_STACKER_DN,
                                         TRAY_PK_GRIP, TRAY_PK_UNGRIP, EMPTY_STACKER_LOCK1, EMPTY_STACKER_LOCK2, EMPTY_STACKER_UNLOCK1, EMPTY_STACKER_UNLOCK2, EMPTY_TRAY_GRIP, EMPTY_TRAY_UNGRIP, EMPTY_TRAY_FWD, EMPTY_TRAY_BWD,
                                         INLET_TABLE_VAC, INLET_TABLE_BACK_VAC, CLEANER_AIR_KNIFE
        };

        public static int[] InterfaceState = {
            HANDLER_STRIP_PK_X_PLACE_POS, HANDLER_STRIP_PK_Z_PLACE_POS, HANDLER_LD_COMPLETE,
            HANDLER_UNIT_PK_X_PICKUP_POS, HANDLER_UNIT_PK_Z_PICKUP_POS, HANDLER_UNIT_COMPLETE,
            UldConveyorTrayUnloading
        };
        #endregion

        public static void GET_MODULE_START_END(){
            CNT_.OutSortStart   = 0;
            CNT_.OutSortEnd     = 95;

            CNT_.OutSawStart    = 352;
            CNT_.OutSawEnd      = 367;
        }
    } //OUTPUT DEFINE
}
