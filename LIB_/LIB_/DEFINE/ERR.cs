using LIB_.DateType;
using Object;
using System;
using System.Threading;

namespace NSS_3310S
{
    public class W : DATA_
    {
        public const int JogMiss                                    = 0;    // 레스피 파일이 없습니다. "RECIPE 파일이 존재 하지 않습니다 !" + etc.CrLf + "Not found Job Recipe file !" + etc.CrLf + "Please open job recipe file !";
        public const int NotLog                                     = 1;    // 작업자 로그인 안됨 "작업자 로그인이 안되어 있습니다 !" + etc.CrLf + "Please operator login !";
        public const int ProductRemove                              = 2;    // 홈 진행 중 에러 발생 경고 메세지
        public const int AllHomeFail                                = 3;    // 초기화 실패  "장비 초기화 실패 하였습니다 !" + etc.CrLf + "INITIALIZE FAIL !";
        public const int ChkDoor                                    = 4;    // Door Chk 실패 "도어 락 상태 확인 바랍니다. !" + etc.CrLf + "CHECK DOOR !";
        public const int EndInitial                                 = 5;    // 초기화 완료  "초기화 완료 되었습니다 !" + etc.CrLf + "INITIALIZE COMPLETE !";
        public const int ManualNotComplete                          = 6;    // 매뉴얼 동작이 끝나지 않았습니다. "매뉴얼 동작이 끝나지 않았습니다 !" + etc.CrLf + "MANUAL RUN NOT FINISH !";
        public const int LotEndComplete                             = 7;    // Lot-End 처리가 끝났습니다. "LOT-END 처리가 끝났습니다. !" + etc.CrLf + "LOT-END SECCUSS !";
        public const int ChkMessageBox                              = 8;    //  
        public const int WorkEnd                                    = 9;    // Work Finish ' 설정수량의 작업이 완료되었습니다. "모든 작업이 완료되었습니다 !" + etc.CrLf + "WORK FINISH !";
        public const int DoorOpen                                   = 10;   // 도어가 열렸습니다.+ vbNewLine 도어열림 상태를 확인하세요. "도어가 열려 있습니다 !" + etc.CrLf + "CLOSE DOOR !";
        public const int OldPasswordFail                            = 11;   // [OLD PASSWORD] 비밀번호가 다릅니다. 확인 해 주세요. "OLD PASSWORD가 일치하지 않습니다." + etc.CrLf + "OLD PASSWORD DOES NOT MATCH !";
        public const int NewPasswordFail                            = 12;   // [NEW PASSWORD] 비밀번호가 다릅니다. 확인 해 주세요. "NEW PASSWORD가 일치하지 않습니다." + etc.CrLf + "NEW PASSWORD DOES NOT MATCH !";
        public const int PasswordOK                                 = 13;   // PASSWORD 변경이 완료 하였습니다. ConfirmUser[num].msg = "패스워드 변경이 완료 되었습니다." + etc.CrLf + "PASSWORD CHANGE HAS BEEN COMPLETED !";
        public const int ManualErrMassage                           = 14;   // 메뉴얼 작업 중 인터락 이나 에러 발생.
        public const int EmptyTray                                  = 15;   // EMPTY STACKER 빈-트레이 공급 해주세요.
        public const int ReworkTrayFull                             = 16;   // REWORK STACKER 트레이 배출 해주세요.
        public const int GoodTrayFull                               = 17;   // GOOD STACKER 트레이 배출 해주세요.
        public const int NotLotLoading                              = 18;   // LOT 등록 안되어있습니다. (ITS 값 없음)
        public const int BarcoderReadingFail                        = 19;   // 바코드 리딩 실패 (YES : 다시 검사 / NO : 메뉴얼 입력)
        public const int NotRunSaw                                  = 20;   // SAW 설비 프로그램 RUN 상태 확인 바랍니다.
        public const int NotRunVision                               = 21;   // VISION PROGRAM RUN 상태 확인 바랍니다.
        public const int NotUnloaderConveyor                        = 22;   // UNLOADER CONVEYOR 설비 READY 상태 확인 바랍니다.
        public const int SawRecipeLoadingFail                       = 23;   // 다이싱 레스피 open 실패 하였습니다.
        public const int VisionRecipeLoadingFail                    = 24;   // 비전 레스피 open 실패 하였습니다.
        public const int UnitSizeInspectionSkip                     = 25;   // 유닛 사이즈 검사  스킵 상태 입니다. (YES : 스킵 상태로 START 진행 / NO : 설비 STOP)
        public const int UnitSizeReturnValueSkip                    = 26;   // 유닛 사이즈 검사 진행 하지만 비전 검사 결과 무시 하고 모두 OK로 진행 합니다. (YES: 유닛 사이즈 검사 결과 무시 하여 START 진행 / NO : 설비 STOP)
        public const int LDCst_Requst                               = 27;   // 로더 콘베어 카세트 공급 하셔야 합니다.   (YES : 매거진 이여서 투입 / NO : LOT 완료 현재 진행 STRIP 완료 후  LOT-END 처리)
        public const int ULDCst_FullCheck                           = 28;   // 언로더 카세트 제거 하셔야 합니다.
        public const int GripperStripPicFail                        = 29;   // 그리퍼 스트립 로딩 중 스트립 사라짐. (YES:스트립 유무 다시 확인 / NO:다시 로딩)
        public const int StripPk_RePic                              = 30;   // 스트립 피커 픽업 실패 (YES:재픽업 시도 / NO:레일 위에 있는 스트립 제거)
        public const int StripPk_Vanish                             = 31;   // 스트립 피커 스트립 중간에 사라짐 (YES:피커에 스트립 있음 / NO:스트립 사라짐)
        public const int SawStripReuestsingal                       = 32;   // 다이싱 스트립 요청 신호 중간에 끊어짐.
        public const int CleanerWater                               = 33;   // 클리너 WATER ON 하려면 유닛 피커 X/Z축 클리너 구간에 있어야 합니다. (YES:무시하고 WATER ON/NO:WATER ON 할 수 없음)
        public const int UnitPlaceSignelOff                         = 34;   // 유닛 플레이스 신호 중간에 끊어짐 
        public const int UnitPkUnit_Vanish                          = 35;   // 유닛 피커에 유닛 사라짐 (유닛 피커 진공 센서 확인) (YES:유닛 유무 다시 확인 / NO:유닛 피커 바닥면 모두 제거 다시 픽업)
        public const int UnitInspectionNgCountOver                  = 36;   // 유닛 검사 시 ng 개수 오버 하였습니다. (YES:다시 검사 / NO:NG 수량 무시 픽업 진행)
        public const int WorkingCancel                              = 37;   // 맵-블록 테이블 작업 취소 플로그
        public const int PickUpFail                                 = 38;
        public const int TrayDisappear                              = 39;   // 트레이 이송부에 트레이 감지 센서 감지 하지 못합니다. (YES : 트레이 제거 후 다시 시작 / NO : 트레이 안착 상태 확인 후 다시 시작)
        public const int HDPkrNotUse                                = 40;   // 헤드 피커 사용 하나 이상 선택 되어 있어야 합니다. 
        public const int UnitPkrPickUpReCheck                       = 41;   // 유닛 피커 픽업 실패 (SAWING STAGE에서 다시 가서 픽업, PCB 제거 다시 처음부터 다시 시작) (YES:픽업 재시도 / NO:유닛 제거 다시 시작함)
        public const int UnitPkrPickUnitCheck                       = 42;   // 유닛 피커 작업 중 중간에 사라짐 (YES:유닛 진공센서 재확인 / NO:유닛피커 유닛 없음)
        public const int WaitUnloaderConveyor                       = 43;   // 언로더 콘베어 배출 신호 안들어옵니다. 콘베어부 확인 바랍니다.
        public const int XMarkInspectionFail                        = 44;   // X-마크 검사 실패 (YES : 다시 검사 / NO : REWORK 트레이로 배출)
        public const int PlaceFail                                  = 45;   //
        public const int DllWarnning                                = 46;
        public const int Rail_StripRemove                           = 47;   // 레일 위 스트립 바코드 재검사 하시겠습니까. (YES : 스트립 제거 / NO : 바코드 다시 검사)
        public const int ChkForm_LotIn                              = 48;   // LOT 등록창 열려 있습니다. LOT 등록창 닫고 다시 실행 하셔야 합니다.
        public const int ITSReadingFail                             = 49;   // 맵블록 테이블 유닛 ITS 값 리딩 중 에러 발생 (YES : 다시 리딩 / NO : ITS 데이터 무시하고 작업 진행)
        public const int RemoveGoodTray                             = 50;   // ok 트레이 제거 하셔야 합니다.! 
        public const int ABF_MESSAGE                                = 51;

