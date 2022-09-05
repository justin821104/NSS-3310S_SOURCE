using LIB_.DateType;
using Object;

namespace NSS_3310S.SEQ.MODULE{
    public class DRY_TABLE_1 : BASE{
        readonly int nThread = T.DryTable1;
        
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
                if (IsBIT[B.Stage1PickUp]){
                    COM_.SetBit(nThread, B.Stage1PickUp, false, "맵-블록 테이블1 픽업 중 홈 진행하여 이여서 작업 진행");
                    COM_.SetBit(nThread, B.StageAirshowerWait, true, "맵-블록 테이블 에어사워 일시 정지 플로그 ON");
                }
            RePLACE:
                while (UTIL_.WaitBIT(nThread, B.Stage1_UnitReceive, false, "유닛 공급 대기")){
                    if (!mtDATA[M.Table1, P.RecieveUnit].bPOS){
                        while (eRTN.SUCESS != MoveStageY(nThread, eMAP_BLOCK.STAGE1, P.RecieveUnit, "", "유닛 받는 위치 이송")) ;
                    }
                    if (mIN[I.STAGE_VACUUM1]) goto GoWORK;
                }
                if (eRTN.SUCESS != UnitReceive(nThread, eMAP_BLOCK.STAGE1, "맵-블록 테이블1에 유닛 공급")) goto RePLACE;
                if (prMACHINE[CP.UseMES] == (int)eUSE.USE){
                    SUBFRM_.gSecsGem.SetPanelModuleIn(CLOT.InfoStrip[nThread].Index, CLOT.InfoStrip[nThread].Barcode, (int)CMES.ModuleID.MB_1);
                }
            GoWORK:
                if (eRTN.SUCESS != StageAirshower(nThread, eMAP_BLOCK.STAGE1, "맵-블록 테이블2 유닛 검사 전 에어 샤워")){
                    StageWorkCencel(nThread, eMAP_BLOCK.STAGE1, "맵-블록1 테이블 작업 취소 됨");
                    UTIL_.DELAY(500);
                    goto RePLACE;
                }

                while (UTIL_.WaitBIT(nThread, B.Stage2Inspection, true, "맵-블록 테이블2 유닛 검사 진행 중이여 대기")) ;
                COM_.SetBit(nThread, B.Stage1Inspection, true, "맵-블록 테이블1 유닛 검사 진행 플로그");
                if (eRTN.SUCESS != UnitInspection(nThread, eMAP_BLOCK.STAGE1, CLOT.InfoStrip[nThread].Barcode, "맵-블록 테이블1 유닛 검사 진행")){
                    StageWorkCencel(nThread, eMAP_BLOCK.STAGE1, "맵-블록1 테이블 작업 취소 됨");
                    UTIL_.DELAY(500);
                    goto RePLACE;
                }

                while (UTIL_.WaitBIT(nThread, B.Stage2Busy, true, "맵-블록 테이블2 유닛 픽업 작업 중 대기")){
                    if (!mtDATA[M.Table1, P.HD1_Unit].bPOS){
                        while (eRTN.SUCESS != MoveStageY(nThread, eMAP_BLOCK.STAGE1, P.HD1_Unit, "", "맵-블록 테이블1 픽업 대기 위치 이송")) ;
                    }
                }
                COM_.SetBit(nThread, B.Stage1Busy, true, "테이블1번 픽업 작업 진행");
                if (!StageUnitPickUp(nThread, eMAP_BLOCK.STAGE1)){
                    StageWorkCencel(nThread, eMAP_BLOCK.STAGE1, "맵-블록1 테이블 작업 취소 됨");
                    UTIL_.DELAY(500);
                    goto RePLACE;
                }
                if (prMACHINE[CP.UseMES] == (int)eUSE.USE){
                    SUBFRM_.gSecsGem.SetPanelModuleOut(CLOT.InfoStrip[nThread].Index, CLOT.InfoStrip[nThread].Barcode, CMES.ModuleID.MB_1);
                }
                StageUnloadingAirshower(nThread, eMAP_BLOCK.STAGE1, "맵-블록 테이블 1 작업 완료 후 에어 샤워");
                if (prMACHINE[CP.UseMES] == (int)eUSE.USE){
                    SUBFRM_.gSecsGem.SetPanelLineOut(CLOT.InfoStrip[nThread].Index, CLOT.InfoStrip[nThread].Barcode);
                    CLOT.GET_LOT.OutCnt++;
                }
                CLOT.RESET_STRIP_INFO(nThread); //InfoStripBarcode[nThread] = "";
                COM_.SetBit(nThread, B.Stage1Working, false, "맵-블록 1 작업 완료");
                StageTack(eMAP_BLOCK.STAGE1);
            } while (true);
        }
    }
}