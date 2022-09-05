using Object;

namespace NSS_3310S.SEQ.MODULE{
    public class GOOD_TRAY_FEEDER_2 : BASE{
        int nThread = T.GoodTrayFeeder2;

        bool CheckRunThread(){
            if (eMCStatus != eMachineStatus.AUTO ||
               (prMACHINE[CP.TrayUnloadingMode] == (int)eULD_TRAY.STACKER && prMACHINE[CP.SelectStackerUnloading] == (int)eGOOD_TRAY.GD1) ||
               (prMACHINE[CP.TrayUnloadingMode] == (int)eULD_TRAY.CONVEYOR && prMACHINE[CP.SelectConveyorUnloading] == (int)eGOOD_TRAY.GD1)){
                UTIL_.DELAY(100);
                return false;
            }
            return true;
        }
        public void DoAuto(){
            UTIL_.DELAY(100);
            do{
            StackerMode:
                if (gExit) break;
                if (!CheckRunThread()) continue;
                while (UTIL_.WaitBIT(nThread, B.GoodTray1Loading, true, "GOOD FEEDER1 트레이 공급 중 대기")) ;
                GetEmptyTray(nThread, eTRAY.GOOD2, "GOOD FEEDER2 트레이 공급");

                while (UTIL_.WaitBIT(nThread, B.GoodTray1Place, true, "GOOD TRAY1 유닛 플레이스 작업 진행 중 대기")) ;
                if (eRTN.UnloadingStacker == TrayUnitPlace(nThread, eTRAY.GOOD2, "GOOD TRAY2 유닛 플레이스 작업")){
                    goto StackerMode;
                }

                if (prMACHINE[CP.TrayUnloadingMode] == (int)eULD_TRAY.CONVEYOR){
                    while (UTIL_.WaitBIT(nThread, B.GoodTray1ULDEnd, true, "GOOD TRAY1 콘베어 배출 완료 때까지 대기")) ;
                }
                OutTray(nThread, eTRAY.GOOD2, "GOOD TRAY2 트레이 배출");
            } while (true);
        }
    }
}