        public const int KIT_CLEANNING                              = 52;   //  LOT START KIT CLEANING POP-UP

        public const int BarcodeReadingTimeOver                     = 53;   // 바코드 리딩 시간 오버 되었습니다. 바코드 연결 상태 및 바코드 상태 확인 바랍니다.
       
        #region >> FUNCTION
        public static void InitailizeWarnning(){
            for (int i = 0; i < ConfirmUser.Length; i++){
                ConfirmUser[i].useable = false;
                ConfirmUser[i].process = false;

                ConfirmUser[i].CheckBox = false;
                ConfirmUser[i].CheckBoxResult = false;
            }
        }

        public static void SetWarnning(int iThread, int nWarnning, bool bState, string comment){
            LogWR_.SaveMarsLog(iThread, eLogTYPE.FNC, comment, "WAR");
            ConfirmUser[nWarnning].useable = bState;
        }
        public static void ViewWarning(int iThread, int num){
            ConfirmUser[num].CheckBox = false;
            ConfirmUser[num].useable = true;
            bWF = true;
            if (iThread > 0) LogWR_.SaveMarsLog(iThread, eLogTYPE.FNC, ConfirmUser[num].msg, "WARNNING");
        }
        public static void ViewWarning(int iThread, int num, bool bCheckBox, string CheckBoxTitle){
            ConfirmUser[num].CheckBox = bCheckBox;
            ConfirmUser[num].CheckBoxTitle = CheckBoxTitle;
            ConfirmUser[num].useable = true;
            bWF = true;
            if (iThread > 0) LogWR_.SaveMarsLog(iThread, eLogTYPE.FNC, ConfirmUser[num].msg, "WARNNING");
        }
        public static void ViewWarning(int iThread, int num, string msg){
            ConfirmUser[num].msg = msg;
            ConfirmUser[num].CheckBox = false;
            ConfirmUser[num].useable = true;
            bWF = true;
            LogWR_.SaveMarsLog(iThread, eLogTYPE.FNC, ConfirmUser[num].msg, "WARNNING");
        }
        public static void ViewWarning(int iThread, int num, string mgs, bool TypeOk){
            ConfirmUser[num].msg = mgs;
            ConfirmUser[num].TypeOk = TypeOk;
            ConfirmUser[num].CheckBox = false;
            ConfirmUser[num].useable = true;
            bWF = true;
            LogWR_.SaveMarsLog(iThread, eLogTYPE.FNC, ConfirmUser[num].msg, "WARNNING");
        }

        public static bool WaitWarning(int iTH, int nWarning, string comment){
            LogThread[iTH].sqeSTS = comment;
            Thread.Sleep(3);
            string s = "[" + nWarning.ToString() + "] " + ConfirmUser[nWarning].msg + " , " + comment + " = ";
            if (ConfirmUser[nWarning].useable){
                LogThread[iTH].waitSTS = s + "WAIT";
                return true;
            }
            LogThread[iTH].waitSTS = s + "END";
            return false;
        }
        #endregion << FUNCTION
    }//WARNING DEFINE

    public class E : DATA_
    {
        public const short PreRunPgm                                  = eEMSBegin - 1;    // 프로그램 다중실행중
        public const short AllHOME                                    = eEMSBegin - 2;    // 장비 초기화 도중 에러 발생.
        public const short NotAllHOME                                 = eEMSBegin - 3;    // 장비 초기화 안되어 있음.

