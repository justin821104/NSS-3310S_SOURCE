using LIB_.DateType;
using NSS_3310S;
using NSS_3310S.SEQ;
using Object;
using System;

namespace SYSTEM{
    public class ManualNumber{
        #region >> HOME
        public const int AllHome                    = 999;

        public const int HomeElevater               = 0;
        public const int HomeRail                   = 1;
        public const int HomeStripPk                = 2;
        public const int HomeUnitPk                 = 3;
        public const int HomeMapBlock1              = 4;
        public const int HomeMapBlock2              = 5;
        public const int HomeVision                 = 6;
        public const int HomeHead1                  = 7;
        public const int HomeHead2                  = 8;
        public const int HomeTrayPk                 = 9;
        public const int HomeOkTrayFeeder           = 10;
        public const int HomeNGTrayFeeder           = 11;
        public const int HomeEmpty                  = 12;
        #endregion

        public const int Trigger1                   = 18;
        public const int Trigger2                   = 19;

        public const int MGZClamp                   = 50;
        public const int Pusher                     = 51;
        public const int InLetTable                 = 52;
        public const int Gripper                    = 53;

        public const int StripVac                   = 54;
        public const int StripBlow                  = 55;
        public const int UnitVac                    = 56;
        public const int UnitBlow                   = 57;
        public const int ScrapVac                   = 58;
        public const int ScrapBlow                  = 59;
        public const int CleanerWater               = 60;
        public const int CleanerAir                 = 61;
        public const int CleanerSwing               = 62;

        public const int StageAirshower             = 63;
        public const int StageVac                   = 64;
        public const int TopCamBlow                 = 65;
        public const int Trigger                    = 66;
        public const int BtmCamCalZig               = 67;
        public const int BtmCamBlow                 = 68;

        public const int PkrVac                     = 69;
        public const int PkrBlow                    = 70;
        public const int PkrFree                    = 71;
        public const int GoodTray1_Clamp            = 72;
        public const int GoodTray2_Clamp            = 73;
        public const int GoodTrayStackerTable       = 74;
        public const int GoodTrayPreAlign           = 75;
        public const int ReworkTray_Clamp           = 76;
        public const int ReworkTrayStackerTable     = 77;

        public const int TrayPkClamp                = 78;
        public const int EmptyStackerStopper        = 79;
        public const int EmptyTrayClamp             = 80;
        public const int EmptyTrayFeeder            = 81;

        public const int MGZRdy                     = 85;
        public const int MGZUnloading               = 86;
        public const int MGZLoading                 = 87;
        public const int MGZSlot                    = 88;

        public const int RailRdy                    = 90;
        public const int RailLoading                = 91;
        public const int RailWork                   = 92;
        public const int RailPicOpen                = 93;

        public const int BarcodeRdy                 = 95;
        public const int BarcodeReading             = 96;

        public const int GripperRdy                 = 100;
        public const int GripperCatch               = 101;
        public const int GripperCatchCheck          = 102;
        public const int GripperBarcodeReading      = 103;
        public const int GripperLoading             = 104;
        public const int GripperStripPic            = 105;

        public const int StripPkPic                 = 106;
        public const int StripPkPlc                 = 107;
        public const int StripPkUp                  = 108;
        public const int StripPkDn                  = 109;
        public const int StripPkRdy                 = 110;
        public const int StripPkFristAlign          = 111;
        public const int StripPkSecondAlign         = 112;

       
        public const int UnitPkRdy                  = 115;
        public const int UnitPkPic                  = 116;
        public const int UnitPkScrap1               = 117;
        public const int UnitPkScrap2               = 118;
        public const int UnitPkBrush                = 119;
        public const int UnitPkCleaner              = 120;
        public const int UnitPkAirshower            = 121;
        public const int UnitPkStage1               = 122;
        public const int UnitPkStage2               = 123;
        public const int UnitPkUp                   = 124;
        public const int UnitPkDn                   = 125;

        public const int PreAlignRdy                = 126;
        public const int PreAlignFirst              = 127;
        public const int PreAlignSecond             = 128;

        public const int StageRdy                   = 130;
        public const int StageReceive               = 131;
        public const int StageAirshowerStart        = 132;
        public const int StageTopCamView            = 133;
        public const int StageHDCam1View            = 134;
        public const int StageHDCam2View            = 135;

        public const int TopCamStaeView             = 136;

        public const int TopCamRdy                  = 138;
        public const int TopCamStage1View           = 139;
        public const int TopCamStage2View           = 140;

        public const int BtmCamRdy                  = 141;
        public const int BtmCamHD1CamCenter         = 142;
        public const int BtmCamHD1PkCenter          = 143;
        public const int BtmCamHD2CamCenter         = 144;
        public const int BtmCamHD2PkCenter          = 145;

        public const int HDXRdy                     = 146;
        public const int HDXCamCenter               = 147;
        public const int HDXPkCenter                = 148;
        public const int HDXReject                  = 149;
        public const int HDXStage1View              = 150;
        public const int HDXStage2View              = 151;
        public const int HDXGoodTray1View           = 152;
        public const int HDXGoodTray2View           = 153;
        public const int HDXReworkTrayView          = 154;

        public const int HDCamCenter                = 155;
        public const int HDPkCenter                 = 156;

        public const int AllPkZRdy                  = 157;
        public const int PkZRdy                     = 158;
        public const int PkOddZPic                  = 159; //홀픽 (1,3,5)
        public const int PkEvenPic                  = 160; //짝픽 (2,4,6)
        public const int PkOddZPlc                  = 161; //홀플 (1,3,5)
        public const int PkEvenPlc                  = 162; //짝플 (2,4,6)

        public const int PkThRdy                    = 163;
        public const int PkPicTh                    = 164;
        public const int PkPlcTh                    = 165;
        public const int PkTh                       = 166;

        public const int TrayFeederRdy              = 167;
        public const int TrayFeederLoading          = 168;
        public const int TrayFeederPlc              = 169;
        public const int TrayFeederStacker          = 170;
        public const int TrayFeederConvULDStart     = 171;
        public const int TrayFeederTrayPusherStart  = 172;
        public const int TrayFeederConvUnloading    = 173;
        public const int TrayFeederHD1Plc           = 174;
        public const int TrayFeederHD2Plc           = 175;

        public const int StageHDCamView             = 176;
        public const int TrayHDCamView              = 177;
        public const int StageZigView               = 178;

        public const int TrayPkRdy                  = 180;
        public const int TrayPkGoodFeeder           = 181;
        public const int TrayPkReworkFeeder         = 182;
        public const int TrayPkEmptyFeeder          = 183;
        public const int TrayPkUp                   = 184;
        public const int TrayPkDn                   = 185;

        public const int EmptyStackerRdy            = 186;
        public const int EmptyStackerLock           = 187;
        public const int EmptyStackerLoading        = 188;
        public const int EmptyStackerWork           = 189;

        public const int RunStageAirshower          = 190;
        public const int RunUnitInspection          = 191;

        public const int RunStripLoading            = 192;
        public const int RunStripPic                = 193;
        public const int RunStripPlc                = 194;
        public const int RunUnitPic                 = 195;
        public const int RunScrap                   = 196;
        public const int RunCleaner                 = 197;
        public const int RunBrush                   = 198;
        public const int RunUnitAirshower           = 199;
        public const int RunUnitCleaning            = 200;
        public const int RunUnitPlc                 = 201;

        public const int RunGoodTray1Unloading      = 202;
        public const int RunGoodTray1Loaidng        = 203;
        public const int RunGoodTray2Unloading      = 204;
        public const int RunGoodTray2Loaidng        = 205;
        public const int RunReworkTrayUnloading     = 206;
        public const int RunReworkTrayLoading       = 207;
        public const int RunEmptyTrayLoading        = 208;
        public const int RunEmptyTrayPic            = 209;

        public const int RunMapBlockLocation        = 210;
        public const int RunTrayLocation            = 211;
        public const int RunPRS                     = 212;

        public const int MasterZigCenterHD          = 213;

        public const int RunMGZLoading              = 215;
        public const int RunMGZUnloading            = 216;
        public const int RunMGZSlotLocation         = 217;

        public const int SawStageVac                = 218;
        public const int SawStageRej                = 219;
        public const int RunPickerCal               = 220;
        public const int RunPickerAutoCal           = 221;

        public const int InLet_VAC                  = 222;
        public const int InLet_Blow                 = 223;
        public const int LDConv_BWD                 = 224;
        public const int LDConv_FWD                 = 225;

        public const int AllPickerAutoCal           = 226;

        public const int StringBarcodeReading       = 227;

        public const int UnitPkrAirShower           = 228;

        public const int PkrPic = 229;

        public const int TopCamCalZigCenter = 300;


        public static int[] MNSol = { MGZClamp, Pusher, InLetTable, Gripper,
                                      StripVac, StripBlow, UnitVac, UnitBlow, ScrapVac, ScrapBlow, CleanerWater, CleanerAir, CleanerSwing,
                                      StageAirshower, StageVac, TopCamBlow, Trigger, BtmCamCalZig, BtmCamBlow,
                                      PkrVac, PkrBlow, PkrFree, GoodTray1_Clamp, GoodTray1_Clamp, GoodTray2_Clamp, GoodTray2_Clamp, GoodTrayStackerTable, GoodTrayPreAlign, ReworkTray_Clamp, ReworkTrayStackerTable, Trigger, Trigger,
                                      TrayPkClamp, EmptyStackerStopper, EmptyTrayClamp, EmptyTrayFeeder,
                                      SawStageVac, SawStageVac, SawStageRej, SawStageRej,
                                      InLet_VAC, InLet_Blow, LDConv_BWD, LDConv_FWD, UnitPkrAirShower
        };
        public static int[] MNMotor = { MGZRdy, MGZUnloading, MGZLoading, MGZSlot, 
                                        RailRdy, RailLoading, RailWork, BarcodeRdy, BarcodeReading, 
                                        GripperRdy, GripperCatch, GripperBarcodeReading, GripperLoading, GripperStripPic,
                                        StripPkPic, StripPkPlc, StripPkUp, StripPkDn, UnitPkPic, UnitPkCleaner, UnitPkStage1, UnitPkStage2, UnitPkUp, UnitPkDn,
                                        StageRdy, StageReceive, TopCamStaeView, TopCamRdy,
                                        HDXRdy, HDCamCenter, HDPkCenter, HDXReject, AllPkZRdy, PkPicTh, PkPlcTh, PkTh, TrayFeederRdy, TrayFeederLoading, TrayFeederPlc, TrayFeederStacker, TrayFeederConvULDStart, TrayFeederTrayPusherStart, TrayFeederConvUnloading, StageHDCamView, TrayHDCamView,
                                        TrayPkGoodFeeder, TrayPkReworkFeeder, TrayPkEmptyFeeder, TrayPkUp, TrayPkDn, EmptyStackerLock, EmptyStackerLoading, EmptyStackerWork
        };
        public static int[] MNCycleRun = { RunMGZLoading, RunMGZUnloading, RunMGZSlotLocation,
                                           RunStageAirshower, RunUnitInspection,
                                           RunStripLoading, RunStripPic, RunStripPlc, RunUnitPic, RunScrap, RunCleaner, RunBrush, RunUnitAirshower, RunUnitCleaning, RunUnitPlc,
                                           RunStageAirshower, RunUnitInspection,
                                           RunGoodTray1Unloading, RunGoodTray2Unloading, RunGoodTray1Loaidng, RunGoodTray2Loaidng,
                                           RunReworkTrayUnloading, RunReworkTrayLoading,
                                           RunEmptyTrayLoading, RunEmptyTrayPic,
                                           RunPickerCal, RunPRS, RunPRS, AllPickerAutoCal, PkrPic, TopCamCalZigCenter
        };
        public static int[] MTMotor = { TopCamStaeView, TopCamStaeView,
                                        StageHDCamView, StageHDCamView,
                                        TrayFeederLoading, TrayFeederConvULDStart, TrayHDCamView };
    }

