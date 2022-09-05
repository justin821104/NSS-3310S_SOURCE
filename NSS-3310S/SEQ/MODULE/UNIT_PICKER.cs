using Object;
using System;
using NSS_3310S.ITS;
using LIB_.DateType;

namespace NSS_3310S.SEQ.MODULE{
    public class UNIT_PICKER : BASE{
        int nThread = T.UnitPk;
        string cmds = string.Empty;
        long TackStart = 0, TackEnd = 0;
        bool bPicSignel = false;

        bool CheckRunThread(){
            if (eMCStatus != eMachineStatus.AUTO){
                UTIL_.DELAY(100);
                return false;
            }
            return true;
        }
        public void DoAuto(){
            do{
                if (gExit) break;
                if (!CheckRunThread()) continue;

            RePIC:
                while (UTIL_.WaitInput(nThread, I.SAW_ULD_REQ, false, "다이싱에서 유닛 배출 요청 할때까지 대기")){
                    if (mIN[I.UNIT_PK_VAC] /*|| bDRYRUN*/){
                        while (UTIL_.WaitBIT(nThread, B.UnitPickupStop, B.StripPkPlc, true, true, "유닛 피커 스크랩 파기 작업 대기", stBIT.OR)) ;
                        COM_.SetBit(nThread, B.UnitPkPic, B.UnitPkMask, true, true, "유닛 피커 작업 스크랩 파기 작업 진행");
                        goto UnitPlace;
                    }
                    if (prMACHINE[CP.UseLotEnd] == (int)eUSE.USE && IsBIT[B.CstRequest] && !IsBIT[B.InRailRequest] && !IsBIT[B.GripperWorking] && !IsBIT[B.StripPkRequest] && !mIN[I.SAW_CUTTING] && !IsBIT[B.Stage1Working] && !IsBIT[B.Stage2Working] && !IsBIT[B.X1Working] && !IsBIT[B.X2Working] && !IsBIT[B.TrayPkWorking]){
                        COM_.ViewWarning(nThread, W.LotEndComplete);
                        while (UTIL_.WaitWarning(nThread, W.LotEndComplete, "LOT-END 처리")) ;
                        if (ConfirmUser[W.LotEndComplete].result) {
                            // lot count 저장!
                            TEACH_.DEL_STRIP_INFO();
                            MsSQL.bITSDataReading = false;
                            COM_.SetBit(nThread, B.LotEnd, true, "LOT-END 처리 진행");
                        ChkTrayPk:
                            if (IsBIT[B.TrayPkPic] || IsBIT[B.TrayPkWorking]){
                                UTIL_.DELAY(500);
                                goto ChkTrayPk;
                            }
                            if (IsBIT[B.GoodTrayWork]) {
                                COM_.Bit(nThread, B.GoodTrayUnloadingMode, true, "LOT-END 진행 GOOD 트레이 배출 플로그 ON");
                                COM_.Bit(nThread, B.GoodTrayWork, false, "LOT-END 진행 중 GOOD 트레이 작업 플러그 OFF");
                            }
                            if (IsBIT[B.ReWorkTrayWork]) COM_.Bit(nThread, B.ReWorkTrayWork, false, "REWORK 트레이 작업 플러그 OFF");
                        ChkTrayUnlaoading:
                            if (IsBIT[B.GoodTray1Place] || IsBIT[B.GoodTray2Place] || IsBIT[B.ReWorkTrayWork] || IsBIT[B.GoodTray1Unloading] || IsBIT[B.GoodTray2Unloading]){
                                UTIL_.DELAY(500);
                                goto ChkTrayUnlaoading;
                            }
                            if (prMACHINE[CP.UseMES] == (int)eUSE.USE){
                                SUBFRM_.gSecsGem.SetLotComplete((int)IsLONG[L.StripCnt]);
                            }
                            LogWR_.SaveLotEnd(CLOT.GET_LOT.LotID, CLOT.GET_LOT.ItsID, (int)IsLONG[L.StripCnt], (int)IsLONG[L.GoodCnt], (int)IsLONG[L.ReworkCnt], (int)IsLONG[L.NGCnt], (int)IsLONG[L.ITSCount]);
                            CLOT.FinishLot(false);
                            UTIL_.DEL_LOT_INFO();
                            bWriteLotInfo = true; // LOT 수량 정보 리셋 !
                            mIN[I.vtStop] = true;
                            LogWR_.SaveLogOperate("LOT-END SIGNAL ON-OFF", "MC");
                            UTIL_.DELAY(2000);
                            bLotEndProcess = true;
                            while (bLotEndProcess) UTIL_.DELAY(100);
                            COM_.Bit(nThread, B.CstRequest, false, "카세트 공급/배출 플러그 OFF");
                        } //LOT-END 처리!
                        else{
                            COM_.Bit(nThread, B.CstRequest, false, "카세트 공급/배출 플러그 OFF");
                        } //매거진 투입!
                    } //LOT-END 처리
                }
                while (UTIL_.WaitBIT(nThread, B.UnitPickupStop, B.StripPkPlc, true, true, "유닛 피커 다이싱 유닛 픽업 작업 대기", stBIT.OR)) ;
                COM_.SetBit(nThread, B.UnitPkPic, true, "유닛 피커 작업 유닛 픽업 작업 진행");
                if (!Pic("다이싱에서 유닛 픽업")) goto RePIC;
            UnitPlace:
                if (!Scrap("스크랩 제거")){
                    COM_.SetBit(nThread, B.UnitPkMask, false, "유닛 피커 작업 진행");
                    goto RePIC;
                }
                Brush("유닛 브러쉬 작업");
                Cleaner("하부 세척 작업");
                AirShower("유닛 에어샤워 작업");
                Plc("맵-블록에 유닛 플레이스");
            } while (true);
        }

