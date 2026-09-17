using NSS_3310S;
using Object;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SYSTEM{
    public class RECEIVE_VISION : DATA_{
        readonly int nThread = T.ReceiveVision;
        /// <summary>
        /// VISION PC UDP 통신
        /// VISION PC IP : 192.168.1.111 / PORT : 6002
        /// HANDLER PC IP : 192.168.1.110 / PORT : 6000
        /// </summary>
        public void DoReceiveEvent(){
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(DEF.SorterIP), DEF.SorterPort);
            UdpClient newSock = new UdpClient(ipep);

            do{
                if (gExit) break;
                UTIL_.DELAY(3);
                byte[] bReceive = newSock.Receive(ref ipep);
                IsSTRING[S.VisionRecieveMessage] = UnicodeEncoding.ASCII.GetString(bReceive);
                RECIEVE(IsSTRING[S.VisionRecieveMessage]);
            } while (true);
        }
        void RECIEVE(string MSG){
            if (MSG.Length <= 0) return;
            string[] sRSLT = MSG.Split(',');
            eRTN eRETURN;
            try{
                switch (sRSLT[0]){
                    case "X_PITCH":
                        if (!DEF.ChkRecieverManual(nThread)) break;
                        iMANUAL.double_1 = double.Parse(sRSLT[1]);
                        if (iMANUAL.double_1 > 0)       eRETURN = LAB_.MT_PITCH_CW(M.TopVisionX, 5, iMANUAL.double_1);
                        else if (iMANUAL.double_1 < 0)  eRETURN = LAB_.MT_PITCH_CCW(M.TopVisionX, 5, iMANUAL.double_1 * -1);
                        break;
                    case "Z_PITCH":
                        if (!DEF.ChkRecieverManual(nThread)) break;
                        iMANUAL.double_1 = double.Parse(sRSLT[1]);
                        if (iMANUAL.double_1 > 0)       eRETURN = LAB_.MT_PITCH_CW(M.TopVisionZ, 5, iMANUAL.double_1);
                        else if (iMANUAL.double_1 < 0)  eRETURN = LAB_.MT_PITCH_CCW(M.TopVisionZ, 5, iMANUAL.double_1 * -1);
                        break;
                    case "Y1_PITCH":
                        if (!DEF.ChkRecieverManual(nThread)) break;
                        iMANUAL.double_1 = double.Parse(sRSLT[1]);
                        if (iMANUAL.double_1 > 0)       eRETURN = LAB_.MT_PITCH_CW(M.Table1, 5, iMANUAL.double_1);
                        else if (iMANUAL.double_1 < 0)  eRETURN = LAB_.MT_PITCH_CCW(M.Table1, 5, iMANUAL.double_1 * -1);
                        break;
                    case "Y2_PITCH":
                        if (!DEF.ChkRecieverManual(nThread)) break;
                        iMANUAL.double_1 = double.Parse(sRSLT[1]);
                        if (iMANUAL.double_1 > 0)       eRETURN = LAB_.MT_PITCH_CW(M.Table2, 5, iMANUAL.double_1);
                        else if (iMANUAL.double_1 < 0)  eRETURN = LAB_.MT_PITCH_CCW(M.Table2, 5, iMANUAL.double_1 * -1);
                        break;
                    case "MAPBLOCK1_TEACHING":
                        if (!DEF.ChkRecieverManual(nThread)) break;
                        iMANUAL.int_2 = (int)eMAP_BLOCK.STAGE1;
                        iMANUAL.int_3 = 0;
                        iMANUAL.int_4 = 0;
                        iMANUAL.int_5 = 1;
                        iMANUAL.int_6 = 1;
                        iMANUAL.bool_1 = false;
                        COM_.RUN_MANUAL(ManualNumber.TopCamStaeView, "맵-블록 테이블1 카메라 티칭 위치로 이송");
                        break;
                    case "MAPBLOCK2_TEACHING":
                        if (!DEF.ChkRecieverManual(nThread)) break;
                        iMANUAL.int_2 = (int)eMAP_BLOCK.STAGE2;
                        iMANUAL.int_3 = 0;
                        iMANUAL.int_4 = 0;
                        iMANUAL.int_5 = 1;
                        iMANUAL.int_6 = 1;
                        iMANUAL.bool_1 = false;
                        COM_.RUN_MANUAL(ManualNumber.TopCamStaeView, "맵-블록 테이블2 카메라 티칭 위치로 이송");
                        break;

                    case "HEAD1_TEACHING":

                        break;
                    case "HEAD2_TEACHING":

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
                    default: break;
                }
            }
            catch (Exception ex){
                LogWR_.SaveLogException("RECIEVE FAIL! (RECEIVE_VISION->RECIEVE)", ex);
            }
        }

        public void ManualSecuss(){
            B.Bit(T.ReceiveVision, B.VisionManualRun, false, "[SECUSS] 메뉴얼 동작 플러그 OFF");
            C.SendVision.SEND("OK,*");
        }
        public void ManualFail(string msg){
            B.Bit(T.ReceiveVision, B.VisionManualRun, false, "[FAIL] 메뉴얼 동작 플러그 OFF");
            C.SendVision.SEND("FAIL," + msg + ",*");
        }
    }

    public class SEND_VISION{
        public void SEND_EVENT(UdpClient SockSend, byte[] Buffer, int BufferLength){
            SockSend.Send(Buffer, BufferLength);
            SockSend.Close();
        }
        public void SEND(string Massage){
            try{
#if _UDP
                UdpClient SocketSend = new UdpClient(DEF.VisionIP, DEF.VisionPort);
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] bBuffer = encoding.GetBytes(Massage);
                SEND_EVENT(SocketSend, bBuffer, bBuffer.Length);
#endif
            }
            catch (Exception ex){
                LogWR_.SaveLogException("SAW MESSAGE SEND FAIL! (SEND_UDP_VISION->SEND)", ex);
            }
        }
    }
}