    public class RUN_MANUAL : DATA_{
        readonly int nThread = T.Manual;
        public double sTIME = 0, eTIME = 0;
#if _NSS3300
        readonly int[] Home_1st = { M.Rail };
        readonly int[] Home_3nd = { M.ElvZ, M.StripPkX, M.UnitPkX, M.GrpX, M.Table1, M.Table2, M.TopVisionX, M.BtnVisionY, M.TrayPickerX, M.TrayFeeder1, M.TrayFeeder2, M.TrayFeeder3, M.TRIGGER1, M.TRIGGER2, M.X1T, M.X2T };
#else
        readonly int[] Home_1st = { M.RailF, M.RailR, };
        readonly int[] Home_3nd = { M.ElvZ, M.StripPkX, M.PreAlign, M.UnitPkX, M.GrpX, M.Table1, M.Table2, M.TopVisionX, M.BtnVisionY, M.TrayPickerX, M.TrayFeeder1, M.TrayFeeder2, M.TrayFeeder3, M.TRIGGER1, M.TRIGGER2, M.X1T, M.X2T };
#endif
        readonly int[] Home_2nd = { M.ElvY, M.Barcode, M.StripPkZ, M.UnitPkZ, M.TopVisionZ, M.BtnVisionZ, M.TrayPickerZ, M.EmptyElv, M.X1Z12, M.X1Z34, M.X1Z56, M.X2Z12, M.X2Z34, M.X2Z56 };
        public void Do(){
            bMF = false;
            do{
                if (gExit) break;
                UTIL_.DELAY(10);
                if (eMCStatus == eMachineStatus.DRY || eMCStatus == eMachineStatus.AUTO || !bMF) continue;
                if (!I.CHK_DOOR()){
                    W.ViewWarning(nThread, W.ChkDoor, sWarnningMessage);
                    O.RESET_DOORLOCK();
                    EndManual();
                    mIN[I.vtStop] = true;
                    continue;
                }

                LogWR_.SaveLogManual(iMANUAL.Label, "MANUAL");
                bErrNotSave = true;
                bBzSTOP = false;
                dRunRate = prMACHINE[CP.ManualRunRate];
                sTIME = Environment.TickCount;
                IsDOUBLE[D.ManRunTime] = 0;
                ConfirmUser[W.ProductRemove].msg = "";
                switch (iMANUAL.Number){
#region >> HOME
                    case ManualNumber.AllHome:
                        BASE.LogStart(nThread, "ALL HOME 진행");
                        LAB_.CLEAR_MOVEDATA();
                        if (bBD){
                            for (int i = 0; i < CNT_.MT; i++) mtSTS[i].strHome = "HOME START";
                            UTIL_.DELAY(1000);
                            for (int i = 0; i < CNT_.MT; i++) mtSTS[i].bHomeComplete = true;
                            for (int i = 0; i < CNT_.THREAD; i++) UseThread[i] = true;
                            for (int i = 0; i < CNT_.MT; i++) mtSTS[i].strHome = "HOME OK";
                            bAllHomeComplete = true;
                            bInitialComplete = true;
                            BASE.LogEnd(nThread, "ALL HOME 완료");
                            break;
                        }
                        if (!CheckInitailSensor()){
                            W.ViewWarning(nThread, W.ProductRemove);
                            B.Bit(nThread, B.InitFail, true, "초기화 진행 중 실패 (InitialSensor Fail)");
                            break;
                        }
                        if (prMACHINE[CP.TrayUnloadingMode] == (int)eULD_TRAY.CONVEYOR && !bDRYRUN){
                            if (!mIN[I.ULD_CONV_READY]){
                                ConfirmUser[W.ProductRemove].msg = "초기화 실패! = 배출 콘베어 런 상태 아닙니다. (READY 신호 안들어옴)";
                                W.ViewWarning(nThread, W.ProductRemove);
                                B.Bit(nThread, B.InitFail, true, "초기화 진행 중 실패 (콘베어 설비 런 상태 아님)");
                                break;
                            }
                        }
                        LAB_.TriggerOutput((int)eTRIGGER.HD1, uVAL.Low);
                        LAB_.TriggerOutput((int)eTRIGGER.HD2, uVAL.Low);
                        B.Initial();
                        if (!InitialMachine()){
                            W.ViewWarning(nThread, W.ProductRemove);
                            B.Bit(nThread, B.InitFail, true, "초기화 진행 중 실패 (InitalMachine Fail)");
                            break;
                        }
                        if (eRTN.SUCESS != BASE.MoveAllPkRdy(nThread, "", "모든 피커 대기 위치 이송")){
                            ConfirmUser[W.ProductRemove].msg = "모든 피커 대기 위치 이송 실패";
                            W.ViewWarning(nThread, W.ProductRemove);
                            B.Bit(nThread, B.InitFail, true, "초기화 진행 중 실패 (All Picker Ready Position Move Fail)");
                            break;
                        }
                        if (eRTN.SUCESS != MoveFirstStandbyPos(nThread, "초기화 완료 후 첫번째 모터 그룹 대기 위치 이송")){
                            ConfirmUser[W.ProductRemove].msg = "초기화 완료 후 첫번째 모터 그룹 대기 위치 이송 실패";
                            W.ViewWarning(nThread, W.ProductRemove);
                            B.Bit(nThread, B.InitFail, true, "초기화 진행 중 실패 (모터 홈 동작 후 첫번째 그룹 대기 위치 이송 실패)");
                            break;
                        }
                        if (eRTN.SUCESS != MoveSecondStandbyPos(nThread, "초기화 완료 후 두번째 모터 그룹 대기 위치 이송")){
                            ConfirmUser[W.ProductRemove].msg = "초기화 완료 후 두번째 모터 그룹 대기 위치 이송 실패";
                            W.ViewWarning(nThread, W.ProductRemove);
                            B.Bit(nThread, B.InitFail, true, "초기화 진행 중 실패 (모터 홈 동작 후 두번째 그룹 대기 위치 이송 실패)");
                            break;
                        }
                        CheckPicker();

                        //REWORK TRAY  유무 확인 후 배출 
#if _NSS3300
                        if (mIN[I.NG_RAIL_STACKER_TRAY_CHECK]){
#else
                        if (!mIN[I.NG_RAIL_STACKER_TRAY_CHECK] || !mIN[I.NG_RAIL_LOADING_TRAY_CHECK]){
#endif
                            if (!UnloaidngReWorkTray("RE-WORK 트레이 배출")){
                                ConfirmUser[W.ProductRemove].msg = "RE-WORK 트레이 배출 실패";
                                W.ViewWarning(nThread, W.ProductRemove);
                                B.Bit(nThread, B.InitFail, true, "초기화 진행 중 실패 (RE-WORK 트레이 배출 실패)");
                                break;
                            }
                        }
                        else{
                            if (eRTN.SUCESS != C.ReworkTrayFeeder.UnGrip("RE-WORK 트레이 언그립")){
                                ConfirmUser[W.ProductRemove].msg = "RE-WORK 트레이 피더 언그립 실패";
                                W.ViewWarning(nThread, W.ProductRemove);
                                B.Bit(nThread, B.InitFail, true, "초기화 진행 중 실패 (RE-WORK 트레이 언그립 실패)");
                            }
                        }

                        //GOOT TRAY 1/2 유무 확인 후 배출
                        if (prMACHINE[CP.TrayUnloadingMode] == (int)eULD_TRAY.CONVEYOR && !bDRYRUN){
                            if (!mIN[I.ULD_CONV_READY]){
                                ConfirmUser[W.ProductRemove].msg = "초기화 실패! = 배출 콘베어 런 상태 아닙니다. (READY 신호 안들어옴)";
                                W.ViewWarning(nThread, W.ProductRemove);
                                B.Bit(nThread, B.InitFail, true, "초기화 진행 중 실패 (배출 콘베어 런 상태 아님)");
                                break;
                            }
                            if (!RunPlaceZoneConveryorUnloading()){
                                ConfirmUser[W.ProductRemove].msg = "초기화 실패! = GOOD TRAY 레일부 TRAY 제거 실패! (GOOD TRAY 레일부에 트레이 모두 제거 하셔야 합니다.)";
                                W.ViewWarning(nThread, W.ProductRemove);
                                B.Bit(nThread, B.InitFail, true, "초기화 진행 중 실패 (TRAY 레일부 트레이 있음)");
                                break;
                            }
#if _NSS3300
#else
                            if (!mIN[I.GOOD_RAIL_TRAY_LOADING_CHECK]){
                                if (mIN[I.ULD_CONV_READY]){
                                    if (!RunTrayConveyorUnloading()){
                                        ConfirmUser[W.ProductRemove].msg = "초기화 실패! = GOOD TRAY 레일부 TRAY 제거 실패! (GOOD TRAY 레일부에 트레이 모두 제거 하셔야 합니다.)";
                                        W.ViewWarning(nThread, W.ProductRemove);
                                        B.Bit(nThread, B.InitFail, true, "초기화 진행 중 실패 (TRAY 레일부 트레이 있음)");
                                        break;
                                    }
                                } //배출 콘베어 트레이 받을 준비 되어 있으면 배출
                                else{
                                    ConfirmUser[W.ProductRemove].msg = "초기화 실패! = GOOD TRAY 레일부 TRAY 제거 실패! (GOOD TRAY 레일부에 트레이 모두 제거 하셔야 합니다.)";
                                    W.ViewWarning(nThread, W.ProductRemove);
                                    B.Bit(nThread, B.InitFail, true, "초기화 진행 중 실패 (TRAY 레일부 트레이 있음)");
                                    break;
                                } //
                            } // GOOD TRAY LOADING 위치에 트레이 감지하고 있으면 무조건
#endif
                        }
                        else{

#if _NSS3300
                            if (mIN[I.GOOD_RAIL_STACKER_CHECK]){
                                if (eRTN.SUCESS != BASE.GoodTrayStackerUp(nThread, "GOOD TRAY 스태커 테이블 업")){
                                    ConfirmUser[W.ProductRemove].msg = "초기화 실패! = GOOD TRAY 스태커 테이블 업 실패!";
                                    W.ViewWarning(nThread, W.ProductRemove);
                                    IsBIT[B.InitFail] = true;
                                    break;
                                }
                                if (eRTN.SUCESS != BASE.GoodTrayStackerDown(nThread, "GOOD TRAY 스태커 테이블 다운")){
                                    ConfirmUser[W.ProductRemove].msg = "초기화 실패! = GOOD TRAY 스태커 테이블 다운 실패!";
                                    W.ViewWarning(nThread, W.ProductRemove);
                                    IsBIT[B.InitFail] = true;
                                    break;
                                }
                            }//스택커에 트레이 유무 확인 
#else
                            if (!mIN[I.GOOD_RAIL_STACKER_CHECK]){
                                if (eRTN.SUCESS != BASE.GoodTrayStackerUp(nThread, "GOOD TRAY 스태커 테이블 업")){
                                    ConfirmUser[W.ProductRemove].msg = "초기화 실패! = GOOD TRAY 스태커 테이블 업 실패!";
                                    W.ViewWarning(nThread, W.ProductRemove);
                                    B.Bit(nThread, B.InitFail, true, "초기화 진행 중 실패 (GOOD 트레이 스태커 테이블 업 실패)");
                                    break;
                                }
                                if (eRTN.SUCESS != BASE.GoodTrayStackerDown(nThread, "GOOD TRAY 스태커 테이블 다운")){
                                    ConfirmUser[W.ProductRemove].msg = "초기화 실패! = GOOD TRAY 스태커 테이블 다운 실패!";
                                    W.ViewWarning(nThread, W.ProductRemove);
                                    B.Bit(nThread, B.InitFail, true, "초기화 진행 중 실패 (GOOD 트레이 스태커 테이블 다운 실패)");
                                    break;
                                }
                            }//스택커에 트레이 유무 확인 
                            if (!mIN[I.GOOD_RAIL_TRAY_LOADING_CHECK] || !mIN[I.GOOD_RAIL_HEAD1_CHECK] || !mIN[I.GOOD_RAIL_HEAD2_CHECK]){
                                ConfirmUser[W.ProductRemove].msg = "초기화 실패! = GOOD TRAY 레일부 TRAY 제거 실패! (GOOD TRAY 레일부에 트레이 모두 제거 하셔야 합니다.)";
                                W.ViewWarning(nThread, W.ProductRemove);
                                B.Bit(nThread, B.InitFail, true, "초기화 진행 중 실패 (레일부 트레이 제거 실패)");
                                break;
                            }
#endif
                        }

                        if (mIN[I.ELV_MZ_EXIST1] || mIN[I.ELV_MZ_EXIST2]){
                            if (eRTN.SUCESS != C.Magazine.OutCassate("매거진 배출")){
                                ConfirmUser[W.ProductRemove].msg = "초기화 실패! = 매거진 배출 실패";
                                W.ViewWarning(nThread, W.ProductRemove);
                                B.Bit(nThread, B.InitFail, true, "초기화 진행 중 실패 (매거진 배출 실패)");
                                break;
                            }
                        }
                        bInitialComplete = true;
                        break;

#endregion

#region >> Handler
                    case ManualNumber.LDConv_FWD:
#if _NSS3300
                        C.Magazine.CwConveyor("MANUAL - LOADING CONVEYOR CASSETTE CW");
#else
                        if (mOUT[O.LD_CONV_STOP])   C.Magazine.Conveyor(eConv.FWD);
                        else                        C.Magazine.Conveyor(eConv.STOP);
#endif

                        break;
                    case ManualNumber.LDConv_BWD:
#if _NSS3300
                        C.Magazine.CcwConveyor("MANUAL - LOADING CONVEYOR CASSETTE CCW");
#else
                        if (mOUT[O.LD_CONV_STOP])   C.Magazine.Conveyor(eConv.BWD);
                        else                        C.Magazine.Conveyor(eConv.STOP);
#endif
                        break;

                    case ManualNumber.MGZClamp:
                        if (mOUT[O.ELV_CLAMP]) {
                            if (DATA_.iMANUAL.bool_1){
                                UTIL_.DELAY(DATA_.iMANUAL.int_1);
                            }
                            C.Magazine.UnClamp("매거진 언클램프"); 
                        }
                        else C.Magazine.Clamp("매거진 클램프");
                        break;
                    case ManualNumber.Pusher:
                        if (mIN[I.PUSHER_FWD])      C.Magazine.PusherBackward("푸셔 후진");
                        else                        C.Magazine.PusherForward("푸셔 전진");
                        break;
                    case ManualNumber.InLetTable:
                        if (mIN[I.INLET_TABLE_UP]) C.Gripper.InLET_DOWN("인-렛 테이블 다운");
                        else C.Gripper.InLET_UP("인-렛 테이블 업");
                        break;
                    case ManualNumber.InLet_VAC:
#if _NSS3300
#else
                        if (mOUT[O.INLET_TABLE_VAC]) C.Gripper.InLetTableVac(stBIT.OFF);
                        else C.Gripper.InLetTableVac(stBIT.ON);
#endif
                        break;
                    case ManualNumber.InLet_Blow:
                        C.Gripper.InletTableBlow();
                        break;
                    case ManualNumber.Gripper:
                        if (mIN[I.GRIPPER_OPEN]) C.Gripper.Grip("그리퍼 그립");
                        else C.Gripper.UnGrip("그리퍼 언-그립");
                        break;

                    case ManualNumber.StripVac:
                        if (mOUT[O.STRIP_PK_VAC]) C.StripPk.Vac(stBIT.OFF);
                        else C.StripPk.Vac(stBIT.ON);
                        break;
                    case ManualNumber.StripBlow:
                        C.StripPk.Blow();
                        break;
                    case ManualNumber.UnitVac:
                        if (mOUT[O.UNIT_PK_VAC]) C.UnitPk.Vac(stBIT.OFF);
                        else C.UnitPk.Vac(stBIT.ON);
                        break;
                    case ManualNumber.UnitBlow:
                        C.UnitPk.Blow();
                        break;
                    case ManualNumber.ScrapVac:
                        if (mOUT[O.SCRAP_VAC_1] || mOUT[O.SCRAP_VAC_2]) C.UnitPk.ScrapVac((eSCRAP)iMANUAL.int_1, stBIT.OFF);
                        else C.UnitPk.ScrapVac((eSCRAP)iMANUAL.int_1, stBIT.ON);
                        break;
                    case ManualNumber.ScrapBlow:
                        C.UnitPk.ScrapBlow((eSCRAP)iMANUAL.int_1);
                        break;
                    case ManualNumber.CleanerWater:
                        if (mOUT[O.CLEANER_WATER_1] || mOUT[O.CLEANER_WATER_2]) C.UnitPk.CleanerWater(stBIT.ON);
                        else C.UnitPk.CleanerWater(stBIT.OFF);
                        break;
                    case ManualNumber.CleanerAir:
#if _NSS3300
                        if (mOUT[O.CLEANER_AIR]){
#else
                        if (mOUT[O.CLEANER_AIR_1] || mOUT[O.CLEANER_AIR_2]){
#endif
                            C.UnitPk.CleanerAir(false);
                        }
                        else C.UnitPk.CleanerAir(true);
                        break;
                    case ManualNumber.CleanerSwing:
                        if (mIN[I.CLEANER_SWING_LEFT]) C.UnitPk.CleanerSwing_Left("클리너 스윙 오른쪽");
                        else C.UnitPk.CleanerSwing_Right("클리너 스윙 왼쪽");
                        break;
                    case ManualNumber.UnitPkrAirShower:
                        if (mOUT[O.CLEANER_AIR_KNIFE]) C.UnitPk.UnitAirshower(stBIT.OFF);
                        else C.UnitPk.UnitAirshower(stBIT.ON);
                        break;

                    case ManualNumber.MGZRdy:
                        //YZ축 위치 확인 필요
                        if (eRTN.SUCESS != C.Magazine.MoveY(P.Ready, "", "ELV' Y축 대기 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.Magazine.MoveZ(P.Ready, "", "ELV' Z축 대기 위치 이송");
                        break;
                    case ManualNumber.MGZLoading:
                        //YZ축 위치 확인 필요 //매거진 있을 경우 매거진 로딩 위치로 못가게
                        if (eRTN.SUCESS != C.Magazine.MoveY(P.Ready, "", "ELV' Y축 대기 위치 이송")) break;
                        if (eRTN.SUCESS != C.Magazine.MoveZ(P.Recive, "", "ELV' Z축 매거진 로딩 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.Magazine.MoveY(P.Recive, "", "ELV' Y축 매거진 로딩 위치 이송");
                        break;
                    case ManualNumber.MGZUnloading:
                        //YZ축 위치 확인 필요
                        if (eRTN.SUCESS != C.Magazine.MoveY(P.Ready, "", "ELV' Y축 대기 위치 이송")) break;
                        if (eRTN.SUCESS != C.Magazine.MoveZ(P.Give, "", "ELV' Z축 매거진 언로딩 위치 이송")) break;
                        if (iMANUAL.Option){
                            if (eRTN.SUCESS != C.Magazine.MoveY(P.Give, "", "ELV' Y축 매거진 언로딩 위치 이송")) break;
                            if (eRTN.SUCESS != C.Magazine.UnClamp("")) break;
                            iMANUAL.CMD = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.ElvULDUpDownPitch]);
                            C.Magazine.MoveZ(P.Give, iMANUAL.CMD, "ELV' Z축 매거진 언로딩 위치 이송");
                        }
                        break;
                    case ManualNumber.MGZSlot:
                        //XY축 위치 확인 필요.
                        if (eRTN.SUCESS != C.Magazine.MoveY(P.FirstSlot, "", "ELV' Y축 스트립 로딩 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.Magazine.MoveMGZSlot(iMANUAL.int_1, "", "매거진 슬롯 " + (iMANUAL.int_1 + 1).ToString() + "번 위치 이송");
                        break;

                    case ManualNumber.RailRdy:
                        C.Gripper.MoveRail(P.Ready, "", "레일 대기 위치 이송");
                        break;
                    case ManualNumber.RailLoading:
                        C.Gripper.MoveRail(P.StripIn, "", "매거진 스트립 투입 레일 위치 이송");
                        break;
                    case ManualNumber.RailWork:
                        C.Gripper.MoveRail(P.StripAlign, "", "스트립 픽업시 레일 위치 이송");
                        break;
                    case ManualNumber.RailPicOpen:
                        C.Gripper.MoveRail(P.Open, "", "스트립 픽업 중 레일 오픈 위치 이송");
                        break;

                    case ManualNumber.GripperRdy:
                        C.Gripper.MoveX(P.Ready, "", "그리퍼 X축 대기 위치 이송");
                        break;
                    case ManualNumber.GripperCatch:
                        C.Gripper.MoveX(P.StripPick, "", "그리퍼 X축 매거진 투입 스트립 잡는 위치 이송");
                        break;
                    case ManualNumber.GripperCatchCheck:
                        iMANUAL.CMD = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.GripperBackPitch]);
                        C.Gripper.MoveX(P.StripPick, iMANUAL.CMD, "그리퍼 X축 스트립 캐치 확인 위치 이송");
                        break;
                    case ManualNumber.GripperBarcodeReading:
                        C.Gripper.MoveX(P.StripBcd, "", "그리퍼 X축 매거진 투입 스트립 잡는 위치 이송");
                        break;
                    case ManualNumber.GripperLoading:
                        C.Gripper.MoveX(P.StripOpn, "", "그리퍼 X축 스트립 레일에 로딩 하는 위치 이송");
                        break;
                    case ManualNumber.GripperStripPic:
                        C.Gripper.MoveX(P.StripLoad, "", "그리퍼 X축 스트립 픽업 정렬 위치 이송");
                        break;

                    case ManualNumber.BarcodeRdy:
                        C.Gripper.MoveBarcode(P.Ready, "", "바코드 Y축 대기 위치 이송");
                        break;
                    case ManualNumber.BarcodeReading:
                        C.Gripper.MoveBarcode(P.BcdRead, "", "바코드 Y축 스트립 바코드 리딩 위치 이송");
                        break;

                    case ManualNumber.StripPkUp:
                        C.StripPk.MoveZ(P.Ready, "", "스트립 피커 Z축 대기 위치 이송");
                        break;
                    case ManualNumber.StripPkDn:
                        if (mtDATA[M.StripPkX, P.StripPckUp].bPOS)
                            C.StripPk.MoveZ(P.StripPckUp, "", "스트립 피커 Z축 스트립 픽업 위치 이송");
                        if (mtDATA[M.StripPkX, P.StripPlc].bPOS)
                            C.StripPk.MoveZ(P.StripPlc, "", "스트립 피커 Z축 스트립 플레이스 위치 이송");
                        break;
                    case ManualNumber.StripPkRdy:
                        if (eRTN.SUCESS != C.StripPk.MoveZ(P.Ready, "", "스트립 피커 Z축 대기 위치 이송")) break;
                        C.StripPk.MoveX(P.Ready, "", "스트립 피커 X축 대기 위치 이송");
                        break;
                    case ManualNumber.StripPkPic:
                        if ((mtDATA[M.StripPkZ, P.Ready].Pos + 5) < mtSTS[M.StripPkZ].CurrentPosition){
                            if (eRTN.SUCESS != C.StripPk.MoveZ(P.Ready, "", "스트립 피커 Z축 대기 위치 이송")) break;
                        }
                        if (eRTN.SUCESS != C.StripPk.MoveX(P.StripPckUp, "", "스트립 피커 X축 스트립 픽업 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.StripPk.MoveZ(P.StripPckUp, "", "스트립 피커 Z축 스트립 픽업 위치 이송");
                        break;
                    case ManualNumber.StripPkPlc:
                        if ((mtDATA[M.StripPkZ, P.Ready].Pos + 5) < mtSTS[M.StripPkZ].CurrentPosition){
                            if (eRTN.SUCESS != C.StripPk.MoveZ(P.Ready, "", "스트립 피커 Z축 대기 위치 이송")) break;
                        }
                        if (eRTN.SUCESS != C.StripPk.MoveX(P.StripPlc, "", "스트립 피커 X축 스트립 플레이스 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.StripPk.MoveZ(P.StripPlc, "", "스트립 피커 Z축 스트립 플레이스 위치 이송");
                        break;
                    case ManualNumber.StripPkFristAlign:
                        if ((mtDATA[M.StripPkZ, P.Ready].Pos + 5) < mtSTS[M.StripPkZ].CurrentPosition){
                            if (eRTN.SUCESS != C.StripPk.MoveZ(P.Ready, "", "스트립 피커 Z축 대기 위치 이송")) break;
                        }
                        if (eRTN.SUCESS != C.StripPk.MoveX(P.FirstTrigger, "", "스트립 피커 X축 스트립 얼라인 첫번째 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.StripPk.MoveZ(P.FirstTrigger, "", "스트립 피커 Z축 스트립 얼라인 첫번째 위치 이송");
                        break;
                    case ManualNumber.StripPkSecondAlign:
                        if ((mtDATA[M.StripPkZ, P.Ready].Pos + 5) < mtSTS[M.StripPkZ].CurrentPosition){
                            if (eRTN.SUCESS != C.StripPk.MoveZ(P.Ready, "", "스트립 피커 Z축 대기 위치 이송")) break;
                        }
                        if (eRTN.SUCESS != C.StripPk.MoveX(P.SecondTrigger, "", "스트립 피커 X축 스트립 얼라인 두번째 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.StripPk.MoveZ(P.SecondTrigger, "", "스트립 피커 Z축 스트립 얼라인 두번째 위치 이송");
                        break;

                    case ManualNumber.PreAlignRdy:
                        C.StripPk.MovePreAlignY(P.Ready, "", "프리얼라인 Y축 대기 위치 이송");
                        break;
                    case ManualNumber.PreAlignFirst:
                        C.StripPk.MovePreAlignY(P.StripTrigger1, "", "프리얼라인 Y축 스트립 첫번째 얼라인 위치 이송");
                        break;
                    case ManualNumber.PreAlignSecond:
                        C.StripPk.MovePreAlignY(P.StripTrigger2, "", "프리얼라인 Y축 스트립 두번째 얼라인 위치 이송");
                        break;

                    case ManualNumber.UnitPkUp:
                        C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송");
                        break;
                    case ManualNumber.UnitPkDn:
                        if (mtDATA[M.UnitPkX, P.UnitPckUp].bPOS)
                            C.UnitPk.MoveZ(P.UnitPckUp, "", "유닛 피커 Z축 유닛 픽업 위치 이송");
                        if (mtDATA[M.UnitPkX, P.Cleaner].bPOS)
                            C.UnitPk.MoveZ(P.Cleaner, "", "유닛 피커 Z축 하부 클리닝 위치 이송");
                        if (mtDATA[M.UnitPkX, P.PlacePallet1].bPOS)
                            C.UnitPk.MoveZ(P.PlacePallet1, "", "유닛 피커 Z축 테이블1 플레이스 위치 이송");
                        if (mtDATA[M.UnitPkX, P.PlacePallet2].bPOS)
                            C.UnitPk.MoveZ(P.PlacePallet2, "", "유닛 피커 Z축 테이블2 플레이스 위치 이송");
                        break;
                    case ManualNumber.UnitPkRdy:
                        if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        C.UnitPk.MoveX(P.Ready, "", "유닛 피커 X축 대기 위치 이송");
                        break;
                    case ManualNumber.UnitPkPic:
                        if ((mtDATA[M.UnitPkZ, P.Ready].Pos + 5) < mtSTS[M.UnitPkZ].CurrentPosition){
                            if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        }
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.UnitPckUp, "", "유닛 피커 X축 유닛 픽업 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.UnitPk.MoveZ(P.UnitPckUp, "", "유닛 피커 Z축 유닛 픽업 위치 이송");
                        break;
                    case ManualNumber.UnitPkScrap1:
                        if ((mtDATA[M.UnitPkZ, P.Ready].Pos + 5) < mtSTS[M.UnitPkZ].CurrentPosition){
                            if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        }
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.Scrap1, "", "유닛 피커 X축 첫번째 스크랩 버리는 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.UnitPk.MoveZ(P.Scrap1, "", "유닛 피커 Z축 첫번째 스크랩 버리는 위치 이송");
                        break;
                    case ManualNumber.UnitPkScrap2:
                        if ((mtDATA[M.UnitPkZ, P.Ready].Pos + 5) < mtSTS[M.UnitPkZ].CurrentPosition){
                            if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        }
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.Scrap2, "", "유닛 피커 X축 두번째 스크랩 버리는 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.UnitPk.MoveZ(P.Scrap2, "", "유닛 피커 Z축 두번째 스크랩 버리는 위치 이송");
                        break;
                    case ManualNumber.UnitPkBrush:
                        if ((mtDATA[M.UnitPkZ, P.Ready].Pos + 5) < mtSTS[M.UnitPkZ].CurrentPosition){
                            if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        }
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.BrushStart, "", "유닛 피커 X축 유닛 브러쉬 시작 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.UnitPk.MoveZ(P.BrushStart, "", "유닛 피커 Z축 유닛 브러쉬 시작 위치 이송");
                        break;
                    case ManualNumber.UnitPkCleaner:
                        if ((mtDATA[M.UnitPkZ, P.Ready].Pos + 5) < mtSTS[M.UnitPkZ].CurrentPosition){
                            if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        }
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.Cleaner, "", "유닛 피커 X축 하부 클리닝 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.UnitPk.MoveZ(P.Cleaner, "", "유닛 피커 Z축 하부 클리닝 위치 이송");
                        break;
                    case ManualNumber.UnitPkAirshower:
                        if ((mtDATA[M.UnitPkZ, P.Ready].Pos + 5) < mtSTS[M.UnitPkZ].CurrentPosition){
                            if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        }
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.AirBlowStart, "", "유닛 피커 X축 유닛 에어샤워 시작 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.UnitPk.MoveZ(P.AirBlowStart, "", "유닛 피커 Z축 유닛 에어샤워 시작 위치 이송");
                        break;
                    case ManualNumber.UnitPkStage1:
                        if ((mtDATA[M.UnitPkZ, P.Ready].Pos + 5) < mtSTS[M.UnitPkZ].CurrentPosition){
                            if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        }
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.PlacePallet1, "", "유닛 피커 X축 테이블1 플레이스 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.UnitPk.MoveZ(P.PlacePallet1, "", "유닛 피커 Z축 테이블1 플레이스 위치 이송");
                        break;
                    case ManualNumber.UnitPkStage2:
                        if ((mtDATA[M.UnitPkZ, P.Ready].Pos + 5) < mtSTS[M.UnitPkZ].CurrentPosition){
                            if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        }
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.PlacePallet2, "", "유닛 피커 X축 테이블2 플레이스 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.UnitPk.MoveZ(P.PlacePallet2, "", "유닛 피커 Z축 테이블2 플레이스 위치 이송");
                        break;

                    case ManualNumber.SawStageVac:
                        BASE.SawStageVac(nThread, iMANUAL.bool_1);
                        break;
                    case ManualNumber.SawStageRej:
                        BASE.SawStageBlow(nThread, iMANUAL.bool_1);
                        break;
#endregion

#region >> Sorter
                    case ManualNumber.Trigger:
#if _NSS3300
                        BASE.Trigger(iMANUAL.int_1);
#else
                        LAB_.TriggerOutput(iMANUAL.int_1, uVAL.High);
                        LAB_.ONE_SHOT(iMANUAL.int_1);
                        LAB_.TriggerOutput(iMANUAL.int_1, uVAL.Low);
                        UTIL_.DELAY((int)prMACHINE[CP.TriggerEnd]);
#endif
                        break;

                    case ManualNumber.StageAirshower:
                        if (mOUT[O.StageAirshowr[iMANUAL.int_1]]) BASE.StageAirshower((eMAP_BLOCK)iMANUAL.int_1, stBIT.OFF);
                        else BASE.StageAirshower((eMAP_BLOCK)iMANUAL.int_1, stBIT.ON);
                        break;
                    case ManualNumber.StageVac:
                        if (mOUT[O.StageVac[iMANUAL.int_1]]) BASE.StageVac((eMAP_BLOCK)iMANUAL.int_1, stBIT.OFF);
                        else BASE.StageVac((eMAP_BLOCK)iMANUAL.int_1, stBIT.ON);
                        break;
                    case ManualNumber.TopCamBlow:
                        mOUT[O.TOP_VISION_BLOW] = !mOUT[O.TOP_VISION_BLOW];
                        break;
                    case ManualNumber.BtmCamCalZig:
                        if (mIN[I.CAM_CAL_ZIG_FWD]) BASE.BottomCameraCalibrationZig_Bwd(nThread, "카메라 CAL' 지그 후진");
                        else BASE.BottomCameraCalibrationZig_Fwd(nThread, "카메라 CAL' 지그 전진");
                        break;

                    case ManualNumber.PkrVac:
                        BASE.PkVac((eHD)DATA_.iMANUAL.int_1, (ePK)DATA_.iMANUAL.int_2, stBIT.ON, true, "");
                        break;
                    case ManualNumber.PkrBlow:
                        BASE.PkBlow((eHD)DATA_.iMANUAL.int_1, (ePK)DATA_.iMANUAL.int_2, true);
                        break;
                    case ManualNumber.PkrFree:
                        BASE.PkFree((eHD)DATA_.iMANUAL.int_1, (ePK)DATA_.iMANUAL.int_2);
                        break;


                    case ManualNumber.GoodTray1_Clamp:
                        if (iMANUAL.bool_1){
                            if (mIN[I.GOOD_TRAY1_UNGRIP_C] || mIN[I.GOOD_TRAY1_UNGRIP_S]) BASE.GoodTrayFeederGrip(nThread, eTRAY.GOOD1, "GOOD TRAY TRANSFER1 TRAY GRIP");
                            else BASE.GoodTrayFeederUnGrip(nThread, eTRAY.GOOD1, "GOOD TRAY TRANSFER1 TRAY UNGRIP");
                        }
                        else{
                            if (iMANUAL.bool_2){
                                if (mIN[I.GOOD_TRAY1_UNGRIP_C]) BASE.GoodTrayFeederFrontGrip(nThread, eTRAY.GOOD1, "GOOD TRAY1 FROTN GRIP");
                                else BASE.GoodTrayFeederFrontUnGrip(nThread, eTRAY.GOOD1, "GOOD TRAY1 FRONT UNGRIP");
                            }
                            else{
                                if (mIN[I.GOOD_TRAY1_UNGRIP_S]) BASE.GoodTrayFeederRearGrip(nThread, eTRAY.GOOD1, "GOOD TRAY1 REAR GRIP");
                                else BASE.GoodTrayFeederRearUnGrip(nThread, eTRAY.GOOD1, "GOOD TRAY1 REAR UNGRIP");
                            }
                        }
                        break;
                    case ManualNumber.GoodTray2_Clamp:
                        if (iMANUAL.bool_1){
                            if (mIN[I.GOOD_TRAY2_UNGRIP_C] || mIN[I.GOOD_TRAY2_UNGRIP_S]) BASE.GoodTrayFeederGrip(nThread, eTRAY.GOOD2, "GOOD TRAY TRANSFER2 TRAY GRIP");
                            else BASE.GoodTrayFeederUnGrip(nThread, eTRAY.GOOD2, "GOOD TRAY TRANSFER2 TRAY UNGRIP");
                        }
                        else{
                            if (iMANUAL.bool_2){
                                if (mIN[I.GOOD_TRAY2_UNGRIP_C]) BASE.GoodTrayFeederFrontGrip(nThread, eTRAY.GOOD2, "GOOD TRAY2 FROTN GRIP");
                                else BASE.GoodTrayFeederFrontUnGrip(nThread, eTRAY.GOOD2, "GOOD TRAY2 FRONT UNGRIP");
                            }
                            else{
                                if (mIN[I.GOOD_TRAY2_UNGRIP_S]) BASE.GoodTrayFeederRearGrip(nThread, eTRAY.GOOD2, "GOOD TRAY2 REAR GRIP");
                                else BASE.GoodTrayFeederRearUnGrip(nThread, eTRAY.GOOD2, "GOOD TRAY2 REAR UNGRIP");
                            }
                        }
                        break;
                    case ManualNumber.GoodTrayStackerTable:
                        if (mIN[I.GOOD_STACKER_UP]) BASE.GoodTrayStackerDown(nThread, "GOOD TRAY STACKER DOWN");
                        else BASE.GoodTrayStackerUp(nThread, "GOOD TRAY STACKER UP");
                        break;
                    case ManualNumber.GoodTrayPreAlign:
                        if (mIN[I.GOOD_TRAY_PRE_ALIGN_FWD]) BASE.GoodTrayPreAlignBwd(nThread, "GOOD TRAY PRE-ALIGN BACKWARD");
                        else BASE.GoodTrayPreAlignFwd(nThread, "GOOD TRAY PRE-ALIGN FORWARD");
                        break;

                    case ManualNumber.ReworkTray_Clamp:
                        if (!mIN[I.NG_TRAY_UNGRIP1] || !mIN[I.NG_TRAY_UNGRIP2]) C.ReworkTrayFeeder.UnGrip("REWORK TRAY UNGRIP");
                        else C.ReworkTrayFeeder.Grip("REWORK TRAY GRIP");
                        break;
                    case ManualNumber.ReworkTrayStackerTable:
                        if (mIN[I.NG_STACKER_UP]) C.ReworkTrayFeeder.StackerTableDn("REWORK STACKER DOWN");
                        else C.ReworkTrayFeeder.StackerTableUp("REWORK STACKER UP");
                        break;

                    case ManualNumber.TrayPkClamp:
                        if (mOUT[O.TRAY_PK_GRIP]) C.TrayPk.UnClamp("TRAY PICKER UNCLAMP");
                        else C.TrayPk.Clamp("TRAY PICKER CLAMP");
                        break;

                    case ManualNumber.EmptyStackerStopper:
                        if (mIN[I.EMPTY_STACKER_LOCK1] || mIN[I.EMPTY_STACKER_LOCK2] || mIN[I.EMPTY_STACKER_LOCK3] || mIN[I.EMPTY_STACKER_LOCK4]) C.EmptyStacker.StopperUnlock("EMPTY STACKER STOPPER UNLOCK");
                        else C.EmptyStacker.StopperLock("EMPTY STACKER STOPPER LOCK");
                        break;
                    case ManualNumber.EmptyTrayClamp:
                        if (mIN[I.EMPTY_TRAY_UNGRIP1] || mIN[I.EMPTY_TRAY_UNGRIP2]) C.EmptyStacker.TransferGrip("EMPTY TRAY FEEDER GRIP");
                        else C.EmptyStacker.TransferUnGrip("EMPTY TRAY FEEDER UNGRIP");
                        break;
                    case ManualNumber.EmptyTrayFeeder:
                        if (mIN[I.EMPTY_FEEDER_FWD]) C.EmptyStacker.TransferBwd("EMTPY TRAY FEEDER BACKWARD");
                        else C.EmptyStacker.TransferFwd("EMPTY TRAY FEEDER FORWARD");
                        break;

                    case ManualNumber.StageRdy:
                        BASE.MoveStageY(nThread, (eMAP_BLOCK)iMANUAL.int_2, P.Ready, "", MtName[iMANUAL.iMT1] + " 대기 위치 이송");
                        break;
                    case ManualNumber.StageReceive:
                        BASE.MoveStageY(nThread, (eMAP_BLOCK)iMANUAL.int_2, P.RecieveUnit, "", MtName[iMANUAL.iMT1] + " 유닛 피커 플레이스 위치 이송");
                        break;
                    case ManualNumber.StageAirshowerStart:
                        iMANUAL.int_1 = iMANUAL.iMT1 == M.Table1 ? (int)eMAP_BLOCK.STAGE1 : (int)eMAP_BLOCK.STAGE2;
                        BASE.MoveStageY(nThread, (eMAP_BLOCK)iMANUAL.int_1, P.AirShowerStart, "", MtName[iMANUAL.iMT1] + " 에어샤워 시작 위치 이송");
                        break;
                    case ManualNumber.StageTopCamView:
                        iMANUAL.int_1 = iMANUAL.iMT1 == M.Table1 ? (int)eMAP_BLOCK.STAGE1 : (int)eMAP_BLOCK.STAGE2;
                        BASE.MoveStageY(nThread, (eMAP_BLOCK)iMANUAL.int_1, P.TopVision_Unit, "", MtName[iMANUAL.iMT1] + " 상부 카메라 좌하단 포켓 위치 이송");
                        break;
                    case ManualNumber.StageHDCam1View:
                        iMANUAL.int_1 = iMANUAL.iMT1 == M.Table1 ? (int)eMAP_BLOCK.STAGE1 : (int)eMAP_BLOCK.STAGE2;
                        BASE.MoveStageY(nThread, (eMAP_BLOCK)iMANUAL.int_1, P.HD1_Unit, "", MtName[iMANUAL.iMT1] + " 헤드1 카메라 좌하단 포켓 위치 이송");
                        break;
                    case ManualNumber.StageHDCam2View:
                        iMANUAL.int_1 = iMANUAL.iMT1 == M.Table1 ? (int)eMAP_BLOCK.STAGE1 : (int)eMAP_BLOCK.STAGE2;
                        BASE.MoveStageY(nThread, (eMAP_BLOCK)iMANUAL.int_1, P.HD2_Unit, "", MtName[iMANUAL.iMT1] + " 헤드2 카메라 좌하단 포켓 위치 이송");
                        break;

                    case ManualNumber.TopCamStaeView:
                        BASE.MoveInpectionPos(nThread, (eMAP_BLOCK)iMANUAL.int_2, iMANUAL.int_3, iMANUAL.int_4, iMANUAL.int_5, iMANUAL.int_6, iMANUAL.bool_1, stBIT.NotUnitOffset, ((eMAP_BLOCK)iMANUAL.int_2).ToString() + "[" + iMANUAL.int_3 + "/" + iMANUAL.int_4 + "/" + iMANUAL.int_5 + "/" + iMANUAL.int_6 + "] TOP CAM VIEW LOCATION");
                        break;
                    case ManualNumber.StageHDCamView:
                        BASE.MoveXUnitPic(nThread, (eHD)iMANUAL.int_1, (eMAP_BLOCK)iMANUAL.int_2, iMANUAL.int_3, iMANUAL.int_4, iMANUAL.int_5, iMANUAL.int_6, iMANUAL.int_7, iMANUAL.bool_1, stBIT.NotUnitOffset, "", ((eHD)iMANUAL.int_1).ToString() + ((eMAP_BLOCK)iMANUAL.int_2).ToString() + "[" + iMANUAL.int_4 + "/" + iMANUAL.int_5 + "/" + iMANUAL.int_6 + "/" + iMANUAL.int_7 + "] 위치 이송");
                        break;
                    case ManualNumber.TrayHDCamView:
                        BASE.MoveXPlc(nThread, (eHD)iMANUAL.int_1, iMANUAL.int_2, (eTRAY)iMANUAL.int_3, iMANUAL.int_4, iMANUAL.int_5, iMANUAL.bool_1, "");
                        break;
                    case ManualNumber.StageZigView:
                        if (eRTN.SUCESS != BASE.MoveAllPkRdy(nThread, "", "모든 피커 대기 위치 이송")) break;
                        BASE.MoveStageZigPos(nThread, (eHD)iMANUAL.int_1, iMANUAL.int_2, iMANUAL.bool_1, ((eHD)iMANUAL.int_1).ToString() + "/" + ((eMAP_BLOCK)prMACHINE[CP.ZigAttachStage]).ToString() + " " + iMANUAL.int_2 + 1 + " 위치 이송");
                        break;

                    case ManualNumber.TopCamRdy:
                        BASE.MoveTopCam(nThread, P.Ready, "", "상부비전 대기 위치 이송");
                        break;
                    case ManualNumber.TopCamStage1View:
                        BASE.MoveTopCam(nThread, P.Pallet1_Unit, "", "상부비전 테이블1 좌하단 포켓 위치 이송");
                        break;
                    case ManualNumber.TopCamStage2View:
                        BASE.MoveTopCam(nThread, P.Pallet2_Unit, "", "상부비전 테이블2 좌하단 포켓 위치 이송");
                        break;

                    case ManualNumber.BtmCamRdy:
                        BASE.MoveBtmY(nThread, P.Ready, "", "하부비전 대기 위치 이송");
                        break;
                    case ManualNumber.BtmCamHD1CamCenter:
                        BASE.MoveBtmY(nThread, P.HD1CamCenter, "", "하부비전 헤드1 카메라 중심 위치 이송");
                        break;
                    case ManualNumber.BtmCamHD1PkCenter:
                        BASE.MoveBtmY(nThread, P.HD1PkCenter, "", "하부비전 헤드1 피커1 중심 위치 이송");
                        break;
                    case ManualNumber.BtmCamHD2CamCenter:
                        BASE.MoveBtmY(nThread, P.HD2CamCenter, "", "하부비전 헤드2 카메라 중심 위치 이송");
                        break;
                    case ManualNumber.BtmCamHD2PkCenter:
                        BASE.MoveBtmY(nThread, P.HD2PkCenter, "", "하부비전 헤드2 피커1 위치 이송");
                        break;

                    case ManualNumber.HDXRdy:
                        iMANUAL.int_1 = iMANUAL.iMT1 == M.TRIGGER1 ? (int)eHD.HD1 : (int)eHD.HD2;
                        if (eRTN.SUCESS != BASE.MoveHDPkRdy(nThread, (eHD)iMANUAL.int_1, "", ((eHD)iMANUAL.int_1).ToString() + " 피커 대기 위치 이송")) break;
                        BASE.MoveX(nThread, (eHD)iMANUAL.int_1, P.Ready, "", ((eHD)iMANUAL.int_1).ToString() + " X축 대기 위치 이송");
                        break;
                    case ManualNumber.HDXCamCenter:
                        iMANUAL.int_1 = iMANUAL.iMT1 == M.TRIGGER1 ? (int)eHD.HD1 : (int)eHD.HD2;
                        if (eRTN.SUCESS != BASE.MoveHDPkRdy(nThread, (eHD)iMANUAL.int_1, "", ((eHD)iMANUAL.int_1).ToString() + " 피커 대기 위치 이송")) break;
                        BASE.MoveX(nThread, (eHD)iMANUAL.int_1, P.BTMCamCenter, "", ((eHD)iMANUAL.int_1).ToString() + " X축 카메라 하부 카메라 중심 위치 이송");
                        break;
                    case ManualNumber.HDXPkCenter:
                        iMANUAL.int_1 = iMANUAL.iMT1 == M.TRIGGER1 ? (int)eHD.HD1 : (int)eHD.HD2;
                        if (eRTN.SUCESS != BASE.MoveHDPkRdy(nThread, (eHD)iMANUAL.int_1, "", ((eHD)iMANUAL.int_1).ToString() + " 피커 대기 위치 이송")) break;
                        BASE.MoveX(nThread, (eHD)iMANUAL.int_1, P.PkCenter, "", ((eHD)iMANUAL.int_1).ToString() + " X축 카메라 하부 카메라 중심 위치 이송");
                        break;
                    case ManualNumber.HDXReject:
                        iMANUAL.int_1 = iMANUAL.iMT1 == M.TRIGGER1 ? (int)eHD.HD1 : (int)eHD.HD2;
                        if (eRTN.SUCESS != BASE.MoveHDPkRdy(nThread, (eHD)iMANUAL.int_1, "", ((eHD)iMANUAL.int_1).ToString() + " 피커 대기 위치 이송")) break;
                        BASE.MoveX(nThread, (eHD)iMANUAL.int_1, P.Reject, "", ((eHD)iMANUAL.int_1).ToString() + " X축 유닛 버리는 위치 이송");
                        break;
                    case ManualNumber.HDXStage1View:
                        iMANUAL.int_1 = iMANUAL.iMT1 == M.TRIGGER1 ? (int)eHD.HD1 : (int)eHD.HD2;
                        if (eRTN.SUCESS != BASE.MoveHDPkRdy(nThread, (eHD)iMANUAL.int_1, "", ((eHD)iMANUAL.int_1).ToString() + " 피커 대기 위치 이송")) break;
                        BASE.MoveX(nThread, (eHD)iMANUAL.int_1, P.Pallet1, "", ((eHD)iMANUAL.int_1).ToString() + " X축 유닛 버리는 위치 이송");
                        break;
                    case ManualNumber.HDXStage2View:
                        iMANUAL.int_1 = iMANUAL.iMT1 == M.TRIGGER1 ? (int)eHD.HD1 : (int)eHD.HD2;
                        if (eRTN.SUCESS != BASE.MoveHDPkRdy(nThread, (eHD)iMANUAL.int_1, "", ((eHD)iMANUAL.int_1).ToString() + " 피커 대기 위치 이송")) break;
                        BASE.MoveX(nThread, (eHD)iMANUAL.int_1, P.Pallet2, "", ((eHD)iMANUAL.int_1).ToString() + " X축 유닛 버리는 위치 이송");
                        break;
                    case ManualNumber.HDXGoodTray1View:
                        iMANUAL.int_1 = iMANUAL.iMT1 == M.TRIGGER1 ? (int)eHD.HD1 : (int)eHD.HD2;
                        if (eRTN.SUCESS != BASE.MoveHDPkRdy(nThread, (eHD)iMANUAL.int_1, "", ((eHD)iMANUAL.int_1).ToString() + " 피커 대기 위치 이송")) break;
                        BASE.MoveX(nThread, (eHD)iMANUAL.int_1, P.Feeder1, "", ((eHD)iMANUAL.int_1).ToString() + " X축 테이블1번 좌하부 포켓 중심 위치 이송");
                        break;
                    case ManualNumber.HDXGoodTray2View:
                        iMANUAL.int_1 = iMANUAL.iMT1 == M.TRIGGER1 ? (int)eHD.HD1 : (int)eHD.HD2;
                        if (eRTN.SUCESS != BASE.MoveHDPkRdy(nThread, (eHD)iMANUAL.int_1, "", ((eHD)iMANUAL.int_1).ToString() + " 피커 대기 위치 이송")) break;
                        BASE.MoveX(nThread, (eHD)iMANUAL.int_1, P.Feeder2, "", ((eHD)iMANUAL.int_1).ToString() + " X축 테이블2번 좌하부 포켓 중심 위치 이송");
                        break;
                    case ManualNumber.HDXReworkTrayView:
                        iMANUAL.int_1 = iMANUAL.iMT1 == M.TRIGGER1 ? (int)eHD.HD1 : (int)eHD.HD2;
                        if (eRTN.SUCESS != BASE.MoveHDPkRdy(nThread, (eHD)iMANUAL.int_1, "", ((eHD)iMANUAL.int_1).ToString() + " 피커 대기 위치 이송")) break;
                        BASE.MoveX(nThread, (eHD)iMANUAL.int_1, P.Feeder3, "", ((eHD)iMANUAL.int_1).ToString() + " X축 테이블2번 좌하부 포켓 중심 위치 이송");
                        break;

                    case ManualNumber.HDCamCenter:
                        BASE.MoveHDCamCenter(nThread, (eHD)iMANUAL.int_1, "", ((eHD)iMANUAL.int_1).ToString() + " 카메라 중심 위치 이송");
                        break;
                    case ManualNumber.HDPkCenter:
                        //DATA_.iMANUAL.bool_1 T:PK CAL Z / F:UNIT INSPECTION Z
                        BASE.MoveHDPkCenter(nThread, (eHD)iMANUAL.int_1, iMANUAL.int_2, iMANUAL.bool_1, "", ((eHD)iMANUAL.int_1).ToString() + " 피커 중심 위치 이송");
                        if (prMACHINE[CP.UseFlaying] != (int)InspectionMode.Flying && !iMANUAL.bool_1) {
                            int pkZNum;
                            double pkZOffset;
                            string pkZCmd;
                            if (eHD.HD1 == (eHD)iMANUAL.int_1) {
                                pkZNum = M.HD1Pk[iMANUAL.int_2];
                                pkZOffset = prMODEL[RP.HD1_PRS_OFFSET[iMANUAL.int_2]];
                                pkZCmd = pkZOffset == 0 ? "" : "offset=" + pkZOffset.ToString();
                            }
                            else {
                                pkZNum = M.HD2Pk[iMANUAL.int_2];
                                pkZOffset = prMODEL[RP.HD2_PRS_OFFSET[iMANUAL.int_2]];
                                pkZCmd = pkZOffset == 0 ? "" : "offset=" + pkZOffset.ToString();
                            }
                            BASE.MovePk(nThread, pkZNum, P.Ready, pkZCmd, "PRS Z OFFSET - " + ((eHD)iMANUAL.int_1).ToString() + " PK" + (iMANUAL.int_2 + 1).ToString() + pkZCmd);
                        } //PRS 검사 STEP이면 피커 Z축 옵셋 값 적용함!
                        break;

                    case ManualNumber.AllPkZRdy:
                        BASE.MoveHDPkRdy(nThread, (eHD)iMANUAL.int_1, "", ((eHD)iMANUAL.int_1).ToString() + " 피커 대기 위치 이송");
                        break;
                    case ManualNumber.PkZRdy:
                        BASE.MovePk(nThread, iMANUAL.iMT1, P.Ready, "", MtName[iMANUAL.iMT1] + " 대기 위치 이송");
                        break;
                    case ManualNumber.PkOddZPic:
                        BASE.MovePk(nThread, iMANUAL.iMT1, P.OddPckUp, "", MtName[iMANUAL.iMT1] + " 픽업 위치 이송");
                        break;
                    case ManualNumber.PkEvenPic:
                        BASE.MovePk(nThread, iMANUAL.iMT1, P.EvenPckUp, "", MtName[iMANUAL.iMT1] + " 픽업 위치 이송");
                        break;
                    case ManualNumber.PkOddZPlc:
                        BASE.MovePk(nThread, iMANUAL.iMT1, P.OddPlc, "", MtName[iMANUAL.iMT1] + " 플레이스 위치 이송");
                        break;
                    case ManualNumber.PkEvenPlc:
                        BASE.MovePk(nThread, iMANUAL.iMT1, P.EvenPlc, "", MtName[iMANUAL.iMT1] + " 플레이스 위치 이송");
                        break;

                    case ManualNumber.PkThRdy:
                        BASE.MovePkTh(nThread, iMANUAL.iMT1, P.Ready, "", MtName[iMANUAL.iMT1] + " 대기 위치 이송");
                        break;
                    case ManualNumber.PkPicTh:
                        BASE.MovePkTh(nThread, iMANUAL.iMT1, P.PkPckUp, "", MtName[iMANUAL.iMT1] + " 픽업 위치 이송");
                        break;
                    case ManualNumber.PkPlcTh:
                        BASE.MovePkTh(nThread, iMANUAL.iMT1, P.PkPlc, "", MtName[iMANUAL.iMT1] + " 플레이스 위치 이송");
                        break;
                    case ManualNumber.PkTh:
                        BASE.MovePkTh(nThread, iMANUAL.iMT1, iMANUAL.double_1, "", MtName[iMANUAL.iMT1] + " " + iMANUAL.double_1.ToString() + " 위치 이송");
                        break;

                    case ManualNumber.MasterZigCenterHD:
                        if (eRTN.SUCESS != BASE.MoveHDPkRdy(nThread, (eHD)iMANUAL.int_1, "", ((eHD)iMANUAL.int_1).ToString() + " 피커 대기 위치 이송")) break;
                        BASE.MoveMasterZigCenter(nThread, (eHD)iMANUAL.int_1, (ePK)iMANUAL.int_3, (eMAP_BLOCK)iMANUAL.int_2, iMANUAL.bool_1, "", ((eHD)iMANUAL.int_1).ToString() + "/" + ((eMAP_BLOCK)iMANUAL.int_2).ToString() + " 마스터 지그 센터 위치 이송");
                        break;

                    case ManualNumber.TrayFeederRdy:
                        BASE.MoveFeederY(nThread, (eTRAY)iMANUAL.int_1, P.Ready, "", ((eTRAY)iMANUAL.int_1).ToString() + " 대기 위치 이송");
                        break;
                    case ManualNumber.TrayFeederLoading:
                        BASE.MoveFeederY(nThread, (eTRAY)iMANUAL.int_1, P.TrayLoad, "", ((eTRAY)iMANUAL.int_1).ToString() + " 트레이 공급 위치 이송");
                        break;
                    case ManualNumber.TrayFeederPlc:
                        BASE.MoveFeederY(nThread, (eTRAY)iMANUAL.int_1, P.Tray_Place[(int)iMANUAL.int_2], "", ((eTRAY)iMANUAL.int_1).ToString() + "축 " + ((eHD)iMANUAL.int_2).ToString() + " 첫번째 포켓 위치 이송");
                        break;
                    case ManualNumber.TrayFeederStacker:
                        BASE.MoveFeederY(nThread, (eTRAY)iMANUAL.int_1, P.Staker, "", ((eTRAY)iMANUAL.int_1).ToString() + " 트레이 스태커 배출 위치 이송");
                        break;
                    case ManualNumber.TrayFeederConvULDStart:
                        BASE.MoveFeederY(nThread, (eTRAY)iMANUAL.int_1, P.FastPsh, "", ((eTRAY)iMANUAL.int_1).ToString() + " 트레이 콘베어 푸쉬 위치 이송");
                        break;
                    case ManualNumber.TrayFeederTrayPusherStart:
                        BASE.MoveFeederY(nThread, (eTRAY)iMANUAL.int_1, P.Psh, "", ((eTRAY)iMANUAL.int_1).ToString() + " 트레이 푸쉬 시작 위치  위치 이송");
                        break;
                    case ManualNumber.TrayFeederConvUnloading:
                        BASE.MoveFeederY(nThread, (eTRAY)iMANUAL.int_1, P.TrayUnload, "", ((eTRAY)iMANUAL.int_1).ToString() + " 트레이 콘베어 언로딩 위치 이송");
                        break;
                    case ManualNumber.TrayFeederHD1Plc:
                        BASE.MoveFeederY(nThread, (eTRAY)iMANUAL.int_1, P.HD1TrayPocket, "", ((eTRAY)iMANUAL.int_1).ToString() + " 트레이 헤드1 우상단 포켓 중심 위치 이송");
                        break;
                    case ManualNumber.TrayFeederHD2Plc:
                        BASE.MoveFeederY(nThread, (eTRAY)iMANUAL.int_1, P.HD2TrayPocket, "", ((eTRAY)iMANUAL.int_1).ToString() + " 트레이 헤드2 우상단 포켓 중심 위치 이송");
                        break;

                    case ManualNumber.TrayPkUp:
                        C.TrayPk.MoveZ(P.Ready, "", "트레이 피커 Z축 대기 위치 이송");
                        break;
                    case ManualNumber.TrayPkDn:
                        if (mtDATA[M.TrayPickerX, P.TrayPckUp].bPOS)
                            C.TrayPk.MoveZ(P.TrayPckUp, "", "트레이 피커 Z축 빈-트레이 픽업 위치 이송");
                        if (mtDATA[M.TrayPickerX, P.OKTrayPlc].bPOS)
                            C.TrayPk.MoveZ(P.OKTrayPlc, "", "트레이 피커 Z축 GOOD 트레이 레일 플레이스 위치 이송");
                        if (mtDATA[M.TrayPickerX, P.NGTrayPlc].bPOS)
                            C.TrayPk.MoveZ(P.NGTrayPlc, "", "트레이 피커 Z축 REWORK 트레이 레일 플레이스 위치 이송");
                        break;

                    case ManualNumber.TrayPkRdy:
                        if (eRTN.SUCESS != C.TrayPk.MoveZ(P.Ready, "", "트레이 피커 Z축 대기 위치 이송")) break;
                        C.TrayPk.MoveX(P.Ready, "", "트레이 피커 X축 대기 위치 이송");
                        break;
                    case ManualNumber.TrayPkEmptyFeeder:
                        if ((mtDATA[M.TrayPickerZ, P.Ready].Pos + 3) < mtSTS[M.TrayPickerZ].CurrentPosition){
                            if (eRTN.SUCESS != C.TrayPk.MoveZ(P.Ready, "", "트레이 피커 Z축 대기 위치 이송")) break;
                        }
                        if (eRTN.SUCESS != C.TrayPk.MoveX(P.TrayPckUp, "", "트레이 피커 X축 빈-트레이 픽업 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.TrayPk.MoveZ(P.TrayPckUp, "", "트레이 피커 Z축 빈-트레이 픽업 위치 이송");
                        break;
                    case ManualNumber.TrayPkGoodFeeder:
                        if ((mtDATA[M.TrayPickerZ, P.Ready].Pos + 3) < mtSTS[M.TrayPickerZ].CurrentPosition){
                            if (eRTN.SUCESS != C.TrayPk.MoveZ(P.Ready, "", "트레이 피커 Z축 대기 위치 이송")) break;
                        }
                        if (eRTN.SUCESS != C.TrayPk.MoveX(P.OKTrayPlc, "", "트레이 피커 X축 GOOD 트레이 레일 플레이스 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.TrayPk.MoveZ(P.OKTrayPlc, "", "트레이 피커 Z축 GOOD 트레이 레일 플레이스 위치 이송");
                        break;
                    case ManualNumber.TrayPkReworkFeeder:
                        if ((mtDATA[M.TrayPickerZ, P.Ready].Pos + 3) < mtSTS[M.TrayPickerZ].CurrentPosition){
                            if (eRTN.SUCESS != C.TrayPk.MoveZ(P.Ready, "", "트레이 피커 Z축 대기 위치 이송")) break;
                        }
                        if (eRTN.SUCESS != C.TrayPk.MoveX(P.NGTrayPlc, "", "트레이 피커 X축 REWORK 트레이 레일 플레이스 위치 이송")) break;
                        if (iMANUAL.Option)
                            C.TrayPk.MoveZ(P.NGTrayPlc, "", "트레이 피커 Z축 REWORK 트레이 레일 플레이스 위치 이송");
                        break;

                    case ManualNumber.EmptyStackerRdy:
                        C.EmptyStacker.MoveZ(P.Ready, "", "빈트레이 스태커 대기 위치 이송");
                        break;
                    case ManualNumber.EmptyStackerLoading:
                        C.EmptyStacker.MoveZ(P.EmptyTraySupply, "", "빈트레이 공급 위치 이송");
                        break;
                    case ManualNumber.EmptyStackerLock:
                        C.EmptyStacker.MoveZ(P.EmptyTrayHold, "", "빈트레이 한피치 다운 후 스토퍼 락 위치 이송");
                        break;
                    case ManualNumber.EmptyStackerWork:
                        C.EmptyStacker.MoveZ(P.EmptyTraySafeArrial, "", "빈트레이 레일 안착 위치 이송");
                        break;
#endregion

#region >> Cycle-Run
                    case ManualNumber.RunMGZLoading:
                        if (mIN[I.ELV_MZ_EXIST1] || mIN[I.ELV_MZ_EXIST2]){
                            if (eRTN.SUCESS != C.Magazine.Clamp("매거진 클램프")) break;
                            if (IsBIT[B.ElvLDLocation]){
                                if (eRTN.SUCESS != C.Magazine.Clamp("매거진 클램프")) break;
                                iMANUAL.ManualCmd = "offset=" + string.Format("{0:0.0}", prMACHINE[CP.ElvUpDownPitch]) + ":spd=10";
                                if (eRTN.SUCESS != C.Magazine.MoveZ(P.Recive, iMANUAL.ManualCmd, "엘리베이터 Z축 매거진 로딩 픽업 위치 이송")) break;
                                if (eRTN.SUCESS != C.Magazine.MoveY(P.FirstSlot, "", "엘리베이터 Y축 매거진 첫번째 슬롯 위치 이송")) break;
                                if (eRTN.SUCESS != C.Magazine.MoveZ(P.FirstSlot, "", "엘리베이터 Z축 매거진 첫번째 슬롯 위치 이송")) break;
                                MAP_.SET_CstMapAllExists(M.ElvZ);
                            }
                            break;
                        }
                        if (IsBIT[B.ElvULDLocation]){
                            if (mIN[I.ELV_MZ_EXIST1] || mIN[I.ELV_MZ_EXIST2]){
                                if (LAB_.INPUT(I.ULD_CONV_MZ_FULL_CHECK1) || !LAB_.INPUT(I.ULD_CONV_MZ_FULL_CHECK2)){
                                    W.ViewWarning(nThread, W.ManualErrMassage, "매거진 레일 배출 레일에 가득차 있음!");
                                    break;
                                }
                                if (eRTN.SUCESS != C.Magazine.OutCassate("메뉴얼 매거진 언로딩")) break;
                            }
                        }
                        iMANUAL.eRESULT = C.Magazine.GetCassate("메뉴얼 매거진 로딩");
                        if (iMANUAL.eRESULT != eRTN.SUCESS){
                            if (iMANUAL.eRESULT == eRTN.NotLoadingMagazine){
                                if (eRTN.SUCESS != C.Magazine.MoveY(P.FirstSlot, "", "엘리베이터 Y축 매거진 첫번째 슬롯 위치 이송")) break;
                                W.ViewWarning(nThread, W.ManualErrMassage, "로딩 레일에 매거진 없음!");
                                break;
                            }
                            else{
                                W.ViewWarning(nThread, W.ManualErrMassage, "매거진 로딩 실패!");
                                break;
                            }
                        }
                        break;
                    case ManualNumber.RunMGZUnloading:
#if _NSS3300
                        if (mIN[I.ULD_CONV_MZ_FULL_CHECK1] || !mIN[I.ULD_CONV_MZ_FULL_CHECK2]){
#else
                        if (mIN[I.ULD_CONV_MZ_FULL_CHECK1] || mIN[I.ULD_CONV_MZ_FULL_CHECK2]){
#endif
                            W.ViewWarning(nThread, W.ManualErrMassage, "매거진 배출 레일에 가득차 있음!");
                            break;
                        }
                        if (mIN[I.ELV_MZ_EXIST1] || mIN[I.ELV_MZ_EXIST2]){
                            if (IsBIT[B.ElvLDLocation]){
#if _NSS3300
                                iMANUAL.ManualCmd = "offset-=" + string.Format("{0:0.0}", prMACHINE[CP.ElvULDUpDownPitch]) + ":spd=10";
#else
                                iMANUAL.ManualCmd = "offset=" + string.Format("{0:0.0}", prMACHINE[CP.ElvULDUpDownPitch]) + ":spd=10";
#endif
                                if (eRTN.SUCESS != C.Magazine.MoveZ(P.Recive, iMANUAL.ManualCmd, "엘리베이터 Z축 매거진 로딩 픽업 위치 이송")) break;
                                if (eRTN.SUCESS != C.Magazine.MoveY(P.FirstSlot, "", "엘리베이터 Y축 매거진 첫번째 슬롯 위치 이송")) break;
                                if (eRTN.SUCESS != C.Magazine.MoveZ(P.FirstSlot, "", "엘리베이터 Z축 매거진 첫번째 슬롯 위치 이송")) break;
                            }
                            if (IsBIT[B.ElvULDLocation]){
                                if (eRTN.SUCESS != C.Magazine.UnClamp("매거진 언클램프")) break;
#if _NSS3300
                                iMANUAL.ManualCmd = "offset=" + string.Format("{0:0.0}", prMACHINE[CP.ElvULDUpDownPitch]);
#else
                                iMANUAL.ManualCmd = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.ElvULDUpDownPitch]);
#endif
                                if (eRTN.SUCESS != C.Magazine.MoveZ(P.Give, iMANUAL.ManualCmd, "엘리베이터 Z축 매거진 언로딩 위치 이송")) break;
                                if (eRTN.SUCESS != C.Magazine.MoveY(P.Ready, "", "엘리베이터 Y축 대기 위치 이송")) break;
                                break;
                            }
                        }
                        if (!mIN[I.RAIL_MOUTH]) {
                            W.ViewWarning(nThread, W.ManualErrMassage, "매거진에서 PCB 돌출되어 있음!");
                            break;
                        }
                        iMANUAL.eRESULT = C.Magazine.OutCassate("메뉴얼 매거진 언로딩");
                        break;
                    case ManualNumber.RunMGZSlotLocation:
                        if (!mIN[I.ELV_MZ_EXIST1] && !mIN[I.ELV_MZ_EXIST2]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "매거진 로딩 되어 있지 않음!");
                            break;
                        }
                        if (IsBIT[B.ElvLDLocation]){
#if _NSS3300
                            iMANUAL.ManualCmd = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.ElvUpDownPitch]) + ":spd=10";
#else
                            iMANUAL.ManualCmd = "offset=" + string.Format("{0:0.0}", prMACHINE[CP.ElvUpDownPitch]) + ":spd=10";
#endif
                            if (eRTN.SUCESS != C.Magazine.MoveZ(P.Recive, iMANUAL.ManualCmd, "엘리베이터 Z축 매거진 로딩 픽업 위치 이송")) break;
                            if (eRTN.SUCESS != C.Magazine.MoveY(P.FirstSlot, "", "엘리베이터 Y축 매거진 첫번째 슬롯 위치 이송")) break;
                            if (eRTN.SUCESS != C.Magazine.MoveZ(P.FirstSlot, "", "엘리베이터 Z축 매거진 첫번째 슬롯 위치 이송")) break;
                        }
                        if (IsBIT[B.ElvULDLocation]){
                            if (eRTN.SUCESS != C.Magazine.OutCassate("메뉴얼 매거진 언로딩")) break;
                            if (!mIN[I.ELV_MZ_EXIST1] && !mIN[I.ELV_MZ_EXIST2]){
                                W.ViewWarning(nThread, W.ManualErrMassage, "매거진 로딩 되어 있지 않음!");
                            }
                            break;
                        }
                        if (eRTN.SUCESS != C.Magazine.MoveY(P.FirstSlot, "", "ELV' Y축 스트립 로딩 위치 이송")) break;
                        C.Magazine.MoveMGZSlot(iMANUAL.int_1, "", "매거진 슬롯 " + (iMANUAL.int_1 + 1).ToString() + "번 위치 이송");
                        break;

                    case ManualNumber.RunStripLoading:
                        if (!mIN[I.RAIL_EXIST1]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "레일 위에 스트립 없음!");
                            break;
                        }
                        if (mIN[I.RAIL_EXIST2]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "레일 픽업 위치에 스트립 있음!");
                            break;
                        }
                        if (mIN[I.INLET_TABLE_UP] && !mIN[I.INLET_TABLE_DN]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "인-레일 테이블 다운 상태인지 확인!");
                            break;
                        }
                        C.Gripper.Process("스트립 로딩");
                        break;
                    case ManualNumber.RunStripPic:
                        if (!mIN[I.RAIL_EXIST2]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "레일 위에 스트립 업습니다!");
                            break;
                        }
                        if (mtDATA[M.GrpX, P.Ready].Pos + 1 < mtSTS[M.GrpX].CurrentPosition){
                            W.ViewWarning(nThread, W.ManualErrMassage, "그리퍼 X축 대기 위치로 보내 주셔야 합니다!");
                            break;
                        }
                        if (mIN[I.STRIP_PK_VAC]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "스트립 피커 진공 센서 ON 상태 확인 바랍니다!");
                            break;
                        }
                        if (prMACHINE[CP.UnitPkSafetyPosition] < mtSTS[M.UnitPkX].CurrentPosition){
                            W.ViewWarning(nThread, W.ManualErrMassage, "유닛 피커 X축 대기 위치로 보내 주셔야 합니다!");
                            break;
                        }

                        if (!C.StripPk.Pic("스트립 픽업")) break;
                        if (eRTN.SUCESS != C.StripPk.MoveZ(P.Ready, "", "스트립 피커 Z축 대기 위치 이송")) break;
                        C.Gripper.MoveRail(P.StripIn, "", "매거진 스트립 투입 레일 위치 이송");
                        if (IsBIT[B.SawManualRun]) C.ReceiveSaw.ManualSecuss();
                        break;
                    case ManualNumber.RunStripPlc:
                        if (!mIN[I.STRIP_PK_VAC]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "스트립 피커 진공 OFF 되어 있음!");
                            break;
                        }
                        if (!mIN[I.SAW_LD_REQ]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "다이싱에서 스트립 공급 요청 비트 OFF 되어 있음!");
                            break;
                        }
                        if (prMACHINE[CP.UnitPkSafetyPosition] < mtSTS[M.UnitPkX].CurrentPosition){
                            W.ViewWarning(nThread, W.ManualErrMassage, "유닛 피커 X축 대기 위치로 보내 주셔야 합니다!");
                            break;
                        }
                        if (IsBIT[B.StripPlacStop]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "다이싱 테이블 스트립 공급 일시 정지 중 입니다!");
                            break;
                        }

                        if (!C.StripPk.Plc("스트립 플레이스")) break;
                        if (eRTN.SUCESS != C.StripPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        if (eRTN.SUCESS != C.StripPk.MoveX(P.Ready, "", "유닛 피커 X축 대기 위치 이송")) break;
                        if (IsBIT[B.SawManualRun]) C.ReceiveSaw.ManualSecuss();
                        break;

                    case ManualNumber.RunUnitPic:
                        iMANUAL.bool_1 = true;
                        if (mIN[I.UNIT_PK_VAC]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "유닛 피커 진공 ON 되어 있음!");
                            break;
                        }
                        if (!mIN[I.SAW_ULD_REQ]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "다이싱에서 유닛 배출 요청 비트 OFF 되어 있음!");
                            break;
                        }
                        if (prMACHINE[CP.StripPkSafetyPosition] < mtSTS[M.StripPkX].CurrentPosition){
                            W.ViewWarning(nThread, W.ManualErrMassage, "스트립 피커 X축 대기 위치로 보내 주셔야 합니다!");
                            break;
                        }
                        if (IsBIT[B.UnitPickupStop]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "다이싱 테이블 유닛 배출 일시 정지 중 입니다!");
                            break;
                        }

                        if (!C.UnitPk.Pic("유닛 픽업")) iMANUAL.bool_1 = false;
                        if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.Ready, "", "유닛 피커 X축 대기 위치 이송")) break;

                        if (iMANUAL.bool_1) { if (IsBIT[B.SawManualRun]) C.ReceiveSaw.ManualSecuss(); }
                        else W.ViewWarning(nThread, W.ManualErrMassage, "유닛 피커 픽업 실패!");
                        break;
                    case ManualNumber.RunScrap:
                        iMANUAL.bool_1 = true;
                        if (prMACHINE[CP.StripPkSafetyPosition] < mtSTS[M.StripPkX].CurrentPosition){
                            W.ViewWarning(nThread, W.ManualErrMassage, "스트립 피커 X축 대기 위치로 보내 주셔야 합니다!");
                            break;
                        }
                        if (prMACHINE[CP.UseScrapVacCheck] == (int)eUSE.USE && (!mIN[I.SCRAP_VAC1] || !mIN[I.SCRAP_VAC2])){
                            W.ViewWarning(nThread, W.ManualErrMassage, "스크랩 진공 OFF 되어 있음!");
                            break;
                        }

                        if (!C.UnitPk.Scrap("스크랩 제거")) iMANUAL.bool_1 = false;
                        if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.Ready, "", "유닛 피커 X축 대기 위치 이송")) break;

                        if (iMANUAL.bool_1) { if (IsBIT[B.SawManualRun]) C.ReceiveSaw.ManualSecuss(); }
                        else W.ViewWarning(nThread, W.ManualErrMassage, "유닛 피커 픽업 실패!");
                        break;
                    case ManualNumber.RunCleaner:
                        if (!mIN[I.UNIT_PK_VAC]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "유닛 피커 진공 OFF 되어 있음!");
                            break;
                        }
                        C.UnitPk.Cleaner("유닛 클리닝");
                        if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.Ready, "", "유닛 피커 X축 대기 위치 이송")) break;
                        if (IsBIT[B.SawManualRun]) C.ReceiveSaw.ManualSecuss();
                        break;
                    case ManualNumber.RunBrush:
                        if (!mIN[I.UNIT_PK_VAC]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "유닛 피커 진공 OFF 되어 있음!");
                            break;
                        }
                        if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.BrushStart, "", "유닛 피커 X축 브러쉬 시작 위치 이송")) break;
                        C.UnitPk.Brush("유닛 브러쉬 작업");
                        if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.Ready, "", "유닛 피커 X축 대기 위치 이송")) break;
                        if (IsBIT[B.SawManualRun]) C.ReceiveSaw.ManualSecuss();
                        break;
                    case ManualNumber.RunUnitAirshower:
                        if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.AirBlowStart, "", "유닛 피커 X축 대기 위치 이송")) break;
                        C.UnitPk.AirShower("유닛 에어샤워");
                        if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.Ready, "", "유닛 피커 X축 대기 위치 이송")) break;
                        if (IsBIT[B.SawManualRun]) C.ReceiveSaw.ManualSecuss();
                        break;
                    case ManualNumber.RunUnitCleaning:
                        if (!mIN[I.UNIT_PK_VAC]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "유닛 피커 진공 OFF 되어 있음!");
                            break;
                        }
                        if (prMACHINE[CP.StripPkSafetyPosition] < mtSTS[M.StripPkX].CurrentPosition){
                            W.ViewWarning(nThread, W.ManualErrMassage, "스트립 피커 X축 대기 위치로 보내 주셔야 합니다!");
                            break;
                        }
                        if (prMACHINE[CP.UseScrapVacCheck] == (int)eUSE.USE && (!mIN[I.SCRAP_VAC1] || !mIN[I.SCRAP_VAC2])){
                            W.ViewWarning(nThread, W.ManualErrMassage, "스크랩 진공 OFF 되어 있음!");
                            break;
                        }
                        if (!C.UnitPk.Scrap("스크랩 제거")) break;

                        if (!mIN[I.UNIT_PK_VAC]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "유닛 피커 진공 OFF 되어 있음!");
                            break;
                        }
                        if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.BrushStart, "", "유닛 피커 X축 브러쉬 시작 위치 이송")) break;
                        if (!C.UnitPk.Brush("유닛 브러쉬 작업")) break;

                        if (!mIN[I.UNIT_PK_VAC]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "유닛 피커 진공 OFF 되어 있음!");
                            break;
                        }
                        if (!C.UnitPk.Cleaner("유닛 클리닝")) break;

                        if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.AirBlowStart, "", "유닛 피커 X축 대기 위치 이송")) break;
                        C.UnitPk.AirShower("유닛 에어샤워");
                        if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.Ready, "", "유닛 피커 X축 대기 위치 이송")) break;
                        if (IsBIT[B.SawManualRun]) C.ReceiveSaw.ManualSecuss();
                        break;

                    case ManualNumber.RunUnitPlc:
                        if (!mIN[I.UNIT_PK_VAC]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "유닛 피커 진공 OFF 되어 있음!");
                            break;
                        }
                        if (mIN[I.StageVac[iMANUAL.int_1]]){
                            W.ViewWarning(nThread, W.ManualErrMassage, DATA_.InputName[I.StageVac[iMANUAL.int_1]] + " 진공 ON 상태 !");
                            break;
                        }
                        if (IsBIT[B.Stage_Pic[iMANUAL.int_1]]){
                            W.ViewWarning(nThread, W.ManualErrMassage, ((eMAP_BLOCK)iMANUAL.int_1).ToString() + " 픽업 작업 중 !");
                            break;
                        }
                        if (IsBIT[B.UnitInspection[iMANUAL.int_1]] || IsBIT[B.StageBusy[iMANUAL.int_1]]){
                            W.ViewWarning(nThread, W.ManualErrMassage, ((eMAP_BLOCK)iMANUAL.int_1).ToString() + " 작업 중 !");
                            break;
                        }
                        if (0 == MAP_.GetPalletPocket((eMAP_BLOCK)iMANUAL.int_1, (int)prMODEL[RP.GroupCntX], (int)prMODEL[RP.GroupCntY], (int)prMODEL[RP.UnitX[iMANUAL.int_1]], (int)prMODEL[RP.UnitY[iMANUAL.int_1]], ref iMANUAL.int_5, ref iMANUAL.int_6, ref iMANUAL.int_7, ref iMANUAL.int_8)){
                            W.ViewWarning(nThread, W.ManualErrMassage, ((eMAP_BLOCK)iMANUAL.int_1).ToString() + " 유닛 정보 남아 있음 !");
                            break;
                        }

                        if (eRTN.SUCESS != BASE.UnitReceive(nThread, (eMAP_BLOCK)iMANUAL.int_1, ((eMAP_BLOCK)iMANUAL.int_1).ToString() + " 유닛 공급")) break;
                        if (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) break;
                        if (eRTN.SUCESS != C.UnitPk.MoveX(P.Ready, "", "유닛 피커 X축 대기 위치 이송")) break;
                        if (IsBIT[B.SawManualRun]) C.ReceiveSaw.ManualSecuss();
                        break;

                    case ManualNumber.RunStageAirshower:
                        if (!mIN[I.StageVac[iMANUAL.int_1]] && !bDRYRUN){
                            W.ViewWarning(nThread, W.ManualErrMassage, ((eMAP_BLOCK)iMANUAL.int_1).ToString() + "번 테이블 진공 센서 OFF 되어 있음");
                            break;
                        }
                        if (IsBIT[B.StageAirshowerWait]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "테이블 에어샤워 작업 일시 정지 플로그 ON 상태!");
                            break;
                        }
                        if (eRTN.SUCESS != BASE.MoveAllPkRdy(nThread, "", "모든 헤드 피커 안전 위치로 이송")) break;
                        BASE.StageAirshower(nThread, (eMAP_BLOCK)iMANUAL.int_1, ((eMAP_BLOCK)iMANUAL.int_1).ToString() + " 유닛 에어샤워 작업");
                        break;
                    case ManualNumber.RunUnitInspection:
                        if (!mIN[I.StageVac[iMANUAL.int_1]] && !bDRYRUN){
                            W.ViewWarning(nThread, W.ManualErrMassage, ((eMAP_BLOCK)iMANUAL.int_1).ToString() + "번 테이블 진공 센서 OFF 되어 있음");
                            break;
                        }
                        //유닛 데이터 확인
                        if (eRTN.SUCESS != BASE.MoveAllPkRdy(nThread, "", "모든 헤드 피커 안전 위치로 이송")) break;
                        
                        BASE.SeqUnitInspection(nThread, (eMAP_BLOCK)iMANUAL.int_1, ((eMAP_BLOCK)iMANUAL.int_1).ToString() + " 유닛 검사 진행");
                        break;

                    case ManualNumber.RunPickerCal:
                        if (!mIN[I.VisionRdy]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "비전 RUN 상태에서 동작 가능 합니다!");
                            break;
                        }
                        BASE.ResetPkrCal(nThread);
                        UTIL_.DELAY(77);
                        if (iMANUAL.bool_1){
                            //T: PK CAL Z / F:UNIT INSPECTION Z
                            if (eRTN.SUCESS != BASE.MoveHDPkCenter(nThread, (eHD)iMANUAL.int_1, iMANUAL.int_2, true, "", ((eHD)iMANUAL.int_1).ToString() + " 피커 중심 위치 이송")) break;
                        }//먼저이송                        
                        if (eRTN.SUCESS != BASE.PkrOnSHOT(nThread)) break;
                        if (eRTN.SUCESS != BASE.ReadPkCal(nThread)){
                            O.SetOutput(nThread, O.PkCalReading, false, "피커 CAL' 데이터 리딩 실패");
                            O.SetOutput(nThread, O.PkCalStart, false, ThreadName[nThread] + " 피커 CAL' 실패");
                            if (mOUT[O.UsePkCal]) BASE.PK_CAL_LIGHT(nThread, false);
                            break;
                        }
                        BASE.MovePkCalPitch(nThread, (eHD)iMANUAL.int_1, "피커 CAL' 옵셋 피치 이송");
                        iMANUAL.XY_1.x = IsDOUBLE[D.PkCal_OffsetX];
                        iMANUAL.XY_1.y = IsDOUBLE[D.PkCal_OffsetY];

                        if (eRTN.SUCESS != BASE.PkrOnSHOT(nThread)) break;
                        if (eRTN.SUCESS != BASE.ReadPkCal(nThread)){
                            O.SetOutput(nThread, O.PkCalReading, false, "피커 CAL' 데이터 리딩 실패");
                            O.SetOutput(nThread, O.PkCalStart, false, ThreadName[nThread] + " 피커 CAL' 실패");
                            if (mOUT[O.UsePkCal]) BASE.PK_CAL_LIGHT(nThread, false);
                            break;
                        }

                        if (iMANUAL.bool_1){
                            iMANUAL.double_1 = mtSTS[iMANUAL.iMT2].CurrentPosition;
                            iMANUAL.ManualCmd = BASE.GetPkOffsetLabel(iMANUAL.double_1);
                            if (iMANUAL.ManualCmd == ""){
                                W.ViewWarning(nThread, W.ManualErrMassage, "피커 회전 위치 확인 후 다시 CAL' 진행!");
                                break;
                            }

                            for (int i = 0; i < 3; i++){
                                BASE.GetPkOffset((eHD)iMANUAL.int_1, (ePK)iMANUAL.int_2, iMANUAL.double_1, ref iMANUAL.XY_2);
                                if (i == 0) iMANUAL.XY_8 = iMANUAL.XY_2;
                                iMANUAL.XY_2.x += iMANUAL.XY_1.x;
                                iMANUAL.XY_2.y -= iMANUAL.XY_1.y;
                                TEACH_.WR_PickerOffset(iMANUAL.int_2 + (8 * iMANUAL.int_1), iMANUAL.XY_2);
                                TEACH_.WR_PickerOffset(iMANUAL.ManualCmd, iMANUAL.int_2 + (8 * iMANUAL.int_1), iMANUAL.XY_2);
                                //T: PK CAL Z / F:UNIT INSPECTION Z
                                BASE.MoveHDPkCenter(nThread, (eHD)iMANUAL.int_1, iMANUAL.int_2, true, "", ((eHD)iMANUAL.int_1).ToString() + " 피커 중심 위치 이송");

                                if (eRTN.SUCESS != BASE.PkrOnSHOT(nThread)) break;
                                if (eRTN.SUCESS != BASE.ReadPkCal(nThread)){
                                    O.SetOutput(nThread, O.PkCalReading, false, "피커 CAL' 데이터 리딩 실패");
                                    O.SetOutput(nThread, O.PkCalStart, false, ThreadName[nThread] + " 피커 CAL' 실패");
                                    if (mOUT[O.UsePkCal]) BASE.PK_CAL_LIGHT(nThread, false);
                                    break;
                                }
                                if (Math.Abs(IsDOUBLE[D.PkCal_OffsetX]) < 0.01 && Math.Abs(IsDOUBLE[D.PkCal_OffsetY]) < 0.01){
                                    break;
                                }

                                BASE.MovePkCalPitch(nThread, (eHD)iMANUAL.int_1, "피커 CAL' 옵셋 피치 이송");
                                iMANUAL.XY_1.x = IsDOUBLE[D.PkCal_OffsetX];
                                iMANUAL.XY_1.y = IsDOUBLE[D.PkCal_OffsetY];
                            }
                            BASE.ResetPkrCal(nThread);
                            if (Math.Abs(IsDOUBLE[D.PkCal_OffsetX]) > 0.01 && Math.Abs(IsDOUBLE[D.PkCal_OffsetY]) > 0.01){
                                W.ViewWarning(nThread, W.ManualErrMassage, "스팩 OUT!" + "[X:" + IsDOUBLE[D.PkCal_OffsetX].ToString() + "/Y:" + IsDOUBLE[D.PkCal_OffsetY].ToString() + "]");
                                break;
                            } //spec out
                        } //피커 옵셋 적용
                        break;

                    case ManualNumber.RunPickerAutoCal:
                        if (!mIN[I.VisionRdy]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "비전 RUN 상태에서 동작 가능 합니다!");
                            break;
                        }
                        if (iMANUAL.ManualCmd == ""){
                            W.ViewWarning(nThread, W.ManualErrMassage, "피커 회전 위치 확인 후 다시 CAL' 진행!");
                            break;
                        }

                        if (eRTN.SUCESS != BASE.MoveHDPkCenter(nThread, (eHD)iMANUAL.int_1, 0, iMANUAL.double_1, true, "", "피커 AUTO CAL' 첫번째 위치 이송")) break;
                        BASE.ResetPkrCal(nThread);
                        UTIL_.DELAY(77);
                        for (int p = 0; p < 6; p++){
                            iMANUAL.bool_1 = false;
                            //T: PK CAL Z / F:UNIT INSPECTION Z
                            if (eRTN.SUCESS != BASE.MoveHDPkCenter(nThread, (eHD)iMANUAL.int_1, p, true, "", ((eHD)iMANUAL.int_1).ToString() + " 피커 중심 위치 이송")) break;
                            if (eRTN.SUCESS != BASE.PkrOnSHOT(nThread)) break;
                            if (eRTN.SUCESS != BASE.ReadPkCal(nThread)){
                                O.SetOutput(nThread, O.PkCalReading, false, "피커 CAL' 데이터 리딩 실패");
                                O.SetOutput(nThread, O.PkCalStart, false, ThreadName[nThread] + " 피커 CAL' 실패");
                                if (mOUT[O.UsePkCal]) BASE.PK_CAL_LIGHT(nThread, false);
                                break;
                            }
                            BASE.MovePkCalPitch(nThread, (eHD)iMANUAL.int_1, "피커 CAL' 옵셋 피치 이송");
                            iMANUAL.XY_1.x = IsDOUBLE[D.PkCal_OffsetX];
                            iMANUAL.XY_1.y = IsDOUBLE[D.PkCal_OffsetY];

                            for (int i = 0; i < 4; i++){
                                BASE.GetPkOffset((eHD)iMANUAL.int_1, (ePK)p, iMANUAL.double_1, ref iMANUAL.XY_2);

                                iMANUAL.XY_2.x += iMANUAL.XY_1.x;
                                iMANUAL.XY_2.y -= iMANUAL.XY_1.y;
                                TEACH_.WR_PickerOffset(p + (8 * iMANUAL.int_1), iMANUAL.XY_2);
                                TEACH_.WR_PickerOffset(iMANUAL.ManualCmd, p + (8 * iMANUAL.int_1), iMANUAL.XY_2);
                                //T: PK CAL Z / F:UNIT INSPECTION Z
                                BASE.MoveHDPkCenter(nThread, (eHD)iMANUAL.int_1, p, true, "", ((eHD)iMANUAL.int_1).ToString() + " 피커 중심 위치 이송");

                                if (eRTN.SUCESS != BASE.PkrOnSHOT(nThread)) break;
                                if (eRTN.SUCESS != BASE.ReadPkCal(nThread)){
                                    O.SetOutput(nThread, O.PkCalReading, false, "피커 CAL' 데이터 리딩 실패");
                                    O.SetOutput(nThread, O.PkCalStart, false, ThreadName[nThread] + " 피커 CAL' 실패");
                                    if (mOUT[O.UsePkCal]) BASE.PK_CAL_LIGHT(nThread, false);
                                    break;
                                }
                                if (Math.Abs(IsDOUBLE[D.PkCal_OffsetX]) < 0.01 && Math.Abs(IsDOUBLE[D.PkCal_OffsetY]) < 0.01){
                                    iMANUAL.bool_1 = true;
                                    break;
                                }
                                BASE.MovePkCalPitch(nThread, (eHD)iMANUAL.int_1, "피커 CAL' 옵셋 피치 이송");
                                iMANUAL.XY_1.x = IsDOUBLE[D.PkCal_OffsetX];
                                iMANUAL.XY_1.y = IsDOUBLE[D.PkCal_OffsetY];
                            }
                            if (!iMANUAL.bool_1){
                                W.ViewWarning(nThread, W.ManualErrMassage, ((eHD)iMANUAL.int_1).ToString() + "-피커 " + (p + 1).ToString() + " CALIBRATION 실패 피커 상태 확인 후 다시 실행 하셔야 합니다 !");
                                break;
                            }
                        } //picker
                        BASE.ResetPkrCal(nThread);
                        if (iMANUAL.bool_1)
                            W.ViewWarning(nThread, W.ManualErrMassage, ((eHD)iMANUAL.int_1).ToString() + " CALIBRATION 완료하였습니다. !");

                        B.Bit(nThread, B.ManualPkrAutoCalView, true, "피커 오토켈리브레이션 뷰어 플로그 ON");
                        break;

                    case ManualNumber.AllPickerAutoCal:
                        iMANUAL.eRESULT = BASE.Seq_HeadPkrAutoCal(nThread);
                        if (iMANUAL.eRESULT == eRTN.SUCESS){
                            W.ViewWarning(nThread, W.ManualErrMassage, "ALL HEAD PICKER PICK-UP / PLACE 위치 AUTO CALIBRATION 성공!");
                        }
                        else{
                            W.ViewWarning(nThread, W.ManualErrMassage, "ALL HEAD PICKER PICK-UP / PLACE 위치 AUTO CALIBRATION 실패!" + ETC.NewLine + "다시 CALIBRATION 진행 하셔야 합니다!");
                        }
                        break;

                    case ManualNumber.PkrPic:
                        BASE.PKPic(nThread, (eHD)iMANUAL.int_1, (eMAP_BLOCK)iMANUAL.int_2, iMANUAL.int_3, 50, iMANUAL.int_4, iMANUAL.int_5, iMANUAL.int_6, iMANUAL.int_7, "UNIT PICKUP");
                        BASE.MoveHDPkRdy(nThread, (eHD)iMANUAL.int_1, "", ((eHD)iMANUAL.int_1).ToString() + " 피커 대기 위치 이송");
                        BASE.PkVacReset((eHD)iMANUAL.int_1);
                        break;

                    case ManualNumber.RunPRS:
                        if (!mIN[I.VisionRdy]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "비전 실행 후 다시 실행 하셔야 합니다!");
                            break;
                        }
                        if (prMACHINE[CP.UesBtmInspection] == (int)eUSE.NotUSE){
                            W.ViewWarning(nThread, W.ManualErrMassage, "MARK 검사 모드 설정 되어 있지 않음!");
                            break;
                        }

                        if (eRTN.SUCESS != BASE.MoveAllPkRdy(nThread, "", "모든 피커 대기 위치 이송")) break;
                        BASE.PRS(nThread, (eHD)iMANUAL.int_1, ((eHD)iMANUAL.int_1).ToString() + " X-마크 검사 실행");
                        break;

                    case ManualNumber.RunReworkTrayLoading:
                        if (!mIN[I.TRAY_PKR_TRAY_CHECK]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "트레이 피커 트레이 없음.");
                            break;
                        }
