using System;
using System.IO.Ports;

namespace OMRON_{
    public class TEMP_{
        public SerialPort cTEMP;
        public string sRESULT;
        public bool ReturnCheck;

        public void IniPort(int numPort){
            if (cTEMP == null) cTEMP = new SerialPort();
            if (cTEMP.IsOpen) cTEMP.Close();
            else{
                try{
                    string sPORT = "COM" + numPort.ToString();
                    cTEMP.PortName = sPORT;
                    cTEMP.BaudRate = 57600;
                    cTEMP.DataBits = 8;
                    cTEMP.StopBits = StopBits.One;
                    cTEMP.Parity = Parity.None;
                    cTEMP.DataReceived += new SerialDataReceivedEventHandler(TEMP_DataReceived);
                    cTEMP.Open();
                }
                catch (Exception ex) { LogWR_.SaveLogException("IniPort Fail : ", ex); }
                return;
            }
        }

        public bool PortCheck(){
            if (cTEMP == null) return false;
            if (cTEMP.IsOpen) return true;
            return false;
        }

        public void PortClose(){
            if (cTEMP == null) return;
            if (cTEMP.IsOpen) cTEMP.Close();
            sRESULT = "";
            cTEMP = null;
        }

        public void TEMP_DataReceived(object sender, SerialDataReceivedEventArgs args){
            try
            {
                if (cTEMP.BytesToRead < 7) return;
                UTIL_.DELAY(10);
                sRESULT = cTEMP.ReadLine().ToString();
                string sTemp = cTEMP.ReadExisting();
                string[] sLine = sRESULT.Split('\r');
                //foreach (string sline in sLine){
                //    sRESULT = sline;
                //}
                sRESULT = sLine[0];
                ReturnCheck = true;
            }
            catch (SystemException ex) { LogWR_.SaveLogException("BCR_DataReceived Fail : ", ex); }
            return;
        }
    }
}