        #region >>ERROR LIST (eErrBegin)
        public const short EMO_SAW_FRONT                              = eErrBegin + 0;    // 다이싱 전면 비상정지 스위치 .,PLEASE CHECK THE EQUIPMENT EMO SWITCH.,X700 [SAW] FRONT EMO SWITCH 입력 상태 확인 바랍니다
        public const short EMO_SAW_REAR                               = eErrBegin + 1;    // SAW REAR EMO SWITCH.,PLEASE CHECK THE EQUIPMENT EMO SWITCH.,X701 [SAW] REAR EMO SWITCH 입력 상태 확인 바랍니다
        public const short EMO_SORTER_FRONT                           = eErrBegin + 2;    // SORTER FRONT EMO SWITCH.,PLEACE CHECK THE EQUIPMENT EMO SWITCH.,X007 [SORTER] FRONT EMO SWITCH 입력 상태 확인 바랍니다.
        public const short EMO_SORTER_RIGHT                           = eErrBegin + 3;    // SORTER RIGHT EMO SWITCH.,PLEACE CHECK THE EQUIPMENT EMO SWITCH.,X008 [SORTER] RIGHT EMO SWITCH 입력 상태 확인 바랍니다.
        public const short EMO_SORTER_BACK                            = eErrBegin + 4;    // SORTER BACK EMO SWITCH.,PLEACE CHECK THE EQUIPMENT EMO SWITCH.,X009 [SORTER] BACK EMO SWITCH 입력 상태 확인 바랍니다.
        public const short DOOR_SAW_FRONT_LEFT                        = eErrBegin + 5;    // SAW FRONT LEFT DOOR OPEN.,PLEACE CLOSE THE DOOR.,X702 [SAW] FRONT LEFT DOOR 확인 센서 상태 확인 바랍니다.
        public const short DOOR_SAW_FRONT_RIGHT                       = eErrBegin + 6;    // SAW FRONT RIGHT DOOR OPEN.,PLEACE CLOSE THE DOOR.,X703 [SAW] FRONT RIGHT DOOR 확인 센서 상태 확인 바랍니다.
        public const short DOOR_SAW_SIDE_LEFT                         = eErrBegin + 7;    // SAW SIDE LEFT DOOR OPEN.,PLEACE CLOSE THE DOOR.,X909 [SAW] SIDE LEFT DOOR 확인 센서 상태 확인 바랍니다 (MGZ DOOR).
        public const short DOOR_SORTER_FRONT_LEFT                     = eErrBegin + 8;    // SORTER FRONT LEFT DOOR OPEN.,PLEACE CLOSE THE DOOR.,X100 [SORTER] FRONT LEFT DOOR 확인 센서 상태 확인 바랍니다.
        public const short DOOR_SORTER_FRONT_RIGHT                    = eErrBegin + 9;    // SORTER FRONT RIGHT DOOR OPEN.,PLEACE CLOSE THE DOOR.,X101 [SORTER] FRONT RIGHT DOOR 확인 센서 상태 확인 바랍니다.
        public const short DOOR_SORTER_RIGHT_SIDE_LEFT                = eErrBegin + 10;   // SORTER RIGHT SIDE OF LEFT DOOR OPEN.,PLEACE CLOSE THE DOOR.,X102 [SORTER] RIGHT SIDE OF LEFT DOOR 확인 센서 상태 확인 바랍니다.
        public const short DOOR_SORTER_RIGHT_SIDE_RIGHT               = eErrBegin + 11;   // SORTER RIGHT SIDE OF RIGHT DOOR OPEN.,PLEACE CLOSE THE DOOR.,X103 [SORTER] RIGHT SIDE OF RIGHT DOOR 확인 센서 상태 확인 바랍니다.
        public const short DOOR_SORTER_BACK_LEFT                      = eErrBegin + 12;   // SORTER BACK LEFT DOOR OPEN.,PLEACE CLOSE THE DOOR.,X104 [SORTER] BACK LEFT DOOR 확인 센서 확인 바랍니다.
        public const short DOOR_SORTER_BACK_RIGHT                     = eErrBegin + 13;   // SORTER BACK RIGHT DOOR OPEN.,PLEACE CLOSE THE DOOR.,X105 [SORTER] BACK RIGHT DOOR 확인 센서 확인 바랍니다.
        public const short LD_CONV_AREA_SENSOR                        = eErrBegin + 14;   // LOADER CONVEYOR AREA SENSOR CHECK.
        public const short SERVO1_SAW                                 = eErrBegin + 15;   // SAW STRIP SERVO 1 OFF.,SERVO PACK CP DOWN.,X901 [SAW] SAW SERVO POWER DOWN CHECK SIGNAL 입력 상태 확인 바랍니다.
        public const short SERVO2_SAW                                 = eErrBegin + 16;   // SAW STRIP SERVO 2 OFF.,SERVO PACK CP DOWN.,X902 [SAW] SAW SERVO POWER DOWN CHECK SIGNAL 입력 상태 확인 바랍니다.
        public const short SERVO3_SAW                                 = eErrBegin + 17;   // SAW STRIP SERVO 3 OFF.,SERVO PACK CP DOWN.,X903 [SAW] SAW SERVO POWER DOWN CHECK SIGNAL 입력 상태 확인 바랍니다.
        public const short SERVO4_SAW                                 = eErrBegin + 18;   // SAW STRIP SERVO 4 OFF.
        public const short CONV_TRIP                                  = eErrBegin + 19;
        public const short SERVO1_SORTER                              = eErrBegin + 20;   // SORTER STRIP SERVO 1 OFF.,설비 SERVO PACK CP DOWN 전장 확인 바랍니다.,X106 [SORTER] SERVO #1 MC30 TRIP SIGNAL 입력 상태 확인 바랍니다.
        public const short SERVO2_SORTER                              = eErrBegin + 21;   // SORTER STRIP SERVO 2 OFF.,설비 SERVO PACK CP DOWN 전장 확인 바랍니다.,X107 [SORTER] SERVO #2 MC31 TRIP SIGNAL 입력 상태 확인 바랍니다.
        public const short SERVO3_SORTER                              = eErrBegin + 22;   // SORTER STRIP SERVO 3 OFF.,설비 SERVO PACK CP DOWN 전장 확인 바랍니다.,X108 [SORTER] SERVO #3 MC32 TRIP SIGNAL 입력 상태 확인 바랍니다.
        public const short SERVO4_SORTER                              = eErrBegin + 23;   // SORTER STRIP SERVO 4 OFF.,설비 SERVO PACK CP DOWN 전장 확인 바랍니다.,X109 [SORTER] SERVO #4 MC33 TRIP SIGNAL 입력 상태 확인 바랍니다.
        public const short DRIVER_AIR_PRESSURE                        = eErrBegin + 24;   // DRIVER AIR PRESSURE DROP,SORTER 설비 DRIVER 메인단 에어량 부족합니다.,X512 [SORTER] DRIVER AIR PRESSURE 센서 확인 바랍니다.
        public const short BLOW_AIR_PRESSURE                          = eErrBegin + 25;   // BLOW AIR PRESSURE DROP,SORTER 설비 BLOW 메인단 에어량 부족합니다.,X513 [SORTER] BLOW AIR PRESSURE 센서 확인 바랍니다.
        public const short STAGE_AIR_PRESSURE                         = eErrBegin + 26;   // STAGE AIR PRESSURE DROP,SORTER 설비 AIR 메인단 에어량 부족합니다.,X514 [SORTER] STAGE AIR PRESSURE 센서 확인 바랍니다.
        public const short PICKER_AIR_PRESSURE                        = eErrBegin + 27;   // PICKER AIR PRESSURE DROP,SOTRER 설비 PICKER AIR 메인단 에어량 부족합니다.,X515 [SORTER] PICKER AIR PRESSURE 센서 확인 바랍니다.
        public const short MagazineClamp                              = eErrBegin + 28;   // 매거진 클램프 실패
        public const short MagezineUnClamp                            = eErrBegin + 29;   // 매거진 언클램프 실패
        public const short PusherForward                              = eErrBegin + 30;   // 푸셔 전진 실패
        public const short PusherBackward                             = eErrBegin + 31;   // 푸셔 후진 실패
        public const short LoaderConveyorTimeOver                     = eErrBegin + 32;   // 로더 카세트 투입 콘베어 구동 시간 오버
        public const short GripperGripFail                            = eErrBegin + 33;   // 그리퍼 그립 실패
        public const short GripperUnGripFail                          = eErrBegin + 34;   // 그리퍼 언그립 실패
        public const short InLetTableUpFail                           = eErrBegin + 35;   // 인-렛 테이블 업 실패
        public const short InLetTableDnFail                           = eErrBegin + 36;   // 인-렛 테이블 다운 실패
        public const short ScrapPickUpFail                            = eErrBegin + 37;   // 스크랩 픽업 실패 
        public const short CleanerSwingForward                        = eErrBegin + 38;   // 클리너 스윙 전진 실패
        public const short CleanerSwingBackward                       = eErrBegin + 39;   // 클리너 스윙 후진 실패
        public const short ScrapBoxVanish                             = eErrBegin + 40;   // 스크랩 박스 사라짐 
        public const short UnitPkUnitVanish                           = eErrBegin + 41;   // 유닛 피커 유닛 사라짐 
        public const short Stage1VacFail                              = eErrBegin + 42;   // 테이블1 진공값이 낮음.
        public const short Stage2VacFail                              = eErrBegin + 43;   // 테이블2 진공값이 낮음.
        public const short TopVisionResponseOverTime                  = eErrBegin + 44;   // 비전 응답 시간 오버 되었습니다.비전 상태 확인 후 다시 시작 하시면 재측정 합니다.
        public const short NotTopVisionDataFile                       = eErrBegin + 45;   // 유닛 검사 결과 텍스트 파일 없음.,비전에서 유닛 결과를 작성 하여 주지 못하였습니다.
        public const short TopVaisionDataReadingFail                  = eErrBegin + 46;   // 유닛 검사 결과 값 읽어 오는 중 오류 발생.
        public const short EmptyStopperLeftForntLockFail              = eErrBegin + 47;   // 빈트레이 스태커 스토퍼 락
        public const short EmptyStopperLeftForntUnlockFail            = eErrBegin + 48;   // 빈트레이 스태커 스토퍼 언락
        public const short EmptyStopperLeftRearLockFail               = eErrBegin + 49;   // 빈트레이 스태커 스토퍼 락
        public const short EmptyStopperLeftRearUnlockFail             = eErrBegin + 50;   // 빈트레이 스태커 스토퍼 언락
        public const short EmptyStopperRightForntLockFail             = eErrBegin + 51;   // 빈트레이 스태커 스토퍼 락
        public const short EmptyStopperRightForntUnlockFail           = eErrBegin + 52;   // 빈트레이 스태커 스토퍼 언락
        public const short EmptyStopperRightRearLockFail              = eErrBegin + 53;   // 빈트레이 스태커 스토퍼 락
        public const short EmptyStopperRightRearUnlockFail            = eErrBegin + 54;   // 빈트레이 스태커 스토퍼 언락
        public const short EmptyFeederGripFail                        = eErrBegin + 55;   // 빈트레이 피커 그립 실패
        public const short EmptyFeederUngripFail                      = eErrBegin + 56;   // 빈트레이 피커 언그립 실패
        public const short EmptyFeederFwdFail                         = eErrBegin + 57;   // 빈트레이 피터 전진 실패
        public const short EmptyFeederBwdFail                         = eErrBegin + 58;   // 빈트레이 피터 후진 실패
        public const short EmptyTrayLoadingFail                       = eErrBegin + 59;   // 빈-트레이 공급 실패 하였습니다.
        public const short TrayPkCleampFail                           = eErrBegin + 60;   // 트레이 피커 트레이 클램프 실패
        public const short TrayPkUnCleampFail                         = eErrBegin + 61;   // 트레이 피커 트레이 언클램프 실패
        public const short GoodFeeder1FrontGripFail                   = eErrBegin + 62;   // GOOD 이송부1 전면 그립 실패
        public const short GoodFeeder1FrontUnGripFail                 = eErrBegin + 63;   // GOOD 이송부1 전면 언그립 실패
        public const short GoodFeeder1RearGripFail                    = eErrBegin + 64;   // GOOD 이송부1 후면 그립 실패
        public const short GoodFeeder1RearUnGripFail                  = eErrBegin + 65;   // GOOD 이송부1 후면 언그립 실패
        public const short GoodFeeder2FrontGripFail                   = eErrBegin + 66;   // GOOD 이송부1 전면 그립 실패
        public const short GoodFeeder2FrontUnGripFail                 = eErrBegin + 67;   // GOOD 이송부1 전면 언그립 실패
        public const short GoodFeeder2RearGripFail                    = eErrBegin + 68;   // GOOD 이송부1 후면 그립 실패
        public const short GoodFeeder2RearUnGripFail                  = eErrBegin + 69;   // GOOD 이송부1 후면 언그립 실패
        public const short GoodTrayPreAlignFwdFail                    = eErrBegin + 70;   // GOOD 트레이 프리 얼라인 전진 실패
        public const short GoodTrayPreAlignBwdFail                    = eErrBegin + 71;   // GOOD 트레이 프리 얼라인 후진 실패
        public const short GoodTrayStackerUpFail                      = eErrBegin + 72;   // GOOD TRAY STACKER UP 실패
        public const short GoodTrayStackerDnFail                      = eErrBegin + 73;   // GOOD TRAY STACKER DOWN 실패
        public const short ReWorkTrayGripFail                         = eErrBegin + 74;   // RE-WORK 트레이 그립 실패
        public const short ReWorkTrayUnGripFail                       = eErrBegin + 75;   // RE-WORK 트레이 언그립 실패
        public const short ReWorkStackerUpFail                        = eErrBegin + 76;   // RE-WORK STACKER TABLE UP 실패
        public const short ReWorkStaackerDnFail                       = eErrBegin + 77;   // RE-WORK STACKER TABLE DOWN 실패
        public const short CameraCalibrationZigFwdFail                = eErrBegin + 78;   // 카메라 CAL' 지그 전진 실패
        public const short CameraCalibrationZigBwdFail                = eErrBegin + 79;   // 카메라 CAL' 지그 후진 실패
        public const short BlowAir1                                   = eErrBegin + 80;   // BLOW AIR 1 PRESSURE DROP,
        public const short BlowAir2                                   = eErrBegin + 81;   // BLOW AIR 2 PRESSURE DROP,
        public const short PusherOverloadCheck                        = eErrBegin + 82;   // 푸셔 전진 중 OVERLOAD 센서 감지 되었습니다.
        public const short BarcodeReadingFail                         = eErrBegin + 83;   // 스트립 바코드 리딩 실패
        public const short BarcoderWriteFail                          = eErrBegin + 84;   // 스트립 바코드 수동 입력 실패
        public const short TrayFrontAlignUpFail                       = eErrBegin + 85;   // 트레이 피커 트레일 앞쪽 얼라인 실린더 업 실패
        public const short TrayRearAlignUpFail                        = eErrBegin + 86;   // 트레이 피커 트레일 뒤쪽 얼라인 실린더 업 실패
        public const short TrayFrontAlignDnFail                       = eErrBegin + 87;   // 트레이 피커 트레일 앞쪽 얼라인 실린더 다운 실패
        public const short TrayRearAlignDnFail                        = eErrBegin + 88;   // 트레이 피커 트레일 뒤쪽 얼라인 실린더 다운 실패
        public const short TrayAlignFwd                               = eErrBegin + 89;   // 트레이 피커 트레이 얼라인 전진 실패
        public const short TrayAlignBwd                               = eErrBegin + 90;   // 트레이 피커 트레이 얼라인 후진 실패
        #endregion