        #region >> SEQ
        public bool Pic(string comment){
            if (mIN[I.UNIT_PK_VAC]){
                while (UTIL_.WaitBIT(nThread, B.UnitPickupStop, B.StripPkPlc, true, true, "유닛 피커 스크랩 파기 작업 대기", stBIT.OR)) ;
                return true;
            }

            LogStart(nThread, comment + " 진행");
            while (eRTN.SUCESS != MoveX(P.UnitPckUp, "", "유닛 피커 X축 유닛 픽업 위치 이송")){
                if (bPicSignel){
                    bPicSignel = false;
                    while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
                    while (eRTN.SUCESS != MoveX(P.Ready, "", "유닛 피커 X축 대기 위치 이송")) ;
                    COM_.SetBit(nThread, B.UnitPkPic, false, "다이싱 유닛 픽업 요청 신호 OFF됨");
                    return false;
                }
            }
            while (UTIL_.WaitInput(nThread, I.SAW_ULD_POS, false, "다이싱 언로더 위치에 와있을때까지 대기")) ;
            IsDOUBLE[D.UnitPk_PicOffsetX] = TEACH_.READ_OFFSET_FILE();
            while (eRTN.SUCESS != MoveX(P.UnitPckUp, "offset=" + string.Format("{0:0.000}", IsDOUBLE[D.UnitPk_PicOffsetX] /** -1*/), comment + "UNIT PICKER X AXIS SAW STAGE PICKUP POSITION MOVING"));

            while (eRTN.SUCESS != MoveZ(P.UnitPckUp, "offset=-10", "유닛 피커 Z축 유닛 픽업 대기 위치 이송")) ;
            while (eRTN.SUCESS != MoveZ(P.UnitPckUp, "spd=10", "유닛 피커 Z축 유닛 픽업 위치 이송")) ;
            if (prMODEL[RP.ScrapVacuum] == (int)eScrapVacuum.USE) AllVac(stBIT.ON);
            else Vac(stBIT.ON);
            while (UTIL_.WaitInput(nThread, I.SAW_STAGE_BLOW, false, "다이싱 테이블 블로우 ")) ;
            RePick:
            if (!mIN[I.UNIT_PK_VAC] && !bDRYRUN){
                while (eRTN.SUCESS != MoveZ(P.Ready, "", "Move unit picker z reaady position (unit pickup fail!)")) ;
                if (bMF) return false;
                COM_.ViewWarning(nThread, W.UnitPkrPickUpReCheck);
                while (UTIL_.WaitWarning(nThread, W.UnitPkrPickUpReCheck, "유닛피커 픽업 재시도 할 것지 자재 유실 처리로 종료 할 것지 선택 기다림")) ;
                if (ConfirmUser[W.UnitPkrPickUpReCheck].result){
                    if (mIN[I.UNIT_PK_VAC]) goto RePick;
                    while (eRTN.SUCESS != MoveX(P.UnitPckUp, "", "MOVE UNIT PICKER X AXIS SAW STAGE PICK-UP POS")){
                        if (bPicSignel){
                            bPicSignel = false;
                            while (eRTN.SUCESS != MoveZ(P.Ready, "", "Move unit picker z reaady position (unit pickup signal off!)")) ;
                            while (eRTN.SUCESS != MoveX(P.Ready, "", "Move unit picker ready position")) ;
                            ResetInterface();
                            return false;
                        }
                    };
                    if (mIN[I.UNIT_PK_VAC]) goto RePick;
                    while (eRTN.SUCESS != MoveZ(P.UnitPckUp, "offset=-5", "MOVE UNIT PICKER Z AXIS SAW STAGE PICK-UP POS -> 1st DOWN")) ;
                    while (eRTN.SUCESS != MoveZ(P.UnitPckUp, "spd=10", "MOVE UNIT PICKER Z AXIS SAW STAGE PICK-UP POS")) ;
                    AllVac(stBIT.ON);
                    SawStageVac(nThread, false);
                    UTIL_.DELAY(2000);
                    SawStageBlow(nThread, true);
                    UTIL_.DELAY(1000);
                    SawStageBlow(nThread, false);
                    UTIL_.DELAY(500);
                    if (!mIN[I.UNIT_PK_VAC]){
                        AllVac(stBIT.OFF);
                        SawStageVac(nThread, true);
                        UTIL_.DELAY(1000);
                        AllBlow();
                    }
                    cmds = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.UnitPkCheckUpPitch]) + ":spd=5";
                    while (eRTN.SUCESS != MoveZ(P.UnitPckUp, cmds, "Move unit picker z saw stage blow off signal position")) ;
                    goto RePick;
                } //재시도
                else{
                    while (eRTN.SUCESS != MoveX(P.Ready, "", "Move unit picker ready position")) ;
                    if (mIN[I.UNIT_PK_VAC]) goto RePick;
                    LAB_.BIT_OUT(O.HANDLER_SCRAP_CHECK, true);
                    UTIL_.DELAY(500);
                    LAB_.BIT_OUT(O.HANDLER_SCRAP_CHECK, false);
                    while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
                    COM_.SetOutput(nThread, O.HANDLER_UNIT_COMPLETE, true, "유닛 피커 유닛 픽업 중 유닛 유실되어 다시 처음부터 픽업");
                    while (mIN[I.SAW_ULD_REQ] || mIN[I.SAW_ULD_POS] || mIN[I.SAW_STAGE_BLOW]) ;
                    ResetInterface();
                    while (mOUT[O.UNIT_PK_VAC] || mOUT[O.SCRAP_VAC_1] || mOUT[O.SCRAP_VAC_2]){
                        UTIL_.OnERROR(E.emsNotUnitPk_VacOff, 500);
                    }
                    COM_.Bit(nThread, B.UnitPkPic, false, "유닛 피커 픽업 유실 처리됨");
                    return false;
                } //유실 처리 처음 부터.
            }
            cmds = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.UnitPkCheckUpPitch]) + ":spd=5";
            while (eRTN.SUCESS != MoveZ(P.UnitPckUp, cmds, "유닛 피커 Z축 유닛 픽업 대기 위치 이송")) ;
            while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
            ChkPic();

        ReCHECK_UNITPKR:
            if (!mIN[I.UNIT_PK_VAC] && !bDRYRUN){
                COM_.ViewWarning(nThread, W.UnitPkrPickUnitCheck);
                while (UTIL_.WaitWarning(nThread, W.UnitPkrPickUnitCheck, "유닛피커 픽업 재시도 할 것지 자재 유실 처리로 종료 할 것지 선택 기다림")) ;
                if (ConfirmUser[W.UnitPkrPickUnitCheck].result) goto ReCHECK_UNITPKR;
                else{
                    while (eRTN.SUCESS != MoveX(P.Ready, "", "Move unit picker ready position")) ;
                    if (mIN[I.UNIT_PK_VAC]) goto ReCHECK_UNITPKR;
                    while (mOUT[O.UNIT_PK_VAC] || mOUT[O.SCRAP_VAC_1] || mOUT[O.SCRAP_VAC_2]){
                        UTIL_.OnERROR(E.emsNotUnitPk_VacOff, 500);
                    }
                    return false;
                }
            }
            COM_.SetBit(nThread, B.UnitPkMask, true, "유닛 피커 작업 진행");
            CLOT.SEND_SAW_STRIP_INFO(nThread);
            LogOneCycle(CLOT.InfoStrip[nThread].Barcode);
            if (prMACHINE[CP.UseMES] == (int)eUSE.USE){
                SUBFRM_.gSecsGem.SetPanelModuleOut(CLOT.SawStageStripIndex, CLOT.SawStageStripBarcode, CMES.ModuleID.SAW_STAGE);
                SUBFRM_.gSecsGem.SetPanelModuleIn(CLOT.InfoStrip[nThread].Index, CLOT.InfoStrip[nThread].Barcode, CMES.ModuleID.UNIT_PK);
            }
            CLOT.RESET_SAW_STRIP_INFO();
            IsLONG[L.UnitCnt]++;
            LogEnd(nThread, comment + " 완료");
            return true;
        }

        public bool Scrap(string comment){
            bool bReturn = true;
        //if (prMACHINE[CP.UseScrapVacCheck] != (int)eUSE.USE){
        //    COM_.SetBit(nThread, B.UnitPkPic, false, "유닛 피커 유닛 픽업 완료");
        //    while (eRTN.SUCESS != MoveX(P.BrushStart, "", "유닛 피커 X축 브러쉬 시작 위치 이송")) ;
        //    return bReturn;
        //}
        ReCHECK_UNITPKR:
            if (!mIN[I.UNIT_PK_VAC] && !bDRYRUN){
                COM_.ViewWarning(nThread, W.UnitPkrPickUnitCheck);
                while (UTIL_.WaitWarning(nThread, W.UnitPkrPickUnitCheck, "유닛피커 픽업 재시도 할 것지 자재 유실 처리로 종료 할 것지 선택 기다림")) ;
                if (ConfirmUser[W.UnitPkrPickUnitCheck].result) goto ReCHECK_UNITPKR;
                else return false;
            }

            LogStart(nThread, comment + " 진행");
        //ReCheck_Scrap:
            if (!bDRYRUN){
                if (prMACHINE[CP.UseScrapVacCheck] == (int)eUSE.USE && (!mIN[I.SCRAP_VAC1] || !mIN[I.SCRAP_VAC2])){
                    UTIL_.OnERROR(E.ScrapPickUpFail);
                    //goto ReCheck_Scrap;
                }
            }

            while (eRTN.SUCESS != MoveZ(P.Scrap1, "", "유닛 피커 Z축 스크랩 버리는 위치 이송")) ;
            for (int i = 0; i < P.SCRAP.Length; i++){
                while (eRTN.SUCESS != MoveX(P.SCRAP[i], "", "유닛 피커 X축 스크랩 버리는 " + (i + 1).ToString("00") + " 위치 이송")) ;
                while (eRTN.SUCESS != MoveZ(P.SCRAP[i], "", "유닛 피커 Z축 스크랩 버리는 " + (i + 1).ToString("00") + " 위치 이송")) ;
                ReCheck_ScrapBox:
                if (prMACHINE[CP.UseScrapBoxCheck] == (int)eUSE.USE && mIN[I.SCRAP_BOX]){
                    UTIL_.OnERROR(E.ScrapBoxVanish);
                    goto ReCheck_ScrapBox;
                }
                for (int j = 0; j < (int)prMACHINE[CP.ScrapBlowRepeatCnt]; j++){
                    ScrapBlow((eSCRAP)i);
                    UTIL_.DELAY(18);
                }
                if (i == 0){
                    COM_.SetBit(nThread, B.UnitPkPic, false, "유닛 피커 유닛 픽업 완료");
                }
            }
            ScrapBlow(eSCRAP.All);
            while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
            ReCheck_UnitPk:
            if ((!mIN[I.UNIT_PK_VAC] || !mOUT[O.UNIT_PK_VAC]) && !bDRYRUN){
                if (bMF) return false;
                UTIL_.OnERROR(E.UnitPkUnitVanish);
                bReturn = false;
                goto ReCheck_UnitPk;
            }
            LogEnd(nThread, comment + " 완료");
            return bReturn;
        }

        public bool Brush(string comment){
            if (prMACHINE[CP.UseBrush] != (int)eUSE.USE){
                while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
                return true;
            }
            LogStart(nThread, comment + " 진행");
            for (int i = 0; i < (int)prMACHINE[CP.BrushRepeatCnt]; i++){
                if (i == 0){
                    while (eRTN.SUCESS != MoveZ(P.BrushStart, "", "유닛 피커 Z축 브러쉬 시작 위치 이송")) ;
                    cmds = "";
                }
                else cmds = "spd=" + string.Format("{0:0}", prMACHINE[CP.BrushSpd]);
                while (eRTN.SUCESS != MoveX(P.BrushStart, cmds, "유닛 피커 X축 브러쉬 시작 위치 이송")) ;
                cmds = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.UnitKitWidthPitch]) + ":spd=" + string.Format("{0:0}", prMACHINE[CP.BrushSpd]);
                while (eRTN.SUCESS != MoveX(P.BrushStart, cmds, "유닛 피커 X축 브러쉬 끝 위치 이송")) ;
            }
            while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
            LogEnd(nThread, comment + " 완료");
            return true;
        }

        public bool Cleaner(string comment){
            if (prMACHINE[CP.UseUnitClear] != (int)eUSE.USE){
                while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
                return true;
            }
            LogStart(nThread, comment + " 진행");
            while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
            while (eRTN.SUCESS != MoveX(P.Cleaner, "", "유닛 피커 X축 클리닝 위치 이송")) ;
            while (eRTN.SUCESS != MoveZ(P.Cleaner, "offset=-10", "유닛 피커 Z축 클리닝 대기 위치 이송")) ;
            while (eRTN.SUCESS != MoveZ(P.Cleaner, "spd=7", "유닛 피커 Z축 클리닝 위치 이송")) ;
            for (int i = 0; i < CleanCnt; i++){
                if (CleanData.MODE[i] == (int)eCLEANER.NOT) continue;
                if (CleanData.MODE[i] == (int)eCLEANER.WASH) CleanerWater(stBIT.ON);
                else if (CleanData.MODE[i] == (int)eCLEANER.DRY) CleanerAir(stBIT.ON);
                else if (CleanData.MODE[i] == (int)eCLEANER.RINSE) Cleaner(stBIT.ON);
                else Cleaner(stBIT.OFF);

                for (int j = 0; j < CleanData.COUNTER[i]; j++){
                    while (eRTN.SUCESS != CleanerSwing_Right("클리닝 스윙 전진")) ;
                    while (eRTN.SUCESS != CleanerSwing_Left("클리닝 스윙 후진")) ;
                }
            }
            Cleaner(stBIT.OFF);
            while (eRTN.SUCESS != MoveZ(P.Cleaner, "offset=-10:spd=10", "유닛 피커 Z축 클리닝 대기 위치 이송")) ;

            if (prMACHINE[CP.UseUnitAirshower] == (int)eUSE.USE){
                while (eRTN.SUCESS != MoveZ(P.AirBlowStart, "", "유닛 피커 Z축 에어샤워 작업 위치 이송")) ;
            }
            else { 
                while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
            }
            LogEnd(nThread, comment + " 완료");
            return true;
        }

        public bool AirShower(string comment){
            if (prMACHINE[CP.UseUnitAirshower] != (int)eUSE.USE){
                while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
                return true;
            }
            LogStart(nThread, comment + " 진행");
            for (int i = 0; i < (int)prMACHINE[CP.UnitAirshowRepeatCnt]; i++){
                UnitAirshower(stBIT.ON);
                if (i == 0){
                    while (eRTN.SUCESS != MoveZ(P.AirBlowStart, "", "유닛 피커 Z축 에어샤워 작업 위치 이송")) ;
                    cmds = "";
                }
                else cmds = "spd=" + string.Format("{0:0}", prMACHINE[CP.UnitAirshowerSpd]);
                while (eRTN.SUCESS != MoveX(P.AirBlowStart, "", "유닛 피커 X축 에어샤워 작업 시작 위치 이송")) ;
                cmds = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.UnitKitWidthPitch]) + ":spd=" + string.Format("{0:0}", prMACHINE[CP.UnitAirshowerSpd]);
                while (eRTN.SUCESS != MoveX(P.AirBlowStart, cmds, "유닛 피커 X축 에어샤워 작업 완료 위치 이송")) ;
            }
            //while (eRTN.SUCESS != MoveX(P.AirBlowStart, "", "유닛 피커 X축 에어샤워 작업 시작 위치 이송")) ;
            UnitAirshower(stBIT.OFF);
            while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
            LogEnd(nThread, comment + " 완료");
            return true;
        }

        public bool Plc(string comment){
            AddMessage(nThread, comment + " 진행");
        RePlace:
            if (prMACHINE[CP.SelectStage] == (int)eMAP_BLOCK.ALL){
                while (UTIL_.WaitBIT(nThread, B.Stage1Working, B.Stage2Working, true, true, "맵-블록 테이블 1/2 - 유닛 피커 받을 준비가 되어 있지 않아 준비 할때까지 대기", stBIT.AND)) ;
            }
            else if (prMACHINE[CP.SelectStage] == (int)eMAP_BLOCK.STAGE1){
                while (UTIL_.WaitBIT(nThread, B.Stage1Working, true, "맵-블록 테이블 1 - 유닛 피커 받을 준비가 되어 있지 않아 준비 할때까지 대기")) ;
            }
            else{
                while (UTIL_.WaitBIT(nThread, B.Stage2Working, true, "맵-블록 테이블 2 - 유닛 피커 받을 준비가 되어 있지 않아 준비 할때까지 대기")) ;
            }

            if (!IsBIT[B.Stage1Working] && (prMACHINE[CP.SelectStage] == (int)eMAP_BLOCK.ALL || prMACHINE[CP.SelectStage] == (int)eMAP_BLOCK.STAGE1)){
                COM_.SetBit(nThread, B.Stage1_UnitReceive, true, "유닛 피커 맵-블록 테이블1에 유닛 공급");
                while (UTIL_.WaitBIT(nThread, B.Stage1_UnitReceive, true, "맵-블록 테이블1 유닛 전달 완료 돨때까지 대기")) ;
                //CLOT.SEND_STRIP_INFO(nThread, T.DryTable1);
            }
            else if (!IsBIT[B.Stage2Working] && (prMACHINE[CP.SelectStage] == (int)eMAP_BLOCK.ALL || prMACHINE[CP.SelectStage] == (int)eMAP_BLOCK.STAGE2)){
                COM_.SetBit(nThread, B.Stage2_UnitReceive, true, "유닛 피커 맵-블록 테이블2에 유닛 공급");
                while (UTIL_.WaitBIT(nThread, B.Stage2_UnitReceive, true, "맵-블록 테이블2 유닛 전달 완료 돨때까지 대기")) ;
                //CLOT.SEND_STRIP_INFO(nThread, T.DryTable2);
            }
            else{
                COM_.ViewWarning(nThread, W.UnitPlaceSignelOff);
                while (UTIL_.WaitWarning(nThread, W.UnitPlaceSignelOff, "맵-블록 테이블에 유닛 전달 중 신호 OFF 됨")) ;
                goto RePlace;
            }
            while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
            if (prMACHINE[CP.UseMES] == (int)eUSE.USE){
                SUBFRM_.gSecsGem.SetPanelModuleOut(CLOT.InfoStrip[nThread].Index, CLOT.InfoStrip[nThread].Barcode, CMES.ModuleID.UNIT_PK);
            }
            CLOT.RESET_STRIP_INFO(nThread);
            //strip barcode 정보 저장
            Tack();
            WorkedAirShower("유닛 피커 플레이스 후 바닥면 에어 샤워");
            WorkedCleaner("유닛 피커 플레이스 후 클리너 박스 클린");
            //UnitPkPlcBrush
            if (prMACHINE[CP.UnitPkPlcBrush] == (int)eUSE.USE){
                for (int i = 0; i < (int)prMACHINE[CP.BrushRepeatCnt]; i++){
                    if (i == 0) cmds = "";
                    else        cmds = "spd=" + string.Format("{0:0}", prMACHINE[CP.BrushSpd]);
                    while (eRTN.SUCESS != MoveX(P.BrushStart, cmds, "유닛 피커 X축 브러쉬 시작 위치 이송")) ;
                    while (eRTN.SUCESS != MoveZ(P.BrushStart, "", "유닛 피커 Z축 브러쉬 시작 위치 이송")) ;
                    cmds = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.UnitKitWidthPitch]) + ":spd=" + string.Format("{0:0}", prMACHINE[CP.BrushSpd]);
                    while (eRTN.SUCESS != MoveX(P.BrushStart, cmds, "유닛 피커 X축 브러쉬 끝 위치 이송")) ;
                }
                while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
            }

            if (mIN[I.SAW_ULD_REQ] && mIN[I.SAW_LD_REQ] && !IsBIT[B.StripPkPlc] && !IsBIT[B.UnitPickupStop])
                while (eRTN.SUCESS != MoveX(P.UnitPckUp, "", "유닛 피커 X축 유닛 픽업 위치 이송")) ;
            else { 
                while (eRTN.SUCESS != MoveX(P.Ready, "", "유닛 피커 X축 대기 위치 이송")) ;            
            }
            AddMessage(nThread, comment + " 완료");
            return true;
        }

        public bool WorkedAirShower(string comment){
            LogStart(nThread, comment + " 진행");
            if (prMACHINE[CP.UseUnitPkWorkedAirshower] != (int)eUSE.USE){
                goto PASS;
            }
            while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
            for (int i = 0; i < (int)prMACHINE[CP.UnitWorkedAirshowRepeatCnt]; i++){
                UnitAirshower(stBIT.ON);
                while (eRTN.SUCESS != MoveX(P.AirBlowStart, "", "유닛 피커 X축 에어샤워 작업 시작 위치 이송")) ;
                if (i == 0){
                    while (eRTN.SUCESS != MoveZ(P.AirBlowStart, "", "유닛 피커 Z축 에어샤워 작업 위치 이송")) ;
                }
                cmds = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.UnitKitWidthPitch])/* + ":spd=" + string.Format("{0:0}", prMACHINE[CP.UnitAirshowerSpd])*/;
                while (eRTN.SUCESS != MoveX(P.AirBlowStart, cmds, "유닛 피커 X축 에어샤워 작업 완료 위치 이송")) ;
            }
        PASS:
            UnitAirshower(stBIT.OFF);
            while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
            LogEnd(nThread, comment + " 완료");
            return true;
        }
        public bool WorkedCleaner(string comment){
            LogStart(nThread, comment + " 진행");
            if (prMACHINE[CP.UseUnitPkWorkedCleaner] != (int)eUSE.USE){
                goto PASS;
            }
            while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
            while (eRTN.SUCESS != MoveX(P.Cleaner, "", "유닛 피커 X축 클리닝 위치 이송")) ;
            while (eRTN.SUCESS != MoveZ(P.Cleaner, "offset=-10", "유닛 피커 Z축 클리닝 대기 위치 이송")) ;
            while (eRTN.SUCESS != MoveZ(P.Cleaner, "spd=7", "유닛 피커 Z축 클리닝 위치 이송")) ;
            AllBlow(stBIT.ON);
            for (int i = 0; i < CleanCnt; i++){
                if (WorkedCleanData.MODE[i] == (int)eCLEANER.NOT) continue;
                if (WorkedCleanData.MODE[i] == (int)eCLEANER.WASH) CleanerWater(stBIT.ON);
                else if (WorkedCleanData.MODE[i] == (int)eCLEANER.DRY) CleanerAir(stBIT.ON);
                else if (WorkedCleanData.MODE[i] == (int)eCLEANER.RINSE) Cleaner(stBIT.ON);
                else Cleaner(stBIT.OFF);

                for (int j = 0; j < WorkedCleanData.COUNTER[i]; j++){
                    while (eRTN.SUCESS != CleanerSwing_Right("클리닝 스윙 전진")) ;
                    while (eRTN.SUCESS != CleanerSwing_Left("클리닝 스윙 후진")) ;
                }
            }
            Cleaner(stBIT.OFF);
            AllBlow(stBIT.OFF);
            while (eRTN.SUCESS != MoveZ(P.Cleaner, "offset=-10:spd=10", "유닛 피커 Z축 클리닝 대기 위치 이송")) ;
            PASS:
            while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
            LogEnd(nThread, comment + " 완료");
            return true;
        }
        #endregion

        #region >> Moudle
        void Tack(){
            TackEnd = Environment.TickCount;
            IsDOUBLE[D.UnitPkCycle] = (TackEnd - TackStart) / 1000;
            LogWR_.SaveLogTack(sJobName + "," + CLOT.GET_LOT.LotID + ",유닛 피커," + IsDOUBLE[D.UnitPkCycle].ToString(), "");
            COM_.SetBit(nThread, B.UnitPkMask, false, "유닛 피커 작업 진행");
            TackStart = Environment.TickCount;
        }

        void ChkPic(){
            if (!mIN[I.SCRAP_VAC1] || !mIN[I.SCRAP_VAC2]){
                if (prMODEL[RP.ScrapAlarm] == (int)eScrapAlarm.USE){
                    LAB_.BIT_OUT(O.HANDLER_SCRAP_CHECK, true);
                    UTIL_.DELAY(500);
                    LAB_.BIT_OUT(O.HANDLER_SCRAP_CHECK, false);
                }
            }
            while (eRTN.SUCESS != MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;
            COM_.SetOutput(nThread, O.HANDLER_UNIT_COMPLETE, true, "다이싱 설비에 유닛 픽업 완료 신호 ON");
            while (mIN[I.SAW_ULD_REQ] || mIN[I.SAW_ULD_POS] || mIN[I.SAW_STAGE_BLOW]) UTIL_.DELAY(10);
            COM_.SetOutput(nThread, O.HANDLER_UNIT_COMPLETE, false, "다이싱 설비에 유닛 픽업 완료 신호 OFF");
        }
        public void ResetInterface(){
            mOUT[O.HANDLER_UNIT_COMPLETE] = false;
            COM_.SetBit(nThread, B.UnitPkPic, false, "유닛 피커 ");
            
        }

        public void AllVac(bool bFlog){
            int nDelay = bFlog ? (int)prMACHINE[CP.UnitPkVacOn] : (int)prMACHINE[CP.UnitPkVacOff];
            LAB_.OUTPUT(O.UNIT_PK_BLOW, false);
            LAB_.OUTPUT(O.SCRAP_BLOW_1, false);
            LAB_.OUTPUT(O.SCRAP_BLOW_2, false);

            LAB_.OUTPUT(O.UNIT_PK_VAC, bFlog);
            LAB_.OUTPUT(O.SCRAP_VAC_1, bFlog);
            LAB_.OUTPUT(O.SCRAP_VAC_2, bFlog);
#if _NSS3300
#else
            LAB_.OUTPUT(O.UNIT_PK_PURGE, bFlog);
            LAB_.OUTPUT(O.SCRAP_PURGE_1, bFlog);
            LAB_.OUTPUT(O.SCRAP_PURGE_2, bFlog);
            LAB_.OUTPUT(O.UNIT_PK_VAC_OFF, !bFlog);
            LAB_.OUTPUT(O.SCRAP1_VAC_OFF, !bFlog);
            LAB_.OUTPUT(O.SCRAP2_VAC_OFF, !bFlog);
#endif
            UTIL_.DELAY(nDelay);
        }
        public void AllBlow(){
            LAB_.OUTPUT(O.UNIT_PK_VAC, false);
            LAB_.OUTPUT(O.SCRAP_VAC_1, false);
            LAB_.OUTPUT(O.SCRAP_VAC_2, false);
#if _NSS3300
#else
            LAB_.OUTPUT(O.UNIT_PK_PURGE, false);
            LAB_.OUTPUT(O.SCRAP_PURGE_1, false);
            LAB_.OUTPUT(O.SCRAP_PURGE_2, false);
            LAB_.OUTPUT(O.UNIT_PK_VAC_OFF, true);
            LAB_.OUTPUT(O.SCRAP1_VAC_OFF, true);
            LAB_.OUTPUT(O.SCRAP2_VAC_OFF, true);
#endif

            LAB_.OUTPUT(O.UNIT_PK_BLOW, true);
            LAB_.OUTPUT(O.SCRAP_BLOW_1, true);
            LAB_.OUTPUT(O.SCRAP_BLOW_2, true);

            UTIL_.DELAY((int)prMACHINE[CP.UnitPkBlowOn]);
            LAB_.OUTPUT(O.UNIT_PK_BLOW, false);
            LAB_.OUTPUT(O.SCRAP_BLOW_1, false);
            LAB_.OUTPUT(O.SCRAP_BLOW_2, false);
        }
        public void AllBlow(bool bFLOG){
            LAB_.OUTPUT(O.UNIT_PK_VAC, false);
            LAB_.OUTPUT(O.SCRAP_VAC_1, false);
            LAB_.OUTPUT(O.SCRAP_VAC_2, false);
#if _NSS3300
#else
            LAB_.OUTPUT(O.UNIT_PK_PURGE, false);
            LAB_.OUTPUT(O.SCRAP_PURGE_1, false);
            LAB_.OUTPUT(O.SCRAP_PURGE_2, false);
            LAB_.OUTPUT(O.UNIT_PK_VAC_OFF, true);
            LAB_.OUTPUT(O.SCRAP1_VAC_OFF, true);
            LAB_.OUTPUT(O.SCRAP2_VAC_OFF, true);
#endif

            LAB_.OUTPUT(O.UNIT_PK_BLOW, bFLOG);
            LAB_.OUTPUT(O.SCRAP_BLOW_1, bFLOG);
            LAB_.OUTPUT(O.SCRAP_BLOW_2, bFLOG);
        }

        public void Vac(bool bFlog){
            int nDelay = bFlog ? (int)prMACHINE[CP.UnitPkVacOn] : (int)prMACHINE[CP.UnitPkVacOff];
            LAB_.OUTPUT(O.UNIT_PK_BLOW, false);
            LAB_.OUTPUT(O.UNIT_PK_VAC, bFlog);
#if _NSS3300
#else
            LAB_.OUTPUT(O.UNIT_PK_PURGE, bFlog);
            LAB_.OUTPUT(O.UNIT_PK_VAC_OFF, !bFlog);
#endif
            UTIL_.DELAY(nDelay);
        }
        public void Blow(){
            LAB_.OUTPUT(O.UNIT_PK_VAC, false);
#if _NSS3300
#else
            LAB_.OUTPUT(O.UNIT_PK_PURGE, false);
            LAB_.OUTPUT(O.UNIT_PK_VAC_OFF, true);
#endif
            LAB_.OUTPUT(O.UNIT_PK_BLOW, true);
            UTIL_.DELAY((int)prMACHINE[CP.UnitPkBlowOn]);
            LAB_.OUTPUT(O.UNIT_PK_BLOW, false);
        }

        public void ScrapVac(eSCRAP Ch, bool bFlog){
            int nDelay = bFlog ? (int)prMACHINE[CP.UnitPkVacOn] : (int)prMACHINE[CP.UnitPkVacOff];
            if (eSCRAP.All == Ch){
                for (int i = 0; i < O.ScrapVac.Length; i++){
                    LAB_.OUTPUT(O.ScrapBlow[i], false);
                    LAB_.OUTPUT(O.ScrapVac[i], bFlog);
#if _NSS3300
#else
                    LAB_.OUTPUT(O.ScrapPurge[i], bFlog);
                    LAB_.OUTPUT(O.ScrapVacOff[i], !bFlog);
#endif
                }
            }
            else{
                LAB_.OUTPUT(O.ScrapBlow[(int)Ch], false);
                LAB_.OUTPUT(O.ScrapVac[(int)Ch], bFlog);
#if _NSS3300
#else
                LAB_.OUTPUT(O.ScrapPurge[(int)Ch], bFlog);
                LAB_.OUTPUT(O.ScrapVacOff[(int)Ch], !bFlog);
#endif
            }
            UTIL_.DELAY(nDelay);
        }
        public void ScrapBlow(eSCRAP Ch){
            if (prMODEL[RP.PCB_TYPE] == (int)ePCB.STRIP){
                Ch = eSCRAP.All;
            }
            if (eSCRAP.All == Ch){
                LAB_.OUTPUT(O.ScrapVac, false);
#if _NSS3300
#else
                LAB_.OUTPUT(O.ScrapPurge, false);
                LAB_.OUTPUT(O.ScrapVacOff, true);
#endif
                LAB_.OUTPUT(O.ScrapBlow, true);
                UTIL_.DELAY((int)prMACHINE[CP.ScrapBlowOn]);
                LAB_.OUTPUT(O.ScrapBlow, false);
            }
            else{
                LAB_.OUTPUT(O.ScrapVac[(int)Ch], false);
#if _NSS3300
#else
                LAB_.OUTPUT(O.ScrapVacOff[(int)Ch], true);
                LAB_.OUTPUT(O.ScrapPurge[(int)Ch], false);
#endif
                LAB_.OUTPUT(O.ScrapBlow[(int)Ch], true);
                UTIL_.DELAY((int)prMACHINE[CP.ScrapBlowOn]);
                LAB_.OUTPUT(O.ScrapBlow[(int)Ch], false);
            }
        }

        public void BrushWater(bool bFlog) { LAB_.OUTPUT(O.BRUSH_WATER, bFlog); }

        public void CleanerAir(bool bFlog) {
#if _NSS3300
            LAB_.OUTPUT(O.CLEANER_AIR, bFlog);
#else
            LAB_.OUTPUT(O.CLEANER_AIR_1, O.CLEANER_AIR_2, bFlog); 
#endif
        }
        public bool CleanerWater(bool bFlog){
            if (bDRYRUN) bFlog = false;

            if (bFlog){
                if (!mtDATA[M.UnitPkX, P.Cleaner].bPOS || !mtDATA[M.UnitPkZ, P.Cleaner].bPOS){
                    UTIL_.OnERROR(E.emsCleanerWaterFail);
                    return false;
                }
            }
            //else if (bFlog){
            //    COM_.ViewWarning(nThread, W.CleanerWater);
            //    while (UTIL_.WaitWarning(nThread, W.CleanerWater, "유닛 피커 클리너 위치에 있지 않음")) ;
            //    if (!ConfirmUser[W.CleanerWater].result) return false;
            //}

#if _NSS3300
            LAB_.OUTPUT(O.CLEANER_WATER_1, O.CLEANER_WATER_2, bFlog);
            LAB_.OUTPUT(O.CLEANER_WATER_3, O.CLEANER_WATER_4, bFlog);
#else
            LAB_.OUTPUT(O.CLEANER_WATER_1, O.CLEANER_WATER_2, bFlog);
#endif
            return true;
        }
        public bool Cleaner(bool bFlog){
            if (!CleanerWater(bFlog)) return false;
            CleanerAir(bFlog);
            return true;
        }

        public void UnitAirshower(bool bFlog) { LAB_.OUTPUT(O.CLEANER_AIR_KNIFE, bFlog); }

        public eRTN CleanerSwing_Right(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.CleanerSwingForward, I.CLEANER_SWING_LEFT, I.CLEANER_SWING_RIGHT, O.CLEANER_SWING_L, O.CLEANER_SWING_R, (int)prMACHINE[CP.CleanerSwingFwdDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN CleanerSwing_Left(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.CleanerSwingBackward, I.CLEANER_SWING_RIGHT, I.CLEANER_SWING_LEFT, O.CLEANER_SWING_R, O.CLEANER_SWING_L, (int)prMACHINE[CP.CleanerSwingBwdDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public eRTN MoveX(int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            bPicSignel = false;

            if (nPos == P.Ready || nPos == P.UnitPckUp || nPos == P.Cleaner || nPos == P.AirBlowStart || nPos == P.PlacePallet1 || nPos == P.PlacePallet2){
                if (!C.Interlock.ChkInterlock(E.emsUnitPkZnotSafetyLocation, true)) return eRTN.EMS;
                if (nPos == P.UnitPckUp && eMCStatus == eMachineStatus.AUTO){
                    if (!mIN[I.SAW_ULD_REQ]){
                        bPicSignel = true;
                        return eRTN.NOT_UNIT_PK_PICKUP;
                    }
                }
            }
            if (nPos == P.UnitPckUp){
                if (!C.Interlock.ChkInterlock(E.emsStripPkXSafetyPosition, true)) return eRTN.EMS;
            }

            IsSTRING[S.UnitPkMessage] = comment + " " + LogWR_.LogPos(M.UnitPkX, nPos);
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.UnitPkX, nPos, 0.005, false, false, false, cmd, IsSTRING[S.UnitPkMessage])) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN MoveZ(int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            if (nPos == P.UnitPckUp)
                if (!C.Interlock.ChkInterlock(E.emsNotUnitPkXPicPos, true)) return eRTN.EMS;
            if (nPos == P.Cleaner)
                if (!C.Interlock.ChkInterlock(E.emsNotCleanerPos, true)) return eRTN.EMS;
            if (nPos == P.PlacePallet1)
                if (!C.Interlock.ChkInterlock(E.emsNotStage1Pos, true) || !C.Interlock.ChkInterlock(E.emsNotUnitPkXStage1Pos, true)) return eRTN.EMS;
            if (nPos == P.PlacePallet2)
                if (!C.Interlock.ChkInterlock(E.emsNotStage2Pos, true) || !C.Interlock.ChkInterlock(E.emsNotUnitPkXStage2Pos, true)) return eRTN.EMS;


            IsSTRING[S.UnitPkMessage] = comment + " " + LogWR_.LogPos(M.UnitPkZ, nPos);
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.UnitPkZ, nPos, 0.005, false, false, false, cmd, IsSTRING[S.UnitPkMessage])) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
#endregion
    }
}