#if _NSS3300
#else
                        if (!mIN[I.NG_RAIL_LOADING_TRAY_CHECK] || !mIN[I.NG_RAIL_HEAD1_TRAY_CHECK] || !mIN[I.NG_RAIL_HEAD2_TRAY_CHECK]){
                            if (IsBIT[B.ReWorkTrayWork]){
                                W.ViewWarning(nThread, W.ManualErrMassage, "NG 트레이 이송부에 트레이 작업 중.");
                                break;
                            }
                            W.ViewWarning(nThread, W.ManualErrMassage, "NG 트레이 이송부에 트레이 있음.");
                            break;
                        }
#endif
                        if (eRTN.SUCESS != BASE.MoveAllPkRdy(nThread, "", "모든 헤드 피커 안전 위치로 이송")) break;
                        C.TrayPk.Plc(eTRAY.REWORK, "트레이 NG TRAY TRANSFER에 내려놈");
                        break;
                    case ManualNumber.RunReworkTrayUnloading:
#if _NSS3300
#else
                        if (mIN[I.NG_RAIL_LOADING_TRAY_CHECK] && mIN[I.NG_RAIL_HEAD1_TRAY_CHECK] && mIN[I.NG_RAIL_HEAD2_TRAY_CHECK]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "NG 트레이 이송부에 트레이 없음.");
                            break;
                        }