        #region >>INTERLOCK LIST (eEMSBegin)
        public const int emsPusherNotBwd                            = eEMSBegin + 0;    // 푸셔 후진 되어 있지 않아 구동 할 수 없습니다.
        public const int emsMGZSlotCount                            = eEMSBegin + 1;    // 매거진 슬롯 번호 범위 초과 하였습니다.
        public const int emsMagazineDisappear                       = eEMSBegin + 2;    // 스트립 공급 중 매거진 사라짐.
        public const int emsMagazineSlotNotMove                     = eEMSBegin + 3;    // 스트립 돌출되어 있어 매거진 이송 할 수 없음.
        public const int emsInRailStripCheck                        = eEMSBegin + 4;    // 인-레일 스트립 감지 되어 있습니다.
        public const int emsInLetTableNotDown                       = eEMSBegin + 5;    // 인-렛 테이블 UP 상태에선 실행 할 수 없습니다.
        public const int emsGripperNotMoveRdy                       = eEMSBegin + 6;    // 그리퍼 축 대기 위치가 아니면 실행 할 수 없습니다.
        public const int emsStripPkZNotReadyPos                     = eEMSBegin + 7;    // 스트립 피커 Z축 대기 위치로 이송 후 구동
        public const int emsGripperNotSafetyLocation                = eEMSBegin + 8;    // 그리퍼 X축 안전 위치가 아닙니다.
        public const int emsRemoveRailStrip                         = eEMSBegin + 9;    // 인-레일 스트립 제거 하셔야 합니다.
        public const int emsCleanerWaterFail                        = eEMSBegin + 10;   // 클리너 워터 ON 할 수 없습니다. 유닛 피커 X/Z축 클리너 위치에 있어야 WATER ON 할 수 있습니다.
        public const int emsUnitPkZnotSafetyLocation                = eEMSBegin + 11;   // 유닛 피커 Z축 대기 위치로 이송 후 구동
        public const int emsNotUnitPlace1                           = eEMSBegin + 12;   // 테이블 1번에 유닛 내려 놓을 수 없습니다. 유닛 피커 Z축 대기 위치로 이송 후 다시 실행
        public const int emsNotUnitPlace2                           = eEMSBegin + 13;   // 테이블 2번에 유닛 내려 놓을 수 없습니다. 유닛 피커 Z축 대기 위치로 이송 후 다시 실행
        public const int emsUnitPkVacNotOff                         = eEMSBegin + 14;   // 유닛 중간에 사라져 제거 시 유닛 진공 출력 OFF 상태여야 함  -> 유닛피커 진공 O
        public const int emsNotVisionReady                          = eEMSBegin + 15;   // 비전 프로그램 READY 상태가 아님.
        public const int emsStage1UnitExist                         = eEMSBegin + 16;   // 테이블1 작업 완료 후 유닛 존재 
        public const int emsStage2UnitExist                         = eEMSBegin + 17;   // 테이블2 작업 완료 후 유닛 존재 
        public const int emsEmptyTrayRailTrayExist                  = eEMSBegin + 18;   // 빈트레이 레일 위에 트레이 존재. 제가 필요
        public const int emsTrayPkEmtpyTrayPicFail                  = eEMSBegin + 19;   // 트레이 피커 빈-트레이 픽업 실패 
        public const int emsCalZigNotBackPos                        = eEMSBegin + 20;   // 카메라 CAL' 지그 후진 상태 아님.
        public const int emsPRSReadingTimeOver                      = eEMSBegin + 21;   // 하부카메라 결과값 없음.
        public const int emsNotPRSFile                              = eEMSBegin + 22;   // 하부카메라 결과값 리딩 실패.
        public const int emsPRSReadingFail                          = eEMSBegin + 23;   // 하부카메라 결과값 리딩 실패.
        public const int emsGoodTrayFeederFrontUnGrip               = eEMSBegin + 24;   // GOOD 트레이 이송부1 트레이 그리퍼 열려 있어 유닛 안착 할 수 없습니다. 트레이 그리퍼 상태 확인 후 그립 하셔야 합니다. 
        public const int emsGoodTrayFeederRearUnGrip                = eEMSBegin + 25;   // GOOD 트레이 이송부2 트레이 그리퍼 열려 있어 유닛 안착 할 수 없습니다. 트레이 그리퍼 상태 확인 후 그립 하셔야 합니다.
        public const int emsNotMoveFeeder1PlcPos                    = eEMSBegin + 26;   // GOOD FEEDER1 유닛 내려 놓는 위치로 이송 할 수 없습니다.
        public const int emsNotMoveFeeder2PlcPos                    = eEMSBegin + 27;   // GOOD FEEDER2 유닛 내려 놓는 위치로 이송 할 수 없습니다.
        public const int emsSawStageThAlarm                         = eEMSBegin + 28;   // 다이싱 테이블 TH축 알람 발생.
        public const int emsNotStripPkXPicPos                       = eEMSBegin + 29;   // 스트립 피커 픽업 위치에 있지 않습니다. 
        public const int emsNotStripPkXPlcPos                       = eEMSBegin + 30;   // 스트립 피커 플레이스 위치에 있지 않습니다. 
        public const int emsNotUnitPkXPicPos                        = eEMSBegin + 31;   // 유닛 피커 픽업 위치에 있지 않습니다. 
        public const int emsNotCleanerPos                           = eEMSBegin + 32;   // 유닛 피커 클리너 박스 위치에 있지 않습니다. 
        public const int emsNotStage1Pos                            = eEMSBegin + 33;   // 맵-블록 테이블1 유닛 피커 받는 위치에 있지 않습니다. 
        public const int emsNotStage2Pos                            = eEMSBegin + 34;   // 맵-블록 테이블2 유닛 피커 받는 위치에 있지 않습니다. 
        public const int emsNotUnitPk_VacOff                        = eEMSBegin + 35;   // 유닛 피커 픽업 실패 하여 유닛 제거 후 다시 픽업 요청 시 진공 OFF 안함.public const int emsPkCalReadingTimeOver = eEMSBegin + 36;   // 하부카메라 피커 CAL' 결과값 리딩 실패.
        public const int emsPkCalReadingTimeOver                    = eEMSBegin + 36;   // 
        public const int emsNotPkCalFile                            = eEMSBegin + 37;   // 하부카메라 피커 CAL' 결과값 리딩 실패.
        public const int emsPkCalReadingFail                        = eEMSBegin + 38;   // 하부카메라 피커 CAL' 결과값 리딩 실패.
        public const int emsPkCalFail                               = eEMSBegin + 39;   // 피커 cal' 검사 실패
        public const int emsStripPkXSafetyPosition                  = eEMSBegin + 40;   // 스트립 피커 X축 안전 위치 아닙니다. 대기 위치로 이송 후 다시 실행.
        public const int emsUnitPkXSafetyPosition                   = eEMSBegin + 41;   // 유닛 피커 X축 안전 위치 아닙니다. 대기 위치로 이송 후 다시 실행.
        public const int emsGripperNotRdyPos                        = eEMSBegin + 42;   // 그리퍼 축 대기 위치가 아니면 실행 할 수 없습니다.
        public const int emsRejectBoxVanish                         = eEMSBegin + 43;   // REJECT BOX 사라짐
        public const int emsRejectBoxFullCheck                      = eEMSBegin + 44;   // REJECT BOX 칩 가득참
        public const int emsHD1PickerXMarkReTrain                   = eEMSBegin + 45;   // HEAD1 유닛 X-마크 다시 등록 하셔야 합니다.
        public const int emsHD2PickerXMarkReTrain                   = eEMSBegin + 46;   // HEAD2 유닛 X-마크 다시 등록 하셔야 합니다.
        public const int emsInLetTableStripCheck                    = eEMSBegin + 47;   // 인렛 테이블 위 스트립 제거 하셔야 합니다.
        public const int emsTrayPkPickUpFail_EmptyTray              = eEMSBegin + 48;   // 빈트레이 픽업 실패 빈트레이 이송부 전진 되어 있지 않음
        public const int emsTrayPkPickUpFail_ReworkTray             = eEMSBegin + 49;   // REWORK 트레이 플레이스 실패 REWORK 트레이 이송부 트레이 공급 위치에 없습니다.
        public const int emsGoodTray1Vanish                         = eEMSBegin + 50;   // GOOD TRAY1 이송부 트레이 유무 감지 센서 트레이 감지 못함
        public const int emsGoodTray2Vanish                         = eEMSBegin + 51;   // GOOD TRAY2 이송부 트레이 유무 감지 센서 트레이 감지 못함
        public const int emsReworkTrayVanish                        = eEMSBegin + 52;   // REWORK TRAY  이송부 트레이 유무 감지 센서 트레이 감지 못함
        public const int emsX1PkThNotPlcPos                         = eEMSBegin + 53;   // X1 피커 TH축 PLACE 위치 아닙니다.
        public const int emsX2PkThNotPlcPos                         = eEMSBegin + 54;   // X2 피커 TH축 PLACE 위치 아닙니다.
        public const int emsTrayPkPickUpFail_GoodTray1              = eEMSBegin + 55;   // GOOD TRAY 플레이스 실패 GOOD TRAY1 이송부 트레이 공급 위치에 업습니다. 
        public const int emsTrayPkPickUpFail_GoodTray2              = eEMSBegin + 56;   // GOOD TRAY 플레이스 실패 GOOD TRAY2 이송부 트레이 공급 위치에 업습니다. 
        public const int emsNotUnitPkXStage1Pos                     = eEMSBegin + 57;   // 유닛 피커 테이블1 플레이스 위치에 있지 않습니다. 
        public const int emsNotUnitPkXStage2Pos                     = eEMSBegin + 58;   // 유닛 피커 테이블2 플레이스 위치에 있지 않습니다. 
        public const int emsDoorOpen                                = eEMSBegin + 59;   // 도어 오픈 센서 감지 하였습니다. 설비 도어 close 상태 확인 바랍니다.
        public const int emsNotMoveMapBlock1BecauseUnitPkr          = eEMSBegin + 60;   // 맵-블록 테이블1 이송 할 수 없습니다. 유닛 피커 맵블록1 테이블 위치에서 z축 대기위치보다 낮습니다.
        public const int emsNotMoveMapBlock2BecauseUnitPkr          = eEMSBegin + 61;   // 맵-블록 테이블1 이송 할 수 없습니다. 유닛 피커 맵블록1 테이블 위치에서 z축 대기위치보다 낮습니다.
        public const int emsReworkStackerNotDown                    = eEMSBegin + 62;   // REWORK TRAY STACKER DOWN 상태가 아닙니다.
        public const int emsGoodStackerNotDown                      = eEMSBegin + 63;   // GOOD TRAY STACKER DOWN 상태가 아닙니다.
        public const int emsRworkStackerTraySensing                 = eEMSBegin + 64;   // REWORK STACKER 레일 위에 TRAY 유무 센서 TRAY 감지 되어있습니다.
        public const int emsGoodStackerTraySensing                  = eEMSBegin + 65;   // GOOD STACKER 레일 위에 TRAY 유무 센서 TRAY 감지 되어있습니다.
        public const int emsNotMoveReworkTrayFeederBecauseTrayPk    = eEMSBegin + 66;   // REWORK TRAY FEEDER 이송 할 수 없습니다. 트레이 피커 REWORK TRAY RAIL 위치에서 피커 Z축 대기 위치보다 낮습니다. 
        public const int emsNotMoveGoodTrayFeederBecauseTrayPk      = eEMSBegin + 67;   // GOOD TRAY FEEDER 이송 할 수 없습니다. 트레이 피커 GOOD TRAY RAIL 위치에서 피커 Z축 대기 위치보다 낮습니다.
        public const int emsReworkLoadingLocationSensing            = eEMSBegin + 68;   // REWORK TRAY LOADING 레일 위에 TRAY 유무 센서 TRAY 감지 되어 있습니다.
        public const int emsGoodLoadingLocationSensing              = eEMSBegin + 69;   // GOOD TRAY LOADING 레일 위에 TRAY 유무 센서 TRAY 감지 되어 있습니다.
        public const int emsRworkPlaceLocationSensing               = eEMSBegin + 70;   // REWORK 유닛 플레이스 레일 위에 TRAY 유무 센서 TRAY 감지 되어 있습니다.
        public const int emsGoodPlaceLocationSensing                = eEMSBegin + 71;   // GOOD 유닛 플레이스 레일 위에 TRAY 유무 센서 TRAY 감지 되어 있습니다.
        public const int emsNotTrayUnloading_ConveyorUnloadingCheck = eEMSBegin + 72;   // GOOD TRAY 콘베어 배출 전  배출 끝단에 TRAY 감지 되어 있습니다. 배출부 레일 확인 바랍니다.
        public const int emsNotGoodTray1Stacker_GoodTray2Clamp      = eEMSBegin + 73;   // GOOD TRAY FEEDER1 스태커 위치로 이송 할 수 없습니다. GOOD TRAY FEEDER2 스태커 위치에서 트레이 락 걸려 있습니다.
        public const int emsNotGoodTray2Stacker_GoodTray1Clamp      = eEMSBegin + 74;   // GOOD TRAY FEEDER2 스태커 위치로 이송 할 수 없습니다. GOOD TRAY FEEDER1 스태커 위치에서 트레이 락 걸려 있습니다.
        public const int emsNotGoodTray1Loading_GoodTray2Clamp      = eEMSBegin + 75;   // GOOD TRAY FEEDER1 로딩 위치로 이송 할 수 없습니다. GOOD TRAY FEEDER2 로딩 위치에서 트레이 락 걸려 있습니다.
        public const int emsNotGoodTray2Loading_GoodTray1Clamp      = eEMSBegin + 76;   // GOOD TRAY FEEDER2 로딩 위치로 이송 할 수 없습니다. GOOD TRAY FEEDER1 로딩 위치에서 트레이 락 걸려 있습니다.
        public const int emsNotGoodTray1Place_GoodTray2Clamp        = eEMSBegin + 77;   // GOOD TRAY FEEDER1 유닛 플레이스 위치로 이송 할 수 없습니다. GOOD TRAY FEEDER2 유닛 플레이스 위치에서 트레이 락 걸려 있습니다.
        public const int emsNotGoodTray2Place_GoodTray1Clamp        = eEMSBegin + 78;   // GOOD TRAY FEEDER2 유닛 플레이스 위치로 이송 할 수 없습니다. GOOD TRAY FEEDER1 유닛 플레이스 위치에서 트레이 락 걸려 있습니다.
        public const int emsNotStripPkVac                           = eEMSBegin + 79;   // 스트립 피커 스트립 픽업 전 진공 센서 확인 되어있습니다. 스트립 피커 상태 확인 바랍니다.
        public const int emsFailITSDataReading                      = eEMSBegin + 80;   // ITS 데이터 리딩 실패 하였습니다.
        public const int emsPRSReadedFail                           = eEMSBegin + 81;   // 결과 값 리딩 후 비전 prs data result 신호 off 안됨
        public const int emsPkCalREadingFail                        = eEMSBegin + 82;   // 피커 켈리브레이션 결과 값 리딩 실패
        public const int emsITSServoNotConnecting                   = eEMSBegin + 83;   // ITS 서보 연결 안되어 있습니다. 서보 연결 상태 확인 바랍니다.
        public const int emsITSCountDataReadingFail                 = eEMSBegin + 84;   // ITS COUNTER 좌표값 없음 ITS 서보 확인 바랍니다.
        public const int emsITSLocationDataReadingFail              = eEMSBegin + 85;   // ITS LOCATION LIST 없음 ITS 서보 확인 바랍니다.
        public const int emsITSLocationDataListFail                 = eEMSBegin + 86;   // ITS LOCATION LIST 배열 틀림 ITS 서보 확인 바랍니다.
        public const int emsLotIDFail                               = eEMSBegin + 87;   // 현재 진행 LOT ID의 스트립 아닙니다. 스트립 ID 또는 LOT ID 확인 바랍니다.
        public const int emsRailStrpRemove                          = eEMSBegin + 88;   // 레일 위에 스트립 제거 하셔야합니다.! 
        public const int emsUnitInspectionReTrayTimeOut             = eEMSBegin + 89;   // 비전에서 유닛 사이즈 검사 재검사 요청 신호 응답 시간 오버 되었습니다.
        public const int emsDesertUnitBarcodeMemory                 = eEMSBegin + 90;   // 맵블록 테이블 유닛 바코드 메모리 지워짐
        public const int emsNotFindITSCountFile                     = eEMSBegin + 91;   // ITS 수량 파일 찾을 수 없습니다. 
        public const int emsNotFindITSLocationFile                  = eEMSBegin + 92;   // ITS 좌표 파일 찾을 수 없습니다. 
        public const int emsITSCountDataParsingFail                 = eEMSBegin + 93;   // ITS 수량 파일 파싱 중 에러 발생 하였습니다.
        public const int emsITSLocationDataParsingFail              = eEMSBegin + 94;   // ITS 좌표계 파일 파싱 중 에러 발생 하였습니다.
        public const int emsSawUldReqSignalFail                     = eEMSBegin + 95;   // 유닛 피커 다이싱 테이블에 픽업 후 다이싱 유닛 언로딩 요청 신호 이상 에러 발생 했습니다.
        public const int emsSawUldPosSignalFail                     = eEMSBegin + 96;   // 유닛 피커 다이싱 테이블에 픽업 후 다이싱 테이블 언로더 위치 신호 이상 에러 발생 했습니다.
        public const int emsSawStageBlowSignalFail                  = eEMSBegin + 97;   // 유닛 피커 다이싱 테이블에 픽업 후 다이싱 테이블 파기 신호 이상 에러 발생 했습니다. 

