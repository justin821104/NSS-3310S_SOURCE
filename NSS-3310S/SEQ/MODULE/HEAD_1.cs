using Object;

namespace NSS_3310S.SEQ.MODULE{
    public class HEAD_1 : BASE{
        readonly int nThread = T.Head1;
        eMAP_BLOCK curStage;
        bool CheckRunThread(){
            if (eMCStatus != eMachineStatus.AUTO || prMACHINE[CP.SelectHead] == (int)eHD.HD2){
                UTIL_.DELAY(100);
                return false;
            }
            return true;
        }
        public void DoAuto(){
            if (prMACHINE[CP.SelectHead] == (int)eHD.ALL) {
                if (!IsBIT[B.X1PicBusy] && !IsBIT[B.X2PicBusy]) B.Bit(nThread, B.X1PicBusy, true, "초기 시작 시 X1 먼저 시작 플러그 ON");
            }
            do{
                if (gExit) break;
                if (!CheckRunThread()) continue;

                if (IsBIT[B.X2PicBusy])
                    while (eRTN.SUCESS != MoveXPicReady(nThread, eHD.HD1, (eMAP_BLOCK)IsLONG[L.CurWorkStage], ePK.PKR1, stBIT.NotCAM, "X1 PICKUP 대기 위치 이송")) ;

                while (B.WaitBIT(nThread, B.X2PicBusy, true, "X2 픽업 진행 완료할때까지 대기")) ;
                B.SetBit(nThread, B.X1PicBusy, true, "X1 픽업 진행 플로그");
                P.INFO_PICKER_DATA_CLEANER(eHD.HD1, L.CurWorkXPic, (int)IsLONG[L.CurWorkStage]);
                UnitPic(nThread, eHD.HD1, "X1 유닛 픽업");
                curStage = (eMAP_BLOCK)IsLONG[L.CurWorkStage];

                while (B.WaitBIT(nThread, B.X2PRSBusy, true, "HEAD X2 PRS 작업 진행 중 완료할때까지 대기")) ;
                B.SetBit(nThread, B.X1PRSBusy, true, "HEAD X1 PRS 작업 진행");
                PRS(nThread, eHD.HD1, "X1 PRS 검사");

                while (B.WaitBIT(nThread, B.X2PlcBusy, true, "HEAD X2 플레이스 진행 완료할때까지 대기")) ;
                B.SetBit(nThread, B.X1PlcBusy, true, "X1 플레이스 진행 플로그");
                UnitPlc(nThread, eHD.HD1, "X1 유닛 플레이스");

                while (B.WaitBIT(nThread, B.X2NGPlcBusy, true, "HEAD X2 NG 유닛 플레이스 진행 완료 할때까지 대기")) ;
                B.SetBit(nThread, B.X1NGPlcBusy, true, "X1 NG유닛 플레이스 진행 플로그");
                PkNGPlc(nThread, eHD.HD1, "NG TRAY 유닛 플레이스");

                PkRejectPlc(nThread, eHD.HD1, curStage, "UNIT REJECT");
                B.SetBit(nThread, B.X1Working, false, "X1 PIC AND PLC 진행");

                PkVacReset(eHD.HD1);
            } while (true);
        }
    }
}