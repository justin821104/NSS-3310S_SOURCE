namespace NSS_3310S
{
    //1:TRUE 0:FALSSE
    public class CP
    {
        public const int ManualRunRate                  = 0;    // 매뉴얼 동작시 가동율 %
        public const int RunRate                        = 1;    // 자동운전시 가동율 %
        public const int LogSaveSkip                    = 2;    // 가동로그 저장안함 (T:사용, F:미사용)
        public const int UphCalcCount                   = 3;    // 택-타임 계산갯수 ea
        public const int StartPushTime                  = 4;    // 스타트 S/W 누름시간 sec
        public const int JogTime                        = 5;    // JOG 제한시간 sec
        public const int BzOffTime                      = 6;    // Buzzer Off Time Sec
        public const int BlinkTime                      = 7;    // 타워램프 깜박임 시 Sec
        public const int EjectChkTime                   = 8;    // 파기 체크 시간 msec
        public const int UseBz                          = 9;    // 부져 사용 유무 (T:사용, F:미사용)
        public const int UseAREA                        = 10;   // 에어리어 센서 사용 유무 (T:사용, F:미사용)
        public const int CylinderOverTime               = 11;   // 실린더 동작 오버타임 시간 (msec)
        public const int ACMotorRunTime                 = 12;   // AC 모터 런닝 오버타임 시간 (msec)
        public const int ACMotorRunStopDelay            = 13;   // AC 모터 정지 대기 시간 (msec)
        public const int UseMES                         = 14;   // MES 사용 유무 (T:사용, F:미사용)
        public const int PusherFwdDelay                 = 15;   // 푸져 전진 후 대기 시간 (msec)
        public const int PusherBwdDelay                 = 16;   // 푸져 후진 후 대기 시간 (msec)
        public const int MgzArrivalDelay                = 17;   // 매거진 로딩 위치 도착 후 대기 시간 (msec)
        public const int ElvClampDelay                  = 18;   // 매거진 클램프 후 대기 시간 (msec)
        public const int ElvUnClampDelay                = 19;   // 매거진 언클램프 후 대기 시간 (msec)
        public const int ElvUpDownPitch                 = 20;   // 매거진 로딩시 피치 (mm)
        public const int UseGripperStripCheck           = 21;   // 그리퍼 그립 스트립 확인 센서 사용 유무 (T:사용, F:미사용)
        public const int GripperLockDelay               = 22;   // 그리퍼 스트립 락 후 대기 시간 (msec)
        public const int GripperUnlockDelay             = 23;   // 그리퍼 스트립 언락 후 대기 시간 (msec)
        public const int GripperBackPitch               = 24;   // 그리퍼 그립/언그립 BACK PITCH (mm)
        public const int GripperReCatchCnt              = 25;   // 그리퍼 스트립 못 잡을 경우 반복 회수 (0:에러 없이 계속 진행)
        public const int InletTableUp                   = 26;   // 인렛 테이블 업 후 대기 시간 (msec)
        public const int InletTableDn                   = 27;   // 인렛 테이블 다운 후 대기 시간 (msec)
        public const int InletVac                       = 28;   // 인렛 테이블 진공 ON 대기 시간 (msec)
        public const int RailOpenPitch                  = 29;   // 레일에서 스트립 픽업시 OPEN 피치 (mm)
        public const int StripPkSafetyPosition          = 30;   // STRIP PICKER X축 안전 위치 (UNIT PICKER 충돌 방지)
        public const int UnitPkSafetyPosition           = 31;   // UNIT PICKER X축 안전 위치 (STRIP PICKER 충돌 방지)
        public const int HandlerPkSafetyRangePitch      = 32;   // STRIP AND UNIT PICKER 안전거리 (충돌 방지용)
        public const int UseRFID                        = 33;   // RFID 사용 유무 (T:사용, F:미사용)
        public const int UseBarcode                     = 34;   // 바코드 사용 유무 (T:사용, F:미사용)
        public const int UsePreAlign                    = 35;   // PRE-ALIGN 사용 유무 (T:사용, F:미사용)
        public const int UseTopInspection               = 36;   // TOP VISION 검사 사용 유무 (T:사용, F:미사용)
        public const int UseTopInspectionResult         = 37;   // TOP VISION 검사 결과 사용 유무 (T:사용, F:미사용)
        public const int UseTopInspectionOffset         = 38;   // 유닛 개별 옵셋값 적용 유무 (T:적용, F:미적용)
        public const int UesBtmInspection               = 39;   // BOTTOM VISION 검사 사용 유무 (T:사용, F:미사용)
        public const int UseBtmInspectionResult         = 40;   // BOTTOM VISION  검사 결과 사용 유무 (T:사용, F:미사용)
        public const int VisionReponseOverTime          = 41;   // 비전 결과 응답 시간 sec
        public const int MapBlockMode                   = 42;   // 맵-블록 테이블 모드 [맵-블록 1번 모드(0:ODD[홀]/1:EVEN[짝]/2:ALL)]
        public const int StripPkVacOn                   = 43;   // STRIP PICKER 진공 ON 대기 사간 (msec)
        public const int StripPkVacOff                  = 44;   // STRIP PICKER 진공 OFF 대기 시간 (msec)
        public const int StripBlowOn                    = 45;   // STRIP PICKER 파기 ON 시간 (msec)
        public const int StripBlowRepeatCnt             = 46;   // 스트립 피커 파기 반복 회수 (count)
        public const int StipPkCheckUpPitch             = 47;   // 스트립 피커 스트립 픽업 체크 피치 (mm)
        public const int UnitPkVacOn                    = 48;   // UNIT PICKER 진공 ON 대기 시간 (msec)
        public const int UnitPkVacOff                   = 49;   // UNIT PICKER 진공 OFF 대기 시간 (msec)
        public const int UnitPkBlowOn                   = 50;   // UNIT PICKER 파기 ON 시간 (msec)
        public const int UseScrapVacCheck               = 51;   // SCRAP 진공 체크 사용 유무 (T:사용, F:미사용)
        public const int ScrapBlowOn                    = 52;   // SCRAP 파기 ON 시간 (msec)
        public const int UnitPkCheckUpPitch             = 53;   // 유닛 피커 유닛 픽업 체크 피치 (mm)
        public const int ScrapBlowRepeatCnt             = 54;   // 스크랩 파기 반복 회수 (count)
        public const int UseBrush                       = 55;   // 브러쉬 사용 유무 (T:사용, F:미사용)
        public const int BrushRepeatCnt                 = 56;   // 브러쉬 반복 회수 (count)
        public const int UseUnitAirshower               = 57;   // 유닛 피커 에어블로우 사용 유무 (T:사용, F:미사용)
        public const int UnitAirshowRepeatCnt           = 58;   // 유닛 피커 에어블로우 반복 회수 (count)
        public const int UseUnitClear                   = 59;   // 유닛 클리너 사용 유무 (T:사용, F:미사용)
        public const int UnitBlowRepeatCnt              = 60;   // 유닛 피커 파키 반복 회수 (count)
        public const int UnitKitWidthPitch              = 61;   // 유닛 피커 브러쉬/에어샤워 완료 피치 (mm)
        public const int UseScrapBoxCheck               = 62;   // 스크랩 박스 유무 확인 체크 (T:스크랩 박스 체크/F:스크랩 박스 미체크)
        public const int CleanerSwingFwdDelay           = 63;   // 클리너 스윙 전진 후 대기 시간 (msec)
        public const int CleanerSwingBwdDelay           = 64;   // 클리너 스윙 후진 후 대기 시간 (msec)
        public const int UnitAirshowerSpd               = 65;   // 유닛 에어샤워 작업 속도 (mm/sec)
        public const int BrushSpd                       = 66;   // 브러쉬 작업 속도 (mm/sec)
        public const int SelectStage                    = 67;   // 맵-블록 사용 선택 (0:맵-블록 테이블1 / 1:맵-블록 테이블2 / 2:전부사용)
        public const int UseStageVacCheck               = 68;   // 맵-블록 진공 센서 확인 사용 유무 (T:사용, F:미사용)
        public const int StageVacOn                     = 69;   // 맵-블록 테이블 진공 ON 대기 시간 (msec)
        public const int StageVacOff                    = 70;   // 맵-블록 테이블 진공 OFF 대기 시간 (msec)
        public const int StageBlowDelay                 = 71;   // 맵-블록 테이블 파기 ON 대기 시간 (msec)

        public const int StageAirshowRepeatCnt          = 73;   // 맵-블록 테이블 비전 검사 전 에어블로우 회수 (count)
        public const int UseInspectionStageAir          = 74;   // 유닛 검사시 테이블 에어샤워 사용 유무 (T:사용, F:미사용)
        public const int InpectionMoveEndDelay          = 75;   // 유닛 검사 모션 이송 완료 후 대기 시간 (msec)
        public const int TriggerEnd                     = 76;   // 트리거 실행 후 대기 시간 (msec)
        public const int UseWorkedAirshower             = 77;   // 맵-블록 테이블 배출시 에어블로우 사용 유무 (T:사용, F:미사용)
        public const int StageWorkedAirshowerRepeatCnt  = 78;   // 맵-블록 테이블 배출시 에어블로우 회수 (count)
        public const int StageHeightPitch               = 79;   // 맵-블록 에어샤워 완료 피치 (mm)
        public const int StageUnitPickupVac             = 80;   // 맵-블록 유닛 픽업시 테이블 진공 상태 (0:NOT VACUUM / 1:VACUUM)
        public const int StagePickupMovingVac           = 81;   // 멥-블록 작업 이송시 테이블 진공 상태 (0: NOT VACUUM / 1:VACUUM)
        public const int SelectHead                     = 82;   // 헤드 사용 선택 (0:HD1/1:HD2/2:ALL)
        public const int UsePkVacCheck                  = 83;   // 피커 진공 센서 확인 사용 유무 (T:사용, F:미사용)
        public const int RePick                         = 84;   // 피커 재-픽업 회수 (count)
        public const int PRSStartPos                    = 85;   // 플라잉 시작 위치 (mm)
        public const int PRSEndPos                      = 86;   // 플라잉 끝 위치 (mm)
        public const int PicUpCheckPitch                = 87;   // 픽업 체크 피치 (mm)
        public const int PlaceCheckPitch                = 88;   // 플레이스 체크 피치 (mm)
        public const int PickerPitch                    = 89;   // 피커 피치 (mm)
        public const int StageHD1CamPickerPicOffsetX    = 90;   // 테이블 픽업시 헤드1 카메라와 피커 옵셋 피치 X (mm)
        public const int StageHD1CamPickerPicOffsetY    = 91;   // 테이블 픽업시 헤드1 카메라와 피커 옵셋 피치 Y (mm)
        public const int StageHD2CamPickerPicOffsetX    = 92;   // 테이블 픽업시 헤드2 카메라와 피커 옵셋 피치 X (mm)
        public const int StageHD2CamPickerPicOffsetY    = 93;   // 테이블 픽업시 헤드2 카메라와 피커 옵셋 피치 Y (mm)
        public const int TrayHD1CamPickerPlaceOffsetX   = 94;   // 트레이 플레이스시 헤드1 카메라와 피커 옵셋 피치 X (mm)
        public const int TrayHD1CamPickerPlaceOffsetY   = 95;   // 트레이 플레이스시 헤드1 카메라와 피커 옵셋 피치 Y (mm)
        public const int TrayHD2CamPickerPlaceOffsetX   = 96;   // 트레이 플레이스시 헤드2 카메라와 피커 옵셋 피치 X (mm)
        public const int TrayHD2CamPickerPlaceOffsetY   = 97;   // 트레이 플레이스시 헤드2 카메라와 피커 옵셋 피치 Y (mm)
        public const int TrayUnloadingMode              = 98;   // GOOD 트레이 배출 모드 선택 (0:CONVEYOR / 1:STACKER)
        public const int TrayPlace_Interlock1           = 99;   // 트레이 인터락 시작 위치 [HEAD1 트레이 첫 줄 위치(mm)]
        public const int TrayPlace_Interlock2           = 100;  // 트레이 인터락 끝 위치 [HEAD2 트레이 마지막 줄 위치(mm)]
        public const int GoodTrayPreAlignFwdDelay       = 101;  // GOOD 트레이 프리 얼라인 전진 후 대기 시간 (msec)
        public const int GoodTrayPreAlignBwdDelay       = 102;  // GOOD 트레이 프리 얼라인 후진 후 대기 시간 (msec)
        public const int UseTrayCheckSensor             = 103;  // 트레이 피더 트레이 유무 센서 체크 사용 선택 (YES : 체크 / NO : 미체크)
        public const int StackerUpDelay                 = 104;  // 트레이 스태커 테이블 업 후 대기 시간 (msec)
        public const int StackerDnDelay                 = 105;  // 트레이 스태커 테이블 다운 후 대기 시간 (msec)
        public const int SelectStackerUnloading         = 106;  // 스태커 배출 모드일 경우 트레이 배출 모드 (0:GOOD TARY 1 / 1:GOOD TRAY 2)
        public const int SelectConveyorUnloading        = 107;  // 콘베어 배출 모드일 경우 트레이 배출 모드 (0:GOOD TARY 1 / 1:GOOD TRAY 2 / 2:전부 사용)
        public const int EmptyStopperLockDelay          = 108;  // 빈-트레이 스토퍼 락 후 대기 시간 (msec)
        public const int EmptyStopperUnlockDelay        = 109;  // 빈-트레이 스토퍼 언락 후 대기 시간 (msec)
        public const int FeederGripDelay                = 110;  // 트레이 피터 그립 후 대기 시간 (msec) 
        public const int FeederUnGripDelay              = 111;  // 트레이 피터 언그립 후 대기 시간 (msec) 
        public const int EmptyFeederFwdDelay            = 112;  // 빈-트레이 피터 전진 후 대기 시간 (msec)
        public const int EmptyFeederBwdDelay            = 113;  // 빈-트레이 피터 후진 후 대기 시간 (msec)
        public const int TrayCleampDelay                = 114;  // 트레이 피커 클램프 후 대기 시간 (msec)
        public const int TrayUnCleampDelay              = 115;  // 트레이 피커 언클램프 후 대기 시간 (msec)
        public const int CamZigFwdDelay                 = 116; // 카메라 CAL' 지그 전진 후 대기 시간 (msec)
        public const int CamZigBwdDelay                 = 117; // 카메라 CAL' 지그 후진 후 대기 시간 (msec)
        public const int PowerMeterComPort              = 118; // 파워미터 컴포트 (COM)
        public const int LightComPort                   = 119; // 조명 콘트롤러 컴포트 (COM)
        public const int PreCamResolution               = 120; // 프리 얼라인 카메라 분해능
        public const int ElvULDUpDownPitch              = 121;   // 매거진 언로딩시 피치 (mm)
        public const int InletBlow                      = 122; // 인렛 테이블 파기 시간 (msec)
        public const int UseFlaying                     = 123;  // X 마크 검사 모드 (T:FLYING / F:STEP)
        public const int UseUnitPkWorkedAirshower       = 124;  // 유닛 피커 플레이스 후 에어샤워 사용 유무 (T:사용 / F:미사용)
        public const int UseUnitPkWorkedCleaner         = 125;  // 유닛 피커 프레이스 후 클리너 박스 클린 동작 사용 유무 (T:사용 / F:미사용)
        public const int UnitWorkedAirshowRepeatCnt     = 126;   // 유닛 피커 작업 완료 후 에어블로우 반복 회수 (count)
        public const int UseTrayFeederTrayCheck         = 127;  //트레이 피더 트레이 유무 센서 사용 유무 (T:사용 / F:미사용)
        public const int UseLotStatPkAutoCal            = 128; // lot 시작시 피커 auto calibration 사용 유무 (T:사용 / F:미사용)
        public const int ReworkTrayPlaceFirstLine       = 129;  //REWORK TRAY 유닛 플레이스 첫 라인 위치 (인터락 확인 위치)
        public const int ReworkTrayPlaceLastLine        = 130;  //REWORK TRAY 유닛 플레이스 마지막 라인 위치 (인터락 확인 위치)
        public const int GoodTray1PlaceFirstLine        = 131;  //GOOD TRAY1 유닛 플레이스 첫 라인 위치 (인터락 확인 위치)
        public const int GoodTray1PlaceLastLine         = 132;  //GOOD TRAY1 유닛 플레이스 마지막 라인 위치 (인터락 확인 위치)
        public const int GoodTray2PlaceFirstLine        = 133;  //GOOD TRAY2 유닛 플레이스 첫 라인 위치 (인터락 확인 위치)
        public const int GoodTray2PlaceLastLine         = 134;  //GOOD TRAY2 유닛 플레이스 마지막 라인 위치 (인터락 확인 위치)
        public const int UsePickUpVac                   = 135;  //유닛 픽업 다운 직전 피커 진공 사용 유무 (T:사용 / F:미사용)
        public const int UseLotEnd                      = 136;  // LOT-END 처리 사용 유무 (T:사용 / F:미사용)
        public const int ZigAttachStage                 = 137;  // 지그 설치 테이블 (0:stage1 / 1:stage2)
        public const int Zig1stPosHD1_X                 = 138;  // HEAD1 지그 첫번째 홀 위치 X
        public const int Zig1stPosHD1_Y                 = 139;  // HEAD1 지그 첫번째 홀 위치 Y
        public const int Zig2ndPosHD1_X                 = 140;  // HEAD1 지그 두번째 홀 위치 X
        public const int Zig2ndPosHD1_Y                 = 141;  // HEAD1 지그 두번째 홀 위치 Y
        public const int ZigLastPosHD1_X                = 142;  // HEAD1 지그 세번째 홀 위치 X
        public const int ZigLastPosHD1_Y                = 143;  // HEAD1 지그 세번째 홀 위치 Y
        public const int Zig1stPosHD2_X                 = 144;  // HEAD2 지그 첫번째 홀 위치 X
        public const int Zig1stPosHD2_Y                 = 145;  // HEAD2 지그 첫번째 홀 위치 Y
        public const int Zig2ndPosHD2_X                 = 146;  // HEAD2 지그 두번째 홀 위치 X
        public const int Zig2ndPosHD2_Y                 = 147;  // HEAD2 지그 두번째 홀 위치 Y
        public const int ZigLastPosHD2_X                = 148;  // HEAD2 지그 세번째 홀 위치 X
        public const int ZigLastPosHD2_Y                = 149;  // HEAD2 지그 세번째 홀 위치 Y
        public const int UseStipPkCheck                 = 150;  // 스트립 피커 픽업 전 피커 검사 사용 유무 (T: 검사 / F:미검사)
        public const int UsePlaceCheck                  = 151;  // 유닛 플레이스 후 피커 검사 사용 유무 (T:검사 / F:미검사)
        public const int PlaceCheckVac                  = 152;  // 유닛 플레이스 후 피커 유닛 검사 진공 값
        public const int PlaceCheckDalay                = 153;  // 유닛 플레이스 후 피커 유닛 검사 대기 시간 (msec)
        public const int UseMsSQL                       = 154;  // ITS 통신 사용 유무 (T:통신/ F:미통신)
        public const int UseRejectBoxCheck              = 155;  // REJECT 박스 확인 유무 
        public const int BarcodeReadingCheck            = 156;  // 바코드 리딩 오버타임 시간 (msec)
        public const int UseMgzCheck1                   = 157;  // 매거진 감지 센서 (X705) 확인 유무 (T:사용/F:미사용)
        public const int UseMgzCheck2                   = 158;  // 매거진 감지 센서 (X706) 확인 유무 (T:사용/F:미사용) 
        public const int UseITSData                     = 159;  // ITS 데이터 쏘팅 사용 유무 (T:사용/F:미사용)
        public const int SelectMotorSpd                 = 160;  // 개별위치값 모터 속도, 가감속 선택 (0:공통 / 1:개별)
        public const int RejectBlowDelay                = 161;  // 피커 REJECT 파기 대기 시간 (msec)
        public const int MGZPitchSpeed                  = 162;  // 매거진 슬롯 피치 이송 속도 (mm/sec)

        #region >> ARRAY
        public static int[] UseData = { LogSaveSkip, UseBz, UseAREA,
                                                    UseGripperStripCheck, UsePreAlign,
                                                    UseTopInspection, UseTopInspectionResult, UseTopInspectionOffset,
                                                    UseScrapVacCheck,
                                                    UseUnitClear, UseUnitAirshower, UseBrush,
                                                    UseStageVacCheck, UseInspectionStageAir, UseWorkedAirshower, UsePkVacCheck,
                                                    UseFlaying, UseUnitPkWorkedAirshower, UseUnitPkWorkedCleaner, UseTrayFeederTrayCheck,
                                                    UseLotStatPkAutoCal, UsePickUpVac, UseLotEnd, UsePlaceCheck, UseStipPkCheck,
                                                    UseRejectBoxCheck, SelectMotorSpd };
        public static int[] UseData1 = {    UseMgzCheck1, UseMgzCheck2, UseITSData,
                                            UesBtmInspection, UseBtmInspectionResult, UseMsSQL, UseBarcode, UseMES, UseRFID
        };

        public static int[] DVPara = new int[] { GripperReCatchCnt,
                                                  PickerPitch, StageHD1CamPickerPicOffsetX, StageHD1CamPickerPicOffsetY, StageHD2CamPickerPicOffsetX, StageHD2CamPickerPicOffsetY,
                                                  TrayHD1CamPickerPlaceOffsetX, TrayHD1CamPickerPlaceOffsetY, TrayHD2CamPickerPlaceOffsetX, TrayHD2CamPickerPlaceOffsetY,
                                                  BrushRepeatCnt, UnitAirshowRepeatCnt, UnitWorkedAirshowRepeatCnt, MGZPitchSpeed
        };

        public static int[] SysPara = new int[]{
                                            PickerPitch,
                                            StageHD1CamPickerPicOffsetX, StageHD1CamPickerPicOffsetY, StageHD2CamPickerPicOffsetX, StageHD2CamPickerPicOffsetY,
                                            TrayHD1CamPickerPlaceOffsetX, TrayHD1CamPickerPlaceOffsetY, TrayHD2CamPickerPlaceOffsetX, TrayHD2CamPickerPlaceOffsetY
        };

        public static int[] MTPara = new int[] {  ElvUpDownPitch, ElvULDUpDownPitch, GripperBackPitch, RailOpenPitch,
                                                  StipPkCheckUpPitch, UnitPkCheckUpPitch, UnitKitWidthPitch, UnitAirshowerSpd, BrushSpd,
                                                  BrushRepeatCnt, UnitAirshowRepeatCnt,
                                                  StageHeightPitch,
                                                  PicUpCheckPitch, PlaceCheckPitch, UnitWorkedAirshowRepeatCnt, MGZPitchSpeed
        };

        public static int[] PkPicOffsetX            = { StageHD1CamPickerPicOffsetX, StageHD2CamPickerPicOffsetX };
        public static int[] PkPicOffsetY            = { StageHD1CamPickerPicOffsetY, StageHD2CamPickerPicOffsetY };
        public static int[] PkPlcOffsetX            = { TrayHD1CamPickerPlaceOffsetX, TrayHD2CamPickerPlaceOffsetX };
        public static int[] PkPlcOffsetY            = { TrayHD1CamPickerPlaceOffsetY, TrayHD2CamPickerPlaceOffsetY };

        public static int[] Zig1stPosX              = { Zig1stPosHD1_X, Zig1stPosHD2_X };
        public static int[] Zig1stPosY              = { Zig1stPosHD1_Y, Zig1stPosHD2_Y };
        public static int[] Zig2ndPosX              = { Zig2ndPosHD1_X, Zig2ndPosHD2_X };
        public static int[] Zig2ndPosY              = { Zig2ndPosHD1_Y, Zig2ndPosHD2_Y };
        public static int[] ZigLastPosX             = { ZigLastPosHD1_X, ZigLastPosHD2_X };
        public static int[] ZigLastPosY             = { ZigLastPosHD1_Y, ZigLastPosHD2_Y };

        public static int[] ZigPosHD1_X             = { Zig1stPosHD1_X, Zig2ndPosHD1_X, ZigLastPosHD1_X };
        public static int[] ZigPosHD1_Y             = { Zig1stPosHD1_Y, Zig2ndPosHD1_Y, ZigLastPosHD1_Y };
        public static int[] ZigPosHD2_X             = { Zig1stPosHD2_X, Zig2ndPosHD2_X, ZigLastPosHD2_X };
        public static int[] ZigPosHD2_Y             = { Zig1stPosHD2_Y, Zig2ndPosHD2_Y, ZigLastPosHD2_Y };
        #endregion
    } //COMMON PARAMETER DEFINE
    public class RP
    {
        public const int UnitSizeX                  = 0;    // 유닛 사이즈 X (mm)
        public const int UnitSizeY                  = 1;    // 유닛 사이즈 Y (mm)
        public const int UnitThickess               = 2;    // 유닛 두께 (mm)
        public const int GroupCntX                  = 3;    // 드라이-테이블 X방향 그룹 수량
        public const int GroupCntY                  = 4;    // 드라이-테이블 Y방향 그룹 수량
        public const int UnitCntX                   = 5;    // 드라이-테이블 X방향 유닛 수량
        public const int UnitCntY                   = 6;    // 드라이-테이블 Y방향 유닛 수량
        public const int GroupPitchX                = 7;    // 드라이-테이블 X방향 그룹 피치
        public const int GroupPitchY                = 8;    // 드라이-테이블 Y방향 그룹 피치
        public const int UnitPitchX                 = 9;    // 드라이-테이블 X방향 유닛 피치
        public const int UnitPitchY                 = 10;   // 드라이-테이블 Y방향 유닛 피치
        public const int StageBlowUnitPickUpCnt     = 11;   // 유닛 픽업 시  테이블 블로우 개수
        public const int StagePickUpWorkSpeed       = 12;   // 테이블 작업 속도 (mm/sec)
        public const int StageAirshowerLowSpeed     = 13;   // 테이블 비전 검사 전 에어블로우 저속 속도 (mm/sec)
        public const int StageAirshowerCnt          = 14;   // 테이블 비전 검사 전 에어블로우 회수 (count)
        public const int SizeNGOverCnt              = 15;   // 비전 검사 NG 오버 개수 (ea)
        public const int TrayCntX                   = 16;   // 트레이 X방향 수량
        public const int TrayCntY                   = 17;   // 트레이 Y방향 수량
        public const int TrayPitchX                 = 18;   // 트레이 X 피치 (mm)
        public const int TrayPitchY                 = 19;   // 트레이 Y 피치 (mm)
        public const int TrayWorkSpeed              = 20;   // 트레이 작업 속도 (mm/sec)
        public const int FirstUnitPickupVacDelay    = 21;   // 첫 유닛 픽업시 진공 대기 시간(msec)
        public const int FirstLinePickupVacDelay    = 22;   // 첫줄 유닛 픽업시 진공 대기 시간(msec)
        public const int PickupVacDelay             = 23;   // 유닛 픽업시 진공 대기 시간(msec)
        public const int PlaceBlowDelay             = 24;   // 유닛 플레이스시 파기 대기 시간(msec)
        public const int MGZSlotCnt                 = 25;   // 매거진 슬롯 개수 (ea)
        public const int MGZSlotPitch               = 26;   // 매거진 슬롯 피치 (mm)
        public const int PreAlignLight              = 27;   // 프리 얼라인 조명 값
        public const int ScrapAlarm                 = 28;   // 스크랩 픽업 실패 시 다이싱 알람 발생 (T:미발생/F:발생)
        public const int PlaceDelay                 = 29;   // 유닛 플레이스 위치에서 대기 시간 (msec)
        public const int ScrapVacuum                = 30;   // 유닛피커 쏘우 스테이지 베큠 시 스크랩 베큠 사용 유무(T:미사용/F사용)
        public const int PCB_TYPE                   = 31;   // PCB 타입 (T:QUAD / F:STRIP)
        public const int MGZ_DIR                    = 32;   // 매거진 스트립 투입 방향 (T:역방향 / F:정방향)
        public const int MGZ_CLAMP_UP_PITCH         = 33;   // 매거진 클램프시 살짝 업 피치 (mm)
        public const int UseStripLoadngPos          = 34;   // 그리퍼 스트립 로딩 위치 (T:RECIPE PARA / F:COM PARA)
        public const int ULDConvWaitTime            = 35;   // 트레이 배출 대기 응답 시간 (msec)
           

        public static int[] DVPara = { MGZSlotCnt, MGZSlotPitch,
                                                                UnitSizeX, UnitSizeY, UnitThickess,
                                                                TrayCntX, TrayCntY, TrayPitchX, TrayPitchY, TrayWorkSpeed,
                                                                UnitCntX, UnitCntY, UnitPitchX, UnitPitchY,
                                                                GroupCntX, GroupCntY, GroupPitchX, GroupPitchY,
                                                                StageBlowUnitPickUpCnt, StagePickUpWorkSpeed, StageAirshowerLowSpeed, StageAirshowerCnt, SizeNGOverCnt
        };
        public static int[] MTPara = { FirstUnitPickupVacDelay, FirstLinePickupVacDelay, PickupVacDelay, PlaceBlowDelay, PlaceDelay,
                                                                UnitSizeX, UnitSizeY,
                                                                UnitCntX, UnitCntY,
                                                                UnitPitchX, UnitPitchY,
                                                                TrayCntX, TrayCntY,
                                                                TrayPitchX, TrayPitchY,
                                                                MGZ_CLAMP_UP_PITCH
        };

        public static int[] GroupX                  = { GroupCntX, GroupCntX };
        public static int[] GroupY                  = { GroupCntY, GroupCntY };
        public static int[] UnitX                   = { UnitCntX, UnitCntX };
        public static int[] UnitY                   = { UnitCntY, UnitCntY };
    } //RECIPE PARAMETER DEFINE
}