        public const int emsBtmCamNotUse                            = eEMSBegin + 98;   // 셋업창에서 하부 카메라 사용 모드로 전환 후 사용하셔야 합니다. (- BOTTOM VISION INSPECTION : USE) 
        public const int emsBtmCamAndITSDataNotUse                  = eEMSBegin + 99;   // 셋업창에서 하부 카메라 사용 하거나 ITS 데이터 사용 하셔야 합니다. (- BOTTOM VISION INSPECTION : USE 또는 ITS 데이터 사용 유무 : USE) 
        #endregion

        #region >>ARRAY
        public static int[] Null                                    = { };
        public static int[] ConverRunCheck                          = { LoaderConveyorTimeOver };
        public static int[] GripperXNotMove                         = { emsInLetTableNotDown, emsStripPkZNotReadyPos };
        public static int[] UnitPlaceFail                           = { emsNotUnitPlace1, emsNotUnitPlace2 };
        public static int[] StageVacuum                             = { Stage1VacFail, Stage2VacFail };
        public static int[] StageUnitExist                          = { emsStage1UnitExist, emsStage2UnitExist };
        public static int[] EmptyStopperLock                        = { EmptyStopperLeftForntLockFail, EmptyStopperLeftRearLockFail, EmptyStopperRightForntLockFail, EmptyStopperRightRearLockFail };
        public static int[] EmptyStopperUnlock                      = { EmptyStopperLeftForntUnlockFail, EmptyStopperLeftRearUnlockFail, EmptyStopperRightForntUnlockFail, EmptyStopperRightRearUnlockFail };
        public static int[] EmptyFeederGrip                         = { EmptyFeederGripFail };
        public static int[] EmptyFeederUnGrip                       = { EmptyFeederUngripFail };
        public static int[] GoodFeeder1Grip                         = { GoodFeeder1FrontGripFail, GoodFeeder1RearGripFail };
        public static int[] GoodFeeder1UnGrip                       = { GoodFeeder1FrontUnGripFail, GoodFeeder1RearUnGripFail };
        public static int[] GoodFeeder1FrontGrip                    = { GoodFeeder1FrontGripFail };
        public static int[] GoodFeeder1FrontUnGrip                  = { GoodFeeder1FrontUnGripFail };
        public static int[] GoodFeeder1RearGrip                     = { GoodFeeder1RearGripFail };
        public static int[] GoodFeeder1RearUnGrip                   = { GoodFeeder1RearUnGripFail };
        public static int[] GoodFeeder2Grip                         = { GoodFeeder2FrontGripFail, GoodFeeder2RearGripFail };
        public static int[] GoodFeeder2UnGrip                       = { GoodFeeder2FrontUnGripFail, GoodFeeder2RearUnGripFail };
        public static int[] GoodFeeder2FrontGrip                    = { GoodFeeder2FrontGripFail };
        public static int[] GoodFeeder2FrontUnGrip                  = { GoodFeeder2FrontUnGripFail };
        public static int[] GoodFeeder2RearGrip                     = { GoodFeeder2RearGripFail };
        public static int[] GoodFeeder2RearUnGrip                   = { GoodFeeder2RearUnGripFail };
        public static int[] ChkGoodTrayTransfer                     = { emsGoodTrayFeederFrontUnGrip, emsGoodTrayFeederRearUnGrip };
        public static int[] ReWorkFeederGrip                        = { ReWorkTrayGripFail };
        public static int[] ReWorkFeederUnGrip                      = { ReWorkTrayUnGripFail };
        public static int[] XMarkReTrain                            = { emsHD1PickerXMarkReTrain, emsHD2PickerXMarkReTrain };
        public static int[] TrayFeederTrayVanish                    = { emsGoodTray1Vanish, emsGoodTray2Vanish, emsReworkTrayVanish };
        public static int[] PkThNotPlcPos                           = { emsX1PkThNotPlcPos, emsX2PkThNotPlcPos };
        public static int[] TrayPkPickUpFail_GoodTray               = { emsTrayPkPickUpFail_GoodTray1, emsTrayPkPickUpFail_GoodTray2 };
        public static int[] TrayAlignUpFail                         = { TrayFrontAlignUpFail, TrayRearAlignUpFail };
        public static int[] TrayAlignDnFail                         = { TrayFrontAlignDnFail, TrayRearAlignDnFail };


