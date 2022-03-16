using NSS_3310S;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SYSTEM{
    /// <summary>
    /// SAW PC UDP 통신
    /// SAW PC IP : 192.168.1.2 / PORT : 5000
    /// HANDLER PC IP : 192.168.1.1 / PORT : 5001
    /// </summary>
    public class RECEIVE_SAW : DATA_{
        int nThread = T.ReceiveSaw;
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
                        IsBIT[B.SawRecipeOpen] = true;
                        break;

                    case "PROCESS":
                        string PROCESS_STATE = sRSLT[1];

                        break;

                    case "SVID":
                        int nSVID = 0;
                        string nVALUE = string.Empty;
                        for (int i = 1; i < sRSLT.Length - 1; i++){
                            string[] aSVID = sRSLT[i].Split('=');
                            nSVID = int.Parse(aSVID[0]);
                            nVALUE = aSVID[1];
                            SUBFRM_.gSecsGem.SetSVID(nSVID, nVALUE);
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
                        SUBFRM_.gSecsGem.OnAlarmSet(sErrNum);
                        break;
                    case "ARST": //Alarm Reset
                        string rAlarm = sRSLT[1];
                        int rErrNum = int.Parse(rAlarm);
                        SUBFRM_.gSecsGem.OnAlarmClear(rErrNum);
                        break;

                    case "ALARMT":
                        string tAlarm = sRSLT[1];
                        if (tAlarm == "1") bINTRK[E.emsSawStageThAlarm - eEMSBegin] = true;
                        else bINTRK[E.emsSawStageThAlarm - eEMSBegin] = false;
                        break;

                    case "VACUUM":
                    case "BLOW":
                        IsBIT[B.SawVacuumInterface] = true;
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
            IsBIT[B.SawManualRun] = false;
            C.SendSaw.SEND("OK,*");
        }
        public void ManualFail(string msg){
            IsBIT[B.SawManualRun] = false;
            C.SendSaw.SEND("FAIL," + msg + ",*");
        }
    }
    public class SEND_SAW : DATA_{
        void SEND_EVENT(UdpClient SockSend, byte[] Buffer, int BufferLength){
            SockSend.Send(Buffer, BufferLength);
            SockSend.Close();
        }
        public void SEND(string Massage){
            try{
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

        public void FailSawManualEvent(string msg){

        }
        public void SecussSawManualEvent(string msg){

        }
    }
}