#endif
                        if (mtSTS[M.TrayFeeder3].CurrentPosition < 900){
                            if (!mIN[I.NG_RAIL_STACKER_TRAY_CHECK]){
                                W.ViewWarning(nThread, W.ManualErrMassage, "NG 트레이 배출 스태커 아래 레일에 스태커 감지 됨.");
                                break;
                            }
                        }
                        if (eRTN.SUCESS != BASE.MoveAllPkRdy(nThread, "", "모든 헤드 피커 안전 위치로 이송")) break;
                        C.ReworkTrayFeeder.OutTray("메뉴얼 REWORK 트레이 스태커로 배출");
                        if (IsBIT[B.ReWorkTrayWork]){
                            B.Bit(nThread, B.ReWorkTrayWork, false, "메뉴얼 REWORK 트레이 배출 플러그 OFF");
                            MAP_.TrayMap_Reset(eTRAY.REWORK, (int)prMODEL[RP.TrayCntX], (int)prMODEL[RP.TrayCntY]);
                        }
                        break;

                    case ManualNumber.RunEmptyTrayLoading:
#if _NSS3300
                        if (!mIN[I.EMPTY_RAIL_LD_TRAY_CHECK]){
#else
                        if (!mIN[I.EMPTY_RAIL_LD_TRAY_CHECK] || !mIN[I.EMPTY_RAIL_ULD_TRAY_CHECK]){
#endif

                            W.ViewWarning(nThread, W.ManualErrMassage, "빈-트레이 이송부에 트레이 있음");
                            break;
                        }
                        if (!mIN[I.EMPTY_STACKER_NONE]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "빈-트레이 스태커에 트레이 없음");
                            break;
                        }
                        C.EmptyStacker.GetEmptyTray("메뉴얼 빈-트레이 공급");
                        break;
                    case ManualNumber.RunEmptyTrayPic:
                        if (mIN[I.TRAY_PKR_TRAY_CHECK]){
                            W.ViewWarning(nThread, W.ManualErrMassage, "트레이 피커 트레이 잡고 있음.");
                            break;
                        }
