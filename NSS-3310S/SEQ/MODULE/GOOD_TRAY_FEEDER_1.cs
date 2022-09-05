using Object;

namespace NSS_3310S.SEQ.MODULE{
    public class GOOD_TRAY_FEEDER_1 : BASE{
        readonly int nThread = T.GoodTrayFeeder1;

        bool CheckRunThread(){
            if (eMCStatus != eMachineStatus.AUTO ||
               (prMACHINE[CP.TrayUnloadingMode] == (int)eULD_TRAY.STACKER && prMACHINE[CP.SelectStackerUnloading] == (int)eGOOD_TRAY.GD2) ||
               (prMACHINE[CP.TrayUnloadingMode] == (int)eULD_TRAY.CONVEYOR && prMACHINE[CP.SelectConveyorUnloading] == (int)eGOOD_TRAY.GD2)){
                UTIL_.DELAY(100);
                return false;
            }
            return true;
        }
        public void DoAuto(){
            do{
            StackerMode:
                if (gExit) break;
                if (!CheckRunThread()) continue;
                while (UTIL_.WaitBIT(nThread, B.GoodTray2Loading, true, "GOOD FEEDER2 트레이 공급 중 대기")) ;
                GetEmptyTray(nThread, eTRAY.GOOD1, "GOOD FEEDER1 트레이 공급");

                while (UTIL_.WaitBIT(nThread, B.GoodTray2Place, true, "GOOD TRAY2 유닛 플레이스 작업 진행 중 대기")) ;
                if (eRTN.UnloadingStacker == TrayUnitPlace(nThread, eTRAY.GOOD1, "GOOD TRAY1 유닛 플레이스 작업")){
                    goto StackerMode;
                }

                if (prMACHINE[CP.TrayUnloadingMode] == (int)eULD_TRAY.CONVEYOR){
                    while (UTIL_.WaitBIT(nThread, B.GoodTray2ULDEnd, true, "GOOD TRAY2 콘베어 배출 완료 때까지 대기")) ;
                }
                OutTray(nThread, eTRAY.GOOD1, "GOOD TRAY1 트레이 배출");
            } while (true);
        }
    }
}
