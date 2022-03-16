using Object;
using System;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace WSTECH_{
    /// <summary>
    /// 201005 YSM
    /// 원석 하이테그 MPM-330 POWER-METER
    /// RS-485/19200
    /// COM PORT : COM 
    /// </summary>
    /// 
    public class PowerMeter{
        #region Values
        private SerialPort cSerial = null;
        public string reciveData { get; private set; }

        private Thread th_reader = null;
        private bool th_bit = false;
        private bool threadCommandLock = false;

        // Read Status
        public double LineVoltageRS { get; private set; }        // 선간 전압 RS
        public double LineVoltageST { get; private set; }       // 선간 전압 ST
        public double LineVoltageTR { get; private set; }       // 선간 전압 TR
        public double PhaseVoltageR { get; private set; }       // 상 전압 R
        public double PhaseVoltageS { get; private set; }       // 상 전압 S
        public double PhaseVoltageT { get; private set; }       // 상 전압 T
        public double PhaseCurrentR { get; private set; }       // 상 전류 R
        public double PhaseCurrentS { get; private set; }       // 상 전류 S
        public double PhaseCurrentT { get; private set; }       // 상 전류 T
        public double TotalActivePower { get; private set; }    // Total 유효전력
        public double TotalReActivePower { get; private set; }  // Total 무효전력
        public double TotalApparentPower { get; private set; }  // Total 피상전력
        public double Frequency { get; private set; }           // 주파수
        public double AveragePowerfactor { get; private set; }  // 평균역률   
        public double ActivePower { get; private set; }         // 유효전력량
        public double ReActivePower { get; private set; }       // 무효전력량
        public double PeakActivePower { get; private set; }     // 유효전력 Peak
        public double PeakCurrentR { get; private set; }        // 전류R상 Peak
        public double PeakCurrentS { get; private set; }        // 전류S상 Peak
        public double PeakCurrentT { get; private set; }        // 전류T상 Peak
        public double ActivePowerR { get; private set; }        // R상 유효전력
        public double ActivePowerS { get; private set; }        // S상 유효전력
        public double ActivePowerT { get; private set; }        // T상 유효전력
        public double ReActivePowerR { get; private set; }      // R상 무효전력
        public double ReActivePowerS { get; private set; }      // S상 무효전력
        public double ReActivePowerT { get; private set; }      // T상 무효전력
        public double ApparentPowerR { get; private set; }      // R상 피상전력
        public double ApparentPowerS { get; private set; }      // S상 피상전력
        public double ApparentPowerT { get; private set; }      // T상 피상전력  
        public double PowerfactorR { get; private set; }        // R상 역률
        public double PowerfactorS { get; private set; }        // S상 역률
        public double PowerfactorT { get; private set; }        // T상 역률
        #endregion

        public PowerMeter(){
            InitData();
            StartThread_Sender();
        }

        private void InitData(){
            threadCommandLock = false;
        }

        private void StartThread_Sender(){
            if (th_reader == null){
                th_bit = true;
                th_reader = new Thread(RunThread);
                th_reader.IsBackground = true;
                th_reader.Start();
            }
        }
        private void EndThread_Sender(){
            if (th_reader != null){
                th_reader.Abort();
                th_bit = false;
                th_reader = null;
            }
        }
        private void RunThread(){
            while (th_bit){
                lock (this){
                    Thread.Sleep(1);
                    if (IsOpen()){
                        try{
                            if (threadCommandLock == false){
                                ReadAllStatus();
                            }
                        }
                        catch (Exception ex){
                            string exceptionData = ex.Message;
                        }
                    }
                    else{
                        InitData();
                    }
                }
            }
        }

        #region Command
        private int baudConvert(Baudrate baud){
            int result = 9600;
            switch (baud){
                case Baudrate.bps9600: result = 9600; break;
                case Baudrate.bps14400: result = 14400; break;
                case Baudrate.bps19200: result = 19200; break;
                case Baudrate.bps38400: result = 38400; break;
                case Baudrate.bps57600: result = 57600; break;
                case Baudrate.bps115200: result = 115200; break;
                default: break;
            }
            return result;
        }

        public bool Open(int com, Baudrate baud){
            threadCommandLock = true;
            string strCom = "COM" + com.ToString();
            try{
                if (cSerial != null){
                    cSerial.Close();
                    cSerial = null;
                }
                if (IsOpen()) return true;
                InitData();
                cSerial = new SerialPort(strCom);
                cSerial.PortName = strCom;
                cSerial.BaudRate = baudConvert(baud);
                cSerial.DataBits = 8;
                cSerial.StopBits = StopBits.One;
                cSerial.Parity = Parity.None;
                cSerial.DataReceived += new SerialDataReceivedEventHandler(Event_DataRecived);
                cSerial.Open();
            }
            catch (Exception ex){
                string exceptionData = ex.Message;
            }
            threadCommandLock = false;
            return (cSerial == null) ? false : cSerial.IsOpen;
        }

        public bool Open(int com, int baud){
            threadCommandLock = true;
            string strCom = "COM" + com.ToString();
            try{
                if (cSerial != null){
                    cSerial.Close();
                    cSerial = null;
                }
                InitData();
                cSerial = new SerialPort(strCom);
                cSerial.PortName = strCom;
                cSerial.BaudRate = baud;
                cSerial.DataBits = 8;
                cSerial.StopBits = StopBits.One;
                cSerial.Parity = Parity.None;
                cSerial.DataReceived += new SerialDataReceivedEventHandler(Event_DataRecived);
                cSerial.Open();
            }
            catch (Exception ex){
                string exceptionData = ex.Message;
            }
            threadCommandLock = false;
            return (cSerial == null) ? false : cSerial.IsOpen;
        }

        public void Close(){
            threadCommandLock = true;
            if (cSerial != null){
                if (cSerial.IsOpen) cSerial.Close();
                cSerial = null;
            }
            threadCommandLock = false;
            InitData();
        }
        public bool IsOpen() { return (cSerial == null) ? false : cSerial.IsOpen; }
        #endregion

        private void Event_DataRecived(object sender, SerialDataReceivedEventArgs e){
            try{
                //Thread.Sleep(50); // 문자 전체 받을때까지 대기
                //reciveData = ((SerialPort)sender).ReadExisting();
                //string dsfsdgf = ConvertAsciiToHex(reciveData);
            }
            catch (Exception ex){
                string exceptionData = ex.Message;
            }
        }

        private string GetCheckSum(string data){
            if (data.Length <= 0){
                return string.Empty;
            }
            string result = "";
            int reg = 0xffff;
            for (int i = 0; i < data.Length; i++){
                reg ^= data[i];
                for (int j = 0; j < 8; j++){
                    if ((reg & 0x01) == 0){
                        reg = reg >> 1;
                    }
                    else{
                        reg = (reg >> 1) ^ 0xa001; //0x8005 //Ox1021;
                    }
                }
            }
            result = reg.ToString("X4");
            result = result.Substring(2, 2) + result.Substring(0, 2);
            return result;
        }
        private byte[] ConvertHexStringToHexByte(string hex){
            try{
                byte[] result = Enumerable.Range(0, hex.Length)
                                          .Where(x => x % 2 == 0)
                                          .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                                          .ToArray();
                return result;
            }
            catch (Exception ex){
                string exceptionMsg = ex.Message;
                return null;
            }
        }
        private string ConvertHexToAscii(string hex){
            try{
                string ascii = "";
                for (int i = 0; i < hex.Length; i += 2){
                    String hs = string.Empty;

                    hs = hex.Substring(i, 2);
                    uint decval = Convert.ToUInt32(hs, 16);
                    char character = Convert.ToChar(decval);
                    ascii += character;
                }
                return ascii;
            }
            catch (Exception ex){
                string exceptionMsg = ex.Message;
                return string.Empty;
            }
        }
        private string ConvertAsciiToHex(string ascii){
            try{
                string result = "";
                StringBuilder sb = new StringBuilder();
                foreach (char item in ascii){
                    sb.AppendFormat("{0:X2}", (int)item);
                }
                result = sb.ToString().Trim();
                return result;
            }
            catch (Exception ex){
                string exceptionMsg = ex.Message;
                return string.Empty;
            }
        }

        private bool SendHex(string hexString){
            try{
                if ((hexString.Length % 2) != 0) return false;
                byte[] byteToSend = new byte[hexString.Length / 2];
                byteToSend = ConvertHexStringToHexByte(hexString);
                cSerial.Write(byteToSend, 0, byteToSend.Length);
                return true;
            }
            catch (Exception ex){
                string exceptionMsg = ex.Message;
                return false;
            }
        }

        private bool ReadAllStatus(){
            int readAddressCount = 30;
            int oneDataSize = 4;
            int dataLength = 2;
            string addCount = readAddressCount.ToString("X4");
            string startAdd = "0000";
            string command = "0103" + startAdd + addCount;
            //string command = "0103" + "0000" + "001E";
            string CRC = GetCheckSum(ConvertHexToAscii(command));
            string recive = "";

            if (IsOpen() == false) return false;
            try{
                if (SendHex(command + CRC) == false) return false;
                
                Thread.Sleep(100);
                recive = cSerial.ReadExisting();
                recive = ConvertAsciiToHex(recive);

                if (recive.Length >=
                    (readAddressCount * oneDataSize)  // 받을 데이터 갯수
                    + dataLength                      // 데이터 갯수 피드백
                    + startAdd.Length                 // 커맨드 피드백
                    + 4 // CRC
                    )
                {
                    // send format
                    // 명령어4자리 + 시작주소4자리 + 읽을테이터 갯수4자리(30이최대) + CRC4자리

                    // return format 
                    // 명령어4자리 + 바이트갯수2자리 + 4자리씩해당 번지 데이터 + CRC4자리

                    // Sample
                    // 보내는 명령-> 0103 0000 001E C5CD
                    // 받는 명령  -> 0103 3C /0000/0000/ 7A3F

                    int startIndex = 4 + dataLength; // command4 + dataLength2 - 1(Index Start From 0)
                    string onDataString = string.Empty;
                    int onDataInt = 0;

                    // Address 40001 선간전압(RS)
                    onDataString = recive.Substring(startIndex + (0 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    LineVoltageRS = onDataInt / 100.0;

                    // Address 40002 선간전압(ST)                                                
                    onDataString = recive.Substring(startIndex + (1 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    LineVoltageST = onDataInt / 100.0;

                    // Address 40003 선간전압(TR)                                                
                    onDataString = recive.Substring(startIndex + (2 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    LineVoltageTR = onDataInt / 100.0;

                    // Address 40004 상 전압(R)                                                  
                    onDataString = recive.Substring(startIndex + (3 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    PhaseVoltageR = onDataInt / 100.0;

                    // Address 40005 상 전압(S)                                                  
                    onDataString = recive.Substring(startIndex + (4 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    PhaseVoltageS = onDataInt / 100.0;

                    // Address 40006 상 전압(T)                                                  
                    onDataString = recive.Substring(startIndex + (5 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    PhaseVoltageT = onDataInt / 100.0;

                    // Address 40007 상 전류(R)                                                  
                    onDataString = recive.Substring(startIndex + (6 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    PhaseCurrentR = onDataInt / 1000.0;

                    // Address 40008 상 전류(S)
                    onDataString = recive.Substring(startIndex + (7 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    PhaseCurrentS = onDataInt / 1000.0;

                    // Address 40009 상 전류(T)
                    onDataString = recive.Substring(startIndex + (8 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    PhaseCurrentT = onDataInt / 1000.0;

                    // Address 40010 Total 유효전력
                    onDataString = recive.Substring(startIndex + (9 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    TotalActivePower = onDataInt / 1000.0;

                    // Address 40011 Total 무효전력
                    onDataString = recive.Substring(startIndex + (10 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    TotalReActivePower = onDataInt / 1000.0;

                    // Address 40012 Total 피상전력
                    onDataString = recive.Substring(startIndex + (11 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    TotalApparentPower = onDataInt / 1000.0;

                    // Address 40013 주파수
                    onDataString = recive.Substring(startIndex + (12 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    Frequency = onDataInt / 10.0;

                    // Address 40014 평균역률
                    onDataString = recive.Substring(startIndex + (13 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    AveragePowerfactor = onDataInt / 100.0;

                    // Address 40015 유효전력량
                    onDataString = recive.Substring(startIndex + (14 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    ActivePower = onDataInt;

                    // Address 40016 무효전력량
                    onDataString = recive.Substring(startIndex + (15 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    ReActivePower = onDataInt;

                    // Address 40017 유효전력 Peak
                    onDataString = recive.Substring(startIndex + (16 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    PeakActivePower = onDataInt / 1000.0;

                    // Address 40018 전류R상 Peak
                    onDataString = recive.Substring(startIndex + (17 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    PeakCurrentR = onDataInt / 1000.0;

                    // Address 40019 전류S상 Peak
                    onDataString = recive.Substring(startIndex + (18 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    PeakCurrentS = onDataInt / 1000.0;

                    // Address 40020 전류T상 Peak
                    onDataString = recive.Substring(startIndex + (19 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    PeakCurrentT = onDataInt / 1000.0;

                    // Address 40021 R상 유효전력
                    onDataString = recive.Substring(startIndex + (20 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    ActivePowerR = onDataInt / 1000.0;

                    // Address 40022 S상 유효전력
                    onDataString = recive.Substring(startIndex + (21 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    ActivePowerS = onDataInt / 1000.0;

                    // Address 40023 T상 유효전력
                    onDataString = recive.Substring(startIndex + (22 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    ActivePowerT = onDataInt / 1000.0;

                    // Address 40024 R상 무효전력
                    onDataString = recive.Substring(startIndex + (23 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    ReActivePowerR = onDataInt / 1000.0;

                    // Address 40025 S상 무효전력
                    onDataString = recive.Substring(startIndex + (24 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    ReActivePowerS = onDataInt / 1000.0;

                    // Address 40026 T상 무효전력
                    onDataString = recive.Substring(startIndex + (25 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    ReActivePowerT = onDataInt / 1000.0;

                    // Address 40027 R상 피상전력
                    onDataString = recive.Substring(startIndex + (26 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    ApparentPowerR = onDataInt / 1000.0;

                    // Address 40028 S상 피상전력
                    onDataString = recive.Substring(startIndex + (27 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    ApparentPowerS = onDataInt / 1000.0;

                    // Address 40029 T상 피상전력
                    onDataString = recive.Substring(startIndex + (28 * oneDataSize), oneDataSize);
                    onDataInt = Convert.ToInt32(onDataString, 16);
                    ApparentPowerT = onDataInt / 1000.0;

                    //// Address 40030 선간전압(TR)
                    //onDataString = recive.Substring(startIndex + (29 * oneDataSize), oneDataSize);
                    //onDataInt = Convert.ToInt32(onDataString, 16);
                    //STATE_FREQUENCY_COMMAND = onDataInt / 100.0;
                }
            }
            catch (Exception ex){
                string exceptionMsg = ex.Message;
                return false;
            }
            return true;
        }
    }
}