#if _NSS3300
                        if (!mIN[I.EMPTY_RAIL_LD_TRAY_CHECK]){
#else
                        if (mIN[I.EMPTY_RAIL_LD_TRAY_CHECK] && mIN[I.EMPTY_RAIL_ULD_TRAY_CHECK]){
#endif
                            W.ViewWarning(nThread, W.ManualErrMassage, "EMPTY TRANSFER TABLE TRAY 없음");
                            break;
                        }
                        if (eRTN.SUCESS != C.EmptyStacker.TransferGrip("Lock tray transfer grip")) break;
                        if (eRTN.SUCESS != C.EmptyStacker.TransferFwd("Forward tray transfer")) break;
                        C.TrayPk.Pic("매뉴얼 트레이 피커 트레이 픽업");
                        break;

                    case ManualNumber.StringBarcodeReading:
                        C.Gripper.ReadBarcode();
                        break;
                    #endregion

                    case ManualNumber.TopCamCalZigCenter:
                        if (!mIN[I.StageVac[iMANUAL.int_1]] && !bDRYRUN){
                            W.ViewWarning(nThread, W.ManualErrMassage, ((eMAP_BLOCK)iMANUAL.int_1).ToString() + "번 테이블 진공 센서 OFF 되어 있음");
                            break;
                        }
                        BASE.MoveCalZigCenter(nThread, (eMAP_BLOCK)DATA_.iMANUAL.int_1, (eMAP_BLOCK)DATA_.iMANUAL.int_1 + "번 켈리브레이션 지그 중심 위치로 이송");
                        break;

                    default: break;
                }
                EndManual();
            } while (true);
        }

        void EndManual(){
            eTIME = Environment.TickCount;
            IsDOUBLE[D.ManRunTime] = (cMATH.TimeMeasure(IsDOUBLE[D.CPUSpeed], (long)sTIME, (long)eTIME)) / 1000;
            bErrNotSave = false;
            bMF = false;
            try{
                if (iMANUAL.Number != 0 && iMANUAL.bLABEL)  lbDumy.BackColor = iMANUAL.BackColor1;
                if (iMANUAL.Number != 0 && iMANUAL.bBUTTON) btDumy.BackColor = iMANUAL.BackColor1;
            }
            catch (Exception ex) { System.Diagnostics.Trace.WriteLine("CLS_MANUAL -> END_MANUAL FAIL" + ETC.NewLine + ex.ToString()); }
            //if (IsBIT[B.SawManualRun]) C.Recieve.RunManual_Fail("FAIL MANUAL-RUN.");
        }

