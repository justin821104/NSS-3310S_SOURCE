using Object;

namespace NSS_3310S.SEQ.MODULE{
    public class HEAD_2 : BASE{
        readonly int nThread = T.Head2;

        bool CheckRunThread(){
            if (eMCStatus != eMachineStatus.AUTO || prMACHINE[CP.SelectHead] == (int)eHD.HD1){
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

                if (IsBIT[B.X1PicBusy])
                    while (eRTN.SUCESS != MoveXPicReady(nThread, eHD.HD2, (eMAP_BLOCK)IsLONG[L.CurWorkStage], ePK.PKR1, stBIT.NotCAM, "X2 PICKUP 대기 위치 이송")) ;

                while (UTIL_.WaitBIT(nThread, B.X1PicBusy, true, "X1 픽업 진행 완료할때까지 대기")) ;
                COM_.SetBit(nThread, B.X2PicBusy, true, "X2 픽업 진행 플로그");
                INFO_PICKER_DATA_CLEANER(eHD.HD2, L.CurWorkXPic);
                UnitPic(nThread, eHD.HD2, "X2 유닛 픽업");

                while (UTIL_.WaitBIT(nThread, B.X1PRSBusy, true, "HEAD X1 PRS 작업 진행 중 완료할때까지 대기")) ;
                COM_.SetBit(nThread, B.X2PRSBusy, true, "HEAD X2 PRS 작업 진행");
                PRS(nThread, eHD.HD2, "X2 PRS 검사");

                while (UTIL_.WaitBIT(nThread, B.X1PlcBusy, true, "HEAD X1 플레이스 진행 완료할때까지 대기")) ;
                COM_.SetBit(nThread, B.X2PlcBusy, true, "X2 플레이스 진행 플로그");
                UnitPlc(nThread, eHD.HD2, "X2 유닛 플레이스");

                while (UTIL_.WaitBIT(nThread, B.X1NGPlcBusy, true, "HEAD X1 NG 유닛 플레이스 진행 완료 할때까지 대기")) ;
                COM_.SetBit(nThread, B.X2NGPlcBusy, true, "X2 NG유닛 플레이스 진행 플로그");
                PkNGPlc(nThread, eHD.HD2, "NG TRAY 유닛 플레이스");

                PkRejectPlc(nThread, eHD.HD2, "UNIT REJECT");
                COM_.SetBit(nThread, B.X2Working, false, "X2 PIC AND PLC 진행");

                PkVacReset(eHD.HD2);
            } while (true);
        }
    }
}