        #endregion

        #region >> FUNCTION
        public static void SET_INI(){
#if _NSS3300
            eDOOR           = new short[] { /*E.DOOR_SAW_FRONT_RIGHT,*/ E.DOOR_SORTER_FRONT_LEFT, E.DOOR_SORTER_FRONT_RIGHT, E.DOOR_SORTER_RIGHT_SIDE_LEFT, E.DOOR_SORTER_RIGHT_SIDE_RIGHT, E.DOOR_SORTER_BACK_LEFT, E.DOOR_SORTER_BACK_RIGHT };
            eTRIP           = new short[] { E.SERVO1_SAW, E.SERVO2_SAW, E.SERVO3_SAW, E.SERVO1_SORTER, E.SERVO3_SORTER };
#else
            eDOOR   = new short[] { DOOR_SAW_FRONT_LEFT, DOOR_SAW_FRONT_RIGHT, DOOR_SAW_SIDE_LEFT, DOOR_SORTER_FRONT_LEFT, DOOR_SORTER_FRONT_RIGHT, DOOR_SORTER_RIGHT_SIDE_LEFT, DOOR_SORTER_RIGHT_SIDE_RIGHT, DOOR_SORTER_BACK_LEFT, DOOR_SORTER_BACK_RIGHT };
            eTRIP   = new short[] { SERVO1_SAW, SERVO2_SAW, SERVO3_SAW, SERVO4_SAW, CONV_TRIP, SERVO1_SORTER, SERVO2_SORTER, SERVO3_SORTER };
#endif
            eEMO    = new short[] { EMO_SAW_FRONT, EMO_SAW_REAR, EMO_SORTER_FRONT, EMO_SORTER_RIGHT, EMO_SORTER_BACK };
            eAEAR   = new short[] { /*LD_CONV_AREA_SENSOR*/ };
            eAIR    = new short[] { DRIVER_AIR_PRESSURE, BLOW_AIR_PRESSURE, STAGE_AIR_PRESSURE, PICKER_AIR_PRESSURE };
        }