#region >> Homming
        bool CheckInitailSensor(){
            bool bFlog = true;
            iMANUAL.Message = "전체 초기화 : 초기화 전 센서 상태 확인! (CheckInitailSensor)";
            BASE.AddMessage(nThread, iMANUAL.Message);

            if (bFlog) iMANUAL.Message = "전체 초기화 : 초기화 전 센서 상태 확인 완료";
            else iMANUAL.Message = "전체 초기화 : 초기화 전 센서 상태 확인 실패";
            BASE.AddMessage(nThread, iMANUAL.Message);
            ConfirmUser[W.ProductRemove].msg = iMANUAL.Message/*sINIT*/;
            return bFlog;
        }

        bool InitialMachine(){
            iMANUAL.Message = "전체 초기화 : MACHINE INITIALIZE (InitialMachine) ";
            BASE.AddMessage(nThread, iMANUAL.Message);
            if (!I.CHK_EMO()) return false;
            if (!Cyclinder_1st()) return false;
            if (!Sensor_1st()) return false;

            bAllHomeComplete = false;
            bInitialComplete = false;
            for (int i = 0; i < CNT_.THREAD; i++) UseThread[i] = true; //디버깅 완료 후 쓰레드 활성화 삭제
            M.IniHomeBuffer();
            LAB_.ALL_ALARMCLEAR();
            UTIL_.DELAY(100);
            if (!LAB_.AlarmStatus(nThread, "iMANUAL.Message")){
                ConfirmUser[W.ProductRemove].msg = iMANUAL.Message + " Motor Alarm!";
                return false;
            }

            if (!Homming(Home_1st, "첫번째 모터 초기화")) return false;
            if (!Homming(Home_2nd, "두번째 모터 초기화")) return false;
            MachineVacuum();
            if (!Sensor_2nd()) return false;
            if (!Cyclinder_2nd()) return false;
            if (!Homming(Home_3nd, "")) return false;
            MachinVacuumOFf();
            bAllHomeComplete = true;
            return true;
        }

        public bool Cyclinder_1st(){
            iMANUAL.Message = "전체 초기화 : 실린더 첫번째 동작 (Cyclinder_1st) ";
            BASE.AddMessage(nThread, iMANUAL.Message);
            if (eRTN.SUCESS != C.Magazine.PusherBackward("PUSHER BACKWORD")){
                iMANUAL.Message = "PUSHER BACKWORD FAIL";
                BASE.AddMessage(nThread, iMANUAL.Message);
                ConfirmUser[W.ProductRemove].msg = iMANUAL.Message;
                return false;
            }

            if (eRTN.SUCESS != BASE.GoodTrayPreAlignBwd(nThread, "GOOD TRAY PRE-ALIGN BACKWARD")){
                iMANUAL.Message = "GOOD TRAY PRE-ALIGN BACKWARD FAIL";
                BASE.AddMessage(nThread, iMANUAL.Message);
                ConfirmUser[W.ProductRemove].msg = iMANUAL.Message;
                return false;
            }

            if (eRTN.SUCESS != BASE.GoodTrayFeederUnGrip(nThread, eTRAY.GOOD1, "GOOD TRAY1 FEEDER UNGRIP")){
                iMANUAL.Message = "GOOD TRAY FEEDER1 UNGRIP FAIL";
                BASE.AddMessage(nThread, iMANUAL.Message);
                ConfirmUser[W.ProductRemove].msg = iMANUAL.Message;
                return false;
            }
            if (eRTN.SUCESS != BASE.GoodTrayFeederUnGrip(nThread, eTRAY.GOOD2, "GOOD TRAY2 FEEDER UNGRIP")){
                iMANUAL.Message = "GOOD TRAY FEEDER2 UNGRIP FAIL";
                BASE.AddMessage(nThread, iMANUAL.Message);
                ConfirmUser[W.ProductRemove].msg = iMANUAL.Message;
                return false;
            }
            if (eRTN.SUCESS != C.EmptyStacker.StopperLock("EMPTY STOPPER LOCK")){
                iMANUAL.Message = "EMPTY TRAY STOPPER FAIL";
                BASE.AddMessage(nThread, iMANUAL.Message);
                ConfirmUser[W.ProductRemove].msg = iMANUAL.Message;
                return false;
            }
            if (eRTN.SUCESS != BASE.GoodTrayStackerDown(nThread, "GOOD TRAY STACKER TABLE DOWN")){
                iMANUAL.Message = "GOOD TRAY STACKER DOWN FAIL";
                BASE.AddMessage(nThread, iMANUAL.Message);
                ConfirmUser[W.ProductRemove].msg = iMANUAL.Message;
                return false;
            }
            if (eRTN.SUCESS != C.ReworkTrayFeeder.StackerTableDn("RE-WORK TRAY STACKER TABLE DOWN")){
                iMANUAL.Message = "EMPTY TRAY STOPPER FAIL";
                BASE.AddMessage(nThread, iMANUAL.Message);
                ConfirmUser[W.ProductRemove].msg = iMANUAL.Message;
                return false;
            }
            if (eRTN.SUCESS != BASE.BottomCameraCalibrationZig_Bwd(nThread, "CAMERA CALIBRATION ZIG BACKWARD")){
                iMANUAL.Message = "CAMERA CALIBRATION ZIG BACKWARD FAIL";
                BASE.AddMessage(nThread, iMANUAL.Message);
                ConfirmUser[W.ProductRemove].msg = iMANUAL.Message;
                return false;
            }
            BASE.AddMessage(nThread, iMANUAL.Message + " 완료!");
            return true;
        }
        public bool Cyclinder_2nd(){
            iMANUAL.Message = "전체 초기화 : 실린더 두번째 동작 (Cyclinder_2nd) ";
            BASE.AddMessage(nThread, iMANUAL.Message);

            if (!mIN[I.TRAY_PKR_TRAY_CHECK]){
                if (eRTN.SUCESS != C.TrayPk.UnClamp("트레이 피커 언클램프")){
                    iMANUAL.Message = "TRAY PICKER TRAY UNCLAMP FAIL";
                    BASE.AddMessage(nThread, iMANUAL.Message);
                    ConfirmUser[W.ProductRemove].msg = iMANUAL.Message;
                    return false;
                }
            }
#if _NSS3300
            if (!mIN[I.EMPTY_RAIL_LD_TRAY_CHECK]){
#else
            if (mIN[I.EMPTY_RAIL_LD_TRAY_CHECK] && mIN[I.EMPTY_RAIL_ULD_TRAY_CHECK]){
#endif

                if (eRTN.SUCESS != C.EmptyStacker.TransferUnGrip("빈-트레이 피터 트레이 언그립")){
                    iMANUAL.Message = "EMPTY TRAY FEEDER TRAY UNGRIP FAIL";
                    BASE.AddMessage(nThread, iMANUAL.Message);
                    ConfirmUser[W.ProductRemove].msg = iMANUAL.Message;
                    return false;
                }
            }
            if (eRTN.SUCESS != C.EmptyStacker.TransferBwd("빈-트레이 피터 후진")){
                iMANUAL.Message = "EMPTY TRAY FEEDER BACKWARD FAIL";
                BASE.AddMessage(nThread, iMANUAL.Message);
                ConfirmUser[W.ProductRemove].msg = iMANUAL.Message;
                return false;
            }
            if (eRTN.SUCESS != C.Gripper.UnGrip("그리퍼 스트립 언그립")){
                iMANUAL.Message = "GRIPPER STRIP UNGRIP FAIL";
                BASE.AddMessage(nThread, iMANUAL.Message);
                ConfirmUser[W.ProductRemove].msg = iMANUAL.Message;
                return false;
            }
            //IN-LET TABLE STRIP CHECK
            if (eRTN.SUCESS != C.Gripper.InLET_DOWN("인-렛 테이블 다운")){
                iMANUAL.Message = "IN-LET TABLE DOWN FAIL";
                BASE.AddMessage(nThread, iMANUAL.Message);
                ConfirmUser[W.ProductRemove].msg = iMANUAL.Message;
                return false;
            }
            //IN-LET TABLE STRIP CHECK
            if (eRTN.SUCESS != BASE.GoodTrayStackerDown(nThread, "GOOD TRAY STACKER TABLE DOWN")){
                iMANUAL.Message = "GOOD TRAY STACKER TABLE DOWN FAIL";
                BASE.AddMessage(nThread, iMANUAL.Message);
                ConfirmUser[W.ProductRemove].msg = iMANUAL.Message;
                return false;
            }
            if (eRTN.SUCESS != C.ReworkTrayFeeder.StackerTableDn("RE-WORK TRAY STACKER TABLE DOWN")){
                iMANUAL.Message = "RE-WORK TRAY STACKER TABLE DOWN FAIL";
                BASE.AddMessage(nThread, iMANUAL.Message);
                ConfirmUser[W.ProductRemove].msg = iMANUAL.Message;
                return false;
            }
            BASE.AddMessage(nThread, iMANUAL.Message + " 완료!");
            return true;
        }
        public bool Sensor_1st(){
            iMANUAL.Message = "전체 초기화 : 센서 확인 1 (Sensor_1st) ";
            BASE.AddMessage(nThread, iMANUAL.Message);
            if (MAP_.CheckStageStatus(eMAP_BLOCK.STAGE1, (int)prMODEL[RP.GroupCntX], (int)prMODEL[RP.GroupCntY], (int)prMODEL[RP.UnitCntX], (int)prMODEL[RP.UnitCntY])){
                BASE.StageVac(eMAP_BLOCK.STAGE1, stBIT.ON);
                B.SetBit(nThread, B.Stage1PickUp, true, "맵-블록 테이블1번 픽업 중에 초기화 진행하여 이여서 픽업 진행 플로그 ON");
            }
            if (MAP_.CheckStageStatus(eMAP_BLOCK.STAGE2, (int)prMODEL[RP.GroupCntX], (int)prMODEL[RP.GroupCntY], (int)prMODEL[RP.UnitCntX], (int)prMODEL[RP.UnitCntY])){
                BASE.StageVac(eMAP_BLOCK.STAGE2, stBIT.ON);
                B.SetBit(nThread, B.Stage2PickUp, true, "맵-블록 테이블2번 픽업 중에 초기화 진행하여 이여서 픽업 진행 플로그 ON");
            }
            if ((mIN[I.ELV_MZ_EXIST1] || mIN[I.ELV_MZ_EXIST2]) /*&& 로더 매거진 콘베어 투입단 센서*/){
                //if (eRTN.SUCESS != C.Magazine.UnClamp("매거진 언클램프")) return false;
                iMANUAL.Message = "로딩 엘리베이터 매거진 제거 후 다시 홈 실행!";
                BASE.AddMessage(nThread, iMANUAL.Message);
                ConfirmUser[W.ProductRemove].msg = iMANUAL.Message;
                return false;
            }
            BASE.AddMessage(nThread, iMANUAL.Message + " 완료!");
            return true;
        }
        public bool Sensor_2nd(){
            iMANUAL.Message = "전체 초기화 : 센서 확인 2 (Sensor_2nd) ";
            BASE.AddMessage(nThread, iMANUAL.Message);
            //in-let table strip check

            BASE.AddMessage(nThread, iMANUAL.Message + " 완료!");
            return true;
        }
        public bool Sensor_Vac(){
            iMANUAL.Message = "전체 초기화 : 진공 센서 체크 (Sensor_Vac) ";
            BASE.AddMessage(nThread, iMANUAL.Message);

            BASE.AddMessage(nThread, iMANUAL.Message + " 완료!");
            return true;
        }
        public bool Homming(int[] mt, string comment){
            if (eMCStatus != eMachineStatus.INITIAL) return false;
            bool bStart = mt.Length > 0 ? true : false;
            if (bStart){
                BASE.AddMessage(nThread, comment + " 진행");
                for (int m = 0; m < mt.Length; m++){
                    mtSTS[mt[m]].strHome = "HOMMING...";
                    LAB_.MT_HOME(mt[m]);
                }
                UTIL_.DELAY(500);
                for (int m = 0; m < mt.Length; m++){
                    if (!enableHome[mt[m]]) continue;
                    if (!LAB_.WAIT_HOME(mt[m])){
                        ConfirmUser[W.ProductRemove].msg = comment + "FAIL!";
                        BASE.AddMessage(nThread, comment + "FAIL!");
                        return false;
                    }
                }
                BASE.AddMessage(nThread, comment + " 완료");
            }
            return true;
        }

        void MachineVacuum(){
            //strip picker
            if (mIN[I.STRIP_PK_VAC]){
                LAB_.OUTPUT(O.STRIP_PK_BLOW, false);
                LAB_.OUTPUT(O.STRIP_PK_VAC, true);
#if _NSS3300
#else
                LAB_.OUTPUT(O.STRIP_PK_PURGE, false);
                LAB_.OUTPUT(O.STRIP_PK_VAC_OFF, false);
#endif
            }

            //unit picker
            if (mIN[I.UNIT_PK_VAC]){
                LAB_.OUTPUT(O.UNIT_PK_BLOW, false);
                LAB_.OUTPUT(O.SCRAP_BLOW_1, false);
                LAB_.OUTPUT(O.SCRAP_BLOW_2, false);

                LAB_.OUTPUT(O.UNIT_PK_VAC, true);
                LAB_.OUTPUT(O.SCRAP_VAC_1, true);
                LAB_.OUTPUT(O.SCRAP_VAC_2, true);
#if _NSS3300
#else
                LAB_.OUTPUT(O.UNIT_PK_VAC_OFF, false);
                LAB_.OUTPUT(O.SCRAP1_VAC_OFF, false);
                LAB_.OUTPUT(O.SCRAP2_VAC_OFF, false);
#endif
            }

            //stage
            for (int i = 0; i < 2; i++){
                if (mIN[I.StageVac[i]]){
                    LAB_.OUTPUT(O.StageVac[i], true);
                    LAB_.OUTPUT(O.StageDrain[i], true);
                    LAB_.OUTPUT(O.StageBackVac[i], false);
                }
            }
        }
        void MachinVacuumOFf(){
            if (!mIN[I.STRIP_PK_VAC]){
                LAB_.OUTPUT(O.STRIP_PK_VAC, false);
#if _NSS3300
#else
                LAB_.OUTPUT(O.STRIP_PK_VAC_OFF, true);
#endif
            }
            if (!mIN[I.UNIT_PK_VAC]){
                LAB_.OUTPUT(O.UNIT_PK_VAC, false);
                LAB_.OUTPUT(O.SCRAP_VAC_1, false);
                LAB_.OUTPUT(O.SCRAP_VAC_2, false);
#if _NSS3300
#else
                LAB_.OUTPUT(O.UNIT_PK_VAC_OFF, true);
                LAB_.OUTPUT(O.SCRAP1_VAC_OFF, true);
                LAB_.OUTPUT(O.SCRAP2_VAC_OFF, true);
#endif
            }
            if (!mIN[I.STAGE_VACUUM1] && !IsBIT[B.Stage1PickUp]){
                LAB_.OUTPUT(O.STAGE1_VAC, false);
                LAB_.OUTPUT(O.STAGE1_DRAIN, false);
            }
            if (!mIN[I.STAGE_VACUUM2] && !IsBIT[B.Stage2PickUp]){
                LAB_.OUTPUT(O.STAGE2_VAC, false);
                LAB_.OUTPUT(O.STAGE2_DRAIN, false);
            }
        }

        public eRTN MoveFirstStandbyPos(int nThread, string comment){
            if (BASE.ChkRunning(nThread)) return eRTN.FAIL;
            int[] mt = {
                M.ElvZ, M.StripPkZ, M.UnitPkZ, M.TopVisionZ, M.TopVisionX, M.BtnVisionY, M.BtnVisionZ, M.TrayPickerZ, M.EmptyElv, M.TRIGGER1, M.TRIGGER2, M.X1T, M.X2T
            };
            int[] pos = new int[mt.Length];
            double[] Toller = new double[mt.Length];
            bool[] OnlyStart = new bool[mt.Length];
            bool[] NoChange = new bool[mt.Length];
            bool[] DontStop = new bool[mt.Length];
            string[] cmds = new string[mt.Length];
            for (int i = 0; i < mt.Length; i++){
                pos[i] = P.Ready;
                Toller[i] = 0.01;
                OnlyStart[i] = false;
                NoChange[i] = false;
                DontStop[i] = false;
                cmds[i] = "";
            }
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, mt, pos, Toller, OnlyStart, NoChange, DontStop, cmds, comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN MoveSecondStandbyPos(int nThread, string comment){
            if (BASE.ChkRunning(nThread)) return eRTN.FAIL;
#if _NSS3300
            int[] mt ={
                M.ElvY, M.GrpX, M.Rail, M.Barcode, M.StripPkX, M.UnitPkX, M.Table1, M.Table2, M.TrayPickerX, M.TrayFeeder1, M.TrayFeeder2, M.TrayFeeder3
            };

#else
            int[] mt ={
                M.ElvY, M.GrpX, M.RailF, M.RailR, M.Barcode, M.StripPkX, M.PreAlign, M.UnitPkX, M.Table1, M.Table2, M.TrayPickerX, M.TrayFeeder1, M.TrayFeeder2, M.TrayFeeder3
            };
            
#endif
            int[] pos = new int[mt.Length];
            double[] Toller = new double[mt.Length];
            bool[] OnlyStart = new bool[mt.Length];
            bool[] NoChange = new bool[mt.Length];
            bool[] DontStop = new bool[mt.Length];
            string[] cmds = new string[mt.Length];
            for (int i = 0; i < mt.Length; i++){
                pos[i] = P.Ready;
                Toller[i] = 0.01;
                OnlyStart[i] = false;
                NoChange[i] = false;
                DontStop[i] = false;
                cmds[i] = "";
            }
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, mt, pos, Toller, OnlyStart, NoChange, DontStop, cmds, comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public void CheckPicker(){
            for (int i = 0; i < CNT_.PKR; i++){
                //HEAD1 CHECK
                if (mAI[i] > mSET_AI[i] && PK_Z[i] != eSTATUS.NONE){
                    PICKER[(int)eHD.HD1].finger[i].valid = true;
                    if (PICKER[(int)eHD.HD1].finger[i].iResult != (int)eSTATUS.MARK /*== (int)eSTATUS.EMPTY*/){
                        PICKER[(int)eHD.HD1].finger[i].iResult = (int)eSTATUS.NG;
                    }
                    BASE.PkVac(eHD.HD1, (ePK)i, stBIT.ON, false, "");
                    B.Bit(nThread, B.X1PkPlc, true, "초기화 진행 후 X1 피커에 유닛 잡고 있어 REJECT BOX에 버리라고 요청 플러그 ON");
                } //진공 ON됨
                else{
                    PICKER[(int)eHD.HD1].finger[i].valid = false;
                    PICKER[(int)eHD.HD1].finger[i].iResult = (int)eSTATUS.EMPTY;
                    BASE.PkVac(eHD.HD1, (ePK)i, stBIT.OFF, false, "");
                }

                //HEAD2 CHECK
                if (mAI[i + 8] > mSET_AI[i + 8] && PK_Z[i + (CNT_.PKR * 1)] != eSTATUS.NONE){
                    PICKER[(int)eHD.HD1].finger[i].valid = true;
                    if (PICKER[(int)eHD.HD2].finger[i].iResult != (int)eSTATUS.MARK /*== (int)eSTATUS.EMPTY*/){
                        PICKER[(int)eHD.HD2].finger[i].iResult = (int)eSTATUS.NG;
                    }
                    BASE.PkVac(eHD.HD2, (ePK)i, stBIT.ON, false, "");
                    B.Bit(nThread, B.X2PkPlc, true, "초기화 진행 후 X2 피커에 유닛 잡고 있어 REJECT BOX에 버리라고 요청 플러그 ON");
                } //진공 ON됨
                else{
                    PICKER[(int)eHD.HD2].finger[i].valid = false;
                    PICKER[(int)eHD.HD2].finger[i].iResult = (int)eSTATUS.EMPTY;
                    BASE.PkVac(eHD.HD2, (ePK)i, stBIT.OFF, false, "");
                }
            }
        }

        public bool RunPlaceZoneConveryorUnloading(){
#if _NSS3300
            if (!mIN[I.GOOD_RAIL_CONVEYOR_CHECK]){
                return true;
            }

#else
            if (mIN[I.GOOD_RAIL_HEAD1_CHECK] && mIN[I.GOOD_RAIL_HEAD2_CHECK] && mIN[I.GOOD_RAIL_CONVEYOR_CHECK]){
                return true;
            }
#endif

            if (eRTN.SUCESS != BASE.GoodTrayFeederUnGrip(nThread, eTRAY.GOOD1, "GOOD TRAY TRANSFER1 TRAY UNGRIP")){
                ConfirmUser[W.ProductRemove].msg = "GOOD FEEDER 1 트레이 이송부 그리퍼 언락 실패. 실린더 확인 바랍니다.";
                return false;
            }
            if (eRTN.SUCESS != BASE.GoodTrayFeederFrontUnGrip(nThread, eTRAY.GOOD2, "GOOD TRAY TRANSFER2 TRAY UNGRIP")){
                ConfirmUser[W.ProductRemove].msg = "GOOD FEEDER 2 트레이 이송부 그리퍼 언락 실패. 실린더 확인 바랍니다.";
                return false;
            }
            if (eRTN.SUCESS != BASE.MoveFeederY(nThread, eTRAY.GOOD1, P.TrayLoad, "offset=-50", "Move good tray transfer y1 tray loading position")){
                ConfirmUser[W.ProductRemove].msg = "OK1 트레이 이송부 트레이 투입 위치로 이송 중 에러 발생";
                return false;
            }
            if (!mIN[I.ULD_CONV_READY]){
                ConfirmUser[W.ProductRemove].msg = "콘베어 READY 상태 확인 바랍니다. GOOD 트레이 배출 할 수 없습니다. GOOD 레일부 트레이 모두 제거 하셔야합니다.";
                return false;
            }
            if (eRTN.SUCESS != BASE.GoodTrayFeederFrontGrip(nThread, eTRAY.GOOD1, "Lock tray transfer1 front grip")){
                ConfirmUser[W.ProductRemove].msg = "OK1 트레이 이송부 상부 그리퍼 락 실패. 그리퍼 락 실린더 확인 바랍니다.";
                return false;
            }
            if (!mIN[I.ULD_CONV_READY]){
                ConfirmUser[W.ProductRemove].msg = "초기화 실패! = 배출 콘베어 런 상태 아닙니다. (READY 신호 안들어옴)";
                return false;
            }
            if (eRTN.SUCESS != BASE.MoveFeederY(nThread, eTRAY.GOOD1, P.TrayUnload, "", "Move tray transfer y1 tray conveyor unlaodgin")){
                ConfirmUser[W.ProductRemove].msg = "OK1 트레이 이송부 트레이 콘베어 배출 위치로 이송 중 에러 발생";
                return false;
            }
            if (eRTN.SUCESS != BASE.GoodTrayFeederUnGrip(nThread, eTRAY.GOOD1, "Unlock tray transfer1 grip")){
                ConfirmUser[W.ProductRemove].msg = "OK1 트레이 이송부 그리퍼 언락 실패. 그리퍼 언락 실린더 확인 바랍니다.";
                return false;
            }
            mOUT[O.UldConveyorTrayUnloading] = true;
            UTIL_.DELAY(100);
            mOUT[O.UldConveyorTrayUnloading] = false;
            if (eRTN.SUCESS != BASE.MoveFeederY(nThread, eTRAY.GOOD1, P.TrayLoad, "", "Move good tray transfer y1 tray loading position")){
                ConfirmUser[W.ProductRemove].msg = "OK1 트레이 이송부 트레이 공급 위치로 이송 중 에러 발생";
                return false;
            }
            return true;
        }
        public bool RunTrayConveyorUnloading(){
            if (eRTN.SUCESS != BASE.GoodTrayFeederUnGrip(nThread, eTRAY.GOOD1, "GOOD TRAY TRANSFER1 TRAY UNGRIP")){
                ConfirmUser[W.ProductRemove].msg = "GOOD FEEDER 1 트레이 이송부 그리퍼 언락 실패. 실린더 확인 바랍니다.";
                return false;
            }
            if (eRTN.SUCESS != BASE.GoodTrayFeederFrontUnGrip(nThread, eTRAY.GOOD2, "GOOD TRAY TRANSFER2 TRAY UNGRIP")){
                ConfirmUser[W.ProductRemove].msg = "GOOD FEEDER 2 트레이 이송부 그리퍼 언락 실패. 실린더 확인 바랍니다.";
                return false;
            }
            if (eRTN.SUCESS != BASE.MoveFeederY(nThread, eTRAY.GOOD1, P.TrayLoad, "", "Move good tray transfer y1 tray loading position")){
                ConfirmUser[W.ProductRemove].msg = "OK1 트레이 이송부 트레이 투입 위치로 이송 중 에러 발생";
                return false;
            }
            if (eRTN.SUCESS != BASE.GoodTrayFeederGrip(nThread, eTRAY.GOOD1, "GOOD TRAY TRANSFER1 TRAY GRIP")){
                ConfirmUser[W.ProductRemove].msg = "GOOD FEEDER 1 트레이 이송부 그리퍼 락 실패. 실린더 확인 바랍니다.";
                return false;
            }
            if (eRTN.SUCESS != BASE.MoveFeederY(nThread, eTRAY.GOOD1, P.FastPsh, "", "GOOD FEEDER1번 언로딩 푸셔 위치 이송")){
                ConfirmUser[W.ProductRemove].msg = "GOOD FEEDER1번 트레이 이송부 트레이 배출 위치로 이송 중 에러 발생";
                return false;
            }
            if (eRTN.SUCESS != BASE.GoodTrayFeederUnGrip(nThread, eTRAY.GOOD1, "GOO TRAY FEEDER 트레이 언그립")){
                ConfirmUser[W.ProductRemove].msg = "GOOD FEEDER1번 트레이 언그립 에러 발생";
                return false;
            }
            if (eRTN.SUCESS != BASE.MoveFeederY(nThread, eTRAY.GOOD1, P.Psh, "", "GOOD TRAY FEEDER1 트레이 푸셔 시작 위치 이송")){
                ConfirmUser[W.ProductRemove].msg = "GOOD TRAY FEEDER1 트레이 푸셔 시작 위치 이송 에러 발생";
                return false;
            }
            if (eRTN.SUCESS != BASE.GoodTrayFeederFrontGrip(nThread, eTRAY.GOOD1, "Lock tray transfer1 front grip")){
                ConfirmUser[W.ProductRemove].msg = "OK1 트레이 이송부 상부 그리퍼 락 실패. 그리퍼 락 실린더 확인 바랍니다.";
                return false;
            }
            if (!mIN[I.ULD_CONV_READY]){
                ConfirmUser[W.ProductRemove].msg = "초기화 실패! = 배출 콘베어 런 상태 아닙니다. (READY 신호 안들어옴)";
                return false;
            }
            if (eRTN.SUCESS != BASE.MoveFeederY(nThread, eTRAY.GOOD1, P.TrayUnload, "", "Move tray transfer y1 tray conveyor unlaodgin")){
                ConfirmUser[W.ProductRemove].msg = "OK1 트레이 이송부 트레이 콘베어 배출 위치로 이송 중 에러 발생";
                return false;
            }
            if (eRTN.SUCESS != BASE.GoodTrayFeederUnGrip(nThread, eTRAY.GOOD1, "Unlock tray transfer1 grip")){
                ConfirmUser[W.ProductRemove].msg = "OK1 트레이 이송부 그리퍼 언락 실패. 그리퍼 언락 실린더 확인 바랍니다.";
                return false;
            }
            mOUT[O.UldConveyorTrayUnloading] = true;
            UTIL_.DELAY(100);
            mOUT[O.UldConveyorTrayUnloading] = false;
            if (eRTN.SUCESS != BASE.MoveFeederY(nThread, eTRAY.GOOD1, P.TrayLoad, "", "Move good tray transfer y1 tray loading position")){
                ConfirmUser[W.ProductRemove].msg = "OK1 트레이 이송부 트레이 공급 위치로 이송 중 에러 발생";
                return false;
            }
            return true;
        }
        public bool UnloaidngReWorkTray(string comment){
            BASE.AddMessage(nThread, comment + "START!");
            if (eRTN.SUCESS != C.ReworkTrayFeeder.MoveY(P.Staker, "", "RE-WORK 트레이 스태커 배출 위치 이송")){
                ConfirmUser[W.ProductRemove].msg = "RE-WORK 트레이 스태커 배출 위치 이송 실패!";
                BASE.AddMessage(nThread, comment + "RE-WORK 트레이 스태커 배출 위치 이송 실패!");
                return false;
            }
            if (eRTN.SUCESS != C.ReworkTrayFeeder.UnGrip("RE-WORK TRAY FEEDER TRAY UNGRIP")){
                ConfirmUser[W.ProductRemove].msg = "RE-WORK TRAY FEEDER TRAY UNGRIP 실패!";
                BASE.AddMessage(nThread, comment + "RE-WORK TRAY FEEDER TRAY UNGRIP 실패!");
                return false;
            }
            if (eRTN.SUCESS != C.ReworkTrayFeeder.StackerTableUp("RE-WORK 트레이 스태커 테이블 업")){
                ConfirmUser[W.ProductRemove].msg = "RE-WORK 트레이 스태커 테이블 업 실패!";
                BASE.AddMessage(nThread, comment + "RE-WORK 트레이 스태커 테이블 업 실패!");
                return false;
            }
            if (eRTN.SUCESS != C.ReworkTrayFeeder.StackerTableDn("RE-WORK 트레이 스태커 테이블 다운")){
                ConfirmUser[W.ProductRemove].msg = "RE-WORK 트레이 스태커 테이블 다운 실패!";
                BASE.AddMessage(nThread, comment + "RE-WORK 트레이 스태커 테이블 다운 실패!");
                return false;
            }
            IsLONG[L.ReworkTrayCnt]++;
            if (eRTN.SUCESS != C.ReworkTrayFeeder.MoveY(P.TrayLoad, "", "RE-WORK 트레이 트레이 공급 위치 이송")){
                ConfirmUser[W.ProductRemove].msg = "RE-WORK 트레이 트레이 공급 위치 이송 실패!";
                BASE.AddMessage(nThread, comment + "RE-WORK 트레이 트레이 공급 위치 이송 실패!");
                return false;
            }
            BASE.AddMessage(nThread, sINIT + "SUCESS!");
            return true;
        }
#endregion
    }

    public class MANUAL_REPEAT : ManualNumber{
        int n1 = 0, n2 = 0;
        readonly int[] FIRST = { MGZClamp, Pusher, InLetTable, Gripper, CleanerSwing, BtmCamCalZig, GoodTray1_Clamp, GoodTray2_Clamp, GoodTrayStackerTable, ReworkTray_Clamp, ReworkTrayStackerTable, TrayPkClamp, EmptyStackerStopper, EmptyTrayClamp, EmptyTrayFeeder, RailWork };
        readonly int[] SECOND = { MGZClamp, Pusher, InLetTable, Gripper, CleanerSwing, BtmCamCalZig, GoodTray1_Clamp, GoodTray2_Clamp, GoodTrayStackerTable, ReworkTray_Clamp, ReworkTrayStackerTable, TrayPkClamp, EmptyStackerStopper, EmptyTrayClamp, EmptyTrayFeeder, RailRdy };

        public void DoRepeat(){
            do{
                if (DATA_.gExit) break;
                UTIL_.DELAY(10);
                if ((DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.DRY) || !DATA_.bMANUAL_REPEAT) continue;

                n1 = DATA_.iMANUAL.Number;
                n2 = GetSecondNumber(n1);
                if (n2 < 0) continue;

                while (DATA_.bMANUAL_REPEAT){
                    COM_.RunRepaeatManual(n1, n2);
                }
            } while (true);
        }

        int GetSecondNumber(int n){
            for (int i = 0; i < FIRST.Length; i++) if (n == FIRST[i]) return SECOND[i];
            for (int i = 0; i < FIRST.Length; i++) if (n == SECOND[i]) return FIRST[i];
            return -1;
        }
    }
}