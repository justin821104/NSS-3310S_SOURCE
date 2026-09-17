using LIB_.DateType;
using Object;

namespace NSS_3310S.SEQ.MODULE{
    public class DRY_TABLE_2 : BASE{
        readonly int nThread = T.DryTable2;

        bool CheckRunThread(){
            if (eMCStatus != eMachineStatus.AUTO){
                UTIL_.DELAY(100);
                return false;
            }
            return true;
        }
        public void DoAuto(){
            UTIL_.DELAY(100);
            do{
                if (gExit) break;
                if (!CheckRunThread()) continue;
                if (IsBIT[B.Stage2PickUp]){
                    B.SetBit(nThread, B.Stage2PickUp, false, "맵-블록 테이블2 픽업 중 홈 진행하여 이여서 작업 진행");
                    B.SetBit(nThread, B.StageAirshowerWait, true, "맵-블록 테이블 에어사워 일시 정지 플로그 ON");
                }
            RePLACE:
                while (B.WaitBIT(nThread, B.Stage2_UnitReceive, false, "유닛 공급 대기")){
                    if (!bBD) {
                        if (!mtDATA[M.Table2, P.RecieveUnit].bPOS) {
                            while (eRTN.SUCESS != MoveStageY(nThread, eMAP_BLOCK.STAGE2, P.RecieveUnit, "", "유닛 받는 위치 이송")) ;
                        }
                    }
                     
                    if (mIN[I.STAGE_VACUUM2]) goto GoWORK;
                }
                if (eRTN.SUCESS != UnitReceive(nThread, eMAP_BLOCK.STAGE2, "맵-블록 테이블2에 유닛 공급")) goto RePLACE;
                if (prMACHINE[CP.UseMES] == (int)eUSE.USE){
                    SUBFRM_.gSecsGem.SetPanelModuleIn(CLOT.InfoStrip[nThread].Index, CLOT.InfoStrip[nThread].Barcode, CMES.ModuleID.MB_2);
                }
            GoWORK:
                if (eRTN.SUCESS != StageAirshower(nThread, eMAP_BLOCK.STAGE2, "맵-블록 테이블1 유닛 검사 전 에어 샤워")){
                    StageWorkCencel(nThread, eMAP_BLOCK.STAGE2, "맵-블록2 테이블 작업 취소 됨");
                    UTIL_.DELAY(500);
                    goto RePLACE;
                }

                while (B.WaitBIT(nThread, B.Stage1Inspection, true, "맵-블록 테이블1 유닛 검사 진행 중이여 대기")) ;
                B.SetBit(nThread, B.Stage2Inspection, true, "맵-블록 테이블2 유닛 검사 진행 플로그");
                if (eRTN.SUCESS != UnitInspection(nThread, eMAP_BLOCK.STAGE2, "맵-블록 테이블2 유닛 검사 진행")){
                    StageWorkCencel(nThread, eMAP_BLOCK.STAGE2, "맵-블록2 테이블 작업 취소 됨");
                    UTIL_.DELAY(500);
                    goto RePLACE;
                }

                while (B.WaitBIT(nThread, B.Stage1Busy, true, "맵-블록 테이블2 유닛 픽업 작업 중 대기")){
                    if (!mtDATA[M.Table2, P.HD1_Unit].bPOS){
                        while (eRTN.SUCESS != MoveStageY(nThread, eMAP_BLOCK.STAGE2, P.HD1_Unit, "", "맵-블록 테이블2 픽업 대기 위치 이송")) ;
                    }
                }
                B.SetBit(nThread, B.Stage2Busy, true, "테이블2번 픽업 작업 진행");
                if (!StageUnitPickUp(nThread, eMAP_BLOCK.STAGE2)){
                    StageWorkCencel(nThread, eMAP_BLOCK.STAGE2, "맵-블록2 테이블 작업 취소 됨");
                    UTIL_.DELAY(500);
                    goto RePLACE;
                }
                if (prMACHINE[CP.UseMES] == (int)eUSE.USE){
                    SUBFRM_.gSecsGem.SetPanelModuleOut(CLOT.InfoStrip[nThread].Index, CLOT.InfoStrip[nThread].Barcode, CMES.ModuleID.MB_2);
                }
                StageUnloadingAirshower(nThread, eMAP_BLOCK.STAGE2, "맵-블록 테이블 2 작업 완료 후 에어 샤워");
                if (prMACHINE[CP.UseMES] == (int)eUSE.USE){
                    SUBFRM_.gSecsGem.SetPanelLineOut(CLOT.InfoStrip[nThread].Index, CLOT.InfoStrip[nThread].Barcode);
                    CLOT.GET_LOT.OutCnt++;
                }
                StageTack(eMAP_BLOCK.STAGE2);
                UTIL_.DELAY(1000);
                LogWR_.SavePCBUnitInfo(nThread, eMAP_BLOCK.STAGE2);
                CLOT.RESET_STRIP_INFO(eSeqBacode.MapBlock2, nThread);
                B.SetBit(nThread, B.Stage2Working, false, "맵-블록 2 작업 완료");
            } while (true);
        }
    }
}