        public static void LabelDEFINE_MOTION_ERR(){
            eMTEnd = CNT_.MT * eMTGap;
            string[] sMT = { "CW LIMIT", "CCW LIMIT", "NOT HOME", "CW+ SOFT LIMIT POSITION",
                           "CCW- SOFT LIMIT POSITION", "MOVING TIME OVER (CMD/ACT NotSame)", "NOT SERVO ON", "SERVO ALARM", "MOVING", "INITIALIZE FAIL"};

            for (int m = 0; m < eMTEnd; m++)
            {
                int MtNUM = (m - eMTBegin) / 10;
                int ErrNUM = (m - eMTBegin) % 10;
                ErrName[m] = "[" + MtName[MtNUM] + "] " + sMT[ErrNUM] + " ERROR";
            }

            for (int e = 0; e < CNT_.ERR; e++){
                if (e >= eErrBegin) ErrName[e] = FILE_.RDString(PATH_.ErrDEFINE, "ERROR DEFINE", "NAME_" + e.ToString(), "");
                ErrINFO[e].enRec = FILE_.RDBool(PATH_.ErrDEFINE, "ERROR DEFINE", "EN_" + e.ToString(), false); //  ErrINFO[e].enRec = true면 저장안함 /  ErrINFO[e].enRec = false면 저장함.
                ErrINFO[e].kind = FILE_.RDInt(PATH_.ErrDEFINE, "ERROR DEFINE", "KINK_" + e.ToString(), 0);
                int rstLevel = FILE_.RDInt(PATH_.ErrDEFINE, "ERROR DEFINE", "LEVEL_" + e.ToString(), 0);
                ErrINFO[e].rstLevel = (eLogLevel)rstLevel;
            }
        } //모션 에러 리스트

