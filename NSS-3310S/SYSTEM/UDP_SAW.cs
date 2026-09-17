using LIB_.DateType;
using NSS_3310S;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace SYSTEM{
    /// <summary>
    /// SAW PC UDP 통신
    /// SAW PC IP : 192.168.1.2 / PORT : 5000
    /// HANDLER PC IP : 192.168.1.1 / PORT : 5001
    /// </summary>
    public class RECEIVE_SAW : DATA_{
        readonly int nThread = T.ReceiveSaw;
        public void DoReceiveEvent(){
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(DEF.HandlerIP), DEF.HandlerPort);
            UdpClient newSock = new UdpClient(ipep);
            do{
                if (gExit) break;
                UTIL_.DELAY(3);
                byte[] bReceive = newSock.Receive(ref ipep);
                IsSTRING[S.SawRecieveMessage] = Encoding.ASCII.GetString(bReceive);
                Recieve(IsSTRING[S.SawRecieveMessage]);
            } while (true);
        }
        void Recieve(string MSG){
            if (MSG.Length <= 0) return;
            string[] sRSLT = MSG.Split(',');
            try{
                switch (sRSLT[0]){
                    case "RECIPE":
                        IsSTRING[S.SawRecipeList] = "";
                        for (int i = 1; i < sRSLT.Length - 1; i++){
                            IsSTRING[S.SawRecipeList] += sRSLT[i] + "\n";
                        }
                        break;
                    case "RECIPE_SEND_OK":
                        
                        break;
                    case "RecipeOpen":
                        B.Bit(T.ReceiveSaw, B.SawRecipeOpen, true, "다이싱 레스피 OPEN 플러그 ON");
                        break;

                    case "PROCESS":
                        string PROCESS_STATE = sRSLT[1];

                        break;
                    case "PV":
                        int nPVID = 0;
                        string pVALUE = string.Empty;
                        for (int i = 1; i < sRSLT.Length - 1; i++){
                            string[] aSVID = sRSLT[i].Split('=');
                            try{
                                nPVID = int.Parse(aSVID[0]);
                                pVALUE = aSVID[1];
                                if (nPVID == CSVID.Sp1Current || nPVID == CSVID.Sp2Current){
                                    try{
                                        double dValue = double.Parse(pVALUE);
                                        if (dValue < 10) SUBFRM_.gSecsGem.SetSVID(nPVID, pVALUE);
                                        else {
                                            //로그 기록 추가 필요???
                                        }
                                    }
                                    catch (Exception e) {
                                        LogWR_.SaveLogException("RECIEVE FAIL (SUB)! (RECEIVE_UDP->RECIEVE => PV SEND FAIL)" + "[ SAW PV " + nPVID + " = " + pVALUE + "]", e);
                                    }
                                } // 인버터에서 전류값 쓰레기값이 들어왔을 경우 10A이상 될 수 없는데 들어와있을 경우 무시
                                else {
                                    SUBFRM_.gSecsGem.SetSVID(nPVID, pVALUE);
                                }
                                //LogWR_.DEBUG_PRINT(nPVID.ToString() + " = " + pVALUE);

                                if (nPVID == CSVID.Sp1BladeAmountOfUse) Sp1BladeAmountOfUse = pVALUE;
                                if (nPVID == CSVID.Sp2BladeAmountOfUse) Sp2BladeAmountOfUse = pVALUE;
                                if (nPVID == CSVID.Sp1BladeCuttingCnt) Sp1BladeCuttingCnt = pVALUE;
                                if (nPVID == CSVID.Sp2BladeCuttingCnt) Sp2BladeCuttingCnt = pVALUE;

                            }
                            catch (Exception EX){
                                string sawdata = "";
                                for (int n = 0; n < aSVID.Length; n++){
                                    sawdata += aSVID[n];
                                }
                                LogWR_.SaveLogException("RECIEVE FAIL! (RECEIVE_UDP->RECIEVE => PV SEND FAIL)" + "[ SAW SVID = " + sawdata + "]" + '\n' + MSG, EX);
                            }
                        }
                        break;
                    case "SVID":
                        int nSVID = 0;
                        string nVALUE = string.Empty;
                        for (int i = 1; i < sRSLT.Length - 1; i++){
                            string[] aSVID = sRSLT[i].Split('=');
                            try{
                                nSVID = int.Parse(aSVID[0]);
                                nVALUE = aSVID[1];
                                SUBFRM_.gSecsGem.SetSVID(nSVID, nVALUE);
                                if (nSVID == CSVID.Sp1BladeAmountOfUse) Sp1BladeAmountOfUse = nVALUE;
                                if (nSVID == CSVID.Sp2BladeAmountOfUse) Sp2BladeAmountOfUse = nVALUE;
                                if (nSVID == CSVID.Sp1BladeCuttingCnt) Sp1BladeCuttingCnt = nVALUE;
                                if (nSVID == CSVID.Sp2BladeCuttingCnt) Sp2BladeCuttingCnt = nVALUE;
                            }
                            catch (Exception ex){
                                string sawdata = "";
                                for (int n =0; n < aSVID.Length; n++){
                                    sawdata += aSVID[n];
                                }
                                LogWR_.SaveLogException("RECIEVE FAIL! (RECEIVE_UDP->RECIEVE => SVID SEND FAIL)" + "[ SAW SVID = " + sawdata + "]", ex);
                            }
                        }
                        break;
                    case "CEID":
                        int nCEID = 0;
                        string cVALUE = string.Empty;
                        for (int i = 1; i < sRSLT.Length - 1; i++){
                            string[] aCEID = sRSLT[i].Split('=');
                            nCEID = int.Parse(aCEID[0]);
                            cVALUE = aCEID[1];
                            SUBFRM_.gSecsGem.SendEvent(nCEID);
                        }
                        break;

                    case "ASET": //Alarm Set
                        string sAlarm = sRSLT[1];
                        int sErrNum = int.Parse(sAlarm);
                        sErrNum += 100; // saw 900부터 시작함으로 +100 해서 1000번부터 시작하게 진행함.
                        SUBFRM_.gSecsGem.OnAlarmSet(sErrNum);
                        break;
                    case "ARST": //Alarm Reset
                        string rAlarm = sRSLT[1];
                        int rErrNum = int.Parse(rAlarm);
                        rErrNum += 100; // saw 900부터 시작함으로 +100 해서 1000번부터 시작하게 진행함.
                        SUBFRM_.gSecsGem.OnAlarmClear(rErrNum);
                        break;

                    case "ALARMT":
                        string tAlarm = sRSLT[1];
                        if (tAlarm == "1") bINTRK[E.emsSawStageThAlarm - eEMSBegin] = true;
                        else bINTRK[E.emsSawStageThAlarm - eEMSBegin] = false;
                        break;

                    case "VACUUM":
                    case "BLOW":
                        B.Bit(T.ReceiveSaw, B.SawVacuumInterface, true, "다이싱 UDP 통신 진공 부분 응답 확인 비트 SawIOInterface 플러그 ON");
                        break;
                    case "BladeChange":
                        //나중에 바코드만 읽어서 현재 달려있는것과 비교 후 장,탈착 보고 추가해야 함! 22.0927 HK.PARK
                        LIB_.DateType.CLOT.bBladeInfo_Sp1 = UTIL_.GET_SPINDLE_BLADE_BARCODE(Object.eSPINDLE.SP1);
                        LIB_.DateType.CLOT.bBladeInfo_Sp2 = UTIL_.GET_SPINDLE_BLADE_BARCODE(Object.eSPINDLE.SP2);
                        break;
                    case "TABLE_OFFSET_R":
                        string XOFFSET = sRSLT[1];
                        try{
                            double xoffset = double.Parse(XOFFSET);
                            if (prMACHINE[CP.SmartLoction_Dir] == 1){
                                xoffset *= -1;
                            }  // 부호 반전 
                            double dPos = mtDATA[M.StripPkX, P.StripPlc].Pos + xoffset;
                            TEACH_.SaveMotorPos(M.StripPkX, P.StripPlc, dPos);
                            C.SendSaw.SEND("TABLE_OFFSET_R_OK,*");
                        }
                        catch (Exception ex){
                            C.SendSaw.SEND("TABLE_OFFSET_R_NG,*");
                            LogWR_.SaveLogException("[UDP] TABLE_OFFSET_R Recieve Fail", ex);
                        }
                        break; 

                    #region >> saw manual-run
                    case "StripRailMove":
                        if (!DEF.ChkRecieverManual(nThread)) break;
                        //ManualNumber.RunStripLoading
                        break;

                    #endregion
                    default: break;
                }
            }
            catch (Exception ex){
                LogWR_.SaveLogException("RECIEVE FAIL! (RECEIVE_UDP->RECIEVE)", ex);
            }
        }

        public void ManualSecuss(){
            B.Bit(T.ReceiveSaw, B.SawManualRun, false, "[SECUSS] 메뉴얼 동작 진행 플러그 OFF");
            C.SendSaw.SEND("OK,*");
        }
        public void ManualFail(string msg){
            B.Bit(T.ReceiveSaw, B.SawManualRun, false, "[FAIL] 메뉴얼 동작 진행 플러그 OFF");
            C.SendSaw.SEND("FAIL," + msg + ",*");
        }
    }
    public class SEND_SAW : DATA_{
        public void SEND_EVENT(UdpClient SockSend, byte[] Buffer, int BufferLength){
            SockSend.Send(Buffer, BufferLength);
            SockSend.Close();
        }
        public void SEND(string Massage){
            try{
                if (!mIN[I.SAW_READY]) return;
#if _UDP
                UdpClient SocketSend = new UdpClient(DEF.SawIP, DEF.SawPort);
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] bBuffer = encoding.GetBytes(Massage);
                SEND_EVENT(SocketSend, bBuffer, bBuffer.Length);
#endif
            }
            catch (Exception ex){
                LogWR_.SaveLogException("SAW MESSAGE SEND FAIL! (SEND_UDP->SEND)", ex);
            }
        }
    }
}