        public static string GET_ERROR_NAME(int iERR){
            try{
                return ErrName[iERR];
            }
            catch (Exception ex) { LogWR_.SaveLogException("mUTIL -> GET_ERROR_NAME", ex); }
            return "";
        }
        public static string GET_ERROR_TITLE_1(int iERR){
            try{
                return ErrTitle_1[iERR];
            }
            catch (Exception ex) { LogWR_.SaveLogException("mUTIL -> GET_ERROR_TITLE_1", ex); }
            return "";
        }
        public static string GET_ERROR_TITLE_2(int iERR){
            try{
                return ErrTitle_2[iERR];
            }
            catch (Exception ex) { LogWR_.SaveLogException("mUTIL -> GET_ERROR_TITLE_2", ex); }
            return "";
        }

        public static void OnERROR_MESSAGE(int Num, string Message, int Delay)
        {
            ErrName[Num] = Message;
            IsERR[Num] = true;
            O.STOP_ACMOTOR();
            UTIL_.DELAY(Delay);
        }
        public static void OnERROR_EXCEPT(string message, int delay)
        {
            int eNUM = CNT_.ERR;
            ErrName[eNUM] = message;
            IsERR[eNUM] = true;
            O.STOP_ACMOTOR();
            UTIL_.DELAY(delay);
        }

        public static int CHK_ERR(){
            for (int i = 0; i < CNT_.ERR; i++){
                if (IsERR[i]) return i;
            }
            return -1;
        }

        public static void CLEAR_ERROR(){
            for (int i = 0; i < CNT_.ERR; i++){
                if (IsERR[i]){
                    if (eLoginLevel >= ErrINFO[i].rstLevel){
                        if (IsERR[i]){
                            // MES 에러 보고
                            SUBFRM_.gSecsGem.OnAlarmClear(CMES.eER + i);
                        }
                        IsERR[i] = false;
                    }
                }
            }
            bOnERROR = false;
            O.BZ_OFF();
        }

        public static void OnERROR_MOTION(int m, int kind, int OnERR_DELAY){
            E.OnERROR((eMTBegin + (eMTGap * m) + kind), OnERR_DELAY);
        }

        public static void OnERROR(int num){
            if (!IsERR[num]){
                //mes alarm 보고
                SUBFRM_.gSecsGem.OnAlarmSet(CMES.eER + num);
            }
            IsERR[num] = true;
            O.STOP_ACMOTOR();
            UTIL_.DELAY(500);
        }
        public static void OnERROR(int num, int OnERR_DELAY){
            if (!IsERR[num]){
                //mes alarm 보고
                SUBFRM_.gSecsGem.OnAlarmSet(CMES.eER + num);
            }
            IsERR[num] = true;
            O.STOP_ACMOTOR();
            UTIL_.DELAY(OnERR_DELAY);
        }

        public static bool OnINTERLOCK(int iNUM, eCHK_INTERLOCK CHK){
            if (bINTRK[iNUM - eEMSBegin]){
                if (bINTRK[iNUM - eEMSBegin]){
                    if (CHK == eCHK_INTERLOCK.AUTO) OnERROR(iNUM, 500);
                    return true; // 인터락 발생 구동 금지
                }
            }
            return false;   // 정상
        }
        public static bool OnINTERLOCK(int iNUM, bool WithError){
            if (bINTRK[iNUM - eEMSBegin]){
                if (bINTRK[iNUM - eEMSBegin]){
                    //if (iNUM == 482 || iNUM == 483 || iNUM == 484){
                    //    double dCP = LAB_.GET_ACTPOS(9);
                    //    LogWR_.DEBUG_PRINT("유닛 피커 에러 " + iNUM.ToString() + " - " + dCP.ToString());
                    //}
                    if (WithError) OnERROR(iNUM, 500);
                    return true; // 인터락 발생 구동 금지
                }
            }
            return false; // 정상
        }
        #endregion << FUNCTION
    } //ERROR DEFINE
}